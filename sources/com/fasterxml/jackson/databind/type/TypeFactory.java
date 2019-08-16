package com.fasterxml.jackson.databind.type;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.JavaType;
import com.fasterxml.jackson.databind.util.ArrayBuilders;
import com.fasterxml.jackson.databind.util.ClassUtil;
import com.fasterxml.jackson.databind.util.LRUMap;
import java.io.Serializable;
import java.lang.reflect.GenericArrayType;
import java.lang.reflect.ParameterizedType;
import java.lang.reflect.Type;
import java.lang.reflect.TypeVariable;
import java.lang.reflect.WildcardType;
import java.util.Collection;
import java.util.List;
import java.util.Map;
import java.util.Properties;
import java.util.concurrent.atomic.AtomicReference;

public final class TypeFactory implements Serializable {
    private static final Class<?> CLS_BOOL = Boolean.TYPE;
    private static final Class<?> CLS_CLASS = Class.class;
    private static final Class<?> CLS_COMPARABLE = Comparable.class;
    private static final Class<?> CLS_ENUM = Enum.class;
    private static final Class<?> CLS_INT = Integer.TYPE;
    private static final Class<?> CLS_LONG = Long.TYPE;
    private static final Class<?> CLS_OBJECT = Object.class;
    private static final Class<?> CLS_STRING = String.class;
    protected static final SimpleType CORE_TYPE_BOOL = new SimpleType(CLS_BOOL);
    protected static final SimpleType CORE_TYPE_CLASS = new SimpleType(CLS_CLASS);
    protected static final SimpleType CORE_TYPE_COMPARABLE = new SimpleType(CLS_COMPARABLE);
    protected static final SimpleType CORE_TYPE_ENUM = new SimpleType(CLS_ENUM);
    protected static final SimpleType CORE_TYPE_INT = new SimpleType(CLS_INT);
    protected static final SimpleType CORE_TYPE_LONG = new SimpleType(CLS_LONG);
    protected static final SimpleType CORE_TYPE_OBJECT = new SimpleType(CLS_OBJECT);
    protected static final SimpleType CORE_TYPE_STRING = new SimpleType(CLS_STRING);
    protected static final TypeBindings EMPTY_BINDINGS = TypeBindings.emptyBindings();
    private static final JavaType[] NO_TYPES = new JavaType[0];
    protected static final TypeFactory instance = new TypeFactory();
    private static final long serialVersionUID = 1;
    protected final ClassLoader _classLoader;
    protected final TypeModifier[] _modifiers;
    protected final TypeParser _parser;
    protected final LRUMap<Class<?>, JavaType> _typeCache;

    private TypeFactory() {
        this._typeCache = new LRUMap<>(16, 100);
        this._parser = new TypeParser(this);
        this._modifiers = null;
        this._classLoader = null;
    }

    protected TypeFactory(TypeParser typeParser, TypeModifier[] typeModifierArr) {
        this(typeParser, typeModifierArr, null);
    }

    protected TypeFactory(TypeParser typeParser, TypeModifier[] typeModifierArr, ClassLoader classLoader) {
        this._typeCache = new LRUMap<>(16, 100);
        this._parser = typeParser.withFactory(this);
        this._modifiers = typeModifierArr;
        this._classLoader = classLoader;
    }

    public TypeFactory withModifier(TypeModifier typeModifier) {
        if (typeModifier == null) {
            return new TypeFactory(this._parser, this._modifiers, this._classLoader);
        }
        if (this._modifiers != null) {
            return new TypeFactory(this._parser, (TypeModifier[]) ArrayBuilders.insertInListNoDup(this._modifiers, typeModifier), this._classLoader);
        }
        return new TypeFactory(this._parser, new TypeModifier[]{typeModifier}, this._classLoader);
    }

    public TypeFactory withClassLoader(ClassLoader classLoader) {
        return new TypeFactory(this._parser, this._modifiers, classLoader);
    }

    public static TypeFactory defaultInstance() {
        return instance;
    }

    public void clearCache() {
        this._typeCache.clear();
    }

    public ClassLoader getClassLoader() {
        return this._classLoader;
    }

    public static JavaType unknownType() {
        return defaultInstance()._unknownType();
    }

