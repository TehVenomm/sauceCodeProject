package android.support.p000v4.math;

/* renamed from: android.support.v4.math.MathUtils */
public class MathUtils {
    private MathUtils() {
    }

    public static double clamp(double d, double d2, double d3) {
        return d < d2 ? d2 : d <= d3 ? d : d3;
    }

    public static float clamp(float f, float f2, float f3) {
        return f < f2 ? f2 : f <= f3 ? f : f3;
    }

    public static int clamp(int i, int i2, int i3) {
        return i < i2 ? i2 : i <= i3 ? i : i3;
    }
}
