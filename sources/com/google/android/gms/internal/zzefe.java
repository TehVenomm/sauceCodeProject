package com.google.android.gms.internal;

import java.util.AbstractMap;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;
import java.util.SortedMap;
import java.util.TreeMap;

class zzefe<K extends Comparable<K>, V> extends AbstractMap<K, V> {
    private boolean zzkeu;
    private final int zzmzv;
    private List<zzefj> zzmzw;
    private Map<K, V> zzmzx;
    private volatile zzefl zzmzy;
    private Map<K, V> zzmzz;

    private zzefe(int i) {
        this.zzmzv = i;
        this.zzmzw = Collections.emptyList();
        this.zzmzx = Collections.emptyMap();
        this.zzmzz = Collections.emptyMap();
    }

    private final int zza(K k) {
        int compareTo;
        int i = 0;
        int size = this.zzmzw.size() - 1;
        if (size >= 0) {
            compareTo = k.compareTo((Comparable) ((zzefj) this.zzmzw.get(size)).getKey());
            if (compareTo > 0) {
                return -(size + 2);
            }
            if (compareTo == 0) {
                return size;
            }
        }
        int i2 = size;
        while (i <= i2) {
            size = (i + i2) / 2;
            compareTo = k.compareTo((Comparable) ((zzefj) this.zzmzw.get(size)).getKey());
            if (compareTo < 0) {
                i2 = size - 1;
            } else if (compareTo <= 0) {
                return size;
            } else {
                i = size + 1;
            }
        }
        return -(i + 1);
    }

    private final void zzcdb() {
        if (this.zzkeu) {
            throw new UnsupportedOperationException();
        }
    }

    private final SortedMap<K, V> zzcdc() {
        zzcdb();
        if (this.zzmzx.isEmpty() && !(this.zzmzx instanceof TreeMap)) {
            this.zzmzx = new TreeMap();
            this.zzmzz = ((TreeMap) this.zzmzx).descendingMap();
        }
        return (SortedMap) this.zzmzx;
    }

    static <FieldDescriptorType extends zzeec<FieldDescriptorType>> zzefe<FieldDescriptorType, Object> zzgv(int i) {
        return new zzeff(i);
    }

    private final V zzgx(int i) {
        zzcdb();
        V value = ((zzefj) this.zzmzw.remove(i)).getValue();
        if (!this.zzmzx.isEmpty()) {
            Iterator it = zzcdc().entrySet().iterator();
            this.zzmzw.add(new zzefj(this, (Entry) it.next()));
            it.remove();
        }
        return value;
    }

    public void clear() {
        zzcdb();
        if (!this.zzmzw.isEmpty()) {
            this.zzmzw.clear();
        }
        if (!this.zzmzx.isEmpty()) {
            this.zzmzx.clear();
        }
    }

    public boolean containsKey(Object obj) {
        Comparable comparable = (Comparable) obj;
        return zza(comparable) >= 0 || this.zzmzx.containsKey(comparable);
    }

    public Set<Entry<K, V>> entrySet() {
        if (this.zzmzy == null) {
            this.zzmzy = new zzefl();
        }
        return this.zzmzy;
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof zzefe)) {
                return super.equals(obj);
            }
            zzefe zzefe = (zzefe) obj;
            int size = size();
            if (size != zzefe.size()) {
                return false;
            }
            int zzccz = zzccz();
            if (zzccz != zzefe.zzccz()) {
                return entrySet().equals(zzefe.entrySet());
            }
            for (int i = 0; i < zzccz; i++) {
                if (!zzgw(i).equals(zzefe.zzgw(i))) {
                    return false;
                }
            }
            if (zzccz != size) {
                return this.zzmzx.equals(zzefe.zzmzx);
            }
        }
        return true;
    }

    public V get(Object obj) {
        Comparable comparable = (Comparable) obj;
        int zza = zza(comparable);
        return zza >= 0 ? ((zzefj) this.zzmzw.get(zza)).getValue() : this.zzmzx.get(comparable);
    }

    public int hashCode() {
        int i = 0;
        for (int i2 = 0; i2 < zzccz(); i2++) {
            i += ((zzefj) this.zzmzw.get(i2)).hashCode();
        }
        return this.zzmzx.size() > 0 ? this.zzmzx.hashCode() + i : i;
    }

    public final boolean isImmutable() {
        return this.zzkeu;
    }

    public /* synthetic */ Object put(Object obj, Object obj2) {
        return zza((Comparable) obj, obj2);
    }

    public V remove(Object obj) {
        zzcdb();
        Comparable comparable = (Comparable) obj;
        int zza = zza(comparable);
        return zza >= 0 ? zzgx(zza) : this.zzmzx.isEmpty() ? null : this.zzmzx.remove(comparable);
    }

    public int size() {
        return this.zzmzw.size() + this.zzmzx.size();
    }

    public final V zza(K k, V v) {
        zzcdb();
        int zza = zza((Comparable) k);
        if (zza >= 0) {
            return ((zzefj) this.zzmzw.get(zza)).setValue(v);
        }
        zzcdb();
        if (this.zzmzw.isEmpty() && !(this.zzmzw instanceof ArrayList)) {
            this.zzmzw = new ArrayList(this.zzmzv);
        }
        int i = -(zza + 1);
        if (i >= this.zzmzv) {
            return zzcdc().put(k, v);
        }
        if (this.zzmzw.size() == this.zzmzv) {
            zzefj zzefj = (zzefj) this.zzmzw.remove(this.zzmzv - 1);
            zzcdc().put((Comparable) zzefj.getKey(), zzefj.getValue());
        }
        this.zzmzw.add(i, new zzefj(this, k, v));
        return null;
    }

    public void zzbhq() {
        if (!this.zzkeu) {
            this.zzmzx = this.zzmzx.isEmpty() ? Collections.emptyMap() : Collections.unmodifiableMap(this.zzmzx);
            this.zzmzz = this.zzmzz.isEmpty() ? Collections.emptyMap() : Collections.unmodifiableMap(this.zzmzz);
            this.zzkeu = true;
        }
    }

    public final int zzccz() {
        return this.zzmzw.size();
    }

    public final Iterable<Entry<K, V>> zzcda() {
        return this.zzmzx.isEmpty() ? zzefg.zzcdd() : this.zzmzx.entrySet();
    }

    public final Entry<K, V> zzgw(int i) {
        return (Entry) this.zzmzw.get(i);
    }
}
