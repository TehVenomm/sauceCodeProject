package com.google.android.gms.internal.measurement;

import java.util.Arrays;
import java.util.Collection;
import java.util.RandomAccess;

final class zzdn extends zzdj<Boolean> implements zzff<Boolean>, zzgu, RandomAccess {
    private static final zzdn zzade;
    private int size;
    private boolean[] zzadf;

    static {
        zzdn zzdn = new zzdn(new boolean[0], 0);
        zzade = zzdn;
        zzdn.zzry();
    }

    zzdn() {
        this(new boolean[10], 0);
    }

    private zzdn(boolean[] zArr, int i) {
        this.zzadf = zArr;
        this.size = i;
    }

    private final void zza(int i, boolean z) {
        zzrz();
        if (i < 0 || i > this.size) {
            throw new IndexOutOfBoundsException(zzao(i));
        }
        if (this.size < this.zzadf.length) {
            System.arraycopy(this.zzadf, i, this.zzadf, i + 1, this.size - i);
        } else {
            boolean[] zArr = new boolean[(((this.size * 3) / 2) + 1)];
            System.arraycopy(this.zzadf, 0, zArr, 0, i);
            System.arraycopy(this.zzadf, i, zArr, i + 1, this.size - i);
            this.zzadf = zArr;
        }
        this.zzadf[i] = z;
        this.size++;
        this.modCount++;
    }

    private final void zzan(int i) {
        if (i < 0 || i >= this.size) {
            throw new IndexOutOfBoundsException(zzao(i));
        }
    }

    private final String zzao(int i) {
        return "Index:" + i + ", Size:" + this.size;
    }

    public final /* synthetic */ void add(int i, Object obj) {
        zza(i, ((Boolean) obj).booleanValue());
    }

    public final boolean addAll(Collection<? extends Boolean> collection) {
        zzrz();
        zzez.checkNotNull(collection);
        if (!(collection instanceof zzdn)) {
            return super.addAll(collection);
        }
        zzdn zzdn = (zzdn) collection;
        if (zzdn.size == 0) {
            return false;
        }
        if (Integer.MAX_VALUE - this.size < zzdn.size) {
            throw new OutOfMemoryError();
        }
        int i = this.size + zzdn.size;
        if (i > this.zzadf.length) {
            this.zzadf = Arrays.copyOf(this.zzadf, i);
        }
        System.arraycopy(zzdn.zzadf, 0, this.zzadf, this.size, zzdn.size);
        this.size = i;
        this.modCount++;
        return true;
    }

    public final void addBoolean(boolean z) {
        zza(this.size, z);
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzdn)) {
            return super.equals(obj);
        }
        zzdn zzdn = (zzdn) obj;
        if (this.size == zzdn.size) {
            boolean[] zArr = zzdn.zzadf;
            int i = 0;
            while (i < this.size) {
                if (this.zzadf[i] == zArr[i]) {
                    i++;
                }
            }
            return true;
        }
        return false;
    }

    public final /* synthetic */ Object get(int i) {
        zzan(i);
        return Boolean.valueOf(this.zzadf[i]);
    }

    public final int hashCode() {
        int i = 1;
        for (int i2 = 0; i2 < this.size; i2++) {
            i = (i * 31) + zzez.zzs(this.zzadf[i2]);
        }
        return i;
    }

    public final /* synthetic */ Object remove(int i) {
        zzrz();
        zzan(i);
        boolean z = this.zzadf[i];
        if (i < this.size - 1) {
            System.arraycopy(this.zzadf, i + 1, this.zzadf, i, (this.size - i) - 1);
        }
        this.size--;
        this.modCount++;
        return Boolean.valueOf(z);
    }

    public final boolean remove(Object obj) {
        zzrz();
        for (int i = 0; i < this.size; i++) {
            if (obj.equals(Boolean.valueOf(this.zzadf[i]))) {
                System.arraycopy(this.zzadf, i + 1, this.zzadf, i, (this.size - i) - 1);
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
        System.arraycopy(this.zzadf, i2, this.zzadf, i, this.size - i2);
        this.size -= i2 - i;
        this.modCount++;
    }

    public final /* synthetic */ Object set(int i, Object obj) {
        boolean booleanValue = ((Boolean) obj).booleanValue();
        zzrz();
        zzan(i);
        boolean z = this.zzadf[i];
        this.zzadf[i] = booleanValue;
        return Boolean.valueOf(z);
    }

    public final int size() {
        return this.size;
    }

    public final /* synthetic */ zzff zzap(int i) {
        if (i >= this.size) {
            return new zzdn(Arrays.copyOf(this.zzadf, i), this.size);
        }
        throw new IllegalArgumentException();
    }
}
