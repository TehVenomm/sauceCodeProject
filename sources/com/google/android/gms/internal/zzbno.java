package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

public final class zzbno extends zzegi<zzbno> {
    public long sequenceNumber;
    public int versionCode;
    public long zzgjz;
    public long zzgka;

    public zzbno() {
        this.versionCode = 1;
        this.sequenceNumber = -1;
        this.zzgjz = -1;
        this.zzgka = -1;
        this.zzncu = null;
        this.zzndd = -1;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzbno)) {
            return false;
        }
        zzbno zzbno = (zzbno) obj;
        return this.versionCode != zzbno.versionCode ? false : this.sequenceNumber != zzbno.sequenceNumber ? false : this.zzgjz != zzbno.zzgjz ? false : this.zzgka != zzbno.zzgka ? false : (this.zzncu == null || this.zzncu.isEmpty()) ? zzbno.zzncu == null || zzbno.zzncu.isEmpty() : this.zzncu.equals(zzbno.zzncu);
    }

    public final int hashCode() {
        int hashCode = getClass().getName().hashCode();
        int i = this.versionCode;
        int i2 = (int) (this.sequenceNumber ^ (this.sequenceNumber >>> 32));
        int i3 = (int) (this.zzgjz ^ (this.zzgjz >>> 32));
        int i4 = (int) (this.zzgka ^ (this.zzgka >>> 32));
        int hashCode2 = (this.zzncu == null || this.zzncu.isEmpty()) ? 0 : this.zzncu.hashCode();
        return hashCode2 + ((((((((((hashCode + 527) * 31) + i) * 31) + i2) * 31) + i3) * 31) + i4) * 31);
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            long zzcdu;
            switch (zzcbr) {
                case 0:
                    break;
                case 8:
                    this.versionCode = zzegf.zzcbz();
                    continue;
                case 16:
                    zzcdu = zzegf.zzcdu();
                    this.sequenceNumber = (zzcdu >>> 1) ^ (-(zzcdu & 1));
                    continue;
                case MotionEventCompat.AXIS_DISTANCE /*24*/:
                    zzcdu = zzegf.zzcdu();
                    this.zzgjz = (zzcdu >>> 1) ^ (-(zzcdu & 1));
                    continue;
                case 32:
                    zzcdu = zzegf.zzcdu();
                    this.zzgka = (zzcdu >>> 1) ^ (-(zzcdu & 1));
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
        zzegg.zzu(1, this.versionCode);
        zzegg.zzd(2, this.sequenceNumber);
        zzegg.zzd(3, this.zzgjz);
        zzegg.zzd(4, this.zzgka);
        super.zza(zzegg);
    }

    protected final int zzn() {
        return (((super.zzn() + zzegg.zzv(1, this.versionCode)) + zzegg.zzf(2, this.sequenceNumber)) + zzegg.zzf(3, this.zzgjz)) + zzegg.zzf(4, this.zzgka);
    }
}
