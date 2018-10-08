package org.apache.commons.lang3.reflect;

import java.lang.reflect.Array;
import java.lang.reflect.GenericArrayType;
import java.lang.reflect.GenericDeclaration;
import java.lang.reflect.ParameterizedType;
import java.lang.reflect.Type;
import java.lang.reflect.TypeVariable;
import java.lang.reflect.WildcardType;
import java.util.Arrays;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;
import org.apache.commons.lang3.ArrayUtils;
import org.apache.commons.lang3.ClassUtils;
import org.apache.commons.lang3.ObjectUtils;
import org.apache.commons.lang3.Validate;
import org.apache.commons.lang3.builder.Builder;

public class TypeUtils {
    public static final WildcardType WILDCARD_ALL = wildcardType().withUpperBounds(Object.class).build();

    private static final class GenericArrayTypeImpl implements GenericArrayType {
        private final Type componentType;

        private GenericArrayTypeImpl(Type type) {
            this.componentType = type;
        }

        public Type getGenericComponentType() {
            return this.componentType;
        }

        public String toString() {
            return TypeUtils.toString(this);
        }

        public boolean equals(Object obj) {
            return obj == this || ((obj instanceof GenericArrayType) && TypeUtils.equals((GenericArrayType) this, (Type) (GenericArrayType) obj));
        }

        public int hashCode() {
            return 1072 | this.componentType.hashCode();
        }
    }

    private static final class ParameterizedTypeImpl implements ParameterizedType {
        private final Class<?> raw;
        private final Type[] typeArguments;
        private final Type useOwner;

        private ParameterizedTypeImpl(Class<?> cls, Type type, Type[] typeArr) {
            this.raw = cls;
            this.useOwner = type;
            this.typeArguments = typeArr;
        }

        public Type getRawType() {
            return this.raw;
        }

        public Type getOwnerType() {
            return this.useOwner;
        }

        public Type[] getActualTypeArguments() {
            return (Type[]) this.typeArguments.clone();
        }

        public String toString() {
            return TypeUtils.toString(this);
        }

        public boolean equals(Object obj) {
            return obj == this || ((obj instanceof ParameterizedType) && TypeUtils.equals((ParameterizedType) this, (Type) (ParameterizedType) obj));
        }

        public int hashCode() {
            return ((((1136 | this.raw.hashCode()) << 4) | ObjectUtils.hashCode(this.useOwner)) << 8) | Arrays.hashCode(this.typeArguments);
        }
    }

    public static class WildcardTypeBuilder implements Builder<WildcardType> {
        private Type[] lowerBounds;
        private Type[] upperBounds;

        private WildcardTypeBuilder() {
        }

        public WildcardTypeBuilder withUpperBounds(Type... typeArr) {
            this.upperBounds = typeArr;
            return this;
        }

        public WildcardTypeBuilder withLowerBounds(Type... typeArr) {
            this.lowerBounds = typeArr;
            return this;
        }

        public WildcardType build() {
            return new WildcardTypeImpl(this.upperBounds, this.lowerBounds);
        }
    }

    private static final class WildcardTypeImpl implements WildcardType {
        private static final Type[] EMPTY_BOUNDS = new Type[0];
        private final Type[] lowerBounds;
        private final Type[] upperBounds;

        private WildcardTypeImpl(Type[] typeArr, Type[] typeArr2) {
            this.upperBounds = (Type[]) ObjectUtils.defaultIfNull(typeArr, EMPTY_BOUNDS);
            this.lowerBounds = (Type[]) ObjectUtils.defaultIfNull(typeArr2, EMPTY_BOUNDS);
        }

        public Type[] getUpperBounds() {
            return (Type[]) this.upperBounds.clone();
        }

        public Type[] getLowerBounds() {
            return (Type[]) this.lowerBounds.clone();
        }

        public String toString() {
            return TypeUtils.toString(this);
        }

        public boolean equals(Object obj) {
            return obj == this || ((obj instanceof WildcardType) && TypeUtils.equals((WildcardType) this, (Type) (WildcardType) obj));
        }

        public int hashCode() {
            return ((18688 | Arrays.hashCode(this.upperBounds)) << 8) | Arrays.hashCode(this.lowerBounds);
        }
    }

    public static boolean isAssignable(Type type, Type type2) {
        return isAssignable(type, type2, null);
    }

