package net.gogame.gowrap.p019ui.layout;

import android.content.Context;
import android.content.res.TypedArray;
import android.graphics.Rect;
import android.util.AttributeSet;
import android.view.Gravity;
import android.view.View;
import android.view.View.MeasureSpec;
import android.view.ViewGroup;
import android.view.ViewGroup.MarginLayoutParams;
import net.gogame.gowrap.p019ui.common.C1680R;

/* renamed from: net.gogame.gowrap.ui.layout.CustomGridLayout */
public class CustomGridLayout extends ViewGroup {
    private static final int DEFAULT_COLUMN_COUNT = 2;
    private static final int DEFAULT_HORIZONTAL_SPACING = 0;
    private static final int DEFAULT_VERTICAL_SPACING = 0;
    private int columnCount;
    private int horizontalSpacing;
    private final Rect mTmpChildRect;
    private final Rect mTmpContainerRect;
    private int verticalSpacing;

    /* renamed from: net.gogame.gowrap.ui.layout.CustomGridLayout$LayoutParams */
    public static class LayoutParams extends android.widget.FrameLayout.LayoutParams {
        public LayoutParams(Context context, AttributeSet attributeSet) {
            super(context, attributeSet);
            context.obtainStyledAttributes(attributeSet, C1680R.styleable.CustomGridLayout_Layout).recycle();
        }

        public LayoutParams(int i, int i2) {
            super(i, i2);
        }

        public LayoutParams(android.view.ViewGroup.LayoutParams layoutParams) {
            super(layoutParams);
        }
    }

    public CustomGridLayout(Context context) {
        super(context);
        this.mTmpContainerRect = new Rect();
        this.mTmpChildRect = new Rect();
        this.columnCount = 2;
        this.horizontalSpacing = 0;
        this.verticalSpacing = 0;
    }

    public CustomGridLayout(Context context, AttributeSet attributeSet) {
        this(context, attributeSet, 0);
    }

    public CustomGridLayout(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
        this.mTmpContainerRect = new Rect();
        this.mTmpChildRect = new Rect();
        this.columnCount = 2;
        this.horizontalSpacing = 0;
        this.verticalSpacing = 0;
        TypedArray obtainStyledAttributes = context.getTheme().obtainStyledAttributes(attributeSet, C1680R.styleable.CustomGridLayout, 0, 0);
        try {
            this.columnCount = obtainStyledAttributes.getInteger(C1680R.styleable.CustomGridLayout_column_count, 2);
            this.horizontalSpacing = obtainStyledAttributes.getDimensionPixelSize(C1680R.styleable.CustomGridLayout_horizontal_spacing, 0);
            this.verticalSpacing = obtainStyledAttributes.getDimensionPixelSize(C1680R.styleable.CustomGridLayout_vertical_spacing, 0);
        } finally {
            obtainStyledAttributes.recycle();
        }
    }

    public int getColumnCount() {
        return this.columnCount;
    }

    public void setColumnCount(int i) {
        this.columnCount = i;
    }

    public boolean shouldDelayChildPressedState() {
        return false;
    }

    private String toMeasureSpecString(int i) {
        return String.format("%s %s", new Object[]{getMode(MeasureSpec.getMode(i)), Integer.valueOf(MeasureSpec.getSize(i))});
    }

    private String getMode(int i) {
        switch (i) {
            case Integer.MIN_VALUE:
                return "AT_MOST";
            case 0:
                return "UNSPECIFIED";
            case 1073741824:
                return "EXACTLY";
            default:
                return String.valueOf(i);
        }
    }

