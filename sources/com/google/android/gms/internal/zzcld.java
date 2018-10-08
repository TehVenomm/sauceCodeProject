package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.nearby.connection.DiscoveryOptions;

public final class zzcld implements Creator<zzclc> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        IBinder iBinder = null;
        int zzd = zzb.zzd(parcel);
        long j = 0;
        IBinder iBinder2 = null;
        String str = null;
        DiscoveryOptions discoveryOptions = null;
        IBinder iBinder3 = null;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    iBinder = zzb.zzr(parcel, readInt);
                    break;
                case 2:
                    iBinder2 = zzb.zzr(parcel, readInt);
                    break;
                case 3:
                    str = zzb.zzq(parcel, readInt);
                    break;
                case 4:
                    j = zzb.zzi(parcel, readInt);
                    break;
                case 5:
                    discoveryOptions = (DiscoveryOptions) zzb.zza(parcel, readInt, DiscoveryOptions.CREATOR);
                    break;
                case 6:
                    iBinder3 = zzb.zzr(parcel, readInt);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new zzclc(iBinder, iBinder2, str, j, discoveryOptions, iBinder3);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzclc[i];
    }
}
