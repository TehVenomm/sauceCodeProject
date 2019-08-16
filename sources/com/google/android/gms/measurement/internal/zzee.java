package com.google.android.gms.measurement.internal;

final class zzee implements Runnable {
    private final /* synthetic */ int zzka;
    private final /* synthetic */ String zzkb;
    private final /* synthetic */ Object zzkc;
    private final /* synthetic */ Object zzkd;
    private final /* synthetic */ Object zzke;
    private final /* synthetic */ zzef zzkf;

    zzee(zzef zzef, int i, String str, Object obj, Object obj2, Object obj3) {
        this.zzkf = zzef;
        this.zzka = i;
        this.zzkb = str;
        this.zzkc = obj;
        this.zzkd = obj2;
        this.zzke = obj3;
    }

    public final void run() {
        zzeo zzac = this.zzkf.zzj.zzac();
        if (zzac.isInitialized()) {
            if (this.zzkf.zzkg == 0) {
                if (this.zzkf.zzad().zzbn()) {
                    zzef zzef = this.zzkf;
                    this.zzkf.zzae();
                    zzef.zzkg = 'C';
                } else {
                    zzef zzef2 = this.zzkf;
                    this.zzkf.zzae();
                    zzef2.zzkg = 'c';
                }
            }
            if (this.zzkf.zzr < 0) {
                this.zzkf.zzr = this.zzkf.zzad().zzao();
            }
            char charAt = "01VDIWEA?".charAt(this.zzka);
            char zza = this.zzkf.zzkg;
            long zzb = this.zzkf.zzr;
            String zza2 = zzef.zza(true, this.zzkb, this.zzkc, this.zzkd, this.zzke);
            String sb = new StringBuilder(String.valueOf(zza2).length() + 24).append("2").append(charAt).append(zza).append(zzb).append(":").append(zza2).toString();
            if (sb.length() > 1024) {
                sb = this.zzkb.substring(0, 1024);
            }
            zzac.zzli.zzc(sb, 1);
            return;
        }
        this.zzkf.zza(6, "Persisted config not initialized. Not logging error/warn");
    }
}
