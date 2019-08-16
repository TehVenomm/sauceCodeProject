package com.fasterxml.jackson.databind.util;

import java.util.Iterator;
import java.util.NoSuchElementException;

public class ArrayIterator<T> implements Iterator<T>, Iterable<T> {

    /* renamed from: _a */
    private final T[] f429_a;
    private int _index = 0;

    public ArrayIterator(T[] tArr) {
        this.f429_a = tArr;
    }

    public boolean hasNext() {
        return this._index < this.f429_a.length;
    }

    public T next() {
        if (this._index >= this.f429_a.length) {
            throw new NoSuchElementException();
        }
        T[] tArr = this.f429_a;
        int i = this._index;
        this._index = i + 1;
        return tArr[i];
    }

    public void remove() {
        throw new UnsupportedOperationException();
    }

    public Iterator<T> iterator() {
        return this;
    }
}
