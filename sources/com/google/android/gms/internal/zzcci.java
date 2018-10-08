package com.google.android.gms.internal;

import android.content.ContentValues;
import android.content.Context;
import android.database.sqlite.SQLiteException;
import android.support.annotation.WorkerThread;
import android.support.v4.util.ArrayMap;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzd;
import com.google.android.gms.measurement.AppMeasurement.Event;
import com.google.android.gms.measurement.AppMeasurement.Param;
import com.google.android.gms.measurement.AppMeasurement.UserProperty;
import com.google.firebase.analytics.FirebaseAnalytics;
import java.io.IOException;
import java.util.Map;

public final class zzcci extends zzcdm {
    private final Map<String, Map<String, String>> zzirm = new ArrayMap();
    private final Map<String, Map<String, Boolean>> zzirn = new ArrayMap();
    private final Map<String, Map<String, Boolean>> zziro = new ArrayMap();
    private final Map<String, zzcfw> zzirp = new ArrayMap();
    private final Map<String, String> zzirq = new ArrayMap();

    zzcci(zzcco zzcco) {
        super(zzcco);
    }

    private static Map<String, String> zza(zzcfw zzcfw) {
        Map<String, String> arrayMap = new ArrayMap();
        if (!(zzcfw == null || zzcfw.zziyj == null)) {
            for (zzcfx zzcfx : zzcfw.zziyj) {
                if (zzcfx != null) {
                    arrayMap.put(zzcfx.key, zzcfx.value);
                }
            }
        }
        return arrayMap;
    }

    private final void zza(String str, zzcfw zzcfw) {
        Map arrayMap = new ArrayMap();
        Map arrayMap2 = new ArrayMap();
        if (!(zzcfw == null || zzcfw.zziyk == null)) {
            for (zzcfv zzcfv : zzcfw.zziyk) {
                if (zzcfv != null) {
                    String zzil = Event.zzil(zzcfv.name);
                    if (zzil != null) {
                        zzcfv.name = zzil;
                    }
                    arrayMap.put(zzcfv.name, zzcfv.zziyf);
                    arrayMap2.put(zzcfv.name, zzcfv.zziyg);
                }
            }
        }
        this.zzirn.put(str, arrayMap);
        this.zziro.put(str, arrayMap2);
    }

    @WorkerThread
    private final zzcfw zzc(String str, byte[] bArr) {
        if (bArr == null) {
            return new zzcfw();
        }
        zzegf zzh = zzegf.zzh(bArr, 0, bArr.length);
        zzego zzcfw = new zzcfw();
        try {
            zzcfw.zza(zzh);
            zzauk().zzayi().zze("Parsed config. version, gmp_app_id", zzcfw.zziyh, zzcfw.zziln);
            return zzcfw;
        } catch (IOException e) {
            zzauk().zzaye().zze("Unable to merge remote config. appId", zzcbo.zzjf(str), e);
            return new zzcfw();
        }
    }

