package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.WorkerThread;
import android.support.v4.util.ArrayMap;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzd;
import com.google.android.gms.measurement.AppMeasurement.zzb;
import java.util.Map;

public final class zzcaf extends zzcdl {
    private final Map<String, Long> zzikn = new ArrayMap();
    private final Map<String, Integer> zziko = new ArrayMap();
    private long zzikp;

    public zzcaf(zzcco zzcco) {
        super(zzcco);
    }

    @WorkerThread
    private final void zza(long j, zzb zzb) {
        if (zzb == null) {
            zzauk().zzayi().log("Not logging ad exposure. No active activity");
        } else if (j < 1000) {
            zzauk().zzayi().zzj("Not logging ad exposure. Less than 1000 ms. exposure", Long.valueOf(j));
        } else {
            Bundle bundle = new Bundle();
            bundle.putLong("_xt", j);
            zzcec.zza(zzb, bundle);
            zzaty().zzc("am", "_xa", bundle);
        }
    }

    @WorkerThread
    private final void zza(String str, long j, zzb zzb) {
        if (zzb == null) {
            zzauk().zzayi().log("Not logging ad unit exposure. No active activity");
        } else if (j < 1000) {
            zzauk().zzayi().zzj("Not logging ad unit exposure. Less than 1000 ms. exposure", Long.valueOf(j));
        } else {
            Bundle bundle = new Bundle();
            bundle.putString("_ai", str);
            bundle.putLong("_xt", j);
            zzcec.zza(zzb, bundle);
            zzaty().zzc("am", "_xu", bundle);
        }
    }

    @WorkerThread
    private final void zzak(long j) {
        for (String put : this.zzikn.keySet()) {
            this.zzikn.put(put, Long.valueOf(j));
        }
        if (!this.zzikn.isEmpty()) {
            this.zzikp = j;
        }
    }

    @WorkerThread
    private final void zzd(String str, long j) {
        zzatu();
        zzug();
        zzbp.zzgf(str);
        if (this.zziko.isEmpty()) {
            this.zzikp = j;
        }
        Integer num = (Integer) this.zziko.get(str);
        if (num != null) {
            this.zziko.put(str, Integer.valueOf(num.intValue() + 1));
        } else if (this.zziko.size() >= 100) {
            zzauk().zzaye().log("Too many ads visible");
        } else {
            this.zziko.put(str, Integer.valueOf(1));
            this.zzikn.put(str, Long.valueOf(j));
        }
    }

    @WorkerThread
    private final void zze(String str, long j) {
        zzatu();
        zzug();
        zzbp.zzgf(str);
        Integer num = (Integer) this.zziko.get(str);
        if (num != null) {
            zzb zzazm = zzauc().zzazm();
            int intValue = num.intValue() - 1;
            if (intValue == 0) {
                this.zziko.remove(str);
                Long l = (Long) this.zzikn.get(str);
                if (l == null) {
                    zzauk().zzayc().log("First ad unit exposure time was never set");
                } else {
                    long longValue = l.longValue();
                    this.zzikn.remove(str);
                    zza(str, j - longValue, zzazm);
                }
                if (!this.zziko.isEmpty()) {
                    return;
                }
                if (this.zzikp == 0) {
                    zzauk().zzayc().log("First ad exposure time was never set");
                    return;
                }
                zza(j - this.zzikp, zzazm);
                this.zzikp = 0;
                return;
            }
            this.zziko.put(str, Integer.valueOf(intValue));
            return;
        }
        zzauk().zzayc().zzj("Call to endAdUnitExposure for unknown ad unit id", str);
    }

    public final void beginAdUnitExposure(String str) {
        if (str == null || str.length() == 0) {
            zzauk().zzayc().log("Ad unit id must be a non-empty string");
            return;
        }
        zzauj().zzg(new zzcag(this, str, zzvu().elapsedRealtime()));
    }

    public final void endAdUnitExposure(String str) {
        if (str == null || str.length() == 0) {
            zzauk().zzayc().log("Ad unit id must be a non-empty string");
            return;
        }
        zzauj().zzg(new zzcah(this, str, zzvu().elapsedRealtime()));
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    @WorkerThread
    public final void zzaj(long j) {
        zzb zzazm = zzauc().zzazm();
        for (String str : this.zzikn.keySet()) {
            zza(str, j - ((Long) this.zzikn.get(str)).longValue(), zzazm);
        }
        if (!this.zzikn.isEmpty()) {
            zza(j - this.zzikp, zzazm);
        }
        zzak(j);
    }

    public final /* bridge */ /* synthetic */ void zzatt() {
        super.zzatt();
    }

    public final /* bridge */ /* synthetic */ void zzatu() {
        super.zzatu();
    }

    public final /* bridge */ /* synthetic */ void zzatv() {
        super.zzatv();
    }

    public final /* bridge */ /* synthetic */ zzcaf zzatw() {
        return super.zzatw();
    }

    public final /* bridge */ /* synthetic */ zzcam zzatx() {
        return super.zzatx();
    }

    public final /* bridge */ /* synthetic */ zzcdo zzaty() {
        return super.zzaty();
    }

    public final /* bridge */ /* synthetic */ zzcbj zzatz() {
        return super.zzatz();
    }

    public final /* bridge */ /* synthetic */ zzcaw zzaua() {
        return super.zzaua();
    }

    public final /* bridge */ /* synthetic */ zzceg zzaub() {
        return super.zzaub();
    }

    public final /* bridge */ /* synthetic */ zzcec zzauc() {
        return super.zzauc();
    }

    public final /* bridge */ /* synthetic */ zzcbk zzaud() {
        return super.zzaud();
    }

    public final /* bridge */ /* synthetic */ zzcaq zzaue() {
        return super.zzaue();
    }

    public final /* bridge */ /* synthetic */ zzcbm zzauf() {
        return super.zzauf();
    }

    public final /* bridge */ /* synthetic */ zzcfo zzaug() {
        return super.zzaug();
    }

    public final /* bridge */ /* synthetic */ zzcci zzauh() {
        return super.zzauh();
    }

    public final /* bridge */ /* synthetic */ zzcfd zzaui() {
        return super.zzaui();
    }

    public final /* bridge */ /* synthetic */ zzccj zzauj() {
        return super.zzauj();
    }

    public final /* bridge */ /* synthetic */ zzcbo zzauk() {
        return super.zzauk();
    }

    public final /* bridge */ /* synthetic */ zzcbz zzaul() {
        return super.zzaul();
    }

    public final /* bridge */ /* synthetic */ zzcap zzaum() {
        return super.zzaum();
    }

    public final /* bridge */ /* synthetic */ void zzug() {
        super.zzug();
    }

    public final /* bridge */ /* synthetic */ zzd zzvu() {
        return super.zzvu();
    }
}
