package com.google.android.gms.common.api.internal;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.google.android.gms.common.ConnectionResult;

final class zzaa implements zzce {
    private /* synthetic */ zzy zzfka;

    private zzaa(zzy zzy) {
        this.zzfka = zzy;
    }

    public final void zzc(@NonNull ConnectionResult connectionResult) {
        this.zzfka.zzfjy.lock();
        try {
            this.zzfka.zzfjv = connectionResult;
            this.zzfka.zzagi();
        } finally {
            this.zzfka.zzfjy.unlock();
        }
    }

    public final void zzf(int i, boolean z) {
        this.zzfka.zzfjy.lock();
        try {
            if (this.zzfka.zzfjx || this.zzfka.zzfjw == null || !this.zzfka.zzfjw.isSuccess()) {
                this.zzfka.zzfjx = false;
                this.zzfka.zze(i, z);
                return;
            }
            this.zzfka.zzfjx = true;
            this.zzfka.zzfjq.onConnectionSuspended(i);
            this.zzfka.zzfjy.unlock();
        } finally {
            this.zzfka.zzfjy.unlock();
        }
    }

    public final void zzi(@Nullable Bundle bundle) {
        this.zzfka.zzfjy.lock();
        try {
            this.zzfka.zzh(bundle);
            this.zzfka.zzfjv = ConnectionResult.zzfez;
            this.zzfka.zzagi();
        } finally {
            this.zzfka.zzfjy.unlock();
        }
    }
}
