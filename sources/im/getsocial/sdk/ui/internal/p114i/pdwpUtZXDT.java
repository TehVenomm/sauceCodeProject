package im.getsocial.sdk.ui.internal.p114i;

import android.animation.Animator;
import android.animation.Animator.AnimatorListener;
import android.app.Activity;
import android.app.Dialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.DialogInterface.OnDismissListener;
import android.graphics.drawable.ColorDrawable;
import android.graphics.drawable.Drawable;
import android.os.Build.VERSION;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnSystemUiVisibilityChangeListener;
import android.view.ViewGroup.LayoutParams;
import android.view.Window;
import android.view.WindowManager;
import android.view.animation.Animation;
import android.view.animation.Animation.AnimationListener;
import android.view.inputmethod.InputMethodManager;
import android.widget.RelativeLayout;
import android.widget.TextView;
import im.getsocial.sdk.ui.C1067R;
import im.getsocial.sdk.ui.internal.p114i.jjbQypPegg.cjrhisSQCL;
import im.getsocial.sdk.ui.internal.p114i.upgqDBbsrL.jjbQypPegg;
import im.getsocial.sdk.ui.internal.p114i.upgqDBbsrL.upgqDBbsrL;
import im.getsocial.sdk.ui.internal.p125h.KluUZYuxme;
import im.getsocial.sdk.ui.internal.p125h.jMsobIMeui;
import im.getsocial.sdk.ui.internal.p131d.p132a.QhisXzMgay;
import im.getsocial.sdk.ui.internal.p131d.p132a.rWfbqYooCV;
import im.getsocial.sdk.ui.internal.p131d.p132a.ztWNWCuZiM;
import im.getsocial.sdk.ui.internal.views.AssetButton;
import im.getsocial.sdk.ui.internal.views.LoadingIndicator;

/* renamed from: im.getsocial.sdk.ui.internal.i.pdwpUtZXDT */
public class pdwpUtZXDT extends upgqDBbsrL {
    /* renamed from: a */
    cjrhisSQCL f3038a;
    /* renamed from: d */
    AssetButton f3039d;
    /* renamed from: e */
    RelativeLayout f3040e;
    /* renamed from: f */
    private final Object f3041f = new Object();
    /* renamed from: g */
    private Dialog f3042g;
    /* renamed from: h */
    private TextView f3043h;
    /* renamed from: i */
    private jjbQypPegg f3044i;
    /* renamed from: j */
    private View f3045j;
    /* renamed from: k */
    private View f3046k;
    /* renamed from: l */
    private View f3047l;
    /* renamed from: m */
    private boolean f3048m;

    /* renamed from: im.getsocial.sdk.ui.internal.i.pdwpUtZXDT$1 */
    class C11721 implements AnimationListener {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f3026a;

        /* renamed from: im.getsocial.sdk.ui.internal.i.pdwpUtZXDT$1$1 */
        class C11711 implements Runnable {
            /* renamed from: a */
            final /* synthetic */ C11721 f3025a;

            C11711(C11721 c11721) {
                this.f3025a = c11721;
            }

            public void run() {
                if (this.f3025a.f3026a.f3042g != null) {
                    this.f3025a.f3026a.f3040e.removeAllViews();
                    this.f3025a.f3026a.f3042g.dismiss();
                    this.f3025a.f3026a.f3042g = null;
                    if (this.f3025a.f3026a.f3045j.getBackground() != null) {
                        im.getsocial.sdk.ui.internal.views.jjbQypPegg.m3596a(this.f3025a.f3026a.f3045j, null);
                    }
                    this.f3025a.f3026a.f3045j = null;
                }
            }
        }

        C11721(pdwpUtZXDT pdwputzxdt) {
            this.f3026a = pdwputzxdt;
        }

        public void onAnimationEnd(Animation animation) {
            this.f3026a.m2523a(new C11711(this));
        }

        public void onAnimationRepeat(Animation animation) {
        }

