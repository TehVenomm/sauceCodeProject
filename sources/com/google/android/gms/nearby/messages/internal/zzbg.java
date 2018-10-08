package com.google.android.gms.nearby.messages.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.internal.zzclm;
import com.google.android.gms.nearby.messages.Message;

public final class zzbg implements Creator<Update> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        int i = 0;
        Message message = null;
        int zzd = zzb.zzd(parcel);
        zze zze = null;
        zza zza = null;
        zzclm zzclm = null;
        int i2 = 0;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    i = zzb.zzg(parcel, readInt);
                    break;
                case 2:
                    i2 = zzb.zzg(parcel, readInt);
                    break;
                case 3:
                    message = (Message) zzb.zza(parcel, readInt, Message.CREATOR);
                    break;
                case 4:
                    zze = (zze) zzb.zza(parcel, readInt, zze.CREATOR);
                    break;
                case 5:
                    zza = (zza) zzb.zza(parcel, readInt, zza.CREATOR);
                    break;
                case 6:
                    zzclm = (zzclm) zzb.zza(parcel, readInt, zzclm.CREATOR);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new Update(i, i2, message, zze, zza, zzclm);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new Update[i];
    }
}
