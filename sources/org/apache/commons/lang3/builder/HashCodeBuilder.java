package org.apache.commons.lang3.builder;

import java.lang.reflect.AccessibleObject;
import java.lang.reflect.Field;
import java.lang.reflect.Modifier;
import java.util.Collection;
import java.util.HashSet;
import java.util.Set;
import org.apache.commons.lang3.ArrayUtils;
import org.apache.commons.lang3.Validate;

public class HashCodeBuilder implements Builder<Integer> {
    private static final int DEFAULT_INITIAL_VALUE = 17;
    private static final int DEFAULT_MULTIPLIER_VALUE = 37;
    private static final ThreadLocal<Set<IDKey>> REGISTRY = new ThreadLocal<>();
    private final int iConstant;
    private int iTotal;

    static Set<IDKey> getRegistry() {
        return (Set) REGISTRY.get();
    }

    static boolean isRegistered(Object obj) {
        Set registry = getRegistry();
        return registry != null && registry.contains(new IDKey(obj));
    }

    private static void reflectionAppend(Object obj, Class<?> cls, HashCodeBuilder hashCodeBuilder, boolean z, String[] strArr) {
        if (!isRegistered(obj)) {
            try {
                register(obj);
                Field[] declaredFields = cls.getDeclaredFields();
                AccessibleObject.setAccessible(declaredFields, true);
                for (Field field : declaredFields) {
                    if (!ArrayUtils.contains((Object[]) strArr, (Object) field.getName()) && field.getName().indexOf(36) == -1 && ((z || !Modifier.isTransient(field.getModifiers())) && !Modifier.isStatic(field.getModifiers()))) {
                        hashCodeBuilder.append(field.get(obj));
                    }
                }
                unregister(obj);
            } catch (IllegalAccessException e) {
                throw new InternalError("Unexpected IllegalAccessException");
            } catch (Throwable th) {
                unregister(obj);
                throw th;
            }
        }
    }

    public static int reflectionHashCode(int i, int i2, Object obj) {
        return reflectionHashCode(i, i2, obj, false, null, new String[0]);
    }

    public static int reflectionHashCode(int i, int i2, Object obj, boolean z) {
        return reflectionHashCode(i, i2, obj, z, null, new String[0]);
    }

    public static <T> int reflectionHashCode(int i, int i2, T t, boolean z, Class<? super T> cls, String... strArr) {
        if (t == null) {
            throw new IllegalArgumentException("The object to build a hash code for must not be null");
        }
        HashCodeBuilder hashCodeBuilder = new HashCodeBuilder(i, i2);
        Class<? super T> cls2 = t.getClass();
        reflectionAppend(t, cls2, hashCodeBuilder, z, strArr);
        while (cls2.getSuperclass() != null && cls2 != cls) {
            cls2 = cls2.getSuperclass();
            reflectionAppend(t, cls2, hashCodeBuilder, z, strArr);
        }
        return hashCodeBuilder.toHashCode();
    }

    public static int reflectionHashCode(Object obj, boolean z) {
        return reflectionHashCode(17, 37, obj, z, null, new String[0]);
    }

    public static int reflectionHashCode(Object obj, Collection<String> collection) {
        return reflectionHashCode(obj, ReflectionToStringBuilder.toNoNullStringArray(collection));
    }

    public static int reflectionHashCode(Object obj, String... strArr) {
        return reflectionHashCode(17, 37, obj, false, null, strArr);
    }

    static void register(Object obj) {
        synchronized (HashCodeBuilder.class) {
            if (getRegistry() == null) {
                REGISTRY.set(new HashSet());
            }
        }
        getRegistry().add(new IDKey(obj));
    }

    static void unregister(Object obj) {
        Set registry = getRegistry();
        if (registry != null) {
            registry.remove(new IDKey(obj));
            synchronized (HashCodeBuilder.class) {
                Set registry2 = getRegistry();
                if (registry2 != null && registry2.isEmpty()) {
                    REGISTRY.remove();
                }
            }
        }
    }

    public HashCodeBuilder() {
        this.iTotal = 0;
        this.iConstant = 37;
        this.iTotal = 17;
    }

    public HashCodeBuilder(int i, int i2) {
        boolean z;
        boolean z2 = true;
        this.iTotal = 0;
        if (i % 2 != 0) {
            z = true;
        } else {
            z = false;
        }
        Validate.isTrue(z, "HashCodeBuilder requires an odd initial value", new Object[0]);
        if (i2 % 2 == 0) {
            z2 = false;
        }
        Validate.isTrue(z2, "HashCodeBuilder requires an odd multiplier", new Object[0]);
        this.iConstant = i2;
        this.iTotal = i;
    }

