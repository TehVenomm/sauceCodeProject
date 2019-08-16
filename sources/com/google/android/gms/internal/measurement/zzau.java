package com.google.android.gms.internal.measurement;

import android.os.RemoteException;
import android.util.Log;
import android.util.Pair;
import com.google.android.gms.measurement.internal.zzgn;

final class zzau extends zzb {
    private final /* synthetic */ zzz zzaa;
    private final /* synthetic */ zzgn zzbk;

    zzau(zzz zzz, zzgn zzgn) {
        this.zzaa = zzz;
        this.zzbk = zzgn;
        super(zzz);
    }

    /* access modifiers changed from: 0000 */
    public final void zzf() throws RemoteException {
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 >= this.zzaa.zzaf.size()) {
                zzd zzd = new zzd(this.zzbk);
                this.zzaa.zzaf.add(new Pair(this.zzbk, zzd));
                this.zzaa.zzar.registerOnMeasurementEventListener(zzd);
                return;
            } else if (this.zzbk.equals(((Pair) this.zzaa.zzaf.get(i2)).first)) {
                Log.w(this.zzaa.zzu, "OnEventListener already registered.");
                return;
            } else {
                i = i2 + 1;
            }
        }
    }
}
