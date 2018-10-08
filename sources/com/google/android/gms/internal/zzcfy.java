package com.google.android.gms.internal;

import java.io.IOException;

public final class zzcfy extends zzegi<zzcfy> {
    private static volatile zzcfy[] zziyn;
    public Integer zzixe;
    public zzcgd zziyo;
    public zzcgd zziyp;
    public Boolean zziyq;

    public zzcfy() {
        this.zzixe = null;
        this.zziyo = null;
        this.zziyp = null;
        this.zziyq = null;
        this.zzncu = null;
        this.zzndd = -1;
    }

    public static zzcfy[] zzbae() {
        if (zziyn == null) {
            synchronized (zzegm.zzndc) {
                if (zziyn == null) {
                    zziyn = new zzcfy[0];
                }
            }
        }
        return zziyn;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzcfy)) {
            return false;
        }
        zzcfy zzcfy = (zzcfy) obj;
        if (this.zzixe == null) {
            if (zzcfy.zzixe != null) {
                return false;
            }
        } else if (!this.zzixe.equals(zzcfy.zzixe)) {
            return false;
        }
        if (this.zziyo == null) {
            if (zzcfy.zziyo != null) {
                return false;
            }
        } else if (!this.zziyo.equals(zzcfy.zziyo)) {
            return false;
        }
        if (this.zziyp == null) {
            if (zzcfy.zziyp != null) {
                return false;
            }
        } else if (!this.zziyp.equals(zzcfy.zziyp)) {
            return false;
        }
        if (this.zziyq == null) {
            if (zzcfy.zziyq != null) {
                return false;
            }
        } else if (!this.zziyq.equals(zzcfy.zziyq)) {
            return false;
        }
        return (this.zzncu == null || this.zzncu.isEmpty()) ? zzcfy.zzncu == null || zzcfy.zzncu.isEmpty() : this.zzncu.equals(zzcfy.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = this.zzixe == null ? 0 : this.zzixe.hashCode();
        int hashCode3 = this.zziyo == null ? 0 : this.zziyo.hashCode();
        int hashCode4 = this.zziyp == null ? 0 : this.zziyp.hashCode();
        int hashCode5 = this.zziyq == null ? 0 : this.zziyq.hashCode();
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
                case 8:
                    this.zzixe = Integer.valueOf(zzegf.zzcbz());
                    continue;
                case 18:
                    if (this.zziyo == null) {
                        this.zziyo = new zzcgd();
                    }
                    zzegf.zza(this.zziyo);
                    continue;
                case 26:
                    if (this.zziyp == null) {
                        this.zziyp = new zzcgd();
                    }
                    zzegf.zza(this.zziyp);
                    continue;
                case 32:
                    this.zziyq = Boolean.valueOf(zzegf.zzcds());
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
        if (this.zzixe != null) {
            zzegg.zzu(1, this.zzixe.intValue());
        }
        if (this.zziyo != null) {
            zzegg.zza(2, this.zziyo);
        }
        if (this.zziyp != null) {
            zzegg.zza(3, this.zziyp);
        }
        if (this.zziyq != null) {
            zzegg.zzl(4, this.zziyq.booleanValue());
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int zzn = super.zzn();
        if (this.zzixe != null) {
            zzn += zzegg.zzv(1, this.zzixe.intValue());
        }
        if (this.zziyo != null) {
            zzn += zzegg.zzb(2, this.zziyo);
        }
        if (this.zziyp != null) {
            zzn += zzegg.zzb(3, this.zziyp);
        }
        if (this.zziyq == null) {
            return zzn;
        }
        this.zziyq.booleanValue();
        return zzn + (zzegg.zzgr(4) + 1);
    }
}
