package com.google.android.gms.internal.measurement;

import android.os.RemoteException;
import android.util.Log;
import android.util.Pair;
import com.google.android.gms.measurement.internal.zzgn;

final class zzaz extends zzb {
    private final /* synthetic */ zzz zzaa;
    private final /* synthetic */ zzgn zzbk;

    zzaz(zzz zzz, zzgn zzgn) {
        this.zzaa = zzz;
        this.zzbk = zzgn;
        super(zzz);
    }

    /* access modifiers changed from: 0000 */
    public final void zzf() throws RemoteException {
        Pair pair;
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 >= this.zzaa.zzaf.size()) {
                pair = null;
                break;
            } else if (this.zzbk.equals(((Pair) this.zzaa.zzaf.get(i2)).first)) {
                pair = (Pair) this.zzaa.zzaf.get(i2);
                break;
            } else {
                i = i2 + 1;
            }
        }
        if (pair == null) {
            Log.w(this.zzaa.zzu, "OnEventListener had not been registered.");
            return;
        }
        this.zzaa.zzar.unregisterOnMeasurementEventListener((zzq) pair.second);
        this.zzaa.zzaf.remove(pair);
    }
}
