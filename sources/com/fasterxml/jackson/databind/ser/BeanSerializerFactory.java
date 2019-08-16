package com.fasterxml.jackson.databind.ser;

import com.fasterxml.jackson.annotation.JsonTypeInfo.C0861As;
import com.fasterxml.jackson.annotation.ObjectIdGenerator;
import com.fasterxml.jackson.annotation.ObjectIdGenerators.PropertyGenerator;
import com.fasterxml.jackson.databind.AnnotationIntrospector;
import com.fasterxml.jackson.databind.AnnotationIntrospector.ReferenceProperty;
import com.fasterxml.jackson.databind.BeanDescription;
import com.fasterxml.jackson.databind.BeanProperty.Std;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonSerializer;
import com.fasterxml.jackson.databind.MapperFeature;
import com.fasterxml.jackson.databind.PropertyMetadata;
import com.fasterxml.jackson.databind.PropertyName;
import com.fasterxml.jackson.databind.SerializationConfig;
import com.fasterxml.jackson.databind.SerializerProvider;
import com.fasterxml.jackson.databind.cfg.SerializerFactoryConfig;
import com.fasterxml.jackson.databind.introspect.AnnotatedField;
import com.fasterxml.jackson.databind.introspect.AnnotatedMember;
import com.fasterxml.jackson.databind.introspect.AnnotatedMethod;
import com.fasterxml.jackson.databind.introspect.BeanPropertyDefinition;
import com.fasterxml.jackson.databind.introspect.ObjectIdInfo;
import com.fasterxml.jackson.databind.jsontype.TypeResolverBuilder;
import com.fasterxml.jackson.databind.jsontype.TypeSerializer;
import com.fasterxml.jackson.databind.ser.impl.FilteredBeanPropertyWriter;
import com.fasterxml.jackson.databind.ser.impl.ObjectIdWriter;
import com.fasterxml.jackson.databind.ser.impl.PropertyBasedObjectIdGenerator;
import com.fasterxml.jackson.databind.ser.std.AtomicReferenceSerializer;
import com.fasterxml.jackson.databind.ser.std.MapSerializer;
import com.fasterxml.jackson.databind.ser.std.StdDelegatingSerializer;
import com.fasterxml.jackson.databind.type.ReferenceType;
import com.fasterxml.jackson.databind.util.ArrayBuilders;
import com.fasterxml.jackson.databind.util.ClassUtil;
import com.fasterxml.jackson.databind.util.Converter;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.List;
import java.util.concurrent.atomic.AtomicReference;

public class BeanSerializerFactory extends BasicSerializerFactory implements Serializable {
    public static final BeanSerializerFactory instance = new BeanSerializerFactory(null);
    private static final long serialVersionUID = 1;

    protected BeanSerializerFactory(SerializerFactoryConfig serializerFactoryConfig) {
        super(serializerFactoryConfig);
    }

    public SerializerFactory withConfig(SerializerFactoryConfig serializerFactoryConfig) {
        if (this._factoryConfig == serializerFactoryConfig) {
            return this;
        }
        if (getClass() == BeanSerializerFactory.class) {
            return new BeanSerializerFactory(serializerFactoryConfig);
        }
        throw new IllegalStateException("Subtype of BeanSerializerFactory (" + getClass().getName() + ") has not properly overridden method 'withAdditionalSerializers': can not instantiate subtype with " + "additional serializer definitions");
    }

    /* access modifiers changed from: protected */
    public Iterable<Serializers> customSerializers() {
        return this._factoryConfig.serializers();
    }

    public JsonSerializer<Object> createSerializer(SerializerProvider serializerProvider, JavaType javaType) throws JsonMappingException {
        boolean z;
        JsonSerializer jsonSerializer;
        SerializationConfig config = serializerProvider.getConfig();
        BeanDescription introspect = config.introspect(javaType);
        JsonSerializer findSerializerFromAnnotation = findSerializerFromAnnotation(serializerProvider, introspect.getClassInfo());
        if (findSerializerFromAnnotation != null) {
            return findSerializerFromAnnotation;
        }
        AnnotationIntrospector annotationIntrospector = config.getAnnotationIntrospector();
        JavaType refineSerializationType = annotationIntrospector == null ? javaType : annotationIntrospector.refineSerializationType(config, introspect.getClassInfo(), javaType);
        if (refineSerializationType == javaType) {
            z = false;
        } else if (!refineSerializationType.hasRawClass(javaType.getRawClass())) {
            introspect = config.introspect(refineSerializationType);
            z = true;
        } else {
            z = true;
        }
        Converter findSerializationConverter = introspect.findSerializationConverter();
        if (findSerializationConverter == null) {
            return _createSerializer2(serializerProvider, refineSerializationType, introspect, z);
        }
        JavaType outputType = findSerializationConverter.getOutputType(serializerProvider.getTypeFactory());
        if (!outputType.hasRawClass(refineSerializationType.getRawClass())) {
            introspect = config.introspect(outputType);
            jsonSerializer = findSerializerFromAnnotation(serializerProvider, introspect.getClassInfo());
        } else {
            jsonSerializer = findSerializerFromAnnotation;
        }
        if (jsonSerializer == null && !outputType.isJavaLangObject()) {
            jsonSerializer = _createSerializer2(serializerProvider, outputType, introspect, true);
        }
        return new StdDelegatingSerializer(findSerializationConverter, outputType, jsonSerializer);
    }

