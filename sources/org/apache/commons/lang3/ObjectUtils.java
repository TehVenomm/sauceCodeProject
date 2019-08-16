package org.apache.commons.lang3;

import java.io.IOException;
import java.io.Serializable;
import java.lang.reflect.Array;
import java.lang.reflect.InvocationTargetException;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.Map.Entry;
import java.util.TreeSet;
import org.apache.commons.lang3.exception.CloneFailedException;
import org.apache.commons.lang3.mutable.MutableInt;
import org.apache.commons.lang3.text.StrBuilder;

public class ObjectUtils {
    public static final Null NULL = new Null();

    public static class Null implements Serializable {
        private static final long serialVersionUID = 7092611880189329093L;

        Null() {
        }

        private Object readResolve() {
            return ObjectUtils.NULL;
        }
    }

    public static <T> T defaultIfNull(T t, T t2) {
        return t != null ? t : t2;
    }

    public static <T> T firstNonNull(T... tArr) {
        if (tArr != null) {
            for (T t : tArr) {
                if (t != null) {
                    return t;
                }
            }
        }
        return null;
    }

    @Deprecated
    public static boolean equals(Object obj, Object obj2) {
        if (obj == obj2) {
            return true;
        }
        if (obj == null || obj2 == null) {
            return false;
        }
        return obj.equals(obj2);
    }

    public static boolean notEqual(Object obj, Object obj2) {
        return !equals(obj, obj2);
    }

    @Deprecated
    public static int hashCode(Object obj) {
        if (obj == null) {
            return 0;
        }
        return obj.hashCode();
    }

    @Deprecated
    public static int hashCodeMulti(Object... objArr) {
        int i = 1;
        if (objArr != null) {
            for (Object hashCode : objArr) {
                i = (i * 31) + hashCode(hashCode);
            }
        }
        return i;
    }

    public static String identityToString(Object obj) {
        if (obj == null) {
            return null;
        }
        StringBuilder sb = new StringBuilder();
        identityToString(sb, obj);
        return sb.toString();
    }

    public static void identityToString(Appendable appendable, Object obj) throws IOException {
        if (obj == null) {
            throw new NullPointerException("Cannot get the toString of a null identity");
        }
        appendable.append(obj.getClass().getName()).append('@').append(Integer.toHexString(System.identityHashCode(obj)));
    }

    public static void identityToString(StrBuilder strBuilder, Object obj) {
        if (obj == null) {
            throw new NullPointerException("Cannot get the toString of a null identity");
        }
        strBuilder.append(obj.getClass().getName()).append('@').append(Integer.toHexString(System.identityHashCode(obj)));
    }

    public static void identityToString(StringBuffer stringBuffer, Object obj) {
        if (obj == null) {
            throw new NullPointerException("Cannot get the toString of a null identity");
        }
        stringBuffer.append(obj.getClass().getName()).append('@').append(Integer.toHexString(System.identityHashCode(obj)));
    }

    public static void identityToString(StringBuilder sb, Object obj) {
        if (obj == null) {
            throw new NullPointerException("Cannot get the toString of a null identity");
        }
        sb.append(obj.getClass().getName()).append('@').append(Integer.toHexString(System.identityHashCode(obj)));
    }

    @Deprecated
    public static String toString(Object obj) {
        return obj == null ? "" : obj.toString();
    }

    @Deprecated
    public static String toString(Object obj, String str) {
        return obj == null ? str : obj.toString();
    }

    public static <T extends Comparable<? super T>> T min(T... tArr) {
        T t = null;
        if (tArr != null) {
            int length = tArr.length;
            int i = 0;
            while (i < length) {
                T t2 = tArr[i];
                if (compare(t2, t, true) >= 0) {
                    t2 = t;
                }
                i++;
                t = t2;
            }
        }
        return t;
    }

    public static <T extends Comparable<? super T>> T max(T... tArr) {
        T t = null;
        if (tArr != null) {
            int length = tArr.length;
            int i = 0;
            while (i < length) {
                T t2 = tArr[i];
                if (compare(t2, t, false) <= 0) {
                    t2 = t;
                }
                i++;
                t = t2;
            }
        }
        return t;
    }

    public static <T extends Comparable<? super T>> int compare(T t, T t2) {
        return compare(t, t2, false);
    }

