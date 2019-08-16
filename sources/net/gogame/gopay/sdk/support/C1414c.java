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
    public boolean f1134A = false;

    /* renamed from: B */
    private boolean f1135B = false;
    /* access modifiers changed from: private */

    /* renamed from: C */
    public OnClickListener f1136C;

    /* renamed from: D */
    private boolean f1137D = true;

    /* renamed from: E */
    private DataSetObserver f1138E = new C1641e(this);

    /* renamed from: F */
    private Runnable f1139F = new C1642f(this);

    /* renamed from: a */
    protected Scroller f1140a = new Scroller(getContext());

    /* renamed from: b */
    protected ListAdapter f1141b;

    /* renamed from: c */
    protected int f1142c;

    /* renamed from: d */
    protected int f1143d;

    /* renamed from: e */
    private final C1643g f1144e = new C1643g(this, 0);
    /* access modifiers changed from: private */

    /* renamed from: f */
    public GestureDetector f1145f;

    /* renamed from: g */
    private int f1146g;

    /* renamed from: h */
    private List f1147h = new ArrayList();
    /* access modifiers changed from: private */

    /* renamed from: i */
    public boolean f1148i = false;

    /* renamed from: j */
    private Rect f1149j = new Rect();

    /* renamed from: k */
    private View f1150k = null;

    /* renamed from: l */
    private int f1151l = 0;

    /* renamed from: m */
    private Drawable f1152m = null;

    /* renamed from: n */
    private Integer f1153n = null;

    /* renamed from: o */
    private int f1154o = Integer.MAX_VALUE;
    /* access modifiers changed from: private */

    /* renamed from: p */
    public int f1155p;

    /* renamed from: q */
    private int f1156q;

    /* renamed from: r */
    private int f1157r;

    /* renamed from: s */
    private C1648l f1158s = null;

    /* renamed from: t */
    private int f1159t = 0;
    /* access modifiers changed from: private */

    /* renamed from: u */
    public boolean f1160u = false;

    /* renamed from: v */
    private C1646j f1161v = null;

    /* renamed from: w */
    private int f1162w = C1647k.f1297a;

    /* renamed from: x */
    private EdgeEffectCompat f1163x;

    /* renamed from: y */
    private EdgeEffectCompat f1164y;

    /* renamed from: z */
    private int f1165z;

    public C1414c(Context context) {
        super(context, null);
        this.f1163x = new EdgeEffectCompat(context);
        this.f1164y = new EdgeEffectCompat(context);
        this.f1145f = new GestureDetector(context, this.f1144e);
        setOnTouchListener(new C1640d(this));
        m894a();
        setWillNotDraw(false);
        if (VERSION.SDK_INT >= 11) {
            C1644h.m958a(this.f1140a);
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public int m889a(int i, int i2) {
        int childCount = getChildCount();
        for (int i3 = 0; i3 < childCount; i3++) {
            getChildAt(i3).getHitRect(this.f1149j);
            if (this.f1149j.contains(i, i2)) {
                return i3;
            }
        }
        return -1;
    }

    /* renamed from: a */
    private View m892a(int i) {
        int itemViewType = this.f1141b.getItemViewType(i);
        if (m903b(itemViewType)) {
            return (View) ((Queue) this.f1147h.get(itemViewType)).poll();
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
        this.f1155p = -1;
        this.f1156q = -1;
        this.f1146g = 0;
        this.f1142c = 0;
        this.f1143d = 0;
        this.f1154o = Integer.MAX_VALUE;
        setCurrentScrollState$6c40596b(C1647k.f1297a);
    }

    /* renamed from: a */
    private void m895a(int i, View view) {
        int itemViewType = this.f1141b.getItemViewType(i);
        if (m903b(itemViewType)) {
            ((Queue) this.f1147h.get(itemViewType)).offer(view);
        }
    }

    /* renamed from: a */
    private void m896a(Canvas canvas, Rect rect) {
        if (this.f1152m != null) {
            this.f1152m.setBounds(rect);
            this.f1152m.draw(canvas);
        }
    }

    /* renamed from: a */
    private void m897a(View view, int i) {
        addViewInLayout(view, i, m893a(view), true);
        LayoutParams a = m893a(view);
        view.measure(a.width > 0 ? MeasureSpec.makeMeasureSpec(a.width, 1073741824) : MeasureSpec.makeMeasureSpec(0, 0), ViewGroup.getChildMeasureSpec(this.f1165z, getPaddingTop() + getPaddingBottom(), a.height));
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public void m898a(Boolean bool) {
        if (this.f1135B != bool.booleanValue()) {
            for (View view = this; view.getParent() instanceof View; view = (View) view.getParent()) {
                if ((view.getParent() instanceof ListView) || (view.getParent() instanceof ScrollView)) {
                    view.getParent().requestDisallowInterceptTouchEvent(bool.booleanValue());
                    this.f1135B = bool.booleanValue();
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
        if (cVar.f1163x != null && cVar.f1164y != null) {
            int i2 = cVar.f1142c + i;
            if (cVar.f1140a != null && !cVar.f1140a.isFinished()) {
                return;
            }
            if (i2 < 0) {
                cVar.f1163x.onPull(((float) Math.abs(i)) / ((float) cVar.getRenderWidth()));
                if (!cVar.f1164y.isFinished()) {
                    cVar.f1164y.onRelease();
                }
            } else if (i2 > cVar.f1154o) {
                cVar.f1164y.onPull(((float) Math.abs(i)) / ((float) cVar.getRenderWidth()));
                if (!cVar.f1163x.isFinished()) {
                    cVar.f1163x.onRelease();
                }
            }
        }
    }

    /* renamed from: b */
    private boolean m903b(int i) {
        return i < this.f1147h.size();
    }

    /* renamed from: c */
    private float m905c() {
        if (VERSION.SDK_INT >= 14) {
            return C1645i.m959a(this.f1140a);
        }
        return 30.0f;
    }

    /* renamed from: c */
    private boolean m906c(int i) {
        return i == this.f1141b.getCount() + -1;
    }

    /* access modifiers changed from: private */
    /* renamed from: d */
    public void m908d() {
        if (this.f1150k != null) {
            this.f1150k.setPressed(false);
            refreshDrawableState();
            this.f1150k = null;
        }
    }

    /* renamed from: e */
    private void m910e() {
        if (this.f1163x != null) {
            this.f1163x.onRelease();
        }
        if (this.f1164y != null) {
            this.f1164y.onRelease();
        }
    }

    /* renamed from: f */
    private boolean m912f() {
        return this.f1141b != null && !this.f1141b.isEmpty() && this.f1154o > 0;
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
        this.f1162w = i;
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public final boolean mo21578a(float f) {
        this.f1140a.fling(this.f1143d, 0, (int) (-f), 0, 0, this.f1154o, 0, 0);
        setCurrentScrollState$6c40596b(C1647k.f1299c);
        requestLayout();
        return true;
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public final boolean mo21579a(MotionEvent motionEvent) {
        this.f1134A = !this.f1140a.isFinished();
        this.f1140a.forceFinished(true);
        setCurrentScrollState$6c40596b(C1647k.f1297a);
        m908d();
        if (!this.f1134A) {
            int a = m889a((int) motionEvent.getX(), (int) motionEvent.getY());
            if (a >= 0) {
                this.f1150k = getChildAt(a);
                if (this.f1150k != null) {
                    this.f1150k.setPressed(true);
                    refreshDrawableState();
                }
            }
        }
        return true;
    }

    /* access modifiers changed from: protected */
    public final void dispatchDraw(Canvas canvas) {
        super.dispatchDraw(canvas);
        if (this.f1163x != null && !this.f1163x.isFinished() && m912f()) {
            int save = canvas.save();
            int height = getHeight();
            canvas.rotate(-90.0f, 0.0f, 0.0f);
            canvas.translate((float) ((-height) + getPaddingBottom()), 0.0f);
            this.f1163x.setSize(getRenderHeight(), getRenderWidth());
            if (this.f1163x.draw(canvas)) {
                invalidate();
            }
            canvas.restoreToCount(save);
        } else if (this.f1164y != null && !this.f1164y.isFinished() && m912f()) {
            int save2 = canvas.save();
            int width = getWidth();
            canvas.rotate(90.0f, 0.0f, 0.0f);
            canvas.translate((float) getPaddingTop(), (float) (-width));
            this.f1164y.setSize(getRenderHeight(), getRenderWidth());
            if (this.f1164y.draw(canvas)) {
                invalidate();
            }
            canvas.restoreToCount(save2);
        }
    }

    /* access modifiers changed from: protected */
    public final void dispatchSetPressed(boolean z) {
    }

    public final ListAdapter getAdapter() {
        return this.f1141b;
    }

    public final int getFirstVisiblePosition() {
        return this.f1155p;
    }

    public final int getLastVisiblePosition() {
        return this.f1156q;
    }

    /* access modifiers changed from: protected */
    public final float getLeftFadingEdgeStrength() {
        int horizontalFadingEdgeLength = getHorizontalFadingEdgeLength();
        if (this.f1142c == 0) {
            return 0.0f;
        }
        if (this.f1142c < horizontalFadingEdgeLength) {
            return ((float) this.f1142c) / ((float) horizontalFadingEdgeLength);
        }
        return 1.0f;
    }

    /* access modifiers changed from: protected */
    public final float getRightFadingEdgeStrength() {
        int horizontalFadingEdgeLength = getHorizontalFadingEdgeLength();
        if (this.f1142c == this.f1154o) {
            return 0.0f;
        }
        if (this.f1154o - this.f1142c < horizontalFadingEdgeLength) {
            return ((float) (this.f1154o - this.f1142c)) / ((float) horizontalFadingEdgeLength);
        }
        return 1.0f;
    }

    public final View getSelectedView() {
        int i = this.f1157r;
        if (i < this.f1155p || i > this.f1156q) {
            return null;
        }
        return getChildAt(i - this.f1155p);
    }

    /* access modifiers changed from: protected */
    public final void onDraw(Canvas canvas) {
        super.onDraw(canvas);
        int childCount = getChildCount();
        Rect rect = this.f1149j;
        this.f1149j.top = getPaddingTop();
        this.f1149j.bottom = this.f1149j.top + getRenderHeight();
        for (int i = 0; i < childCount; i++) {
            if (i != childCount - 1 || !m906c(this.f1156q)) {
                View childAt = getChildAt(i);
                rect.left = childAt.getRight();
                rect.right = childAt.getRight() + this.f1151l;
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
            if (this.f1141b != null) {
                invalidate();
                if (this.f1148i) {
                    int i6 = this.f1142c;
                    m894a();
                    removeAllViewsInLayout();
                    this.f1143d = i6;
                    this.f1148i = false;
                }
                if (this.f1153n != null) {
                    this.f1143d = this.f1153n.intValue();
                    this.f1153n = null;
                }
                if (this.f1140a.computeScrollOffset()) {
                    this.f1143d = this.f1140a.getCurrX();
                }
                if (this.f1143d < 0) {
                    this.f1143d = 0;
                    if (this.f1137D && this.f1163x.isFinished()) {
                        this.f1163x.onAbsorb((int) m905c());
                    }
                    this.f1140a.forceFinished(true);
                    setCurrentScrollState$6c40596b(C1647k.f1297a);
                } else if (this.f1143d > this.f1154o) {
                    this.f1143d = this.f1154o;
                    if (this.f1137D && this.f1164y.isFinished()) {
                        this.f1164y.onAbsorb((int) m905c());
                    }
                    this.f1140a.forceFinished(true);
                    setCurrentScrollState$6c40596b(C1647k.f1297a);
                }
                int i7 = this.f1142c - this.f1143d;
                View leftmostChild = getLeftmostChild();
                while (leftmostChild != null && leftmostChild.getRight() + i7 <= 0) {
                    this.f1146g = (m906c(this.f1155p) ? leftmostChild.getMeasuredWidth() : this.f1151l + leftmostChild.getMeasuredWidth()) + this.f1146g;
                    m895a(this.f1155p, leftmostChild);
                    removeViewInLayout(leftmostChild);
                    this.f1155p++;
                    leftmostChild = getLeftmostChild();
                }
                while (true) {
                    View rightmostChild = getRightmostChild();
                    if (rightmostChild == null || rightmostChild.getLeft() + i7 < getWidth()) {
                        View rightmostChild2 = getRightmostChild();
                    } else {
                        m895a(this.f1156q, rightmostChild);
                        removeViewInLayout(rightmostChild);
                        this.f1156q--;
                    }
                }
                View rightmostChild22 = getRightmostChild();
                int right = rightmostChild22 != null ? rightmostChild22.getRight() : 0;
                while (right + i7 + this.f1151l < getWidth() && this.f1156q + 1 < this.f1141b.getCount()) {
                    this.f1156q++;
                    if (this.f1155p < 0) {
                        this.f1155p = this.f1156q;
                    }
                    View view = this.f1141b.getView(this.f1156q, m892a(this.f1156q), this);
                    m897a(view, -1);
                    right += (this.f1156q == 0 ? 0 : this.f1151l) + view.getMeasuredWidth();
                    if (this.f1158s != null && this.f1141b != null && this.f1141b.getCount() - (this.f1156q + 1) < this.f1159t && !this.f1160u) {
                        this.f1160u = true;
                    }
                }
                View leftmostChild2 = getLeftmostChild();
                int left = leftmostChild2 != null ? leftmostChild2.getLeft() : 0;
                while ((left + i7) - this.f1151l > 0 && this.f1155p > 0) {
                    this.f1155p--;
                    View view2 = this.f1141b.getView(this.f1155p, m892a(this.f1155p), this);
                    m897a(view2, 0);
                    left -= this.f1155p == 0 ? view2.getMeasuredWidth() : this.f1151l + view2.getMeasuredWidth();
                    this.f1146g -= left + i7 == 0 ? view2.getMeasuredWidth() : this.f1151l + view2.getMeasuredWidth();
                }
                int childCount = getChildCount();
                if (!this.f1137D) {
                    this.f1146g = 0;
                    i5 = 0;
                } else {
                    i5 = i7;
                }
                if (childCount > 0) {
                    this.f1146g = i5 + this.f1146g;
                    int i8 = this.f1146g;
                    for (int i9 = 0; i9 < childCount; i9++) {
                        View childAt = getChildAt(i9);
                        int paddingLeft = getPaddingLeft() + i8;
                        int paddingTop = getPaddingTop();
                        childAt.layout(paddingLeft, paddingTop, childAt.getMeasuredWidth() + paddingLeft, childAt.getMeasuredHeight() + paddingTop);
                        i8 += childAt.getMeasuredWidth() + this.f1151l;
                    }
                }
                this.f1142c = this.f1143d;
                if (m906c(this.f1156q)) {
                    View rightmostChild3 = getRightmostChild();
                    if (rightmostChild3 != null) {
                        int i10 = this.f1154o;
                        this.f1154o = ((rightmostChild3.getRight() - getPaddingLeft()) + this.f1142c) - getRenderWidth();
                        if (this.f1154o < 0) {
                            this.f1154o = 0;
                        }
                        if (this.f1154o != i10) {
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
        if (!this.f1140a.isFinished()) {
            ViewCompat.postOnAnimation(this, this.f1139F);
        } else if (this.f1162w == C1647k.f1299c) {
            setCurrentScrollState$6c40596b(C1647k.f1297a);
        }
    }

    /* access modifiers changed from: protected */
    public final void onMeasure(int i, int i2) {
        super.onMeasure(i, i2);
        this.f1165z = i2;
    }

    public final void onRestoreInstanceState(Parcelable parcelable) {
        if (parcelable instanceof Bundle) {
            Bundle bundle = (Bundle) parcelable;
            this.f1153n = Integer.valueOf(bundle.getInt("BUNDLE_ID_CURRENT_X"));
            super.onRestoreInstanceState(bundle.getParcelable("BUNDLE_ID_PARENT_STATE"));
        }
    }

    public final Parcelable onSaveInstanceState() {
        Bundle bundle = new Bundle();
        bundle.putParcelable("BUNDLE_ID_PARENT_STATE", super.onSaveInstanceState());
        bundle.putInt("BUNDLE_ID_CURRENT_X", this.f1142c);
        return bundle;
    }

    public final boolean onTouchEvent(MotionEvent motionEvent) {
        if (motionEvent.getAction() == 1) {
            if (this.f1140a == null || this.f1140a.isFinished()) {
                setCurrentScrollState$6c40596b(C1647k.f1297a);
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
        if (this.f1141b != null) {
            this.f1141b.unregisterDataSetObserver(this.f1138E);
        }
        if (listAdapter != null) {
            this.f1160u = false;
            this.f1141b = listAdapter;
            this.f1141b.registerDataSetObserver(this.f1138E);
        }
        int viewTypeCount = this.f1141b.getViewTypeCount();
        this.f1147h.clear();
        for (int i = 0; i < viewTypeCount; i++) {
            this.f1147h.add(new LinkedList());
        }
        m901b();
    }

    public final void setDivider(Drawable drawable) {
        this.f1152m = drawable;
        if (drawable != null) {
            setDividerWidth(drawable.getIntrinsicWidth());
        } else {
            setDividerWidth(0);
        }
    }

    public final void setDividerWidth(int i) {
        this.f1151l = i;
        requestLayout();
        invalidate();
    }

    public final void setOnClickListener(OnClickListener onClickListener) {
        this.f1136C = onClickListener;
    }

    public final void setOnScrollStateChangedListener(C1646j jVar) {
        this.f1161v = jVar;
    }

    public final void setScrollingEnabled(boolean z) {
        this.f1137D = z;
    }

    public final void setSelection(int i) {
        this.f1157r = i;
    }
}
