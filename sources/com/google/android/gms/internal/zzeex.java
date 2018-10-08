package com.google.android.gms.internal;

public class zzeex {
    private static final zzedz zzmxp = zzedz.zzcci();
    private zzedk zzmzq;
    private volatile zzeey zzmzr;
    private volatile zzedk zzmzs;

    private final zzedk zzcbi() {
        if (this.zzmzs != null) {
            return this.zzmzs;
        }
        synchronized (this) {
            if (this.zzmzs != null) {
                zzedk zzedk = this.zzmzs;
                return zzedk;
            }
            if (this.zzmzr == null) {
                this.zzmzs = zzedk.zzmxr;
            } else {
                this.zzmzs = this.zzmzr.zzcbi();
            }
            zzedk = this.zzmzs;
            return zzedk;
        }
    }

    private zzeey zzf(zzeey zzeey) {
        if (this.zzmzr == null) {
            synchronized (this) {
                if (this.zzmzr != null) {
                } else {
                    try {
                        this.zzmzr = zzeey;
                        this.zzmzs = zzedk.zzmxr;
                    } catch (zzeer e) {
                        this.zzmzr = zzeey;
                        this.zzmzs = zzedk.zzmxr;
                    }
                }
            }
        }
        return this.zzmzr;
    }

    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzeex)) {
            return false;
        }
        zzeex zzeex = (zzeex) obj;
        zzeey zzeey = this.zzmzr;
        zzeey zzeey2 = zzeex.zzmzr;
        return (zzeey == null && zzeey2 == null) ? zzcbi().equals(zzeex.zzcbi()) : (zzeey == null || zzeey2 == null) ? zzeey != null ? zzeey.equals(zzeex.zzf(zzeey.zzcco())) : zzf(zzeey2.zzcco()).equals(zzeey2) : zzeey.equals(zzeey2);
    }

    public int hashCode() {
        return 1;
    }

    public final zzeey zzg(zzeey zzeey) {
        zzeey zzeey2 = this.zzmzr;
        this.zzmzq = null;
        this.zzmzs = null;
        this.zzmzr = zzeey;
        return zzeey2;
    }
}
