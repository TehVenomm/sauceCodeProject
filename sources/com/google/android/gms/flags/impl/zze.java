package com.google.android.gms.flags.impl;

import android.content.SharedPreferences;
import java.util.concurrent.Callable;

final class zze implements Callable<Integer> {
    private /* synthetic */ SharedPreferences zzhat;
    private /* synthetic */ String zzhau;
    private /* synthetic */ Integer zzhaw;

    zze(SharedPreferences sharedPreferences, String str, Integer num) {
        this.zzhat = sharedPreferences;
        this.zzhau = str;
        this.zzhaw = num;
    }

    public final /* synthetic */ Object call() throws Exception {
        return Integer.valueOf(this.zzhat.getInt(this.zzhau, this.zzhaw.intValue()));
    }
}
