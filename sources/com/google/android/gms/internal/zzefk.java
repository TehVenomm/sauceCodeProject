package com.google.android.gms.internal;

import java.util.Iterator;
import java.util.Map.Entry;

final class zzefk implements Iterator<Entry<K, V>> {
    private int pos;
    private /* synthetic */ zzefe zznad;
    private boolean zznae;
    private Iterator<Entry<K, V>> zznaf;

    private zzefk(zzefe zzefe) {
        this.zznad = zzefe;
        this.pos = -1;
    }

    private final Iterator<Entry<K, V>> zzcdf() {
        if (this.zznaf == null) {
            this.zznaf = this.zznad.zzmzx.entrySet().iterator();
        }
        return this.zznaf;
    }

    public final boolean hasNext() {
        return this.pos + 1 < this.zznad.zzmzw.size() || zzcdf().hasNext();
    }

    public final /* synthetic */ Object next() {
        this.zznae = true;
        int i = this.pos + 1;
        this.pos = i;
        return i < this.zznad.zzmzw.size() ? (Entry) this.zznad.zzmzw.get(this.pos) : (Entry) zzcdf().next();
    }

    public final void remove() {
        if (this.zznae) {
            this.zznae = false;
            this.zznad.zzcdb();
            if (this.pos < this.zznad.zzmzw.size()) {
                zzefe zzefe = this.zznad;
                int i = this.pos;
                this.pos = i - 1;
                zzefe.zzgx(i);
                return;
            }
            zzcdf().remove();
            return;
        }
        throw new IllegalStateException("remove() was called before next()");
    }
}
