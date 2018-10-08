package com.google.android.gms.internal;

import java.io.IOException;

public final class zzcft extends zzegi<zzcft> {
    private static volatile zzcft[] zzixx;
    public Integer zzixi;
    public String zzixy;
    public zzcfr zzixz;

    public zzcft() {
        this.zzixi = null;
        this.zzixy = null;
        this.zzixz = null;
        this.zzncu = null;
        this.zzndd = -1;
    }

    public static zzcft[] zzbab() {
        if (zzixx == null) {
            synchronized (zzegm.zzndc) {
                if (zzixx == null) {
                    zzixx = new zzcft[0];
                }
            }
        }
        return zzixx;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzcft)) {
            return false;
        }
        zzcft zzcft = (zzcft) obj;
        if (this.zzixi == null) {
            if (zzcft.zzixi != null) {
                return false;
            }
        } else if (!this.zzixi.equals(zzcft.zzixi)) {
            return false;
        }
        if (this.zzixy == null) {
            if (zzcft.zzixy != null) {
                return false;
            }
        } else if (!this.zzixy.equals(zzcft.zzixy)) {
            return false;
        }
        if (this.zzixz == null) {
            if (zzcft.zzixz != null) {
                return false;
            }
        } else if (!this.zzixz.equals(zzcft.zzixz)) {
            return false;
        }
        return (this.zzncu == null || this.zzncu.isEmpty()) ? zzcft.zzncu == null || zzcft.zzncu.isEmpty() : this.zzncu.equals(zzcft.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = this.zzixi == null ? 0 : this.zzixi.hashCode();
        int hashCode3 = this.zzixy == null ? 0 : this.zzixy.hashCode();
        int hashCode4 = this.zzixz == null ? 0 : this.zzixz.hashCode();
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
                case 8:
                    this.zzixi = Integer.valueOf(zzegf.zzcbz());
                    continue;
                case 18:
                    this.zzixy = zzegf.readString();
                    continue;
                case 26:
                    if (this.zzixz == null) {
                        this.zzixz = new zzcfr();
                    }
                    zzegf.zza(this.zzixz);
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
        if (this.zzixy != null) {
            zzegg.zzl(2, this.zzixy);
        }
        if (this.zzixz != null) {
            zzegg.zza(3, this.zzixz);
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int zzn = super.zzn();
        if (this.zzixi != null) {
            zzn += zzegg.zzv(1, this.zzixi.intValue());
        }
        if (this.zzixy != null) {
            zzn += zzegg.zzm(2, this.zzixy);
        }
        return this.zzixz != null ? zzn + zzegg.zzb(3, this.zzixz) : zzn;
    }
}
