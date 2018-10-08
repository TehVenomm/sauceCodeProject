package com.google.android.gms.internal;

import android.content.ComponentName;
import android.content.Context;
import android.os.RemoteException;
import android.support.annotation.WorkerThread;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.stats.zza;
import com.google.android.gms.common.util.zzd;
import com.google.android.gms.measurement.AppMeasurement.zzb;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.atomic.AtomicReference;

public final class zzceg extends zzcdm {
    private final zzcet zzivp;
    private zzcbg zzivq;
    private Boolean zzivr;
    private final zzcau zzivs;
    private final zzcfi zzivt;
    private final List<Runnable> zzivu = new ArrayList();
    private final zzcau zzivv;

    protected zzceg(zzcco zzcco) {
        super(zzcco);
        this.zzivt = new zzcfi(zzcco.zzvu());
        this.zzivp = new zzcet(this);
        this.zzivs = new zzceh(this, zzcco);
        this.zzivv = new zzcel(this, zzcco);
    }

    @WorkerThread
    private final void onServiceDisconnected(ComponentName componentName) {
        zzug();
        if (this.zzivq != null) {
            this.zzivq = null;
            zzauk().zzayi().zzj("Disconnected from device MeasurementService", componentName);
            zzug();
            zzxe();
        }
    }

    @WorkerThread
    private final void zzazq() {
        zzug();
        zzauk().zzayi().zzj("Processing queued up service tasks", Integer.valueOf(this.zzivu.size()));
        for (Runnable run : this.zzivu) {
            try {
                run.run();
            } catch (Throwable th) {
                zzauk().zzayc().zzj("Task exception while flushing queue", th);
            }
        }
        this.zzivu.clear();
        this.zzivv.cancel();
    }

    @WorkerThread
    private final void zzj(Runnable runnable) throws IllegalStateException {
        zzug();
        if (isConnected()) {
            runnable.run();
        } else if (((long) this.zzivu.size()) >= zzcap.zzawo()) {
            zzauk().zzayc().log("Discarding data. Max runnable queue size reached");
        } else {
            this.zzivu.add(runnable);
            this.zzivv.zzs(60000);
            zzxe();
        }
    }

    @WorkerThread
    private final void zzwt() {
        zzug();
        this.zzivt.start();
        this.zzivs.zzs(zzcap.zzawg());
    }

    @WorkerThread
    private final void zzwu() {
        zzug();
        if (isConnected()) {
            zzauk().zzayi().log("Inactivity, disconnecting from the service");
            disconnect();
        }
    }

