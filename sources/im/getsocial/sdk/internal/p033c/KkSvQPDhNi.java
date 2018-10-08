package im.getsocial.sdk.internal.p033c;

import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.Bundle;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg;

/* renamed from: im.getsocial.sdk.internal.c.KkSvQPDhNi */
public class KkSvQPDhNi implements rFvvVpjzZH {
    /* renamed from: a */
    private static final cjrhisSQCL f1221a = upgqDBbsrL.m1274a(KkSvQPDhNi.class);
    /* renamed from: b */
    private final Context f1222b;

    @XdbacJlTDQ
    KkSvQPDhNi(Context context) {
        this.f1222b = context;
    }

    /* renamed from: a */
    private Bundle m1086a() {
        try {
            ApplicationInfo applicationInfo = this.f1222b.getPackageManager().getApplicationInfo(this.f1222b.getPackageName(), 128);
            return applicationInfo.metaData == null ? Bundle.EMPTY : applicationInfo.metaData;
        } catch (NameNotFoundException e) {
            f1221a.mo4396d("Failed to load meta-data, NameNotFound: " + e.getMessage());
            return Bundle.EMPTY;
        }
    }

    /* renamed from: a */
    public final int mo4366a(String str, int i) {
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Key could not be null");
        return m1086a().getInt(str, 0);
    }

    /* renamed from: a */
    public final String mo4367a(String str) {
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Key could not be null");
        return m1086a().getString(str);
    }

    /* renamed from: a */
    public final boolean mo4368a(String str, boolean z) {
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Key could not be null");
        return m1086a().getBoolean(str, z);
    }
}