    /* access modifiers changed from: protected */
    public JsonSerializer<?> _createSerializer2(SerializerProvider serializerProvider, JavaType javaType, BeanDescription beanDescription, boolean z) throws JsonMappingException {
        JsonSerializer<?> jsonSerializer = null;
        SerializationConfig config = serializerProvider.getConfig();
        if (javaType.isContainerType()) {
            if (!z) {
                z = usesStaticTyping(config, beanDescription, null);
            }
            jsonSerializer = buildContainerSerializer(serializerProvider, javaType, beanDescription, z);
            if (jsonSerializer != null) {
                return jsonSerializer;
            }
        } else {
            if (!javaType.isReferenceType()) {
                for (Serializers findSerializer : customSerializers()) {
                    jsonSerializer = findSerializer.findSerializer(config, javaType, beanDescription);
                    if (jsonSerializer != null) {
                        break;
                    }
                }
            } else {
                jsonSerializer = findReferenceSerializer(serializerProvider, (ReferenceType) javaType, beanDescription, z);
            }
            if (jsonSerializer == null) {
                jsonSerializer = findSerializerByAnnotations(serializerProvider, javaType, beanDescription);
            }
        }
        if (jsonSerializer == null) {
            jsonSerializer = findSerializerByLookup(javaType, config, beanDescription, z);
            if (jsonSerializer == null) {
                jsonSerializer = findSerializerByPrimaryType(serializerProvider, javaType, beanDescription, z);
                if (jsonSerializer == null) {
                    jsonSerializer = findBeanSerializer(serializerProvider, javaType, beanDescription);
                    if (jsonSerializer == null) {
                        jsonSerializer = findSerializerByAddonType(config, javaType, beanDescription, z);
                        if (jsonSerializer == null) {
                            jsonSerializer = serializerProvider.getUnknownTypeSerializer(beanDescription.getBeanClass());
                        }
                    }
                }
            }
        }
        if (jsonSerializer == null || !this._factoryConfig.hasSerializerModifiers()) {
            return jsonSerializer;
        }
        Iterator it = this._factoryConfig.serializerModifiers().iterator();
        while (true) {
            JsonSerializer jsonSerializer2 = jsonSerializer;
            if (!it.hasNext()) {
                return jsonSerializer2;
            }
            jsonSerializer = ((BeanSerializerModifier) it.next()).modifySerializer(config, beanDescription, jsonSerializer2);
        }
    }

    public JsonSerializer<Object> findBeanSerializer(SerializerProvider serializerProvider, JavaType javaType, BeanDescription beanDescription) throws JsonMappingException {
        if (isPotentialBeanType(javaType.getRawClass()) || javaType.isEnumType()) {
            return constructBeanSerializer(serializerProvider, beanDescription);
        }
        return null;
    }

    public JsonSerializer<?> findReferenceSerializer(SerializerProvider serializerProvider, ReferenceType referenceType, BeanDescription beanDescription, boolean z) throws JsonMappingException {
        TypeSerializer typeSerializer;
        JavaType contentType = referenceType.getContentType();
        TypeSerializer typeSerializer2 = (TypeSerializer) contentType.getTypeHandler();
        SerializationConfig config = serializerProvider.getConfig();
        if (typeSerializer2 == null) {
            typeSerializer = createTypeSerializer(config, contentType);
        } else {
            typeSerializer = typeSerializer2;
        }
        JsonSerializer jsonSerializer = (JsonSerializer) contentType.getValueHandler();
        for (Serializers findReferenceSerializer : customSerializers()) {
            JsonSerializer<?> findReferenceSerializer2 = findReferenceSerializer.findReferenceSerializer(config, referenceType, beanDescription, typeSerializer, jsonSerializer);
            if (findReferenceSerializer2 != null) {
                return findReferenceSerializer2;
            }
        }
        if (referenceType.isTypeOrSubTypeOf(AtomicReference.class)) {
            return new AtomicReferenceSerializer(referenceType, z, typeSerializer, jsonSerializer);
        }
        return null;
    }

