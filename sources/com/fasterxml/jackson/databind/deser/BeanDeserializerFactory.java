package com.fasterxml.jackson.databind.deser;

import com.fasterxml.jackson.annotation.ObjectIdGenerator;
import com.fasterxml.jackson.annotation.ObjectIdGenerators.PropertyGenerator;
import com.fasterxml.jackson.annotation.ObjectIdResolver;
import com.fasterxml.jackson.databind.AbstractTypeResolver;
import com.fasterxml.jackson.databind.AnnotationIntrospector.ReferenceProperty;
import com.fasterxml.jackson.databind.BeanDescription;
import com.fasterxml.jackson.databind.BeanProperty.Std;
import com.fasterxml.jackson.databind.DeserializationConfig;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.JsonDeserializer;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.MapperFeature;
import com.fasterxml.jackson.databind.PropertyMetadata;
import com.fasterxml.jackson.databind.PropertyName;
import com.fasterxml.jackson.databind.annotation.JsonPOJOBuilder.Value;
import com.fasterxml.jackson.databind.cfg.DeserializerFactoryConfig;
import com.fasterxml.jackson.databind.cfg.MapperConfig;
import com.fasterxml.jackson.databind.deser.impl.FieldProperty;
import com.fasterxml.jackson.databind.deser.impl.MethodProperty;
import com.fasterxml.jackson.databind.deser.impl.NoClassDefFoundDeserializer;
import com.fasterxml.jackson.databind.deser.impl.ObjectIdReader;
import com.fasterxml.jackson.databind.deser.impl.PropertyBasedObjectIdGenerator;
import com.fasterxml.jackson.databind.deser.impl.SetterlessProperty;
import com.fasterxml.jackson.databind.deser.std.ThrowableDeserializer;
import com.fasterxml.jackson.databind.introspect.AnnotatedField;
import com.fasterxml.jackson.databind.introspect.AnnotatedMember;
import com.fasterxml.jackson.databind.introspect.AnnotatedMethod;
import com.fasterxml.jackson.databind.introspect.BeanPropertyDefinition;
import com.fasterxml.jackson.databind.introspect.ObjectIdInfo;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import com.fasterxml.jackson.databind.util.ClassUtil;
import com.fasterxml.jackson.databind.util.SimpleBeanPropertyDefinition;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;

public class BeanDeserializerFactory extends BasicDeserializerFactory implements Serializable {
    private static final Class<?>[] INIT_CAUSE_PARAMS = {Throwable.class};
    private static final Class<?>[] NO_VIEWS = new Class[0];
    public static final BeanDeserializerFactory instance = new BeanDeserializerFactory(new DeserializerFactoryConfig());
    private static final long serialVersionUID = 1;

    public BeanDeserializerFactory(DeserializerFactoryConfig deserializerFactoryConfig) {
        super(deserializerFactoryConfig);
    }

    public DeserializerFactory withConfig(DeserializerFactoryConfig deserializerFactoryConfig) {
        if (this._factoryConfig == deserializerFactoryConfig) {
            return this;
        }
        if (getClass() == BeanDeserializerFactory.class) {
            return new BeanDeserializerFactory(deserializerFactoryConfig);
        }
        throw new IllegalStateException("Subtype of BeanDeserializerFactory (" + getClass().getName() + ") has not properly overridden method 'withAdditionalDeserializers': can not instantiate subtype with " + "additional deserializer definitions");
    }

    public JsonDeserializer<Object> createBeanDeserializer(DeserializationContext deserializationContext, JavaType javaType, BeanDescription beanDescription) throws JsonMappingException {
        DeserializationConfig config = deserializationContext.getConfig();
        JsonDeserializer<Object> _findCustomBeanDeserializer = _findCustomBeanDeserializer(javaType, config, beanDescription);
        if (_findCustomBeanDeserializer != null) {
            return _findCustomBeanDeserializer;
        }
        if (javaType.isThrowable()) {
            return buildThrowableDeserializer(deserializationContext, javaType, beanDescription);
        }
        if (javaType.isAbstract() && !javaType.isPrimitive()) {
            JavaType materializeAbstractType = materializeAbstractType(deserializationContext, javaType, beanDescription);
            if (materializeAbstractType != null) {
                return buildBeanDeserializer(deserializationContext, materializeAbstractType, config.introspect(materializeAbstractType));
            }
        }
        JsonDeserializer<Object> findStdDeserializer = findStdDeserializer(deserializationContext, javaType, beanDescription);
        if (findStdDeserializer != null) {
            return findStdDeserializer;
        }
        if (!isPotentialBeanType(javaType.getRawClass())) {
            return null;
        }
        return buildBeanDeserializer(deserializationContext, javaType, beanDescription);
    }

