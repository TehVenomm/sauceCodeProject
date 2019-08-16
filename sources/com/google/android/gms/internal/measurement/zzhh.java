package com.google.android.gms.internal.measurement;

import java.util.Map.Entry;

final class zzhh implements Comparable<zzhh>, Entry<K, V> {
    private V value;
    private final /* synthetic */ zzhc zzalq;
    private final K zzalu;

    zzhh(zzhc zzhc, K k, V v) {
        this.zzalq = zzhc;
        this.zzalu = k;
        this.value = v;
    }

    zzhh(zzhc zzhc, Entry<K, V> entry) {
        this(zzhc, (Comparable) entry.getKey(), entry.getValue());
    }

    private static boolean equals(Object obj, Object obj2) {
        return obj == null ? obj2 == null : obj.equals(obj2);
    }

    public final /* synthetic */ int compareTo(Object obj) {
        return ((Comparable) getKey()).compareTo((Comparable) ((zzhh) obj).getKey());
    }

    public final boolean equals(Object obj) {
        if (obj != this) {
            if (!(obj instanceof Entry)) {
                return false;
            }
            Entry entry = (Entry) obj;
            if (!equals(this.zzalu, entry.getKey()) || !equals(this.value, entry.getValue())) {
                return false;
            }
        }
        return true;
    }

    public final /* synthetic */ Object getKey() {
        return this.zzalu;
    }

    public final V getValue() {
        return this.value;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.zzalu == null ? 0 : this.zzalu.hashCode();
        if (this.value != null) {
            i = this.value.hashCode();
        }
        return hashCode ^ i;
    }

    public final V setValue(V v) {
        this.zzalq.zzwk();
        V v2 = this.value;
        this.value = v;
        return v2;
    }

    public final String toString() {
        String valueOf = String.valueOf(this.zzalu);
        String valueOf2 = String.valueOf(this.value);
        return new StringBuilder(String.valueOf(valueOf).length() + 1 + String.valueOf(valueOf2).length()).append(valueOf).append("=").append(valueOf2).toString();
    }
}
