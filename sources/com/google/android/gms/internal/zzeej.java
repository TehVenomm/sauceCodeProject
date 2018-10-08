package com.google.android.gms.internal;

final class zzeej implements zzeem {
    int hashCode = 0;

    zzeej() {
    }

    public final int zza(boolean z, int i, boolean z2, int i2) {
        this.hashCode = (this.hashCode * 53) + i;
        return i;
    }

    public final zzedk zza(boolean z, zzedk zzedk, boolean z2, zzedk zzedk2) {
        this.hashCode = (this.hashCode * 53) + zzedk.hashCode();
        return zzedk;
    }

    public final <T> zzeeq<T> zza(zzeeq<T> zzeeq, zzeeq<T> zzeeq2) {
        this.hashCode = (this.hashCode * 53) + zzeeq.hashCode();
        return zzeeq;
    }

    public final <T extends zzeey> T zza(T t, T t2) {
        int i;
        if (t == null) {
            i = 37;
        } else if (t instanceof zzeed) {
            Object obj = (zzeed) t;
            if (obj.zzmxn == 0) {
                int i2 = this.hashCode;
                this.hashCode = 0;
                obj.zza(zzeel.zzmzb, (Object) this, obj);
                obj.zzmyr = zza(obj.zzmyr, obj.zzmyr);
                obj.zzmxn = this.hashCode;
                this.hashCode = i2;
            }
            i = obj.zzmxn;
        } else {
            i = t.hashCode();
        }
        this.hashCode = i + (this.hashCode * 53);
        return t;
    }

    public final zzefq zza(zzefq zzefq, zzefq zzefq2) {
        this.hashCode = (this.hashCode * 53) + zzefq.hashCode();
        return zzefq;
    }

    public final String zza(boolean z, String str, boolean z2, String str2) {
        this.hashCode = (this.hashCode * 53) + str.hashCode();
        return str;
    }
}
