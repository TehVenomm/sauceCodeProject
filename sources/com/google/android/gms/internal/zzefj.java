package com.google.android.gms.internal;

import java.util.Map.Entry;

final class zzefj implements Comparable<zzefj>, Entry<K, V> {
    private V value;
    private final K zznac;
    private /* synthetic */ zzefe zznad;

    zzefj(zzefe zzefe, K k, V v) {
        this.zznad = zzefe;
        this.zznac = k;
        this.value = v;
    }

    zzefj(zzefe zzefe, Entry<K, V> entry) {
        this(zzefe, (Comparable) entry.getKey(), entry.getValue());
    }

    private static boolean equals(Object obj, Object obj2) {
        return obj == null ? obj2 == null : obj.equals(obj2);
    }

    public final /* synthetic */ int compareTo(Object obj) {
        return ((Comparable) getKey()).compareTo((Comparable) ((zzefj) obj).getKey());
    }

    public final boolean equals(Object obj) {
        if (obj != this) {
            if (!(obj instanceof Entry)) {
                return false;
            }
            Entry entry = (Entry) obj;
            if (!equals(this.zznac, entry.getKey())) {
                return false;
            }
            if (!equals(this.value, entry.getValue())) {
                return false;
            }
        }
        return true;
    }

    public final /* synthetic */ Object getKey() {
        return this.zznac;
    }

    public final V getValue() {
        return this.value;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.zznac == null ? 0 : this.zznac.hashCode();
        if (this.value != null) {
            i = this.value.hashCode();
        }
        return hashCode ^ i;
    }

    public final V setValue(V v) {
        this.zznad.zzcdb();
        V v2 = this.value;
        this.value = v;
        return v2;
    }

    public final String toString() {
        String valueOf = String.valueOf(this.zznac);
        String valueOf2 = String.valueOf(this.value);
        return new StringBuilder((String.valueOf(valueOf).length() + 1) + String.valueOf(valueOf2).length()).append(valueOf).append("=").append(valueOf2).toString();
    }
}
