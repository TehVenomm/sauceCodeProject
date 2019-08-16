package com.google.android.gms.measurement.internal;

import android.content.Context;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import com.google.android.gms.common.util.Clock;
import com.google.android.gms.common.util.VisibleForTesting;
import java.util.List;

public final class zzdy extends zzg {
    private String zzce;
    private String zzcg;
    private String zzcm;
    private String zzco;
    private long zzcr;
    private String zzcu;
    private List<String> zzcw;
    private int zzds;
    private int zzjr;
    private String zzjs;
    private long zzjt;
    private long zzs;

    zzdy(zzfj zzfj, long j) {
        super(zzfj);
        this.zzs = j;
    }

    @WorkerThread
    @VisibleForTesting
    private final String zzge() {
        try {
            Class loadClass = getContext().getClassLoader().loadClass("com.google.firebase.analytics.FirebaseAnalytics");
            if (loadClass == null) {
                return null;
            }
            try {
                Object invoke = loadClass.getDeclaredMethod("getInstance", new Class[]{Context.class}).invoke(null, new Object[]{getContext()});
                if (invoke == null) {
                    return null;
                }
                try {
                    return (String) loadClass.getDeclaredMethod("getFirebaseInstanceId", new Class[0]).invoke(invoke, new Object[0]);
                } catch (Exception e) {
                    zzab().zzgp().zzao("Failed to retrieve Firebase Instance Id");
                    return null;
                }
            } catch (Exception e2) {
                zzab().zzgo().zzao("Failed to obtain Firebase Analytics instance");
                return null;
            }
        } catch (ClassNotFoundException e3) {
            return null;
        }
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    /* access modifiers changed from: 0000 */
    public final String getGmpAppId() {
        zzbi();
        return this.zzcg;
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

    /* access modifiers changed from: 0000 */
    public final String zzag() {
        zzbi();
        return this.zzce;
    }

    /* access modifiers changed from: 0000 */
    public final String zzah() {
        zzbi();
        return this.zzcu;
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Removed duplicated region for block: B:24:0x0105  */
    /* JADX WARNING: Removed duplicated region for block: B:32:0x0124  */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final com.google.android.gms.measurement.internal.zzn zzai(java.lang.String r32) {
        /*
            r31 = this;
            r31.zzo()
            r31.zzm()
            java.lang.String r4 = r31.zzag()
            java.lang.String r5 = r31.getGmpAppId()
            r31.zzbi()
            r0 = r31
            java.lang.String r6 = r0.zzcm
            int r2 = r31.zzgf()
            long r7 = (long) r2
            r31.zzbi()
            r0 = r31
            java.lang.String r9 = r0.zzco
            com.google.android.gms.measurement.internal.zzs r2 = r31.zzad()
            long r10 = r2.zzao()
            r31.zzbi()
            r31.zzo()
            r0 = r31
            long r2 = r0.zzjt
            r12 = 0
            int r2 = (r2 > r12 ? 1 : (r2 == r12 ? 0 : -1))
            if (r2 != 0) goto L_0x0055
            r0 = r31
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj
            com.google.android.gms.measurement.internal.zzjs r2 = r2.zzz()
            android.content.Context r3 = r31.getContext()
            android.content.Context r12 = r31.getContext()
            java.lang.String r12 = r12.getPackageName()
            long r2 = r2.zzc(r3, r12)
            r0 = r31
            r0.zzjt = r2
        L_0x0055:
            r0 = r31
            long r12 = r0.zzjt
            r0 = r31
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj
            boolean r15 = r2.isEnabled()
            com.google.android.gms.measurement.internal.zzeo r2 = r31.zzac()
            boolean r2 = r2.zzmc
            if (r2 != 0) goto L_0x0113
            r16 = 1
        L_0x006b:
            r31.zzo()
            r31.zzm()
            r0 = r31
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj
            boolean r2 = r2.isEnabled()
            if (r2 != 0) goto L_0x0117
            r17 = 0
        L_0x007d:
            r31.zzbi()
            r0 = r31
            long r0 = r0.zzcr
            r18 = r0
            r0 = r31
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj
            long r20 = r2.zzic()
            int r22 = r31.zzgg()
            com.google.android.gms.measurement.internal.zzs r2 = r31.zzad()
            java.lang.Boolean r2 = r2.zzbr()
            boolean r23 = r2.booleanValue()
            com.google.android.gms.measurement.internal.zzs r2 = r31.zzad()
            r2.zzm()
            java.lang.String r3 = "google_analytics_ssaid_collection_enabled"
            java.lang.Boolean r2 = r2.zzj(r3)
            if (r2 == 0) goto L_0x00b3
            boolean r2 = r2.booleanValue()
            if (r2 == 0) goto L_0x011d
        L_0x00b3:
            r2 = 1
        L_0x00b4:
            java.lang.Boolean r2 = java.lang.Boolean.valueOf(r2)
            boolean r24 = r2.booleanValue()
            com.google.android.gms.measurement.internal.zzeo r2 = r31.zzac()
            boolean r25 = r2.zzhi()
            java.lang.String r26 = r31.zzah()
            com.google.android.gms.measurement.internal.zzs r2 = r31.zzad()
            java.lang.String r3 = r31.zzag()
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r14 = com.google.android.gms.measurement.internal.zzak.zzij
            boolean r2 = r2.zze(r3, r14)
            if (r2 == 0) goto L_0x0121
            com.google.android.gms.measurement.internal.zzs r2 = r31.zzad()
            java.lang.String r3 = "google_analytics_default_allow_ad_personalization_signals"
            java.lang.Boolean r2 = r2.zzj(r3)
            if (r2 == 0) goto L_0x0121
            boolean r2 = r2.booleanValue()
            if (r2 != 0) goto L_0x011f
            r2 = 1
        L_0x00eb:
            java.lang.Boolean r27 = java.lang.Boolean.valueOf(r2)
        L_0x00ef:
            r0 = r31
            long r0 = r0.zzs
            r28 = r0
            com.google.android.gms.measurement.internal.zzs r2 = r31.zzad()
            java.lang.String r3 = r31.zzag()
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r14 = com.google.android.gms.measurement.internal.zzak.zzix
            boolean r2 = r2.zze(r3, r14)
            if (r2 == 0) goto L_0x0124
            r0 = r31
            java.util.List<java.lang.String> r0 = r0.zzcw
            r30 = r0
        L_0x010b:
            com.google.android.gms.measurement.internal.zzn r3 = new com.google.android.gms.measurement.internal.zzn
            r14 = r32
            r3.<init>(r4, r5, r6, r7, r9, r10, r12, r14, r15, r16, r17, r18, r20, r22, r23, r24, r25, r26, r27, r28, r30)
            return r3
        L_0x0113:
            r16 = 0
            goto L_0x006b
        L_0x0117:
            java.lang.String r17 = r31.zzge()
            goto L_0x007d
        L_0x011d:
            r2 = 0
            goto L_0x00b4
        L_0x011f:
            r2 = 0
            goto L_0x00eb
        L_0x0121:
            r27 = 0
            goto L_0x00ef
        L_0x0124:
            r30 = 0
            goto L_0x010b
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzdy.zzai(java.lang.String):com.google.android.gms.measurement.internal.zzn");
    }

    /* access modifiers changed from: 0000 */
    @Nullable
    public final List<String> zzbh() {
        return this.zzcw;
    }

    /* access modifiers changed from: protected */
    public final boolean zzbk() {
        return true;
    }

    /* access modifiers changed from: protected */
    /* JADX WARNING: Removed duplicated region for block: B:31:0x00df  */
    /* JADX WARNING: Removed duplicated region for block: B:35:0x00e9 A[Catch:{ IllegalStateException -> 0x0225 }] */
    /* JADX WARNING: Removed duplicated region for block: B:37:0x00fc A[Catch:{ IllegalStateException -> 0x0225 }] */
    /* JADX WARNING: Removed duplicated region for block: B:40:0x011e  */
    /* JADX WARNING: Removed duplicated region for block: B:46:0x0143  */
    /* JADX WARNING: Removed duplicated region for block: B:49:0x014b  */
    /* JADX WARNING: Removed duplicated region for block: B:85:0x0222  */
    /* JADX WARNING: Removed duplicated region for block: B:97:0x0262  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void zzbl() {
        /*
            r14 = this;
            r12 = 0
            r4 = 1
            r5 = 0
            java.lang.String r0 = "unknown"
            java.lang.String r1 = "Unknown"
            r3 = -2147483648(0xffffffff80000000, float:-0.0)
            java.lang.String r2 = "Unknown"
            android.content.Context r6 = r14.getContext()
            java.lang.String r6 = r6.getPackageName()
            android.content.Context r7 = r14.getContext()
            android.content.pm.PackageManager r7 = r7.getPackageManager()
            if (r7 != 0) goto L_0x015a
            com.google.android.gms.measurement.internal.zzef r8 = r14.zzab()
            com.google.android.gms.measurement.internal.zzeh r8 = r8.zzgk()
            java.lang.String r9 = "PackageManager is null, app identity information might be inaccurate. appId"
            java.lang.Object r10 = com.google.android.gms.measurement.internal.zzef.zzam(r6)
            r8.zza(r9, r10)
        L_0x002f:
            r14.zzce = r6
            r14.zzco = r0
            r14.zzcm = r1
            r14.zzjr = r3
            r14.zzjs = r2
            r14.zzjt = r12
            r14.zzae()
            android.content.Context r0 = r14.getContext()
            com.google.android.gms.common.api.Status r2 = com.google.android.gms.common.api.internal.GoogleServices.initialize(r0)
            if (r2 == 0) goto L_0x01b9
            boolean r0 = r2.isSuccess()
            if (r0 == 0) goto L_0x01b9
            r0 = r4
        L_0x004f:
            com.google.android.gms.measurement.internal.zzfj r1 = r14.zzj
            java.lang.String r1 = r1.zzhx()
            boolean r1 = android.text.TextUtils.isEmpty(r1)
            if (r1 != 0) goto L_0x01bc
            java.lang.String r1 = "am"
            com.google.android.gms.measurement.internal.zzfj r3 = r14.zzj
            java.lang.String r3 = r3.zzhy()
            boolean r1 = r1.equals(r3)
            if (r1 == 0) goto L_0x01bc
            r1 = r4
        L_0x006a:
            r0 = r0 | r1
            if (r0 != 0) goto L_0x007c
            if (r2 != 0) goto L_0x01bf
            com.google.android.gms.measurement.internal.zzef r1 = r14.zzab()
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgk()
            java.lang.String r2 = "GoogleService failed to initialize (no status)"
            r1.zzao(r2)
        L_0x007c:
            if (r0 == 0) goto L_0x0266
            com.google.android.gms.measurement.internal.zzs r0 = r14.zzad()
            java.lang.Boolean r0 = r0.zzbq()
            com.google.android.gms.measurement.internal.zzs r1 = r14.zzad()
            boolean r1 = r1.zzbp()
            if (r1 == 0) goto L_0x01da
            com.google.android.gms.measurement.internal.zzfj r0 = r14.zzj
            boolean r0 = r0.zzhw()
            if (r0 == 0) goto L_0x0266
            com.google.android.gms.measurement.internal.zzef r0 = r14.zzab()
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgq()
            java.lang.String r1 = "Collection disabled with firebase_analytics_collection_deactivated=1"
            r0.zzao(r1)
            r0 = r5
        L_0x00a6:
            java.lang.String r1 = ""
            r14.zzcg = r1
            java.lang.String r1 = ""
            r14.zzcu = r1
            r14.zzcr = r12
            r14.zzae()
            com.google.android.gms.measurement.internal.zzfj r1 = r14.zzj
            java.lang.String r1 = r1.zzhx()
            boolean r1 = android.text.TextUtils.isEmpty(r1)
            if (r1 != 0) goto L_0x00d5
            java.lang.String r1 = "am"
            com.google.android.gms.measurement.internal.zzfj r2 = r14.zzj
            java.lang.String r2 = r2.zzhy()
            boolean r1 = r1.equals(r2)
            if (r1 == 0) goto L_0x00d5
            com.google.android.gms.measurement.internal.zzfj r1 = r14.zzj
            java.lang.String r1 = r1.zzhx()
            r14.zzcu = r1
        L_0x00d5:
            java.lang.String r2 = com.google.android.gms.common.api.internal.GoogleServices.getGoogleAppId()     // Catch:{ IllegalStateException -> 0x0225 }
            boolean r1 = android.text.TextUtils.isEmpty(r2)     // Catch:{ IllegalStateException -> 0x0225 }
            if (r1 == 0) goto L_0x0222
            java.lang.String r1 = ""
        L_0x00e1:
            r14.zzcg = r1     // Catch:{ IllegalStateException -> 0x0225 }
            boolean r1 = android.text.TextUtils.isEmpty(r2)     // Catch:{ IllegalStateException -> 0x0225 }
            if (r1 != 0) goto L_0x00fa
            com.google.android.gms.common.internal.StringResourceValueReader r1 = new com.google.android.gms.common.internal.StringResourceValueReader     // Catch:{ IllegalStateException -> 0x0225 }
            android.content.Context r2 = r14.getContext()     // Catch:{ IllegalStateException -> 0x0225 }
            r1.<init>(r2)     // Catch:{ IllegalStateException -> 0x0225 }
            java.lang.String r2 = "admob_app_id"
            java.lang.String r1 = r1.getString(r2)     // Catch:{ IllegalStateException -> 0x0225 }
            r14.zzcu = r1     // Catch:{ IllegalStateException -> 0x0225 }
        L_0x00fa:
            if (r0 == 0) goto L_0x010d
            com.google.android.gms.measurement.internal.zzef r0 = r14.zzab()     // Catch:{ IllegalStateException -> 0x0225 }
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgs()     // Catch:{ IllegalStateException -> 0x0225 }
            java.lang.String r1 = "App package, google app id"
            java.lang.String r2 = r14.zzce     // Catch:{ IllegalStateException -> 0x0225 }
            java.lang.String r3 = r14.zzcg     // Catch:{ IllegalStateException -> 0x0225 }
            r0.zza(r1, r2, r3)     // Catch:{ IllegalStateException -> 0x0225 }
        L_0x010d:
            r0 = 0
            r14.zzcw = r0
            com.google.android.gms.measurement.internal.zzs r0 = r14.zzad()
            java.lang.String r1 = r14.zzce
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r2 = com.google.android.gms.measurement.internal.zzak.zzix
            boolean r0 = r0.zze(r1, r2)
            if (r0 == 0) goto L_0x0145
            r14.zzae()
            com.google.android.gms.measurement.internal.zzs r0 = r14.zzad()
            java.lang.String r1 = "analytics.safelisted_events"
            java.util.List r1 = r0.zzk(r1)
            if (r1 == 0) goto L_0x0258
            int r0 = r1.size()
            if (r0 != 0) goto L_0x0239
            com.google.android.gms.measurement.internal.zzef r0 = r14.zzab()
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgn()
            java.lang.String r2 = "Safelisted event list cannot be empty. Ignoring"
            r0.zzao(r2)
            r0 = r5
        L_0x0141:
            if (r0 == 0) goto L_0x0145
            r14.zzcw = r1
        L_0x0145:
            int r0 = android.os.Build.VERSION.SDK_INT
            r1 = 16
            if (r0 < r1) goto L_0x0262
            if (r7 == 0) goto L_0x025e
            android.content.Context r0 = r14.getContext()
            boolean r0 = com.google.android.gms.common.wrappers.InstantApps.isInstantApp(r0)
            if (r0 == 0) goto L_0x025b
        L_0x0157:
            r14.zzds = r4
        L_0x0159:
            return
        L_0x015a:
            java.lang.String r0 = r7.getInstallerPackageName(r6)     // Catch:{ IllegalArgumentException -> 0x0187 }
        L_0x015e:
            if (r0 != 0) goto L_0x019a
            java.lang.String r0 = "manual_install"
        L_0x0162:
            android.content.Context r8 = r14.getContext()     // Catch:{ NameNotFoundException -> 0x01a5 }
            java.lang.String r8 = r8.getPackageName()     // Catch:{ NameNotFoundException -> 0x01a5 }
            r9 = 0
            android.content.pm.PackageInfo r8 = r7.getPackageInfo(r8, r9)     // Catch:{ NameNotFoundException -> 0x01a5 }
            if (r8 == 0) goto L_0x002f
            android.content.pm.ApplicationInfo r9 = r8.applicationInfo     // Catch:{ NameNotFoundException -> 0x01a5 }
            java.lang.CharSequence r9 = r7.getApplicationLabel(r9)     // Catch:{ NameNotFoundException -> 0x01a5 }
            boolean r10 = android.text.TextUtils.isEmpty(r9)     // Catch:{ NameNotFoundException -> 0x01a5 }
            if (r10 != 0) goto L_0x0181
            java.lang.String r2 = r9.toString()     // Catch:{ NameNotFoundException -> 0x01a5 }
        L_0x0181:
            java.lang.String r1 = r8.versionName     // Catch:{ NameNotFoundException -> 0x01a5 }
            int r3 = r8.versionCode     // Catch:{ NameNotFoundException -> 0x01a5 }
            goto L_0x002f
        L_0x0187:
            r8 = move-exception
            com.google.android.gms.measurement.internal.zzef r8 = r14.zzab()
            com.google.android.gms.measurement.internal.zzeh r8 = r8.zzgk()
            java.lang.String r9 = "Error retrieving app installer package name. appId"
            java.lang.Object r10 = com.google.android.gms.measurement.internal.zzef.zzam(r6)
            r8.zza(r9, r10)
            goto L_0x015e
        L_0x019a:
            java.lang.String r8 = "com.android.vending"
            boolean r8 = r8.equals(r0)
            if (r8 == 0) goto L_0x0162
            java.lang.String r0 = ""
            goto L_0x0162
        L_0x01a5:
            r8 = move-exception
            com.google.android.gms.measurement.internal.zzef r8 = r14.zzab()
            com.google.android.gms.measurement.internal.zzeh r8 = r8.zzgk()
            java.lang.String r9 = "Error retrieving package info. appId, appName"
            java.lang.Object r10 = com.google.android.gms.measurement.internal.zzef.zzam(r6)
            r8.zza(r9, r10, r2)
            goto L_0x002f
        L_0x01b9:
            r0 = r5
            goto L_0x004f
        L_0x01bc:
            r1 = r5
            goto L_0x006a
        L_0x01bf:
            com.google.android.gms.measurement.internal.zzef r1 = r14.zzab()
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgk()
            java.lang.String r3 = "GoogleService failed to initialize, status"
            int r8 = r2.getStatusCode()
            java.lang.Integer r8 = java.lang.Integer.valueOf(r8)
            java.lang.String r2 = r2.getStatusMessage()
            r1.zza(r3, r8, r2)
            goto L_0x007c
        L_0x01da:
            if (r0 == 0) goto L_0x01fa
            boolean r1 = r0.booleanValue()
            if (r1 != 0) goto L_0x01fa
            com.google.android.gms.measurement.internal.zzfj r0 = r14.zzj
            boolean r0 = r0.zzhw()
            if (r0 == 0) goto L_0x0266
            com.google.android.gms.measurement.internal.zzef r0 = r14.zzab()
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgq()
            java.lang.String r1 = "Collection disabled with firebase_analytics_collection_enabled=0"
            r0.zzao(r1)
            r0 = r5
            goto L_0x00a6
        L_0x01fa:
            if (r0 != 0) goto L_0x0212
            boolean r0 = com.google.android.gms.common.api.internal.GoogleServices.isMeasurementExplicitlyDisabled()
            if (r0 == 0) goto L_0x0212
            com.google.android.gms.measurement.internal.zzef r0 = r14.zzab()
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgq()
            java.lang.String r1 = "Collection disabled with google_app_measurement_enable=0"
            r0.zzao(r1)
            r0 = r5
            goto L_0x00a6
        L_0x0212:
            com.google.android.gms.measurement.internal.zzef r0 = r14.zzab()
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgs()
            java.lang.String r1 = "Collection enabled"
            r0.zzao(r1)
            r0 = r4
            goto L_0x00a6
        L_0x0222:
            r1 = r2
            goto L_0x00e1
        L_0x0225:
            r0 = move-exception
            com.google.android.gms.measurement.internal.zzef r1 = r14.zzab()
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgk()
            java.lang.String r2 = "getGoogleAppId or isMeasurementEnabled failed with exception. appId"
            java.lang.Object r3 = com.google.android.gms.measurement.internal.zzef.zzam(r6)
            r1.zza(r2, r3, r0)
            goto L_0x010d
        L_0x0239:
            java.util.Iterator r2 = r1.iterator()
        L_0x023d:
            boolean r0 = r2.hasNext()
            if (r0 == 0) goto L_0x0258
            java.lang.Object r0 = r2.next()
            java.lang.String r0 = (java.lang.String) r0
            com.google.android.gms.measurement.internal.zzjs r3 = r14.zzz()
            java.lang.String r6 = "safelisted event"
            boolean r0 = r3.zzq(r6, r0)
            if (r0 != 0) goto L_0x023d
            r0 = r5
            goto L_0x0141
        L_0x0258:
            r0 = r4
            goto L_0x0141
        L_0x025b:
            r4 = r5
            goto L_0x0157
        L_0x025e:
            r14.zzds = r5
            goto L_0x0159
        L_0x0262:
            r14.zzds = r5
            goto L_0x0159
        L_0x0266:
            r0 = r5
            goto L_0x00a6
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzdy.zzbl():void");
    }

    /* access modifiers changed from: 0000 */
    public final int zzgf() {
        zzbi();
        return this.zzjr;
    }

    /* access modifiers changed from: 0000 */
    public final int zzgg() {
        zzbi();
        return this.zzds;
    }

    public final /* bridge */ /* synthetic */ void zzl() {
        super.zzl();
    }

    public final /* bridge */ /* synthetic */ void zzm() {
        super.zzm();
    }

    public final /* bridge */ /* synthetic */ void zzn() {
        super.zzn();
    }

    public final /* bridge */ /* synthetic */ void zzo() {
        super.zzo();
    }

    public final /* bridge */ /* synthetic */ zza zzp() {
        return super.zzp();
    }

    public final /* bridge */ /* synthetic */ zzgp zzq() {
        return super.zzq();
    }

    public final /* bridge */ /* synthetic */ zzdy zzr() {
        return super.zzr();
    }

    public final /* bridge */ /* synthetic */ zzhv zzs() {
        return super.zzs();
    }

    public final /* bridge */ /* synthetic */ zzhq zzt() {
        return super.zzt();
    }

    public final /* bridge */ /* synthetic */ zzeb zzu() {
        return super.zzu();
    }

    public final /* bridge */ /* synthetic */ zziw zzv() {
        return super.zzv();
    }

    public final /* bridge */ /* synthetic */ zzac zzw() {
        return super.zzw();
    }

    public final /* bridge */ /* synthetic */ Clock zzx() {
        return super.zzx();
    }

    public final /* bridge */ /* synthetic */ zzed zzy() {
        return super.zzy();
    }

    public final /* bridge */ /* synthetic */ zzjs zzz() {
        return super.zzz();
    }
}
