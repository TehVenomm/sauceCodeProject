package com.google.android.gms.internal;

import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import android.support.annotation.Nullable;
import android.support.annotation.Size;
import android.text.TextUtils;
import com.facebook.appevents.AppEventsConstants;
import com.google.android.gms.common.api.internal.zzca;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzd;
import com.google.android.gms.common.util.zzq;
import com.google.android.gms.common.zze;
import java.lang.reflect.InvocationTargetException;
import org.apache.commons.lang3.time.DateUtils;

public final class zzcap extends zzcdl {
    private static String zzimj = String.valueOf(zze.GOOGLE_PLAY_SERVICES_VERSION_CODE / 1000).replaceAll("(\\d+)(\\d)(\\d\\d)", "$1.$2.$3");
    private Boolean zzdqt;

    zzcap(zzcco zzcco) {
        super(zzcco);
    }

    public static boolean zzaie() {
        return zzca.zzaie();
    }

    public static long zzauu() {
        return 11200;
    }

    static String zzavk() {
        return (String) zzcbe.zzins.get();
    }

    public static int zzavl() {
        return 25;
    }

    public static int zzavm() {
        return 40;
    }

    public static int zzavn() {
        return 24;
    }

    static int zzavo() {
        return 40;
    }

    static int zzavp() {
        return 100;
    }

    static int zzavq() {
        return 256;
    }

    static int zzavr() {
        return 1000;
    }

    public static int zzavs() {
        return 36;
    }

    public static int zzavt() {
        return 2048;
    }

    static int zzavu() {
        return 500;
    }

    public static long zzavv() {
        return (long) ((Integer) zzcbe.zzioc.get()).intValue();
    }

    public static long zzavw() {
        return (long) ((Integer) zzcbe.zzioe.get()).intValue();
    }

    static int zzavx() {
        return 25;
    }

    static int zzavy() {
        return 1000;
    }

    static int zzavz() {
        return 25;
    }

    static int zzawa() {
        return 1000;
    }

    static long zzawb() {
        return 15552000000L;
    }

    static long zzawc() {
        return 15552000000L;
    }

    static long zzawd() {
        return DateUtils.MILLIS_PER_HOUR;
    }

    static long zzawe() {
        return 60000;
    }

    static long zzawf() {
        return 61000;
    }

    static long zzawg() {
        return ((Long) zzcbe.zzioy.get()).longValue();
    }

    public static String zzawh() {
        return "google_app_measurement.db";
    }

    static String zzawi() {
        return "google_app_measurement_local.db";
    }

    public static boolean zzawj() {
        return false;
    }

    public static long zzawl() {
        return ((Long) zzcbe.zziov.get()).longValue();
    }

    public static long zzawm() {
        return ((Long) zzcbe.zzioq.get()).longValue();
    }

    public static long zzawn() {
        return ((Long) zzcbe.zzior.get()).longValue();
    }

    public static long zzawo() {
        return 1000;
    }

    public static long zzawp() {
        return Math.max(0, ((Long) zzcbe.zzinu.get()).longValue());
    }

    public static int zzawq() {
        return Math.max(0, ((Integer) zzcbe.zzioa.get()).intValue());
    }

    public static int zzawr() {
        return Math.max(1, ((Integer) zzcbe.zziob.get()).intValue());
    }

    public static int zzaws() {
        return 100000;
    }

    public static String zzawt() {
        return (String) zzcbe.zzioi.get();
    }

    public static long zzawu() {
        return ((Long) zzcbe.zzinv.get()).longValue();
    }

    public static long zzawv() {
        return Math.max(0, ((Long) zzcbe.zzioj.get()).longValue());
    }

    public static long zzaww() {
        return Math.max(0, ((Long) zzcbe.zziol.get()).longValue());
    }

    public static long zzawx() {
        return Math.max(0, ((Long) zzcbe.zziom.get()).longValue());
    }

    public static long zzawy() {
        return Math.max(0, ((Long) zzcbe.zzion.get()).longValue());
    }

    public static long zzawz() {
        return Math.max(0, ((Long) zzcbe.zzioo.get()).longValue());
    }

    public static long zzaxa() {
        return Math.max(0, ((Long) zzcbe.zziop.get()).longValue());
    }

    public static long zzaxb() {
        return ((Long) zzcbe.zziok.get()).longValue();
    }

    public static long zzaxc() {
        return Math.max(0, ((Long) zzcbe.zzios.get()).longValue());
    }

    public static long zzaxd() {
        return Math.max(0, ((Long) zzcbe.zziot.get()).longValue());
    }

