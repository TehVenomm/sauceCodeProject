package com.google.android.gms.common.data;

import java.util.NoSuchElementException;

public final class zzh<T> extends zzb<T> {
    private T zzfqu;

    public zzh(DataBuffer<T> dataBuffer) {
        super(dataBuffer);
    }

    public final T next() {
        if (hasNext()) {
            this.zzfpz++;
            if (this.zzfpz == 0) {
                this.zzfqu = this.zzfpy.get(0);
                if (!(this.zzfqu instanceof zzc)) {
                    String valueOf = String.valueOf(this.zzfqu.getClass());
                    throw new IllegalStateException(new StringBuilder(String.valueOf(valueOf).length() + 44).append("DataBuffer reference of type ").append(valueOf).append(" is not movable").toString());
                }
            }
            ((zzc) this.zzfqu).zzbu(this.zzfpz);
            return this.zzfqu;
        }
        throw new NoSuchElementException("Cannot advance the iterator beyond " + this.zzfpz);
    }
}
