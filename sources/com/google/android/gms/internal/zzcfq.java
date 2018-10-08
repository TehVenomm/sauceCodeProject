package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

public final class zzcfq extends zzegi<zzcfq> {
    private static volatile zzcfq[] zzixh;
    public Integer zzixi;
    public String zzixj;
    public zzcfr[] zzixk;
    private Boolean zzixl;
    public zzcfs zzixm;

    public zzcfq() {
        this.zzixi = null;
        this.zzixj = null;
        this.zzixk = zzcfr.zzbaa();
        this.zzixl = null;
        this.zzixm = null;
        this.zzncu = null;
        this.zzndd = -1;
    }

    public static zzcfq[] zzazz() {
        if (zzixh == null) {
            synchronized (zzegm.zzndc) {
                if (zzixh == null) {
                    zzixh = new zzcfq[0];
                }
            }
        }
        return zzixh;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzcfq)) {
            return false;
        }
        zzcfq zzcfq = (zzcfq) obj;
        if (this.zzixi == null) {
            if (zzcfq.zzixi != null) {
                return false;
            }
        } else if (!this.zzixi.equals(zzcfq.zzixi)) {
            return false;
        }
        if (this.zzixj == null) {
            if (zzcfq.zzixj != null) {
                return false;
            }
        } else if (!this.zzixj.equals(zzcfq.zzixj)) {
            return false;
        }
        if (!zzegm.equals(this.zzixk, zzcfq.zzixk)) {
            return false;
        }
        if (this.zzixl == null) {
            if (zzcfq.zzixl != null) {
                return false;
            }
        } else if (!this.zzixl.equals(zzcfq.zzixl)) {
            return false;
        }
        if (this.zzixm == null) {
            if (zzcfq.zzixm != null) {
                return false;
            }
        } else if (!this.zzixm.equals(zzcfq.zzixm)) {
            return false;
        }
        return (this.zzncu == null || this.zzncu.isEmpty()) ? zzcfq.zzncu == null || zzcfq.zzncu.isEmpty() : this.zzncu.equals(zzcfq.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = this.zzixi == null ? 0 : this.zzixi.hashCode();
        int hashCode3 = this.zzixj == null ? 0 : this.zzixj.hashCode();
        int hashCode4 = zzegm.hashCode(this.zzixk);
        int hashCode5 = this.zzixl == null ? 0 : this.zzixl.hashCode();
        int hashCode6 = this.zzixm == null ? 0 : this.zzixm.hashCode();
        if (!(this.zzncu == null || this.zzncu.isEmpty())) {
            i = this.zzncu.hashCode();
        }
        return ((((((((((hashCode2 + ((hashCode + 527) * 31)) * 31) + hashCode3) * 31) + hashCode4) * 31) + hashCode5) * 31) + hashCode6) * 31) + i;
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            switch (zzcbr) {
                case 0:
                    break;
                case 8:
                    this.zzixi = Integer.valueOf(zzegf.zzcbz());
                    continue;
                case 18:
                    this.zzixj = zzegf.readString();
                    continue;
                case 26:
                    int zzb = zzegr.zzb(zzegf, 26);
                    zzcbr = this.zzixk == null ? 0 : this.zzixk.length;
                    Object obj = new zzcfr[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zzixk, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = new zzcfr();
                        zzegf.zza(obj[zzcbr]);
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = new zzcfr();
                    zzegf.zza(obj[zzcbr]);
                    this.zzixk = obj;
                    continue;
                case 32:
                    this.zzixl = Boolean.valueOf(zzegf.zzcds());
                    continue;
                case MotionEventCompat.AXIS_GENERIC_11 /*42*/:
                    if (this.zzixm == null) {
                        this.zzixm = new zzcfs();
                    }
                    zzegf.zza(this.zzixm);
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
        if (this.zzixi != null) {
            zzegg.zzu(1, this.zzixi.intValue());
        }
        if (this.zzixj != null) {
            zzegg.zzl(2, this.zzixj);
        }
        if (this.zzixk != null && this.zzixk.length > 0) {
            for (zzego zzego : this.zzixk) {
                if (zzego != null) {
                    zzegg.zza(3, zzego);
                }
            }
        }
        if (this.zzixl != null) {
            zzegg.zzl(4, this.zzixl.booleanValue());
        }
        if (this.zzixm != null) {
            zzegg.zza(5, this.zzixm);
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int zzn = super.zzn();
        if (this.zzixi != null) {
            zzn += zzegg.zzv(1, this.zzixi.intValue());
        }
        if (this.zzixj != null) {
            zzn += zzegg.zzm(2, this.zzixj);
        }
        if (this.zzixk != null && this.zzixk.length > 0) {
            int i = zzn;
            for (zzego zzego : this.zzixk) {
                if (zzego != null) {
                    i += zzegg.zzb(3, zzego);
                }
            }
            zzn = i;
        }
        if (this.zzixl != null) {
            this.zzixl.booleanValue();
            zzn += zzegg.zzgr(4) + 1;
        }
        return this.zzixm != null ? zzn + zzegg.zzb(5, this.zzixm) : zzn;
    }
}