    public HashCodeBuilder append(boolean z) {
        this.iTotal = (z ? 0 : 1) + (this.iConstant * this.iTotal);
        return this;
    }

    public HashCodeBuilder append(boolean[] zArr) {
        if (zArr == null) {
            this.iTotal *= this.iConstant;
        } else {
            for (boolean append : zArr) {
                append(append);
            }
        }
        return this;
    }

    public HashCodeBuilder append(byte b) {
        this.iTotal = (this.iTotal * this.iConstant) + b;
        return this;
    }

    public HashCodeBuilder append(byte[] bArr) {
        if (bArr == null) {
            this.iTotal *= this.iConstant;
        } else {
            for (byte append : bArr) {
                append(append);
            }
        }
        return this;
    }

    public HashCodeBuilder append(char c) {
        this.iTotal = (this.iTotal * this.iConstant) + c;
        return this;
    }

    public HashCodeBuilder append(char[] cArr) {
        if (cArr == null) {
            this.iTotal *= this.iConstant;
        } else {
            for (char append : cArr) {
                append(append);
            }
        }
        return this;
    }

    public HashCodeBuilder append(double d) {
        return append(Double.doubleToLongBits(d));
    }

    public HashCodeBuilder append(double[] dArr) {
        if (dArr == null) {
            this.iTotal *= this.iConstant;
        } else {
            for (double append : dArr) {
                append(append);
            }
        }
        return this;
    }

    public HashCodeBuilder append(float f) {
        this.iTotal = (this.iTotal * this.iConstant) + Float.floatToIntBits(f);
        return this;
    }

    public HashCodeBuilder append(float[] fArr) {
        if (fArr == null) {
            this.iTotal *= this.iConstant;
        } else {
            for (float append : fArr) {
                append(append);
            }
        }
        return this;
    }

    public HashCodeBuilder append(int i) {
        this.iTotal = (this.iTotal * this.iConstant) + i;
        return this;
    }

    public HashCodeBuilder append(int[] iArr) {
        if (iArr == null) {
            this.iTotal *= this.iConstant;
        } else {
            for (int append : iArr) {
                append(append);
            }
        }
        return this;
    }

    public HashCodeBuilder append(long j) {
        this.iTotal = (this.iTotal * this.iConstant) + ((int) ((j >> 32) ^ j));
        return this;
    }

    public HashCodeBuilder append(long[] jArr) {
        if (jArr == null) {
            this.iTotal *= this.iConstant;
        } else {
            for (long append : jArr) {
                append(append);
            }
        }
        return this;
    }

    public HashCodeBuilder append(Object obj) {
        if (obj == null) {
            this.iTotal *= this.iConstant;
        } else if (!obj.getClass().isArray()) {
            this.iTotal = (this.iTotal * this.iConstant) + obj.hashCode();
        } else if (obj instanceof long[]) {
            append((long[]) obj);
        } else if (obj instanceof int[]) {
            append((int[]) obj);
        } else if (obj instanceof short[]) {
            append((short[]) obj);
        } else if (obj instanceof char[]) {
            append((char[]) obj);
        } else if (obj instanceof byte[]) {
            append((byte[]) obj);
        } else if (obj instanceof double[]) {
            append((double[]) obj);
        } else if (obj instanceof float[]) {
            append((float[]) obj);
        } else if (obj instanceof boolean[]) {
            append((boolean[]) obj);
        } else {
            append((Object[]) obj);
        }
        return this;
    }

    public HashCodeBuilder append(Object[] objArr) {
        if (objArr == null) {
            this.iTotal *= this.iConstant;
        } else {
            for (Object append : objArr) {
                append(append);
            }
        }
        return this;
    }

    public HashCodeBuilder append(short s) {
        this.iTotal = (this.iTotal * this.iConstant) + s;
        return this;
    }

    public HashCodeBuilder append(short[] sArr) {
        if (sArr == null) {
            this.iTotal *= this.iConstant;
        } else {
            for (short append : sArr) {
                append(append);
            }
        }
        return this;
    }

    public HashCodeBuilder appendSuper(int i) {
        this.iTotal = (this.iTotal * this.iConstant) + i;
        return this;
    }

    public int toHashCode() {
        return this.iTotal;
    }

    public Integer build() {
        return Integer.valueOf(toHashCode());
    }

    public int hashCode() {
        return toHashCode();
    }
}
