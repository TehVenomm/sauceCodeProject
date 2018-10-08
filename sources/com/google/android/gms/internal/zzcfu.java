package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

public final class zzcfu extends zzegi<zzcfu> {
    public Integer zziya;
    public String zziyb;
    public Boolean zziyc;
    public String[] zziyd;

    public zzcfu() {
        this.zziya = null;
        this.zziyb = null;
        this.zziyc = null;
        this.zziyd = zzegr.EMPTY_STRING_ARRAY;
        this.zzncu = null;
        this.zzndd = -1;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzcfu)) {
            return false;
        }
        zzcfu zzcfu = (zzcfu) obj;
        if (this.zziya == null) {
            if (zzcfu.zziya != null) {
                return false;
            }
        } else if (!this.zziya.equals(zzcfu.zziya)) {
            return false;
        }
        if (this.zziyb == null) {
            if (zzcfu.zziyb != null) {
                return false;
            }
        } else if (!this.zziyb.equals(zzcfu.zziyb)) {
            return false;
        }
        if (this.zziyc == null) {
            if (zzcfu.zziyc != null) {
                return false;
            }
        } else if (!this.zziyc.equals(zzcfu.zziyc)) {
            return false;
        }
        return !zzegm.equals(this.zziyd, zzcfu.zziyd) ? false : (this.zzncu == null || this.zzncu.isEmpty()) ? zzcfu.zzncu == null || zzcfu.zzncu.isEmpty() : this.zzncu.equals(zzcfu.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int intValue = this.zziya == null ? 0 : this.zziya.intValue();
        int hashCode2 = this.zziyb == null ? 0 : this.zziyb.hashCode();
        int hashCode3 = this.zziyc == null ? 0 : this.zziyc.hashCode();
        int hashCode4 = zzegm.hashCode(this.zziyd);
        if (!(this.zzncu == null || this.zzncu.isEmpty())) {
            i = this.zzncu.hashCode();
        }
        return ((((((((intValue + ((hashCode + 527) * 31)) * 31) + hashCode2) * 31) + hashCode3) * 31) + hashCode4) * 31) + i;
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            int position;
            switch (zzcbr) {
                case 0:
                    break;
                case 8:
                    position = zzegf.getPosition();
                    int zzcbz = zzegf.zzcbz();
                    switch (zzcbz) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            this.zziya = Integer.valueOf(zzcbz);
                            break;
                        default:
                            zzegf.zzha(position);
                            zza(zzegf, zzcbr);
                            continue;
                    }
                case 18:
                    this.zziyb = zzegf.readString();
                    continue;
                case MotionEventCompat.AXIS_DISTANCE /*24*/:
                    this.zziyc = Boolean.valueOf(zzegf.zzcds());
                    continue;
                case MotionEventCompat.AXIS_GENERIC_3 /*34*/:
                    position = zzegr.zzb(zzegf, 34);
                    zzcbr = this.zziyd == null ? 0 : this.zziyd.length;
                    Object obj = new String[(position + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zziyd, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = zzegf.readString();
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = zzegf.readString();
                    this.zziyd = obj;
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
        if (this.zziya != null) {
            zzegg.zzu(1, this.zziya.intValue());
        }
        if (this.zziyb != null) {
            zzegg.zzl(2, this.zziyb);
        }
        if (this.zziyc != null) {
            zzegg.zzl(3, this.zziyc.booleanValue());
        }
        if (this.zziyd != null && this.zziyd.length > 0) {
            for (String str : this.zziyd) {
                if (str != null) {
                    zzegg.zzl(4, str);
                }
            }
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int i = 0;
        int zzn = super.zzn();
        if (this.zziya != null) {
            zzn += zzegg.zzv(1, this.zziya.intValue());
        }
        if (this.zziyb != null) {
            zzn += zzegg.zzm(2, this.zziyb);
        }
        if (this.zziyc != null) {
            this.zziyc.booleanValue();
            zzn += zzegg.zzgr(3) + 1;
        }
        if (this.zziyd == null || this.zziyd.length <= 0) {
            return zzn;
        }
        int i2 = 0;
        for (String str : this.zziyd) {
            if (str != null) {
                i2++;
                i += zzegg.zzrc(str);
            }
        }
        return (zzn + i) + (i2 * 1);
    }
}
