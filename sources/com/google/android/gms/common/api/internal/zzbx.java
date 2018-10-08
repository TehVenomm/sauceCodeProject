package com.google.android.gms.common.api.internal;

import android.content.Context;
import android.os.Looper;
import android.support.annotation.NonNull;
import com.google.android.gms.common.api.Api.ApiOptions;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApi;
import com.google.android.gms.common.api.Result;

public final class zzbx<O extends ApiOptions> extends zzan {
    private final GoogleApi<O> zzfob;

    public zzbx(GoogleApi<O> googleApi) {
        super("Method is not supported by connectionless client. APIs supporting connectionless client must not call this method.");
        this.zzfob = googleApi;
    }

    public final Context getContext() {
        return this.zzfob.getApplicationContext();
    }

    public final Looper getLooper() {
        return this.zzfob.getLooper();
    }

    public final void zza(zzdf zzdf) {
    }

    public final void zzb(zzdf zzdf) {
    }

    public final <A extends zzb, R extends Result, T extends zzm<R, A>> T zzd(@NonNull T t) {
        return this.zzfob.zza((zzm) t);
    }

    public final <A extends zzb, T extends zzm<? extends Result, A>> T zze(@NonNull T t) {
        return this.zzfob.zzb((zzm) t);
    }
}