    @WorkerThread
    private final void zzjm(String str) {
        zzwh();
        zzug();
        zzbp.zzgf(str);
        if (this.zzirp.get(str) == null) {
            byte[] zziy = zzaue().zziy(str);
            if (zziy == null) {
                this.zzirm.put(str, null);
                this.zzirn.put(str, null);
                this.zziro.put(str, null);
                this.zzirp.put(str, null);
                this.zzirq.put(str, null);
                return;
            }
            zzcfw zzc = zzc(str, zziy);
            this.zzirm.put(str, zza(zzc));
            zza(str, zzc);
            this.zzirp.put(str, zzc);
            this.zzirq.put(str, null);
        }
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    @WorkerThread
    final String zzap(String str, String str2) {
        zzug();
        zzjm(str);
        Map map = (Map) this.zzirm.get(str);
        return map != null ? (String) map.get(str2) : null;
    }

    @WorkerThread
    final boolean zzaq(String str, String str2) {
        zzug();
        zzjm(str);
        if (zzaug().zzkg(str) && zzcfo.zzkd(str2)) {
            return true;
        }
        if (zzaug().zzkh(str) && zzcfo.zzju(str2)) {
            return true;
        }
        Map map = (Map) this.zzirn.get(str);
        if (map == null) {
            return false;
        }
        Boolean bool = (Boolean) map.get(str2);
        return bool == null ? false : bool.booleanValue();
    }

    @WorkerThread
    final boolean zzar(String str, String str2) {
        zzug();
        zzjm(str);
        if (FirebaseAnalytics.Event.ECOMMERCE_PURCHASE.equals(str2)) {
            return true;
        }
        Map map = (Map) this.zziro.get(str);
        if (map == null) {
            return false;
        }
        Boolean bool = (Boolean) map.get(str2);
        return bool == null ? false : bool.booleanValue();
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

    @WorkerThread
    protected final boolean zzb(String str, byte[] bArr, String str2) {
        zzwh();
        zzug();
        zzbp.zzgf(str);
        zzego zzc = zzc(str, bArr);
        if (zzc == null) {
            return false;
        }
        zza(str, zzc);
        this.zzirp.put(str, zzc);
        this.zzirq.put(str, str2);
        this.zzirm.put(str, zza(zzc));
        zzcdl zzatx = zzatx();
        zzcfp[] zzcfpArr = zzc.zziyl;
        zzbp.zzu(zzcfpArr);
        for (zzcfp zzcfp : zzcfpArr) {
            for (zzcfq zzcfq : zzcfp.zzixg) {
                String zzil = Event.zzil(zzcfq.zzixj);
                if (zzil != null) {
                    zzcfq.zzixj = zzil;
                }
                for (zzcfr zzcfr : zzcfq.zzixk) {
                    String zzil2 = Param.zzil(zzcfr.zzixr);
                    if (zzil2 != null) {
                        zzcfr.zzixr = zzil2;
                    }
                }
            }
            for (zzcft zzcft : zzcfp.zzixf) {
                String zzil3 = UserProperty.zzil(zzcft.zzixy);
                if (zzil3 != null) {
                    zzcft.zzixy = zzil3;
                }
            }
        }
        zzatx.zzaue().zza(str, zzcfpArr);
        try {
            zzc.zziyl = null;
            byte[] bArr2 = new byte[zzc.zzbjo()];
            zzc.zza(zzegg.zzi(bArr2, 0, bArr2.length));
            bArr = bArr2;
        } catch (IOException e) {
            zzauk().zzaye().zze("Unable to serialize reduced-size config. Storing full config instead. appId", zzcbo.zzjf(str), e);
        }
        zzcdl zzaue = zzaue();
        zzbp.zzgf(str);
        zzaue.zzug();
        zzaue.zzwh();
        ContentValues contentValues = new ContentValues();
        contentValues.put("remote_config", bArr);
        try {
            if (((long) zzaue.getWritableDatabase().update("apps", contentValues, "app_id = ?", new String[]{str})) == 0) {
                zzaue.zzauk().zzayc().zzj("Failed to update remote config (got 0). appId", zzcbo.zzjf(str));
            }
        } catch (SQLiteException e2) {
            zzaue.zzauk().zzayc().zze("Error storing remote config. appId", zzcbo.zzjf(str), e2);
        }
        return true;
    }

    @WorkerThread
    protected final zzcfw zzjn(String str) {
        zzwh();
        zzug();
        zzbp.zzgf(str);
        zzjm(str);
        return (zzcfw) this.zzirp.get(str);
    }

    @WorkerThread
    protected final String zzjo(String str) {
        zzug();
        return (String) this.zzirq.get(str);
    }

    @WorkerThread
    protected final void zzjp(String str) {
        zzug();
        this.zzirq.put(str, null);
    }

    @WorkerThread
    final void zzjq(String str) {
        zzug();
        this.zzirp.remove(str);
    }

    public final /* bridge */ /* synthetic */ void zzug() {
        super.zzug();
    }

    protected final void zzuh() {
    }

    public final /* bridge */ /* synthetic */ zzd zzvu() {
        return super.zzvu();
    }
}
