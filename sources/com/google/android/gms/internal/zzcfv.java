package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

public final class zzcfv extends zzegi<zzcfv> {
    private static volatile zzcfv[] zziye;
    public String name;
    public Boolean zziyf;
    public Boolean zziyg;

    public zzcfv() {
        this.name = null;
        this.zziyf = null;
        this.zziyg = null;
        this.zzncu = null;
        this.zzndd = -1;
    }

    public static zzcfv[] zzbac() {
        if (zziye == null) {
            synchronized (zzegm.zzndc) {
                if (zziye == null) {
                    zziye = new zzcfv[0];
                }
            }
        }
        return zziye;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzcfv)) {
            return false;
        }
        zzcfv zzcfv = (zzcfv) obj;
        if (this.name == null) {
            if (zzcfv.name != null) {
                return false;
            }
        } else if (!this.name.equals(zzcfv.name)) {
            return false;
        }
        if (this.zziyf == null) {
            if (zzcfv.zziyf != null) {
                return false;
            }
        } else if (!this.zziyf.equals(zzcfv.zziyf)) {
            return false;
        }
        if (this.zziyg == null) {
            if (zzcfv.zziyg != null) {
                return false;
            }
        } else if (!this.zziyg.equals(zzcfv.zziyg)) {
            return false;
        }
        return (this.zzncu == null || this.zzncu.isEmpty()) ? zzcfv.zzncu == null || zzcfv.zzncu.isEmpty() : this.zzncu.equals(zzcfv.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = this.name == null ? 0 : this.name.hashCode();
        int hashCode3 = this.zziyf == null ? 0 : this.zziyf.hashCode();
        int hashCode4 = this.zziyg == null ? 0 : this.zziyg.hashCode();
        if (!(this.zzncu == null || this.zzncu.isEmpty())) {
            i = this.zzncu.hashCode();
        }
        return ((((((hashCode2 + ((hashCode + 527) * 31)) * 31) + hashCode3) * 31) + hashCode4) * 31) + i;
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            switch (zzcbr) {
                case 0:
                    break;
                case 10:
                    this.name = zzegf.readString();
                    continue;
                case 16:
                    this.zziyf = Boolean.valueOf(zzegf.zzcds());
                    continue;
                case MotionEventCompat.AXIS_DISTANCE /*24*/:
                    this.zziyg = Boolean.valueOf(zzegf.zzcds());
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
        if (this.name != null) {
            zzegg.zzl(1, this.name);
        }
        if (this.zziyf != null) {
            zzegg.zzl(2, this.zziyf.booleanValue());
        }
        if (this.zziyg != null) {
            zzegg.zzl(3, this.zziyg.booleanValue());
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int zzn = super.zzn();
        if (this.name != null) {
            zzn += zzegg.zzm(1, this.name);
        }
        if (this.zziyf != null) {
            this.zziyf.booleanValue();
            zzn += zzegg.zzgr(2) + 1;
        }
        if (this.zziyg == null) {
            return zzn;
        }
        this.zziyg.booleanValue();
        return zzn + (zzegg.zzgr(3) + 1);
    }
}
