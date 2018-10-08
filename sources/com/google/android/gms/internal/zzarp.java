package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import com.google.android.gms.auth.api.accounttransfer.zzo;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.internal.zzaa;
import com.google.android.gms.common.internal.zzq;

public final class zzarp extends zzaa<zzaru> {
    private final Bundle zzdzy;

    public zzarp(Context context, Looper looper, zzq zzq, zzo zzo, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
        super(context, looper, 128, zzq, connectionCallbacks, onConnectionFailedListener);
        if (zzo == null) {
            this.zzdzy = new Bundle();
            return;
        }
        throw new NoSuchMethodError();
    }

    protected final /* synthetic */ IInterface zze(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.auth.api.accounttransfer.internal.IAccountTransferService");
        return queryLocalInterface instanceof zzaru ? (zzaru) queryLocalInterface : new zzarv(iBinder);
    }

    protected final String zzhc() {
        return "com.google.android.gms.auth.api.accounttransfer.service.START";
    }

    protected final String zzhd() {
        return "com.google.android.gms.auth.api.accounttransfer.internal.IAccountTransferService";
    }

    protected final Bundle zzzs() {
        return this.zzdzy;
    }
}
