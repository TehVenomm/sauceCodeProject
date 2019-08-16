package com.google.android.gms.internal.measurement;

import java.io.IOException;

public final class zzbx extends zziq<zzbx> {
    private static volatile zzbx[] zzzr;
    public String name;
    public Boolean zzzs;
    public Boolean zzzt;
    public Integer zzzu;

    public zzbx() {
        this.name = null;
        this.zzzs = null;
        this.zzzt = null;
        this.zzzu = null;
        this.zzaoo = null;
        this.zzaow = -1;
    }

    public static zzbx[] zzrc() {
        if (zzzr == null) {
            synchronized (zziu.zzaov) {
                if (zzzr == null) {
                    zzzr = new zzbx[0];
                }
            }
        }
        return zzzr;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzbx)) {
            return false;
        }
        zzbx zzbx = (zzbx) obj;
        if (this.name == null) {
            if (zzbx.name != null) {
                return false;
            }
        } else if (!this.name.equals(zzbx.name)) {
            return false;
        }
        if (this.zzzs == null) {
            if (zzbx.zzzs != null) {
                return false;
            }
        } else if (!this.zzzs.equals(zzbx.zzzs)) {
            return false;
        }
        if (this.zzzt == null) {
            if (zzbx.zzzt != null) {
                return false;
            }
        } else if (!this.zzzt.equals(zzbx.zzzt)) {
            return false;
        }
        if (this.zzzu == null) {
            if (zzbx.zzzu != null) {
                return false;
            }
        } else if (!this.zzzu.equals(zzbx.zzzu)) {
            return false;
        }
        return (this.zzaoo == null || this.zzaoo.isEmpty()) ? zzbx.zzaoo == null || zzbx.zzaoo.isEmpty() : this.zzaoo.equals(zzbx.zzaoo);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = this.name == null ? 0 : this.name.hashCode();
        int hashCode3 = this.zzzs == null ? 0 : this.zzzs.hashCode();
        int hashCode4 = this.zzzt == null ? 0 : this.zzzt.hashCode();
        int hashCode5 = this.zzzu == null ? 0 : this.zzzu.hashCode();
        if (this.zzaoo != null && !this.zzaoo.isEmpty()) {
            i = this.zzaoo.hashCode();
        }
        return ((((((((hashCode2 + ((hashCode + 527) * 31)) * 31) + hashCode3) * 31) + hashCode4) * 31) + hashCode5) * 31) + i;
    }

    public final /* synthetic */ zziw zza(zzil zzil) throws IOException {
        while (true) {
            int zzsg = zzil.zzsg();
            switch (zzsg) {
                case 0:
                    break;
                case 10:
                    this.name = zzil.readString();
                    continue;
                case 16:
                    this.zzzs = Boolean.valueOf(zzil.zzsm());
                    continue;
                case 24:
                    this.zzzt = Boolean.valueOf(zzil.zzsm());
                    continue;
                case 32:
                    this.zzzu = Integer.valueOf(zzil.zzta());
                    continue;
                default:
                    if (!super.zza(zzil, zzsg)) {
                        break;
                    } else {
                        continue;
                    }
            }
        }
        return this;
    }

    public final void zza(zzio zzio) throws IOException {
        if (this.name != null) {
            zzio.zzb(1, this.name);
        }
        if (this.zzzs != null) {
            zzio.zzb(2, this.zzzs.booleanValue());
        }
        if (this.zzzt != null) {
            zzio.zzb(3, this.zzzt.booleanValue());
        }
        if (this.zzzu != null) {
            zzio.zzc(4, this.zzzu.intValue());
        }
        super.zza(zzio);
    }

    /* access modifiers changed from: protected */
    public final int zzqy() {
        int zzqy = super.zzqy();
        if (this.name != null) {
            zzqy += zzio.zzc(1, this.name);
        }
        if (this.zzzs != null) {
            this.zzzs.booleanValue();
            zzqy += zzio.zzbi(2) + 1;
        }
        if (this.zzzt != null) {
            this.zzzt.booleanValue();
            zzqy += zzio.zzbi(3) + 1;
        }
        return this.zzzu != null ? zzqy + zzio.zzg(4, this.zzzu.intValue()) : zzqy;
    }
}