    public TypeSerializer findPropertyTypeSerializer(JavaType javaType, SerializationConfig serializationConfig, AnnotatedMember annotatedMember) throws JsonMappingException {
        TypeResolverBuilder findPropertyTypeResolver = serializationConfig.getAnnotationIntrospector().findPropertyTypeResolver(serializationConfig, annotatedMember, javaType);
        if (findPropertyTypeResolver == null) {
            return createTypeSerializer(serializationConfig, javaType);
        }
        return findPropertyTypeResolver.buildTypeSerializer(serializationConfig, javaType, serializationConfig.getSubtypeResolver().collectAndResolveSubtypesByClass(serializationConfig, annotatedMember, javaType));
    }

    public TypeSerializer findPropertyContentTypeSerializer(JavaType javaType, SerializationConfig serializationConfig, AnnotatedMember annotatedMember) throws JsonMappingException {
        JavaType contentType = javaType.getContentType();
        TypeResolverBuilder findPropertyContentTypeResolver = serializationConfig.getAnnotationIntrospector().findPropertyContentTypeResolver(serializationConfig, annotatedMember, javaType);
        if (findPropertyContentTypeResolver == null) {
            return createTypeSerializer(serializationConfig, contentType);
        }
        return findPropertyContentTypeResolver.buildTypeSerializer(serializationConfig, contentType, serializationConfig.getSubtypeResolver().collectAndResolveSubtypesByClass(serializationConfig, annotatedMember, contentType));
    }

    /* access modifiers changed from: protected */
    public JsonSerializer<Object> constructBeanSerializer(SerializerProvider serializerProvider, BeanDescription beanDescription) throws JsonMappingException {
        List removeOverlappingTypeIds;
        List list;
        List list2;
        BeanSerializerBuilder beanSerializerBuilder;
        JsonSerializer jsonSerializer;
        if (beanDescription.getBeanClass() == Object.class) {
            return serializerProvider.getUnknownTypeSerializer(Object.class);
        }
        SerializationConfig config = serializerProvider.getConfig();
        BeanSerializerBuilder constructBeanSerializerBuilder = constructBeanSerializerBuilder(beanDescription);
        constructBeanSerializerBuilder.setConfig(config);
        List findBeanProperties = findBeanProperties(serializerProvider, beanDescription, constructBeanSerializerBuilder);
        if (findBeanProperties == null) {
            removeOverlappingTypeIds = new ArrayList();
        } else {
            removeOverlappingTypeIds = removeOverlappingTypeIds(serializerProvider, beanDescription, constructBeanSerializerBuilder, findBeanProperties);
        }
        serializerProvider.getAnnotationIntrospector().findAndAddVirtualProperties(config, beanDescription.getClassInfo(), removeOverlappingTypeIds);
        if (this._factoryConfig.hasSerializerModifiers()) {
            Iterator it = this._factoryConfig.serializerModifiers().iterator();
            while (true) {
                list = removeOverlappingTypeIds;
                if (!it.hasNext()) {
                    break;
                }
                removeOverlappingTypeIds = ((BeanSerializerModifier) it.next()).changeProperties(config, beanDescription, list);
            }
        } else {
            list = removeOverlappingTypeIds;
        }
        List filterBeanProperties = filterBeanProperties(config, beanDescription, list);
        if (this._factoryConfig.hasSerializerModifiers()) {
            Iterator it2 = this._factoryConfig.serializerModifiers().iterator();
            while (true) {
                list2 = filterBeanProperties;
                if (!it2.hasNext()) {
                    break;
                }
                filterBeanProperties = ((BeanSerializerModifier) it2.next()).orderProperties(config, beanDescription, list2);
            }
        } else {
            list2 = filterBeanProperties;
        }
        constructBeanSerializerBuilder.setObjectIdWriter(constructObjectIdHandler(serializerProvider, beanDescription, list2));
        constructBeanSerializerBuilder.setProperties(list2);
        constructBeanSerializerBuilder.setFilterId(findFilterId(config, beanDescription));
        AnnotatedMember findAnyGetter = beanDescription.findAnyGetter();
        if (findAnyGetter != null) {
            if (config.canOverrideAccessModifiers()) {
                findAnyGetter.fixAccess(config.isEnabled(MapperFeature.OVERRIDE_PUBLIC_ACCESS_MODIFIERS));
            }
            JavaType type = findAnyGetter.getType();
            boolean isEnabled = config.isEnabled(MapperFeature.USE_STATIC_TYPING);
            JavaType contentType = type.getContentType();
            TypeSerializer createTypeSerializer = createTypeSerializer(config, contentType);
            JsonSerializer findSerializerFromAnnotation = findSerializerFromAnnotation(serializerProvider, findAnyGetter);
            if (findSerializerFromAnnotation == null) {
                jsonSerializer = MapSerializer.construct(null, type, isEnabled, createTypeSerializer, null, null, null);
            } else {
                jsonSerializer = findSerializerFromAnnotation;
            }
            constructBeanSerializerBuilder.setAnyGetter(new AnyGetterWriter(new Std(PropertyName.construct(findAnyGetter.getName()), contentType, (PropertyName) null, beanDescription.getClassAnnotations(), findAnyGetter, PropertyMetadata.STD_OPTIONAL), findAnyGetter, jsonSerializer));
        }
        processViews(config, constructBeanSerializerBuilder);
        if (this._factoryConfig.hasSerializerModifiers()) {
            beanSerializerBuilder = constructBeanSerializerBuilder;
            for (BeanSerializerModifier updateBuilder : this._factoryConfig.serializerModifiers()) {
                beanSerializerBuilder = updateBuilder.updateBuilder(config, beanDescription, beanSerializerBuilder);
            }
        } else {
            beanSerializerBuilder = constructBeanSerializerBuilder;
        }
        JsonSerializer<Object> build = beanSerializerBuilder.build();
        if (build != null || !beanDescription.hasKnownClassAnnotations()) {
            return build;
        }
        return beanSerializerBuilder.createDummy();
    }

