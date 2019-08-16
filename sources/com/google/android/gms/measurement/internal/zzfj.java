package com.google.android.gms.measurement.internal;

import android.app.Application;
import android.content.Context;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.WorkerThread;
import android.text.TextUtils;
import android.util.Pair;
import com.google.android.gms.common.api.internal.GoogleServices;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.util.Clock;
import com.google.android.gms.common.util.DefaultClock;
import com.google.android.gms.common.util.VisibleForTesting;
import com.google.android.gms.common.wrappers.Wrappers;
import com.google.android.gms.internal.measurement.zzcm;
import com.google.android.gms.internal.measurement.zzp;
import com.google.android.gms.internal.measurement.zzx;
import java.net.URL;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.concurrent.atomic.AtomicReference;

public class zzfj implements zzgh {
    private static volatile zzfj zzoa;
    private final Clock zzac;
    private boolean zzdh = false;
    private final long zzdr;
    private final zzr zzfv;
    private final Context zzob;
    private final String zzoc;
    private final String zzod;
    private final zzs zzoe;
    private final zzeo zzof;
    private final zzef zzog;
    private final zzfc zzoh;
    private final zziw zzoi;
    private final zzjs zzoj;
    private final zzed zzok;
    private final zzhq zzol;
    private final zzgp zzom;
    private final zza zzon;
    private final zzhl zzoo;
    private zzeb zzop;
    private zzhv zzoq;
    private zzac zzor;
    private zzdy zzos;
    private zzeu zzot;
    private Boolean zzou;
    private long zzov;
    private volatile Boolean zzow;
    @VisibleForTesting
    private Boolean zzox;
    @VisibleForTesting
    private Boolean zzoy;
    private int zzoz;
    private AtomicInteger zzpa = new AtomicInteger(0);
    private final boolean zzt;
    private final String zzv;

    private zzfj(zzgm zzgm) {
        boolean z = true;
        Preconditions.checkNotNull(zzgm);
        this.zzfv = new zzr(zzgm.zzob);
        zzak.zza(this.zzfv);
        this.zzob = zzgm.zzob;
        this.zzv = zzgm.zzv;
        this.zzoc = zzgm.zzoc;
        this.zzod = zzgm.zzod;
        this.zzt = zzgm.zzt;
        this.zzow = zzgm.zzow;
        zzx zzx = zzgm.zzpr;
        if (!(zzx == null || zzx.zzw == null)) {
            Object obj = zzx.zzw.get("measurementEnabled");
            if (obj instanceof Boolean) {
                this.zzox = (Boolean) obj;
            }
            Object obj2 = zzx.zzw.get("measurementDeactivated");
            if (obj2 instanceof Boolean) {
                this.zzoy = (Boolean) obj2;
            }
        }
        zzcm.zzr(this.zzob);
        this.zzac = DefaultClock.getInstance();
        this.zzdr = this.zzac.currentTimeMillis();
        this.zzoe = new zzs(this);
        zzeo zzeo = new zzeo(this);
        zzeo.initialize();
        this.zzof = zzeo;
        zzef zzef = new zzef(this);
        zzef.initialize();
        this.zzog = zzef;
        zzjs zzjs = new zzjs(this);
        zzjs.initialize();
        this.zzoj = zzjs;
        zzed zzed = new zzed(this);
        zzed.initialize();
        this.zzok = zzed;
        this.zzon = new zza(this);
        zzhq zzhq = new zzhq(this);
        zzhq.initialize();
        this.zzol = zzhq;
        zzgp zzgp = new zzgp(this);
        zzgp.initialize();
        this.zzom = zzgp;
        zziw zziw = new zziw(this);
        zziw.initialize();
        this.zzoi = zziw;
        zzhl zzhl = new zzhl(this);
        zzhl.initialize();
        this.zzoo = zzhl;
        zzfc zzfc = new zzfc(this);
        zzfc.initialize();
        this.zzoh = zzfc;
        if ((zzgm.zzpr == null || zzgm.zzpr.zzs == 0) ? false : true) {
            z = false;
        }
        zzr zzr = this.zzfv;
        if (this.zzob.getApplicationContext() instanceof Application) {
            zzgp zzq = zzq();
            if (zzq.getContext().getApplicationContext() instanceof Application) {
                Application application = (Application) zzq.getContext().getApplicationContext();
                if (zzq.zzpu == null) {
                    zzq.zzpu = new zzhj(zzq, null);
                }
                if (z) {
                    application.unregisterActivityLifecycleCallbacks(zzq.zzpu);
                    application.registerActivityLifecycleCallbacks(zzq.zzpu);
                    zzq.zzab().zzgs().zzao("Registered activity lifecycle callback");
                }
            }
        } else {
            zzab().zzgn().zzao("Application context is not an Application");
        }
        this.zzoh.zza((Runnable) new zzfl(this, zzgm));
    }

