package im.getsocial.sdk.ui.internal.views;

import android.annotation.SuppressLint;
import android.content.Context;
import android.graphics.Rect;
import android.graphics.drawable.Drawable;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.Animation;
import android.view.animation.Animation.AnimationListener;
import android.view.animation.Interpolator;
import android.view.animation.LinearInterpolator;
import android.view.animation.TranslateAnimation;
import android.widget.AbsListView;
import android.widget.AbsListView.LayoutParams;
import android.widget.AbsListView.OnScrollListener;
import android.widget.AbsListView.RecyclerListener;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.ListAdapter;
import android.widget.ListView;
import im.getsocial.sdk.ui.internal.views.recycling.Recycler;
import im.getsocial.sdk.ui.internal.views.recycling.ViewVisitor;
import java.util.concurrent.TimeUnit;

@SuppressLint({"ViewConstructor"})
public class OverscrollingListView extends FrameLayout {
    /* renamed from: a */
    private static final int f3211a = ((int) TimeUnit.MILLISECONDS.toMillis(384));
    /* renamed from: b */
    private final ViewVisitor f3212b = new Recycler();
    /* renamed from: c */
    private final jjbQypPegg f3213c;
    /* renamed from: d */
    private final LinearLayout f3214d;
    /* renamed from: e */
    private final LinearLayout f3215e;
    /* renamed from: f */
    private TranslateAnimation f3216f;
    /* renamed from: g */
    private TranslateAnimation f3217g;
    /* renamed from: h */
    private final Interpolator f3218h = new LinearInterpolator();
    /* renamed from: i */
    private final Interpolator f3219i = new C12051(this);

    public interface OnOverscrollListener {
        /* renamed from: a */
        int mo4684a(int i, OnOverscrollActionDoneListener onOverscrollActionDoneListener);

        /* renamed from: a */
        void mo4685a();

        /* renamed from: a */
        void mo4686a(int i);
    }

    /* renamed from: im.getsocial.sdk.ui.internal.views.OverscrollingListView$1 */
    class C12051 implements Interpolator {
        /* renamed from: a */
        final /* synthetic */ OverscrollingListView f3181a;

        C12051(OverscrollingListView overscrollingListView) {
            this.f3181a = overscrollingListView;
        }

