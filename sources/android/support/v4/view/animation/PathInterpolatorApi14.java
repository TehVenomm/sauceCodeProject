package android.support.p000v4.view.animation;

import android.graphics.Path;
import android.graphics.PathMeasure;
import android.view.animation.Interpolator;

/* renamed from: android.support.v4.view.animation.PathInterpolatorApi14 */
class PathInterpolatorApi14 implements Interpolator {
    private static final float PRECISION = 0.002f;

    /* renamed from: mX */
    private final float[] f19mX;

    /* renamed from: mY */
    private final float[] f20mY;

    PathInterpolatorApi14(float f, float f2) {
        this(createQuad(f, f2));
    }

    PathInterpolatorApi14(float f, float f2, float f3, float f4) {
        this(createCubic(f, f2, f3, f4));
    }

    PathInterpolatorApi14(Path path) {
        PathMeasure pathMeasure = new PathMeasure(path, false);
        float length = pathMeasure.getLength();
        int i = ((int) (length / PRECISION)) + 1;
        this.f19mX = new float[i];
        this.f20mY = new float[i];
        float[] fArr = new float[2];
        for (int i2 = 0; i2 < i; i2++) {
            pathMeasure.getPosTan((((float) i2) * length) / ((float) (i - 1)), fArr, null);
            this.f19mX[i2] = fArr[0];
            this.f20mY[i2] = fArr[1];
        }
    }

    private static Path createCubic(float f, float f2, float f3, float f4) {
        Path path = new Path();
        path.moveTo(0.0f, 0.0f);
        path.cubicTo(f, f2, f3, f4, 1.0f, 1.0f);
        return path;
    }

    private static Path createQuad(float f, float f2) {
        Path path = new Path();
        path.moveTo(0.0f, 0.0f);
        path.quadTo(f, f2, 1.0f, 1.0f);
        return path;
    }

    public float getInterpolation(float f) {
        if (f <= 0.0f) {
            return 0.0f;
        }
        if (f >= 1.0f) {
            return 1.0f;
        }
        int length = this.f19mX.length - 1;
        int i = 0;
        while (length - i > 1) {
            int i2 = (i + length) / 2;
            if (f < this.f19mX[i2]) {
                length = i2;
            } else {
                i = i2;
            }
        }
        float f2 = this.f19mX[length] - this.f19mX[i];
        if (f2 == 0.0f) {
            return this.f20mY[i];
        }
        float f3 = (f - this.f19mX[i]) / f2;
        float f4 = this.f20mY[i];
        return (f3 * (this.f20mY[length] - f4)) + f4;
    }
}
