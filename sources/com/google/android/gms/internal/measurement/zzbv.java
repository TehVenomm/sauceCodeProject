package com.google.android.gms.internal.measurement;

import com.google.android.gms.internal.measurement.zzbk.zza;
import com.google.android.gms.internal.measurement.zzbk.zzd;
import java.io.IOException;

public final class zzbv extends zziq<zzbv> {
    private static volatile zzbv[] zzze;
    public Integer zzzf;
    public zzd[] zzzg;
    public zza[] zzzh;
    private Boolean zzzi;
    private Boolean zzzj;

    public zzbv() {
        this.zzzf = null;
        this.zzzg = new zzd[0];
        this.zzzh = new zza[0];
        this.zzzi = null;
        this.zzzj = null;
        this.zzaoo = null;
        this.zzaow = -1;
    }

    public static zzbv[] zzqx() {
        if (zzze == null) {
            synchronized (zziu.zzaov) {
                if (zzze == null) {
                    zzze = new zzbv[0];
                }
            }
        }
        return zzze;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof zzbv)) {
            return false;
        }
        zzbv zzbv = (zzbv) obj;
        if (this.zzzf == null) {
            if (zzbv.zzzf != null) {
                return false;
            }
        } else if (!this.zzzf.equals(zzbv.zzzf)) {
            return false;
        }
        if (!zziu.equals(this.zzzg, zzbv.zzzg)) {
            return false;
        }
        if (!zziu.equals(this.zzzh, zzbv.zzzh)) {
            return false;
        }
        if (this.zzzi == null) {
            if (zzbv.zzzi != null) {
                return false;
            }
        } else if (!this.zzzi.equals(zzbv.zzzi)) {
            return false;
        }
        if (this.zzzj == null) {
            if (zzbv.zzzj != null) {
                return false;
            }
        } else if (!this.zzzj.equals(zzbv.zzzj)) {
            return false;
        }
        return (this.zzaoo == null || this.zzaoo.isEmpty()) ? zzbv.zzaoo == null || zzbv.zzaoo.isEmpty() : this.zzaoo.equals(zzbv.zzaoo);
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = getClass().getName().hashCode();
        int hashCode2 = this.zzzf == null ? 0 : this.zzzf.hashCode();
        int hashCode3 = zziu.hashCode(this.zzzg);
        int hashCode4 = zziu.hashCode(this.zzzh);
        int hashCode5 = this.zzzi == null ? 0 : this.zzzi.hashCode();
        int hashCode6 = this.zzzj == null ? 0 : this.zzzj.hashCode();
        if (this.zzaoo != null && !this.zzaoo.isEmpty()) {
            i = this.zzaoo.hashCode();
        }
        return ((((((((((hashCode2 + ((hashCode + 527) * 31)) * 31) + hashCode3) * 31) + hashCode4) * 31) + hashCode5) * 31) + hashCode6) * 31) + i;
    }

    public final /* synthetic */ zziw zza(zzil zzil) throws IOException {
        while (true) {
            int zzsg = zzil.zzsg();
            switch (zzsg) {
                case 0:
                    break;
                case 8:
                    this.zzzf = Integer.valueOf(zzil.zzta());
                    continue;
                case 18:
                    int zzb = zzix.zzb(zzil, 18);
                    int length = this.zzzg == null ? 0 : this.zzzg.length;
                    zzd[] zzdArr = new zzd[(zzb + length)];
                    if (length != 0) {
                        System.arraycopy(this.zzzg, 0, zzdArr, 0, length);
                    }
                    while (true) {
                        int i = length;
                        if (i < zzdArr.length - 1) {
                            zzdArr[i] = (zzd) zzil.zza(zzd.zzkj());
                            zzil.zzsg();
                            length = i + 1;
                        } else {
                            zzdArr[i] = (zzd) zzil.zza(zzd.zzkj());
                            this.zzzg = zzdArr;
                            continue;
                        }
                    }
                case 26:
                    int zzb2 = zzix.zzb(zzil, 26);
                    int length2 = this.zzzh == null ? 0 : this.zzzh.length;
                    zza[] zzaArr = new zza[(zzb2 + length2)];
                    if (length2 != 0) {
                        System.arraycopy(this.zzzh, 0, zzaArr, 0, length2);
                    }
                    while (true) {
                        int i2 = length2;
                        if (i2 < zzaArr.length - 1) {
                            zzaArr[i2] = (zza) zzil.zza(zza.zzkj());
                            zzil.zzsg();
                            length2 = i2 + 1;
                        } else {
                            zzaArr[i2] = (zza) zzil.zza(zza.zzkj());
                            this.zzzh = zzaArr;
                            continue;
                        }
                    }
                case 32:
                    this.zzzi = Boolean.valueOf(zzil.zzsm());
                    continue;
                case 40:
                    this.zzzj = Boolean.valueOf(zzil.zzsm());
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
        if (this.zzzf != null) {
            zzio.zzc(1, this.zzzf.intValue());
        }
        if (this.zzzg != null && this.zzzg.length > 0) {
            for (zzd zzd : this.zzzg) {
                if (zzd != null) {
                    zzio.zze(2, zzd);
                }
            }
        }
        if (this.zzzh != null && this.zzzh.length > 0) {
            for (zza zza : this.zzzh) {
                if (zza != null) {
                    zzio.zze(3, zza);
                }
            }
        }
        if (this.zzzi != null) {
            zzio.zzb(4, this.zzzi.booleanValue());
        }
        if (this.zzzj != null) {
            zzio.zzb(5, this.zzzj.booleanValue());
        }
        super.zza(zzio);
    }

    /* access modifiers changed from: protected */
    public final int zzqy() {
        int zzqy = super.zzqy();
        if (this.zzzf != null) {
            zzqy += zzio.zzg(1, this.zzzf.intValue());
        }
        if (this.zzzg != null && this.zzzg.length > 0) {
            int i = zzqy;
            for (zzd zzd : this.zzzg) {
                if (zzd != null) {
                    i += zzee.zzc(2, (zzgi) zzd);
                }
            }
            zzqy = i;
        }
        if (this.zzzh != null && this.zzzh.length > 0) {
            for (zza zza : this.zzzh) {
                if (zza != null) {
                    zzqy += zzee.zzc(3, (zzgi) zza);
                }
            }
        }
        if (this.zzzi != null) {
            this.zzzi.booleanValue();
            zzqy += zzio.zzbi(4) + 1;
        }
        if (this.zzzj == null) {
            return zzqy;
        }
        this.zzzj.booleanValue();
        return zzqy + zzio.zzbi(5) + 1;
    }
}