    public static zzfj zza(Context context, zzx zzx) {
        if (zzx != null && (zzx.origin == null || zzx.zzv == null)) {
            zzx = new zzx(zzx.zzr, zzx.zzs, zzx.zzt, zzx.zzu, null, null, zzx.zzw);
        }
        Preconditions.checkNotNull(context);
        Preconditions.checkNotNull(context.getApplicationContext());
        if (zzoa == null) {
            synchronized (zzfj.class) {
                try {
                    if (zzoa == null) {
                        zzoa = new zzfj(new zzgm(context, zzx));
                    }
                } finally {
                    while (true) {
                        Class<zzfj> cls = zzfj.class;
                    }
                }
            }
        } else if (!(zzx == null || zzx.zzw == null || !zzx.zzw.containsKey("dataCollectionDefaultEnabled"))) {
            zzoa.zza(zzx.zzw.getBoolean("dataCollectionDefaultEnabled"));
        }
        return zzoa;
    }

    @VisibleForTesting
    public static zzfj zza(Context context, String str, String str2, Bundle bundle) {
        return zza(context, new zzx(0, 0, true, null, null, null, bundle));
    }

    private static void zza(zzg zzg) {
        if (zzg == null) {
            throw new IllegalStateException("Component not created");
        } else if (!zzg.isInitialized()) {
            String valueOf = String.valueOf(zzg.getClass());
            throw new IllegalStateException(new StringBuilder(String.valueOf(valueOf).length() + 27).append("Component not initialized: ").append(valueOf).toString());
        }
    }

    private static void zza(zzge zzge) {
        if (zzge == null) {
            throw new IllegalStateException("Component not created");
        } else if (!zzge.isInitialized()) {
            String valueOf = String.valueOf(zzge.getClass());
            throw new IllegalStateException(new StringBuilder(String.valueOf(valueOf).length() + 27).append("Component not initialized: ").append(valueOf).toString());
        }
    }

    private static void zza(zzgf zzgf) {
        if (zzgf == null) {
            throw new IllegalStateException("Component not created");
        }
    }

    /* access modifiers changed from: private */
    @WorkerThread
    public final void zza(zzgm zzgm) {
        zzeh zzgq;
        String str;
        zzaa().zzo();
        zzs.zzbm();
        zzac zzac2 = new zzac(this);
        zzac2.initialize();
        this.zzor = zzac2;
        zzdy zzdy = new zzdy(this, zzgm.zzs);
        zzdy.initialize();
        this.zzos = zzdy;
        zzeb zzeb = new zzeb(this);
        zzeb.initialize();
        this.zzop = zzeb;
        zzhv zzhv = new zzhv(this);
        zzhv.initialize();
        this.zzoq = zzhv;
        this.zzoj.zzbj();
        this.zzof.zzbj();
        this.zzot = new zzeu(this);
        this.zzos.zzbj();
        zzab().zzgq().zza("App measurement is starting up, version", Long.valueOf(this.zzoe.zzao()));
        zzr zzr = this.zzfv;
        zzab().zzgq().zzao("To enable debug logging run: adb shell setprop log.tag.FA VERBOSE");
        zzr zzr2 = this.zzfv;
        String zzag = zzdy.zzag();
        if (TextUtils.isEmpty(this.zzv)) {
            if (zzz().zzbr(zzag)) {
                zzgq = zzab().zzgq();
                str = "Faster debug mode event logging enabled. To disable, run:\n  adb shell setprop debug.firebase.analytics.app .none.";
            } else {
                zzgq = zzab().zzgq();
                String valueOf = String.valueOf(zzag);
                str = valueOf.length() != 0 ? "To enable faster debug mode event logging run:\n  adb shell setprop debug.firebase.analytics.app ".concat(valueOf) : new String("To enable faster debug mode event logging run:\n  adb shell setprop debug.firebase.analytics.app ");
            }
            zzgq.zzao(str);
        }
        zzab().zzgr().zzao("Debug-level message logging enabled");
        if (this.zzoz != this.zzpa.get()) {
            zzab().zzgk().zza("Not all components initialized", Integer.valueOf(this.zzoz), Integer.valueOf(this.zzpa.get()));
        }
        this.zzdh = true;
    }

