package com.google.android.gms.measurement.internal;

import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.res.Resources.NotFoundException;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.Size;
import android.support.annotation.WorkerThread;
import android.text.TextUtils;
import com.facebook.appevents.AppEventsConstants;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.util.Clock;
import com.google.android.gms.common.util.ProcessUtils;
import com.google.android.gms.common.util.VisibleForTesting;
import com.google.android.gms.common.wrappers.Wrappers;
import java.lang.reflect.InvocationTargetException;
import java.util.Arrays;
import java.util.List;

public final class zzs extends zzgf {
    private Boolean zzeb;
    @NonNull
    private zzu zzec = zzv.zzee;
    private Boolean zzed;

    zzs(zzfj zzfj) {
        super(zzfj);
        zzak.zza(zzfj);
    }

    static String zzbm() {
        return (String) zzak.zzgf.get(null);
    }

    @Nullable
    @VisibleForTesting
    private final Bundle zzbo() {
        Bundle bundle = null;
        try {
            if (getContext().getPackageManager() == null) {
                zzab().zzgk().zzao("Failed to load metadata: PackageManager is null");
                return bundle;
            }
            ApplicationInfo applicationInfo = Wrappers.packageManager(getContext()).getApplicationInfo(getContext().getPackageName(), 128);
            if (applicationInfo != null) {
                return applicationInfo.metaData;
            }
            zzab().zzgk().zzao("Failed to load metadata: ApplicationInfo is null");
            return bundle;
        } catch (NameNotFoundException e) {
            zzab().zzgk().zza("Failed to load metadata: Package name not found", e);
            return bundle;
        }
    }

    public static long zzbs() {
        return ((Long) zzak.zzhi.get(null)).longValue();
    }

    public static long zzbt() {
        return ((Long) zzak.zzgi.get(null)).longValue();
    }

    public static boolean zzbv() {
        return ((Boolean) zzak.zzge.get(null)).booleanValue();
    }

