package im.getsocial.sdk.ui.internal.p125h;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Bitmap.Config;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.GradientDrawable;
import android.os.Build.VERSION;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.view.ViewGroup.MarginLayoutParams;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.TextView;
import im.getsocial.sdk.internal.p030e.zoToeBNOjF;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.sharedl10n.Localization;
import im.getsocial.sdk.sharedl10n.LocalizationAdapter;
import im.getsocial.sdk.ui.activities.p116a.p123g.cjrhisSQCL;
import im.getsocial.sdk.ui.internal.p131d.p132a.EmkjBpiUfq;
import im.getsocial.sdk.ui.internal.p131d.p132a.KSZKMmRWhZ;
import im.getsocial.sdk.ui.internal.p131d.p132a.KkSvQPDhNi;
import im.getsocial.sdk.ui.internal.p131d.p132a.bpiSwUyLit;
import im.getsocial.sdk.ui.internal.p131d.p132a.iFpupLCESp;
import im.getsocial.sdk.ui.internal.p131d.p132a.jMsobIMeui;
import im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL;
import im.getsocial.sdk.ui.internal.views.AssetButton;
import im.getsocial.sdk.ui.internal.views.MultiTextView;
import im.getsocial.sdk.ui.internal.views.RoundedCornerMaskView;
import im.getsocial.sdk.ui.internal.views.jjbQypPegg;

/* renamed from: im.getsocial.sdk.ui.internal.h.KluUZYuxme */
public final class KluUZYuxme {
    /* renamed from: c */
    private static volatile Drawable f2966c;
    /* renamed from: d */
    private static volatile Drawable f2967d;
    /* renamed from: e */
    private static volatile Drawable f2968e;
    /* renamed from: f */
    private static volatile Drawable f2969f;
    @XdbacJlTDQ
    /* renamed from: a */
    upgqDBbsrL f2970a;
    @XdbacJlTDQ
    /* renamed from: b */
    Localization f2971b;
    /* renamed from: g */
    private final Context f2972g;

    private KluUZYuxme(Context context) {
        ztWNWCuZiM.m1221a((Object) this);
        this.f2972g = context;
    }

    /* renamed from: a */
    public static KluUZYuxme m3299a(Context context) {
        return new KluUZYuxme(context);
    }

    /* renamed from: a */
    public static void m3300a() {
        f2966c = null;
        f2969f = null;
        f2967d = null;
        f2968e = null;
    }

    /* renamed from: a */
    private void m3301a(View view, int i) {
        LayoutParams layoutParams = view.getLayoutParams();
        if (layoutParams == null) {
            layoutParams = new LayoutParams(-1, -1);
        }
        layoutParams.width = this.f2970a.m3253b((float) i);
        view.setLayoutParams(layoutParams);
    }

    /* renamed from: a */
    private void m3302a(TextView textView, im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme kluUZYuxme) {
        textView.setTextColor(kluUZYuxme.m3156c().m3215a());
        textView.setTextSize(this.f2970a.m3256c((float) kluUZYuxme.m3157d()));
        textView.setTypeface(fOrCGNYyfk.m3331b(this.f2972g, kluUZYuxme));
    }

    /* renamed from: b */
    private void m3303b(View view, int i) {
        LayoutParams layoutParams = view.getLayoutParams();
        if (layoutParams == null) {
            layoutParams = new LayoutParams(-1, -1);
        }
        layoutParams.height = this.f2970a.m3253b((float) i);
        view.setLayoutParams(layoutParams);
    }

    /* renamed from: a */
    public final void m3304a(int i, MultiTextView multiTextView) {
        multiTextView.m3554a();
        im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme kluUZYuxme = (im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme) this.f2970a.m3255b().m3213d().get(this.f2970a.m3255b().m3212c().m3122f().m3204e());
        multiTextView.m3556a(String.valueOf(i), (im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme) this.f2970a.m3255b().m3213d().get(this.f2970a.m3255b().m3212c().m3126j().m3209a()));
        multiTextView.m3556a(" ", kluUZYuxme);
        multiTextView.m3556a(LocalizationAdapter.comments(this.f2971b, i), kluUZYuxme);
    }

    /* renamed from: a */
    public final void m3305a(View view) {
        m3303b(view, 56);
    }

