package org.apache.commons.lang3;

import com.google.android.gms.nearby.messages.Strategy;
import java.lang.reflect.Array;
import java.util.Arrays;
import java.util.BitSet;
import java.util.Comparator;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;
import org.apache.commons.lang3.builder.EqualsBuilder;
import org.apache.commons.lang3.builder.HashCodeBuilder;
import org.apache.commons.lang3.builder.ToStringBuilder;
import org.apache.commons.lang3.builder.ToStringStyle;
import org.apache.commons.lang3.math.NumberUtils;
import org.apache.commons.lang3.mutable.MutableInt;

public class ArrayUtils {
    public static final boolean[] EMPTY_BOOLEAN_ARRAY = new boolean[0];
    public static final Boolean[] EMPTY_BOOLEAN_OBJECT_ARRAY = new Boolean[0];
    public static final byte[] EMPTY_BYTE_ARRAY = new byte[0];
    public static final Byte[] EMPTY_BYTE_OBJECT_ARRAY = new Byte[0];
    public static final Character[] EMPTY_CHARACTER_OBJECT_ARRAY = new Character[0];
    public static final char[] EMPTY_CHAR_ARRAY = new char[0];
    public static final Class<?>[] EMPTY_CLASS_ARRAY = new Class[0];
    public static final double[] EMPTY_DOUBLE_ARRAY = new double[0];
    public static final Double[] EMPTY_DOUBLE_OBJECT_ARRAY = new Double[0];
    public static final float[] EMPTY_FLOAT_ARRAY = new float[0];
    public static final Float[] EMPTY_FLOAT_OBJECT_ARRAY = new Float[0];
    public static final Integer[] EMPTY_INTEGER_OBJECT_ARRAY = new Integer[0];
    public static final int[] EMPTY_INT_ARRAY = new int[0];
    public static final long[] EMPTY_LONG_ARRAY = new long[0];
    public static final Long[] EMPTY_LONG_OBJECT_ARRAY = new Long[0];
    public static final Object[] EMPTY_OBJECT_ARRAY = new Object[0];
    public static final short[] EMPTY_SHORT_ARRAY = new short[0];
    public static final Short[] EMPTY_SHORT_OBJECT_ARRAY = new Short[0];
    public static final String[] EMPTY_STRING_ARRAY = new String[0];
    public static final int INDEX_NOT_FOUND = -1;

    /* renamed from: org.apache.commons.lang3.ArrayUtils$1 */
    static class C15831 implements Comparator<T> {
        C15831() {
        }

        public int compare(T t, T t2) {
            return t.compareTo(t2);
        }
    }

    public static String toString(Object obj) {
        return toString(obj, "{}");
    }

    public static String toString(Object obj, String str) {
        return obj == null ? str : new ToStringBuilder(obj, ToStringStyle.SIMPLE_STYLE).append(obj).toString();
    }

    public static int hashCode(Object obj) {
        return new HashCodeBuilder().append(obj).toHashCode();
    }

    @Deprecated
    public static boolean isEquals(Object obj, Object obj2) {
        return new EqualsBuilder().append(obj, obj2).isEquals();
    }

    public static Map<Object, Object> toMap(Object[] objArr) {
        if (objArr == null) {
            return null;
        }
        Map<Object, Object> hashMap = new HashMap((int) (((double) objArr.length) * 1.5d));
        for (int i = 0; i < objArr.length; i++) {
            Object obj = objArr[i];
            if (obj instanceof Entry) {
                Entry entry = (Entry) obj;
                hashMap.put(entry.getKey(), entry.getValue());
            } else if (obj instanceof Object[]) {
                Object[] objArr2 = (Object[]) obj;
                if (objArr2.length < 2) {
                    throw new IllegalArgumentException("Array element " + i + ", '" + obj + "', has a length less than 2");
                }
                hashMap.put(objArr2[0], objArr2[1]);
            } else {
                throw new IllegalArgumentException("Array element " + i + ", '" + obj + "', is neither of type Map.Entry nor an Array");
            }
        }
        return hashMap;
    }

    public static <T> T[] toArray(T... tArr) {
        return tArr;
    }

    public static <T> T[] clone(T[] tArr) {
        if (tArr == null) {
            return null;
        }
        return (Object[]) tArr.clone();
    }

    public static long[] clone(long[] jArr) {
        if (jArr == null) {
            return null;
        }
        return (long[]) jArr.clone();
    }

    public static int[] clone(int[] iArr) {
        if (iArr == null) {
            return null;
        }
        return (int[]) iArr.clone();
    }

    public static short[] clone(short[] sArr) {
        if (sArr == null) {
            return null;
        }
        return (short[]) sArr.clone();
    }

    public static char[] clone(char[] cArr) {
        if (cArr == null) {
            return null;
        }
        return (char[]) cArr.clone();
    }

    public static byte[] clone(byte[] bArr) {
        if (bArr == null) {
            return null;
        }
        return (byte[]) bArr.clone();
    }

    public static double[] clone(double[] dArr) {
        if (dArr == null) {
            return null;
        }
        return (double[]) dArr.clone();
    }

    public static float[] clone(float[] fArr) {
        if (fArr == null) {
            return null;
        }
        return (float[]) fArr.clone();
    }

    public static boolean[] clone(boolean[] zArr) {
        if (zArr == null) {
            return null;
        }
        return (boolean[]) zArr.clone();
    }

    public static Object[] nullToEmpty(Object[] objArr) {
        if (isEmpty(objArr)) {
            return EMPTY_OBJECT_ARRAY;
        }
        return objArr;
    }

    public static Class<?>[] nullToEmpty(Class<?>[] clsArr) {
        if (isEmpty((Object[]) clsArr)) {
            return EMPTY_CLASS_ARRAY;
        }
        return clsArr;
    }

    public static String[] nullToEmpty(String[] strArr) {
        if (isEmpty((Object[]) strArr)) {
            return EMPTY_STRING_ARRAY;
        }
        return strArr;
    }

    public static long[] nullToEmpty(long[] jArr) {
        if (isEmpty(jArr)) {
            return EMPTY_LONG_ARRAY;
        }
        return jArr;
    }

    public static int[] nullToEmpty(int[] iArr) {
        if (isEmpty(iArr)) {
            return EMPTY_INT_ARRAY;
        }
        return iArr;
    }

    public static short[] nullToEmpty(short[] sArr) {
        if (isEmpty(sArr)) {
            return EMPTY_SHORT_ARRAY;
        }
        return sArr;
    }

    public static char[] nullToEmpty(char[] cArr) {
        if (isEmpty(cArr)) {
            return EMPTY_CHAR_ARRAY;
        }
        return cArr;
    }

    public static byte[] nullToEmpty(byte[] bArr) {
        if (isEmpty(bArr)) {
            return EMPTY_BYTE_ARRAY;
        }
        return bArr;
    }

    public static double[] nullToEmpty(double[] dArr) {
        if (isEmpty(dArr)) {
            return EMPTY_DOUBLE_ARRAY;
        }
        return dArr;
    }

    public static float[] nullToEmpty(float[] fArr) {
        if (isEmpty(fArr)) {
            return EMPTY_FLOAT_ARRAY;
        }
        return fArr;
    }

    public static boolean[] nullToEmpty(boolean[] zArr) {
        if (isEmpty(zArr)) {
            return EMPTY_BOOLEAN_ARRAY;
        }
        return zArr;
    }

    public static Long[] nullToEmpty(Long[] lArr) {
        if (isEmpty((Object[]) lArr)) {
            return EMPTY_LONG_OBJECT_ARRAY;
        }
        return lArr;
    }

    public static Integer[] nullToEmpty(Integer[] numArr) {
        if (isEmpty((Object[]) numArr)) {
            return EMPTY_INTEGER_OBJECT_ARRAY;
        }
        return numArr;
    }

    public static Short[] nullToEmpty(Short[] shArr) {
        if (isEmpty((Object[]) shArr)) {
            return EMPTY_SHORT_OBJECT_ARRAY;
        }
        return shArr;
    }

    public static Character[] nullToEmpty(Character[] chArr) {
        if (isEmpty((Object[]) chArr)) {
            return EMPTY_CHARACTER_OBJECT_ARRAY;
        }
        return chArr;
    }

    public static Byte[] nullToEmpty(Byte[] bArr) {
        if (isEmpty((Object[]) bArr)) {
            return EMPTY_BYTE_OBJECT_ARRAY;
        }
        return bArr;
    }

    public static Double[] nullToEmpty(Double[] dArr) {
        if (isEmpty((Object[]) dArr)) {
            return EMPTY_DOUBLE_OBJECT_ARRAY;
        }
        return dArr;
    }

    public static Float[] nullToEmpty(Float[] fArr) {
        if (isEmpty((Object[]) fArr)) {
            return EMPTY_FLOAT_OBJECT_ARRAY;
        }
        return fArr;
    }

    public static Boolean[] nullToEmpty(Boolean[] boolArr) {
        if (isEmpty((Object[]) boolArr)) {
            return EMPTY_BOOLEAN_OBJECT_ARRAY;
        }
        return boolArr;
    }

    public static <T> T[] subarray(T[] tArr, int i, int i2) {
        if (tArr == null) {
            return null;
        }
        if (i < 0) {
            i = 0;
        }
        if (i2 > tArr.length) {
            i2 = tArr.length;
        }
        int i3 = i2 - i;
        Class componentType = tArr.getClass().getComponentType();
        if (i3 <= 0) {
            return (Object[]) Array.newInstance(componentType, 0);
        }
        Object[] objArr = (Object[]) Array.newInstance(componentType, i3);
        System.arraycopy(tArr, i, objArr, 0, i3);
        return objArr;
    }

    public static long[] subarray(long[] jArr, int i, int i2) {
        if (jArr == null) {
            return null;
        }
        if (i < 0) {
            i = 0;
        }
        if (i2 > jArr.length) {
            i2 = jArr.length;
        }
        int i3 = i2 - i;
        if (i3 <= 0) {
            return EMPTY_LONG_ARRAY;
        }
        Object obj = new long[i3];
        System.arraycopy(jArr, i, obj, 0, i3);
        return obj;
    }

    public static int[] subarray(int[] iArr, int i, int i2) {
        if (iArr == null) {
            return null;
        }
        if (i < 0) {
            i = 0;
        }
        if (i2 > iArr.length) {
            i2 = iArr.length;
        }
        int i3 = i2 - i;
        if (i3 <= 0) {
            return EMPTY_INT_ARRAY;
        }
        Object obj = new int[i3];
        System.arraycopy(iArr, i, obj, 0, i3);
        return obj;
    }

    public static short[] subarray(short[] sArr, int i, int i2) {
        if (sArr == null) {
            return null;
        }
        if (i < 0) {
            i = 0;
        }
        if (i2 > sArr.length) {
            i2 = sArr.length;
        }
        int i3 = i2 - i;
        if (i3 <= 0) {
            return EMPTY_SHORT_ARRAY;
        }
        Object obj = new short[i3];
        System.arraycopy(sArr, i, obj, 0, i3);
        return obj;
    }

    public static char[] subarray(char[] cArr, int i, int i2) {
        if (cArr == null) {
            return null;
        }
        if (i < 0) {
            i = 0;
        }
        if (i2 > cArr.length) {
            i2 = cArr.length;
        }
        int i3 = i2 - i;
        if (i3 <= 0) {
            return EMPTY_CHAR_ARRAY;
        }
        Object obj = new char[i3];
        System.arraycopy(cArr, i, obj, 0, i3);
        return obj;
    }

    public static byte[] subarray(byte[] bArr, int i, int i2) {
        if (bArr == null) {
            return null;
        }
        if (i < 0) {
            i = 0;
        }
        if (i2 > bArr.length) {
            i2 = bArr.length;
        }
        int i3 = i2 - i;
        if (i3 <= 0) {
            return EMPTY_BYTE_ARRAY;
        }
        Object obj = new byte[i3];
        System.arraycopy(bArr, i, obj, 0, i3);
        return obj;
    }

    public static double[] subarray(double[] dArr, int i, int i2) {
        if (dArr == null) {
            return null;
        }
        if (i < 0) {
            i = 0;
        }
        if (i2 > dArr.length) {
            i2 = dArr.length;
        }
        int i3 = i2 - i;
        if (i3 <= 0) {
            return EMPTY_DOUBLE_ARRAY;
        }
        Object obj = new double[i3];
        System.arraycopy(dArr, i, obj, 0, i3);
        return obj;
    }

    public static float[] subarray(float[] fArr, int i, int i2) {
        if (fArr == null) {
            return null;
        }
        if (i < 0) {
            i = 0;
        }
        if (i2 > fArr.length) {
            i2 = fArr.length;
        }
        int i3 = i2 - i;
        if (i3 <= 0) {
            return EMPTY_FLOAT_ARRAY;
        }
        Object obj = new float[i3];
        System.arraycopy(fArr, i, obj, 0, i3);
        return obj;
    }

