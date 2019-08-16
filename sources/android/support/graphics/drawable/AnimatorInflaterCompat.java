package android.support.graphics.drawable;

import android.animation.Animator;
import android.animation.AnimatorInflater;
import android.animation.AnimatorSet;
import android.animation.Keyframe;
import android.animation.ObjectAnimator;
import android.animation.PropertyValuesHolder;
import android.animation.TypeEvaluator;
import android.animation.ValueAnimator;
import android.content.Context;
import android.content.res.Resources;
import android.content.res.Resources.NotFoundException;
import android.content.res.Resources.Theme;
import android.content.res.TypedArray;
import android.content.res.XmlResourceParser;
import android.graphics.Path;
import android.graphics.PathMeasure;
import android.os.Build.VERSION;
import android.support.annotation.AnimatorRes;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import android.support.p000v4.content.res.TypedArrayUtils;
import android.support.p000v4.graphics.PathParser;
import android.support.p000v4.graphics.PathParser.PathDataNode;
import android.util.AttributeSet;
import android.util.Log;
import android.util.TypedValue;
import android.util.Xml;
import android.view.InflateException;
import com.google.android.gms.nearby.messages.Strategy;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Iterator;
import org.xmlpull.v1.XmlPullParser;
import org.xmlpull.v1.XmlPullParserException;

@RestrictTo({Scope.LIBRARY_GROUP})
public class AnimatorInflaterCompat {
    private static final boolean DBG_ANIMATOR_INFLATER = false;
    private static final int MAX_NUM_POINTS = 100;
    private static final String TAG = "AnimatorInflater";
    private static final int TOGETHER = 0;
    private static final int VALUE_TYPE_COLOR = 3;
    private static final int VALUE_TYPE_FLOAT = 0;
    private static final int VALUE_TYPE_INT = 1;
    private static final int VALUE_TYPE_PATH = 2;
    private static final int VALUE_TYPE_UNDEFINED = 4;

    private static class PathDataEvaluator implements TypeEvaluator<PathDataNode[]> {
        private PathDataNode[] mNodeArray;

        private PathDataEvaluator() {
        }

        PathDataEvaluator(PathDataNode[] pathDataNodeArr) {
            this.mNodeArray = pathDataNodeArr;
        }

        public PathDataNode[] evaluate(float f, PathDataNode[] pathDataNodeArr, PathDataNode[] pathDataNodeArr2) {
            if (!PathParser.canMorph(pathDataNodeArr, pathDataNodeArr2)) {
                throw new IllegalArgumentException("Can't interpolate between two incompatible pathData");
            }
            if (this.mNodeArray == null || !PathParser.canMorph(this.mNodeArray, pathDataNodeArr)) {
                this.mNodeArray = PathParser.deepCopyNodes(pathDataNodeArr);
            }
            for (int i = 0; i < pathDataNodeArr.length; i++) {
                this.mNodeArray[i].interpolatePathDataNode(pathDataNodeArr[i], pathDataNodeArr2[i], f);
            }
            return this.mNodeArray;
        }
    }

    private static Animator createAnimatorFromXml(Context context, Resources resources, Theme theme, XmlPullParser xmlPullParser, float f) throws XmlPullParserException, IOException {
        return createAnimatorFromXml(context, resources, theme, xmlPullParser, Xml.asAttributeSet(xmlPullParser), null, 0, f);
    }

