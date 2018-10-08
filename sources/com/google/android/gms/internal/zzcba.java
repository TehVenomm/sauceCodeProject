package com.google.android.gms.internal;

import java.util.Iterator;

final class zzcba implements Iterator<String> {
    private Iterator<String> zzinh = this.zzini.zzing.keySet().iterator();
    private /* synthetic */ zzcaz zzini;

    zzcba(zzcaz zzcaz) {
        this.zzini = zzcaz;
    }

    public final boolean hasNext() {
        return this.zzinh.hasNext();
    }

    public final /* synthetic */ Object next() {
        return (String) this.zzinh.next();
    }

    public final void remove() {
        throw new UnsupportedOperationException("Remove not supported");
    }
}
