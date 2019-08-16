package com.google.android.gms.internal.measurement;

import com.google.android.gms.internal.measurement.zzey.zzd;
import java.io.IOException;
import java.util.Arrays;

public final class zzhs {
    private static final zzhs zzaly = new zzhs(0, new int[0], new Object[0], false);
    private int count;
    private boolean zzacz;
    private int zzaia;
    private Object[] zzakk;
    private int[] zzalz;

    private zzhs() {
        this(0, new int[8], new Object[8], true);
    }

    private zzhs(int i, int[] iArr, Object[] objArr, boolean z) {
        this.zzaia = -1;
        this.count = i;
        this.zzalz = iArr;
        this.zzakk = objArr;
        this.zzacz = z;
    }

    static zzhs zza(zzhs zzhs, zzhs zzhs2) {
        int i = zzhs.count + zzhs2.count;
        int[] copyOf = Arrays.copyOf(zzhs.zzalz, i);
        System.arraycopy(zzhs2.zzalz, 0, copyOf, zzhs.count, zzhs2.count);
        Object[] copyOf2 = Arrays.copyOf(zzhs.zzakk, i);
        System.arraycopy(zzhs2.zzakk, 0, copyOf2, zzhs.count, zzhs2.count);
        return new zzhs(i, copyOf, copyOf2, true);
    }

    private static void zzb(int i, Object obj, zzim zzim) throws IOException {
        int i2 = i >>> 3;
        switch (i & 7) {
            case 0:
                zzim.zzi(i2, ((Long) obj).longValue());
                return;
            case 1:
                zzim.zzc(i2, ((Long) obj).longValue());
                return;
            case 2:
                zzim.zza(i2, (zzdp) obj);
                return;
            case 3:
                if (zzim.zztk() == zzd.zzaio) {
                    zzim.zzbr(i2);
                    ((zzhs) obj).zzb(zzim);
                    zzim.zzbs(i2);
                    return;
                }
                zzim.zzbs(i2);
                ((zzhs) obj).zzb(zzim);
                zzim.zzbr(i2);
                return;
            case 5:
                zzim.zzf(i2, ((Integer) obj).intValue());
                return;
            default:
                throw new RuntimeException(zzfi.zzuy());
        }
    }

    public static zzhs zzwq() {
        return zzaly;
    }

    static zzhs zzwr() {
        return new zzhs();
    }

    public final boolean equals(Object obj) {
        boolean z;
        boolean z2;
        if (this != obj) {
            if (obj == null || !(obj instanceof zzhs)) {
                return false;
            }
            zzhs zzhs = (zzhs) obj;
            if (this.count != zzhs.count) {
                return false;
            }
            int[] iArr = this.zzalz;
            int[] iArr2 = zzhs.zzalz;
            int i = this.count;
            int i2 = 0;
            while (true) {
                if (i2 >= i) {
                    z = true;
                    break;
                } else if (iArr[i2] != iArr2[i2]) {
                    z = false;
                    break;
                } else {
                    i2++;
                }
            }
            if (!z) {
                return false;
            }
            Object[] objArr = this.zzakk;
            Object[] objArr2 = zzhs.zzakk;
            int i3 = this.count;
            int i4 = 0;
            while (true) {
                if (i4 >= i3) {
                    z2 = true;
                    break;
                } else if (!objArr[i4].equals(objArr2[i4])) {
                    z2 = false;
                    break;
                } else {
                    i4++;
                }
            }
            if (!z2) {
                return false;
            }
        }
        return true;
    }

    public final int hashCode() {
        int i = 17;
        int i2 = this.count;
        int[] iArr = this.zzalz;
        int i3 = 17;
        for (int i4 = 0; i4 < this.count; i4++) {
            i3 = (i3 * 31) + iArr[i4];
        }
        Object[] objArr = this.zzakk;
        for (int i5 = 0; i5 < this.count; i5++) {
            i = (i * 31) + objArr[i5].hashCode();
        }
        return i + ((((i2 + 527) * 31) + i3) * 31);
    }

    /* access modifiers changed from: 0000 */
    public final void zza(zzim zzim) throws IOException {
        if (zzim.zztk() == zzd.zzaip) {
            for (int i = this.count - 1; i >= 0; i--) {
                zzim.zza(this.zzalz[i] >>> 3, this.zzakk[i]);
            }
            return;
        }
        for (int i2 = 0; i2 < this.count; i2++) {
            zzim.zza(this.zzalz[i2] >>> 3, this.zzakk[i2]);
        }
    }

    /* access modifiers changed from: 0000 */
    public final void zzb(int i, Object obj) {
        if (!this.zzacz) {
            throw new UnsupportedOperationException();
        }
        if (this.count == this.zzalz.length) {
            int i2 = (this.count < 4 ? 8 : this.count >> 1) + this.count;
            this.zzalz = Arrays.copyOf(this.zzalz, i2);
            this.zzakk = Arrays.copyOf(this.zzakk, i2);
        }
        this.zzalz[this.count] = i;
        this.zzakk[this.count] = obj;
        this.count++;
    }

    public final void zzb(zzim zzim) throws IOException {
        if (this.count != 0) {
            if (zzim.zztk() == zzd.zzaio) {
                for (int i = 0; i < this.count; i++) {
                    zzb(this.zzalz[i], this.zzakk[i], zzim);
                }
                return;
            }
            for (int i2 = this.count - 1; i2 >= 0; i2--) {
                zzb(this.zzalz[i2], this.zzakk[i2], zzim);
            }
        }
    }

    /* access modifiers changed from: 0000 */
    public final void zzb(StringBuilder sb, int i) {
        for (int i2 = 0; i2 < this.count; i2++) {
            zzgj.zzb(sb, i, String.valueOf(this.zzalz[i2] >>> 3), this.zzakk[i2]);
        }
    }

    public final void zzry() {
        this.zzacz = false;
    }

    public final int zzuk() {
        int zzuk;
        int i = this.zzaia;
        if (i != -1) {
            return i;
        }
        int i2 = 0;
        for (int i3 = 0; i3 < this.count; i3++) {
            int i4 = this.zzalz[i3];
            int i5 = i4 >>> 3;
            switch (i4 & 7) {
                case 0:
                    zzuk = zzee.zze(i5, ((Long) this.zzakk[i3]).longValue());
                    break;
                case 1:
                    zzuk = zzee.zzg(i5, ((Long) this.zzakk[i3]).longValue());
                    break;
                case 2:
                    zzuk = zzee.zzc(i5, (zzdp) this.zzakk[i3]);
                    break;
                case 3:
                    zzuk = ((zzhs) this.zzakk[i3]).zzuk() + (zzee.zzbi(i5) << 1);
                    break;
                case 5:
                    zzuk = zzee.zzj(i5, ((Integer) this.zzakk[i3]).intValue());
                    break;
                default:
                    throw new IllegalStateException(zzfi.zzuy());
            }
            i2 += zzuk;
        }
        this.zzaia = i2;
        return i2;
    }

    public final int zzws() {
        int i = this.zzaia;
        if (i != -1) {
            return i;
        }
        int i2 = 0;
        for (int i3 = 0; i3 < this.count; i3++) {
            i2 += zzee.zzd(this.zzalz[i3] >>> 3, (zzdp) this.zzakk[i3]);
        }
        this.zzaia = i2;
        return i2;
    }
}
