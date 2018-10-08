package com.google.android.gms.common.api.internal;

import android.support.annotation.WorkerThread;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.internal.zzam;
import java.util.Set;

@WorkerThread
public interface zzcy {
    void zzb(zzam zzam, Set<Scope> set);

    void zzh(ConnectionResult connectionResult);
}
