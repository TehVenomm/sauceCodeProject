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
public final class C1390c extends AdapterView {
    /* renamed from: A */
    private boolean f3583A = false;
    /* renamed from: B */
    private boolean f3584B = false;
    /* renamed from: C */
    private OnClickListener f3585C;
    /* renamed from: D */
    private boolean f3586D = true;
    /* renamed from: E */
    private DataSetObserver f3587E = new C1392e(this);
    /* renamed from: F */
    private Runnable f3588F = new C1393f(this);
    /* renamed from: a */
    protected Scroller f3589a = new Scroller(getContext());
    /* renamed from: b */
    protected ListAdapter f3590b;
    /* renamed from: c */
    protected int f3591c;
    /* renamed from: d */
    protected int f3592d;
    /* renamed from: e */
    private final C1394g f3593e = new C1394g();
    /* renamed from: f */
    private GestureDetector f3594f;
    /* renamed from: g */
    private int f3595g;
    /* renamed from: h */
    private List f3596h = new ArrayList();
    /* renamed from: i */
    private boolean f3597i = false;
    /* renamed from: j */
    private Rect f3598j = new Rect();
    /* renamed from: k */
    private View f3599k = null;
    /* renamed from: l */
    private int f3600l = 0;
    /* renamed from: m */
    private Drawable f3601m = null;
    /* renamed from: n */
    private Integer f3602n = null;
    /* renamed from: o */
    private int f3603o = Strategy.TTL_SECONDS_INFINITE;
    /* renamed from: p */
    private int f3604p;
    /* renamed from: q */
    private int f3605q;
    /* renamed from: r */
    private int f3606r;
    /* renamed from: s */
    private C1399l f3607s = null;
    /* renamed from: t */
    private int f3608t = 0;
    /* renamed from: u */
    private boolean f3609u = false;
    /* renamed from: v */
    private C1397j f3610v = null;
    /* renamed from: w */
    private int f3611w = C1398k.f3619a;
    /* renamed from: x */
    private EdgeEffectCompat f3612x;
    /* renamed from: y */
    private EdgeEffectCompat f3613y;
    /* renamed from: z */
    private int f3614z;

    public C1390c(Context context) {
        super(context, null);
        this.f3612x = new EdgeEffectCompat(context);
        this.f3613y = new EdgeEffectCompat(context);
        this.f3594f = new GestureDetector(context, this.f3593e);
        setOnTouchListener(new C1391d(this));
        m3920a();
        setWillNotDraw(false);
        if (VERSION.SDK_INT >= 11) {
            C1395h.m3944a(this.f3589a);
        }
    }

    /* renamed from: a */
    private int m3915a(int i, int i2) {
        int childCount = getChildCount();
        for (int i3 = 0; i3 < childCount; i3++) {
            getChildAt(i3).getHitRect(this.f3598j);
            if (this.f3598j.contains(i, i2)) {
                return i3;
            }
        }
        return -1;
    }

    /* renamed from: a */
    private View m3918a(int i) {
        int itemViewType = this.f3590b.getItemViewType(i);
        return m3929b(itemViewType) ? (View) ((Queue) this.f3596h.get(itemViewType)).poll() : null;
    }

    /* renamed from: a */
    private static LayoutParams m3919a(View view) {
        LayoutParams layoutParams = view.getLayoutParams();
        return layoutParams == null ? new LayoutParams(-2, -1) : layoutParams;
    }

    /* renamed from: a */
    private void m3920a() {
        this.f3604p = -1;
        this.f3605q = -1;
        this.f3595g = 0;
        this.f3591c = 0;
        this.f3592d = 0;
        this.f3603o = Strategy.TTL_SECONDS_INFINITE;
        setCurrentScrollState$6c40596b(C1398k.f3619a);
    }

