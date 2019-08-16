package android.support.graphics.drawable;

import android.content.Context;
import android.content.res.Resources;
import android.content.res.Resources.Theme;
import android.content.res.TypedArray;
import android.graphics.Path;
import android.graphics.PathMeasure;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import android.support.p000v4.content.res.TypedArrayUtils;
import android.support.p000v4.graphics.PathParser;
import android.util.AttributeSet;
import android.view.InflateException;
import android.view.animation.Interpolator;
import org.xmlpull.v1.XmlPullParser;

@RestrictTo({Scope.LIBRARY_GROUP})
public class PathInterpolatorCompat implements Interpolator {
    public static final double EPSILON = 1.0E-5d;
    public static final int MAX_NUM_POINTS = 3000;
    private static final float PRECISION = 0.002f;

    /* renamed from: mX */
    private float[] f3mX;

    /* renamed from: mY */
    private float[] f4mY;

    public PathInterpolatorCompat(Context context, AttributeSet attributeSet, XmlPullParser xmlPullParser) {
        this(context.getResources(), context.getTheme(), attributeSet, xmlPullParser);
    }

    public PathInterpolatorCompat(Resources resources, Theme theme, AttributeSet attributeSet, XmlPullParser xmlPullParser) {
        TypedArray obtainAttributes = TypedArrayUtils.obtainAttributes(resources, theme, attributeSet, AndroidResources.STYLEABLE_PATH_INTERPOLATOR);
        parseInterpolatorFromTypeArray(obtainAttributes, xmlPullParser);
        obtainAttributes.recycle();
    }

    private void initCubic(float f, float f2, float f3, float f4) {
        Path path = new Path();
        path.moveTo(0.0f, 0.0f);
        path.cubicTo(f, f2, f3, f4, 1.0f, 1.0f);
        initPath(path);
    }

    private void initPath(Path path) {
        PathMeasure pathMeasure = new PathMeasure(path, false);
        float length = pathMeasure.getLength();
        int min = Math.min(3000, ((int) (length / PRECISION)) + 1);
        if (min <= 0) {
            throw new IllegalArgumentException("The Path has a invalid length " + length);
        }
        this.f3mX = new float[min];
        this.f4mY = new float[min];
        float[] fArr = new float[2];
        for (int i = 0; i < min; i++) {
            pathMeasure.getPosTan((((float) i) * length) / ((float) (min - 1)), fArr, null);
            this.f3mX[i] = fArr[0];
            this.f4mY[i] = fArr[1];
        }
        if (((double) Math.abs(this.f3mX[0])) > 1.0E-5d || ((double) Math.abs(this.f4mY[0])) > 1.0E-5d || ((double) Math.abs(this.f3mX[min - 1] - 1.0f)) > 1.0E-5d || ((double) Math.abs(this.f4mY[min - 1] - 1.0f)) > 1.0E-5d) {
            throw new IllegalArgumentException("The Path must start at (0,0) and end at (1,1) start: " + this.f3mX[0] + "," + this.f4mY[0] + " end:" + this.f3mX[min - 1] + "," + this.f4mY[min - 1]);
        }
        float f = 0.0f;
        int i2 = 0;
        int i3 = 0;
        while (i3 < min) {
            float f2 = this.f3mX[i2];
            if (f2 < f) {
                throw new IllegalArgumentException("The Path cannot loop back on itself, x :" + f2);
            }
            this.f3mX[i3] = f2;
            i2++;
            i3++;
            f = f2;
        }
        if (pathMeasure.nextContour()) {
            throw new IllegalArgumentException("The Path should be continuous, can't have 2+ contours");
        }
    }

    private void initQuad(float f, float f2) {
        Path path = new Path();
        path.moveTo(0.0f, 0.0f);
        path.quadTo(f, f2, 1.0f, 1.0f);
        initPath(path);
    }

    private void parseInterpolatorFromTypeArray(TypedArray typedArray, XmlPullParser xmlPullParser) {
        if (TypedArrayUtils.hasAttribute(xmlPullParser, "pathData")) {
            String namedString = TypedArrayUtils.getNamedString(typedArray, xmlPullParser, "pathData", 4);
            Path createPathFromPathData = PathParser.createPathFromPathData(namedString);
            if (createPathFromPathData == null) {
                throw new InflateException("The path is null, which is created from " + namedString);
            }
            initPath(createPathFromPathData);
        } else if (!TypedArrayUtils.hasAttribute(xmlPullParser, "controlX1")) {
            throw new InflateException("pathInterpolator requires the controlX1 attribute");
        } else if (!TypedArrayUtils.hasAttribute(xmlPullParser, "controlY1")) {
            throw new InflateException("pathInterpolator requires the controlY1 attribute");
        } else {
            float namedFloat = TypedArrayUtils.getNamedFloat(typedArray, xmlPullParser, "controlX1", 0, 0.0f);
            float namedFloat2 = TypedArrayUtils.getNamedFloat(typedArray, xmlPullParser, "controlY1", 1, 0.0f);
            boolean hasAttribute = TypedArrayUtils.hasAttribute(xmlPullParser, "controlX2");
            if (hasAttribute != TypedArrayUtils.hasAttribute(xmlPullParser, "controlY2")) {
                throw new InflateException("pathInterpolator requires both controlX2 and controlY2 for cubic Beziers.");
            } else if (!hasAttribute) {
                initQuad(namedFloat, namedFloat2);
            } else {
                initCubic(namedFloat, namedFloat2, TypedArrayUtils.getNamedFloat(typedArray, xmlPullParser, "controlX2", 2, 0.0f), TypedArrayUtils.getNamedFloat(typedArray, xmlPullParser, "controlY2", 3, 0.0f));
            }
        }
    }

    public float getInterpolation(float f) {
        if (f <= 0.0f) {
            return 0.0f;
        }
        if (f >= 1.0f) {
            return 1.0f;
        }
        int length = this.f3mX.length - 1;
        int i = 0;
        while (length - i > 1) {
            int i2 = (i + length) / 2;
            if (f < this.f3mX[i2]) {
                length = i2;
            } else {
                i = i2;
            }
        }
        float f2 = this.f3mX[length] - this.f3mX[i];
        if (f2 == 0.0f) {
            return this.f4mY[i];
        }
        float f3 = (f - this.f3mX[i]) / f2;
        float f4 = this.f4mY[i];
        return (f3 * (this.f4mY[length] - f4)) + f4;
    }
}
