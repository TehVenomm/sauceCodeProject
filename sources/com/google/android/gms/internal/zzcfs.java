package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

public final class zzcfs extends zzegi<zzcfs> {
    public Integer zzixs;
    public Boolean zzixt;
    public String zzixu;
    public String zzixv;
    public String zzixw;

    public zzcfs() {
        this.zzixs = null;
        this.zzixt = null;
        this.zzixu = null;
        this.zzixv = null;
        this.zzixw = null;
        this.zzncu = null;
        this.zzndd = -1;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzcfs)) {
            return false;
        }
        zzcfs zzcfs = (zzcfs) obj;
        if (this.zzixs == null) {
            if (zzcfs.zzixs != null) {
                return false;
            }
        } else if (!this.zzixs.equals(zzcfs.zzixs)) {
            return false;
        }
        if (this.zzixt == null) {
            if (zzcfs.zzixt != null) {
                return false;
            }
        } else if (!this.zzixt.equals(zzcfs.zzixt)) {
            return false;
        }
        if (this.zzixu == null) {
            if (zzcfs.zzixu != null) {
                return false;
            }
        } else if (!this.zzixu.equals(zzcfs.zzixu)) {
            return false;
        }
        if (this.zzixv == null) {
            if (zzcfs.zzixv != null) {
                return false;
            }
        } else if (!this.zzixv.equals(zzcfs.zzixv)) {
            return false;
        }
        if (this.zzixw == null) {
            if (zzcfs.zzixw != null) {
                return false;
            }
        } else if (!this.zzixw.equals(zzcfs.zzixw)) {
            return false;
        }
        return (this.zzncu == null || this.zzncu.isEmpty()) ? zzcfs.zzncu == null || zzcfs.zzncu.isEmpty() : this.zzncu.equals(zzcfs.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int intValue = this.zzixs == null ? 0 : this.zzixs.intValue();
        int hashCode2 = this.zzixt == null ? 0 : this.zzixt.hashCode();
        int hashCode3 = this.zzixu == null ? 0 : this.zzixu.hashCode();
        int hashCode4 = this.zzixv == null ? 0 : this.zzixv.hashCode();
        int hashCode5 = this.zzixw == null ? 0 : this.zzixw.hashCode();
        if (!(this.zzncu == null || this.zzncu.isEmpty())) {
            i = this.zzncu.hashCode();
        }
        return ((((((((((intValue + ((hashCode + 527) * 31)) * 31) + hashCode2) * 31) + hashCode3) * 31) + hashCode4) * 31) + hashCode5) * 31) + i;
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            switch (zzcbr) {
                case 0:
                    break;
                case 8:
                    int position = zzegf.getPosition();
                    int zzcbz = zzegf.zzcbz();
                    switch (zzcbz) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            this.zzixs = Integer.valueOf(zzcbz);
                            break;
                        default:
                            zzegf.zzha(position);
                            zza(zzegf, zzcbr);
                            continue;
                    }
                case 16:
                    this.zzixt = Boolean.valueOf(zzegf.zzcds());
                    continue;
                case 26:
                    this.zzixu = zzegf.readString();
                    continue;
                case MotionEventCompat.AXIS_GENERIC_3 /*34*/:
                    this.zzixv = zzegf.readString();
                    continue;
                case MotionEventCompat.AXIS_GENERIC_11 /*42*/:
                    this.zzixw = zzegf.readString();
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
        if (this.zzixs != null) {
            zzegg.zzu(1, this.zzixs.intValue());
        }
        if (this.zzixt != null) {
            zzegg.zzl(2, this.zzixt.booleanValue());
        }
        if (this.zzixu != null) {
            zzegg.zzl(3, this.zzixu);
        }
        if (this.zzixv != null) {
            zzegg.zzl(4, this.zzixv);
        }
        if (this.zzixw != null) {
            zzegg.zzl(5, this.zzixw);
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int zzn = super.zzn();
        if (this.zzixs != null) {
            zzn += zzegg.zzv(1, this.zzixs.intValue());
        }
        if (this.zzixt != null) {
            this.zzixt.booleanValue();
            zzn += zzegg.zzgr(2) + 1;
        }
        if (this.zzixu != null) {
            zzn += zzegg.zzm(3, this.zzixu);
        }
        if (this.zzixv != null) {
            zzn += zzegg.zzm(4, this.zzixv);
        }
        return this.zzixw != null ? zzn + zzegg.zzm(5, this.zzixw) : zzn;
    }
}
