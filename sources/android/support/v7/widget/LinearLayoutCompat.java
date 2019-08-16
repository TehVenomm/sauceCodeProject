package android.support.p003v7.widget;

import android.content.Context;
import android.content.res.TypedArray;
import android.graphics.Canvas;
import android.graphics.drawable.Drawable;
import android.os.Build.VERSION;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import android.support.p000v4.view.GravityCompat;
import android.support.p000v4.view.InputDeviceCompat;
import android.support.p000v4.view.ViewCompat;
import android.support.p003v7.appcompat.C0260R;
import android.util.AttributeSet;
import android.view.View;
import android.view.View.MeasureSpec;
import android.view.ViewGroup;
import android.view.ViewGroup.MarginLayoutParams;
import android.view.accessibility.AccessibilityEvent;
import android.view.accessibility.AccessibilityNodeInfo;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;

/* renamed from: android.support.v7.widget.LinearLayoutCompat */
public class LinearLayoutCompat extends ViewGroup {
    public static final int HORIZONTAL = 0;
    private static final int INDEX_BOTTOM = 2;
    private static final int INDEX_CENTER_VERTICAL = 0;
    private static final int INDEX_FILL = 3;
    private static final int INDEX_TOP = 1;
    public static final int SHOW_DIVIDER_BEGINNING = 1;
    public static final int SHOW_DIVIDER_END = 4;
    public static final int SHOW_DIVIDER_MIDDLE = 2;
    public static final int SHOW_DIVIDER_NONE = 0;
    public static final int VERTICAL = 1;
    private static final int VERTICAL_GRAVITY_COUNT = 4;
    private boolean mBaselineAligned;
    private int mBaselineAlignedChildIndex;
    private int mBaselineChildTop;
    private Drawable mDivider;
    private int mDividerHeight;
    private int mDividerPadding;
    private int mDividerWidth;
    private int mGravity;
    private int[] mMaxAscent;
    private int[] mMaxDescent;
    private int mOrientation;
    private int mShowDividers;
    private int mTotalLength;
    private boolean mUseLargestChild;
    private float mWeightSum;

    @RestrictTo({Scope.LIBRARY_GROUP})
    @Retention(RetentionPolicy.SOURCE)
    /* renamed from: android.support.v7.widget.LinearLayoutCompat$DividerMode */
    public @interface DividerMode {
    }

    /* renamed from: android.support.v7.widget.LinearLayoutCompat$LayoutParams */
    public static class LayoutParams extends MarginLayoutParams {
        public int gravity;
        public float weight;

        public LayoutParams(int i, int i2) {
            super(i, i2);
            this.gravity = -1;
            this.weight = 0.0f;
        }

        public LayoutParams(int i, int i2, float f) {
            super(i, i2);
            this.gravity = -1;
            this.weight = f;
        }

        public LayoutParams(Context context, AttributeSet attributeSet) {
            super(context, attributeSet);
            this.gravity = -1;
            TypedArray obtainStyledAttributes = context.obtainStyledAttributes(attributeSet, C0260R.styleable.LinearLayoutCompat_Layout);
            this.weight = obtainStyledAttributes.getFloat(C0260R.styleable.LinearLayoutCompat_Layout_android_layout_weight, 0.0f);
            this.gravity = obtainStyledAttributes.getInt(C0260R.styleable.LinearLayoutCompat_Layout_android_layout_gravity, -1);
            obtainStyledAttributes.recycle();
        }

        public LayoutParams(LayoutParams layoutParams) {
            super(layoutParams);
            this.gravity = -1;
            this.weight = layoutParams.weight;
            this.gravity = layoutParams.gravity;
        }

        public LayoutParams(android.view.ViewGroup.LayoutParams layoutParams) {
            super(layoutParams);
            this.gravity = -1;
        }

        public LayoutParams(MarginLayoutParams marginLayoutParams) {
            super(marginLayoutParams);
            this.gravity = -1;
        }
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    @Retention(RetentionPolicy.SOURCE)
    /* renamed from: android.support.v7.widget.LinearLayoutCompat$OrientationMode */
    public @interface OrientationMode {
    }

    public LinearLayoutCompat(Context context) {
        this(context, null);
    }

    public LinearLayoutCompat(Context context, AttributeSet attributeSet) {
        this(context, attributeSet, 0);
    }

    public LinearLayoutCompat(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
        this.mBaselineAligned = true;
        this.mBaselineAlignedChildIndex = -1;
        this.mBaselineChildTop = 0;
        this.mGravity = 8388659;
        TintTypedArray obtainStyledAttributes = TintTypedArray.obtainStyledAttributes(context, attributeSet, C0260R.styleable.LinearLayoutCompat, i, 0);
        int i2 = obtainStyledAttributes.getInt(C0260R.styleable.LinearLayoutCompat_android_orientation, -1);
        if (i2 >= 0) {
            setOrientation(i2);
        }
        int i3 = obtainStyledAttributes.getInt(C0260R.styleable.LinearLayoutCompat_android_gravity, -1);
        if (i3 >= 0) {
            setGravity(i3);
        }
        boolean z = obtainStyledAttributes.getBoolean(C0260R.styleable.LinearLayoutCompat_android_baselineAligned, true);
        if (!z) {
            setBaselineAligned(z);
        }
        this.mWeightSum = obtainStyledAttributes.getFloat(C0260R.styleable.LinearLayoutCompat_android_weightSum, -1.0f);
        this.mBaselineAlignedChildIndex = obtainStyledAttributes.getInt(C0260R.styleable.LinearLayoutCompat_android_baselineAlignedChildIndex, -1);
        this.mUseLargestChild = obtainStyledAttributes.getBoolean(C0260R.styleable.LinearLayoutCompat_measureWithLargestChild, false);
        setDividerDrawable(obtainStyledAttributes.getDrawable(C0260R.styleable.LinearLayoutCompat_divider));
        this.mShowDividers = obtainStyledAttributes.getInt(C0260R.styleable.LinearLayoutCompat_showDividers, 0);
        this.mDividerPadding = obtainStyledAttributes.getDimensionPixelSize(C0260R.styleable.LinearLayoutCompat_dividerPadding, 0);
        obtainStyledAttributes.recycle();
    }

