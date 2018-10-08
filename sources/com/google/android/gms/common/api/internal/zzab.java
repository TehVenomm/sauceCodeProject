package com.google.android.gms.common.api.internal;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.google.android.gms.common.ConnectionResult;

final class zzab implements zzce {
    private /* synthetic */ zzy zzfka;

    private zzab(zzy zzy) {
        this.zzfka = zzy;
    }

    public final void zzc(@NonNull ConnectionResult connectionResult) {
        this.zzfka.zzfjy.lock();
        try {
            this.zzfka.zzfjw = connectionResult;
            this.zzfka.zzagi();
        } finally {
            this.zzfka.zzfjy.unlock();
        }
    }

    public final void zzf(int i, boolean z) {
        this.zzfka.zzfjy.lock();
        try {
            if (this.zzfka.zzfjx) {
                this.zzfka.zzfjx = false;
                this.zzfka.zze(i, z);
                return;
            }
            this.zzfka.zzfjx = true;
            this.zzfka.zzfjp.onConnectionSuspended(i);
            this.zzfka.zzfjy.unlock();
        } finally {
            this.zzfka.zzfjy.unlock();
        }
    }

    public final void zzi(@Nullable Bundle bundle) {
        this.zzfka.zzfjy.lock();
        try {
            this.zzfka.zzfjw = ConnectionResult.zzfez;
            this.zzfka.zzagi();
        } finally {
            this.zzfka.zzfjy.unlock();
        }
    }
}
