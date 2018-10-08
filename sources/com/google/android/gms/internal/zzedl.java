package com.google.android.gms.internal;

import java.util.Iterator;
import java.util.NoSuchElementException;

final class zzedl implements Iterator {
    private final int limit = this.zzmxu.size();
    private int position = 0;
    private /* synthetic */ zzedk zzmxu;

    zzedl(zzedk zzedk) {
        this.zzmxu = zzedk;
    }

    private final byte nextByte() {
        try {
            zzedk zzedk = this.zzmxu;
            int i = this.position;
            this.position = i + 1;
            return zzedk.zzgi(i);
        } catch (IndexOutOfBoundsException e) {
            throw new NoSuchElementException(e.getMessage());
        }
    }

    public final boolean hasNext() {
        return this.position < this.limit;
    }

    public final /* synthetic */ Object next() {
        return Byte.valueOf(nextByte());
    }

    public final void remove() {
        throw new UnsupportedOperationException();
    }
}