    public static int zzaxe() {
        return Math.min(20, Math.max(0, ((Integer) zzcbe.zziou.get()).intValue()));
    }

    public static boolean zzaxg() {
        return ((Boolean) zzcbe.zzinr.get()).booleanValue();
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    public final long zza(String str, zzcbf<Long> zzcbf) {
        if (str == null) {
            return ((Long) zzcbf.get()).longValue();
        }
        Object zzap = zzauh().zzap(str, zzcbf.getKey());
        if (TextUtils.isEmpty(zzap)) {
            return ((Long) zzcbf.get()).longValue();
        }
        try {
            return ((Long) zzcbf.get(Long.valueOf(Long.valueOf(zzap).longValue()))).longValue();
        } catch (NumberFormatException e) {
            return ((Long) zzcbf.get()).longValue();
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

    public final boolean zzawk() {
        Boolean zzit = zzit("firebase_analytics_collection_deactivated");
        return zzit != null && zzit.booleanValue();
    }

    public final String zzaxf() {
        try {
            return (String) Class.forName("android.os.SystemProperties").getMethod("get", new Class[]{String.class, String.class}).invoke(null, new Object[]{"debug.firebase.analytics.app", ""});
        } catch (ClassNotFoundException e) {
            zzauk().zzayc().zzj("Could not find SystemProperties class", e);
        } catch (NoSuchMethodException e2) {
            zzauk().zzayc().zzj("Could not find SystemProperties.get() method", e2);
        } catch (IllegalAccessException e3) {
            zzauk().zzayc().zzj("Could not access SystemProperties.get()", e3);
        } catch (InvocationTargetException e4) {
            zzauk().zzayc().zzj("SystemProperties.get() threw an exception", e4);
        }
        return "";
    }

    public final int zzb(String str, zzcbf<Integer> zzcbf) {
        if (str == null) {
            return ((Integer) zzcbf.get()).intValue();
        }
        Object zzap = zzauh().zzap(str, zzcbf.getKey());
        if (TextUtils.isEmpty(zzap)) {
            return ((Integer) zzcbf.get()).intValue();
        }
        try {
            return ((Integer) zzcbf.get(Integer.valueOf(Integer.valueOf(zzap).intValue()))).intValue();
        } catch (NumberFormatException e) {
            return ((Integer) zzcbf.get()).intValue();
        }
    }

    public final int zzis(@Size(min = 1) String str) {
        return zzb(str, zzcbe.zziog);
    }

    @Nullable
    final Boolean zzit(@Size(min = 1) String str) {
        zzbp.zzgf(str);
        try {
            if (getContext().getPackageManager() == null) {
                zzauk().zzayc().log("Failed to load metadata: PackageManager is null");
                return null;
            }
            ApplicationInfo applicationInfo = zzbdp.zzcs(getContext()).getApplicationInfo(getContext().getPackageName(), 128);
            if (applicationInfo == null) {
                zzauk().zzayc().log("Failed to load metadata: ApplicationInfo is null");
                return null;
            } else if (applicationInfo.metaData != null) {
                return applicationInfo.metaData.containsKey(str) ? Boolean.valueOf(applicationInfo.metaData.getBoolean(str)) : null;
            } else {
                zzauk().zzayc().log("Failed to load metadata: Metadata bundle is null");
                return null;
            }
        } catch (NameNotFoundException e) {
            zzauk().zzayc().zzj("Failed to load metadata: Package name not found", e);
            return null;
        }
    }

    public final boolean zziu(String str) {
        return AppEventsConstants.EVENT_PARAM_VALUE_YES.equals(zzauh().zzap(str, "gaia_collection_enabled"));
    }

    public final /* bridge */ /* synthetic */ void zzug() {
        super.zzug();
    }

    public final /* bridge */ /* synthetic */ zzd zzvu() {
        return super.zzvu();
    }

    public final boolean zzxr() {
        if (this.zzdqt == null) {
            synchronized (this) {
                if (this.zzdqt == null) {
                    ApplicationInfo applicationInfo = getContext().getApplicationInfo();
                    String zzalk = zzq.zzalk();
                    if (applicationInfo != null) {
                        String str = applicationInfo.processName;
                        boolean z = str != null && str.equals(zzalk);
                        this.zzdqt = Boolean.valueOf(z);
                    }
                    if (this.zzdqt == null) {
                        this.zzdqt = Boolean.TRUE;
                        zzauk().zzayc().log("My process not in the list of running processes");
                    }
                }
            }
        }
        return this.zzdqt.booleanValue();
    }
}