    private static boolean isAssignable(Type type, Type type2, Map<TypeVariable<?>, Type> map) {
        if (type2 == null || (type2 instanceof Class)) {
            return isAssignable(type, (Class) type2);
        }
        if (type2 instanceof ParameterizedType) {
            return isAssignable(type, (ParameterizedType) type2, (Map) map);
        }
        if (type2 instanceof GenericArrayType) {
            return isAssignable(type, (GenericArrayType) type2, (Map) map);
        }
        if (type2 instanceof WildcardType) {
            return isAssignable(type, (WildcardType) type2, (Map) map);
        }
        if (type2 instanceof TypeVariable) {
            return isAssignable(type, (TypeVariable) type2, (Map) map);
        }
        throw new IllegalStateException("found an unhandled type: " + type2);
    }

    private static boolean isAssignable(Type type, Class<?> cls) {
        if (type == null) {
            if (cls == null || !cls.isPrimitive()) {
                return true;
            }
            return false;
        } else if (cls == null) {
            return false;
        } else {
            if (cls.equals(type)) {
                return true;
            }
            if (type instanceof Class) {
                return ClassUtils.isAssignable((Class) type, (Class) cls);
            }
            if (type instanceof ParameterizedType) {
                return isAssignable(getRawType((ParameterizedType) type), (Class) cls);
            }
            if (type instanceof TypeVariable) {
                for (Type isAssignable : ((TypeVariable) type).getBounds()) {
                    if (isAssignable(isAssignable, (Class) cls)) {
                        return true;
                    }
                }
                return false;
            } else if (type instanceof GenericArrayType) {
                if (cls.equals(Object.class) || (cls.isArray() && isAssignable(((GenericArrayType) type).getGenericComponentType(), cls.getComponentType()))) {
                    return true;
                }
                return false;
            } else if (type instanceof WildcardType) {
                return false;
            } else {
                throw new IllegalStateException("found an unhandled type: " + type);
            }
        }
    }

    private static boolean isAssignable(Type type, ParameterizedType parameterizedType, Map<TypeVariable<?>, Type> map) {
        if (type == null) {
            return true;
        }
        if (parameterizedType == null) {
            return false;
        }
        if (parameterizedType.equals(type)) {
            return true;
        }
        Class rawType = getRawType(parameterizedType);
        Map typeArguments = getTypeArguments(type, rawType, null);
        if (typeArguments == null) {
            return false;
        }
        if (typeArguments.isEmpty()) {
            return true;
        }
        Map typeArguments2 = getTypeArguments(parameterizedType, rawType, (Map) map);
        for (TypeVariable typeVariable : typeArguments2.keySet()) {
            Type unrollVariableAssignments = unrollVariableAssignments(typeVariable, typeArguments2);
            Type unrollVariableAssignments2 = unrollVariableAssignments(typeVariable, typeArguments);
            if (!(unrollVariableAssignments2 == null || unrollVariableAssignments.equals(unrollVariableAssignments2))) {
                if (!(unrollVariableAssignments instanceof WildcardType) || !isAssignable(unrollVariableAssignments2, unrollVariableAssignments, (Map) map)) {
                    return false;
                }
            }
        }
        return true;
    }

    private static Type unrollVariableAssignments(TypeVariable<?> typeVariable, Map<TypeVariable<?>, Type> map) {
        Type type;
        while (true) {
            type = (Type) map.get(typeVariable);
            if (!(type instanceof TypeVariable) || type.equals(typeVariable)) {
                return type;
            }
            typeVariable = (TypeVariable) type;
        }
        return type;
    }

    private static boolean isAssignable(Type type, GenericArrayType genericArrayType, Map<TypeVariable<?>, Type> map) {
        if (type == null) {
            return true;
        }
        if (genericArrayType == null) {
            return false;
        }
        if (genericArrayType.equals(type)) {
            return true;
        }
        Type genericComponentType = genericArrayType.getGenericComponentType();
        if (type instanceof Class) {
            Class cls = (Class) type;
            if (cls.isArray() && isAssignable(cls.getComponentType(), genericComponentType, (Map) map)) {
                return true;
            }
            return false;
        } else if (type instanceof GenericArrayType) {
            return isAssignable(((GenericArrayType) type).getGenericComponentType(), genericComponentType, (Map) map);
        } else {
            if (type instanceof WildcardType) {
                for (Type isAssignable : getImplicitUpperBounds((WildcardType) type)) {
                    if (isAssignable(isAssignable, (Type) genericArrayType)) {
                        return true;
                    }
                }
                return false;
            } else if (type instanceof TypeVariable) {
                for (Type isAssignable2 : getImplicitBounds((TypeVariable) type)) {
                    if (isAssignable(isAssignable2, (Type) genericArrayType)) {
                        return true;
                    }
                }
                return false;
            } else if (type instanceof ParameterizedType) {
                return false;
            } else {
                throw new IllegalStateException("found an unhandled type: " + type);
            }
        }
    }

