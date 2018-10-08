package com.google.android.gms.flags.impl;

import android.content.SharedPreferences;
import java.util.concurrent.Callable;

final class zzc implements Callable<Boolean> {
    private /* synthetic */ SharedPreferences zzhat;
    private /* synthetic */ String zzhau;
    private /* synthetic */ Boolean zzhav;

    zzc(SharedPreferences sharedPreferences, String str, Boolean bool) {
        this.zzhat = sharedPreferences;
        this.zzhau = str;
        this.zzhav = bool;
    }

    public final /* synthetic */ Object call() throws Exception {
        return Boolean.valueOf(this.zzhat.getBoolean(this.zzhau, this.zzhav.booleanValue()));
    }
}
