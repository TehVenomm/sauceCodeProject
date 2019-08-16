package com.google.android.gms.internal.measurement;

import java.util.ArrayList;
import java.util.List;

final class zzgw<E> extends zzdj<E> {
    private static final zzgw<Object> zzalf;
    private final List<E> zzajs;

    static {
        zzgw<Object> zzgw = new zzgw<>(new ArrayList(0));
        zzalf = zzgw;
        zzgw.zzry();
    }

    zzgw() {
        this(new ArrayList(10));
    }

    private zzgw(List<E> list) {
        this.zzajs = list;
    }

    public static <E> zzgw<E> zzwb() {
        return zzalf;
    }

    public final void add(int i, E e) {
        zzrz();
        this.zzajs.add(i, e);
        this.modCount++;
    }

    public final E get(int i) {
        return this.zzajs.get(i);
    }

    public final E remove(int i) {
        zzrz();
        E remove = this.zzajs.remove(i);
        this.modCount++;
        return remove;
    }

    public final E set(int i, E e) {
        zzrz();
        E e2 = this.zzajs.set(i, e);
        this.modCount++;
        return e2;
    }

    public final int size() {
        return this.zzajs.size();
    }

    public final /* synthetic */ zzff zzap(int i) {
        if (i < size()) {
            throw new IllegalArgumentException();
        }
        ArrayList arrayList = new ArrayList(i);
        arrayList.addAll(this.zzajs);
        return new zzgw(arrayList);
    }
}
