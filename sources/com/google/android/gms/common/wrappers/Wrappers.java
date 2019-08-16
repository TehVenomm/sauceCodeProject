package com.google.android.gms.common.wrappers;

import android.content.Context;
import com.google.android.gms.common.annotation.KeepForSdk;
import com.google.android.gms.common.util.VisibleForTesting;

@KeepForSdk
public class Wrappers {
    private static Wrappers zzhz = new Wrappers();
    private PackageManagerWrapper zzhy = null;

    @KeepForSdk
    public static PackageManagerWrapper packageManager(Context context) {
        return zzhz.zzi(context);
    }

    @VisibleForTesting
    private final PackageManagerWrapper zzi(Context context) {
        PackageManagerWrapper packageManagerWrapper;
        synchronized (this) {
            if (this.zzhy == null) {
                if (context.getApplicationContext() != null) {
                    context = context.getApplicationContext();
                }
                this.zzhy = new PackageManagerWrapper(context);
            }
            packageManagerWrapper = this.zzhy;
        }
        return packageManagerWrapper;
    }
}
