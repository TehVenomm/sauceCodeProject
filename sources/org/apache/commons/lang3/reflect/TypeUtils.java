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
            return isAssignable(type, (ParameterizedType) type2, map);
        }
        if (type2 instanceof GenericArrayType) {
            return isAssignable(type, (GenericArrayType) type2, map);
        }
        if (type2 instanceof WildcardType) {
            return isAssignable(type, (WildcardType) type2, map);
        }
        if (type2 instanceof TypeVariable) {
            return isAssignable(type, (TypeVariable) type2, map);
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
                return ClassUtils.isAssignable((Class) type, cls);
            }
            if (type instanceof ParameterizedType) {
                return isAssignable((Type) getRawType((ParameterizedType) type), cls);
            }
            if (type instanceof TypeVariable) {
                for (Type isAssignable : ((TypeVariable) type).getBounds()) {
                    if (isAssignable(isAssignable, cls)) {
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
        Map typeArguments2 = getTypeArguments(parameterizedType, rawType, map);
        for (TypeVariable typeVariable : typeArguments2.keySet()) {
            Type unrollVariableAssignments = unrollVariableAssignments(typeVariable, typeArguments2);
            Type unrollVariableAssignments2 = unrollVariableAssignments(typeVariable, typeArguments);
            if (unrollVariableAssignments2 != null && !unrollVariableAssignments.equals(unrollVariableAssignments2)) {
                if (!(unrollVariableAssignments instanceof WildcardType) || !isAssignable(unrollVariableAssignments2, unrollVariableAssignments, map)) {
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
            if (!cls.isArray() || !isAssignable((Type) cls.getComponentType(), genericComponentType, map)) {
                return false;
            }
            return true;
        } else if (type instanceof GenericArrayType) {
            return isAssignable(((GenericArrayType) type).getGenericComponentType(), genericComponentType, map);
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
        if (type instanceof WildcardType) {
            WildcardType wildcardType2 = (WildcardType) type;
            Type[] implicitUpperBounds2 = getImplicitUpperBounds(wildcardType2);
            Type[] implicitLowerBounds2 = getImplicitLowerBounds(wildcardType2);
            for (Type substituteTypeVariables : implicitUpperBounds) {
                Type substituteTypeVariables2 = substituteTypeVariables(substituteTypeVariables, map);
                for (Type isAssignable : implicitUpperBounds2) {
                    if (!isAssignable(isAssignable, substituteTypeVariables2, map)) {
                        return false;
                    }
                }
            }
            for (Type substituteTypeVariables3 : implicitLowerBounds) {
                Type substituteTypeVariables4 = substituteTypeVariables(substituteTypeVariables3, map);
                for (Type isAssignable2 : implicitLowerBounds2) {
                    if (!isAssignable(substituteTypeVariables4, isAssignable2, map)) {
                        return false;
                    }
                }
            }
            return true;
        }
        for (Type substituteTypeVariables5 : implicitUpperBounds) {
            if (!isAssignable(type, substituteTypeVariables(substituteTypeVariables5, map), map)) {
                return false;
            }
        }
        for (Type substituteTypeVariables6 : implicitLowerBounds) {
            if (!isAssignable(substituteTypeVariables(substituteTypeVariables6, map), type, map)) {
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
                if (isAssignable(isAssignable, typeVariable, map)) {
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
        return getTypeArguments(type, cls, null);
    }

    private static Map<TypeVariable<?>, Type> getTypeArguments(Type type, Class<?> cls, Map<TypeVariable<?>, Type> map) {
        int i = 0;
        if (type instanceof Class) {
            return getTypeArguments((Class) type, cls, map);
        }
        if (type instanceof ParameterizedType) {
            return getTypeArguments((ParameterizedType) type, cls, map);
        }
        if (type instanceof GenericArrayType) {
            Type genericComponentType = ((GenericArrayType) type).getGenericComponentType();
            if (cls.isArray()) {
                cls = cls.getComponentType();
            }
            return getTypeArguments(genericComponentType, cls, map);
        } else if (type instanceof WildcardType) {
            Type[] implicitUpperBounds = getImplicitUpperBounds((WildcardType) type);
            int length = implicitUpperBounds.length;
            while (i < length) {
                Type type2 = implicitUpperBounds[i];
                if (isAssignable(type2, cls)) {
                    return getTypeArguments(type2, cls, map);
                }
                i++;
            }
            return null;
        } else if (type instanceof TypeVariable) {
            Type[] implicitBounds = getImplicitBounds((TypeVariable) type);
            int length2 = implicitBounds.length;
            while (i < length2) {
                Type type3 = implicitBounds[i];
                if (isAssignable(type3, cls)) {
                    return getTypeArguments(type3, cls, map);
                }
                i++;
            }
            return null;
        } else {
            throw new IllegalStateException("found an unhandled type: " + type);
        }
    }

    private static Map<TypeVariable<?>, Type> getTypeArguments(ParameterizedType parameterizedType, Class<?> cls, Map<TypeVariable<?>, Type> map) {
        Map<TypeVariable<?>, Type> hashMap;
        Class rawType = getRawType(parameterizedType);
        if (!isAssignable((Type) rawType, cls)) {
            return null;
        }
        Type ownerType = parameterizedType.getOwnerType();
        if (ownerType instanceof ParameterizedType) {
            ParameterizedType parameterizedType2 = (ParameterizedType) ownerType;
            hashMap = getTypeArguments(parameterizedType2, getRawType(parameterizedType2), map);
        } else {
            hashMap = map == null ? new HashMap() : new HashMap(map);
        }
        Type[] actualTypeArguments = parameterizedType.getActualTypeArguments();
        TypeVariable[] typeParameters = rawType.getTypeParameters();
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 >= typeParameters.length) {
                break;
            }
            Type type = actualTypeArguments[i2];
            TypeVariable typeVariable = typeParameters[i2];
            if (hashMap.containsKey(type)) {
                type = (Type) hashMap.get(type);
            }
            hashMap.put(typeVariable, type);
            i = i2 + 1;
        }
        return !cls.equals(rawType) ? getTypeArguments(getClosestParentType(rawType, cls), cls, hashMap) : hashMap;
    }

    private static Map<TypeVariable<?>, Type> getTypeArguments(Class<?> cls, Class<?> cls2, Map<TypeVariable<?>, Type> map) {
        if (!isAssignable((Type) cls, cls2)) {
            return null;
        }
        if (cls.isPrimitive()) {
            if (cls2.isPrimitive()) {
                return new HashMap();
            }
            cls = ClassUtils.primitiveToWrapper(cls);
        }
        HashMap hashMap = map == null ? new HashMap() : new HashMap(map);
        if (!cls2.equals(cls)) {
            return getTypeArguments(getClosestParentType(cls, cls2), cls2, (Map<TypeVariable<?>, Type>) hashMap);
        }
        return hashMap;
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
        Map determineTypeArguments = determineTypeArguments(getRawType(parameterizedType2), parameterizedType);
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
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 < actualTypeArguments.length) {
                TypeVariable typeVariable = typeParameters[i2];
                Type type = actualTypeArguments[i2];
                if (asList.contains(type) && map.containsKey(typeVariable)) {
                    map.put((TypeVariable) type, map.get(typeVariable));
                }
                i = i2 + 1;
            } else {
                return;
            }
        }
    }

    private static Type getClosestParentType(Class<?> cls, Class<?> cls2) {
        Class cls3;
        if (cls2.isInterface()) {
            Type[] genericInterfaces = cls.getGenericInterfaces();
            Type type = null;
            int length = genericInterfaces.length;
            int i = 0;
            while (i < length) {
                Type type2 = genericInterfaces[i];
                if (type2 instanceof ParameterizedType) {
                    cls3 = getRawType((ParameterizedType) type2);
                } else if (type2 instanceof Class) {
                    cls3 = (Class) type2;
                } else {
                    throw new IllegalStateException("Unexpected generic interface type found: " + type2);
                }
                if (!isAssignable((Type) cls3, cls2) || !isAssignable(type, (Type) cls3)) {
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
            return isAssignable((Type) obj.getClass(), type, null);
        }
        if (!(type instanceof Class) || !((Class) type).isPrimitive()) {
            return true;
        }
        return false;
    }

    public static Type[] normalizeUpperBounds(Type[] typeArr) {
        boolean z;
        Validate.notNull(typeArr, "null value specified for bounds array", new Object[0]);
        if (typeArr.length < 2) {
            return typeArr;
        }
        HashSet hashSet = new HashSet(typeArr.length);
        for (Type type : typeArr) {
            int length = typeArr.length;
            int i = 0;
            while (true) {
                if (i >= length) {
                    z = false;
                    break;
                }
                Type type2 = typeArr[i];
                if (type != type2 && isAssignable(type2, type, null)) {
                    z = true;
                    break;
                }
                i++;
            }
            if (!z) {
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
            Type[] implicitBounds = getImplicitBounds(typeVariable);
            int length = implicitBounds.length;
            int i = 0;
            while (true) {
                if (i < length) {
                    if (!isAssignable(type, substituteTypeVariables(implicitBounds[i], map), map)) {
                        return false;
                    }
                    i++;
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
                HashMap hashMap = new HashMap(map);
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
        int i;
        int i2 = 0;
        while (i2 < typeArr.length) {
            Type unrollVariables = unrollVariables(map, typeArr[i2]);
            if (unrollVariables == null) {
                i = i2 - 1;
                typeArr = (Type[]) ArrayUtils.remove((T[]) typeArr, i2);
            } else {
                typeArr[i2] = unrollVariables;
                i = i2;
            }
            i2 = i + 1;
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
        return parameterizeWithOwner((Type) null, cls, typeArr);
    }

    public static final ParameterizedType parameterize(Class<?> cls, Map<TypeVariable<?>, Type> map) {
        Validate.notNull(cls, "raw class is null", new Object[0]);
        Validate.notNull(map, "typeArgMappings is null", new Object[0]);
        return parameterizeWithOwner((Type) null, cls, extractTypeArgumentsFrom(map, cls.getTypeParameters()));
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
        Validate.noNullElements((T[]) typeArr, "null type argument at index %s", new Object[0]);
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
        return parameterizeWithOwner(type, cls, extractTypeArgumentsFrom(map, cls.getTypeParameters()));
    }

    private static Type[] extractTypeArgumentsFrom(Map<TypeVariable<?>, Type> map, TypeVariable<?>[] typeVariableArr) {
        Type[] typeArr = new Type[typeVariableArr.length];
        int length = typeVariableArr.length;
        int i = 0;
        int i2 = 0;
        while (i < length) {
            TypeVariable<?> typeVariable = typeVariableArr[i];
            Validate.isTrue(map.containsKey(typeVariable), "missing argument mapping for %s", toString(typeVariable));
            int i3 = i2 + 1;
            typeArr[i2] = (Type) map.get(typeVariable);
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

    /* access modifiers changed from: private */
    public static boolean equals(ParameterizedType parameterizedType, Type type) {
        if (type instanceof ParameterizedType) {
            ParameterizedType parameterizedType2 = (ParameterizedType) type;
            if (equals(parameterizedType.getRawType(), parameterizedType2.getRawType()) && equals(parameterizedType.getOwnerType(), parameterizedType2.getOwnerType())) {
                return equals(parameterizedType.getActualTypeArguments(), parameterizedType2.getActualTypeArguments());
            }
        }
        return false;
    }

    /* access modifiers changed from: private */
    public static boolean equals(GenericArrayType genericArrayType, Type type) {
        return (type instanceof GenericArrayType) && equals(genericArrayType.getGenericComponentType(), ((GenericArrayType) type).getGenericComponentType());
    }

    /* access modifiers changed from: private */
    public static boolean equals(WildcardType wildcardType, Type type) {
        if (!(type instanceof WildcardType)) {
            return true;
        }
        WildcardType wildcardType2 = (WildcardType) type;
        if (!equals(getImplicitLowerBounds(wildcardType), getImplicitLowerBounds(wildcardType2)) || !equals(getImplicitUpperBounds(wildcardType), getImplicitUpperBounds(wildcardType2))) {
            return false;
        }
        return true;
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
        StringBuilder sb = new StringBuilder();
        GenericDeclaration genericDeclaration = typeVariable.getGenericDeclaration();
        if (genericDeclaration instanceof Class) {
            Class cls = (Class) genericDeclaration;
            while (cls.getEnclosingClass() != null) {
                sb.insert(0, cls.getSimpleName()).insert(0, ClassUtils.PACKAGE_SEPARATOR_CHAR);
                cls = cls.getEnclosingClass();
            }
            sb.insert(0, cls.getName());
        } else if (genericDeclaration instanceof Type) {
            sb.append(toString((Type) genericDeclaration));
        } else {
            sb.append(genericDeclaration);
        }
        return sb.append(':').append(typeVariableToString(typeVariable)).toString();
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
        StringBuilder sb = new StringBuilder();
        if (cls.getEnclosingClass() != null) {
            sb.append(classToString(cls.getEnclosingClass())).append(ClassUtils.PACKAGE_SEPARATOR_CHAR).append(cls.getSimpleName());
        } else {
            sb.append(cls.getName());
        }
        if (cls.getTypeParameters().length > 0) {
            sb.append('<');
            appendAllTo(sb, ", ", cls.getTypeParameters());
            sb.append('>');
        }
        return sb.toString();
    }

    private static String typeVariableToString(TypeVariable<?> typeVariable) {
        StringBuilder sb = new StringBuilder(typeVariable.getName());
        Type[] bounds = typeVariable.getBounds();
        if (bounds.length > 0 && (bounds.length != 1 || !Object.class.equals(bounds[0]))) {
            sb.append(" extends ");
            appendAllTo(sb, " & ", typeVariable.getBounds());
        }
        return sb.toString();
    }

    private static String parameterizedTypeToString(ParameterizedType parameterizedType) {
        StringBuilder sb = new StringBuilder();
        Type ownerType = parameterizedType.getOwnerType();
        Class cls = (Class) parameterizedType.getRawType();
        Type[] actualTypeArguments = parameterizedType.getActualTypeArguments();
        if (ownerType == null) {
            sb.append(cls.getName());
        } else {
            if (ownerType instanceof Class) {
                sb.append(((Class) ownerType).getName());
            } else {
                sb.append(ownerType.toString());
            }
            sb.append(ClassUtils.PACKAGE_SEPARATOR_CHAR).append(cls.getSimpleName());
        }
        appendAllTo(sb.append('<'), ", ", actualTypeArguments).append('>');
        return sb.toString();
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

    private static StringBuilder appendAllTo(StringBuilder sb, String str, Type... typeArr) {
        Validate.notEmpty((T[]) Validate.noNullElements((T[]) typeArr));
        if (typeArr.length > 0) {
            sb.append(toString(typeArr[0]));
            for (int i = 1; i < typeArr.length; i++) {
                sb.append(str).append(toString(typeArr[i]));
            }
        }
        return sb;
    }
}
