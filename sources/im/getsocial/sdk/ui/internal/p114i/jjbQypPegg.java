package im.getsocial.sdk.ui.internal.p114i;

import android.app.AlertDialog.Builder;
import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.view.View;
import android.view.ViewGroup;
import android.view.inputmethod.InputMethodManager;
import android.widget.FrameLayout;
import android.widget.Toast;
import com.facebook.internal.AnalyticsEvents;
import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocial;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.sharedl10n.Localization;
import im.getsocial.sdk.ui.UiActionListener;
import im.getsocial.sdk.ui.internal.p115f.jjbQypPegg.upgqDBbsrL;
import im.getsocial.sdk.ui.internal.views.PlaceholderView;
import java.util.HashMap;
import java.util.Map;
import javax.annotation.OverridingMethodsMustInvokeSuper;

/* renamed from: im.getsocial.sdk.ui.internal.i.jjbQypPegg */
public final class jjbQypPegg {

    /* renamed from: im.getsocial.sdk.ui.internal.i.jjbQypPegg$jjbQypPegg */
    public static class jjbQypPegg<P extends upgqDBbsrL> {
        /* renamed from: a */
        private P f2549a;

        protected jjbQypPegg() {
        }

        /* renamed from: a */
        protected void mo4641a() {
        }

        /* renamed from: a */
        public final void m2520a(P p) {
            this.f2549a = p;
        }

