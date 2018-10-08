package com.google.android.gms.internal;

import java.io.IOException;

public final class zzcfp extends zzegi<zzcfp> {
    private static volatile zzcfp[] zzixd;
    public Integer zzixe;
    public zzcft[] zzixf;
    public zzcfq[] zzixg;

    public zzcfp() {
        this.zzixe = null;
        this.zzixf = zzcft.zzbab();
        this.zzixg = zzcfq.zzazz();
        this.zzncu = null;
        this.zzndd = -1;
    }

    public static zzcfp[] zzazy() {
        if (zzixd == null) {
            synchronized (zzegm.zzndc) {
                if (zzixd == null) {
                    zzixd = new zzcfp[0];
                }
            }
        }
        return zzixd;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzcfp)) {
            return false;
        }
        zzcfp zzcfp = (zzcfp) obj;
        if (this.zzixe == null) {
            if (zzcfp.zzixe != null) {
                return false;
            }
        } else if (!this.zzixe.equals(zzcfp.zzixe)) {
            return false;
        }
        return !zzegm.equals(this.zzixf, zzcfp.zzixf) ? false : !zzegm.equals(this.zzixg, zzcfp.zzixg) ? false : (this.zzncu == null || this.zzncu.isEmpty()) ? zzcfp.zzncu == null || zzcfp.zzncu.isEmpty() : this.zzncu.equals(zzcfp.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = this.zzixe == null ? 0 : this.zzixe.hashCode();
        int hashCode3 = zzegm.hashCode(this.zzixf);
        int hashCode4 = zzegm.hashCode(this.zzixg);
        if (!(this.zzncu == null || this.zzncu.isEmpty())) {
            i = this.zzncu.hashCode();
        }
        return ((((((hashCode2 + ((hashCode + 527) * 31)) * 31) + hashCode3) * 31) + hashCode4) * 31) + i;
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            int zzb;
            Object obj;
            switch (zzcbr) {
                case 0:
                    break;
                case 8:
                    this.zzixe = Integer.valueOf(zzegf.zzcbz());
                    continue;
                case 18:
                    zzb = zzegr.zzb(zzegf, 18);
                    zzcbr = this.zzixf == null ? 0 : this.zzixf.length;
                    obj = new zzcft[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zzixf, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = new zzcft();
                        zzegf.zza(obj[zzcbr]);
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = new zzcft();
                    zzegf.zza(obj[zzcbr]);
                    this.zzixf = obj;
                    continue;
                case 26:
                    zzb = zzegr.zzb(zzegf, 26);
                    zzcbr = this.zzixg == null ? 0 : this.zzixg.length;
                    obj = new zzcfq[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zzixg, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = new zzcfq();
                        zzegf.zza(obj[zzcbr]);
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = new zzcfq();
                    zzegf.zza(obj[zzcbr]);
                    this.zzixg = obj;
                    continue;
                default:
                    if (!super.zza(zzegf, zzcbr)) {
                        break;
                    }
                    continue;
            }
            return this;
        }
    }

    public final void zza(zzegg zzegg) throws IOException {
        int i = 0;
        if (this.zzixe != null) {
            zzegg.zzu(1, this.zzixe.intValue());
        }
        if (this.zzixf != null && this.zzixf.length > 0) {
            for (zzego zzego : this.zzixf) {
                if (zzego != null) {
                    zzegg.zza(2, zzego);
                }
            }
        }
        if (this.zzixg != null && this.zzixg.length > 0) {
            while (i < this.zzixg.length) {
                zzego zzego2 = this.zzixg[i];
                if (zzego2 != null) {
                    zzegg.zza(3, zzego2);
                }
                i++;
            }
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int i = 0;
        int zzn = super.zzn();
        if (this.zzixe != null) {
            zzn += zzegg.zzv(1, this.zzixe.intValue());
        }
        if (this.zzixf != null && this.zzixf.length > 0) {
            int i2 = zzn;
            for (zzego zzego : this.zzixf) {
                if (zzego != null) {
                    i2 += zzegg.zzb(2, zzego);
                }
            }
            zzn = i2;
        }
        if (this.zzixg != null && this.zzixg.length > 0) {
            while (i < this.zzixg.length) {
                zzego zzego2 = this.zzixg[i];
                if (zzego2 != null) {
                    zzn += zzegg.zzb(3, zzego2);
                }
                i++;
            }
        }
        return zzn;
    }
}
