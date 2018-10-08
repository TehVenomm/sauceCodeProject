package com.google.android.gms.auth.api.signin.internal;

import android.content.Context;
import android.content.Intent;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions.Builder;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.internal.zzaa;
import com.google.android.gms.common.internal.zzq;

public final class zzd extends zzaa<zzt> {
    private final GoogleSignInOptions zzect;

    public zzd(Context context, Looper looper, zzq zzq, GoogleSignInOptions googleSignInOptions, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
        super(context, looper, 91, zzq, connectionCallbacks, onConnectionFailedListener);
        if (googleSignInOptions == null) {
            googleSignInOptions = new Builder().build();
        }
        if (!zzq.zzajs().isEmpty()) {
            Builder builder = new Builder(googleSignInOptions);
            for (Scope requestScopes : zzq.zzajs()) {
                builder.requestScopes(requestScopes, new Scope[0]);
            }
            googleSignInOptions = builder.build();
        }
        this.zzect = googleSignInOptions;
    }

    public final boolean zzaak() {
        return true;
    }

    public final Intent zzaal() {
        return zze.zza(getContext(), this.zzect);
    }

    public final GoogleSignInOptions zzaam() {
        return this.zzect;
    }

    protected final /* synthetic */ IInterface zze(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.auth.api.signin.internal.ISignInService");
        return queryLocalInterface instanceof zzt ? (zzt) queryLocalInterface : new zzu(iBinder);
    }

    protected final String zzhc() {
        return "com.google.android.gms.auth.api.signin.service.START";
    }

    protected final String zzhd() {
        return "com.google.android.gms.auth.api.signin.internal.ISignInService";
    }
}
