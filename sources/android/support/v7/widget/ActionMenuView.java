package android.support.p003v7.widget;

import android.content.Context;
import android.content.res.Configuration;
import android.graphics.drawable.Drawable;
import android.support.annotation.Nullable;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import android.support.annotation.StyleRes;
import android.support.p003v7.view.menu.ActionMenuItemView;
import android.support.p003v7.view.menu.MenuBuilder;
import android.support.p003v7.view.menu.MenuBuilder.ItemInvoker;
import android.support.p003v7.view.menu.MenuItemImpl;
import android.support.p003v7.view.menu.MenuPresenter.Callback;
import android.support.p003v7.view.menu.MenuView;
import android.util.AttributeSet;
import android.view.ContextThemeWrapper;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.MeasureSpec;
import android.view.ViewDebug.ExportedProperty;
import android.view.accessibility.AccessibilityEvent;

/* renamed from: android.support.v7.widget.ActionMenuView */
public class ActionMenuView extends LinearLayoutCompat implements ItemInvoker, MenuView {
    static final int GENERATED_ITEM_PADDING = 4;
    static final int MIN_CELL_SIZE = 56;
    private static final String TAG = "ActionMenuView";
    private Callback mActionMenuPresenterCallback;
    private boolean mFormatItems;
    private int mFormatItemsWidth;
    private int mGeneratedItemPadding;
    private MenuBuilder mMenu;
    MenuBuilder.Callback mMenuBuilderCallback;
    private int mMinCellSize;
    OnMenuItemClickListener mOnMenuItemClickListener;
    private Context mPopupContext;
    private int mPopupTheme;
    private ActionMenuPresenter mPresenter;
    private boolean mReserveOverflow;

    @RestrictTo({Scope.LIBRARY_GROUP})
    /* renamed from: android.support.v7.widget.ActionMenuView$ActionMenuChildView */
    public interface ActionMenuChildView {
        boolean needsDividerAfter();

        boolean needsDividerBefore();
    }

    /* renamed from: android.support.v7.widget.ActionMenuView$ActionMenuPresenterCallback */
    private static class ActionMenuPresenterCallback implements Callback {
        ActionMenuPresenterCallback() {
        }

        public void onCloseMenu(MenuBuilder menuBuilder, boolean z) {
        }

        public boolean onOpenSubMenu(MenuBuilder menuBuilder) {
            return false;
        }
    }

    /* renamed from: android.support.v7.widget.ActionMenuView$LayoutParams */
    public static class LayoutParams extends android.support.p003v7.widget.LinearLayoutCompat.LayoutParams {
        @ExportedProperty
        public int cellsUsed;
        @ExportedProperty
        public boolean expandable;
        boolean expanded;
        @ExportedProperty
        public int extraPixels;
        @ExportedProperty
        public boolean isOverflowButton;
        @ExportedProperty
        public boolean preventEdgeOffset;

        public LayoutParams(int i, int i2) {
            super(i, i2);
            this.isOverflowButton = false;
        }

        LayoutParams(int i, int i2, boolean z) {
            super(i, i2);
            this.isOverflowButton = z;
        }

        public LayoutParams(Context context, AttributeSet attributeSet) {
            super(context, attributeSet);
        }

        public LayoutParams(LayoutParams layoutParams) {
            super((android.view.ViewGroup.LayoutParams) layoutParams);
            this.isOverflowButton = layoutParams.isOverflowButton;
        }

        public LayoutParams(android.view.ViewGroup.LayoutParams layoutParams) {
            super(layoutParams);
        }
    }

    /* renamed from: android.support.v7.widget.ActionMenuView$MenuBuilderCallback */
    private class MenuBuilderCallback implements MenuBuilder.Callback {
        MenuBuilderCallback() {
        }

        public boolean onMenuItemSelected(MenuBuilder menuBuilder, MenuItem menuItem) {
            return ActionMenuView.this.mOnMenuItemClickListener != null && ActionMenuView.this.mOnMenuItemClickListener.onMenuItemClick(menuItem);
        }

        public void onMenuModeChange(MenuBuilder menuBuilder) {
            if (ActionMenuView.this.mMenuBuilderCallback != null) {
                ActionMenuView.this.mMenuBuilderCallback.onMenuModeChange(menuBuilder);
            }
        }
    }

