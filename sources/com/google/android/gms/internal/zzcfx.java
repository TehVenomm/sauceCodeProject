package com.google.android.gms.internal;

import java.io.IOException;

public final class zzcfx extends zzegi<zzcfx> {
    private static volatile zzcfx[] zziym;
    public String key;
    public String value;

    public zzcfx() {
        this.key = null;
        this.value = null;
        this.zzncu = null;
        this.zzndd = -1;
    }

    public static zzcfx[] zzbad() {
        if (zziym == null) {
            synchronized (zzegm.zzndc) {
                if (zziym == null) {
                    zziym = new zzcfx[0];
                }
            }
        }
        return zziym;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzcfx)) {
            return false;
        }
        zzcfx zzcfx = (zzcfx) obj;
        if (this.key == null) {
            if (zzcfx.key != null) {
                return false;
            }
        } else if (!this.key.equals(zzcfx.key)) {
            return false;
        }
        if (this.value == null) {
            if (zzcfx.value != null) {
                return false;
            }
        } else if (!this.value.equals(zzcfx.value)) {
            return false;
        }
        return (this.zzncu == null || this.zzncu.isEmpty()) ? zzcfx.zzncu == null || zzcfx.zzncu.isEmpty() : this.zzncu.equals(zzcfx.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = this.key == null ? 0 : this.key.hashCode();
        int hashCode3 = this.value == null ? 0 : this.value.hashCode();
        if (!(this.zzncu == null || this.zzncu.isEmpty())) {
            i = this.zzncu.hashCode();
        }
        return ((((hashCode2 + ((hashCode + 527) * 31)) * 31) + hashCode3) * 31) + i;
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            switch (zzcbr) {
                case 0:
                    break;
                case 10:
                    this.key = zzegf.readString();
                    continue;
                case 18:
                    this.value = zzegf.readString();
                    continue;
                default:
                    if (!super.zza(zzegf, zzcbr)) {
                        break;
                    }
                    continue;
            }
            return this;
        }
    }

    public final void zza(zzegg zzegg) throws IOException {
        if (this.key != null) {
            zzegg.zzl(1, this.key);
        }
        if (this.value != null) {
            zzegg.zzl(2, this.value);
        }
        super.zza(zzegg);
    }

    protected final int zzn() {
        int zzn = super.zzn();
        if (this.key != null) {
            zzn += zzegg.zzm(1, this.key);
        }
        return this.value != null ? zzn + zzegg.zzm(2, this.value) : zzn;
    }
}
