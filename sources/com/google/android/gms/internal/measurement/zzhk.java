package com.google.android.gms.internal.measurement;

import java.util.Iterator;
import java.util.Map.Entry;

final class zzhk implements Iterator<Entry<K, V>> {
    private int pos;
    private final /* synthetic */ zzhc zzalq;
    private Iterator<Entry<K, V>> zzalr;
    private boolean zzalv;

    private zzhk(zzhc zzhc) {
        this.zzalq = zzhc;
        this.pos = -1;
    }

    /* synthetic */ zzhk(zzhc zzhc, zzhb zzhb) {
        this(zzhc);
    }

    private final Iterator<Entry<K, V>> zzwm() {
        if (this.zzalr == null) {
            this.zzalr = this.zzalq.zzalm.entrySet().iterator();
        }
        return this.zzalr;
    }

    public final boolean hasNext() {
        return this.pos + 1 < this.zzalq.zzall.size() || (!this.zzalq.zzalm.isEmpty() && zzwm().hasNext());
    }

    public final /* synthetic */ Object next() {
        this.zzalv = true;
        int i = this.pos + 1;
        this.pos = i;
        return i < this.zzalq.zzall.size() ? (Entry) this.zzalq.zzall.get(this.pos) : (Entry) zzwm().next();
    }

    public final void remove() {
        if (!this.zzalv) {
            throw new IllegalStateException("remove() was called before next()");
        }
        this.zzalv = false;
        this.zzalq.zzwk();
        if (this.pos < this.zzalq.zzall.size()) {
            zzhc zzhc = this.zzalq;
            int i = this.pos;
            this.pos = i - 1;
            zzhc.zzcg(i);
            return;
        }
        zzwm().remove();
    }
}