    private void forceUniformHeight(int i, int i2) {
        int makeMeasureSpec = MeasureSpec.makeMeasureSpec(getMeasuredHeight(), 1073741824);
        for (int i3 = 0; i3 < i; i3++) {
            View virtualChildAt = getVirtualChildAt(i3);
            if (virtualChildAt.getVisibility() != 8) {
                LayoutParams layoutParams = (LayoutParams) virtualChildAt.getLayoutParams();
                if (layoutParams.height == -1) {
                    int i4 = layoutParams.width;
                    layoutParams.width = virtualChildAt.getMeasuredWidth();
                    measureChildWithMargins(virtualChildAt, i2, 0, makeMeasureSpec, 0);
                    layoutParams.width = i4;
                }
            }
        }
    }

    private void forceUniformWidth(int i, int i2) {
        int makeMeasureSpec = MeasureSpec.makeMeasureSpec(getMeasuredWidth(), 1073741824);
        for (int i3 = 0; i3 < i; i3++) {
            View virtualChildAt = getVirtualChildAt(i3);
            if (virtualChildAt.getVisibility() != 8) {
                LayoutParams layoutParams = (LayoutParams) virtualChildAt.getLayoutParams();
                if (layoutParams.width == -1) {
                    int i4 = layoutParams.height;
                    layoutParams.height = virtualChildAt.getMeasuredHeight();
                    measureChildWithMargins(virtualChildAt, makeMeasureSpec, 0, i2, 0);
                    layoutParams.height = i4;
                }
            }
        }
    }

    private void setChildFrame(View view, int i, int i2, int i3, int i4) {
        view.layout(i, i2, i + i3, i2 + i4);
    }

    /* access modifiers changed from: protected */
    public boolean checkLayoutParams(android.view.ViewGroup.LayoutParams layoutParams) {
        return layoutParams instanceof LayoutParams;
    }

    /* access modifiers changed from: 0000 */
    public void drawDividersHorizontal(Canvas canvas) {
        int right;
        int virtualChildCount = getVirtualChildCount();
        boolean isLayoutRtl = ViewUtils.isLayoutRtl(this);
        for (int i = 0; i < virtualChildCount; i++) {
            View virtualChildAt = getVirtualChildAt(i);
            if (!(virtualChildAt == null || virtualChildAt.getVisibility() == 8 || !hasDividerBeforeChildAt(i))) {
                LayoutParams layoutParams = (LayoutParams) virtualChildAt.getLayoutParams();
                drawVerticalDivider(canvas, isLayoutRtl ? layoutParams.rightMargin + virtualChildAt.getRight() : (virtualChildAt.getLeft() - layoutParams.leftMargin) - this.mDividerWidth);
            }
        }
        if (hasDividerBeforeChildAt(virtualChildCount)) {
            View virtualChildAt2 = getVirtualChildAt(virtualChildCount - 1);
            if (virtualChildAt2 == null) {
                right = isLayoutRtl ? getPaddingLeft() : (getWidth() - getPaddingRight()) - this.mDividerWidth;
            } else {
                LayoutParams layoutParams2 = (LayoutParams) virtualChildAt2.getLayoutParams();
                right = isLayoutRtl ? (virtualChildAt2.getLeft() - layoutParams2.leftMargin) - this.mDividerWidth : layoutParams2.rightMargin + virtualChildAt2.getRight();
            }
            drawVerticalDivider(canvas, right);
        }
    }

    /* access modifiers changed from: 0000 */
    public void drawDividersVertical(Canvas canvas) {
        int bottom;
        int virtualChildCount = getVirtualChildCount();
        for (int i = 0; i < virtualChildCount; i++) {
            View virtualChildAt = getVirtualChildAt(i);
            if (!(virtualChildAt == null || virtualChildAt.getVisibility() == 8 || !hasDividerBeforeChildAt(i))) {
                drawHorizontalDivider(canvas, (virtualChildAt.getTop() - ((LayoutParams) virtualChildAt.getLayoutParams()).topMargin) - this.mDividerHeight);
            }
        }
        if (hasDividerBeforeChildAt(virtualChildCount)) {
            View virtualChildAt2 = getVirtualChildAt(virtualChildCount - 1);
            if (virtualChildAt2 == null) {
                bottom = (getHeight() - getPaddingBottom()) - this.mDividerHeight;
            } else {
                LayoutParams layoutParams = (LayoutParams) virtualChildAt2.getLayoutParams();
                bottom = layoutParams.bottomMargin + virtualChildAt2.getBottom();
            }
            drawHorizontalDivider(canvas, bottom);
        }
    }

    /* access modifiers changed from: 0000 */
    public void drawHorizontalDivider(Canvas canvas, int i) {
        this.mDivider.setBounds(getPaddingLeft() + this.mDividerPadding, i, (getWidth() - getPaddingRight()) - this.mDividerPadding, this.mDividerHeight + i);
        this.mDivider.draw(canvas);
    }

    /* access modifiers changed from: 0000 */
    public void drawVerticalDivider(Canvas canvas, int i) {
        this.mDivider.setBounds(i, getPaddingTop() + this.mDividerPadding, this.mDividerWidth + i, (getHeight() - getPaddingBottom()) - this.mDividerPadding);
        this.mDivider.draw(canvas);
    }

    /* access modifiers changed from: protected */
    public LayoutParams generateDefaultLayoutParams() {
        if (this.mOrientation == 0) {
            return new LayoutParams(-2, -2);
        }
        if (this.mOrientation == 1) {
            return new LayoutParams(-1, -2);
        }
        return null;
    }

    public LayoutParams generateLayoutParams(AttributeSet attributeSet) {
        return new LayoutParams(getContext(), attributeSet);
    }

    /* access modifiers changed from: protected */
    public LayoutParams generateLayoutParams(android.view.ViewGroup.LayoutParams layoutParams) {
        return new LayoutParams(layoutParams);
    }

