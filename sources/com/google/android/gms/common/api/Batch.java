package com.google.android.gms.common.api;

import com.google.android.gms.common.api.internal.zzs;
import java.util.ArrayList;
import java.util.List;

public final class Batch extends zzs<BatchResult> {
    private final Object mLock;
    private int zzfge;
    private boolean zzfgf;
    private boolean zzfgg;
    private final PendingResult<?>[] zzfgh;

    public static final class Builder {
        private GoogleApiClient zzeoz;
        private List<PendingResult<?>> zzfgj = new ArrayList();

        public Builder(GoogleApiClient googleApiClient) {
            this.zzeoz = googleApiClient;
        }

        public final <R extends Result> BatchResultToken<R> add(PendingResult<R> pendingResult) {
            BatchResultToken<R> batchResultToken = new BatchResultToken(this.zzfgj.size());
            this.zzfgj.add(pendingResult);
            return batchResultToken;
        }

        public final Batch build() {
            return new Batch(this.zzfgj, this.zzeoz);
        }
    }

    private Batch(List<PendingResult<?>> list, GoogleApiClient googleApiClient) {
        super(googleApiClient);
        this.mLock = new Object();
        this.zzfge = list.size();
        this.zzfgh = new PendingResult[this.zzfge];
        if (list.isEmpty()) {
            setResult(new BatchResult(Status.zzfhp, this.zzfgh));
            return;
        }
        for (int i = 0; i < list.size(); i++) {
            PendingResult pendingResult = (PendingResult) list.get(i);
            this.zzfgh[i] = pendingResult;
            pendingResult.zza(new zzb(this));
        }
    }

    public final void cancel() {
        super.cancel();
        for (PendingResult cancel : this.zzfgh) {
            cancel.cancel();
        }
    }

    public final BatchResult createFailedResult(Status status) {
        return new BatchResult(status, this.zzfgh);
    }

    public final /* synthetic */ Result zzb(Status status) {
        return createFailedResult(status);
    }
}
