package com.google.android.gms.internal;

import java.io.IOException;

public final class zzehk extends zzegi<zzehk> implements Cloneable {
    private int zzngs;
    private int zzngt;

    public zzehk() {
        this.zzngs = -1;
        this.zzngt = 0;
        this.zzncu = null;
        this.zzndd = -1;
    }

    private zzehk zzceq() {
        try {
            return (zzehk) super.zzcdy();
        } catch (CloneNotSupportedException e) {
            throw new AssertionError(e);
        }
    }

    public final /* synthetic */ Object clone() throws CloneNotSupportedException {
        return zzceq();
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzehk)) {
            return false;
        }
        zzehk zzehk = (zzehk) obj;
        return this.zzngs != zzehk.zzngs ? false : this.zzngt != zzehk.zzngt ? false : (this.zzncu == null || this.zzncu.isEmpty()) ? zzehk.zzncu == null || zzehk.zzncu.isEmpty() : this.zzncu.equals(zzehk.zzncu);
    }

    public final int hashCode() {
        int hashCode = getClass().getName().hashCode();
        int i = this.zzngs;
        int i2 = this.zzngt;
        int hashCode2 = (this.zzncu == null || this.zzncu.isEmpty()) ? 0 : this.zzncu.hashCode();
        return hashCode2 + ((((((hashCode + 527) * 31) + i) * 31) + i2) * 31);
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            int position;
            int zzcbs;
            switch (zzcbr) {
                case 0:
                    break;
                case 8:
                    position = zzegf.getPosition();
                    zzcbs = zzegf.zzcbs();
                    switch (zzcbs) {
                        case -1:
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                            this.zzngs = zzcbs;
                            break;
                        default:
                            zzegf.zzha(position);
                            zza(zzegf, zzcbr);
                            continue;
                    }
                case 16:
                    position = zzegf.getPosition();
                    zzcbs = zzegf.zzcbs();
                    switch (zzcbs) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 100:
                            this.zzngt = zzcbs;
                            break;
                        default:
                            zzegf.zzha(position);
                            zza(zzegf, zzcbr);
                            continue;
                    }
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
        if (this.zzngs != -1) {
            zzegg.zzu(1, this.zzngs);
        }
        if (this.zzngt != 0) {
            zzegg.zzu(2, this.zzngt);
        }
        super.zza(zzegg);
    }

    public final /* synthetic */ zzegi zzcdy() throws CloneNotSupportedException {
        return (zzehk) clone();
    }

    public final /* synthetic */ zzego zzcdz() throws CloneNotSupportedException {
        return (zzehk) clone();
    }

    protected final int zzn() {
        int zzn = super.zzn();
        if (this.zzngs != -1) {
            zzn += zzegg.zzv(1, this.zzngs);
        }
        return this.zzngt != 0 ? zzn + zzegg.zzv(2, this.zzngt) : zzn;
    }
}
