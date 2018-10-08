package com.google.android.gms.internal;

import android.content.Context;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.Build.VERSION;
import android.support.annotation.WorkerThread;
import android.text.TextUtils;
import com.facebook.internal.AnalyticsEvents;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzca;
import com.google.android.gms.common.util.zzd;
import com.google.firebase.iid.FirebaseInstanceId;
import java.math.BigInteger;
import java.util.Locale;

public final class zzcbj extends zzcdm {
    private String mAppId;
    private String zzcxs;
    private String zzdmg;
    private String zzdmh;
    private String zziky;
    private long zzilc;
    private int zzioz;
    private long zzipa;
    private int zzipb;

    zzcbj(zzcco zzcco) {
        super(zzcco);
    }

    @WorkerThread
    private final String zzaup() {
        zzug();
        try {
            return FirebaseInstanceId.getInstance().getId();
        } catch (IllegalStateException e) {
            zzauk().zzaye().log("Failed to retrieve Firebase Instance Id");
            return null;
        }
    }

    final String getAppId() {
        zzwh();
        return this.mAppId;
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    final String getGmpAppId() {
        zzwh();
        return this.zzcxs;
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
    final String zzaxz() {
        zzaug().zzazx().nextBytes(new byte[16]);
        return String.format(Locale.US, "%032x", new Object[]{new BigInteger(1, r0)});
    }

    final int zzaya() {
        zzwh();
        return this.zzioz;
    }

    @WorkerThread
    final zzcak zzjb(String str) {
        zzug();
        String appId = getAppId();
        String gmpAppId = getGmpAppId();
        zzwh();
        String str2 = this.zzdmh;
        long zzaya = (long) zzaya();
        zzwh();
        String str3 = this.zziky;
        long zzauu = zzcap.zzauu();
        zzwh();
        zzug();
        if (this.zzipa == 0) {
            this.zzipa = this.zzikb.zzaug().zzai(getContext(), getContext().getPackageName());
        }
        long j = this.zzipa;
        boolean isEnabled = this.zzikb.isEnabled();
        boolean z = !zzaul().zziqy;
        String zzaup = zzaup();
        zzwh();
        long zzazd = this.zzikb.zzazd();
        zzwh();
        return new zzcak(appId, gmpAppId, str2, zzaya, str3, zzauu, j, str, isEnabled, z, zzaup, 0, zzazd, this.zzipb);
    }

    public final /* bridge */ /* synthetic */ void zzug() {
        super.zzug();
    }

    protected final void zzuh() {
        String str = "unknown";
        String str2 = AnalyticsEvents.PARAMETER_DIALOG_OUTCOME_VALUE_UNKNOWN;
        int i = Integer.MIN_VALUE;
        String str3 = AnalyticsEvents.PARAMETER_DIALOG_OUTCOME_VALUE_UNKNOWN;
        String packageName = getContext().getPackageName();
        PackageManager packageManager = getContext().getPackageManager();
        if (packageManager == null) {
            zzauk().zzayc().zzj("PackageManager is null, app identity information might be inaccurate. appId", zzcbo.zzjf(packageName));
        } else {
            try {
                str = packageManager.getInstallerPackageName(packageName);
            } catch (IllegalArgumentException e) {
                zzauk().zzayc().zzj("Error retrieving app installer package name. appId", zzcbo.zzjf(packageName));
            }
            if (str == null) {
                str = "manual_install";
            } else if ("com.android.vending".equals(str)) {
                str = "";
            }
            try {
                PackageInfo packageInfo = packageManager.getPackageInfo(getContext().getPackageName(), 0);
                if (packageInfo != null) {
                    CharSequence applicationLabel = packageManager.getApplicationLabel(packageInfo.applicationInfo);
                    if (!TextUtils.isEmpty(applicationLabel)) {
                        str3 = applicationLabel.toString();
                    }
                    str2 = packageInfo.versionName;
                    i = packageInfo.versionCode;
                }
            } catch (NameNotFoundException e2) {
                zzauk().zzayc().zze("Error retrieving package info. appId, appName", zzcbo.zzjf(packageName), str3);
            }
        }
        this.mAppId = packageName;
        this.zziky = str;
        this.zzdmh = str2;
        this.zzioz = i;
        this.zzdmg = str3;
        this.zzipa = 0;
        zzcap.zzawj();
        Status zzcc = zzca.zzcc(getContext());
        int i2 = (zzcc == null || !zzcc.isSuccess()) ? 0 : 1;
        if (i2 == 0) {
            if (zzcc == null) {
                zzauk().zzayc().log("GoogleService failed to initialize (no status)");
            } else {
                zzauk().zzayc().zze("GoogleService failed to initialize, status", Integer.valueOf(zzcc.getStatusCode()), zzcc.getStatusMessage());
            }
        }
        if (i2 != 0) {
            Boolean zzit = zzaum().zzit("firebase_analytics_collection_enabled");
            if (zzaum().zzawk()) {
                zzauk().zzayg().log("Collection disabled with firebase_analytics_collection_deactivated=1");
                i2 = 0;
            } else if (zzit != null && !zzit.booleanValue()) {
                zzauk().zzayg().log("Collection disabled with firebase_analytics_collection_enabled=0");
                i2 = 0;
            } else if (zzit == null && zzcap.zzaie()) {
                zzauk().zzayg().log("Collection disabled with google_app_measurement_enable=0");
                i2 = 0;
            } else {
                zzauk().zzayi().log("Collection enabled");
                i2 = 1;
            }
        } else {
            i2 = 0;
        }
        this.zzcxs = "";
        this.zzilc = 0;
        zzcap.zzawj();
        try {
            str2 = zzca.zzaid();
            if (TextUtils.isEmpty(str2)) {
                str2 = "";
            }
            this.zzcxs = str2;
            if (i2 != 0) {
                zzauk().zzayi().zze("App package, google app id", this.mAppId, this.zzcxs);
            }
        } catch (IllegalStateException e3) {
            zzauk().zzayc().zze("getGoogleAppId or isMeasurementEnabled failed with exception. appId", zzcbo.zzjf(packageName), e3);
        }
        if (VERSION.SDK_INT >= 16) {
            this.zzipb = zzbdn.zzcq(getContext()) ? 1 : 0;
        } else {
            this.zzipb = 0;
        }
    }

    public final /* bridge */ /* synthetic */ zzd zzvu() {
        return super.zzvu();
    }
}