    @WorkerThread
    static boolean zzbx() {
        return ((Boolean) zzak.zzhy.get(null)).booleanValue();
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    @WorkerThread
    public final long zza(String str, @NonNull zzdu<Long> zzdu) {
        if (str == null) {
            return ((Long) zzdu.get(null)).longValue();
        }
        String zzb = this.zzec.zzb(str, zzdu.getKey());
        if (TextUtils.isEmpty(zzb)) {
            return ((Long) zzdu.get(null)).longValue();
        }
        try {
            return ((Long) zzdu.get(Long.valueOf(Long.parseLong(zzb)))).longValue();
        } catch (NumberFormatException e) {
            return ((Long) zzdu.get(null)).longValue();
        }
    }

    /* access modifiers changed from: 0000 */
    public final void zza(@NonNull zzu zzu) {
        this.zzec = zzu;
    }

    public final boolean zza(zzdu<Boolean> zzdu) {
        return zzd(null, zzdu);
    }

    public final /* bridge */ /* synthetic */ zzfc zzaa() {
        return super.zzaa();
    }

    public final /* bridge */ /* synthetic */ zzef zzab() {
        return super.zzab();
    }

    public final /* bridge */ /* synthetic */ zzeo zzac() {
        return super.zzac();
    }

    public final /* bridge */ /* synthetic */ zzs zzad() {
        return super.zzad();
    }

    public final /* bridge */ /* synthetic */ zzr zzae() {
        return super.zzae();
    }

    public final long zzao() {
        zzae();
        return 16250;
    }

    @WorkerThread
    public final int zzb(String str, @NonNull zzdu<Integer> zzdu) {
        if (str == null) {
            return ((Integer) zzdu.get(null)).intValue();
        }
        String zzb = this.zzec.zzb(str, zzdu.getKey());
        if (TextUtils.isEmpty(zzb)) {
            return ((Integer) zzdu.get(null)).intValue();
        }
        try {
            return ((Integer) zzdu.get(Integer.valueOf(Integer.parseInt(zzb)))).intValue();
        } catch (NumberFormatException e) {
            return ((Integer) zzdu.get(null)).intValue();
        }
    }

    public final boolean zzbn() {
        if (this.zzed == null) {
            synchronized (this) {
                if (this.zzed == null) {
                    ApplicationInfo applicationInfo = getContext().getApplicationInfo();
                    String myProcessName = ProcessUtils.getMyProcessName();
                    if (applicationInfo != null) {
                        String str = applicationInfo.processName;
                        this.zzed = Boolean.valueOf(str != null && str.equals(myProcessName));
                    }
                    if (this.zzed == null) {
                        this.zzed = Boolean.TRUE;
                        zzab().zzgk().zzao("My process not in the list of running processes");
                    }
                }
            }
        }
        return this.zzed.booleanValue();
    }

    public final boolean zzbp() {
        zzae();
        Boolean zzj = zzj("firebase_analytics_collection_deactivated");
        return zzj != null && zzj.booleanValue();
    }

    public final Boolean zzbq() {
        zzae();
        return zzj("firebase_analytics_collection_enabled");
    }

    public final Boolean zzbr() {
        zzm();
        Boolean zzj = zzj("google_analytics_adid_collection_enabled");
        return Boolean.valueOf(zzj == null || zzj.booleanValue());
    }

    public final String zzbu() {
        try {
            return (String) Class.forName("android.os.SystemProperties").getMethod("get", new Class[]{String.class, String.class}).invoke(null, new Object[]{"debug.firebase.analytics.app", ""});
        } catch (ClassNotFoundException e) {
            zzab().zzgk().zza("Could not find SystemProperties class", e);
        } catch (NoSuchMethodException e2) {
            zzab().zzgk().zza("Could not find SystemProperties.get() method", e2);
        } catch (IllegalAccessException e3) {
            zzab().zzgk().zza("Could not access SystemProperties.get()", e3);
        } catch (InvocationTargetException e4) {
            zzab().zzgk().zza("SystemProperties.get() threw an exception", e4);
        }
        return "";
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final boolean zzbw() {
        if (this.zzeb == null) {
            this.zzeb = zzj("app_measurement_lite");
            if (this.zzeb == null) {
                this.zzeb = Boolean.valueOf(false);
            }
        }
        return this.zzeb.booleanValue() || !this.zzj.zzia();
    }

    @WorkerThread
    public final double zzc(String str, @NonNull zzdu<Double> zzdu) {
        if (str == null) {
            return ((Double) zzdu.get(null)).doubleValue();
        }
        String zzb = this.zzec.zzb(str, zzdu.getKey());
        if (TextUtils.isEmpty(zzb)) {
            return ((Double) zzdu.get(null)).doubleValue();
        }
        try {
            return ((Double) zzdu.get(Double.valueOf(Double.parseDouble(zzb)))).doubleValue();
        } catch (NumberFormatException e) {
            return ((Double) zzdu.get(null)).doubleValue();
        }
    }

    @WorkerThread
    public final boolean zzd(String str, @NonNull zzdu<Boolean> zzdu) {
        if (str == null) {
            return ((Boolean) zzdu.get(null)).booleanValue();
        }
        String zzb = this.zzec.zzb(str, zzdu.getKey());
        return TextUtils.isEmpty(zzb) ? ((Boolean) zzdu.get(null)).booleanValue() : ((Boolean) zzdu.get(Boolean.valueOf(Boolean.parseBoolean(zzb)))).booleanValue();
    }

    public final boolean zze(String str, zzdu<Boolean> zzdu) {
        return zzd(str, zzdu);
    }

    @WorkerThread
    public final int zzi(@Size(min = 1) String str) {
        return zzb(str, zzak.zzgt);
    }

    /* access modifiers changed from: 0000 */
    @Nullable
    @VisibleForTesting
    public final Boolean zzj(@Size(min = 1) String str) {
        Preconditions.checkNotEmpty(str);
        Bundle zzbo = zzbo();
        if (zzbo == null) {
            zzab().zzgk().zzao("Failed to load metadata: Metadata bundle is null");
            return null;
        } else if (zzbo.containsKey(str)) {
            return Boolean.valueOf(zzbo.getBoolean(str));
        } else {
            return null;
        }
    }

    /* access modifiers changed from: 0000 */
    @Nullable
    @VisibleForTesting
    public final List<String> zzk(@Size(min = 1) String str) {
        Integer valueOf;
        Preconditions.checkNotEmpty(str);
        Bundle zzbo = zzbo();
        if (zzbo == null) {
            zzab().zzgk().zzao("Failed to load metadata: Metadata bundle is null");
            valueOf = null;
        } else {
            valueOf = !zzbo.containsKey(str) ? null : Integer.valueOf(zzbo.getInt(str));
        }
        if (valueOf == null) {
            return null;
        }
        try {
            String[] stringArray = getContext().getResources().getStringArray(valueOf.intValue());
            if (stringArray != null) {
                return Arrays.asList(stringArray);
            }
            return null;
        } catch (NotFoundException e) {
            zzab().zzgk().zza("Failed to load string array from metadata: resource not found", e);
            return null;
        }
    }

    public final /* bridge */ /* synthetic */ void zzl() {
        super.zzl();
    }

    public final boolean zzl(String str) {
        return AppEventsConstants.EVENT_PARAM_VALUE_YES.equals(this.zzec.zzb(str, "gaia_collection_enabled"));
    }

    public final /* bridge */ /* synthetic */ void zzm() {
        super.zzm();
    }

    public final boolean zzm(String str) {
        return AppEventsConstants.EVENT_PARAM_VALUE_YES.equals(this.zzec.zzb(str, "measurement.event_sampling_enabled"));
    }

    public final /* bridge */ /* synthetic */ void zzn() {
        super.zzn();
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final boolean zzn(String str) {
        return zzd(str, zzak.zzhs);
    }

    public final /* bridge */ /* synthetic */ void zzo() {
        super.zzo();
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final boolean zzo(String str) {
        return zzd(str, zzak.zzhm);
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final String zzp(String str) {
        zzdu<String> zzdu = zzak.zzhn;
        return str == null ? (String) zzdu.get(null) : (String) zzdu.get(this.zzec.zzb(str, zzdu.getKey()));
    }

    /* access modifiers changed from: 0000 */
    public final boolean zzq(String str) {
        return zzd(str, zzak.zzht);
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final boolean zzr(String str) {
        return zzd(str, zzak.zzhu);
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final boolean zzs(String str) {
        return zzd(str, zzak.zzhv);
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final boolean zzt(String str) {
        return zzd(str, zzak.zzhx);
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final boolean zzu(String str) {
        return zzd(str, zzak.zzhw);
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final boolean zzv(String str) {
        return zzd(str, zzak.zzhz);
    }

    public final /* bridge */ /* synthetic */ zzac zzw() {
        return super.zzw();
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final boolean zzw(String str) {
        return zzd(str, zzak.zzia);
    }

    public final /* bridge */ /* synthetic */ Clock zzx() {
        return super.zzx();
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final boolean zzx(String str) {
        return zzd(str, zzak.zzib);
    }

    public final /* bridge */ /* synthetic */ zzed zzy() {
        return super.zzy();
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final boolean zzy(String str) {
        return zzd(str, zzak.zzic);
    }

    public final /* bridge */ /* synthetic */ zzjs zzz() {
        return super.zzz();
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final boolean zzz(String str) {
        return zzd(str, zzak.zzih);
    }
}
