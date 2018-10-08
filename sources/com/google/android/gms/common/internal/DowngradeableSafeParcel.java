package com.google.android.gms.common.internal;

import com.google.android.gms.common.internal.safeparcel.zza;

public abstract class DowngradeableSafeParcel extends zza implements ReflectedParcelable {
    private static final Object zzfts = new Object();
    private static ClassLoader zzftt = null;
    private static Integer zzftu = null;
    private boolean zzftv = false;

    private static ClassLoader zzakb() {
        synchronized (zzfts) {
        }
        return null;
    }

    protected static Integer zzakc() {
        synchronized (zzfts) {
        }
        return null;
    }

    protected static boolean zzga(String str) {
        zzakb();
        return true;
    }
}
