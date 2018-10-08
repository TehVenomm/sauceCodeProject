package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;
import java.io.IOException;
import java.util.Arrays;

public final class zzehh extends zzegi<zzehh> implements Cloneable {
    private byte[] zznfu;
    private String zznfv;
    private byte[][] zznfw;
    private boolean zznfx;

    public zzehh() {
        this.zznfu = zzegr.zzndo;
        this.zznfv = "";
        this.zznfw = zzegr.zzndn;
        this.zznfx = false;
        this.zzncu = null;
        this.zzndd = -1;
    }

    private zzehh zzcem() {
        try {
            zzehh zzehh = (zzehh) super.zzcdy();
            if (this.zznfw != null && this.zznfw.length > 0) {
                zzehh.zznfw = (byte[][]) this.zznfw.clone();
            }
            return zzehh;
        } catch (CloneNotSupportedException e) {
            throw new AssertionError(e);
        }
    }

    public final /* synthetic */ Object clone() throws CloneNotSupportedException {
        return zzcem();
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzehh)) {
            return false;
        }
        zzehh zzehh = (zzehh) obj;
        if (!Arrays.equals(this.zznfu, zzehh.zznfu)) {
            return false;
        }
        if (this.zznfv == null) {
            if (zzehh.zznfv != null) {
                return false;
            }
        } else if (!this.zznfv.equals(zzehh.zznfv)) {
            return false;
        }
        return !zzegm.zza(this.zznfw, zzehh.zznfw) ? false : this.zznfx != zzehh.zznfx ? false : (this.zzncu == null || this.zzncu.isEmpty()) ? zzehh.zzncu == null || zzehh.zzncu.isEmpty() : this.zzncu.equals(zzehh.zzncu);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = Arrays.hashCode(this.zznfu);
        int hashCode3 = this.zznfv == null ? 0 : this.zznfv.hashCode();
        int zzd = zzegm.zzd(this.zznfw);
        int i2 = this.zznfx ? 1231 : 1237;
        if (!(this.zzncu == null || this.zzncu.isEmpty())) {
            i = this.zzncu.hashCode();
        }
        return ((((((hashCode3 + ((((hashCode + 527) * 31) + hashCode2) * 31)) * 31) + zzd) * 31) + i2) * 31) + i;
    }

    public final /* synthetic */ zzego zza(zzegf zzegf) throws IOException {
        while (true) {
            int zzcbr = zzegf.zzcbr();
            switch (zzcbr) {
                case 0:
                    break;
                case 10:
                    this.zznfu = zzegf.readBytes();
                    continue;
                case 18:
                    int zzb = zzegr.zzb(zzegf, 18);
                    zzcbr = this.zznfw == null ? 0 : this.zznfw.length;
                    Object obj = new byte[(zzb + zzcbr)][];
                    if (zzcbr != 0) {
                        System.arraycopy(this.zznfw, 0, obj, 0, zzcbr);
                    }
                    while (zzcbr < obj.length - 1) {
                        obj[zzcbr] = zzegf.readBytes();
                        zzegf.zzcbr();
                        zzcbr++;
                    }
                    obj[zzcbr] = zzegf.readBytes();
                    this.zznfw = obj;
                    continue;
                case MotionEventCompat.AXIS_DISTANCE /*24*/:
                    this.zznfx = zzegf.zzcds();
                    continue;
                case MotionEventCompat.AXIS_GENERIC_3 /*34*/:
                    this.zznfv = zzegf.readString();
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
        if (!Arrays.equals(this.zznfu, zzegr.zzndo)) {
            zzegg.zzc(1, this.zznfu);
        }
        if (this.zznfw != null && this.zznfw.length > 0) {
            for (byte[] bArr : this.zznfw) {
                if (bArr != null) {
                    zzegg.zzc(2, bArr);
                }
            }
        }
        if (this.zznfx) {
            zzegg.zzl(3, this.zznfx);
        }
        if (!(this.zznfv == null || this.zznfv.equals(""))) {
            zzegg.zzl(4, this.zznfv);
        }
        super.zza(zzegg);
    }

    public final /* synthetic */ zzegi zzcdy() throws CloneNotSupportedException {
        return (zzehh) clone();
    }

    public final /* synthetic */ zzego zzcdz() throws CloneNotSupportedException {
        return (zzehh) clone();
    }

    protected final int zzn() {
        int i = 0;
        int zzn = super.zzn();
        if (!Arrays.equals(this.zznfu, zzegr.zzndo)) {
            zzn += zzegg.zzd(1, this.zznfu);
        }
        if (this.zznfw != null && this.zznfw.length > 0) {
            int i2 = 0;
            for (byte[] bArr : this.zznfw) {
                if (bArr != null) {
                    i++;
                    i2 += zzegg.zzaw(bArr);
                }
            }
            zzn = (zzn + i2) + (i * 1);
        }
        if (this.zznfx) {
            zzn += zzegg.zzgr(3) + 1;
        }
        return (this.zznfv == null || this.zznfv.equals("")) ? zzn : zzn + zzegg.zzm(4, this.zznfv);
    }
}
