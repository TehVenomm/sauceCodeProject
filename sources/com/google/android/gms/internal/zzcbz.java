package com.google.android.gms.internal;

import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.os.Build.VERSION;
import android.support.annotation.NonNull;
import android.support.annotation.WorkerThread;
import android.text.TextUtils;
import android.util.Pair;
import com.google.android.gms.ads.identifier.AdvertisingIdClient;
import com.google.android.gms.ads.identifier.AdvertisingIdClient.Info;
import io.fabric.sdk.android.services.common.CommonUtils;
import java.math.BigInteger;
import java.util.Locale;

final class zzcbz extends zzcdm {
    static final Pair<String, Long> zziqe = new Pair("", Long.valueOf(0));
    private SharedPreferences zzdtw;
    public final zzccd zziqf = new zzccd(this, "health_monitor", zzcap.zzawp());
    public final zzccc zziqg = new zzccc(this, "last_upload", 0);
    public final zzccc zziqh = new zzccc(this, "last_upload_attempt", 0);
    public final zzccc zziqi = new zzccc(this, "backoff", 0);
    public final zzccc zziqj = new zzccc(this, "last_delete_stale", 0);
    public final zzccc zziqk = new zzccc(this, "midnight_offset", 0);
    public final zzccc zziql = new zzccc(this, "first_open_time", 0);
    public final zzcce zziqm = new zzcce(this, "app_instance_id", null);
    private String zziqn;
    private boolean zziqo;
    private long zziqp;
    private String zziqq;
    private long zziqr;
    private final Object zziqs = new Object();
    public final zzccc zziqt = new zzccc(this, "time_before_start", 10000);
    public final zzccc zziqu = new zzccc(this, "session_timeout", 1800000);
    public final zzccb zziqv = new zzccb(this, "start_new_session", true);
    public final zzccc zziqw = new zzccc(this, "last_pause_time", 0);
    public final zzccc zziqx = new zzccc(this, "time_active", 0);
    public boolean zziqy;

    zzcbz(zzcco zzcco) {
        super(zzcco);
    }

    @WorkerThread
    private final SharedPreferences zzayk() {
        zzug();
        zzwh();
        return this.zzdtw;
    }

    @WorkerThread
    final void setMeasurementEnabled(boolean z) {
        zzug();
        zzauk().zzayi().zzj("Setting measurementEnabled", Boolean.valueOf(z));
        Editor edit = zzayk().edit();
        edit.putBoolean("measurement_enabled", z);
        edit.apply();
    }

    @WorkerThread
    final String zzayl() {
        zzug();
        return zzayk().getString("gmp_app_id", null);
    }

    final String zzaym() {
        String str;
        synchronized (this.zziqs) {
            if (Math.abs(zzvu().elapsedRealtime() - this.zziqr) < 1000) {
                str = this.zziqq;
            } else {
                str = null;
            }
        }
        return str;
    }

    @WorkerThread
    final Boolean zzayn() {
        zzug();
        return !zzayk().contains("use_service") ? null : Boolean.valueOf(zzayk().getBoolean("use_service", false));
    }

    @WorkerThread
    final void zzayo() {
        boolean z = true;
        zzug();
        zzauk().zzayi().log("Clearing collection preferences.");
        boolean contains = zzayk().contains("measurement_enabled");
        if (contains) {
            z = zzbn(true);
        }
        Editor edit = zzayk().edit();
        edit.clear();
        edit.apply();
        if (contains) {
            setMeasurementEnabled(z);
        }
    }

    @WorkerThread
    protected final String zzayp() {
        zzug();
        String string = zzayk().getString("previous_os_version", null);
        zzaua().zzwh();
        String str = VERSION.RELEASE;
        if (!(TextUtils.isEmpty(str) || str.equals(string))) {
            Editor edit = zzayk().edit();
            edit.putString("previous_os_version", str);
            edit.apply();
        }
        return string;
    }

    @WorkerThread
    final void zzbm(boolean z) {
        zzug();
        zzauk().zzayi().zzj("Setting useService", Boolean.valueOf(z));
        Editor edit = zzayk().edit();
        edit.putBoolean("use_service", z);
        edit.apply();
    }

    @WorkerThread
    final boolean zzbn(boolean z) {
        zzug();
        return zzayk().getBoolean("measurement_enabled", z);
    }

    @WorkerThread
    @NonNull
    final Pair<String, Boolean> zzjh(String str) {
        zzug();
        long elapsedRealtime = zzvu().elapsedRealtime();
        if (this.zziqn != null && elapsedRealtime < this.zziqp) {
            return new Pair(this.zziqn, Boolean.valueOf(this.zziqo));
        }
        this.zziqp = elapsedRealtime + zzaum().zza(str, zzcbe.zzint);
        AdvertisingIdClient.setShouldSkipGmsCoreVersionCheck(true);
        try {
            Info advertisingIdInfo = AdvertisingIdClient.getAdvertisingIdInfo(getContext());
            if (advertisingIdInfo != null) {
                this.zziqn = advertisingIdInfo.getId();
                this.zziqo = advertisingIdInfo.isLimitAdTrackingEnabled();
            }
            if (this.zziqn == null) {
                this.zziqn = "";
            }
        } catch (Throwable th) {
            zzauk().zzayh().zzj("Unable to get advertising id", th);
            this.zziqn = "";
        }
        AdvertisingIdClient.setShouldSkipGmsCoreVersionCheck(false);
        return new Pair(this.zziqn, Boolean.valueOf(this.zziqo));
    }

    @WorkerThread
    final String zzji(String str) {
        zzug();
        String str2 = (String) zzjh(str).first;
        if (zzcfo.zzed(CommonUtils.MD5_INSTANCE) == null) {
            return null;
        }
        return String.format(Locale.US, "%032X", new Object[]{new BigInteger(1, zzcfo.zzed(CommonUtils.MD5_INSTANCE).digest(str2.getBytes()))});
    }

    @WorkerThread
    final void zzjj(String str) {
        zzug();
        Editor edit = zzayk().edit();
        edit.putString("gmp_app_id", str);
        edit.apply();
    }

    final void zzjk(String str) {
        synchronized (this.zziqs) {
            this.zziqq = str;
            this.zziqr = zzvu().elapsedRealtime();
        }
    }

    protected final void zzuh() {
        this.zzdtw = getContext().getSharedPreferences("com.google.android.gms.measurement.prefs", 0);
        this.zziqy = this.zzdtw.getBoolean("has_been_opened", false);
        if (!this.zziqy) {
            Editor edit = this.zzdtw.edit();
            edit.putBoolean("has_been_opened", true);
            edit.apply();
        }
    }
}
