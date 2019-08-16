package com.google.android.gms.internal.measurement;

import java.util.Arrays;
import java.util.Collection;
import java.util.RandomAccess;

final class zzeu extends zzdj<Float> implements zzff<Float>, zzgu, RandomAccess {
    private static final zzeu zzahm;
    private int size;
    private float[] zzahn;

    static {
        zzeu zzeu = new zzeu(new float[0], 0);
        zzahm = zzeu;
        zzeu.zzry();
    }

    zzeu() {
        this(new float[10], 0);
    }

    private zzeu(float[] fArr, int i) {
        this.zzahn = fArr;
        this.size = i;
    }

    private final void zzan(int i) {
        if (i < 0 || i >= this.size) {
            throw new IndexOutOfBoundsException(zzao(i));
        }
    }

    private final String zzao(int i) {
        return "Index:" + i + ", Size:" + this.size;
    }

    private final void zzc(int i, float f) {
        zzrz();
        if (i < 0 || i > this.size) {
            throw new IndexOutOfBoundsException(zzao(i));
        }
        if (this.size < this.zzahn.length) {
            System.arraycopy(this.zzahn, i, this.zzahn, i + 1, this.size - i);
        } else {
            float[] fArr = new float[(((this.size * 3) / 2) + 1)];
            System.arraycopy(this.zzahn, 0, fArr, 0, i);
            System.arraycopy(this.zzahn, i, fArr, i + 1, this.size - i);
            this.zzahn = fArr;
        }
        this.zzahn[i] = f;
        this.size++;
        this.modCount++;
    }

    public final /* synthetic */ void add(int i, Object obj) {
        zzc(i, ((Float) obj).floatValue());
    }

    public final boolean addAll(Collection<? extends Float> collection) {
        zzrz();
        zzez.checkNotNull(collection);
        if (!(collection instanceof zzeu)) {
            return super.addAll(collection);
        }
        zzeu zzeu = (zzeu) collection;
        if (zzeu.size == 0) {
            return false;
        }
        if (Integer.MAX_VALUE - this.size < zzeu.size) {
            throw new OutOfMemoryError();
        }
        int i = this.size + zzeu.size;
        if (i > this.zzahn.length) {
            this.zzahn = Arrays.copyOf(this.zzahn, i);
        }
        System.arraycopy(zzeu.zzahn, 0, this.zzahn, this.size, zzeu.size);
        this.size = i;
        this.modCount++;
        return true;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzeu)) {
            return super.equals(obj);
        }
        zzeu zzeu = (zzeu) obj;
        if (this.size == zzeu.size) {
            float[] fArr = zzeu.zzahn;
            int i = 0;
            while (i < this.size) {
                if (Float.floatToIntBits(this.zzahn[i]) == Float.floatToIntBits(fArr[i])) {
                    i++;
                }
            }
            return true;
        }
        return false;
    }

    public final /* synthetic */ Object get(int i) {
        zzan(i);
        return Float.valueOf(this.zzahn[i]);
    }

    public final int hashCode() {
        int i = 1;
        for (int i2 = 0; i2 < this.size; i2++) {
            i = (i * 31) + Float.floatToIntBits(this.zzahn[i2]);
        }
        return i;
    }

    public final /* synthetic */ Object remove(int i) {
        zzrz();
        zzan(i);
        float f = this.zzahn[i];
        if (i < this.size - 1) {
            System.arraycopy(this.zzahn, i + 1, this.zzahn, i, (this.size - i) - 1);
        }
        this.size--;
        this.modCount++;
        return Float.valueOf(f);
    }

    public final boolean remove(Object obj) {
        zzrz();
        for (int i = 0; i < this.size; i++) {
            if (obj.equals(Float.valueOf(this.zzahn[i]))) {
                System.arraycopy(this.zzahn, i + 1, this.zzahn, i, (this.size - i) - 1);
                this.size--;
                this.modCount++;
                return true;
            }
        }
        return false;
    }

    /* access modifiers changed from: protected */
    public final void removeRange(int i, int i2) {
        zzrz();
        if (i2 < i) {
            throw new IndexOutOfBoundsException("toIndex < fromIndex");
        }
        System.arraycopy(this.zzahn, i2, this.zzahn, i, this.size - i2);
        this.size -= i2 - i;
        this.modCount++;
    }

    public final /* synthetic */ Object set(int i, Object obj) {
        float floatValue = ((Float) obj).floatValue();
        zzrz();
        zzan(i);
        float f = this.zzahn[i];
        this.zzahn[i] = floatValue;
        return Float.valueOf(f);
    }

    public final int size() {
        return this.size;
    }

    public final /* synthetic */ zzff zzap(int i) {
        if (i >= this.size) {
            return new zzeu(Arrays.copyOf(this.zzahn, i), this.size);
        }
        throw new IllegalArgumentException();
    }

    public final void zzc(float f) {
        zzc(this.size, f);
    }
}
