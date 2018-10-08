package com.google.android.gms.drive.events;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.drive.DriveSpace;
import java.util.Arrays;
import java.util.HashSet;
import java.util.List;
import java.util.Locale;
import java.util.Set;

public final class zze extends zza {
    public static final Creator<zze> CREATOR = new zzf();
    private int zzdxt;
    private int zzgeu;
    private boolean zzgev;
    private List<DriveSpace> zzgew;
    private final Set<DriveSpace> zzgex;

    zze(int i, int i2, boolean z, List<DriveSpace> list) {
        this(i, i2, z, list, list == null ? null : new HashSet(list));
    }

    private zze(int i, int i2, boolean z, List<DriveSpace> list, Set<DriveSpace> set) {
        this.zzdxt = i;
        this.zzgeu = i2;
        this.zzgev = z;
        this.zzgew = list;
        this.zzgex = set;
    }

    public final boolean equals(Object obj) {
        if (obj == null || obj.getClass() != getClass()) {
            return false;
        }
        if (obj == this) {
            return true;
        }
        zze zze = (zze) obj;
        return zzbf.equal(this.zzgex, zze.zzgex) && this.zzgeu == zze.zzgeu && this.zzgev == zze.zzgev;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzgex, Integer.valueOf(this.zzgeu), Boolean.valueOf(this.zzgev)});
    }

    public final String toString() {
        return String.format(Locale.US, "ChangesAvailableOptions[ChangesSizeLimit=%d, Repeats=%s, Spaces=%s]", new Object[]{Integer.valueOf(this.zzgeu), Boolean.valueOf(this.zzgev), this.zzgew});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zzc(parcel, 2, this.zzgeu);
        zzd.zza(parcel, 3, this.zzgev);
        zzd.zzc(parcel, 4, this.zzgew, false);
        zzd.zzai(parcel, zze);
    }
}
