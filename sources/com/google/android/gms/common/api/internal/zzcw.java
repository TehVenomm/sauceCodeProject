package com.google.android.gms.common.api.internal;

import android.content.Context;
import android.os.Bundle;
import android.os.Handler;
import android.support.annotation.BinderThread;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api.zza;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.internal.zzbs;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.internal.zzcpj;
import com.google.android.gms.internal.zzcpm;
import com.google.android.gms.internal.zzcpn;
import com.google.android.gms.internal.zzcpr;
import com.google.android.gms.internal.zzcpz;
import java.util.Set;

public final class zzcw extends zzcpr implements ConnectionCallbacks, OnConnectionFailedListener {
    private static zza<? extends zzcpm, zzcpn> zzfox = zzcpj.zzdwr;
    private final Context mContext;
    private final Handler mHandler;
    private Set<Scope> zzecn;
    private final zza<? extends zzcpm, zzcpn> zzffz;
    private zzq zzfkd;
    private zzcpm zzflj;
    private zzcy zzfoy;

    @WorkerThread
    public zzcw(Context context, Handler handler, @NonNull zzq zzq) {
        this(context, handler, zzq, zzfox);
    }

    @WorkerThread
    public zzcw(Context context, Handler handler, @NonNull zzq zzq, zza<? extends zzcpm, zzcpn> zza) {
        this.mContext = context;
        this.mHandler = handler;
        this.zzfkd = (zzq) zzbp.zzb((Object) zzq, (Object) "ClientSettings must not be null");
        this.zzecn = zzq.zzajr();
        this.zzffz = zza;
    }

    @WorkerThread
    private final void zzc(zzcpz zzcpz) {
        ConnectionResult zzagc = zzcpz.zzagc();
        if (zzagc.isSuccess()) {
            zzbs zzbca = zzcpz.zzbca();
            ConnectionResult zzagc2 = zzbca.zzagc();
            if (zzagc2.isSuccess()) {
                this.zzfoy.zzb(zzbca.zzakl(), this.zzecn);
            } else {
                String valueOf = String.valueOf(zzagc2);
                Log.wtf("SignInCoordinator", new StringBuilder(String.valueOf(valueOf).length() + 48).append("Sign-in succeeded with resolve account failure: ").append(valueOf).toString(), new Exception());
                this.zzfoy.zzh(zzagc2);
                this.zzflj.disconnect();
                return;
            }
        }
        this.zzfoy.zzh(zzagc);
        this.zzflj.disconnect();
    }

    @WorkerThread
    public final void onConnected(@Nullable Bundle bundle) {
        this.zzflj.zza(this);
    }

    @WorkerThread
    public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        this.zzfoy.zzh(connectionResult);
    }

    @WorkerThread
    public final void onConnectionSuspended(int i) {
        this.zzflj.disconnect();
    }

    @WorkerThread
    public final void zza(zzcy zzcy) {
        if (this.zzflj != null) {
            this.zzflj.disconnect();
        }
        this.zzfkd.zzc(Integer.valueOf(System.identityHashCode(this)));
        this.zzflj = (zzcpm) this.zzffz.zza(this.mContext, this.mHandler.getLooper(), this.zzfkd, this.zzfkd.zzajx(), this, this);
        this.zzfoy = zzcy;
        this.zzflj.connect();
    }

    public final zzcpm zzaib() {
        return this.zzflj;
    }

    public final void zzaim() {
        if (this.zzflj != null) {
            this.zzflj.disconnect();
        }
    }

    @BinderThread
    public final void zzb(zzcpz zzcpz) {
        this.mHandler.post(new zzcx(this, zzcpz));
    }
}