    private static boolean isAssignable(Type type, WildcardType wildcardType, Map<TypeVariable<?>, Type> map) {
        if (type == null) {
            return true;
        }
        if (wildcardType == null) {
            return false;
        }
        if (wildcardType.equals(type)) {
            return true;
        }
        Type[] implicitUpperBounds = getImplicitUpperBounds(wildcardType);
        Type[] implicitLowerBounds = getImplicitLowerBounds(wildcardType);
        Type substituteTypeVariables;
        if (type instanceof WildcardType) {
            Type substituteTypeVariables2;
            WildcardType wildcardType2 = (WildcardType) type;
            Type[] implicitUpperBounds2 = getImplicitUpperBounds(wildcardType2);
            Type[] implicitLowerBounds2 = getImplicitLowerBounds(wildcardType2);
            for (Type substituteTypeVariables3 : implicitUpperBounds) {
                substituteTypeVariables2 = substituteTypeVariables(substituteTypeVariables3, map);
                for (Type isAssignable : implicitUpperBounds2) {
                    if (!isAssignable(isAssignable, substituteTypeVariables2, (Map) map)) {
                        return false;
                    }
                }
            }
            for (Type substituteTypeVariables32 : implicitLowerBounds) {
                substituteTypeVariables = substituteTypeVariables(substituteTypeVariables32, map);
                for (Type substituteTypeVariables22 : implicitLowerBounds2) {
                    if (!isAssignable(substituteTypeVariables, substituteTypeVariables22, (Map) map)) {
                        return false;
                    }
                }
            }
            return true;
        }
        for (Type substituteTypeVariables4 : implicitUpperBounds) {
            if (!isAssignable(type, substituteTypeVariables(substituteTypeVariables4, map), (Map) map)) {
                return false;
            }
        }
        for (Type substituteTypeVariables5 : implicitLowerBounds) {
            if (!isAssignable(substituteTypeVariables(substituteTypeVariables5, map), type, (Map) map)) {
                return false;
            }
        }
        return true;
    }

    private static boolean isAssignable(Type type, TypeVariable<?> typeVariable, Map<TypeVariable<?>, Type> map) {
        if (type == null) {
            return true;
        }
        if (typeVariable == null) {
            return false;
        }
        if (typeVariable.equals(type)) {
            return true;
        }
        if (type instanceof TypeVariable) {
            for (Type isAssignable : getImplicitBounds((TypeVariable) type)) {
                if (isAssignable(isAssignable, (TypeVariable) typeVariable, (Map) map)) {
                    return true;
                }
            }
        }
        if ((type instanceof Class) || (type instanceof ParameterizedType) || (type instanceof GenericArrayType) || (type instanceof WildcardType)) {
            return false;
        }
        throw new IllegalStateException("found an unhandled type: " + type);
    }

    private static Type substituteTypeVariables(Type type, Map<TypeVariable<?>, Type> map) {
        if (!(type instanceof TypeVariable) || map == null) {
            return type;
        }
        Type type2 = (Type) map.get(type);
        if (type2 != null) {
            return type2;
        }
        throw new IllegalArgumentException("missing assignment type for type variable " + type);
    }

    public static Map<TypeVariable<?>, Type> getTypeArguments(ParameterizedType parameterizedType) {
        return getTypeArguments(parameterizedType, getRawType(parameterizedType), null);
    }

    public static Map<TypeVariable<?>, Type> getTypeArguments(Type type, Class<?> cls) {
        return getTypeArguments(type, (Class) cls, null);
    }

    private static Map<TypeVariable<?>, Type> getTypeArguments(Type type, Class<?> cls, Map<TypeVariable<?>, Type> map) {
        int i = 0;
        if (type instanceof Class) {
            return getTypeArguments((Class) type, (Class) cls, (Map) map);
        }
        if (type instanceof ParameterizedType) {
            return getTypeArguments((ParameterizedType) type, (Class) cls, (Map) map);
        }
        if (type instanceof GenericArrayType) {
            Class componentType;
            Type genericComponentType = ((GenericArrayType) type).getGenericComponentType();
            if (cls.isArray()) {
                componentType = cls.getComponentType();
            }
            return getTypeArguments(genericComponentType, componentType, (Map) map);
        } else if (type instanceof WildcardType) {
            r2 = getImplicitUpperBounds((WildcardType) type);
            r3 = r2.length;
            while (i < r3) {
                r4 = r2[i];
                if (isAssignable(r4, (Class) cls)) {
                    return getTypeArguments(r4, (Class) cls, (Map) map);
                }
                i++;
            }
            return null;
        } else if (type instanceof TypeVariable) {
            r2 = getImplicitBounds((TypeVariable) type);
            r3 = r2.length;
            while (i < r3) {
                r4 = r2[i];
                if (isAssignable(r4, (Class) cls)) {
                    return getTypeArguments(r4, (Class) cls, (Map) map);
                }
                i++;
            }
            return null;
        } else {
            throw new IllegalStateException("found an unhandled type: " + type);
        }
    }