    /* access modifiers changed from: protected */
    public ObjectIdWriter constructObjectIdHandler(SerializerProvider serializerProvider, BeanDescription beanDescription, List<BeanPropertyWriter> list) throws JsonMappingException {
        ObjectIdInfo objectIdInfo = beanDescription.getObjectIdInfo();
        if (objectIdInfo == null) {
            return null;
        }
        Class<PropertyGenerator> generatorType = objectIdInfo.getGeneratorType();
        if (generatorType == PropertyGenerator.class) {
            String simpleName = objectIdInfo.getPropertyName().getSimpleName();
            int size = list.size();
            for (int i = 0; i != size; i++) {
                BeanPropertyWriter beanPropertyWriter = (BeanPropertyWriter) list.get(i);
                if (simpleName.equals(beanPropertyWriter.getName())) {
                    if (i > 0) {
                        list.remove(i);
                        list.add(0, beanPropertyWriter);
                    }
                    return ObjectIdWriter.construct(beanPropertyWriter.getType(), (PropertyName) null, (ObjectIdGenerator<?>) new PropertyBasedObjectIdGenerator<Object>(objectIdInfo, beanPropertyWriter), objectIdInfo.getAlwaysAsId());
                }
            }
            throw new IllegalArgumentException("Invalid Object Id definition for " + beanDescription.getBeanClass().getName() + ": can not find property with name '" + simpleName + "'");
        }
        return ObjectIdWriter.construct(serializerProvider.getTypeFactory().findTypeParameters(serializerProvider.constructType(generatorType), ObjectIdGenerator.class)[0], objectIdInfo.getPropertyName(), serializerProvider.objectIdGeneratorInstance(beanDescription.getClassInfo(), objectIdInfo), objectIdInfo.getAlwaysAsId());
    }

    /* access modifiers changed from: protected */
    public BeanPropertyWriter constructFilteredBeanWriter(BeanPropertyWriter beanPropertyWriter, Class<?>[] clsArr) {
        return FilteredBeanPropertyWriter.constructViewBased(beanPropertyWriter, clsArr);
    }

    /* access modifiers changed from: protected */
    public PropertyBuilder constructPropertyBuilder(SerializationConfig serializationConfig, BeanDescription beanDescription) {
        return new PropertyBuilder(serializationConfig, beanDescription);
    }

    /* access modifiers changed from: protected */
    public BeanSerializerBuilder constructBeanSerializerBuilder(BeanDescription beanDescription) {
        return new BeanSerializerBuilder(beanDescription);
    }