    public static Class<?> rawClass(Type type) {
        if (type instanceof Class) {
            return (Class) type;
        }
        return defaultInstance().constructType(type).getRawClass();
    }

    public Class<?> findClass(String str) throws ClassNotFoundException {
        if (str.indexOf(46) < 0) {
            Class<?> _findPrimitive = _findPrimitive(str);
            if (_findPrimitive != null) {
                return _findPrimitive;
            }
        }
        Throwable th = null;
        ClassLoader classLoader = getClassLoader();
        if (classLoader == null) {
            classLoader = Thread.currentThread().getContextClassLoader();
        }
        if (classLoader != null) {
            try {
                return classForName(str, true, classLoader);
            } catch (Exception e) {
                th = ClassUtil.getRootCause(e);
            }
        }
        try {
            return classForName(str);
        } catch (Exception e2) {
            if (th == null) {
                th = ClassUtil.getRootCause(e2);
            }
            if (th instanceof RuntimeException) {
                throw ((RuntimeException) th);
            }
            throw new ClassNotFoundException(th.getMessage(), th);
        }
    }

    /* access modifiers changed from: protected */
    public Class<?> classForName(String str, boolean z, ClassLoader classLoader) throws ClassNotFoundException {
        return Class.forName(str, true, classLoader);
    }

    /* access modifiers changed from: protected */
    public Class<?> classForName(String str) throws ClassNotFoundException {
        return Class.forName(str);
    }

    /* access modifiers changed from: protected */
    public Class<?> _findPrimitive(String str) {
        if ("int".equals(str)) {
            return Integer.TYPE;
        }
        if ("long".equals(str)) {
            return Long.TYPE;
        }
        if ("float".equals(str)) {
            return Float.TYPE;
        }
        if ("double".equals(str)) {
            return Double.TYPE;
        }
        if ("boolean".equals(str)) {
            return Boolean.TYPE;
        }
        if ("byte".equals(str)) {
            return Byte.TYPE;
        }
        if ("char".equals(str)) {
            return Character.TYPE;
        }
        if ("short".equals(str)) {
            return Short.TYPE;
        }
        if ("void".equals(str)) {
            return Void.TYPE;
        }
        return null;
    }

