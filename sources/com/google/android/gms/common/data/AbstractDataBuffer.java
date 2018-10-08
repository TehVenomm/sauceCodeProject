package com.google.android.gms.common.data;

import android.os.Bundle;
import java.util.Iterator;

public abstract class AbstractDataBuffer<T> implements DataBuffer<T> {
    protected final DataHolder zzfkz;

    protected AbstractDataBuffer(DataHolder dataHolder) {
        this.zzfkz = dataHolder;
    }

    @Deprecated
    public final void close() {
        release();
    }

    public abstract T get(int i);

    public int getCount() {
        return this.zzfkz == null ? 0 : this.zzfkz.zzfqk;
    }

    @Deprecated
    public boolean isClosed() {
        return this.zzfkz == null || this.zzfkz.isClosed();
    }

    public Iterator<T> iterator() {
        return new zzb(this);
    }

    public void release() {
        if (this.zzfkz != null) {
            this.zzfkz.close();
        }
    }

    public Iterator<T> singleRefIterator() {
        return new zzh(this);
    }

    public final Bundle zzafh() {
        return this.zzfkz.zzafh();
    }
}
