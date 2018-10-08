package com.google.android.gms.common.api.internal;

import android.support.annotation.NonNull;
import android.support.v4.util.ArrayMap;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.zza;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import java.util.Collections;

final class zzag implements OnCompleteListener<Void> {
    private /* synthetic */ zzad zzfks;
    private zzcv zzfkt;

    zzag(zzad zzad, zzcv zzcv) {
        this.zzfks = zzad;
        this.zzfkt = zzcv;
    }

    final void cancel() {
        this.zzfkt.zzaaj();
    }

    public final void onComplete(@NonNull Task<Void> task) {
        this.zzfks.zzfjy.lock();
        try {
            if (this.zzfks.zzfkn) {
                if (task.isSuccessful()) {
                    this.zzfks.zzfkp = new ArrayMap(this.zzfks.zzfkf.size());
                    for (zzac zzafj : this.zzfks.zzfkf.values()) {
                        this.zzfks.zzfkp.put(zzafj.zzafj(), ConnectionResult.zzfez);
                    }
                } else if (task.getException() instanceof zza) {
                    zza zza = (zza) task.getException();
                    if (this.zzfks.zzfkl) {
                        this.zzfks.zzfkp = new ArrayMap(this.zzfks.zzfkf.size());
                        for (zzac zzac : this.zzfks.zzfkf.values()) {
                            zzh zzafj2 = zzac.zzafj();
                            ConnectionResult zza2 = zza.zza(zzac);
                            if (this.zzfks.zza(zzac, zza2)) {
                                this.zzfks.zzfkp.put(zzafj2, new ConnectionResult(16));
                            } else {
                                this.zzfks.zzfkp.put(zzafj2, zza2);
                            }
                        }
                    } else {
                        this.zzfks.zzfkp = zza.zzafg();
                    }
                } else {
                    Log.e("ConnectionlessGAC", "Unexpected availability exception", task.getException());
                    this.zzfks.zzfkp = Collections.emptyMap();
                }
                if (this.zzfks.isConnected()) {
                    this.zzfks.zzfko.putAll(this.zzfks.zzfkp);
                    if (this.zzfks.zzagq() == null) {
                        this.zzfks.zzago();
                        this.zzfks.zzagp();
                        this.zzfks.zzfkj.signalAll();
                    }
                }
                this.zzfkt.zzaaj();
                this.zzfks.zzfjy.unlock();
                return;
            }
            this.zzfkt.zzaaj();
        } finally {
            this.zzfks.zzfjy.unlock();
        }
    }
}
