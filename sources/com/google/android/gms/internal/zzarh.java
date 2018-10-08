package com.google.android.gms.internal;

import android.content.Context;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import com.github.droidfu.support.DisplaySupport;
import com.google.android.gms.auth.account.zzc;
import com.google.android.gms.auth.account.zzd;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.internal.zzaa;
import com.google.android.gms.common.internal.zzq;

public final class zzarh extends zzaa<zzc> {
    public zzarh(Context context, Looper looper, zzq zzq, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
        super(context, looper, DisplaySupport.SCREEN_DENSITY_LOW, zzq, connectionCallbacks, onConnectionFailedListener);
    }

    protected final /* synthetic */ IInterface zze(IBinder iBinder) {
        return zzd.zzab(iBinder);
    }

    protected final String zzhc() {
        return "com.google.android.gms.auth.account.workaccount.START";
    }

    protected final String zzhd() {
        return "com.google.android.gms.auth.account.IWorkAccountService";
    }
}
