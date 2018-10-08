package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.support.annotation.Nullable;
import com.google.android.gms.auth.api.Auth.AuthCredentialsOptions;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.internal.zzaa;
import com.google.android.gms.common.internal.zzq;

public final class zzaso extends zzaa<zzast> {
    @Nullable
    private final AuthCredentialsOptions zzebm;

    public zzaso(Context context, Looper looper, zzq zzq, AuthCredentialsOptions authCredentialsOptions, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
        super(context, looper, 68, zzq, connectionCallbacks, onConnectionFailedListener);
        this.zzebm = authCredentialsOptions;
    }

    protected final /* synthetic */ IInterface zze(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.auth.api.credentials.internal.ICredentialsService");
        return queryLocalInterface instanceof zzast ? (zzast) queryLocalInterface : new zzasu(iBinder);
    }

    protected final String zzhc() {
        return "com.google.android.gms.auth.api.credentials.service.START";
    }

    protected final String zzhd() {
        return "com.google.android.gms.auth.api.credentials.internal.ICredentialsService";
    }

    protected final Bundle zzzs() {
        return this.zzebm == null ? new Bundle() : this.zzebm.zzzs();
    }

    final AuthCredentialsOptions zzzz() {
        return this.zzebm;
    }
}
