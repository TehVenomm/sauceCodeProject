package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

public final class zzcga extends zzegi<zzcga> {
    private static volatile zzcga[] zziyv;
    public String name;
    public String zzfwi;
    private Float zziww;
    public Double zziwx;
    public Long zziyw;

    public zzcga() {
        this.name = null;
        this.zzfwi = null;
        this.zziyw = null;
        this.zziww = null;
        this.zziwx = null;
        this.zzncu = null;
        this.zzndd = -1;
    }

    public static zzcga[] zzbag() {
        if (zziyv == null) {
            synchronized (zzegm.zzndc) {
                if (zziyv == null) {
                    zziyv = new zzcga[0];
                }
            }
        }
        return zziyv;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzcga)) {
            return false;
        }
        zzcga zzcga = (zzcga) obj;
        if (this.name == null) {
            if (zzcga.name != null) {
                return false;
            }
        } else if (!this.name.equals(zzcga.name)) {
            return false;
        }
        if (this.zzfwi == null) {
            if (zzcga.zzfwi != null) {
                return false;
            }
        } else if (!this.zzfwi.equals(zzcga.zzfwi)) {
            return false;
        }
        if (this.zziyw == null) {
            if (zzcga.zziyw != null) {
                return false;
            }
        } else if (!this.zziyw.equals(zzcga.zziyw)) {
            return false;
        }
        if (this.zziww == null) {
            if (zzcga.zziww != null) {
                return false;
            }
        } else if (!this.zziww.equals(zzcga.zziww)) {
            return false;
        }
        if (this.zziwx == null) {
            if (zzcga.zziwx != null) {
                return false;
            }
        } else if (!this.zziwx.equals(zzcga.zziwx)) {
            return false;
        }
        return (this.zzncu == null || this.zzncu.isEmpty()) ? zzcga.zzncu == null || zzcga.zzncu.isEmpty() : this.zzncu.equals(zzcga.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = this.name == null ? 0 : this.name.hashCode();
        int hashCode3 = this.zzfwi == null ? 0 : this.zzfwi.hashCode();
        int hashCode4 = this.zziyw == null ? 0 : this.zziyw.hashCode();
        int hashCode5 = this.zziww == null ? 0 : this.zziww.hashCode();
        int hashCode6 = this.zziwx == null ? 0 : this.zziwx.hashCode();
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
                case 10:
                    this.name = zzegf.readString();
                    continue;
                case 18:
                    this.zzfwi = zzegf.readString();
                    continue;
                case MotionEventCompat.AXIS_DISTANCE /*24*/:
                    this.zziyw = Long.valueOf(zzegf.zzcdu());
                    continue;
                case MotionEventCompat.AXIS_GENERIC_6 /*37*/:
                    this.zziww = Float.valueOf(Float.intBitsToFloat(zzegf.zzcdv()));
                    continue;
                case MotionEventCompat.AXIS_GENERIC_10 /*41*/:
                    this.zziwx = Double.valueOf(Double.longBitsToDouble(zzegf.zzcdw()));
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
        if (this.zzfwi != null) {
            zzegg.zzl(2, this.zzfwi);
        }
        if (this.zziyw != null) {
            zzegg.zzb(3, this.zziyw.longValue());
        }
        if (this.zziww != null) {
            zzegg.zzc(4, this.zziww.floatValue());
        }
        if (this.zziwx != null) {
            zzegg.zza(5, this.zziwx.doubleValue());
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int zzn = super.zzn();
        if (this.name != null) {
            zzn += zzegg.zzm(1, this.name);
        }
        if (this.zzfwi != null) {
            zzn += zzegg.zzm(2, this.zzfwi);
        }
        if (this.zziyw != null) {
            zzn += zzegg.zze(3, this.zziyw.longValue());
        }
        if (this.zziww != null) {
            this.zziww.floatValue();
            zzn += zzegg.zzgr(4) + 4;
        }
        if (this.zziwx == null) {
            return zzn;
        }
        this.zziwx.doubleValue();
        return zzn + (zzegg.zzgr(5) + 8);
    }
}
