package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;
import android.util.Log;
import com.google.android.gms.dynamic.zzn;
import com.google.android.gms.dynamite.DynamiteModule;
import com.google.android.gms.dynamite.DynamiteModule.zzc;
import com.google.android.gms.dynamite.descriptors.com.google.android.gms.flags.ModuleDescriptor;

public final class zzbva {
    private boolean zzaqo = false;
    private zzbvb zzhap = null;

    public final void initialize(Context context) {
        Throwable e;
        synchronized (this) {
            if (this.zzaqo) {
                return;
            }
            try {
                this.zzhap = zzbvc.asInterface(DynamiteModule.zza(context, DynamiteModule.zzgpk, ModuleDescriptor.MODULE_ID).zzgv("com.google.android.gms.flags.impl.FlagProviderImpl"));
                this.zzhap.init(zzn.zzw(context));
                this.zzaqo = true;
            } catch (zzc e2) {
                e = e2;
                Log.w("FlagValueProvider", "Failed to initialize flags module.", e);
            } catch (RemoteException e3) {
                e = e3;
                Log.w("FlagValueProvider", "Failed to initialize flags module.", e);
            }
        }
    }

    public final <T> T zzb(zzbut<T> zzbut) {
        synchronized (this) {
            if (this.zzaqo) {
                return zzbut.zza(this.zzhap);
            }
            T zzik = zzbut.zzik();
            return zzik;
        }
    }
}
