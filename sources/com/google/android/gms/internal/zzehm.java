package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;
import jp.colopl.util.ImageUtil;

public final class zzehm extends zzego {
    public long zzgbv;
    public String zzngv;
    public String zzngw;
    public long zzngx;
    public String zzngy;
    public long zzngz;
    public String zznha;
    public String zznhb;
    public String zznhc;
    public String zznhd;
    public String zznhe;
    public int zznhf;
    public zzehl[] zznhg;

    public zzehm() {
        this.zzngv = "";
        this.zzngw = "";
        this.zzngx = 0;
        this.zzngy = "";
        this.zzngz = 0;
        this.zzgbv = 0;
        this.zznha = "";
        this.zznhb = "";
        this.zznhc = "";
        this.zznhd = "";
        this.zznhe = "";
        this.zznhf = 0;
        this.zznhg = zzehl.zzcer();
        this.zzndd = -1;
    }

    public static zzehm zzay(byte[] bArr) throws zzegn {
        return (zzehm) zzego.zza(new zzehm(), bArr);
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
                case 18:
                    this.zzngw = zzegf.readString();
                    continue;
                case MotionEventCompat.AXIS_DISTANCE /*24*/:
                    this.zzngx = zzegf.zzcdr();
                    continue;
                case MotionEventCompat.AXIS_GENERIC_3 /*34*/:
                    this.zzngy = zzegf.readString();
                    continue;
                case 40:
                    this.zzngz = zzegf.zzcdr();
                    continue;
                case 48:
                    this.zzgbv = zzegf.zzcdr();
                    continue;
                case 58:
                    this.zznha = zzegf.readString();
                    continue;
                case 66:
                    this.zznhb = zzegf.readString();
                    continue;
                case 74:
                    this.zznhc = zzegf.readString();
                    continue;
                case 82:
                    this.zznhd = zzegf.readString();
                    continue;
                case ImageUtil.OUTPUT_QUALITY /*90*/:
                    this.zznhe = zzegf.readString();
                    continue;
                case 96:
                    this.zznhf = zzegf.zzcbs();
                    continue;
                case 106:
                    int zzb = zzegr.zzb(zzegf, 106);
                    zzcbr = this.zznhg == null ? 0 : this.zznhg.length;
                    Object obj = new zzehl[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zznhg, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = new zzehl();
                        zzegf.zza(obj[zzcbr]);
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = new zzehl();
                    zzegf.zza(obj[zzcbr]);
                    this.zznhg = obj;
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
        if (!(this.zzngw == null || this.zzngw.equals(""))) {
            zzegg.zzl(2, this.zzngw);
        }
        if (this.zzngx != 0) {
            zzegg.zzb(3, this.zzngx);
        }
        if (!(this.zzngy == null || this.zzngy.equals(""))) {
            zzegg.zzl(4, this.zzngy);
        }
        if (this.zzngz != 0) {
            zzegg.zzb(5, this.zzngz);
        }
        if (this.zzgbv != 0) {
            zzegg.zzb(6, this.zzgbv);
        }
        if (!(this.zznha == null || this.zznha.equals(""))) {
            zzegg.zzl(7, this.zznha);
        }
        if (!(this.zznhb == null || this.zznhb.equals(""))) {
            zzegg.zzl(8, this.zznhb);
        }
        if (!(this.zznhc == null || this.zznhc.equals(""))) {
            zzegg.zzl(9, this.zznhc);
        }
        if (!(this.zznhd == null || this.zznhd.equals(""))) {
            zzegg.zzl(10, this.zznhd);
        }
        if (!(this.zznhe == null || this.zznhe.equals(""))) {
            zzegg.zzl(11, this.zznhe);
        }
        if (this.zznhf != 0) {
            zzegg.zzu(12, this.zznhf);
        }
        if (this.zznhg != null && this.zznhg.length > 0) {
            for (zzego zzego : this.zznhg) {
                if (zzego != null) {
                    zzegg.zza(13, zzego);
                }
            }
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int zzn = super.zzn();
        if (!(this.zzngv == null || this.zzngv.equals(""))) {
            zzn += zzegg.zzm(1, this.zzngv);
        }
        if (!(this.zzngw == null || this.zzngw.equals(""))) {
            zzn += zzegg.zzm(2, this.zzngw);
        }
        if (this.zzngx != 0) {
            zzn += zzegg.zze(3, this.zzngx);
        }
        if (!(this.zzngy == null || this.zzngy.equals(""))) {
            zzn += zzegg.zzm(4, this.zzngy);
        }
        if (this.zzngz != 0) {
            zzn += zzegg.zze(5, this.zzngz);
        }
        if (this.zzgbv != 0) {
            zzn += zzegg.zze(6, this.zzgbv);
        }
        if (!(this.zznha == null || this.zznha.equals(""))) {
            zzn += zzegg.zzm(7, this.zznha);
        }
        if (!(this.zznhb == null || this.zznhb.equals(""))) {
            zzn += zzegg.zzm(8, this.zznhb);
        }
        if (!(this.zznhc == null || this.zznhc.equals(""))) {
            zzn += zzegg.zzm(9, this.zznhc);
        }
        if (!(this.zznhd == null || this.zznhd.equals(""))) {
            zzn += zzegg.zzm(10, this.zznhd);
        }
        if (!(this.zznhe == null || this.zznhe.equals(""))) {
            zzn += zzegg.zzm(11, this.zznhe);
        }
        if (this.zznhf != 0) {
            zzn += zzegg.zzv(12, this.zznhf);
        }
        if (this.zznhg == null || this.zznhg.length <= 0) {
            return zzn;
        }
        int i = zzn;
        for (zzego zzego : this.zznhg) {
            if (zzego != null) {
                i += zzegg.zzb(13, zzego);
            }
        }
        return i;
    }
}
