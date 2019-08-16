package com.fasterxml.jackson.databind;

import com.fasterxml.jackson.annotation.JsonAutoDetect.Visibility;
import com.fasterxml.jackson.annotation.JsonInclude.Include;
import com.fasterxml.jackson.annotation.JsonInclude.Value;
import com.fasterxml.jackson.annotation.JsonTypeInfo.C0861As;
import com.fasterxml.jackson.annotation.JsonTypeInfo.C0862Id;
import com.fasterxml.jackson.annotation.PropertyAccessor;
import com.fasterxml.jackson.core.Base64Variant;
import com.fasterxml.jackson.core.Base64Variants;
import com.fasterxml.jackson.core.FormatSchema;
import com.fasterxml.jackson.core.JsonEncoding;
import com.fasterxml.jackson.core.JsonFactory;
import com.fasterxml.jackson.core.JsonGenerationException;
import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonParser.Feature;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.core.JsonToken;
import com.fasterxml.jackson.core.ObjectCodec;
import com.fasterxml.jackson.core.PrettyPrinter;
import com.fasterxml.jackson.core.TreeNode;
import com.fasterxml.jackson.core.Version;
import com.fasterxml.jackson.core.Versioned;
import com.fasterxml.jackson.core.p015io.CharacterEscapes;
import com.fasterxml.jackson.core.p015io.SegmentedStringWriter;
import com.fasterxml.jackson.core.type.ResolvedType;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.core.util.ByteArrayBuilder;
import com.fasterxml.jackson.core.util.DefaultPrettyPrinter;
import com.fasterxml.jackson.databind.cfg.BaseSettings;
import com.fasterxml.jackson.databind.cfg.ContextAttributes;
import com.fasterxml.jackson.databind.cfg.HandlerInstantiator;
import com.fasterxml.jackson.databind.cfg.PackageVersion;
import com.fasterxml.jackson.databind.deser.BeanDeserializerFactory;
import com.fasterxml.jackson.databind.deser.DefaultDeserializationContext;
import com.fasterxml.jackson.databind.deser.DeserializationProblemHandler;
import com.fasterxml.jackson.databind.deser.DeserializerFactory;
import com.fasterxml.jackson.databind.introspect.BasicClassIntrospector;
import com.fasterxml.jackson.databind.introspect.ClassIntrospector;
import com.fasterxml.jackson.databind.introspect.ClassIntrospector.MixInResolver;
import com.fasterxml.jackson.databind.introspect.JacksonAnnotationIntrospector;
import com.fasterxml.jackson.databind.introspect.SimpleMixInResolver;
import com.fasterxml.jackson.databind.introspect.VisibilityChecker;
import com.fasterxml.jackson.databind.introspect.VisibilityChecker.Std;
import com.fasterxml.jackson.databind.jsonFormatVisitors.JsonFormatVisitorWrapper;
import com.fasterxml.jackson.databind.jsonschema.JsonSchema;
import com.fasterxml.jackson.databind.jsontype.NamedType;
import com.fasterxml.jackson.databind.jsontype.SubtypeResolver;
import com.fasterxml.jackson.databind.jsontype.TypeDeserializer;
import com.fasterxml.jackson.databind.jsontype.TypeResolverBuilder;
import com.fasterxml.jackson.databind.jsontype.TypeSerializer;
import com.fasterxml.jackson.databind.jsontype.impl.StdSubtypeResolver;
import com.fasterxml.jackson.databind.jsontype.impl.StdTypeResolverBuilder;
import com.fasterxml.jackson.databind.node.ArrayNode;
import com.fasterxml.jackson.databind.node.JsonNodeFactory;
import com.fasterxml.jackson.databind.node.NullNode;
import com.fasterxml.jackson.databind.node.ObjectNode;
import com.fasterxml.jackson.databind.node.POJONode;
import com.fasterxml.jackson.databind.node.TreeTraversingParser;
import com.fasterxml.jackson.databind.ser.BeanSerializerFactory;
import com.fasterxml.jackson.databind.ser.DefaultSerializerProvider;
import com.fasterxml.jackson.databind.ser.DefaultSerializerProvider.Impl;
import com.fasterxml.jackson.databind.ser.FilterProvider;
import com.fasterxml.jackson.databind.ser.SerializerFactory;
import com.fasterxml.jackson.databind.type.SimpleType;
import com.fasterxml.jackson.databind.type.TypeFactory;
import com.fasterxml.jackson.databind.util.RootNameLookup;
import com.fasterxml.jackson.databind.util.StdDateFormat;
import com.fasterxml.jackson.databind.util.TokenBuffer;
import java.io.Closeable;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.Reader;
import java.io.Serializable;
import java.io.Writer;
import java.lang.reflect.Type;
import java.net.URL;
import java.text.DateFormat;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedHashSet;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.ServiceLoader;
import java.util.Set;
import java.util.TimeZone;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.atomic.AtomicReference;

public class ObjectMapper extends ObjectCodec implements Versioned, Serializable {
    protected static final AnnotationIntrospector DEFAULT_ANNOTATION_INTROSPECTOR = new JacksonAnnotationIntrospector();
    protected static final BaseSettings DEFAULT_BASE = new BaseSettings(null, DEFAULT_ANNOTATION_INTROSPECTOR, STD_VISIBILITY_CHECKER, null, TypeFactory.defaultInstance(), null, StdDateFormat.instance, null, Locale.getDefault(), null, Base64Variants.getDefaultVariant());
    private static final JavaType JSON_NODE_TYPE = SimpleType.constructUnsafe(JsonNode.class);
    protected static final VisibilityChecker<?> STD_VISIBILITY_CHECKER = Std.defaultInstance();
    @Deprecated
    protected static final PrettyPrinter _defaultPrettyPrinter = new DefaultPrettyPrinter();
    private static final long serialVersionUID = 1;
    protected DeserializationConfig _deserializationConfig;
    protected DefaultDeserializationContext _deserializationContext;
    protected InjectableValues _injectableValues;
    protected final JsonFactory _jsonFactory;
    protected SimpleMixInResolver _mixIns;
    protected Set<Object> _registeredModuleTypes;
    protected final ConcurrentHashMap<JavaType, JsonDeserializer<Object>> _rootDeserializers;
    protected SerializationConfig _serializationConfig;
    protected SerializerFactory _serializerFactory;
    protected DefaultSerializerProvider _serializerProvider;
    protected SubtypeResolver _subtypeResolver;
    protected TypeFactory _typeFactory;

    public static class DefaultTypeResolverBuilder extends StdTypeResolverBuilder implements Serializable {
        private static final long serialVersionUID = 1;
        protected final DefaultTyping _appliesFor;

        public DefaultTypeResolverBuilder(DefaultTyping defaultTyping) {
            this._appliesFor = defaultTyping;
        }

        public TypeDeserializer buildTypeDeserializer(DeserializationConfig deserializationConfig, JavaType javaType, Collection<NamedType> collection) {
            if (useForType(javaType)) {
                return super.buildTypeDeserializer(deserializationConfig, javaType, collection);
            }
            return null;
        }

        public TypeSerializer buildTypeSerializer(SerializationConfig serializationConfig, JavaType javaType, Collection<NamedType> collection) {
            if (useForType(javaType)) {
                return super.buildTypeSerializer(serializationConfig, javaType, collection);
            }
            return null;
        }

