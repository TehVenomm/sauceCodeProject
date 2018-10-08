package com.google.android.gms.auth;

import android.os.IBinder;
import android.os.RemoteException;
import com.google.android.gms.internal.zzei;
import java.io.IOException;
import java.util.List;

final class zzg implements zzi<List<AccountChangeEvent>> {
    private /* synthetic */ String zzdxq;
    private /* synthetic */ int zzdxr;

    zzg(String str, int i) {
        this.zzdxq = str;
        this.zzdxr = i;
    }

    public final /* synthetic */ Object zzaa(IBinder iBinder) throws RemoteException, IOException, GoogleAuthException {
        return ((AccountChangeEventsResponse) zzd.zzl(zzei.zza(iBinder).zza(new AccountChangeEventsRequest().setAccountName(this.zzdxq).setEventIndex(this.zzdxr)))).getEvents();
    }
}
