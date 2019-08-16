package com.google.android.gms.internal.games;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.games.request.Requests.UpdateRequestsResult;
import java.util.Set;

final class zzcg implements UpdateRequestsResult {
    private final /* synthetic */ Status zzbd;

    zzcg(zzcf zzcf, Status status) {
        this.zzbd = status;
    }

    public final Set<String> getRequestIds() {
        return null;
    }

    public final int getRequestOutcome(String str) {
        String valueOf = String.valueOf(str);
        throw new IllegalArgumentException(valueOf.length() != 0 ? "Unknown request ID ".concat(valueOf) : new String("Unknown request ID "));
    }

    public final Status getStatus() {
        return this.zzbd;
    }

    public final void release() {
    }
}
