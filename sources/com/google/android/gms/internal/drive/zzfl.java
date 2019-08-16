package com.google.android.gms.internal.drive;

import android.os.Parcel;
import android.os.ParcelFileDescriptor;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;

@Class(creator = "OnFetchThumbnailResponseCreator")
@Reserved({1})
public final class zzfl extends AbstractSafeParcelable {
    public static final Creator<zzfl> CREATOR = new zzfm();
    @Field(mo13990id = 2)
    private final ParcelFileDescriptor zzhr;

    @Constructor
    public zzfl(@Param(mo13993id = 2) ParcelFileDescriptor parcelFileDescriptor) {
        this.zzhr = parcelFileDescriptor;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeParcelable(parcel, 2, this.zzhr, i | 1, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
