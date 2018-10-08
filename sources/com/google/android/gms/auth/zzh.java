package com.google.android.gms.auth;

import android.accounts.Account;
import android.os.Bundle;
import android.os.IBinder;
import android.os.RemoteException;
import com.google.android.gms.internal.zzei;
import java.io.IOException;

final class zzh implements zzi<Bundle> {
    private /* synthetic */ Account zzdxn;

    zzh(Account account) {
        this.zzdxn = account;
    }

    public final /* synthetic */ Object zzaa(IBinder iBinder) throws RemoteException, IOException, GoogleAuthException {
        return (Bundle) zzd.zzl(zzei.zza(iBinder).zza(this.zzdxn));
    }
}
