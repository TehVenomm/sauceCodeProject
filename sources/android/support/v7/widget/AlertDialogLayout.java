package android.support.p003v7.widget;

import android.content.Context;
import android.graphics.drawable.Drawable;
import android.support.annotation.Nullable;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import android.support.p000v4.view.GravityCompat;
import android.support.p000v4.view.ViewCompat;
import android.support.p003v7.appcompat.C0260R;
import android.support.p003v7.widget.LinearLayoutCompat.LayoutParams;
import android.util.AttributeSet;
import android.view.View;
import android.view.View.MeasureSpec;
import android.view.ViewGroup;

@RestrictTo({Scope.LIBRARY_GROUP})
/* renamed from: android.support.v7.widget.AlertDialogLayout */
public class AlertDialogLayout extends LinearLayoutCompat {
    public AlertDialogLayout(@Nullable Context context) {
        super(context);
    }

    public AlertDialogLayout(@Nullable Context context, @Nullable AttributeSet attributeSet) {
        super(context, attributeSet);
    }

    private void forceUniformWidth(int i, int i2) {
        int makeMeasureSpec = MeasureSpec.makeMeasureSpec(getMeasuredWidth(), 1073741824);
        for (int i3 = 0; i3 < i; i3++) {
            View childAt = getChildAt(i3);
            if (childAt.getVisibility() != 8) {
                LayoutParams layoutParams = (LayoutParams) childAt.getLayoutParams();
                if (layoutParams.width == -1) {
                    int i4 = layoutParams.height;
                    layoutParams.height = childAt.getMeasuredHeight();
                    measureChildWithMargins(childAt, makeMeasureSpec, 0, i2, 0);
                    layoutParams.height = i4;
                }
            }
        }
    }

    private static int resolveMinimumHeight(View view) {
        int minimumHeight = ViewCompat.getMinimumHeight(view);
        if (minimumHeight > 0) {
            return minimumHeight;
        }
        if (!(view instanceof ViewGroup)) {
            return 0;
        }
        ViewGroup viewGroup = (ViewGroup) view;
        if (viewGroup.getChildCount() == 1) {
            return resolveMinimumHeight(viewGroup.getChildAt(0));
        }
        return 0;
    }

    private void setChildFrame(View view, int i, int i2, int i3, int i4) {
        view.layout(i, i2, i + i3, i2 + i4);
    }

    private boolean tryOnMeasure(int i, int i2) {
        int i3;
        int i4;
        int i5;
        int i6;
        int i7;
        View view = null;
        View view2 = null;
        View view3 = null;
        int childCount = getChildCount();
        for (int i8 = 0; i8 < childCount; i8++) {
            View childAt = getChildAt(i8);
            if (childAt.getVisibility() != 8) {
                int id = childAt.getId();
                if (id == C0260R.C0263id.topPanel) {
                    view = childAt;
                } else if (id == C0260R.C0263id.buttonPanel) {
                    view2 = childAt;
                } else if (id != C0260R.C0263id.contentPanel && id != C0260R.C0263id.customPanel) {
                    return false;
                } else {
                    if (view3 != null) {
                        return false;
                    }
                    view3 = childAt;
                }
            }
        }
        int mode = MeasureSpec.getMode(i2);
        int size = MeasureSpec.getSize(i2);
        int mode2 = MeasureSpec.getMode(i);
        int i9 = 0;
        int paddingBottom = getPaddingBottom() + getPaddingTop();
        if (view != null) {
            view.measure(i, 0);
            paddingBottom += view.getMeasuredHeight();
            i9 = View.combineMeasuredStates(0, view.getMeasuredState());
        }
        int i10 = 0;
        if (view2 != null) {
            view2.measure(i, 0);
            i10 = resolveMinimumHeight(view2);
            int measuredHeight = view2.getMeasuredHeight() - i10;
            paddingBottom += i10;
            i9 = View.combineMeasuredStates(i9, view2.getMeasuredState());
            i3 = measuredHeight;
        } else {
            i3 = 0;
        }
        if (view3 != null) {
            view3.measure(i, mode == 0 ? 0 : MeasureSpec.makeMeasureSpec(Math.max(0, size - paddingBottom), mode));
            int measuredHeight2 = view3.getMeasuredHeight();
            paddingBottom += measuredHeight2;
            i9 = View.combineMeasuredStates(i9, view3.getMeasuredState());
            i4 = measuredHeight2;
        } else {
            i4 = 0;
        }
        int i11 = size - paddingBottom;
        if (view2 != null) {
            int min = Math.min(i11, i3);
            if (min > 0) {
                i11 -= min;
                i7 = min + i10;
            } else {
                i7 = i10;
            }
            view2.measure(i, MeasureSpec.makeMeasureSpec(i7, 1073741824));
            i5 = (paddingBottom - i10) + view2.getMeasuredHeight();
            i6 = View.combineMeasuredStates(i9, view2.getMeasuredState());
        } else {
            i5 = paddingBottom;
            i6 = i9;
        }
        if (view3 != null && i11 > 0) {
            view3.measure(i, MeasureSpec.makeMeasureSpec(i4 + i11, mode));
            i5 = (i5 - i4) + view3.getMeasuredHeight();
            i6 = View.combineMeasuredStates(i6, view3.getMeasuredState());
        }
        int i12 = 0;
        for (int i13 = 0; i13 < childCount; i13++) {
            View childAt2 = getChildAt(i13);
            if (childAt2.getVisibility() != 8) {
                i12 = Math.max(i12, childAt2.getMeasuredWidth());
            }
        }
        setMeasuredDimension(View.resolveSizeAndState(i12 + getPaddingLeft() + getPaddingRight(), i, i6), View.resolveSizeAndState(i5, i2, 0));
        if (mode2 != 1073741824) {
            forceUniformWidth(childCount, i2);
        }
        return true;
    }