    private final void zzbi() {
        if (!this.zzdh) {
            throw new IllegalStateException("AppMeasurement is not initialized");
        }
    }

    private final zzhl zzhv() {
        zza((zzge) this.zzoo);
        return this.zzoo;
    }

    public final Context getContext() {
        return this.zzob;
    }

    @WorkerThread
    public final boolean isEnabled() {
        boolean booleanValue;
        boolean z = true;
        zzaa().zzo();
        zzbi();
        if (this.zzoe.zza(zzak.zzil)) {
            if (this.zzoe.zzbp()) {
                return false;
            }
            if (this.zzoy != null && this.zzoy.booleanValue()) {
                return false;
            }
            Boolean zzhg = zzac().zzhg();
            if (zzhg != null) {
                return zzhg.booleanValue();
            }
            Boolean zzbq = this.zzoe.zzbq();
            if (zzbq != null) {
                return zzbq.booleanValue();
            }
            if (this.zzox != null) {
                return this.zzox.booleanValue();
            }
            if (GoogleServices.isMeasurementExplicitlyDisabled()) {
                return false;
            }
            if (!this.zzoe.zza(zzak.zzig) || this.zzow == null) {
                return true;
            }
            return this.zzow.booleanValue();
        } else if (this.zzoe.zzbp()) {
            return false;
        } else {
            Boolean zzbq2 = this.zzoe.zzbq();
            if (zzbq2 != null) {
                booleanValue = zzbq2.booleanValue();
            } else {
                if (GoogleServices.isMeasurementExplicitlyDisabled()) {
                    z = false;
                }
                booleanValue = (!z || this.zzow == null || !((Boolean) zzak.zzig.get(null)).booleanValue()) ? z : this.zzow.booleanValue();
            }
            return zzac().zze(booleanValue);
        }
    }

    /* access modifiers changed from: protected */
    @WorkerThread
    public final void start() {
        boolean z = false;
        zzaa().zzo();
        if (zzac().zzlj.get() == 0) {
            zzac().zzlj.set(this.zzac.currentTimeMillis());
        }
        if (Long.valueOf(zzac().zzlo.get()).longValue() == 0) {
            zzab().zzgs().zza("Persisting first open", Long.valueOf(this.zzdr));
            zzac().zzlo.set(this.zzdr);
        }
        if (zzie()) {
            zzr zzr = this.zzfv;
            if (!TextUtils.isEmpty(zzr().getGmpAppId()) || !TextUtils.isEmpty(zzr().zzah())) {
                zzz();
                if (zzjs.zza(zzr().getGmpAppId(), zzac().zzhc(), zzr().zzah(), zzac().zzhd())) {
                    zzab().zzgq().zzao("Rechecking which service to use due to a GMP App Id change");
                    zzac().zzhf();
                    zzu().resetAnalyticsData();
                    this.zzoq.disconnect();
                    this.zzoq.zzis();
                    zzac().zzlo.set(this.zzdr);
                    zzac().zzlq.zzau(null);
                }
                zzac().zzar(zzr().getGmpAppId());
                zzac().zzas(zzr().zzah());
            }
            zzq().zzbg(zzac().zzlq.zzho());
            zzr zzr2 = this.zzfv;
            if (!TextUtils.isEmpty(zzr().getGmpAppId()) || !TextUtils.isEmpty(zzr().zzah())) {
                boolean isEnabled = isEnabled();
                if (!zzac().zzhj() && !this.zzoe.zzbp()) {
                    zzeo zzac2 = zzac();
                    if (!isEnabled) {
                        z = true;
                    }
                    zzac2.zzf(z);
                }
                if (isEnabled) {
                    zzq().zzim();
                }
                zzs().zza(new AtomicReference<>());
            }
        } else if (isEnabled()) {
            if (!zzz().zzbp("android.permission.INTERNET")) {
                zzab().zzgk().zzao("App is missing INTERNET permission");
            }
            if (!zzz().zzbp("android.permission.ACCESS_NETWORK_STATE")) {
                zzab().zzgk().zzao("App is missing ACCESS_NETWORK_STATE permission");
            }
            zzr zzr3 = this.zzfv;
            if (!Wrappers.packageManager(this.zzob).isCallerInstantApp() && !this.zzoe.zzbw()) {
                if (!zzez.zzl(this.zzob)) {
                    zzab().zzgk().zzao("AppMeasurementReceiver not registered/enabled");
                }
                if (!zzjs.zzb(this.zzob, false)) {
                    zzab().zzgk().zzao("AppMeasurementService not registered/enabled");
                }
            }
            zzab().zzgk().zzao("Uploading is not possible. App measurement disabled");
        }
        zzac().zzly.set(this.zzoe.zza(zzak.zziu));
        zzac().zzlz.set(this.zzoe.zza(zzak.zziv));
    }

