package com.google.android.gms.internal;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;

public abstract class zzeed<MessageType extends zzeed<MessageType, BuilderType>, BuilderType extends zzeee<MessageType, BuilderType>> extends zzedf<MessageType, BuilderType> {
    protected zzefq zzmyr = zzefq.zzcdh();
    protected int zzmys = -1;

    protected static <T extends zzeed<T, ?>> T zza(T t, zzedk zzedk) throws zzeer {
        Object obj = 1;
        T zza = zza((zzeed) t, zzedk, zzedz.zzcci());
        if (zza != null) {
            if ((zza.zza(zzeel.zzmza, Boolean.TRUE, null) != null ? 1 : null) == null) {
                throw new zzefp(zza).zzcdg().zze(zza);
            }
        }
        if (zza != null) {
            if (zza.zza(zzeel.zzmza, Boolean.TRUE, null) == null) {
                obj = null;
            }
            if (obj == null) {
                throw new zzefp(zza).zzcdg().zze(zza);
            }
        }
        return zza;
    }

    private static <T extends zzeed<T, ?>> T zza(T t, zzedk zzedk, zzedz zzedz) throws zzeer {
        T zza;
        try {
            zzedt zzcbm = zzedk.zzcbm();
            zza = zza((zzeed) t, zzcbm, zzedz);
            zzcbm.zzgk(0);
            return zza;
        } catch (zzeer e) {
            throw e.zze(zza);
        } catch (zzeer e2) {
            throw e2;
        }
    }

    static <T extends zzeed<T, ?>> T zza(T t, zzedt zzedt, zzedz zzedz) throws zzeer {
        zzeed zzeed = (zzeed) t.zza(zzeel.zzmze, null, null);
        try {
            zzeed.zza(zzeel.zzmzc, (Object) zzedt, (Object) zzedz);
            zzeed.zza(zzeel.zzmzd, null, null);
            zzeed.zzmyr.zzbhq();
            return zzeed;
        } catch (RuntimeException e) {
            if (e.getCause() instanceof zzeer) {
                throw ((zzeer) e.getCause());
            }
            throw e;
        }
    }

    protected static <T extends zzeed<T, ?>> T zza(T t, byte[] bArr) throws zzeer {
        T zza = zza((zzeed) t, bArr, zzedz.zzcci());
        if (zza != null) {
            if ((zza.zza(zzeel.zzmza, Boolean.TRUE, null) != null ? 1 : null) == null) {
                throw new zzefp(zza).zzcdg().zze(zza);
            }
        }
        return zza;
    }

    private static <T extends zzeed<T, ?>> T zza(T t, byte[] bArr, zzedz zzedz) throws zzeer {
        T zza;
        try {
            zzedt zzas = zzedt.zzas(bArr);
            zza = zza((zzeed) t, zzas, zzedz);
            zzas.zzgk(0);
            return zza;
        } catch (zzeer e) {
            throw e.zze(zza);
        } catch (zzeer e2) {
            throw e2;
        }
    }

    static Object zza(Method method, Object obj, Object... objArr) {
        Throwable e;
        try {
            return method.invoke(obj, objArr);
        } catch (Throwable e2) {
            throw new RuntimeException("Couldn't use Java reflection to implement protocol message reflection.", e2);
        } catch (InvocationTargetException e3) {
            e2 = e3.getCause();
            if (e2 instanceof RuntimeException) {
                throw ((RuntimeException) e2);
            } else if (e2 instanceof Error) {
                throw ((Error) e2);
            } else {
                throw new RuntimeException("Unexpected exception thrown by generated accessor method.", e2);
            }
        }
    }

    protected static <E> zzeeq<E> zzccm() {
        return zzefd.zzccy();
    }

    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!((zzeed) zza(zzeel.zzmzg, null, null)).getClass().isInstance(obj)) {
            return false;
        }
        try {
            Object obj2 = zzeeg.zzmyw;
            obj = (zzeed) obj;
            zza(zzeel.zzmzb, obj2, obj);
            this.zzmyr = obj2.zza(this.zzmyr, obj.zzmyr);
            return true;
        } catch (zzeeh e) {
            return false;
        }
    }

    public int hashCode() {
        if (this.zzmxn != 0) {
            return this.zzmxn;
        }
        Object zzeej = new zzeej();
        zza(zzeel.zzmzb, zzeej, (Object) this);
        this.zzmyr = zzeej.zza(this.zzmyr, this.zzmyr);
        this.zzmxn = zzeej.hashCode;
        return this.zzmxn;
    }

    public String toString() {
        return zzefb.zza(this, super.toString());
    }

    protected abstract Object zza(int i, Object obj, Object obj2);

    public final /* synthetic */ zzeez zzccn() {
        zzeee zzeee = (zzeee) zza(zzeel.zzmzf, null, null);
        zzeee.zza(this);
        return zzeee;
    }

    public final /* synthetic */ zzeey zzcco() {
        return (zzeed) zza(zzeel.zzmzg, null, null);
    }
}
