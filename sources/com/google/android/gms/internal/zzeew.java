package com.google.android.gms.internal;

import java.util.Iterator;
import java.util.Map.Entry;

final class zzeew<K> implements Iterator<Entry<K, Object>> {
    private Iterator<Entry<K, Object>> zzlnx;

    public zzeew(Iterator<Entry<K, Object>> it) {
        this.zzlnx = it;
    }

    public final boolean hasNext() {
        return this.zzlnx.hasNext();
    }

    public final /* synthetic */ Object next() {
        Entry entry = (Entry) this.zzlnx.next();
        return entry.getValue() instanceof zzeet ? new zzeev(entry) : entry;
    }

    public final void remove() {
        this.zzlnx.remove();
    }
}