        /* renamed from: j */
        protected final P m2521j() {
            return this.f2549a;
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.i.jjbQypPegg$cjrhisSQCL */
    public static abstract class cjrhisSQCL<P extends upgqDBbsrL> extends upgqDBbsrL<P> {
        /* renamed from: a */
        private View f2554a;
        /* renamed from: d */
        private pdwpUtZXDT f2555d;
        /* renamed from: e */
        private boolean f2556e = false;
        /* renamed from: f */
        private boolean f2557f;

        /* renamed from: im.getsocial.sdk.ui.internal.i.jjbQypPegg$cjrhisSQCL$1 */
        class C11701 implements OnClickListener {
            /* renamed from: a */
            final /* synthetic */ cjrhisSQCL f3024a;

            C11701(cjrhisSQCL cjrhissqcl) {
                this.f3024a = cjrhissqcl;
            }

            public void onClick(DialogInterface dialogInterface, int i) {
                dialogInterface.dismiss();
            }
        }

        @OverridingMethodsMustInvokeSuper
        /* renamed from: c */
        private void mo4663c() {
            this.f2556e = true;
            ((upgqDBbsrL) m2524n()).mo4693p();
        }

        /* renamed from: A */
        public final boolean m2528A() {
            return this.f2556e;
        }

        /* renamed from: a */
        protected final <T extends View> T m2529a(int i, Class<T> cls) {
            return (View) cls.cast(m2536q().findViewById(i));
        }

        /* renamed from: a */
        protected abstract View mo4580a(ViewGroup viewGroup);

        /* renamed from: a */
        protected abstract void mo4581a();

        /* renamed from: a */
        public final void m2532a(pdwpUtZXDT pdwputzxdt) {
            this.f2555d = pdwputzxdt;
        }

        /* renamed from: a */
        public final void m2533a(String str, String str2) {
            new Builder(m2526p()).setTitle(str).setMessage(str2).setNeutralButton(this.b.strings().OkButton, new C11701(this)).show();
        }

        /* renamed from: a */
        public final void m2534a(String str, String str2, String str3) {
            PlaceholderView.m3590a(m2536q(), str, str2, str3, true);
        }

        @OverridingMethodsMustInvokeSuper
        protected void a_() {
            ((upgqDBbsrL) m2524n()).m2571A();
            mo4663c();
        }

        /* renamed from: b */
        public final void m2535b(String str) {
            Toast.makeText(m2526p(), str, 0).show();
        }

        /* renamed from: q */
        public final View m2536q() {
            if (this.f2554a == null) {
                this.f2554a = mo4580a(new FrameLayout(m2526p()));
                mo4581a();
            }
            return this.f2554a;
        }

        @OverridingMethodsMustInvokeSuper
        /* renamed from: r */
        protected final void m2537r() {
            mo4663c();
        }

        @OverridingMethodsMustInvokeSuper
        /* renamed from: s */
        protected final void m2538s() {
            this.f2556e = false;
            ((upgqDBbsrL) m2524n()).mo4694q();
        }

        @OverridingMethodsMustInvokeSuper
        /* renamed from: t */
        protected final void m2539t() {
            m2538s();
        }

        /* renamed from: u */
        public final void m2540u() {
            this.f2557f = true;
            this.f2555d.m3431b();
        }

        /* renamed from: v */
        public final void m2541v() {
            this.f2557f = false;
            this.f2555d.m3432c();
        }

        /* renamed from: w */
        protected final boolean m2542w() {
            return this.f2557f;
        }

        /* renamed from: x */
        public final void m2543x() {
            PlaceholderView.m3592a(m2536q());
        }

        /* renamed from: y */
        public final void m2544y() {
            View findFocus = m2536q().findFocus();
            if (findFocus != null) {
                ((InputMethodManager) m2526p().getSystemService("input_method")).hideSoftInputFromWindow(findFocus.getWindowToken(), 0);
            }
        }

        /* renamed from: z */
        protected final Builder m2545z() {
            return new Builder(m2526p());
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.i.jjbQypPegg$upgqDBbsrL */
    public static abstract class upgqDBbsrL<V extends cjrhisSQCL, M extends jjbQypPegg> extends im.getsocial.sdk.ui.internal.p115f.jjbQypPegg.jjbQypPegg<V> {
        /* renamed from: a */
        private static final cjrhisSQCL f2566a = im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL.m1274a(upgqDBbsrL.class);
        /* renamed from: b */
        protected UiActionListener f2567b;
        @XdbacJlTDQ
        /* renamed from: c */
        protected Localization f2568c;
        @XdbacJlTDQ
        /* renamed from: d */
        im.getsocial.sdk.internal.p033c.p056i.jjbQypPegg f2569d;
        @XdbacJlTDQ
        /* renamed from: e */
        im.getsocial.sdk.internal.p036a.p045h.jjbQypPegg f2570e;
        /* renamed from: f */
        private String f2571f;
        /* renamed from: g */
        private final M f2572g;
        /* renamed from: h */
        private long f2573h = 0;
        /* renamed from: i */
        private boolean f2574i;
        /* renamed from: j */
        private cjrhisSQCL f2575j;
        /* renamed from: k */
        private boolean f2576k;

        public upgqDBbsrL(V v, M m) {
            super(v);
            ztWNWCuZiM.m1221a((Object) this);
            this.f2572g = m;
            this.f2572g.m2520a(this);
        }

        /* renamed from: a */
        private void m2570a(int i, String str, String str2) {
            Map hashMap = new HashMap();
            hashMap.put("error_key", String.valueOf(i));
            hashMap.put("source", str);
            hashMap.put(AnalyticsEvents.PARAMETER_SHARE_ERROR_MESSAGE, str2);
            m2579a("ui_error", hashMap);
        }

        /* renamed from: A */
        final void m2571A() {
            Object obj = null;
            Object obj2 = !this.f2569d.mo4401a() ? 1 : null;
            if (!GetSocial.isInitialized()) {
                obj = 1;
            }
            if (obj2 != null) {
                ((cjrhisSQCL) mo4733t()).m2534a(this.f2568c.strings().ConnectionLostTitle, this.f2568c.strings().ConnectionLostMessage, im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3142z().m3173a());
                m2570a(ErrorCode.NO_INTERNET, mo4592b(), "NO_INTERNET");
            } else if (obj != null) {
                ((cjrhisSQCL) mo4733t()).m2534a(this.f2568c.strings().ErrorAlertMessageTitle, this.f2568c.strings().ErrorAlertMessage, im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3142z().m3173a());
                m2570a(ErrorCode.SDK_NOT_INITIALIZED, mo4592b(), "SDK_NOT_INITIALIZED");
            } else {
                this.f2572g.mo4641a();
                d_();
            }
        }

        /* renamed from: B */
        public final long m2572B() {
            return this.f2573h;
        }

        /* renamed from: C */
        public final boolean m2573C() {
            return this.f2574i;
        }

        /* renamed from: D */
        public final boolean m2574D() {
            return this.f2576k;
        }

        /* renamed from: a */
        protected abstract String mo4591a();

        /* renamed from: a */
        public final void m2576a(long j) {
            this.f2573h = j;
        }

        /* renamed from: a */
        public final void m2577a(UiActionListener uiActionListener) {
            this.f2567b = uiActionListener;
        }

        /* renamed from: a */
        public final void m2578a(cjrhisSQCL cjrhissqcl) {
            this.f2575j = cjrhissqcl;
        }

        /* renamed from: a */
        protected final void m2579a(String str, Map<String, String> map) {
            this.f2570e.m1053a(str, map);
        }

        /* renamed from: a */
        public final void m2580a(Throwable th) {
            f2566a.mo4392b(th);
            ((cjrhisSQCL) mo4733t()).m2541v();
            if (th instanceof GetSocialException) {
                GetSocialException getSocialException = (GetSocialException) th;
                if (getSocialException.getErrorCode() == ErrorCode.NOT_FOUND) {
                    ((cjrhisSQCL) mo4733t()).m2534a(this.f2568c.strings().ErrorAlertMessageTitle, this.f2568c.strings().ActivityNotFound, im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL.m3237a().m3255b().m3212c().m3116E().m3173a());
                    return;
                } else if (getSocialException.getErrorCode() == ErrorCode.USER_IS_BANNED) {
                    ((cjrhisSQCL) mo4733t()).m2533a(this.f2568c.strings().ErrorAlertMessageTitle, this.f2568c.strings().ErrorYoureBanned);
                    return;
                }
            }
            ((cjrhisSQCL) mo4733t()).m2535b(this.f2568c.strings().ErrorAlertMessage);
        }

        /* renamed from: a */
        public final void m2581a(boolean z) {
            this.f2574i = z;
        }

        /* renamed from: b */
        public abstract String mo4592b();

        /* renamed from: b */
        public final void m2583b(boolean z) {
            this.f2576k = z;
        }

        /* renamed from: c */
        public final void m2584c(String str) {
            this.f2571f = str;
        }

        public abstract void d_();

        /* renamed from: p */
        protected void mo4693p() {
        }

        /* renamed from: q */
        protected void mo4694q() {
        }

        /* renamed from: r */
        public void mo4695r() {
        }

        /* renamed from: s */
        public void mo4696s() {
        }

        /* renamed from: u */
        public final UiActionListener m2589u() {
            return this.f2567b;
        }

        /* renamed from: v */
        public final String m2590v() {
            return this.f2571f == null ? mo4591a() : this.f2571f;
        }

        /* renamed from: w */
        protected final void m2591w() {
            this.f2575j.mo4732a();
        }

        /* renamed from: x */
        protected final void m2592x() {
            this.f2575j.m3404b();
        }

        /* renamed from: y */
        protected final M m2593y() {
            return this.f2572g;
        }

        /* renamed from: z */
        public final cjrhisSQCL m2594z() {
            return (cjrhisSQCL) mo4733t();
        }
    }
}
