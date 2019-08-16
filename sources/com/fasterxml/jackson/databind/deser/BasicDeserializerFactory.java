package com.fasterxml.jackson.databind.deser;

import com.fasterxml.jackson.annotation.JsonCreator.Mode;
import com.fasterxml.jackson.core.JsonLocation;
import com.fasterxml.jackson.databind.AbstractTypeResolver;
import com.fasterxml.jackson.databind.AnnotationIntrospector;
import com.fasterxml.jackson.databind.BeanDescription;
import com.fasterxml.jackson.databind.BeanProperty.Std;
import com.fasterxml.jackson.databind.DeserializationConfig;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.KeyDeserializer;
import com.fasterxml.jackson.databind.MapperFeature;
import com.fasterxml.jackson.databind.PropertyMetadata;
import com.fasterxml.jackson.databind.PropertyName;
import com.fasterxml.jackson.databind.cfg.DeserializerFactoryConfig;
import com.fasterxml.jackson.databind.cfg.HandlerInstantiator;
import com.fasterxml.jackson.databind.cfg.MapperConfig;
import com.fasterxml.jackson.databind.deser.impl.CreatorCollector;
import com.fasterxml.jackson.databind.deser.std.ArrayBlockingQueueDeserializer;
import com.fasterxml.jackson.databind.deser.std.AtomicReferenceDeserializer;
import com.fasterxml.jackson.databind.deser.std.CollectionDeserializer;
import com.fasterxml.jackson.databind.deser.std.DateDeserializers;
import com.fasterxml.jackson.databind.deser.std.EnumDeserializer;
import com.fasterxml.jackson.databind.deser.std.EnumSetDeserializer;
import com.fasterxml.jackson.databind.deser.std.JdkDeserializers;
import com.fasterxml.jackson.databind.deser.std.JsonLocationInstantiator;
import com.fasterxml.jackson.databind.deser.std.JsonNodeDeserializer;
import com.fasterxml.jackson.databind.deser.std.MapEntryDeserializer;
import com.fasterxml.jackson.databind.deser.std.NumberDeserializers;
import com.fasterxml.jackson.databind.deser.std.ObjectArrayDeserializer;
import com.fasterxml.jackson.databind.deser.std.PrimitiveArrayDeserializers;
import com.fasterxml.jackson.databind.deser.std.StdKeyDeserializers;
import com.fasterxml.jackson.databind.deser.std.StringArrayDeserializer;
import com.fasterxml.jackson.databind.deser.std.StringCollectionDeserializer;
import com.fasterxml.jackson.databind.deser.std.StringDeserializer;
import com.fasterxml.jackson.databind.deser.std.TokenBufferDeserializer;
import com.fasterxml.jackson.databind.deser.std.UntypedObjectDeserializer;
import com.fasterxml.jackson.databind.ext.OptionalHandlerFactory;
import com.fasterxml.jackson.databind.introspect.Annotated;
import com.fasterxml.jackson.databind.introspect.AnnotatedClass;
import com.fasterxml.jackson.databind.introspect.AnnotatedConstructor;
import com.fasterxml.jackson.databind.introspect.AnnotatedMember;
import com.fasterxml.jackson.databind.introspect.AnnotatedMethod;
import com.fasterxml.jackson.databind.introspect.AnnotatedParameter;
import com.fasterxml.jackson.databind.introspect.AnnotatedWithParams;
import com.fasterxml.jackson.databind.introspect.BasicBeanDescription;
import com.fasterxml.jackson.databind.introspect.BeanPropertyDefinition;
import com.fasterxml.jackson.databind.introspect.POJOPropertyBuilder;
import com.fasterxml.jackson.databind.introspect.VisibilityChecker;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import com.fasterxml.jackson.databind.jsontype.TypeResolverBuilder;
import com.fasterxml.jackson.databind.type.ArrayType;
import com.fasterxml.jackson.databind.type.CollectionLikeType;
import com.fasterxml.jackson.databind.type.CollectionType;
import com.fasterxml.jackson.databind.type.MapLikeType;
import com.fasterxml.jackson.databind.type.MapType;
import com.fasterxml.jackson.databind.type.ReferenceType;
import com.fasterxml.jackson.databind.type.TypeFactory;
import com.fasterxml.jackson.databind.util.ClassUtil;
import com.fasterxml.jackson.databind.util.EnumResolver;
import com.fasterxml.jackson.databind.util.SimpleBeanPropertyDefinition;
import com.fasterxml.jackson.databind.util.TokenBuffer;
import java.io.Serializable;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.EnumSet;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.NavigableMap;
import java.util.Queue;
import java.util.Set;
import java.util.SortedMap;
import java.util.SortedSet;
import java.util.TreeMap;
import java.util.TreeSet;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;
import java.util.concurrent.ConcurrentNavigableMap;
import java.util.concurrent.ConcurrentSkipListMap;
import java.util.concurrent.atomic.AtomicReference;

public abstract class BasicDeserializerFactory extends DeserializerFactory implements Serializable {
    private static final Class<?> CLASS_CHAR_BUFFER = CharSequence.class;
    private static final Class<?> CLASS_ITERABLE = Iterable.class;
    private static final Class<?> CLASS_MAP_ENTRY = Entry.class;
    private static final Class<?> CLASS_OBJECT = Object.class;
    private static final Class<?> CLASS_STRING = String.class;
    protected static final PropertyName UNWRAPPED_CREATOR_PARAM_NAME = new PropertyName("@JsonUnwrapped");
    static final HashMap<String, Class<? extends Collection>> _collectionFallbacks = new HashMap<>();
    static final HashMap<String, Class<? extends Map>> _mapFallbacks = new HashMap<>();
    protected final DeserializerFactoryConfig _factoryConfig;

    /* access modifiers changed from: protected */
    public abstract DeserializerFactory withConfig(DeserializerFactoryConfig deserializerFactoryConfig);

    static {
        _mapFallbacks.put(Map.class.getName(), LinkedHashMap.class);
        _mapFallbacks.put(ConcurrentMap.class.getName(), ConcurrentHashMap.class);
        _mapFallbacks.put(SortedMap.class.getName(), TreeMap.class);
        _mapFallbacks.put(NavigableMap.class.getName(), TreeMap.class);
        _mapFallbacks.put(ConcurrentNavigableMap.class.getName(), ConcurrentSkipListMap.class);
        _collectionFallbacks.put(Collection.class.getName(), ArrayList.class);
        _collectionFallbacks.put(List.class.getName(), ArrayList.class);
        _collectionFallbacks.put(Set.class.getName(), HashSet.class);
        _collectionFallbacks.put(SortedSet.class.getName(), TreeSet.class);
        _collectionFallbacks.put(Queue.class.getName(), LinkedList.class);
        _collectionFallbacks.put("java.util.Deque", LinkedList.class);
        _collectionFallbacks.put("java.util.NavigableSet", TreeSet.class);
    }

    protected BasicDeserializerFactory(DeserializerFactoryConfig deserializerFactoryConfig) {
        this._factoryConfig = deserializerFactoryConfig;
    }

    public DeserializerFactoryConfig getFactoryConfig() {
        return this._factoryConfig;
    }

    public final DeserializerFactory withAdditionalDeserializers(Deserializers deserializers) {
        return withConfig(this._factoryConfig.withAdditionalDeserializers(deserializers));
    }

    public final DeserializerFactory withAdditionalKeyDeserializers(KeyDeserializers keyDeserializers) {
        return withConfig(this._factoryConfig.withAdditionalKeyDeserializers(keyDeserializers));
    }

    public final DeserializerFactory withDeserializerModifier(BeanDeserializerModifier beanDeserializerModifier) {
        return withConfig(this._factoryConfig.withDeserializerModifier(beanDeserializerModifier));
    }

    public final DeserializerFactory withAbstractTypeResolver(AbstractTypeResolver abstractTypeResolver) {
        return withConfig(this._factoryConfig.withAbstractTypeResolver(abstractTypeResolver));
    }

    public final DeserializerFactory withValueInstantiators(ValueInstantiators valueInstantiators) {
        return withConfig(this._factoryConfig.withValueInstantiators(valueInstantiators));
    }

    public JavaType mapAbstractType(DeserializationConfig deserializationConfig, JavaType javaType) throws JsonMappingException {
        JavaType _mapAbstractType2;
        while (true) {
            _mapAbstractType2 = _mapAbstractType2(deserializationConfig, javaType);
            if (_mapAbstractType2 == null) {
                return javaType;
            }
            Class rawClass = javaType.getRawClass();
            Class rawClass2 = _mapAbstractType2.getRawClass();
            if (rawClass != rawClass2 && rawClass.isAssignableFrom(rawClass2)) {
                javaType = _mapAbstractType2;
            }
        }
        throw new IllegalArgumentException("Invalid abstract type resolution from " + javaType + " to " + _mapAbstractType2 + ": latter is not a subtype of former");
    }

    private JavaType _mapAbstractType2(DeserializationConfig deserializationConfig, JavaType javaType) throws JsonMappingException {
        Class rawClass = javaType.getRawClass();
        if (this._factoryConfig.hasAbstractTypeResolvers()) {
            for (AbstractTypeResolver findTypeMapping : this._factoryConfig.abstractTypeResolvers()) {
                JavaType findTypeMapping2 = findTypeMapping.findTypeMapping(deserializationConfig, javaType);
                if (findTypeMapping2 != null && findTypeMapping2.getRawClass() != rawClass) {
                    return findTypeMapping2;
                }
            }
        }
        return null;
    }

