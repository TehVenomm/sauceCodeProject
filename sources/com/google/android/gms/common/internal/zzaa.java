package com.google.android.gms.common.internal;

import android.accounts.Account;
import android.content.Context;
import android.os.IInterface;
import android.os.Looper;
import android.support.annotation.NonNull;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.android.gms.common.api.Api.zze;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.zzc;
import java.util.Set;

public abstract class zzaa<T extends IInterface> extends zzd<T> implements zze, zzae {
    private final Account zzdva;
    private final Set<Scope> zzecn;
    private final zzq zzfkd;

    protected zzaa(Context context, Looper looper, int i, zzq zzq, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
        this(context, looper, zzaf.zzcf(context), GoogleApiAvailability.getInstance(), i, zzq, (ConnectionCallbacks) zzbp.zzu(connectionCallbacks), (OnConnectionFailedListener) zzbp.zzu(onConnectionFailedListener));
    }

    private zzaa(Context context, Looper looper, zzaf zzaf, GoogleApiAvailability googleApiAvailability, int i, zzq zzq, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
        super(context, looper, zzaf, googleApiAvailability, i, connectionCallbacks == null ? null : new zzab(connectionCallbacks), onConnectionFailedListener == null ? null : new zzac(onConnectionFailedListener), zzq.zzajv());
        this.zzfkd = zzq;
        this.zzdva = zzq.getAccount();
        Set zzajs = zzq.zzajs();
        Set<Scope> zzb = zzb(zzajs);
        for (Scope contains : zzb) {
            if (!zzajs.contains(contains)) {
                throw new IllegalStateException("Expanding scopes is not permitted, use implied scopes instead");
            }
        }
        this.zzecn = zzb;
    }

    public final Account getAccount() {
        return this.zzdva;
    }

    public zzc[] zzajh() {
        return new zzc[0];
    }

    protected final Set<Scope> zzajl() {
        return this.zzecn;
    }

    protected final zzq zzakd() {
        return this.zzfkd;
    }

    @NonNull
    protected Set<Scope> zzb(@NonNull Set<Scope> set) {
        return set;
    }
}
