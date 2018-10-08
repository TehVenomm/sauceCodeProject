package com.google.android.gms.games.multiplayer;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.games.GameEntity;
import java.util.ArrayList;

public class zza implements Creator<InvitationEntity> {
    public /* synthetic */ Object createFromParcel(Parcel parcel) {
        return zzk(parcel);
    }

    public /* synthetic */ Object[] newArray(int i) {
        return new InvitationEntity[i];
    }

    public InvitationEntity zzk(Parcel parcel) {
        int i = 0;
        GameEntity gameEntity = null;
        int zzd = zzb.zzd(parcel);
        long j = 0;
        String str = null;
        ParticipantEntity participantEntity = null;
        ArrayList arrayList = null;
        int i2 = 0;
        int i3 = 0;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    gameEntity = (GameEntity) zzb.zza(parcel, readInt, GameEntity.CREATOR);
                    break;
                case 2:
                    str = zzb.zzq(parcel, readInt);
                    break;
                case 3:
                    j = zzb.zzi(parcel, readInt);
                    break;
                case 4:
                    i = zzb.zzg(parcel, readInt);
                    break;
                case 5:
                    participantEntity = (ParticipantEntity) zzb.zza(parcel, readInt, ParticipantEntity.CREATOR);
                    break;
                case 6:
                    arrayList = zzb.zzc(parcel, readInt, ParticipantEntity.CREATOR);
                    break;
                case 7:
                    i2 = zzb.zzg(parcel, readInt);
                    break;
                case 8:
                    i3 = zzb.zzg(parcel, readInt);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new InvitationEntity(gameEntity, str, j, i, participantEntity, arrayList, i2, i3);
    }
}
