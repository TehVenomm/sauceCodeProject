package im.getsocial.sdk.ui.internal;

import android.app.Activity;
import android.app.Application;
import android.content.Context;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.rFvvVpjzZH;
import im.getsocial.sdk.pushnotifications.Notification;
import im.getsocial.sdk.pushnotifications.Notification.Key.OpenActivity;
import im.getsocial.sdk.ui.ViewBuilder;
import im.getsocial.sdk.ui.activities.ActivityDetailsViewBuilder;
import im.getsocial.sdk.ui.activities.ActivityFeedViewBuilder;
import im.getsocial.sdk.ui.internal.p114i.pdwpUtZXDT;
import im.getsocial.sdk.ui.invites.InvitesViewBuilder;
import java.util.Map;

public class jjbQypPegg {
    /* renamed from: e */
    private static final cjrhisSQCL f3062e = upgqDBbsrL.m1274a(jjbQypPegg.class);
    @XdbacJlTDQ
    /* renamed from: a */
    cjrhisSQCL f3063a;
    @XdbacJlTDQ
    /* renamed from: b */
    im.getsocial.sdk.ui.internal.p131d.upgqDBbsrL f3064b;
    @XdbacJlTDQ
    /* renamed from: c */
    im.getsocial.sdk.internal.p033c.p052d.jjbQypPegg f3065c;
    @XdbacJlTDQ
    /* renamed from: d */
    rFvvVpjzZH f3066d;
    /* renamed from: f */
    private im.getsocial.sdk.ui.internal.p114i.upgqDBbsrL.upgqDBbsrL f3067f;
    /* renamed from: g */
    private im.getsocial.sdk.ui.internal.p114i.cjrhisSQCL f3068g;
    /* renamed from: h */
    private boolean f3069h;

    /* renamed from: im.getsocial.sdk.ui.internal.jjbQypPegg$1 */
    class C11821 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ Activity f3049a;
        /* renamed from: b */
        final /* synthetic */ jjbQypPegg f3050b;

        C11821(jjbQypPegg jjbqyppegg, Activity activity) {
            this.f3050b = jjbqyppegg;
            this.f3049a = activity;
        }

        public void run() {
            im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(this.f3049a), "Activity can't be null");
            jjbQypPegg.m3439a(this.f3050b, this.f3049a);
            jjbQypPegg.m3442b(this.f3050b, this.f3049a);
            jjbQypPegg.m3445c(this.f3050b, this.f3049a);
            this.f3050b.f3069h = true;
            this.f3050b.f3068g = jjbQypPegg.m3434a(this.f3049a);
            if (this.f3050b.f3068g != null) {
                this.f3050b.f3067f = this.f3050b.f3068g.m3409g();
                this.f3050b.f3068g.m3407e();
            }
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.jjbQypPegg$6 */
    class C11876 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f3060a;

        C11876(jjbQypPegg jjbqyppegg) {
            this.f3060a = jjbqyppegg;
        }

