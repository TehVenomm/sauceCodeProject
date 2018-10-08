package com.google.android.gms.nearby.messages.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.nearby.messages.Strategy;

public final class zzay implements Creator<zzax> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        boolean z = false;
        zzaf zzaf = null;
        int zzd = zzb.zzd(parcel);
        Strategy strategy = null;
        IBinder iBinder = null;
        String str = null;
        String str2 = null;
        IBinder iBinder2 = null;
        ClientAppContext clientAppContext = null;
        boolean z2 = false;
        int i = 0;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    i = zzb.zzg(parcel, readInt);
                    break;
                case 2:
                    zzaf = (zzaf) zzb.zza(parcel, readInt, zzaf.CREATOR);
                    break;
                case 3:
                    strategy = (Strategy) zzb.zza(parcel, readInt, Strategy.CREATOR);
                    break;
                case 4:
                    iBinder = zzb.zzr(parcel, readInt);
                    break;
                case 5:
                    str = zzb.zzq(parcel, readInt);
                    break;
                case 6:
                    str2 = zzb.zzq(parcel, readInt);
                    break;
                case 7:
                    z = zzb.zzc(parcel, readInt);
                    break;
                case 8:
                    iBinder2 = zzb.zzr(parcel, readInt);
                    break;
                case 9:
                    z2 = zzb.zzc(parcel, readInt);
                    break;
                case 10:
                    clientAppContext = (ClientAppContext) zzb.zza(parcel, readInt, ClientAppContext.CREATOR);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new zzax(i, zzaf, strategy, iBinder, str, str2, z, iBinder2, z2, clientAppContext);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzax[i];
    }
}
