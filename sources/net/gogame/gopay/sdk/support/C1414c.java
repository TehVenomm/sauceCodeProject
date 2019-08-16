package net.gogame.gopay.sdk.support;

import android.annotation.SuppressLint;
import android.content.Context;
import android.database.DataSetObserver;
import android.graphics.Canvas;
import android.graphics.Rect;
import android.graphics.drawable.Drawable;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.os.Parcelable;
import android.support.p000v4.view.ViewCompat;
import android.support.p000v4.widget.EdgeEffectCompat;
import android.view.GestureDetector;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.MeasureSpec;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.widget.AdapterView;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.ScrollView;
import android.widget.Scroller;
import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.Queue;

/* renamed from: net.gogame.gopay.sdk.support.c */
public final class C1414c extends AdapterView {
    /* access modifiers changed from: private */

    /* renamed from: A */
    public boolean f1140A = false;

    /* renamed from: B */
    private boolean f1141B = false;
    /* access modifiers changed from: private */

    /* renamed from: C */
    public OnClickListener f1142C;

    /* renamed from: D */
    private boolean f1143D = true;

    /* renamed from: E */
    private DataSetObserver f1144E = new C1641e(this);

    /* renamed from: F */
    private Runnable f1145F = new C1642f(this);

    /* renamed from: a */
    protected Scroller f1146a = new Scroller(getContext());

    /* renamed from: b */
    protected ListAdapter f1147b;

    /* renamed from: c */
    protected int f1148c;

    /* renamed from: d */
    protected int f1149d;

    /* renamed from: e */
    private final C1643g f1150e = new C1643g(this, 0);
    /* access modifiers changed from: private */

    /* renamed from: f */
    public GestureDetector f1151f;

    /* renamed from: g */
    private int f1152g;

    /* renamed from: h */
    private List f1153h = new ArrayList();
    /* access modifiers changed from: private */

    /* renamed from: i */
    public boolean f1154i = false;

    /* renamed from: j */
    private Rect f1155j = new Rect();

    /* renamed from: k */
    private View f1156k = null;

    /* renamed from: l */
    private int f1157l = 0;

    /* renamed from: m */
    private Drawable f1158m = null;

    /* renamed from: n */
    private Integer f1159n = null;

    /* renamed from: o */
    private int f1160o = Integer.MAX_VALUE;
    /* access modifiers changed from: private */

    /* renamed from: p */
    public int f1161p;

    /* renamed from: q */
    private int f1162q;

    /* renamed from: r */
    private int f1163r;

    /* renamed from: s */
    private C1648l f1164s = null;

    /* renamed from: t */
    private int f1165t = 0;
    /* access modifiers changed from: private */

    /* renamed from: u */
    public boolean f1166u = false;

    /* renamed from: v */
    private C1646j f1167v = null;

    /* renamed from: w */
    private int f1168w = C1647k.f1309a;

    /* renamed from: x */
    private EdgeEffectCompat f1169x;

    /* renamed from: y */
    private EdgeEffectCompat f1170y;

    /* renamed from: z */
    private int f1171z;

