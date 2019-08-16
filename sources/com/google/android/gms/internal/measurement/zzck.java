package com.google.android.gms.internal.measurement;

import android.content.Context;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.pm.ProviderInfo;
import android.net.Uri;

public final class zzck {
    private static volatile zzcw<Boolean> zzaav = zzcw.zzrp();
    private static final Object zzaaw = new Object();

    public static boolean zza(Context context, Uri uri) {
        boolean z;
        boolean z2 = true;
        String authority = uri.getAuthority();
        if (!"com.google.android.gms.phenotype".equals(authority)) {
            throw new IllegalArgumentException(new StringBuilder(String.valueOf(authority).length() + 91).append(authority).append(" is an unsupported authority. Only com.google.android.gms.phenotype authority is supported.").toString());
        } else if (zzaav.isPresent()) {
            return ((Boolean) zzaav.get()).booleanValue();
        } else {
            synchronized (zzaaw) {
                if (zzaav.isPresent()) {
                    boolean booleanValue = ((Boolean) zzaav.get()).booleanValue();
                    return booleanValue;
                }
                if ("com.google.android.gms".equals(context.getPackageName())) {
                    z = true;
                } else {
                    ProviderInfo resolveContentProvider = context.getPackageManager().resolveContentProvider("com.google.android.gms.phenotype", 0);
                    z = resolveContentProvider != null && "com.google.android.gms".equals(resolveContentProvider.packageName);
                }
                if (!z || !zzq(context)) {
                    z2 = false;
                }
                zzaav = zzcw.zzf(Boolean.valueOf(z2));
                return ((Boolean) zzaav.get()).booleanValue();
            }
        }
    }

    private static boolean zzq(Context context) {
        try {
            return (context.getPackageManager().getApplicationInfo("com.google.android.gms", 0).flags & 129) != 0;
        } catch (NameNotFoundException e) {
            return false;
        }
    }
}
