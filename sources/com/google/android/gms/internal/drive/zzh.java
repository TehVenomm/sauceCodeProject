package com.google.android.gms.internal.drive;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import com.google.android.gms.drive.DriveId;

@Class(creator = "TransferProgressDataCreator")
@Reserved({1})
public final class zzh extends AbstractSafeParcelable {
    public static final Creator<zzh> CREATOR = new zzi();
    @Field(mo13990id = 4)
    final int status;
    @Field(mo13990id = 2)
    final int zzcr;
    @Field(mo13990id = 5)
    final long zzcu;
    @Field(mo13990id = 6)
    final long zzcv;
    @Field(mo13990id = 3)
    final DriveId zzk;

    @Constructor
    public zzh(@Param(mo13993id = 2) int i, @Param(mo13993id = 3) DriveId driveId, @Param(mo13993id = 4) int i2, @Param(mo13993id = 5) long j, @Param(mo13993id = 6) long j2) {
        this.zzcr = i;
        this.zzk = driveId;
        this.status = i2;
        this.zzcu = j;
        this.zzcv = j2;
    }

    public final boolean equals(Object obj) {
        if (obj == null || obj.getClass() != getClass()) {
            return false;
        }
        if (obj == this) {
            return true;
        }
        zzh zzh = (zzh) obj;
        return this.zzcr == zzh.zzcr && Objects.equal(this.zzk, zzh.zzk) && this.status == zzh.status && this.zzcu == zzh.zzcu && this.zzcv == zzh.zzcv;
    }

    public final int hashCode() {
        return Objects.hashCode(Integer.valueOf(this.zzcr), this.zzk, Integer.valueOf(this.status), Long.valueOf(this.zzcu), Long.valueOf(this.zzcv));
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 2, this.zzcr);
        SafeParcelWriter.writeParcelable(parcel, 3, this.zzk, i, false);
        SafeParcelWriter.writeInt(parcel, 4, this.status);
        SafeParcelWriter.writeLong(parcel, 5, this.zzcu);
        SafeParcelWriter.writeLong(parcel, 6, this.zzcv);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
