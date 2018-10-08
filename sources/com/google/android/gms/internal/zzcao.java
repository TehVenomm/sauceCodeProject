package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;

public final class zzcao implements Creator<zzcan> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        int zzd = zzb.zzd(parcel);
        int i = 0;
        String str = null;
        String str2 = null;
        zzcfl zzcfl = null;
        long j = 0;
        boolean z = false;
        String str3 = null;
        zzcbc zzcbc = null;
        long j2 = 0;
        zzcbc zzcbc2 = null;
        long j3 = 0;
        zzcbc zzcbc3 = null;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    i = zzb.zzg(parcel, readInt);
                    break;
                case 2:
                    str = zzb.zzq(parcel, readInt);
                    break;
                case 3:
                    str2 = zzb.zzq(parcel, readInt);
                    break;
                case 4:
                    zzcfl = (zzcfl) zzb.zza(parcel, readInt, zzcfl.CREATOR);
                    break;
                case 5:
                    j = zzb.zzi(parcel, readInt);
                    break;
                case 6:
                    z = zzb.zzc(parcel, readInt);
                    break;
                case 7:
                    str3 = zzb.zzq(parcel, readInt);
                    break;
                case 8:
                    zzcbc = (zzcbc) zzb.zza(parcel, readInt, zzcbc.CREATOR);
                    break;
                case 9:
                    j2 = zzb.zzi(parcel, readInt);
                    break;
                case 10:
                    zzcbc2 = (zzcbc) zzb.zza(parcel, readInt, zzcbc.CREATOR);
                    break;
                case 11:
                    j3 = zzb.zzi(parcel, readInt);
                    break;
                case 12:
                    zzcbc3 = (zzcbc) zzb.zza(parcel, readInt, zzcbc.CREATOR);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new zzcan(i, str, str2, zzcfl, j, z, str3, zzcbc, j2, zzcbc2, j3, zzcbc3);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzcan[i];
    }
}
