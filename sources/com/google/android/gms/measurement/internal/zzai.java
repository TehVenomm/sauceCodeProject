package com.google.android.gms.measurement.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;

@Class(creator = "EventParcelCreator")
@Reserved({1})
public final class zzai extends AbstractSafeParcelable {
    public static final Creator<zzai> CREATOR = new zzal();
    @Field(mo13990id = 2)
    public final String name;
    @Field(mo13990id = 4)
    public final String origin;
    @Field(mo13990id = 3)
    public final zzah zzfq;
    @Field(mo13990id = 5)
    public final long zzfu;

    zzai(zzai zzai, long j) {
        Preconditions.checkNotNull(zzai);
        this.name = zzai.name;
        this.zzfq = zzai.zzfq;
        this.origin = zzai.origin;
        this.zzfu = j;
    }

    @Constructor
    public zzai(@Param(mo13993id = 2) String str, @Param(mo13993id = 3) zzah zzah, @Param(mo13993id = 4) String str2, @Param(mo13993id = 5) long j) {
        this.name = str;
        this.zzfq = zzah;
        this.origin = str2;
        this.zzfu = j;
    }

    public final String toString() {
        String str = this.origin;
        String str2 = this.name;
        String valueOf = String.valueOf(this.zzfq);
        return new StringBuilder(String.valueOf(str).length() + 21 + String.valueOf(str2).length() + String.valueOf(valueOf).length()).append("origin=").append(str).append(",name=").append(str2).append(",params=").append(valueOf).toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeString(parcel, 2, this.name, false);
        SafeParcelWriter.writeParcelable(parcel, 3, this.zzfq, i, false);
        SafeParcelWriter.writeString(parcel, 4, this.origin, false);
        SafeParcelWriter.writeLong(parcel, 5, this.zzfu);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
