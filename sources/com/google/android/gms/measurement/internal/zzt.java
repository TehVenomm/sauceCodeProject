package com.google.android.gms.measurement.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.SafeParcelReader;

public final class zzt implements Creator<zzq> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        int validateObjectHeader = SafeParcelReader.validateObjectHeader(parcel);
        String str = null;
        String str2 = null;
        zzjn zzjn = null;
        long j = 0;
        boolean z = false;
        String str3 = null;
        zzai zzai = null;
        long j2 = 0;
        zzai zzai2 = null;
        long j3 = 0;
        zzai zzai3 = null;
        while (parcel.dataPosition() < validateObjectHeader) {
            int readHeader = SafeParcelReader.readHeader(parcel);
            switch (SafeParcelReader.getFieldId(readHeader)) {
                case 2:
                    str = SafeParcelReader.createString(parcel, readHeader);
                    break;
                case 3:
                    str2 = SafeParcelReader.createString(parcel, readHeader);
                    break;
                case 4:
                    zzjn = (zzjn) SafeParcelReader.createParcelable(parcel, readHeader, zzjn.CREATOR);
                    break;
                case 5:
                    j = SafeParcelReader.readLong(parcel, readHeader);
                    break;
                case 6:
                    z = SafeParcelReader.readBoolean(parcel, readHeader);
                    break;
                case 7:
                    str3 = SafeParcelReader.createString(parcel, readHeader);
                    break;
                case 8:
                    zzai = (zzai) SafeParcelReader.createParcelable(parcel, readHeader, zzai.CREATOR);
                    break;
                case 9:
                    j2 = SafeParcelReader.readLong(parcel, readHeader);
                    break;
                case 10:
                    zzai2 = (zzai) SafeParcelReader.createParcelable(parcel, readHeader, zzai.CREATOR);
                    break;
                case 11:
                    j3 = SafeParcelReader.readLong(parcel, readHeader);
                    break;
                case 12:
                    zzai3 = (zzai) SafeParcelReader.createParcelable(parcel, readHeader, zzai.CREATOR);
                    break;
                default:
                    SafeParcelReader.skipUnknownField(parcel, readHeader);
                    break;
            }
        }
        SafeParcelReader.ensureAtEnd(parcel, validateObjectHeader);
        return new zzq(str, str2, zzjn, j, z, str3, zzai, j2, zzai2, j3, zzai3);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzq[i];
    }
}
