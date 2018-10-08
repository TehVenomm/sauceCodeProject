package com.google.android.gms.common.api.internal;

import android.os.IBinder;
import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzc;
import com.google.android.gms.common.api.Api.zze;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import java.util.Collections;
import java.util.Map;
import java.util.Set;
import java.util.WeakHashMap;

public final class zzdi {
    public static final Status zzfpk = new Status(8, "The connection to Google Play services was lost");
    private static final zzs<?>[] zzfpl = new zzs[0];
    private final Map<zzc<?>, zze> zzfmh;
    final Set<zzs<?>> zzfpm = Collections.synchronizedSet(Collections.newSetFromMap(new WeakHashMap()));
    private final zzdl zzfpn = new zzdj(this);

    public zzdi(Map<zzc<?>, zze> map) {
        this.zzfmh = map;
    }

    public final void release() {
        for (PendingResult pendingResult : (zzs[]) this.zzfpm.toArray(zzfpl)) {
            pendingResult.zza(null);
            if (pendingResult.zzafr() != null) {
                pendingResult.setResultCallback(null);
                IBinder zzaff = ((zze) this.zzfmh.get(((zzm) pendingResult).zzafd())).zzaff();
                if (pendingResult.isReady()) {
                    pendingResult.zza(new zzdk(pendingResult, null, zzaff, null));
                } else if (zzaff == null || !zzaff.isBinderAlive()) {
                    pendingResult.zza(null);
                    pendingResult.cancel();
                    pendingResult.zzafr().intValue();
                    throw new NullPointerException();
                } else {
                    zzdl zzdk = new zzdk(pendingResult, null, zzaff, null);
                    pendingResult.zza(zzdk);
                    try {
                        zzaff.linkToDeath(zzdk, 0);
                    } catch (RemoteException e) {
                        pendingResult.cancel();
                        pendingResult.zzafr().intValue();
                        throw new NullPointerException();
                    }
                }
                this.zzfpm.remove(pendingResult);
            } else if (pendingResult.zzage()) {
                this.zzfpm.remove(pendingResult);
            }
        }
    }

    public final void zzaiq() {
        for (zzs zzt : (zzs[]) this.zzfpm.toArray(zzfpl)) {
            zzt.zzt(zzfpk);
        }
    }

    final void zzb(zzs<? extends Result> zzs) {
        this.zzfpm.add(zzs);
        zzs.zza(this.zzfpn);
    }
}
