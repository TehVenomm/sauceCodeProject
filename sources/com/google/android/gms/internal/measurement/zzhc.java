package com.google.android.gms.internal.measurement;

import java.lang.Comparable;
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

class zzhc<K extends Comparable<K>, V> extends AbstractMap<K, V> {
    private boolean zzaey;
    private final int zzalk;
    /* access modifiers changed from: private */
    public List<zzhh> zzall;
    /* access modifiers changed from: private */
    public Map<K, V> zzalm;
    private volatile zzhj zzaln;
    /* access modifiers changed from: private */
    public Map<K, V> zzalo;
    private volatile zzhd zzalp;

    private zzhc(int i) {
        this.zzalk = i;
        this.zzall = Collections.emptyList();
        this.zzalm = Collections.emptyMap();
        this.zzalo = Collections.emptyMap();
    }

    /* synthetic */ zzhc(int i, zzhb zzhb) {
        this(i);
    }

    private final int zza(K k) {
        int size = this.zzall.size() - 1;
        if (size >= 0) {
            int compareTo = k.compareTo((Comparable) ((zzhh) this.zzall.get(size)).getKey());
            if (compareTo > 0) {
                return -(size + 2);
            }
            if (compareTo == 0) {
                return size;
            }
        }
        int i = 0;
        int i2 = size;
        while (i <= i2) {
            int i3 = (i + i2) / 2;
            int compareTo2 = k.compareTo((Comparable) ((zzhh) this.zzall.get(i3)).getKey());
            if (compareTo2 < 0) {
                i2 = i3 - 1;
            } else if (compareTo2 <= 0) {
                return i3;
            } else {
                i = i3 + 1;
            }
        }
        return -(i + 1);
    }

    static <FieldDescriptorType extends zzeq<FieldDescriptorType>> zzhc<FieldDescriptorType, Object> zzce(int i) {
        return new zzhb(i);
    }

    /* access modifiers changed from: private */
    public final V zzcg(int i) {
        zzwk();
        V value = ((zzhh) this.zzall.remove(i)).getValue();
        if (!this.zzalm.isEmpty()) {
            Iterator it = zzwl().entrySet().iterator();
            this.zzall.add(new zzhh(this, (Entry) it.next()));
            it.remove();
        }
        return value;
    }

    /* access modifiers changed from: private */
    public final void zzwk() {
        if (this.zzaey) {
            throw new UnsupportedOperationException();
        }
    }

    private final SortedMap<K, V> zzwl() {
        zzwk();
        if (this.zzalm.isEmpty() && !(this.zzalm instanceof TreeMap)) {
            this.zzalm = new TreeMap();
            this.zzalo = ((TreeMap) this.zzalm).descendingMap();
        }
        return (SortedMap) this.zzalm;
    }

    public void clear() {
        zzwk();
        if (!this.zzall.isEmpty()) {
            this.zzall.clear();
        }
        if (!this.zzalm.isEmpty()) {
            this.zzalm.clear();
        }
    }

    public boolean containsKey(Object obj) {
        Comparable comparable = (Comparable) obj;
        return zza((K) comparable) >= 0 || this.zzalm.containsKey(comparable);
    }

    public Set<Entry<K, V>> entrySet() {
        if (this.zzaln == null) {
            this.zzaln = new zzhj(this, null);
        }
        return this.zzaln;
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof zzhc)) {
                return super.equals(obj);
            }
            zzhc zzhc = (zzhc) obj;
            int size = size();
            if (size != zzhc.size()) {
                return false;
            }
            int zzwh = zzwh();
            if (zzwh != zzhc.zzwh()) {
                return entrySet().equals(zzhc.entrySet());
            }
            for (int i = 0; i < zzwh; i++) {
                if (!zzcf(i).equals(zzhc.zzcf(i))) {
                    return false;
                }
            }
            if (zzwh != size) {
                return this.zzalm.equals(zzhc.zzalm);
            }
        }
        return true;
    }

    public V get(Object obj) {
        Comparable comparable = (Comparable) obj;
        int zza = zza((K) comparable);
        return zza >= 0 ? ((zzhh) this.zzall.get(zza)).getValue() : this.zzalm.get(comparable);
    }

    public int hashCode() {
        int i = 0;
        for (int i2 = 0; i2 < zzwh(); i2++) {
            i += ((zzhh) this.zzall.get(i2)).hashCode();
        }
        return this.zzalm.size() > 0 ? this.zzalm.hashCode() + i : i;
    }

    public final boolean isImmutable() {
        return this.zzaey;
    }

    public V remove(Object obj) {
        zzwk();
        Comparable comparable = (Comparable) obj;
        int zza = zza((K) comparable);
        if (zza >= 0) {
            return zzcg(zza);
        }
        if (this.zzalm.isEmpty()) {
            return null;
        }
        return this.zzalm.remove(comparable);
    }

    public int size() {
        return this.zzall.size() + this.zzalm.size();
    }

    /* renamed from: zza */
    public final V put(K k, V v) {
        zzwk();
        int zza = zza(k);
        if (zza >= 0) {
            return ((zzhh) this.zzall.get(zza)).setValue(v);
        }
        zzwk();
        if (this.zzall.isEmpty() && !(this.zzall instanceof ArrayList)) {
            this.zzall = new ArrayList(this.zzalk);
        }
        int i = -(zza + 1);
        if (i >= this.zzalk) {
            return zzwl().put(k, v);
        }
        if (this.zzall.size() == this.zzalk) {
            zzhh zzhh = (zzhh) this.zzall.remove(this.zzalk - 1);
            zzwl().put((Comparable) zzhh.getKey(), zzhh.getValue());
        }
        this.zzall.add(i, new zzhh(this, k, v));
        return null;
    }

    public final Entry<K, V> zzcf(int i) {
        return (Entry) this.zzall.get(i);
    }

    public void zzry() {
        if (!this.zzaey) {
            this.zzalm = this.zzalm.isEmpty() ? Collections.emptyMap() : Collections.unmodifiableMap(this.zzalm);
            this.zzalo = this.zzalo.isEmpty() ? Collections.emptyMap() : Collections.unmodifiableMap(this.zzalo);
            this.zzaey = true;
        }
    }

    public final int zzwh() {
        return this.zzall.size();
    }

    public final Iterable<Entry<K, V>> zzwi() {
        return this.zzalm.isEmpty() ? zzhg.zzwn() : this.zzalm.entrySet();
    }

    /* access modifiers changed from: 0000 */
    public final Set<Entry<K, V>> zzwj() {
        if (this.zzalp == null) {
            this.zzalp = new zzhd(this, null);
        }
        return this.zzalp;
    }
}
