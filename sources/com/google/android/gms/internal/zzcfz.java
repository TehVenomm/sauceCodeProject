package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

public final class zzcfz extends zzegi<zzcfz> {
    private static volatile zzcfz[] zziyr;
    public Integer count;
    public String name;
    public zzcga[] zziys;
    public Long zziyt;
    public Long zziyu;

    public zzcfz() {
        this.zziys = zzcga.zzbag();
        this.name = null;
        this.zziyt = null;
        this.zziyu = null;
        this.count = null;
        this.zzncu = null;
        this.zzndd = -1;
    }

    public static zzcfz[] zzbaf() {
        if (zziyr == null) {
            synchronized (zzegm.zzndc) {
                if (zziyr == null) {
                    zziyr = new zzcfz[0];
                }
            }
        }
        return zziyr;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzcfz)) {
            return false;
        }
        zzcfz zzcfz = (zzcfz) obj;
        if (!zzegm.equals(this.zziys, zzcfz.zziys)) {
            return false;
        }
        if (this.name == null) {
            if (zzcfz.name != null) {
                return false;
            }
        } else if (!this.name.equals(zzcfz.name)) {
            return false;
        }
        if (this.zziyt == null) {
            if (zzcfz.zziyt != null) {
                return false;
            }
        } else if (!this.zziyt.equals(zzcfz.zziyt)) {
            return false;
        }
        if (this.zziyu == null) {
            if (zzcfz.zziyu != null) {
                return false;
            }
        } else if (!this.zziyu.equals(zzcfz.zziyu)) {
            return false;
        }
        if (this.count == null) {
            if (zzcfz.count != null) {
                return false;
            }
        } else if (!this.count.equals(zzcfz.count)) {
            return false;
        }
        return (this.zzncu == null || this.zzncu.isEmpty()) ? zzcfz.zzncu == null || zzcfz.zzncu.isEmpty() : this.zzncu.equals(zzcfz.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = zzegm.hashCode(this.zziys);
        int hashCode3 = this.name == null ? 0 : this.name.hashCode();
        int hashCode4 = this.zziyt == null ? 0 : this.zziyt.hashCode();
        int hashCode5 = this.zziyu == null ? 0 : this.zziyu.hashCode();
        int hashCode6 = this.count == null ? 0 : this.count.hashCode();
        if (!(this.zzncu == null || this.zzncu.isEmpty())) {
            i = this.zzncu.hashCode();
        }
        return ((((((((hashCode3 + ((((hashCode + 527) * 31) + hashCode2) * 31)) * 31) + hashCode4) * 31) + hashCode5) * 31) + hashCode6) * 31) + i;
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            switch (zzcbr) {
                case 0:
                    break;
                case 10:
                    int zzb = zzegr.zzb(zzegf, 10);
                    zzcbr = this.zziys == null ? 0 : this.zziys.length;
                    Object obj = new zzcga[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zziys, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = new zzcga();
                        zzegf.zza(obj[zzcbr]);
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = new zzcga();
                    zzegf.zza(obj[zzcbr]);
                    this.zziys = obj;
                    continue;
                case 18:
                    this.name = zzegf.readString();
                    continue;
                case MotionEventCompat.AXIS_DISTANCE /*24*/:
                    this.zziyt = Long.valueOf(zzegf.zzcdu());
                    continue;
                case 32:
                    this.zziyu = Long.valueOf(zzegf.zzcdu());
                    continue;
                case 40:
                    this.count = Integer.valueOf(zzegf.zzcbz());
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
        if (this.zziys != null && this.zziys.length > 0) {
            for (zzego zzego : this.zziys) {
                if (zzego != null) {
                    zzegg.zza(1, zzego);
                }
            }
        }
        if (this.name != null) {
            zzegg.zzl(2, this.name);
        }
        if (this.zziyt != null) {
            zzegg.zzb(3, this.zziyt.longValue());
        }
        if (this.zziyu != null) {
            zzegg.zzb(4, this.zziyu.longValue());
        }
        if (this.count != null) {
            zzegg.zzu(5, this.count.intValue());
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int zzn = super.zzn();
        if (this.zziys != null && this.zziys.length > 0) {
            for (zzego zzego : this.zziys) {
                if (zzego != null) {
                    zzn += zzegg.zzb(1, zzego);
                }
            }
        }
        if (this.name != null) {
            zzn += zzegg.zzm(2, this.name);
        }
        if (this.zziyt != null) {
            zzn += zzegg.zze(3, this.zziyt.longValue());
        }
        if (this.zziyu != null) {
            zzn += zzegg.zze(4, this.zziyu.longValue());
        }
        return this.count != null ? zzn + zzegg.zzv(5, this.count.intValue()) : zzn;
    }
}
