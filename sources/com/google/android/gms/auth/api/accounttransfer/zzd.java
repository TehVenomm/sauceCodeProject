package com.google.android.gms.auth.api.accounttransfer;

import android.os.RemoteException;
import com.google.android.gms.internal.zzaru;
import com.google.android.gms.internal.zzasa;

final class zzd extends zzc {
    private /* synthetic */ zzasa zzdyv;

    zzd(AccountTransferClient accountTransferClient, zzasa zzasa) {
        this.zzdyv = zzasa;
        super();
    }

    protected final void zza(zzaru zzaru) throws RemoteException {
        zzaru.zza(this.zzdze, this.zzdyv);
    }
}
