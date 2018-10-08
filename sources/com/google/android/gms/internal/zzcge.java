package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

public final class zzcge extends zzegi<zzcge> {
    private static volatile zzcge[] zzjac;
    public String name;
    public String zzfwi;
    private Float zziww;
    public Double zziwx;
    public Long zziyw;
    public Long zzjad;

    public zzcge() {
        this.zzjad = null;
        this.name = null;
        this.zzfwi = null;
        this.zziyw = null;
        this.zziww = null;
        this.zziwx = null;
        this.zzncu = null;
        this.zzndd = -1;
    }

    public static zzcge[] zzbai() {
        if (zzjac == null) {
            synchronized (zzegm.zzndc) {
                if (zzjac == null) {
                    zzjac = new zzcge[0];
                }
            }
        }
        return zzjac;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzcge)) {
            return false;
        }
        zzcge zzcge = (zzcge) obj;
        if (this.zzjad == null) {
            if (zzcge.zzjad != null) {
                return false;
            }
        } else if (!this.zzjad.equals(zzcge.zzjad)) {
            return false;
        }
        if (this.name == null) {
            if (zzcge.name != null) {
                return false;
            }
        } else if (!this.name.equals(zzcge.name)) {
            return false;
        }
        if (this.zzfwi == null) {
            if (zzcge.zzfwi != null) {
                return false;
            }
        } else if (!this.zzfwi.equals(zzcge.zzfwi)) {
            return false;
        }
        if (this.zziyw == null) {
            if (zzcge.zziyw != null) {
                return false;
            }
        } else if (!this.zziyw.equals(zzcge.zziyw)) {
            return false;
        }
        if (this.zziww == null) {
            if (zzcge.zziww != null) {
                return false;
            }
        } else if (!this.zziww.equals(zzcge.zziww)) {
            return false;
        }
        if (this.zziwx == null) {
            if (zzcge.zziwx != null) {
                return false;
            }
        } else if (!this.zziwx.equals(zzcge.zziwx)) {
            return false;
        }
        return (this.zzncu == null || this.zzncu.isEmpty()) ? zzcge.zzncu == null || zzcge.zzncu.isEmpty() : this.zzncu.equals(zzcge.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = this.zzjad == null ? 0 : this.zzjad.hashCode();
        int hashCode3 = this.name == null ? 0 : this.name.hashCode();
        int hashCode4 = this.zzfwi == null ? 0 : this.zzfwi.hashCode();
        int hashCode5 = this.zziyw == null ? 0 : this.zziyw.hashCode();
        int hashCode6 = this.zziww == null ? 0 : this.zziww.hashCode();
        int hashCode7 = this.zziwx == null ? 0 : this.zziwx.hashCode();
        if (!(this.zzncu == null || this.zzncu.isEmpty())) {
            i = this.zzncu.hashCode();
        }
        return ((((((((((((hashCode2 + ((hashCode + 527) * 31)) * 31) + hashCode3) * 31) + hashCode4) * 31) + hashCode5) * 31) + hashCode6) * 31) + hashCode7) * 31) + i;
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            switch (zzcbr) {
                case 0:
                    break;
                case 8:
                    this.zzjad = Long.valueOf(zzegf.zzcdu());
                    continue;
                case 18:
                    this.name = zzegf.readString();
                    continue;
                case 26:
                    this.zzfwi = zzegf.readString();
                    continue;
                case 32:
                    this.zziyw = Long.valueOf(zzegf.zzcdu());
                    continue;
                case MotionEventCompat.AXIS_GENERIC_14 /*45*/:
                    this.zziww = Float.valueOf(Float.intBitsToFloat(zzegf.zzcdv()));
                    continue;
                case 49:
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
        if (this.zzjad != null) {
            zzegg.zzb(1, this.zzjad.longValue());
        }
        if (this.name != null) {
            zzegg.zzl(2, this.name);
        }
        if (this.zzfwi != null) {
            zzegg.zzl(3, this.zzfwi);
        }
        if (this.zziyw != null) {
            zzegg.zzb(4, this.zziyw.longValue());
        }
        if (this.zziww != null) {
            zzegg.zzc(5, this.zziww.floatValue());
        }
        if (this.zziwx != null) {
            zzegg.zza(6, this.zziwx.doubleValue());
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int zzn = super.zzn();
        if (this.zzjad != null) {
            zzn += zzegg.zze(1, this.zzjad.longValue());
        }
        if (this.name != null) {
            zzn += zzegg.zzm(2, this.name);
        }
        if (this.zzfwi != null) {
            zzn += zzegg.zzm(3, this.zzfwi);
        }
        if (this.zziyw != null) {
            zzn += zzegg.zze(4, this.zziyw.longValue());
        }
        if (this.zziww != null) {
            this.zziww.floatValue();
            zzn += zzegg.zzgr(5) + 4;
        }
        if (this.zziwx == null) {
            return zzn;
        }
        this.zziwx.doubleValue();
        return zzn + (zzegg.zzgr(6) + 8);
    }
}
