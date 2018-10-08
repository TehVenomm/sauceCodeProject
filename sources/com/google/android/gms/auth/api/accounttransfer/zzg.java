package com.google.android.gms.auth.api.accounttransfer;

import android.os.RemoteException;
import com.google.android.gms.internal.zzarq;
import com.google.android.gms.internal.zzaru;

final class zzg extends zzb<DeviceMetaData> {
    private /* synthetic */ zzarq zzdyy;

    zzg(AccountTransferClient accountTransferClient, zzarq zzarq) {
        this.zzdyy = zzarq;
        super();
    }

    protected final void zza(zzaru zzaru) throws RemoteException {
        zzaru.zza(new zzh(this, this), this.zzdyy);
    }
}
