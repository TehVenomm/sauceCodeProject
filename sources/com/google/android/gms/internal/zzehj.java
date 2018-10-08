package com.google.android.gms.internal;

import java.io.IOException;

public final class zzehj extends zzegi<zzehj> implements Cloneable {
    private static volatile zzehj[] zzngr;
    private String key;
    private String value;

    public zzehj() {
        this.key = "";
        this.value = "";
        this.zzncu = null;
        this.zzndd = -1;
    }

    public static zzehj[] zzceo() {
        if (zzngr == null) {
            synchronized (zzegm.zzndc) {
                if (zzngr == null) {
                    zzngr = new zzehj[0];
                }
            }
        }
        return zzngr;
    }

    private zzehj zzcep() {
        try {
            return (zzehj) super.zzcdy();
        } catch (CloneNotSupportedException e) {
            throw new AssertionError(e);
        }
    }

    public final /* synthetic */ Object clone() throws CloneNotSupportedException {
        return zzcep();
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzehj)) {
            return false;
        }
        zzehj zzehj = (zzehj) obj;
        if (this.key == null) {
            if (zzehj.key != null) {
                return false;
            }
        } else if (!this.key.equals(zzehj.key)) {
            return false;
        }
        if (this.value == null) {
            if (zzehj.value != null) {
                return false;
            }
        } else if (!this.value.equals(zzehj.value)) {
            return false;
        }
        return (this.zzncu == null || this.zzncu.isEmpty()) ? zzehj.zzncu == null || zzehj.zzncu.isEmpty() : this.zzncu.equals(zzehj.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = this.key == null ? 0 : this.key.hashCode();
        int hashCode3 = this.value == null ? 0 : this.value.hashCode();
        if (!(this.zzncu == null || this.zzncu.isEmpty())) {
            i = this.zzncu.hashCode();
        }
        return ((((hashCode2 + ((hashCode + 527) * 31)) * 31) + hashCode3) * 31) + i;
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            switch (zzcbr) {
                case 0:
                    break;
                case 10:
                    this.key = zzegf.readString();
                    continue;
                case 18:
                    this.value = zzegf.readString();
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
        if (!(this.key == null || this.key.equals(""))) {
            zzegg.zzl(1, this.key);
        }
        if (!(this.value == null || this.value.equals(""))) {
            zzegg.zzl(2, this.value);
        }
        super.zza(zzegg);
    }

    public final /* synthetic */ zzegi zzcdy() throws CloneNotSupportedException {
        return (zzehj) clone();
    }

    public final /* synthetic */ zzego zzcdz() throws CloneNotSupportedException {
        return (zzehj) clone();
    }

    protected final int zzn() {
        int zzn = super.zzn();
        if (!(this.key == null || this.key.equals(""))) {
            zzn += zzegg.zzm(1, this.key);
        }
        return (this.value == null || this.value.equals("")) ? zzn : zzn + zzegg.zzm(2, this.value);
    }
}
