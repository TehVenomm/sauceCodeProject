package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

public final class zzehf extends zzegi<zzehf> implements Cloneable {
    private String[] zznfo;
    private String[] zznfp;
    private int[] zznfq;
    private long[] zznfr;
    private long[] zznfs;

    public zzehf() {
        this.zznfo = zzegr.EMPTY_STRING_ARRAY;
        this.zznfp = zzegr.EMPTY_STRING_ARRAY;
        this.zznfq = zzegr.zzndi;
        this.zznfr = zzegr.zzndj;
        this.zznfs = zzegr.zzndj;
        this.zzncu = null;
        this.zzndd = -1;
    }

    private zzehf zzcek() {
        try {
            zzehf zzehf = (zzehf) super.zzcdy();
            if (this.zznfo != null && this.zznfo.length > 0) {
                zzehf.zznfo = (String[]) this.zznfo.clone();
            }
            if (this.zznfp != null && this.zznfp.length > 0) {
                zzehf.zznfp = (String[]) this.zznfp.clone();
            }
            if (this.zznfq != null && this.zznfq.length > 0) {
                zzehf.zznfq = (int[]) this.zznfq.clone();
            }
            if (this.zznfr != null && this.zznfr.length > 0) {
                zzehf.zznfr = (long[]) this.zznfr.clone();
            }
            if (this.zznfs != null && this.zznfs.length > 0) {
                zzehf.zznfs = (long[]) this.zznfs.clone();
            }
            return zzehf;
        } catch (CloneNotSupportedException e) {
            throw new AssertionError(e);
        }
    }

