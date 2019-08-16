package com.google.android.gms.common.data;

import com.google.android.gms.common.annotation.KeepForSdk;
import java.util.NoSuchElementException;

@KeepForSdk
public class SingleRefDataBufferIterator<T> extends DataBufferIterator<T> {
    private T zamf;

    public SingleRefDataBufferIterator(DataBuffer<T> dataBuffer) {
        super(dataBuffer);
    }

    public T next() {
        if (!hasNext()) {
            throw new NoSuchElementException("Cannot advance the iterator beyond " + this.zalk);
        }
        this.zalk++;
        if (this.zalk == 0) {
            this.zamf = this.zalj.get(0);
            if (!(this.zamf instanceof DataBufferRef)) {
                String valueOf = String.valueOf(this.zamf.getClass());
                throw new IllegalStateException(new StringBuilder(String.valueOf(valueOf).length() + 44).append("DataBuffer reference of type ").append(valueOf).append(" is not movable").toString());
            }
        } else {
            ((DataBufferRef) this.zamf).zag(this.zalk);
        }
        return this.zamf;
    }
}
