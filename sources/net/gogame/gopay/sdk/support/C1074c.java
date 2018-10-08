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
import android.support.v4.view.ViewCompat;
import android.support.v4.widget.EdgeEffectCompat;
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
import com.google.android.gms.nearby.messages.Strategy;
import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.Queue;

/* renamed from: net.gogame.gopay.sdk.support.c */
public final class C1074c extends AdapterView {
    /* renamed from: A */
    private boolean f1195A = false;
    /* renamed from: B */
    private boolean f1196B = false;
    /* renamed from: C */
    private OnClickListener f1197C;
    /* renamed from: D */
    private boolean f1198D = true;
    /* renamed from: E */
    private DataSetObserver f1199E = new C1076e(this);
    /* renamed from: F */
    private Runnable f1200F = new C1077f(this);
    /* renamed from: a */
    protected Scroller f1201a = new Scroller(getContext());
    /* renamed from: b */
    protected ListAdapter f1202b;
    /* renamed from: c */
    protected int f1203c;
    /* renamed from: d */
    protected int f1204d;
    /* renamed from: e */
    private final C1078g f1205e = new C1078g();
    /* renamed from: f */
    private GestureDetector f1206f;
    /* renamed from: g */
    private int f1207g;
    /* renamed from: h */
    private List f1208h = new ArrayList();
    /* renamed from: i */
    private boolean f1209i = false;
    /* renamed from: j */
    private Rect f1210j = new Rect();
    /* renamed from: k */
    private View f1211k = null;
    /* renamed from: l */
    private int f1212l = 0;
    /* renamed from: m */
    private Drawable f1213m = null;
    /* renamed from: n */
    private Integer f1214n = null;
    /* renamed from: o */
    private int f1215o = Strategy.TTL_SECONDS_INFINITE;
    /* renamed from: p */
    private int f1216p;
    /* renamed from: q */
    private int f1217q;
    /* renamed from: r */
    private int f1218r;
    /* renamed from: s */
    private C1083l f1219s = null;
    /* renamed from: t */
    private int f1220t = 0;
    /* renamed from: u */
    private boolean f1221u = false;
    /* renamed from: v */
    private C1081j f1222v = null;
    /* renamed from: w */
    private int f1223w = C1082k.f1231a;
    /* renamed from: x */
    private EdgeEffectCompat f1224x;
    /* renamed from: y */
    private EdgeEffectCompat f1225y;
    /* renamed from: z */
    private int f1226z;

    public C1074c(Context context) {
        super(context, null);
        this.f1224x = new EdgeEffectCompat(context);
        this.f1225y = new EdgeEffectCompat(context);
        this.f1206f = new GestureDetector(context, this.f1205e);
        setOnTouchListener(new C1075d(this));
        m895a();
        setWillNotDraw(false);
        if (VERSION.SDK_INT >= 11) {
            C1079h.m919a(this.f1201a);
        }
    }

    /* renamed from: a */
    private int m890a(int i, int i2) {
        int childCount = getChildCount();
        for (int i3 = 0; i3 < childCount; i3++) {
            getChildAt(i3).getHitRect(this.f1210j);
            if (this.f1210j.contains(i, i2)) {
                return i3;
            }
        }
        return -1;
    }

    /* renamed from: a */
    private View m893a(int i) {
        int itemViewType = this.f1202b.getItemViewType(i);
        return m904b(itemViewType) ? (View) ((Queue) this.f1208h.get(itemViewType)).poll() : null;
    }

    /* renamed from: a */
    private static LayoutParams m894a(View view) {
        LayoutParams layoutParams = view.getLayoutParams();
        return layoutParams == null ? new LayoutParams(-2, -1) : layoutParams;
    }

    /* renamed from: a */
    private void m895a() {
        this.f1216p = -1;
        this.f1217q = -1;
        this.f1207g = 0;
        this.f1203c = 0;
        this.f1204d = 0;
        this.f1215o = Strategy.TTL_SECONDS_INFINITE;
        setCurrentScrollState$6c40596b(C1082k.f1231a);
    }

