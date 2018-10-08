package com.google.android.gms.internal;

import java.io.IOException;

public final class zzehl extends zzego {
    private static volatile zzehl[] zzngu;
    public String zzngv;

    public zzehl() {
        this.zzngv = "";
        this.zzndd = -1;
    }

    public static zzehl[] zzcer() {
        if (zzngu == null) {
            synchronized (zzegm.zzndc) {
                if (zzngu == null) {
                    zzngu = new zzehl[0];
                }
            }
        }
        return zzngu;
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            switch (zzcbr) {
                case 0:
                    break;
                case 10:
                    this.zzngv = zzegf.readString();
                    continue;
                default:
                    if (!zzegf.zzgl(zzcbr)) {
                        break;
                    }
                    continue;
            }
            return this;
        }
    }

    public final void zza(zzegg zzegg) throws IOException {
        if (!(this.zzngv == null || this.zzngv.equals(""))) {
            zzegg.zzl(1, this.zzngv);
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int zzn = super.zzn();
        return (this.zzngv == null || this.zzngv.equals("")) ? zzn : zzn + zzegg.zzm(1, this.zzngv);
    }
}
