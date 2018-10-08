package com.google.android.gms.internal;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.support.annotation.MainThread;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.Size;
import android.support.annotation.WorkerThread;
import android.support.v4.util.ArrayMap;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzd;
import com.google.android.gms.measurement.AppMeasurement.zza;
import com.google.android.gms.measurement.AppMeasurement.zzb;
import java.util.Iterator;
import java.util.Map;
import java.util.concurrent.CopyOnWriteArrayList;

public final class zzcec extends zzcdm {
    protected zzcef zziva;
    private volatile zzb zzivb;
    private zzb zzivc;
    private long zzivd;
    private final Map<Activity, zzcef> zzive = new ArrayMap();
    private final CopyOnWriteArrayList<zza> zzivf = new CopyOnWriteArrayList();
    private boolean zzivg;
    private zzb zzivh;
    private String zzivi;

    public zzcec(zzcco zzcco) {
        super(zzcco);
    }

    @MainThread
    private final void zza(Activity activity, zzcef zzcef, boolean z) {
        int i = 1;
        zzb zzb = this.zzivb != null ? this.zzivb : (this.zzivc == null || Math.abs(zzvu().elapsedRealtime() - this.zzivd) >= 1000) ? null : this.zzivc;
        zzb = zzb != null ? new zzb(zzb) : null;
        this.zzivg = true;
        try {
            Iterator it = this.zzivf.iterator();
            while (it.hasNext()) {
                try {
                    i = ((zza) it.next()).zza(zzb, zzcef) & i;
                } catch (Exception e) {
                    zzauk().zzayc().zzj("onScreenChangeCallback threw exception", e);
                }
            }
        } catch (Exception e2) {
            zzauk().zzayc().zzj("onScreenChangeCallback loop threw exception", e2);
        } finally {
            this.zzivg = false;
        }
        zzb zzb2 = this.zzivb == null ? this.zzivc : this.zzivb;
        if (i != 0) {
            if (zzcef.zzikh == null) {
                zzcef.zzikh = zzjt(activity.getClass().getCanonicalName());
            }
            zzb = new zzcef(zzcef);
            this.zzivc = this.zzivb;
            this.zzivd = zzvu().elapsedRealtime();
            this.zzivb = zzb;
            zzauj().zzg(new zzced(this, z, zzb2, zzb));
        }
    }

    @WorkerThread
    private final void zza(@NonNull zzcef zzcef) {
        zzatw().zzaj(zzvu().elapsedRealtime());
        if (zzaui().zzbr(zzcef.zzivo)) {
            zzcef.zzivo = false;
        }
    }

    public static void zza(zzb zzb, Bundle bundle) {
        if (bundle != null && zzb != null && !bundle.containsKey("_sc")) {
            if (zzb.zzikg != null) {
                bundle.putString("_sn", zzb.zzikg);
            }
            bundle.putString("_sc", zzb.zzikh);
            bundle.putLong("_si", zzb.zziki);
        }
    }