    /* renamed from: a */
    public final void m3306a(View view, boolean z) {
        KkSvQPDhNi u = this.f2970a.m3255b().m3212c().m3137u();
        view.setBackgroundColor(u.m3152a().m3215a());
        FrameLayout.LayoutParams layoutParams = (FrameLayout.LayoutParams) view.getLayoutParams();
        layoutParams.height = this.f2970a.m3244a(u.m3153b().m3174a());
        int i = z ? 56 : 0;
        if (VERSION.SDK_INT >= 17) {
            layoutParams.setMarginStart(this.f2970a.m3253b((float) i));
        }
        layoutParams.setMargins(this.f2970a.m3253b((float) i), this.f2970a.m3253b(4.0f), 0, 0);
        view.setLayoutParams(layoutParams);
        view.requestLayout();
    }

    /* renamed from: a */
    public final void m3307a(ViewGroup viewGroup) {
        viewGroup.setBackgroundColor(this.f2970a.m3255b().m3212c().m3115D().m3172e().m3215a());
    }

    /* renamed from: a */
    public final void m3308a(ImageView imageView) {
        bpiSwUyLit D = this.f2970a.m3255b().m3212c().m3115D();
        if (f2968e == null) {
            synchronized (KluUZYuxme.class) {
                try {
                    if (f2968e == null) {
                        f2968e = new BitmapDrawable(this.f2972g.getApplicationContext().getResources(), this.f2970a.m3247a(this.f2972g, D.m3171d(), Bitmap.createBitmap(1, 1, Config.ALPHA_8)));
                    }
                } catch (Throwable th) {
                    while (true) {
                        Class cls = KluUZYuxme.class;
                    }
                }
            }
        }
        LayoutParams layoutParams = imageView.getLayoutParams();
        layoutParams.width = this.f2970a.m3253b(65.0f);
        layoutParams.height = this.f2970a.m3253b(65.0f);
        imageView.setLayoutParams(layoutParams);
        imageView.setImageDrawable(f2968e);
    }

    /* renamed from: a */
    public final void m3309a(TextView textView) {
        m3310a(textView, this.f2970a.m3255b().m3212c().m3123g().m3209a());
    }

    /* renamed from: a */
    public final void m3310a(TextView textView, jMsobIMeui jmsobimeui) {
        m3302a(textView, this.f2970a.m3248a(jmsobimeui));
    }

    /* renamed from: a */
    public final void m3311a(cjrhisSQCL cjrhissqcl, boolean z) {
        KSZKMmRWhZ p = this.f2970a.m3255b().m3212c().m3132p();
        Drawable gradientDrawable = new GradientDrawable();
        gradientDrawable.setStroke(this.f2970a.m3244a(p.m3149e()), p.m3150f().m3215a());
        gradientDrawable.setCornerRadius((float) this.f2970a.m3244a(p.m3151g()));
        gradientDrawable.setColor(p.m3147c().m3215a());
        jjbQypPegg.m3596a((View) cjrhissqcl, gradientDrawable);
        FrameLayout.LayoutParams layoutParams = (FrameLayout.LayoutParams) cjrhissqcl.getLayoutParams();
        int b = this.f2970a.m3253b(60.0f) + (this.f2970a.m3253b(8.0f) * 2);
        int b2 = this.f2970a.m3253b(8.0f);
        layoutParams.rightMargin = b;
        layoutParams.leftMargin = b2;
        if (VERSION.SDK_INT >= 17) {
            layoutParams.setMarginEnd(b);
            layoutParams.setMarginStart(b2);
        }
        if (z) {
            layoutParams.topMargin = (this.f2970a.m3253b(40.0f) + this.f2970a.m3253b(8.0f)) - (this.f2970a.m3253b((float) p.m3151g()) * 2);
            layoutParams.gravity = 48;
        } else {
            layoutParams.bottomMargin = (this.f2970a.m3253b(40.0f) + this.f2970a.m3253b(8.0f)) - (this.f2970a.m3253b((float) p.m3151g()) * 2);
            layoutParams.gravity = 80;
        }
        layoutParams.height = -2;
        cjrhissqcl.setLayoutParams(layoutParams);
        cjrhissqcl.m3068a((this.f2970a.m3253b(40.0f) * 5) + this.f2970a.m3253b(8.0f));
    }

    /* renamed from: a */
    public final void m3312a(AssetButton assetButton) {
        m3303b((View) assetButton, 35);
        m3313a(assetButton, this.f2970a.m3255b().m3212c().m3128l());
    }

    /* renamed from: a */
    public final void m3313a(AssetButton assetButton, iFpupLCESp ifpuplcesp) {
        assetButton.m3504a(ifpuplcesp.m3188j(), ifpuplcesp.m3189k(), ifpuplcesp.m3190l(), ifpuplcesp.m3191m());
        assetButton.m3502a((im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme) this.f2970a.m3255b().m3213d().get(ifpuplcesp.m3183e()));
        assetButton.m3501a(this.f2970a.m3244a(ifpuplcesp.m3184f().m3174a()), this.f2970a.m3244a(ifpuplcesp.m3185g().m3174a()));
    }

