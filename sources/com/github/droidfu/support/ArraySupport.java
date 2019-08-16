package com.github.droidfu.support;

import java.lang.reflect.Array;

public class ArraySupport {
    public static <T> T[] delete(T[] tArr, int i) {
        int length = tArr.length;
        if (i < 0 || i >= length) {
            throw new IndexOutOfBoundsException("Index: " + i + ", Length: " + length);
        }
        T[] tArr2 = (Object[]) Array.newInstance(tArr.getClass().getComponentType(), length - 1);
        System.arraycopy(tArr, 0, tArr2, 0, i);
        if (i < length - 1) {
            System.arraycopy(tArr, i + 1, tArr2, i, (length - i) - 1);
        }
        return tArr2;
    }

    public static <T> int find(T[] tArr, T t) {
        for (int i = 0; i < tArr.length; i++) {
            if (tArr[i].equals(t)) {
                return i;
            }
        }
        return -1;
    }

    public static <T> T[] join(T[] tArr, T[] tArr2) {
        if (tArr == null) {
            return tArr2;
        }
        if (tArr2 == null) {
            return tArr;
        }
        Object[] objArr = (Object[]) Array.newInstance(tArr.getClass().getComponentType(), tArr.length + tArr2.length);
        System.arraycopy(tArr, 0, objArr, 0, tArr.length);
        System.arraycopy(tArr2, 0, objArr, tArr.length, tArr2.length);
        return objArr;
    }
}
