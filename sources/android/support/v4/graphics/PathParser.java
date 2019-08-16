package android.support.p000v4.graphics;

import android.graphics.Path;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import android.util.Log;
import java.util.ArrayList;

@RestrictTo({Scope.LIBRARY_GROUP})
/* renamed from: android.support.v4.graphics.PathParser */
public class PathParser {
    private static final String LOGTAG = "PathParser";

    /* renamed from: android.support.v4.graphics.PathParser$ExtractFloatResult */
    private static class ExtractFloatResult {
        int mEndPosition;
        boolean mEndWithNegOrDot;

        ExtractFloatResult() {
        }
    }

    /* renamed from: android.support.v4.graphics.PathParser$PathDataNode */
    public static class PathDataNode {
        @RestrictTo({Scope.LIBRARY_GROUP})
        public float[] mParams;
        @RestrictTo({Scope.LIBRARY_GROUP})
        public char mType;

        PathDataNode(char c, float[] fArr) {
            this.mType = (char) c;
            this.mParams = fArr;
        }

        PathDataNode(PathDataNode pathDataNode) {
            this.mType = (char) pathDataNode.mType;
            this.mParams = PathParser.copyOfRange(pathDataNode.mParams, 0, pathDataNode.mParams.length);
        }

        private static void addCommand(Path path, float[] fArr, char c, char c2, float[] fArr2) {
            int i;
            float f;
            float f2;
            float f3;
            float f4;
            float f5;
            float f6;
            float f7;
            float f8;
            float f9;
            float f10;
            float f11 = fArr[0];
            float f12 = fArr[1];
            float f13 = fArr[2];
            float f14 = fArr[3];
            float f15 = fArr[4];
            float f16 = fArr[5];
            switch (c2) {
                case 'A':
                case 'a':
                    i = 7;
                    break;
                case 'C':
                case 'c':
                    i = 6;
                    break;
                case 'H':
                case 'V':
                case 'h':
                case 'v':
                    i = 1;
                    break;
                case 'L':
                case 'M':
                case 'T':
                case 'l':
                case 'm':
                case 't':
                    i = 2;
                    break;
                case 'Q':
                case 'S':
                case 'q':
                case 's':
                    i = 4;
                    break;
                case 'Z':
                case 'z':
                    path.close();
                    path.moveTo(f15, f16);
                    i = 2;
                    f14 = f16;
                    f13 = f15;
                    f12 = f16;
                    f11 = f15;
                    break;
                default:
                    i = 2;
                    break;
            }
            int i2 = 0;
            float f17 = f16;
            float f18 = f15;
            float f19 = f12;
            float f20 = f11;
            while (i2 < fArr2.length) {
                switch (c2) {
                    case 'A':
                        drawArc(path, f20, f19, fArr2[i2 + 5], fArr2[i2 + 6], fArr2[i2 + 0], fArr2[i2 + 1], fArr2[i2 + 2], fArr2[i2 + 3] != 0.0f, fArr2[i2 + 4] != 0.0f);
                        float f21 = fArr2[i2 + 5];
                        float f22 = fArr2[i2 + 6];
                        f3 = f17;
                        f4 = f18;
                        f2 = f22;
                        f = f21;
                        f5 = f22;
                        f6 = f21;
                        break;
                    case 'C':
                        path.cubicTo(fArr2[i2 + 0], fArr2[i2 + 1], fArr2[i2 + 2], fArr2[i2 + 3], fArr2[i2 + 4], fArr2[i2 + 5]);
                        float f23 = fArr2[i2 + 4];
                        float f24 = fArr2[i2 + 5];
                        f = fArr2[i2 + 2];
                        f2 = fArr2[i2 + 3];
                        f3 = f17;
                        f4 = f18;
                        f5 = f24;
                        f6 = f23;
                        break;
                    case 'H':
                        path.lineTo(fArr2[i2 + 0], f19);
                        f3 = f17;
                        f4 = f18;
                        f5 = f19;
                        f6 = fArr2[i2 + 0];
                        break;
                    case 'L':
                        path.lineTo(fArr2[i2 + 0], fArr2[i2 + 1]);
                        float f25 = fArr2[i2 + 0];
                        f3 = f17;
                        f4 = f18;
                        f5 = fArr2[i2 + 1];
                        f6 = f25;
                        break;
                    case 'M':
                        float f26 = fArr2[i2 + 0];
                        float f27 = fArr2[i2 + 1];
                        if (i2 <= 0) {
                            path.moveTo(fArr2[i2 + 0], fArr2[i2 + 1]);
                            f3 = f27;
                            f4 = f26;
                            f5 = f27;
                            f6 = f26;
                            break;
                        } else {
                            path.lineTo(fArr2[i2 + 0], fArr2[i2 + 1]);
                            f3 = f17;
                            f4 = f18;
                            f5 = f27;
                            f6 = f26;
                            break;
                        }
                    case 'Q':
                        path.quadTo(fArr2[i2 + 0], fArr2[i2 + 1], fArr2[i2 + 2], fArr2[i2 + 3]);
                        f = fArr2[i2 + 0];
                        f2 = fArr2[i2 + 1];
                        float f28 = fArr2[i2 + 2];
                        f3 = f17;
                        f4 = f18;
                        f5 = fArr2[i2 + 3];
                        f6 = f28;
                        break;
                    case 'S':
                        if (c == 'c' || c == 's' || c == 'C' || c == 'S') {
                            f8 = (2.0f * f19) - f2;
                            f9 = (2.0f * f20) - f;
                        } else {
                            f9 = f20;
                            f8 = f19;
                        }
                        path.cubicTo(f9, f8, fArr2[i2 + 0], fArr2[i2 + 1], fArr2[i2 + 2], fArr2[i2 + 3]);
                        f = fArr2[i2 + 0];
                        f2 = fArr2[i2 + 1];
                        float f29 = fArr2[i2 + 2];
                        f3 = f17;
                        f4 = f18;
                        f5 = fArr2[i2 + 3];
                        f6 = f29;
                        break;
                    case 'T':
                        if (c == 'q' || c == 't' || c == 'Q' || c == 'T') {
                            f = (2.0f * f20) - f;
                            f2 = (2.0f * f19) - f2;
                        } else {
                            f = f20;
                            f2 = f19;
                        }
                        path.quadTo(f, f2, fArr2[i2 + 0], fArr2[i2 + 1]);
                        float f30 = fArr2[i2 + 0];
                        f3 = f17;
                        f4 = f18;
                        f5 = fArr2[i2 + 1];
                        f6 = f30;
                        break;
                    case 'V':
                        path.lineTo(f20, fArr2[i2 + 0]);
                        f3 = f17;
                        f4 = f18;
                        f5 = fArr2[i2 + 0];
                        f6 = f20;
                        break;
                    case 'a':
                        drawArc(path, f20, f19, fArr2[i2 + 5] + f20, fArr2[i2 + 6] + f19, fArr2[i2 + 0], fArr2[i2 + 1], fArr2[i2 + 2], fArr2[i2 + 3] != 0.0f, fArr2[i2 + 4] != 0.0f);
                        float f31 = f20 + fArr2[i2 + 5];
                        float f32 = fArr2[i2 + 6] + f19;
                        f3 = f17;
                        f4 = f18;
                        f2 = f32;
                        f = f31;
                        f5 = f32;
                        f6 = f31;
                        break;
                    case 'c':
                        path.rCubicTo(fArr2[i2 + 0], fArr2[i2 + 1], fArr2[i2 + 2], fArr2[i2 + 3], fArr2[i2 + 4], fArr2[i2 + 5]);
                        f = f20 + fArr2[i2 + 2];
                        f2 = f19 + fArr2[i2 + 3];
                        f3 = f17;
                        f4 = f18;
                        f5 = fArr2[i2 + 5] + f19;
                        f6 = f20 + fArr2[i2 + 4];
                        break;
                    case 'h':
                        path.rLineTo(fArr2[i2 + 0], 0.0f);
                        f3 = f17;
                        f4 = f18;
                        f5 = f19;
                        f6 = f20 + fArr2[i2 + 0];
                        break;
                    case 'l':
                        path.rLineTo(fArr2[i2 + 0], fArr2[i2 + 1]);
                        f3 = f17;
                        f4 = f18;
                        f5 = fArr2[i2 + 1] + f19;
                        f6 = f20 + fArr2[i2 + 0];
                        break;
                    case 'm':
                        float f33 = f20 + fArr2[i2 + 0];
                        float f34 = fArr2[i2 + 1] + f19;
                        if (i2 <= 0) {
                            path.rMoveTo(fArr2[i2 + 0], fArr2[i2 + 1]);
                            f3 = f34;
                            f4 = f33;
                            f5 = f34;
                            f6 = f33;
                            break;
                        } else {
                            path.rLineTo(fArr2[i2 + 0], fArr2[i2 + 1]);
                            f3 = f17;
                            f4 = f18;
                            f5 = f34;
                            f6 = f33;
                            break;
                        }
                    case 'q':
                        path.rQuadTo(fArr2[i2 + 0], fArr2[i2 + 1], fArr2[i2 + 2], fArr2[i2 + 3]);
                        f = f20 + fArr2[i2 + 0];
                        f2 = f19 + fArr2[i2 + 1];
                        f3 = f17;
                        f4 = f18;
                        f5 = fArr2[i2 + 3] + f19;
                        f6 = f20 + fArr2[i2 + 2];
                        break;
                    case 's':
                        float f35 = 0.0f;
                        if (c == 'c' || c == 's' || c == 'C' || c == 'S') {
                            f35 = f19 - f2;
                            f10 = f20 - f;
                        } else {
                            f10 = 0.0f;
                        }
                        path.rCubicTo(f10, f35, fArr2[i2 + 0], fArr2[i2 + 1], fArr2[i2 + 2], fArr2[i2 + 3]);
                        f = f20 + fArr2[i2 + 0];
                        f2 = f19 + fArr2[i2 + 1];
                        f3 = f17;
                        f4 = f18;
                        f5 = fArr2[i2 + 3] + f19;
                        f6 = f20 + fArr2[i2 + 2];
                        break;
                    case 't':
                        float f36 = 0.0f;
                        if (c == 'q' || c == 't' || c == 'Q' || c == 'T') {
                            f36 = f20 - f;
                            f7 = f19 - f2;
                        } else {
                            f7 = 0.0f;
                        }
                        path.rQuadTo(f36, f7, fArr2[i2 + 0], fArr2[i2 + 1]);
                        f = f20 + f36;
                        f2 = f7 + f19;
                        f3 = f17;
                        f4 = f18;
                        f5 = fArr2[i2 + 1] + f19;
                        f6 = f20 + fArr2[i2 + 0];
                        break;
                    case 'v':
                        path.rLineTo(0.0f, fArr2[i2 + 0]);
                        f3 = f17;
                        f4 = f18;
                        f5 = fArr2[i2 + 0] + f19;
                        f6 = f20;
                        break;
                    default:
                        f3 = f17;
                        f4 = f18;
                        f5 = f19;
                        f6 = f20;
                        break;
                }
                i2 += i;
                f17 = f3;
                f18 = f4;
                f19 = f5;
                f20 = f6;
                c = c2;
            }
            fArr[0] = f20;
            fArr[1] = f19;
            fArr[2] = f;
            fArr[3] = f2;
            fArr[4] = f18;
            fArr[5] = f17;
        }