        /* JADX WARNING: Code restructure failed: missing block: B:17:0x0048, code lost:
            if (r5.isArrayType() == false) goto L_0x004f;
         */
        /* JADX WARNING: Code restructure failed: missing block: B:18:0x004a, code lost:
            r5 = r5.getContentType();
         */
        /* JADX WARNING: Code restructure failed: missing block: B:20:0x0053, code lost:
            if (r5.isReferenceType() == false) goto L_0x005a;
         */
        /* JADX WARNING: Code restructure failed: missing block: B:21:0x0055, code lost:
            r5 = r5.getReferencedType();
         */
        /* JADX WARNING: Code restructure failed: missing block: B:23:0x005e, code lost:
            if (r5.isFinal() != false) goto L_0x006e;
         */
        /* JADX WARNING: Code restructure failed: missing block: B:25:0x006a, code lost:
            if (com.fasterxml.jackson.core.TreeNode.class.isAssignableFrom(r5.getRawClass()) != false) goto L_0x006e;
         */
        /* JADX WARNING: Code restructure failed: missing block: B:27:0x006e, code lost:
            r1 = false;
         */
        /* JADX WARNING: Code restructure failed: missing block: B:37:?, code lost:
            return r1;
         */
        /* Code decompiled incorrectly, please refer to instructions dump. */
        public boolean useForType(com.fasterxml.jackson.databind.JavaType r5) {
            /*
                r4 = this;
                r1 = 1
                r0 = 0
                int[] r2 = com.fasterxml.jackson.databind.ObjectMapper.C08652.f425x3ef634e7
                com.fasterxml.jackson.databind.ObjectMapper$DefaultTyping r3 = r4._appliesFor
                int r3 = r3.ordinal()
                r2 = r2[r3]
                switch(r2) {
                    case 1: goto L_0x0014;
                    case 2: goto L_0x001f;
                    case 3: goto L_0x0044;
                    default: goto L_0x000f;
                }
            L_0x000f:
                boolean r0 = r5.isJavaLangObject()
            L_0x0013:
                return r0
            L_0x0014:
                boolean r2 = r5.isArrayType()
                if (r2 == 0) goto L_0x001f
                com.fasterxml.jackson.databind.JavaType r5 = r5.getContentType()
                goto L_0x0014
            L_0x001f:
                boolean r2 = r5.isReferenceType()
                if (r2 == 0) goto L_0x002a
                com.fasterxml.jackson.databind.JavaType r5 = r5.getReferencedType()
                goto L_0x001f
            L_0x002a:
                boolean r2 = r5.isJavaLangObject()
                if (r2 != 0) goto L_0x0042
                boolean r2 = r5.isConcrete()
                if (r2 != 0) goto L_0x0013
                java.lang.Class<com.fasterxml.jackson.core.TreeNode> r2 = com.fasterxml.jackson.core.TreeNode.class
                java.lang.Class r3 = r5.getRawClass()
                boolean r2 = r2.isAssignableFrom(r3)
                if (r2 != 0) goto L_0x0013
            L_0x0042:
                r0 = r1
                goto L_0x0013
            L_0x0044:
                boolean r2 = r5.isArrayType()
                if (r2 == 0) goto L_0x004f
                com.fasterxml.jackson.databind.JavaType r5 = r5.getContentType()
                goto L_0x0044
            L_0x004f:
                boolean r2 = r5.isReferenceType()
                if (r2 == 0) goto L_0x005a
                com.fasterxml.jackson.databind.JavaType r5 = r5.getReferencedType()
                goto L_0x004f
            L_0x005a:
                boolean r2 = r5.isFinal()
                if (r2 != 0) goto L_0x006e
                java.lang.Class<com.fasterxml.jackson.core.TreeNode> r2 = com.fasterxml.jackson.core.TreeNode.class
                java.lang.Class r3 = r5.getRawClass()
                boolean r2 = r2.isAssignableFrom(r3)
                if (r2 != 0) goto L_0x006e
            L_0x006c:
                r0 = r1
                goto L_0x0013
            L_0x006e:
                r1 = r0
                goto L_0x006c
            */
            throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.ObjectMapper.DefaultTypeResolverBuilder.useForType(com.fasterxml.jackson.databind.JavaType):boolean");
        }
    }

    public enum DefaultTyping {
        JAVA_LANG_OBJECT,
        OBJECT_AND_NON_CONCRETE,
        NON_CONCRETE_AND_ARRAYS,
        NON_FINAL
    }

    public ObjectMapper() {
        this(null, null, null);
    }

    public ObjectMapper(JsonFactory jsonFactory) {
        this(jsonFactory, null, null);
    }

    protected ObjectMapper(ObjectMapper objectMapper) {
        this._rootDeserializers = new ConcurrentHashMap<>(64, 0.6f, 2);
        this._jsonFactory = objectMapper._jsonFactory.copy();
        this._jsonFactory.setCodec(this);
        this._subtypeResolver = objectMapper._subtypeResolver;
        this._typeFactory = objectMapper._typeFactory;
        this._injectableValues = objectMapper._injectableValues;
        SimpleMixInResolver copy = objectMapper._mixIns.copy();
        this._mixIns = copy;
        RootNameLookup rootNameLookup = new RootNameLookup();
        this._serializationConfig = new SerializationConfig(objectMapper._serializationConfig, copy, rootNameLookup);
        this._deserializationConfig = new DeserializationConfig(objectMapper._deserializationConfig, copy, rootNameLookup);
        this._serializerProvider = objectMapper._serializerProvider.copy();
        this._deserializationContext = objectMapper._deserializationContext.copy();
        this._serializerFactory = objectMapper._serializerFactory;
        Set<Object> set = objectMapper._registeredModuleTypes;
        if (set == null) {
            this._registeredModuleTypes = null;
        } else {
            this._registeredModuleTypes = new LinkedHashSet(set);
        }
    }

    public ObjectMapper(JsonFactory jsonFactory, DefaultSerializerProvider defaultSerializerProvider, DefaultDeserializationContext defaultDeserializationContext) {
        this._rootDeserializers = new ConcurrentHashMap<>(64, 0.6f, 2);
        if (jsonFactory == null) {
            this._jsonFactory = new MappingJsonFactory(this);
        } else {
            this._jsonFactory = jsonFactory;
            if (jsonFactory.getCodec() == null) {
                this._jsonFactory.setCodec(this);
            }
        }
        this._subtypeResolver = new StdSubtypeResolver();
        RootNameLookup rootNameLookup = new RootNameLookup();
        this._typeFactory = TypeFactory.defaultInstance();
        SimpleMixInResolver simpleMixInResolver = new SimpleMixInResolver(null);
        this._mixIns = simpleMixInResolver;
        BaseSettings withClassIntrospector = DEFAULT_BASE.withClassIntrospector(defaultClassIntrospector());
        this._serializationConfig = new SerializationConfig(withClassIntrospector, this._subtypeResolver, simpleMixInResolver, rootNameLookup);
        this._deserializationConfig = new DeserializationConfig(withClassIntrospector, this._subtypeResolver, simpleMixInResolver, rootNameLookup);
        boolean requiresPropertyOrdering = this._jsonFactory.requiresPropertyOrdering();
        if (this._serializationConfig.isEnabled(MapperFeature.SORT_PROPERTIES_ALPHABETICALLY) ^ requiresPropertyOrdering) {
            configure(MapperFeature.SORT_PROPERTIES_ALPHABETICALLY, requiresPropertyOrdering);
        }
        if (defaultSerializerProvider == null) {
            defaultSerializerProvider = new Impl();
        }
        this._serializerProvider = defaultSerializerProvider;
        if (defaultDeserializationContext == null) {
            defaultDeserializationContext = new DefaultDeserializationContext.Impl((DeserializerFactory) BeanDeserializerFactory.instance);
        }
        this._deserializationContext = defaultDeserializationContext;
        this._serializerFactory = BeanSerializerFactory.instance;
    }

    /* access modifiers changed from: protected */
    public ClassIntrospector defaultClassIntrospector() {
        return new BasicClassIntrospector();
    }

    public ObjectMapper copy() {
        _checkInvalidCopy(ObjectMapper.class);
        return new ObjectMapper(this);
    }

    /* access modifiers changed from: protected */
    public void _checkInvalidCopy(Class<?> cls) {
        if (getClass() != cls) {
            throw new IllegalStateException("Failed copy(): " + getClass().getName() + " (version: " + version() + ") does not override copy(); it has to");
        }
    }

    /* access modifiers changed from: protected */
    public ObjectReader _newReader(DeserializationConfig deserializationConfig) {
        return new ObjectReader(this, deserializationConfig);
    }

    /* access modifiers changed from: protected */
    public ObjectReader _newReader(DeserializationConfig deserializationConfig, JavaType javaType, Object obj, FormatSchema formatSchema, InjectableValues injectableValues) {
        return new ObjectReader(this, deserializationConfig, javaType, obj, formatSchema, injectableValues);
    }

    /* access modifiers changed from: protected */
    public ObjectWriter _newWriter(SerializationConfig serializationConfig) {
        return new ObjectWriter(this, serializationConfig);
    }

    /* access modifiers changed from: protected */
    public ObjectWriter _newWriter(SerializationConfig serializationConfig, FormatSchema formatSchema) {
        return new ObjectWriter(this, serializationConfig, formatSchema);
    }

    /* access modifiers changed from: protected */
    public ObjectWriter _newWriter(SerializationConfig serializationConfig, JavaType javaType, PrettyPrinter prettyPrinter) {
        return new ObjectWriter(this, serializationConfig, javaType, prettyPrinter);
    }

    public Version version() {
        return PackageVersion.VERSION;
    }

