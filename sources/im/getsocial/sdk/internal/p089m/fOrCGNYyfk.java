package im.getsocial.sdk.internal.p089m;

import android.content.Context;
import android.content.pm.PackageManager.NameNotFoundException;

/* renamed from: im.getsocial.sdk.internal.m.fOrCGNYyfk */
public final class fOrCGNYyfk {
    private fOrCGNYyfk() {
    }

    /* renamed from: a */
    public static boolean m2114a(Context context, String str) {
        try {
            context.getPackageManager().getPackageInfo(str, 128);
            return true;
        } catch (NameNotFoundException e) {
            return false;
        }
    }

    /* renamed from: b */
    public static boolean m2115b(Context context, String str) {
        try {
            return context.getPackageManager().getPackageInfo(str, 128).applicationInfo.enabled;
        } catch (NameNotFoundException e) {
            return false;
        }
    }
}
