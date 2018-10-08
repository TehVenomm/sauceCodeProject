package com.google.android.gms.common.api.internal;

import android.os.Looper;
import android.support.annotation.NonNull;
import com.google.android.gms.common.internal.zzbp;

public final class zzcj<L> {
    private volatile L mListener;
    private final zzck zzfoo;
    private final zzcl<L> zzfop;

    zzcj(@NonNull Looper looper, @NonNull L l, @NonNull String str) {
        this.zzfoo = new zzck(this, looper);
        this.mListener = zzbp.zzb((Object) l, (Object) "Listener must not be null");
        this.zzfop = new zzcl(l, zzbp.zzgf(str));
    }

    public final void clear() {
        this.mListener = null;
    }

    public final void zza(zzcm<? super L> zzcm) {
        zzbp.zzb((Object) zzcm, (Object) "Notifier must not be null");
        this.zzfoo.sendMessage(this.zzfoo.obtainMessage(1, zzcm));
    }

    public final boolean zzadh() {
        return this.mListener != null;
    }

    @NonNull
    public final zzcl<L> zzaik() {
        return this.zzfop;
    }

    final void zzb(zzcm<? super L> zzcm) {
        Object obj = this.mListener;
        if (obj == null) {
            zzcm.zzagw();
            return;
        }
        try {
            zzcm.zzq(obj);
        } catch (RuntimeException e) {
            zzcm.zzagw();
            throw e;
        }
    }
}
