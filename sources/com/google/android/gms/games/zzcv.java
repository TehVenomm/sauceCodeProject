package com.google.android.gms.games;

import android.support.annotation.NonNull;
import com.google.android.gms.common.api.ApiException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.ApiExceptionUtil;
import com.google.android.gms.games.TurnBasedMultiplayerClient.MatchOutOfDateApiException;
import com.google.android.gms.games.internal.zzbl;
import com.google.android.gms.games.multiplayer.turnbased.TurnBasedMatch;

final class zzcv implements zzbl<TurnBasedMatch> {
    zzcv() {
    }

    public final /* synthetic */ ApiException zza(@NonNull Status status, @NonNull Object obj) {
        return status.getStatusCode() == 26593 ? new MatchOutOfDateApiException(status, (TurnBasedMatch) obj) : ApiExceptionUtil.fromStatus(status);
    }
}
