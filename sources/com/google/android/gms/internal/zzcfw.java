package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import com.facebook.GraphRequest;
import java.io.IOException;

public final class zzcfw extends zzegi<zzcfw> {
    public String zziln;
    public Long zziyh;
    private Integer zziyi;
    public zzcfx[] zziyj;
    public zzcfv[] zziyk;
    public zzcfp[] zziyl;

    public zzcfw() {
        this.zziyh = null;
        this.zziln = null;
        this.zziyi = null;
        this.zziyj = zzcfx.zzbad();
        this.zziyk = zzcfv.zzbac();
        this.zziyl = zzcfp.zzazy();
        this.zzncu = null;
        this.zzndd = -1;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzcfw)) {
            return false;
        }
        zzcfw zzcfw = (zzcfw) obj;
        if (this.zziyh == null) {
            if (zzcfw.zziyh != null) {
                return false;
            }
        } else if (!this.zziyh.equals(zzcfw.zziyh)) {
            return false;
        }
        if (this.zziln == null) {
            if (zzcfw.zziln != null) {
                return false;
            }
        } else if (!this.zziln.equals(zzcfw.zziln)) {
            return false;
        }
        if (this.zziyi == null) {
            if (zzcfw.zziyi != null) {
                return false;
            }
        } else if (!this.zziyi.equals(zzcfw.zziyi)) {
            return false;
        }
        return !zzegm.equals(this.zziyj, zzcfw.zziyj) ? false : !zzegm.equals(this.zziyk, zzcfw.zziyk) ? false : !zzegm.equals(this.zziyl, zzcfw.zziyl) ? false : (this.zzncu == null || this.zzncu.isEmpty()) ? zzcfw.zzncu == null || zzcfw.zzncu.isEmpty() : this.zzncu.equals(zzcfw.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = this.zziyh == null ? 0 : this.zziyh.hashCode();
        int hashCode3 = this.zziln == null ? 0 : this.zziln.hashCode();
        int hashCode4 = this.zziyi == null ? 0 : this.zziyi.hashCode();
        int hashCode5 = zzegm.hashCode(this.zziyj);
        int hashCode6 = zzegm.hashCode(this.zziyk);
        int hashCode7 = zzegm.hashCode(this.zziyl);
        if (!(this.zzncu == null || this.zzncu.isEmpty())) {
            i = this.zzncu.hashCode();
        }
        return ((((((((((((hashCode2 + ((hashCode + 527) * 31)) * 31) + hashCode3) * 31) + hashCode4) * 31) + hashCode5) * 31) + hashCode6) * 31) + hashCode7) * 31) + i;
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
                    this.zziyh = Long.valueOf(zzegf.zzcdu());
                    continue;
                case 18:
                    this.zziln = zzegf.readString();
                    continue;
                case MotionEventCompat.AXIS_DISTANCE /*24*/:
                    this.zziyi = Integer.valueOf(zzegf.zzcbz());
                    continue;
                case MotionEventCompat.AXIS_GENERIC_3 /*34*/:
                    zzb = zzegr.zzb(zzegf, 34);
                    zzcbr = this.zziyj == null ? 0 : this.zziyj.length;
                    obj = new zzcfx[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zziyj, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = new zzcfx();
                        zzegf.zza(obj[zzcbr]);
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = new zzcfx();
                    zzegf.zza(obj[zzcbr]);
                    this.zziyj = obj;
                    continue;
                case MotionEventCompat.AXIS_GENERIC_11 /*42*/:
                    zzb = zzegr.zzb(zzegf, 42);
                    zzcbr = this.zziyk == null ? 0 : this.zziyk.length;
                    obj = new zzcfv[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zziyk, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = new zzcfv();
                        zzegf.zza(obj[zzcbr]);
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = new zzcfv();
                    zzegf.zza(obj[zzcbr]);
                    this.zziyk = obj;
                    continue;
                case GraphRequest.MAXIMUM_BATCH_SIZE /*50*/:
                    zzb = zzegr.zzb(zzegf, 50);
                    zzcbr = this.zziyl == null ? 0 : this.zziyl.length;
                    obj = new zzcfp[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zziyl, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = new zzcfp();
                        zzegf.zza(obj[zzcbr]);
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = new zzcfp();
                    zzegf.zza(obj[zzcbr]);
                    this.zziyl = obj;
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
        if (this.zziyh != null) {
            zzegg.zzb(1, this.zziyh.longValue());
        }
        if (this.zziln != null) {
            zzegg.zzl(2, this.zziln);
        }
        if (this.zziyi != null) {
            zzegg.zzu(3, this.zziyi.intValue());
        }
        if (this.zziyj != null && this.zziyj.length > 0) {
            for (zzego zzego : this.zziyj) {
                if (zzego != null) {
                    zzegg.zza(4, zzego);
                }
            }
        }
        if (this.zziyk != null && this.zziyk.length > 0) {
            for (zzego zzego2 : this.zziyk) {
                if (zzego2 != null) {
                    zzegg.zza(5, zzego2);
                }
            }
        }
        if (this.zziyl != null && this.zziyl.length > 0) {
            while (i < this.zziyl.length) {
                zzego zzego3 = this.zziyl[i];
                if (zzego3 != null) {
                    zzegg.zza(6, zzego3);
                }
                i++;
            }
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int i;
        int i2 = 0;
        int zzn = super.zzn();
        if (this.zziyh != null) {
            zzn += zzegg.zze(1, this.zziyh.longValue());
        }
        if (this.zziln != null) {
            zzn += zzegg.zzm(2, this.zziln);
        }
        if (this.zziyi != null) {
            zzn += zzegg.zzv(3, this.zziyi.intValue());
        }
        if (this.zziyj != null && this.zziyj.length > 0) {
            i = zzn;
            for (zzego zzego : this.zziyj) {
                if (zzego != null) {
                    i += zzegg.zzb(4, zzego);
                }
            }
            zzn = i;
        }
        if (this.zziyk != null && this.zziyk.length > 0) {
            i = zzn;
            for (zzego zzego2 : this.zziyk) {
                if (zzego2 != null) {
                    i += zzegg.zzb(5, zzego2);
                }
            }
            zzn = i;
        }
        if (this.zziyl != null && this.zziyl.length > 0) {
            while (i2 < this.zziyl.length) {
                zzego zzego3 = this.zziyl[i2];
                if (zzego3 != null) {
                    zzn += zzegg.zzb(6, zzego3);
                }
                i2++;
            }
        }
        return zzn;
    }
}