    public JsonDeserializer<Object> createBuilderBasedDeserializer(DeserializationContext deserializationContext, JavaType javaType, BeanDescription beanDescription, Class<?> cls) throws JsonMappingException {
        return buildBuilderBasedDeserializer(deserializationContext, javaType, deserializationContext.getConfig().introspectForBuilder(deserializationContext.constructType(cls)));
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<?> findStdDeserializer(DeserializationContext deserializationContext, JavaType javaType, BeanDescription beanDescription) throws JsonMappingException {
        JsonDeserializer<?> findDefaultDeserializer = findDefaultDeserializer(deserializationContext, javaType, beanDescription);
        if (findDefaultDeserializer == null || !this._factoryConfig.hasDeserializerModifiers()) {
            return findDefaultDeserializer;
        }
        Iterator it = this._factoryConfig.deserializerModifiers().iterator();
        while (true) {
            JsonDeserializer<?> jsonDeserializer = findDefaultDeserializer;
            if (!it.hasNext()) {
                return jsonDeserializer;
            }
            findDefaultDeserializer = ((BeanDeserializerModifier) it.next()).modifyDeserializer(deserializationContext.getConfig(), beanDescription, jsonDeserializer);
        }
    }

    /* access modifiers changed from: protected */
    public JavaType materializeAbstractType(DeserializationContext deserializationContext, JavaType javaType, BeanDescription beanDescription) throws JsonMappingException {
        for (AbstractTypeResolver resolveAbstractType : this._factoryConfig.abstractTypeResolvers()) {
            JavaType resolveAbstractType2 = resolveAbstractType.resolveAbstractType(deserializationContext.getConfig(), beanDescription);
            if (resolveAbstractType2 != null) {
                return resolveAbstractType2;
            }
        }
        return null;
    }

    public JsonDeserializer<Object> buildBeanDeserializer(DeserializationContext deserializationContext, JavaType javaType, BeanDescription beanDescription) throws JsonMappingException {
        BeanDeserializerBuilder beanDeserializerBuilder;
        JsonDeserializer build;
        try {
            ValueInstantiator findValueInstantiator = findValueInstantiator(deserializationContext, beanDescription);
            BeanDeserializerBuilder constructBeanDeserializerBuilder = constructBeanDeserializerBuilder(deserializationContext, beanDescription);
            constructBeanDeserializerBuilder.setValueInstantiator(findValueInstantiator);
            addBeanProps(deserializationContext, beanDescription, constructBeanDeserializerBuilder);
            addObjectIdReader(deserializationContext, beanDescription, constructBeanDeserializerBuilder);
            addReferenceProperties(deserializationContext, beanDescription, constructBeanDeserializerBuilder);
            addInjectables(deserializationContext, beanDescription, constructBeanDeserializerBuilder);
            DeserializationConfig config = deserializationContext.getConfig();
            if (this._factoryConfig.hasDeserializerModifiers()) {
                Iterator it = this._factoryConfig.deserializerModifiers().iterator();
                while (true) {
                    beanDeserializerBuilder = constructBeanDeserializerBuilder;
                    if (!it.hasNext()) {
                        break;
                    }
                    constructBeanDeserializerBuilder = ((BeanDeserializerModifier) it.next()).updateBuilder(config, beanDescription, beanDeserializerBuilder);
                }
            } else {
                beanDeserializerBuilder = constructBeanDeserializerBuilder;
            }
            if (!javaType.isAbstract() || findValueInstantiator.canInstantiate()) {
                build = beanDeserializerBuilder.build();
            } else {
                build = beanDeserializerBuilder.buildAbstract();
            }
            if (!this._factoryConfig.hasDeserializerModifiers()) {
                return build;
            }
            Iterator it2 = this._factoryConfig.deserializerModifiers().iterator();
            while (true) {
                JsonDeserializer jsonDeserializer = build;
                if (!it2.hasNext()) {
                    return jsonDeserializer;
                }
                build = ((BeanDeserializerModifier) it2.next()).modifyDeserializer(config, beanDescription, jsonDeserializer);
            }
        } catch (NoClassDefFoundError e) {
            return new NoClassDefFoundDeserializer(e);
        }
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<Object> buildBuilderBasedDeserializer(DeserializationContext deserializationContext, JavaType javaType, BeanDescription beanDescription) throws JsonMappingException {
        ValueInstantiator findValueInstantiator = findValueInstantiator(deserializationContext, beanDescription);
        DeserializationConfig config = deserializationContext.getConfig();
        BeanDeserializerBuilder constructBeanDeserializerBuilder = constructBeanDeserializerBuilder(deserializationContext, beanDescription);
        constructBeanDeserializerBuilder.setValueInstantiator(findValueInstantiator);
        addBeanProps(deserializationContext, beanDescription, constructBeanDeserializerBuilder);
        addObjectIdReader(deserializationContext, beanDescription, constructBeanDeserializerBuilder);
        addReferenceProperties(deserializationContext, beanDescription, constructBeanDeserializerBuilder);
        addInjectables(deserializationContext, beanDescription, constructBeanDeserializerBuilder);
        Value findPOJOBuilderConfig = beanDescription.findPOJOBuilderConfig();
        String str = findPOJOBuilderConfig == null ? "build" : findPOJOBuilderConfig.buildMethodName;
        AnnotatedMethod findMethod = beanDescription.findMethod(str, null);
        if (findMethod != null && config.canOverrideAccessModifiers()) {
            ClassUtil.checkAndFixAccess(findMethod.getMember(), config.isEnabled(MapperFeature.OVERRIDE_PUBLIC_ACCESS_MODIFIERS));
        }
        constructBeanDeserializerBuilder.setPOJOBuilder(findMethod, findPOJOBuilderConfig);
        if (this._factoryConfig.hasDeserializerModifiers()) {
            for (BeanDeserializerModifier updateBuilder : this._factoryConfig.deserializerModifiers()) {
                constructBeanDeserializerBuilder = updateBuilder.updateBuilder(config, beanDescription, constructBeanDeserializerBuilder);
            }
        }
        JsonDeserializer buildBuilderBased = constructBeanDeserializerBuilder.buildBuilderBased(javaType, str);
        if (!this._factoryConfig.hasDeserializerModifiers()) {
            return buildBuilderBased;
        }
        Iterator it = this._factoryConfig.deserializerModifiers().iterator();
        while (true) {
            JsonDeserializer jsonDeserializer = buildBuilderBased;
            if (!it.hasNext()) {
                return jsonDeserializer;
            }
            buildBuilderBased = ((BeanDeserializerModifier) it.next()).modifyDeserializer(config, beanDescription, jsonDeserializer);
        }
    }

    /* access modifiers changed from: protected */
    public void addObjectIdReader(DeserializationContext deserializationContext, BeanDescription beanDescription, BeanDeserializerBuilder beanDeserializerBuilder) throws JsonMappingException {
        JavaType javaType;
        SettableBeanProperty settableBeanProperty;
        ObjectIdGenerator objectIdGeneratorInstance;
        ObjectIdInfo objectIdInfo = beanDescription.getObjectIdInfo();
        if (objectIdInfo != null) {
            Class<PropertyGenerator> generatorType = objectIdInfo.getGeneratorType();
            ObjectIdResolver objectIdResolverInstance = deserializationContext.objectIdResolverInstance(beanDescription.getClassInfo(), objectIdInfo);
            if (generatorType == PropertyGenerator.class) {
                PropertyName propertyName = objectIdInfo.getPropertyName();
                settableBeanProperty = beanDeserializerBuilder.findProperty(propertyName);
                if (settableBeanProperty == null) {
                    throw new IllegalArgumentException("Invalid Object Id definition for " + beanDescription.getBeanClass().getName() + ": can not find property with name '" + propertyName + "'");
                }
                javaType = settableBeanProperty.getType();
                objectIdGeneratorInstance = new PropertyBasedObjectIdGenerator(objectIdInfo.getScope());
            } else {
                javaType = deserializationContext.getTypeFactory().findTypeParameters(deserializationContext.constructType(generatorType), ObjectIdGenerator.class)[0];
                settableBeanProperty = null;
                objectIdGeneratorInstance = deserializationContext.objectIdGeneratorInstance(beanDescription.getClassInfo(), objectIdInfo);
            }
            beanDeserializerBuilder.setObjectIdReader(ObjectIdReader.construct(javaType, objectIdInfo.getPropertyName(), objectIdGeneratorInstance, deserializationContext.findRootValueDeserializer(javaType), settableBeanProperty, objectIdResolverInstance));
        }
    }

    public JsonDeserializer<Object> buildThrowableDeserializer(DeserializationContext deserializationContext, JavaType javaType, BeanDescription beanDescription) throws JsonMappingException {
        BeanDeserializerBuilder beanDeserializerBuilder;
        DeserializationConfig config = deserializationContext.getConfig();
        BeanDeserializerBuilder constructBeanDeserializerBuilder = constructBeanDeserializerBuilder(deserializationContext, beanDescription);
        constructBeanDeserializerBuilder.setValueInstantiator(findValueInstantiator(deserializationContext, beanDescription));
        addBeanProps(deserializationContext, beanDescription, constructBeanDeserializerBuilder);
        AnnotatedMethod findMethod = beanDescription.findMethod("initCause", INIT_CAUSE_PARAMS);
        if (findMethod != null) {
            SettableBeanProperty constructSettableProperty = constructSettableProperty(deserializationContext, beanDescription, SimpleBeanPropertyDefinition.construct((MapperConfig<?>) deserializationContext.getConfig(), (AnnotatedMember) findMethod, new PropertyName("cause")), findMethod.getParameterType(0));
            if (constructSettableProperty != null) {
                constructBeanDeserializerBuilder.addOrReplaceProperty(constructSettableProperty, true);
            }
        }
        constructBeanDeserializerBuilder.addIgnorable("localizedMessage");
        constructBeanDeserializerBuilder.addIgnorable("suppressed");
        constructBeanDeserializerBuilder.addIgnorable("message");
        if (this._factoryConfig.hasDeserializerModifiers()) {
            Iterator it = this._factoryConfig.deserializerModifiers().iterator();
            while (true) {
                beanDeserializerBuilder = constructBeanDeserializerBuilder;
                if (!it.hasNext()) {
                    break;
                }
                constructBeanDeserializerBuilder = ((BeanDeserializerModifier) it.next()).updateBuilder(config, beanDescription, beanDeserializerBuilder);
            }
        } else {
            beanDeserializerBuilder = constructBeanDeserializerBuilder;
        }
        JsonDeserializer build = beanDeserializerBuilder.build();
        if (build instanceof BeanDeserializer) {
            build = new ThrowableDeserializer((BeanDeserializer) build);
        }
        if (!this._factoryConfig.hasDeserializerModifiers()) {
            return build;
        }
        Iterator it2 = this._factoryConfig.deserializerModifiers().iterator();
        while (true) {
            JsonDeserializer jsonDeserializer = build;
            if (!it2.hasNext()) {
                return jsonDeserializer;
            }
            build = ((BeanDeserializerModifier) it2.next()).modifyDeserializer(config, beanDescription, jsonDeserializer);
        }
    }

    /* access modifiers changed from: protected */
    public BeanDeserializerBuilder constructBeanDeserializerBuilder(DeserializationContext deserializationContext, BeanDescription beanDescription) {
        return new BeanDeserializerBuilder(beanDescription, deserializationContext.getConfig());
    }

    /* access modifiers changed from: protected */
    /* JADX WARNING: Removed duplicated region for block: B:67:0x0168  */
    /* JADX WARNING: Removed duplicated region for block: B:85:0x0112 A[SYNTHETIC] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void addBeanProps(com.fasterxml.jackson.databind.DeserializationContext r12, com.fasterxml.jackson.databind.BeanDescription r13, com.fasterxml.jackson.databind.deser.BeanDeserializerBuilder r14) throws com.fasterxml.jackson.databind.JsonMappingException {
        /*
            r11 = this;
            com.fasterxml.jackson.databind.deser.ValueInstantiator r0 = r14.getValueInstantiator()
            com.fasterxml.jackson.databind.DeserializationConfig r1 = r12.getConfig()
            com.fasterxml.jackson.databind.deser.SettableBeanProperty[] r8 = r0.getFromObjectArguments(r1)
            com.fasterxml.jackson.databind.JavaType r0 = r13.getType()
            boolean r0 = r0.isAbstract()
            if (r0 != 0) goto L_0x004e
            r0 = 1
            r6 = r0
        L_0x0018:
            com.fasterxml.jackson.databind.AnnotationIntrospector r0 = r12.getAnnotationIntrospector()
            com.fasterxml.jackson.databind.introspect.AnnotatedClass r1 = r13.getClassInfo()
            java.lang.Boolean r1 = r0.findIgnoreUnknownProperties(r1)
            if (r1 == 0) goto L_0x002d
            boolean r1 = r1.booleanValue()
            r14.setIgnoreUnknownProperties(r1)
        L_0x002d:
            com.fasterxml.jackson.databind.introspect.AnnotatedClass r1 = r13.getClassInfo()
            r2 = 0
            java.lang.String[] r0 = r0.findPropertiesToIgnore(r1, r2)
            java.util.HashSet r5 = com.fasterxml.jackson.databind.util.ArrayBuilders.arrayToSet(r0)
            java.util.Iterator r1 = r5.iterator()
        L_0x003e:
            boolean r0 = r1.hasNext()
            if (r0 == 0) goto L_0x0051
            java.lang.Object r0 = r1.next()
            java.lang.String r0 = (java.lang.String) r0
            r14.addIgnorable(r0)
            goto L_0x003e
        L_0x004e:
            r0 = 0
            r6 = r0
            goto L_0x0018
        L_0x0051:
            com.fasterxml.jackson.databind.introspect.AnnotatedMethod r0 = r13.findAnySetter()
            if (r0 == 0) goto L_0x005e
            com.fasterxml.jackson.databind.deser.SettableAnyProperty r1 = r11.constructAnySetter(r12, r13, r0)
            r14.setAnySetter(r1)
        L_0x005e:
            if (r0 != 0) goto L_0x007a
            java.util.Set r0 = r13.getIgnoredPropertyNames()
            if (r0 == 0) goto L_0x007a
            java.util.Iterator r1 = r0.iterator()
        L_0x006a:
            boolean r0 = r1.hasNext()
            if (r0 == 0) goto L_0x007a
            java.lang.Object r0 = r1.next()
            java.lang.String r0 = (java.lang.String) r0
            r14.addIgnorable(r0)
            goto L_0x006a
        L_0x007a:
            com.fasterxml.jackson.databind.MapperFeature r0 = com.fasterxml.jackson.databind.MapperFeature.USE_GETTERS_AS_SETTERS
            boolean r0 = r12.isEnabled(r0)
            if (r0 == 0) goto L_0x00c1
            com.fasterxml.jackson.databind.MapperFeature r0 = com.fasterxml.jackson.databind.MapperFeature.AUTO_DETECT_GETTERS
            boolean r0 = r12.isEnabled(r0)
            if (r0 == 0) goto L_0x00c1
            r0 = 1
            r7 = r0
        L_0x008c:
            java.util.List r4 = r13.findProperties()
            r0 = r11
            r1 = r12
            r2 = r13
            r3 = r14
            java.util.List r0 = r0.filterBeanProps(r1, r2, r3, r4, r5)
            com.fasterxml.jackson.databind.cfg.DeserializerFactoryConfig r1 = r11._factoryConfig
            boolean r1 = r1.hasDeserializerModifiers()
            if (r1 == 0) goto L_0x00c4
            com.fasterxml.jackson.databind.cfg.DeserializerFactoryConfig r1 = r11._factoryConfig
            java.lang.Iterable r1 = r1.deserializerModifiers()
            java.util.Iterator r2 = r1.iterator()
            r1 = r0
        L_0x00ab:
            boolean r0 = r2.hasNext()
            if (r0 == 0) goto L_0x00c5
            java.lang.Object r0 = r2.next()
            com.fasterxml.jackson.databind.deser.BeanDeserializerModifier r0 = (com.fasterxml.jackson.databind.deser.BeanDeserializerModifier) r0
            com.fasterxml.jackson.databind.DeserializationConfig r3 = r12.getConfig()
            java.util.List r0 = r0.updateProperties(r3, r13, r1)
            r1 = r0
            goto L_0x00ab
        L_0x00c1:
            r0 = 0
            r7 = r0
            goto L_0x008c
        L_0x00c4:
            r1 = r0
        L_0x00c5:
            java.util.Iterator r4 = r1.iterator()
        L_0x00c9:
            boolean r0 = r4.hasNext()
            if (r0 == 0) goto L_0x018c
            java.lang.Object r0 = r4.next()
            com.fasterxml.jackson.databind.introspect.BeanPropertyDefinition r0 = (com.fasterxml.jackson.databind.introspect.BeanPropertyDefinition) r0
            r1 = 0
            boolean r2 = r0.hasSetter()
            if (r2 == 0) goto L_0x012a
            com.fasterxml.jackson.databind.introspect.AnnotatedMethod r1 = r0.getSetter()
            r2 = 0
            com.fasterxml.jackson.databind.JavaType r1 = r1.getParameterType(r2)
            com.fasterxml.jackson.databind.deser.SettableBeanProperty r1 = r11.constructSettableProperty(r12, r13, r0, r1)
            r3 = r1
        L_0x00ea:
            if (r6 == 0) goto L_0x0172
            boolean r1 = r0.hasConstructorParameter()
            if (r1 == 0) goto L_0x0172
            java.lang.String r5 = r0.getName()
            r1 = 0
            if (r8 == 0) goto L_0x018d
            int r9 = r8.length
            r0 = 0
            r2 = r0
        L_0x00fc:
            if (r2 >= r9) goto L_0x018d
            r0 = r8[r2]
            java.lang.String r10 = r0.getName()
            boolean r10 = r5.equals(r10)
            if (r10 == 0) goto L_0x0164
            boolean r10 = r0 instanceof com.fasterxml.jackson.databind.deser.CreatorProperty
            if (r10 == 0) goto L_0x0164
            com.fasterxml.jackson.databind.deser.CreatorProperty r0 = (com.fasterxml.jackson.databind.deser.CreatorProperty) r0
        L_0x0110:
            if (r0 != 0) goto L_0x0168
            java.lang.String r0 = "Could not find creator property with name '%s' (in class %s)"
            r1 = 2
            java.lang.Object[] r1 = new java.lang.Object[r1]
            r2 = 0
            r1[r2] = r5
            r2 = 1
            java.lang.Class r3 = r13.getBeanClass()
            java.lang.String r3 = r3.getName()
            r1[r2] = r3
            com.fasterxml.jackson.databind.JsonMappingException r0 = r12.mappingException(r0, r1)
            throw r0
        L_0x012a:
            boolean r2 = r0.hasField()
            if (r2 == 0) goto L_0x013e
            com.fasterxml.jackson.databind.introspect.AnnotatedField r1 = r0.getField()
            com.fasterxml.jackson.databind.JavaType r1 = r1.getType()
            com.fasterxml.jackson.databind.deser.SettableBeanProperty r1 = r11.constructSettableProperty(r12, r13, r0, r1)
            r3 = r1
            goto L_0x00ea
        L_0x013e:
            if (r7 == 0) goto L_0x018f
            boolean r2 = r0.hasGetter()
            if (r2 == 0) goto L_0x018f
            com.fasterxml.jackson.databind.introspect.AnnotatedMethod r2 = r0.getGetter()
            java.lang.Class r2 = r2.getRawType()
            java.lang.Class<java.util.Collection> r3 = java.util.Collection.class
            boolean r3 = r3.isAssignableFrom(r2)
            if (r3 != 0) goto L_0x015e
            java.lang.Class<java.util.Map> r3 = java.util.Map.class
            boolean r2 = r3.isAssignableFrom(r2)
            if (r2 == 0) goto L_0x018f
        L_0x015e:
            com.fasterxml.jackson.databind.deser.SettableBeanProperty r1 = r11.constructSetterlessProperty(r12, r13, r0)
            r3 = r1
            goto L_0x00ea
        L_0x0164:
            int r0 = r2 + 1
            r2 = r0
            goto L_0x00fc
        L_0x0168:
            if (r3 == 0) goto L_0x016d
            r0.setFallbackSetter(r3)
        L_0x016d:
            r14.addCreatorProperty(r0)
            goto L_0x00c9
        L_0x0172:
            if (r3 == 0) goto L_0x00c9
            java.lang.Class[] r0 = r0.findViews()
            if (r0 != 0) goto L_0x0184
            com.fasterxml.jackson.databind.MapperFeature r1 = com.fasterxml.jackson.databind.MapperFeature.DEFAULT_VIEW_INCLUSION
            boolean r1 = r12.isEnabled(r1)
            if (r1 != 0) goto L_0x0184
            java.lang.Class<?>[] r0 = NO_VIEWS
        L_0x0184:
            r3.setViews(r0)
            r14.addProperty(r3)
            goto L_0x00c9
        L_0x018c:
            return
        L_0x018d:
            r0 = r1
            goto L_0x0110
        L_0x018f:
            r3 = r1
            goto L_0x00ea
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.deser.BeanDeserializerFactory.addBeanProps(com.fasterxml.jackson.databind.DeserializationContext, com.fasterxml.jackson.databind.BeanDescription, com.fasterxml.jackson.databind.deser.BeanDeserializerBuilder):void");
    }

    /* access modifiers changed from: protected */
    public List<BeanPropertyDefinition> filterBeanProps(DeserializationContext deserializationContext, BeanDescription beanDescription, BeanDeserializerBuilder beanDeserializerBuilder, List<BeanPropertyDefinition> list, Set<String> set) throws JsonMappingException {
        ArrayList arrayList = new ArrayList(Math.max(4, list.size()));
        HashMap hashMap = new HashMap();
        for (BeanPropertyDefinition beanPropertyDefinition : list) {
            String name = beanPropertyDefinition.getName();
            if (!set.contains(name)) {
                if (!beanPropertyDefinition.hasConstructorParameter()) {
                    Class cls = null;
                    if (beanPropertyDefinition.hasSetter()) {
                        cls = beanPropertyDefinition.getSetter().getRawParameterType(0);
                    } else if (beanPropertyDefinition.hasField()) {
                        cls = beanPropertyDefinition.getField().getRawType();
                    }
                    if (cls != null && isIgnorableType(deserializationContext.getConfig(), beanDescription, cls, hashMap)) {
                        beanDeserializerBuilder.addIgnorable(name);
                    }
                }
                arrayList.add(beanPropertyDefinition);
            }
        }
        return arrayList;
    }

    /* access modifiers changed from: protected */
    public void addReferenceProperties(DeserializationContext deserializationContext, BeanDescription beanDescription, BeanDeserializerBuilder beanDeserializerBuilder) throws JsonMappingException {
        JavaType type;
        Map findBackReferenceProperties = beanDescription.findBackReferenceProperties();
        if (findBackReferenceProperties != null) {
            for (Entry entry : findBackReferenceProperties.entrySet()) {
                String str = (String) entry.getKey();
                AnnotatedMember annotatedMember = (AnnotatedMember) entry.getValue();
                if (annotatedMember instanceof AnnotatedMethod) {
                    type = ((AnnotatedMethod) annotatedMember).getParameterType(0);
                } else {
                    type = annotatedMember.getType();
                }
                beanDeserializerBuilder.addBackReferenceProperty(str, constructSettableProperty(deserializationContext, beanDescription, SimpleBeanPropertyDefinition.construct(deserializationContext.getConfig(), annotatedMember), type));
            }
        }
    }

    /* access modifiers changed from: protected */
    public void addInjectables(DeserializationContext deserializationContext, BeanDescription beanDescription, BeanDeserializerBuilder beanDeserializerBuilder) throws JsonMappingException {
        boolean z;
        Map findInjectables = beanDescription.findInjectables();
        if (findInjectables != null) {
            boolean canOverrideAccessModifiers = deserializationContext.canOverrideAccessModifiers();
            if (!canOverrideAccessModifiers || !deserializationContext.isEnabled(MapperFeature.OVERRIDE_PUBLIC_ACCESS_MODIFIERS)) {
                z = false;
            } else {
                z = true;
            }
            for (Entry entry : findInjectables.entrySet()) {
                AnnotatedMember annotatedMember = (AnnotatedMember) entry.getValue();
                if (canOverrideAccessModifiers) {
                    annotatedMember.fixAccess(z);
                }
                beanDeserializerBuilder.addInjectable(PropertyName.construct(annotatedMember.getName()), annotatedMember.getType(), beanDescription.getClassAnnotations(), annotatedMember, entry.getKey());
            }
        }
    }

    /* access modifiers changed from: protected */
    public SettableAnyProperty constructAnySetter(DeserializationContext deserializationContext, BeanDescription beanDescription, AnnotatedMethod annotatedMethod) throws JsonMappingException {
        if (deserializationContext.canOverrideAccessModifiers()) {
            annotatedMethod.fixAccess(deserializationContext.isEnabled(MapperFeature.OVERRIDE_PUBLIC_ACCESS_MODIFIERS));
        }
        JavaType parameterType = annotatedMethod.getParameterType(1);
        Std std = new Std(PropertyName.construct(annotatedMethod.getName()), parameterType, (PropertyName) null, beanDescription.getClassAnnotations(), (AnnotatedMember) annotatedMethod, PropertyMetadata.STD_OPTIONAL);
        JavaType resolveType = resolveType(deserializationContext, beanDescription, parameterType, annotatedMethod);
        JsonDeserializer findDeserializerFromAnnotation = findDeserializerFromAnnotation(deserializationContext, annotatedMethod);
        JavaType modifyTypeByAnnotation = modifyTypeByAnnotation(deserializationContext, annotatedMethod, resolveType);
        if (findDeserializerFromAnnotation == null) {
            findDeserializerFromAnnotation = (JsonDeserializer) modifyTypeByAnnotation.getValueHandler();
        }
        return new SettableAnyProperty(std, annotatedMethod, modifyTypeByAnnotation, findDeserializerFromAnnotation, (TypeDeserializer) modifyTypeByAnnotation.getTypeHandler());
    }

    /* access modifiers changed from: protected */
    public SettableBeanProperty constructSettableProperty(DeserializationContext deserializationContext, BeanDescription beanDescription, BeanPropertyDefinition beanPropertyDefinition, JavaType javaType) throws JsonMappingException {
        SettableBeanProperty fieldProperty;
        AnnotatedMember nonConstructorMutator = beanPropertyDefinition.getNonConstructorMutator();
        if (deserializationContext.canOverrideAccessModifiers()) {
            nonConstructorMutator.fixAccess(deserializationContext.isEnabled(MapperFeature.OVERRIDE_PUBLIC_ACCESS_MODIFIERS));
        }
        Std std = new Std(beanPropertyDefinition.getFullName(), javaType, beanPropertyDefinition.getWrapperName(), beanDescription.getClassAnnotations(), nonConstructorMutator, beanPropertyDefinition.getMetadata());
        JavaType resolveType = resolveType(deserializationContext, beanDescription, javaType, nonConstructorMutator);
        if (resolveType != javaType) {
            std.withType(resolveType);
        }
        JsonDeserializer findDeserializerFromAnnotation = findDeserializerFromAnnotation(deserializationContext, nonConstructorMutator);
        JavaType modifyTypeByAnnotation = modifyTypeByAnnotation(deserializationContext, nonConstructorMutator, resolveType);
        TypeDeserializer typeDeserializer = (TypeDeserializer) modifyTypeByAnnotation.getTypeHandler();
        if (nonConstructorMutator instanceof AnnotatedMethod) {
            fieldProperty = new MethodProperty(beanPropertyDefinition, modifyTypeByAnnotation, typeDeserializer, beanDescription.getClassAnnotations(), (AnnotatedMethod) nonConstructorMutator);
        } else {
            fieldProperty = new FieldProperty(beanPropertyDefinition, modifyTypeByAnnotation, typeDeserializer, beanDescription.getClassAnnotations(), (AnnotatedField) nonConstructorMutator);
        }
        if (findDeserializerFromAnnotation != null) {
            fieldProperty = fieldProperty.withValueDeserializer(findDeserializerFromAnnotation);
        }
        ReferenceProperty findReferenceType = beanPropertyDefinition.findReferenceType();
        if (findReferenceType != null && findReferenceType.isManagedReference()) {
            fieldProperty.setManagedReferenceName(findReferenceType.getName());
        }
        ObjectIdInfo findObjectIdInfo = beanPropertyDefinition.findObjectIdInfo();
        if (findObjectIdInfo != null) {
            fieldProperty.setObjectIdInfo(findObjectIdInfo);
        }
        return fieldProperty;
    }

    /* access modifiers changed from: protected */
    public SettableBeanProperty constructSetterlessProperty(DeserializationContext deserializationContext, BeanDescription beanDescription, BeanPropertyDefinition beanPropertyDefinition) throws JsonMappingException {
        AnnotatedMethod getter = beanPropertyDefinition.getGetter();
        if (deserializationContext.canOverrideAccessModifiers()) {
            getter.fixAccess(deserializationContext.isEnabled(MapperFeature.OVERRIDE_PUBLIC_ACCESS_MODIFIERS));
        }
        JavaType type = getter.getType();
        JsonDeserializer findDeserializerFromAnnotation = findDeserializerFromAnnotation(deserializationContext, getter);
        JavaType resolveType = resolveType(deserializationContext, beanDescription, modifyTypeByAnnotation(deserializationContext, getter, type), getter);
        SetterlessProperty setterlessProperty = new SetterlessProperty(beanPropertyDefinition, resolveType, (TypeDeserializer) resolveType.getTypeHandler(), beanDescription.getClassAnnotations(), getter);
        if (findDeserializerFromAnnotation != null) {
            return setterlessProperty.withValueDeserializer(findDeserializerFromAnnotation);
        }
        return setterlessProperty;
    }

    /* access modifiers changed from: protected */
    public boolean isPotentialBeanType(Class<?> cls) {
        String canBeABeanType = ClassUtil.canBeABeanType(cls);
        if (canBeABeanType != null) {
            throw new IllegalArgumentException("Can not deserialize Class " + cls.getName() + " (of type " + canBeABeanType + ") as a Bean");
        } else if (ClassUtil.isProxyType(cls)) {
            throw new IllegalArgumentException("Can not deserialize Proxy class " + cls.getName() + " as a Bean");
        } else {
            String isLocalType = ClassUtil.isLocalType(cls, true);
            if (isLocalType == null) {
                return true;
            }
            throw new IllegalArgumentException("Can not deserialize Class " + cls.getName() + " (of type " + isLocalType + ") as a Bean");
        }
    }

    /* access modifiers changed from: protected */
    public boolean isIgnorableType(DeserializationConfig deserializationConfig, BeanDescription beanDescription, Class<?> cls, Map<Class<?>, Boolean> map) {
        Boolean bool = (Boolean) map.get(cls);
        if (bool != null) {
            return bool.booleanValue();
        }
        Boolean isIgnorableType = deserializationConfig.getAnnotationIntrospector().isIgnorableType(deserializationConfig.introspectClassAnnotations(cls).getClassInfo());
        if (isIgnorableType == null) {
            return false;
        }
        return isIgnorableType.booleanValue();
    }
}