    public static boolean[] subarray(boolean[] zArr, int i, int i2) {
        if (zArr == null) {
            return null;
        }
        if (i < 0) {
            i = 0;
        }
        if (i2 > zArr.length) {
            i2 = zArr.length;
        }
        int i3 = i2 - i;
        if (i3 <= 0) {
            return EMPTY_BOOLEAN_ARRAY;
        }
        Object obj = new boolean[i3];
        System.arraycopy(zArr, i, obj, 0, i3);
        return obj;
    }

    public static boolean isSameLength(Object[] objArr, Object[] objArr2) {
        if ((objArr != null || objArr2 == null || objArr2.length <= 0) && ((objArr2 != null || objArr == null || objArr.length <= 0) && (objArr == null || objArr2 == null || objArr.length == objArr2.length))) {
            return true;
        }
        return false;
    }

    public static boolean isSameLength(long[] jArr, long[] jArr2) {
        if ((jArr != null || jArr2 == null || jArr2.length <= 0) && ((jArr2 != null || jArr == null || jArr.length <= 0) && (jArr == null || jArr2 == null || jArr.length == jArr2.length))) {
            return true;
        }
        return false;
    }

    public static boolean isSameLength(int[] iArr, int[] iArr2) {
        if ((iArr != null || iArr2 == null || iArr2.length <= 0) && ((iArr2 != null || iArr == null || iArr.length <= 0) && (iArr == null || iArr2 == null || iArr.length == iArr2.length))) {
            return true;
        }
        return false;
    }

    public static boolean isSameLength(short[] sArr, short[] sArr2) {
        if ((sArr != null || sArr2 == null || sArr2.length <= 0) && ((sArr2 != null || sArr == null || sArr.length <= 0) && (sArr == null || sArr2 == null || sArr.length == sArr2.length))) {
            return true;
        }
        return false;
    }

    public static boolean isSameLength(char[] cArr, char[] cArr2) {
        if ((cArr != null || cArr2 == null || cArr2.length <= 0) && ((cArr2 != null || cArr == null || cArr.length <= 0) && (cArr == null || cArr2 == null || cArr.length == cArr2.length))) {
            return true;
        }
        return false;
    }

    public static boolean isSameLength(byte[] bArr, byte[] bArr2) {
        if ((bArr != null || bArr2 == null || bArr2.length <= 0) && ((bArr2 != null || bArr == null || bArr.length <= 0) && (bArr == null || bArr2 == null || bArr.length == bArr2.length))) {
            return true;
        }
        return false;
    }

    public static boolean isSameLength(double[] dArr, double[] dArr2) {
        if ((dArr != null || dArr2 == null || dArr2.length <= 0) && ((dArr2 != null || dArr == null || dArr.length <= 0) && (dArr == null || dArr2 == null || dArr.length == dArr2.length))) {
            return true;
        }
        return false;
    }

    public static boolean isSameLength(float[] fArr, float[] fArr2) {
        if ((fArr != null || fArr2 == null || fArr2.length <= 0) && ((fArr2 != null || fArr == null || fArr.length <= 0) && (fArr == null || fArr2 == null || fArr.length == fArr2.length))) {
            return true;
        }
        return false;
    }

    public static boolean isSameLength(boolean[] zArr, boolean[] zArr2) {
        if ((zArr != null || zArr2 == null || zArr2.length <= 0) && ((zArr2 != null || zArr == null || zArr.length <= 0) && (zArr == null || zArr2 == null || zArr.length == zArr2.length))) {
            return true;
        }
        return false;
    }

    public static int getLength(Object obj) {
        if (obj == null) {
            return 0;
        }
        return Array.getLength(obj);
    }

    public static boolean isSameType(Object obj, Object obj2) {
        if (obj != null && obj2 != null) {
            return obj.getClass().getName().equals(obj2.getClass().getName());
        }
        throw new IllegalArgumentException("The Array must not be null");
    }

    public static void reverse(Object[] objArr) {
        if (objArr != null) {
            reverse(objArr, 0, objArr.length);
        }
    }

    public static void reverse(long[] jArr) {
        if (jArr != null) {
            reverse(jArr, 0, jArr.length);
        }
    }

    public static void reverse(int[] iArr) {
        if (iArr != null) {
            reverse(iArr, 0, iArr.length);
        }
    }

    public static void reverse(short[] sArr) {
        if (sArr != null) {
            reverse(sArr, 0, sArr.length);
        }
    }

    public static void reverse(char[] cArr) {
        if (cArr != null) {
            reverse(cArr, 0, cArr.length);
        }
    }

    public static void reverse(byte[] bArr) {
        if (bArr != null) {
            reverse(bArr, 0, bArr.length);
        }
    }

    public static void reverse(double[] dArr) {
        if (dArr != null) {
            reverse(dArr, 0, dArr.length);
        }
    }

    public static void reverse(float[] fArr) {
        if (fArr != null) {
            reverse(fArr, 0, fArr.length);
        }
    }

    public static void reverse(boolean[] zArr) {
        if (zArr != null) {
            reverse(zArr, 0, zArr.length);
        }
    }

