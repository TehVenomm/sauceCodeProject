package com.google.android.gms.internal.measurement;

import com.google.android.gms.internal.measurement.zzbq.zza;
import java.io.IOException;

public final class zzbw extends zziq<zzbw> {
    public String zzcg;
    public Long zzzk;
    private Integer zzzl;
    public zza[] zzzm;
    public zzbx[] zzzn;
    public zzbv[] zzzo;
    private String zzzp;
    public Boolean zzzq;

    public zzbw() {
        this.zzzk = null;
        this.zzcg = null;
        this.zzzl = null;
        this.zzzm = new zza[0];
        this.zzzn = zzbx.zzrc();
        this.zzzo = zzbv.zzqx();
        this.zzzp = null;
        this.zzzq = null;
        this.zzaoo = null;
        this.zzaow = -1;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzbw)) {
            return false;
        }
        zzbw zzbw = (zzbw) obj;
        if (this.zzzk == null) {
            if (zzbw.zzzk != null) {
                return false;
            }
        } else if (!this.zzzk.equals(zzbw.zzzk)) {
            return false;
        }
        if (this.zzcg == null) {
            if (zzbw.zzcg != null) {
                return false;
            }
        } else if (!this.zzcg.equals(zzbw.zzcg)) {
            return false;
        }
        if (this.zzzl == null) {
            if (zzbw.zzzl != null) {
                return false;
            }
        } else if (!this.zzzl.equals(zzbw.zzzl)) {
            return false;
        }
        if (!zziu.equals(this.zzzm, zzbw.zzzm)) {
            return false;
        }
        if (!zziu.equals(this.zzzn, zzbw.zzzn)) {
            return false;
        }
        if (!zziu.equals(this.zzzo, zzbw.zzzo)) {
            return false;
        }
        if (this.zzzp == null) {
            if (zzbw.zzzp != null) {
                return false;
            }
        } else if (!this.zzzp.equals(zzbw.zzzp)) {
            return false;
        }
        if (this.zzzq == null) {
            if (zzbw.zzzq != null) {
                return false;
            }
        } else if (!this.zzzq.equals(zzbw.zzzq)) {
            return false;
        }
        return (this.zzaoo == null || this.zzaoo.isEmpty()) ? zzbw.zzaoo == null || zzbw.zzaoo.isEmpty() : this.zzaoo.equals(zzbw.zzaoo);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = this.zzzk == null ? 0 : this.zzzk.hashCode();
        int hashCode3 = this.zzcg == null ? 0 : this.zzcg.hashCode();
        int hashCode4 = this.zzzl == null ? 0 : this.zzzl.hashCode();
        int hashCode5 = zziu.hashCode(this.zzzm);
        int hashCode6 = zziu.hashCode(this.zzzn);
        int hashCode7 = zziu.hashCode(this.zzzo);
        int hashCode8 = this.zzzp == null ? 0 : this.zzzp.hashCode();
        int hashCode9 = this.zzzq == null ? 0 : this.zzzq.hashCode();
        if (this.zzaoo != null && !this.zzaoo.isEmpty()) {
            i = this.zzaoo.hashCode();
        }
        return ((((((((((((((((hashCode2 + ((hashCode + 527) * 31)) * 31) + hashCode3) * 31) + hashCode4) * 31) + hashCode5) * 31) + hashCode6) * 31) + hashCode7) * 31) + hashCode8) * 31) + hashCode9) * 31) + i;
    }

    public final /* synthetic */ zziw zza(zzil zzil) throws IOException {
        while (true) {
            int zzsg = zzil.zzsg();
            switch (zzsg) {
                case 0:
                    break;
                case 8:
                    this.zzzk = Long.valueOf(zzil.zztb());
                    continue;
                case 18:
                    this.zzcg = zzil.readString();
                    continue;
                case 24:
                    this.zzzl = Integer.valueOf(zzil.zzta());
                    continue;
                case 34:
                    int zzb = zzix.zzb(zzil, 34);
                    int length = this.zzzm == null ? 0 : this.zzzm.length;
                    zza[] zzaArr = new zza[(zzb + length)];
                    if (length != 0) {
                        System.arraycopy(this.zzzm, 0, zzaArr, 0, length);
                    }
                    while (true) {
                        int i = length;
                        if (i < zzaArr.length - 1) {
                            zzaArr[i] = (zza) zzil.zza(zza.zzkj());
                            zzil.zzsg();
                            length = i + 1;
                        } else {
                            zzaArr[i] = (zza) zzil.zza(zza.zzkj());
                            this.zzzm = zzaArr;
                            continue;
                        }
                    }
                case 42:
                    int zzb2 = zzix.zzb(zzil, 42);
                    int length2 = this.zzzn == null ? 0 : this.zzzn.length;
                    zzbx[] zzbxArr = new zzbx[(zzb2 + length2)];
                    if (length2 != 0) {
                        System.arraycopy(this.zzzn, 0, zzbxArr, 0, length2);
                    }
                    while (length2 < zzbxArr.length - 1) {
                        zzbxArr[length2] = new zzbx();
                        zzil.zza((zziw) zzbxArr[length2]);
                        zzil.zzsg();
                        length2++;
                    }
                    zzbxArr[length2] = new zzbx();
                    zzil.zza((zziw) zzbxArr[length2]);
                    this.zzzn = zzbxArr;
                    continue;
                case 50:
                    int zzb3 = zzix.zzb(zzil, 50);
                    int length3 = this.zzzo == null ? 0 : this.zzzo.length;
                    zzbv[] zzbvArr = new zzbv[(zzb3 + length3)];
                    if (length3 != 0) {
                        System.arraycopy(this.zzzo, 0, zzbvArr, 0, length3);
                    }
                    while (length3 < zzbvArr.length - 1) {
                        zzbvArr[length3] = new zzbv();
                        zzil.zza((zziw) zzbvArr[length3]);
                        zzil.zzsg();
                        length3++;
                    }
                    zzbvArr[length3] = new zzbv();
                    zzil.zza((zziw) zzbvArr[length3]);
                    this.zzzo = zzbvArr;
                    continue;
                case 58:
                    this.zzzp = zzil.readString();
                    continue;
                case 64:
                    this.zzzq = Boolean.valueOf(zzil.zzsm());
                    continue;
                default:
                    if (!super.zza(zzil, zzsg)) {
                        break;
                    } else {
                        continue;
                    }
            }
        }
        return this;
    }

    public final void zza(zzio zzio) throws IOException {
        if (this.zzzk != null) {
            long longValue = this.zzzk.longValue();
            zzio.zzb(1, 0);
            zzio.zzbz(longValue);
        }
        if (this.zzcg != null) {
            zzio.zzb(2, this.zzcg);
        }
        if (this.zzzl != null) {
            zzio.zzc(3, this.zzzl.intValue());
        }
        if (this.zzzm != null && this.zzzm.length > 0) {
            for (zza zza : this.zzzm) {
                if (zza != null) {
                    zzio.zze(4, zza);
                }
            }
        }
        if (this.zzzn != null && this.zzzn.length > 0) {
            for (zzbx zzbx : this.zzzn) {
                if (zzbx != null) {
                    zzio.zza(5, zzbx);
                }
            }
        }
        if (this.zzzo != null && this.zzzo.length > 0) {
            for (zzbv zzbv : this.zzzo) {
                if (zzbv != null) {
                    zzio.zza(6, zzbv);
                }
            }
        }
        if (this.zzzp != null) {
            zzio.zzb(7, this.zzzp);
        }
        if (this.zzzq != null) {
            zzio.zzb(8, this.zzzq.booleanValue());
        }
        super.zza(zzio);
    }

    /* access modifiers changed from: protected */
    public final int zzqy() {
        int i;
        int i2 = 1;
        int zzqy = super.zzqy();
        if (this.zzzk != null) {
            long longValue = this.zzzk.longValue();
            int zzbi = zzio.zzbi(1);
            if ((-128 & longValue) != 0) {
                i2 = (-16384 & longValue) == 0 ? 2 : (-2097152 & longValue) == 0 ? 3 : (-268435456 & longValue) == 0 ? 4 : (-34359738368L & longValue) == 0 ? 5 : (-4398046511104L & longValue) == 0 ? 6 : (-562949953421312L & longValue) == 0 ? 7 : (-72057594037927936L & longValue) == 0 ? 8 : (longValue & Long.MIN_VALUE) == 0 ? 9 : 10;
            }
            i = i2 + zzbi + zzqy;
        } else {
            i = zzqy;
        }
        if (this.zzcg != null) {
            i += zzio.zzc(2, this.zzcg);
        }
        if (this.zzzl != null) {
            i += zzio.zzg(3, this.zzzl.intValue());
        }
        if (this.zzzm != null && this.zzzm.length > 0) {
            int i3 = i;
            for (zza zza : this.zzzm) {
                if (zza != null) {
                    i3 += zzee.zzc(4, (zzgi) zza);
                }
            }
            i = i3;
        }
        if (this.zzzn != null && this.zzzn.length > 0) {
            int i4 = i;
            for (zzbx zzbx : this.zzzn) {
                if (zzbx != null) {
                    i4 += zzio.zzb(5, (zziw) zzbx);
                }
            }
            i = i4;
        }
        if (this.zzzo != null && this.zzzo.length > 0) {
            for (zzbv zzbv : this.zzzo) {
                if (zzbv != null) {
                    i += zzio.zzb(6, (zziw) zzbv);
                }
            }
        }
        if (this.zzzp != null) {
            i += zzio.zzc(7, this.zzzp);
        }
        if (this.zzzq == null) {
            return i;
        }
        this.zzzq.booleanValue();
        return i + zzio.zzbi(8) + 1;
    }
}