    /* access modifiers changed from: protected */
    public void onLayout(boolean z, int i, int i2, int i3, int i4) {
        int paddingTop;
        int i5;
        int paddingLeft = getPaddingLeft();
        int i6 = i3 - i;
        int paddingRight = getPaddingRight();
        int paddingRight2 = getPaddingRight();
        int measuredHeight = getMeasuredHeight();
        int childCount = getChildCount();
        int gravity = getGravity();
        switch (gravity & 112) {
            case 16:
                paddingTop = (((i4 - i2) - measuredHeight) / 2) + getPaddingTop();
                break;
            case 80:
                paddingTop = ((getPaddingTop() + i4) - i2) - measuredHeight;
                break;
            default:
                paddingTop = getPaddingTop();
                break;
        }
        Drawable dividerDrawable = getDividerDrawable();
        int intrinsicHeight = dividerDrawable == null ? 0 : dividerDrawable.getIntrinsicHeight();
        int i7 = paddingTop;
        for (int i8 = 0; i8 < childCount; i8++) {
            View childAt = getChildAt(i8);
            if (!(childAt == null || childAt.getVisibility() == 8)) {
                int measuredWidth = childAt.getMeasuredWidth();
                int measuredHeight2 = childAt.getMeasuredHeight();
                LayoutParams layoutParams = (LayoutParams) childAt.getLayoutParams();
                int i9 = layoutParams.gravity;
                if (i9 < 0) {
                    i9 = 8388615 & gravity;
                }
                switch (GravityCompat.getAbsoluteGravity(i9, ViewCompat.getLayoutDirection(this)) & 7) {
                    case 1:
                        i5 = ((((((i6 - paddingLeft) - paddingRight2) - measuredWidth) / 2) + paddingLeft) + layoutParams.leftMargin) - layoutParams.rightMargin;
                        break;
                    case 5:
                        i5 = ((i6 - paddingRight) - measuredWidth) - layoutParams.rightMargin;
                        break;
                    default:
                        i5 = paddingLeft + layoutParams.leftMargin;
                        break;
                }
                int i10 = layoutParams.topMargin + (hasDividerBeforeChildAt(i8) ? i7 + intrinsicHeight : i7);
                setChildFrame(childAt, i5, i10, measuredWidth, measuredHeight2);
                i7 = i10 + layoutParams.bottomMargin + measuredHeight2;
            }
        }
    }

    /* access modifiers changed from: protected */
    public void onMeasure(int i, int i2) {
        if (!tryOnMeasure(i, i2)) {
            super.onMeasure(i, i2);
        }
    }
}
