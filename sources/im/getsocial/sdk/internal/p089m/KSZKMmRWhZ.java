package im.getsocial.sdk.internal.p089m;

import android.content.Context;
import android.content.pm.ProviderInfo;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.invites.ImageContentProvider;

/* renamed from: im.getsocial.sdk.internal.m.KSZKMmRWhZ */
public final class KSZKMmRWhZ {
    /* renamed from: a */
    private static cjrhisSQCL f2211a = upgqDBbsrL.m1274a(KSZKMmRWhZ.class);

    private KSZKMmRWhZ() {
    }

    /* renamed from: a */
    public static String m2108a(Context context) {
        ProviderInfo a = im.getsocial.sdk.invites.p092a.p097j.upgqDBbsrL.m2391a(context);
        if (a != null) {
            return a.authority;
        }
        f2211a.mo4391b("Can not create media content URI, %s is not found in the AndroidManifest.xml", ImageContentProvider.class.getSimpleName());
        return null;
    }
}