    @WorkerThread
    public final void disconnect() {
        zzug();
        zzwh();
        try {
            zza.zzaky();
            getContext().unbindService(this.zzivp);
        } catch (IllegalStateException e) {
        } catch (IllegalArgumentException e2) {
        }
        this.zzivq = null;
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    @WorkerThread
    public final boolean isConnected() {
        zzug();
        zzwh();
        return this.zzivq != null;
    }

    @WorkerThread
    protected final void zza(zzcbg zzcbg) {
        zzug();
        zzbp.zzu(zzcbg);
        this.zzivq = zzcbg;
        zzwt();
        zzazq();
    }

    @WorkerThread
    final void zza(zzcbg zzcbg, com.google.android.gms.common.internal.safeparcel.zza zza) {
        zzug();
        zzatu();
        zzwh();
        zzcap.zzawj();
        List arrayList = new ArrayList();
        zzcap.zzaws();
        int i = 100;
        for (int i2 = 0; i2 < 1001 && r5 == 100; i2++) {
            Object zzdv = zzaud().zzdv(100);
            if (zzdv != null) {
                arrayList.addAll(zzdv);
                i = zzdv.size();
            } else {
                i = 0;
            }
            if (zza != null && r5 < 100) {
                arrayList.add(zza);
            }
            ArrayList arrayList2 = (ArrayList) arrayList;
            int size = arrayList2.size();
            int i3 = 0;
            while (i3 < size) {
                Object obj = arrayList2.get(i3);
                i3++;
                com.google.android.gms.common.internal.safeparcel.zza zza2 = (com.google.android.gms.common.internal.safeparcel.zza) obj;
                if (zza2 instanceof zzcbc) {
                    try {
                        zzcbg.zza((zzcbc) zza2, zzatz().zzjb(zzauk().zzayj()));
                    } catch (RemoteException e) {
                        zzauk().zzayc().zzj("Failed to send event to the service", e);
                    }
                } else if (zza2 instanceof zzcfl) {
                    try {
                        zzcbg.zza((zzcfl) zza2, zzatz().zzjb(zzauk().zzayj()));
                    } catch (RemoteException e2) {
                        zzauk().zzayc().zzj("Failed to send attribute to the service", e2);
                    }
                } else if (zza2 instanceof zzcan) {
                    try {
                        zzcbg.zza((zzcan) zza2, zzatz().zzjb(zzauk().zzayj()));
                    } catch (RemoteException e22) {
                        zzauk().zzayc().zzj("Failed to send conditional property to the service", e22);
                    }
                } else {
                    zzauk().zzayc().log("Discarding data. Unrecognized parcel type.");
                }
            }
        }
    }

    @WorkerThread
    protected final void zza(zzb zzb) {
        zzug();
        zzwh();
        zzj(new zzcek(this, zzb));
    }

    @WorkerThread
    public final void zza(AtomicReference<String> atomicReference) {
        zzug();
        zzwh();
        zzj(new zzcei(this, atomicReference));
    }

    @WorkerThread
    protected final void zza(AtomicReference<List<zzcan>> atomicReference, String str, String str2, String str3) {
        zzug();
        zzwh();
        zzj(new zzcep(this, atomicReference, str, str2, str3));
    }

    @WorkerThread
    protected final void zza(AtomicReference<List<zzcfl>> atomicReference, String str, String str2, String str3, boolean z) {
        zzug();
        zzwh();
        zzj(new zzceq(this, atomicReference, str, str2, str3, z));
    }

    @WorkerThread
    protected final void zza(AtomicReference<List<zzcfl>> atomicReference, boolean z) {
        zzug();
        zzwh();
        zzj(new zzces(this, atomicReference, z));
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
    protected final void zzazo() {
        zzug();
        zzwh();
        zzj(new zzcem(this));
    }

    @WorkerThread
    protected final void zzazp() {
        zzug();
        zzwh();
        zzj(new zzcej(this));
    }

    @WorkerThread
    protected final void zzb(zzcfl zzcfl) {
        zzug();
        zzwh();
        zzcap.zzawj();
        zzj(new zzcer(this, zzaud().zza(zzcfl), zzcfl));
    }

    @WorkerThread
    protected final void zzc(zzcbc zzcbc, String str) {
        zzbp.zzu(zzcbc);
        zzug();
        zzwh();
        zzcap.zzawj();
        zzj(new zzcen(this, true, zzaud().zza(zzcbc), zzcbc, str));
    }

    @WorkerThread
    protected final void zzf(zzcan zzcan) {
        zzbp.zzu(zzcan);
        zzug();
        zzwh();
        zzcap.zzawj();
        zzj(new zzceo(this, true, zzaud().zzc(zzcan), new zzcan(zzcan), zzcan));
    }

    public final /* bridge */ /* synthetic */ void zzug() {
        super.zzug();
    }

    protected final void zzuh() {
    }

    public final /* bridge */ /* synthetic */ zzd zzvu() {
        return super.zzvu();
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    @android.support.annotation.WorkerThread
    final void zzxe() {
        /*
        r6 = this;
        r1 = 0;
        r2 = 1;
        r6.zzug();
        r6.zzwh();
        r0 = r6.isConnected();
        if (r0 == 0) goto L_0x000f;
    L_0x000e:
        return;
    L_0x000f:
        r0 = r6.zzivr;
        if (r0 != 0) goto L_0x0067;
    L_0x0013:
        r0 = r6.zzaul();
        r0 = r0.zzayn();
        r6.zzivr = r0;
        r0 = r6.zzivr;
        if (r0 != 0) goto L_0x0067;
    L_0x0021:
        r0 = r6.zzauk();
        r0 = r0.zzayi();
        r3 = "State of service unknown";
        r0.log(r3);
        r6.zzug();
        r6.zzwh();
        com.google.android.gms.internal.zzcap.zzawj();
        r0 = r6.zzauk();
        r0 = r0.zzayi();
        r3 = "Checking service availability";
        r0.log(r3);
        r0 = com.google.android.gms.common.zze.zzaew();
        r3 = r6.getContext();
        r0 = r0.isGooglePlayServicesAvailable(r3);
        switch(r0) {
            case 0: goto L_0x0082;
            case 1: goto L_0x0091;
            case 2: goto L_0x00ae;
            case 3: goto L_0x00bd;
            case 9: goto L_0x00cb;
            case 18: goto L_0x009f;
            default: goto L_0x0053;
        };
    L_0x0053:
        r0 = r1;
    L_0x0054:
        r0 = java.lang.Boolean.valueOf(r0);
        r6.zzivr = r0;
        r0 = r6.zzaul();
        r3 = r6.zzivr;
        r3 = r3.booleanValue();
        r0.zzbm(r3);
    L_0x0067:
        r0 = r6.zzivr;
        r0 = r0.booleanValue();
        if (r0 == 0) goto L_0x00da;
    L_0x006f:
        r0 = r6.zzauk();
        r0 = r0.zzayi();
        r1 = "Using measurement service";
        r0.log(r1);
        r0 = r6.zzivp;
        r0.zzazr();
        goto L_0x000e;
    L_0x0082:
        r0 = r6.zzauk();
        r0 = r0.zzayi();
        r3 = "Service available";
        r0.log(r3);
        r0 = r2;
        goto L_0x0054;
    L_0x0091:
        r0 = r6.zzauk();
        r0 = r0.zzayi();
        r3 = "Service missing";
        r0.log(r3);
        goto L_0x0053;
    L_0x009f:
        r0 = r6.zzauk();
        r0 = r0.zzaye();
        r3 = "Service updating";
        r0.log(r3);
        r0 = r2;
        goto L_0x0054;
    L_0x00ae:
        r0 = r6.zzauk();
        r0 = r0.zzayh();
        r3 = "Service container out of date";
        r0.log(r3);
        r0 = r2;
        goto L_0x0054;
    L_0x00bd:
        r0 = r6.zzauk();
        r0 = r0.zzaye();
        r3 = "Service disabled";
        r0.log(r3);
        goto L_0x0053;
    L_0x00cb:
        r0 = r6.zzauk();
        r0 = r0.zzaye();
        r3 = "Service invalid";
        r0.log(r3);
        goto L_0x0053;
    L_0x00da:
        com.google.android.gms.internal.zzcap.zzawj();
        r0 = r6.getContext();
        r0 = r0.getPackageManager();
        r3 = new android.content.Intent;
        r3.<init>();
        r4 = r6.getContext();
        r5 = "com.google.android.gms.measurement.AppMeasurementService";
        r3 = r3.setClassName(r4, r5);
        r4 = 65536; // 0x10000 float:9.18355E-41 double:3.2379E-319;
        r0 = r0.queryIntentServices(r3, r4);
        if (r0 == 0) goto L_0x0130;
    L_0x00fc:
        r0 = r0.size();
        if (r0 <= 0) goto L_0x0130;
    L_0x0102:
        if (r2 == 0) goto L_0x0132;
    L_0x0104:
        r0 = r6.zzauk();
        r0 = r0.zzayi();
        r1 = "Using local app measurement service";
        r0.log(r1);
        r0 = new android.content.Intent;
        r1 = "com.google.android.gms.measurement.START";
        r0.<init>(r1);
        r1 = r6.getContext();
        com.google.android.gms.internal.zzcap.zzawj();
        r2 = new android.content.ComponentName;
        r3 = "com.google.android.gms.measurement.AppMeasurementService";
        r2.<init>(r1, r3);
        r0.setComponent(r2);
        r1 = r6.zzivp;
        r1.zzk(r0);
        goto L_0x000e;
    L_0x0130:
        r2 = r1;
        goto L_0x0102;
    L_0x0132:
        r0 = r6.zzauk();
        r0 = r0.zzayc();
        r1 = "Unable to use remote or local measurement implementation. Please register the AppMeasurementService service in the app manifest";
        r0.log(r1);
        goto L_0x000e;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzceg.zzxe():void");
    }
}
