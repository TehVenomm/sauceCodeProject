package com.google.android.gms.internal;

import java.util.AbstractSet;
import java.util.Iterator;
import java.util.Map.Entry;

final class zzefl extends AbstractSet<Entry<K, V>> {
    private /* synthetic */ zzefe zznad;

    private zzefl(zzefe zzefe) {
        this.zznad = zzefe;
    }

    public final /* synthetic */ boolean add(Object obj) {
        Entry entry = (Entry) obj;
        if (contains(entry)) {
            return false;
        }
        this.zznad.zza((Comparable) entry.getKey(), entry.getValue());
        return true;
    }

    public final void clear() {
        this.zznad.clear();
    }

    public final boolean contains(Object obj) {
        Entry entry = (Entry) obj;
        Object obj2 = this.zznad.get(entry.getKey());
        Object value = entry.getValue();
        return obj2 == value || (obj2 != null && obj2.equals(value));
    }

    public final Iterator<Entry<K, V>> iterator() {
        return new zzefk(this.zznad);
    }

    public final boolean remove(Object obj) {
        Entry entry = (Entry) obj;
        if (!contains(entry)) {
            return false;
        }
        this.zznad.remove(entry.getKey());
        return true;
    }

    public final int size() {
        return this.zznad.size();
    }
}