    public ValueInstantiator findValueInstantiator(DeserializationContext deserializationContext, BeanDescription beanDescription) throws JsonMappingException {
        ValueInstantiator valueInstantiator;
        DeserializationConfig config = deserializationContext.getConfig();
        ValueInstantiator valueInstantiator2 = null;
        AnnotatedClass classInfo = beanDescription.getClassInfo();
        Object findValueInstantiator = deserializationContext.getAnnotationIntrospector().findValueInstantiator(classInfo);
        if (findValueInstantiator != null) {
            valueInstantiator2 = _valueInstantiatorInstance(config, classInfo, findValueInstantiator);
        }
        if (valueInstantiator2 == null) {
            valueInstantiator2 = _findStdValueInstantiator(config, beanDescription);
            if (valueInstantiator2 == null) {
                valueInstantiator2 = _constructDefaultValueInstantiator(deserializationContext, beanDescription);
            }
        }
        if (this._factoryConfig.hasValueInstantiators()) {
            valueInstantiator = valueInstantiator2;
            for (ValueInstantiators valueInstantiators : this._factoryConfig.valueInstantiators()) {
                valueInstantiator = valueInstantiators.findValueInstantiator(config, beanDescription, valueInstantiator);
                if (valueInstantiator == null) {
                    throw JsonMappingException.from(deserializationContext.getParser(), "Broken registered ValueInstantiators (of type " + valueInstantiators.getClass().getName() + "): returned null ValueInstantiator");
                }
            }
        } else {
            valueInstantiator = valueInstantiator2;
        }
        if (valueInstantiator.getIncompleteParameter() == null) {
            return valueInstantiator;
        }
        AnnotatedParameter incompleteParameter = valueInstantiator.getIncompleteParameter();
        throw new IllegalArgumentException("Argument #" + incompleteParameter.getIndex() + " of constructor " + incompleteParameter.getOwner() + " has no property name annotation; must have name when multiple-parameter constructor annotated as Creator");
    }

