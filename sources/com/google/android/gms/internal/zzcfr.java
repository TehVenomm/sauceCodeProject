package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

public final class zzcfr extends zzegi<zzcfr> {
    private static volatile zzcfr[] zzixn;
    public zzcfu zzixo;
    public zzcfs zzixp;
    public Boolean zzixq;
    public String zzixr;

    public zzcfr() {
        this.zzixo = null;
        this.zzixp = null;
        this.zzixq = null;
        this.zzixr = null;
        this.zzncu = null;
        this.zzndd = -1;
    }

    public static zzcfr[] zzbaa() {
        if (zzixn == null) {
            synchronized (zzegm.zzndc) {
                if (zzixn == null) {
                    zzixn = new zzcfr[0];
                }
            }
        }
        return zzixn;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzcfr)) {
            return false;
        }
        zzcfr zzcfr = (zzcfr) obj;
        if (this.zzixo == null) {
            if (zzcfr.zzixo != null) {
                return false;
            }
        } else if (!this.zzixo.equals(zzcfr.zzixo)) {
            return false;
        }
        if (this.zzixp == null) {
            if (zzcfr.zzixp != null) {
                return false;
            }
        } else if (!this.zzixp.equals(zzcfr.zzixp)) {
            return false;
        }
        if (this.zzixq == null) {
            if (zzcfr.zzixq != null) {
                return false;
            }
        } else if (!this.zzixq.equals(zzcfr.zzixq)) {
            return false;
        }
        if (this.zzixr == null) {
            if (zzcfr.zzixr != null) {
                return false;
            }
        } else if (!this.zzixr.equals(zzcfr.zzixr)) {
            return false;
        }
        return (this.zzncu == null || this.zzncu.isEmpty()) ? zzcfr.zzncu == null || zzcfr.zzncu.isEmpty() : this.zzncu.equals(zzcfr.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = this.zzixo == null ? 0 : this.zzixo.hashCode();
        int hashCode3 = this.zzixp == null ? 0 : this.zzixp.hashCode();
        int hashCode4 = this.zzixq == null ? 0 : this.zzixq.hashCode();
        int hashCode5 = this.zzixr == null ? 0 : this.zzixr.hashCode();
        if (!(this.zzncu == null || this.zzncu.isEmpty())) {
            i = this.zzncu.hashCode();
        }
        return ((((((((hashCode2 + ((hashCode + 527) * 31)) * 31) + hashCode3) * 31) + hashCode4) * 31) + hashCode5) * 31) + i;
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            switch (zzcbr) {
                case 0:
                    break;
                case 10:
                    if (this.zzixo == null) {
                        this.zzixo = new zzcfu();
                    }
                    zzegf.zza(this.zzixo);
                    continue;
                case 18:
                    if (this.zzixp == null) {
                        this.zzixp = new zzcfs();
                    }
                    zzegf.zza(this.zzixp);
                    continue;
                case MotionEventCompat.AXIS_DISTANCE /*24*/:
                    this.zzixq = Boolean.valueOf(zzegf.zzcds());
                    continue;
                case MotionEventCompat.AXIS_GENERIC_3 /*34*/:
                    this.zzixr = zzegf.readString();
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
        if (this.zzixo != null) {
            zzegg.zza(1, this.zzixo);
        }
        if (this.zzixp != null) {
            zzegg.zza(2, this.zzixp);
        }
        if (this.zzixq != null) {
            zzegg.zzl(3, this.zzixq.booleanValue());
        }
        if (this.zzixr != null) {
            zzegg.zzl(4, this.zzixr);
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int zzn = super.zzn();
        if (this.zzixo != null) {
            zzn += zzegg.zzb(1, this.zzixo);
        }
        if (this.zzixp != null) {
            zzn += zzegg.zzb(2, this.zzixp);
        }
        if (this.zzixq != null) {
            this.zzixq.booleanValue();
            zzn += zzegg.zzgr(3) + 1;
        }
        return this.zzixr != null ? zzn + zzegg.zzm(4, this.zzixr) : zzn;
    }
}
