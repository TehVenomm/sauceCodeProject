package com.google.android.gms.games.internal.game;

import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.SafeParcelReader;

public final class zzc implements Creator<ScreenshotEntity> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        int validateObjectHeader = SafeParcelReader.validateObjectHeader(parcel);
        Uri uri = null;
        int i = 0;
        int i2 = 0;
        while (parcel.dataPosition() < validateObjectHeader) {
            int readHeader = SafeParcelReader.readHeader(parcel);
            switch (SafeParcelReader.getFieldId(readHeader)) {
                case 1:
                    uri = (Uri) SafeParcelReader.createParcelable(parcel, readHeader, Uri.CREATOR);
                    break;
                case 2:
                    i2 = SafeParcelReader.readInt(parcel, readHeader);
                    break;
                case 3:
                    i = SafeParcelReader.readInt(parcel, readHeader);
                    break;
                default:
                    SafeParcelReader.skipUnknownField(parcel, readHeader);
                    break;
            }
        }
        SafeParcelReader.ensureAtEnd(parcel, validateObjectHeader);
        return new ScreenshotEntity(uri, i2, i);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new ScreenshotEntity[i];
    }
}
