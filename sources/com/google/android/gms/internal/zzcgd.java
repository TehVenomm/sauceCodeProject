package com.google.android.gms.internal;

import java.io.IOException;

public final class zzcgd extends zzegi<zzcgd> {
    public long[] zzjaa;
    public long[] zzjab;

    public zzcgd() {
        this.zzjaa = zzegr.zzndj;
        this.zzjab = zzegr.zzndj;
        this.zzncu = null;
        this.zzndd = -1;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzcgd)) {
            return false;
        }
        zzcgd zzcgd = (zzcgd) obj;
        return !zzegm.equals(this.zzjaa, zzcgd.zzjaa) ? false : !zzegm.equals(this.zzjab, zzcgd.zzjab) ? false : (this.zzncu == null || this.zzncu.isEmpty()) ? zzcgd.zzncu == null || zzcgd.zzncu.isEmpty() : this.zzncu.equals(zzcgd.zzncu);
    }

    public final int hashCode() {
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = zzegm.hashCode(this.zzjaa);
        int hashCode3 = zzegm.hashCode(this.zzjab);
        int hashCode4 = (this.zzncu == null || this.zzncu.isEmpty()) ? 0 : this.zzncu.hashCode();
        return hashCode4 + ((((((hashCode + 527) * 31) + hashCode2) * 31) + hashCode3) * 31);
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            int zzb;
            Object obj;
            int zzgm;
            Object obj2;
            switch (zzcbr) {
                case 0:
                    break;
                case 8:
                    zzb = zzegr.zzb(zzegf, 8);
                    zzcbr = this.zzjaa == null ? 0 : this.zzjaa.length;
                    obj = new long[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zzjaa, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = zzegf.zzcdu();
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = zzegf.zzcdu();
                    this.zzjaa = obj;
                    continue;
                case 10:
                    zzgm = zzegf.zzgm(zzegf.zzcbz());
                    zzb = zzegf.getPosition();
                    zzcbr = 0;
                    while (zzegf.zzcdx() > 0) {
                        zzegf.zzcdu();
                        zzcbr++;
                    }
                    zzegf.zzha(zzb);
                    zzb = this.zzjaa == null ? 0 : this.zzjaa.length;
                    obj2 = new long[(zzcbr + zzb)];
                    if (zzb != 0) {
                        System.arraycopy(this.zzjaa, 0, obj2, 0, zzb);
                    }
                    while (zzb < obj2.length) {
                        obj2[zzb] = zzegf.zzcdu();
                        zzb++;
                    }
                    this.zzjaa = obj2;
                    zzegf.zzgn(zzgm);
                    continue;
                case 16:
                    zzb = zzegr.zzb(zzegf, 16);
                    zzcbr = this.zzjab == null ? 0 : this.zzjab.length;
                    obj = new long[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zzjab, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = zzegf.zzcdu();
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = zzegf.zzcdu();
                    this.zzjab = obj;
                    continue;
                case 18:
                    zzgm = zzegf.zzgm(zzegf.zzcbz());
                    zzb = zzegf.getPosition();
                    zzcbr = 0;
                    while (zzegf.zzcdx() > 0) {
                        zzegf.zzcdu();
                        zzcbr++;
                    }
                    zzegf.zzha(zzb);
                    zzb = this.zzjab == null ? 0 : this.zzjab.length;
                    obj2 = new long[(zzcbr + zzb)];
                    if (zzb != 0) {
                        System.arraycopy(this.zzjab, 0, obj2, 0, zzb);
                    }
                    while (zzb < obj2.length) {
                        obj2[zzb] = zzegf.zzcdu();
                        zzb++;
                    }
                    this.zzjab = obj2;
                    zzegf.zzgn(zzgm);
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
        if (this.zzjaa != null && this.zzjaa.length > 0) {
            for (long zza : this.zzjaa) {
                zzegg.zza(1, zza);
            }
        }
        if (this.zzjab != null && this.zzjab.length > 0) {
            while (i < this.zzjab.length) {
                zzegg.zza(2, this.zzjab[i]);
                i++;
            }
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int i;
        int i2;
        int i3 = 0;
        int zzn = super.zzn();
        if (this.zzjaa == null || this.zzjaa.length <= 0) {
            i = zzn;
        } else {
            i2 = 0;
            for (long zzcp : this.zzjaa) {
                i2 += zzegg.zzcp(zzcp);
            }
            i = (this.zzjaa.length * 1) + (i2 + zzn);
        }
        if (this.zzjab == null || this.zzjab.length <= 0) {
            return i;
        }
        i2 = 0;
        while (i3 < this.zzjab.length) {
            i2 += zzegg.zzcp(this.zzjab[i3]);
            i3++;
        }
        return (i2 + i) + (this.zzjab.length * 1);
    }
}
