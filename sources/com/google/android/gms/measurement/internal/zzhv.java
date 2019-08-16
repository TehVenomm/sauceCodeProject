package com.google.android.gms.measurement.internal;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.stats.ConnectionTracker;
import com.google.android.gms.common.util.Clock;
import com.google.android.gms.common.util.VisibleForTesting;
import com.google.android.gms.internal.measurement.zzp;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.atomic.AtomicReference;

@VisibleForTesting
public final class zzhv extends zzg {
    /* access modifiers changed from: private */
    public final zzin zzre;
    /* access modifiers changed from: private */
    public zzdx zzrf;
    private volatile Boolean zzrg;
    private final zzaa zzrh;
    private final zzjd zzri;
    private final List<Runnable> zzrj = new ArrayList();
    private final zzaa zzrk;

    protected zzhv(zzfj zzfj) {
        super(zzfj);
        this.zzri = new zzjd(zzfj.zzx());
        this.zzre = new zzin(this);
        this.zzrh = new zzhu(this, zzfj);
        this.zzrk = new zzif(this, zzfj);
    }

    /* access modifiers changed from: private */
    @WorkerThread
    public final void onServiceDisconnected(ComponentName componentName) {
        zzo();
        if (this.zzrf != null) {
            this.zzrf = null;
            zzab().zzgs().zza("Disconnected from device MeasurementService", componentName);
            zzo();
            zzis();
        }
    }

    @WorkerThread
    private final void zzd(Runnable runnable) throws IllegalStateException {
        zzo();
        if (isConnected()) {
            runnable.run();
        } else if (((long) this.zzrj.size()) >= 1000) {
            zzab().zzgk().zzao("Discarding data. Max runnable queue size reached");
        } else {
            this.zzrj.add(runnable);
            this.zzrk.zzv(60000);
            zzis();
        }
    }

    @Nullable
    @WorkerThread
    private final zzn zzi(boolean z) {
        zzae();
        return zzr().zzai(z ? zzab().zzgu() : null);
    }

    private final boolean zziq() {
        zzae();
        return true;
    }

    /* access modifiers changed from: private */
    @WorkerThread
    public final void zzir() {
        zzo();
        this.zzri.start();
        this.zzrh.zzv(((Long) zzak.zzhl.get(null)).longValue());
    }

    /* access modifiers changed from: private */
    @WorkerThread
    public final void zziu() {
        zzo();
        if (isConnected()) {
            zzab().zzgs().zzao("Inactivity, disconnecting from the service");
            disconnect();
        }
    }

    /* access modifiers changed from: private */
    @WorkerThread
    public final void zziv() {
        zzo();
        zzab().zzgs().zza("Processing queued up service tasks", Integer.valueOf(this.zzrj.size()));
        for (Runnable run : this.zzrj) {
            try {
                run.run();
            } catch (Exception e) {
                zzab().zzgk().zza("Task exception while flushing queue", e);
            }
        }
        this.zzrj.clear();
        this.zzrk.cancel();
    }

    @WorkerThread
    public final void disconnect() {
        zzo();
        zzbi();
        this.zzre.zziw();
        try {
            ConnectionTracker.getInstance().unbindService(getContext(), this.zzre);
        } catch (IllegalArgumentException | IllegalStateException e) {
        }
        this.zzrf = null;
    }

