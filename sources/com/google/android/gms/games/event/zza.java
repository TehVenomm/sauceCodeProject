package com.google.android.gms.games.event;

import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.SafeParcelReader;
import com.google.android.gms.games.Player;
import com.google.android.gms.games.PlayerEntity;

public final class zza implements Creator<EventEntity> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        int validateObjectHeader = SafeParcelReader.validateObjectHeader(parcel);
        long j = 0;
        boolean z = false;
        String str = null;
        String str2 = null;
        String str3 = null;
        Uri uri = null;
        String str4 = null;
        Player player = null;
        String str5 = null;
        while (parcel.dataPosition() < validateObjectHeader) {
            int readHeader = SafeParcelReader.readHeader(parcel);
            switch (SafeParcelReader.getFieldId(readHeader)) {
                case 1:
                    str = SafeParcelReader.createString(parcel, readHeader);
                    break;
                case 2:
                    str2 = SafeParcelReader.createString(parcel, readHeader);
                    break;
                case 3:
                    str3 = SafeParcelReader.createString(parcel, readHeader);
                    break;
                case 4:
                    uri = (Uri) SafeParcelReader.createParcelable(parcel, readHeader, Uri.CREATOR);
                    break;
                case 5:
                    str4 = SafeParcelReader.createString(parcel, readHeader);
                    break;
                case 6:
                    player = (PlayerEntity) SafeParcelReader.createParcelable(parcel, readHeader, PlayerEntity.CREATOR);
                    break;
                case 7:
                    j = SafeParcelReader.readLong(parcel, readHeader);
                    break;
                case 8:
                    str5 = SafeParcelReader.createString(parcel, readHeader);
                    break;
                case 9:
                    z = SafeParcelReader.readBoolean(parcel, readHeader);
                    break;
                default:
                    SafeParcelReader.skipUnknownField(parcel, readHeader);
                    break;
            }
        }
        SafeParcelReader.ensureAtEnd(parcel, validateObjectHeader);
        return new EventEntity(str, str2, str3, uri, str4, player, j, str5, z);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new EventEntity[i];
    }
}
