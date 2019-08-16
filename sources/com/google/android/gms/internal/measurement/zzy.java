package com.google.android.gms.internal.measurement;

import android.content.Context;
import android.os.Bundle;
import android.os.RemoteException;
import android.util.Log;
import com.google.android.gms.dynamic.ObjectWrapper;
import java.util.ArrayList;

final class zzy extends zzb {
    private final /* synthetic */ Context val$context;
    private final /* synthetic */ zzz zzaa;
    private final /* synthetic */ String zzx;
    private final /* synthetic */ String zzy;
    private final /* synthetic */ Bundle zzz;

    zzy(zzz zzz2, String str, String str2, Context context, Bundle bundle) {
        this.zzaa = zzz2;
        this.zzx = str;
        this.zzy = str2;
        this.val$context = context;
        this.zzz = bundle;
        super(zzz2);
    }

    public final void zzf() {
        String str;
        String str2;
        String str3;
        boolean z;
        int i;
        try {
            this.zzaa.zzaf = new ArrayList();
            if (zzz.zza(this.zzx, this.zzy)) {
                str2 = this.zzy;
                str3 = this.zzx;
                str = this.zzaa.zzu;
            } else {
                str = null;
                str2 = null;
                str3 = null;
            }
            zzz.zze(this.val$context);
            boolean z2 = zzz.zzai.booleanValue() || str3 != null;
            this.zzaa.zzar = this.zzaa.zza(this.val$context, z2);
            if (this.zzaa.zzar == null) {
                Log.w(this.zzaa.zzu, "Failed to connect to measurement client.");
                return;
            }
            int zzh = zzz.zzd(this.val$context);
            int zzi = zzz.zzc(this.val$context);
            if (z2) {
                z = zzi < zzh;
                i = Math.max(zzh, zzi);
            } else {
                z = zzh > 0;
                i = zzh > 0 ? zzh : zzi;
            }
            this.zzaa.zzar.initialize(ObjectWrapper.wrap(this.val$context), new zzx(16250, (long) i, z, str, str3, str2, this.zzz), this.timestamp);
        } catch (RemoteException e) {
            this.zzaa.zza((Exception) e, true, false);
        }
    }
}
