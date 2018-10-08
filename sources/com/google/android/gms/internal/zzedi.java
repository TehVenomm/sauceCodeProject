package com.google.android.gms.internal;

import java.util.AbstractList;
import java.util.Collection;
import java.util.List;
import java.util.RandomAccess;

abstract class zzedi<E> extends AbstractList<E> implements zzeeq<E> {
    private boolean zzmxq = true;

    zzedi() {
    }

    public void add(int i, E e) {
        zzcbl();
        super.add(i, e);
    }

    public boolean add(E e) {
        zzcbl();
        return super.add(e);
    }

    public boolean addAll(int i, Collection<? extends E> collection) {
        zzcbl();
        return super.addAll(i, collection);
    }

    public boolean addAll(Collection<? extends E> collection) {
        zzcbl();
        return super.addAll(collection);
    }

    public void clear() {
        zzcbl();
        super.clear();
    }

    public boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof List)) {
            return false;
        }
        if (!(obj instanceof RandomAccess)) {
            return super.equals(obj);
        }
        List list = (List) obj;
        int size = size();
        if (size != list.size()) {
            return false;
        }
        for (int i = 0; i < size; i++) {
            if (!get(i).equals(list.get(i))) {
                return false;
            }
        }
        return true;
    }

    public int hashCode() {
        int i = 1;
        for (int i2 = 0; i2 < size(); i2++) {
            i = (i * 31) + get(i2).hashCode();
        }
        return i;
    }

    public E remove(int i) {
        zzcbl();
        return super.remove(i);
    }

    public boolean remove(Object obj) {
        zzcbl();
        return super.remove(obj);
    }

    public boolean removeAll(Collection<?> collection) {
        zzcbl();
        return super.removeAll(collection);
    }

    public boolean retainAll(Collection<?> collection) {
        zzcbl();
        return super.retainAll(collection);
    }

    public E set(int i, E e) {
        zzcbl();
        return super.set(i, e);
    }

    public final void zzbhq() {
        this.zzmxq = false;
    }

    public final boolean zzcbk() {
        return this.zzmxq;
    }

    protected final void zzcbl() {
        if (!this.zzmxq) {
            throw new UnsupportedOperationException();
        }
    }
}