        public float getInterpolation(float f) {
            return Math.abs(f - 1.0f);
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.views.OverscrollingListView$4 */
    class C12094 implements AnimationListener {
        /* renamed from: a */
        final /* synthetic */ OverscrollingListView f3186a;

        C12094(OverscrollingListView overscrollingListView) {
            this.f3186a = overscrollingListView;
        }

        public void onAnimationEnd(Animation animation) {
            this.f3186a.f3214d.setVisibility(8);
        }

        public void onAnimationRepeat(Animation animation) {
        }

        public void onAnimationStart(Animation animation) {
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.views.OverscrollingListView$5 */
    class C12105 implements AnimationListener {
        /* renamed from: a */
        final /* synthetic */ OverscrollingListView f3187a;

        C12105(OverscrollingListView overscrollingListView) {
            this.f3187a = overscrollingListView;
        }

        public void onAnimationEnd(Animation animation) {
            this.f3187a.f3215e.setVisibility(8);
        }

        public void onAnimationRepeat(Animation animation) {
        }

        public void onAnimationStart(Animation animation) {
        }
    }

    public interface OnOverscrollActionDoneListener {
        /* renamed from: a */
        void mo4746a();
    }

    private class jjbQypPegg extends ListView implements OnScrollListener {
        /* renamed from: a */
        final /* synthetic */ OverscrollingListView f3193a;
        /* renamed from: b */
        private View f3194b;
        /* renamed from: c */
        private View f3195c;
        /* renamed from: d */
        private OnOverscrollListener f3196d;
        /* renamed from: e */
        private OnOverscrollListener f3197e;
        /* renamed from: f */
        private LayoutParams f3198f;
        /* renamed from: g */
        private LayoutParams f3199g;
        /* renamed from: h */
        private boolean f3200h = true;
        /* renamed from: i */
        private boolean f3201i = true;
        /* renamed from: j */
        private float f3202j;
        /* renamed from: k */
        private final Rect f3203k = new Rect();
        /* renamed from: l */
        private int f3204l;
        /* renamed from: m */
        private int f3205m;
        /* renamed from: n */
        private int f3206n;
        /* renamed from: o */
        private boolean f3207o = true;
        /* renamed from: p */
        private float f3208p;
        /* renamed from: q */
        private float f3209q = -1.0f;
        /* renamed from: r */
        private final Runnable f3210r = new C12131(this);

        /* renamed from: im.getsocial.sdk.ui.internal.views.OverscrollingListView$jjbQypPegg$1 */
        class C12131 implements Runnable {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f3190a;

            /* renamed from: im.getsocial.sdk.ui.internal.views.OverscrollingListView$jjbQypPegg$1$1 */
            class C12111 implements OnOverscrollActionDoneListener {
                /* renamed from: a */
                final /* synthetic */ C12131 f3188a;

                C12111(C12131 c12131) {
                    this.f3188a = c12131;
                }

                /* renamed from: a */
                public final void mo4746a() {
                    this.f3188a.f3190a.f3204l = this.f3188a.f3190a.f3200h ? jjbQypPegg.m3562a(this.f3188a.f3190a, this.f3188a.f3190a.f3194b) : 0;
                    if (this.f3188a.f3190a.f3204l != 0) {
                        this.f3188a.f3190a.smoothScrollBy(this.f3188a.f3190a.f3204l, OverscrollingListView.f3211a);
                    }
                }
            }

            /* renamed from: im.getsocial.sdk.ui.internal.views.OverscrollingListView$jjbQypPegg$1$2 */
            class C12122 implements OnOverscrollActionDoneListener {
                /* renamed from: a */
                final /* synthetic */ C12131 f3189a;

                C12122(C12131 c12131) {
                    this.f3189a = c12131;
                }

                /* renamed from: a */
                public final void mo4746a() {
                    this.f3189a.f3190a.f3204l = this.f3189a.f3190a.f3201i ? jjbQypPegg.m3562a(this.f3189a.f3190a, this.f3189a.f3190a.f3195c) : 0;
                    if (this.f3189a.f3190a.f3204l != 0) {
                        this.f3189a.f3190a.smoothScrollBy(-this.f3189a.f3190a.f3204l, OverscrollingListView.f3211a);
                    }
                }
            }

            C12131(jjbQypPegg jjbqyppegg) {
                this.f3190a = jjbqyppegg;
            }

            public void run() {
                if (this.f3190a.f3200h && this.f3190a.f3194b != null) {
                    this.f3190a.f3204l = jjbQypPegg.m3562a(this.f3190a, this.f3190a.f3194b);
                    this.f3190a.smoothScrollBy(this.f3190a.f3204l - this.f3190a.f3196d.mo4684a(this.f3190a.f3204l, new C12111(this)), OverscrollingListView.f3211a);
                } else if (this.f3190a.f3201i && this.f3190a.f3195c != null) {
                    this.f3190a.f3204l = jjbQypPegg.m3562a(this.f3190a, this.f3190a.f3195c);
                    this.f3190a.smoothScrollBy((-this.f3190a.f3204l) + this.f3190a.f3197e.mo4684a(this.f3190a.f3204l, new C12122(this)), OverscrollingListView.f3211a);
                }
            }
        }

        public <T extends View & OnOverscrollListener> jjbQypPegg(final OverscrollingListView overscrollingListView, Context context, T t, T t2) {
            this.f3193a = overscrollingListView;
            super(context);
            setRecyclerListener(new RecyclerListener(this) {
                /* renamed from: b */
                final /* synthetic */ jjbQypPegg f3192b;

                public void onMovedToScrapHeap(View view) {
                    this.f3192b.f3193a.f3212b.mo4747a(view);
                }
            });
            setOverScrollMode(1);
            setVerticalScrollBarEnabled(false);
            setHorizontalScrollBarEnabled(false);
            setHeaderDividersEnabled(false);
            setFooterDividersEnabled(false);
            if (t != null) {
                this.f3194b = t;
                this.f3196d = (OnOverscrollListener) t;
                this.f3198f = new LayoutParams(-1, 1);
                t.setLayoutParams(this.f3198f);
                super.addHeaderView(t);
            }
            if (t2 != null) {
                this.f3195c = t2;
                this.f3197e = (OnOverscrollListener) t2;
                this.f3199g = new LayoutParams(-1, 1);
                t2.setLayoutParams(this.f3199g);
                super.addFooterView(t2);
            }
            super.setOnScrollListener(this);
        }

        /* renamed from: a */
        static /* synthetic */ int m3562a(jjbQypPegg jjbqyppegg, View view) {
            jjbqyppegg.f3193a.getGlobalVisibleRect(jjbqyppegg.f3203k);
            if (view == jjbqyppegg.f3194b) {
                jjbqyppegg.f3205m = jjbqyppegg.f3203k.top + jjbqyppegg.getPaddingTop();
                jjbqyppegg.f3206n = jjbqyppegg.f3194b.getGlobalVisibleRect(jjbqyppegg.f3203k) ? jjbqyppegg.f3203k.bottom : jjbqyppegg.f3205m;
            } else if (view == jjbqyppegg.f3195c) {
                jjbqyppegg.f3206n = jjbqyppegg.f3203k.bottom - jjbqyppegg.getPaddingBottom();
                jjbqyppegg.f3205m = jjbqyppegg.f3195c.getGlobalVisibleRect(jjbqyppegg.f3203k) ? jjbqyppegg.f3203k.top : jjbqyppegg.f3206n;
            }
            return jjbqyppegg.f3206n - jjbqyppegg.f3205m;
        }

        public boolean onInterceptTouchEvent(MotionEvent motionEvent) {
            this.f3202j = motionEvent.getRawY();
            return super.onInterceptTouchEvent(motionEvent);
        }

        public void onScroll(AbsListView absListView, int i, int i2, int i3) {
            boolean z = false;
            this.f3200h = i == 0;
            if (i + i2 == i3) {
                z = true;
            }
            this.f3201i = z;
            if (!(this.f3194b == null || this.f3200h)) {
                this.f3198f.height = 1;
                this.f3194b.setLayoutParams(this.f3198f);
            }
            if (!(this.f3195c == null || this.f3201i)) {
                this.f3199g.height = 1;
                this.f3195c.setLayoutParams(this.f3199g);
            }
            if (this.f3200h) {
                OverscrollingListView.m3580g(this.f3193a);
            }
        }

        public void onScrollStateChanged(AbsListView absListView, int i) {
        }

        @SuppressLint({"ClickableViewAccessibility"})
        public boolean onTouchEvent(MotionEvent motionEvent) {
            boolean z = false;
            float rawY = motionEvent.getRawY();
            if (!(this.f3200h && this.f3201i)) {
                switch (motionEvent.getActionMasked()) {
                    case 0:
                    case 2:
                        float f = rawY - this.f3202j;
                        if (this.f3200h && this.f3194b != null) {
                            if (this.f3209q == -1.0f) {
                                this.f3209q = rawY;
                            }
                            this.f3204l = Math.round(rawY - this.f3209q) + 1;
                            if (f > 0.0f) {
                                this.f3198f.height = Math.max(this.f3204l, this.f3198f.height);
                                this.f3194b.setLayoutParams(this.f3198f);
                            }
                            this.f3196d.mo4686a(this.f3204l);
                        } else if (!this.f3201i || this.f3195c == null) {
                            this.f3204l = 0;
                            this.f3209q = -1.0f;
                        } else {
                            if (this.f3209q == -1.0f) {
                                this.f3209q = rawY;
                            }
                            this.f3204l = Math.round(this.f3209q - rawY) + 1;
                            if (f < 0.0f) {
                                this.f3199g.height = Math.max(this.f3204l, this.f3199g.height);
                                this.f3195c.setLayoutParams(this.f3199g);
                            }
                            this.f3197e.mo4686a(this.f3204l);
                        }
                        if ((f < 0.0f && this.f3207o) || (f > 0.0f && !this.f3207o)) {
                            this.f3208p = rawY;
                        }
                        if (f > 0.0f) {
                            z = true;
                        }
                        this.f3207o = z;
                        if (Math.abs(rawY - this.f3208p) > 32.0f) {
                            this.f3208p = rawY;
                            if (!this.f3207o) {
                                OverscrollingListView.m3581h(this.f3193a);
                                break;
                            }
                            OverscrollingListView.m3580g(this.f3193a);
                            break;
                        }
                        break;
                    case 1:
                    case 3:
                        post(this.f3210r);
                        break;
                }
            }
            this.f3202j = rawY;
            return super.onTouchEvent(motionEvent);
        }

        public void setPadding(int i, int i2, int i3, int i4) {
            super.setPadding(i, i2, i3, i4);
        }
    }

    public <T extends View & OnOverscrollListener> OverscrollingListView(Context context, T t, T t2) {
        super(context);
        ViewGroup.LayoutParams layoutParams = new FrameLayout.LayoutParams(-1, -2);
        layoutParams.gravity = 48;
        this.f3214d = new LinearLayout(this, context) {
            /* renamed from: a */
            final /* synthetic */ OverscrollingListView f3184a;

            protected void onSizeChanged(int i, final int i2, int i3, int i4) {
                post(new Runnable(this) {
                    /* renamed from: b */
                    final /* synthetic */ C12072 f3183b;

                    public void run() {
                        this.f3183b.f3184a.f3213c.setPadding(0, i2, 0, this.f3183b.f3184a.f3213c.getPaddingBottom());
                        this.f3183b.f3184a.f3213c.f3208p = (float) i2;
                    }
                });
                this.f3184a.f3216f = new TranslateAnimation(0.0f, 0.0f, 0.0f, (float) (-i2));
                this.f3184a.f3216f.setDuration(200);
            }
        };
        this.f3214d.setLayoutParams(layoutParams);
        layoutParams = new FrameLayout.LayoutParams(-1, -2);
        layoutParams.gravity = 80;
        this.f3215e = new LinearLayout(this, context) {
            /* renamed from: a */
            final /* synthetic */ OverscrollingListView f3185a;

            protected void onSizeChanged(int i, int i2, int i3, int i4) {
                this.f3185a.f3217g = new TranslateAnimation(0.0f, 0.0f, 0.0f, (float) i2);
                this.f3185a.f3217g.setDuration(200);
            }
        };
        this.f3215e.setLayoutParams(layoutParams);
        layoutParams = new FrameLayout.LayoutParams(-1, -1);
        this.f3213c = new jjbQypPegg(this, context, t, t2);
        this.f3213c.setLayoutParams(layoutParams);
        this.f3213c.setClipToPadding(false);
        addView(this.f3213c);
        addView(this.f3214d);
        addView(this.f3215e);
    }

    /* renamed from: a */
    private static boolean m3572a(Animation animation) {
        return animation.hasStarted() && !animation.hasEnded();
    }

    /* renamed from: g */
    static /* synthetic */ void m3580g(OverscrollingListView overscrollingListView) {
        if (overscrollingListView.f3214d.getVisibility() == 8 && !m3572a(overscrollingListView.f3216f)) {
            overscrollingListView.f3214d.setVisibility(0);
            overscrollingListView.f3216f.reset();
            overscrollingListView.f3216f.setAnimationListener(null);
            overscrollingListView.f3216f.setInterpolator(overscrollingListView.f3219i);
            overscrollingListView.f3214d.startAnimation(overscrollingListView.f3216f);
            overscrollingListView.f3215e.setVisibility(0);
            overscrollingListView.f3217g.reset();
            overscrollingListView.f3217g.setAnimationListener(null);
            overscrollingListView.f3217g.setInterpolator(overscrollingListView.f3219i);
            overscrollingListView.f3215e.startAnimation(overscrollingListView.f3217g);
        }
    }

    /* renamed from: h */
    static /* synthetic */ void m3581h(OverscrollingListView overscrollingListView) {
        if (!overscrollingListView.f3213c.f3200h && overscrollingListView.f3214d.getVisibility() == 0 && !m3572a(overscrollingListView.f3216f)) {
            overscrollingListView.f3216f.setAnimationListener(new C12094(overscrollingListView));
            overscrollingListView.f3216f.reset();
            overscrollingListView.f3216f.setInterpolator(overscrollingListView.f3218h);
            overscrollingListView.f3214d.startAnimation(overscrollingListView.f3216f);
            overscrollingListView.f3217g.setAnimationListener(new C12105(overscrollingListView));
            overscrollingListView.f3217g.reset();
            overscrollingListView.f3217g.setInterpolator(overscrollingListView.f3218h);
            overscrollingListView.f3215e.startAnimation(overscrollingListView.f3217g);
        }
    }

    /* renamed from: a */
    public final void m3582a() {
        if (this.f3213c.f3197e != null) {
            this.f3213c.f3197e.mo4685a();
        }
    }

    /* renamed from: a */
    public final void m3583a(int i) {
        this.f3213c.smoothScrollToPosition(i);
    }

    /* renamed from: a */
    public final void m3584a(Drawable drawable) {
        this.f3213c.setDivider(null);
    }

    /* renamed from: a */
    public final void m3585a(View view) {
        this.f3214d.addView(view);
    }

    /* renamed from: a */
    public final void m3586a(ListAdapter listAdapter) {
        this.f3213c.setAdapter(listAdapter);
    }

    /* renamed from: b */
    public final void m3587b() {
        if (this.f3213c.f3196d != null) {
            this.f3213c.f3196d.mo4685a();
        }
    }

    /* renamed from: b */
    public final void m3588b(View view) {
        this.f3213c.addHeaderView(view, null, false);
    }
}