    /* access modifiers changed from: protected */
    public boolean isPotentialBeanType(Class<?> cls) {
        return ClassUtil.canBeABeanType(cls) == null && !ClassUtil.isProxyType(cls);
    }

    /* access modifiers changed from: protected */
    public List<BeanPropertyWriter> findBeanProperties(SerializerProvider serializerProvider, BeanDescription beanDescription, BeanSerializerBuilder beanSerializerBuilder) throws JsonMappingException {
        boolean z;
        List<BeanPropertyDefinition> findProperties = beanDescription.findProperties();
        SerializationConfig config = serializerProvider.getConfig();
        removeIgnorableTypes(config, beanDescription, findProperties);
        if (config.isEnabled(MapperFeature.REQUIRE_SETTERS_FOR_GETTERS)) {
            removeSetterlessGetters(config, beanDescription, findProperties);
        }
        if (findProperties.isEmpty()) {
            return null;
        }
        boolean usesStaticTyping = usesStaticTyping(config, beanDescription, null);
        PropertyBuilder constructPropertyBuilder = constructPropertyBuilder(config, beanDescription);
        ArrayList arrayList = new ArrayList(findProperties.size());
        boolean canOverrideAccessModifiers = config.canOverrideAccessModifiers();
        if (!canOverrideAccessModifiers || !config.isEnabled(MapperFeature.OVERRIDE_PUBLIC_ACCESS_MODIFIERS)) {
            z = false;
        } else {
            z = true;
        }
        for (BeanPropertyDefinition beanPropertyDefinition : findProperties) {
            AnnotatedMember accessor = beanPropertyDefinition.getAccessor();
            if (!beanPropertyDefinition.isTypeId()) {
                ReferenceProperty findReferenceType = beanPropertyDefinition.findReferenceType();
                if (findReferenceType == null || !findReferenceType.isBackReference()) {
                    if (accessor instanceof AnnotatedMethod) {
                        arrayList.add(_constructWriter(serializerProvider, beanPropertyDefinition, constructPropertyBuilder, usesStaticTyping, (AnnotatedMethod) accessor));
                    } else {
                        arrayList.add(_constructWriter(serializerProvider, beanPropertyDefinition, constructPropertyBuilder, usesStaticTyping, (AnnotatedField) accessor));
                    }
                }
            } else if (accessor != null) {
                if (canOverrideAccessModifiers) {
                    accessor.fixAccess(z);
                }
                beanSerializerBuilder.setTypeId(accessor);
            }
        }
        return arrayList;
    }

    /* access modifiers changed from: protected */
    public List<BeanPropertyWriter> filterBeanProperties(SerializationConfig serializationConfig, BeanDescription beanDescription, List<BeanPropertyWriter> list) {
        String[] findPropertiesToIgnore = serializationConfig.getAnnotationIntrospector().findPropertiesToIgnore(beanDescription.getClassInfo(), true);
        if (findPropertiesToIgnore != null && findPropertiesToIgnore.length > 0) {
            HashSet arrayToSet = ArrayBuilders.arrayToSet(findPropertiesToIgnore);
            Iterator it = list.iterator();
            while (it.hasNext()) {
                if (arrayToSet.contains(((BeanPropertyWriter) it.next()).getName())) {
                    it.remove();
                }
            }
        }
        return list;
    }

    /* access modifiers changed from: protected */
    public void processViews(SerializationConfig serializationConfig, BeanSerializerBuilder beanSerializerBuilder) {
        List properties = beanSerializerBuilder.getProperties();
        boolean isEnabled = serializationConfig.isEnabled(MapperFeature.DEFAULT_VIEW_INCLUSION);
        int size = properties.size();
        BeanPropertyWriter[] beanPropertyWriterArr = new BeanPropertyWriter[size];
        int i = 0;
        int i2 = 0;
        while (i < size) {
            BeanPropertyWriter beanPropertyWriter = (BeanPropertyWriter) properties.get(i);
            Class[] views = beanPropertyWriter.getViews();
            if (views != null) {
                i2++;
                beanPropertyWriterArr[i] = constructFilteredBeanWriter(beanPropertyWriter, views);
            } else if (isEnabled) {
                beanPropertyWriterArr[i] = beanPropertyWriter;
            }
            i++;
            i2 = i2;
        }
        if (!isEnabled || i2 != 0) {
            beanSerializerBuilder.setFilteredProperties(beanPropertyWriterArr);
        }
    }

