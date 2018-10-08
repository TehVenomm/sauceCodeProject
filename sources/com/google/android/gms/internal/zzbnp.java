package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;

public final class zzbnp extends zzegi<zzbnp> {
    public int versionCode;
    public long zzgjz;
    public String zzgkb;
    public long zzgkc;
    public int zzgkd;

    public zzbnp() {
        this.versionCode = 1;
        this.zzgkb = "";
        this.zzgkc = -1;
        this.zzgjz = -1;
        this.zzgkd = -1;
        this.zzncu = null;
        this.zzndd = -1;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzbnp)) {
            return false;
        }
        zzbnp zzbnp = (zzbnp) obj;
        if (this.versionCode != zzbnp.versionCode) {
            return false;
        }
        if (this.zzgkb == null) {
            if (zzbnp.zzgkb != null) {
                return false;
            }
        } else if (!this.zzgkb.equals(zzbnp.zzgkb)) {
            return false;
        }
        return this.zzgkc != zzbnp.zzgkc ? false : this.zzgjz != zzbnp.zzgjz ? false : this.zzgkd != zzbnp.zzgkd ? false : (this.zzncu == null || this.zzncu.isEmpty()) ? zzbnp.zzncu == null || zzbnp.zzncu.isEmpty() : this.zzncu.equals(zzbnp.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int i2 = this.versionCode;
        int hashCode2 = this.zzgkb == null ? 0 : this.zzgkb.hashCode();
        int i3 = (int) (this.zzgkc ^ (this.zzgkc >>> 32));
        int i4 = (int) (this.zzgjz ^ (this.zzgjz >>> 32));
        int i5 = this.zzgkd;
        if (!(this.zzncu == null || this.zzncu.isEmpty())) {
            i = this.zzncu.hashCode();
        }
        return ((((((((hashCode2 + ((((hashCode + 527) * 31) + i2) * 31)) * 31) + i3) * 31) + i4) * 31) + i5) * 31) + i;
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
                case 18:
                    this.zzgkb = zzegf.readString();
                    continue;
                case MotionEventCompat.AXIS_DISTANCE /*24*/:
                    zzcdu = zzegf.zzcdu();
                    this.zzgkc = (zzcdu >>> 1) ^ (-(zzcdu & 1));
                    continue;
                case 32:
                    zzcdu = zzegf.zzcdu();
                    this.zzgjz = (zzcdu >>> 1) ^ (-(zzcdu & 1));
                    continue;
                case 40:
                    this.zzgkd = zzegf.zzcbz();
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
        zzegg.zzl(2, this.zzgkb);
        zzegg.zzd(3, this.zzgkc);
        zzegg.zzd(4, this.zzgjz);
        if (this.zzgkd != -1) {
            zzegg.zzu(5, this.zzgkd);
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int zzn = (((super.zzn() + zzegg.zzv(1, this.versionCode)) + zzegg.zzm(2, this.zzgkb)) + zzegg.zzf(3, this.zzgkc)) + zzegg.zzf(4, this.zzgjz);
        return this.zzgkd != -1 ? zzn + zzegg.zzv(5, this.zzgkd) : zzn;
    }
}