    /* JADX WARNING: Code restructure failed: missing block: B:8:0x001f, code lost:
        if (r2._registeredModuleTypes.add(r0) == false) goto L_0x0021;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.fasterxml.jackson.databind.ObjectMapper registerModule(com.fasterxml.jackson.databind.Module r3) {
        /*
            r2 = this;
            com.fasterxml.jackson.databind.MapperFeature r0 = com.fasterxml.jackson.databind.MapperFeature.IGNORE_DUPLICATE_MODULE_REGISTRATIONS
            boolean r0 = r2.isEnabled(r0)
            if (r0 == 0) goto L_0x0022
            java.lang.Object r0 = r3.getTypeId()
            if (r0 == 0) goto L_0x0022
            java.util.Set<java.lang.Object> r1 = r2._registeredModuleTypes
            if (r1 != 0) goto L_0x0019
            java.util.LinkedHashSet r1 = new java.util.LinkedHashSet
            r1.<init>()
            r2._registeredModuleTypes = r1
        L_0x0019:
            java.util.Set<java.lang.Object> r1 = r2._registeredModuleTypes
            boolean r0 = r1.add(r0)
            if (r0 != 0) goto L_0x0022
        L_0x0021:
            return r2
        L_0x0022:
            java.lang.String r0 = r3.getModuleName()
            if (r0 != 0) goto L_0x0030
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.String r1 = "Module without defined name"
            r0.<init>(r1)
            throw r0
        L_0x0030:
            com.fasterxml.jackson.core.Version r0 = r3.version()
            if (r0 != 0) goto L_0x003e
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.String r1 = "Module without defined version"
            r0.<init>(r1)
            throw r0
        L_0x003e:
            com.fasterxml.jackson.databind.ObjectMapper$1 r0 = new com.fasterxml.jackson.databind.ObjectMapper$1
            r0.<init>(r2)
            r3.setupModule(r0)
            goto L_0x0021
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.ObjectMapper.registerModule(com.fasterxml.jackson.databind.Module):com.fasterxml.jackson.databind.ObjectMapper");
    }

    public ObjectMapper registerModules(Module... moduleArr) {
        for (Module registerModule : moduleArr) {
            registerModule(registerModule);
        }
        return this;
    }

    public ObjectMapper registerModules(Iterable<Module> iterable) {
        for (Module registerModule : iterable) {
            registerModule(registerModule);
        }
        return this;
    }

    public static List<Module> findModules() {
        return findModules(null);
    }

    public static List<Module> findModules(ClassLoader classLoader) {
        ArrayList arrayList = new ArrayList();
        Iterator it = (classLoader == null ? ServiceLoader.load(Module.class) : ServiceLoader.load(Module.class, classLoader)).iterator();
        while (it.hasNext()) {
            arrayList.add((Module) it.next());
        }
        return arrayList;
    }

    public ObjectMapper findAndRegisterModules() {
        return registerModules((Iterable<Module>) findModules());
    }

    public SerializationConfig getSerializationConfig() {
        return this._serializationConfig;
    }

    public DeserializationConfig getDeserializationConfig() {
        return this._deserializationConfig;
    }

    public DeserializationContext getDeserializationContext() {
        return this._deserializationContext;
    }

    public ObjectMapper setSerializerFactory(SerializerFactory serializerFactory) {
        this._serializerFactory = serializerFactory;
        return this;
    }

    public SerializerFactory getSerializerFactory() {
        return this._serializerFactory;
    }

    public ObjectMapper setSerializerProvider(DefaultSerializerProvider defaultSerializerProvider) {
        this._serializerProvider = defaultSerializerProvider;
        return this;
    }

    public SerializerProvider getSerializerProvider() {
        return this._serializerProvider;
    }

    public SerializerProvider getSerializerProviderInstance() {
        return _serializerProvider(this._serializationConfig);
    }

    public ObjectMapper setMixIns(Map<Class<?>, Class<?>> map) {
        this._mixIns.setLocalDefinitions(map);
        return this;
    }

    public ObjectMapper addMixIn(Class<?> cls, Class<?> cls2) {
        this._mixIns.addLocalDefinition(cls, cls2);
        return this;
    }

    public ObjectMapper setMixInResolver(MixInResolver mixInResolver) {
        SimpleMixInResolver withOverrides = this._mixIns.withOverrides(mixInResolver);
        if (withOverrides != this._mixIns) {
            this._mixIns = withOverrides;
            this._deserializationConfig = new DeserializationConfig(this._deserializationConfig, withOverrides);
            this._serializationConfig = new SerializationConfig(this._serializationConfig, withOverrides);
        }
        return this;
    }

    public Class<?> findMixInClassFor(Class<?> cls) {
        return this._mixIns.findMixInClassFor(cls);
    }

    public int mixInCount() {
        return this._mixIns.localSize();
    }

    @Deprecated
    public void setMixInAnnotations(Map<Class<?>, Class<?>> map) {
        setMixIns(map);
    }

    @Deprecated
    public final void addMixInAnnotations(Class<?> cls, Class<?> cls2) {
        addMixIn(cls, cls2);
    }

    public VisibilityChecker<?> getVisibilityChecker() {
        return this._serializationConfig.getDefaultVisibilityChecker();
    }

    @Deprecated
    public void setVisibilityChecker(VisibilityChecker<?> visibilityChecker) {
        setVisibility(visibilityChecker);
    }

    public ObjectMapper setVisibility(VisibilityChecker<?> visibilityChecker) {
        this._deserializationConfig = this._deserializationConfig.with(visibilityChecker);
        this._serializationConfig = this._serializationConfig.with(visibilityChecker);
        return this;
    }

    public ObjectMapper setVisibility(PropertyAccessor propertyAccessor, Visibility visibility) {
        this._deserializationConfig = this._deserializationConfig.withVisibility(propertyAccessor, visibility);
        this._serializationConfig = this._serializationConfig.withVisibility(propertyAccessor, visibility);
        return this;
    }

    public SubtypeResolver getSubtypeResolver() {
        return this._subtypeResolver;
    }

    public ObjectMapper setSubtypeResolver(SubtypeResolver subtypeResolver) {
        this._subtypeResolver = subtypeResolver;
        this._deserializationConfig = this._deserializationConfig.with(subtypeResolver);
        this._serializationConfig = this._serializationConfig.with(subtypeResolver);
        return this;
    }

    public ObjectMapper setAnnotationIntrospector(AnnotationIntrospector annotationIntrospector) {
        this._serializationConfig = this._serializationConfig.with(annotationIntrospector);
        this._deserializationConfig = this._deserializationConfig.with(annotationIntrospector);
        return this;
    }

    public ObjectMapper setAnnotationIntrospectors(AnnotationIntrospector annotationIntrospector, AnnotationIntrospector annotationIntrospector2) {
        this._serializationConfig = this._serializationConfig.with(annotationIntrospector);
        this._deserializationConfig = this._deserializationConfig.with(annotationIntrospector2);
        return this;
    }

    public ObjectMapper setPropertyNamingStrategy(PropertyNamingStrategy propertyNamingStrategy) {
        this._serializationConfig = this._serializationConfig.with(propertyNamingStrategy);
        this._deserializationConfig = this._deserializationConfig.with(propertyNamingStrategy);
        return this;
    }

    public PropertyNamingStrategy getPropertyNamingStrategy() {
        return this._serializationConfig.getPropertyNamingStrategy();
    }

    public ObjectMapper setSerializationInclusion(Include include) {
        setPropertyInclusion(Value.construct(include, Include.USE_DEFAULTS));
        return this;
    }

    public ObjectMapper setPropertyInclusion(Value value) {
        this._serializationConfig = this._serializationConfig.withPropertyInclusion(value);
        return this;
    }

    public ObjectMapper setDefaultPrettyPrinter(PrettyPrinter prettyPrinter) {
        this._serializationConfig = this._serializationConfig.withDefaultPrettyPrinter(prettyPrinter);
        return this;
    }

    public ObjectMapper enableDefaultTyping() {
        return enableDefaultTyping(DefaultTyping.OBJECT_AND_NON_CONCRETE);
    }

    public ObjectMapper enableDefaultTyping(DefaultTyping defaultTyping) {
        return enableDefaultTyping(defaultTyping, C0861As.WRAPPER_ARRAY);
    }

    public ObjectMapper enableDefaultTyping(DefaultTyping defaultTyping, C0861As as) {
        if (as != C0861As.EXTERNAL_PROPERTY) {
            return setDefaultTyping(new DefaultTypeResolverBuilder(defaultTyping).init(C0862Id.CLASS, null).inclusion(as));
        }
        throw new IllegalArgumentException("Can not use includeAs of " + as);
    }

    public ObjectMapper enableDefaultTypingAsProperty(DefaultTyping defaultTyping, String str) {
        return setDefaultTyping(new DefaultTypeResolverBuilder(defaultTyping).init(C0862Id.CLASS, null).inclusion(C0861As.PROPERTY).typeProperty(str));
    }

    public ObjectMapper disableDefaultTyping() {
        return setDefaultTyping(null);
    }

    public ObjectMapper setDefaultTyping(TypeResolverBuilder<?> typeResolverBuilder) {
        this._deserializationConfig = this._deserializationConfig.with(typeResolverBuilder);
        this._serializationConfig = this._serializationConfig.with(typeResolverBuilder);
        return this;
    }

    public void registerSubtypes(Class<?>... clsArr) {
        getSubtypeResolver().registerSubtypes(clsArr);
    }

    public void registerSubtypes(NamedType... namedTypeArr) {
        getSubtypeResolver().registerSubtypes(namedTypeArr);
    }

    public TypeFactory getTypeFactory() {
        return this._typeFactory;
    }

    public ObjectMapper setTypeFactory(TypeFactory typeFactory) {
        this._typeFactory = typeFactory;
        this._deserializationConfig = this._deserializationConfig.with(typeFactory);
        this._serializationConfig = this._serializationConfig.with(typeFactory);
        return this;
    }

    public JavaType constructType(Type type) {
        return this._typeFactory.constructType(type);
    }

    public JsonNodeFactory getNodeFactory() {
        return this._deserializationConfig.getNodeFactory();
    }

    public ObjectMapper setNodeFactory(JsonNodeFactory jsonNodeFactory) {
        this._deserializationConfig = this._deserializationConfig.with(jsonNodeFactory);
        return this;
    }

    public ObjectMapper addHandler(DeserializationProblemHandler deserializationProblemHandler) {
        this._deserializationConfig = this._deserializationConfig.withHandler(deserializationProblemHandler);
        return this;
    }

    public ObjectMapper clearProblemHandlers() {
        this._deserializationConfig = this._deserializationConfig.withNoProblemHandlers();
        return this;
    }

    public ObjectMapper setConfig(DeserializationConfig deserializationConfig) {
        this._deserializationConfig = deserializationConfig;
        return this;
    }

    @Deprecated
    public void setFilters(FilterProvider filterProvider) {
        this._serializationConfig = this._serializationConfig.withFilters(filterProvider);
    }

    public ObjectMapper setFilterProvider(FilterProvider filterProvider) {
        this._serializationConfig = this._serializationConfig.withFilters(filterProvider);
        return this;
    }

    public ObjectMapper setBase64Variant(Base64Variant base64Variant) {
        this._serializationConfig = this._serializationConfig.with(base64Variant);
        this._deserializationConfig = this._deserializationConfig.with(base64Variant);
        return this;
    }

    public ObjectMapper setConfig(SerializationConfig serializationConfig) {
        this._serializationConfig = serializationConfig;
        return this;
    }

    public JsonFactory getFactory() {
        return this._jsonFactory;
    }

    @Deprecated
    public JsonFactory getJsonFactory() {
        return getFactory();
    }

    public ObjectMapper setDateFormat(DateFormat dateFormat) {
        this._deserializationConfig = this._deserializationConfig.with(dateFormat);
        this._serializationConfig = this._serializationConfig.with(dateFormat);
        return this;
    }

    public DateFormat getDateFormat() {
        return this._serializationConfig.getDateFormat();
    }

    public Object setHandlerInstantiator(HandlerInstantiator handlerInstantiator) {
        this._deserializationConfig = this._deserializationConfig.with(handlerInstantiator);
        this._serializationConfig = this._serializationConfig.with(handlerInstantiator);
        return this;
    }

    public ObjectMapper setInjectableValues(InjectableValues injectableValues) {
        this._injectableValues = injectableValues;
        return this;
    }

    public InjectableValues getInjectableValues() {
        return this._injectableValues;
    }

    public ObjectMapper setLocale(Locale locale) {
        this._deserializationConfig = this._deserializationConfig.with(locale);
        this._serializationConfig = this._serializationConfig.with(locale);
        return this;
    }

    public ObjectMapper setTimeZone(TimeZone timeZone) {
        this._deserializationConfig = this._deserializationConfig.with(timeZone);
        this._serializationConfig = this._serializationConfig.with(timeZone);
        return this;
    }

    public boolean isEnabled(MapperFeature mapperFeature) {
        return this._serializationConfig.isEnabled(mapperFeature);
    }

    public ObjectMapper configure(MapperFeature mapperFeature, boolean z) {
        SerializationConfig without;
        DeserializationConfig without2;
        if (z) {
            without = this._serializationConfig.with(mapperFeature);
        } else {
            without = this._serializationConfig.without(mapperFeature);
        }
        this._serializationConfig = without;
        if (z) {
            without2 = this._deserializationConfig.with(mapperFeature);
        } else {
            without2 = this._deserializationConfig.without(mapperFeature);
        }
        this._deserializationConfig = without2;
        return this;
    }

    public ObjectMapper enable(MapperFeature... mapperFeatureArr) {
        this._deserializationConfig = this._deserializationConfig.with(mapperFeatureArr);
        this._serializationConfig = this._serializationConfig.with(mapperFeatureArr);
        return this;
    }

    public ObjectMapper disable(MapperFeature... mapperFeatureArr) {
        this._deserializationConfig = this._deserializationConfig.without(mapperFeatureArr);
        this._serializationConfig = this._serializationConfig.without(mapperFeatureArr);
        return this;
    }

    public boolean isEnabled(SerializationFeature serializationFeature) {
        return this._serializationConfig.isEnabled(serializationFeature);
    }

    public ObjectMapper configure(SerializationFeature serializationFeature, boolean z) {
        this._serializationConfig = z ? this._serializationConfig.with(serializationFeature) : this._serializationConfig.without(serializationFeature);
        return this;
    }

    public ObjectMapper enable(SerializationFeature serializationFeature) {
        this._serializationConfig = this._serializationConfig.with(serializationFeature);
        return this;
    }

    public ObjectMapper enable(SerializationFeature serializationFeature, SerializationFeature... serializationFeatureArr) {
        this._serializationConfig = this._serializationConfig.with(serializationFeature, serializationFeatureArr);
        return this;
    }

    public ObjectMapper disable(SerializationFeature serializationFeature) {
        this._serializationConfig = this._serializationConfig.without(serializationFeature);
        return this;
    }

    public ObjectMapper disable(SerializationFeature serializationFeature, SerializationFeature... serializationFeatureArr) {
        this._serializationConfig = this._serializationConfig.without(serializationFeature, serializationFeatureArr);
        return this;
    }

    public boolean isEnabled(DeserializationFeature deserializationFeature) {
        return this._deserializationConfig.isEnabled(deserializationFeature);
    }

    public ObjectMapper configure(DeserializationFeature deserializationFeature, boolean z) {
        this._deserializationConfig = z ? this._deserializationConfig.with(deserializationFeature) : this._deserializationConfig.without(deserializationFeature);
        return this;
    }

    public ObjectMapper enable(DeserializationFeature deserializationFeature) {
        this._deserializationConfig = this._deserializationConfig.with(deserializationFeature);
        return this;
    }

    public ObjectMapper enable(DeserializationFeature deserializationFeature, DeserializationFeature... deserializationFeatureArr) {
        this._deserializationConfig = this._deserializationConfig.with(deserializationFeature, deserializationFeatureArr);
        return this;
    }

    public ObjectMapper disable(DeserializationFeature deserializationFeature) {
        this._deserializationConfig = this._deserializationConfig.without(deserializationFeature);
        return this;
    }

    public ObjectMapper disable(DeserializationFeature deserializationFeature, DeserializationFeature... deserializationFeatureArr) {
        this._deserializationConfig = this._deserializationConfig.without(deserializationFeature, deserializationFeatureArr);
        return this;
    }

    public boolean isEnabled(Feature feature) {
        return this._deserializationConfig.isEnabled(feature, this._jsonFactory);
    }

    public ObjectMapper configure(Feature feature, boolean z) {
        this._jsonFactory.configure(feature, z);
        return this;
    }

    public ObjectMapper enable(Feature... featureArr) {
        for (Feature enable : featureArr) {
            this._jsonFactory.enable(enable);
        }
        return this;
    }

    public ObjectMapper disable(Feature... featureArr) {
        for (Feature disable : featureArr) {
            this._jsonFactory.disable(disable);
        }
        return this;
    }

    public boolean isEnabled(JsonGenerator.Feature feature) {
        return this._serializationConfig.isEnabled(feature, this._jsonFactory);
    }

    public ObjectMapper configure(JsonGenerator.Feature feature, boolean z) {
        this._jsonFactory.configure(feature, z);
        return this;
    }

    public ObjectMapper enable(JsonGenerator.Feature... featureArr) {
        for (JsonGenerator.Feature enable : featureArr) {
            this._jsonFactory.enable(enable);
        }
        return this;
    }

    public ObjectMapper disable(JsonGenerator.Feature... featureArr) {
        for (JsonGenerator.Feature disable : featureArr) {
            this._jsonFactory.disable(disable);
        }
        return this;
    }

    public boolean isEnabled(JsonFactory.Feature feature) {
        return this._jsonFactory.isEnabled(feature);
    }

    public <T> T readValue(JsonParser jsonParser, Class<T> cls) throws IOException, JsonParseException, JsonMappingException {
        return _readValue(getDeserializationConfig(), jsonParser, this._typeFactory.constructType((Type) cls));
    }

    public <T> T readValue(JsonParser jsonParser, TypeReference<?> typeReference) throws IOException, JsonParseException, JsonMappingException {
        return _readValue(getDeserializationConfig(), jsonParser, this._typeFactory.constructType(typeReference));
    }

    public final <T> T readValue(JsonParser jsonParser, ResolvedType resolvedType) throws IOException, JsonParseException, JsonMappingException {
        return _readValue(getDeserializationConfig(), jsonParser, (JavaType) resolvedType);
    }

    public <T> T readValue(JsonParser jsonParser, JavaType javaType) throws IOException, JsonParseException, JsonMappingException {
        return _readValue(getDeserializationConfig(), jsonParser, javaType);
    }

    public <T extends TreeNode> T readTree(JsonParser jsonParser) throws IOException, JsonProcessingException {
        DeserializationConfig deserializationConfig = getDeserializationConfig();
        if (jsonParser.getCurrentToken() == null && jsonParser.nextToken() == null) {
            return null;
        }
        T t = (JsonNode) _readValue(deserializationConfig, jsonParser, JSON_NODE_TYPE);
        if (t == null) {
            return getNodeFactory().nullNode();
        }
        return t;
    }

    public <T> MappingIterator<T> readValues(JsonParser jsonParser, ResolvedType resolvedType) throws IOException, JsonProcessingException {
        return readValues(jsonParser, (JavaType) resolvedType);
    }

    public <T> MappingIterator<T> readValues(JsonParser jsonParser, JavaType javaType) throws IOException, JsonProcessingException {
        DefaultDeserializationContext createDeserializationContext = createDeserializationContext(jsonParser, getDeserializationConfig());
        return new MappingIterator<>(javaType, jsonParser, createDeserializationContext, _findRootDeserializer(createDeserializationContext, javaType), false, null);
    }

    public <T> MappingIterator<T> readValues(JsonParser jsonParser, Class<T> cls) throws IOException, JsonProcessingException {
        return readValues(jsonParser, this._typeFactory.constructType((Type) cls));
    }

    public <T> MappingIterator<T> readValues(JsonParser jsonParser, TypeReference<?> typeReference) throws IOException, JsonProcessingException {
        return readValues(jsonParser, this._typeFactory.constructType(typeReference));
    }

    public JsonNode readTree(InputStream inputStream) throws IOException, JsonProcessingException {
        JsonNode jsonNode = (JsonNode) _readMapAndClose(this._jsonFactory.createParser(inputStream), JSON_NODE_TYPE);
        return jsonNode == null ? NullNode.instance : jsonNode;
    }

    public JsonNode readTree(Reader reader) throws IOException, JsonProcessingException {
        JsonNode jsonNode = (JsonNode) _readMapAndClose(this._jsonFactory.createParser(reader), JSON_NODE_TYPE);
        return jsonNode == null ? NullNode.instance : jsonNode;
    }

    public JsonNode readTree(String str) throws IOException, JsonProcessingException {
        JsonNode jsonNode = (JsonNode) _readMapAndClose(this._jsonFactory.createParser(str), JSON_NODE_TYPE);
        return jsonNode == null ? NullNode.instance : jsonNode;
    }

    public JsonNode readTree(byte[] bArr) throws IOException, JsonProcessingException {
        JsonNode jsonNode = (JsonNode) _readMapAndClose(this._jsonFactory.createParser(bArr), JSON_NODE_TYPE);
        return jsonNode == null ? NullNode.instance : jsonNode;
    }

    public JsonNode readTree(File file) throws IOException, JsonProcessingException {
        JsonNode jsonNode = (JsonNode) _readMapAndClose(this._jsonFactory.createParser(file), JSON_NODE_TYPE);
        return jsonNode == null ? NullNode.instance : jsonNode;
    }

    public JsonNode readTree(URL url) throws IOException, JsonProcessingException {
        JsonNode jsonNode = (JsonNode) _readMapAndClose(this._jsonFactory.createParser(url), JSON_NODE_TYPE);
        return jsonNode == null ? NullNode.instance : jsonNode;
    }

    public void writeValue(JsonGenerator jsonGenerator, Object obj) throws IOException, JsonGenerationException, JsonMappingException {
        SerializationConfig serializationConfig = getSerializationConfig();
        if (serializationConfig.isEnabled(SerializationFeature.INDENT_OUTPUT) && jsonGenerator.getPrettyPrinter() == null) {
            jsonGenerator.setPrettyPrinter(serializationConfig.constructDefaultPrettyPrinter());
        }
        if (!serializationConfig.isEnabled(SerializationFeature.CLOSE_CLOSEABLE) || !(obj instanceof Closeable)) {
            _serializerProvider(serializationConfig).serializeValue(jsonGenerator, obj);
            if (serializationConfig.isEnabled(SerializationFeature.FLUSH_AFTER_WRITE_VALUE)) {
                jsonGenerator.flush();
                return;
            }
            return;
        }
        _writeCloseableValue(jsonGenerator, obj, serializationConfig);
    }

    public void writeTree(JsonGenerator jsonGenerator, TreeNode treeNode) throws IOException, JsonProcessingException {
        SerializationConfig serializationConfig = getSerializationConfig();
        _serializerProvider(serializationConfig).serializeValue(jsonGenerator, treeNode);
        if (serializationConfig.isEnabled(SerializationFeature.FLUSH_AFTER_WRITE_VALUE)) {
            jsonGenerator.flush();
        }
    }

    public void writeTree(JsonGenerator jsonGenerator, JsonNode jsonNode) throws IOException, JsonProcessingException {
        SerializationConfig serializationConfig = getSerializationConfig();
        _serializerProvider(serializationConfig).serializeValue(jsonGenerator, jsonNode);
        if (serializationConfig.isEnabled(SerializationFeature.FLUSH_AFTER_WRITE_VALUE)) {
            jsonGenerator.flush();
        }
    }

    public ObjectNode createObjectNode() {
        return this._deserializationConfig.getNodeFactory().objectNode();
    }

    public ArrayNode createArrayNode() {
        return this._deserializationConfig.getNodeFactory().arrayNode();
    }

    public JsonParser treeAsTokens(TreeNode treeNode) {
        return new TreeTraversingParser((JsonNode) treeNode, this);
    }

    public <T> T treeToValue(TreeNode treeNode, Class<T> cls) throws JsonProcessingException {
        if (cls != Object.class) {
            try {
                if (cls.isAssignableFrom(treeNode.getClass())) {
                    return treeNode;
                }
            } catch (JsonProcessingException e) {
                throw e;
            } catch (IOException e2) {
                throw new IllegalArgumentException(e2.getMessage(), e2);
            }
        }
        if (treeNode.asToken() == JsonToken.VALUE_EMBEDDED_OBJECT && (treeNode instanceof POJONode)) {
            Object pojo = ((POJONode) treeNode).getPojo();
            if (pojo == null || cls.isInstance(pojo)) {
                return pojo;
            }
        }
        return readValue(treeAsTokens(treeNode), cls);
    }

    public <T extends JsonNode> T valueToTree(Object obj) throws IllegalArgumentException {
        if (obj == null) {
            return null;
        }
        TokenBuffer tokenBuffer = new TokenBuffer((ObjectCodec) this, false);
        if (isEnabled(DeserializationFeature.USE_BIG_DECIMAL_FOR_FLOATS)) {
            tokenBuffer = tokenBuffer.forceUseOfBigDecimal(true);
        }
        try {
            writeValue((JsonGenerator) tokenBuffer, obj);
            JsonParser asParser = tokenBuffer.asParser();
            T t = (JsonNode) readTree(asParser);
            asParser.close();
            return t;
        } catch (IOException e) {
            throw new IllegalArgumentException(e.getMessage(), e);
        }
    }

    public boolean canSerialize(Class<?> cls) {
        return _serializerProvider(getSerializationConfig()).hasSerializerFor(cls, null);
    }

    public boolean canSerialize(Class<?> cls, AtomicReference<Throwable> atomicReference) {
        return _serializerProvider(getSerializationConfig()).hasSerializerFor(cls, atomicReference);
    }

    public boolean canDeserialize(JavaType javaType) {
        return createDeserializationContext(null, getDeserializationConfig()).hasValueDeserializerFor(javaType, null);
    }

    public boolean canDeserialize(JavaType javaType, AtomicReference<Throwable> atomicReference) {
        return createDeserializationContext(null, getDeserializationConfig()).hasValueDeserializerFor(javaType, atomicReference);
    }

    public <T> T readValue(File file, Class<T> cls) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(file), this._typeFactory.constructType((Type) cls));
    }

    public <T> T readValue(File file, TypeReference typeReference) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(file), this._typeFactory.constructType(typeReference));
    }

    public <T> T readValue(File file, JavaType javaType) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(file), javaType);
    }

    public <T> T readValue(URL url, Class<T> cls) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(url), this._typeFactory.constructType((Type) cls));
    }

    public <T> T readValue(URL url, TypeReference typeReference) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(url), this._typeFactory.constructType(typeReference));
    }

    public <T> T readValue(URL url, JavaType javaType) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(url), javaType);
    }

    public <T> T readValue(String str, Class<T> cls) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(str), this._typeFactory.constructType((Type) cls));
    }

    public <T> T readValue(String str, TypeReference typeReference) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(str), this._typeFactory.constructType(typeReference));
    }

    public <T> T readValue(String str, JavaType javaType) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(str), javaType);
    }

    public <T> T readValue(Reader reader, Class<T> cls) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(reader), this._typeFactory.constructType((Type) cls));
    }

    public <T> T readValue(Reader reader, TypeReference typeReference) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(reader), this._typeFactory.constructType(typeReference));
    }

    public <T> T readValue(Reader reader, JavaType javaType) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(reader), javaType);
    }

    public <T> T readValue(InputStream inputStream, Class<T> cls) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(inputStream), this._typeFactory.constructType((Type) cls));
    }

    public <T> T readValue(InputStream inputStream, TypeReference typeReference) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(inputStream), this._typeFactory.constructType(typeReference));
    }

    public <T> T readValue(InputStream inputStream, JavaType javaType) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(inputStream), javaType);
    }

    public <T> T readValue(byte[] bArr, Class<T> cls) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(bArr), this._typeFactory.constructType((Type) cls));
    }

    public <T> T readValue(byte[] bArr, int i, int i2, Class<T> cls) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(bArr, i, i2), this._typeFactory.constructType((Type) cls));
    }

    public <T> T readValue(byte[] bArr, TypeReference typeReference) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(bArr), this._typeFactory.constructType(typeReference));
    }

    public <T> T readValue(byte[] bArr, int i, int i2, TypeReference typeReference) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(bArr, i, i2), this._typeFactory.constructType(typeReference));
    }

    public <T> T readValue(byte[] bArr, JavaType javaType) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(bArr), javaType);
    }

    public <T> T readValue(byte[] bArr, int i, int i2, JavaType javaType) throws IOException, JsonParseException, JsonMappingException {
        return _readMapAndClose(this._jsonFactory.createParser(bArr, i, i2), javaType);
    }

    public void writeValue(File file, Object obj) throws IOException, JsonGenerationException, JsonMappingException {
        _configAndWriteValue(this._jsonFactory.createGenerator(file, JsonEncoding.UTF8), obj);
    }

    public void writeValue(OutputStream outputStream, Object obj) throws IOException, JsonGenerationException, JsonMappingException {
        _configAndWriteValue(this._jsonFactory.createGenerator(outputStream, JsonEncoding.UTF8), obj);
    }

    public void writeValue(Writer writer, Object obj) throws IOException, JsonGenerationException, JsonMappingException {
        _configAndWriteValue(this._jsonFactory.createGenerator(writer), obj);
    }

    public String writeValueAsString(Object obj) throws JsonProcessingException {
        SegmentedStringWriter segmentedStringWriter = new SegmentedStringWriter(this._jsonFactory._getBufferRecycler());
        try {
            _configAndWriteValue(this._jsonFactory.createGenerator((Writer) segmentedStringWriter), obj);
            return segmentedStringWriter.getAndClear();
        } catch (JsonProcessingException e) {
            throw e;
        } catch (IOException e2) {
            throw JsonMappingException.fromUnexpectedIOE(e2);
        }
    }

    public byte[] writeValueAsBytes(Object obj) throws JsonProcessingException {
        ByteArrayBuilder byteArrayBuilder = new ByteArrayBuilder(this._jsonFactory._getBufferRecycler());
        try {
            _configAndWriteValue(this._jsonFactory.createGenerator((OutputStream) byteArrayBuilder, JsonEncoding.UTF8), obj);
            byte[] byteArray = byteArrayBuilder.toByteArray();
            byteArrayBuilder.release();
            return byteArray;
        } catch (JsonProcessingException e) {
            throw e;
        } catch (IOException e2) {
            throw JsonMappingException.fromUnexpectedIOE(e2);
        }
    }

    public ObjectWriter writer() {
        return _newWriter(getSerializationConfig());
    }

    public ObjectWriter writer(SerializationFeature serializationFeature) {
        return _newWriter(getSerializationConfig().with(serializationFeature));
    }

    public ObjectWriter writer(SerializationFeature serializationFeature, SerializationFeature... serializationFeatureArr) {
        return _newWriter(getSerializationConfig().with(serializationFeature, serializationFeatureArr));
    }

    public ObjectWriter writer(DateFormat dateFormat) {
        return _newWriter(getSerializationConfig().with(dateFormat));
    }

    public ObjectWriter writerWithView(Class<?> cls) {
        return _newWriter(getSerializationConfig().withView(cls));
    }

    public ObjectWriter writerFor(Class<?> cls) {
        return _newWriter(getSerializationConfig(), cls == null ? null : this._typeFactory.constructType((Type) cls), null);
    }

    public ObjectWriter writerFor(TypeReference<?> typeReference) {
        return _newWriter(getSerializationConfig(), typeReference == null ? null : this._typeFactory.constructType(typeReference), null);
    }

    public ObjectWriter writerFor(JavaType javaType) {
        return _newWriter(getSerializationConfig(), javaType, null);
    }

    public ObjectWriter writer(PrettyPrinter prettyPrinter) {
        if (prettyPrinter == null) {
            prettyPrinter = ObjectWriter.NULL_PRETTY_PRINTER;
        }
        return _newWriter(getSerializationConfig(), null, prettyPrinter);
    }

    public ObjectWriter writerWithDefaultPrettyPrinter() {
        SerializationConfig serializationConfig = getSerializationConfig();
        return _newWriter(serializationConfig, null, serializationConfig.getDefaultPrettyPrinter());
    }

    public ObjectWriter writer(FilterProvider filterProvider) {
        return _newWriter(getSerializationConfig().withFilters(filterProvider));
    }

    public ObjectWriter writer(FormatSchema formatSchema) {
        _verifySchemaType(formatSchema);
        return _newWriter(getSerializationConfig(), formatSchema);
    }

    public ObjectWriter writer(Base64Variant base64Variant) {
        return _newWriter(getSerializationConfig().with(base64Variant));
    }

    public ObjectWriter writer(CharacterEscapes characterEscapes) {
        return _newWriter(getSerializationConfig()).with(characterEscapes);
    }

    public ObjectWriter writer(ContextAttributes contextAttributes) {
        return _newWriter(getSerializationConfig().with(contextAttributes));
    }

    @Deprecated
    public ObjectWriter writerWithType(Class<?> cls) {
        return _newWriter(getSerializationConfig(), cls == null ? null : this._typeFactory.constructType((Type) cls), null);
    }

    @Deprecated
    public ObjectWriter writerWithType(TypeReference<?> typeReference) {
        return _newWriter(getSerializationConfig(), typeReference == null ? null : this._typeFactory.constructType(typeReference), null);
    }

    @Deprecated
    public ObjectWriter writerWithType(JavaType javaType) {
        return _newWriter(getSerializationConfig(), javaType, null);
    }

    public ObjectReader reader() {
        return _newReader(getDeserializationConfig()).with(this._injectableValues);
    }

    public ObjectReader reader(DeserializationFeature deserializationFeature) {
        return _newReader(getDeserializationConfig().with(deserializationFeature));
    }

    public ObjectReader reader(DeserializationFeature deserializationFeature, DeserializationFeature... deserializationFeatureArr) {
        return _newReader(getDeserializationConfig().with(deserializationFeature, deserializationFeatureArr));
    }

    public ObjectReader readerForUpdating(Object obj) {
        return _newReader(getDeserializationConfig(), this._typeFactory.constructType((Type) obj.getClass()), obj, null, this._injectableValues);
    }

    public ObjectReader readerFor(JavaType javaType) {
        return _newReader(getDeserializationConfig(), javaType, null, null, this._injectableValues);
    }

    public ObjectReader readerFor(Class<?> cls) {
        return _newReader(getDeserializationConfig(), this._typeFactory.constructType((Type) cls), null, null, this._injectableValues);
    }

    public ObjectReader readerFor(TypeReference<?> typeReference) {
        return _newReader(getDeserializationConfig(), this._typeFactory.constructType(typeReference), null, null, this._injectableValues);
    }

    public ObjectReader reader(JsonNodeFactory jsonNodeFactory) {
        return _newReader(getDeserializationConfig()).with(jsonNodeFactory);
    }

    public ObjectReader reader(FormatSchema formatSchema) {
        _verifySchemaType(formatSchema);
        return _newReader(getDeserializationConfig(), null, null, formatSchema, this._injectableValues);
    }

    public ObjectReader reader(InjectableValues injectableValues) {
        return _newReader(getDeserializationConfig(), null, null, null, injectableValues);
    }

    public ObjectReader readerWithView(Class<?> cls) {
        return _newReader(getDeserializationConfig().withView(cls));
    }

    public ObjectReader reader(Base64Variant base64Variant) {
        return _newReader(getDeserializationConfig().with(base64Variant));
    }

    public ObjectReader reader(ContextAttributes contextAttributes) {
        return _newReader(getDeserializationConfig().with(contextAttributes));
    }

    @Deprecated
    public ObjectReader reader(JavaType javaType) {
        return _newReader(getDeserializationConfig(), javaType, null, null, this._injectableValues);
    }

    @Deprecated
    public ObjectReader reader(Class<?> cls) {
        return _newReader(getDeserializationConfig(), this._typeFactory.constructType((Type) cls), null, null, this._injectableValues);
    }

    @Deprecated
    public ObjectReader reader(TypeReference<?> typeReference) {
        return _newReader(getDeserializationConfig(), this._typeFactory.constructType(typeReference), null, null, this._injectableValues);
    }

    public <T> T convertValue(Object obj, Class<T> cls) throws IllegalArgumentException {
        if (obj == null) {
            return null;
        }
        return _convert(obj, this._typeFactory.constructType((Type) cls));
    }

    public <T> T convertValue(Object obj, TypeReference<?> typeReference) throws IllegalArgumentException {
        return convertValue(obj, this._typeFactory.constructType(typeReference));
    }

    public <T> T convertValue(Object obj, JavaType javaType) throws IllegalArgumentException {
        if (obj == null) {
            return null;
        }
        return _convert(obj, javaType);
    }

    /* access modifiers changed from: protected */
    public Object _convert(Object obj, JavaType javaType) throws IllegalArgumentException {
        Class<Object> rawClass = javaType.getRawClass();
        if (rawClass == Object.class || javaType.hasGenericTypes() || !rawClass.isAssignableFrom(obj.getClass())) {
            TokenBuffer tokenBuffer = new TokenBuffer((ObjectCodec) this, false);
            if (isEnabled(DeserializationFeature.USE_BIG_DECIMAL_FOR_FLOATS)) {
                tokenBuffer = tokenBuffer.forceUseOfBigDecimal(true);
            }
            try {
                _serializerProvider(getSerializationConfig().without(SerializationFeature.WRAP_ROOT_VALUE)).serializeValue(tokenBuffer, obj);
                JsonParser asParser = tokenBuffer.asParser();
                DeserializationConfig deserializationConfig = getDeserializationConfig();
                JsonToken _initForReading = _initForReading(asParser);
                if (_initForReading == JsonToken.VALUE_NULL) {
                    DefaultDeserializationContext createDeserializationContext = createDeserializationContext(asParser, deserializationConfig);
                    obj = _findRootDeserializer(createDeserializationContext, javaType).getNullValue(createDeserializationContext);
                } else if (_initForReading == JsonToken.END_ARRAY || _initForReading == JsonToken.END_OBJECT) {
                    obj = null;
                } else {
                    DefaultDeserializationContext createDeserializationContext2 = createDeserializationContext(asParser, deserializationConfig);
                    obj = _findRootDeserializer(createDeserializationContext2, javaType).deserialize(asParser, createDeserializationContext2);
                }
                asParser.close();
            } catch (IOException e) {
                throw new IllegalArgumentException(e.getMessage(), e);
            }
        }
        return obj;
    }

