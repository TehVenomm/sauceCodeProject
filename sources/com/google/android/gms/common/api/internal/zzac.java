package com.google.android.gms.common.api.internal;

import android.content.Context;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.NonNull;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.ApiOptions;
import com.google.android.gms.common.api.Api.zza;
import com.google.android.gms.common.api.Api.zze;
import com.google.android.gms.common.api.GoogleApi;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.internal.zzcpm;
import com.google.android.gms.internal.zzcpn;

public final class zzac<O extends ApiOptions> extends GoogleApi<O> {
    private final zza<? extends zzcpm, zzcpn> zzfhg;
    private final zze zzfkb;
    private final zzw zzfkc;
    private final zzq zzfkd;

    public zzac(@NonNull Context context, Api<O> api, Looper looper, @NonNull zze zze, @NonNull zzw zzw, zzq zzq, zza<? extends zzcpm, zzcpn> zza) {
        super(context, api, looper);
        this.zzfkb = zze;
        this.zzfkc = zzw;
        this.zzfkd = zzq;
        this.zzfhg = zza;
        this.zzfgp.zzb((GoogleApi) this);
    }

    public final zze zza(Looper looper, zzbr<O> zzbr) {
        this.zzfkc.zza(zzbr);
        return this.zzfkb;
    }

    public final zzcw zza(Context context, Handler handler) {
        return new zzcw(context, handler, this.zzfkd, this.zzfhg);
    }

    public final zze zzagm() {
        return this.zzfkb;
    }
}
