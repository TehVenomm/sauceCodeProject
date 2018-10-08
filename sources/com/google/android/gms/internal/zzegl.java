package com.google.android.gms.internal;

import java.io.IOException;
import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

final class zzegl implements Cloneable {
    private Object value;
    private zzegj<?, ?> zznda;
    private List<zzegq> zzndb = new ArrayList();

    zzegl() {
    }

    private final byte[] toByteArray() throws IOException {
        byte[] bArr = new byte[zzn()];
        zza(zzegg.zzav(bArr));
        return bArr;
    }

    private zzegl zzcea() {
        zzegl zzegl = new zzegl();
        try {
            zzegl.zznda = this.zznda;
            if (this.zzndb == null) {
                zzegl.zzndb = null;
            } else {
                zzegl.zzndb.addAll(this.zzndb);
            }
            if (this.value != null) {
                if (this.value instanceof zzego) {
                    zzegl.value = (zzego) ((zzego) this.value).clone();
                } else if (this.value instanceof byte[]) {
                    zzegl.value = ((byte[]) this.value).clone();
                } else if (this.value instanceof byte[][]) {
                    byte[][] bArr = (byte[][]) this.value;
                    r4 = new byte[bArr.length][];
                    zzegl.value = r4;
                    for (r2 = 0; r2 < bArr.length; r2++) {
                        r4[r2] = (byte[]) bArr[r2].clone();
                    }
                } else if (this.value instanceof boolean[]) {
                    zzegl.value = ((boolean[]) this.value).clone();
                } else if (this.value instanceof int[]) {
                    zzegl.value = ((int[]) this.value).clone();
                } else if (this.value instanceof long[]) {
                    zzegl.value = ((long[]) this.value).clone();
                } else if (this.value instanceof float[]) {
                    zzegl.value = ((float[]) this.value).clone();
                } else if (this.value instanceof double[]) {
                    zzegl.value = ((double[]) this.value).clone();
                } else if (this.value instanceof zzego[]) {
                    zzego[] zzegoArr = (zzego[]) this.value;
                    r4 = new zzego[zzegoArr.length];
                    zzegl.value = r4;
                    for (r2 = 0; r2 < zzegoArr.length; r2++) {
                        r4[r2] = (zzego) zzegoArr[r2].clone();
                    }
                }
            }
            return zzegl;
        } catch (CloneNotSupportedException e) {
            throw new AssertionError(e);
        }
    }

    public final /* synthetic */ Object clone() throws CloneNotSupportedException {
        return zzcea();
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzegl)) {
            return false;
        }
        zzegl zzegl = (zzegl) obj;
        if (this.value != null && zzegl.value != null) {
            return this.zznda == zzegl.zznda ? !this.zznda.zzmhh.isArray() ? this.value.equals(zzegl.value) : this.value instanceof byte[] ? Arrays.equals((byte[]) this.value, (byte[]) zzegl.value) : this.value instanceof int[] ? Arrays.equals((int[]) this.value, (int[]) zzegl.value) : this.value instanceof long[] ? Arrays.equals((long[]) this.value, (long[]) zzegl.value) : this.value instanceof float[] ? Arrays.equals((float[]) this.value, (float[]) zzegl.value) : this.value instanceof double[] ? Arrays.equals((double[]) this.value, (double[]) zzegl.value) : this.value instanceof boolean[] ? Arrays.equals((boolean[]) this.value, (boolean[]) zzegl.value) : Arrays.deepEquals((Object[]) this.value, (Object[]) zzegl.value) : false;
        } else {
            if (this.zzndb != null && zzegl.zzndb != null) {
                return this.zzndb.equals(zzegl.zzndb);
            }
            try {
                return Arrays.equals(toByteArray(), zzegl.toByteArray());
            } catch (Throwable e) {
                throw new IllegalStateException(e);
            }
        }
    }

    public final int hashCode() {
        try {
            return Arrays.hashCode(toByteArray()) + 527;
        } catch (Throwable e) {
            throw new IllegalStateException(e);
        }
    }

    final void zza(zzegg zzegg) throws IOException {
        if (this.value != null) {
            zzegj zzegj = this.zznda;
            Object obj = this.value;
            if (zzegj.zzncv) {
                int length = Array.getLength(obj);
                for (int i = 0; i < length; i++) {
                    Object obj2 = Array.get(obj, i);
                    if (obj2 != null) {
                        zzegj.zza(obj2, zzegg);
                    }
                }
                return;
            }
            zzegj.zza(obj, zzegg);
            return;
        }
        for (zzegq zzegq : this.zzndb) {
            zzegg.zzhc(zzegq.tag);
            zzegg.zzax(zzegq.zzjao);
        }
    }

    final void zza(zzegq zzegq) {
        this.zzndb.add(zzegq);
    }

    final <T> T zzb(zzegj<?, T> zzegj) {
        if (this.value == null) {
            this.zznda = zzegj;
            this.value = zzegj.zzav(this.zzndb);
            this.zzndb = null;
        } else if (!this.zznda.equals(zzegj)) {
            throw new IllegalStateException("Tried to getExtension with a different Extension.");
        }
        return this.value;
    }

    final int zzn() {
        int i = 0;
        int i2;
        if (this.value != null) {
            zzegj zzegj = this.zznda;
            Object obj = this.value;
            if (!zzegj.zzncv) {
                return zzegj.zzbx(obj);
            }
            int length = Array.getLength(obj);
            for (i2 = 0; i2 < length; i2++) {
                if (Array.get(obj, i2) != null) {
                    i += zzegj.zzbx(Array.get(obj, i2));
                }
            }
            return i;
        }
        i2 = 0;
        for (zzegq zzegq : this.zzndb) {
            i2 = (zzegq.zzjao.length + (zzegg.zzhd(zzegq.tag) + 0)) + i2;
        }
        return i2;
    }
}