    /* JADX WARNING: Removed duplicated region for block: B:51:0x00d3  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.fasterxml.jackson.databind.JavaType constructSpecializedType(com.fasterxml.jackson.databind.JavaType r8, java.lang.Class<?> r9) {
        /*
            r7 = this;
            r6 = 2
            r5 = 1
            r4 = 0
            r1 = 0
            java.lang.Class r0 = r8.getRawClass()
            if (r0 != r9) goto L_0x000b
        L_0x000a:
            return r8
        L_0x000b:
            java.lang.Class<java.lang.Object> r2 = java.lang.Object.class
            if (r0 != r2) goto L_0x0019
            com.fasterxml.jackson.databind.type.TypeBindings r0 = com.fasterxml.jackson.databind.type.TypeBindings.emptyBindings()
            com.fasterxml.jackson.databind.JavaType r0 = r7._fromClass(r1, r9, r0)
        L_0x0017:
            r8 = r0
            goto L_0x000a
        L_0x0019:
            boolean r2 = r0.isAssignableFrom(r9)
            if (r2 != 0) goto L_0x0035
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.String r1 = "Class %s not subtype of %s"
            java.lang.Object[] r2 = new java.lang.Object[r6]
            java.lang.String r3 = r9.getName()
            r2[r4] = r3
            r2[r5] = r8
            java.lang.String r1 = java.lang.String.format(r1, r2)
            r0.<init>(r1)
            throw r0
        L_0x0035:
            com.fasterxml.jackson.databind.type.TypeBindings r2 = r8.getBindings()
            boolean r2 = r2.isEmpty()
            if (r2 == 0) goto L_0x0048
            com.fasterxml.jackson.databind.type.TypeBindings r0 = com.fasterxml.jackson.databind.type.TypeBindings.emptyBindings()
            com.fasterxml.jackson.databind.JavaType r0 = r7._fromClass(r1, r9, r0)
            goto L_0x0017
        L_0x0048:
            boolean r2 = r8.isContainerType()
            if (r2 == 0) goto L_0x009c
            boolean r2 = r8.isMapLikeType()
            if (r2 == 0) goto L_0x0075
            java.lang.Class<java.util.HashMap> r0 = java.util.HashMap.class
            if (r9 == r0) goto L_0x0064
            java.lang.Class<java.util.LinkedHashMap> r0 = java.util.LinkedHashMap.class
            if (r9 == r0) goto L_0x0064
            java.lang.Class<java.util.EnumMap> r0 = java.util.EnumMap.class
            if (r9 == r0) goto L_0x0064
            java.lang.Class<java.util.TreeMap> r0 = java.util.TreeMap.class
            if (r9 != r0) goto L_0x009c
        L_0x0064:
            com.fasterxml.jackson.databind.JavaType r0 = r8.getKeyType()
            com.fasterxml.jackson.databind.JavaType r2 = r8.getContentType()
            com.fasterxml.jackson.databind.type.TypeBindings r0 = com.fasterxml.jackson.databind.type.TypeBindings.create(r9, r0, r2)
            com.fasterxml.jackson.databind.JavaType r0 = r7._fromClass(r1, r9, r0)
            goto L_0x0017
        L_0x0075:
            boolean r2 = r8.isCollectionLikeType()
            if (r2 == 0) goto L_0x009c
            java.lang.Class<java.util.ArrayList> r2 = java.util.ArrayList.class
            if (r9 == r2) goto L_0x008b
            java.lang.Class<java.util.LinkedList> r2 = java.util.LinkedList.class
            if (r9 == r2) goto L_0x008b
            java.lang.Class<java.util.HashSet> r2 = java.util.HashSet.class
            if (r9 == r2) goto L_0x008b
            java.lang.Class<java.util.TreeSet> r2 = java.util.TreeSet.class
            if (r9 != r2) goto L_0x0098
        L_0x008b:
            com.fasterxml.jackson.databind.JavaType r0 = r8.getContentType()
            com.fasterxml.jackson.databind.type.TypeBindings r0 = com.fasterxml.jackson.databind.type.TypeBindings.create(r9, r0)
            com.fasterxml.jackson.databind.JavaType r0 = r7._fromClass(r1, r9, r0)
            goto L_0x0017
        L_0x0098:
            java.lang.Class<java.util.EnumSet> r2 = java.util.EnumSet.class
            if (r0 == r2) goto L_0x000a
        L_0x009c:
            java.lang.reflect.TypeVariable[] r0 = r9.getTypeParameters()
            int r2 = r0.length
            if (r2 != 0) goto L_0x00ad
            com.fasterxml.jackson.databind.type.TypeBindings r0 = com.fasterxml.jackson.databind.type.TypeBindings.emptyBindings()
            com.fasterxml.jackson.databind.JavaType r0 = r7._fromClass(r1, r9, r0)
            goto L_0x0017
        L_0x00ad:
            boolean r0 = r8.isInterface()
            if (r0 == 0) goto L_0x00dd
            com.fasterxml.jackson.databind.type.TypeBindings r0 = com.fasterxml.jackson.databind.type.TypeBindings.emptyBindings()
            com.fasterxml.jackson.databind.JavaType[] r3 = new com.fasterxml.jackson.databind.JavaType[r5]
            r3[r4] = r8
            com.fasterxml.jackson.databind.JavaType r0 = r8.refine(r9, r0, r1, r3)
        L_0x00bf:
            if (r0 != 0) goto L_0x0017
            int r0 = r8.containedTypeCount()
            if (r0 != r2) goto L_0x00f7
            if (r2 != r5) goto L_0x00e8
            com.fasterxml.jackson.databind.JavaType r0 = r8.containedType(r4)
            com.fasterxml.jackson.databind.type.TypeBindings r0 = com.fasterxml.jackson.databind.type.TypeBindings.create(r9, r0)
        L_0x00d1:
            if (r0 != 0) goto L_0x00d7
            com.fasterxml.jackson.databind.type.TypeBindings r0 = com.fasterxml.jackson.databind.type.TypeBindings.emptyBindings()
        L_0x00d7:
            com.fasterxml.jackson.databind.JavaType r0 = r7._fromClass(r1, r9, r0)
            goto L_0x0017
        L_0x00dd:
            com.fasterxml.jackson.databind.type.TypeBindings r0 = com.fasterxml.jackson.databind.type.TypeBindings.emptyBindings()
            com.fasterxml.jackson.databind.JavaType[] r3 = NO_TYPES
            com.fasterxml.jackson.databind.JavaType r0 = r8.refine(r9, r0, r8, r3)
            goto L_0x00bf
        L_0x00e8:
            if (r2 != r6) goto L_0x00f7
            com.fasterxml.jackson.databind.JavaType r0 = r8.containedType(r4)
            com.fasterxml.jackson.databind.JavaType r2 = r8.containedType(r5)
            com.fasterxml.jackson.databind.type.TypeBindings r0 = com.fasterxml.jackson.databind.type.TypeBindings.create(r9, r0, r2)
            goto L_0x00d1
        L_0x00f7:
            r0 = r1
            goto L_0x00d1
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.type.TypeFactory.constructSpecializedType(com.fasterxml.jackson.databind.JavaType, java.lang.Class):com.fasterxml.jackson.databind.JavaType");
    }

    public JavaType constructGeneralizedType(JavaType javaType, Class<?> cls) {
        Class<?> rawClass = javaType.getRawClass();
        if (rawClass == cls) {
            return javaType;
        }
        JavaType findSuperType = javaType.findSuperType(cls);
        if (findSuperType != null) {
            return findSuperType;
        }
        if (!cls.isAssignableFrom(rawClass)) {
            throw new IllegalArgumentException(String.format("Class %s not a super-type of %s", new Object[]{cls.getName(), javaType}));
        }
        throw new IllegalArgumentException(String.format("Internal error: class %s not included as super-type for %s", new Object[]{cls.getName(), javaType}));
    }

    public JavaType constructFromCanonical(String str) throws IllegalArgumentException {
        return this._parser.parse(str);
    }

    public JavaType[] findTypeParameters(JavaType javaType, Class<?> cls) {
        JavaType findSuperType = javaType.findSuperType(cls);
        if (findSuperType == null) {
            return NO_TYPES;
        }
        return findSuperType.getBindings().typeParameterArray();
    }

    @Deprecated
    public JavaType[] findTypeParameters(Class<?> cls, Class<?> cls2, TypeBindings typeBindings) {
        return findTypeParameters(constructType((Type) cls, typeBindings), cls2);
    }

    @Deprecated
    public JavaType[] findTypeParameters(Class<?> cls, Class<?> cls2) {
        return findTypeParameters(constructType((Type) cls), cls2);
    }

    public JavaType moreSpecificType(JavaType javaType, JavaType javaType2) {
        if (javaType == null) {
            return javaType2;
        }
        if (javaType2 == null) {
            return javaType;
        }
        Class rawClass = javaType.getRawClass();
        Class rawClass2 = javaType2.getRawClass();
        if (rawClass == rawClass2 || !rawClass.isAssignableFrom(rawClass2)) {
            return javaType;
        }
        return javaType2;
    }

    public JavaType constructType(Type type) {
        return _fromAny(null, type, EMPTY_BINDINGS);
    }

    public JavaType constructType(Type type, TypeBindings typeBindings) {
        return _fromAny(null, type, typeBindings);
    }

    public JavaType constructType(TypeReference<?> typeReference) {
        return _fromAny(null, typeReference.getType(), EMPTY_BINDINGS);
    }

    @Deprecated
    public JavaType constructType(Type type, Class<?> cls) {
        return _fromAny(null, type, cls == null ? TypeBindings.emptyBindings() : constructType((Type) cls).getBindings());
    }

    @Deprecated
    public JavaType constructType(Type type, JavaType javaType) {
        return _fromAny(null, type, javaType == null ? TypeBindings.emptyBindings() : javaType.getBindings());
    }

    public ArrayType constructArrayType(Class<?> cls) {
        return ArrayType.construct(_fromAny(null, cls, null), null);
    }

    public ArrayType constructArrayType(JavaType javaType) {
        return ArrayType.construct(javaType, null);
    }

    public CollectionType constructCollectionType(Class<? extends Collection> cls, Class<?> cls2) {
        return constructCollectionType(cls, _fromClass(null, cls2, EMPTY_BINDINGS));
    }

    public CollectionType constructCollectionType(Class<? extends Collection> cls, JavaType javaType) {
        return (CollectionType) _fromClass(null, cls, TypeBindings.create(cls, javaType));
    }

    public CollectionLikeType constructCollectionLikeType(Class<?> cls, Class<?> cls2) {
        return constructCollectionLikeType(cls, _fromClass(null, cls2, EMPTY_BINDINGS));
    }

    public CollectionLikeType constructCollectionLikeType(Class<?> cls, JavaType javaType) {
        JavaType _fromClass = _fromClass(null, cls, TypeBindings.createIfNeeded(cls, javaType));
        if (_fromClass instanceof CollectionLikeType) {
            return (CollectionLikeType) _fromClass;
        }
        return CollectionLikeType.upgradeFrom(_fromClass, javaType);
    }

    public MapType constructMapType(Class<? extends Map> cls, Class<?> cls2, Class<?> cls3) {
        JavaType _fromClass;
        JavaType javaType;
        if (cls == Properties.class) {
            JavaType javaType2 = CORE_TYPE_STRING;
            _fromClass = javaType2;
            javaType = javaType2;
        } else {
            JavaType _fromClass2 = _fromClass(null, cls2, EMPTY_BINDINGS);
            _fromClass = _fromClass(null, cls3, EMPTY_BINDINGS);
            javaType = _fromClass2;
        }
        return constructMapType(cls, javaType, _fromClass);
    }

    public MapType constructMapType(Class<? extends Map> cls, JavaType javaType, JavaType javaType2) {
        return (MapType) _fromClass(null, cls, TypeBindings.create(cls, new JavaType[]{javaType, javaType2}));
    }

    public MapLikeType constructMapLikeType(Class<?> cls, Class<?> cls2, Class<?> cls3) {
        return constructMapLikeType(cls, _fromClass(null, cls2, EMPTY_BINDINGS), _fromClass(null, cls3, EMPTY_BINDINGS));
    }

    public MapLikeType constructMapLikeType(Class<?> cls, JavaType javaType, JavaType javaType2) {
        JavaType _fromClass = _fromClass(null, cls, TypeBindings.createIfNeeded(cls, new JavaType[]{javaType, javaType2}));
        if (_fromClass instanceof MapLikeType) {
            return (MapLikeType) _fromClass;
        }
        return MapLikeType.upgradeFrom(_fromClass, javaType, javaType2);
    }

    public JavaType constructSimpleType(Class<?> cls, JavaType[] javaTypeArr) {
        return _fromClass(null, cls, TypeBindings.create(cls, javaTypeArr));
    }

    @Deprecated
    public JavaType constructSimpleType(Class<?> cls, Class<?> cls2, JavaType[] javaTypeArr) {
        return constructSimpleType(cls, javaTypeArr);
    }

    public JavaType constructReferenceType(Class<?> cls, JavaType javaType) {
        return ReferenceType.construct(cls, null, null, null, javaType);
    }

    public JavaType uncheckedSimpleType(Class<?> cls) {
        return _constructSimple(cls, EMPTY_BINDINGS, null, null);
    }

    public JavaType constructParametricType(Class<?> cls, Class<?>... clsArr) {
        int length = clsArr.length;
        JavaType[] javaTypeArr = new JavaType[length];
        for (int i = 0; i < length; i++) {
            javaTypeArr[i] = _fromClass(null, clsArr[i], null);
        }
        return constructParametricType(cls, javaTypeArr);
    }

    public JavaType constructParametricType(Class<?> cls, JavaType... javaTypeArr) {
        return _fromClass(null, cls, TypeBindings.create(cls, javaTypeArr));
    }

    public JavaType constructParametrizedType(Class<?> cls, Class<?> cls2, JavaType... javaTypeArr) {
        return constructParametricType(cls, javaTypeArr);
    }

    public JavaType constructParametrizedType(Class<?> cls, Class<?> cls2, Class<?>... clsArr) {
        return constructParametricType(cls, clsArr);
    }

    public CollectionType constructRawCollectionType(Class<? extends Collection> cls) {
        return constructCollectionType(cls, unknownType());
    }

    public CollectionLikeType constructRawCollectionLikeType(Class<?> cls) {
        return constructCollectionLikeType(cls, unknownType());
    }

    public MapType constructRawMapType(Class<? extends Map> cls) {
        return constructMapType(cls, unknownType(), unknownType());
    }

    public MapLikeType constructRawMapLikeType(Class<?> cls) {
        return constructMapLikeType(cls, unknownType(), unknownType());
    }

    private JavaType _mapType(Class<?> cls, TypeBindings typeBindings, JavaType javaType, JavaType[] javaTypeArr) {
        JavaType javaType2;
        JavaType javaType3;
        if (cls == Properties.class) {
            SimpleType simpleType = CORE_TYPE_STRING;
            javaType2 = simpleType;
            javaType3 = simpleType;
        } else {
            List typeParameters = typeBindings.getTypeParameters();
            switch (typeParameters.size()) {
                case 0:
                    JavaType _unknownType = _unknownType();
                    javaType2 = _unknownType;
                    javaType3 = _unknownType;
                    break;
                case 2:
                    javaType2 = (JavaType) typeParameters.get(1);
                    javaType3 = (JavaType) typeParameters.get(0);
                    break;
                default:
                    throw new IllegalArgumentException("Strange Map type " + cls.getName() + ": can not determine type parameters");
            }
        }
        return MapType.construct(cls, typeBindings, javaType, javaTypeArr, javaType3, javaType2);
    }

    private JavaType _collectionType(Class<?> cls, TypeBindings typeBindings, JavaType javaType, JavaType[] javaTypeArr) {
        JavaType javaType2;
        List typeParameters = typeBindings.getTypeParameters();
        if (typeParameters.isEmpty()) {
            javaType2 = _unknownType();
        } else if (typeParameters.size() == 1) {
            javaType2 = (JavaType) typeParameters.get(0);
        } else {
            throw new IllegalArgumentException("Strange Collection type " + cls.getName() + ": can not determine type parameters");
        }
        return CollectionType.construct(cls, typeBindings, javaType, javaTypeArr, javaType2);
    }

    private JavaType _referenceType(Class<?> cls, TypeBindings typeBindings, JavaType javaType, JavaType[] javaTypeArr) {
        JavaType javaType2;
        List typeParameters = typeBindings.getTypeParameters();
        if (typeParameters.isEmpty()) {
            javaType2 = _unknownType();
        } else if (typeParameters.size() == 1) {
            javaType2 = (JavaType) typeParameters.get(0);
        } else {
            throw new IllegalArgumentException("Strange Reference type " + cls.getName() + ": can not determine type parameters");
        }
        return ReferenceType.construct(cls, typeBindings, javaType, javaTypeArr, javaType2);
    }

    /* access modifiers changed from: protected */
    public JavaType _constructSimple(Class<?> cls, TypeBindings typeBindings, JavaType javaType, JavaType[] javaTypeArr) {
        if (typeBindings.isEmpty()) {
            JavaType _findWellKnownSimple = _findWellKnownSimple(cls);
            if (_findWellKnownSimple != null) {
                return _findWellKnownSimple;
            }
        }
        return _newSimpleType(cls, typeBindings, javaType, javaTypeArr);
    }