    private static Animator createAnimatorFromXml(Context context, Resources resources, Theme theme, XmlPullParser xmlPullParser, AttributeSet attributeSet, AnimatorSet animatorSet, int i, float f) throws XmlPullParserException, IOException {
        boolean z;
        Animator animator = null;
        int depth = xmlPullParser.getDepth();
        ArrayList arrayList = null;
        while (true) {
            int next = xmlPullParser.next();
            if ((next != 3 || xmlPullParser.getDepth() > depth) && next != 1) {
                if (next == 2) {
                    String name = xmlPullParser.getName();
                    if (name.equals("objectAnimator")) {
                        animator = loadObjectAnimator(context, resources, theme, attributeSet, f, xmlPullParser);
                        z = false;
                    } else if (name.equals("animator")) {
                        animator = loadAnimator(context, resources, theme, attributeSet, null, f, xmlPullParser);
                        z = false;
                    } else if (name.equals("set")) {
                        Animator animatorSet2 = new AnimatorSet();
                        TypedArray obtainAttributes = TypedArrayUtils.obtainAttributes(resources, theme, attributeSet, AndroidResources.STYLEABLE_ANIMATOR_SET);
                        createAnimatorFromXml(context, resources, theme, xmlPullParser, attributeSet, (AnimatorSet) animatorSet2, TypedArrayUtils.getNamedInt(obtainAttributes, xmlPullParser, "ordering", 0, 0), f);
                        obtainAttributes.recycle();
                        animator = animatorSet2;
                        z = false;
                    } else if (name.equals("propertyValuesHolder")) {
                        PropertyValuesHolder[] loadValues = loadValues(context, resources, theme, xmlPullParser, Xml.asAttributeSet(xmlPullParser));
                        if (!(loadValues == null || animator == null || !(animator instanceof ValueAnimator))) {
                            ((ValueAnimator) animator).setValues(loadValues);
                        }
                        z = true;
                    } else {
                        throw new RuntimeException("Unknown animator name: " + xmlPullParser.getName());
                    }
                    if (animatorSet != null && !z) {
                        ArrayList arrayList2 = arrayList == null ? new ArrayList() : arrayList;
                        arrayList2.add(animator);
                        arrayList = arrayList2;
                    }
                }
            }
        }
        if (!(animatorSet == null || arrayList == null)) {
            Animator[] animatorArr = new Animator[arrayList.size()];
            int i2 = 0;
            Iterator it = arrayList.iterator();
            while (true) {
                int i3 = i2;
                if (!it.hasNext()) {
                    break;
                }
                animatorArr[i3] = (Animator) it.next();
                i2 = i3 + 1;
            }
            if (i == 0) {
                animatorSet.playTogether(animatorArr);
            } else {
                animatorSet.playSequentially(animatorArr);
            }
        }
        return animator;
    }

    private static Keyframe createNewKeyframe(Keyframe keyframe, float f) {
        return keyframe.getType() == Float.TYPE ? Keyframe.ofFloat(f) : keyframe.getType() == Integer.TYPE ? Keyframe.ofInt(f) : Keyframe.ofObject(f);
    }

    private static void distributeKeyframes(Keyframe[] keyframeArr, float f, int i, int i2) {
        float f2 = f / ((float) ((i2 - i) + 2));
        while (i <= i2) {
            keyframeArr[i].setFraction(keyframeArr[i - 1].getFraction() + f2);
            i++;
        }
    }

    private static void dumpKeyframes(Object[] objArr, String str) {
        if (objArr != null && objArr.length != 0) {
            Log.d(TAG, str);
            int length = objArr.length;
            for (int i = 0; i < length; i++) {
                Keyframe keyframe = objArr[i];
                Log.d(TAG, "Keyframe " + i + ": fraction " + (keyframe.getFraction() < 0.0f ? "null" : Float.valueOf(keyframe.getFraction())) + ", " + ", value : " + (keyframe.hasValue() ? keyframe.getValue() : "null"));
            }
        }
    }

