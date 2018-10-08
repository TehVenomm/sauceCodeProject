package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.os.ParcelFileDescriptor;
import android.os.ParcelFileDescriptor.AutoCloseOutputStream;
import android.os.RemoteException;
import android.support.annotation.NonNull;
import android.util.Log;
import android.util.Pair;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.common.internal.zzaa;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.nearby.connection.ConnectionsStatusCodes;
import com.google.android.gms.nearby.connection.Payload;
import java.io.IOException;

public final class zzcgp extends zzaa<zzcjg> {
    private final long zzjbe = ((long) hashCode());
    private zzckp zzjbf;

    public zzcgp(Context context, Looper looper, zzq zzq, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
        super(context, looper, 54, zzq, connectionCallbacks, onConnectionFailedListener);
    }

    public final void disconnect() {
        if (isConnected()) {
            try {
                ((zzcjg) zzajj()).zza(new zzcgn());
            } catch (Throwable e) {
                Log.w("NearbyConnectionsClient", "Failed to notify client disconnect.", e);
            }
        }
        if (this.zzjbf != null) {
            this.zzjbf.shutdown();
            this.zzjbf = null;
        }
        super.disconnect();
    }

    protected final /* synthetic */ void zza(@NonNull IInterface iInterface) {
        super.zza((IInterface) (zzcjg) iInterface);
        this.zzjbf = new zzckp();
    }

    public final void zza(zzn<Status> zzn, String[] strArr, Payload payload, boolean z) throws RemoteException {
        try {
            Pair zza = zzckt.zza(payload);
            ((zzcjg) zzajj()).zza(new zzcky(new zzchm(zzn).asBinder(), strArr, (zzckr) zza.first, z));
            if (zza.second != null) {
                Pair pair = (Pair) zza.second;
                this.zzjbf.zza(payload.asStream().asInputStream(), new AutoCloseOutputStream((ParcelFileDescriptor) pair.first), new AutoCloseOutputStream((ParcelFileDescriptor) pair.second), payload.getId());
            }
        } catch (IOException e) {
            zzn.setResult(new Status(ConnectionsStatusCodes.STATUS_PAYLOAD_IO_ERROR));
        }
    }

    protected final /* synthetic */ IInterface zze(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.internal.connection.INearbyConnectionService");
        return queryLocalInterface instanceof zzcjg ? (zzcjg) queryLocalInterface : new zzcjh(iBinder);
    }

    protected final String zzhc() {
        return "com.google.android.gms.nearby.connection.service.START";
    }

    protected final String zzhd() {
        return "com.google.android.gms.nearby.internal.connection.INearbyConnectionService";
    }

    public final void zzj(zzn<Status> zzn, String str) throws RemoteException {
        ((zzcjg) zzajj()).zza(new zzcku(new zzchm(zzn).asBinder(), str));
    }

    protected final Bundle zzzs() {
        Bundle bundle = new Bundle();
        bundle.putLong("clientId", this.zzjbe);
        return bundle;
    }
}
