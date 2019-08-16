package com.google.android.gms.internal.drive;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import com.google.android.gms.drive.zzh;
import java.util.Collections;
import java.util.List;

@Class(creator = "OnDownloadProgressResponseCreator")
@Reserved({1})
public final class zzff extends AbstractSafeParcelable {
    public static final Creator<zzff> CREATOR = new zzfg();
    private static final List<zzh> zzhh = Collections.emptyList();
    @Field(mo13990id = 4)
    private final int status;
    @Field(mo13990id = 2)
    final long zzhi;
    @Field(mo13990id = 3)
    final long zzhj;
    @Nullable
    @Field(mo13990id = 5)
    private final List<zzh> zzhk;

    @Constructor
    public zzff(@Param(mo13993id = 2) long j, @Param(mo13993id = 3) long j2, @Param(mo13993id = 4) int i, @Param(mo13993id = 5) List<zzh> list) {
        this.zzhi = j;
        this.zzhj = j2;
        this.status = i;
        this.zzhk = list;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeLong(parcel, 2, this.zzhi);
        SafeParcelWriter.writeLong(parcel, 3, this.zzhj);
        SafeParcelWriter.writeInt(parcel, 4, this.status);
        SafeParcelWriter.writeTypedList(parcel, 5, this.zzhk, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
