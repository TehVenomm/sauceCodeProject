package com.google.android.gms.common.api.internal;

import android.os.DeadObjectException;
import android.os.RemoteException;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.Api.zzc;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzbp;

public abstract class zzm<R extends Result, A extends zzb> extends zzs<R> implements zzn<R> {
    private final Api<?> zzfda;
    private final zzc<A> zzfip;

    @Deprecated
    protected zzm(zzc<A> zzc, GoogleApiClient googleApiClient) {
        super((GoogleApiClient) zzbp.zzb((Object) googleApiClient, (Object) "GoogleApiClient must not be null"));
        this.zzfip = (zzc) zzbp.zzu(zzc);
        this.zzfda = null;
    }

    protected zzm(Api<?> api, GoogleApiClient googleApiClient) {
        super((GoogleApiClient) zzbp.zzb((Object) googleApiClient, (Object) "GoogleApiClient must not be null"));
        this.zzfip = api.zzafd();
        this.zzfda = api;
    }

    private final void zzc(RemoteException remoteException) {
        zzs(new Status(8, remoteException.getLocalizedMessage(), null));
    }

    public /* bridge */ /* synthetic */ void setResult(Object obj) {
        super.setResult((Result) obj);
    }

    protected abstract void zza(A a) throws RemoteException;

    public final zzc<A> zzafd() {
        return this.zzfip;
    }

    public final Api<?> zzafi() {
        return this.zzfda;
    }

    public final void zzb(A a) throws DeadObjectException {
        try {
            zza(a);
        } catch (RemoteException e) {
            zzc(e);
            throw e;
        } catch (RemoteException e2) {
            zzc(e2);
        }
    }

    public final void zzs(Status status) {
        zzbp.zzb(!status.isSuccess(), (Object) "Failed result must not be success");
        setResult(zzb(status));
    }
}
