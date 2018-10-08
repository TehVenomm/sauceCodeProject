package com.google.android.gms.common.data;

import com.google.android.gms.common.internal.zzbp;
import java.util.Iterator;
import java.util.NoSuchElementException;

public class zzb<T> implements Iterator<T> {
    protected final DataBuffer<T> zzfpy;
    protected int zzfpz = -1;

    public zzb(DataBuffer<T> dataBuffer) {
        this.zzfpy = (DataBuffer) zzbp.zzu(dataBuffer);
    }

    public boolean hasNext() {
        return this.zzfpz < this.zzfpy.getCount() + -1;
    }

    public T next() {
        if (hasNext()) {
            DataBuffer dataBuffer = this.zzfpy;
            int i = this.zzfpz + 1;
            this.zzfpz = i;
            return dataBuffer.get(i);
        }
        throw new NoSuchElementException("Cannot advance the iterator beyond " + this.zzfpz);
    }

    public void remove() {
        throw new UnsupportedOperationException("Cannot remove elements from a DataBufferIterator");
    }
}
