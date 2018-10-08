package com.google.android.gms.internal;

import java.io.IOException;

public abstract class zzegi<M extends zzegi<M>> extends zzego {
    protected zzegk zzncu;

    public /* synthetic */ Object clone() throws CloneNotSupportedException {
        return zzcdy();
    }

    public final <T> T zza(zzegj<M, T> zzegj) {
        if (this.zzncu == null) {
            return null;
        }
        zzegl zzhf = this.zzncu.zzhf(zzegj.tag >>> 3);
        return zzhf != null ? zzhf.zzb(zzegj) : null;
    }

    public void zza(zzegg zzegg) throws IOException {
        if (this.zzncu != null) {
            for (int i = 0; i < this.zzncu.size(); i++) {
                this.zzncu.zzhg(i).zza(zzegg);
            }
        }
    }

    protected final boolean zza(zzegf zzegf, int i) throws IOException {
        int position = zzegf.getPosition();
        if (!zzegf.zzgl(i)) {
            return false;
        }
        int i2 = i >>> 3;
        zzegq zzegq = new zzegq(i, zzegf.zzz(position, zzegf.getPosition() - position));
        zzegl zzegl = null;
        if (this.zzncu == null) {
            this.zzncu = new zzegk();
        } else {
            zzegl = this.zzncu.zzhf(i2);
        }
        if (zzegl == null) {
            zzegl = new zzegl();
            this.zzncu.zza(i2, zzegl);
        }
        zzegl.zza(zzegq);
        return true;
    }

    public M zzcdy() throws CloneNotSupportedException {
        zzegi zzegi = (zzegi) super.zzcdz();
        zzegm.zza(this, zzegi);
        return zzegi;
    }

    public /* synthetic */ zzego zzcdz() throws CloneNotSupportedException {
        return (zzegi) clone();
    }

    protected int zzn() {
        int i = 0;
        if (this.zzncu == null) {
            return 0;
        }
        int i2 = 0;
        while (i < this.zzncu.size()) {
            i2 += this.zzncu.zzhg(i).zzn();
            i++;
        }
        return i2;
    }
}
