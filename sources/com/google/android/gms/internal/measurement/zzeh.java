package com.google.android.gms.internal.measurement;

import java.util.Arrays;
import java.util.Collection;
import java.util.RandomAccess;

final class zzeh extends zzdj<Double> implements zzff<Double>, zzgu, RandomAccess {
    private static final zzeh zzaeo;
    private int size;
    private double[] zzaep;

    static {
        zzeh zzeh = new zzeh(new double[0], 0);
        zzaeo = zzeh;
        zzeh.zzry();
    }

    zzeh() {
        this(new double[10], 0);
    }

    private zzeh(double[] dArr, int i) {
        this.zzaep = dArr;
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

    private final void zzc(int i, double d) {
        zzrz();
        if (i < 0 || i > this.size) {
            throw new IndexOutOfBoundsException(zzao(i));
        }
        if (this.size < this.zzaep.length) {
            System.arraycopy(this.zzaep, i, this.zzaep, i + 1, this.size - i);
        } else {
            double[] dArr = new double[(((this.size * 3) / 2) + 1)];
            System.arraycopy(this.zzaep, 0, dArr, 0, i);
            System.arraycopy(this.zzaep, i, dArr, i + 1, this.size - i);
            this.zzaep = dArr;
        }
        this.zzaep[i] = d;
        this.size++;
        this.modCount++;
    }

    public final /* synthetic */ void add(int i, Object obj) {
        zzc(i, ((Double) obj).doubleValue());
    }

    public final boolean addAll(Collection<? extends Double> collection) {
        zzrz();
        zzez.checkNotNull(collection);
        if (!(collection instanceof zzeh)) {
            return super.addAll(collection);
        }
        zzeh zzeh = (zzeh) collection;
        if (zzeh.size == 0) {
            return false;
        }
        if (Integer.MAX_VALUE - this.size < zzeh.size) {
            throw new OutOfMemoryError();
        }
        int i = this.size + zzeh.size;
        if (i > this.zzaep.length) {
            this.zzaep = Arrays.copyOf(this.zzaep, i);
        }
        System.arraycopy(zzeh.zzaep, 0, this.zzaep, this.size, zzeh.size);
        this.size = i;
        this.modCount++;
        return true;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzeh)) {
            return super.equals(obj);
        }
        zzeh zzeh = (zzeh) obj;
        if (this.size == zzeh.size) {
            double[] dArr = zzeh.zzaep;
            int i = 0;
            while (i < this.size) {
                if (Double.doubleToLongBits(this.zzaep[i]) == Double.doubleToLongBits(dArr[i])) {
                    i++;
                }
            }
            return true;
        }
        return false;
    }

    public final /* synthetic */ Object get(int i) {
        zzan(i);
        return Double.valueOf(this.zzaep[i]);
    }

    public final int hashCode() {
        int i = 1;
        for (int i2 = 0; i2 < this.size; i2++) {
            i = (i * 31) + zzez.zzbx(Double.doubleToLongBits(this.zzaep[i2]));
        }
        return i;
    }

    public final /* synthetic */ Object remove(int i) {
        zzrz();
        zzan(i);
        double d = this.zzaep[i];
        if (i < this.size - 1) {
            System.arraycopy(this.zzaep, i + 1, this.zzaep, i, (this.size - i) - 1);
        }
        this.size--;
        this.modCount++;
        return Double.valueOf(d);
    }

    public final boolean remove(Object obj) {
        zzrz();
        for (int i = 0; i < this.size; i++) {
            if (obj.equals(Double.valueOf(this.zzaep[i]))) {
                System.arraycopy(this.zzaep, i + 1, this.zzaep, i, (this.size - i) - 1);
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
        System.arraycopy(this.zzaep, i2, this.zzaep, i, this.size - i2);
        this.size -= i2 - i;
        this.modCount++;
    }

    public final /* synthetic */ Object set(int i, Object obj) {
        double doubleValue = ((Double) obj).doubleValue();
        zzrz();
        zzan(i);
        double d = this.zzaep[i];
        this.zzaep[i] = doubleValue;
        return Double.valueOf(d);
    }

    public final int size() {
        return this.size;
    }

    public final /* synthetic */ zzff zzap(int i) {
        if (i >= this.size) {
            return new zzeh(Arrays.copyOf(this.zzaep, i), this.size);
        }
        throw new IllegalArgumentException();
    }

    public final void zzf(double d) {
        zzc(this.size, d);
    }
}
