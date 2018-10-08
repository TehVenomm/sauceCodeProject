package com.google.android.gms.common.api.internal;

import android.os.Looper;
import android.support.annotation.NonNull;
import android.util.Log;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Releasable;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.ResultCallback;
import com.google.android.gms.common.api.ResultCallbacks;
import com.google.android.gms.common.api.ResultTransform;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.TransformedResult;
import com.google.android.gms.common.internal.zzbp;
import java.lang.ref.WeakReference;

public final class zzdf<R extends Result> extends TransformedResult<R> implements ResultCallback<R> {
    private final Object zzfiz = new Object();
    private final WeakReference<GoogleApiClient> zzfjb;
    private ResultTransform<? super R, ? extends Result> zzfpb = null;
    private zzdf<? extends Result> zzfpc = null;
    private volatile ResultCallbacks<? super R> zzfpd = null;
    private PendingResult<R> zzfpe = null;
    private Status zzfpf = null;
    private final zzdh zzfpg;
    private boolean zzfph = false;

    public zzdf(WeakReference<GoogleApiClient> weakReference) {
        zzbp.zzb((Object) weakReference, (Object) "GoogleApiClient reference must not be null");
        this.zzfjb = weakReference;
        GoogleApiClient googleApiClient = (GoogleApiClient) this.zzfjb.get();
        this.zzfpg = new zzdh(this, googleApiClient != null ? googleApiClient.getLooper() : Looper.getMainLooper());
    }

    private final void zzain() {
        if (this.zzfpb != null || this.zzfpd != null) {
            GoogleApiClient googleApiClient = (GoogleApiClient) this.zzfjb.get();
            if (!(this.zzfph || this.zzfpb == null || googleApiClient == null)) {
                googleApiClient.zza(this);
                this.zzfph = true;
            }
            if (this.zzfpf != null) {
                zzw(this.zzfpf);
            } else if (this.zzfpe != null) {
                this.zzfpe.setResultCallback(this);
            }
        }
    }

    private final boolean zzaip() {
        return (this.zzfpd == null || ((GoogleApiClient) this.zzfjb.get()) == null) ? false : true;
    }

    private static void zzd(Result result) {
        if (result instanceof Releasable) {
            try {
                ((Releasable) result).release();
            } catch (Throwable e) {
                String valueOf = String.valueOf(result);
                Log.w("TransformedResultImpl", new StringBuilder(String.valueOf(valueOf).length() + 18).append("Unable to release ").append(valueOf).toString(), e);
            }
        }
    }

    private final void zzd(Status status) {
        synchronized (this.zzfiz) {
            this.zzfpf = status;
            zzw(this.zzfpf);
        }
    }

    private final void zzw(Status status) {
        synchronized (this.zzfiz) {
            if (this.zzfpb != null) {
                Status onFailure = this.zzfpb.onFailure(status);
                zzbp.zzb((Object) onFailure, (Object) "onFailure must not return null");
                this.zzfpc.zzd(onFailure);
            } else if (zzaip()) {
                this.zzfpd.onFailure(status);
            }
        }
    }

    public final void andFinally(@NonNull ResultCallbacks<? super R> resultCallbacks) {
        boolean z = true;
        synchronized (this.zzfiz) {
            zzbp.zza(this.zzfpd == null, (Object) "Cannot call andFinally() twice.");
            if (this.zzfpb != null) {
                z = false;
            }
            zzbp.zza(z, (Object) "Cannot call then() and andFinally() on the same TransformedResult.");
            this.zzfpd = resultCallbacks;
            zzain();
        }
    }

    public final void onResult(R r) {
        synchronized (this.zzfiz) {
            if (!r.getStatus().isSuccess()) {
                zzd(r.getStatus());
                zzd((Result) r);
            } else if (this.zzfpb != null) {
                zzct.zzahm().submit(new zzdg(this, r));
            } else if (zzaip()) {
                this.zzfpd.onSuccess(r);
            }
        }
    }

    @NonNull
    public final <S extends Result> TransformedResult<S> then(@NonNull ResultTransform<? super R, ? extends S> resultTransform) {
        TransformedResult zzdf;
        boolean z = true;
        synchronized (this.zzfiz) {
            zzbp.zza(this.zzfpb == null, (Object) "Cannot call then() twice.");
            if (this.zzfpd != null) {
                z = false;
            }
            zzbp.zza(z, (Object) "Cannot call then() and andFinally() on the same TransformedResult.");
            this.zzfpb = resultTransform;
            zzdf = new zzdf(this.zzfjb);
            this.zzfpc = zzdf;
            zzain();
        }
        return zzdf;
    }

    public final void zza(PendingResult<?> pendingResult) {
        synchronized (this.zzfiz) {
            this.zzfpe = pendingResult;
            zzain();
        }
    }

    final void zzaio() {
        this.zzfpd = null;
    }
}