        private static void arcToBezier(Path path, double d, double d2, double d3, double d4, double d5, double d6, double d7, double d8, double d9) {
            int ceil = (int) Math.ceil(Math.abs((4.0d * d9) / 3.141592653589793d));
            double cos = Math.cos(d7);
            double sin = Math.sin(d7);
            double cos2 = Math.cos(d8);
            double sin2 = Math.sin(d8);
            double d10 = (((-d3) * cos) * sin2) - ((d4 * sin) * cos2);
            double d11 = (sin2 * (-d3) * sin) + (cos2 * d4 * cos);
            double d12 = d9 / ((double) ceil);
            int i = 0;
            while (i < ceil) {
                double d13 = d8 + d12;
                double sin3 = Math.sin(d13);
                double cos3 = Math.cos(d13);
                double d14 = (((d3 * cos) * cos3) + d) - ((d4 * sin) * sin3);
                double d15 = (d3 * sin * cos3) + d2 + (d4 * cos * sin3);
                double d16 = (((-d3) * cos) * sin3) - ((d4 * sin) * cos3);
                double d17 = (cos3 * d4 * cos) + (sin3 * (-d3) * sin);
                double tan = Math.tan((d13 - d8) / 2.0d);
                double sqrt = ((Math.sqrt((tan * (3.0d * tan)) + 4.0d) - 1.0d) * Math.sin(d13 - d8)) / 3.0d;
                path.rLineTo(0.0f, 0.0f);
                path.cubicTo((float) ((sqrt * d10) + d5), (float) ((d11 * sqrt) + d6), (float) (d14 - (sqrt * d16)), (float) (d15 - (sqrt * d17)), (float) d14, (float) d15);
                i++;
                d8 = d13;
                d5 = d14;
                d11 = d17;
                d6 = d15;
                d10 = d16;
            }
        }

