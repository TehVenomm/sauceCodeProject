package com.google.android.gms.internal;

import android.content.Context;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.support.v4.media.TransportMediator;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.internal.zzaa;
import com.google.android.gms.common.internal.zzq;

public final class zzath extends zzaa<zzatd> {
    public zzath(Context context, Looper looper, zzq zzq, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
        super(context, looper, TransportMediator.KEYCODE_MEDIA_PLAY, zzq, connectionCallbacks, onConnectionFailedListener);
    }

    protected final /* synthetic */ IInterface zze(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.auth.api.phone.internal.ISmsRetrieverApiService");
        return queryLocalInterface instanceof zzatd ? (zzatd) queryLocalInterface : new zzate(iBinder);
    }

    protected final String zzhc() {
        return "com.google.android.gms.auth.api.phone.service.SmsRetrieverApiService.START";
    }

    protected final String zzhd() {
        return "com.google.android.gms.auth.api.phone.internal.ISmsRetrieverApiService";
    }
}