    /* access modifiers changed from: protected */
    public JavaType _newSimpleType(Class<?> cls, TypeBindings typeBindings, JavaType javaType, JavaType[] javaTypeArr) {
        return new SimpleType(cls, typeBindings, javaType, javaTypeArr);
    }

    /* access modifiers changed from: protected */
    public JavaType _unknownType() {
        return CORE_TYPE_OBJECT;
    }

    /* access modifiers changed from: protected */
    public JavaType _findWellKnownSimple(Class<?> cls) {
        if (cls.isPrimitive()) {
            if (cls == CLS_BOOL) {
                return CORE_TYPE_BOOL;
            }
            if (cls == CLS_INT) {
                return CORE_TYPE_INT;
            }
            if (cls == CLS_LONG) {
                return CORE_TYPE_LONG;
            }
        } else if (cls == CLS_STRING) {
            return CORE_TYPE_STRING;
        } else {
            if (cls == CLS_OBJECT) {
                return CORE_TYPE_OBJECT;
            }
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public JavaType _fromAny(ClassStack classStack, Type type, TypeBindings typeBindings) {
        JavaType _fromWildcard;
        if (type instanceof Class) {
            _fromWildcard = _fromClass(classStack, (Class) type, EMPTY_BINDINGS);
        } else if (type instanceof ParameterizedType) {
            _fromWildcard = _fromParamType(classStack, (ParameterizedType) type, typeBindings);
        } else if (type instanceof JavaType) {
            return (JavaType) type;
        } else {
            if (type instanceof GenericArrayType) {
                _fromWildcard = _fromArrayType(classStack, (GenericArrayType) type, typeBindings);
            } else if (type instanceof TypeVariable) {
                _fromWildcard = _fromVariable(classStack, (TypeVariable) type, typeBindings);
            } else if (type instanceof WildcardType) {
                _fromWildcard = _fromWildcard(classStack, (WildcardType) type, typeBindings);
            } else {
                throw new IllegalArgumentException("Unrecognized Type: " + (type == null ? "[null]" : type.toString()));
            }
        }
        if (this._modifiers != null) {
            TypeBindings bindings = _fromWildcard.getBindings();
            if (bindings == null) {
                bindings = EMPTY_BINDINGS;
            }
            TypeModifier[] typeModifierArr = this._modifiers;
            int length = typeModifierArr.length;
            int i = 0;
            while (i < length) {
                TypeModifier typeModifier = typeModifierArr[i];
                JavaType modifyType = typeModifier.modifyType(_fromWildcard, type, bindings, this);
                if (modifyType == null) {
                    throw new IllegalStateException(String.format("TypeModifier %s (of type %s) return null for type %s", new Object[]{typeModifier, typeModifier.getClass().getName(), _fromWildcard}));
                }
                i++;
                _fromWildcard = modifyType;
            }
        }
        return _fromWildcard;
    }

    /* access modifiers changed from: protected */
    public JavaType _fromClass(ClassStack classStack, Class<?> cls, TypeBindings typeBindings) {
        boolean z;
        ClassStack child;
        JavaType _resolveSuperClass;
        JavaType[] _resolveSuperInterfaces;
        JavaType javaType;
        JavaType _findWellKnownSimple = _findWellKnownSimple(cls);
        if (_findWellKnownSimple != null) {
            return _findWellKnownSimple;
        }
        if (typeBindings == null || typeBindings.isEmpty()) {
            z = true;
        } else {
            z = false;
        }
        if (z) {
            _findWellKnownSimple = (JavaType) this._typeCache.get(cls);
            if (_findWellKnownSimple != null) {
                return _findWellKnownSimple;
            }
        }
        JavaType javaType2 = _findWellKnownSimple;
        if (classStack == null) {
            child = new ClassStack(cls);
        } else {
            ClassStack find = classStack.find(cls);
            if (find != null) {
                ResolvedRecursiveType resolvedRecursiveType = new ResolvedRecursiveType(cls, EMPTY_BINDINGS);
                find.addSelfReference(resolvedRecursiveType);
                return resolvedRecursiveType;
            }
            child = classStack.child(cls);
        }
        if (cls.isArray()) {
            javaType = ArrayType.construct(_fromAny(child, cls.getComponentType(), typeBindings), typeBindings);
        } else {
            if (cls.isInterface()) {
                _resolveSuperClass = null;
                _resolveSuperInterfaces = _resolveSuperInterfaces(child, cls, typeBindings);
            } else {
                _resolveSuperClass = _resolveSuperClass(child, cls, typeBindings);
                _resolveSuperInterfaces = _resolveSuperInterfaces(child, cls, typeBindings);
            }
            javaType = cls == Properties.class ? MapType.construct(cls, typeBindings, _resolveSuperClass, _resolveSuperInterfaces, CORE_TYPE_STRING, CORE_TYPE_STRING) : _resolveSuperClass != null ? _resolveSuperClass.refine(cls, typeBindings, _resolveSuperClass, _resolveSuperInterfaces) : javaType2;
            if (javaType == null) {
                javaType = _fromWellKnownClass(child, cls, typeBindings, _resolveSuperClass, _resolveSuperInterfaces);
                if (javaType == null) {
                    javaType = _fromWellKnownInterface(child, cls, typeBindings, _resolveSuperClass, _resolveSuperInterfaces);
                    if (javaType == null) {
                        javaType = _newSimpleType(cls, typeBindings, _resolveSuperClass, _resolveSuperInterfaces);
                    }
                }
            }
        }
        child.resolveSelfReferences(javaType);
        if (!z) {
            return javaType;
        }
        this._typeCache.putIfAbsent(cls, javaType);
        return javaType;
    }

    /* access modifiers changed from: protected */
    public JavaType _resolveSuperClass(ClassStack classStack, Class<?> cls, TypeBindings typeBindings) {
        Type genericSuperclass = ClassUtil.getGenericSuperclass(cls);
        if (genericSuperclass == null) {
            return null;
        }
        return _fromAny(classStack, genericSuperclass, typeBindings);
    }

    /* access modifiers changed from: protected */
    public JavaType[] _resolveSuperInterfaces(ClassStack classStack, Class<?> cls, TypeBindings typeBindings) {
        Type[] genericInterfaces = ClassUtil.getGenericInterfaces(cls);
        if (genericInterfaces == null || genericInterfaces.length == 0) {
            return NO_TYPES;
        }
        int length = genericInterfaces.length;
        JavaType[] javaTypeArr = new JavaType[length];
        for (int i = 0; i < length; i++) {
            javaTypeArr[i] = _fromAny(classStack, genericInterfaces[i], typeBindings);
        }
        return javaTypeArr;
    }

    /* access modifiers changed from: protected */
    public JavaType _fromWellKnownClass(ClassStack classStack, Class<?> cls, TypeBindings typeBindings, JavaType javaType, JavaType[] javaTypeArr) {
        if (cls == Map.class) {
            return _mapType(cls, typeBindings, javaType, javaTypeArr);
        }
        if (cls == Collection.class) {
            return _collectionType(cls, typeBindings, javaType, javaTypeArr);
        }
        if (cls == AtomicReference.class) {
            return _referenceType(cls, typeBindings, javaType, javaTypeArr);
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public JavaType _fromWellKnownInterface(ClassStack classStack, Class<?> cls, TypeBindings typeBindings, JavaType javaType, JavaType[] javaTypeArr) {
        for (JavaType refine : javaTypeArr) {
            JavaType refine2 = refine.refine(cls, typeBindings, javaType, javaTypeArr);
            if (refine2 != null) {
                return refine2;
            }
        }
        return null;
    }

    /* access modifiers changed from: protected */
    public JavaType _fromParamType(ClassStack classStack, ParameterizedType parameterizedType, TypeBindings typeBindings) {
        TypeBindings create;
        Class<?> cls = (Class) parameterizedType.getRawType();
        if (cls == CLS_ENUM) {
            return CORE_TYPE_ENUM;
        }
        if (cls == CLS_COMPARABLE) {
            return CORE_TYPE_COMPARABLE;
        }
        if (cls == CLS_CLASS) {
            return CORE_TYPE_CLASS;
        }
        Type[] actualTypeArguments = parameterizedType.getActualTypeArguments();
        int length = actualTypeArguments == null ? 0 : actualTypeArguments.length;
        if (length == 0) {
            create = EMPTY_BINDINGS;
        } else {
            JavaType[] javaTypeArr = new JavaType[length];
            for (int i = 0; i < length; i++) {
                javaTypeArr[i] = _fromAny(classStack, actualTypeArguments[i], typeBindings);
            }
            create = TypeBindings.create(cls, javaTypeArr);
        }
        return _fromClass(classStack, cls, create);
    }

    /* access modifiers changed from: protected */
    public JavaType _fromArrayType(ClassStack classStack, GenericArrayType genericArrayType, TypeBindings typeBindings) {
        return ArrayType.construct(_fromAny(classStack, genericArrayType.getGenericComponentType(), typeBindings), typeBindings);
    }

    /* access modifiers changed from: protected */
    public JavaType _fromVariable(ClassStack classStack, TypeVariable<?> typeVariable, TypeBindings typeBindings) {
        String name = typeVariable.getName();
        JavaType findBoundType = typeBindings.findBoundType(name);
        if (findBoundType != null) {
            return findBoundType;
        }
        if (typeBindings.hasUnbound(name)) {
            return CORE_TYPE_OBJECT;
        }
        return _fromAny(classStack, typeVariable.getBounds()[0], typeBindings.withUnboundVariable(name));
    }

    /* access modifiers changed from: protected */
    public JavaType _fromWildcard(ClassStack classStack, WildcardType wildcardType, TypeBindings typeBindings) {
        return _fromAny(classStack, wildcardType.getUpperBounds()[0], typeBindings);
    }
}
