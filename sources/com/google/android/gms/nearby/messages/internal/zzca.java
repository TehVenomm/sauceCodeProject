package com.google.android.gms.nearby.messages.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.SafeParcelReader;
import com.google.android.gms.nearby.messages.Strategy;

public final class zzca implements Creator<zzbz> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        int validateObjectHeader = SafeParcelReader.validateObjectHeader(parcel);
        zzaf zzaf = null;
        Strategy strategy = null;
        IBinder iBinder = null;
        String str = null;
        String str2 = null;
        IBinder iBinder2 = null;
        ClientAppContext clientAppContext = null;
        boolean z = false;
        boolean z2 = false;
        int i = 0;
        int i2 = 0;
        while (parcel.dataPosition() < validateObjectHeader) {
            int readHeader = SafeParcelReader.readHeader(parcel);
            switch (SafeParcelReader.getFieldId(readHeader)) {
                case 1:
                    i = SafeParcelReader.readInt(parcel, readHeader);
                    break;
                case 2:
                    zzaf = (zzaf) SafeParcelReader.createParcelable(parcel, readHeader, zzaf.CREATOR);
                    break;
                case 3:
                    strategy = (Strategy) SafeParcelReader.createParcelable(parcel, readHeader, Strategy.CREATOR);
                    break;
                case 4:
                    iBinder = SafeParcelReader.readIBinder(parcel, readHeader);
                    break;
                case 5:
                    str = SafeParcelReader.createString(parcel, readHeader);
                    break;
                case 6:
                    str2 = SafeParcelReader.createString(parcel, readHeader);
                    break;
                case 7:
                    z = SafeParcelReader.readBoolean(parcel, readHeader);
                    break;
                case 8:
                    iBinder2 = SafeParcelReader.readIBinder(parcel, readHeader);
                    break;
                case 9:
                    z2 = SafeParcelReader.readBoolean(parcel, readHeader);
                    break;
                case 10:
                    clientAppContext = (ClientAppContext) SafeParcelReader.createParcelable(parcel, readHeader, ClientAppContext.CREATOR);
                    break;
                case 11:
                    i2 = SafeParcelReader.readInt(parcel, readHeader);
                    break;
                default:
                    SafeParcelReader.skipUnknownField(parcel, readHeader);
                    break;
            }
        }
        SafeParcelReader.ensureAtEnd(parcel, validateObjectHeader);
        return new zzbz(i, zzaf, strategy, iBinder, str, str2, z, iBinder2, z2, clientAppContext, i2);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzbz[i];
    }
}
