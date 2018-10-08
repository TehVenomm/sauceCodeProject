package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.text.TextUtils;
import com.google.android.gms.auth.api.zzd;
import com.google.android.gms.auth.api.zzf;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.internal.zzaa;
import com.google.android.gms.common.internal.zzq;

public final class zzasy extends zzaa<zzatb> {
    private final Bundle zzdzy;

    public zzasy(Context context, Looper looper, zzq zzq, zzf zzf, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
        super(context, looper, 16, zzq, connectionCallbacks, onConnectionFailedListener);
        if (zzf == null) {
            this.zzdzy = new Bundle();
            return;
        }
        throw new NoSuchMethodError();
    }

    public final boolean zzaaa() {
        zzq zzakd = zzakd();
        return (TextUtils.isEmpty(zzakd.getAccountName()) || zzakd.zzc(zzd.API).isEmpty()) ? false : true;
    }

    protected final /* synthetic */ IInterface zze(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.auth.api.internal.IAuthService");
        return queryLocalInterface instanceof zzatb ? (zzatb) queryLocalInterface : new zzatc(iBinder);
    }

    protected final String zzhc() {
        return "com.google.android.gms.auth.service.START";
    }

    protected final String zzhd() {
        return "com.google.android.gms.auth.api.internal.IAuthService";
    }

    protected final Bundle zzzs() {
        return this.zzdzy;
    }
}
