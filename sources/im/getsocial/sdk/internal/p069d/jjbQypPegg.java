package im.getsocial.sdk.internal.p069d;

import android.app.Application;
import im.getsocial.sdk.GetSocial;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.rFvvVpjzZH;
import im.getsocial.sdk.internal.p089m.zoToeBNOjF;

/* renamed from: im.getsocial.sdk.internal.d.jjbQypPegg */
public class jjbQypPegg {
    /* renamed from: b */
    private static final cjrhisSQCL f1505b = upgqDBbsrL.m1274a(jjbQypPegg.class);
    @XdbacJlTDQ
    /* renamed from: a */
    rFvvVpjzZH f1506a;

    /* renamed from: b */
    private static void m1574b(Application application) {
        try {
            im.getsocial.sdk.internal.p091l.jjbQypPegg.m2092a().mo4720a(application);
        } catch (Exception e) {
            f1505b.mo4387a("UI framework can not be found: " + e.getMessage());
        }
    }

    /* renamed from: a */
    public final void m1575a() {
        Application application;
        f1505b.mo4393c("GetSocial setup didn't happen! Check if you have AutoInitSdkContentProvider declared in your manifest!");
        try {
            application = (Application) Class.forName("android.app.ActivityThread").getMethod("currentApplication", new Class[0]).invoke(null, new Object[0]);
        } catch (Exception e) {
            f1505b.mo4387a("Failed to get application using reflection.");
            application = new Application();
        }
        try {
            m1576a(application);
        } catch (Throwable e2) {
            f1505b.mo4395c(e2);
            f1505b.mo4388a("Failed to make fallback setup, exception: %s", e2);
        }
        if (this.f1506a == null) {
            throw new Exception("Injection failed");
        }
    }

    /* renamed from: a */
    public final void m1576a(Application application) {
        im.getsocial.sdk.internal.p033c.p041b.jjbQypPegg.m1192a(application);
        zoToeBNOjF.m2151a(application);
        ztWNWCuZiM.m1221a((Object) this);
        f1505b.mo4387a("Register activity lifecycle callbacks");
        im.getsocial.sdk.jjbQypPegg.m2411a(application);
        jjbQypPegg.m1574b(application);
        if (this.f1506a.mo4368a("im.getsocial.sdk.AutoInitSdk", true)) {
            GetSocial.init();
        }
    }
}
