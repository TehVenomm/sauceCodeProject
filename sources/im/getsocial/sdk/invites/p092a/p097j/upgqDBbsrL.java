package im.getsocial.sdk.invites.p092a.p097j;

import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.pm.ProviderInfo;
import android.content.pm.ResolveInfo;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.invites.InstallReferrerReceiver;
import im.getsocial.sdk.invites.MultipleInstallReferrerReceiver;
import java.util.ArrayList;
import java.util.List;

/* renamed from: im.getsocial.sdk.invites.a.j.upgqDBbsrL */
public final class upgqDBbsrL implements jjbQypPegg {
    /* renamed from: b */
    private static final cjrhisSQCL f2433b = im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL.m1274a(upgqDBbsrL.class);
    /* renamed from: c */
    private final Context f2434c;

    @XdbacJlTDQ
    public upgqDBbsrL(Context context) {
        this.f2434c = context;
    }

    /* renamed from: a */
    public static ProviderInfo m2391a(Context context) {
        try {
            ProviderInfo[] providerInfoArr = context.getPackageManager().getPackageInfo(context.getPackageName(), 8).providers;
            if (providerInfoArr != null) {
                for (ProviderInfo providerInfo : providerInfoArr) {
                    if (a.equals(providerInfo.name)) {
                        return providerInfo;
                    }
                }
            }
        } catch (NameNotFoundException e) {
            f2433b.mo4388a("Failed to check if %s is declared in the AndroidManifest, error: %s", a, e.getMessage());
        }
        return null;
    }

    /* renamed from: a */
    public final boolean mo4573a() {
        return upgqDBbsrL.m2391a(this.f2434c) != null;
    }

    /* renamed from: b */
    public final int mo4574b() {
        List arrayList = new ArrayList();
        for (ResolveInfo resolveInfo : this.f2434c.getPackageManager().queryBroadcastReceivers(new Intent("com.android.vending.INSTALL_REFERRER"), 0)) {
            if (resolveInfo.activityInfo.packageName.equals(this.f2434c.getPackageName())) {
                arrayList.add(resolveInfo.activityInfo.name);
            }
        }
        int i = arrayList.size() == 1 ? 1 : 0;
        int i2 = arrayList.size() > 1 ? 1 : 0;
        boolean contains = arrayList.contains(InstallReferrerReceiver.class.getName());
        boolean contains2 = arrayList.contains(MultipleInstallReferrerReceiver.class.getName());
        int i3 = (arrayList.indexOf(InstallReferrerReceiver.class.getName()) == 0 || arrayList.indexOf(MultipleInstallReferrerReceiver.class.getName()) == 0) ? 1 : 0;
        if (i != 0) {
            if (contains || contains2) {
                f2433b.mo4390b("Correct configuration of INSTALL_REFERRER receivers.");
                return 1;
            }
            f2433b.mo4391b("GetSocial Analytics may not work. Declare %s in AndroidManifest.xml or call %s.onIntentReceived(...) from %s", MultipleInstallReferrerReceiver.class.getName(), InstallReferrerReceiver.class.getName(), arrayList.get(0));
            return 0;
        } else if (i2 == 0) {
            f2433b.mo4394c("GetSocial Analytics will not be correct. %s is not declared in AndroidManifest.xml.", MultipleInstallReferrerReceiver.class.getName());
            return -1;
        } else if (contains2) {
            if (contains) {
                f2433b.mo4394c("Only %s has to be defined. Remove %s.", MultipleInstallReferrerReceiver.class.getName(), InstallReferrerReceiver.class.getName());
                return -1;
            } else if (i3 != 0) {
                f2433b.mo4390b("Correct configuration of INSTALL_REFERRER receivers.");
                return 1;
            } else {
                f2433b.mo4391b("GetSocial Analytics may not work. %s should be defined as a first INSTALL_REFERRER receiver.", MultipleInstallReferrerReceiver.class.getName());
                return 0;
            }
        } else if (!contains) {
            f2433b.mo4394c("GetSocial Analytics will not be correct. %s is not declared in AndroidManifest.xml.", MultipleInstallReferrerReceiver.class.getName());
            return -1;
        } else if (i3 != 0) {
            f2433b.mo4391b("Replace %s with %s to deliver INSTALL_REFERRER to other defined receivers.", InstallReferrerReceiver.class.getName(), MultipleInstallReferrerReceiver.class.getName());
            return 0;
        } else {
            f2433b.mo4391b("GetSocial Analytics may not work. Declare %s in AndroidManifest.xml or call %s.onIntentReceived(...) from %s", MultipleInstallReferrerReceiver.class.getName(), InstallReferrerReceiver.class.getName(), arrayList.get(0));
            return 0;
        }
    }
}