        private static void drawArc(Path path, float f, float f2, float f3, float f4, float f5, float f6, float f7, boolean z, boolean z2) {
            double d;
            double d2;
            double radians = Math.toRadians((double) f7);
            double cos = Math.cos(radians);
            double sin = Math.sin(radians);
            double d3 = ((((double) f) * cos) + (((double) f2) * sin)) / ((double) f5);
            double d4 = ((((double) (-f)) * sin) + (((double) f2) * cos)) / ((double) f6);
            double d5 = ((((double) f3) * cos) + (((double) f4) * sin)) / ((double) f5);
            double d6 = ((((double) (-f3)) * sin) + (((double) f4) * cos)) / ((double) f6);
            double d7 = d3 - d5;
            double d8 = d4 - d6;
            double d9 = (d3 + d5) / 2.0d;
            double d10 = (d4 + d6) / 2.0d;
            double d11 = (d7 * d7) + (d8 * d8);
            if (d11 == 0.0d) {
                Log.w(PathParser.LOGTAG, " Points are coincident");
                return;
            }
            double d12 = (1.0d / d11) - 0.25d;
            if (d12 < 0.0d) {
                Log.w(PathParser.LOGTAG, "Points are too far apart " + d11);
                float sqrt = (float) (Math.sqrt(d11) / 1.99999d);
                drawArc(path, f, f2, f3, f4, f5 * sqrt, f6 * sqrt, f7, z, z2);
                return;
            }
            double sqrt2 = Math.sqrt(d12);
            double d13 = sqrt2 * d7;
            double d14 = sqrt2 * d8;
            if (z == z2) {
                d = d9 - d14;
                d2 = d10 + d13;
            } else {
                d = d14 + d9;
                d2 = d10 - d13;
            }
            double atan2 = Math.atan2(d4 - d2, d3 - d);
            double atan22 = Math.atan2(d6 - d2, d5 - d) - atan2;
            if (z2 != (atan22 >= 0.0d)) {
                atan22 = atan22 > 0.0d ? atan22 - 6.283185307179586d : atan22 + 6.283185307179586d;
            }
            double d15 = d * ((double) f5);
            double d16 = ((double) f6) * d2;
            arcToBezier(path, (d15 * cos) - (d16 * sin), (d16 * cos) + (d15 * sin), (double) f5, (double) f6, (double) f, (double) f2, radians, atan2, atan22);
        }

