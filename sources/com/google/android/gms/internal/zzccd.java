package com.google.android.gms.internal;

import android.content.SharedPreferences.Editor;
import android.support.annotation.WorkerThread;
import android.util.Pair;
import com.google.android.gms.common.internal.zzbp;

public final class zzccd {
    private final long zzdua;
    private /* synthetic */ zzcbz zzirb;
    private String zzird;
    private final String zzire;
    private final String zzirf;

    private zzccd(zzcbz zzcbz, String str, long j) {
        this.zzirb = zzcbz;
        zzbp.zzgf(str);
        zzbp.zzbh(j > 0);
        this.zzird = String.valueOf(str).concat(":start");
        this.zzire = String.valueOf(str).concat(":count");
        this.zzirf = String.valueOf(str).concat(":value");
        this.zzdua = j;
    }

    @WorkerThread
    private final void zzze() {
        this.zzirb.zzug();
        long currentTimeMillis = this.zzirb.zzvu().currentTimeMillis();
        Editor edit = this.zzirb.zzdtw.edit();
        edit.remove(this.zzire);
        edit.remove(this.zzirf);
        edit.putLong(this.zzird, currentTimeMillis);
        edit.apply();
    }

    @WorkerThread
    private final long zzzg() {
        return this.zzirb.zzayk().getLong(this.zzird, 0);
    }

    @WorkerThread
    public final void zzf(String str, long j) {
        this.zzirb.zzug();
        if (zzzg() == 0) {
            zzze();
        }
        if (str == null) {
            str = "";
        }
        long j2 = this.zzirb.zzdtw.getLong(this.zzire, 0);
        if (j2 <= 0) {
            Editor edit = this.zzirb.zzdtw.edit();
            edit.putString(this.zzirf, str);
            edit.putLong(this.zzire, 1);
            edit.apply();
            return;
        }
        Object obj = (this.zzirb.zzaug().zzazx().nextLong() & Long.MAX_VALUE) < Long.MAX_VALUE / (j2 + 1) ? 1 : null;
        Editor edit2 = this.zzirb.zzdtw.edit();
        if (obj != null) {
            edit2.putString(this.zzirf, str);
        }
        edit2.putLong(this.zzire, j2 + 1);
        edit2.apply();
    }

    @WorkerThread
    public final Pair<String, Long> zzzf() {
        this.zzirb.zzug();
        this.zzirb.zzug();
        long zzzg = zzzg();
        if (zzzg == 0) {
            zzze();
            zzzg = 0;
        } else {
            zzzg = Math.abs(zzzg - this.zzirb.zzvu().currentTimeMillis());
        }
        if (zzzg < this.zzdua) {
            return null;
        }
        if (zzzg > (this.zzdua << 1)) {
            zzze();
            return null;
        }
        String string = this.zzirb.zzayk().getString(this.zzirf, null);
        long j = this.zzirb.zzayk().getLong(this.zzire, 0);
        zzze();
        return (string == null || j <= 0) ? zzcbz.zziqe : new Pair(string, Long.valueOf(j));
    }
}
