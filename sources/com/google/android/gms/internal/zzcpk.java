package com.google.android.gms.internal;

import android.content.Context;
import android.os.Looper;
import com.google.android.gms.common.api.Api.zza;
import com.google.android.gms.common.api.Api.zze;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.internal.zzq;

final class zzcpk extends zza<zzcpw, zzcpn> {
    zzcpk() {
    }

    public final /* synthetic */ zze zza(Context context, Looper looper, zzq zzq, Object obj, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
        zzcpn zzcpn = (zzcpn) obj;
        return new zzcpw(context, looper, true, zzq, zzcpn == null ? zzcpn.zzjnd : zzcpn, connectionCallbacks, onConnectionFailedListener);
    }
}
