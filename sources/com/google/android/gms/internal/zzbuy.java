package com.google.android.gms.internal;

import android.os.RemoteException;

public final class zzbuy extends zzbut<String> {
    public zzbuy(int i, String str, String str2) {
        super(0, str, str2);
    }

    private final String zze(zzbvb zzbvb) {
        try {
            return zzbvb.getStringFlagValue(getKey(), (String) zzik(), getSource());
        } catch (RemoteException e) {
            return (String) zzik();
        }
    }

    public final /* synthetic */ Object zza(zzbvb zzbvb) {
        return zze(zzbvb);
    }
}