        public static void nodesToPath(PathDataNode[] pathDataNodeArr, Path path) {
            float[] fArr = new float[6];
            char c = 'm';
            for (int i = 0; i < pathDataNodeArr.length; i++) {
                addCommand(path, fArr, c, pathDataNodeArr[i].mType, pathDataNodeArr[i].mParams);
                c = pathDataNodeArr[i].mType;
            }
        }

        public void interpolatePathDataNode(PathDataNode pathDataNode, PathDataNode pathDataNode2, float f) {
            for (int i = 0; i < pathDataNode.mParams.length; i++) {
                this.mParams[i] = (pathDataNode.mParams[i] * (1.0f - f)) + (pathDataNode2.mParams[i] * f);
            }
        }
    }

    private static void addNode(ArrayList<PathDataNode> arrayList, char c, float[] fArr) {
        arrayList.add(new PathDataNode(c, fArr));
    }

    public static boolean canMorph(PathDataNode[] pathDataNodeArr, PathDataNode[] pathDataNodeArr2) {
        if (pathDataNodeArr == null || pathDataNodeArr2 == null || pathDataNodeArr.length != pathDataNodeArr2.length) {
            return false;
        }
        for (int i = 0; i < pathDataNodeArr.length; i++) {
            if (pathDataNodeArr[i].mType != pathDataNodeArr2[i].mType || pathDataNodeArr[i].mParams.length != pathDataNodeArr2[i].mParams.length) {
                return false;
            }
        }
        return true;
    }

    static float[] copyOfRange(float[] fArr, int i, int i2) {
        if (i > i2) {
            throw new IllegalArgumentException();
        }
        int length = fArr.length;
        if (i < 0 || i > length) {
            throw new ArrayIndexOutOfBoundsException();
        }
        int i3 = i2 - i;
        int min = Math.min(i3, length - i);
        float[] fArr2 = new float[i3];
        System.arraycopy(fArr, i, fArr2, 0, min);
        return fArr2;
    }