    /* renamed from: android.support.v7.widget.ActionMenuView$OnMenuItemClickListener */
    public interface OnMenuItemClickListener {
        boolean onMenuItemClick(MenuItem menuItem);
    }

    public ActionMenuView(Context context) {
        this(context, null);
    }

    public ActionMenuView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        setBaselineAligned(false);
        float f = context.getResources().getDisplayMetrics().density;
        this.mMinCellSize = (int) (56.0f * f);
        this.mGeneratedItemPadding = (int) (f * 4.0f);
        this.mPopupContext = context;
        this.mPopupTheme = 0;
    }

    static int measureChildForCells(View view, int i, int i2, int i3, int i4) {
        int i5;
        boolean z = false;
        LayoutParams layoutParams = (LayoutParams) view.getLayoutParams();
        int makeMeasureSpec = MeasureSpec.makeMeasureSpec(MeasureSpec.getSize(i3) - i4, MeasureSpec.getMode(i3));
        ActionMenuItemView actionMenuItemView = view instanceof ActionMenuItemView ? (ActionMenuItemView) view : null;
        boolean z2 = actionMenuItemView != null && actionMenuItemView.hasText();
        if (i2 <= 0 || (z2 && i2 < 2)) {
            i5 = 0;
        } else {
            view.measure(MeasureSpec.makeMeasureSpec(i * i2, Integer.MIN_VALUE), makeMeasureSpec);
            int measuredWidth = view.getMeasuredWidth();
            i5 = measuredWidth / i;
            if (measuredWidth % i != 0) {
                i5++;
            }
            if (z2 && i5 < 2) {
                i5 = 2;
            }
        }
        if (!layoutParams.isOverflowButton && z2) {
            z = true;
        }
        layoutParams.expandable = z;
        layoutParams.cellsUsed = i5;
        view.measure(MeasureSpec.makeMeasureSpec(i5 * i, 1073741824), makeMeasureSpec);
        return i5;
    }

    /* JADX WARNING: Removed duplicated region for block: B:107:0x027b  */
    /* JADX WARNING: Removed duplicated region for block: B:81:0x01d0  */
    /* JADX WARNING: Removed duplicated region for block: B:85:0x01df  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private void onMeasureExactFormat(int r33, int r34) {
        /*
            r32 = this;
            int r21 = android.view.View.MeasureSpec.getMode(r34)
            int r6 = android.view.View.MeasureSpec.getSize(r33)
            int r19 = android.view.View.MeasureSpec.getSize(r34)
            int r7 = r32.getPaddingLeft()
            int r8 = r32.getPaddingRight()
            int r9 = r32.getPaddingTop()
            int r10 = r32.getPaddingBottom()
            int r18 = r9 + r10
            r9 = -2
            r0 = r34
            r1 = r18
            int r24 = getChildMeasureSpec(r0, r1, r9)
            int r7 = r7 + r8
            int r25 = r6 - r7
            r0 = r32
            int r6 = r0.mMinCellSize
            int r16 = r25 / r6
            r0 = r32
            int r6 = r0.mMinCellSize
            if (r16 != 0) goto L_0x003f
            r6 = 0
            r0 = r32
            r1 = r25
            r0.setMeasuredDimension(r1, r6)
        L_0x003e:
            return
        L_0x003f:
            r0 = r32
            int r7 = r0.mMinCellSize
            int r6 = r25 % r6
            int r6 = r6 / r16
            int r26 = r7 + r6
            r15 = 0
            r13 = 0
            r14 = 0
            r12 = 0
            r9 = 0
            r10 = 0
            int r27 = r32.getChildCount()
            r6 = 0
            r17 = r6
        L_0x0057:
            r0 = r17
            r1 = r27
            if (r0 >= r1) goto L_0x0103
            r0 = r32
            r1 = r17
            android.view.View r8 = r0.getChildAt(r1)
            int r6 = r8.getVisibility()
            r7 = 8
            if (r6 != r7) goto L_0x0074
            r6 = r10
            r8 = r9
        L_0x006f:
            int r17 = r17 + 1
            r10 = r6
            r9 = r8
            goto L_0x0057
        L_0x0074:
            boolean r7 = r8 instanceof android.support.p003v7.view.menu.ActionMenuItemView
            int r12 = r12 + 1
            if (r7 == 0) goto L_0x0091
            r0 = r32
            int r6 = r0.mGeneratedItemPadding
            r20 = 0
            r0 = r32
            int r0 = r0.mGeneratedItemPadding
            r22 = r0
            r23 = 0
            r0 = r20
            r1 = r22
            r2 = r23
            r8.setPadding(r6, r0, r1, r2)
        L_0x0091:
            android.view.ViewGroup$LayoutParams r6 = r8.getLayoutParams()
            android.support.v7.widget.ActionMenuView$LayoutParams r6 = (android.support.p003v7.widget.ActionMenuView.LayoutParams) r6
            r20 = 0
            r0 = r20
            r6.expanded = r0
            r20 = 0
            r0 = r20
            r6.extraPixels = r0
            r20 = 0
            r0 = r20
            r6.cellsUsed = r0
            r20 = 0
            r0 = r20
            r6.expandable = r0
            r20 = 0
            r0 = r20
            r6.leftMargin = r0
            r20 = 0
            r0 = r20
            r6.rightMargin = r0
            if (r7 == 0) goto L_0x00fe
            r7 = r8
            android.support.v7.view.menu.ActionMenuItemView r7 = (android.support.p003v7.view.menu.ActionMenuItemView) r7
            boolean r7 = r7.hasText()
            if (r7 == 0) goto L_0x00fe
            r7 = 1
        L_0x00c7:
            r6.preventEdgeOffset = r7
            boolean r7 = r6.isOverflowButton
            if (r7 == 0) goto L_0x0100
            r7 = 1
        L_0x00ce:
            r0 = r26
            r1 = r24
            r2 = r18
            int r7 = measureChildForCells(r8, r0, r7, r1, r2)
            int r13 = java.lang.Math.max(r13, r7)
            boolean r0 = r6.expandable
            r20 = r0
            if (r20 == 0) goto L_0x00e4
            int r14 = r14 + 1
        L_0x00e4:
            boolean r6 = r6.isOverflowButton
            if (r6 == 0) goto L_0x00e9
            r9 = 1
        L_0x00e9:
            int r16 = r16 - r7
            int r6 = r8.getMeasuredHeight()
            int r15 = java.lang.Math.max(r15, r6)
            r6 = 1
            if (r7 != r6) goto L_0x0314
            r6 = 1
            int r6 = r6 << r17
            long r6 = (long) r6
            long r6 = r6 | r10
            r8 = r9
            goto L_0x006f
        L_0x00fe:
            r7 = 0
            goto L_0x00c7
        L_0x0100:
            r7 = r16
            goto L_0x00ce
        L_0x0103:
            if (r9 == 0) goto L_0x013d
            r6 = 2
            if (r12 != r6) goto L_0x013d
            r6 = 1
            r7 = r6
        L_0x010a:
            r20 = 0
            r22 = r10
            r18 = r16
        L_0x0110:
            if (r14 <= 0) goto L_0x016a
            if (r18 <= 0) goto L_0x016a
            r8 = 2147483647(0x7fffffff, float:NaN)
            r10 = 0
            r16 = 0
            r6 = 0
            r17 = r6
        L_0x011e:
            r0 = r17
            r1 = r27
            if (r0 >= r1) goto L_0x0162
            r0 = r32
            r1 = r17
            android.view.View r6 = r0.getChildAt(r1)
            android.view.ViewGroup$LayoutParams r6 = r6.getLayoutParams()
            android.support.v7.widget.ActionMenuView$LayoutParams r6 = (android.support.p003v7.widget.ActionMenuView.LayoutParams) r6
            boolean r0 = r6.expandable
            r28 = r0
            if (r28 != 0) goto L_0x0140
            r6 = r8
        L_0x0139:
            int r17 = r17 + 1
            r8 = r6
            goto L_0x011e
        L_0x013d:
            r6 = 0
            r7 = r6
            goto L_0x010a
        L_0x0140:
            int r0 = r6.cellsUsed
            r28 = r0
            r0 = r28
            if (r0 >= r8) goto L_0x0152
            int r6 = r6.cellsUsed
            r8 = 1
            int r8 = r8 << r17
            long r10 = (long) r8
            r8 = 1
            r16 = r8
            goto L_0x0139
        L_0x0152:
            int r6 = r6.cellsUsed
            if (r6 != r8) goto L_0x0311
            r6 = 1
            int r6 = r6 << r17
            long r0 = (long) r6
            r28 = r0
            long r10 = r10 | r28
            int r16 = r16 + 1
            r6 = r8
            goto L_0x0139
        L_0x0162:
            long r22 = r22 | r10
            r0 = r16
            r1 = r18
            if (r0 <= r1) goto L_0x01ef
        L_0x016a:
            if (r9 != 0) goto L_0x0278
            r6 = 1
            if (r12 != r6) goto L_0x0278
            r6 = 1
        L_0x0170:
            if (r18 <= 0) goto L_0x02c4
            r8 = 0
            int r7 = (r22 > r8 ? 1 : (r22 == r8 ? 0 : -1))
            if (r7 == 0) goto L_0x02c4
            int r7 = r12 + -1
            r0 = r18
            if (r0 < r7) goto L_0x0183
            if (r6 != 0) goto L_0x0183
            r7 = 1
            if (r13 <= r7) goto L_0x02c4
        L_0x0183:
            int r7 = java.lang.Long.bitCount(r22)
            float r7 = (float) r7
            if (r6 != 0) goto L_0x030e
            r8 = 1
            long r8 = r8 & r22
            r10 = 0
            int r6 = (r8 > r10 ? 1 : (r8 == r10 ? 0 : -1))
            if (r6 == 0) goto L_0x01a8
            r6 = 0
            r0 = r32
            android.view.View r6 = r0.getChildAt(r6)
            android.view.ViewGroup$LayoutParams r6 = r6.getLayoutParams()
            android.support.v7.widget.ActionMenuView$LayoutParams r6 = (android.support.p003v7.widget.ActionMenuView.LayoutParams) r6
            boolean r6 = r6.preventEdgeOffset
            if (r6 != 0) goto L_0x01a8
            r6 = 1056964608(0x3f000000, float:0.5)
            float r7 = r7 - r6
        L_0x01a8:
            r6 = 1
            int r8 = r27 + -1
            int r6 = r6 << r8
            long r8 = (long) r6
            long r8 = r8 & r22
            r10 = 0
            int r6 = (r8 > r10 ? 1 : (r8 == r10 ? 0 : -1))
            if (r6 == 0) goto L_0x030e
            int r6 = r27 + -1
            r0 = r32
            android.view.View r6 = r0.getChildAt(r6)
            android.view.ViewGroup$LayoutParams r6 = r6.getLayoutParams()
            android.support.v7.widget.ActionMenuView$LayoutParams r6 = (android.support.p003v7.widget.ActionMenuView.LayoutParams) r6
            boolean r6 = r6.preventEdgeOffset
            if (r6 != 0) goto L_0x030e
            r6 = 1056964608(0x3f000000, float:0.5)
            float r6 = r7 - r6
        L_0x01cb:
            r7 = 0
            int r7 = (r6 > r7 ? 1 : (r6 == r7 ? 0 : -1))
            if (r7 <= 0) goto L_0x027b
            int r7 = r18 * r26
            float r7 = (float) r7
            float r6 = r7 / r6
            int r6 = (int) r6
            r7 = r6
        L_0x01d7:
            r6 = 0
            r9 = r6
            r8 = r20
        L_0x01db:
            r0 = r27
            if (r9 >= r0) goto L_0x02c6
            r6 = 1
            int r6 = r6 << r9
            long r10 = (long) r6
            long r10 = r10 & r22
            r12 = 0
            int r6 = (r10 > r12 ? 1 : (r10 == r12 ? 0 : -1))
            if (r6 != 0) goto L_0x027f
            r6 = r8
        L_0x01eb:
            int r9 = r9 + 1
            r8 = r6
            goto L_0x01db
        L_0x01ef:
            r6 = 0
            r16 = r22
            r20 = r6
        L_0x01f4:
            r0 = r20
            r1 = r27
            if (r0 >= r1) goto L_0x0272
            r0 = r32
            r1 = r20
            android.view.View r22 = r0.getChildAt(r1)
            android.view.ViewGroup$LayoutParams r6 = r22.getLayoutParams()
            android.support.v7.widget.ActionMenuView$LayoutParams r6 = (android.support.p003v7.widget.ActionMenuView.LayoutParams) r6
            r23 = 1
            int r23 = r23 << r20
            r0 = r23
            long r0 = (long) r0
            r28 = r0
            long r28 = r28 & r10
            r30 = 0
            int r23 = (r28 > r30 ? 1 : (r28 == r30 ? 0 : -1))
            if (r23 != 0) goto L_0x0230
            int r6 = r6.cellsUsed
            int r22 = r8 + 1
            r0 = r22
            if (r6 != r0) goto L_0x030a
            r6 = 1
            int r6 = r6 << r20
            long r0 = (long) r6
            r22 = r0
            long r16 = r16 | r22
            r6 = r18
        L_0x022b:
            int r20 = r20 + 1
            r18 = r6
            goto L_0x01f4
        L_0x0230:
            if (r7 == 0) goto L_0x025f
            boolean r0 = r6.preventEdgeOffset
            r23 = r0
            if (r23 == 0) goto L_0x025f
            r23 = 1
            r0 = r18
            r1 = r23
            if (r0 != r1) goto L_0x025f
            r0 = r32
            int r0 = r0.mGeneratedItemPadding
            r23 = r0
            int r23 = r23 + r26
            r28 = 0
            r0 = r32
            int r0 = r0.mGeneratedItemPadding
            r29 = r0
            r30 = 0
            r0 = r22
            r1 = r23
            r2 = r28
            r3 = r29
            r4 = r30
            r0.setPadding(r1, r2, r3, r4)
        L_0x025f:
            int r0 = r6.cellsUsed
            r22 = r0
            int r22 = r22 + 1
            r0 = r22
            r6.cellsUsed = r0
            r22 = 1
            r0 = r22
            r6.expanded = r0
            int r6 = r18 + -1
            goto L_0x022b
        L_0x0272:
            r20 = 1
            r22 = r16
            goto L_0x0110
        L_0x0278:
            r6 = 0
            goto L_0x0170
        L_0x027b:
            r6 = 0
            r7 = r6
            goto L_0x01d7
        L_0x027f:
            r0 = r32
            android.view.View r10 = r0.getChildAt(r9)
            android.view.ViewGroup$LayoutParams r6 = r10.getLayoutParams()
            android.support.v7.widget.ActionMenuView$LayoutParams r6 = (android.support.p003v7.widget.ActionMenuView.LayoutParams) r6
            boolean r10 = r10 instanceof android.support.p003v7.view.menu.ActionMenuItemView
            if (r10 == 0) goto L_0x02a2
            r6.extraPixels = r7
            r8 = 1
            r6.expanded = r8
            if (r9 != 0) goto L_0x029f
            boolean r8 = r6.preventEdgeOffset
            if (r8 != 0) goto L_0x029f
            int r8 = -r7
            int r8 = r8 / 2
            r6.leftMargin = r8
        L_0x029f:
            r6 = 1
            goto L_0x01eb
        L_0x02a2:
            boolean r10 = r6.isOverflowButton
            if (r10 == 0) goto L_0x02b3
            r6.extraPixels = r7
            r8 = 1
            r6.expanded = r8
            int r8 = -r7
            int r8 = r8 / 2
            r6.rightMargin = r8
            r6 = 1
            goto L_0x01eb
        L_0x02b3:
            if (r9 == 0) goto L_0x02b9
            int r10 = r7 / 2
            r6.leftMargin = r10
        L_0x02b9:
            int r10 = r27 + -1
            if (r9 == r10) goto L_0x0307
            int r10 = r7 / 2
            r6.rightMargin = r10
            r6 = r8
            goto L_0x01eb
        L_0x02c4:
            r8 = r20
        L_0x02c6:
            if (r8 == 0) goto L_0x02f5
            r6 = 0
            r7 = r6
        L_0x02ca:
            r0 = r27
            if (r7 >= r0) goto L_0x02f5
            r0 = r32
            android.view.View r8 = r0.getChildAt(r7)
            android.view.ViewGroup$LayoutParams r6 = r8.getLayoutParams()
            android.support.v7.widget.ActionMenuView$LayoutParams r6 = (android.support.p003v7.widget.ActionMenuView.LayoutParams) r6
            boolean r9 = r6.expanded
            if (r9 != 0) goto L_0x02e2
        L_0x02de:
            int r6 = r7 + 1
            r7 = r6
            goto L_0x02ca
        L_0x02e2:
            int r9 = r6.cellsUsed
            int r9 = r9 * r26
            int r6 = r6.extraPixels
            int r6 = r6 + r9
            r9 = 1073741824(0x40000000, float:2.0)
            int r6 = android.view.View.MeasureSpec.makeMeasureSpec(r6, r9)
            r0 = r24
            r8.measure(r6, r0)
            goto L_0x02de
        L_0x02f5:
            r6 = 1073741824(0x40000000, float:2.0)
            r0 = r21
            if (r0 == r6) goto L_0x0304
        L_0x02fb:
            r0 = r32
            r1 = r25
            r0.setMeasuredDimension(r1, r15)
            goto L_0x003e
        L_0x0304:
            r15 = r19
            goto L_0x02fb
        L_0x0307:
            r6 = r8
            goto L_0x01eb
        L_0x030a:
            r6 = r18
            goto L_0x022b
        L_0x030e:
            r6 = r7
            goto L_0x01cb
        L_0x0311:
            r6 = r8
            goto L_0x0139
        L_0x0314:
            r6 = r10
            r8 = r9
            goto L_0x006f
        */
        throw new UnsupportedOperationException("Method not decompiled: android.support.p003v7.widget.ActionMenuView.onMeasureExactFormat(int, int):void");
    }

    /* access modifiers changed from: protected */
    public boolean checkLayoutParams(android.view.ViewGroup.LayoutParams layoutParams) {
        return layoutParams != null && (layoutParams instanceof LayoutParams);
    }

    public void dismissPopupMenus() {
        if (this.mPresenter != null) {
            this.mPresenter.dismissPopupMenus();
        }
    }

    public boolean dispatchPopulateAccessibilityEvent(AccessibilityEvent accessibilityEvent) {
        return false;
    }

    /* access modifiers changed from: protected */
    public LayoutParams generateDefaultLayoutParams() {
        LayoutParams layoutParams = new LayoutParams(-2, -2);
        layoutParams.gravity = 16;
        return layoutParams;
    }

    public LayoutParams generateLayoutParams(AttributeSet attributeSet) {
        return new LayoutParams(getContext(), attributeSet);
    }

    /* access modifiers changed from: protected */
    public LayoutParams generateLayoutParams(android.view.ViewGroup.LayoutParams layoutParams) {
        if (layoutParams == null) {
            return generateDefaultLayoutParams();
        }
        LayoutParams layoutParams2 = layoutParams instanceof LayoutParams ? new LayoutParams((LayoutParams) layoutParams) : new LayoutParams(layoutParams);
        if (layoutParams2.gravity > 0) {
            return layoutParams2;
        }
        layoutParams2.gravity = 16;
        return layoutParams2;
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    public LayoutParams generateOverflowButtonLayoutParams() {
        LayoutParams generateDefaultLayoutParams = generateDefaultLayoutParams();
        generateDefaultLayoutParams.isOverflowButton = true;
        return generateDefaultLayoutParams;
    }

    public Menu getMenu() {
        if (this.mMenu == null) {
            Context context = getContext();
            this.mMenu = new MenuBuilder(context);
            this.mMenu.setCallback(new MenuBuilderCallback());
            this.mPresenter = new ActionMenuPresenter(context);
            this.mPresenter.setReserveOverflow(true);
            this.mPresenter.setCallback(this.mActionMenuPresenterCallback != null ? this.mActionMenuPresenterCallback : new ActionMenuPresenterCallback());
            this.mMenu.addMenuPresenter(this.mPresenter, this.mPopupContext);
            this.mPresenter.setMenuView(this);
        }
        return this.mMenu;
    }

    @Nullable
    public Drawable getOverflowIcon() {
        getMenu();
        return this.mPresenter.getOverflowIcon();
    }

    public int getPopupTheme() {
        return this.mPopupTheme;
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    public int getWindowAnimations() {
        return 0;
    }

    /* access modifiers changed from: protected */
    @RestrictTo({Scope.LIBRARY_GROUP})
    public boolean hasSupportDividerBeforeChildAt(int i) {
        boolean z = false;
        if (i == 0) {
            return false;
        }
        View childAt = getChildAt(i - 1);
        View childAt2 = getChildAt(i);
        if (i < getChildCount() && (childAt instanceof ActionMenuChildView)) {
            z = ((ActionMenuChildView) childAt).needsDividerAfter() | false;
        }
        return (i <= 0 || !(childAt2 instanceof ActionMenuChildView)) ? z : ((ActionMenuChildView) childAt2).needsDividerBefore() | z;
    }

    public boolean hideOverflowMenu() {
        return this.mPresenter != null && this.mPresenter.hideOverflowMenu();
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    public void initialize(MenuBuilder menuBuilder) {
        this.mMenu = menuBuilder;
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    public boolean invokeItem(MenuItemImpl menuItemImpl) {
        return this.mMenu.performItemAction(menuItemImpl, 0);
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    public boolean isOverflowMenuShowPending() {
        return this.mPresenter != null && this.mPresenter.isOverflowMenuShowPending();
    }

    public boolean isOverflowMenuShowing() {
        return this.mPresenter != null && this.mPresenter.isOverflowMenuShowing();
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    public boolean isOverflowReserved() {
        return this.mReserveOverflow;
    }

    public void onConfigurationChanged(Configuration configuration) {
        super.onConfigurationChanged(configuration);
        if (this.mPresenter != null) {
            this.mPresenter.updateMenuView(false);
            if (this.mPresenter.isOverflowMenuShowing()) {
                this.mPresenter.hideOverflowMenu();
                this.mPresenter.showOverflowMenu();
            }
        }
    }

    public void onDetachedFromWindow() {
        super.onDetachedFromWindow();
        dismissPopupMenus();
    }

    /* access modifiers changed from: protected */
    public void onLayout(boolean z, int i, int i2, int i3, int i4) {
        int i5;
        int i6;
        boolean z2;
        int width;
        int i7;
        if (!this.mFormatItems) {
            super.onLayout(z, i, i2, i3, i4);
            return;
        }
        int childCount = getChildCount();
        int i8 = (i4 - i2) / 2;
        int dividerWidth = getDividerWidth();
        int i9 = 0;
        int i10 = 0;
        int paddingRight = ((i3 - i) - getPaddingRight()) - getPaddingLeft();
        boolean z3 = false;
        boolean isLayoutRtl = ViewUtils.isLayoutRtl(this);
        for (int i11 = 0; i11 < childCount; i11++) {
            View childAt = getChildAt(i11);
            if (childAt.getVisibility() == 8) {
                z2 = z3;
            } else {
                LayoutParams layoutParams = (LayoutParams) childAt.getLayoutParams();
                if (layoutParams.isOverflowButton) {
                    int measuredWidth = childAt.getMeasuredWidth();
                    if (hasSupportDividerBeforeChildAt(i11)) {
                        measuredWidth += dividerWidth;
                    }
                    int measuredHeight = childAt.getMeasuredHeight();
                    if (isLayoutRtl) {
                        i7 = layoutParams.leftMargin + getPaddingLeft();
                        width = i7 + measuredWidth;
                    } else {
                        width = (getWidth() - getPaddingRight()) - layoutParams.rightMargin;
                        i7 = width - measuredWidth;
                    }
                    int i12 = i8 - (measuredHeight / 2);
                    childAt.layout(i7, i12, width, measuredHeight + i12);
                    z2 = true;
                    paddingRight -= measuredWidth;
                } else {
                    int measuredWidth2 = layoutParams.rightMargin + childAt.getMeasuredWidth() + layoutParams.leftMargin;
                    i9 += measuredWidth2;
                    paddingRight -= measuredWidth2;
                    if (hasSupportDividerBeforeChildAt(i11)) {
                        i9 += dividerWidth;
                    }
                    i10++;
                    z2 = z3;
                }
            }
            z3 = z2;
        }
        if (childCount != 1 || z3) {
            int i13 = i10 - (z3 ? 0 : 1);
            int max = Math.max(0, i13 > 0 ? paddingRight / i13 : 0);
            if (isLayoutRtl) {
                int width2 = getWidth() - getPaddingRight();
                int i14 = 0;
                while (i14 < childCount) {
                    View childAt2 = getChildAt(i14);
                    LayoutParams layoutParams2 = (LayoutParams) childAt2.getLayoutParams();
                    if (childAt2.getVisibility() == 8) {
                        i6 = width2;
                    } else if (layoutParams2.isOverflowButton) {
                        i6 = width2;
                    } else {
                        int i15 = width2 - layoutParams2.rightMargin;
                        int measuredWidth3 = childAt2.getMeasuredWidth();
                        int measuredHeight2 = childAt2.getMeasuredHeight();
                        int i16 = i8 - (measuredHeight2 / 2);
                        childAt2.layout(i15 - measuredWidth3, i16, i15, measuredHeight2 + i16);
                        i6 = i15 - ((layoutParams2.leftMargin + measuredWidth3) + max);
                    }
                    i14++;
                    width2 = i6;
                }
                return;
            }
            int paddingLeft = getPaddingLeft();
            int i17 = 0;
            while (i17 < childCount) {
                View childAt3 = getChildAt(i17);
                LayoutParams layoutParams3 = (LayoutParams) childAt3.getLayoutParams();
                if (childAt3.getVisibility() == 8) {
                    i5 = paddingLeft;
                } else if (layoutParams3.isOverflowButton) {
                    i5 = paddingLeft;
                } else {
                    int i18 = paddingLeft + layoutParams3.leftMargin;
                    int measuredWidth4 = childAt3.getMeasuredWidth();
                    int measuredHeight3 = childAt3.getMeasuredHeight();
                    int i19 = i8 - (measuredHeight3 / 2);
                    childAt3.layout(i18, i19, i18 + measuredWidth4, measuredHeight3 + i19);
                    i5 = layoutParams3.rightMargin + measuredWidth4 + max + i18;
                }
                i17++;
                paddingLeft = i5;
            }
            return;
        }
        View childAt4 = getChildAt(0);
        int measuredWidth5 = childAt4.getMeasuredWidth();
        int measuredHeight4 = childAt4.getMeasuredHeight();
        int i20 = ((i3 - i) / 2) - (measuredWidth5 / 2);
        int i21 = i8 - (measuredHeight4 / 2);
        childAt4.layout(i20, i21, measuredWidth5 + i20, measuredHeight4 + i21);
    }

    /* access modifiers changed from: protected */
    public void onMeasure(int i, int i2) {
        boolean z = this.mFormatItems;
        this.mFormatItems = MeasureSpec.getMode(i) == 1073741824;
        if (z != this.mFormatItems) {
            this.mFormatItemsWidth = 0;
        }
        int size = MeasureSpec.getSize(i);
        if (!(!this.mFormatItems || this.mMenu == null || size == this.mFormatItemsWidth)) {
            this.mFormatItemsWidth = size;
            this.mMenu.onItemsChanged(true);
        }
        int childCount = getChildCount();
        if (!this.mFormatItems || childCount <= 0) {
            for (int i3 = 0; i3 < childCount; i3++) {
                LayoutParams layoutParams = (LayoutParams) getChildAt(i3).getLayoutParams();
                layoutParams.rightMargin = 0;
                layoutParams.leftMargin = 0;
            }
            super.onMeasure(i, i2);
            return;
        }
        onMeasureExactFormat(i, i2);
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    public MenuBuilder peekMenu() {
        return this.mMenu;
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    public void setExpandedActionViewsExclusive(boolean z) {
        this.mPresenter.setExpandedActionViewsExclusive(z);
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    public void setMenuCallbacks(Callback callback, MenuBuilder.Callback callback2) {
        this.mActionMenuPresenterCallback = callback;
        this.mMenuBuilderCallback = callback2;
    }

    public void setOnMenuItemClickListener(OnMenuItemClickListener onMenuItemClickListener) {
        this.mOnMenuItemClickListener = onMenuItemClickListener;
    }

    public void setOverflowIcon(@Nullable Drawable drawable) {
        getMenu();
        this.mPresenter.setOverflowIcon(drawable);
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    public void setOverflowReserved(boolean z) {
        this.mReserveOverflow = z;
    }

    public void setPopupTheme(@StyleRes int i) {
        if (this.mPopupTheme != i) {
            this.mPopupTheme = i;
            if (i == 0) {
                this.mPopupContext = getContext();
            } else {
                this.mPopupContext = new ContextThemeWrapper(getContext(), i);
            }
        }
    }

    @RestrictTo({Scope.LIBRARY_GROUP})
    public void setPresenter(ActionMenuPresenter actionMenuPresenter) {
        this.mPresenter = actionMenuPresenter;
        this.mPresenter.setMenuView(this);
    }

    public boolean showOverflowMenu() {
        return this.mPresenter != null && this.mPresenter.showOverflowMenu();
    }
}
