package com.google.android.gms.internal;

import android.content.SharedPreferences.Editor;
import android.support.annotation.WorkerThread;
import com.google.android.gms.common.internal.zzbp;

public final class zzcce {
    private String mValue;
    private final String zzbfl;
    private boolean zzira;
    private /* synthetic */ zzcbz zzirb;
    private final String zzirg = null;

    public zzcce(zzcbz zzcbz, String str, String str2) {
        this.zzirb = zzcbz;
        zzbp.zzgf(str);
        this.zzbfl = str;
    }

    @WorkerThread
    public final String zzayq() {
        if (!this.zzira) {
            this.zzira = true;
            this.mValue = this.zzirb.zzdtw.getString(this.zzbfl, null);
        }
        return this.mValue;
    }

    @WorkerThread
    public final void zzjl(String str) {
        if (!zzcfo.zzau(str, this.mValue)) {
            Editor edit = this.zzirb.zzdtw.edit();
            edit.putString(this.zzbfl, str);
            edit.apply();
            this.mValue = str;
        }
    }
}
