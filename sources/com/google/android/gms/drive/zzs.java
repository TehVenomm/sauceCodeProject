package com.google.android.gms.drive;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class zzs extends zza {
    public static final Creator<zzs> CREATOR = new zzt();
    private String zzgeh;
    private int zzgei;
    private String zzgej;
    private String zzgek;
    private int zzgel;
    private boolean zzgem;

    public zzs(String str, int i, String str2, String str3, int i2, boolean z) {
        this.zzgeh = str;
        this.zzgei = i;
        this.zzgej = str2;
        this.zzgek = str3;
        this.zzgel = i2;
        this.zzgem = z;
    }

    private static boolean zzco(int i) {
        switch (i) {
            case 256:
            case 257:
            case 258:
                return true;
            default:
                return false;
        }
    }

    public final boolean equals(Object obj) {
        if (obj == null || obj.getClass() != getClass()) {
            return false;
        }
        if (obj == this) {
            return true;
        }
        zzs zzs = (zzs) obj;
        return zzbf.equal(this.zzgeh, zzs.zzgeh) && this.zzgei == zzs.zzgei && this.zzgel == zzs.zzgel && this.zzgem == zzs.zzgem;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzgeh, Integer.valueOf(this.zzgei), Integer.valueOf(this.zzgel), Boolean.valueOf(this.zzgem)});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        boolean z;
        int i2 = -1;
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, !zzco(this.zzgei) ? null : this.zzgeh, false);
        zzd.zzc(parcel, 3, !zzco(this.zzgei) ? -1 : this.zzgei);
        zzd.zza(parcel, 4, this.zzgej, false);
        zzd.zza(parcel, 5, this.zzgek, false);
        switch (this.zzgel) {
            case 0:
            case 1:
            case 2:
            case 3:
                z = true;
                break;
            default:
                z = false;
                break;
        }
        if (z) {
            i2 = this.zzgel;
        }
        zzd.zzc(parcel, 6, i2);
        zzd.zza(parcel, 7, this.zzgem);
        zzd.zzai(parcel, zze);
    }
}
