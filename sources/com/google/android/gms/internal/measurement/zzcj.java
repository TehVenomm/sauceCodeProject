package com.google.android.gms.internal.measurement;

import android.content.Context;
import android.support.annotation.GuardedBy;
import android.support.p000v4.content.PermissionChecker;
import android.util.Log;

final class zzcj implements zzce {
    @GuardedBy("GservicesLoader.class")
    static zzcj zzaau;
    private final Context zzob;

    private zzcj() {
        this.zzob = null;
    }

    private zzcj(Context context) {
        this.zzob = context;
        this.zzob.getContentResolver().registerContentObserver(zzbz.CONTENT_URI, true, new zzcl(this, null));
    }

    /* access modifiers changed from: private */
    /* renamed from: zzde */
    public final String zzdd(String str) {
        if (this.zzob == null) {
            return null;
        }
        try {
            return (String) zzch.zza(new zzci(this, str));
        } catch (SecurityException e) {
            SecurityException securityException = e;
            String valueOf = String.valueOf(str);
            Log.e("GservicesLoader", valueOf.length() != 0 ? "Unable to read GServices for: ".concat(valueOf) : new String("Unable to read GServices for: "), securityException);
            return null;
        }
    }

    static zzcj zzp(Context context) {
        zzcj zzcj;
        synchronized (zzcj.class) {
            try {
                if (zzaau == null) {
                    zzaau = PermissionChecker.checkSelfPermission(context, "com.google.android.providers.gsf.permission.READ_GSERVICES") == 0 ? new zzcj(context) : new zzcj();
                }
                zzcj = zzaau;
            } finally {
                Class<zzcj> cls = zzcj.class;
            }
        }
        return zzcj;
    }

    /* access modifiers changed from: 0000 */
    public final /* synthetic */ String zzdf(String str) {
        return zzbz.zza(this.zzob.getContentResolver(), str, (String) null);
    }
}
