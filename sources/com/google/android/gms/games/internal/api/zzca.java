package com.google.android.gms.games.internal.api;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.games.request.Requests.UpdateRequestsResult;
import java.util.Set;

final class zzca implements UpdateRequestsResult {
    private /* synthetic */ Status zzeik;

    zzca(zzbz zzbz, Status status) {
        this.zzeik = status;
    }

    public final Set<String> getRequestIds() {
        return null;
    }

    public final int getRequestOutcome(String str) {
        String valueOf = String.valueOf(str);
        throw new IllegalArgumentException(valueOf.length() != 0 ? "Unknown request ID ".concat(valueOf) : new String("Unknown request ID "));
    }

    public final Status getStatus() {
        return this.zzeik;
    }

    public final void release() {
    }
}
