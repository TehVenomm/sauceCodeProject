package com.google.android.gms.flags.impl;

import android.content.SharedPreferences;
import java.util.concurrent.Callable;

final class zzi implements Callable<String> {
    private /* synthetic */ SharedPreferences zzhat;
    private /* synthetic */ String zzhau;
    private /* synthetic */ String zzhay;

    zzi(SharedPreferences sharedPreferences, String str, String str2) {
        this.zzhat = sharedPreferences;
        this.zzhau = str;
        this.zzhay = str2;
    }

    public final /* synthetic */ Object call() throws Exception {
        return this.zzhat.getString(this.zzhau, this.zzhay);
    }
}
