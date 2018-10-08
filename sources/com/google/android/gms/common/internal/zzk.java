package com.google.android.gms.common.internal;

import android.os.Bundle;
import android.os.IBinder;
import android.support.annotation.BinderThread;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.util.Log;

public final class zzk extends zzaw {
    private zzd zzfth;
    private final int zzfti;

    public zzk(@NonNull zzd zzd, int i) {
        this.zzfth = zzd;
        this.zzfti = i;
    }

    @BinderThread
    public final void zza(int i, @Nullable Bundle bundle) {
        Log.wtf("GmsClient", "received deprecated onAccountValidationComplete callback, ignoring", new Exception());
    }

    @BinderThread
    public final void zza(int i, @NonNull IBinder iBinder, @Nullable Bundle bundle) {
        zzbp.zzb(this.zzfth, (Object) "onPostInitComplete can be called only once per call to getRemoteService");
        this.zzfth.zza(i, iBinder, bundle, this.zzfti);
        this.zzfth = null;
    }
}
