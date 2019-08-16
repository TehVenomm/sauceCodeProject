package com.google.android.gms.internal.measurement;

import java.util.Arrays;
import java.util.Collection;
import java.util.RandomAccess;

final class zzfa extends zzdj<Integer> implements zzfd, zzgu, RandomAccess {
    private static final zzfa zzaiu;
    private int size;
    private int[] zzaiv;

    static {
        zzfa zzfa = new zzfa(new int[0], 0);
        zzaiu = zzfa;
        zzfa.zzry();
    }

    zzfa() {
        this(new int[10], 0);
    }

    private zzfa(int[] iArr, int i) {
        this.zzaiv = iArr;
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

    private final void zzo(int i, int i2) {
        zzrz();
        if (i < 0 || i > this.size) {
            throw new IndexOutOfBoundsException(zzao(i));
        }
        if (this.size < this.zzaiv.length) {
            System.arraycopy(this.zzaiv, i, this.zzaiv, i + 1, this.size - i);
        } else {
            int[] iArr = new int[(((this.size * 3) / 2) + 1)];
            System.arraycopy(this.zzaiv, 0, iArr, 0, i);
            System.arraycopy(this.zzaiv, i, iArr, i + 1, this.size - i);
            this.zzaiv = iArr;
        }
        this.zzaiv[i] = i2;
        this.size++;
        this.modCount++;
    }

    public static zzfa zzus() {
        return zzaiu;
    }

    public final /* synthetic */ void add(int i, Object obj) {
        zzo(i, ((Integer) obj).intValue());
    }

    public final boolean addAll(Collection<? extends Integer> collection) {
        zzrz();
        zzez.checkNotNull(collection);
        if (!(collection instanceof zzfa)) {
            return super.addAll(collection);
        }
        zzfa zzfa = (zzfa) collection;
        if (zzfa.size == 0) {
            return false;
        }
        if (Integer.MAX_VALUE - this.size < zzfa.size) {
            throw new OutOfMemoryError();
        }
        int i = this.size + zzfa.size;
        if (i > this.zzaiv.length) {
            this.zzaiv = Arrays.copyOf(this.zzaiv, i);
        }
        System.arraycopy(zzfa.zzaiv, 0, this.zzaiv, this.size, zzfa.size);
        this.size = i;
        this.modCount++;
        return true;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzfa)) {
            return super.equals(obj);
        }
        zzfa zzfa = (zzfa) obj;
        if (this.size == zzfa.size) {
            int[] iArr = zzfa.zzaiv;
            int i = 0;
            while (i < this.size) {
                if (this.zzaiv[i] == iArr[i]) {
                    i++;
                }
            }
            return true;
        }
        return false;
    }

    public final /* synthetic */ Object get(int i) {
        return Integer.valueOf(getInt(i));
    }

    public final int getInt(int i) {
        zzan(i);
        return this.zzaiv[i];
    }

    public final int hashCode() {
        int i = 1;
        for (int i2 = 0; i2 < this.size; i2++) {
            i = (i * 31) + this.zzaiv[i2];
        }
        return i;
    }

    public final /* synthetic */ Object remove(int i) {
        zzrz();
        zzan(i);
        int i2 = this.zzaiv[i];
        if (i < this.size - 1) {
            System.arraycopy(this.zzaiv, i + 1, this.zzaiv, i, (this.size - i) - 1);
        }
        this.size--;
        this.modCount++;
        return Integer.valueOf(i2);
    }

    public final boolean remove(Object obj) {
        zzrz();
        for (int i = 0; i < this.size; i++) {
            if (obj.equals(Integer.valueOf(this.zzaiv[i]))) {
                System.arraycopy(this.zzaiv, i + 1, this.zzaiv, i, (this.size - i) - 1);
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
        System.arraycopy(this.zzaiv, i2, this.zzaiv, i, this.size - i2);
        this.size -= i2 - i;
        this.modCount++;
    }

    public final /* synthetic */ Object set(int i, Object obj) {
        int intValue = ((Integer) obj).intValue();
        zzrz();
        zzan(i);
        int i2 = this.zzaiv[i];
        this.zzaiv[i] = intValue;
        return Integer.valueOf(i2);
    }

    public final int size() {
        return this.size;
    }

    /* renamed from: zzbt */
    public final zzfd zzap(int i) {
        if (i >= this.size) {
            return new zzfa(Arrays.copyOf(this.zzaiv, i), this.size);
        }
        throw new IllegalArgumentException();
    }

    public final void zzbu(int i) {
        zzo(this.size, i);
    }
}
