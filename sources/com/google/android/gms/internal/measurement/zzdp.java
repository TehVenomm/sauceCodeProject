package com.google.android.gms.internal.measurement;

import java.io.IOException;
import java.io.Serializable;
import java.nio.charset.Charset;
import java.util.Comparator;
import java.util.Iterator;

public abstract class zzdp implements Serializable, Iterable<Byte> {
    public static final zzdp zzadh = new zzdz(zzez.zzair);
    private static final zzdv zzadi = (zzdi.zzrv() ? new zzdy(null) : new zzdt(null));
    private static final Comparator<zzdp> zzadk = new zzdr();
    private int zzadj = 0;

    zzdp() {
    }

    /* access modifiers changed from: private */
    public static int zza(byte b) {
        return b & 255;
    }

    static zzdx zzas(int i) {
        return new zzdx(i, null);
    }

    static int zzb(int i, int i2, int i3) {
        int i4 = i2 - i;
        if ((i | i2 | i4 | (i3 - i2)) >= 0) {
            return i4;
        }
        if (i < 0) {
            throw new IndexOutOfBoundsException("Beginning index: " + i + " < 0");
        } else if (i2 < i) {
            throw new IndexOutOfBoundsException("Beginning index larger than ending index: " + i + ", " + i2);
        } else {
            throw new IndexOutOfBoundsException("End index: " + i2 + " >= " + i3);
        }
    }

    public static zzdp zzb(byte[] bArr, int i, int i2) {
        zzb(i, i + i2, bArr.length);
        return new zzdz(zzadi.zzc(bArr, i, i2));
    }

    public static zzdp zzdq(String str) {
        return new zzdz(str.getBytes(zzez.UTF_8));
    }

    static zzdp zze(byte[] bArr) {
        return new zzdz(bArr);
    }

    public abstract boolean equals(Object obj);

    public final int hashCode() {
        int i = this.zzadj;
        if (i == 0) {
            int size = size();
            i = zza(size, 0, size);
            if (i == 0) {
                i = 1;
            }
            this.zzadj = i;
        }
        return i;
    }

    public /* synthetic */ Iterator iterator() {
        return new zzdo(this);
    }

    public abstract int size();

    public final String toString() {
        return String.format("<ByteString@%s size=%d>", new Object[]{Integer.toHexString(System.identityHashCode(this)), Integer.valueOf(size())});
    }

    /* access modifiers changed from: protected */
    public abstract int zza(int i, int i2, int i3);

    public abstract zzdp zza(int i, int i2);

    /* access modifiers changed from: protected */
    public abstract String zza(Charset charset);

    /* access modifiers changed from: 0000 */
    public abstract void zza(zzdm zzdm) throws IOException;

    public abstract byte zzaq(int i);

    /* access modifiers changed from: 0000 */
    public abstract byte zzar(int i);

    public final String zzsa() {
        return size() == 0 ? "" : zza(zzez.UTF_8);
    }

    public abstract boolean zzsb();

    /* access modifiers changed from: protected */
    public final int zzsc() {
        return this.zzadj;
    }
}
