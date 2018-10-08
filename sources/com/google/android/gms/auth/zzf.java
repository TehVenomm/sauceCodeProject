package com.google.android.gms.auth;

import android.os.Bundle;
import android.os.IBinder;
import android.os.RemoteException;
import com.google.android.gms.internal.zzei;
import java.io.IOException;

final class zzf implements zzi<Void> {
    private /* synthetic */ Bundle val$extras;
    private /* synthetic */ String zzdxp;

    zzf(String str, Bundle bundle) {
        this.zzdxp = str;
        this.val$extras = bundle;
    }

    public final /* synthetic */ Object zzaa(IBinder iBinder) throws RemoteException, IOException, GoogleAuthException {
        Bundle bundle = (Bundle) zzd.zzl(zzei.zza(iBinder).zza(this.zzdxp, this.val$extras));
        String string = bundle.getString("Error");
        if (bundle.getBoolean("booleanResult")) {
            return null;
        }
        throw new GoogleAuthException(string);
    }
}