    /* renamed from: a */
    public final void m3314a(RoundedCornerMaskView roundedCornerMaskView) {
        bpiSwUyLit D = this.f2970a.m3255b().m3212c().m3115D();
        if (D.m3169b().m3174a() == 0) {
            roundedCornerMaskView.m3593a(0.01f);
        } else {
            roundedCornerMaskView.m3593a((float) this.f2970a.m3244a(D.m3169b().m3174a()));
        }
        roundedCornerMaskView.setBackgroundColor(this.f2970a.m3255b().m3212c().m3117a().m3164d().m3215a());
    }

    /* renamed from: a */
    public final void m3315a(String str, int i, int i2, ImageView imageView, boolean z) {
        imageView.setBackgroundColor(this.f2970a.m3255b().m3212c().m3117a().m3164d().m3215a());
        bpiSwUyLit D = this.f2970a.m3255b().m3212c().m3115D();
        if (f2967d == null) {
            synchronized (KluUZYuxme.class) {
                try {
                    if (f2967d == null) {
                        f2967d = new BitmapDrawable(this.f2972g.getApplicationContext().getResources(), this.f2970a.m3247a(this.f2972g, D.m3170c(), Bitmap.createBitmap(1, 1, Config.ALPHA_8)));
                    }
                } catch (Throwable th) {
                    while (true) {
                        Class cls = KluUZYuxme.class;
                    }
                }
            }
        }
        Drawable drawable = null;
        if (z) {
            drawable = f2967d;
        }
        im.getsocial.sdk.internal.p072g.jjbQypPegg.m1911a(str, i, i2).m1933a(imageView, drawable);
    }

    /* renamed from: a */
    public final void m3316a(String str, ImageView imageView) {
        m3317a(str, imageView, 36);
    }

    /* renamed from: a */
    public final void m3317a(String str, ImageView imageView, int i) {
        EmkjBpiUfq s = this.f2970a.m3255b().m3212c().m3135s();
        if (f2966c == null) {
            synchronized (KluUZYuxme.class) {
                try {
                    if (f2966c == null) {
                        f2966c = new BitmapDrawable(this.f2972g.getApplicationContext().getResources(), Bitmap.createScaledBitmap(this.f2970a.m3247a(this.f2972g, s.m3104a(), Bitmap.createBitmap(36, 36, Config.ALPHA_8)), this.f2970a.m3253b(36.0f), this.f2970a.m3253b(36.0f), true));
                    }
                } catch (Throwable th) {
                    while (true) {
                        Class cls = KluUZYuxme.class;
                    }
                }
            }
        }
        m3301a((View) imageView, i);
        m3303b((View) imageView, i);
        if (str == null || str.isEmpty()) {
            im.getsocial.sdk.internal.p072g.jjbQypPegg.m1914a(imageView);
            imageView.setImageDrawable(f2966c);
            return;
        }
        im.getsocial.sdk.internal.p072g.jjbQypPegg.m1910a(str).m1931a(this.f2970a.m3253b((float) i), this.f2970a.m3253b((float) i)).m1932a(new HptYHntaqF(this.f2970a.m3244a(s.m3107d().m3174a()), this.f2970a.m3244a(s.m3105b().m3174a()), s.m3106c().m3215a())).m1933a(imageView, f2966c);
    }

    /* renamed from: a */
    public final void m3318a(boolean z, TextView textView) {
        if (f2969f == null) {
            synchronized (KluUZYuxme.class) {
                try {
                    if (f2969f == null) {
                        String a = this.f2970a.m3255b().m3212c().m3127k().m3173a();
                        int b = this.f2970a.m3253b(14.0f);
                        Drawable bitmapDrawable = new BitmapDrawable(this.f2972g.getApplicationContext().getResources(), Bitmap.createScaledBitmap(this.f2970a.m3247a(this.f2972g, a, Bitmap.createBitmap(b, b, Config.ALPHA_8)), b, b, true));
                        bitmapDrawable.setBounds(0, 0, b, b);
                        f2969f = bitmapDrawable;
                    }
                } catch (Throwable th) {
                    while (true) {
                        Class cls = KluUZYuxme.class;
                    }
                }
            }
        }
        textView.setCompoundDrawablePadding(this.f2970a.m3253b(5.0f));
        textView.setCompoundDrawables(null, null, z ? f2969f : null, null);
    }