    @Deprecated
    public JsonSchema generateJsonSchema(Class<?> cls) throws JsonMappingException {
        return _serializerProvider(getSerializationConfig()).generateJsonSchema(cls);
    }

    public void acceptJsonFormatVisitor(Class<?> cls, JsonFormatVisitorWrapper jsonFormatVisitorWrapper) throws JsonMappingException {
        acceptJsonFormatVisitor(this._typeFactory.constructType((Type) cls), jsonFormatVisitorWrapper);
    }

    public void acceptJsonFormatVisitor(JavaType javaType, JsonFormatVisitorWrapper jsonFormatVisitorWrapper) throws JsonMappingException {
        if (javaType == null) {
            throw new IllegalArgumentException("type must be provided");
        }
        _serializerProvider(getSerializationConfig()).acceptJsonFormatVisitor(javaType, jsonFormatVisitorWrapper);
    }

    /* access modifiers changed from: protected */
    public DefaultSerializerProvider _serializerProvider(SerializationConfig serializationConfig) {
        return this._serializerProvider.createInstance(serializationConfig, this._serializerFactory);
    }

    /* access modifiers changed from: protected */
    @Deprecated
    public PrettyPrinter _defaultPrettyPrinter() {
        return this._serializationConfig.constructDefaultPrettyPrinter();
    }

