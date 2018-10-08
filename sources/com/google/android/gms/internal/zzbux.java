package com.google.android.gms.internal;

import android.os.RemoteException;

public final class zzbux extends zzbut<Long> {
    public zzbux(int i, String str, Long l) {
        super(0, str, l);
    }

    private final Long zzd(zzbvb zzbvb) {
        try {
            return Long.valueOf(zzbvb.getLongFlagValue(getKey(), ((Long) zzik()).longValue(), getSource()));
        } catch (RemoteException e) {
            return (Long) zzik();
        }
    }

    public final /* synthetic */ Object zza(zzbvb zzbvb) {
        return zzd(zzbvb);
    }
}
