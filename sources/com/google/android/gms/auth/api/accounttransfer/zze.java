package com.google.android.gms.auth.api.accounttransfer;

import android.os.RemoteException;
import com.google.android.gms.internal.zzaru;
import com.google.android.gms.internal.zzary;

final class zze extends zzb<byte[]> {
    private /* synthetic */ zzary zzdyw;

    zze(AccountTransferClient accountTransferClient, zzary zzary) {
        this.zzdyw = zzary;
        super();
    }

    protected final void zza(zzaru zzaru) throws RemoteException {
        zzaru.zza(new zzf(this, this), this.zzdyw);
    }
}
