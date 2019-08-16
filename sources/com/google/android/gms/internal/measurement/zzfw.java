package com.google.android.gms.internal.measurement;

import java.util.Arrays;
import java.util.Collection;
import java.util.RandomAccess;

final class zzfw extends zzdj<Long> implements zzfg, zzgu, RandomAccess {
    private static final zzfw zzajy;
    private int size;
    private long[] zzajz;

    static {
        zzfw zzfw = new zzfw(new long[0], 0);
        zzajy = zzfw;
        zzfw.zzry();
    }

    zzfw() {
        this(new long[10], 0);
    }

    private zzfw(long[] jArr, int i) {
        this.zzajz = jArr;
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

    private final void zzk(int i, long j) {
        zzrz();
        if (i < 0 || i > this.size) {
            throw new IndexOutOfBoundsException(zzao(i));
        }
        if (this.size < this.zzajz.length) {
            System.arraycopy(this.zzajz, i, this.zzajz, i + 1, this.size - i);
        } else {
            long[] jArr = new long[(((this.size * 3) / 2) + 1)];
            System.arraycopy(this.zzajz, 0, jArr, 0, i);
            System.arraycopy(this.zzajz, i, jArr, i + 1, this.size - i);
            this.zzajz = jArr;
        }
        this.zzajz[i] = j;
        this.size++;
        this.modCount++;
    }

    public static zzfw zzvk() {
        return zzajy;
    }

    public final /* synthetic */ void add(int i, Object obj) {
        zzk(i, ((Long) obj).longValue());
    }

    public final boolean addAll(Collection<? extends Long> collection) {
        zzrz();
        zzez.checkNotNull(collection);
        if (!(collection instanceof zzfw)) {
            return super.addAll(collection);
        }
        zzfw zzfw = (zzfw) collection;
        if (zzfw.size == 0) {
            return false;
        }
        if (Integer.MAX_VALUE - this.size < zzfw.size) {
            throw new OutOfMemoryError();
        }
        int i = this.size + zzfw.size;
        if (i > this.zzajz.length) {
            this.zzajz = Arrays.copyOf(this.zzajz, i);
        }
        System.arraycopy(zzfw.zzajz, 0, this.zzajz, this.size, zzfw.size);
        this.size = i;
        this.modCount++;
        return true;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzfw)) {
            return super.equals(obj);
        }
        zzfw zzfw = (zzfw) obj;
        if (this.size == zzfw.size) {
            long[] jArr = zzfw.zzajz;
            int i = 0;
            while (i < this.size) {
                if (this.zzajz[i] == jArr[i]) {
                    i++;
                }
            }
            return true;
        }
        return false;
    }

    public final /* synthetic */ Object get(int i) {
        return Long.valueOf(getLong(i));
    }

    public final long getLong(int i) {
        zzan(i);
        return this.zzajz[i];
    }

    public final int hashCode() {
        int i = 1;
        for (int i2 = 0; i2 < this.size; i2++) {
            i = (i * 31) + zzez.zzbx(this.zzajz[i2]);
        }
        return i;
    }

    public final /* synthetic */ Object remove(int i) {
        zzrz();
        zzan(i);
        long j = this.zzajz[i];
        if (i < this.size - 1) {
            System.arraycopy(this.zzajz, i + 1, this.zzajz, i, (this.size - i) - 1);
        }
        this.size--;
        this.modCount++;
        return Long.valueOf(j);
    }

    public final boolean remove(Object obj) {
        zzrz();
        for (int i = 0; i < this.size; i++) {
            if (obj.equals(Long.valueOf(this.zzajz[i]))) {
                System.arraycopy(this.zzajz, i + 1, this.zzajz, i, (this.size - i) - 1);
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
        System.arraycopy(this.zzajz, i2, this.zzajz, i, this.size - i2);
        this.size -= i2 - i;
        this.modCount++;
    }

    public final /* synthetic */ Object set(int i, Object obj) {
        long longValue = ((Long) obj).longValue();
        zzrz();
        zzan(i);
        long j = this.zzajz[i];
        this.zzajz[i] = longValue;
        return Long.valueOf(j);
    }

    public final int size() {
        return this.size;
    }

    /* renamed from: zzbv */
    public final zzfg zzap(int i) {
        if (i >= this.size) {
            return new zzfw(Arrays.copyOf(this.zzajz, i), this.size);
        }
        throw new IllegalArgumentException();
    }

    public final void zzby(long j) {
        zzk(this.size, j);
    }
}