    public final /* synthetic */ Object clone() throws CloneNotSupportedException {
        return zzcek();
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzehf)) {
            return false;
        }
        zzehf zzehf = (zzehf) obj;
        return !zzegm.equals(this.zznfo, zzehf.zznfo) ? false : !zzegm.equals(this.zznfp, zzehf.zznfp) ? false : !zzegm.equals(this.zznfq, zzehf.zznfq) ? false : !zzegm.equals(this.zznfr, zzehf.zznfr) ? false : !zzegm.equals(this.zznfs, zzehf.zznfs) ? false : (this.zzncu == null || this.zzncu.isEmpty()) ? zzehf.zzncu == null || zzehf.zzncu.isEmpty() : this.zzncu.equals(zzehf.zzncu);
    }

    public final int hashCode() {
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = zzegm.hashCode(this.zznfo);
        int hashCode3 = zzegm.hashCode(this.zznfp);
        int hashCode4 = zzegm.hashCode(this.zznfq);
        int hashCode5 = zzegm.hashCode(this.zznfr);
        int hashCode6 = zzegm.hashCode(this.zznfs);
        int hashCode7 = (this.zzncu == null || this.zzncu.isEmpty()) ? 0 : this.zzncu.hashCode();
        return hashCode7 + ((((((((((((hashCode + 527) * 31) + hashCode2) * 31) + hashCode3) * 31) + hashCode4) * 31) + hashCode5) * 31) + hashCode6) * 31);
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
                case 10:
                    zzb = zzegr.zzb(zzegf, 10);
                    zzcbr = this.zznfo == null ? 0 : this.zznfo.length;
                    obj = new String[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zznfo, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = zzegf.readString();
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = zzegf.readString();
                    this.zznfo = obj;
                    continue;
                case 18:
                    zzb = zzegr.zzb(zzegf, 18);
                    zzcbr = this.zznfp == null ? 0 : this.zznfp.length;
                    obj = new String[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zznfp, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = zzegf.readString();
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = zzegf.readString();
                    this.zznfp = obj;
                    continue;
                case MotionEventCompat.AXIS_DISTANCE /*24*/:
                    zzb = zzegr.zzb(zzegf, 24);
                    zzcbr = this.zznfq == null ? 0 : this.zznfq.length;
                    obj = new int[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zznfq, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = zzegf.zzcbs();
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = zzegf.zzcbs();
                    this.zznfq = obj;
                    continue;
                case 26:
                    zzgm = zzegf.zzgm(zzegf.zzcbz());
                    zzb = zzegf.getPosition();
                    zzcbr = 0;
                    while (zzegf.zzcdx() > 0) {
                        zzegf.zzcbs();
                        zzcbr++;
                    }
                    zzegf.zzha(zzb);
                    zzb = this.zznfq == null ? 0 : this.zznfq.length;
                    obj2 = new int[(zzcbr + zzb)];
                    if (zzb != 0) {
                        System.arraycopy(this.zznfq, 0, obj2, 0, zzb);
                    }
                    while (zzb < obj2.length) {
                        obj2[zzb] = zzegf.zzcbs();
                        zzb++;
                    }
                    this.zznfq = obj2;
                    zzegf.zzgn(zzgm);
                    continue;
                case 32:
                    zzb = zzegr.zzb(zzegf, 32);
                    zzcbr = this.zznfr == null ? 0 : this.zznfr.length;
                    obj = new long[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zznfr, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = zzegf.zzcdr();
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = zzegf.zzcdr();
                    this.zznfr = obj;
                    continue;
                case MotionEventCompat.AXIS_GENERIC_3 /*34*/:
                    zzgm = zzegf.zzgm(zzegf.zzcbz());
                    zzb = zzegf.getPosition();
                    zzcbr = 0;
                    while (zzegf.zzcdx() > 0) {
                        zzegf.zzcdr();
                        zzcbr++;
                    }
                    zzegf.zzha(zzb);
                    zzb = this.zznfr == null ? 0 : this.zznfr.length;
                    obj2 = new long[(zzcbr + zzb)];
                    if (zzb != 0) {
                        System.arraycopy(this.zznfr, 0, obj2, 0, zzb);
                    }
                    while (zzb < obj2.length) {
                        obj2[zzb] = zzegf.zzcdr();
                        zzb++;
                    }
                    this.zznfr = obj2;
                    zzegf.zzgn(zzgm);
                    continue;
                case 40:
                    zzb = zzegr.zzb(zzegf, 40);
                    zzcbr = this.zznfs == null ? 0 : this.zznfs.length;
                    obj = new long[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zznfs, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = zzegf.zzcdr();
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = zzegf.zzcdr();
                    this.zznfs = obj;
                    continue;
                case MotionEventCompat.AXIS_GENERIC_11 /*42*/:
                    zzgm = zzegf.zzgm(zzegf.zzcbz());
                    zzb = zzegf.getPosition();
                    zzcbr = 0;
                    while (zzegf.zzcdx() > 0) {
                        zzegf.zzcdr();
                        zzcbr++;
                    }
                    zzegf.zzha(zzb);
                    zzb = this.zznfs == null ? 0 : this.zznfs.length;
                    obj2 = new long[(zzcbr + zzb)];
                    if (zzb != 0) {
                        System.arraycopy(this.zznfs, 0, obj2, 0, zzb);
                    }
                    while (zzb < obj2.length) {
                        obj2[zzb] = zzegf.zzcdr();
                        zzb++;
                    }
                    this.zznfs = obj2;
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
        if (this.zznfo != null && this.zznfo.length > 0) {
            for (String str : this.zznfo) {
                if (str != null) {
                    zzegg.zzl(1, str);
                }
            }
        }
        if (this.zznfp != null && this.zznfp.length > 0) {
            for (String str2 : this.zznfp) {
                if (str2 != null) {
                    zzegg.zzl(2, str2);
                }
            }
        }
        if (this.zznfq != null && this.zznfq.length > 0) {
            for (int zzu : this.zznfq) {
                zzegg.zzu(3, zzu);
            }
        }
        if (this.zznfr != null && this.zznfr.length > 0) {
            for (long zzb : this.zznfr) {
                zzegg.zzb(4, zzb);
            }
        }
        if (this.zznfs != null && this.zznfs.length > 0) {
            while (i < this.zznfs.length) {
                zzegg.zzb(5, this.zznfs[i]);
                i++;
            }
        }
        super.zza(zzegg);
    }

    public final /* synthetic */ zzegi zzcdy() throws CloneNotSupportedException {
        return (zzehf) clone();
    }

    public final /* synthetic */ zzego zzcdz() throws CloneNotSupportedException {
        return (zzehf) clone();
    }

    protected final int zzn() {
        int i;
        int i2;
        int i3 = 0;
        int zzn = super.zzn();
        if (this.zznfo != null && this.zznfo.length > 0) {
            i = 0;
            i2 = 0;
            for (String str : this.zznfo) {
                if (str != null) {
                    i++;
                    i2 += zzegg.zzrc(str);
                }
            }
            zzn = (i * 1) + (i2 + zzn);
        }
        if (this.zznfp != null && this.zznfp.length > 0) {
            i = 0;
            i2 = 0;
            for (String str2 : this.zznfp) {
                if (str2 != null) {
                    i2++;
                    i += zzegg.zzrc(str2);
                }
            }
            zzn = (i + zzn) + (i2 * 1);
        }
        if (this.zznfq != null && this.zznfq.length > 0) {
            i2 = 0;
            for (int zzgs : this.zznfq) {
                i2 += zzegg.zzgs(zzgs);
            }
            zzn = (zzn + i2) + (this.zznfq.length * 1);
        }
        if (this.zznfr != null && this.zznfr.length > 0) {
            i = 0;
            for (long zzcp : this.zznfr) {
                i += zzegg.zzcp(zzcp);
            }
            zzn = (i + zzn) + (this.zznfr.length * 1);
        }
        if (this.zznfs == null || this.zznfs.length <= 0) {
            return zzn;
        }
        i = 0;
        while (i3 < this.zznfs.length) {
            i += zzegg.zzcp(this.zznfs[i3]);
            i3++;
        }
        return (i + zzn) + (this.zznfs.length * 1);
    }
}
