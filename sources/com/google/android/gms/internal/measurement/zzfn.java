package com.google.android.gms.internal.measurement;

public class zzfn {
    private static final zzel zzacw = zzel.zztp();
    private zzdp zzajm;
    private volatile zzgi zzajn;
    private volatile zzdp zzajo;

    private final zzgi zzh(zzgi zzgi) {
        if (this.zzajn == null) {
            synchronized (this) {
                if (this.zzajn == null) {
                    try {
                        this.zzajn = zzgi;
                        this.zzajo = zzdp.zzadh;
                    } catch (zzfi e) {
                        this.zzajn = zzgi;
                        this.zzajo = zzdp.zzadh;
                    }
                }
            }
        }
        return this.zzajn;
    }

    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzfn)) {
            return false;
        }
        zzfn zzfn = (zzfn) obj;
        zzgi zzgi = this.zzajn;
        zzgi zzgi2 = zzfn.zzajn;
        return (zzgi == null && zzgi2 == null) ? zzrs().equals(zzfn.zzrs()) : (zzgi == null || zzgi2 == null) ? zzgi != null ? zzgi.equals(zzfn.zzh(zzgi.zzuh())) : zzh(zzgi2.zzuh()).equals(zzgi2) : zzgi.equals(zzgi2);
    }

    public int hashCode() {
        return 1;
    }

    public final zzgi zzi(zzgi zzgi) {
        zzgi zzgi2 = this.zzajn;
        this.zzajm = null;
        this.zzajo = null;
        this.zzajn = zzgi;
        return zzgi2;
    }

    public final zzdp zzrs() {
        if (this.zzajo != null) {
            return this.zzajo;
        }
        synchronized (this) {
            if (this.zzajo != null) {
                zzdp zzdp = this.zzajo;
                return zzdp;
            }
            if (this.zzajn == null) {
                this.zzajo = zzdp.zzadh;
            } else {
                this.zzajo = this.zzajn.zzrs();
            }
            zzdp zzdp2 = this.zzajo;
            return zzdp2;
        }
    }

    public final int zzuk() {
        if (this.zzajo != null) {
            return this.zzajo.size();
        }
        if (this.zzajn != null) {
            return this.zzajn.zzuk();
        }
        return 0;
    }
}