    private static Map<TypeVariable<?>, Type> getTypeArguments(ParameterizedType parameterizedType, Class<?> cls, Map<TypeVariable<?>, Type> map) {
        Type rawType = getRawType(parameterizedType);
        if (!isAssignable(rawType, (Class) cls)) {
            return null;
        }
        Map typeArguments;
        Type ownerType = parameterizedType.getOwnerType();
        if (ownerType instanceof ParameterizedType) {
            ParameterizedType parameterizedType2 = (ParameterizedType) ownerType;
            typeArguments = getTypeArguments(parameterizedType2, getRawType(parameterizedType2), (Map) map);
        } else {
            Object hashMap = map == null ? new HashMap() : new HashMap(map);
        }
        Type[] actualTypeArguments = parameterizedType.getActualTypeArguments();
        TypeVariable[] typeParameters = rawType.getTypeParameters();
        for (int i = 0; i < typeParameters.length; i++) {
            Object obj = actualTypeArguments[i];
            Object obj2 = typeParameters[i];
            if (typeArguments.containsKey(obj)) {
                obj = (Type) typeArguments.get(obj);
            }
            typeArguments.put(obj2, obj);
        }
        return !cls.equals(rawType) ? getTypeArguments(getClosestParentType(rawType, cls), (Class) cls, typeArguments) : typeArguments;
    }

    private static Map<TypeVariable<?>, Type> getTypeArguments(Class<?> cls, Class<?> cls2, Map<TypeVariable<?>, Type> map) {
        if (!isAssignable((Type) cls, (Class) cls2)) {
            return null;
        }
        if (cls.isPrimitive()) {
            if (cls2.isPrimitive()) {
                return new HashMap();
            }
            cls = ClassUtils.primitiveToWrapper(cls);
        }
        Map hashMap = map == null ? new HashMap() : new HashMap(map);
        if (cls2.equals(cls)) {
            return hashMap;
        }
        return getTypeArguments(getClosestParentType(cls, cls2), (Class) cls2, hashMap);
    }

    public static Map<TypeVariable<?>, Type> determineTypeArguments(Class<?> cls, ParameterizedType parameterizedType) {
        Validate.notNull(cls, "cls is null", new Object[0]);
        Validate.notNull(parameterizedType, "superType is null", new Object[0]);
        Class rawType = getRawType(parameterizedType);
        if (!isAssignable((Type) cls, rawType)) {
            return null;
        }
        if (cls.equals(rawType)) {
            return getTypeArguments(parameterizedType, rawType, null);
        }
        Type closestParentType = getClosestParentType(cls, rawType);
        if (closestParentType instanceof Class) {
            return determineTypeArguments((Class) closestParentType, parameterizedType);
        }
        ParameterizedType parameterizedType2 = (ParameterizedType) closestParentType;
        Map<TypeVariable<?>, Type> determineTypeArguments = determineTypeArguments(getRawType(parameterizedType2), parameterizedType);
        mapTypeVariablesToArguments(cls, parameterizedType2, determineTypeArguments);
        return determineTypeArguments;
    }

    private static <T> void mapTypeVariablesToArguments(Class<T> cls, ParameterizedType parameterizedType, Map<TypeVariable<?>, Type> map) {
        Type ownerType = parameterizedType.getOwnerType();
        if (ownerType instanceof ParameterizedType) {
            mapTypeVariablesToArguments(cls, (ParameterizedType) ownerType, map);
        }
        Type[] actualTypeArguments = parameterizedType.getActualTypeArguments();
        TypeVariable[] typeParameters = getRawType(parameterizedType).getTypeParameters();
        List asList = Arrays.asList(cls.getTypeParameters());
        for (int i = 0; i < actualTypeArguments.length; i++) {
            Object obj = typeParameters[i];
            Object obj2 = actualTypeArguments[i];
            if (asList.contains(obj2) && map.containsKey(obj)) {
                map.put((TypeVariable) obj2, map.get(obj));
            }
        }
    }

