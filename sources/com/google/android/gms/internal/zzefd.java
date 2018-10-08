package com.google.android.gms.internal;

import java.util.ArrayList;
import java.util.List;

final class zzefd<E> extends zzedi<E> {
    private static final zzefd<Object> zzmzt;
    private final List<E> zzmzu;

    static {
        zzedi zzefd = new zzefd();
        zzmzt = zzefd;
        zzefd.zzbhq();
    }

    zzefd() {
        this(new ArrayList(10));
    }

    private zzefd(List<E> list) {
        this.zzmzu = list;
    }

    public static <E> zzefd<E> zzccy() {
        return zzmzt;
    }

    public final void add(int i, E e) {
        zzcbl();
        this.zzmzu.add(i, e);
        this.modCount++;
    }

    public final E get(int i) {
        return this.zzmzu.get(i);
    }

    public final E remove(int i) {
        zzcbl();
        E remove = this.zzmzu.remove(i);
        this.modCount++;
        return remove;
    }

    public final E set(int i, E e) {
        zzcbl();
        E e2 = this.zzmzu.set(i, e);
        this.modCount++;
        return e2;
    }

    public final int size() {
        return this.zzmzu.size();
    }

    public final /* synthetic */ zzeeq zzgu(int i) {
        if (i < size()) {
            throw new IllegalArgumentException();
        }
        List arrayList = new ArrayList(i);
        arrayList.addAll(this.zzmzu);
        return new zzefd(arrayList);
    }
}