    /* renamed from: a */
    private void m3921a(int i, View view) {
        int itemViewType = this.f3590b.getItemViewType(i);
        if (m3929b(itemViewType)) {
            ((Queue) this.f3596h.get(itemViewType)).offer(view);
        }
    }

    /* renamed from: a */
    private void m3922a(Canvas canvas, Rect rect) {
        if (this.f3601m != null) {
            this.f3601m.setBounds(rect);
            this.f3601m.draw(canvas);
        }
    }

    /* renamed from: a */
    private void m3923a(View view, int i) {
        addViewInLayout(view, i, C1390c.m3919a(view), true);
        LayoutParams a = C1390c.m3919a(view);
        view.measure(a.width > 0 ? MeasureSpec.makeMeasureSpec(a.width, 1073741824) : MeasureSpec.makeMeasureSpec(0, 0), ViewGroup.getChildMeasureSpec(this.f3614z, getPaddingTop() + getPaddingBottom(), a.height));
    }

    /* renamed from: a */
    private void m3924a(Boolean bool) {
        if (this.f3584B != bool.booleanValue()) {
            View view = this;
            while (view.getParent() instanceof View) {
                if ((view.getParent() instanceof ListView) || (view.getParent() instanceof ScrollView)) {
                    view.getParent().requestDisallowInterceptTouchEvent(bool.booleanValue());
                    this.f3584B = bool.booleanValue();
                    return;
                }
                view = (View) view.getParent();
            }
        }
    }

    /* renamed from: b */
    private void m3927b() {
        m3920a();
        removeAllViewsInLayout();
        requestLayout();
    }

    /* renamed from: b */
    static /* synthetic */ void m3928b(C1390c c1390c, int i) {
        if (c1390c.f3612x != null && c1390c.f3613y != null) {
            int i2 = c1390c.f3591c + i;
            if (c1390c.f3589a != null && !c1390c.f3589a.isFinished()) {
                return;
            }
            if (i2 < 0) {
                c1390c.f3612x.onPull(((float) Math.abs(i)) / ((float) c1390c.getRenderWidth()));
                if (!c1390c.f3613y.isFinished()) {
                    c1390c.f3613y.onRelease();
                }
            } else if (i2 > c1390c.f3603o) {
                c1390c.f3613y.onPull(((float) Math.abs(i)) / ((float) c1390c.getRenderWidth()));
                if (!c1390c.f3612x.isFinished()) {
                    c1390c.f3612x.onRelease();
                }
            }
        }
    }

    /* renamed from: b */
    private boolean m3929b(int i) {
        return i < this.f3596h.size();
    }

    /* renamed from: c */
    private float m3931c() {
        return VERSION.SDK_INT >= 14 ? C1396i.m3945a(this.f3589a) : 30.0f;
    }

    /* renamed from: c */
    private boolean m3932c(int i) {
        return i == this.f3590b.getCount() + -1;
    }

    /* renamed from: d */
    private void m3934d() {
        if (this.f3599k != null) {
            this.f3599k.setPressed(false);
            refreshDrawableState();
            this.f3599k = null;
        }
    }

    /* renamed from: e */
    private void m3936e() {
        if (this.f3612x != null) {
            this.f3612x.onRelease();
        }
        if (this.f3613y != null) {
            this.f3613y.onRelease();
        }
    }

    /* renamed from: f */
    private boolean m3938f() {
        return (this.f3590b == null || this.f3590b.isEmpty() || this.f3603o <= 0) ? false : true;
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
        this.f3611w = i;
    }

    /* renamed from: a */
    protected final boolean m3942a(float f) {
        this.f3589a.fling(this.f3592d, 0, (int) (-f), 0, 0, this.f3603o, 0, 0);
        setCurrentScrollState$6c40596b(C1398k.f3621c);
        requestLayout();
        return true;
    }