    private static Type getClosestParentType(Class<?> cls, Class<?> cls2) {
        if (cls2.isInterface()) {
            Type[] genericInterfaces = cls.getGenericInterfaces();
            Type type = null;
            int length = genericInterfaces.length;
            int i = 0;
            while (i < length) {
                Type rawType;
                Type type2 = genericInterfaces[i];
                if (type2 instanceof ParameterizedType) {
                    rawType = getRawType((ParameterizedType) type2);
                } else if (type2 instanceof Class) {
                    Class cls3 = (Class) type2;
                } else {
                    throw new IllegalStateException("Unexpected generic interface type found: " + type2);
                }
                if (!isAssignable(rawType, (Class) cls2) || !isAssignable(type, rawType)) {
                    type2 = type;
                }
                i++;
                type = type2;
            }
            if (type != null) {
                return type;
            }
        }
        return cls.getGenericSuperclass();
    }

    public static boolean isInstance(Object obj, Type type) {
        if (type == null) {
            return false;
        }
        if (obj != null) {
            return isAssignable(obj.getClass(), type, null);
        }
        if ((type instanceof Class) && ((Class) type).isPrimitive()) {
            return false;
        }
        return true;
    }

    public static Type[] normalizeUpperBounds(Type[] typeArr) {
        Validate.notNull(typeArr, "null value specified for bounds array", new Object[0]);
        if (typeArr.length < 2) {
            return typeArr;
        }
        Set hashSet = new HashSet(typeArr.length);
        for (Type type : typeArr) {
            int i;
            for (Type type2 : typeArr) {
                if (type != type2 && isAssignable(type2, type, null)) {
                    i = 1;
                    break;
                }
            }
            i = 0;
            if (i == 0) {
                hashSet.add(type);
            }
        }
        return (Type[]) hashSet.toArray(new Type[hashSet.size()]);
    }

    public static Type[] getImplicitBounds(TypeVariable<?> typeVariable) {
        Validate.notNull(typeVariable, "typeVariable is null", new Object[0]);
        Type[] bounds = typeVariable.getBounds();
        if (bounds.length != 0) {
            return normalizeUpperBounds(bounds);
        }
        return new Type[]{Object.class};
    }

    public static Type[] getImplicitUpperBounds(WildcardType wildcardType) {
        Validate.notNull(wildcardType, "wildcardType is null", new Object[0]);
        Type[] upperBounds = wildcardType.getUpperBounds();
        if (upperBounds.length != 0) {
            return normalizeUpperBounds(upperBounds);
        }
        return new Type[]{Object.class};
    }

    public static Type[] getImplicitLowerBounds(WildcardType wildcardType) {
        Validate.notNull(wildcardType, "wildcardType is null", new Object[0]);
        Type[] lowerBounds = wildcardType.getLowerBounds();
        if (lowerBounds.length != 0) {
            return lowerBounds;
        }
        return new Type[]{null};
    }

    public static boolean typesSatisfyVariables(Map<TypeVariable<?>, Type> map) {
        Validate.notNull(map, "typeVarAssigns is null", new Object[0]);
        for (Entry entry : map.entrySet()) {
            TypeVariable typeVariable = (TypeVariable) entry.getKey();
            Type type = (Type) entry.getValue();
            for (Type substituteTypeVariables : getImplicitBounds(typeVariable)) {
                if (!isAssignable(type, substituteTypeVariables(substituteTypeVariables, map), (Map) map)) {
                    return false;
                }
            }
        }
        return true;
    }

    private static Class<?> getRawType(ParameterizedType parameterizedType) {
        Type rawType = parameterizedType.getRawType();
        if (rawType instanceof Class) {
            return (Class) rawType;
        }
        throw new IllegalStateException("Wait... What!? Type of rawType: " + rawType);
    }

    public static Class<?> getRawType(Type type, Type type2) {
        if (type instanceof Class) {
            return (Class) type;
        }
        if (type instanceof ParameterizedType) {
            return getRawType((ParameterizedType) type);
        }
        if (type instanceof TypeVariable) {
            if (type2 == null) {
                return null;
            }
            GenericDeclaration genericDeclaration = ((TypeVariable) type).getGenericDeclaration();
            if (!(genericDeclaration instanceof Class)) {
                return null;
            }
            Map typeArguments = getTypeArguments(type2, (Class) genericDeclaration);
            if (typeArguments == null) {
                return null;
            }
            Type type3 = (Type) typeArguments.get(type);
            if (type3 == null) {
                return null;
            }
            return getRawType(type3, type2);
        } else if (type instanceof GenericArrayType) {
            return Array.newInstance(getRawType(((GenericArrayType) type).getGenericComponentType(), type2), 0).getClass();
        } else {
            if (type instanceof WildcardType) {
                return null;
            }
            throw new IllegalArgumentException("unknown type: " + type);
        }
    }

