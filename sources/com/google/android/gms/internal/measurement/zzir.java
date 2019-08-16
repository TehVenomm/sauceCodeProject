package com.google.android.gms.internal.measurement;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;

final class zzir implements Cloneable {
    private Object value;
    private zzip<?, ?> zzaop;
    private List<zziy> zzaoq = new ArrayList();

    zzir() {
    }

    private final byte[] toByteArray() throws IOException {
        byte[] bArr = new byte[zzqy()];
        zza(zzio.zzj(bArr));
        return bArr;
    }

    /* access modifiers changed from: private */
    /* renamed from: zzxc */
    public final zzir clone() {
        int i = 0;
        zzir zzir = new zzir();
        try {
            zzir.zzaop = this.zzaop;
            if (this.zzaoq == null) {
                zzir.zzaoq = null;
            } else {
                zzir.zzaoq.addAll(this.zzaoq);
            }
            if (this.value != null) {
                if (this.value instanceof zziw) {
                    zzir.value = (zziw) ((zziw) this.value).clone();
                } else if (this.value instanceof byte[]) {
                    zzir.value = ((byte[]) this.value).clone();
                } else if (this.value instanceof byte[][]) {
                    byte[][] bArr = (byte[][]) this.value;
                    byte[][] bArr2 = new byte[bArr.length][];
                    zzir.value = bArr2;
                    for (int i2 = 0; i2 < bArr.length; i2++) {
                        bArr2[i2] = (byte[]) bArr[i2].clone();
                    }
                } else if (this.value instanceof boolean[]) {
                    zzir.value = ((boolean[]) this.value).clone();
                } else if (this.value instanceof int[]) {
                    zzir.value = ((int[]) this.value).clone();
                } else if (this.value instanceof long[]) {
                    zzir.value = ((long[]) this.value).clone();
                } else if (this.value instanceof float[]) {
                    zzir.value = ((float[]) this.value).clone();
                } else if (this.value instanceof double[]) {
                    zzir.value = ((double[]) this.value).clone();
                } else if (this.value instanceof zziw[]) {
                    zziw[] zziwArr = (zziw[]) this.value;
                    zziw[] zziwArr2 = new zziw[zziwArr.length];
                    zzir.value = zziwArr2;
                    while (true) {
                        int i3 = i;
                        if (i3 >= zziwArr.length) {
                            break;
                        }
                        zziwArr2[i3] = (zziw) zziwArr[i3].clone();
                        i = i3 + 1;
                    }
                }
            }
            return zzir;
        } catch (CloneNotSupportedException e) {
            throw new AssertionError(e);
        }
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzir)) {
            return false;
        }
        zzir zzir = (zzir) obj;
        if (this.value == null || zzir.value == null) {
            if (this.zzaoq != null && zzir.zzaoq != null) {
                return this.zzaoq.equals(zzir.zzaoq);
            }
            try {
                return Arrays.equals(toByteArray(), zzir.toByteArray());
            } catch (IOException e) {
                throw new IllegalStateException(e);
            }
        } else if (this.zzaop == zzir.zzaop) {
            return !this.zzaop.zzaon.isArray() ? this.value.equals(zzir.value) : this.value instanceof byte[] ? Arrays.equals((byte[]) this.value, (byte[]) zzir.value) : this.value instanceof int[] ? Arrays.equals((int[]) this.value, (int[]) zzir.value) : this.value instanceof long[] ? Arrays.equals((long[]) this.value, (long[]) zzir.value) : this.value instanceof float[] ? Arrays.equals((float[]) this.value, (float[]) zzir.value) : this.value instanceof double[] ? Arrays.equals((double[]) this.value, (double[]) zzir.value) : this.value instanceof boolean[] ? Arrays.equals((boolean[]) this.value, (boolean[]) zzir.value) : Arrays.deepEquals((Object[]) this.value, (Object[]) zzir.value);
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
    public final void zza(zzio zzio) throws IOException {
        if (this.value != null) {
            throw new NoSuchMethodError();
        }
        for (zziy zziy : this.zzaoq) {
            zzio.zzck(zziy.tag);
            zzio.zzk(zziy.zzado);
        }
    }

    /* access modifiers changed from: 0000 */
    public final void zza(zziy zziy) throws IOException {
        if (this.zzaoq != null) {
            this.zzaoq.add(zziy);
        } else if (this.value instanceof zziw) {
            byte[] bArr = zziy.zzado;
            zzil zzj = zzil.zzj(bArr, 0, bArr.length);
            int zzta = zzj.zzta();
            if (zzta != bArr.length - zzio.zzbj(zzta)) {
                throw zzit.zzxd();
            }
            zziw zza = ((zziw) this.value).zza(zzj);
            this.zzaop = this.zzaop;
            this.value = zza;
            this.zzaoq = null;
        } else if (this.value instanceof zziw[]) {
            Collections.singletonList(zziy);
            throw new NoSuchMethodError();
        } else if (this.value instanceof zzgi) {
            Collections.singletonList(zziy);
            throw new NoSuchMethodError();
        } else if (this.value instanceof zzgi[]) {
            Collections.singletonList(zziy);
            throw new NoSuchMethodError();
        } else {
            Collections.singletonList(zziy);
            throw new NoSuchMethodError();
        }
    }

    /* access modifiers changed from: 0000 */
    public final int zzqy() {
        if (this.value != null) {
            throw new NoSuchMethodError();
        }
        Iterator it = this.zzaoq.iterator();
        int i = 0;
        while (true) {
            int i2 = i;
            if (!it.hasNext()) {
                return i2;
            }
            zziy zziy = (zziy) it.next();
            i = zziy.zzado.length + zzio.zzbq(zziy.tag) + 0 + i2;
        }
    }
}