    /* renamed from: a */
    private void m896a(int i, View view) {
        int itemViewType = this.f1202b.getItemViewType(i);
        if (m904b(itemViewType)) {
            ((Queue) this.f1208h.get(itemViewType)).offer(view);
        }
    }

    /* renamed from: a */
    private void m897a(Canvas canvas, Rect rect) {
        if (this.f1213m != null) {
            this.f1213m.setBounds(rect);
            this.f1213m.draw(canvas);
        }
    }

    /* renamed from: a */
    private void m898a(View view, int i) {
        addViewInLayout(view, i, C1074c.m894a(view), true);
        LayoutParams a = C1074c.m894a(view);
        view.measure(a.width > 0 ? MeasureSpec.makeMeasureSpec(a.width, 1073741824) : MeasureSpec.makeMeasureSpec(0, 0), ViewGroup.getChildMeasureSpec(this.f1226z, getPaddingTop() + getPaddingBottom(), a.height));
    }

    /* renamed from: a */
    private void m899a(Boolean bool) {
        if (this.f1196B != bool.booleanValue()) {
            View view = this;
            while (view.getParent() instanceof View) {
                if ((view.getParent() instanceof ListView) || (view.getParent() instanceof ScrollView)) {
                    view.getParent().requestDisallowInterceptTouchEvent(bool.booleanValue());
                    this.f1196B = bool.booleanValue();
                    return;
                }
                view = (View) view.getParent();
            }
        }
    }

    /* renamed from: b */
    private void m902b() {
        m895a();
        removeAllViewsInLayout();
        requestLayout();
    }

    /* renamed from: b */
    static /* synthetic */ void m903b(C1074c c1074c, int i) {
        if (c1074c.f1224x != null && c1074c.f1225y != null) {
            int i2 = c1074c.f1203c + i;
            if (c1074c.f1201a != null && !c1074c.f1201a.isFinished()) {
                return;
            }
            if (i2 < 0) {
                c1074c.f1224x.onPull(((float) Math.abs(i)) / ((float) c1074c.getRenderWidth()));
                if (!c1074c.f1225y.isFinished()) {
                    c1074c.f1225y.onRelease();
                }
            } else if (i2 > c1074c.f1215o) {
                c1074c.f1225y.onPull(((float) Math.abs(i)) / ((float) c1074c.getRenderWidth()));
                if (!c1074c.f1224x.isFinished()) {
                    c1074c.f1224x.onRelease();
                }
            }
        }
    }

    /* renamed from: b */
    private boolean m904b(int i) {
        return i < this.f1208h.size();
    }

    /* renamed from: c */
    private float m906c() {
        return VERSION.SDK_INT >= 14 ? C1080i.m920a(this.f1201a) : 30.0f;
    }

    /* renamed from: c */
    private boolean m907c(int i) {
        return i == this.f1202b.getCount() + -1;
    }

    /* renamed from: d */
    private void m909d() {
        if (this.f1211k != null) {
            this.f1211k.setPressed(false);
            refreshDrawableState();
            this.f1211k = null;
        }
    }

    /* renamed from: e */
    private void m911e() {
        if (this.f1224x != null) {
            this.f1224x.onRelease();
        }
        if (this.f1225y != null) {
            this.f1225y.onRelease();
        }
    }