    public static <T extends Comparable<? super T>> int compare(T t, T t2, boolean z) {
        int i = -1;
        if (t == t2) {
            return 0;
        }
        if (t == null) {
            if (!z) {
                return -1;
            }
            return 1;
        } else if (t2 != null) {
            return t.compareTo(t2);
        } else {
            if (!z) {
                i = 1;
            }
            return i;
        }
    }

    public static <T extends Comparable<? super T>> T median(T... tArr) {
        Validate.notEmpty(tArr);
        Validate.noNullElements(tArr);
        TreeSet treeSet = new TreeSet();
        Collections.addAll(treeSet, tArr);
        return (Comparable) treeSet.toArray()[(treeSet.size() - 1) / 2];
    }

    public static <T> T median(Comparator<T> comparator, T... tArr) {
        Validate.notEmpty(tArr, "null/empty items", new Object[0]);
        Validate.noNullElements(tArr);
        Validate.notNull(comparator, "null comparator", new Object[0]);
        TreeSet treeSet = new TreeSet(comparator);
        Collections.addAll(treeSet, tArr);
        return treeSet.toArray()[(treeSet.size() - 1) / 2];
    }

    public static <T> T mode(T... tArr) {
        int i;
        int i2 = 0;
        if (!ArrayUtils.isNotEmpty(tArr)) {
            return null;
        }
        HashMap hashMap = new HashMap(tArr.length);
        for (T t : tArr) {
            MutableInt mutableInt = (MutableInt) hashMap.get(t);
            if (mutableInt == null) {
                hashMap.put(t, new MutableInt(1));
            } else {
                mutableInt.increment();
            }
        }
        T t2 = null;
        for (Entry entry : hashMap.entrySet()) {
            int intValue = ((MutableInt) entry.getValue()).intValue();
            if (intValue == i2) {
                i = i2;
                t2 = null;
            } else if (intValue > i2) {
                T key = entry.getKey();
                i = intValue;
                t2 = key;
            } else {
                i = i2;
            }
            i2 = i;
        }
        return t2;
    }

    public static <T> T clone(T t) {
        if (!(t instanceof Cloneable)) {
            return null;
        }
        if (t.getClass().isArray()) {
            Class componentType = t.getClass().getComponentType();
            if (!componentType.isPrimitive()) {
                return ((Object[]) t).clone();
            }
            int length = Array.getLength(t);
            T newInstance = Array.newInstance(componentType, length);
            while (true) {
                int i = length - 1;
                if (length <= 0) {
                    return newInstance;
                }
                Array.set(newInstance, i, Array.get(t, i));
                length = i;
            }
        } else {
            try {
                return t.getClass().getMethod("clone", new Class[0]).invoke(t, new Object[0]);
            } catch (NoSuchMethodException e) {
                throw new CloneFailedException("Cloneable type " + t.getClass().getName() + " has no clone method", e);
            } catch (IllegalAccessException e2) {
                throw new CloneFailedException("Cannot clone Cloneable type " + t.getClass().getName(), e2);
            } catch (InvocationTargetException e3) {
                throw new CloneFailedException("Exception cloning Cloneable type " + t.getClass().getName(), e3.getCause());
            }
        }
    }

    public static <T> T cloneIfPossible(T t) {
        Object clone = clone(t);
        return clone == null ? t : clone;
    }

    public static boolean CONST(boolean z) {
        return z;
    }

    public static byte CONST(byte b) {
        return b;
    }

    public static byte CONST_BYTE(int i) throws IllegalArgumentException {
        if (i >= -128 && i <= 127) {
            return (byte) i;
        }
        throw new IllegalArgumentException("Supplied value must be a valid byte literal between -128 and 127: [" + i + "]");
    }

    public static char CONST(char c) {
        return c;
    }

    public static short CONST(short s) {
        return s;
    }

    public static short CONST_SHORT(int i) throws IllegalArgumentException {
        if (i >= -32768 && i <= 32767) {
            return (short) i;
        }
        throw new IllegalArgumentException("Supplied value must be a valid byte literal between -32768 and 32767: [" + i + "]");
    }

    public static int CONST(int i) {
        return i;
    }

    public static long CONST(long j) {
        return j;
    }

    public static float CONST(float f) {
        return f;
    }

    public static double CONST(double d) {
        return d;
    }

    public static <T> T CONST(T t) {
        return t;
    }
}