    /* access modifiers changed from: protected */
    public void onMeasure(int i, int i2) {
        int makeMeasureSpec;
        int makeMeasureSpec2;
        int i3;
        int i4;
        int i5;
        int i6;
        int mode = MeasureSpec.getMode(i);
        int size = MeasureSpec.getSize(i);
        int mode2 = MeasureSpec.getMode(i2);
        int size2 = MeasureSpec.getSize(i2);
        int i7 = 0;
        int childCount = getChildCount();
        for (int i8 = 0; i8 < childCount; i8++) {
            if (getChildAt(i8).getVisibility() != 8) {
                i7++;
            }
        }
        int i9 = (i7 + (this.columnCount - 1)) / this.columnCount;
        switch (mode) {
            case Integer.MIN_VALUE:
            case 1073741824:
                makeMeasureSpec = MeasureSpec.makeMeasureSpec((((size - ((this.columnCount - 1) * this.horizontalSpacing)) - getPaddingLeft()) - getPaddingRight()) / this.columnCount, mode);
                break;
            default:
                makeMeasureSpec = MeasureSpec.makeMeasureSpec(0, 0);
                break;
        }
        switch (mode2) {
            case Integer.MIN_VALUE:
            case 1073741824:
                makeMeasureSpec2 = MeasureSpec.makeMeasureSpec((((size2 - ((i9 - 1) * this.verticalSpacing)) - getPaddingTop()) - getPaddingBottom()) / i9, mode2);
                break;
            default:
                makeMeasureSpec2 = MeasureSpec.makeMeasureSpec(0, 0);
                break;
        }
        int i10 = 0;
        int i11 = 0;
        int i12 = 0;
        int i13 = 0;
        int paddingTop = getPaddingTop();
        int i14 = 0;
        int i15 = 0;
        while (i15 < childCount) {
            View childAt = getChildAt(i15);
            if (childAt.getVisibility() != 8) {
                measureChildWithMargins(childAt, makeMeasureSpec, 0, makeMeasureSpec2, 0);
                MarginLayoutParams marginLayoutParams = (MarginLayoutParams) childAt.getLayoutParams();
                i10 = Math.max(i10, childAt.getMeasuredWidth() + marginLayoutParams.leftMargin + marginLayoutParams.rightMargin);
                i11 = Math.max(i11, childAt.getMeasuredHeight() + marginLayoutParams.topMargin + marginLayoutParams.bottomMargin);
                i6 = combineMeasuredStates(i12, childAt.getMeasuredState());
                i3 = Math.max(i14, marginLayoutParams.bottomMargin + childAt.getMeasuredHeight() + marginLayoutParams.topMargin);
                i5 = i13 + 1;
                if (i5 >= this.columnCount) {
                    i5 = 0;
                    i4 = paddingTop + i3 + this.verticalSpacing;
                    i3 = 0;
                } else {
                    i4 = paddingTop;
                }
            } else {
                i3 = i14;
                i4 = paddingTop;
                i5 = i13;
                i6 = i12;
            }
            i15++;
            i14 = i3;
            paddingTop = i4;
            i13 = i5;
            i12 = i6;
        }
        int paddingBottom = getPaddingBottom() + i14 + paddingTop;
        int max = Math.max((this.columnCount * i10) + (this.horizontalSpacing * (this.columnCount - 1)) + getPaddingLeft() + getPaddingRight(), getSuggestedMinimumWidth());
        int max2 = Math.max(paddingBottom, getSuggestedMinimumHeight());
        setMeasuredDimension(resolveSizeAndState(max, i, i12), resolveSizeAndState(max2, i2, i12 << 16));
    }

    private int getColumnLeft(int i) {
        int measuredWidth = (((getMeasuredWidth() - getPaddingLeft()) - getPaddingRight()) - ((this.columnCount - 1) * this.horizontalSpacing)) / this.columnCount;
        return ((measuredWidth + this.horizontalSpacing) * i) + getPaddingLeft();
    }

    private int resolve(int i, int i2, int i3) {
        if (i == -1) {
            return i3;
        }
        if (i == -2) {
            return i2;
        }
        return i;
    }

    /* access modifiers changed from: protected */
    public void onLayout(boolean z, int i, int i2, int i3, int i4) {
        int i5;
        int childCount = getChildCount();
        int i6 = 0;
        int paddingTop = getPaddingTop();
        int i7 = 0;
        int i8 = 0;
        while (i8 < childCount) {
            View childAt = getChildAt(i8);
            if (childAt.getVisibility() != 8) {
                LayoutParams layoutParams = (LayoutParams) childAt.getLayoutParams();
                this.mTmpContainerRect.left = getColumnLeft(i6) + layoutParams.leftMargin;
                this.mTmpContainerRect.right = (getColumnLeft(i6 + 1) - layoutParams.rightMargin) - this.horizontalSpacing;
                this.mTmpContainerRect.top = layoutParams.topMargin + paddingTop;
                this.mTmpContainerRect.bottom = this.mTmpContainerRect.top + childAt.getMeasuredHeight();
                int resolve = resolve(layoutParams.width, childAt.getMeasuredWidth(), this.mTmpContainerRect.width());
                int resolve2 = resolve(layoutParams.height, childAt.getMeasuredHeight(), this.mTmpContainerRect.height());
                i7 = Math.max(i7, layoutParams.topMargin + resolve2 + layoutParams.bottomMargin);
                Gravity.apply(layoutParams.gravity, resolve, resolve2, this.mTmpContainerRect, this.mTmpChildRect);
                childAt.measure(MeasureSpec.makeMeasureSpec(resolve, Integer.MIN_VALUE), MeasureSpec.makeMeasureSpec(resolve2, Integer.MIN_VALUE));
                childAt.layout(this.mTmpChildRect.left, this.mTmpChildRect.top, this.mTmpChildRect.right, this.mTmpChildRect.bottom);
                i6++;
                if (i6 >= this.columnCount) {
                    i6 = 0;
                    i5 = 0;
                    paddingTop += this.verticalSpacing + i7;
                    i8++;
                    i7 = i5;
                }
            }
            i5 = i7;
            i8++;
            i7 = i5;
        }
    }

    public LayoutParams generateLayoutParams(AttributeSet attributeSet) {
        return new LayoutParams(getContext(), attributeSet);
    }

    /* access modifiers changed from: protected */
    public LayoutParams generateDefaultLayoutParams() {
        return new LayoutParams(-1, -2);
    }

    /* access modifiers changed from: protected */
    public android.view.ViewGroup.LayoutParams generateLayoutParams(android.view.ViewGroup.LayoutParams layoutParams) {
        return new LayoutParams(layoutParams);
    }

    /* access modifiers changed from: protected */
    public boolean checkLayoutParams(android.view.ViewGroup.LayoutParams layoutParams) {
        return layoutParams instanceof LayoutParams;
    }
}