    public int getBaseline() {
        int i;
        if (this.mBaselineAlignedChildIndex < 0) {
            return super.getBaseline();
        }
        if (getChildCount() <= this.mBaselineAlignedChildIndex) {
            throw new RuntimeException("mBaselineAlignedChildIndex of LinearLayout set to an index that is out of bounds.");
        }
        View childAt = getChildAt(this.mBaselineAlignedChildIndex);
        int baseline = childAt.getBaseline();
        if (baseline != -1) {
            int i2 = this.mBaselineChildTop;
            if (this.mOrientation == 1) {
                int i3 = this.mGravity & 112;
                if (i3 != 48) {
                    switch (i3) {
                        case 16:
                            i = i2 + (((((getBottom() - getTop()) - getPaddingTop()) - getPaddingBottom()) - this.mTotalLength) / 2);
                            break;
                        case 80:
                            i = ((getBottom() - getTop()) - getPaddingBottom()) - this.mTotalLength;
                            break;
                        default:
                            i = i2;
                            break;
                    }
                    return ((LayoutParams) childAt.getLayoutParams()).topMargin + i + baseline;
                }
            }
            i = i2;
            return ((LayoutParams) childAt.getLayoutParams()).topMargin + i + baseline;
        } else if (this.mBaselineAlignedChildIndex == 0) {
            return -1;
        } else {
            throw new RuntimeException("mBaselineAlignedChildIndex of LinearLayout points to a View that doesn't know how to get its baseline.");
        }
    }

    public int getBaselineAlignedChildIndex() {
        return this.mBaselineAlignedChildIndex;
    }

    /* access modifiers changed from: 0000 */
    public int getChildrenSkipCount(View view, int i) {
        return 0;
    }

    public Drawable getDividerDrawable() {
        return this.mDivider;
    }