    /* access modifiers changed from: protected */
    public void removeIgnorableTypes(SerializationConfig serializationConfig, BeanDescription beanDescription, List<BeanPropertyDefinition> list) {
        AnnotationIntrospector annotationIntrospector = serializationConfig.getAnnotationIntrospector();
        HashMap hashMap = new HashMap();
        Iterator it = list.iterator();
        while (it.hasNext()) {
            AnnotatedMember accessor = ((BeanPropertyDefinition) it.next()).getAccessor();
            if (accessor == null) {
                it.remove();
            } else {
                Class rawType = accessor.getRawType();
                Boolean bool = (Boolean) hashMap.get(rawType);
                if (bool == null) {
                    bool = annotationIntrospector.isIgnorableType(serializationConfig.introspectClassAnnotations(rawType).getClassInfo());
                    if (bool == null) {
                        bool = Boolean.FALSE;
                    }
                    hashMap.put(rawType, bool);
                }
                if (bool.booleanValue()) {
                    it.remove();
                }
            }
        }
    }

    /* access modifiers changed from: protected */
    public void removeSetterlessGetters(SerializationConfig serializationConfig, BeanDescription beanDescription, List<BeanPropertyDefinition> list) {
        Iterator it = list.iterator();
        while (it.hasNext()) {
            BeanPropertyDefinition beanPropertyDefinition = (BeanPropertyDefinition) it.next();
            if (!beanPropertyDefinition.couldDeserialize() && !beanPropertyDefinition.isExplicitlyIncluded()) {
                it.remove();
            }
        }
    }

    /* access modifiers changed from: protected */
    public List<BeanPropertyWriter> removeOverlappingTypeIds(SerializerProvider serializerProvider, BeanDescription beanDescription, BeanSerializerBuilder beanSerializerBuilder, List<BeanPropertyWriter> list) {
        int size = list.size();
        for (int i = 0; i < size; i++) {
            BeanPropertyWriter beanPropertyWriter = (BeanPropertyWriter) list.get(i);
            TypeSerializer typeSerializer = beanPropertyWriter.getTypeSerializer();
            if (typeSerializer != null && typeSerializer.getTypeInclusion() == C0861As.EXTERNAL_PROPERTY) {
                PropertyName construct = PropertyName.construct(typeSerializer.getPropertyName());
                Iterator it = list.iterator();
                while (true) {
                    if (!it.hasNext()) {
                        break;
                    }
                    BeanPropertyWriter beanPropertyWriter2 = (BeanPropertyWriter) it.next();
                    if (beanPropertyWriter2 != beanPropertyWriter && beanPropertyWriter2.wouldConflictWithName(construct)) {
                        beanPropertyWriter.assignTypeSerializer(null);
                        break;
                    }
                }
            }
        }
        return list;
    }

    /* access modifiers changed from: protected */
    public BeanPropertyWriter _constructWriter(SerializerProvider serializerProvider, BeanPropertyDefinition beanPropertyDefinition, PropertyBuilder propertyBuilder, boolean z, AnnotatedMember annotatedMember) throws JsonMappingException {
        PropertyName fullName = beanPropertyDefinition.getFullName();
        if (serializerProvider.canOverrideAccessModifiers()) {
            annotatedMember.fixAccess(serializerProvider.isEnabled(MapperFeature.OVERRIDE_PUBLIC_ACCESS_MODIFIERS));
        }
        JavaType type = annotatedMember.getType();
        Std std = new Std(fullName, type, beanPropertyDefinition.getWrapperName(), propertyBuilder.getClassAnnotations(), annotatedMember, beanPropertyDefinition.getMetadata());
        JsonSerializer findSerializerFromAnnotation = findSerializerFromAnnotation(serializerProvider, annotatedMember);
        if (findSerializerFromAnnotation instanceof ResolvableSerializer) {
            ((ResolvableSerializer) findSerializerFromAnnotation).resolve(serializerProvider);
        }
        JsonSerializer handlePrimaryContextualization = serializerProvider.handlePrimaryContextualization(findSerializerFromAnnotation, std);
        TypeSerializer typeSerializer = null;
        if (type.isContainerType() || type.isReferenceType()) {
            typeSerializer = findPropertyContentTypeSerializer(type, serializerProvider.getConfig(), annotatedMember);
        }
        return propertyBuilder.buildWriter(serializerProvider, beanPropertyDefinition, type, handlePrimaryContextualization, findPropertyTypeSerializer(type, serializerProvider.getConfig(), annotatedMember), typeSerializer, annotatedMember, z);
    }
}
