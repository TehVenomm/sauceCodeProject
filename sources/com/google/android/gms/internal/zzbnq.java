package com.google.android.gms.internal;

import java.io.IOException;

public final class zzbnq extends zzegi<zzbnq> {
    public long zzgjz;
    public long zzgkc;

    public zzbnq() {
        this.zzgkc = -1;
        this.zzgjz = -1;
        this.zzncu = null;
        this.zzndd = -1;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzbnq)) {
            return false;
        }
        zzbnq zzbnq = (zzbnq) obj;
        return this.zzgkc != zzbnq.zzgkc ? false : this.zzgjz != zzbnq.zzgjz ? false : (this.zzncu == null || this.zzncu.isEmpty()) ? zzbnq.zzncu == null || zzbnq.zzncu.isEmpty() : this.zzncu.equals(zzbnq.zzncu);
    }

    public final int hashCode() {
        int hashCode = getClass().getName().hashCode();
        int i = (int) (this.zzgkc ^ (this.zzgkc >>> 32));
        int i2 = (int) (this.zzgjz ^ (this.zzgjz >>> 32));
        int hashCode2 = (this.zzncu == null || this.zzncu.isEmpty()) ? 0 : this.zzncu.hashCode();
        return hashCode2 + ((((((hashCode + 527) * 31) + i) * 31) + i2) * 31);
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            long zzcdu;
            switch (zzcbr) {
                case 0:
                    break;
                case 8:
                    zzcdu = zzegf.zzcdu();
                    this.zzgkc = (zzcdu >>> 1) ^ (-(zzcdu & 1));
                    continue;
                case 16:
                    zzcdu = zzegf.zzcdu();
                    this.zzgjz = (zzcdu >>> 1) ^ (-(zzcdu & 1));
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
        zzegg.zzd(1, this.zzgkc);
        zzegg.zzd(2, this.zzgjz);
        super.zza(zzegg);
    }

    protected final int zzn() {
        return (super.zzn() + zzegg.zzf(1, this.zzgkc)) + zzegg.zzf(2, this.zzgjz);
    }
}
