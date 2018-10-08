package com.google.android.gms.internal;

import android.content.SharedPreferences.Editor;
import android.support.annotation.WorkerThread;
import com.google.android.gms.common.internal.zzbp;

public final class zzccc {
    private final String zzbfl;
    private long zzdmy;
    private boolean zzira;
    private /* synthetic */ zzcbz zzirb;
    private final long zzirc;

    public zzccc(zzcbz zzcbz, String str, long j) {
        this.zzirb = zzcbz;
        zzbp.zzgf(str);
        this.zzbfl = str;
        this.zzirc = j;
    }

    @WorkerThread
    public final long get() {
        if (!this.zzira) {
            this.zzira = true;
            this.zzdmy = this.zzirb.zzdtw.getLong(this.zzbfl, this.zzirc);
        }
        return this.zzdmy;
    }

    @WorkerThread
    public final void set(long j) {
        Editor edit = this.zzirb.zzdtw.edit();
        edit.putLong(this.zzbfl, j);
        edit.apply();
        this.zzdmy = j;
    }
}