    private static PropertyValuesHolder getPVH(TypedArray typedArray, int i, int i2, int i3, String str) {
        PropertyValuesHolder propertyValuesHolder;
        TypedValue peekValue = typedArray.peekValue(i2);
        boolean z = peekValue != null;
        int i4 = z ? peekValue.type : 0;
        TypedValue peekValue2 = typedArray.peekValue(i3);
        boolean z2 = peekValue2 != null;
        int i5 = z2 ? peekValue2.type : 0;
        if (i == 4) {
            i = ((!z || !isColorType(i4)) && (!z2 || !isColorType(i5))) ? 0 : 3;
        }
        boolean z3 = i == 0;
        if (i == 2) {
            String string = typedArray.getString(i2);
            String string2 = typedArray.getString(i3);
            PathDataNode[] createNodesFromPathData = PathParser.createNodesFromPathData(string);
            PathDataNode[] createNodesFromPathData2 = PathParser.createNodesFromPathData(string2);
            if (!(createNodesFromPathData == null && createNodesFromPathData2 == null)) {
                if (createNodesFromPathData != null) {
                    PathDataEvaluator pathDataEvaluator = new PathDataEvaluator();
                    if (createNodesFromPathData2 == null) {
                        return PropertyValuesHolder.ofObject(str, pathDataEvaluator, new Object[]{createNodesFromPathData});
                    } else if (!PathParser.canMorph(createNodesFromPathData, createNodesFromPathData2)) {
                        throw new InflateException(" Can't morph from " + string + " to " + string2);
                    } else {
                        return PropertyValuesHolder.ofObject(str, pathDataEvaluator, new Object[]{createNodesFromPathData, createNodesFromPathData2});
                    }
                } else if (createNodesFromPathData2 != null) {
                    return PropertyValuesHolder.ofObject(str, new PathDataEvaluator(), new Object[]{createNodesFromPathData2});
                }
            }
            return null;
        }
        ArgbEvaluator argbEvaluator = i == 3 ? ArgbEvaluator.getInstance() : null;
        if (z3) {
            if (z) {
                float f = i4 == 5 ? typedArray.getDimension(i2, 0.0f) : typedArray.getFloat(i2, 0.0f);
                if (z2) {
                    propertyValuesHolder = PropertyValuesHolder.ofFloat(str, new float[]{f, i5 == 5 ? typedArray.getDimension(i3, 0.0f) : typedArray.getFloat(i3, 0.0f)});
                } else {
                    propertyValuesHolder = PropertyValuesHolder.ofFloat(str, new float[]{f});
                }
            } else {
                propertyValuesHolder = PropertyValuesHolder.ofFloat(str, new float[]{i5 == 5 ? typedArray.getDimension(i3, 0.0f) : typedArray.getFloat(i3, 0.0f)});
            }
        } else if (z) {
            int i6 = i4 == 5 ? (int) typedArray.getDimension(i2, 0.0f) : isColorType(i4) ? typedArray.getColor(i2, 0) : typedArray.getInt(i2, 0);
            if (z2) {
                int i7 = i5 == 5 ? (int) typedArray.getDimension(i3, 0.0f) : isColorType(i5) ? typedArray.getColor(i3, 0) : typedArray.getInt(i3, 0);
                propertyValuesHolder = PropertyValuesHolder.ofInt(str, new int[]{i6, i7});
            } else {
                propertyValuesHolder = PropertyValuesHolder.ofInt(str, new int[]{i6});
            }
        } else if (z2) {
            int i8 = i5 == 5 ? (int) typedArray.getDimension(i3, 0.0f) : isColorType(i5) ? typedArray.getColor(i3, 0) : typedArray.getInt(i3, 0);
            propertyValuesHolder = PropertyValuesHolder.ofInt(str, new int[]{i8});
        } else {
            propertyValuesHolder = null;
        }
        if (propertyValuesHolder == null || argbEvaluator == null) {
            return propertyValuesHolder;
        }
        propertyValuesHolder.setEvaluator(argbEvaluator);
        return propertyValuesHolder;
    }

    private static int inferValueTypeFromValues(TypedArray typedArray, int i, int i2) {
        TypedValue peekValue = typedArray.peekValue(i);
        boolean z = peekValue != null;
        int i3 = z ? peekValue.type : 0;
        TypedValue peekValue2 = typedArray.peekValue(i2);
        boolean z2 = peekValue2 != null;
        return ((!z || !isColorType(i3)) && (!z2 || !isColorType(z2 ? peekValue2.type : 0))) ? 0 : 3;
    }

