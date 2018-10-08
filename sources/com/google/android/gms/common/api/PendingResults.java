package com.google.android.gms.common.api;

import android.os.Looper;
import com.google.android.gms.common.api.internal.zzcp;
import com.google.android.gms.common.api.internal.zzda;
import com.google.android.gms.common.api.internal.zzs;
import com.google.android.gms.common.internal.zzbp;

public final class PendingResults {

    static final class zza<R extends Result> extends zzs<R> {
        private final R zzfhk;

        public zza(R r) {
            super(Looper.getMainLooper());
            this.zzfhk = r;
        }

        protected final R zzb(Status status) {
            if (status.getStatusCode() == this.zzfhk.getStatus().getStatusCode()) {
                return this.zzfhk;
            }
            throw new UnsupportedOperationException("Creating failed results is not supported");
        }
    }

    static final class zzb<R extends Result> extends zzs<R> {
        private final R zzfhl;

        public zzb(GoogleApiClient googleApiClient, R r) {
            super(googleApiClient);
            this.zzfhl = r;
        }

        protected final R zzb(Status status) {
            return this.zzfhl;
        }
    }

    static final class zzc<R extends Result> extends zzs<R> {
        public zzc(GoogleApiClient googleApiClient) {
            super(googleApiClient);
        }

        protected final R zzb(Status status) {
            throw new UnsupportedOperationException("Creating failed results is not supported");
        }
    }

    private PendingResults() {
    }

    public static PendingResult<Status> canceledPendingResult() {
        PendingResult<Status> zzda = new zzda(Looper.getMainLooper());
        zzda.cancel();
        return zzda;
    }

    public static <R extends Result> PendingResult<R> canceledPendingResult(R r) {
        zzbp.zzb((Object) r, (Object) "Result must not be null");
        zzbp.zzb(r.getStatus().getStatusCode() == 16, (Object) "Status code must be CommonStatusCodes.CANCELED");
        PendingResult<R> zza = new zza(r);
        zza.cancel();
        return zza;
    }

    public static <R extends Result> OptionalPendingResult<R> immediatePendingResult(R r) {
        zzbp.zzb((Object) r, (Object) "Result must not be null");
        PendingResult zzc = new zzc(null);
        zzc.setResult(r);
        return new zzcp(zzc);
    }

    public static PendingResult<Status> immediatePendingResult(Status status) {
        zzbp.zzb((Object) status, (Object) "Result must not be null");
        PendingResult zzda = new zzda(Looper.getMainLooper());
        zzda.setResult(status);
        return zzda;
    }

    public static <R extends Result> PendingResult<R> zza(R r, GoogleApiClient googleApiClient) {
        zzbp.zzb((Object) r, (Object) "Result must not be null");
        zzbp.zzb(!r.getStatus().isSuccess(), (Object) "Status code must not be SUCCESS");
        PendingResult zzb = new zzb(googleApiClient, r);
        zzb.setResult(r);
        return zzb;
    }

    public static PendingResult<Status> zza(Status status, GoogleApiClient googleApiClient) {
        zzbp.zzb((Object) status, (Object) "Result must not be null");
        PendingResult zzda = new zzda(googleApiClient);
        zzda.setResult(status);
        return zzda;
    }

    public static <R extends Result> OptionalPendingResult<R> zzb(R r, GoogleApiClient googleApiClient) {
        zzbp.zzb((Object) r, (Object) "Result must not be null");
        PendingResult zzc = new zzc(googleApiClient);
        zzc.setResult(r);
        return new zzcp(zzc);
    }
}
