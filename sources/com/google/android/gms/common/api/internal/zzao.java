package com.google.android.gms.common.api.internal;

import android.os.Bundle;
import android.os.DeadObjectException;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.Api.zze;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.internal.zzby;

public final class zzao implements zzbk {
    private final zzbl zzflb;
    private boolean zzflc = false;

    public zzao(zzbl zzbl) {
        this.zzflb = zzbl;
    }

    public final void begin() {
    }

    public final void connect() {
        if (this.zzflc) {
            this.zzflc = false;
            this.zzflb.zza(new zzaq(this, this));
        }
    }

    public final boolean disconnect() {
        if (this.zzflc) {
            return false;
        }
        if (this.zzflb.zzfjo.zzahi()) {
            this.zzflc = true;
            for (zzdf zzaio : this.zzflb.zzfjo.zzfmm) {
                zzaio.zzaio();
            }
            return false;
        }
        this.zzflb.zzg(null);
        return true;
    }

    public final void onConnected(Bundle bundle) {
    }

    public final void onConnectionSuspended(int i) {
        this.zzflb.zzg(null);
        this.zzflb.zzfna.zzf(i, this.zzflc);
    }

    public final void zza(ConnectionResult connectionResult, Api<?> api, boolean z) {
    }

    final void zzagx() {
        if (this.zzflc) {
            this.zzflc = false;
            this.zzflb.zzfjo.zzfmn.release();
            disconnect();
        }
    }

    public final <A extends zzb, R extends Result, T extends zzm<R, A>> T zzd(T t) {
        return zze(t);
    }

    public final <A extends zzb, T extends zzm<? extends Result, A>> T zze(T t) {
        try {
            this.zzflb.zzfjo.zzfmn.zzb(t);
            zzbd zzbd = this.zzflb.zzfjo;
            zzb zzb = (zze) zzbd.zzfmh.get(t.zzafd());
            zzbp.zzb((Object) zzb, (Object) "Appropriate Api was not requested.");
            if (zzb.isConnected() || !this.zzflb.zzfmw.containsKey(t.zzafd())) {
                if (zzb instanceof zzby) {
                    zzb = zzby.zzako();
                }
                t.zzb(zzb);
                return t;
            }
            t.zzs(new Status(17));
            return t;
        } catch (DeadObjectException e) {
            this.zzflb.zza(new zzap(this, this));
        }
    }
}