    private static int inferValueTypeOfKeyframe(Resources resources, Theme theme, AttributeSet attributeSet, XmlPullParser xmlPullParser) {
        int i = 0;
        TypedArray obtainAttributes = TypedArrayUtils.obtainAttributes(resources, theme, attributeSet, AndroidResources.STYLEABLE_KEYFRAME);
        TypedValue peekNamedValue = TypedArrayUtils.peekNamedValue(obtainAttributes, xmlPullParser, "value", 0);
        if ((peekNamedValue != null) && isColorType(peekNamedValue.type)) {
            i = 3;
        }
        obtainAttributes.recycle();
        return i;
    }

    private static boolean isColorType(int i) {
        return i >= 28 && i <= 31;
    }

    public static Animator loadAnimator(Context context, @AnimatorRes int i) throws NotFoundException {
        return VERSION.SDK_INT >= 24 ? AnimatorInflater.loadAnimator(context, i) : loadAnimator(context, context.getResources(), context.getTheme(), i);
    }

    public static Animator loadAnimator(Context context, Resources resources, Theme theme, @AnimatorRes int i) throws NotFoundException {
        return loadAnimator(context, resources, theme, i, 1.0f);
    }

    public static Animator loadAnimator(Context context, Resources resources, Theme theme, @AnimatorRes int i, float f) throws NotFoundException {
        XmlResourceParser xmlResourceParser = null;
        try {
            xmlResourceParser = resources.getAnimation(i);
            Animator createAnimatorFromXml = createAnimatorFromXml(context, resources, theme, xmlResourceParser, f);
            if (xmlResourceParser != null) {
                xmlResourceParser.close();
            }
            return createAnimatorFromXml;
        } catch (XmlPullParserException e) {
            NotFoundException notFoundException = new NotFoundException("Can't load animation resource ID #0x" + Integer.toHexString(i));
            notFoundException.initCause(e);
            throw notFoundException;
        } catch (IOException e2) {
            NotFoundException notFoundException2 = new NotFoundException("Can't load animation resource ID #0x" + Integer.toHexString(i));
            notFoundException2.initCause(e2);
            throw notFoundException2;
        } catch (Throwable th) {
            if (xmlResourceParser != null) {
                xmlResourceParser.close();
            }
            throw th;
        }
    }

    private static ValueAnimator loadAnimator(Context context, Resources resources, Theme theme, AttributeSet attributeSet, ValueAnimator valueAnimator, float f, XmlPullParser xmlPullParser) throws NotFoundException {
        TypedArray obtainAttributes = TypedArrayUtils.obtainAttributes(resources, theme, attributeSet, AndroidResources.STYLEABLE_ANIMATOR);
        TypedArray obtainAttributes2 = TypedArrayUtils.obtainAttributes(resources, theme, attributeSet, AndroidResources.STYLEABLE_PROPERTY_ANIMATOR);
        if (valueAnimator == null) {
            valueAnimator = new ValueAnimator();
        }
        parseAnimatorFromTypeArray(valueAnimator, obtainAttributes, obtainAttributes2, f, xmlPullParser);
        int namedResourceId = TypedArrayUtils.getNamedResourceId(obtainAttributes, xmlPullParser, "interpolator", 0, 0);
        if (namedResourceId > 0) {
            valueAnimator.setInterpolator(AnimationUtilsCompat.loadInterpolator(context, namedResourceId));
        }
        obtainAttributes.recycle();
        if (obtainAttributes2 != null) {
            obtainAttributes2.recycle();
        }
        return valueAnimator;
    }

