package com.google.android.gms.auth.api.accounttransfer;

import android.os.RemoteException;
import com.google.android.gms.internal.zzaru;
import com.google.android.gms.internal.zzarw;

final class zzj extends zzc {
    private /* synthetic */ zzarw zzdzb;

    zzj(AccountTransferClient accountTransferClient, zzarw zzarw) {
        this.zzdzb = zzarw;
        super();
    }

    protected final void zza(zzaru zzaru) throws RemoteException {
        zzaru.zza(this.zzdze, this.zzdzb);
    }
}
