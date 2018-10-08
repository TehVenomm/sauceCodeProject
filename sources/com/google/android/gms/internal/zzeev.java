package com.google.android.gms.internal;

import java.util.Map.Entry;

final class zzeev<K> implements Entry<K, Object> {
    private Entry<K, zzeet> zzmzp;

    private zzeev(Entry<K, zzeet> entry) {
        this.zzmzp = entry;
    }

    public final K getKey() {
        return this.zzmzp.getKey();
    }

    public final Object getValue() {
        return ((zzeet) this.zzmzp.getValue()) == null ? null : zzeet.zzccx();
    }

    public final Object setValue(Object obj) {
        if (obj instanceof zzeey) {
            return ((zzeet) this.zzmzp.getValue()).zzg((zzeey) obj);
        }
        throw new IllegalArgumentException("LazyField now only used for MessageSet, and the value of MessageSet must be an instance of MessageLite");
    }
}