    private ValueInstantiator _findStdValueInstantiator(DeserializationConfig deserializationConfig, BeanDescription beanDescription) throws JsonMappingException {
        if (beanDescription.getBeanClass() == JsonLocation.class) {
            return new JsonLocationInstantiator();
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public ValueInstantiator _constructDefaultValueInstantiator(DeserializationContext deserializationContext, BeanDescription beanDescription) throws JsonMappingException {
        CreatorCollector creatorCollector = new CreatorCollector(beanDescription, deserializationContext.getConfig());
        AnnotationIntrospector annotationIntrospector = deserializationContext.getAnnotationIntrospector();
        DeserializationConfig config = deserializationContext.getConfig();
        VisibilityChecker findAutoDetectVisibility = annotationIntrospector.findAutoDetectVisibility(beanDescription.getClassInfo(), config.getDefaultVisibilityChecker());
        Map _findCreatorsFromProperties = _findCreatorsFromProperties(deserializationContext, beanDescription);
        _addDeserializerFactoryMethods(deserializationContext, beanDescription, findAutoDetectVisibility, annotationIntrospector, creatorCollector, _findCreatorsFromProperties);
        if (beanDescription.getType().isConcrete()) {
            _addDeserializerConstructors(deserializationContext, beanDescription, findAutoDetectVisibility, annotationIntrospector, creatorCollector, _findCreatorsFromProperties);
        }
        return creatorCollector.constructValueInstantiator(config);
    }

    /* access modifiers changed from: protected */
    public Map<AnnotatedWithParams, BeanPropertyDefinition[]> _findCreatorsFromProperties(DeserializationContext deserializationContext, BeanDescription beanDescription) throws JsonMappingException {
        Map<AnnotatedWithParams, BeanPropertyDefinition[]> map;
        Map<AnnotatedWithParams, BeanPropertyDefinition[]> emptyMap = Collections.emptyMap();
        for (BeanPropertyDefinition beanPropertyDefinition : beanDescription.findProperties()) {
            Iterator constructorParameters = beanPropertyDefinition.getConstructorParameters();
            while (true) {
                if (constructorParameters.hasNext()) {
                    AnnotatedParameter annotatedParameter = (AnnotatedParameter) constructorParameters.next();
                    AnnotatedWithParams owner = annotatedParameter.getOwner();
                    BeanPropertyDefinition[] beanPropertyDefinitionArr = (BeanPropertyDefinition[]) emptyMap.get(owner);
                    int index = annotatedParameter.getIndex();
                    if (beanPropertyDefinitionArr == null) {
                        if (emptyMap.isEmpty()) {
                            map = new LinkedHashMap<>();
                        } else {
                            map = emptyMap;
                        }
                        beanPropertyDefinitionArr = new BeanPropertyDefinition[owner.getParameterCount()];
                        map.put(owner, beanPropertyDefinitionArr);
                    } else if (beanPropertyDefinitionArr[index] != null) {
                        throw new IllegalStateException("Conflict: parameter #" + index + " of " + owner + " bound to more than one property; " + beanPropertyDefinitionArr[index] + " vs " + beanPropertyDefinition);
                    } else {
                        map = emptyMap;
                    }
                    beanPropertyDefinitionArr[index] = beanPropertyDefinition;
                    emptyMap = map;
                }
            }
        }
        return emptyMap;
    }

    public ValueInstantiator _valueInstantiatorInstance(DeserializationConfig deserializationConfig, Annotated annotated, Object obj) throws JsonMappingException {
        if (obj == null) {
            return null;
        }
        if (obj instanceof ValueInstantiator) {
            return (ValueInstantiator) obj;
        }
        if (!(obj instanceof Class)) {
            throw new IllegalStateException("AnnotationIntrospector returned key deserializer definition of type " + obj.getClass().getName() + "; expected type KeyDeserializer or Class<KeyDeserializer> instead");
        }
        Class cls = (Class) obj;
        if (ClassUtil.isBogusClass(cls)) {
            return null;
        }
        if (!ValueInstantiator.class.isAssignableFrom(cls)) {
            throw new IllegalStateException("AnnotationIntrospector returned Class " + cls.getName() + "; expected Class<ValueInstantiator>");
        }
        HandlerInstantiator handlerInstantiator = deserializationConfig.getHandlerInstantiator();
        if (handlerInstantiator != null) {
            ValueInstantiator valueInstantiatorInstance = handlerInstantiator.valueInstantiatorInstance(deserializationConfig, annotated, cls);
            if (valueInstantiatorInstance != null) {
                return valueInstantiatorInstance;
            }
        }
        return (ValueInstantiator) ClassUtil.createInstance(cls, deserializationConfig.canOverrideAccessModifiers());
    }

    /* access modifiers changed from: protected */
    public void _addDeserializerConstructors(DeserializationContext deserializationContext, BeanDescription beanDescription, VisibilityChecker<?> visibilityChecker, AnnotationIntrospector annotationIntrospector, CreatorCollector creatorCollector, Map<AnnotatedWithParams, BeanPropertyDefinition[]> map) throws JsonMappingException {
        List list;
        int i;
        int i2;
        int i3;
        AnnotatedConstructor findDefaultConstructor = beanDescription.findDefaultConstructor();
        if (findDefaultConstructor != null && (!creatorCollector.hasDefaultCreator() || annotationIntrospector.hasCreatorAnnotation(findDefaultConstructor))) {
            creatorCollector.setDefaultCreator(findDefaultConstructor);
        }
        List list2 = null;
        for (AnnotatedConstructor annotatedConstructor : beanDescription.getConstructors()) {
            boolean hasCreatorAnnotation = annotationIntrospector.hasCreatorAnnotation(annotatedConstructor);
            BeanPropertyDefinition[] beanPropertyDefinitionArr = (BeanPropertyDefinition[]) map.get(annotatedConstructor);
            int parameterCount = annotatedConstructor.getParameterCount();
            if (parameterCount == 1) {
                BeanPropertyDefinition beanPropertyDefinition = beanPropertyDefinitionArr == null ? null : beanPropertyDefinitionArr[0];
                if (_checkIfCreatorPropertyBased(annotationIntrospector, annotatedConstructor, beanPropertyDefinition)) {
                    SettableBeanProperty[] settableBeanPropertyArr = new SettableBeanProperty[1];
                    PropertyName fullName = beanPropertyDefinition == null ? null : beanPropertyDefinition.getFullName();
                    AnnotatedParameter parameter = annotatedConstructor.getParameter(0);
                    settableBeanPropertyArr[0] = constructCreatorProperty(deserializationContext, beanDescription, fullName, 0, parameter, annotationIntrospector.findInjectableValueId(parameter));
                    creatorCollector.addPropertyCreator(annotatedConstructor, hasCreatorAnnotation, settableBeanPropertyArr);
                } else {
                    _handleSingleArgumentConstructor(deserializationContext, beanDescription, visibilityChecker, annotationIntrospector, creatorCollector, annotatedConstructor, hasCreatorAnnotation, visibilityChecker.isCreatorVisible((AnnotatedMember) annotatedConstructor));
                    if (beanPropertyDefinition != null) {
                        ((POJOPropertyBuilder) beanPropertyDefinition).removeConstructors();
                    }
                }
            } else {
                AnnotatedParameter annotatedParameter = null;
                SettableBeanProperty[] settableBeanPropertyArr2 = new SettableBeanProperty[parameterCount];
                int i4 = 0;
                int i5 = 0;
                int i6 = 0;
                int i7 = 0;
                while (i7 < parameterCount) {
                    AnnotatedParameter parameter2 = annotatedConstructor.getParameter(i7);
                    BeanPropertyDefinition beanPropertyDefinition2 = beanPropertyDefinitionArr == null ? null : beanPropertyDefinitionArr[i7];
                    Object findInjectableValueId = annotationIntrospector.findInjectableValueId(parameter2);
                    PropertyName fullName2 = beanPropertyDefinition2 == null ? null : beanPropertyDefinition2.getFullName();
                    if (beanPropertyDefinition2 != null && beanPropertyDefinition2.isExplicitlyNamed()) {
                        int i8 = i4 + 1;
                        settableBeanPropertyArr2[i7] = constructCreatorProperty(deserializationContext, beanDescription, fullName2, i7, parameter2, findInjectableValueId);
                        i = i6;
                        i2 = i5;
                        i3 = i8;
                        parameter2 = annotatedParameter;
                    } else if (findInjectableValueId != null) {
                        int i9 = i6 + 1;
                        settableBeanPropertyArr2[i7] = constructCreatorProperty(deserializationContext, beanDescription, fullName2, i7, parameter2, findInjectableValueId);
                        i = i9;
                        i2 = i5;
                        i3 = i4;
                        parameter2 = annotatedParameter;
                    } else if (annotationIntrospector.findUnwrappingNameTransformer(parameter2) != null) {
                        settableBeanPropertyArr2[i7] = constructCreatorProperty(deserializationContext, beanDescription, UNWRAPPED_CREATOR_PARAM_NAME, i7, parameter2, null);
                        i3 = i4 + 1;
                        i = i6;
                        i2 = i5;
                        parameter2 = annotatedParameter;
                    } else if (hasCreatorAnnotation && fullName2 != null && !fullName2.isEmpty()) {
                        int i10 = i5 + 1;
                        settableBeanPropertyArr2[i7] = constructCreatorProperty(deserializationContext, beanDescription, fullName2, i7, parameter2, findInjectableValueId);
                        i = i6;
                        i2 = i10;
                        i3 = i4;
                        parameter2 = annotatedParameter;
                    } else if (annotatedParameter == null) {
                        i = i6;
                        i2 = i5;
                        i3 = i4;
                    } else {
                        i = i6;
                        i2 = i5;
                        i3 = i4;
                        parameter2 = annotatedParameter;
                    }
                    i7++;
                    i6 = i;
                    i5 = i2;
                    i4 = i3;
                    annotatedParameter = parameter2;
                }
                int i11 = i4 + i5;
                if (hasCreatorAnnotation || i4 > 0 || i6 > 0) {
                    if (i11 + i6 == parameterCount) {
                        creatorCollector.addPropertyCreator(annotatedConstructor, hasCreatorAnnotation, settableBeanPropertyArr2);
                    } else if (i4 == 0 && i6 + 1 == parameterCount) {
                        creatorCollector.addDelegatingCreator(annotatedConstructor, hasCreatorAnnotation, settableBeanPropertyArr2);
                    } else {
                        PropertyName _findImplicitParamName = _findImplicitParamName(annotatedParameter, annotationIntrospector);
                        if (_findImplicitParamName == null || _findImplicitParamName.isEmpty()) {
                            int index = annotatedParameter.getIndex();
                            if (index != 0 || !ClassUtil.isNonStaticInnerClass(annotatedConstructor.getDeclaringClass())) {
                                throw new IllegalArgumentException("Argument #" + index + " of constructor " + annotatedConstructor + " has no property name annotation; must have name when multiple-parameter constructor annotated as Creator");
                            }
                            throw new IllegalArgumentException("Non-static inner classes like " + annotatedConstructor.getDeclaringClass().getName() + " can not use @JsonCreator for constructors");
                        }
                    }
                }
                if (!creatorCollector.hasDefaultCreator()) {
                    if (list2 == null) {
                        list = new LinkedList();
                    } else {
                        list = list2;
                    }
                    list.add(annotatedConstructor);
                } else {
                    list = list2;
                }
                list2 = list;
            }
        }
        if (list2 != null && !creatorCollector.hasDelegatingCreator() && !creatorCollector.hasPropertyBasedCreator()) {
            _checkImplicitlyNamedConstructors(deserializationContext, beanDescription, visibilityChecker, annotationIntrospector, creatorCollector, list2);
        }
    }

    /* access modifiers changed from: protected */
    public void _checkImplicitlyNamedConstructors(DeserializationContext deserializationContext, BeanDescription beanDescription, VisibilityChecker<?> visibilityChecker, AnnotationIntrospector annotationIntrospector, CreatorCollector creatorCollector, List<AnnotatedConstructor> list) throws JsonMappingException {
        Iterator it = list.iterator();
        SettableBeanProperty[] settableBeanPropertyArr = null;
        AnnotatedConstructor annotatedConstructor = null;
        while (true) {
            if (!it.hasNext()) {
                break;
            }
            AnnotatedConstructor annotatedConstructor2 = (AnnotatedConstructor) it.next();
            if (visibilityChecker.isCreatorVisible((AnnotatedMember) annotatedConstructor2)) {
                int parameterCount = annotatedConstructor2.getParameterCount();
                SettableBeanProperty[] settableBeanPropertyArr2 = new SettableBeanProperty[parameterCount];
                int i = 0;
                while (true) {
                    if (i < parameterCount) {
                        AnnotatedParameter parameter = annotatedConstructor2.getParameter(i);
                        PropertyName _findParamName = _findParamName(parameter, annotationIntrospector);
                        if (_findParamName == null || _findParamName.isEmpty()) {
                            break;
                        }
                        settableBeanPropertyArr2[i] = constructCreatorProperty(deserializationContext, beanDescription, _findParamName, parameter.getIndex(), parameter, null);
                        i++;
                    } else if (annotatedConstructor != null) {
                        annotatedConstructor = null;
                        break;
                    } else {
                        settableBeanPropertyArr = settableBeanPropertyArr2;
                        annotatedConstructor = annotatedConstructor2;
                    }
                }
            }
        }
        if (annotatedConstructor != null) {
            creatorCollector.addPropertyCreator(annotatedConstructor, false, settableBeanPropertyArr);
            BasicBeanDescription basicBeanDescription = (BasicBeanDescription) beanDescription;
            for (SettableBeanProperty settableBeanProperty : settableBeanPropertyArr) {
                PropertyName fullName = settableBeanProperty.getFullName();
                if (!basicBeanDescription.hasProperty(fullName)) {
                    basicBeanDescription.addProperty(SimpleBeanPropertyDefinition.construct((MapperConfig<?>) deserializationContext.getConfig(), settableBeanProperty.getMember(), fullName));
                }
            }
        }
    }

    /* access modifiers changed from: protected */
    public boolean _checkIfCreatorPropertyBased(AnnotationIntrospector annotationIntrospector, AnnotatedWithParams annotatedWithParams, BeanPropertyDefinition beanPropertyDefinition) {
        Mode findCreatorBinding = annotationIntrospector.findCreatorBinding(annotatedWithParams);
        if (findCreatorBinding == Mode.PROPERTIES) {
            return true;
        }
        if (findCreatorBinding == Mode.DELEGATING) {
            return false;
        }
        if ((beanPropertyDefinition != null && beanPropertyDefinition.isExplicitlyNamed()) || annotationIntrospector.findInjectableValueId(annotatedWithParams.getParameter(0)) != null) {
            return true;
        }
        if (beanPropertyDefinition != null) {
            String name = beanPropertyDefinition.getName();
            if (name != null && !name.isEmpty() && beanPropertyDefinition.couldSerialize()) {
                return true;
            }
        }
        return false;
    }

    /* access modifiers changed from: protected */
    public boolean _handleSingleArgumentConstructor(DeserializationContext deserializationContext, BeanDescription beanDescription, VisibilityChecker<?> visibilityChecker, AnnotationIntrospector annotationIntrospector, CreatorCollector creatorCollector, AnnotatedConstructor annotatedConstructor, boolean z, boolean z2) throws JsonMappingException {
        Class<Boolean> rawParameterType = annotatedConstructor.getRawParameterType(0);
        if (rawParameterType == String.class || rawParameterType == CharSequence.class) {
            if (!z && !z2) {
                return true;
            }
            creatorCollector.addStringCreator(annotatedConstructor, z);
            return true;
        } else if (rawParameterType == Integer.TYPE || rawParameterType == Integer.class) {
            if (!z && !z2) {
                return true;
            }
            creatorCollector.addIntCreator(annotatedConstructor, z);
            return true;
        } else if (rawParameterType == Long.TYPE || rawParameterType == Long.class) {
            if (!z && !z2) {
                return true;
            }
            creatorCollector.addLongCreator(annotatedConstructor, z);
            return true;
        } else if (rawParameterType == Double.TYPE || rawParameterType == Double.class) {
            if (!z && !z2) {
                return true;
            }
            creatorCollector.addDoubleCreator(annotatedConstructor, z);
            return true;
        } else if (rawParameterType == Boolean.TYPE || rawParameterType == Boolean.class) {
            if (!z && !z2) {
                return true;
            }
            creatorCollector.addBooleanCreator(annotatedConstructor, z);
            return true;
        } else if (!z) {
            return false;
        } else {
            creatorCollector.addDelegatingCreator(annotatedConstructor, z, null);
            return true;
        }
    }

    /* access modifiers changed from: protected */
    public void _addDeserializerFactoryMethods(DeserializationContext deserializationContext, BeanDescription beanDescription, VisibilityChecker<?> visibilityChecker, AnnotationIntrospector annotationIntrospector, CreatorCollector creatorCollector, Map<AnnotatedWithParams, BeanPropertyDefinition[]> map) throws JsonMappingException {
        BeanPropertyDefinition beanPropertyDefinition;
        DeserializationConfig config = deserializationContext.getConfig();
        for (AnnotatedMethod annotatedMethod : beanDescription.getFactoryMethods()) {
            boolean hasCreatorAnnotation = annotationIntrospector.hasCreatorAnnotation(annotatedMethod);
            int parameterCount = annotatedMethod.getParameterCount();
            if (parameterCount != 0) {
                BeanPropertyDefinition[] beanPropertyDefinitionArr = (BeanPropertyDefinition[]) map.get(annotatedMethod);
                if (parameterCount == 1) {
                    if (beanPropertyDefinitionArr == null) {
                        beanPropertyDefinition = null;
                    } else {
                        beanPropertyDefinition = beanPropertyDefinitionArr[0];
                    }
                    if (!_checkIfCreatorPropertyBased(annotationIntrospector, annotatedMethod, beanPropertyDefinition)) {
                        _handleSingleArgumentFactory(config, beanDescription, visibilityChecker, annotationIntrospector, creatorCollector, annotatedMethod, hasCreatorAnnotation);
                    }
                } else if (!hasCreatorAnnotation) {
                    continue;
                }
                AnnotatedParameter annotatedParameter = null;
                SettableBeanProperty[] settableBeanPropertyArr = new SettableBeanProperty[parameterCount];
                int i = 0;
                int i2 = 0;
                int i3 = 0;
                int i4 = 0;
                while (i4 < parameterCount) {
                    AnnotatedParameter parameter = annotatedMethod.getParameter(i4);
                    BeanPropertyDefinition beanPropertyDefinition2 = beanPropertyDefinitionArr == null ? null : beanPropertyDefinitionArr[i4];
                    Object findInjectableValueId = annotationIntrospector.findInjectableValueId(parameter);
                    PropertyName fullName = beanPropertyDefinition2 == null ? null : beanPropertyDefinition2.getFullName();
                    if (beanPropertyDefinition2 != null && beanPropertyDefinition2.isExplicitlyNamed()) {
                        i2++;
                        settableBeanPropertyArr[i4] = constructCreatorProperty(deserializationContext, beanDescription, fullName, i4, parameter, findInjectableValueId);
                        parameter = annotatedParameter;
                    } else if (findInjectableValueId != null) {
                        i3++;
                        settableBeanPropertyArr[i4] = constructCreatorProperty(deserializationContext, beanDescription, fullName, i4, parameter, findInjectableValueId);
                        parameter = annotatedParameter;
                    } else if (annotationIntrospector.findUnwrappingNameTransformer(parameter) != null) {
                        settableBeanPropertyArr[i4] = constructCreatorProperty(deserializationContext, beanDescription, UNWRAPPED_CREATOR_PARAM_NAME, i4, parameter, null);
                        i++;
                        parameter = annotatedParameter;
                    } else if (hasCreatorAnnotation && fullName != null && !fullName.isEmpty()) {
                        i++;
                        settableBeanPropertyArr[i4] = constructCreatorProperty(deserializationContext, beanDescription, fullName, i4, parameter, findInjectableValueId);
                        parameter = annotatedParameter;
                    } else if (annotatedParameter != null) {
                        parameter = annotatedParameter;
                    }
                    i4++;
                    annotatedParameter = parameter;
                }
                int i5 = i2 + i;
                if (hasCreatorAnnotation || i2 > 0 || i3 > 0) {
                    if (i5 + i3 == parameterCount) {
                        creatorCollector.addPropertyCreator(annotatedMethod, hasCreatorAnnotation, settableBeanPropertyArr);
                    } else if (i2 == 0 && i3 + 1 == parameterCount) {
                        creatorCollector.addDelegatingCreator(annotatedMethod, hasCreatorAnnotation, settableBeanPropertyArr);
                    } else {
                        throw new IllegalArgumentException("Argument #" + annotatedParameter.getIndex() + " of factory method " + annotatedMethod + " has no property name annotation; must have name when multiple-parameter constructor annotated as Creator");
                    }
                }
            } else if (hasCreatorAnnotation) {
                creatorCollector.setDefaultCreator(annotatedMethod);
            }
        }
    }

    /* access modifiers changed from: protected */
    public boolean _handleSingleArgumentFactory(DeserializationConfig deserializationConfig, BeanDescription beanDescription, VisibilityChecker<?> visibilityChecker, AnnotationIntrospector annotationIntrospector, CreatorCollector creatorCollector, AnnotatedMethod annotatedMethod, boolean z) throws JsonMappingException {
        Class<Boolean> rawParameterType = annotatedMethod.getRawParameterType(0);
        if (rawParameterType == String.class || rawParameterType == CharSequence.class) {
            if (!z && !visibilityChecker.isCreatorVisible((AnnotatedMember) annotatedMethod)) {
                return true;
            }
            creatorCollector.addStringCreator(annotatedMethod, z);
            return true;
        } else if (rawParameterType == Integer.TYPE || rawParameterType == Integer.class) {
            if (!z && !visibilityChecker.isCreatorVisible((AnnotatedMember) annotatedMethod)) {
                return true;
            }
            creatorCollector.addIntCreator(annotatedMethod, z);
            return true;
        } else if (rawParameterType == Long.TYPE || rawParameterType == Long.class) {
            if (!z && !visibilityChecker.isCreatorVisible((AnnotatedMember) annotatedMethod)) {
                return true;
            }
            creatorCollector.addLongCreator(annotatedMethod, z);
            return true;
        } else if (rawParameterType == Double.TYPE || rawParameterType == Double.class) {
            if (!z && !visibilityChecker.isCreatorVisible((AnnotatedMember) annotatedMethod)) {
                return true;
            }
            creatorCollector.addDoubleCreator(annotatedMethod, z);
            return true;
        } else if (rawParameterType == Boolean.TYPE || rawParameterType == Boolean.class) {
            if (!z && !visibilityChecker.isCreatorVisible((AnnotatedMember) annotatedMethod)) {
                return true;
            }
            creatorCollector.addBooleanCreator(annotatedMethod, z);
            return true;
        } else if (!z) {
            return false;
        } else {
            creatorCollector.addDelegatingCreator(annotatedMethod, z, null);
            return true;
        }
    }

    /* access modifiers changed from: protected */
    public SettableBeanProperty constructCreatorProperty(DeserializationContext deserializationContext, BeanDescription beanDescription, PropertyName propertyName, int i, AnnotatedParameter annotatedParameter, Object obj) throws JsonMappingException {
        PropertyMetadata construct;
        Std std;
        TypeDeserializer typeDeserializer;
        DeserializationConfig config = deserializationContext.getConfig();
        AnnotationIntrospector annotationIntrospector = deserializationContext.getAnnotationIntrospector();
        if (annotationIntrospector == null) {
            construct = PropertyMetadata.STD_REQUIRED_OR_OPTIONAL;
        } else {
            Boolean hasRequiredMarker = annotationIntrospector.hasRequiredMarker(annotatedParameter);
            construct = PropertyMetadata.construct(hasRequiredMarker != null && hasRequiredMarker.booleanValue(), annotationIntrospector.findPropertyDescription(annotatedParameter), annotationIntrospector.findPropertyIndex(annotatedParameter), annotationIntrospector.findPropertyDefaultValue(annotatedParameter));
        }
        JavaType resolveType = beanDescription.resolveType(annotatedParameter.getParameterType());
        Std std2 = new Std(propertyName, resolveType, annotationIntrospector.findWrapperName(annotatedParameter), beanDescription.getClassAnnotations(), (AnnotatedMember) annotatedParameter, construct);
        JavaType resolveType2 = resolveType(deserializationContext, beanDescription, resolveType, annotatedParameter);
        if (resolveType2 != resolveType) {
            std = std2.withType(resolveType2);
        } else {
            std = std2;
        }
        JsonDeserializer findDeserializerFromAnnotation = findDeserializerFromAnnotation(deserializationContext, annotatedParameter);
        JavaType modifyTypeByAnnotation = modifyTypeByAnnotation(deserializationContext, annotatedParameter, resolveType2);
        TypeDeserializer typeDeserializer2 = (TypeDeserializer) modifyTypeByAnnotation.getTypeHandler();
        if (typeDeserializer2 == null) {
            typeDeserializer = findTypeDeserializer(config, modifyTypeByAnnotation);
        } else {
            typeDeserializer = typeDeserializer2;
        }
        CreatorProperty creatorProperty = new CreatorProperty(propertyName, modifyTypeByAnnotation, std.getWrapperName(), typeDeserializer, beanDescription.getClassAnnotations(), annotatedParameter, i, obj, construct);
        if (findDeserializerFromAnnotation != null) {
            return creatorProperty.withValueDeserializer(deserializationContext.handlePrimaryContextualization(findDeserializerFromAnnotation, creatorProperty, modifyTypeByAnnotation));
        }
        return creatorProperty;
    }

    /* access modifiers changed from: protected */
    public PropertyName _findParamName(AnnotatedParameter annotatedParameter, AnnotationIntrospector annotationIntrospector) {
        if (!(annotatedParameter == null || annotationIntrospector == null)) {
            PropertyName findNameForDeserialization = annotationIntrospector.findNameForDeserialization(annotatedParameter);
            if (findNameForDeserialization != null) {
                return findNameForDeserialization;
            }
            String findImplicitPropertyName = annotationIntrospector.findImplicitPropertyName(annotatedParameter);
            if (findImplicitPropertyName != null && !findImplicitPropertyName.isEmpty()) {
                return PropertyName.construct(findImplicitPropertyName);
            }
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public PropertyName _findImplicitParamName(AnnotatedParameter annotatedParameter, AnnotationIntrospector annotationIntrospector) {
        String findImplicitPropertyName = annotationIntrospector.findImplicitPropertyName(annotatedParameter);
        if (findImplicitPropertyName == null || findImplicitPropertyName.isEmpty()) {
            return null;
        }
        return PropertyName.construct(findImplicitPropertyName);
    }

    /* access modifiers changed from: protected */
    @Deprecated
    public PropertyName _findExplicitParamName(AnnotatedParameter annotatedParameter, AnnotationIntrospector annotationIntrospector) {
        if (annotatedParameter == null || annotationIntrospector == null) {
            return null;
        }
        return annotationIntrospector.findNameForDeserialization(annotatedParameter);
    }

    /* access modifiers changed from: protected */
    @Deprecated
    public boolean _hasExplicitParamName(AnnotatedParameter annotatedParameter, AnnotationIntrospector annotationIntrospector) {
        if (annotatedParameter == null || annotationIntrospector == null) {
            return false;
        }
        PropertyName findNameForDeserialization = annotationIntrospector.findNameForDeserialization(annotatedParameter);
        if (findNameForDeserialization == null || !findNameForDeserialization.hasSimpleName()) {
            return false;
        }
        return true;
    }

    public JsonDeserializer<?> createArrayDeserializer(DeserializationContext deserializationContext, ArrayType arrayType, BeanDescription beanDescription) throws JsonMappingException {
        TypeDeserializer typeDeserializer;
        DeserializationConfig config = deserializationContext.getConfig();
        JavaType contentType = arrayType.getContentType();
        JsonDeserializer jsonDeserializer = (JsonDeserializer) contentType.getValueHandler();
        TypeDeserializer typeDeserializer2 = (TypeDeserializer) contentType.getTypeHandler();
        if (typeDeserializer2 == null) {
            typeDeserializer = findTypeDeserializer(config, contentType);
        } else {
            typeDeserializer = typeDeserializer2;
        }
        JsonDeserializer _findCustomArrayDeserializer = _findCustomArrayDeserializer(arrayType, config, beanDescription, typeDeserializer, jsonDeserializer);
        if (_findCustomArrayDeserializer == null) {
            if (jsonDeserializer == null) {
                Class<String> rawClass = contentType.getRawClass();
                if (contentType.isPrimitive()) {
                    return PrimitiveArrayDeserializers.forType(rawClass);
                }
                if (rawClass == String.class) {
                    return StringArrayDeserializer.instance;
                }
            }
            _findCustomArrayDeserializer = new ObjectArrayDeserializer(arrayType, jsonDeserializer, typeDeserializer);
        }
        if (!this._factoryConfig.hasDeserializerModifiers()) {
            return _findCustomArrayDeserializer;
        }
        Iterator it = this._factoryConfig.deserializerModifiers().iterator();
        while (true) {
            JsonDeserializer jsonDeserializer2 = _findCustomArrayDeserializer;
            if (!it.hasNext()) {
                return jsonDeserializer2;
            }
            _findCustomArrayDeserializer = ((BeanDeserializerModifier) it.next()).modifyArrayDeserializer(config, arrayType, beanDescription, jsonDeserializer2);
        }
    }

    public JsonDeserializer<?> createCollectionDeserializer(DeserializationContext deserializationContext, CollectionType collectionType, BeanDescription beanDescription) throws JsonMappingException {
        TypeDeserializer typeDeserializer;
        JavaType contentType = collectionType.getContentType();
        JsonDeserializer jsonDeserializer = (JsonDeserializer) contentType.getValueHandler();
        DeserializationConfig config = deserializationContext.getConfig();
        TypeDeserializer typeDeserializer2 = (TypeDeserializer) contentType.getTypeHandler();
        if (typeDeserializer2 == null) {
            typeDeserializer = findTypeDeserializer(config, contentType);
        } else {
            typeDeserializer = typeDeserializer2;
        }
        JsonDeserializer _findCustomCollectionDeserializer = _findCustomCollectionDeserializer(collectionType, config, beanDescription, typeDeserializer, jsonDeserializer);
        if (_findCustomCollectionDeserializer == null) {
            Class rawClass = collectionType.getRawClass();
            if (jsonDeserializer == null && EnumSet.class.isAssignableFrom(rawClass)) {
                _findCustomCollectionDeserializer = new EnumSetDeserializer(contentType, null);
            }
        }
        if (_findCustomCollectionDeserializer == null) {
            if (collectionType.isInterface() || collectionType.isAbstract()) {
                CollectionType _mapAbstractCollectionType = _mapAbstractCollectionType(collectionType, config);
                if (_mapAbstractCollectionType != null) {
                    beanDescription = config.introspectForCreation(_mapAbstractCollectionType);
                    collectionType = _mapAbstractCollectionType;
                } else if (collectionType.getTypeHandler() == null) {
                    throw new IllegalArgumentException("Can not find a deserializer for non-concrete Collection type " + collectionType);
                } else {
                    _findCustomCollectionDeserializer = AbstractDeserializer.constructForNonPOJO(beanDescription);
                }
            }
            if (_findCustomCollectionDeserializer == null) {
                ValueInstantiator findValueInstantiator = findValueInstantiator(deserializationContext, beanDescription);
                if (!findValueInstantiator.canCreateUsingDefault() && collectionType.getRawClass() == ArrayBlockingQueue.class) {
                    return new ArrayBlockingQueueDeserializer(collectionType, jsonDeserializer, typeDeserializer, findValueInstantiator);
                }
                if (contentType.getRawClass() == String.class) {
                    _findCustomCollectionDeserializer = new StringCollectionDeserializer(collectionType, jsonDeserializer, findValueInstantiator);
                } else {
                    _findCustomCollectionDeserializer = new CollectionDeserializer(collectionType, jsonDeserializer, typeDeserializer, findValueInstantiator);
                }
            }
        }
        if (!this._factoryConfig.hasDeserializerModifiers()) {
            return _findCustomCollectionDeserializer;
        }
        Iterator it = this._factoryConfig.deserializerModifiers().iterator();
        while (true) {
            JsonDeserializer jsonDeserializer2 = _findCustomCollectionDeserializer;
            if (!it.hasNext()) {
                return jsonDeserializer2;
            }
            _findCustomCollectionDeserializer = ((BeanDeserializerModifier) it.next()).modifyCollectionDeserializer(config, collectionType, beanDescription, jsonDeserializer2);
        }
    }

    /* access modifiers changed from: protected */
    public CollectionType _mapAbstractCollectionType(JavaType javaType, DeserializationConfig deserializationConfig) {
        Class cls = (Class) _collectionFallbacks.get(javaType.getRawClass().getName());
        if (cls == null) {
            return null;
        }
        return (CollectionType) deserializationConfig.constructSpecializedType(javaType, cls);
    }

    public JsonDeserializer<?> createCollectionLikeDeserializer(DeserializationContext deserializationContext, CollectionLikeType collectionLikeType, BeanDescription beanDescription) throws JsonMappingException {
        TypeDeserializer typeDeserializer;
        JavaType contentType = collectionLikeType.getContentType();
        JsonDeserializer jsonDeserializer = (JsonDeserializer) contentType.getValueHandler();
        DeserializationConfig config = deserializationContext.getConfig();
        TypeDeserializer typeDeserializer2 = (TypeDeserializer) contentType.getTypeHandler();
        if (typeDeserializer2 == null) {
            typeDeserializer = findTypeDeserializer(config, contentType);
        } else {
            typeDeserializer = typeDeserializer2;
        }
        JsonDeserializer<?> _findCustomCollectionLikeDeserializer = _findCustomCollectionLikeDeserializer(collectionLikeType, config, beanDescription, typeDeserializer, jsonDeserializer);
        if (_findCustomCollectionLikeDeserializer == null || !this._factoryConfig.hasDeserializerModifiers()) {
            return _findCustomCollectionLikeDeserializer;
        }
        Iterator it = this._factoryConfig.deserializerModifiers().iterator();
        while (true) {
            JsonDeserializer<?> jsonDeserializer2 = _findCustomCollectionLikeDeserializer;
            if (!it.hasNext()) {
                return jsonDeserializer2;
            }
            _findCustomCollectionLikeDeserializer = ((BeanDeserializerModifier) it.next()).modifyCollectionLikeDeserializer(config, collectionLikeType, beanDescription, jsonDeserializer2);
        }
    }

    /* JADX WARNING: type inference failed for: r8v0, types: [com.fasterxml.jackson.databind.JsonDeserializer] */
    /* JADX WARNING: type inference failed for: r8v1 */
    /* JADX WARNING: type inference failed for: r8v2 */
    /* JADX WARNING: type inference failed for: r8v3, types: [com.fasterxml.jackson.databind.JsonDeserializer<?>] */
    /* JADX WARNING: type inference failed for: r8v4, types: [com.fasterxml.jackson.databind.JsonDeserializer] */
    /* JADX WARNING: type inference failed for: r8v5, types: [com.fasterxml.jackson.databind.JsonDeserializer] */
    /* JADX WARNING: type inference failed for: r8v6 */
    /* JADX WARNING: type inference failed for: r8v7 */
    /* JADX WARNING: type inference failed for: r8v8, types: [com.fasterxml.jackson.databind.deser.std.MapDeserializer] */
    /* JADX WARNING: type inference failed for: r8v9, types: [com.fasterxml.jackson.databind.deser.AbstractDeserializer] */
    /* JADX WARNING: type inference failed for: r8v10, types: [com.fasterxml.jackson.databind.deser.std.EnumMapDeserializer] */
    /* JADX WARNING: type inference failed for: r8v11 */
    /* JADX WARNING: type inference failed for: r8v12 */
    /* JADX WARNING: type inference failed for: r8v13 */
    /* JADX WARNING: type inference failed for: r8v14 */
    /* JADX WARNING: type inference failed for: r8v15 */
    /* JADX WARNING: type inference failed for: r8v16 */
    /* JADX WARNING: Multi-variable type inference failed. Error: jadx.core.utils.exceptions.JadxRuntimeException: No candidate types for var: r8v1
      assigns: []
      uses: []
      mth insns count: 89
    	at jadx.core.dex.visitors.typeinference.TypeSearch.fillTypeCandidates(TypeSearch.java:237)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.typeinference.TypeSearch.run(TypeSearch.java:53)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.runMultiVariableSearch(TypeInferenceVisitor.java:99)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.visit(TypeInferenceVisitor.java:92)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:27)
    	at jadx.core.dex.visitors.DepthTraversal.lambda$visit$1(DepthTraversal.java:14)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
    	at jadx.core.ProcessClass.process(ProcessClass.java:30)
    	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:311)
    	at jadx.api.JavaClass.decompile(JavaClass.java:62)
    	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:217)
     */
    /* JADX WARNING: Removed duplicated region for block: B:26:0x00a8  */
    /* JADX WARNING: Unknown variable types count: 5 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.fasterxml.jackson.databind.JsonDeserializer<?> createMapDeserializer(com.fasterxml.jackson.databind.DeserializationContext r15, com.fasterxml.jackson.databind.type.MapType r16, com.fasterxml.jackson.databind.BeanDescription r17) throws com.fasterxml.jackson.databind.JsonMappingException {
        /*
            r14 = this;
            com.fasterxml.jackson.databind.DeserializationConfig r3 = r15.getConfig()
            com.fasterxml.jackson.databind.JavaType r9 = r16.getKeyType()
            com.fasterxml.jackson.databind.JavaType r2 = r16.getContentType()
            java.lang.Object r7 = r2.getValueHandler()
            com.fasterxml.jackson.databind.JsonDeserializer r7 = (com.fasterxml.jackson.databind.JsonDeserializer) r7
            java.lang.Object r5 = r9.getValueHandler()
            com.fasterxml.jackson.databind.KeyDeserializer r5 = (com.fasterxml.jackson.databind.KeyDeserializer) r5
            java.lang.Object r1 = r2.getTypeHandler()
            com.fasterxml.jackson.databind.jsontype.TypeDeserializer r1 = (com.fasterxml.jackson.databind.jsontype.TypeDeserializer) r1
            if (r1 != 0) goto L_0x00f4
            com.fasterxml.jackson.databind.jsontype.TypeDeserializer r6 = r14.findTypeDeserializer(r3, r2)
        L_0x0024:
            r1 = r14
            r2 = r16
            r4 = r17
            com.fasterxml.jackson.databind.JsonDeserializer r8 = r1._findCustomMapDeserializer(r2, r3, r4, r5, r6, r7)
            if (r8 != 0) goto L_0x00f1
            java.lang.Class r1 = r16.getRawClass()
            java.lang.Class<java.util.EnumMap> r2 = java.util.EnumMap.class
            boolean r2 = r2.isAssignableFrom(r1)
            if (r2 == 0) goto L_0x0057
            java.lang.Class r2 = r9.getRawClass()
            if (r2 == 0) goto L_0x0047
            boolean r2 = r2.isEnum()
            if (r2 != 0) goto L_0x004f
        L_0x0047:
            java.lang.IllegalArgumentException r1 = new java.lang.IllegalArgumentException
            java.lang.String r2 = "Can not construct EnumMap; generic (key) type not available"
            r1.<init>(r2)
            throw r1
        L_0x004f:
            com.fasterxml.jackson.databind.deser.std.EnumMapDeserializer r8 = new com.fasterxml.jackson.databind.deser.std.EnumMapDeserializer
            r2 = 0
            r0 = r16
            r8.<init>(r0, r2, r7, r6)
        L_0x0057:
            if (r8 != 0) goto L_0x00f1
            boolean r2 = r16.isInterface()
            if (r2 != 0) goto L_0x0065
            boolean r2 = r16.isAbstract()
            if (r2 == 0) goto L_0x00ee
        L_0x0065:
            java.util.HashMap<java.lang.String, java.lang.Class<? extends java.util.Map>> r2 = _mapFallbacks
            java.lang.String r1 = r1.getName()
            java.lang.Object r1 = r2.get(r1)
            java.lang.Class r1 = (java.lang.Class) r1
            if (r1 == 0) goto L_0x00c5
            r0 = r16
            com.fasterxml.jackson.databind.JavaType r1 = r3.constructSpecializedType(r0, r1)
            com.fasterxml.jackson.databind.type.MapType r1 = (com.fasterxml.jackson.databind.type.MapType) r1
            com.fasterxml.jackson.databind.BeanDescription r17 = r3.introspectForCreation(r1)
            r9 = r1
        L_0x0080:
            if (r8 != 0) goto L_0x00a0
            r0 = r17
            com.fasterxml.jackson.databind.deser.ValueInstantiator r10 = r14.findValueInstantiator(r15, r0)
            com.fasterxml.jackson.databind.deser.std.MapDeserializer r8 = new com.fasterxml.jackson.databind.deser.std.MapDeserializer
            r11 = r5
            r12 = r7
            r13 = r6
            r8.<init>(r9, r10, r11, r12, r13)
            com.fasterxml.jackson.databind.AnnotationIntrospector r1 = r3.getAnnotationIntrospector()
            com.fasterxml.jackson.databind.introspect.AnnotatedClass r2 = r17.getClassInfo()
            r4 = 0
            java.lang.String[] r1 = r1.findPropertiesToIgnore(r2, r4)
            r8.setIgnorableProperties(r1)
        L_0x00a0:
            com.fasterxml.jackson.databind.cfg.DeserializerFactoryConfig r1 = r14._factoryConfig
            boolean r1 = r1.hasDeserializerModifiers()
            if (r1 == 0) goto L_0x00ed
            com.fasterxml.jackson.databind.cfg.DeserializerFactoryConfig r1 = r14._factoryConfig
            java.lang.Iterable r1 = r1.deserializerModifiers()
            java.util.Iterator r2 = r1.iterator()
        L_0x00b2:
            boolean r1 = r2.hasNext()
            if (r1 == 0) goto L_0x00ed
            java.lang.Object r1 = r2.next()
            com.fasterxml.jackson.databind.deser.BeanDeserializerModifier r1 = (com.fasterxml.jackson.databind.deser.BeanDeserializerModifier) r1
            r0 = r17
            com.fasterxml.jackson.databind.JsonDeserializer r8 = r1.modifyMapDeserializer(r3, r9, r0, r8)
            goto L_0x00b2
        L_0x00c5:
            java.lang.Object r1 = r16.getTypeHandler()
            if (r1 != 0) goto L_0x00e6
            java.lang.IllegalArgumentException r1 = new java.lang.IllegalArgumentException
            java.lang.StringBuilder r2 = new java.lang.StringBuilder
            r2.<init>()
            java.lang.String r3 = "Can not find a deserializer for non-concrete Map type "
            java.lang.StringBuilder r2 = r2.append(r3)
            r0 = r16
            java.lang.StringBuilder r2 = r2.append(r0)
            java.lang.String r2 = r2.toString()
            r1.<init>(r2)
            throw r1
        L_0x00e6:
            com.fasterxml.jackson.databind.deser.AbstractDeserializer r8 = com.fasterxml.jackson.databind.deser.AbstractDeserializer.constructForNonPOJO(r17)
            r9 = r16
            goto L_0x0080
        L_0x00ed:
            return r8
        L_0x00ee:
            r9 = r16
            goto L_0x0080
        L_0x00f1:
            r9 = r16
            goto L_0x00a0
        L_0x00f4:
            r6 = r1
            goto L_0x0024
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.deser.BasicDeserializerFactory.createMapDeserializer(com.fasterxml.jackson.databind.DeserializationContext, com.fasterxml.jackson.databind.type.MapType, com.fasterxml.jackson.databind.BeanDescription):com.fasterxml.jackson.databind.JsonDeserializer");
    }

    public JsonDeserializer<?> createMapLikeDeserializer(DeserializationContext deserializationContext, MapLikeType mapLikeType, BeanDescription beanDescription) throws JsonMappingException {
        TypeDeserializer typeDeserializer;
        JavaType keyType = mapLikeType.getKeyType();
        JavaType contentType = mapLikeType.getContentType();
        DeserializationConfig config = deserializationContext.getConfig();
        JsonDeserializer jsonDeserializer = (JsonDeserializer) contentType.getValueHandler();
        KeyDeserializer keyDeserializer = (KeyDeserializer) keyType.getValueHandler();
        TypeDeserializer typeDeserializer2 = (TypeDeserializer) contentType.getTypeHandler();
        if (typeDeserializer2 == null) {
            typeDeserializer = findTypeDeserializer(config, contentType);
        } else {
            typeDeserializer = typeDeserializer2;
        }
        JsonDeserializer<?> _findCustomMapLikeDeserializer = _findCustomMapLikeDeserializer(mapLikeType, config, beanDescription, keyDeserializer, typeDeserializer, jsonDeserializer);
        if (_findCustomMapLikeDeserializer == null || !this._factoryConfig.hasDeserializerModifiers()) {
            return _findCustomMapLikeDeserializer;
        }
        Iterator it = this._factoryConfig.deserializerModifiers().iterator();
        while (true) {
            JsonDeserializer<?> jsonDeserializer2 = _findCustomMapLikeDeserializer;
            if (!it.hasNext()) {
                return jsonDeserializer2;
            }
            _findCustomMapLikeDeserializer = ((BeanDeserializerModifier) it.next()).modifyMapLikeDeserializer(config, mapLikeType, beanDescription, jsonDeserializer2);
        }
    }

    public JsonDeserializer<?> createEnumDeserializer(DeserializationContext deserializationContext, JavaType javaType, BeanDescription beanDescription) throws JsonMappingException {
        JsonDeserializer jsonDeserializer;
        DeserializationConfig config = deserializationContext.getConfig();
        Class rawClass = javaType.getRawClass();
        JsonDeserializer _findCustomEnumDeserializer = _findCustomEnumDeserializer(rawClass, config, beanDescription);
        if (_findCustomEnumDeserializer == null) {
            Iterator it = beanDescription.getFactoryMethods().iterator();
            while (true) {
                if (!it.hasNext()) {
                    jsonDeserializer = _findCustomEnumDeserializer;
                    break;
                }
                AnnotatedMethod annotatedMethod = (AnnotatedMethod) it.next();
                if (deserializationContext.getAnnotationIntrospector().hasCreatorAnnotation(annotatedMethod)) {
                    if (annotatedMethod.getParameterCount() != 1 || !annotatedMethod.getRawReturnType().isAssignableFrom(rawClass)) {
                        throw new IllegalArgumentException("Unsuitable method (" + annotatedMethod + ") decorated with @JsonCreator (for Enum type " + rawClass.getName() + ")");
                    }
                    jsonDeserializer = EnumDeserializer.deserializerForCreator(config, rawClass, annotatedMethod);
                }
            }
            if (jsonDeserializer == null) {
                jsonDeserializer = new EnumDeserializer(constructEnumResolver(rawClass, config, beanDescription.findJsonValueMethod()));
            }
        } else {
            jsonDeserializer = _findCustomEnumDeserializer;
        }
        if (!this._factoryConfig.hasDeserializerModifiers()) {
            return jsonDeserializer;
        }
        Iterator it2 = this._factoryConfig.deserializerModifiers().iterator();
        while (true) {
            JsonDeserializer jsonDeserializer2 = jsonDeserializer;
            if (!it2.hasNext()) {
                return jsonDeserializer2;
            }
            jsonDeserializer = ((BeanDeserializerModifier) it2.next()).modifyEnumDeserializer(config, javaType, beanDescription, jsonDeserializer2);
        }
    }

    public JsonDeserializer<?> createTreeDeserializer(DeserializationConfig deserializationConfig, JavaType javaType, BeanDescription beanDescription) throws JsonMappingException {
        Class rawClass = javaType.getRawClass();
        JsonDeserializer<?> _findCustomTreeNodeDeserializer = _findCustomTreeNodeDeserializer(rawClass, deserializationConfig, beanDescription);
        return _findCustomTreeNodeDeserializer != null ? _findCustomTreeNodeDeserializer : JsonNodeDeserializer.getDeserializer(rawClass);
    }

    public JsonDeserializer<?> createReferenceDeserializer(DeserializationContext deserializationContext, ReferenceType referenceType, BeanDescription beanDescription) throws JsonMappingException {
        TypeDeserializer typeDeserializer;
        JavaType contentType = referenceType.getContentType();
        JsonDeserializer jsonDeserializer = (JsonDeserializer) contentType.getValueHandler();
        DeserializationConfig config = deserializationContext.getConfig();
        TypeDeserializer typeDeserializer2 = (TypeDeserializer) contentType.getTypeHandler();
        if (typeDeserializer2 == null) {
            typeDeserializer = findTypeDeserializer(config, contentType);
        } else {
            typeDeserializer = typeDeserializer2;
        }
        JsonDeserializer<?> _findCustomReferenceDeserializer = _findCustomReferenceDeserializer(referenceType, config, beanDescription, typeDeserializer, jsonDeserializer);
        if (_findCustomReferenceDeserializer == null && AtomicReference.class.isAssignableFrom(referenceType.getRawClass())) {
            return new AtomicReferenceDeserializer(contentType, typeDeserializer, jsonDeserializer);
        }
        if (_findCustomReferenceDeserializer == null || !this._factoryConfig.hasDeserializerModifiers()) {
            return _findCustomReferenceDeserializer;
        }
        Iterator it = this._factoryConfig.deserializerModifiers().iterator();
        while (true) {
            JsonDeserializer<?> jsonDeserializer2 = _findCustomReferenceDeserializer;
            if (!it.hasNext()) {
                return jsonDeserializer2;
            }
            _findCustomReferenceDeserializer = ((BeanDeserializerModifier) it.next()).modifyReferenceDeserializer(config, referenceType, beanDescription, jsonDeserializer2);
        }
    }

    public TypeDeserializer findTypeDeserializer(DeserializationConfig deserializationConfig, JavaType javaType) throws JsonMappingException {
        Collection collection = null;
        AnnotatedClass classInfo = deserializationConfig.introspectClassAnnotations(javaType.getRawClass()).getClassInfo();
        TypeResolverBuilder findTypeResolver = deserializationConfig.getAnnotationIntrospector().findTypeResolver(deserializationConfig, classInfo, javaType);
        if (findTypeResolver == null) {
            findTypeResolver = deserializationConfig.getDefaultTyper(javaType);
            if (findTypeResolver == null) {
                return null;
            }
        } else {
            collection = deserializationConfig.getSubtypeResolver().collectAndResolveSubtypesByTypeId(deserializationConfig, classInfo);
        }
        if (findTypeResolver.getDefaultImpl() == null && javaType.isAbstract()) {
            JavaType mapAbstractType = mapAbstractType(deserializationConfig, javaType);
            if (!(mapAbstractType == null || mapAbstractType.getRawClass() == javaType.getRawClass())) {
                findTypeResolver = findTypeResolver.defaultImpl(mapAbstractType.getRawClass());
            }
        }
        return findTypeResolver.buildTypeDeserializer(deserializationConfig, javaType, collection);
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<?> findOptionalStdDeserializer(DeserializationContext deserializationContext, JavaType javaType, BeanDescription beanDescription) throws JsonMappingException {
        return OptionalHandlerFactory.instance.findDeserializer(javaType, deserializationContext.getConfig(), beanDescription);
    }

    public KeyDeserializer createKeyDeserializer(DeserializationContext deserializationContext, JavaType javaType) throws JsonMappingException {
        DeserializationConfig config = deserializationContext.getConfig();
        KeyDeserializer keyDeserializer = null;
        if (this._factoryConfig.hasKeyDeserializers()) {
            BeanDescription introspectClassAnnotations = config.introspectClassAnnotations(javaType.getRawClass());
            for (KeyDeserializers findKeyDeserializer : this._factoryConfig.keyDeserializers()) {
                keyDeserializer = findKeyDeserializer.findKeyDeserializer(javaType, config, introspectClassAnnotations);
                if (keyDeserializer != null) {
                    break;
                }
            }
        }
        if (keyDeserializer == null) {
            if (javaType.isEnumType()) {
                return _createEnumKeyDeserializer(deserializationContext, javaType);
            }
            keyDeserializer = StdKeyDeserializers.findStringBasedKeyDeserializer(config, javaType);
        }
        if (keyDeserializer == null || !this._factoryConfig.hasDeserializerModifiers()) {
            return keyDeserializer;
        }
        Iterator it = this._factoryConfig.deserializerModifiers().iterator();
        while (true) {
            KeyDeserializer keyDeserializer2 = keyDeserializer;
            if (!it.hasNext()) {
                return keyDeserializer2;
            }
            keyDeserializer = ((BeanDeserializerModifier) it.next()).modifyKeyDeserializer(config, javaType, keyDeserializer2);
        }
    }

    private KeyDeserializer _createEnumKeyDeserializer(DeserializationContext deserializationContext, JavaType javaType) throws JsonMappingException {
        DeserializationConfig config = deserializationContext.getConfig();
        Class rawClass = javaType.getRawClass();
        BeanDescription introspect = config.introspect(javaType);
        KeyDeserializer findKeyDeserializerFromAnnotation = findKeyDeserializerFromAnnotation(deserializationContext, introspect.getClassInfo());
        if (findKeyDeserializerFromAnnotation != null) {
            return findKeyDeserializerFromAnnotation;
        }
        JsonDeserializer _findCustomEnumDeserializer = _findCustomEnumDeserializer(rawClass, config, introspect);
        if (_findCustomEnumDeserializer != null) {
            return StdKeyDeserializers.constructDelegatingKeyDeserializer(config, javaType, _findCustomEnumDeserializer);
        }
        JsonDeserializer findDeserializerFromAnnotation = findDeserializerFromAnnotation(deserializationContext, introspect.getClassInfo());
        if (findDeserializerFromAnnotation != null) {
            return StdKeyDeserializers.constructDelegatingKeyDeserializer(config, javaType, findDeserializerFromAnnotation);
        }
        EnumResolver constructEnumResolver = constructEnumResolver(rawClass, config, introspect.findJsonValueMethod());
        AnnotationIntrospector annotationIntrospector = config.getAnnotationIntrospector();
        for (AnnotatedMethod annotatedMethod : introspect.getFactoryMethods()) {
            if (annotationIntrospector.hasCreatorAnnotation(annotatedMethod)) {
                if (annotatedMethod.getParameterCount() != 1 || !annotatedMethod.getRawReturnType().isAssignableFrom(rawClass)) {
                    throw new IllegalArgumentException("Unsuitable method (" + annotatedMethod + ") decorated with @JsonCreator (for Enum type " + rawClass.getName() + ")");
                } else if (annotatedMethod.getRawParameterType(0) != String.class) {
                    throw new IllegalArgumentException("Parameter #0 type for factory method (" + annotatedMethod + ") not suitable, must be java.lang.String");
                } else {
                    if (config.canOverrideAccessModifiers()) {
                        ClassUtil.checkAndFixAccess(annotatedMethod.getMember(), deserializationContext.isEnabled(MapperFeature.OVERRIDE_PUBLIC_ACCESS_MODIFIERS));
                    }
                    return StdKeyDeserializers.constructEnumKeyDeserializer(constructEnumResolver, annotatedMethod);
                }
            }
        }
        return StdKeyDeserializers.constructEnumKeyDeserializer(constructEnumResolver);
    }

    public TypeDeserializer findPropertyTypeDeserializer(DeserializationConfig deserializationConfig, JavaType javaType, AnnotatedMember annotatedMember) throws JsonMappingException {
        TypeResolverBuilder findPropertyTypeResolver = deserializationConfig.getAnnotationIntrospector().findPropertyTypeResolver(deserializationConfig, annotatedMember, javaType);
        if (findPropertyTypeResolver == null) {
            return findTypeDeserializer(deserializationConfig, javaType);
        }
        return findPropertyTypeResolver.buildTypeDeserializer(deserializationConfig, javaType, deserializationConfig.getSubtypeResolver().collectAndResolveSubtypesByTypeId(deserializationConfig, annotatedMember, javaType));
    }

    public TypeDeserializer findPropertyContentTypeDeserializer(DeserializationConfig deserializationConfig, JavaType javaType, AnnotatedMember annotatedMember) throws JsonMappingException {
        TypeResolverBuilder findPropertyContentTypeResolver = deserializationConfig.getAnnotationIntrospector().findPropertyContentTypeResolver(deserializationConfig, annotatedMember, javaType);
        JavaType contentType = javaType.getContentType();
        if (findPropertyContentTypeResolver == null) {
            return findTypeDeserializer(deserializationConfig, contentType);
        }
        return findPropertyContentTypeResolver.buildTypeDeserializer(deserializationConfig, contentType, deserializationConfig.getSubtypeResolver().collectAndResolveSubtypesByTypeId(deserializationConfig, annotatedMember, contentType));
    }

    public JsonDeserializer<?> findDefaultDeserializer(DeserializationContext deserializationContext, JavaType javaType, BeanDescription beanDescription) throws JsonMappingException {
        JavaType javaType2;
        JavaType javaType3;
        TypeDeserializer typeDeserializer;
        JavaType javaType4;
        JavaType javaType5;
        Class<TokenBuffer> rawClass = javaType.getRawClass();
        if (rawClass == CLASS_OBJECT) {
            DeserializationConfig config = deserializationContext.getConfig();
            if (this._factoryConfig.hasAbstractTypeResolvers()) {
                JavaType _findRemappedType = _findRemappedType(config, List.class);
                javaType4 = _findRemappedType(config, Map.class);
                javaType5 = _findRemappedType;
            } else {
                javaType4 = null;
                javaType5 = null;
            }
            return new UntypedObjectDeserializer(javaType5, javaType4);
        } else if (rawClass == CLASS_STRING || rawClass == CLASS_CHAR_BUFFER) {
            return StringDeserializer.instance;
        } else {
            if (rawClass == CLASS_ITERABLE) {
                TypeFactory typeFactory = deserializationContext.getTypeFactory();
                JavaType[] findTypeParameters = typeFactory.findTypeParameters(javaType, CLASS_ITERABLE);
                return createCollectionDeserializer(deserializationContext, typeFactory.constructCollectionType(Collection.class, (findTypeParameters == null || findTypeParameters.length != 1) ? TypeFactory.unknownType() : findTypeParameters[0]), beanDescription);
            } else if (rawClass == CLASS_MAP_ENTRY) {
                JavaType containedType = javaType.containedType(0);
                if (containedType == null) {
                    javaType2 = TypeFactory.unknownType();
                } else {
                    javaType2 = containedType;
                }
                JavaType containedType2 = javaType.containedType(1);
                if (containedType2 == null) {
                    javaType3 = TypeFactory.unknownType();
                } else {
                    javaType3 = containedType2;
                }
                TypeDeserializer typeDeserializer2 = (TypeDeserializer) javaType3.getTypeHandler();
                if (typeDeserializer2 == null) {
                    typeDeserializer = findTypeDeserializer(deserializationContext.getConfig(), javaType3);
                } else {
                    typeDeserializer = typeDeserializer2;
                }
                return new MapEntryDeserializer(javaType, (KeyDeserializer) javaType2.getValueHandler(), (JsonDeserializer) javaType3.getValueHandler(), typeDeserializer);
            } else {
                String name = rawClass.getName();
                if (rawClass.isPrimitive() || name.startsWith("java.")) {
                    JsonDeserializer<?> find = NumberDeserializers.find(rawClass, name);
                    if (find == null) {
                        find = DateDeserializers.find(rawClass, name);
                    }
                    if (find != null) {
                        return find;
                    }
                }
                if (rawClass == TokenBuffer.class) {
                    return new TokenBufferDeserializer();
                }
                JsonDeserializer<?> findOptionalStdDeserializer = findOptionalStdDeserializer(deserializationContext, javaType, beanDescription);
                return findOptionalStdDeserializer == null ? JdkDeserializers.find(rawClass, name) : findOptionalStdDeserializer;
            }
        }
    }

    /* access modifiers changed from: protected */
    public JavaType _findRemappedType(DeserializationConfig deserializationConfig, Class<?> cls) throws JsonMappingException {
        JavaType mapAbstractType = mapAbstractType(deserializationConfig, deserializationConfig.constructType(cls));
        if (mapAbstractType == null || mapAbstractType.hasRawClass(cls)) {
            return null;
        }
        return mapAbstractType;
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<?> _findCustomTreeNodeDeserializer(Class<? extends JsonNode> cls, DeserializationConfig deserializationConfig, BeanDescription beanDescription) throws JsonMappingException {
        for (Deserializers findTreeNodeDeserializer : this._factoryConfig.deserializers()) {
            JsonDeserializer<?> findTreeNodeDeserializer2 = findTreeNodeDeserializer.findTreeNodeDeserializer(cls, deserializationConfig, beanDescription);
            if (findTreeNodeDeserializer2 != null) {
                return findTreeNodeDeserializer2;
            }
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<?> _findCustomReferenceDeserializer(ReferenceType referenceType, DeserializationConfig deserializationConfig, BeanDescription beanDescription, TypeDeserializer typeDeserializer, JsonDeserializer<?> jsonDeserializer) throws JsonMappingException {
        for (Deserializers findReferenceDeserializer : this._factoryConfig.deserializers()) {
            JsonDeserializer<?> findReferenceDeserializer2 = findReferenceDeserializer.findReferenceDeserializer(referenceType, deserializationConfig, beanDescription, typeDeserializer, jsonDeserializer);
            if (findReferenceDeserializer2 != null) {
                return findReferenceDeserializer2;
            }
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<Object> _findCustomBeanDeserializer(JavaType javaType, DeserializationConfig deserializationConfig, BeanDescription beanDescription) throws JsonMappingException {
        for (Deserializers findBeanDeserializer : this._factoryConfig.deserializers()) {
            JsonDeserializer<Object> findBeanDeserializer2 = findBeanDeserializer.findBeanDeserializer(javaType, deserializationConfig, beanDescription);
            if (findBeanDeserializer2 != null) {
                return findBeanDeserializer2;
            }
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<?> _findCustomArrayDeserializer(ArrayType arrayType, DeserializationConfig deserializationConfig, BeanDescription beanDescription, TypeDeserializer typeDeserializer, JsonDeserializer<?> jsonDeserializer) throws JsonMappingException {
        for (Deserializers findArrayDeserializer : this._factoryConfig.deserializers()) {
            JsonDeserializer<?> findArrayDeserializer2 = findArrayDeserializer.findArrayDeserializer(arrayType, deserializationConfig, beanDescription, typeDeserializer, jsonDeserializer);
            if (findArrayDeserializer2 != null) {
                return findArrayDeserializer2;
            }
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<?> _findCustomCollectionDeserializer(CollectionType collectionType, DeserializationConfig deserializationConfig, BeanDescription beanDescription, TypeDeserializer typeDeserializer, JsonDeserializer<?> jsonDeserializer) throws JsonMappingException {
        for (Deserializers findCollectionDeserializer : this._factoryConfig.deserializers()) {
            JsonDeserializer<?> findCollectionDeserializer2 = findCollectionDeserializer.findCollectionDeserializer(collectionType, deserializationConfig, beanDescription, typeDeserializer, jsonDeserializer);
            if (findCollectionDeserializer2 != null) {
                return findCollectionDeserializer2;
            }
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<?> _findCustomCollectionLikeDeserializer(CollectionLikeType collectionLikeType, DeserializationConfig deserializationConfig, BeanDescription beanDescription, TypeDeserializer typeDeserializer, JsonDeserializer<?> jsonDeserializer) throws JsonMappingException {
        for (Deserializers findCollectionLikeDeserializer : this._factoryConfig.deserializers()) {
            JsonDeserializer<?> findCollectionLikeDeserializer2 = findCollectionLikeDeserializer.findCollectionLikeDeserializer(collectionLikeType, deserializationConfig, beanDescription, typeDeserializer, jsonDeserializer);
            if (findCollectionLikeDeserializer2 != null) {
                return findCollectionLikeDeserializer2;
            }
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<?> _findCustomEnumDeserializer(Class<?> cls, DeserializationConfig deserializationConfig, BeanDescription beanDescription) throws JsonMappingException {
        for (Deserializers findEnumDeserializer : this._factoryConfig.deserializers()) {
            JsonDeserializer<?> findEnumDeserializer2 = findEnumDeserializer.findEnumDeserializer(cls, deserializationConfig, beanDescription);
            if (findEnumDeserializer2 != null) {
                return findEnumDeserializer2;
            }
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<?> _findCustomMapDeserializer(MapType mapType, DeserializationConfig deserializationConfig, BeanDescription beanDescription, KeyDeserializer keyDeserializer, TypeDeserializer typeDeserializer, JsonDeserializer<?> jsonDeserializer) throws JsonMappingException {
        for (Deserializers findMapDeserializer : this._factoryConfig.deserializers()) {
            JsonDeserializer<?> findMapDeserializer2 = findMapDeserializer.findMapDeserializer(mapType, deserializationConfig, beanDescription, keyDeserializer, typeDeserializer, jsonDeserializer);
            if (findMapDeserializer2 != null) {
                return findMapDeserializer2;
            }
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<?> _findCustomMapLikeDeserializer(MapLikeType mapLikeType, DeserializationConfig deserializationConfig, BeanDescription beanDescription, KeyDeserializer keyDeserializer, TypeDeserializer typeDeserializer, JsonDeserializer<?> jsonDeserializer) throws JsonMappingException {
        for (Deserializers findMapLikeDeserializer : this._factoryConfig.deserializers()) {
            JsonDeserializer<?> findMapLikeDeserializer2 = findMapLikeDeserializer.findMapLikeDeserializer(mapLikeType, deserializationConfig, beanDescription, keyDeserializer, typeDeserializer, jsonDeserializer);
            if (findMapLikeDeserializer2 != null) {
                return findMapLikeDeserializer2;
            }
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<Object> findDeserializerFromAnnotation(DeserializationContext deserializationContext, Annotated annotated) throws JsonMappingException {
        Object findDeserializer = deserializationContext.getAnnotationIntrospector().findDeserializer(annotated);
        if (findDeserializer == null) {
            return null;
        }
        return deserializationContext.deserializerInstance(annotated, findDeserializer);
    }

    /* access modifiers changed from: protected */
    public KeyDeserializer findKeyDeserializerFromAnnotation(DeserializationContext deserializationContext, Annotated annotated) throws JsonMappingException {
        Object findKeyDeserializer = deserializationContext.getAnnotationIntrospector().findKeyDeserializer(annotated);
        if (findKeyDeserializer == null) {
            return null;
        }
        return deserializationContext.keyDeserializerInstance(annotated, findKeyDeserializer);
    }

    /* access modifiers changed from: protected */
    public <T extends JavaType> T modifyTypeByAnnotation(DeserializationContext deserializationContext, Annotated annotated, T t) throws JsonMappingException {
        AnnotationIntrospector annotationIntrospector = deserializationContext.getAnnotationIntrospector();
        if (annotationIntrospector == null) {
            return t;
        }
        if (t.isMapLikeType()) {
            JavaType keyType = t.getKeyType();
            if (keyType != null && keyType.getValueHandler() == null) {
                KeyDeserializer keyDeserializerInstance = deserializationContext.keyDeserializerInstance(annotated, annotationIntrospector.findKeyDeserializer(annotated));
                if (keyDeserializerInstance != null) {
                    t = ((MapLikeType) t).withKeyValueHandler(keyDeserializerInstance);
                    t.getKeyType();
                }
            }
        }
        JavaType contentType = t.getContentType();
        if (contentType != null && contentType.getValueHandler() == null) {
            JsonDeserializer deserializerInstance = deserializationContext.deserializerInstance(annotated, annotationIntrospector.findContentDeserializer(annotated));
            if (deserializerInstance != null) {
                t = t.withContentValueHandler(deserializerInstance);
            }
        }
        return annotationIntrospector.refineDeserializationType(deserializationContext.getConfig(), annotated, t);
    }

    /* access modifiers changed from: protected */
    public JavaType resolveType(DeserializationContext deserializationContext, BeanDescription beanDescription, JavaType javaType, AnnotatedMember annotatedMember) throws JsonMappingException {
        TypeDeserializer findTypeDeserializer;
        AnnotationIntrospector annotationIntrospector = deserializationContext.getAnnotationIntrospector();
        if (annotationIntrospector == null) {
            return javaType;
        }
        if (javaType.isMapLikeType() && javaType.getKeyType() != null) {
            KeyDeserializer keyDeserializerInstance = deserializationContext.keyDeserializerInstance(annotatedMember, annotationIntrospector.findKeyDeserializer(annotatedMember));
            if (keyDeserializerInstance != null) {
                javaType = ((MapLikeType) javaType).withKeyValueHandler(keyDeserializerInstance);
                javaType.getKeyType();
            }
        }
        if (javaType.isContainerType() || javaType.isReferenceType()) {
            JsonDeserializer deserializerInstance = deserializationContext.deserializerInstance(annotatedMember, annotationIntrospector.findContentDeserializer(annotatedMember));
            if (deserializerInstance != null) {
                javaType = javaType.withContentValueHandler(deserializerInstance);
            }
            if (annotatedMember instanceof AnnotatedMember) {
                TypeDeserializer findPropertyContentTypeDeserializer = findPropertyContentTypeDeserializer(deserializationContext.getConfig(), javaType, annotatedMember);
                if (findPropertyContentTypeDeserializer != null) {
                    javaType = javaType.withContentTypeHandler(findPropertyContentTypeDeserializer);
                }
            }
        }
        if (annotatedMember instanceof AnnotatedMember) {
            findTypeDeserializer = findPropertyTypeDeserializer(deserializationContext.getConfig(), javaType, annotatedMember);
        } else {
            findTypeDeserializer = findTypeDeserializer(deserializationContext.getConfig(), javaType);
        }
        if (findTypeDeserializer != null) {
            return javaType.withTypeHandler(findTypeDeserializer);
        }
        return javaType;
    }

    /* access modifiers changed from: protected */
    public EnumResolver constructEnumResolver(Class<?> cls, DeserializationConfig deserializationConfig, AnnotatedMethod annotatedMethod) {
        if (annotatedMethod == null) {
            return EnumResolver.constructUnsafe(cls, deserializationConfig.getAnnotationIntrospector());
        }
        Method annotated = annotatedMethod.getAnnotated();
        if (deserializationConfig.canOverrideAccessModifiers()) {
            ClassUtil.checkAndFixAccess(annotated, deserializationConfig.isEnabled(MapperFeature.OVERRIDE_PUBLIC_ACCESS_MODIFIERS));
        }
        return EnumResolver.constructUnsafeUsingMethod(cls, annotated);
    }

    /* access modifiers changed from: protected */
    public AnnotatedMethod _findJsonValueFor(DeserializationConfig deserializationConfig, JavaType javaType) {
        if (javaType == null) {
            return null;
        }
        return deserializationConfig.introspect(javaType).findJsonValueMethod();
    }
}
