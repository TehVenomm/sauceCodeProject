package com.google.android.gms.internal;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.List;

public final class zzegj<M extends zzegi<M>, T> {
    public final int tag;
    private int type;
    protected final Class<T> zzmhh;
    private zzeed<?, ?> zzmyt;
    protected final boolean zzncv;

    private zzegj(int i, Class<T> cls, int i2, boolean z) {
        this(11, cls, null, i2, false);
    }

    private zzegj(int i, Class<T> cls, zzeed<?, ?> zzeed, int i2, boolean z) {
        this.type = i;
        this.zzmhh = cls;
        this.tag = i2;
        this.zzncv = false;
        this.zzmyt = null;
    }

    public static <M extends zzegi<M>, T extends zzego> zzegj<M, T> zza(int i, Class<T> cls, long j) {
        return new zzegj(11, cls, (int) j, false);
    }

    private final Object zzb(zzegf zzegf) {
        String valueOf;
        Class componentType = this.zzncv ? this.zzmhh.getComponentType() : this.zzmhh;
        try {
            zzego zzego;
            switch (this.type) {
                case 10:
                    zzego = (zzego) componentType.newInstance();
                    zzegf.zza(zzego, this.tag >>> 3);
                    return zzego;
                case 11:
                    zzego = (zzego) componentType.newInstance();
                    zzegf.zza(zzego);
                    return zzego;
                default:
                    throw new IllegalArgumentException("Unknown type " + this.type);
            }
        } catch (Throwable e) {
            valueOf = String.valueOf(componentType);
            throw new IllegalArgumentException(new StringBuilder(String.valueOf(valueOf).length() + 33).append("Error creating instance of class ").append(valueOf).toString(), e);
        } catch (Throwable e2) {
            valueOf = String.valueOf(componentType);
            throw new IllegalArgumentException(new StringBuilder(String.valueOf(valueOf).length() + 33).append("Error creating instance of class ").append(valueOf).toString(), e2);
        } catch (Throwable e22) {
            throw new IllegalArgumentException("Error reading extension field", e22);
        }
    }

    public final boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof zzegj)) {
                return false;
            }
            zzegj zzegj = (zzegj) obj;
            if (this.type != zzegj.type || this.zzmhh != zzegj.zzmhh || this.tag != zzegj.tag) {
                return false;
            }
            if (this.zzncv != zzegj.zzncv) {
                return false;
            }
        }
        return true;
    }

    public final int hashCode() {
        int i = this.type;
        int hashCode = this.zzmhh.hashCode();
        return (this.zzncv ? 1 : 0) + ((((((i + 1147) * 31) + hashCode) * 31) + this.tag) * 31);
    }

    protected final void zza(Object obj, zzegg zzegg) {
        try {
            zzegg.zzhc(this.tag);
            switch (this.type) {
                case 10:
                    int i = this.tag;
                    ((zzego) obj).zza(zzegg);
                    zzegg.zzt(i >>> 3, 4);
                    return;
                case 11:
                    zzegg.zzb((zzego) obj);
                    return;
                default:
                    throw new IllegalArgumentException("Unknown type " + this.type);
            }
        } catch (Throwable e) {
            throw new IllegalStateException(e);
        }
        throw new IllegalStateException(e);
    }

    final T zzav(List<zzegq> list) {
        int i = 0;
        if (list == null) {
            return null;
        }
        if (this.zzncv) {
            int i2;
            List arrayList = new ArrayList();
            for (i2 = 0; i2 < list.size(); i2++) {
                zzegq zzegq = (zzegq) list.get(i2);
                if (zzegq.zzjao.length != 0) {
                    arrayList.add(zzb(zzegf.zzau(zzegq.zzjao)));
                }
            }
            i2 = arrayList.size();
            if (i2 == 0) {
                return null;
            }
            T cast = this.zzmhh.cast(Array.newInstance(this.zzmhh.getComponentType(), i2));
            while (i < i2) {
                Array.set(cast, i, arrayList.get(i));
                i++;
            }
            return cast;
        } else if (list.isEmpty()) {
            return null;
        } else {
            return this.zzmhh.cast(zzb(zzegf.zzau(((zzegq) list.get(list.size() - 1)).zzjao)));
        }
    }

    protected final int zzbx(Object obj) {
        int i = this.tag >>> 3;
        switch (this.type) {
            case 10:
                return (zzegg.zzgr(i) << 1) + ((zzego) obj).zzbjo();
            case 11:
                return zzegg.zzb(i, (zzego) obj);
            default:
                throw new IllegalArgumentException("Unknown type " + this.type);
        }
    }
}
