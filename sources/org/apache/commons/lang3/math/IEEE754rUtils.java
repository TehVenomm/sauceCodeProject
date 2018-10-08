package org.apache.commons.lang3.math;

import org.apache.commons.lang3.Validate;

public class IEEE754rUtils {
    public static double min(double... dArr) {
        int i = 1;
        if (dArr == null) {
            throw new IllegalArgumentException("The Array must not be null");
        }
        Validate.isTrue(dArr.length != 0, "Array cannot be empty.", new Object[0]);
        double d = dArr[0];
        while (i < dArr.length) {
            d = min(dArr[i], d);
            i++;
        }
        return d;
    }

    public static float min(float... fArr) {
        int i = 1;
        if (fArr == null) {
            throw new IllegalArgumentException("The Array must not be null");
        }
        boolean z;
        if (fArr.length != 0) {
            z = true;
        } else {
            z = false;
        }
        Validate.isTrue(z, "Array cannot be empty.", new Object[0]);
        float f = fArr[0];
        while (i < fArr.length) {
            f = min(fArr[i], f);
            i++;
        }
        return f;
    }

    public static double min(double d, double d2, double d3) {
        return min(min(d, d2), d3);
    }

    public static double min(double d, double d2) {
        if (Double.isNaN(d)) {
            return d2;
        }
        if (Double.isNaN(d2)) {
            return d;
        }
        return Math.min(d, d2);
    }

    public static float min(float f, float f2, float f3) {
        return min(min(f, f2), f3);
    }

    public static float min(float f, float f2) {
        if (Float.isNaN(f)) {
            return f2;
        }
        if (Float.isNaN(f2)) {
            return f;
        }
        return Math.min(f, f2);
    }

    public static double max(double... dArr) {
        int i = 1;
        if (dArr == null) {
            throw new IllegalArgumentException("The Array must not be null");
        }
        boolean z;
        if (dArr.length != 0) {
            z = true;
        } else {
            z = false;
        }
        Validate.isTrue(z, "Array cannot be empty.", new Object[0]);
        double d = dArr[0];
        while (i < dArr.length) {
            d = max(dArr[i], d);
            i++;
        }
        return d;
    }

    public static float max(float... fArr) {
        int i = 1;
        if (fArr == null) {
            throw new IllegalArgumentException("The Array must not be null");
        }
        boolean z;
        if (fArr.length != 0) {
            z = true;
        } else {
            z = false;
        }
        Validate.isTrue(z, "Array cannot be empty.", new Object[0]);
        float f = fArr[0];
        while (i < fArr.length) {
            f = max(fArr[i], f);
            i++;
        }
        return f;
    }

    public static double max(double d, double d2, double d3) {
        return max(max(d, d2), d3);
    }

    public static double max(double d, double d2) {
        if (Double.isNaN(d)) {
            return d2;
        }
        if (Double.isNaN(d2)) {
            return d;
        }
        return Math.max(d, d2);
    }

    public static float max(float f, float f2, float f3) {
        return max(max(f, f2), f3);
    }

    public static float max(float f, float f2) {
        if (Float.isNaN(f)) {
            return f2;
        }
        if (Float.isNaN(f2)) {
            return f;
        }
        return Math.max(f, f2);
    }
}