    public static void reverse(boolean[] zArr, int i, int i2) {
        if (zArr != null) {
            if (i < 0) {
                i = 0;
            }
            int min = Math.min(zArr.length, i2) - 1;
            for (i = 
/*
Method generation error in method: org.apache.commons.lang3.ArrayUtils.reverse(boolean[], int, int):void, dex: classes.dex
jadx.core.utils.exceptions.CodegenException: Error generate insn: PHI: (r4_2 'i' int) = (r4_0 'i' int), (r4_1 'i' int) binds: {(r4_0 'i' int)=B:1:0x0003, (r4_1 'i' int)=B:2:0x0005} in method: org.apache.commons.lang3.ArrayUtils.reverse(boolean[], int, int):void, dex: classes.dex
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:226)
	at jadx.core.codegen.RegionGen.makeLoop(RegionGen.java:184)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:61)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeRegionIndent(RegionGen.java:93)
	at jadx.core.codegen.RegionGen.makeIf(RegionGen.java:118)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:57)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.MethodGen.addInstructions(MethodGen.java:187)
	at jadx.core.codegen.ClassGen.addMethod(ClassGen.java:320)
	at jadx.core.codegen.ClassGen.addMethods(ClassGen.java:257)
	at jadx.core.codegen.ClassGen.addClassBody(ClassGen.java:220)
	at jadx.core.codegen.ClassGen.addClassCode(ClassGen.java:110)
	at jadx.core.codegen.ClassGen.makeClass(ClassGen.java:75)
	at jadx.core.codegen.CodeGen.visit(CodeGen.java:12)
	at jadx.core.ProcessClass.process(ProcessClass.java:40)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
Caused by: jadx.core.utils.exceptions.CodegenException: PHI can be used only in fallback mode
	at jadx.core.codegen.InsnGen.fallbackOnlyInsn(InsnGen.java:537)
	at jadx.core.codegen.InsnGen.makeInsnBody(InsnGen.java:509)
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:220)
	... 23 more

*/

            public static void reverse(byte[] bArr, int i, int i2) {
                if (bArr != null) {
                    if (i < 0) {
                        i = 0;
                    }
                    int min = Math.min(bArr.length, i2) - 1;
                    for (i = 
/*
Method generation error in method: org.apache.commons.lang3.ArrayUtils.reverse(byte[], int, int):void, dex: classes.dex
jadx.core.utils.exceptions.CodegenException: Error generate insn: PHI: (r4_2 'i' int) = (r4_0 'i' int), (r4_1 'i' int) binds: {(r4_1 'i' int)=B:2:0x0005, (r4_0 'i' int)=B:1:0x0003} in method: org.apache.commons.lang3.ArrayUtils.reverse(byte[], int, int):void, dex: classes.dex
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:226)
	at jadx.core.codegen.RegionGen.makeLoop(RegionGen.java:184)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:61)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeRegionIndent(RegionGen.java:93)
	at jadx.core.codegen.RegionGen.makeIf(RegionGen.java:118)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:57)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.MethodGen.addInstructions(MethodGen.java:187)
	at jadx.core.codegen.ClassGen.addMethod(ClassGen.java:320)
	at jadx.core.codegen.ClassGen.addMethods(ClassGen.java:257)
	at jadx.core.codegen.ClassGen.addClassBody(ClassGen.java:220)
	at jadx.core.codegen.ClassGen.addClassCode(ClassGen.java:110)
	at jadx.core.codegen.ClassGen.makeClass(ClassGen.java:75)
	at jadx.core.codegen.CodeGen.visit(CodeGen.java:12)
	at jadx.core.ProcessClass.process(ProcessClass.java:40)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
Caused by: jadx.core.utils.exceptions.CodegenException: PHI can be used only in fallback mode
	at jadx.core.codegen.InsnGen.fallbackOnlyInsn(InsnGen.java:537)
	at jadx.core.codegen.InsnGen.makeInsnBody(InsnGen.java:509)
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:220)
	... 23 more

*/

                    public static void reverse(char[] cArr, int i, int i2) {
                        if (cArr != null) {
                            if (i < 0) {
                                i = 0;
                            }
                            int min = Math.min(cArr.length, i2) - 1;
                            for (i = 
/*
Method generation error in method: org.apache.commons.lang3.ArrayUtils.reverse(char[], int, int):void, dex: classes.dex
jadx.core.utils.exceptions.CodegenException: Error generate insn: PHI: (r4_2 'i' int) = (r4_0 'i' int), (r4_1 'i' int) binds: {(r4_1 'i' int)=B:2:0x0005, (r4_0 'i' int)=B:1:0x0003} in method: org.apache.commons.lang3.ArrayUtils.reverse(char[], int, int):void, dex: classes.dex
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:226)
	at jadx.core.codegen.RegionGen.makeLoop(RegionGen.java:184)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:61)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeRegionIndent(RegionGen.java:93)
	at jadx.core.codegen.RegionGen.makeIf(RegionGen.java:118)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:57)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.MethodGen.addInstructions(MethodGen.java:187)
	at jadx.core.codegen.ClassGen.addMethod(ClassGen.java:320)
	at jadx.core.codegen.ClassGen.addMethods(ClassGen.java:257)
	at jadx.core.codegen.ClassGen.addClassBody(ClassGen.java:220)
	at jadx.core.codegen.ClassGen.addClassCode(ClassGen.java:110)
	at jadx.core.codegen.ClassGen.makeClass(ClassGen.java:75)
	at jadx.core.codegen.CodeGen.visit(CodeGen.java:12)
	at jadx.core.ProcessClass.process(ProcessClass.java:40)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
Caused by: jadx.core.utils.exceptions.CodegenException: PHI can be used only in fallback mode
	at jadx.core.codegen.InsnGen.fallbackOnlyInsn(InsnGen.java:537)
	at jadx.core.codegen.InsnGen.makeInsnBody(InsnGen.java:509)
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:220)
	... 23 more

*/

                            public static void reverse(double[] dArr, int i, int i2) {
                                if (dArr != null) {
                                    if (i < 0) {
                                        i = 0;
                                    }
                                    int min = Math.min(dArr.length, i2) - 1;
                                    for (i = 
/*
Method generation error in method: org.apache.commons.lang3.ArrayUtils.reverse(double[], int, int):void, dex: classes.dex
jadx.core.utils.exceptions.CodegenException: Error generate insn: PHI: (r7_2 'i' int) = (r7_0 'i' int), (r7_1 'i' int) binds: {(r7_0 'i' int)=B:1:0x0003, (r7_1 'i' int)=B:2:0x0005} in method: org.apache.commons.lang3.ArrayUtils.reverse(double[], int, int):void, dex: classes.dex
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:226)
	at jadx.core.codegen.RegionGen.makeLoop(RegionGen.java:184)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:61)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeRegionIndent(RegionGen.java:93)
	at jadx.core.codegen.RegionGen.makeIf(RegionGen.java:118)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:57)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.MethodGen.addInstructions(MethodGen.java:187)
	at jadx.core.codegen.ClassGen.addMethod(ClassGen.java:320)
	at jadx.core.codegen.ClassGen.addMethods(ClassGen.java:257)
	at jadx.core.codegen.ClassGen.addClassBody(ClassGen.java:220)
	at jadx.core.codegen.ClassGen.addClassCode(ClassGen.java:110)
	at jadx.core.codegen.ClassGen.makeClass(ClassGen.java:75)
	at jadx.core.codegen.CodeGen.visit(CodeGen.java:12)
	at jadx.core.ProcessClass.process(ProcessClass.java:40)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
Caused by: jadx.core.utils.exceptions.CodegenException: PHI can be used only in fallback mode
	at jadx.core.codegen.InsnGen.fallbackOnlyInsn(InsnGen.java:537)
	at jadx.core.codegen.InsnGen.makeInsnBody(InsnGen.java:509)
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:220)
	... 23 more

*/

                                    public static void reverse(float[] fArr, int i, int i2) {
                                        if (fArr != null) {
                                            if (i < 0) {
                                                i = 0;
                                            }
                                            int min = Math.min(fArr.length, i2) - 1;
                                            for (i = 
/*
Method generation error in method: org.apache.commons.lang3.ArrayUtils.reverse(float[], int, int):void, dex: classes.dex
jadx.core.utils.exceptions.CodegenException: Error generate insn: PHI: (r4_2 'i' int) = (r4_0 'i' int), (r4_1 'i' int) binds: {(r4_1 'i' int)=B:2:0x0005, (r4_0 'i' int)=B:1:0x0003} in method: org.apache.commons.lang3.ArrayUtils.reverse(float[], int, int):void, dex: classes.dex
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:226)
	at jadx.core.codegen.RegionGen.makeLoop(RegionGen.java:184)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:61)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeRegionIndent(RegionGen.java:93)
	at jadx.core.codegen.RegionGen.makeIf(RegionGen.java:118)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:57)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.MethodGen.addInstructions(MethodGen.java:187)
	at jadx.core.codegen.ClassGen.addMethod(ClassGen.java:320)
	at jadx.core.codegen.ClassGen.addMethods(ClassGen.java:257)
	at jadx.core.codegen.ClassGen.addClassBody(ClassGen.java:220)
	at jadx.core.codegen.ClassGen.addClassCode(ClassGen.java:110)
	at jadx.core.codegen.ClassGen.makeClass(ClassGen.java:75)
	at jadx.core.codegen.CodeGen.visit(CodeGen.java:12)
	at jadx.core.ProcessClass.process(ProcessClass.java:40)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
Caused by: jadx.core.utils.exceptions.CodegenException: PHI can be used only in fallback mode
	at jadx.core.codegen.InsnGen.fallbackOnlyInsn(InsnGen.java:537)
	at jadx.core.codegen.InsnGen.makeInsnBody(InsnGen.java:509)
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:220)
	... 23 more

*/

                                            public static void reverse(int[] iArr, int i, int i2) {
                                                if (iArr != null) {
                                                    if (i < 0) {
                                                        i = 0;
                                                    }
                                                    int min = Math.min(iArr.length, i2) - 1;
                                                    for (i = 
/*
Method generation error in method: org.apache.commons.lang3.ArrayUtils.reverse(int[], int, int):void, dex: classes.dex
jadx.core.utils.exceptions.CodegenException: Error generate insn: PHI: (r4_2 'i' int) = (r4_0 'i' int), (r4_1 'i' int) binds: {(r4_1 'i' int)=B:2:0x0005, (r4_0 'i' int)=B:1:0x0003} in method: org.apache.commons.lang3.ArrayUtils.reverse(int[], int, int):void, dex: classes.dex
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:226)
	at jadx.core.codegen.RegionGen.makeLoop(RegionGen.java:184)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:61)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeRegionIndent(RegionGen.java:93)
	at jadx.core.codegen.RegionGen.makeIf(RegionGen.java:118)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:57)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.MethodGen.addInstructions(MethodGen.java:187)
	at jadx.core.codegen.ClassGen.addMethod(ClassGen.java:320)
	at jadx.core.codegen.ClassGen.addMethods(ClassGen.java:257)
	at jadx.core.codegen.ClassGen.addClassBody(ClassGen.java:220)
	at jadx.core.codegen.ClassGen.addClassCode(ClassGen.java:110)
	at jadx.core.codegen.ClassGen.makeClass(ClassGen.java:75)
	at jadx.core.codegen.CodeGen.visit(CodeGen.java:12)
	at jadx.core.ProcessClass.process(ProcessClass.java:40)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
Caused by: jadx.core.utils.exceptions.CodegenException: PHI can be used only in fallback mode
	at jadx.core.codegen.InsnGen.fallbackOnlyInsn(InsnGen.java:537)
	at jadx.core.codegen.InsnGen.makeInsnBody(InsnGen.java:509)
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:220)
	... 23 more

*/

                                                    public static void reverse(long[] jArr, int i, int i2) {
                                                        if (jArr != null) {
                                                            if (i < 0) {
                                                                i = 0;
                                                            }
                                                            int min = Math.min(jArr.length, i2) - 1;
                                                            for (i = 
/*
Method generation error in method: org.apache.commons.lang3.ArrayUtils.reverse(long[], int, int):void, dex: classes.dex
jadx.core.utils.exceptions.CodegenException: Error generate insn: PHI: (r7_2 'i' int) = (r7_0 'i' int), (r7_1 'i' int) binds: {(r7_0 'i' int)=B:1:0x0003, (r7_1 'i' int)=B:2:0x0005} in method: org.apache.commons.lang3.ArrayUtils.reverse(long[], int, int):void, dex: classes.dex
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:226)
	at jadx.core.codegen.RegionGen.makeLoop(RegionGen.java:184)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:61)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeRegionIndent(RegionGen.java:93)
	at jadx.core.codegen.RegionGen.makeIf(RegionGen.java:118)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:57)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.MethodGen.addInstructions(MethodGen.java:187)
	at jadx.core.codegen.ClassGen.addMethod(ClassGen.java:320)
	at jadx.core.codegen.ClassGen.addMethods(ClassGen.java:257)
	at jadx.core.codegen.ClassGen.addClassBody(ClassGen.java:220)
	at jadx.core.codegen.ClassGen.addClassCode(ClassGen.java:110)
	at jadx.core.codegen.ClassGen.makeClass(ClassGen.java:75)
	at jadx.core.codegen.CodeGen.visit(CodeGen.java:12)
	at jadx.core.ProcessClass.process(ProcessClass.java:40)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
Caused by: jadx.core.utils.exceptions.CodegenException: PHI can be used only in fallback mode
	at jadx.core.codegen.InsnGen.fallbackOnlyInsn(InsnGen.java:537)
	at jadx.core.codegen.InsnGen.makeInsnBody(InsnGen.java:509)
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:220)
	... 23 more

*/

                                                            public static void reverse(Object[] objArr, int i, int i2) {
                                                                if (objArr != null) {
                                                                    if (i < 0) {
                                                                        i = 0;
                                                                    }
                                                                    int min = Math.min(objArr.length, i2) - 1;
                                                                    for (i = 
/*
Method generation error in method: org.apache.commons.lang3.ArrayUtils.reverse(java.lang.Object[], int, int):void, dex: classes.dex
jadx.core.utils.exceptions.CodegenException: Error generate insn: PHI: (r4_2 'i' int) = (r4_0 'i' int), (r4_1 'i' int) binds: {(r4_1 'i' int)=B:2:0x0005, (r4_0 'i' int)=B:1:0x0003} in method: org.apache.commons.lang3.ArrayUtils.reverse(java.lang.Object[], int, int):void, dex: classes.dex
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:226)
	at jadx.core.codegen.RegionGen.makeLoop(RegionGen.java:184)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:61)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeRegionIndent(RegionGen.java:93)
	at jadx.core.codegen.RegionGen.makeIf(RegionGen.java:118)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:57)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.MethodGen.addInstructions(MethodGen.java:187)
	at jadx.core.codegen.ClassGen.addMethod(ClassGen.java:320)
	at jadx.core.codegen.ClassGen.addMethods(ClassGen.java:257)
	at jadx.core.codegen.ClassGen.addClassBody(ClassGen.java:220)
	at jadx.core.codegen.ClassGen.addClassCode(ClassGen.java:110)
	at jadx.core.codegen.ClassGen.makeClass(ClassGen.java:75)
	at jadx.core.codegen.CodeGen.visit(CodeGen.java:12)
	at jadx.core.ProcessClass.process(ProcessClass.java:40)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
Caused by: jadx.core.utils.exceptions.CodegenException: PHI can be used only in fallback mode
	at jadx.core.codegen.InsnGen.fallbackOnlyInsn(InsnGen.java:537)
	at jadx.core.codegen.InsnGen.makeInsnBody(InsnGen.java:509)
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:220)
	... 23 more

*/

                                                                    public static void reverse(short[] sArr, int i, int i2) {
                                                                        if (sArr != null) {
                                                                            if (i < 0) {
                                                                                i = 0;
                                                                            }
                                                                            int min = Math.min(sArr.length, i2) - 1;
                                                                            for (i = 
/*
Method generation error in method: org.apache.commons.lang3.ArrayUtils.reverse(short[], int, int):void, dex: classes.dex
jadx.core.utils.exceptions.CodegenException: Error generate insn: PHI: (r4_2 'i' int) = (r4_0 'i' int), (r4_1 'i' int) binds: {(r4_0 'i' int)=B:1:0x0003, (r4_1 'i' int)=B:2:0x0005} in method: org.apache.commons.lang3.ArrayUtils.reverse(short[], int, int):void, dex: classes.dex
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:226)
	at jadx.core.codegen.RegionGen.makeLoop(RegionGen.java:184)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:61)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeRegionIndent(RegionGen.java:93)
	at jadx.core.codegen.RegionGen.makeIf(RegionGen.java:118)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:57)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.RegionGen.makeSimpleRegion(RegionGen.java:87)
	at jadx.core.codegen.RegionGen.makeRegion(RegionGen.java:53)
	at jadx.core.codegen.MethodGen.addInstructions(MethodGen.java:187)
	at jadx.core.codegen.ClassGen.addMethod(ClassGen.java:320)
	at jadx.core.codegen.ClassGen.addMethods(ClassGen.java:257)
	at jadx.core.codegen.ClassGen.addClassBody(ClassGen.java:220)
	at jadx.core.codegen.ClassGen.addClassCode(ClassGen.java:110)
	at jadx.core.codegen.ClassGen.makeClass(ClassGen.java:75)
	at jadx.core.codegen.CodeGen.visit(CodeGen.java:12)
	at jadx.core.ProcessClass.process(ProcessClass.java:40)
	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:282)
	at jadx.api.JavaClass.decompile(JavaClass.java:62)
	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:200)
	at jadx.api.JadxDecompiler$$Lambda$8/1758893871.run(Unknown Source)
Caused by: jadx.core.utils.exceptions.CodegenException: PHI can be used only in fallback mode
	at jadx.core.codegen.InsnGen.fallbackOnlyInsn(InsnGen.java:537)
	at jadx.core.codegen.InsnGen.makeInsnBody(InsnGen.java:509)
	at jadx.core.codegen.InsnGen.makeInsn(InsnGen.java:220)
	... 23 more

*/

                                                                            public static int indexOf(Object[] objArr, Object obj) {
                                                                                return indexOf(objArr, obj, 0);
                                                                            }

                                                                            public static int indexOf(Object[] objArr, Object obj, int i) {
                                                                                if (objArr == null) {
                                                                                    return -1;
                                                                                }
                                                                                int i2;
                                                                                if (i < 0) {
                                                                                    i2 = 0;
                                                                                } else {
                                                                                    i2 = i;
                                                                                }
                                                                                if (obj == null) {
                                                                                    while (i2 < objArr.length) {
                                                                                        if (objArr[i2] == null) {
                                                                                            return i2;
                                                                                        }
                                                                                        i2++;
                                                                                    }
                                                                                } else if (objArr.getClass().getComponentType().isInstance(obj)) {
                                                                                    while (i2 < objArr.length) {
                                                                                        if (obj.equals(objArr[i2])) {
                                                                                            return i2;
                                                                                        }
                                                                                        i2++;
                                                                                    }
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static int lastIndexOf(Object[] objArr, Object obj) {
                                                                                return lastIndexOf(objArr, obj, (int) Strategy.TTL_SECONDS_INFINITE);
                                                                            }

                                                                            public static int lastIndexOf(Object[] objArr, Object obj, int i) {
                                                                                if (objArr == null || i < 0) {
                                                                                    return -1;
                                                                                }
                                                                                int length;
                                                                                if (i >= objArr.length) {
                                                                                    length = objArr.length - 1;
                                                                                } else {
                                                                                    length = i;
                                                                                }
                                                                                if (obj == null) {
                                                                                    while (length >= 0) {
                                                                                        if (objArr[length] == null) {
                                                                                            return length;
                                                                                        }
                                                                                        length--;
                                                                                    }
                                                                                    return -1;
                                                                                } else if (!objArr.getClass().getComponentType().isInstance(obj)) {
                                                                                    return -1;
                                                                                } else {
                                                                                    while (length >= 0) {
                                                                                        if (obj.equals(objArr[length])) {
                                                                                            return length;
                                                                                        }
                                                                                        length--;
                                                                                    }
                                                                                    return -1;
                                                                                }
                                                                            }

                                                                            public static boolean contains(Object[] objArr, Object obj) {
                                                                                return indexOf(objArr, obj) != -1;
                                                                            }

                                                                            public static int indexOf(long[] jArr, long j) {
                                                                                return indexOf(jArr, j, 0);
                                                                            }

                                                                            public static int indexOf(long[] jArr, long j, int i) {
                                                                                if (jArr == null) {
                                                                                    return -1;
                                                                                }
                                                                                if (i < 0) {
                                                                                    i = 0;
                                                                                }
                                                                                while (i < jArr.length) {
                                                                                    if (j == jArr[i]) {
                                                                                        return i;
                                                                                    }
                                                                                    i++;
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static int lastIndexOf(long[] jArr, long j) {
                                                                                return lastIndexOf(jArr, j, (int) Strategy.TTL_SECONDS_INFINITE);
                                                                            }

                                                                            public static int lastIndexOf(long[] jArr, long j, int i) {
                                                                                if (jArr == null || i < 0) {
                                                                                    return -1;
                                                                                }
                                                                                if (i >= jArr.length) {
                                                                                    i = jArr.length - 1;
                                                                                }
                                                                                for (int i2 = i; i2 >= 0; i2--) {
                                                                                    if (j == jArr[i2]) {
                                                                                        return i2;
                                                                                    }
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static boolean contains(long[] jArr, long j) {
                                                                                return indexOf(jArr, j) != -1;
                                                                            }

                                                                            public static int indexOf(int[] iArr, int i) {
                                                                                return indexOf(iArr, i, 0);
                                                                            }

                                                                            public static int indexOf(int[] iArr, int i, int i2) {
                                                                                if (iArr == null) {
                                                                                    return -1;
                                                                                }
                                                                                if (i2 < 0) {
                                                                                    i2 = 0;
                                                                                }
                                                                                while (i2 < iArr.length) {
                                                                                    if (i == iArr[i2]) {
                                                                                        return i2;
                                                                                    }
                                                                                    i2++;
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static int lastIndexOf(int[] iArr, int i) {
                                                                                return lastIndexOf(iArr, i, (int) Strategy.TTL_SECONDS_INFINITE);
                                                                            }

                                                                            public static int lastIndexOf(int[] iArr, int i, int i2) {
                                                                                if (iArr == null || i2 < 0) {
                                                                                    return -1;
                                                                                }
                                                                                if (i2 >= iArr.length) {
                                                                                    i2 = iArr.length - 1;
                                                                                }
                                                                                for (int i3 = i2; i3 >= 0; i3--) {
                                                                                    if (i == iArr[i3]) {
                                                                                        return i3;
                                                                                    }
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static boolean contains(int[] iArr, int i) {
                                                                                return indexOf(iArr, i) != -1;
                                                                            }

                                                                            public static int indexOf(short[] sArr, short s) {
                                                                                return indexOf(sArr, s, 0);
                                                                            }

                                                                            public static int indexOf(short[] sArr, short s, int i) {
                                                                                if (sArr == null) {
                                                                                    return -1;
                                                                                }
                                                                                if (i < 0) {
                                                                                    i = 0;
                                                                                }
                                                                                while (i < sArr.length) {
                                                                                    if (s == sArr[i]) {
                                                                                        return i;
                                                                                    }
                                                                                    i++;
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static int lastIndexOf(short[] sArr, short s) {
                                                                                return lastIndexOf(sArr, s, (int) Strategy.TTL_SECONDS_INFINITE);
                                                                            }

                                                                            public static int lastIndexOf(short[] sArr, short s, int i) {
                                                                                if (sArr == null || i < 0) {
                                                                                    return -1;
                                                                                }
                                                                                if (i >= sArr.length) {
                                                                                    i = sArr.length - 1;
                                                                                }
                                                                                for (int i2 = i; i2 >= 0; i2--) {
                                                                                    if (s == sArr[i2]) {
                                                                                        return i2;
                                                                                    }
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static boolean contains(short[] sArr, short s) {
                                                                                return indexOf(sArr, s) != -1;
                                                                            }

                                                                            public static int indexOf(char[] cArr, char c) {
                                                                                return indexOf(cArr, c, 0);
                                                                            }

                                                                            public static int indexOf(char[] cArr, char c, int i) {
                                                                                if (cArr == null) {
                                                                                    return -1;
                                                                                }
                                                                                if (i < 0) {
                                                                                    i = 0;
                                                                                }
                                                                                while (i < cArr.length) {
                                                                                    if (c == cArr[i]) {
                                                                                        return i;
                                                                                    }
                                                                                    i++;
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static int lastIndexOf(char[] cArr, char c) {
                                                                                return lastIndexOf(cArr, c, (int) Strategy.TTL_SECONDS_INFINITE);
                                                                            }

                                                                            public static int lastIndexOf(char[] cArr, char c, int i) {
                                                                                if (cArr == null || i < 0) {
                                                                                    return -1;
                                                                                }
                                                                                if (i >= cArr.length) {
                                                                                    i = cArr.length - 1;
                                                                                }
                                                                                for (int i2 = i; i2 >= 0; i2--) {
                                                                                    if (c == cArr[i2]) {
                                                                                        return i2;
                                                                                    }
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static boolean contains(char[] cArr, char c) {
                                                                                return indexOf(cArr, c) != -1;
                                                                            }

                                                                            public static int indexOf(byte[] bArr, byte b) {
                                                                                return indexOf(bArr, b, 0);
                                                                            }

                                                                            public static int indexOf(byte[] bArr, byte b, int i) {
                                                                                if (bArr == null) {
                                                                                    return -1;
                                                                                }
                                                                                if (i < 0) {
                                                                                    i = 0;
                                                                                }
                                                                                while (i < bArr.length) {
                                                                                    if (b == bArr[i]) {
                                                                                        return i;
                                                                                    }
                                                                                    i++;
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static int lastIndexOf(byte[] bArr, byte b) {
                                                                                return lastIndexOf(bArr, b, (int) Strategy.TTL_SECONDS_INFINITE);
                                                                            }

                                                                            public static int lastIndexOf(byte[] bArr, byte b, int i) {
                                                                                if (bArr == null || i < 0) {
                                                                                    return -1;
                                                                                }
                                                                                if (i >= bArr.length) {
                                                                                    i = bArr.length - 1;
                                                                                }
                                                                                for (int i2 = i; i2 >= 0; i2--) {
                                                                                    if (b == bArr[i2]) {
                                                                                        return i2;
                                                                                    }
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static boolean contains(byte[] bArr, byte b) {
                                                                                return indexOf(bArr, b) != -1;
                                                                            }

                                                                            public static int indexOf(double[] dArr, double d) {
                                                                                return indexOf(dArr, d, 0);
                                                                            }

                                                                            public static int indexOf(double[] dArr, double d, double d2) {
                                                                                return indexOf(dArr, d, 0, d2);
                                                                            }

                                                                            public static int indexOf(double[] dArr, double d, int i) {
                                                                                if (isEmpty(dArr)) {
                                                                                    return -1;
                                                                                }
                                                                                if (i < 0) {
                                                                                    i = 0;
                                                                                }
                                                                                while (i < dArr.length) {
                                                                                    if (d == dArr[i]) {
                                                                                        return i;
                                                                                    }
                                                                                    i++;
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static int indexOf(double[] dArr, double d, int i, double d2) {
                                                                                if (isEmpty(dArr)) {
                                                                                    return -1;
                                                                                }
                                                                                if (i < 0) {
                                                                                    i = 0;
                                                                                }
                                                                                double d3 = d - d2;
                                                                                double d4 = d + d2;
                                                                                while (i < dArr.length) {
                                                                                    if (dArr[i] >= d3 && dArr[i] <= d4) {
                                                                                        return i;
                                                                                    }
                                                                                    i++;
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static int lastIndexOf(double[] dArr, double d) {
                                                                                return lastIndexOf(dArr, d, (int) Strategy.TTL_SECONDS_INFINITE);
                                                                            }

                                                                            public static int lastIndexOf(double[] dArr, double d, double d2) {
                                                                                return lastIndexOf(dArr, d, Strategy.TTL_SECONDS_INFINITE, d2);
                                                                            }

                                                                            public static int lastIndexOf(double[] dArr, double d, int i) {
                                                                                if (isEmpty(dArr) || i < 0) {
                                                                                    return -1;
                                                                                }
                                                                                if (i >= dArr.length) {
                                                                                    i = dArr.length - 1;
                                                                                }
                                                                                for (int i2 = i; i2 >= 0; i2--) {
                                                                                    if (d == dArr[i2]) {
                                                                                        return i2;
                                                                                    }
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static int lastIndexOf(double[] dArr, double d, int i, double d2) {
                                                                                if (isEmpty(dArr) || i < 0) {
                                                                                    return -1;
                                                                                }
                                                                                if (i >= dArr.length) {
                                                                                    i = dArr.length - 1;
                                                                                }
                                                                                double d3 = d - d2;
                                                                                double d4 = d + d2;
                                                                                int i2 = i;
                                                                                while (i2 >= 0) {
                                                                                    if (dArr[i2] >= d3 && dArr[i2] <= d4) {
                                                                                        return i2;
                                                                                    }
                                                                                    i2--;
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static boolean contains(double[] dArr, double d) {
                                                                                return indexOf(dArr, d) != -1;
                                                                            }

                                                                            public static boolean contains(double[] dArr, double d, double d2) {
                                                                                return indexOf(dArr, d, 0, d2) != -1;
                                                                            }

                                                                            public static int indexOf(float[] fArr, float f) {
                                                                                return indexOf(fArr, f, 0);
                                                                            }

                                                                            public static int indexOf(float[] fArr, float f, int i) {
                                                                                if (isEmpty(fArr)) {
                                                                                    return -1;
                                                                                }
                                                                                if (i < 0) {
                                                                                    i = 0;
                                                                                }
                                                                                while (i < fArr.length) {
                                                                                    if (f == fArr[i]) {
                                                                                        return i;
                                                                                    }
                                                                                    i++;
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static int lastIndexOf(float[] fArr, float f) {
                                                                                return lastIndexOf(fArr, f, (int) Strategy.TTL_SECONDS_INFINITE);
                                                                            }

                                                                            public static int lastIndexOf(float[] fArr, float f, int i) {
                                                                                if (isEmpty(fArr) || i < 0) {
                                                                                    return -1;
                                                                                }
                                                                                if (i >= fArr.length) {
                                                                                    i = fArr.length - 1;
                                                                                }
                                                                                for (int i2 = i; i2 >= 0; i2--) {
                                                                                    if (f == fArr[i2]) {
                                                                                        return i2;
                                                                                    }
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static boolean contains(float[] fArr, float f) {
                                                                                return indexOf(fArr, f) != -1;
                                                                            }

                                                                            public static int indexOf(boolean[] zArr, boolean z) {
                                                                                return indexOf(zArr, z, 0);
                                                                            }

                                                                            public static int indexOf(boolean[] zArr, boolean z, int i) {
                                                                                if (isEmpty(zArr)) {
                                                                                    return -1;
                                                                                }
                                                                                if (i < 0) {
                                                                                    i = 0;
                                                                                }
                                                                                while (i < zArr.length) {
                                                                                    if (z == zArr[i]) {
                                                                                        return i;
                                                                                    }
                                                                                    i++;
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static int lastIndexOf(boolean[] zArr, boolean z) {
                                                                                return lastIndexOf(zArr, z, (int) Strategy.TTL_SECONDS_INFINITE);
                                                                            }

                                                                            public static int lastIndexOf(boolean[] zArr, boolean z, int i) {
                                                                                if (isEmpty(zArr) || i < 0) {
                                                                                    return -1;
                                                                                }
                                                                                if (i >= zArr.length) {
                                                                                    i = zArr.length - 1;
                                                                                }
                                                                                for (int i2 = i; i2 >= 0; i2--) {
                                                                                    if (z == zArr[i2]) {
                                                                                        return i2;
                                                                                    }
                                                                                }
                                                                                return -1;
                                                                            }

                                                                            public static boolean contains(boolean[] zArr, boolean z) {
                                                                                return indexOf(zArr, z) != -1;
                                                                            }

                                                                            public static char[] toPrimitive(Character[] chArr) {
                                                                                if (chArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (chArr.length == 0) {
                                                                                    return EMPTY_CHAR_ARRAY;
                                                                                }
                                                                                char[] cArr = new char[chArr.length];
                                                                                for (int i = 0; i < chArr.length; i++) {
                                                                                    cArr[i] = chArr[i].charValue();
                                                                                }
                                                                                return cArr;
                                                                            }

                                                                            public static char[] toPrimitive(Character[] chArr, char c) {
                                                                                if (chArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (chArr.length == 0) {
                                                                                    return EMPTY_CHAR_ARRAY;
                                                                                }
                                                                                char[] cArr = new char[chArr.length];
                                                                                for (int i = 0; i < chArr.length; i++) {
                                                                                    Character ch = chArr[i];
                                                                                    cArr[i] = ch == null ? c : ch.charValue();
                                                                                }
                                                                                return cArr;
                                                                            }

                                                                            public static Character[] toObject(char[] cArr) {
                                                                                if (cArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (cArr.length == 0) {
                                                                                    return EMPTY_CHARACTER_OBJECT_ARRAY;
                                                                                }
                                                                                Character[] chArr = new Character[cArr.length];
                                                                                for (int i = 0; i < cArr.length; i++) {
                                                                                    chArr[i] = Character.valueOf(cArr[i]);
                                                                                }
                                                                                return chArr;
                                                                            }

                                                                            public static long[] toPrimitive(Long[] lArr) {
                                                                                if (lArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (lArr.length == 0) {
                                                                                    return EMPTY_LONG_ARRAY;
                                                                                }
                                                                                long[] jArr = new long[lArr.length];
                                                                                for (int i = 0; i < lArr.length; i++) {
                                                                                    jArr[i] = lArr[i].longValue();
                                                                                }
                                                                                return jArr;
                                                                            }

                                                                            public static long[] toPrimitive(Long[] lArr, long j) {
                                                                                if (lArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (lArr.length == 0) {
                                                                                    return EMPTY_LONG_ARRAY;
                                                                                }
                                                                                long[] jArr = new long[lArr.length];
                                                                                for (int i = 0; i < lArr.length; i++) {
                                                                                    Long l = lArr[i];
                                                                                    jArr[i] = l == null ? j : l.longValue();
                                                                                }
                                                                                return jArr;
                                                                            }

                                                                            public static Long[] toObject(long[] jArr) {
                                                                                if (jArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (jArr.length == 0) {
                                                                                    return EMPTY_LONG_OBJECT_ARRAY;
                                                                                }
                                                                                Long[] lArr = new Long[jArr.length];
                                                                                for (int i = 0; i < jArr.length; i++) {
                                                                                    lArr[i] = Long.valueOf(jArr[i]);
                                                                                }
                                                                                return lArr;
                                                                            }

                                                                            public static int[] toPrimitive(Integer[] numArr) {
                                                                                if (numArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (numArr.length == 0) {
                                                                                    return EMPTY_INT_ARRAY;
                                                                                }
                                                                                int[] iArr = new int[numArr.length];
                                                                                for (int i = 0; i < numArr.length; i++) {
                                                                                    iArr[i] = numArr[i].intValue();
                                                                                }
                                                                                return iArr;
                                                                            }

                                                                            public static int[] toPrimitive(Integer[] numArr, int i) {
                                                                                if (numArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (numArr.length == 0) {
                                                                                    return EMPTY_INT_ARRAY;
                                                                                }
                                                                                int[] iArr = new int[numArr.length];
                                                                                for (int i2 = 0; i2 < numArr.length; i2++) {
                                                                                    Integer num = numArr[i2];
                                                                                    iArr[i2] = num == null ? i : num.intValue();
                                                                                }
                                                                                return iArr;
                                                                            }

                                                                            public static Integer[] toObject(int[] iArr) {
                                                                                if (iArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (iArr.length == 0) {
                                                                                    return EMPTY_INTEGER_OBJECT_ARRAY;
                                                                                }
                                                                                Integer[] numArr = new Integer[iArr.length];
                                                                                for (int i = 0; i < iArr.length; i++) {
                                                                                    numArr[i] = Integer.valueOf(iArr[i]);
                                                                                }
                                                                                return numArr;
                                                                            }

                                                                            public static short[] toPrimitive(Short[] shArr) {
                                                                                if (shArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (shArr.length == 0) {
                                                                                    return EMPTY_SHORT_ARRAY;
                                                                                }
                                                                                short[] sArr = new short[shArr.length];
                                                                                for (int i = 0; i < shArr.length; i++) {
                                                                                    sArr[i] = shArr[i].shortValue();
                                                                                }
                                                                                return sArr;
                                                                            }

                                                                            public static short[] toPrimitive(Short[] shArr, short s) {
                                                                                if (shArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (shArr.length == 0) {
                                                                                    return EMPTY_SHORT_ARRAY;
                                                                                }
                                                                                short[] sArr = new short[shArr.length];
                                                                                for (int i = 0; i < shArr.length; i++) {
                                                                                    Short sh = shArr[i];
                                                                                    sArr[i] = sh == null ? s : sh.shortValue();
                                                                                }
                                                                                return sArr;
                                                                            }

                                                                            public static Short[] toObject(short[] sArr) {
                                                                                if (sArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (sArr.length == 0) {
                                                                                    return EMPTY_SHORT_OBJECT_ARRAY;
                                                                                }
                                                                                Short[] shArr = new Short[sArr.length];
                                                                                for (int i = 0; i < sArr.length; i++) {
                                                                                    shArr[i] = Short.valueOf(sArr[i]);
                                                                                }
                                                                                return shArr;
                                                                            }

                                                                            public static byte[] toPrimitive(Byte[] bArr) {
                                                                                if (bArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (bArr.length == 0) {
                                                                                    return EMPTY_BYTE_ARRAY;
                                                                                }
                                                                                byte[] bArr2 = new byte[bArr.length];
                                                                                for (int i = 0; i < bArr.length; i++) {
                                                                                    bArr2[i] = bArr[i].byteValue();
                                                                                }
                                                                                return bArr2;
                                                                            }

                                                                            public static byte[] toPrimitive(Byte[] bArr, byte b) {
                                                                                if (bArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (bArr.length == 0) {
                                                                                    return EMPTY_BYTE_ARRAY;
                                                                                }
                                                                                byte[] bArr2 = new byte[bArr.length];
                                                                                for (int i = 0; i < bArr.length; i++) {
                                                                                    Byte b2 = bArr[i];
                                                                                    bArr2[i] = b2 == null ? b : b2.byteValue();
                                                                                }
                                                                                return bArr2;
                                                                            }

                                                                            public static Byte[] toObject(byte[] bArr) {
                                                                                if (bArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (bArr.length == 0) {
                                                                                    return EMPTY_BYTE_OBJECT_ARRAY;
                                                                                }
                                                                                Byte[] bArr2 = new Byte[bArr.length];
                                                                                for (int i = 0; i < bArr.length; i++) {
                                                                                    bArr2[i] = Byte.valueOf(bArr[i]);
                                                                                }
                                                                                return bArr2;
                                                                            }

                                                                            public static double[] toPrimitive(Double[] dArr) {
                                                                                if (dArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (dArr.length == 0) {
                                                                                    return EMPTY_DOUBLE_ARRAY;
                                                                                }
                                                                                double[] dArr2 = new double[dArr.length];
                                                                                for (int i = 0; i < dArr.length; i++) {
                                                                                    dArr2[i] = dArr[i].doubleValue();
                                                                                }
                                                                                return dArr2;
                                                                            }

                                                                            public static double[] toPrimitive(Double[] dArr, double d) {
                                                                                if (dArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (dArr.length == 0) {
                                                                                    return EMPTY_DOUBLE_ARRAY;
                                                                                }
                                                                                double[] dArr2 = new double[dArr.length];
                                                                                for (int i = 0; i < dArr.length; i++) {
                                                                                    Double d2 = dArr[i];
                                                                                    dArr2[i] = d2 == null ? d : d2.doubleValue();
                                                                                }
                                                                                return dArr2;
                                                                            }

                                                                            public static Double[] toObject(double[] dArr) {
                                                                                if (dArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (dArr.length == 0) {
                                                                                    return EMPTY_DOUBLE_OBJECT_ARRAY;
                                                                                }
                                                                                Double[] dArr2 = new Double[dArr.length];
                                                                                for (int i = 0; i < dArr.length; i++) {
                                                                                    dArr2[i] = Double.valueOf(dArr[i]);
                                                                                }
                                                                                return dArr2;
                                                                            }

                                                                            public static float[] toPrimitive(Float[] fArr) {
                                                                                if (fArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (fArr.length == 0) {
                                                                                    return EMPTY_FLOAT_ARRAY;
                                                                                }
                                                                                float[] fArr2 = new float[fArr.length];
                                                                                for (int i = 0; i < fArr.length; i++) {
                                                                                    fArr2[i] = fArr[i].floatValue();
                                                                                }
                                                                                return fArr2;
                                                                            }

                                                                            public static float[] toPrimitive(Float[] fArr, float f) {
                                                                                if (fArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (fArr.length == 0) {
                                                                                    return EMPTY_FLOAT_ARRAY;
                                                                                }
                                                                                float[] fArr2 = new float[fArr.length];
                                                                                for (int i = 0; i < fArr.length; i++) {
                                                                                    Float f2 = fArr[i];
                                                                                    fArr2[i] = f2 == null ? f : f2.floatValue();
                                                                                }
                                                                                return fArr2;
                                                                            }

                                                                            public static Float[] toObject(float[] fArr) {
                                                                                if (fArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (fArr.length == 0) {
                                                                                    return EMPTY_FLOAT_OBJECT_ARRAY;
                                                                                }
                                                                                Float[] fArr2 = new Float[fArr.length];
                                                                                for (int i = 0; i < fArr.length; i++) {
                                                                                    fArr2[i] = Float.valueOf(fArr[i]);
                                                                                }
                                                                                return fArr2;
                                                                            }

                                                                            public static boolean[] toPrimitive(Boolean[] boolArr) {
                                                                                if (boolArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (boolArr.length == 0) {
                                                                                    return EMPTY_BOOLEAN_ARRAY;
                                                                                }
                                                                                boolean[] zArr = new boolean[boolArr.length];
                                                                                for (int i = 0; i < boolArr.length; i++) {
                                                                                    zArr[i] = boolArr[i].booleanValue();
                                                                                }
                                                                                return zArr;
                                                                            }

                                                                            public static boolean[] toPrimitive(Boolean[] boolArr, boolean z) {
                                                                                if (boolArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (boolArr.length == 0) {
                                                                                    return EMPTY_BOOLEAN_ARRAY;
                                                                                }
                                                                                boolean[] zArr = new boolean[boolArr.length];
                                                                                for (int i = 0; i < boolArr.length; i++) {
                                                                                    Boolean bool = boolArr[i];
                                                                                    zArr[i] = bool == null ? z : bool.booleanValue();
                                                                                }
                                                                                return zArr;
                                                                            }

                                                                            public static Boolean[] toObject(boolean[] zArr) {
                                                                                if (zArr == null) {
                                                                                    return null;
                                                                                }
                                                                                if (zArr.length == 0) {
                                                                                    return EMPTY_BOOLEAN_OBJECT_ARRAY;
                                                                                }
                                                                                Boolean[] boolArr = new Boolean[zArr.length];
                                                                                for (int i = 0; i < zArr.length; i++) {
                                                                                    boolArr[i] = zArr[i] ? Boolean.TRUE : Boolean.FALSE;
                                                                                }
                                                                                return boolArr;
                                                                            }

                                                                            public static boolean isEmpty(Object[] objArr) {
                                                                                return objArr == null || objArr.length == 0;
                                                                            }

                                                                            public static boolean isEmpty(long[] jArr) {
                                                                                return jArr == null || jArr.length == 0;
                                                                            }

                                                                            public static boolean isEmpty(int[] iArr) {
                                                                                return iArr == null || iArr.length == 0;
                                                                            }

                                                                            public static boolean isEmpty(short[] sArr) {
                                                                                return sArr == null || sArr.length == 0;
                                                                            }

                                                                            public static boolean isEmpty(char[] cArr) {
                                                                                return cArr == null || cArr.length == 0;
                                                                            }

                                                                            public static boolean isEmpty(byte[] bArr) {
                                                                                return bArr == null || bArr.length == 0;
                                                                            }

                                                                            public static boolean isEmpty(double[] dArr) {
                                                                                return dArr == null || dArr.length == 0;
                                                                            }

                                                                            public static boolean isEmpty(float[] fArr) {
                                                                                return fArr == null || fArr.length == 0;
                                                                            }

                                                                            public static boolean isEmpty(boolean[] zArr) {
                                                                                return zArr == null || zArr.length == 0;
                                                                            }

                                                                            public static <T> boolean isNotEmpty(T[] tArr) {
                                                                                return (tArr == null || tArr.length == 0) ? false : true;
                                                                            }

                                                                            public static boolean isNotEmpty(long[] jArr) {
                                                                                return (jArr == null || jArr.length == 0) ? false : true;
                                                                            }

                                                                            public static boolean isNotEmpty(int[] iArr) {
                                                                                return (iArr == null || iArr.length == 0) ? false : true;
                                                                            }

                                                                            public static boolean isNotEmpty(short[] sArr) {
                                                                                return (sArr == null || sArr.length == 0) ? false : true;
                                                                            }

                                                                            public static boolean isNotEmpty(char[] cArr) {
                                                                                return (cArr == null || cArr.length == 0) ? false : true;
                                                                            }

                                                                            public static boolean isNotEmpty(byte[] bArr) {
                                                                                return (bArr == null || bArr.length == 0) ? false : true;
                                                                            }

                                                                            public static boolean isNotEmpty(double[] dArr) {
                                                                                return (dArr == null || dArr.length == 0) ? false : true;
                                                                            }

                                                                            public static boolean isNotEmpty(float[] fArr) {
                                                                                return (fArr == null || fArr.length == 0) ? false : true;
                                                                            }

                                                                            public static boolean isNotEmpty(boolean[] zArr) {
                                                                                return (zArr == null || zArr.length == 0) ? false : true;
                                                                            }

                                                                            public static <T> T[] addAll(T[] tArr, T... tArr2) {
                                                                                if (tArr == null) {
                                                                                    return clone((Object[]) tArr2);
                                                                                }
                                                                                if (tArr2 == null) {
                                                                                    return clone((Object[]) tArr);
                                                                                }
                                                                                Class componentType = tArr.getClass().getComponentType();
                                                                                Object[] objArr = (Object[]) Array.newInstance(componentType, tArr.length + tArr2.length);
                                                                                System.arraycopy(tArr, 0, objArr, 0, tArr.length);
                                                                                try {
                                                                                    System.arraycopy(tArr2, 0, objArr, tArr.length, tArr2.length);
                                                                                    return objArr;
                                                                                } catch (Throwable e) {
                                                                                    Class componentType2 = tArr2.getClass().getComponentType();
                                                                                    if (componentType.isAssignableFrom(componentType2)) {
                                                                                        throw e;
                                                                                    }
                                                                                    throw new IllegalArgumentException("Cannot store " + componentType2.getName() + " in an array of " + componentType.getName(), e);
                                                                                }
                                                                            }

                                                                            public static boolean[] addAll(boolean[] zArr, boolean... zArr2) {
                                                                                if (zArr == null) {
                                                                                    return clone(zArr2);
                                                                                }
                                                                                if (zArr2 == null) {
                                                                                    return clone(zArr);
                                                                                }
                                                                                boolean[] zArr3 = new boolean[(zArr.length + zArr2.length)];
                                                                                System.arraycopy(zArr, 0, zArr3, 0, zArr.length);
                                                                                System.arraycopy(zArr2, 0, zArr3, zArr.length, zArr2.length);
                                                                                return zArr3;
                                                                            }

                                                                            public static char[] addAll(char[] cArr, char... cArr2) {
                                                                                if (cArr == null) {
                                                                                    return clone(cArr2);
                                                                                }
                                                                                if (cArr2 == null) {
                                                                                    return clone(cArr);
                                                                                }
                                                                                char[] cArr3 = new char[(cArr.length + cArr2.length)];
                                                                                System.arraycopy(cArr, 0, cArr3, 0, cArr.length);
                                                                                System.arraycopy(cArr2, 0, cArr3, cArr.length, cArr2.length);
                                                                                return cArr3;
                                                                            }

                                                                            public static byte[] addAll(byte[] bArr, byte... bArr2) {
                                                                                if (bArr == null) {
                                                                                    return clone(bArr2);
                                                                                }
                                                                                if (bArr2 == null) {
                                                                                    return clone(bArr);
                                                                                }
                                                                                byte[] bArr3 = new byte[(bArr.length + bArr2.length)];
                                                                                System.arraycopy(bArr, 0, bArr3, 0, bArr.length);
                                                                                System.arraycopy(bArr2, 0, bArr3, bArr.length, bArr2.length);
                                                                                return bArr3;
                                                                            }

                                                                            public static short[] addAll(short[] sArr, short... sArr2) {
                                                                                if (sArr == null) {
                                                                                    return clone(sArr2);
                                                                                }
                                                                                if (sArr2 == null) {
                                                                                    return clone(sArr);
                                                                                }
                                                                                short[] sArr3 = new short[(sArr.length + sArr2.length)];
                                                                                System.arraycopy(sArr, 0, sArr3, 0, sArr.length);
                                                                                System.arraycopy(sArr2, 0, sArr3, sArr.length, sArr2.length);
                                                                                return sArr3;
                                                                            }

                                                                            public static int[] addAll(int[] iArr, int... iArr2) {
                                                                                if (iArr == null) {
                                                                                    return clone(iArr2);
                                                                                }
                                                                                if (iArr2 == null) {
                                                                                    return clone(iArr);
                                                                                }
                                                                                int[] iArr3 = new int[(iArr.length + iArr2.length)];
                                                                                System.arraycopy(iArr, 0, iArr3, 0, iArr.length);
                                                                                System.arraycopy(iArr2, 0, iArr3, iArr.length, iArr2.length);
                                                                                return iArr3;
                                                                            }

                                                                            public static long[] addAll(long[] jArr, long... jArr2) {
                                                                                if (jArr == null) {
                                                                                    return clone(jArr2);
                                                                                }
                                                                                if (jArr2 == null) {
                                                                                    return clone(jArr);
                                                                                }
                                                                                long[] jArr3 = new long[(jArr.length + jArr2.length)];
                                                                                System.arraycopy(jArr, 0, jArr3, 0, jArr.length);
                                                                                System.arraycopy(jArr2, 0, jArr3, jArr.length, jArr2.length);
                                                                                return jArr3;
                                                                            }

                                                                            public static float[] addAll(float[] fArr, float... fArr2) {
                                                                                if (fArr == null) {
                                                                                    return clone(fArr2);
                                                                                }
                                                                                if (fArr2 == null) {
                                                                                    return clone(fArr);
                                                                                }
                                                                                float[] fArr3 = new float[(fArr.length + fArr2.length)];
                                                                                System.arraycopy(fArr, 0, fArr3, 0, fArr.length);
                                                                                System.arraycopy(fArr2, 0, fArr3, fArr.length, fArr2.length);
                                                                                return fArr3;
                                                                            }

                                                                            public static double[] addAll(double[] dArr, double... dArr2) {
                                                                                if (dArr == null) {
                                                                                    return clone(dArr2);
                                                                                }
                                                                                if (dArr2 == null) {
                                                                                    return clone(dArr);
                                                                                }
                                                                                double[] dArr3 = new double[(dArr.length + dArr2.length)];
                                                                                System.arraycopy(dArr, 0, dArr3, 0, dArr.length);
                                                                                System.arraycopy(dArr2, 0, dArr3, dArr.length, dArr2.length);
                                                                                return dArr3;
                                                                            }

                                                                            public static <T> T[] add(T[] tArr, T t) {
                                                                                Class componentType;
                                                                                if (tArr != null) {
                                                                                    componentType = tArr.getClass().getComponentType();
                                                                                } else if (t != null) {
                                                                                    componentType = t.getClass();
                                                                                } else {
                                                                                    throw new IllegalArgumentException("Arguments cannot both be null");
                                                                                }
                                                                                Object[] objArr = (Object[]) copyArrayGrow1(tArr, componentType);
                                                                                objArr[objArr.length - 1] = t;
                                                                                return objArr;
                                                                            }

                                                                            public static boolean[] add(boolean[] zArr, boolean z) {
                                                                                boolean[] zArr2 = (boolean[]) copyArrayGrow1(zArr, Boolean.TYPE);
                                                                                zArr2[zArr2.length - 1] = z;
                                                                                return zArr2;
                                                                            }

                                                                            public static byte[] add(byte[] bArr, byte b) {
                                                                                byte[] bArr2 = (byte[]) copyArrayGrow1(bArr, Byte.TYPE);
                                                                                bArr2[bArr2.length - 1] = b;
                                                                                return bArr2;
                                                                            }

                                                                            public static char[] add(char[] cArr, char c) {
                                                                                char[] cArr2 = (char[]) copyArrayGrow1(cArr, Character.TYPE);
                                                                                cArr2[cArr2.length - 1] = c;
                                                                                return cArr2;
                                                                            }

                                                                            public static double[] add(double[] dArr, double d) {
                                                                                double[] dArr2 = (double[]) copyArrayGrow1(dArr, Double.TYPE);
                                                                                dArr2[dArr2.length - 1] = d;
                                                                                return dArr2;
                                                                            }

                                                                            public static float[] add(float[] fArr, float f) {
                                                                                float[] fArr2 = (float[]) copyArrayGrow1(fArr, Float.TYPE);
                                                                                fArr2[fArr2.length - 1] = f;
                                                                                return fArr2;
                                                                            }

                                                                            public static int[] add(int[] iArr, int i) {
                                                                                int[] iArr2 = (int[]) copyArrayGrow1(iArr, Integer.TYPE);
                                                                                iArr2[iArr2.length - 1] = i;
                                                                                return iArr2;
                                                                            }

                                                                            public static long[] add(long[] jArr, long j) {
                                                                                long[] jArr2 = (long[]) copyArrayGrow1(jArr, Long.TYPE);
                                                                                jArr2[jArr2.length - 1] = j;
                                                                                return jArr2;
                                                                            }

                                                                            public static short[] add(short[] sArr, short s) {
                                                                                short[] sArr2 = (short[]) copyArrayGrow1(sArr, Short.TYPE);
                                                                                sArr2[sArr2.length - 1] = s;
                                                                                return sArr2;
                                                                            }

                                                                            private static Object copyArrayGrow1(Object obj, Class<?> cls) {
                                                                                if (obj == null) {
                                                                                    return Array.newInstance(cls, 1);
                                                                                }
                                                                                int length = Array.getLength(obj);
                                                                                Object newInstance = Array.newInstance(obj.getClass().getComponentType(), length + 1);
                                                                                System.arraycopy(obj, 0, newInstance, 0, length);
                                                                                return newInstance;
                                                                            }

                                                                            public static <T> T[] add(T[] tArr, int i, T t) {
                                                                                Class componentType;
                                                                                if (tArr != null) {
                                                                                    componentType = tArr.getClass().getComponentType();
                                                                                } else if (t != null) {
                                                                                    componentType = t.getClass();
                                                                                } else {
                                                                                    throw new IllegalArgumentException("Array and element cannot both be null");
                                                                                }
                                                                                return (Object[]) add(tArr, i, t, componentType);
                                                                            }

                                                                            public static boolean[] add(boolean[] zArr, int i, boolean z) {
                                                                                return (boolean[]) add(zArr, i, Boolean.valueOf(z), Boolean.TYPE);
                                                                            }

                                                                            public static char[] add(char[] cArr, int i, char c) {
                                                                                return (char[]) add(cArr, i, Character.valueOf(c), Character.TYPE);
                                                                            }

                                                                            public static byte[] add(byte[] bArr, int i, byte b) {
                                                                                return (byte[]) add(bArr, i, Byte.valueOf(b), Byte.TYPE);
                                                                            }

                                                                            public static short[] add(short[] sArr, int i, short s) {
                                                                                return (short[]) add(sArr, i, Short.valueOf(s), Short.TYPE);
                                                                            }

                                                                            public static int[] add(int[] iArr, int i, int i2) {
                                                                                return (int[]) add(iArr, i, Integer.valueOf(i2), Integer.TYPE);
                                                                            }

                                                                            public static long[] add(long[] jArr, int i, long j) {
                                                                                return (long[]) add(jArr, i, Long.valueOf(j), Long.TYPE);
                                                                            }

                                                                            public static float[] add(float[] fArr, int i, float f) {
                                                                                return (float[]) add(fArr, i, Float.valueOf(f), Float.TYPE);
                                                                            }

                                                                            public static double[] add(double[] dArr, int i, double d) {
                                                                                return (double[]) add(dArr, i, Double.valueOf(d), Double.TYPE);
                                                                            }

                                                                            private static Object add(Object obj, int i, Object obj2, Class<?> cls) {
                                                                                Object newInstance;
                                                                                if (obj != null) {
                                                                                    int length = Array.getLength(obj);
                                                                                    if (i > length || i < 0) {
                                                                                        throw new IndexOutOfBoundsException("Index: " + i + ", Length: " + length);
                                                                                    }
                                                                                    newInstance = Array.newInstance(cls, length + 1);
                                                                                    System.arraycopy(obj, 0, newInstance, 0, i);
                                                                                    Array.set(newInstance, i, obj2);
                                                                                    if (i >= length) {
                                                                                        return newInstance;
                                                                                    }
                                                                                    System.arraycopy(obj, i, newInstance, i + 1, length - i);
                                                                                    return newInstance;
                                                                                } else if (i != 0) {
                                                                                    throw new IndexOutOfBoundsException("Index: " + i + ", Length: 0");
                                                                                } else {
                                                                                    newInstance = Array.newInstance(cls, 1);
                                                                                    Array.set(newInstance, 0, obj2);
                                                                                    return newInstance;
                                                                                }
                                                                            }

                                                                            public static <T> T[] remove(T[] tArr, int i) {
                                                                                return (Object[]) remove((Object) tArr, i);
                                                                            }

                                                                            public static <T> T[] removeElement(T[] tArr, Object obj) {
                                                                                int indexOf = indexOf((Object[]) tArr, obj);
                                                                                if (indexOf == -1) {
                                                                                    return clone((Object[]) tArr);
                                                                                }
                                                                                return remove((Object[]) tArr, indexOf);
                                                                            }

                                                                            public static boolean[] remove(boolean[] zArr, int i) {
                                                                                return (boolean[]) remove((Object) zArr, i);
                                                                            }

                                                                            public static boolean[] removeElement(boolean[] zArr, boolean z) {
                                                                                int indexOf = indexOf(zArr, z);
                                                                                if (indexOf == -1) {
                                                                                    return clone(zArr);
                                                                                }
                                                                                return remove(zArr, indexOf);
                                                                            }

                                                                            public static byte[] remove(byte[] bArr, int i) {
                                                                                return (byte[]) remove((Object) bArr, i);
                                                                            }

                                                                            public static byte[] removeElement(byte[] bArr, byte b) {
                                                                                int indexOf = indexOf(bArr, b);
                                                                                if (indexOf == -1) {
                                                                                    return clone(bArr);
                                                                                }
                                                                                return remove(bArr, indexOf);
                                                                            }

                                                                            public static char[] remove(char[] cArr, int i) {
                                                                                return (char[]) remove((Object) cArr, i);
                                                                            }

                                                                            public static char[] removeElement(char[] cArr, char c) {
                                                                                int indexOf = indexOf(cArr, c);
                                                                                if (indexOf == -1) {
                                                                                    return clone(cArr);
                                                                                }
                                                                                return remove(cArr, indexOf);
                                                                            }

                                                                            public static double[] remove(double[] dArr, int i) {
                                                                                return (double[]) remove((Object) dArr, i);
                                                                            }

                                                                            public static double[] removeElement(double[] dArr, double d) {
                                                                                int indexOf = indexOf(dArr, d);
                                                                                if (indexOf == -1) {
                                                                                    return clone(dArr);
                                                                                }
                                                                                return remove(dArr, indexOf);
                                                                            }

                                                                            public static float[] remove(float[] fArr, int i) {
                                                                                return (float[]) remove((Object) fArr, i);
                                                                            }

                                                                            public static float[] removeElement(float[] fArr, float f) {
                                                                                int indexOf = indexOf(fArr, f);
                                                                                if (indexOf == -1) {
                                                                                    return clone(fArr);
                                                                                }
                                                                                return remove(fArr, indexOf);
                                                                            }

                                                                            public static int[] remove(int[] iArr, int i) {
                                                                                return (int[]) remove((Object) iArr, i);
                                                                            }

                                                                            public static int[] removeElement(int[] iArr, int i) {
                                                                                int indexOf = indexOf(iArr, i);
                                                                                if (indexOf == -1) {
                                                                                    return clone(iArr);
                                                                                }
                                                                                return remove(iArr, indexOf);
                                                                            }

                                                                            public static long[] remove(long[] jArr, int i) {
                                                                                return (long[]) remove((Object) jArr, i);
                                                                            }

                                                                            public static long[] removeElement(long[] jArr, long j) {
                                                                                int indexOf = indexOf(jArr, j);
                                                                                if (indexOf == -1) {
                                                                                    return clone(jArr);
                                                                                }
                                                                                return remove(jArr, indexOf);
                                                                            }

                                                                            public static short[] remove(short[] sArr, int i) {
                                                                                return (short[]) remove((Object) sArr, i);
                                                                            }

                                                                            public static short[] removeElement(short[] sArr, short s) {
                                                                                int indexOf = indexOf(sArr, s);
                                                                                if (indexOf == -1) {
                                                                                    return clone(sArr);
                                                                                }
                                                                                return remove(sArr, indexOf);
                                                                            }

                                                                            private static Object remove(Object obj, int i) {
                                                                                int length = getLength(obj);
                                                                                if (i < 0 || i >= length) {
                                                                                    throw new IndexOutOfBoundsException("Index: " + i + ", Length: " + length);
                                                                                }
                                                                                Object newInstance = Array.newInstance(obj.getClass().getComponentType(), length - 1);
                                                                                System.arraycopy(obj, 0, newInstance, 0, i);
                                                                                if (i < length - 1) {
                                                                                    System.arraycopy(obj, i + 1, newInstance, i, (length - i) - 1);
                                                                                }
                                                                                return newInstance;
                                                                            }

                                                                            public static <T> T[] removeAll(T[] tArr, int... iArr) {
                                                                                return (Object[]) removeAll((Object) tArr, clone(iArr));
                                                                            }

                                                                            public static <T> T[] removeElements(T[] tArr, T... tArr2) {
                                                                                if (isEmpty((Object[]) tArr) || isEmpty((Object[]) tArr2)) {
                                                                                    return clone((Object[]) tArr);
                                                                                }
                                                                                int i;
                                                                                HashMap hashMap = new HashMap(tArr2.length);
                                                                                for (Object obj : tArr2) {
                                                                                    Object obj2;
                                                                                    MutableInt mutableInt = (MutableInt) hashMap.get(obj2);
                                                                                    if (mutableInt == null) {
                                                                                        hashMap.put(obj2, new MutableInt(1));
                                                                                    } else {
                                                                                        mutableInt.increment();
                                                                                    }
                                                                                }
                                                                                BitSet bitSet = new BitSet();
                                                                                for (Entry entry : hashMap.entrySet()) {
                                                                                    obj2 = entry.getKey();
                                                                                    int intValue = ((MutableInt) entry.getValue()).intValue();
                                                                                    i = 0;
                                                                                    for (int i2 = 0; i2 < intValue; i2++) {
                                                                                        int indexOf = indexOf((Object[]) tArr, obj2, i);
                                                                                        if (indexOf < 0) {
                                                                                            break;
                                                                                        }
                                                                                        i = indexOf + 1;
                                                                                        bitSet.set(indexOf);
                                                                                    }
                                                                                }
                                                                                return (Object[]) removeAll((Object) tArr, bitSet);
                                                                            }

                                                                            public static byte[] removeAll(byte[] bArr, int... iArr) {
                                                                                return (byte[]) removeAll((Object) bArr, clone(iArr));
                                                                            }

                                                                            public static byte[] removeElements(byte[] bArr, byte... bArr2) {
                                                                                if (isEmpty(bArr) || isEmpty(bArr2)) {
                                                                                    return clone(bArr);
                                                                                }
                                                                                int i;
                                                                                Map hashMap = new HashMap(bArr2.length);
                                                                                for (byte valueOf : bArr2) {
                                                                                    Byte valueOf2 = Byte.valueOf(valueOf);
                                                                                    MutableInt mutableInt = (MutableInt) hashMap.get(valueOf2);
                                                                                    if (mutableInt == null) {
                                                                                        hashMap.put(valueOf2, new MutableInt(1));
                                                                                    } else {
                                                                                        mutableInt.increment();
                                                                                    }
                                                                                }
                                                                                BitSet bitSet = new BitSet();
                                                                                for (Entry entry : hashMap.entrySet()) {
                                                                                    Byte b = (Byte) entry.getKey();
                                                                                    int intValue = ((MutableInt) entry.getValue()).intValue();
                                                                                    int i2 = 0;
                                                                                    for (i = 0; i < intValue; i++) {
                                                                                        int indexOf = indexOf(bArr, b.byteValue(), i2);
                                                                                        if (indexOf < 0) {
                                                                                            break;
                                                                                        }
                                                                                        i2 = indexOf + 1;
                                                                                        bitSet.set(indexOf);
                                                                                    }
                                                                                }
                                                                                return (byte[]) removeAll((Object) bArr, bitSet);
                                                                            }

                                                                            public static short[] removeAll(short[] sArr, int... iArr) {
                                                                                return (short[]) removeAll((Object) sArr, clone(iArr));
                                                                            }

                                                                            public static short[] removeElements(short[] sArr, short... sArr2) {
                                                                                if (isEmpty(sArr) || isEmpty(sArr2)) {
                                                                                    return clone(sArr);
                                                                                }
                                                                                int i;
                                                                                HashMap hashMap = new HashMap(sArr2.length);
                                                                                for (short valueOf : sArr2) {
                                                                                    Short valueOf2 = Short.valueOf(valueOf);
                                                                                    MutableInt mutableInt = (MutableInt) hashMap.get(valueOf2);
                                                                                    if (mutableInt == null) {
                                                                                        hashMap.put(valueOf2, new MutableInt(1));
                                                                                    } else {
                                                                                        mutableInt.increment();
                                                                                    }
                                                                                }
                                                                                BitSet bitSet = new BitSet();
                                                                                for (Entry entry : hashMap.entrySet()) {
                                                                                    Short sh = (Short) entry.getKey();
                                                                                    int intValue = ((MutableInt) entry.getValue()).intValue();
                                                                                    int i2 = 0;
                                                                                    for (i = 0; i < intValue; i++) {
                                                                                        int indexOf = indexOf(sArr, sh.shortValue(), i2);
                                                                                        if (indexOf < 0) {
                                                                                            break;
                                                                                        }
                                                                                        i2 = indexOf + 1;
                                                                                        bitSet.set(indexOf);
                                                                                    }
                                                                                }
                                                                                return (short[]) removeAll((Object) sArr, bitSet);
                                                                            }

                                                                            public static int[] removeAll(int[] iArr, int... iArr2) {
                                                                                return (int[]) removeAll((Object) iArr, clone(iArr2));
                                                                            }

                                                                            public static int[] removeElements(int[] iArr, int... iArr2) {
                                                                                if (isEmpty(iArr) || isEmpty(iArr2)) {
                                                                                    return clone(iArr);
                                                                                }
                                                                                int i;
                                                                                HashMap hashMap = new HashMap(iArr2.length);
                                                                                for (int valueOf : iArr2) {
                                                                                    Integer valueOf2 = Integer.valueOf(valueOf);
                                                                                    MutableInt mutableInt = (MutableInt) hashMap.get(valueOf2);
                                                                                    if (mutableInt == null) {
                                                                                        hashMap.put(valueOf2, new MutableInt(1));
                                                                                    } else {
                                                                                        mutableInt.increment();
                                                                                    }
                                                                                }
                                                                                BitSet bitSet = new BitSet();
                                                                                for (Entry entry : hashMap.entrySet()) {
                                                                                    Integer num = (Integer) entry.getKey();
                                                                                    int intValue = ((MutableInt) entry.getValue()).intValue();
                                                                                    int i2 = 0;
                                                                                    for (i = 0; i < intValue; i++) {
                                                                                        int indexOf = indexOf(iArr, num.intValue(), i2);
                                                                                        if (indexOf < 0) {
                                                                                            break;
                                                                                        }
                                                                                        i2 = indexOf + 1;
                                                                                        bitSet.set(indexOf);
                                                                                    }
                                                                                }
                                                                                return (int[]) removeAll((Object) iArr, bitSet);
                                                                            }

                                                                            public static char[] removeAll(char[] cArr, int... iArr) {
                                                                                return (char[]) removeAll((Object) cArr, clone(iArr));
                                                                            }

                                                                            public static char[] removeElements(char[] cArr, char... cArr2) {
                                                                                if (isEmpty(cArr) || isEmpty(cArr2)) {
                                                                                    return clone(cArr);
                                                                                }
                                                                                int i;
                                                                                HashMap hashMap = new HashMap(cArr2.length);
                                                                                for (char valueOf : cArr2) {
                                                                                    Character valueOf2 = Character.valueOf(valueOf);
                                                                                    MutableInt mutableInt = (MutableInt) hashMap.get(valueOf2);
                                                                                    if (mutableInt == null) {
                                                                                        hashMap.put(valueOf2, new MutableInt(1));
                                                                                    } else {
                                                                                        mutableInt.increment();
                                                                                    }
                                                                                }
                                                                                BitSet bitSet = new BitSet();
                                                                                for (Entry entry : hashMap.entrySet()) {
                                                                                    Character ch = (Character) entry.getKey();
                                                                                    int intValue = ((MutableInt) entry.getValue()).intValue();
                                                                                    int i2 = 0;
                                                                                    for (i = 0; i < intValue; i++) {
                                                                                        int indexOf = indexOf(cArr, ch.charValue(), i2);
                                                                                        if (indexOf < 0) {
                                                                                            break;
                                                                                        }
                                                                                        i2 = indexOf + 1;
                                                                                        bitSet.set(indexOf);
                                                                                    }
                                                                                }
                                                                                return (char[]) removeAll((Object) cArr, bitSet);
                                                                            }

                                                                            public static long[] removeAll(long[] jArr, int... iArr) {
                                                                                return (long[]) removeAll((Object) jArr, clone(iArr));
                                                                            }

                                                                            public static long[] removeElements(long[] jArr, long... jArr2) {
                                                                                if (isEmpty(jArr) || isEmpty(jArr2)) {
                                                                                    return clone(jArr);
                                                                                }
                                                                                int i;
                                                                                HashMap hashMap = new HashMap(jArr2.length);
                                                                                for (long valueOf : jArr2) {
                                                                                    Long valueOf2 = Long.valueOf(valueOf);
                                                                                    MutableInt mutableInt = (MutableInt) hashMap.get(valueOf2);
                                                                                    if (mutableInt == null) {
                                                                                        hashMap.put(valueOf2, new MutableInt(1));
                                                                                    } else {
                                                                                        mutableInt.increment();
                                                                                    }
                                                                                }
                                                                                BitSet bitSet = new BitSet();
                                                                                for (Entry entry : hashMap.entrySet()) {
                                                                                    Long l = (Long) entry.getKey();
                                                                                    int intValue = ((MutableInt) entry.getValue()).intValue();
                                                                                    int i2 = 0;
                                                                                    for (i = 0; i < intValue; i++) {
                                                                                        int indexOf = indexOf(jArr, l.longValue(), i2);
                                                                                        if (indexOf < 0) {
                                                                                            break;
                                                                                        }
                                                                                        i2 = indexOf + 1;
                                                                                        bitSet.set(indexOf);
                                                                                    }
                                                                                }
                                                                                return (long[]) removeAll((Object) jArr, bitSet);
                                                                            }

                                                                            public static float[] removeAll(float[] fArr, int... iArr) {
                                                                                return (float[]) removeAll((Object) fArr, clone(iArr));
                                                                            }

                                                                            public static float[] removeElements(float[] fArr, float... fArr2) {
                                                                                if (isEmpty(fArr) || isEmpty(fArr2)) {
                                                                                    return clone(fArr);
                                                                                }
                                                                                int i;
                                                                                HashMap hashMap = new HashMap(fArr2.length);
                                                                                for (float valueOf : fArr2) {
                                                                                    Float valueOf2 = Float.valueOf(valueOf);
                                                                                    MutableInt mutableInt = (MutableInt) hashMap.get(valueOf2);
                                                                                    if (mutableInt == null) {
                                                                                        hashMap.put(valueOf2, new MutableInt(1));
                                                                                    } else {
                                                                                        mutableInt.increment();
                                                                                    }
                                                                                }
                                                                                BitSet bitSet = new BitSet();
                                                                                for (Entry entry : hashMap.entrySet()) {
                                                                                    Float f = (Float) entry.getKey();
                                                                                    int intValue = ((MutableInt) entry.getValue()).intValue();
                                                                                    int i2 = 0;
                                                                                    for (i = 0; i < intValue; i++) {
                                                                                        int indexOf = indexOf(fArr, f.floatValue(), i2);
                                                                                        if (indexOf < 0) {
                                                                                            break;
                                                                                        }
                                                                                        i2 = indexOf + 1;
                                                                                        bitSet.set(indexOf);
                                                                                    }
                                                                                }
                                                                                return (float[]) removeAll((Object) fArr, bitSet);
                                                                            }

                                                                            public static double[] removeAll(double[] dArr, int... iArr) {
                                                                                return (double[]) removeAll((Object) dArr, clone(iArr));
                                                                            }

                                                                            public static double[] removeElements(double[] dArr, double... dArr2) {
                                                                                if (isEmpty(dArr) || isEmpty(dArr2)) {
                                                                                    return clone(dArr);
                                                                                }
                                                                                int i;
                                                                                HashMap hashMap = new HashMap(dArr2.length);
                                                                                for (double valueOf : dArr2) {
                                                                                    Double valueOf2 = Double.valueOf(valueOf);
                                                                                    MutableInt mutableInt = (MutableInt) hashMap.get(valueOf2);
                                                                                    if (mutableInt == null) {
                                                                                        hashMap.put(valueOf2, new MutableInt(1));
                                                                                    } else {
                                                                                        mutableInt.increment();
                                                                                    }
                                                                                }
                                                                                BitSet bitSet = new BitSet();
                                                                                for (Entry entry : hashMap.entrySet()) {
                                                                                    Double d = (Double) entry.getKey();
                                                                                    int intValue = ((MutableInt) entry.getValue()).intValue();
                                                                                    int i2 = 0;
                                                                                    for (i = 0; i < intValue; i++) {
                                                                                        int indexOf = indexOf(dArr, d.doubleValue(), i2);
                                                                                        if (indexOf < 0) {
                                                                                            break;
                                                                                        }
                                                                                        i2 = indexOf + 1;
                                                                                        bitSet.set(indexOf);
                                                                                    }
                                                                                }
                                                                                return (double[]) removeAll((Object) dArr, bitSet);
                                                                            }

                                                                            public static boolean[] removeAll(boolean[] zArr, int... iArr) {
                                                                                return (boolean[]) removeAll((Object) zArr, clone(iArr));
                                                                            }

                                                                            public static boolean[] removeElements(boolean[] zArr, boolean... zArr2) {
                                                                                if (isEmpty(zArr) || isEmpty(zArr2)) {
                                                                                    return clone(zArr);
                                                                                }
                                                                                int i;
                                                                                HashMap hashMap = new HashMap(2);
                                                                                for (boolean valueOf : zArr2) {
                                                                                    Boolean valueOf2 = Boolean.valueOf(valueOf);
                                                                                    MutableInt mutableInt = (MutableInt) hashMap.get(valueOf2);
                                                                                    if (mutableInt == null) {
                                                                                        hashMap.put(valueOf2, new MutableInt(1));
                                                                                    } else {
                                                                                        mutableInt.increment();
                                                                                    }
                                                                                }
                                                                                BitSet bitSet = new BitSet();
                                                                                for (Entry entry : hashMap.entrySet()) {
                                                                                    Boolean bool = (Boolean) entry.getKey();
                                                                                    int intValue = ((MutableInt) entry.getValue()).intValue();
                                                                                    int i2 = 0;
                                                                                    for (i = 0; i < intValue; i++) {
                                                                                        int indexOf = indexOf(zArr, bool.booleanValue(), i2);
                                                                                        if (indexOf < 0) {
                                                                                            break;
                                                                                        }
                                                                                        i2 = indexOf + 1;
                                                                                        bitSet.set(indexOf);
                                                                                    }
                                                                                }
                                                                                return (boolean[]) removeAll((Object) zArr, bitSet);
                                                                            }

                                                                            static Object removeAll(Object obj, int... iArr) {
                                                                                int i;
                                                                                int i2;
                                                                                int length = getLength(obj);
                                                                                int length2;
                                                                                if (isNotEmpty(iArr)) {
                                                                                    Arrays.sort(iArr);
                                                                                    i = length;
                                                                                    length2 = iArr.length;
                                                                                    i2 = 0;
                                                                                    while (true) {
                                                                                        int i3 = length2 - 1;
                                                                                        if (i3 < 0) {
                                                                                            break;
                                                                                        }
                                                                                        length2 = iArr[i3];
                                                                                        if (length2 >= 0 && length2 < length) {
                                                                                            if (length2 >= i) {
                                                                                                length2 = i3;
                                                                                            } else {
                                                                                                i2++;
                                                                                                i = length2;
                                                                                                length2 = i3;
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                    throw new IndexOutOfBoundsException("Index: " + length2 + ", Length: " + length);
                                                                                }
                                                                                i2 = 0;
                                                                                Object newInstance = Array.newInstance(obj.getClass().getComponentType(), length - i2);
                                                                                if (i2 < length) {
                                                                                    i2 = length - i2;
                                                                                    i = iArr.length - 1;
                                                                                    while (i >= 0) {
                                                                                        length2 = iArr[i];
                                                                                        if (length - length2 > 1) {
                                                                                            length = (length - length2) - 1;
                                                                                            i2 -= length;
                                                                                            System.arraycopy(obj, length2 + 1, newInstance, i2, length);
                                                                                        }
                                                                                        i--;
                                                                                        length = length2;
                                                                                    }
                                                                                    if (length > 0) {
                                                                                        System.arraycopy(obj, 0, newInstance, 0, length);
                                                                                    }
                                                                                }
                                                                                return newInstance;
                                                                            }

                                                                            static Object removeAll(Object obj, BitSet bitSet) {
                                                                                int i = 0;
                                                                                int length = getLength(obj);
                                                                                Object newInstance = Array.newInstance(obj.getClass().getComponentType(), length - bitSet.cardinality());
                                                                                int i2 = 0;
                                                                                while (true) {
                                                                                    int nextSetBit = bitSet.nextSetBit(i2);
                                                                                    if (nextSetBit == -1) {
                                                                                        break;
                                                                                    }
                                                                                    int i3 = nextSetBit - i2;
                                                                                    if (i3 > 0) {
                                                                                        System.arraycopy(obj, i2, newInstance, i, i3);
                                                                                        i += i3;
                                                                                    }
                                                                                    i2 = bitSet.nextClearBit(nextSetBit);
                                                                                }
                                                                                length -= i2;
                                                                                if (length > 0) {
                                                                                    System.arraycopy(obj, i2, newInstance, i, length);
                                                                                }
                                                                                return newInstance;
                                                                            }

                                                                            public static <T extends Comparable<? super T>> boolean isSorted(T[] tArr) {
                                                                                return isSorted(tArr, new C15831());
                                                                            }

                                                                            public static <T> boolean isSorted(T[] tArr, Comparator<T> comparator) {
                                                                                if (comparator == null) {
                                                                                    throw new IllegalArgumentException("Comparator should not be null.");
                                                                                } else if (tArr == null || tArr.length < 2) {
                                                                                    return true;
                                                                                } else {
                                                                                    T t = tArr[0];
                                                                                    int length = tArr.length;
                                                                                    Object obj = t;
                                                                                    int i = 1;
                                                                                    while (i < length) {
                                                                                        Object obj2 = tArr[i];
                                                                                        if (comparator.compare(obj, obj2) > 0) {
                                                                                            return false;
                                                                                        }
                                                                                        i++;
                                                                                        obj = obj2;
                                                                                    }
                                                                                    return true;
                                                                                }
                                                                            }

                                                                            public static boolean isSorted(int[] iArr) {
                                                                                if (iArr == null || iArr.length < 2) {
                                                                                    return true;
                                                                                }
                                                                                int i = iArr[0];
                                                                                int length = iArr.length;
                                                                                int i2 = i;
                                                                                i = 1;
                                                                                while (i < length) {
                                                                                    int i3 = iArr[i];
                                                                                    if (NumberUtils.compare(i2, i3) > 0) {
                                                                                        return false;
                                                                                    }
                                                                                    i++;
                                                                                    i2 = i3;
                                                                                }
                                                                                return true;
                                                                            }

                                                                            public static boolean isSorted(long[] jArr) {
                                                                                if (jArr == null || jArr.length < 2) {
                                                                                    return true;
                                                                                }
                                                                                long j = jArr[0];
                                                                                int length = jArr.length;
                                                                                long j2 = j;
                                                                                int i = 1;
                                                                                while (i < length) {
                                                                                    long j3 = jArr[i];
                                                                                    if (NumberUtils.compare(j2, j3) > 0) {
                                                                                        return false;
                                                                                    }
                                                                                    i++;
                                                                                    j2 = j3;
                                                                                }
                                                                                return true;
                                                                            }

                                                                            public static boolean isSorted(short[] sArr) {
                                                                                if (sArr == null || sArr.length < 2) {
                                                                                    return true;
                                                                                }
                                                                                short s = sArr[0];
                                                                                int length = sArr.length;
                                                                                short s2 = s;
                                                                                int i = 1;
                                                                                while (i < length) {
                                                                                    short s3 = sArr[i];
                                                                                    if (NumberUtils.compare(s2, s3) > 0) {
                                                                                        return false;
                                                                                    }
                                                                                    i++;
                                                                                    s2 = s3;
                                                                                }
                                                                                return true;
                                                                            }

                                                                            public static boolean isSorted(double[] dArr) {
                                                                                if (dArr == null || dArr.length < 2) {
                                                                                    return true;
                                                                                }
                                                                                double d = dArr[0];
                                                                                int length = dArr.length;
                                                                                double d2 = d;
                                                                                int i = 1;
                                                                                while (i < length) {
                                                                                    double d3 = dArr[i];
                                                                                    if (Double.compare(d2, d3) > 0) {
                                                                                        return false;
                                                                                    }
                                                                                    i++;
                                                                                    d2 = d3;
                                                                                }
                                                                                return true;
                                                                            }

                                                                            public static boolean isSorted(float[] fArr) {
                                                                                if (fArr == null || fArr.length < 2) {
                                                                                    return true;
                                                                                }
                                                                                float f = fArr[0];
                                                                                int length = fArr.length;
                                                                                float f2 = f;
                                                                                int i = 1;
                                                                                while (i < length) {
                                                                                    float f3 = fArr[i];
                                                                                    if (Float.compare(f2, f3) > 0) {
                                                                                        return false;
                                                                                    }
                                                                                    i++;
                                                                                    f2 = f3;
                                                                                }
                                                                                return true;
                                                                            }

                                                                            public static boolean isSorted(byte[] bArr) {
                                                                                if (bArr == null || bArr.length < 2) {
                                                                                    return true;
                                                                                }
                                                                                byte b = bArr[0];
                                                                                int length = bArr.length;
                                                                                byte b2 = b;
                                                                                int i = 1;
                                                                                while (i < length) {
                                                                                    byte b3 = bArr[i];
                                                                                    if (NumberUtils.compare(b2, b3) > 0) {
                                                                                        return false;
                                                                                    }
                                                                                    i++;
                                                                                    b2 = b3;
                                                                                }
                                                                                return true;
                                                                            }

                                                                            public static boolean isSorted(char[] cArr) {
                                                                                if (cArr == null || cArr.length < 2) {
                                                                                    return true;
                                                                                }
                                                                                char c = cArr[0];
                                                                                int length = cArr.length;
                                                                                char c2 = c;
                                                                                int i = 1;
                                                                                while (i < length) {
                                                                                    char c3 = cArr[i];
                                                                                    if (CharUtils.compare(c2, c3) > 0) {
                                                                                        return false;
                                                                                    }
                                                                                    i++;
                                                                                    c2 = c3;
                                                                                }
                                                                                return true;
                                                                            }

                                                                            public static boolean isSorted(boolean[] zArr) {
                                                                                if (zArr == null || zArr.length < 2) {
                                                                                    return true;
                                                                                }
                                                                                boolean z = zArr[0];
                                                                                int length = zArr.length;
                                                                                boolean z2 = z;
                                                                                int i = 1;
                                                                                while (i < length) {
                                                                                    boolean z3 = zArr[i];
                                                                                    if (BooleanUtils.compare(z2, z3) > 0) {
                                                                                        return false;
                                                                                    }
                                                                                    i++;
                                                                                    z2 = z3;
                                                                                }
                                                                                return true;
                                                                            }
                                                                        }
