package com.google.android.gms.internal;

import android.content.SharedPreferences.Editor;
import android.support.annotation.WorkerThread;
import com.google.android.gms.common.internal.zzbp;

public final class zzccb {
    private final String zzbfl;
    private boolean zzfgk;
    private final boolean zziqz = true;
    private boolean zzira;
    private /* synthetic */ zzcbz zzirb;

    public zzccb(zzcbz zzcbz, String str, boolean z) {
        this.zzirb = zzcbz;
        zzbp.zzgf(str);
        this.zzbfl = str;
    }

    @WorkerThread
    public final boolean get() {
        if (!this.zzira) {
            this.zzira = true;
            this.zzfgk = this.zzirb.zzdtw.getBoolean(this.zzbfl, this.zziqz);
        }
        return this.zzfgk;
    }

    @WorkerThread
    public final void set(boolean z) {
        Editor edit = this.zzirb.zzdtw.edit();
        edit.putBoolean(this.zzbfl, z);
        edit.apply();
        this.zzfgk = z;
    }
}
