package com.google.android.gms.flags.impl;

import android.content.SharedPreferences;
import java.util.concurrent.Callable;

final class zzg implements Callable<Long> {
    private /* synthetic */ SharedPreferences zzhat;
    private /* synthetic */ String zzhau;
    private /* synthetic */ Long zzhax;

    zzg(SharedPreferences sharedPreferences, String str, Long l) {
        this.zzhat = sharedPreferences;
        this.zzhau = str;
        this.zzhax = l;
    }

    public final /* synthetic */ Object call() throws Exception {
        return Long.valueOf(this.zzhat.getLong(this.zzhau, this.zzhax.longValue()));
    }
}
