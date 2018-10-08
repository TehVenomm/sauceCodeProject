package com.google.android.gms.auth.api.accounttransfer;

import android.os.RemoteException;
import com.google.android.gms.internal.zzaru;
import com.google.android.gms.internal.zzasc;

final class zzi extends zzc {
    private /* synthetic */ zzasc zzdza;

    zzi(AccountTransferClient accountTransferClient, zzasc zzasc) {
        this.zzdza = zzasc;
        super();
    }

    protected final void zza(zzaru zzaru) throws RemoteException {
        zzaru.zza(this.zzdze, this.zzdza);
    }
}