    public static boolean isArrayType(Type type) {
        return (type instanceof GenericArrayType) || ((type instanceof Class) && ((Class) type).isArray());
    }

    public static Type getArrayComponentType(Type type) {
        if (type instanceof Class) {
            Class cls = (Class) type;
            if (cls.isArray()) {
                return cls.getComponentType();
            }
            return null;
        } else if (type instanceof GenericArrayType) {
            return ((GenericArrayType) type).getGenericComponentType();
        } else {
            return null;
        }
    }

    public static Type unrollVariables(Map<TypeVariable<?>, Type> map, Type type) {
        if (map == null) {
            map = Collections.emptyMap();
        }
        if (!containsTypeVariables(type)) {
            return type;
        }
        if (type instanceof TypeVariable) {
            return unrollVariables(map, (Type) map.get(type));
        }
        if (type instanceof ParameterizedType) {
            ParameterizedType parameterizedType = (ParameterizedType) type;
            if (parameterizedType.getOwnerType() != null) {
                Map<TypeVariable<?>, Type> hashMap = new HashMap(map);
                hashMap.putAll(getTypeArguments(parameterizedType));
                map = hashMap;
            }
            Type[] actualTypeArguments = parameterizedType.getActualTypeArguments();
            for (int i = 0; i < actualTypeArguments.length; i++) {
                Type unrollVariables = unrollVariables(map, actualTypeArguments[i]);
                if (unrollVariables != null) {
                    actualTypeArguments[i] = unrollVariables;
                }
            }
            return parameterizeWithOwner(parameterizedType.getOwnerType(), (Class) parameterizedType.getRawType(), actualTypeArguments);
        } else if (!(type instanceof WildcardType)) {
            return type;
        } else {
            WildcardType wildcardType = (WildcardType) type;
            return wildcardType().withUpperBounds(unrollBounds(map, wildcardType.getUpperBounds())).withLowerBounds(unrollBounds(map, wildcardType.getLowerBounds())).build();
        }
    }

    private static Type[] unrollBounds(Map<TypeVariable<?>, Type> map, Type[] typeArr) {
        int i = 0;
        while (i < typeArr.length) {
            Type unrollVariables = unrollVariables(map, typeArr[i]);
            if (unrollVariables == null) {
                typeArr = (Type[]) ArrayUtils.remove((Object[]) typeArr, i);
                i--;
            } else {
                typeArr[i] = unrollVariables;
            }
            i++;
        }
        return typeArr;
    }

    public static boolean containsTypeVariables(Type type) {
        boolean z = false;
        if (type instanceof TypeVariable) {
            return true;
        }
        if (type instanceof Class) {
            if (((Class) type).getTypeParameters().length <= 0) {
                return false;
            }
            return true;
        } else if (type instanceof ParameterizedType) {
            for (Type containsTypeVariables : ((ParameterizedType) type).getActualTypeArguments()) {
                if (containsTypeVariables(containsTypeVariables)) {
                    return true;
                }
            }
            return false;
        } else if (!(type instanceof WildcardType)) {
            return false;
        } else {
            WildcardType wildcardType = (WildcardType) type;
            if (containsTypeVariables(getImplicitLowerBounds(wildcardType)[0]) || containsTypeVariables(getImplicitUpperBounds(wildcardType)[0])) {
                z = true;
            }
            return z;
        }
    }

    public static final ParameterizedType parameterize(Class<?> cls, Type... typeArr) {
        return parameterizeWithOwner(null, (Class) cls, typeArr);
    }

    public static final ParameterizedType parameterize(Class<?> cls, Map<TypeVariable<?>, Type> map) {
        Validate.notNull(cls, "raw class is null", new Object[0]);
        Validate.notNull(map, "typeArgMappings is null", new Object[0]);
        return parameterizeWithOwner(null, (Class) cls, extractTypeArgumentsFrom(map, cls.getTypeParameters()));
    }

