package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;
import com.google.android.gms.games.multiplayer.ParticipantResult;

final class zzda extends zzdp {
    private /* synthetic */ String zzhif;
    private /* synthetic */ byte[] zzhih;
    private /* synthetic */ ParticipantResult[] zzhij;

    zzda(zzcu zzcu, GoogleApiClient googleApiClient, String str, byte[] bArr, ParticipantResult[] participantResultArr) {
        this.zzhif = str;
        this.zzhih = bArr;
        this.zzhij = participantResultArr;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zza((zzn) this, this.zzhif, this.zzhih, this.zzhij);
    }
}