    /* access modifiers changed from: protected */
    public final void _configAndWriteValue(JsonGenerator jsonGenerator, Object obj) throws IOException {
        SerializationConfig serializationConfig = getSerializationConfig();
        serializationConfig.initialize(jsonGenerator);
        if (!serializationConfig.isEnabled(SerializationFeature.CLOSE_CLOSEABLE) || !(obj instanceof Closeable)) {
            boolean z = false;
            try {
                _serializerProvider(serializationConfig).serializeValue(jsonGenerator, obj);
                z = true;
                jsonGenerator.close();
            } catch (Throwable th) {
                if (!z) {
                    jsonGenerator.disable(JsonGenerator.Feature.AUTO_CLOSE_JSON_CONTENT);
                    try {
                        jsonGenerator.close();
                    } catch (IOException e) {
                    }
                }
                throw th;
            }
        } else {
            _configAndWriteCloseable(jsonGenerator, obj, serializationConfig);
        }
    }

    /* access modifiers changed from: protected */
    @Deprecated
    public final void _configAndWriteValue(JsonGenerator jsonGenerator, Object obj, Class<?> cls) throws IOException {
        SerializationConfig withView = getSerializationConfig().withView(cls);
        withView.initialize(jsonGenerator);
        if (!withView.isEnabled(SerializationFeature.CLOSE_CLOSEABLE) || !(obj instanceof Closeable)) {
            boolean z = false;
            try {
                _serializerProvider(withView).serializeValue(jsonGenerator, obj);
                z = true;
                jsonGenerator.close();
            } catch (Throwable th) {
                if (!z) {
                    jsonGenerator.disable(JsonGenerator.Feature.AUTO_CLOSE_JSON_CONTENT);
                    try {
                        jsonGenerator.close();
                    } catch (IOException e) {
                    }
                }
                throw th;
            }
        } else {
            _configAndWriteCloseable(jsonGenerator, obj, withView);
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:19:0x0027  */
    /* JADX WARNING: Removed duplicated region for block: B:23:0x0031 A[SYNTHETIC, Splitter:B:23:0x0031] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final void _configAndWriteCloseable(com.fasterxml.jackson.core.JsonGenerator r5, java.lang.Object r6, com.fasterxml.jackson.databind.SerializationConfig r7) throws java.io.IOException {
        /*
            r4 = this;
            r3 = 0
            r0 = r6
            java.io.Closeable r0 = (java.io.Closeable) r0
            com.fasterxml.jackson.databind.ser.DefaultSerializerProvider r1 = r4._serializerProvider(r7)     // Catch:{ all -> 0x0023 }
            r1.serializeValue(r5, r6)     // Catch:{ all -> 0x0023 }
            r1 = 0
            r5.close()     // Catch:{ all -> 0x003d }
            r2 = 0
            r0.close()     // Catch:{ all -> 0x0041 }
            if (r3 == 0) goto L_0x001d
            com.fasterxml.jackson.core.JsonGenerator$Feature r0 = com.fasterxml.jackson.core.JsonGenerator.Feature.AUTO_CLOSE_JSON_CONTENT
            r3.disable(r0)
            r1.close()     // Catch:{ IOException -> 0x0035 }
        L_0x001d:
            if (r3 == 0) goto L_0x0022
            r2.close()     // Catch:{ IOException -> 0x0037 }
        L_0x0022:
            return
        L_0x0023:
            r1 = move-exception
            r2 = r0
        L_0x0025:
            if (r5 == 0) goto L_0x002f
            com.fasterxml.jackson.core.JsonGenerator$Feature r0 = com.fasterxml.jackson.core.JsonGenerator.Feature.AUTO_CLOSE_JSON_CONTENT
            r5.disable(r0)
            r5.close()     // Catch:{ IOException -> 0x0039 }
        L_0x002f:
            if (r2 == 0) goto L_0x0034
            r2.close()     // Catch:{ IOException -> 0x003b }
        L_0x0034:
            throw r1
        L_0x0035:
            r0 = move-exception
            goto L_0x001d
        L_0x0037:
            r0 = move-exception
            goto L_0x0022
        L_0x0039:
            r0 = move-exception
            goto L_0x002f
        L_0x003b:
            r0 = move-exception
            goto L_0x0034
        L_0x003d:
            r1 = move-exception
            r2 = r0
            r5 = r3
            goto L_0x0025
        L_0x0041:
            r0 = move-exception
            r1 = r0
            r2 = r3
            r5 = r3
            goto L_0x0025
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.ObjectMapper._configAndWriteCloseable(com.fasterxml.jackson.core.JsonGenerator, java.lang.Object, com.fasterxml.jackson.databind.SerializationConfig):void");
    }

    /* JADX WARNING: Removed duplicated region for block: B:14:0x0023 A[SYNTHETIC, Splitter:B:14:0x0023] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final void _writeCloseableValue(com.fasterxml.jackson.core.JsonGenerator r4, java.lang.Object r5, com.fasterxml.jackson.databind.SerializationConfig r6) throws java.io.IOException {
        /*
            r3 = this;
            r0 = r5
            java.io.Closeable r0 = (java.io.Closeable) r0
            com.fasterxml.jackson.databind.ser.DefaultSerializerProvider r1 = r3._serializerProvider(r6)     // Catch:{ all -> 0x001f }
            r1.serializeValue(r4, r5)     // Catch:{ all -> 0x001f }
            com.fasterxml.jackson.databind.SerializationFeature r1 = com.fasterxml.jackson.databind.SerializationFeature.FLUSH_AFTER_WRITE_VALUE     // Catch:{ all -> 0x001f }
            boolean r1 = r6.isEnabled(r1)     // Catch:{ all -> 0x001f }
            if (r1 == 0) goto L_0x0015
            r4.flush()     // Catch:{ all -> 0x001f }
        L_0x0015:
            r2 = 0
            r0.close()     // Catch:{ all -> 0x002b }
            if (r2 == 0) goto L_0x001e
            r2.close()     // Catch:{ IOException -> 0x0027 }
        L_0x001e:
            return
        L_0x001f:
            r1 = move-exception
            r2 = r0
        L_0x0021:
            if (r2 == 0) goto L_0x0026
            r2.close()     // Catch:{ IOException -> 0x0029 }
        L_0x0026:
            throw r1
        L_0x0027:
            r0 = move-exception
            goto L_0x001e
        L_0x0029:
            r0 = move-exception
            goto L_0x0026
        L_0x002b:
            r0 = move-exception
            r1 = r0
            goto L_0x0021
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.ObjectMapper._writeCloseableValue(com.fasterxml.jackson.core.JsonGenerator, java.lang.Object, com.fasterxml.jackson.databind.SerializationConfig):void");
    }

    /* access modifiers changed from: protected */
    public DefaultDeserializationContext createDeserializationContext(JsonParser jsonParser, DeserializationConfig deserializationConfig) {
        return this._deserializationContext.createInstance(deserializationConfig, jsonParser, this._injectableValues);
    }

    /* access modifiers changed from: protected */
    public Object _readValue(DeserializationConfig deserializationConfig, JsonParser jsonParser, JavaType javaType) throws IOException, JsonParseException, JsonMappingException {
        Object obj;
        JsonToken _initForReading = _initForReading(jsonParser);
        if (_initForReading == JsonToken.VALUE_NULL) {
            DefaultDeserializationContext createDeserializationContext = createDeserializationContext(jsonParser, deserializationConfig);
            obj = _findRootDeserializer(createDeserializationContext, javaType).getNullValue(createDeserializationContext);
        } else if (_initForReading == JsonToken.END_ARRAY || _initForReading == JsonToken.END_OBJECT) {
            obj = null;
        } else {
            DefaultDeserializationContext createDeserializationContext2 = createDeserializationContext(jsonParser, deserializationConfig);
            JsonDeserializer _findRootDeserializer = _findRootDeserializer(createDeserializationContext2, javaType);
            if (deserializationConfig.useRootWrapping()) {
                obj = _unwrapAndDeserialize(jsonParser, createDeserializationContext2, deserializationConfig, javaType, _findRootDeserializer);
            } else {
                obj = _findRootDeserializer.deserialize(jsonParser, createDeserializationContext2);
            }
        }
        jsonParser.clearCurrentToken();
        return obj;
    }

    /* access modifiers changed from: protected */
    public Object _readMapAndClose(JsonParser jsonParser, JavaType javaType) throws IOException, JsonParseException, JsonMappingException {
        Object obj;
        try {
            JsonToken _initForReading = _initForReading(jsonParser);
            if (_initForReading == JsonToken.VALUE_NULL) {
                DefaultDeserializationContext createDeserializationContext = createDeserializationContext(jsonParser, getDeserializationConfig());
                obj = _findRootDeserializer(createDeserializationContext, javaType).getNullValue(createDeserializationContext);
            } else if (_initForReading == JsonToken.END_ARRAY || _initForReading == JsonToken.END_OBJECT) {
                obj = null;
            } else {
                DeserializationConfig deserializationConfig = getDeserializationConfig();
                DefaultDeserializationContext createDeserializationContext2 = createDeserializationContext(jsonParser, deserializationConfig);
                JsonDeserializer _findRootDeserializer = _findRootDeserializer(createDeserializationContext2, javaType);
                if (deserializationConfig.useRootWrapping()) {
                    obj = _unwrapAndDeserialize(jsonParser, createDeserializationContext2, deserializationConfig, javaType, _findRootDeserializer);
                } else {
                    obj = _findRootDeserializer.deserialize(jsonParser, createDeserializationContext2);
                }
                createDeserializationContext2.checkUnresolvedObjectId();
            }
            jsonParser.clearCurrentToken();
            return obj;
        } finally {
            try {
                jsonParser.close();
            } catch (IOException e) {
            }
        }
    }

    /* access modifiers changed from: protected */
    public JsonToken _initForReading(JsonParser jsonParser) throws IOException {
        this._deserializationConfig.initialize(jsonParser);
        JsonToken currentToken = jsonParser.getCurrentToken();
        if (currentToken == null) {
            currentToken = jsonParser.nextToken();
            if (currentToken == null) {
                throw JsonMappingException.from(jsonParser, "No content to map due to end-of-input");
            }
        }
        return currentToken;
    }

    /* access modifiers changed from: protected */
    public Object _unwrapAndDeserialize(JsonParser jsonParser, DeserializationContext deserializationContext, DeserializationConfig deserializationConfig, JavaType javaType, JsonDeserializer<Object> jsonDeserializer) throws IOException {
        String simpleName = deserializationConfig.findRootName(javaType).getSimpleName();
        if (jsonParser.getCurrentToken() != JsonToken.START_OBJECT) {
            throw JsonMappingException.from(jsonParser, "Current token not START_OBJECT (needed to unwrap root name '" + simpleName + "'), but " + jsonParser.getCurrentToken());
        } else if (jsonParser.nextToken() != JsonToken.FIELD_NAME) {
            throw JsonMappingException.from(jsonParser, "Current token not FIELD_NAME (to contain expected root name '" + simpleName + "'), but " + jsonParser.getCurrentToken());
        } else {
            String currentName = jsonParser.getCurrentName();
            if (!simpleName.equals(currentName)) {
                throw JsonMappingException.from(jsonParser, "Root name '" + currentName + "' does not match expected ('" + simpleName + "') for type " + javaType);
            }
            jsonParser.nextToken();
            Object deserialize = jsonDeserializer.deserialize(jsonParser, deserializationContext);
            if (jsonParser.nextToken() == JsonToken.END_OBJECT) {
                return deserialize;
            }
            throw JsonMappingException.from(jsonParser, "Current token not END_OBJECT (to match wrapper object with root name '" + simpleName + "'), but " + jsonParser.getCurrentToken());
        }
    }

    /* access modifiers changed from: protected */
    public JsonDeserializer<Object> _findRootDeserializer(DeserializationContext deserializationContext, JavaType javaType) throws JsonMappingException {
        JsonDeserializer<Object> jsonDeserializer = (JsonDeserializer) this._rootDeserializers.get(javaType);
        if (jsonDeserializer == null) {
            jsonDeserializer = deserializationContext.findRootValueDeserializer(javaType);
            if (jsonDeserializer == null) {
                throw JsonMappingException.from(deserializationContext, "Can not find a deserializer for type " + javaType);
            }
            this._rootDeserializers.put(javaType, jsonDeserializer);
        }
        return jsonDeserializer;
    }

    /* access modifiers changed from: protected */
    public void _verifySchemaType(FormatSchema formatSchema) {
        if (formatSchema != null && !this._jsonFactory.canUseSchema(formatSchema)) {
            throw new IllegalArgumentException("Can not use FormatSchema of type " + formatSchema.getClass().getName() + " for format " + this._jsonFactory.getFormatName());
        }
    }
}
