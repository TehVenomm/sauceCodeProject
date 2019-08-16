package com.google.android.gms.internal.nearby;

import android.os.Parcel;
import android.os.ParcelUuid;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.VersionField;
import java.util.Arrays;

@Class(creator = "BleFilterCreator")
public final class zzgp extends AbstractSafeParcelable {
    public static final Creator<zzgp> CREATOR = new zzgq();
    @VersionField(getter = "getVersionCode", mo13996id = 1)
    private final int zzex;
    @Nullable
    @Field(getter = "getServiceUuid", mo13990id = 4)
    private final ParcelUuid zzge;
    @Nullable
    @Field(getter = "getServiceUuidMask", mo13990id = 5)
    private final ParcelUuid zzgf;
    @Nullable
    @Field(getter = "getServiceDataUuid", mo13990id = 6)
    private final ParcelUuid zzgg;
    @Nullable
    @Field(getter = "getServiceData", mo13990id = 7)
    private final byte[] zzgh;
    @Nullable
    @Field(getter = "getServiceDataMask", mo13990id = 8)
    private final byte[] zzgi;
    @Field(getter = "getManufacturerId", mo13990id = 9)
    private final int zzgj;
    @Nullable
    @Field(getter = "getManufacturerData", mo13990id = 10)
    private final byte[] zzgk;
    @Nullable
    @Field(getter = "getManufacturerDataMask", mo13990id = 11)
    private final byte[] zzgl;

    @Constructor
    zzgp(@Param(mo13993id = 1) int i, @Param(mo13993id = 4) ParcelUuid parcelUuid, @Param(mo13993id = 5) ParcelUuid parcelUuid2, @Param(mo13993id = 6) ParcelUuid parcelUuid3, @Param(mo13993id = 7) byte[] bArr, @Param(mo13993id = 8) byte[] bArr2, @Param(mo13993id = 9) int i2, @Param(mo13993id = 10) byte[] bArr3, @Param(mo13993id = 11) byte[] bArr4) {
        this.zzex = i;
        this.zzge = parcelUuid;
        this.zzgf = parcelUuid2;
        this.zzgg = parcelUuid3;
        this.zzgh = bArr;
        this.zzgi = bArr2;
        this.zzgj = i2;
        this.zzgk = bArr3;
        this.zzgl = bArr4;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null || getClass() != obj.getClass()) {
            return false;
        }
        zzgp zzgp = (zzgp) obj;
        return this.zzgj == zzgp.zzgj && Arrays.equals(this.zzgk, zzgp.zzgk) && Arrays.equals(this.zzgl, zzgp.zzgl) && Objects.equal(this.zzgg, zzgp.zzgg) && Arrays.equals(this.zzgh, zzgp.zzgh) && Arrays.equals(this.zzgi, zzgp.zzgi) && Objects.equal(this.zzge, zzgp.zzge) && Objects.equal(this.zzgf, zzgp.zzgf);
    }

    public final int hashCode() {
        return Objects.hashCode(Integer.valueOf(this.zzgj), Integer.valueOf(Arrays.hashCode(this.zzgk)), Integer.valueOf(Arrays.hashCode(this.zzgl)), this.zzgg, Integer.valueOf(Arrays.hashCode(this.zzgh)), Integer.valueOf(Arrays.hashCode(this.zzgi)), this.zzge, this.zzgf);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 1, this.zzex);
        SafeParcelWriter.writeParcelable(parcel, 4, this.zzge, i, false);
        SafeParcelWriter.writeParcelable(parcel, 5, this.zzgf, i, false);
        SafeParcelWriter.writeParcelable(parcel, 6, this.zzgg, i, false);
        SafeParcelWriter.writeByteArray(parcel, 7, this.zzgh, false);
        SafeParcelWriter.writeByteArray(parcel, 8, this.zzgi, false);
        SafeParcelWriter.writeInt(parcel, 9, this.zzgj);
        SafeParcelWriter.writeByteArray(parcel, 10, this.zzgk, false);
        SafeParcelWriter.writeByteArray(parcel, 11, this.zzgl, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