    public static PathDataNode[] createNodesFromPathData(String str) {
        if (str == null) {
            return null;
        }
        ArrayList arrayList = new ArrayList();
        int i = 1;
        int i2 = 0;
        while (i < str.length()) {
            int nextStart = nextStart(str, i);
            String trim = str.substring(i2, nextStart).trim();
            if (trim.length() > 0) {
                addNode(arrayList, trim.charAt(0), getFloats(trim));
            }
            i = nextStart + 1;
            i2 = nextStart;
        }
        if (i - i2 == 1 && i2 < str.length()) {
            addNode(arrayList, str.charAt(i2), new float[0]);
        }
        return (PathDataNode[]) arrayList.toArray(new PathDataNode[arrayList.size()]);
    }

    public static Path createPathFromPathData(String str) {
        Path path = new Path();
        PathDataNode[] createNodesFromPathData = createNodesFromPathData(str);
        if (createNodesFromPathData == null) {
            return null;
        }
        try {
            PathDataNode.nodesToPath(createNodesFromPathData, path);
            return path;
        } catch (RuntimeException e) {
            throw new RuntimeException("Error in parsing " + str, e);
        }
    }

    public static PathDataNode[] deepCopyNodes(PathDataNode[] pathDataNodeArr) {
        if (pathDataNodeArr == null) {
            return null;
        }
        PathDataNode[] pathDataNodeArr2 = new PathDataNode[pathDataNodeArr.length];
        for (int i = 0; i < pathDataNodeArr.length; i++) {
            pathDataNodeArr2[i] = new PathDataNode(pathDataNodeArr[i]);
        }
        return pathDataNodeArr2;
    }

    private static void extract(String str, int i, ExtractFloatResult extractFloatResult) {
        extractFloatResult.mEndWithNegOrDot = false;
        boolean z = false;
        boolean z2 = false;
        boolean z3 = false;
        for (int i2 = i; i2 < str.length(); i2++) {
            switch (str.charAt(i2)) {
                case ' ':
                case ',':
                    z = false;
                    z2 = true;
                    break;
                case '-':
                    if (i2 != i && !z) {
                        extractFloatResult.mEndWithNegOrDot = true;
                        z = false;
                        z2 = true;
                        break;
                    } else {
                        z = false;
                        break;
                    }
                case '.':
                    if (z3) {
                        extractFloatResult.mEndWithNegOrDot = true;
                        z = false;
                        z2 = true;
                        break;
                    } else {
                        z3 = true;
                        z = false;
                        break;
                    }
                case 'E':
                case 'e':
                    z = true;
                    break;
                default:
                    z = false;
                    break;
            }
            if (z2) {
                extractFloatResult.mEndPosition = i2;
            }
        }
        extractFloatResult.mEndPosition = i2;
    }

    private static float[] getFloats(String str) {
        int i;
        int i2 = 0;
        if (str.charAt(0) == 'z' || str.charAt(0) == 'Z') {
            return new float[0];
        }
        try {
            float[] fArr = new float[str.length()];
            ExtractFloatResult extractFloatResult = new ExtractFloatResult();
            int length = str.length();
            int i3 = 1;
            while (i3 < length) {
                extract(str, i3, extractFloatResult);
                int i4 = extractFloatResult.mEndPosition;
                if (i3 < i4) {
                    i = i2 + 1;
                    fArr[i2] = Float.parseFloat(str.substring(i3, i4));
                } else {
                    i = i2;
                }
                if (extractFloatResult.mEndWithNegOrDot) {
                    i3 = i4;
                    i2 = i;
                } else {
                    i3 = i4 + 1;
                    i2 = i;
                }
            }
            return copyOfRange(fArr, 0, i2);
        } catch (NumberFormatException e) {
            throw new RuntimeException("error in parsing \"" + str + "\"", e);
        }
    }

    private static int nextStart(String str, int i) {
        while (i < str.length()) {
            char charAt = str.charAt(i);
            if (((charAt - 'A') * (charAt - 'Z') <= 0 || (charAt - 'a') * (charAt - 'z') <= 0) && charAt != 'e' && charAt != 'E') {
                break;
            }
            i++;
        }
        return i;
    }

    public static void updateNodes(PathDataNode[] pathDataNodeArr, PathDataNode[] pathDataNodeArr2) {
        for (int i = 0; i < pathDataNodeArr2.length; i++) {
            pathDataNodeArr[i].mType = (char) pathDataNodeArr2[i].mType;
            for (int i2 = 0; i2 < pathDataNodeArr2[i].mParams.length; i2++) {
                pathDataNodeArr[i].mParams[i2] = pathDataNodeArr2[i].mParams[i2];
            }
        }
    }
}
