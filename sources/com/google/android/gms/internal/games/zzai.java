package com.google.android.gms.internal.games;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.AnyClient;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.BaseImplementation.ResultHolder;
import com.google.android.gms.games.internal.zze;
import com.google.android.gms.games.multiplayer.Invitations.LoadInvitationsResult;

final class zzai extends zzaj {
    private final /* synthetic */ int zzkd;

    zzai(zzah zzah, GoogleApiClient googleApiClient, int i) {
        this.zzkd = i;
        super(googleApiClient, null);
    }

    /* access modifiers changed from: protected */
    public final /* synthetic */ void doExecute(AnyClient anyClient) throws RemoteException {
        ((zze) anyClient).zza((ResultHolder<LoadInvitationsResult>) this, this.zzkd);
    }
}