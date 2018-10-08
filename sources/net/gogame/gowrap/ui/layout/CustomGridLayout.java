package net.gogame.gowrap.ui.layout;

import android.content.Context;
import android.content.res.TypedArray;
import android.graphics.Rect;
import android.util.AttributeSet;
import android.view.Gravity;
import android.view.View;
import android.view.View.MeasureSpec;
import android.view.ViewGroup;
import android.view.ViewGroup.MarginLayoutParams;
import net.gogame.gowrap.ui.common.C1135R;

public class CustomGridLayout extends ViewGroup {
    private static final int DEFAULT_COLUMN_COUNT = 2;
    private static final int DEFAULT_HORIZONTAL_SPACING = 0;
    private static final int DEFAULT_VERTICAL_SPACING = 0;
    private int columnCount;
    private int horizontalSpacing;
    private final Rect mTmpChildRect;
    private final Rect mTmpContainerRect;
    private int verticalSpacing;

    public static class LayoutParams extends android.widget.FrameLayout.LayoutParams {
        public LayoutParams(Context context, AttributeSet attributeSet) {
            super(context, attributeSet);
            context.obtainStyledAttributes(attributeSet, C1135R.styleable.CustomGridLayout_Layout).recycle();
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
        TypedArray obtainStyledAttributes = context.getTheme().obtainStyledAttributes(attributeSet, C1135R.styleable.CustomGridLayout, 0, 0);
        try {
            this.columnCount = obtainStyledAttributes.getInteger(C1135R.styleable.CustomGridLayout_column_count, 2);
            this.horizontalSpacing = obtainStyledAttributes.getDimensionPixelSize(C1135R.styleable.CustomGridLayout_horizontal_spacing, 0);
            this.verticalSpacing = obtainStyledAttributes.getDimensionPixelSize(C1135R.styleable.CustomGridLayout_vertical_spacing, 0);
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

    protected void onMeasure(int i, int i2) {
        int i3;
        int mode = MeasureSpec.getMode(i);
        int size = MeasureSpec.getSize(i);
        int mode2 = MeasureSpec.getMode(i2);
        int size2 = MeasureSpec.getSize(i2);
        int childCount = getChildCount();
        int i4 = 0;
        for (i3 = 0; i3 < childCount; i3++) {
            if (getChildAt(i3).getVisibility() != 8) {
                i4++;
            }
        }
        i4 = (i4 + (this.columnCount - 1)) / this.columnCount;
        switch (mode) {
            case Integer.MIN_VALUE:
            case 1073741824:
                mode = MeasureSpec.makeMeasureSpec((((size - ((this.columnCount - 1) * this.horizontalSpacing)) - getPaddingLeft()) - getPaddingRight()) / this.columnCount, mode);
                break;
            default:
                mode = MeasureSpec.makeMeasureSpec(0, 0);
                break;
        }
        switch (mode2) {
            case Integer.MIN_VALUE:
            case 1073741824:
                mode2 = MeasureSpec.makeMeasureSpec((((size2 - ((i4 - 1) * this.verticalSpacing)) - getPaddingTop()) - getPaddingBottom()) / i4, mode2);
                break;
            default:
                mode2 = MeasureSpec.makeMeasureSpec(0, 0);
                break;
        }
        int i5 = 0;
        int i6 = 0;
        int i7 = 0;
        int i8 = 0;
        int paddingTop = getPaddingTop();
        int i9 = 0;
        int i10 = 0;
        while (i10 < childCount) {
            View childAt = getChildAt(i10);
            if (childAt.getVisibility() != 8) {
                measureChildWithMargins(childAt, mode, 0, mode2, 0);
                MarginLayoutParams marginLayoutParams = (MarginLayoutParams) childAt.getLayoutParams();
                i5 = Math.max(i5, (childAt.getMeasuredWidth() + marginLayoutParams.leftMargin) + marginLayoutParams.rightMargin);
                i6 = Math.max(i6, (childAt.getMeasuredHeight() + marginLayoutParams.topMargin) + marginLayoutParams.bottomMargin);
                size2 = combineMeasuredStates(i7, childAt.getMeasuredState());
                i4 = Math.max(i9, marginLayoutParams.bottomMargin + (childAt.getMeasuredHeight() + marginLayoutParams.topMargin));
                i3 = i8 + 1;
                if (i3 >= this.columnCount) {
                    size = 0;
                    i3 = paddingTop + (i4 + this.verticalSpacing);
                    i4 = 0;
                    i9 = i6;
                    paddingTop = i5;
                } else {
                    size = i3;
                    i9 = i6;
                    i3 = paddingTop;
                    paddingTop = i5;
                }
            } else {
                i4 = i9;
                i3 = paddingTop;
                size = i8;
                size2 = i7;
                i9 = i6;
                paddingTop = i5;
            }
            i10++;
            i7 = size2;
            i6 = i9;
            i5 = paddingTop;
            i9 = i4;
            paddingTop = i3;
            i8 = size;
        }
        i4 = (getPaddingBottom() + i9) + paddingTop;
        i3 = Math.max((((this.columnCount * i5) + (this.horizontalSpacing * (this.columnCount - 1))) + getPaddingLeft()) + getPaddingRight(), getSuggestedMinimumWidth());
        i4 = Math.max(i4, getSuggestedMinimumHeight());
        setMeasuredDimension(resolveSizeAndState(i3, i, i7), resolveSizeAndState(i4, i2, i7 << 16));
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

    protected void onLayout(boolean z, int i, int i2, int i3, int i4) {
        int childCount = getChildCount();
        int i5 = 0;
        int paddingTop = getPaddingTop();
        int i6 = 0;
        int i7 = 0;
        while (i7 < childCount) {
            int i8;
            View childAt = getChildAt(i7);
            if (childAt.getVisibility() != 8) {
                LayoutParams layoutParams = (LayoutParams) childAt.getLayoutParams();
                this.mTmpContainerRect.left = getColumnLeft(i5) + layoutParams.leftMargin;
                this.mTmpContainerRect.right = (getColumnLeft(i5 + 1) - layoutParams.rightMargin) - this.horizontalSpacing;
                this.mTmpContainerRect.top = layoutParams.topMargin + paddingTop;
                this.mTmpContainerRect.bottom = this.mTmpContainerRect.top + childAt.getMeasuredHeight();
                int resolve = resolve(layoutParams.width, childAt.getMeasuredWidth(), this.mTmpContainerRect.width());
                int resolve2 = resolve(layoutParams.height, childAt.getMeasuredHeight(), this.mTmpContainerRect.height());
                i6 = Math.max(i6, (layoutParams.topMargin + resolve2) + layoutParams.bottomMargin);
                Gravity.apply(layoutParams.gravity, resolve, resolve2, this.mTmpContainerRect, this.mTmpChildRect);
                childAt.measure(MeasureSpec.makeMeasureSpec(resolve, Integer.MIN_VALUE), MeasureSpec.makeMeasureSpec(resolve2, Integer.MIN_VALUE));
                childAt.layout(this.mTmpChildRect.left, this.mTmpChildRect.top, this.mTmpChildRect.right, this.mTmpChildRect.bottom);
                i8 = i5 + 1;
                if (i8 >= this.columnCount) {
                    i6 = paddingTop + (this.verticalSpacing + i6);
                    i8 = 0;
                    paddingTop = 0;
                } else {
                    int i9 = i6;
                    i6 = paddingTop;
                    paddingTop = i8;
                    i8 = i9;
                }
            } else {
                i8 = i6;
                i6 = paddingTop;
                paddingTop = i5;
            }
            i7++;
            i5 = paddingTop;
            paddingTop = i6;
            i6 = i8;
        }
    }

    public LayoutParams generateLayoutParams(AttributeSet attributeSet) {
        return new LayoutParams(getContext(), attributeSet);
    }

    protected LayoutParams generateDefaultLayoutParams() {
        return new LayoutParams(-1, -2);
    }

    protected android.view.ViewGroup.LayoutParams generateLayoutParams(android.view.ViewGroup.LayoutParams layoutParams) {
        return new LayoutParams(layoutParams);
    }

    protected boolean checkLayoutParams(android.view.ViewGroup.LayoutParams layoutParams) {
        return layoutParams instanceof LayoutParams;
    }
}