        public void onAnimationStart(Animation animation) {
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.i.pdwpUtZXDT$2 */
    class C11732 implements OnSystemUiVisibilityChangeListener {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f3027a;

        C11732(pdwpUtZXDT pdwputzxdt) {
            this.f3027a = pdwputzxdt;
        }

        public void onSystemUiVisibilityChange(int i) {
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.i.pdwpUtZXDT$3 */
    class C11743 implements OnDismissListener {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f3028a;

        C11743(pdwpUtZXDT pdwputzxdt) {
            this.f3028a = pdwputzxdt;
        }

        public void onDismiss(DialogInterface dialogInterface) {
            if (!this.f3028a.f3048m) {
                this.f3028a.f3044i.mo4731a(false);
            }
            this.f3028a.f3048m = false;
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.i.pdwpUtZXDT$7 */
    class C11807 implements OnClickListener {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f3036a;

        C11807(pdwpUtZXDT pdwputzxdt) {
            this.f3036a = pdwputzxdt;
        }

        public void onClick(View view) {
            view.setEnabled(false);
            this.f3036a.f3044i.mo4731a(false);
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.i.pdwpUtZXDT$8 */
    class C11818 implements OnClickListener {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f3037a;

        C11818(pdwpUtZXDT pdwputzxdt) {
            this.f3037a = pdwputzxdt;
        }

        public void onClick(View view) {
            this.f3037a.f3044i.mo4732a();
        }
    }

    /* renamed from: a */
    private void m3418a(cjrhisSQCL cjrhissqcl) {
        this.f3038a = cjrhissqcl;
        this.f3038a.m2532a(this);
        this.f3047l = cjrhissqcl.m2536q();
        if (this.f3047l.getParent() == null) {
            this.f3040e.addView(this.f3047l, new LayoutParams(-1, -1));
        }
        this.f3047l.setVisibility(0);
    }

    /* renamed from: a */
    public final void mo4734a() {
        synchronized (this.f3041f) {
            this.f3038a = null;
            if (!(this.f3042g == null || this.f3048m)) {
                this.f3048m = true;
                AnimationListener c11721 = new C11721(this);
                im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL a = im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a();
                jMsobIMeui jmsobimeui = new jMsobIMeui(a.m3246a(a.m3255b().m3212c().m3117a().m3161a()), a.m3246a(a.m3255b().m3212c().m3117a().m3162b()));
                jmsobimeui.m3335a(a.m3255b().m3212c().m3117a().m3166f(), 333);
                jmsobimeui.m3334a(this.f3046k, this.f3045j, c11721);
            }
        }
    }

    /* renamed from: a */
    public final void mo4735a(cjrhisSQCL cjrhissqcl, upgqDBbsrL.jjbQypPegg jjbqyppegg) {
        Object obj = (this.f3042g == null || !this.f3042g.isShowing()) ? null : 1;
        final View view;
        if (obj != null) {
            LoadingIndicator.m3518b(this.f3046k);
            view = this.f3047l;
            m3418a(cjrhissqcl);
            if (jjbqyppegg == upgqDBbsrL.jjbQypPegg.INCOMING) {
                im.getsocial.sdk.ui.internal.p125h.pdwpUtZXDT.m3362a(view, this.f3047l, new AnimatorListener(this) {
                    /* renamed from: b */
                    final /* synthetic */ pdwpUtZXDT f3031b;

                    /* renamed from: im.getsocial.sdk.ui.internal.i.pdwpUtZXDT$4$1 */
                    class C11751 implements Runnable {
                        /* renamed from: a */
                        final /* synthetic */ C11764 f3029a;

                        C11751(C11764 c11764) {
                            this.f3029a = c11764;
                        }

                        public void run() {
                            view.setVisibility(8);
                        }
                    }

                    public void onAnimationCancel(Animator animator) {
                    }

                    public void onAnimationEnd(Animator animator) {
                        this.f3031b.m2523a(new C11751(this));
                    }

                    public void onAnimationRepeat(Animator animator) {
                    }

                    public void onAnimationStart(Animator animator) {
                    }
                });
            } else if (jjbqyppegg == upgqDBbsrL.jjbQypPegg.OUTGOING) {
                im.getsocial.sdk.ui.internal.p125h.pdwpUtZXDT.m3363b(this.f3047l, view, new AnimatorListener(this) {
                    /* renamed from: b */
                    final /* synthetic */ pdwpUtZXDT f3034b;

                    /* renamed from: im.getsocial.sdk.ui.internal.i.pdwpUtZXDT$5$1 */
                    class C11771 implements Runnable {
                        /* renamed from: a */
                        final /* synthetic */ C11785 f3032a;

                        C11771(C11785 c11785) {
                            this.f3032a = c11785;
                        }

                        public void run() {
                            this.f3032a.f3034b.f3040e.removeView(view);
                        }
                    }

                    public void onAnimationCancel(Animator animator) {
                    }

                    public void onAnimationEnd(Animator animator) {
                        this.f3034b.m2523a(new C11771(this));
                    }

                    public void onAnimationRepeat(Animator animator) {
                    }

                    public void onAnimationStart(Animator animator) {
                    }
                });
            } else if (jjbqyppegg == upgqDBbsrL.jjbQypPegg.NO_ANIMATION) {
                this.f3040e.removeView(view);
            }
        } else {
            Context a = m2525o().m3099a();
            synchronized (this.f3041f) {
                if (this.f3042g != null) {
                    this.f3042g.dismiss();
                    this.f3042g = null;
                }
                this.f3042g = new Dialog(this, a) {
                    /* renamed from: a */
                    final /* synthetic */ pdwpUtZXDT f3035a;

                    public void onBackPressed() {
                        this.f3035a.f3044i.mo4732a();
                    }
                };
                this.f3046k = LayoutInflater.from(a).inflate(C1067R.layout.window_frame, null);
                this.f3045j = this.f3046k.findViewById(C1067R.id.layoutWindowFrame);
                this.f3040e = (RelativeLayout) this.f3046k.findViewById(C1067R.id.layoutContent);
                this.f3039d = (AssetButton) this.f3046k.findViewById(C1067R.id.buttonBack);
                this.f3043h = (TextView) this.f3046k.findViewById(C1067R.id.titleViewWindowTitle);
                this.f3043h.setGravity(17);
                this.f3046k.setBackgroundColor(im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3117a().m3165e().m3215a());
                View findViewById = this.f3046k.findViewById(C1067R.id.layoutHeader);
                view = (AssetButton) this.f3046k.findViewById(C1067R.id.buttonClose);
                View view2 = this.f3045j;
                im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL a2 = im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a();
                QhisXzMgay a3 = a2.m3255b().m3212c().m3117a();
                int a4 = a2.m3244a(a2.m3255b().m3210a().m3195c().m3174a() - a3.m3162b().m3174a()) / 2;
                Drawable colorDrawable = a3.m3163c() == null ? new ColorDrawable(a3.m3164d().m3215a()) : a2.m3254b(a, a3.m3163c());
                int a5 = a2.m3246a(a3.m3161a());
                int a6 = a2.m3246a(a3.m3162b());
                im.getsocial.sdk.ui.internal.views.jjbQypPegg.m3596a(view2, colorDrawable);
                im.getsocial.sdk.ui.internal.views.jjbQypPegg.m3595a(view2, 0, a4, 0, a4);
                im.getsocial.sdk.ui.internal.views.jjbQypPegg.m3594a(view2, a5, a6);
                RelativeLayout relativeLayout = this.f3040e;
                im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL a7 = im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a();
                relativeLayout.setPadding(a7.m3246a(a7.m3255b().m3212c().m3122f().mo4722a()), a7.m3246a(a7.m3255b().m3212c().m3122f().mo4724c()), a7.m3246a(a7.m3255b().m3212c().m3122f().mo4723b()), a7.m3246a(a7.m3255b().m3212c().m3122f().mo4725d()));
                im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL a8 = im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a();
                rWfbqYooCV b = a8.m3255b().m3212c().m3118b();
                im.getsocial.sdk.ui.internal.views.jjbQypPegg.m3594a(findViewById, -1, a8.m3246a(b.m3205a()));
                findViewById.setBackgroundColor(b.m3206b().m3215a());
                View view3 = this.f3039d;
                im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL a9 = im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a();
                ztWNWCuZiM d = a9.m3255b().m3212c().m3120d();
                im.getsocial.sdk.ui.internal.views.jjbQypPegg.m3594a(view3, a9.m3246a(d.m3186h()), a9.m3246a(d.m3187i()));
                view3.m3505a(d.m3188j(), d.m3190l());
                im.getsocial.sdk.ui.internal.views.jjbQypPegg.m3597a(view3, d);
                view3.setOnClickListener(new C11818(this));
                a8 = im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a();
                ztWNWCuZiM e = a8.m3255b().m3212c().m3121e();
                im.getsocial.sdk.ui.internal.views.jjbQypPegg.m3594a(view, a8.m3246a(e.m3186h()), a8.m3246a(e.m3187i()));
                view.m3505a(e.m3188j(), e.m3190l());
                im.getsocial.sdk.ui.internal.views.jjbQypPegg.m3597a(view, e);
                view.setEnabled(true);
                view.setOnClickListener(new C11807(this));
                TextView textView = this.f3043h;
                KluUZYuxme.m3299a(m2526p()).m3310a(textView, im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3119c().m3209a());
                a8 = im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a();
                rWfbqYooCV b2 = a8.m3255b().m3212c().m3118b();
                im.getsocial.sdk.ui.internal.views.jjbQypPegg.m3595a(textView, 0, a8.m3246a(b2.m3207c()), 0, a8.m3246a(b2.m3208d()));
                view3 = this.f3046k;
                this.f3042g.requestWindowFeature(1);
                this.f3042g.setContentView(view3);
                Window window = this.f3042g.getWindow();
                if (window != null) {
                    window.setSoftInputMode(16);
                    window.setType(1000);
                    window.clearFlags(1026);
                    window.setWindowAnimations(16973824);
                    window.setBackgroundDrawableResource(17170445);
                    window.setLayout(-1, -1);
                    if (((a.getWindow().getAttributes().flags & 1024) == 1024 ? 1 : null) != null) {
                        view3.setFitsSystemWindows(false);
                        window.addFlags(1024);
                        if (VERSION.SDK_INT >= 24) {
                            im.getsocial.sdk.ui.internal.p128a.jjbQypPegg.m3092a(window);
                        }
                    }
                }
            }
            m3418a(cjrhissqcl);
            Activity a10 = m2525o().m3099a();
            this.f3042g.getWindow().setFlags(8, 8);
            if (VERSION.SDK_INT >= 16) {
                this.f3042g.getWindow().getDecorView().setSystemUiVisibility(a10.getWindow().getDecorView().getWindowSystemUiVisibility());
                a10.getWindow().getDecorView().setOnSystemUiVisibilityChangeListener(new C11732(this));
            }
            this.f3042g.show();
            this.f3042g.setOnDismissListener(new C11743(this));
            this.f3042g.getWindow().clearFlags(8);
            ((WindowManager) a10.getSystemService("window")).updateViewLayout(a10.getWindow().getDecorView(), a10.getWindow().getAttributes());
            im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL a11 = im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a();
            jMsobIMeui jmsobimeui = new jMsobIMeui(a11.m3246a(a11.m3255b().m3212c().m3117a().m3161a()), a11.m3246a(a11.m3255b().m3212c().m3117a().m3162b()));
            jmsobimeui.m3335a(a11.m3255b().m3212c().m3117a().m3166f(), 333);
            jmsobimeui.m3333a(this.f3046k, this.f3045j);
        }
        if (this.f3045j != null) {
            ((InputMethodManager) m2526p().getSystemService("input_method")).hideSoftInputFromWindow(this.f3045j.getWindowToken(), 0);
        }
    }

    /* renamed from: a */
    public final void mo4737a(String str) {
        this.f3043h.setText(str);
    }

    /* renamed from: a */
    public final void mo4738a(boolean z) {
        this.f3039d.setVisibility(z ? 0 : 4);
    }

    /* renamed from: b */
    public final void m3431b() {
        LoadingIndicator.m3515a(this.f3046k);
    }

    /* renamed from: c */
    public final void m3432c() {
        LoadingIndicator.m3518b(this.f3046k);
    }
}
