package com.google.android.gms.internal;

import android.os.RemoteException;

public final class zzbuv extends zzbut<Boolean> {
    public zzbuv(int i, String str, Boolean bool) {
        super(0, str, bool);
    }

    private final Boolean zzb(zzbvb zzbvb) {
        try {
            return Boolean.valueOf(zzbvb.getBooleanFlagValue(getKey(), ((Boolean) zzik()).booleanValue(), getSource()));
        } catch (RemoteException e) {
            return (Boolean) zzik();
        }
    }

    public final /* synthetic */ Object zza(zzbvb zzbvb) {
        return zzb(zzbvb);
    }
}