    /* renamed from: a */
    public final void m3319a(boolean z, AssetButton assetButton) {
        iFpupLCESp i = this.f2970a.m3255b().m3212c().m3125i();
        m3301a((View) assetButton, 20);
        m3303b((View) assetButton, 20);
        if (z) {
            assetButton.m3505a(i.m3188j(), i.m3190l());
            assetButton.setVisibility(0);
            return;
        }
        assetButton.setVisibility(8);
    }

    /* renamed from: b */
    public final zoToeBNOjF<Integer, Integer> m3320b() {
        int a = this.f2970a.m3255b().m3210a().m3194b().m3174a();
        int a2 = this.f2970a.m3255b().m3212c().m3122f().mo4722a().m3174a();
        int a3 = this.f2970a.m3255b().m3212c().m3122f().mo4723b().m3174a();
        double a4 = this.f2970a.m3255b().m3212c().m3115D().m3168a().m3214a();
        a = this.f2970a.m3253b((float) (((a - 36) - a2) - a3));
        return zoToeBNOjF.m1676a(Integer.valueOf(a), Integer.valueOf((int) (((double) a) / a4)));
    }

    /* renamed from: b */
    public final void m3321b(int i, MultiTextView multiTextView) {
        multiTextView.m3554a();
        im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme kluUZYuxme = (im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme) this.f2970a.m3255b().m3213d().get(this.f2970a.m3255b().m3212c().m3122f().m3204e());
        multiTextView.m3556a(String.valueOf(i), (im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme) this.f2970a.m3255b().m3213d().get(this.f2970a.m3255b().m3212c().m3126j().m3209a()));
        multiTextView.m3556a(" ", kluUZYuxme);
        multiTextView.m3556a(LocalizationAdapter.likes(this.f2971b, i), kluUZYuxme);
    }

    /* renamed from: b */
    public final void m3322b(TextView textView) {
        m3310a(textView, this.f2970a.m3255b().m3212c().m3124h().m3209a());
    }

    /* renamed from: b */
    public final void m3323b(AssetButton assetButton) {
        iFpupLCESp x = this.f2970a.m3255b().m3212c().m3140x();
        m3301a((View) assetButton, 60);
        m3303b((View) assetButton, 40);
        assetButton.m3505a(x.m3188j(), x.m3190l());
        int b = this.f2970a.m3253b(8.0f);
        MarginLayoutParams marginLayoutParams = (MarginLayoutParams) assetButton.getLayoutParams();
        marginLayoutParams.setMargins(b, b, b, b);
        if (VERSION.SDK_INT >= 17) {
            marginLayoutParams.setMarginEnd(b);
            marginLayoutParams.setMarginStart(b);
        }
    }

    /* renamed from: b */
    public final void m3324b(boolean z, AssetButton assetButton) {
        iFpupLCESp t = this.f2970a.m3255b().m3212c().m3136t();
        m3303b((View) assetButton, 20);
        m3301a((View) assetButton, 20);
        if (z) {
            assetButton.m3505a(t.m3192n(), t.m3188j());
        } else {
            assetButton.m3505a(t.m3188j(), t.m3192n());
        }
        assetButton.invalidate();
    }

    /* renamed from: c */
    public final void m3325c(TextView textView) {
        m3310a(textView, this.f2970a.m3255b().m3212c().m3122f().m3204e());
    }

    /* renamed from: d */
    public final void m3326d(TextView textView) {
        KSZKMmRWhZ p = this.f2970a.m3255b().m3212c().m3132p();
        textView.setGravity(16);
        m3303b((View) textView, 40);
        Drawable gradientDrawable = new GradientDrawable();
        gradientDrawable.setStroke(this.f2970a.m3244a(p.m3149e()), p.m3150f().m3215a());
        gradientDrawable.setCornerRadius((float) this.f2970a.m3244a(p.m3151g()));
        gradientDrawable.setColor(p.m3147c().m3215a());
        jjbQypPegg.m3596a((View) textView, gradientDrawable);
        textView.setHintTextColor(p.m3148d().m3215a());
        m3302a(textView, this.f2970a.m3248a(p.m3146b()));
        int b = this.f2970a.m3253b(8.0f);
        MarginLayoutParams marginLayoutParams = (MarginLayoutParams) textView.getLayoutParams();
        marginLayoutParams.setMargins(b, b, 0, b);
        if (VERSION.SDK_INT >= 17) {
            marginLayoutParams.setMarginEnd(0);
            marginLayoutParams.setMarginStart(b);
        }
        if (textView instanceof EditText) {
            if (!this.f2970a.m3261d()) {
                textView.setInputType(524289);
            }
            textView.setPadding(b, 0, 0, 0);
            textView.setImeOptions(268435460);
        }
    }
}