    public C1414c(Context context) {
        super(context, null);
        this.f1169x = new EdgeEffectCompat(context);
        this.f1170y = new EdgeEffectCompat(context);
        this.f1151f = new GestureDetector(context, this.f1150e);
        setOnTouchListener(new C1640d(this));
        m894a();
        setWillNotDraw(false);
        if (VERSION.SDK_INT >= 11) {
            C1644h.m958a(this.f1146a);
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public int m889a(int i, int i2) {
        int childCount = getChildCount();
        for (int i3 = 0; i3 < childCount; i3++) {
            getChildAt(i3).getHitRect(this.f1155j);
            if (this.f1155j.contains(i, i2)) {
                return i3;
            }
        }
        return -1;
    }

    /* renamed from: a */
    private View m892a(int i) {
        int itemViewType = this.f1147b.getItemViewType(i);
        if (m903b(itemViewType)) {
            return (View) ((Queue) this.f1153h.get(itemViewType)).poll();
        }
        return null;
    }

    /* renamed from: a */
    private static LayoutParams m893a(View view) {
        LayoutParams layoutParams = view.getLayoutParams();
        return layoutParams == null ? new LayoutParams(-2, -1) : layoutParams;
    }

    /* renamed from: a */
    private void m894a() {
        this.f1161p = -1;
        this.f1162q = -1;
        this.f1152g = 0;
        this.f1148c = 0;
        this.f1149d = 0;
        this.f1160o = Integer.MAX_VALUE;
        setCurrentScrollState$6c40596b(C1647k.f1309a);
    }

    /* renamed from: a */
    private void m895a(int i, View view) {
        int itemViewType = this.f1147b.getItemViewType(i);
        if (m903b(itemViewType)) {
            ((Queue) this.f1153h.get(itemViewType)).offer(view);
        }
    }

    /* renamed from: a */
    private void m896a(Canvas canvas, Rect rect) {
        if (this.f1158m != null) {
            this.f1158m.setBounds(rect);
            this.f1158m.draw(canvas);
        }
    }

    /* renamed from: a */
    private void m897a(View view, int i) {
        addViewInLayout(view, i, m893a(view), true);
        LayoutParams a = m893a(view);
        view.measure(a.width > 0 ? MeasureSpec.makeMeasureSpec(a.width, 1073741824) : MeasureSpec.makeMeasureSpec(0, 0), ViewGroup.getChildMeasureSpec(this.f1171z, getPaddingTop() + getPaddingBottom(), a.height));
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public void m898a(Boolean bool) {
        if (this.f1141B != bool.booleanValue()) {
            for (View view = this; view.getParent() instanceof View; view = (View) view.getParent()) {
                if ((view.getParent() instanceof ListView) || (view.getParent() instanceof ScrollView)) {
                    view.getParent().requestDisallowInterceptTouchEvent(bool.booleanValue());
                    this.f1141B = bool.booleanValue();
                    return;
                }
            }
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: b */
    public void m901b() {
        m894a();
        removeAllViewsInLayout();
        requestLayout();
    }

    /* renamed from: b */
    static /* synthetic */ void m902b(C1414c cVar, int i) {
        if (cVar.f1169x != null && cVar.f1170y != null) {
            int i2 = cVar.f1148c + i;
            if (cVar.f1146a != null && !cVar.f1146a.isFinished()) {
                return;
            }
            if (i2 < 0) {
                cVar.f1169x.onPull(((float) Math.abs(i)) / ((float) cVar.getRenderWidth()));
                if (!cVar.f1170y.isFinished()) {
                    cVar.f1170y.onRelease();
                }
            } else if (i2 > cVar.f1160o) {
                cVar.f1170y.onPull(((float) Math.abs(i)) / ((float) cVar.getRenderWidth()));
                if (!cVar.f1169x.isFinished()) {
                    cVar.f1169x.onRelease();
                }
            }
        }
    }

    /* renamed from: b */
    private boolean m903b(int i) {
        return i < this.f1153h.size();
    }

    /* renamed from: c */
    private float m905c() {
        if (VERSION.SDK_INT >= 14) {
            return C1645i.m959a(this.f1146a);
        }
        return 30.0f;
    }

    /* renamed from: c */
    private boolean m906c(int i) {
        return i == this.f1147b.getCount() + -1;
    }

    /* access modifiers changed from: private */
    /* renamed from: d */
    public void m908d() {
        if (this.f1156k != null) {
            this.f1156k.setPressed(false);
            refreshDrawableState();
            this.f1156k = null;
        }
    }

    /* renamed from: e */
    private void m910e() {
        if (this.f1169x != null) {
            this.f1169x.onRelease();
        }
        if (this.f1170y != null) {
            this.f1170y.onRelease();
        }
    }

    /* renamed from: f */
    private boolean m912f() {
        return this.f1147b != null && !this.f1147b.isEmpty() && this.f1160o > 0;
    }

    private View getLeftmostChild() {
        return getChildAt(0);
    }

    private int getRenderHeight() {
        return (getHeight() - getPaddingTop()) - getPaddingBottom();
    }

    private int getRenderWidth() {
        return (getWidth() - getPaddingLeft()) - getPaddingRight();
    }

    private View getRightmostChild() {
        return getChildAt(getChildCount() - 1);
    }

    /* access modifiers changed from: private */
    public void setCurrentScrollState$6c40596b(int i) {
        this.f1168w = i;
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public final boolean mo21578a(float f) {
        this.f1146a.fling(this.f1149d, 0, (int) (-f), 0, 0, this.f1160o, 0, 0);
        setCurrentScrollState$6c40596b(C1647k.f1311c);
        requestLayout();
        return true;
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public final boolean mo21579a(MotionEvent motionEvent) {
        this.f1140A = !this.f1146a.isFinished();
        this.f1146a.forceFinished(true);
        setCurrentScrollState$6c40596b(C1647k.f1309a);
        m908d();
        if (!this.f1140A) {
            int a = m889a((int) motionEvent.getX(), (int) motionEvent.getY());
            if (a >= 0) {
                this.f1156k = getChildAt(a);
                if (this.f1156k != null) {
                    this.f1156k.setPressed(true);
                    refreshDrawableState();
                }
            }
        }
        return true;
    }

    /* access modifiers changed from: protected */
    public final void dispatchDraw(Canvas canvas) {
        super.dispatchDraw(canvas);
        if (this.f1169x != null && !this.f1169x.isFinished() && m912f()) {
            int save = canvas.save();
            int height = getHeight();
            canvas.rotate(-90.0f, 0.0f, 0.0f);
            canvas.translate((float) ((-height) + getPaddingBottom()), 0.0f);
            this.f1169x.setSize(getRenderHeight(), getRenderWidth());
            if (this.f1169x.draw(canvas)) {
                invalidate();
            }
            canvas.restoreToCount(save);
        } else if (this.f1170y != null && !this.f1170y.isFinished() && m912f()) {
            int save2 = canvas.save();
            int width = getWidth();
            canvas.rotate(90.0f, 0.0f, 0.0f);
            canvas.translate((float) getPaddingTop(), (float) (-width));
            this.f1170y.setSize(getRenderHeight(), getRenderWidth());
            if (this.f1170y.draw(canvas)) {
                invalidate();
            }
            canvas.restoreToCount(save2);
        }
    }

    /* access modifiers changed from: protected */
    public final void dispatchSetPressed(boolean z) {
    }

    public final ListAdapter getAdapter() {
        return this.f1147b;
    }

    public final int getFirstVisiblePosition() {
        return this.f1161p;
    }

    public final int getLastVisiblePosition() {
        return this.f1162q;
    }

    /* access modifiers changed from: protected */
    public final float getLeftFadingEdgeStrength() {
        int horizontalFadingEdgeLength = getHorizontalFadingEdgeLength();
        if (this.f1148c == 0) {
            return 0.0f;
        }
        if (this.f1148c < horizontalFadingEdgeLength) {
            return ((float) this.f1148c) / ((float) horizontalFadingEdgeLength);
        }
        return 1.0f;
    }

    /* access modifiers changed from: protected */
    public final float getRightFadingEdgeStrength() {
        int horizontalFadingEdgeLength = getHorizontalFadingEdgeLength();
        if (this.f1148c == this.f1160o) {
            return 0.0f;
        }
        if (this.f1160o - this.f1148c < horizontalFadingEdgeLength) {
            return ((float) (this.f1160o - this.f1148c)) / ((float) horizontalFadingEdgeLength);
        }
        return 1.0f;
    }

    public final View getSelectedView() {
        int i = this.f1163r;
        if (i < this.f1161p || i > this.f1162q) {
            return null;
        }
        return getChildAt(i - this.f1161p);
    }

    /* access modifiers changed from: protected */
    public final void onDraw(Canvas canvas) {
        super.onDraw(canvas);
        int childCount = getChildCount();
        Rect rect = this.f1155j;
        this.f1155j.top = getPaddingTop();
        this.f1155j.bottom = this.f1155j.top + getRenderHeight();
        for (int i = 0; i < childCount; i++) {
            if (i != childCount - 1 || !m906c(this.f1162q)) {
                View childAt = getChildAt(i);
                rect.left = childAt.getRight();
                rect.right = childAt.getRight() + this.f1157l;
                if (rect.left < getPaddingLeft()) {
                    rect.left = getPaddingLeft();
                }
                if (rect.right > getWidth() - getPaddingRight()) {
                    rect.right = getWidth() - getPaddingRight();
                }
                m896a(canvas, rect);
                if (i == 0 && childAt.getLeft() > getPaddingLeft()) {
                    rect.left = getPaddingLeft();
                    rect.right = childAt.getLeft();
                    m896a(canvas, rect);
                }
            }
        }
    }

    /* access modifiers changed from: protected */
    @SuppressLint({"WrongCall"})
    public final void onLayout(boolean z, int i, int i2, int i3, int i4) {
        int i5;
        boolean z2;
        do {
            super.onLayout(z, i, i2, i3, i4);
            if (this.f1147b != null) {
                invalidate();
                if (this.f1154i) {
                    int i6 = this.f1148c;
                    m894a();
                    removeAllViewsInLayout();
                    this.f1149d = i6;
                    this.f1154i = false;
                }
                if (this.f1159n != null) {
                    this.f1149d = this.f1159n.intValue();
                    this.f1159n = null;
                }
                if (this.f1146a.computeScrollOffset()) {
                    this.f1149d = this.f1146a.getCurrX();
                }
                if (this.f1149d < 0) {
                    this.f1149d = 0;
                    if (this.f1143D && this.f1169x.isFinished()) {
                        this.f1169x.onAbsorb((int) m905c());
                    }
                    this.f1146a.forceFinished(true);
                    setCurrentScrollState$6c40596b(C1647k.f1309a);
                } else if (this.f1149d > this.f1160o) {
                    this.f1149d = this.f1160o;
                    if (this.f1143D && this.f1170y.isFinished()) {
                        this.f1170y.onAbsorb((int) m905c());
                    }
                    this.f1146a.forceFinished(true);
                    setCurrentScrollState$6c40596b(C1647k.f1309a);
                }
                int i7 = this.f1148c - this.f1149d;
                View leftmostChild = getLeftmostChild();
                while (leftmostChild != null && leftmostChild.getRight() + i7 <= 0) {
                    this.f1152g = (m906c(this.f1161p) ? leftmostChild.getMeasuredWidth() : this.f1157l + leftmostChild.getMeasuredWidth()) + this.f1152g;
                    m895a(this.f1161p, leftmostChild);
                    removeViewInLayout(leftmostChild);
                    this.f1161p++;
                    leftmostChild = getLeftmostChild();
                }
                while (true) {
                    View rightmostChild = getRightmostChild();
                    if (rightmostChild == null || rightmostChild.getLeft() + i7 < getWidth()) {
                        View rightmostChild2 = getRightmostChild();
                    } else {
                        m895a(this.f1162q, rightmostChild);
                        removeViewInLayout(rightmostChild);
                        this.f1162q--;
                    }
                }
                View rightmostChild22 = getRightmostChild();
                int right = rightmostChild22 != null ? rightmostChild22.getRight() : 0;
                while (right + i7 + this.f1157l < getWidth() && this.f1162q + 1 < this.f1147b.getCount()) {
                    this.f1162q++;
                    if (this.f1161p < 0) {
                        this.f1161p = this.f1162q;
                    }
                    View view = this.f1147b.getView(this.f1162q, m892a(this.f1162q), this);
                    m897a(view, -1);
                    right += (this.f1162q == 0 ? 0 : this.f1157l) + view.getMeasuredWidth();
                    if (this.f1164s != null && this.f1147b != null && this.f1147b.getCount() - (this.f1162q + 1) < this.f1165t && !this.f1166u) {
                        this.f1166u = true;
                    }
                }
                View leftmostChild2 = getLeftmostChild();
                int left = leftmostChild2 != null ? leftmostChild2.getLeft() : 0;
                while ((left + i7) - this.f1157l > 0 && this.f1161p > 0) {
                    this.f1161p--;
                    View view2 = this.f1147b.getView(this.f1161p, m892a(this.f1161p), this);
                    m897a(view2, 0);
                    left -= this.f1161p == 0 ? view2.getMeasuredWidth() : this.f1157l + view2.getMeasuredWidth();
                    this.f1152g -= left + i7 == 0 ? view2.getMeasuredWidth() : this.f1157l + view2.getMeasuredWidth();
                }
                int childCount = getChildCount();
                if (!this.f1143D) {
                    this.f1152g = 0;
                    i5 = 0;
                } else {
                    i5 = i7;
                }
                if (childCount > 0) {
                    this.f1152g = i5 + this.f1152g;
                    int i8 = this.f1152g;
                    for (int i9 = 0; i9 < childCount; i9++) {
                        View childAt = getChildAt(i9);
                        int paddingLeft = getPaddingLeft() + i8;
                        int paddingTop = getPaddingTop();
                        childAt.layout(paddingLeft, paddingTop, childAt.getMeasuredWidth() + paddingLeft, childAt.getMeasuredHeight() + paddingTop);
                        i8 += childAt.getMeasuredWidth() + this.f1157l;
                    }
                }
                this.f1148c = this.f1149d;
                if (m906c(this.f1162q)) {
                    View rightmostChild3 = getRightmostChild();
                    if (rightmostChild3 != null) {
                        int i10 = this.f1160o;
                        this.f1160o = ((rightmostChild3.getRight() - getPaddingLeft()) + this.f1148c) - getRenderWidth();
                        if (this.f1160o < 0) {
                            this.f1160o = 0;
                        }
                        if (this.f1160o != i10) {
                            z2 = true;
                            continue;
                        }
                    }
                }
                z2 = false;
                continue;
            } else {
                return;
            }
        } while (z2);
        if (!this.f1146a.isFinished()) {
            ViewCompat.postOnAnimation(this, this.f1145F);
        } else if (this.f1168w == C1647k.f1311c) {
            setCurrentScrollState$6c40596b(C1647k.f1309a);
        }
    }

    /* access modifiers changed from: protected */
    public final void onMeasure(int i, int i2) {
        super.onMeasure(i, i2);
        this.f1171z = i2;
    }

    public final void onRestoreInstanceState(Parcelable parcelable) {
        if (parcelable instanceof Bundle) {
            Bundle bundle = (Bundle) parcelable;
            this.f1159n = Integer.valueOf(bundle.getInt("BUNDLE_ID_CURRENT_X"));
            super.onRestoreInstanceState(bundle.getParcelable("BUNDLE_ID_PARENT_STATE"));
        }
    }

    public final Parcelable onSaveInstanceState() {
        Bundle bundle = new Bundle();
        bundle.putParcelable("BUNDLE_ID_PARENT_STATE", super.onSaveInstanceState());
        bundle.putInt("BUNDLE_ID_CURRENT_X", this.f1148c);
        return bundle;
    }

    public final boolean onTouchEvent(MotionEvent motionEvent) {
        if (motionEvent.getAction() == 1) {
            if (this.f1146a == null || this.f1146a.isFinished()) {
                setCurrentScrollState$6c40596b(C1647k.f1309a);
            }
            m898a(Boolean.valueOf(false));
            m910e();
        } else if (motionEvent.getAction() == 3) {
            m908d();
            m910e();
            m898a(Boolean.valueOf(false));
        }
        return super.onTouchEvent(motionEvent);
    }

    public final void setAdapter(ListAdapter listAdapter) {
        if (this.f1147b != null) {
            this.f1147b.unregisterDataSetObserver(this.f1144E);
        }
        if (listAdapter != null) {
            this.f1166u = false;
            this.f1147b = listAdapter;
            this.f1147b.registerDataSetObserver(this.f1144E);
        }
        int viewTypeCount = this.f1147b.getViewTypeCount();
        this.f1153h.clear();
        for (int i = 0; i < viewTypeCount; i++) {
            this.f1153h.add(new LinkedList());
        }
        m901b();
    }

    public final void setDivider(Drawable drawable) {
        this.f1158m = drawable;
        if (drawable != null) {
            setDividerWidth(drawable.getIntrinsicWidth());
        } else {
            setDividerWidth(0);
        }
    }

    public final void setDividerWidth(int i) {
        this.f1157l = i;
        requestLayout();
        invalidate();
    }

    public final void setOnClickListener(OnClickListener onClickListener) {
        this.f1142C = onClickListener;
    }

    public final void setOnScrollStateChangedListener(C1646j jVar) {
        this.f1167v = jVar;
    }

    public final void setScrollingEnabled(boolean z) {
        this.f1143D = z;
    }

    public final void setSelection(int i) {
        this.f1163r = i;
    }
}