        public void run() {
            if (this.f3060a.f3068g != null) {
                jjbQypPegg.m3444c(this.f3060a);
                this.f3060a.f3068g.m3405c();
            }
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.jjbQypPegg$7 */
    class C11887 extends im.getsocial.sdk.internal.p089m.jjbQypPegg {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f3061a;

        C11887(jjbQypPegg jjbqyppegg) {
            this.f3061a = jjbqyppegg;
        }

        public void onActivityPaused(Activity activity) {
            jjbQypPegg.m3446d(this.f3061a, activity);
        }

        public void onActivityResumed(Activity activity) {
            jjbQypPegg jjbqyppegg = this.f3061a;
            jjbqyppegg.f3065c.m1244a(new C11821(jjbqyppegg, activity));
        }
    }

    public jjbQypPegg() {
        ztWNWCuZiM.m1221a((Object) this);
        if (this.f3065c == null) {
            try {
                new im.getsocial.sdk.internal.p069d.jjbQypPegg().m1575a();
            } catch (Exception e) {
                this.f3065c = new im.getsocial.sdk.internal.p033c.p052d.upgqDBbsrL();
            }
        }
    }

    /* renamed from: a */
    public static ActivityFeedViewBuilder m3433a(String str) {
        return ActivityFeedViewBuilder.create(str);
    }

    /* renamed from: a */
    static /* synthetic */ im.getsocial.sdk.ui.internal.p114i.cjrhisSQCL m3434a(Activity activity) {
        im.getsocial.sdk.ui.internal.p114i.cjrhisSQCL b = im.getsocial.sdk.ui.internal.p135g.jjbQypPegg.m3292b(activity);
        im.getsocial.sdk.ui.internal.p135g.jjbQypPegg.m3290a(activity);
        return b;
    }

    /* renamed from: a */
    public static InvitesViewBuilder m3438a() {
        return new InvitesViewBuilder();
    }

    /* renamed from: a */
    static /* synthetic */ void m3439a(jjbQypPegg jjbqyppegg, Activity activity) {
        if (jjbqyppegg.f3063a != null && jjbqyppegg.f3063a.m3099a() != activity) {
            jjbqyppegg.f3067f = null;
        }
    }

    /* renamed from: b */
    public static ActivityDetailsViewBuilder m3441b(String str) {
        return ActivityDetailsViewBuilder.create(str);
    }

    /* renamed from: b */
    static /* synthetic */ void m3442b(jjbQypPegg jjbqyppegg, Activity activity) {
        if (jjbqyppegg.f3063a.m3099a() != activity) {
            jjbqyppegg.f3063a.m3100a(activity);
        }
    }

    /* renamed from: c */
    static /* synthetic */ void m3444c(jjbQypPegg jjbqyppegg) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(jjbqyppegg.f3063a), "Can't ensure window if activity is null");
        if (jjbqyppegg.f3067f == null) {
            jjbqyppegg.f3067f = new pdwpUtZXDT();
            jjbqyppegg.f3068g = new im.getsocial.sdk.ui.internal.p114i.cjrhisSQCL(jjbqyppegg.f3067f);
        }
    }

    /* renamed from: c */
    static /* synthetic */ void m3445c(jjbQypPegg jjbqyppegg, Activity activity) {
        if (!jjbqyppegg.f3064b.m3259c()) {
            String a = jjbqyppegg.f3066d.mo4367a("im.getsocial.sdk.UiConfigurationFile");
            if ((!im.getsocial.sdk.internal.p033c.p066m.ztWNWCuZiM.m1521a(a) ? 1 : null) == null) {
                jjbqyppegg.m3449a((Context) activity);
            } else if (!jjbqyppegg.m3450a((Context) activity, a)) {
                jjbqyppegg.m3449a((Context) activity);
            }
        }
    }

    /* renamed from: d */
    static /* synthetic */ void m3446d(jjbQypPegg jjbqyppegg, Activity activity) {
        jjbqyppegg.f3069h = false;
        if (jjbqyppegg.f3068g != null) {
            im.getsocial.sdk.ui.internal.p135g.jjbQypPegg.m3291a(activity, jjbqyppegg.f3068g);
            jjbqyppegg.f3068g.m3406d();
        }
    }

    /* renamed from: a */
    public final void m3447a(Application application) {
        f3062e.mo4387a("Register activity lifecycle callbacks");
        application.registerActivityLifecycleCallbacks(new C11887(this));
    }

    /* renamed from: a */
    public final void m3448a(final boolean z) {
        this.f3065c.m1244a(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f3059b;

            public void run() {
                if (this.f3059b.f3068g != null) {
                    this.f3059b.f3068g.mo4731a(z);
                }
            }
        });
    }

    /* renamed from: a */
    public final boolean m3449a(final Context context) {
        return this.f3065c.m1244a(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f3052b;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(context), "Can't load default UI configuration with null context");
                this.f3052b.f3064b.m3250a(context);
            }
        });
    }

    /* renamed from: a */
    public final boolean m3450a(final Context context, final String str) {
        return this.f3065c.m1244a(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ jjbQypPegg f3055c;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1516b(str), "Can't load configuration from null or empty path");
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(context), "Can't load UI configuration with null context");
                this.f3055c.f3064b.m3251a(context, str);
            }
        });
    }

    /* renamed from: a */
    public final boolean m3451a(Notification notification, boolean z) {
        if (!z) {
            return false;
        }
        switch (notification.getActionType()) {
            case 2:
                if (this.f3068g != null) {
                    this.f3068g.m3408f();
                }
                Map actionData = notification.getActionData();
                String str = (String) actionData.get(OpenActivity.FEED_NAME);
                if (str == null) {
                    ActivityDetailsViewBuilder.create((String) actionData.get(OpenActivity.ACTIVITY_ID)).setCommentId((String) actionData.get(OpenActivity.COMMENT_ID)).show();
                } else {
                    ActivityFeedViewBuilder.create(str).show();
                }
                return true;
            case 3:
                if (this.f3068g != null) {
                    this.f3068g.m3408f();
                }
                new InvitesViewBuilder().show();
                return true;
            default:
                return false;
        }
    }

    /* renamed from: a */
    public final boolean m3452a(final ViewBuilder viewBuilder) {
        return this.f3065c.m1244a(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f3057b;

            public void run() {
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(this.f3057b.f3069h, "Can't open view before calling onResume");
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(this.f3057b.f3063a), "Can't open view with null activity");
                im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(this.f3057b.f3064b.m3259c(), "Can't open view before default UI configuration is loaded");
                this.f3057b.f3064b.m3249a(this.f3057b.f3063a.m3099a());
                jjbQypPegg.m3444c(this.f3057b);
                this.f3057b.f3068g.m3401a(im.getsocial.sdk.ui.upgqDBbsrL.m3625a(viewBuilder), im.getsocial.sdk.ui.upgqDBbsrL.m3627b(viewBuilder));
            }
        });
    }

    /* renamed from: b */
    public final void m3453b() {
        this.f3065c.m1244a(new C11876(this));
    }

    /* renamed from: c */
    public final boolean m3454c() {
        return this.f3068g != null && this.f3068g.mo4732a();
    }
}