    public static final ParameterizedType parameterizeWithOwner(Type type, Class<?> cls, Type... typeArr) {
        boolean z;
        Validate.notNull(cls, "raw class is null", new Object[0]);
        if (cls.getEnclosingClass() == null) {
            Validate.isTrue(type == null, "no owner allowed for top-level %s", cls);
            type = null;
        } else if (type == null) {
            type = cls.getEnclosingClass();
        } else {
            Validate.isTrue(isAssignable(type, cls.getEnclosingClass()), "%s is invalid owner type for parameterized %s", type, cls);
        }
        Validate.noNullElements((Object[]) typeArr, "null type argument at index %s", new Object[0]);
        if (cls.getTypeParameters().length == typeArr.length) {
            z = true;
        } else {
            z = false;
        }
        Validate.isTrue(z, "invalid number of type parameters specified: expected %s, got %s", Integer.valueOf(cls.getTypeParameters().length), Integer.valueOf(typeArr.length));
        return new ParameterizedTypeImpl(cls, type, typeArr);
    }

    public static final ParameterizedType parameterizeWithOwner(Type type, Class<?> cls, Map<TypeVariable<?>, Type> map) {
        Validate.notNull(cls, "raw class is null", new Object[0]);
        Validate.notNull(map, "typeArgMappings is null", new Object[0]);
        return parameterizeWithOwner(type, (Class) cls, extractTypeArgumentsFrom(map, cls.getTypeParameters()));
    }

    private static Type[] extractTypeArgumentsFrom(Map<TypeVariable<?>, Type> map, TypeVariable<?>[] typeVariableArr) {
        Type[] typeArr = new Type[typeVariableArr.length];
        int length = typeVariableArr.length;
        int i = 0;
        int i2 = 0;
        while (i < length) {
            Type type = typeVariableArr[i];
            Validate.isTrue(map.containsKey(type), "missing argument mapping for %s", toString(type));
            int i3 = i2 + 1;
            typeArr[i2] = (Type) map.get(type);
            i++;
            i2 = i3;
        }
        return typeArr;
    }

    public static WildcardTypeBuilder wildcardType() {
        return new WildcardTypeBuilder();
    }

    public static GenericArrayType genericArrayType(Type type) {
        return new GenericArrayTypeImpl((Type) Validate.notNull(type, "componentType is null", new Object[0]));
    }

    public static boolean equals(Type type, Type type2) {
        if (ObjectUtils.equals(type, type2)) {
            return true;
        }
        if (type instanceof ParameterizedType) {
            return equals((ParameterizedType) type, type2);
        }
        if (type instanceof GenericArrayType) {
            return equals((GenericArrayType) type, type2);
        }
        if (type instanceof WildcardType) {
            return equals((WildcardType) type, type2);
        }
        return false;
    }

    private static boolean equals(ParameterizedType parameterizedType, Type type) {
        if (type instanceof ParameterizedType) {
            ParameterizedType parameterizedType2 = (ParameterizedType) type;
            if (equals(parameterizedType.getRawType(), parameterizedType2.getRawType()) && equals(parameterizedType.getOwnerType(), parameterizedType2.getOwnerType())) {
                return equals(parameterizedType.getActualTypeArguments(), parameterizedType2.getActualTypeArguments());
            }
        }
        return false;
    }

    private static boolean equals(GenericArrayType genericArrayType, Type type) {
        return (type instanceof GenericArrayType) && equals(genericArrayType.getGenericComponentType(), ((GenericArrayType) type).getGenericComponentType());
    }

    private static boolean equals(WildcardType wildcardType, Type type) {
        if (!(type instanceof WildcardType)) {
            return true;
        }
        WildcardType wildcardType2 = (WildcardType) type;
        if (equals(getImplicitLowerBounds(wildcardType), getImplicitLowerBounds(wildcardType2)) && equals(getImplicitUpperBounds(wildcardType), getImplicitUpperBounds(wildcardType2))) {
            return true;
        }
        return false;
    }

    private static boolean equals(Type[] typeArr, Type[] typeArr2) {
        if (typeArr.length != typeArr2.length) {
            return false;
        }
        for (int i = 0; i < typeArr.length; i++) {
            if (!equals(typeArr[i], typeArr2[i])) {
                return false;
            }
        }
        return true;
    }

    public static String toString(Type type) {
        Validate.notNull(type);
        if (type instanceof Class) {
            return classToString((Class) type);
        }
        if (type instanceof ParameterizedType) {
            return parameterizedTypeToString((ParameterizedType) type);
        }
        if (type instanceof WildcardType) {
            return wildcardTypeToString((WildcardType) type);
        }
        if (type instanceof TypeVariable) {
            return typeVariableToString((TypeVariable) type);
        }
        if (type instanceof GenericArrayType) {
            return genericArrayTypeToString((GenericArrayType) type);
        }
        throw new IllegalArgumentException(ObjectUtils.identityToString(type));
    }