    private static Keyframe loadKeyframe(Context context, Resources resources, Theme theme, AttributeSet attributeSet, int i, XmlPullParser xmlPullParser) throws XmlPullParserException, IOException {
        TypedArray obtainAttributes = TypedArrayUtils.obtainAttributes(resources, theme, attributeSet, AndroidResources.STYLEABLE_KEYFRAME);
        Keyframe keyframe = null;
        float namedFloat = TypedArrayUtils.getNamedFloat(obtainAttributes, xmlPullParser, "fraction", 3, -1.0f);
        TypedValue peekNamedValue = TypedArrayUtils.peekNamedValue(obtainAttributes, xmlPullParser, "value", 0);
        boolean z = peekNamedValue != null;
        if (i == 4) {
            i = (!z || !isColorType(peekNamedValue.type)) ? 0 : 3;
        }
        if (z) {
            switch (i) {
                case 0:
                    keyframe = Keyframe.ofFloat(namedFloat, TypedArrayUtils.getNamedFloat(obtainAttributes, xmlPullParser, "value", 0, 0.0f));
                    break;
                case 1:
                case 3:
                    keyframe = Keyframe.ofInt(namedFloat, TypedArrayUtils.getNamedInt(obtainAttributes, xmlPullParser, "value", 0, 0));
                    break;
            }
        } else {
            keyframe = i == 0 ? Keyframe.ofFloat(namedFloat) : Keyframe.ofInt(namedFloat);
        }
        int namedResourceId = TypedArrayUtils.getNamedResourceId(obtainAttributes, xmlPullParser, "interpolator", 1, 0);
        if (namedResourceId > 0) {
            keyframe.setInterpolator(AnimationUtilsCompat.loadInterpolator(context, namedResourceId));
        }
        obtainAttributes.recycle();
        return keyframe;
    }

    private static ObjectAnimator loadObjectAnimator(Context context, Resources resources, Theme theme, AttributeSet attributeSet, float f, XmlPullParser xmlPullParser) throws NotFoundException {
        ObjectAnimator objectAnimator = new ObjectAnimator();
        loadAnimator(context, resources, theme, attributeSet, objectAnimator, f, xmlPullParser);
        return objectAnimator;
    }

