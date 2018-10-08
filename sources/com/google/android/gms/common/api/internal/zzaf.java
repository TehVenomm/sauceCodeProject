package com.google.android.gms.common.api.internal;

import android.support.annotation.NonNull;
import android.support.v4.util.ArrayMap;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.zza;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import java.util.Collections;

final class zzaf implements OnCompleteListener<Void> {
    private /* synthetic */ zzad zzfks;

    private zzaf(zzad zzad) {
        this.zzfks = zzad;
    }

    public final void onComplete(@NonNull Task<Void> task) {
        this.zzfks.zzfjy.lock();
        try {
            if (this.zzfks.zzfkn) {
                if (task.isSuccessful()) {
                    this.zzfks.zzfko = new ArrayMap(this.zzfks.zzfke.size());
                    for (zzac zzafj : this.zzfks.zzfke.values()) {
                        this.zzfks.zzfko.put(zzafj.zzafj(), ConnectionResult.zzfez);
                    }
                } else if (task.getException() instanceof zza) {
                    zza zza = (zza) task.getException();
                    if (this.zzfks.zzfkl) {
                        this.zzfks.zzfko = new ArrayMap(this.zzfks.zzfke.size());
                        for (zzac zzac : this.zzfks.zzfke.values()) {
                            zzh zzafj2 = zzac.zzafj();
                            ConnectionResult zza2 = zza.zza(zzac);
                            if (this.zzfks.zza(zzac, zza2)) {
                                this.zzfks.zzfko.put(zzafj2, new ConnectionResult(16));
                            } else {
                                this.zzfks.zzfko.put(zzafj2, zza2);
                            }
                        }
                    } else {
                        this.zzfks.zzfko = zza.zzafg();
                    }
                    this.zzfks.zzfkr = this.zzfks.zzagq();
                } else {
                    Log.e("ConnectionlessGAC", "Unexpected availability exception", task.getException());
                    this.zzfks.zzfko = Collections.emptyMap();
                    this.zzfks.zzfkr = new ConnectionResult(8);
                }
                if (this.zzfks.zzfkp != null) {
                    this.zzfks.zzfko.putAll(this.zzfks.zzfkp);
                    this.zzfks.zzfkr = this.zzfks.zzagq();
                }
                if (this.zzfks.zzfkr == null) {
                    this.zzfks.zzago();
                    this.zzfks.zzagp();
                } else {
                    this.zzfks.zzfkn = false;
                    this.zzfks.zzfkh.zzc(this.zzfks.zzfkr);
                }
                this.zzfks.zzfkj.signalAll();
                this.zzfks.zzfjy.unlock();
            }
        } finally {
            this.zzfks.zzfjy.unlock();
        }
    }
}