    public static String toLongString(TypeVariable<?> typeVariable) {
        Validate.notNull(typeVariable, "var is null", new Object[0]);
        StringBuilder stringBuilder = new StringBuilder();
        GenericDeclaration genericDeclaration = typeVariable.getGenericDeclaration();
        if (genericDeclaration instanceof Class) {
            Class cls = (Class) genericDeclaration;
            while (cls.getEnclosingClass() != null) {
                stringBuilder.insert(0, cls.getSimpleName()).insert(0, ClassUtils.PACKAGE_SEPARATOR_CHAR);
                cls = cls.getEnclosingClass();
            }
            stringBuilder.insert(0, cls.getName());
        } else if (genericDeclaration instanceof Type) {
            stringBuilder.append(toString((Type) genericDeclaration));
        } else {
            stringBuilder.append(genericDeclaration);
        }
        return stringBuilder.append(':').append(typeVariableToString(typeVariable)).toString();
    }

    public static <T> Typed<T> wrap(final Type type) {
        return new Typed<T>() {
            public Type getType() {
                return type;
            }
        };
    }

    public static <T> Typed<T> wrap(Class<T> cls) {
        return wrap((Type) cls);
    }

    private static String classToString(Class<?> cls) {
        StringBuilder stringBuilder = new StringBuilder();
        if (cls.getEnclosingClass() != null) {
            stringBuilder.append(classToString(cls.getEnclosingClass())).append(ClassUtils.PACKAGE_SEPARATOR_CHAR).append(cls.getSimpleName());
        } else {
            stringBuilder.append(cls.getName());
        }
        if (cls.getTypeParameters().length > 0) {
            stringBuilder.append('<');
            appendAllTo(stringBuilder, ", ", cls.getTypeParameters());
            stringBuilder.append('>');
        }
        return stringBuilder.toString();
    }

    private static String typeVariableToString(TypeVariable<?> typeVariable) {
        StringBuilder stringBuilder = new StringBuilder(typeVariable.getName());
        Type[] bounds = typeVariable.getBounds();
        if (bounds.length > 0 && !(bounds.length == 1 && Object.class.equals(bounds[0]))) {
            stringBuilder.append(" extends ");
            appendAllTo(stringBuilder, " & ", typeVariable.getBounds());
        }
        return stringBuilder.toString();
    }

    private static String parameterizedTypeToString(ParameterizedType parameterizedType) {
        StringBuilder stringBuilder = new StringBuilder();
        Type ownerType = parameterizedType.getOwnerType();
        Class cls = (Class) parameterizedType.getRawType();
        Type[] actualTypeArguments = parameterizedType.getActualTypeArguments();
        if (ownerType == null) {
            stringBuilder.append(cls.getName());
        } else {
            if (ownerType instanceof Class) {
                stringBuilder.append(((Class) ownerType).getName());
            } else {
                stringBuilder.append(ownerType.toString());
            }
            stringBuilder.append(ClassUtils.PACKAGE_SEPARATOR_CHAR).append(cls.getSimpleName());
        }
        appendAllTo(stringBuilder.append('<'), ", ", actualTypeArguments).append('>');
        return stringBuilder.toString();
    }

    private static String wildcardTypeToString(WildcardType wildcardType) {
        StringBuilder append = new StringBuilder().append('?');
        Type[] lowerBounds = wildcardType.getLowerBounds();
        Type[] upperBounds = wildcardType.getUpperBounds();
        if (lowerBounds.length > 1 || (lowerBounds.length == 1 && lowerBounds[0] != null)) {
            appendAllTo(append.append(" super "), " & ", lowerBounds);
        } else if (upperBounds.length > 1 || (upperBounds.length == 1 && !Object.class.equals(upperBounds[0]))) {
            appendAllTo(append.append(" extends "), " & ", upperBounds);
        }
        return append.toString();
    }

    private static String genericArrayTypeToString(GenericArrayType genericArrayType) {
        return String.format("%s[]", new Object[]{toString(genericArrayType.getGenericComponentType())});
    }

    private static StringBuilder appendAllTo(StringBuilder stringBuilder, String str, Type... typeArr) {
        Validate.notEmpty(Validate.noNullElements((Object[]) typeArr));
        if (typeArr.length > 0) {
            stringBuilder.append(toString(typeArr[0]));
            for (int i = 1; i < typeArr.length; i++) {
                stringBuilder.append(str).append(toString(typeArr[i]));
            }
        }
        return stringBuilder;
    }
}
