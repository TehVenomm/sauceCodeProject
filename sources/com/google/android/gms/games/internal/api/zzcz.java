package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;
import com.google.android.gms.games.multiplayer.ParticipantResult;

final class zzcz extends zzdp {
    private /* synthetic */ String zzhif;
    private /* synthetic */ byte[] zzhih;
    private /* synthetic */ String zzhii;
    private /* synthetic */ ParticipantResult[] zzhij;

    zzcz(zzcu zzcu, GoogleApiClient googleApiClient, String str, byte[] bArr, String str2, ParticipantResult[] participantResultArr) {
        this.zzhif = str;
        this.zzhih = bArr;
        this.zzhii = str2;
        this.zzhij = participantResultArr;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zza((zzn) this, this.zzhif, this.zzhih, this.zzhii, this.zzhij);
    }
}