    /* JADX WARNING: Removed duplicated region for block: B:17:0x0045  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static android.animation.PropertyValuesHolder loadPvh(android.content.Context r8, android.content.res.Resources r9, android.content.res.Resources.Theme r10, org.xmlpull.v1.XmlPullParser r11, java.lang.String r12, int r13) throws org.xmlpull.v1.XmlPullParserException, java.io.IOException {
        /*
            r7 = 0
            r0 = 0
            r6 = r0
        L_0x0003:
            int r0 = r11.next()
            r1 = 3
            if (r0 == r1) goto L_0x0043
            r1 = 1
            if (r0 == r1) goto L_0x0043
            java.lang.String r0 = r11.getName()
            java.lang.String r1 = "keyframe"
            boolean r0 = r0.equals(r1)
            if (r0 == 0) goto L_0x0003
            r0 = 4
            if (r13 != r0) goto L_0x0103
            android.util.AttributeSet r0 = android.util.Xml.asAttributeSet(r11)
            int r4 = inferValueTypeOfKeyframe(r9, r10, r0, r11)
        L_0x0024:
            android.util.AttributeSet r3 = android.util.Xml.asAttributeSet(r11)
            r0 = r8
            r1 = r9
            r2 = r10
            r5 = r11
            android.animation.Keyframe r0 = loadKeyframe(r0, r1, r2, r3, r4, r5)
            if (r0 == 0) goto L_0x003c
            if (r6 != 0) goto L_0x0039
            java.util.ArrayList r6 = new java.util.ArrayList
            r6.<init>()
        L_0x0039:
            r6.add(r0)
        L_0x003c:
            r0 = r6
            r11.next()
            r6 = r0
            r13 = r4
            goto L_0x0003
        L_0x0043:
            if (r6 == 0) goto L_0x0101
            int r2 = r6.size()
            if (r2 <= 0) goto L_0x0101
            r0 = 0
            java.lang.Object r0 = r6.get(r0)
            android.animation.Keyframe r0 = (android.animation.Keyframe) r0
            int r1 = r2 + -1
            java.lang.Object r1 = r6.get(r1)
            android.animation.Keyframe r1 = (android.animation.Keyframe) r1
            float r3 = r1.getFraction()
            r4 = 1065353216(0x3f800000, float:1.0)
            int r4 = (r3 > r4 ? 1 : (r3 == r4 ? 0 : -1))
            if (r4 >= 0) goto L_0x00fe
            r4 = 0
            int r3 = (r3 > r4 ? 1 : (r3 == r4 ? 0 : -1))
            if (r3 >= 0) goto L_0x009d
            r3 = 1065353216(0x3f800000, float:1.0)
            r1.setFraction(r3)
            r1 = r2
        L_0x006f:
            float r2 = r0.getFraction()
            r3 = 0
            int r3 = (r2 > r3 ? 1 : (r2 == r3 ? 0 : -1))
            if (r3 == 0) goto L_0x0081
            r3 = 0
            int r2 = (r2 > r3 ? 1 : (r2 == r3 ? 0 : -1))
            if (r2 >= 0) goto L_0x00ad
            r2 = 0
            r0.setFraction(r2)
        L_0x0081:
            android.animation.Keyframe[] r5 = new android.animation.Keyframe[r1]
            r6.toArray(r5)
            r2 = 0
        L_0x0087:
            if (r2 >= r1) goto L_0x00ef
            r0 = r5[r2]
            float r3 = r0.getFraction()
            r4 = 0
            int r3 = (r3 > r4 ? 1 : (r3 == r4 ? 0 : -1))
            if (r3 >= 0) goto L_0x009a
            if (r2 != 0) goto L_0x00b9
            r3 = 0
            r0.setFraction(r3)
        L_0x009a:
            int r2 = r2 + 1
            goto L_0x0087
        L_0x009d:
            int r3 = r6.size()
            r4 = 1065353216(0x3f800000, float:1.0)
            android.animation.Keyframe r1 = createNewKeyframe(r1, r4)
            r6.add(r3, r1)
            int r1 = r2 + 1
            goto L_0x006f
        L_0x00ad:
            r2 = 0
            r3 = 0
            android.animation.Keyframe r0 = createNewKeyframe(r0, r3)
            r6.add(r2, r0)
            int r1 = r1 + 1
            goto L_0x0081
        L_0x00b9:
            int r3 = r1 + -1
            if (r2 != r3) goto L_0x00c3
            r3 = 1065353216(0x3f800000, float:1.0)
            r0.setFraction(r3)
            goto L_0x009a
        L_0x00c3:
            int r3 = r2 + 1
            r0 = r2
        L_0x00c6:
            int r4 = r1 + -1
            if (r3 >= r4) goto L_0x00d5
            r4 = r5[r3]
            float r4 = r4.getFraction()
            r6 = 0
            int r4 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1))
            if (r4 < 0) goto L_0x00ea
        L_0x00d5:
            int r3 = r0 + 1
            r3 = r5[r3]
            float r3 = r3.getFraction()
            int r4 = r2 + -1
            r4 = r5[r4]
            float r4 = r4.getFraction()
            float r3 = r3 - r4
            distributeKeyframes(r5, r3, r2, r0)
            goto L_0x009a
        L_0x00ea:
            int r4 = r3 + 1
            r0 = r3
            r3 = r4
            goto L_0x00c6
        L_0x00ef:
            android.animation.PropertyValuesHolder r0 = android.animation.PropertyValuesHolder.ofKeyframe(r12, r5)
            r1 = 3
            if (r13 != r1) goto L_0x00fd
            android.support.graphics.drawable.ArgbEvaluator r1 = android.support.graphics.drawable.ArgbEvaluator.getInstance()
            r0.setEvaluator(r1)
        L_0x00fd:
            return r0
        L_0x00fe:
            r1 = r2
            goto L_0x006f
        L_0x0101:
            r0 = r7
            goto L_0x00fd
        L_0x0103:
            r4 = r13
            goto L_0x0024
        */
        throw new UnsupportedOperationException("Method not decompiled: android.support.graphics.drawable.AnimatorInflaterCompat.loadPvh(android.content.Context, android.content.res.Resources, android.content.res.Resources$Theme, org.xmlpull.v1.XmlPullParser, java.lang.String, int):android.animation.PropertyValuesHolder");
    }

    private static PropertyValuesHolder[] loadValues(Context context, Resources resources, Theme theme, XmlPullParser xmlPullParser, AttributeSet attributeSet) throws XmlPullParserException, IOException {
        ArrayList arrayList = null;
        while (true) {
            int eventType = xmlPullParser.getEventType();
            if (eventType != 3 && eventType != 1) {
                if (eventType != 2) {
                    xmlPullParser.next();
                } else {
                    if (xmlPullParser.getName().equals("propertyValuesHolder")) {
                        TypedArray obtainAttributes = TypedArrayUtils.obtainAttributes(resources, theme, attributeSet, AndroidResources.STYLEABLE_PROPERTY_VALUES_HOLDER);
                        String namedString = TypedArrayUtils.getNamedString(obtainAttributes, xmlPullParser, "propertyName", 3);
                        int namedInt = TypedArrayUtils.getNamedInt(obtainAttributes, xmlPullParser, "valueType", 2, 4);
                        PropertyValuesHolder loadPvh = loadPvh(context, resources, theme, xmlPullParser, namedString, namedInt);
                        if (loadPvh == null) {
                            loadPvh = getPVH(obtainAttributes, namedInt, 0, 1, namedString);
                        }
                        if (loadPvh != null) {
                            if (arrayList == null) {
                                arrayList = new ArrayList();
                            }
                            arrayList.add(loadPvh);
                        }
                        obtainAttributes.recycle();
                    }
                    ArrayList arrayList2 = arrayList;
                    xmlPullParser.next();
                    arrayList = arrayList2;
                }
            }
        }
        if (arrayList == null) {
            return null;
        }
        int size = arrayList.size();
        PropertyValuesHolder[] propertyValuesHolderArr = new PropertyValuesHolder[size];
        for (int i = 0; i < size; i++) {
            propertyValuesHolderArr[i] = (PropertyValuesHolder) arrayList.get(i);
        }
        return propertyValuesHolderArr;
    }

    private static void parseAnimatorFromTypeArray(ValueAnimator valueAnimator, TypedArray typedArray, TypedArray typedArray2, float f, XmlPullParser xmlPullParser) {
        long namedInt = (long) TypedArrayUtils.getNamedInt(typedArray, xmlPullParser, "duration", 1, Strategy.TTL_SECONDS_DEFAULT);
        long namedInt2 = (long) TypedArrayUtils.getNamedInt(typedArray, xmlPullParser, "startOffset", 2, 0);
        int namedInt3 = TypedArrayUtils.getNamedInt(typedArray, xmlPullParser, "valueType", 7, 4);
        if (TypedArrayUtils.hasAttribute(xmlPullParser, "valueFrom") && TypedArrayUtils.hasAttribute(xmlPullParser, "valueTo")) {
            if (namedInt3 == 4) {
                namedInt3 = inferValueTypeFromValues(typedArray, 5, 6);
            }
            PropertyValuesHolder pvh = getPVH(typedArray, namedInt3, 5, 6, "");
            if (pvh != null) {
                valueAnimator.setValues(new PropertyValuesHolder[]{pvh});
            }
        }
        valueAnimator.setDuration(namedInt);
        valueAnimator.setStartDelay(namedInt2);
        valueAnimator.setRepeatCount(TypedArrayUtils.getNamedInt(typedArray, xmlPullParser, "repeatCount", 3, 0));
        valueAnimator.setRepeatMode(TypedArrayUtils.getNamedInt(typedArray, xmlPullParser, "repeatMode", 4, 1));
        if (typedArray2 != null) {
            setupObjectAnimator(valueAnimator, typedArray2, namedInt3, f, xmlPullParser);
        }
    }

    private static void setupObjectAnimator(ValueAnimator valueAnimator, TypedArray typedArray, int i, float f, XmlPullParser xmlPullParser) {
        ObjectAnimator objectAnimator = (ObjectAnimator) valueAnimator;
        String namedString = TypedArrayUtils.getNamedString(typedArray, xmlPullParser, "pathData", 1);
        if (namedString != null) {
            String namedString2 = TypedArrayUtils.getNamedString(typedArray, xmlPullParser, "propertyXName", 2);
            String namedString3 = TypedArrayUtils.getNamedString(typedArray, xmlPullParser, "propertyYName", 3);
            if (i == 2 || i == 4) {
            }
            if (namedString2 == null && namedString3 == null) {
                throw new InflateException(typedArray.getPositionDescription() + " propertyXName or propertyYName is needed for PathData");
            }
            setupPathMotion(PathParser.createPathFromPathData(namedString), objectAnimator, 0.5f * f, namedString2, namedString3);
            return;
        }
        objectAnimator.setPropertyName(TypedArrayUtils.getNamedString(typedArray, xmlPullParser, "propertyName", 0));
    }

    private static void setupPathMotion(Path path, ObjectAnimator objectAnimator, float f, String str, String str2) {
        int i;
        PathMeasure pathMeasure = new PathMeasure(path, false);
        float f2 = 0.0f;
        ArrayList arrayList = new ArrayList();
        arrayList.add(Float.valueOf(0.0f));
        do {
            f2 += pathMeasure.getLength();
            arrayList.add(Float.valueOf(f2));
        } while (pathMeasure.nextContour());
        PathMeasure pathMeasure2 = new PathMeasure(path, false);
        int min = Math.min(100, ((int) (f2 / f)) + 1);
        float[] fArr = new float[min];
        float[] fArr2 = new float[min];
        float[] fArr3 = new float[2];
        int i2 = 0;
        float f3 = f2 / ((float) (min - 1));
        float f4 = 0.0f;
        int i3 = 0;
        while (i3 < min) {
            pathMeasure2.getPosTan(f4, fArr3, null);
            pathMeasure2.getPosTan(f4, fArr3, null);
            fArr[i3] = fArr3[0];
            fArr2[i3] = fArr3[1];
            f4 += f3;
            if (i2 + 1 >= arrayList.size() || f4 <= ((Float) arrayList.get(i2 + 1)).floatValue()) {
                i = i2;
            } else {
                f4 -= ((Float) arrayList.get(i2 + 1)).floatValue();
                i = i2 + 1;
                pathMeasure2.nextContour();
            }
            i3++;
            i2 = i;
        }
        PropertyValuesHolder propertyValuesHolder = null;
        PropertyValuesHolder propertyValuesHolder2 = null;
        if (str != null) {
            propertyValuesHolder = PropertyValuesHolder.ofFloat(str, fArr);
        }
        if (str2 != null) {
            propertyValuesHolder2 = PropertyValuesHolder.ofFloat(str2, fArr2);
        }
        if (propertyValuesHolder == null) {
            objectAnimator.setValues(new PropertyValuesHolder[]{propertyValuesHolder2});
        } else if (propertyValuesHolder2 == null) {
            objectAnimator.setValues(new PropertyValuesHolder[]{propertyValuesHolder});
        } else {
            objectAnimator.setValues(new PropertyValuesHolder[]{propertyValuesHolder, propertyValuesHolder2});
        }
    }
}