    @WorkerThread
    public final void zza(@NonNull zzp zzp) {
        zzaa().zzo();
        zza((zzge) zzhv());
        String zzag = zzr().zzag();
        Pair zzap = zzac().zzap(zzag);
        if (!this.zzoe.zzbr().booleanValue() || ((Boolean) zzap.second).booleanValue()) {
            zzab().zzgr().zzao("ADID unavailable to retrieve Deferred Deep Link. Skipping");
            zzz().zzb(zzp, "");
        } else if (!zzhv().zzgv()) {
            zzab().zzgn().zzao("Network is not available for Deferred Deep Link request. Skipping");
            zzz().zzb(zzp, "");
        } else {
            URL zza = zzz().zza(zzr().zzad().zzao(), zzag, (String) zzap.first);
            zzhl zzhv = zzhv();
            zzfi zzfi = new zzfi(this, zzp);
            zzhv.zzo();
            zzhv.zzbi();
            Preconditions.checkNotNull(zza);
            Preconditions.checkNotNull(zzfi);
            zzhv.zzaa().zzb((Runnable) new zzhn(zzhv, zzag, zza, null, null, zzfi));
        }
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Code restructure failed: missing block: B:21:0x0082, code lost:
        if (r4.isEmpty() == false) goto L_0x0084;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final /* synthetic */ void zza(com.google.android.gms.internal.measurement.zzp r9, java.lang.String r10, int r11, java.lang.Throwable r12, byte[] r13, java.util.Map r14) {
        /*
            r8 = this;
            r0 = 1
            r1 = 0
            r2 = 200(0xc8, float:2.8E-43)
            if (r11 == r2) goto L_0x000e
            r2 = 204(0xcc, float:2.86E-43)
            if (r11 == r2) goto L_0x000e
            r2 = 304(0x130, float:4.26E-43)
            if (r11 != r2) goto L_0x002e
        L_0x000e:
            if (r12 != 0) goto L_0x002e
            r2 = r0
        L_0x0011:
            if (r2 != 0) goto L_0x0030
            com.google.android.gms.measurement.internal.zzef r0 = r8.zzab()
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgn()
            java.lang.String r1 = "Network Request for Deferred Deep Link failed. response, exception"
            java.lang.Integer r2 = java.lang.Integer.valueOf(r11)
            r0.zza(r1, r2, r12)
            com.google.android.gms.measurement.internal.zzjs r0 = r8.zzz()
            java.lang.String r1 = ""
            r0.zzb(r9, r1)
        L_0x002d:
            return
        L_0x002e:
            r2 = r1
            goto L_0x0011
        L_0x0030:
            int r2 = r13.length
            if (r2 != 0) goto L_0x003d
            com.google.android.gms.measurement.internal.zzjs r0 = r8.zzz()
            java.lang.String r1 = ""
            r0.zzb(r9, r1)
            goto L_0x002d
        L_0x003d:
            java.lang.String r2 = new java.lang.String
            r2.<init>(r13)
            org.json.JSONObject r3 = new org.json.JSONObject     // Catch:{ JSONException -> 0x009d }
            r3.<init>(r2)     // Catch:{ JSONException -> 0x009d }
            java.lang.String r2 = "deeplink"
            java.lang.String r4 = ""
            java.lang.String r2 = r3.optString(r2, r4)     // Catch:{ JSONException -> 0x009d }
            java.lang.String r4 = "gclid"
            java.lang.String r5 = ""
            java.lang.String r3 = r3.optString(r4, r5)     // Catch:{ JSONException -> 0x009d }
            com.google.android.gms.measurement.internal.zzjs r4 = r8.zzz()     // Catch:{ JSONException -> 0x009d }
            r4.zzm()     // Catch:{ JSONException -> 0x009d }
            boolean r5 = android.text.TextUtils.isEmpty(r2)     // Catch:{ JSONException -> 0x009d }
            if (r5 != 0) goto L_0x00b6
            android.content.Context r4 = r4.getContext()     // Catch:{ JSONException -> 0x009d }
            android.content.pm.PackageManager r4 = r4.getPackageManager()     // Catch:{ JSONException -> 0x009d }
            android.content.Intent r5 = new android.content.Intent     // Catch:{ JSONException -> 0x009d }
            java.lang.String r6 = "android.intent.action.VIEW"
            android.net.Uri r7 = android.net.Uri.parse(r2)     // Catch:{ JSONException -> 0x009d }
            r5.<init>(r6, r7)     // Catch:{ JSONException -> 0x009d }
            r6 = 0
            java.util.List r4 = r4.queryIntentActivities(r5, r6)     // Catch:{ JSONException -> 0x009d }
            if (r4 == 0) goto L_0x00b6
            boolean r4 = r4.isEmpty()     // Catch:{ JSONException -> 0x009d }
            if (r4 != 0) goto L_0x00b6
        L_0x0084:
            if (r0 != 0) goto L_0x00b8
            com.google.android.gms.measurement.internal.zzef r0 = r8.zzab()     // Catch:{ JSONException -> 0x009d }
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgn()     // Catch:{ JSONException -> 0x009d }
            java.lang.String r1 = "Deferred Deep Link validation failed. gclid, deep link"
            r0.zza(r1, r3, r2)     // Catch:{ JSONException -> 0x009d }
            com.google.android.gms.measurement.internal.zzjs r0 = r8.zzz()     // Catch:{ JSONException -> 0x009d }
            java.lang.String r1 = ""
            r0.zzb(r9, r1)     // Catch:{ JSONException -> 0x009d }
            goto L_0x002d
        L_0x009d:
            r0 = move-exception
            com.google.android.gms.measurement.internal.zzef r1 = r8.zzab()
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgk()
            java.lang.String r2 = "Failed to parse the Deferred Deep Link response. exception"
            r1.zza(r2, r0)
            com.google.android.gms.measurement.internal.zzjs r0 = r8.zzz()
            java.lang.String r1 = ""
            r0.zzb(r9, r1)
            goto L_0x002d
        L_0x00b6:
            r0 = r1
            goto L_0x0084
        L_0x00b8:
            android.os.Bundle r0 = new android.os.Bundle     // Catch:{ JSONException -> 0x009d }
            r0.<init>()     // Catch:{ JSONException -> 0x009d }
            java.lang.String r1 = "deeplink"
            r0.putString(r1, r2)     // Catch:{ JSONException -> 0x009d }
            java.lang.String r1 = "gclid"
            r0.putString(r1, r3)     // Catch:{ JSONException -> 0x009d }
            com.google.android.gms.measurement.internal.zzgp r1 = r8.zzom     // Catch:{ JSONException -> 0x009d }
            java.lang.String r3 = "auto"
            java.lang.String r4 = "_cmp"
            r1.logEvent(r3, r4, r0)     // Catch:{ JSONException -> 0x009d }
            com.google.android.gms.measurement.internal.zzjs r0 = r8.zzz()     // Catch:{ JSONException -> 0x009d }
            r0.zzb(r9, r2)     // Catch:{ JSONException -> 0x009d }
            goto L_0x002d
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzfj.zza(com.google.android.gms.internal.measurement.zzp, java.lang.String, int, java.lang.Throwable, byte[], java.util.Map):void");
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zza(boolean z) {
        this.zzow = Boolean.valueOf(z);
    }

    public final zzfc zzaa() {
        zza((zzge) this.zzoh);
        return this.zzoh;
    }

    public final zzef zzab() {
        zza((zzge) this.zzog);
        return this.zzog;
    }

    public final zzeo zzac() {
        zza((zzgf) this.zzof);
        return this.zzof;
    }

    public final zzs zzad() {
        return this.zzoe;
    }

    public final zzr zzae() {
        return this.zzfv;
    }

    /* access modifiers changed from: 0000 */
    public final void zzb(zzg zzg) {
        this.zzoz++;
    }

    /* access modifiers changed from: 0000 */
    public final void zzb(zzge zzge) {
        this.zzoz++;
    }

    public final zzef zzhs() {
        if (this.zzog == null || !this.zzog.isInitialized()) {
            return null;
        }
        return this.zzog;
    }

    public final zzeu zzht() {
        return this.zzot;
    }

    /* access modifiers changed from: 0000 */
    public final zzfc zzhu() {
        return this.zzoh;
    }

    public final boolean zzhw() {
        return TextUtils.isEmpty(this.zzv);
    }

    public final String zzhx() {
        return this.zzv;
    }

    public final String zzhy() {
        return this.zzoc;
    }

    public final String zzhz() {
        return this.zzod;
    }

    public final boolean zzia() {
        return this.zzt;
    }

    @WorkerThread
    public final boolean zzib() {
        return this.zzow != null && this.zzow.booleanValue();
    }

    /* access modifiers changed from: 0000 */
    public final long zzic() {
        Long valueOf = Long.valueOf(zzac().zzlo.get());
        return valueOf.longValue() == 0 ? this.zzdr : Math.min(this.zzdr, valueOf.longValue());
    }

    /* access modifiers changed from: 0000 */
    public final void zzid() {
        this.zzpa.incrementAndGet();
    }

    /* access modifiers changed from: protected */
    @WorkerThread
    public final boolean zzie() {
        boolean z = false;
        zzbi();
        zzaa().zzo();
        if (this.zzou == null || this.zzov == 0 || (this.zzou != null && !this.zzou.booleanValue() && Math.abs(this.zzac.elapsedRealtime() - this.zzov) > 1000)) {
            this.zzov = this.zzac.elapsedRealtime();
            zzr zzr = this.zzfv;
            this.zzou = Boolean.valueOf(zzz().zzbp("android.permission.INTERNET") && zzz().zzbp("android.permission.ACCESS_NETWORK_STATE") && (Wrappers.packageManager(this.zzob).isCallerInstantApp() || this.zzoe.zzbw() || (zzez.zzl(this.zzob) && zzjs.zzb(this.zzob, false))));
            if (this.zzou.booleanValue()) {
                if (zzz().zzr(zzr().getGmpAppId(), zzr().zzah()) || !TextUtils.isEmpty(zzr().zzah())) {
                    z = true;
                }
                this.zzou = Boolean.valueOf(z);
            }
        }
        return this.zzou.booleanValue();
    }

    /* access modifiers changed from: 0000 */
    public final void zzl() {
        zzr zzr = this.zzfv;
        throw new IllegalStateException("Unexpected call on client side");
    }

    /* access modifiers changed from: 0000 */
    public final void zzm() {
        zzr zzr = this.zzfv;
    }

    public final zza zzp() {
        if (this.zzon != null) {
            return this.zzon;
        }
        throw new IllegalStateException("Component not created");
    }

    public final zzgp zzq() {
        zza((zzg) this.zzom);
        return this.zzom;
    }

    public final zzdy zzr() {
        zza((zzg) this.zzos);
        return this.zzos;
    }

    public final zzhv zzs() {
        zza((zzg) this.zzoq);
        return this.zzoq;
    }

    public final zzhq zzt() {
        zza((zzg) this.zzol);
        return this.zzol;
    }

    public final zzeb zzu() {
        zza((zzg) this.zzop);
        return this.zzop;
    }

    public final zziw zzv() {
        zza((zzg) this.zzoi);
        return this.zzoi;
    }

    public final zzac zzw() {
        zza((zzge) this.zzor);
        return this.zzor;
    }

    public final Clock zzx() {
        return this.zzac;
    }

    public final zzed zzy() {
        zza((zzgf) this.zzok);
        return this.zzok;
    }

    public final zzjs zzz() {
        zza((zzgf) this.zzoj);
        return this.zzoj;
    }
}
