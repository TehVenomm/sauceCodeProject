package com.google.android.gms.internal;

import android.content.Context;
import android.text.TextUtils;
import android.util.Log;
import android.util.Pair;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzd;
import com.google.android.gms.measurement.AppMeasurement;

public final class zzcbo extends zzcdm {
    private final String zzfve = zzcap.zzavk();
    private final long zzikz = zzcap.zzauu();
    private final char zzipi;
    private final zzcbq zzipj;
    private final zzcbq zzipk;
    private final zzcbq zzipl;
    private final zzcbq zzipm;
    private final zzcbq zzipn;
    private final zzcbq zzipo;
    private final zzcbq zzipp;
    private final zzcbq zzipq;
    private final zzcbq zzipr;

    zzcbo(zzcco zzcco) {
        super(zzcco);
        if (zzaum().zzxr()) {
            zzcap.zzawj();
            this.zzipi = (char) 67;
        } else {
            zzcap.zzawj();
            this.zzipi = (char) 99;
        }
        this.zzipj = new zzcbq(this, 6, false, false);
        this.zzipk = new zzcbq(this, 6, true, false);
        this.zzipl = new zzcbq(this, 6, false, true);
        this.zzipm = new zzcbq(this, 5, false, false);
        this.zzipn = new zzcbq(this, 5, true, false);
        this.zzipo = new zzcbq(this, 5, false, true);
        this.zzipp = new zzcbq(this, 4, false, false);
        this.zzipq = new zzcbq(this, 3, false, false);
        this.zzipr = new zzcbq(this, 2, false, false);
    }

    private static String zza(boolean z, String str, Object obj, Object obj2, Object obj3) {
        if (str == null) {
            Object obj4 = "";
        }
        Object zzc = zzc(z, obj);
        Object zzc2 = zzc(z, obj2);
        Object zzc3 = zzc(z, obj3);
        StringBuilder stringBuilder = new StringBuilder();
        String str2 = "";
        if (!TextUtils.isEmpty(obj4)) {
            stringBuilder.append(obj4);
            str2 = ": ";
        }
        if (!TextUtils.isEmpty(zzc)) {
            stringBuilder.append(str2);
            stringBuilder.append(zzc);
            str2 = ", ";
        }
        if (!TextUtils.isEmpty(zzc2)) {
            stringBuilder.append(str2);
            stringBuilder.append(zzc2);
            str2 = ", ";
        }
        if (!TextUtils.isEmpty(zzc3)) {
            stringBuilder.append(str2);
            stringBuilder.append(zzc3);
        }
        return stringBuilder.toString();
    }

    private static String zzc(boolean z, Object obj) {
        if (obj == null) {
            return "";
        }
        Object valueOf = obj instanceof Integer ? Long.valueOf((long) ((Integer) obj).intValue()) : obj;
        if (valueOf instanceof Long) {
            if (!z) {
                return String.valueOf(valueOf);
            }
            if (Math.abs(((Long) valueOf).longValue()) < 100) {
                return String.valueOf(valueOf);
            }
            String str = String.valueOf(valueOf).charAt(0) == '-' ? "-" : "";
            String valueOf2 = String.valueOf(Math.abs(((Long) valueOf).longValue()));
            return new StringBuilder((String.valueOf(str).length() + 43) + String.valueOf(str).length()).append(str).append(Math.round(Math.pow(10.0d, (double) (valueOf2.length() - 1)))).append("...").append(str).append(Math.round(Math.pow(10.0d, (double) valueOf2.length()) - 1.0d)).toString();
        } else if (valueOf instanceof Boolean) {
            return String.valueOf(valueOf);
        } else {
            if (!(valueOf instanceof Throwable)) {
                return valueOf instanceof zzcbr ? ((zzcbr) valueOf).zzgqc : z ? "-" : String.valueOf(valueOf);
            } else {
                Throwable th = (Throwable) valueOf;
                StringBuilder stringBuilder = new StringBuilder(z ? th.getClass().getName() : th.toString());
                String zzjg = zzjg(AppMeasurement.class.getCanonicalName());
                String zzjg2 = zzjg(zzcco.class.getCanonicalName());
                for (StackTraceElement stackTraceElement : th.getStackTrace()) {
                    if (!stackTraceElement.isNativeMethod()) {
                        String className = stackTraceElement.getClassName();
                        if (className != null) {
                            className = zzjg(className);
                            if (className.equals(zzjg) || className.equals(zzjg2)) {
                                stringBuilder.append(": ");
                                stringBuilder.append(stackTraceElement);
                                break;
                            }
                        } else {
                            continue;
                        }
                    }
                }
                return stringBuilder.toString();
            }
        }
    }

    protected static Object zzjf(String str) {
        return str == null ? null : new zzcbr(str);
    }

    private static String zzjg(String str) {
        if (TextUtils.isEmpty(str)) {
            return "";
        }
        int lastIndexOf = str.lastIndexOf(46);
        return lastIndexOf != -1 ? str.substring(0, lastIndexOf) : str;
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    protected final void zza(int i, boolean z, boolean z2, String str, Object obj, Object obj2, Object obj3) {
        if (!z && zzad(i)) {
            zzk(i, zza(false, str, obj, obj2, obj3));
        }
        if (!z2 && i >= 5) {
            zzbp.zzu(str);
            zzcdm zzayw = this.zzikb.zzayw();
            if (zzayw == null) {
                zzk(6, "Scheduler not set. Not logging error/warn");
            } else if (zzayw.isInitialized()) {
                int i2 = i < 0 ? 0 : i;
                if (i2 >= 9) {
                    i2 = 8;
                }
                char charAt = "01VDIWEA?".charAt(i2);
                char c = this.zzipi;
                long j = this.zzikz;
                String zza = zza(true, str, obj, obj2, obj3);
                String stringBuilder = new StringBuilder((String.valueOf("2").length() + 23) + String.valueOf(zza).length()).append("2").append(charAt).append(c).append(j).append(":").append(zza).toString();
                if (stringBuilder.length() > 1024) {
                    stringBuilder = str.substring(0, 1024);
                }
                zzayw.zzg(new zzcbp(this, stringBuilder));
            } else {
                zzk(6, "Scheduler not initialized. Not logging error/warn");
            }
        }
    }

    protected final boolean zzad(int i) {
        return Log.isLoggable(this.zzfve, i);
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

    public final zzcbq zzayc() {
        return this.zzipj;
    }

    public final zzcbq zzayd() {
        return this.zzipk;
    }

    public final zzcbq zzaye() {
        return this.zzipm;
    }

    public final zzcbq zzayf() {
        return this.zzipo;
    }

    public final zzcbq zzayg() {
        return this.zzipp;
    }

    public final zzcbq zzayh() {
        return this.zzipq;
    }

    public final zzcbq zzayi() {
        return this.zzipr;
    }

    public final String zzayj() {
        Pair zzzf = zzaul().zziqf.zzzf();
        if (zzzf == null || zzzf == zzcbz.zziqe) {
            return null;
        }
        String valueOf = String.valueOf(zzzf.second);
        String str = (String) zzzf.first;
        return new StringBuilder((String.valueOf(valueOf).length() + 1) + String.valueOf(str).length()).append(valueOf).append(":").append(str).toString();
    }

    protected final void zzk(int i, String str) {
        Log.println(i, this.zzfve, str);
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
