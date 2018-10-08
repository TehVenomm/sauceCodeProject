package com.google.android.gms.internal;

import java.io.IOException;

public final class zzehg extends zzegi<zzehg> implements Cloneable {
    private String version;
    private int zzily;
    private String zznft;

    public zzehg() {
        this.zzily = 0;
        this.zznft = "";
        this.version = "";
        this.zzncu = null;
        this.zzndd = -1;
    }

    private zzehg zzcel() {
        try {
            return (zzehg) super.zzcdy();
        } catch (CloneNotSupportedException e) {
            throw new AssertionError(e);
        }
    }

    public final /* synthetic */ Object clone() throws CloneNotSupportedException {
        return zzcel();
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzehg)) {
            return false;
        }
        zzehg zzehg = (zzehg) obj;
        if (this.zzily != zzehg.zzily) {
            return false;
        }
        if (this.zznft == null) {
            if (zzehg.zznft != null) {
                return false;
            }
        } else if (!this.zznft.equals(zzehg.zznft)) {
            return false;
        }
        if (this.version == null) {
            if (zzehg.version != null) {
                return false;
            }
        } else if (!this.version.equals(zzehg.version)) {
            return false;
        }
        return (this.zzncu == null || this.zzncu.isEmpty()) ? zzehg.zzncu == null || zzehg.zzncu.isEmpty() : this.zzncu.equals(zzehg.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int i2 = this.zzily;
        int hashCode2 = this.zznft == null ? 0 : this.zznft.hashCode();
        int hashCode3 = this.version == null ? 0 : this.version.hashCode();
        if (!(this.zzncu == null || this.zzncu.isEmpty())) {
            i = this.zzncu.hashCode();
        }
        return ((((hashCode2 + ((((hashCode + 527) * 31) + i2) * 31)) * 31) + hashCode3) * 31) + i;
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            switch (zzcbr) {
                case 0:
                    break;
                case 8:
                    this.zzily = zzegf.zzcbs();
                    continue;
                case 18:
                    this.zznft = zzegf.readString();
                    continue;
                case 26:
                    this.version = zzegf.readString();
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
        if (this.zzily != 0) {
            zzegg.zzu(1, this.zzily);
        }
        if (!(this.zznft == null || this.zznft.equals(""))) {
            zzegg.zzl(2, this.zznft);
        }
        if (!(this.version == null || this.version.equals(""))) {
            zzegg.zzl(3, this.version);
        }
        super.zza(zzegg);
    }

    public final /* synthetic */ zzegi zzcdy() throws CloneNotSupportedException {
        return (zzehg) clone();
    }

    public final /* synthetic */ zzego zzcdz() throws CloneNotSupportedException {
        return (zzehg) clone();
    }

    protected final int zzn() {
        int zzn = super.zzn();
        if (this.zzily != 0) {
            zzn += zzegg.zzv(1, this.zzily);
        }
        if (!(this.zznft == null || this.zznft.equals(""))) {
            zzn += zzegg.zzm(2, this.zznft);
        }
        return (this.version == null || this.version.equals("")) ? zzn : zzn + zzegg.zzm(3, this.version);
    }
}