    private static String zzjt(String str) {
        String[] split = str.split("\\.");
        if (split.length == 0) {
            return str.substring(0, 36);
        }
        String str2 = split[split.length - 1];
        return str2.length() > 36 ? str2.substring(0, 36) : str2;
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    @MainThread
    public final void onActivityDestroyed(Activity activity) {
        this.zzive.remove(activity);
    }

    @MainThread
    public final void onActivityPaused(Activity activity) {
        zzcef zzq = zzq(activity);
        this.zzivc = this.zzivb;
        this.zzivd = zzvu().elapsedRealtime();
        this.zzivb = null;
        zzauj().zzg(new zzcee(this, zzq));
    }

    @MainThread
    public final void onActivityResumed(Activity activity) {
        zza(activity, zzq(activity), false);
        zzcdl zzatw = zzatw();
        zzatw.zzauj().zzg(new zzcai(zzatw, zzatw.zzvu().elapsedRealtime()));
    }

    @MainThread
    public final void onActivitySaveInstanceState(Activity activity, Bundle bundle) {
        if (bundle != null) {
            zzcef zzcef = (zzcef) this.zzive.get(activity);
            if (zzcef != null) {
                Bundle bundle2 = new Bundle();
                bundle2.putLong("id", zzcef.zziki);
                bundle2.putString("name", zzcef.zzikg);
                bundle2.putString("referrer_name", zzcef.zzikh);
                bundle.putBundle("com.google.firebase.analytics.screen_service", bundle2);
            }
        }
    }

    @MainThread
    public final void registerOnScreenChangeCallback(@NonNull zza zza) {
        zzatu();
        if (zza == null) {
            zzauk().zzaye().log("Attempting to register null OnScreenChangeCallback");
            return;
        }
        this.zzivf.remove(zza);
        this.zzivf.add(zza);
    }

    @MainThread
    public final void setCurrentScreen(@NonNull Activity activity, @Nullable @Size(max = 36, min = 1) String str, @Nullable @Size(max = 36, min = 1) String str2) {
        if (activity == null) {
            zzauk().zzaye().log("setCurrentScreen must be called with a non-null activity");
            return;
        }
        zzauj();
        if (!zzccj.zzaq()) {
            zzauk().zzaye().log("setCurrentScreen must be called from the main thread");
        } else if (this.zzivg) {
            zzauk().zzaye().log("Cannot call setCurrentScreen from onScreenChangeCallback");
        } else if (this.zzivb == null) {
            zzauk().zzaye().log("setCurrentScreen cannot be called while no activity active");
        } else if (this.zzive.get(activity) == null) {
            zzauk().zzaye().log("setCurrentScreen must be called with an activity in the activity lifecycle");
        } else {
            if (str2 == null) {
                str2 = zzjt(activity.getClass().getCanonicalName());
            }
            boolean equals = this.zzivb.zzikh.equals(str2);
            boolean zzau = zzcfo.zzau(this.zzivb.zzikg, str);
            if (equals && zzau) {
                zzauk().zzayf().log("setCurrentScreen cannot be called with the same class and name");
            } else if (str != null && (str.length() <= 0 || str.length() > zzcap.zzavp())) {
                zzauk().zzaye().zzj("Invalid screen name length in setCurrentScreen. Length", Integer.valueOf(str.length()));
            } else if (str2 == null || (str2.length() > 0 && str2.length() <= zzcap.zzavp())) {
                Object obj;
                zzcbq zzayi = zzauk().zzayi();
                if (str == null) {
                    obj = "null";
                } else {
                    String str3 = str;
                }
                zzayi.zze("Setting current screen to name, class", obj, str2);
                zzcef zzcef = new zzcef(str, str2, zzaug().zzazw());
                this.zzive.put(activity, zzcef);
                zza(activity, zzcef, true);
            } else {
                zzauk().zzaye().zzj("Invalid class name length in setCurrentScreen. Length", Integer.valueOf(str2.length()));
            }
        }
    }

    @MainThread
    public final void unregisterOnScreenChangeCallback(@NonNull zza zza) {
        zzatu();
        this.zzivf.remove(zza);
    }

    @WorkerThread
    public final void zza(String str, zzb zzb) {
        zzug();
        synchronized (this) {
            if (this.zzivi == null || this.zzivi.equals(str) || zzb != null) {
                this.zzivi = str;
                this.zzivh = zzb;
            }
        }
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
    public final zzcef zzazm() {
        zzwh();
        zzug();
        return this.zziva;
    }

    public final zzb zzazn() {
        zzatu();
        zzb zzb = this.zzivb;
        return zzb == null ? null : new zzb(zzb);
    }

    @MainThread
    final zzcef zzq(@NonNull Activity activity) {
        zzbp.zzu(activity);
        zzcef zzcef = (zzcef) this.zzive.get(activity);
        if (zzcef != null) {
            return zzcef;
        }
        zzcef = new zzcef(null, zzjt(activity.getClass().getCanonicalName()), zzaug().zzazw());
        this.zzive.put(activity, zzcef);
        return zzcef;
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