    @WorkerThread
    public final void getAppInstanceId(zzp zzp) {
        zzo();
        zzbi();
        zzd((Runnable) new zzib(this, zzi(false), zzp));
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    @WorkerThread
    public final boolean isConnected() {
        zzo();
        zzbi();
        return this.zzrf != null;
    }

    /* access modifiers changed from: protected */
    @WorkerThread
    public final void resetAnalyticsData() {
        zzo();
        zzm();
        zzbi();
        zzn zzi = zzi(false);
        if (zziq()) {
            zzu().resetAnalyticsData();
        }
        zzd((Runnable) new zzhz(this, zzi));
    }

    @WorkerThread
    public final void zza(zzp zzp, zzai zzai, String str) {
        zzo();
        zzbi();
        if (zzz().zzd(12451000) != 0) {
            zzab().zzgn().zzao("Not bundling data. Service unavailable or out of date");
            zzz().zza(zzp, new byte[0]);
            return;
        }
        zzd((Runnable) new zzic(this, zzai, str, zzp));
    }

    /* access modifiers changed from: protected */
    @WorkerThread
    public final void zza(zzp zzp, String str, String str2) {
        zzo();
        zzbi();
        zzd((Runnable) new zzii(this, str, str2, zzi(false), zzp));
    }

    /* access modifiers changed from: protected */
    @WorkerThread
    public final void zza(zzp zzp, String str, String str2, boolean z) {
        zzo();
        zzbi();
        zzd((Runnable) new zzik(this, str, str2, z, zzi(false), zzp));
    }

    /* access modifiers changed from: protected */
    @WorkerThread
    @VisibleForTesting
    public final void zza(zzdx zzdx) {
        zzo();
        Preconditions.checkNotNull(zzdx);
        this.zzrf = zzdx;
        zzir();
        zziv();
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Removed duplicated region for block: B:14:0x0042  */
    @android.support.annotation.WorkerThread
    @com.google.android.gms.common.util.VisibleForTesting
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void zza(com.google.android.gms.measurement.internal.zzdx r12, com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable r13, com.google.android.gms.measurement.internal.zzn r14) {
        /*
            r11 = this;
            r3 = 0
            r6 = 100
            r11.zzo()
            r11.zzm()
            r11.zzbi()
            boolean r7 = r11.zziq()
            r5 = r3
            r4 = r6
        L_0x0012:
            r0 = 1001(0x3e9, float:1.403E-42)
            if (r5 >= r0) goto L_0x00aa
            if (r4 != r6) goto L_0x00aa
            java.util.ArrayList r0 = new java.util.ArrayList
            r0.<init>()
            if (r7 == 0) goto L_0x00a8
            com.google.android.gms.measurement.internal.zzeb r1 = r11.zzu()
            java.util.List r1 = r1.zzc(r6)
            if (r1 == 0) goto L_0x00a8
            r0.addAll(r1)
            int r1 = r1.size()
            r4 = r1
        L_0x0031:
            if (r13 == 0) goto L_0x0038
            if (r4 >= r6) goto L_0x0038
            r0.add(r13)
        L_0x0038:
            r1 = r0
            java.util.ArrayList r1 = (java.util.ArrayList) r1
            int r8 = r1.size()
            r2 = r3
        L_0x0040:
            if (r2 >= r8) goto L_0x00a3
            java.lang.Object r0 = r1.get(r2)
            int r2 = r2 + 1
            com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable r0 = (com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable) r0
            boolean r9 = r0 instanceof com.google.android.gms.measurement.internal.zzai
            if (r9 == 0) goto L_0x0063
            com.google.android.gms.measurement.internal.zzai r0 = (com.google.android.gms.measurement.internal.zzai) r0     // Catch:{ RemoteException -> 0x0054 }
            r12.zza(r0, r14)     // Catch:{ RemoteException -> 0x0054 }
            goto L_0x0040
        L_0x0054:
            r0 = move-exception
            com.google.android.gms.measurement.internal.zzef r9 = r11.zzab()
            com.google.android.gms.measurement.internal.zzeh r9 = r9.zzgk()
            java.lang.String r10 = "Failed to send event to the service"
            r9.zza(r10, r0)
            goto L_0x0040
        L_0x0063:
            boolean r9 = r0 instanceof com.google.android.gms.measurement.internal.zzjn
            if (r9 == 0) goto L_0x007c
            com.google.android.gms.measurement.internal.zzjn r0 = (com.google.android.gms.measurement.internal.zzjn) r0     // Catch:{ RemoteException -> 0x006d }
            r12.zza(r0, r14)     // Catch:{ RemoteException -> 0x006d }
            goto L_0x0040
        L_0x006d:
            r0 = move-exception
            com.google.android.gms.measurement.internal.zzef r9 = r11.zzab()
            com.google.android.gms.measurement.internal.zzeh r9 = r9.zzgk()
            java.lang.String r10 = "Failed to send attribute to the service"
            r9.zza(r10, r0)
            goto L_0x0040
        L_0x007c:
            boolean r9 = r0 instanceof com.google.android.gms.measurement.internal.zzq
            if (r9 == 0) goto L_0x0095
            com.google.android.gms.measurement.internal.zzq r0 = (com.google.android.gms.measurement.internal.zzq) r0     // Catch:{ RemoteException -> 0x0086 }
            r12.zza(r0, r14)     // Catch:{ RemoteException -> 0x0086 }
            goto L_0x0040
        L_0x0086:
            r0 = move-exception
            com.google.android.gms.measurement.internal.zzef r9 = r11.zzab()
            com.google.android.gms.measurement.internal.zzeh r9 = r9.zzgk()
            java.lang.String r10 = "Failed to send conditional property to the service"
            r9.zza(r10, r0)
            goto L_0x0040
        L_0x0095:
            com.google.android.gms.measurement.internal.zzef r0 = r11.zzab()
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgk()
            java.lang.String r9 = "Discarding data. Unrecognized parcel type."
            r0.zzao(r9)
            goto L_0x0040
        L_0x00a3:
            int r0 = r5 + 1
            r5 = r0
            goto L_0x0012
        L_0x00a8:
            r4 = r3
            goto L_0x0031
        L_0x00aa:
            return
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzhv.zza(com.google.android.gms.measurement.internal.zzdx, com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable, com.google.android.gms.measurement.internal.zzn):void");
    }

    /* access modifiers changed from: protected */
    @WorkerThread
    public final void zza(zzhr zzhr) {
        zzo();
        zzbi();
        zzd((Runnable) new zzid(this, zzhr));
    }

    @WorkerThread
    public final void zza(AtomicReference<String> atomicReference) {
        zzo();
        zzbi();
        zzd((Runnable) new zzhy(this, atomicReference, zzi(false)));
    }

    /* access modifiers changed from: protected */
    @WorkerThread
    public final void zza(AtomicReference<List<zzq>> atomicReference, String str, String str2, String str3) {
        zzo();
        zzbi();
        zzd((Runnable) new zzij(this, atomicReference, str, str2, str3, zzi(false)));
    }

    /* access modifiers changed from: protected */
    @WorkerThread
    public final void zza(AtomicReference<List<zzjn>> atomicReference, String str, String str2, String str3, boolean z) {
        zzo();
        zzbi();
        zzd((Runnable) new zzil(this, atomicReference, str, str2, str3, z, zzi(false)));
    }

    /* access modifiers changed from: protected */
    @WorkerThread
    public final void zza(AtomicReference<List<zzjn>> atomicReference, boolean z) {
        zzo();
        zzbi();
        zzd((Runnable) new zzhw(this, atomicReference, zzi(false), z));
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

    /* access modifiers changed from: protected */
    @WorkerThread
    public final void zzb(zzjn zzjn) {
        zzo();
        zzbi();
        zzd((Runnable) new zzhx(this, zziq() && zzu().zza(zzjn), zzjn, zzi(true)));
    }

    /* access modifiers changed from: protected */
    public final boolean zzbk() {
        return false;
    }

    /* access modifiers changed from: protected */
    @WorkerThread
    public final void zzc(zzai zzai, String str) {
        Preconditions.checkNotNull(zzai);
        zzo();
        zzbi();
        boolean zziq = zziq();
        zzd((Runnable) new zzih(this, zziq, zziq && zzu().zza(zzai), zzai, zzi(true), str));
    }

    /* access modifiers changed from: protected */
    @WorkerThread
    public final void zzd(zzq zzq) {
        Preconditions.checkNotNull(zzq);
        zzo();
        zzbi();
        zzae();
        zzd((Runnable) new zzig(this, true, zzu().zzc(zzq), new zzq(zzq), zzi(true), zzq));
    }

    /* access modifiers changed from: protected */
    @WorkerThread
    public final void zzim() {
        zzo();
        zzbi();
        zzn zzi = zzi(true);
        boolean zza = zzad().zza(zzak.zzjd);
        if (zza) {
            zzu().zzgh();
        }
        zzd((Runnable) new zzia(this, zzi, zza));
    }

    /* access modifiers changed from: protected */
    @WorkerThread
    public final void zzip() {
        zzo();
        zzbi();
        zzd((Runnable) new zzie(this, zzi(true)));
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zzis() {
        boolean z;
        boolean z2;
        boolean z3 = true;
        zzo();
        zzbi();
        if (!isConnected()) {
            if (this.zzrg == null) {
                zzo();
                zzbi();
                Boolean zzhe = zzac().zzhe();
                if (zzhe == null || !zzhe.booleanValue()) {
                    zzae();
                    if (zzr().zzgg() != 1) {
                        zzab().zzgs().zzao("Checking service availability");
                        int zzd = zzz().zzd(12451000);
                        switch (zzd) {
                            case 0:
                                zzab().zzgs().zzao("Service available");
                                z = true;
                                z2 = true;
                                break;
                            case 1:
                                zzab().zzgs().zzao("Service missing");
                                z = false;
                                z2 = true;
                                break;
                            case 2:
                                zzab().zzgr().zzao("Service container out of date");
                                if (zzz().zzjx() >= 15300) {
                                    Boolean zzhe2 = zzac().zzhe();
                                    z = zzhe2 == null || zzhe2.booleanValue();
                                    z2 = false;
                                    break;
                                } else {
                                    z = false;
                                    z2 = true;
                                    break;
                                }
                                break;
                            case 3:
                                zzab().zzgn().zzao("Service disabled");
                                z = false;
                                z2 = false;
                                break;
                            case 9:
                                zzab().zzgn().zzao("Service invalid");
                                z = false;
                                z2 = false;
                                break;
                            case 18:
                                zzab().zzgn().zzao("Service updating");
                                z = true;
                                z2 = true;
                                break;
                            default:
                                zzab().zzgn().zza("Unexpected service status", Integer.valueOf(zzd));
                                z = false;
                                z2 = false;
                                break;
                        }
                    } else {
                        z = true;
                        z2 = true;
                    }
                    if (!z && zzad().zzbw()) {
                        zzab().zzgk().zzao("No way to upload. Consider using the full version of Analytics");
                        z2 = false;
                    }
                    if (z2) {
                        zzac().zzd(z);
                    }
                } else {
                    z = true;
                }
                this.zzrg = Boolean.valueOf(z);
            }
            if (this.zzrg.booleanValue()) {
                this.zzre.zzix();
            } else if (!zzad().zzbw()) {
                zzae();
                List queryIntentServices = getContext().getPackageManager().queryIntentServices(new Intent().setClassName(getContext(), "com.google.android.gms.measurement.AppMeasurementService"), 65536);
                if (queryIntentServices == null || queryIntentServices.size() <= 0) {
                    z3 = false;
                }
                if (z3) {
                    Intent intent = new Intent("com.google.android.gms.measurement.START");
                    Context context = getContext();
                    zzae();
                    intent.setComponent(new ComponentName(context, "com.google.android.gms.measurement.AppMeasurementService"));
                    this.zzre.zzb(intent);
                    return;
                }
                zzab().zzgk().zzao("Unable to use remote or local measurement implementation. Please register the AppMeasurementService service in the app manifest");
            }
        }
    }

    /* access modifiers changed from: 0000 */
    public final Boolean zzit() {
        return this.zzrg;
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
