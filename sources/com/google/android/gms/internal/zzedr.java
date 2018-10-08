package com.google.android.gms.internal;

import java.io.IOException;

class zzedr extends zzedq {
    protected final byte[] zzjao;

    zzedr(byte[] bArr) {
        this.zzjao = bArr;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzedk)) {
            return false;
        }
        if (size() != ((zzedk) obj).size()) {
            return false;
        }
        if (size() == 0) {
            return true;
        }
        if (!(obj instanceof zzedr)) {
            return obj.equals(this);
        }
        zzedr zzedr = (zzedr) obj;
        int zzcbn = zzcbn();
        int zzcbn2 = zzedr.zzcbn();
        return (zzcbn == 0 || zzcbn2 == 0 || zzcbn == zzcbn2) ? zza((zzedr) obj, 0, size()) : false;
    }

    public int size() {
        return this.zzjao.length;
    }

    final void zza(zzedj zzedj) throws IOException {
        zzedj.zzb(this.zzjao, zzcbo(), size());
    }

    protected void zza(byte[] bArr, int i, int i2, int i3) {
        System.arraycopy(this.zzjao, 0, bArr, 0, i3);
    }

    final boolean zza(zzedk zzedk, int i, int i2) {
        if (i2 > zzedk.size()) {
            throw new IllegalArgumentException("Length too large: " + i2 + size());
        } else if (i2 + 0 > zzedk.size()) {
            throw new IllegalArgumentException("Ran off end of other: 0" + ", " + i2 + ", " + zzedk.size());
        } else if (!(zzedk instanceof zzedr)) {
            return zzedk.zzs(0, i2 + 0).equals(zzs(0, i2));
        } else {
            zzedr zzedr = (zzedr) zzedk;
            byte[] bArr = this.zzjao;
            byte[] bArr2 = zzedr.zzjao;
            int zzcbo = zzcbo();
            int zzcbo2 = zzcbo();
            int zzcbo3 = zzedr.zzcbo();
            while (zzcbo2 < zzcbo + i2) {
                if (bArr[zzcbo2] != bArr2[zzcbo3]) {
                    return false;
                }
                zzcbo2++;
                zzcbo3++;
            }
            return true;
        }
    }

    public final zzedt zzcbm() {
        return zzedt.zzb(this.zzjao, zzcbo(), size(), true);
    }

    protected int zzcbo() {
        return 0;
    }

    protected final int zzf(int i, int i2, int i3) {
        return zzeen.zza(i, this.zzjao, zzcbo(), i3);
    }

    public byte zzgi(int i) {
        return this.zzjao[i];
    }

    public final zzedk zzs(int i, int i2) {
        int zzg = zzedk.zzg(i, i2, size());
        return zzg == 0 ? zzedk.zzmxr : new zzedn(this.zzjao, zzcbo() + i, zzg);
    }
}
