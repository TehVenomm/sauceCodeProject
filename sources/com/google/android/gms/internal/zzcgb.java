package com.google.android.gms.internal;

import java.io.IOException;

public final class zzcgb extends zzegi<zzcgb> {
    public zzcgc[] zziyx;

    public zzcgb() {
        this.zziyx = zzcgc.zzbah();
        this.zzncu = null;
        this.zzndd = -1;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzcgb)) {
            return false;
        }
        zzcgb zzcgb = (zzcgb) obj;
        return !zzegm.equals(this.zziyx, zzcgb.zziyx) ? false : (this.zzncu == null || this.zzncu.isEmpty()) ? zzcgb.zzncu == null || zzcgb.zzncu.isEmpty() : this.zzncu.equals(zzcgb.zzncu);
    }

    public final int hashCode() {
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = zzegm.hashCode(this.zziyx);
        int hashCode3 = (this.zzncu == null || this.zzncu.isEmpty()) ? 0 : this.zzncu.hashCode();
        return hashCode3 + ((((hashCode + 527) * 31) + hashCode2) * 31);
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            switch (zzcbr) {
                case 0:
                    break;
                case 10:
                    int zzb = zzegr.zzb(zzegf, 10);
                    zzcbr = this.zziyx == null ? 0 : this.zziyx.length;
                    Object obj = new zzcgc[(zzb + zzcbr)];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zziyx, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = new zzcgc();
                        zzegf.zza(obj[zzcbr]);
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = new zzcgc();
                    zzegf.zza(obj[zzcbr]);
                    this.zziyx = obj;
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
        if (this.zziyx != null && this.zziyx.length > 0) {
            for (zzego zzego : this.zziyx) {
                if (zzego != null) {
                    zzegg.zza(1, zzego);
                }
            }
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int zzn = super.zzn();
        if (this.zziyx != null && this.zziyx.length > 0) {
            for (zzego zzego : this.zziyx) {
                if (zzego != null) {
                    zzn += zzegg.zzb(1, zzego);
                }
            }
        }
        return zzn;
    }
}