    /* renamed from: a */
    protected final boolean m3943a(MotionEvent motionEvent) {
        this.f3583A = !this.f3589a.isFinished();
        this.f3589a.forceFinished(true);
        setCurrentScrollState$6c40596b(C1398k.f3619a);
        m3934d();
        if (!this.f3583A) {
            int a = m3915a((int) motionEvent.getX(), (int) motionEvent.getY());
            if (a >= 0) {
                this.f3599k = getChildAt(a);
                if (this.f3599k != null) {
                    this.f3599k.setPressed(true);
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
        if (this.f3612x != null && !this.f3612x.isFinished() && m3938f()) {
            save = canvas.save();
            height = getHeight();
            canvas.rotate(-90.0f, 0.0f, 0.0f);
            canvas.translate((float) ((-height) + getPaddingBottom()), 0.0f);
            this.f3612x.setSize(getRenderHeight(), getRenderWidth());
            if (this.f3612x.draw(canvas)) {
                invalidate();
            }
            canvas.restoreToCount(save);
        } else if (this.f3613y != null && !this.f3613y.isFinished() && m3938f()) {
            save = canvas.save();
            height = getWidth();
            canvas.rotate(90.0f, 0.0f, 0.0f);
            canvas.translate((float) getPaddingTop(), (float) (-height));
            this.f3613y.setSize(getRenderHeight(), getRenderWidth());
            if (this.f3613y.draw(canvas)) {
                invalidate();
            }
            canvas.restoreToCount(save);
        }
    }

    protected final void dispatchSetPressed(boolean z) {
    }

    public final ListAdapter getAdapter() {
        return this.f3590b;
    }

    public final int getFirstVisiblePosition() {
        return this.f3604p;
    }

    public final int getLastVisiblePosition() {
        return this.f3605q;
    }

    protected final float getLeftFadingEdgeStrength() {
        int horizontalFadingEdgeLength = getHorizontalFadingEdgeLength();
        return this.f3591c == 0 ? 0.0f : this.f3591c < horizontalFadingEdgeLength ? ((float) this.f3591c) / ((float) horizontalFadingEdgeLength) : 1.0f;
    }

    protected final float getRightFadingEdgeStrength() {
        int horizontalFadingEdgeLength = getHorizontalFadingEdgeLength();
        return this.f3591c == this.f3603o ? 0.0f : this.f3603o - this.f3591c < horizontalFadingEdgeLength ? ((float) (this.f3603o - this.f3591c)) / ((float) horizontalFadingEdgeLength) : 1.0f;
    }

    public final View getSelectedView() {
        int i = this.f3606r;
        return (i < this.f3604p || i > this.f3605q) ? null : getChildAt(i - this.f3604p);
    }

    protected final void onDraw(Canvas canvas) {
        super.onDraw(canvas);
        int childCount = getChildCount();
        Rect rect = this.f3598j;
        this.f3598j.top = getPaddingTop();
        this.f3598j.bottom = this.f3598j.top + getRenderHeight();
        for (int i = 0; i < childCount; i++) {
            if (i != childCount - 1 || !m3932c(this.f3605q)) {
                View childAt = getChildAt(i);
                rect.left = childAt.getRight();
                rect.right = childAt.getRight() + this.f3600l;
                if (rect.left < getPaddingLeft()) {
                    rect.left = getPaddingLeft();
                }
                if (rect.right > getWidth() - getPaddingRight()) {
                    rect.right = getWidth() - getPaddingRight();
                }
                m3922a(canvas, rect);
                if (i == 0 && childAt.getLeft() > getPaddingLeft()) {
                    rect.left = getPaddingLeft();
                    rect.right = childAt.getLeft();
                    m3922a(canvas, rect);
                }
            }
        }
    }

    @SuppressLint({"WrongCall"})
    protected final void onLayout(boolean z, int i, int i2, int i3, int i4) {
        boolean z2;
        do {
            super.onLayout(z, i, i2, i3, i4);
            if (this.f3590b != null) {
                int i5;
                View rightmostChild;
                View view;
                int i6;
                invalidate();
                if (this.f3597i) {
                    i5 = this.f3591c;
                    m3920a();
                    removeAllViewsInLayout();
                    this.f3592d = i5;
                    this.f3597i = false;
                }
                if (this.f3602n != null) {
                    this.f3592d = this.f3602n.intValue();
                    this.f3602n = null;
                }
                if (this.f3589a.computeScrollOffset()) {
                    this.f3592d = this.f3589a.getCurrX();
                }
                if (this.f3592d < 0) {
                    this.f3592d = 0;
                    if (this.f3586D && this.f3612x.isFinished()) {
                        this.f3612x.onAbsorb((int) m3931c());
                    }
                    this.f3589a.forceFinished(true);
                    setCurrentScrollState$6c40596b(C1398k.f3619a);
                } else if (this.f3592d > this.f3603o) {
                    this.f3592d = this.f3603o;
                    if (this.f3586D && this.f3613y.isFinished()) {
                        this.f3613y.onAbsorb((int) m3931c());
                    }
                    this.f3589a.forceFinished(true);
                    setCurrentScrollState$6c40596b(C1398k.f3619a);
                }
                int i7 = this.f3591c - this.f3592d;
                View leftmostChild = getLeftmostChild();
                while (leftmostChild != null && leftmostChild.getRight() + i7 <= 0) {
                    this.f3595g = (m3932c(this.f3604p) ? leftmostChild.getMeasuredWidth() : this.f3600l + leftmostChild.getMeasuredWidth()) + this.f3595g;
                    m3921a(this.f3604p, leftmostChild);
                    removeViewInLayout(leftmostChild);
                    this.f3604p++;
                    leftmostChild = getLeftmostChild();
                }
                while (true) {
                    rightmostChild = getRightmostChild();
                    if (rightmostChild == null || rightmostChild.getLeft() + i7 < getWidth()) {
                        rightmostChild = getRightmostChild();
                    } else {
                        m3921a(this.f3605q, rightmostChild);
                        removeViewInLayout(rightmostChild);
                        this.f3605q--;
                    }
                }
                rightmostChild = getRightmostChild();
                i5 = rightmostChild != null ? rightmostChild.getRight() : 0;
                while ((i5 + i7) + this.f3600l < getWidth() && this.f3605q + 1 < this.f3590b.getCount()) {
                    this.f3605q++;
                    if (this.f3604p < 0) {
                        this.f3604p = this.f3605q;
                    }
                    view = this.f3590b.getView(this.f3605q, m3918a(this.f3605q), this);
                    m3923a(view, -1);
                    i5 += (this.f3605q == 0 ? 0 : this.f3600l) + view.getMeasuredWidth();
                    if (!(this.f3607s == null || this.f3590b == null || this.f3590b.getCount() - (this.f3605q + 1) >= this.f3608t || this.f3609u)) {
                        this.f3609u = true;
                    }
                }
                rightmostChild = getLeftmostChild();
                i5 = rightmostChild != null ? rightmostChild.getLeft() : 0;
                while ((i5 + i7) - this.f3600l > 0 && this.f3604p > 0) {
                    this.f3604p--;
                    view = this.f3590b.getView(this.f3604p, m3918a(this.f3604p), this);
                    m3923a(view, 0);
                    i5 -= this.f3604p == 0 ? view.getMeasuredWidth() : this.f3600l + view.getMeasuredWidth();
                    this.f3595g -= i5 + i7 == 0 ? view.getMeasuredWidth() : this.f3600l + view.getMeasuredWidth();
                }
                int childCount = getChildCount();
                if (this.f3586D) {
                    i5 = i7;
                } else {
                    this.f3595g = 0;
                    i5 = 0;
                }
                if (childCount > 0) {
                    this.f3595g = i5 + this.f3595g;
                    i6 = this.f3595g;
                    for (i5 = 0; i5 < childCount; i5++) {
                        View childAt = getChildAt(i5);
                        int paddingLeft = getPaddingLeft() + i6;
                        int paddingTop = getPaddingTop();
                        childAt.layout(paddingLeft, paddingTop, childAt.getMeasuredWidth() + paddingLeft, childAt.getMeasuredHeight() + paddingTop);
                        i6 += childAt.getMeasuredWidth() + this.f3600l;
                    }
                }
                this.f3591c = this.f3592d;
                if (m3932c(this.f3605q)) {
                    rightmostChild = getRightmostChild();
                    if (rightmostChild != null) {
                        i6 = this.f3603o;
                        this.f3603o = ((rightmostChild.getRight() - getPaddingLeft()) + this.f3591c) - getRenderWidth();
                        if (this.f3603o < 0) {
                            this.f3603o = 0;
                        }
                        if (this.f3603o != i6) {
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
        if (!this.f3589a.isFinished()) {
            ViewCompat.postOnAnimation(this, this.f3588F);
        } else if (this.f3611w == C1398k.f3621c) {
            setCurrentScrollState$6c40596b(C1398k.f3619a);
        }
    }

    protected final void onMeasure(int i, int i2) {
        super.onMeasure(i, i2);
        this.f3614z = i2;
    }

    public final void onRestoreInstanceState(Parcelable parcelable) {
        if (parcelable instanceof Bundle) {
            Bundle bundle = (Bundle) parcelable;
            this.f3602n = Integer.valueOf(bundle.getInt("BUNDLE_ID_CURRENT_X"));
            super.onRestoreInstanceState(bundle.getParcelable("BUNDLE_ID_PARENT_STATE"));
        }
    }

    public final Parcelable onSaveInstanceState() {
        Parcelable bundle = new Bundle();
        bundle.putParcelable("BUNDLE_ID_PARENT_STATE", super.onSaveInstanceState());
        bundle.putInt("BUNDLE_ID_CURRENT_X", this.f3591c);
        return bundle;
    }

    public final boolean onTouchEvent(MotionEvent motionEvent) {
        if (motionEvent.getAction() == 1) {
            if (this.f3589a == null || this.f3589a.isFinished()) {
                setCurrentScrollState$6c40596b(C1398k.f3619a);
            }
            m3924a(Boolean.valueOf(false));
            m3936e();
        } else if (motionEvent.getAction() == 3) {
            m3934d();
            m3936e();
            m3924a(Boolean.valueOf(false));
        }
        return super.onTouchEvent(motionEvent);
    }

    public final void setAdapter(ListAdapter listAdapter) {
        int i = 0;
        if (this.f3590b != null) {
            this.f3590b.unregisterDataSetObserver(this.f3587E);
        }
        if (listAdapter != null) {
            this.f3609u = false;
            this.f3590b = listAdapter;
            this.f3590b.registerDataSetObserver(this.f3587E);
        }
        int viewTypeCount = this.f3590b.getViewTypeCount();
        this.f3596h.clear();
        while (i < viewTypeCount) {
            this.f3596h.add(new LinkedList());
            i++;
        }
        m3927b();
    }

    public final void setDivider(Drawable drawable) {
        this.f3601m = drawable;
        if (drawable != null) {
            setDividerWidth(drawable.getIntrinsicWidth());
        } else {
            setDividerWidth(0);
        }
    }

    public final void setDividerWidth(int i) {
        this.f3600l = i;
        requestLayout();
        invalidate();
    }

    public final void setOnClickListener(OnClickListener onClickListener) {
        this.f3585C = onClickListener;
    }

    public final void setOnScrollStateChangedListener(C1397j c1397j) {
        this.f3610v = c1397j;
    }

    public final void setScrollingEnabled(boolean z) {
        this.f3586D = z;
    }

    public final void setSelection(int i) {
        this.f3606r = i;
    }
}