    /* renamed from: f */
    private boolean m913f() {
        return (this.f1202b == null || this.f1202b.isEmpty() || this.f1215o <= 0) ? false : true;
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

    private void setCurrentScrollState$6c40596b(int i) {
        this.f1223w = i;
    }

    /* renamed from: a */
    protected final boolean m917a(float f) {
        this.f1201a.fling(this.f1204d, 0, (int) (-f), 0, 0, this.f1215o, 0, 0);
        setCurrentScrollState$6c40596b(C1082k.f1233c);
        requestLayout();
        return true;
    }

    /* renamed from: a */
    protected final boolean m918a(MotionEvent motionEvent) {
        this.f1195A = !this.f1201a.isFinished();
        this.f1201a.forceFinished(true);
        setCurrentScrollState$6c40596b(C1082k.f1231a);
        m909d();
        if (!this.f1195A) {
            int a = m890a((int) motionEvent.getX(), (int) motionEvent.getY());
            if (a >= 0) {
                this.f1211k = getChildAt(a);
                if (this.f1211k != null) {
                    this.f1211k.setPressed(true);
                    refreshDrawableState();
                }
            }
        }
        return true;
    }

    protected final void dispatchDraw(Canvas canvas) {
        super.dispatchDraw(canvas);
        int save;
        int height;
        if (this.f1224x != null && !this.f1224x.isFinished() && m913f()) {
            save = canvas.save();
            height = getHeight();
            canvas.rotate(-90.0f, 0.0f, 0.0f);
            canvas.translate((float) ((-height) + getPaddingBottom()), 0.0f);
            this.f1224x.setSize(getRenderHeight(), getRenderWidth());
            if (this.f1224x.draw(canvas)) {
                invalidate();
            }
            canvas.restoreToCount(save);
        } else if (this.f1225y != null && !this.f1225y.isFinished() && m913f()) {
            save = canvas.save();
            height = getWidth();
            canvas.rotate(90.0f, 0.0f, 0.0f);
            canvas.translate((float) getPaddingTop(), (float) (-height));
            this.f1225y.setSize(getRenderHeight(), getRenderWidth());
            if (this.f1225y.draw(canvas)) {
                invalidate();
            }
            canvas.restoreToCount(save);
        }
    }

    protected final void dispatchSetPressed(boolean z) {
    }

    public final ListAdapter getAdapter() {
        return this.f1202b;
    }

    public final int getFirstVisiblePosition() {
        return this.f1216p;
    }

    public final int getLastVisiblePosition() {
        return this.f1217q;
    }

    protected final float getLeftFadingEdgeStrength() {
        int horizontalFadingEdgeLength = getHorizontalFadingEdgeLength();
        return this.f1203c == 0 ? 0.0f : this.f1203c < horizontalFadingEdgeLength ? ((float) this.f1203c) / ((float) horizontalFadingEdgeLength) : 1.0f;
    }

    protected final float getRightFadingEdgeStrength() {
        int horizontalFadingEdgeLength = getHorizontalFadingEdgeLength();
        return this.f1203c == this.f1215o ? 0.0f : this.f1215o - this.f1203c < horizontalFadingEdgeLength ? ((float) (this.f1215o - this.f1203c)) / ((float) horizontalFadingEdgeLength) : 1.0f;
    }

    public final View getSelectedView() {
        int i = this.f1218r;
        return (i < this.f1216p || i > this.f1217q) ? null : getChildAt(i - this.f1216p);
    }

    protected final void onDraw(Canvas canvas) {
        super.onDraw(canvas);
        int childCount = getChildCount();
        Rect rect = this.f1210j;
        this.f1210j.top = getPaddingTop();
        this.f1210j.bottom = this.f1210j.top + getRenderHeight();
        for (int i = 0; i < childCount; i++) {
            if (i != childCount - 1 || !m907c(this.f1217q)) {
                View childAt = getChildAt(i);
                rect.left = childAt.getRight();
                rect.right = childAt.getRight() + this.f1212l;
                if (rect.left < getPaddingLeft()) {
                    rect.left = getPaddingLeft();
                }
                if (rect.right > getWidth() - getPaddingRight()) {
                    rect.right = getWidth() - getPaddingRight();
                }
                m897a(canvas, rect);
                if (i == 0 && childAt.getLeft() > getPaddingLeft()) {
                    rect.left = getPaddingLeft();
                    rect.right = childAt.getLeft();
                    m897a(canvas, rect);
                }
            }
        }
    }

    @SuppressLint({"WrongCall"})
    protected final void onLayout(boolean z, int i, int i2, int i3, int i4) {
        boolean z2;
        do {
            super.onLayout(z, i, i2, i3, i4);
            if (this.f1202b != null) {
                int i5;
                View rightmostChild;
                View view;
                int i6;
                invalidate();
                if (this.f1209i) {
                    i5 = this.f1203c;
                    m895a();
                    removeAllViewsInLayout();
                    this.f1204d = i5;
                    this.f1209i = false;
                }
                if (this.f1214n != null) {
                    this.f1204d = this.f1214n.intValue();
                    this.f1214n = null;
                }
                if (this.f1201a.computeScrollOffset()) {
                    this.f1204d = this.f1201a.getCurrX();
                }
                if (this.f1204d < 0) {
                    this.f1204d = 0;
                    if (this.f1198D && this.f1224x.isFinished()) {
                        this.f1224x.onAbsorb((int) m906c());
                    }
                    this.f1201a.forceFinished(true);
                    setCurrentScrollState$6c40596b(C1082k.f1231a);
                } else if (this.f1204d > this.f1215o) {
                    this.f1204d = this.f1215o;
                    if (this.f1198D && this.f1225y.isFinished()) {
                        this.f1225y.onAbsorb((int) m906c());
                    }
                    this.f1201a.forceFinished(true);
                    setCurrentScrollState$6c40596b(C1082k.f1231a);
                }
                int i7 = this.f1203c - this.f1204d;
                View leftmostChild = getLeftmostChild();
                while (leftmostChild != null && leftmostChild.getRight() + i7 <= 0) {
                    this.f1207g = (m907c(this.f1216p) ? leftmostChild.getMeasuredWidth() : this.f1212l + leftmostChild.getMeasuredWidth()) + this.f1207g;
                    m896a(this.f1216p, leftmostChild);
                    removeViewInLayout(leftmostChild);
                    this.f1216p++;
                    leftmostChild = getLeftmostChild();
                }
                while (true) {
                    rightmostChild = getRightmostChild();
                    if (rightmostChild == null || rightmostChild.getLeft() + i7 < getWidth()) {
                        rightmostChild = getRightmostChild();
                    } else {
                        m896a(this.f1217q, rightmostChild);
                        removeViewInLayout(rightmostChild);
                        this.f1217q--;
                    }
                }
                rightmostChild = getRightmostChild();
                i5 = rightmostChild != null ? rightmostChild.getRight() : 0;
                while ((i5 + i7) + this.f1212l < getWidth() && this.f1217q + 1 < this.f1202b.getCount()) {
                    this.f1217q++;
                    if (this.f1216p < 0) {
                        this.f1216p = this.f1217q;
                    }
                    view = this.f1202b.getView(this.f1217q, m893a(this.f1217q), this);
                    m898a(view, -1);
                    i5 += (this.f1217q == 0 ? 0 : this.f1212l) + view.getMeasuredWidth();
                    if (!(this.f1219s == null || this.f1202b == null || this.f1202b.getCount() - (this.f1217q + 1) >= this.f1220t || this.f1221u)) {
                        this.f1221u = true;
                    }
                }
                rightmostChild = getLeftmostChild();
                i5 = rightmostChild != null ? rightmostChild.getLeft() : 0;
                while ((i5 + i7) - this.f1212l > 0 && this.f1216p > 0) {
                    this.f1216p--;
                    view = this.f1202b.getView(this.f1216p, m893a(this.f1216p), this);
                    m898a(view, 0);
                    i5 -= this.f1216p == 0 ? view.getMeasuredWidth() : this.f1212l + view.getMeasuredWidth();
                    this.f1207g -= i5 + i7 == 0 ? view.getMeasuredWidth() : this.f1212l + view.getMeasuredWidth();
                }
                int childCount = getChildCount();
                if (this.f1198D) {
                    i5 = i7;
                } else {
                    this.f1207g = 0;
                    i5 = 0;
                }
                if (childCount > 0) {
                    this.f1207g = i5 + this.f1207g;
                    i6 = this.f1207g;
                    for (i5 = 0; i5 < childCount; i5++) {
                        View childAt = getChildAt(i5);
                        int paddingLeft = getPaddingLeft() + i6;
                        int paddingTop = getPaddingTop();
                        childAt.layout(paddingLeft, paddingTop, childAt.getMeasuredWidth() + paddingLeft, childAt.getMeasuredHeight() + paddingTop);
                        i6 += childAt.getMeasuredWidth() + this.f1212l;
                    }
                }
                this.f1203c = this.f1204d;
                if (m907c(this.f1217q)) {
                    rightmostChild = getRightmostChild();
                    if (rightmostChild != null) {
                        i6 = this.f1215o;
                        this.f1215o = ((rightmostChild.getRight() - getPaddingLeft()) + this.f1203c) - getRenderWidth();
                        if (this.f1215o < 0) {
                            this.f1215o = 0;
                        }
                        if (this.f1215o != i6) {
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
        if (!this.f1201a.isFinished()) {
            ViewCompat.postOnAnimation(this, this.f1200F);
        } else if (this.f1223w == C1082k.f1233c) {
            setCurrentScrollState$6c40596b(C1082k.f1231a);
        }
    }

    protected final void onMeasure(int i, int i2) {
        super.onMeasure(i, i2);
        this.f1226z = i2;
    }

    public final void onRestoreInstanceState(Parcelable parcelable) {
        if (parcelable instanceof Bundle) {
            Bundle bundle = (Bundle) parcelable;
            this.f1214n = Integer.valueOf(bundle.getInt("BUNDLE_ID_CURRENT_X"));
            super.onRestoreInstanceState(bundle.getParcelable("BUNDLE_ID_PARENT_STATE"));
        }
    }

    public final Parcelable onSaveInstanceState() {
        Parcelable bundle = new Bundle();
        bundle.putParcelable("BUNDLE_ID_PARENT_STATE", super.onSaveInstanceState());
        bundle.putInt("BUNDLE_ID_CURRENT_X", this.f1203c);
        return bundle;
    }

    public final boolean onTouchEvent(MotionEvent motionEvent) {
        if (motionEvent.getAction() == 1) {
            if (this.f1201a == null || this.f1201a.isFinished()) {
                setCurrentScrollState$6c40596b(C1082k.f1231a);
            }
            m899a(Boolean.valueOf(false));
            m911e();
        } else if (motionEvent.getAction() == 3) {
            m909d();
            m911e();
            m899a(Boolean.valueOf(false));
        }
        return super.onTouchEvent(motionEvent);
    }

    public final void setAdapter(ListAdapter listAdapter) {
        int i = 0;
        if (this.f1202b != null) {
            this.f1202b.unregisterDataSetObserver(this.f1199E);
        }
        if (listAdapter != null) {
            this.f1221u = false;
            this.f1202b = listAdapter;
            this.f1202b.registerDataSetObserver(this.f1199E);
        }
        int viewTypeCount = this.f1202b.getViewTypeCount();
        this.f1208h.clear();
        while (i < viewTypeCount) {
            this.f1208h.add(new LinkedList());
            i++;
        }
        m902b();
    }

    public final void setDivider(Drawable drawable) {
        this.f1213m = drawable;
        if (drawable != null) {
            setDividerWidth(drawable.getIntrinsicWidth());
        } else {
            setDividerWidth(0);
        }
    }

    public final void setDividerWidth(int i) {
        this.f1212l = i;
        requestLayout();
        invalidate();
    }

    public final void setOnClickListener(OnClickListener onClickListener) {
        this.f1197C = onClickListener;
    }

    public final void setOnScrollStateChangedListener(C1081j c1081j) {
        this.f1222v = c1081j;
    }

    public final void setScrollingEnabled(boolean z) {
        this.f1198D = z;
    }

    public final void setSelection(int i) {
        this.f1218r = i;
    }
}
