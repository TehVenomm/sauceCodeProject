package com.google.android.gms.internal;

import android.os.RemoteException;

public final class zzbuw extends zzbut<Integer> {
    public zzbuw(int i, String str, Integer num) {
        super(0, str, num);
    }

    private final Integer zzc(zzbvb zzbvb) {
        try {
            return Integer.valueOf(zzbvb.getIntFlagValue(getKey(), ((Integer) zzik()).intValue(), getSource()));
        } catch (RemoteException e) {
            return (Integer) zzik();
        }
    }

    public final /* synthetic */ Object zza(zzbvb zzbvb) {
        return zzc(zzbvb);
    }
}
