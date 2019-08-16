package com.google.android.gms.internal.drive;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;

final class zziu implements Cloneable {
    private Object value;
    private zzis<?, ?> zznc;
    private List<zziz> zznd = new ArrayList();

    zziu() {
    }

    private final byte[] toByteArray() throws IOException {
        byte[] bArr = new byte[zzaq()];
        zza(zzip.zzb(bArr));
        return bArr;
    }

    /* access modifiers changed from: private */
    /* renamed from: zzbj */
    public final zziu clone() {
        int i = 0;
        zziu zziu = new zziu();
        try {
            zziu.zznc = this.zznc;
            if (this.zznd == null) {
                zziu.zznd = null;
            } else {
                zziu.zznd.addAll(this.zznd);
            }
            if (this.value != null) {
                if (this.value instanceof zzix) {
                    zziu.value = (zzix) ((zzix) this.value).clone();
                } else if (this.value instanceof byte[]) {
                    zziu.value = ((byte[]) this.value).clone();
                } else if (this.value instanceof byte[][]) {
                    byte[][] bArr = (byte[][]) this.value;
                    byte[][] bArr2 = new byte[bArr.length][];
                    zziu.value = bArr2;
                    for (int i2 = 0; i2 < bArr.length; i2++) {
                        bArr2[i2] = (byte[]) bArr[i2].clone();
                    }
                } else if (this.value instanceof boolean[]) {
                    zziu.value = ((boolean[]) this.value).clone();
                } else if (this.value instanceof int[]) {
                    zziu.value = ((int[]) this.value).clone();
                } else if (this.value instanceof long[]) {
                    zziu.value = ((long[]) this.value).clone();
                } else if (this.value instanceof float[]) {
                    zziu.value = ((float[]) this.value).clone();
                } else if (this.value instanceof double[]) {
                    zziu.value = ((double[]) this.value).clone();
                } else if (this.value instanceof zzix[]) {
                    zzix[] zzixArr = (zzix[]) this.value;
                    zzix[] zzixArr2 = new zzix[zzixArr.length];
                    zziu.value = zzixArr2;
                    while (true) {
                        int i3 = i;
                        if (i3 >= zzixArr.length) {
                            break;
                        }
                        zzixArr2[i3] = (zzix) zzixArr[i3].clone();
                        i = i3 + 1;
                    }
                }
            }
            return zziu;
        } catch (CloneNotSupportedException e) {
            throw new AssertionError(e);
        }
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zziu)) {
            return false;
        }
        zziu zziu = (zziu) obj;
        if (this.value == null || zziu.value == null) {
            if (this.zznd != null && zziu.zznd != null) {
                return this.zznd.equals(zziu.zznd);
            }
            try {
                return Arrays.equals(toByteArray(), zziu.toByteArray());
            } catch (IOException e) {
                throw new IllegalStateException(e);
            }
        } else if (this.zznc == zziu.zznc) {
            return !this.zznc.zzmx.isArray() ? this.value.equals(zziu.value) : this.value instanceof byte[] ? Arrays.equals((byte[]) this.value, (byte[]) zziu.value) : this.value instanceof int[] ? Arrays.equals((int[]) this.value, (int[]) zziu.value) : this.value instanceof long[] ? Arrays.equals((long[]) this.value, (long[]) zziu.value) : this.value instanceof float[] ? Arrays.equals((float[]) this.value, (float[]) zziu.value) : this.value instanceof double[] ? Arrays.equals((double[]) this.value, (double[]) zziu.value) : this.value instanceof boolean[] ? Arrays.equals((boolean[]) this.value, (boolean[]) zziu.value) : Arrays.deepEquals((Object[]) this.value, (Object[]) zziu.value);
        } else {
            return false;
        }
    }

    public final int hashCode() {
        try {
            return Arrays.hashCode(toByteArray()) + 527;
        } catch (IOException e) {
            throw new IllegalStateException(e);
        }
    }

    /* access modifiers changed from: 0000 */
    public final void zza(zzip zzip) throws IOException {
        if (this.value != null) {
            throw new NoSuchMethodError();
        }
        for (zziz zziz : this.zznd) {
            zzip.zzp(zziz.tag);
            zzip.zzc(zziz.zzng);
        }
    }

    /* access modifiers changed from: 0000 */
    public final void zza(zziz zziz) throws IOException {
        if (this.zznd != null) {
            this.zznd.add(zziz);
        } else if (this.value instanceof zzix) {
            byte[] bArr = zziz.zzng;
            zzio zza = zzio.zza(bArr, 0, bArr.length);
            int zzbe = zza.zzbe();
            if (zzbe != bArr.length - zzip.zzm(zzbe)) {
                throw zziw.zzbk();
            }
            zzix zza2 = ((zzix) this.value).zza(zza);
            this.zznc = this.zznc;
            this.value = zza2;
            this.zznd = null;
        } else if (this.value instanceof zzix[]) {
            Collections.singletonList(zziz);
            throw new NoSuchMethodError();
        } else {
            Collections.singletonList(zziz);
            throw new NoSuchMethodError();
        }
    }

    /* access modifiers changed from: 0000 */
    public final int zzaq() {
        if (this.value != null) {
            throw new NoSuchMethodError();
        }
        Iterator it = this.zznd.iterator();
        int i = 0;
        while (true) {
            int i2 = i;
            if (!it.hasNext()) {
                return i2;
            }
            zziz zziz = (zziz) it.next();
            i = zziz.zzng.length + zzip.zzq(zziz.tag) + 0 + i2;
        }
    }
}