    public int getDividerPadding() {
        return this.mDividerPadding;
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    public int getDividerWidth() {
        return this.mDividerWidth;
    }

    public int getGravity() {
        return this.mGravity;
    }

    /* access modifiers changed from: 0000 */
    public int getLocationOffset(View view) {
        return 0;
    }

    /* access modifiers changed from: 0000 */
    public int getNextLocationOffset(View view) {
        return 0;
    }

    public int getOrientation() {
        return this.mOrientation;
    }

    public int getShowDividers() {
        return this.mShowDividers;
    }

    /* access modifiers changed from: 0000 */
    public View getVirtualChildAt(int i) {
        return getChildAt(i);
    }

    /* access modifiers changed from: 0000 */
    public int getVirtualChildCount() {
        return getChildCount();
    }

    public float getWeightSum() {
        return this.mWeightSum;
    }

    /* access modifiers changed from: protected */
    public boolean hasDividerBeforeChildAt(int i) {
        if (i == 0) {
            if ((this.mShowDividers & 1) == 0) {
                return false;
            }
        } else if (i == getChildCount()) {
            if ((this.mShowDividers & 4) == 0) {
                return false;
            }
        } else if ((this.mShowDividers & 2) == 0) {
            return false;
        } else {
            for (int i2 = i - 1; i2 >= 0; i2--) {
                if (getChildAt(i2).getVisibility() != 8) {
                    return true;
                }
            }
            return false;
        }
        return true;
    }

    public boolean isBaselineAligned() {
        return this.mBaselineAligned;
    }

    public boolean isMeasureWithLargestChildEnabled() {
        return this.mUseLargestChild;
    }

    /* access modifiers changed from: 0000 */
    public void layoutHorizontal(int i, int i2, int i3, int i4) {
        int paddingLeft;
        int i5;
        int i6;
        int i7;
        int i8;
        int i9;
        boolean isLayoutRtl = ViewUtils.isLayoutRtl(this);
        int paddingTop = getPaddingTop();
        int i10 = i4 - i2;
        int paddingBottom = getPaddingBottom();
        int paddingBottom2 = getPaddingBottom();
        int virtualChildCount = getVirtualChildCount();
        int i11 = this.mGravity;
        int i12 = this.mGravity;
        boolean z = this.mBaselineAligned;
        int[] iArr = this.mMaxAscent;
        int[] iArr2 = this.mMaxDescent;
        switch (GravityCompat.getAbsoluteGravity(i11 & GravityCompat.RELATIVE_HORIZONTAL_GRAVITY_MASK, ViewCompat.getLayoutDirection(this))) {
            case 1:
                paddingLeft = getPaddingLeft() + (((i3 - i) - this.mTotalLength) / 2);
                break;
            case 5:
                paddingLeft = ((getPaddingLeft() + i3) - i) - this.mTotalLength;
                break;
            default:
                paddingLeft = getPaddingLeft();
                break;
        }
        if (isLayoutRtl) {
            i5 = virtualChildCount - 1;
            i6 = -1;
        } else {
            i5 = 0;
            i6 = 1;
        }
        int i13 = 0;
        while (i13 < virtualChildCount) {
            int i14 = i5 + (i6 * i13);
            View virtualChildAt = getVirtualChildAt(i14);
            if (virtualChildAt == null) {
                i8 = paddingLeft + measureNullChild(i14);
                i7 = i13;
            } else if (virtualChildAt.getVisibility() != 8) {
                int measuredWidth = virtualChildAt.getMeasuredWidth();
                int measuredHeight = virtualChildAt.getMeasuredHeight();
                LayoutParams layoutParams = (LayoutParams) virtualChildAt.getLayoutParams();
                int i15 = (!z || layoutParams.height == -1) ? -1 : virtualChildAt.getBaseline();
                int i16 = layoutParams.gravity;
                if (i16 < 0) {
                    i16 = i12 & 112;
                }
                switch (i16 & 112) {
                    case 16:
                        i9 = ((((((i10 - paddingTop) - paddingBottom2) - measuredHeight) / 2) + paddingTop) + layoutParams.topMargin) - layoutParams.bottomMargin;
                        break;
                    case 48:
                        i9 = layoutParams.topMargin + paddingTop;
                        if (i15 != -1) {
                            i9 += iArr[1] - i15;
                            break;
                        }
                        break;
                    case 80:
                        i9 = ((i10 - paddingBottom) - measuredHeight) - layoutParams.bottomMargin;
                        if (i15 != -1) {
                            i9 -= iArr2[2] - (virtualChildAt.getMeasuredHeight() - i15);
                            break;
                        }
                        break;
                    default:
                        i9 = paddingTop;
                        break;
                }
                if (hasDividerBeforeChildAt(i14)) {
                    paddingLeft += this.mDividerWidth;
                }
                int i17 = paddingLeft + layoutParams.leftMargin;
                setChildFrame(virtualChildAt, i17 + getLocationOffset(virtualChildAt), i9, measuredWidth, measuredHeight);
                i7 = getChildrenSkipCount(virtualChildAt, i14) + i13;
                i8 = i17 + layoutParams.rightMargin + measuredWidth + getNextLocationOffset(virtualChildAt);
            } else {
                i7 = i13;
                i8 = paddingLeft;
            }
            i13 = i7 + 1;
            paddingLeft = i8;
        }
    }

    /* access modifiers changed from: 0000 */
    public void layoutVertical(int i, int i2, int i3, int i4) {
        int paddingTop;
        int i5;
        int i6;
        int i7;
        int paddingLeft = getPaddingLeft();
        int i8 = i3 - i;
        int paddingRight = getPaddingRight();
        int paddingRight2 = getPaddingRight();
        int virtualChildCount = getVirtualChildCount();
        int i9 = this.mGravity;
        int i10 = this.mGravity;
        switch (i9 & 112) {
            case 16:
                paddingTop = getPaddingTop() + (((i4 - i2) - this.mTotalLength) / 2);
                break;
            case 80:
                paddingTop = ((getPaddingTop() + i4) - i2) - this.mTotalLength;
                break;
            default:
                paddingTop = getPaddingTop();
                break;
        }
        int i11 = 0;
        int i12 = paddingTop;
        while (i11 < virtualChildCount) {
            View virtualChildAt = getVirtualChildAt(i11);
            if (virtualChildAt == null) {
                i6 = i12 + measureNullChild(i11);
                i5 = i11;
            } else if (virtualChildAt.getVisibility() != 8) {
                int measuredWidth = virtualChildAt.getMeasuredWidth();
                int measuredHeight = virtualChildAt.getMeasuredHeight();
                LayoutParams layoutParams = (LayoutParams) virtualChildAt.getLayoutParams();
                int i13 = layoutParams.gravity;
                if (i13 < 0) {
                    i13 = 8388615 & i10;
                }
                switch (GravityCompat.getAbsoluteGravity(i13, ViewCompat.getLayoutDirection(this)) & 7) {
                    case 1:
                        i7 = ((((((i8 - paddingLeft) - paddingRight2) - measuredWidth) / 2) + paddingLeft) + layoutParams.leftMargin) - layoutParams.rightMargin;
                        break;
                    case 5:
                        i7 = ((i8 - paddingRight) - measuredWidth) - layoutParams.rightMargin;
                        break;
                    default:
                        i7 = paddingLeft + layoutParams.leftMargin;
                        break;
                }
                if (hasDividerBeforeChildAt(i11)) {
                    i12 += this.mDividerHeight;
                }
                int i14 = i12 + layoutParams.topMargin;
                setChildFrame(virtualChildAt, i7, i14 + getLocationOffset(virtualChildAt), measuredWidth, measuredHeight);
                i5 = getChildrenSkipCount(virtualChildAt, i11) + i11;
                i6 = i14 + layoutParams.bottomMargin + measuredHeight + getNextLocationOffset(virtualChildAt);
            } else {
                i5 = i11;
                i6 = i12;
            }
            i11 = i5 + 1;
            i12 = i6;
        }
    }

    /* access modifiers changed from: 0000 */
    public void measureChildBeforeLayout(View view, int i, int i2, int i3, int i4, int i5) {
        measureChildWithMargins(view, i2, i3, i4, i5);
    }

    /* access modifiers changed from: 0000 */
    public void measureHorizontal(int i, int i2) {
        int i3;
        int i4;
        int i5;
        boolean z;
        int i6;
        float f;
        int i7;
        boolean z2;
        boolean z3;
        boolean z4;
        int max;
        int max2;
        int i8;
        boolean z5;
        int i9;
        this.mTotalLength = 0;
        int i10 = 0;
        int i11 = 0;
        int i12 = 0;
        int i13 = 0;
        boolean z6 = true;
        float f2 = 0.0f;
        int virtualChildCount = getVirtualChildCount();
        int mode = MeasureSpec.getMode(i);
        int mode2 = MeasureSpec.getMode(i2);
        boolean z7 = false;
        boolean z8 = false;
        if (this.mMaxAscent == null || this.mMaxDescent == null) {
            this.mMaxAscent = new int[4];
            this.mMaxDescent = new int[4];
        }
        int[] iArr = this.mMaxAscent;
        int[] iArr2 = this.mMaxDescent;
        iArr[3] = -1;
        iArr[2] = -1;
        iArr[1] = -1;
        iArr[0] = -1;
        iArr2[3] = -1;
        iArr2[2] = -1;
        iArr2[1] = -1;
        iArr2[0] = -1;
        boolean z9 = this.mBaselineAligned;
        boolean z10 = this.mUseLargestChild;
        boolean z11 = mode == 1073741824;
        int i14 = Integer.MIN_VALUE;
        int i15 = 0;
        while (i15 < virtualChildCount) {
            View virtualChildAt = getVirtualChildAt(i15);
            if (virtualChildAt == null) {
                this.mTotalLength += measureNullChild(i15);
                i7 = i14;
                z5 = z8;
                max = i10;
                i8 = i13;
                z4 = z7;
                i9 = i11;
                f = f2;
            } else if (virtualChildAt.getVisibility() == 8) {
                i15 += getChildrenSkipCount(virtualChildAt, i15);
                i7 = i14;
                z5 = z8;
                max = i10;
                i8 = i13;
                z4 = z7;
                i9 = i11;
                f = f2;
            } else {
                if (hasDividerBeforeChildAt(i15)) {
                    this.mTotalLength += this.mDividerWidth;
                }
                LayoutParams layoutParams = (LayoutParams) virtualChildAt.getLayoutParams();
                f = f2 + layoutParams.weight;
                if (mode == 1073741824 && layoutParams.width == 0 && layoutParams.weight > 0.0f) {
                    if (z11) {
                        this.mTotalLength += layoutParams.leftMargin + layoutParams.rightMargin;
                    } else {
                        int i16 = this.mTotalLength;
                        this.mTotalLength = Math.max(i16, layoutParams.leftMargin + i16 + layoutParams.rightMargin);
                    }
                    if (z9) {
                        int makeMeasureSpec = MeasureSpec.makeMeasureSpec(0, 0);
                        virtualChildAt.measure(makeMeasureSpec, makeMeasureSpec);
                        i7 = i14;
                        z2 = z8;
                    } else {
                        i7 = i14;
                        z2 = true;
                    }
                } else {
                    int i17 = Integer.MIN_VALUE;
                    if (layoutParams.width == 0 && layoutParams.weight > 0.0f) {
                        i17 = 0;
                        layoutParams.width = -2;
                    }
                    int i18 = i17;
                    measureChildBeforeLayout(virtualChildAt, i15, i, f == 0.0f ? this.mTotalLength : 0, i2, 0);
                    if (i18 != Integer.MIN_VALUE) {
                        layoutParams.width = i18;
                    }
                    int measuredWidth = virtualChildAt.getMeasuredWidth();
                    if (z11) {
                        this.mTotalLength += layoutParams.leftMargin + measuredWidth + layoutParams.rightMargin + getNextLocationOffset(virtualChildAt);
                    } else {
                        int i19 = this.mTotalLength;
                        this.mTotalLength = Math.max(i19, i19 + measuredWidth + layoutParams.leftMargin + layoutParams.rightMargin + getNextLocationOffset(virtualChildAt));
                    }
                    if (z10) {
                        i7 = Math.max(measuredWidth, i14);
                        z2 = z8;
                    } else {
                        i7 = i14;
                        z2 = z8;
                    }
                }
                if (mode2 == 1073741824 || layoutParams.height != -1) {
                    z3 = false;
                    z4 = z7;
                } else {
                    z3 = true;
                    z4 = true;
                }
                int i20 = layoutParams.topMargin + layoutParams.bottomMargin;
                int measuredHeight = virtualChildAt.getMeasuredHeight() + i20;
                int combineMeasuredStates = View.combineMeasuredStates(i11, virtualChildAt.getMeasuredState());
                if (z9) {
                    int baseline = virtualChildAt.getBaseline();
                    if (baseline != -1) {
                        int i21 = ((((layoutParams.gravity < 0 ? this.mGravity : layoutParams.gravity) & 112) >> 4) & -2) >> 1;
                        iArr[i21] = Math.max(iArr[i21], baseline);
                        iArr2[i21] = Math.max(iArr2[i21], measuredHeight - baseline);
                    }
                }
                max = Math.max(i10, measuredHeight);
                boolean z12 = z6 && layoutParams.height == -1;
                if (layoutParams.weight > 0.0f) {
                    i8 = Math.max(i13, z3 ? i20 : measuredHeight);
                    max2 = i12;
                } else {
                    if (z3) {
                        measuredHeight = i20;
                    }
                    max2 = Math.max(i12, measuredHeight);
                    i8 = i13;
                }
                i15 += getChildrenSkipCount(virtualChildAt, i15);
                z5 = z2;
                i9 = combineMeasuredStates;
                z6 = z12;
                i12 = max2;
            }
            i15++;
            z8 = z5;
            i10 = max;
            i14 = i7;
            i13 = i8;
            z7 = z4;
            i11 = i9;
            f2 = f;
        }
        if (this.mTotalLength > 0 && hasDividerBeforeChildAt(virtualChildCount)) {
            this.mTotalLength += this.mDividerWidth;
        }
        int max3 = (iArr[1] == -1 && iArr[0] == -1 && iArr[2] == -1 && iArr[3] == -1) ? i10 : Math.max(i10, Math.max(iArr[3], Math.max(iArr[0], Math.max(iArr[1], iArr[2]))) + Math.max(iArr2[3], Math.max(iArr2[0], Math.max(iArr2[1], iArr2[2]))));
        if (z10 && (mode == Integer.MIN_VALUE || mode == 0)) {
            this.mTotalLength = 0;
            int i22 = 0;
            while (i22 < virtualChildCount) {
                View virtualChildAt2 = getVirtualChildAt(i22);
                if (virtualChildAt2 == null) {
                    this.mTotalLength += measureNullChild(i22);
                    i6 = i22;
                } else if (virtualChildAt2.getVisibility() == 8) {
                    i6 = getChildrenSkipCount(virtualChildAt2, i22) + i22;
                } else {
                    LayoutParams layoutParams2 = (LayoutParams) virtualChildAt2.getLayoutParams();
                    if (z11) {
                        this.mTotalLength = layoutParams2.rightMargin + layoutParams2.leftMargin + i14 + getNextLocationOffset(virtualChildAt2) + this.mTotalLength;
                        i6 = i22;
                    } else {
                        int i23 = this.mTotalLength;
                        this.mTotalLength = Math.max(i23, layoutParams2.rightMargin + i23 + i14 + layoutParams2.leftMargin + getNextLocationOffset(virtualChildAt2));
                        i6 = i22;
                    }
                }
                i22 = i6 + 1;
            }
        }
        this.mTotalLength += getPaddingLeft() + getPaddingRight();
        int resolveSizeAndState = View.resolveSizeAndState(Math.max(this.mTotalLength, getSuggestedMinimumWidth()), i, 0);
        int i24 = (16777215 & resolveSizeAndState) - this.mTotalLength;
        if (z8 || (i24 != 0 && f2 > 0.0f)) {
            if (this.mWeightSum > 0.0f) {
                f2 = this.mWeightSum;
            }
            iArr[3] = -1;
            iArr[2] = -1;
            iArr[1] = -1;
            iArr[0] = -1;
            iArr2[3] = -1;
            iArr2[2] = -1;
            iArr2[1] = -1;
            iArr2[0] = -1;
            int i25 = -1;
            this.mTotalLength = 0;
            int i26 = 0;
            int i27 = i12;
            int i28 = i11;
            boolean z13 = z6;
            float f3 = f2;
            while (i26 < virtualChildCount) {
                View virtualChildAt3 = getVirtualChildAt(i26);
                if (virtualChildAt3 == null) {
                    i5 = i24;
                    z = z13;
                } else if (virtualChildAt3.getVisibility() == 8) {
                    i5 = i24;
                    z = z13;
                } else {
                    LayoutParams layoutParams3 = (LayoutParams) virtualChildAt3.getLayoutParams();
                    float f4 = layoutParams3.weight;
                    if (f4 > 0.0f) {
                        int i29 = (int) ((((float) i24) * f4) / f3);
                        f3 -= f4;
                        int i30 = i24 - i29;
                        int childMeasureSpec = getChildMeasureSpec(i2, getPaddingTop() + getPaddingBottom() + layoutParams3.topMargin + layoutParams3.bottomMargin, layoutParams3.height);
                        if (layoutParams3.width == 0 && mode == 1073741824) {
                            virtualChildAt3.measure(MeasureSpec.makeMeasureSpec(i29 > 0 ? i29 : 0, 1073741824), childMeasureSpec);
                        } else {
                            int measuredWidth2 = virtualChildAt3.getMeasuredWidth() + i29;
                            if (measuredWidth2 < 0) {
                                measuredWidth2 = 0;
                            }
                            virtualChildAt3.measure(MeasureSpec.makeMeasureSpec(measuredWidth2, 1073741824), childMeasureSpec);
                        }
                        i28 = View.combineMeasuredStates(i28, virtualChildAt3.getMeasuredState() & ViewCompat.MEASURED_STATE_MASK);
                        i24 = i30;
                    }
                    if (z11) {
                        this.mTotalLength += virtualChildAt3.getMeasuredWidth() + layoutParams3.leftMargin + layoutParams3.rightMargin + getNextLocationOffset(virtualChildAt3);
                    } else {
                        int i31 = this.mTotalLength;
                        this.mTotalLength = Math.max(i31, virtualChildAt3.getMeasuredWidth() + i31 + layoutParams3.leftMargin + layoutParams3.rightMargin + getNextLocationOffset(virtualChildAt3));
                    }
                    boolean z14 = mode2 != 1073741824 && layoutParams3.height == -1;
                    int i32 = layoutParams3.topMargin + layoutParams3.bottomMargin;
                    int measuredHeight2 = virtualChildAt3.getMeasuredHeight() + i32;
                    i25 = Math.max(i25, measuredHeight2);
                    i27 = Math.max(i27, z14 ? i32 : measuredHeight2);
                    z = z13 && layoutParams3.height == -1;
                    if (z9) {
                        int baseline2 = virtualChildAt3.getBaseline();
                        if (baseline2 != -1) {
                            int i33 = ((((layoutParams3.gravity < 0 ? this.mGravity : layoutParams3.gravity) & 112) >> 4) & -2) >> 1;
                            iArr[i33] = Math.max(iArr[i33], baseline2);
                            iArr2[i33] = Math.max(iArr2[i33], measuredHeight2 - baseline2);
                            i5 = i24;
                        }
                    }
                    i5 = i24;
                }
                i26++;
                i24 = i5;
                z13 = z;
            }
            this.mTotalLength += getPaddingLeft() + getPaddingRight();
            if (iArr[1] == -1 && iArr[0] == -1 && iArr[2] == -1 && iArr[3] == -1) {
                z6 = z13;
                i11 = i28;
                i4 = i27;
                i3 = i25;
            } else {
                i3 = Math.max(i25, Math.max(iArr[3], Math.max(iArr[0], Math.max(iArr[1], iArr[2]))) + Math.max(iArr2[3], Math.max(iArr2[0], Math.max(iArr2[1], iArr2[2]))));
                z6 = z13;
                i11 = i28;
                i4 = i27;
            }
        } else {
            int max4 = Math.max(i12, i13);
            if (z10 && mode != 1073741824) {
                int i34 = 0;
                while (true) {
                    int i35 = i34;
                    if (i35 >= virtualChildCount) {
                        break;
                    }
                    View virtualChildAt4 = getVirtualChildAt(i35);
                    if (!(virtualChildAt4 == null || virtualChildAt4.getVisibility() == 8 || ((LayoutParams) virtualChildAt4.getLayoutParams()).weight <= 0.0f)) {
                        virtualChildAt4.measure(MeasureSpec.makeMeasureSpec(i14, 1073741824), MeasureSpec.makeMeasureSpec(virtualChildAt4.getMeasuredHeight(), 1073741824));
                    }
                    i34 = i35 + 1;
                }
            }
            i4 = max4;
            i3 = max3;
        }
        if (z6 || mode2 == 1073741824) {
            i4 = i3;
        }
        setMeasuredDimension((-16777216 & i11) | resolveSizeAndState, View.resolveSizeAndState(Math.max(i4 + getPaddingTop() + getPaddingBottom(), getSuggestedMinimumHeight()), i2, i11 << 16));
        if (z7) {
            forceUniformHeight(virtualChildCount, i);
        }
    }

    /* access modifiers changed from: 0000 */
    public int measureNullChild(int i) {
        return 0;
    }

    /* access modifiers changed from: 0000 */
    public void measureVertical(int i, int i2) {
        int i3;
        int i4;
        int i5;
        int i6;
        int i7;
        float f;
        int i8;
        boolean z;
        boolean z2;
        int max;
        int max2;
        int i9;
        boolean z3;
        int i10;
        this.mTotalLength = 0;
        int i11 = 0;
        int i12 = 0;
        int i13 = 0;
        int i14 = 0;
        boolean z4 = true;
        float f2 = 0.0f;
        int virtualChildCount = getVirtualChildCount();
        int mode = MeasureSpec.getMode(i);
        int mode2 = MeasureSpec.getMode(i2);
        boolean z5 = false;
        boolean z6 = false;
        int i15 = this.mBaselineAlignedChildIndex;
        boolean z7 = this.mUseLargestChild;
        int i16 = Integer.MIN_VALUE;
        int i17 = 0;
        while (i17 < virtualChildCount) {
            View virtualChildAt = getVirtualChildAt(i17);
            if (virtualChildAt == null) {
                this.mTotalLength += measureNullChild(i17);
                i8 = i16;
                z3 = z6;
                i9 = i14;
                z2 = z5;
                max = i11;
                i10 = i12;
                f = f2;
            } else if (virtualChildAt.getVisibility() == 8) {
                i17 += getChildrenSkipCount(virtualChildAt, i17);
                i8 = i16;
                z3 = z6;
                i9 = i14;
                z2 = z5;
                max = i11;
                i10 = i12;
                f = f2;
            } else {
                if (hasDividerBeforeChildAt(i17)) {
                    this.mTotalLength += this.mDividerHeight;
                }
                LayoutParams layoutParams = (LayoutParams) virtualChildAt.getLayoutParams();
                f = f2 + layoutParams.weight;
                if (mode2 == 1073741824 && layoutParams.height == 0 && layoutParams.weight > 0.0f) {
                    int i18 = this.mTotalLength;
                    this.mTotalLength = Math.max(i18, layoutParams.topMargin + i18 + layoutParams.bottomMargin);
                    i8 = i16;
                    z = true;
                } else {
                    int i19 = Integer.MIN_VALUE;
                    if (layoutParams.height == 0 && layoutParams.weight > 0.0f) {
                        i19 = 0;
                        layoutParams.height = -2;
                    }
                    int i20 = i19;
                    measureChildBeforeLayout(virtualChildAt, i17, i, 0, i2, f == 0.0f ? this.mTotalLength : 0);
                    if (i20 != Integer.MIN_VALUE) {
                        layoutParams.height = i20;
                    }
                    int measuredHeight = virtualChildAt.getMeasuredHeight();
                    int i21 = this.mTotalLength;
                    this.mTotalLength = Math.max(i21, i21 + measuredHeight + layoutParams.topMargin + layoutParams.bottomMargin + getNextLocationOffset(virtualChildAt));
                    if (z7) {
                        i8 = Math.max(measuredHeight, i16);
                        z = z6;
                    } else {
                        i8 = i16;
                        z = z6;
                    }
                }
                if (i15 >= 0 && i15 == i17 + 1) {
                    this.mBaselineChildTop = this.mTotalLength;
                }
                if (i17 >= i15 || layoutParams.weight <= 0.0f) {
                    boolean z8 = false;
                    if (mode == 1073741824 || layoutParams.width != -1) {
                        z2 = z5;
                    } else {
                        z8 = true;
                        z2 = true;
                    }
                    int i22 = layoutParams.leftMargin + layoutParams.rightMargin;
                    int measuredWidth = virtualChildAt.getMeasuredWidth() + i22;
                    max = Math.max(i11, measuredWidth);
                    int combineMeasuredStates = View.combineMeasuredStates(i12, virtualChildAt.getMeasuredState());
                    boolean z9 = z4 && layoutParams.width == -1;
                    if (layoutParams.weight > 0.0f) {
                        i9 = Math.max(i14, z8 ? i22 : measuredWidth);
                        max2 = i13;
                    } else {
                        if (z8) {
                            measuredWidth = i22;
                        }
                        max2 = Math.max(i13, measuredWidth);
                        i9 = i14;
                    }
                    i17 += getChildrenSkipCount(virtualChildAt, i17);
                    z3 = z;
                    i10 = combineMeasuredStates;
                    z4 = z9;
                    i13 = max2;
                } else {
                    throw new RuntimeException("A child of LinearLayout with index less than mBaselineAlignedChildIndex has weight > 0, which won't work.  Either remove the weight, or don't set mBaselineAlignedChildIndex.");
                }
            }
            i17++;
            z6 = z3;
            i16 = i8;
            i14 = i9;
            z5 = z2;
            i11 = max;
            i12 = i10;
            f2 = f;
        }
        if (this.mTotalLength > 0 && hasDividerBeforeChildAt(virtualChildCount)) {
            this.mTotalLength += this.mDividerHeight;
        }
        if (z7 && (mode2 == Integer.MIN_VALUE || mode2 == 0)) {
            this.mTotalLength = 0;
            int i23 = 0;
            while (i23 < virtualChildCount) {
                View virtualChildAt2 = getVirtualChildAt(i23);
                if (virtualChildAt2 == null) {
                    this.mTotalLength += measureNullChild(i23);
                    i7 = i23;
                } else if (virtualChildAt2.getVisibility() == 8) {
                    i7 = getChildrenSkipCount(virtualChildAt2, i23) + i23;
                } else {
                    LayoutParams layoutParams2 = (LayoutParams) virtualChildAt2.getLayoutParams();
                    int i24 = this.mTotalLength;
                    this.mTotalLength = Math.max(i24, layoutParams2.bottomMargin + i24 + i16 + layoutParams2.topMargin + getNextLocationOffset(virtualChildAt2));
                    i7 = i23;
                }
                i23 = i7 + 1;
            }
        }
        this.mTotalLength += getPaddingTop() + getPaddingBottom();
        int resolveSizeAndState = View.resolveSizeAndState(Math.max(this.mTotalLength, getSuggestedMinimumHeight()), i2, 0);
        int i25 = (16777215 & resolveSizeAndState) - this.mTotalLength;
        if (z6 || (i25 != 0 && f2 > 0.0f)) {
            if (this.mWeightSum > 0.0f) {
                f2 = this.mWeightSum;
            }
            this.mTotalLength = 0;
            int i26 = 0;
            i3 = i11;
            int i27 = i12;
            boolean z10 = z4;
            int i28 = i13;
            float f3 = f2;
            while (i26 < virtualChildCount) {
                View virtualChildAt3 = getVirtualChildAt(i26);
                if (virtualChildAt3.getVisibility() == 8) {
                    i6 = i3;
                    i5 = i27;
                } else {
                    LayoutParams layoutParams3 = (LayoutParams) virtualChildAt3.getLayoutParams();
                    float f4 = layoutParams3.weight;
                    if (f4 > 0.0f) {
                        int i29 = (int) ((((float) i25) * f4) / f3);
                        f3 -= f4;
                        int i30 = i25 - i29;
                        int childMeasureSpec = getChildMeasureSpec(i, getPaddingLeft() + getPaddingRight() + layoutParams3.leftMargin + layoutParams3.rightMargin, layoutParams3.width);
                        if (layoutParams3.height == 0 && mode2 == 1073741824) {
                            virtualChildAt3.measure(childMeasureSpec, MeasureSpec.makeMeasureSpec(i29 > 0 ? i29 : 0, 1073741824));
                        } else {
                            int measuredHeight2 = virtualChildAt3.getMeasuredHeight() + i29;
                            if (measuredHeight2 < 0) {
                                measuredHeight2 = 0;
                            }
                            virtualChildAt3.measure(childMeasureSpec, MeasureSpec.makeMeasureSpec(measuredHeight2, 1073741824));
                        }
                        i5 = View.combineMeasuredStates(i27, virtualChildAt3.getMeasuredState() & InputDeviceCompat.SOURCE_ANY);
                        i25 = i30;
                    } else {
                        i5 = i27;
                    }
                    int i31 = layoutParams3.leftMargin + layoutParams3.rightMargin;
                    int measuredWidth2 = virtualChildAt3.getMeasuredWidth() + i31;
                    int max3 = Math.max(i3, measuredWidth2);
                    if (!(mode != 1073741824 && layoutParams3.width == -1)) {
                        i31 = measuredWidth2;
                    }
                    i28 = Math.max(i28, i31);
                    boolean z11 = z10 && layoutParams3.width == -1;
                    int i32 = this.mTotalLength;
                    this.mTotalLength = Math.max(i32, layoutParams3.bottomMargin + virtualChildAt3.getMeasuredHeight() + i32 + layoutParams3.topMargin + getNextLocationOffset(virtualChildAt3));
                    i6 = max3;
                    z10 = z11;
                }
                i26++;
                i3 = i6;
                i27 = i5;
            }
            this.mTotalLength += getPaddingTop() + getPaddingBottom();
            z4 = z10;
            i12 = i27;
            i4 = i28;
        } else {
            int max4 = Math.max(i13, i14);
            if (z7 && mode2 != 1073741824) {
                int i33 = 0;
                while (true) {
                    int i34 = i33;
                    if (i34 >= virtualChildCount) {
                        break;
                    }
                    View virtualChildAt4 = getVirtualChildAt(i34);
                    if (!(virtualChildAt4 == null || virtualChildAt4.getVisibility() == 8 || ((LayoutParams) virtualChildAt4.getLayoutParams()).weight <= 0.0f)) {
                        virtualChildAt4.measure(MeasureSpec.makeMeasureSpec(virtualChildAt4.getMeasuredWidth(), 1073741824), MeasureSpec.makeMeasureSpec(i16, 1073741824));
                    }
                    i33 = i34 + 1;
                }
            }
            i3 = i11;
            i4 = max4;
        }
        if (z4 || mode == 1073741824) {
            i4 = i3;
        }
        setMeasuredDimension(View.resolveSizeAndState(Math.max(i4 + getPaddingLeft() + getPaddingRight(), getSuggestedMinimumWidth()), i, i12), resolveSizeAndState);
        if (z5) {
            forceUniformWidth(virtualChildCount, i2);
        }
    }

    /* access modifiers changed from: protected */
    public void onDraw(Canvas canvas) {
        if (this.mDivider != null) {
            if (this.mOrientation == 1) {
                drawDividersVertical(canvas);
            } else {
                drawDividersHorizontal(canvas);
            }
        }
    }

    public void onInitializeAccessibilityEvent(AccessibilityEvent accessibilityEvent) {
        if (VERSION.SDK_INT >= 14) {
            super.onInitializeAccessibilityEvent(accessibilityEvent);
            accessibilityEvent.setClassName(LinearLayoutCompat.class.getName());
        }
    }

    public void onInitializeAccessibilityNodeInfo(AccessibilityNodeInfo accessibilityNodeInfo) {
        if (VERSION.SDK_INT >= 14) {
            super.onInitializeAccessibilityNodeInfo(accessibilityNodeInfo);
            accessibilityNodeInfo.setClassName(LinearLayoutCompat.class.getName());
        }
    }

    /* access modifiers changed from: protected */
    public void onLayout(boolean z, int i, int i2, int i3, int i4) {
        if (this.mOrientation == 1) {
            layoutVertical(i, i2, i3, i4);
        } else {
            layoutHorizontal(i, i2, i3, i4);
        }
    }

    /* access modifiers changed from: protected */
    public void onMeasure(int i, int i2) {
        if (this.mOrientation == 1) {
            measureVertical(i, i2);
        } else {
            measureHorizontal(i, i2);
        }
    }

    public void setBaselineAligned(boolean z) {
        this.mBaselineAligned = z;
    }

    public void setBaselineAlignedChildIndex(int i) {
        if (i < 0 || i >= getChildCount()) {
            throw new IllegalArgumentException("base aligned child index out of range (0, " + getChildCount() + ")");
        }
        this.mBaselineAlignedChildIndex = i;
    }

    public void setDividerDrawable(Drawable drawable) {
        boolean z = false;
        if (drawable != this.mDivider) {
            this.mDivider = drawable;
            if (drawable != null) {
                this.mDividerWidth = drawable.getIntrinsicWidth();
                this.mDividerHeight = drawable.getIntrinsicHeight();
            } else {
                this.mDividerWidth = 0;
                this.mDividerHeight = 0;
            }
            if (drawable == null) {
                z = true;
            }
            setWillNotDraw(z);
            requestLayout();
        }
    }

    public void setDividerPadding(int i) {
        this.mDividerPadding = i;
    }

    public void setGravity(int i) {
        if (this.mGravity != i) {
            int i2 = (8388615 & i) == 0 ? 8388611 | i : i;
            if ((i2 & 112) == 0) {
                i2 |= 48;
            }
            this.mGravity = i2;
            requestLayout();
        }
    }

    public void setHorizontalGravity(int i) {
        int i2 = i & GravityCompat.RELATIVE_HORIZONTAL_GRAVITY_MASK;
        if ((this.mGravity & GravityCompat.RELATIVE_HORIZONTAL_GRAVITY_MASK) != i2) {
            this.mGravity = i2 | (this.mGravity & -8388616);
            requestLayout();
        }
    }

    public void setMeasureWithLargestChildEnabled(boolean z) {
        this.mUseLargestChild = z;
    }

    public void setOrientation(int i) {
        if (this.mOrientation != i) {
            this.mOrientation = i;
            requestLayout();
        }
    }

    public void setShowDividers(int i) {
        if (i != this.mShowDividers) {
            requestLayout();
        }
        this.mShowDividers = i;
    }

    public void setVerticalGravity(int i) {
        int i2 = i & 112;
        if ((this.mGravity & 112) != i2) {
            this.mGravity = i2 | (this.mGravity & -113);
            requestLayout();
        }
    }

    public void setWeightSum(float f) {
        this.mWeightSum = Math.max(0.0f, f);
    }

    public boolean shouldDelayChildPressedState() {
        return false;
    }
}
