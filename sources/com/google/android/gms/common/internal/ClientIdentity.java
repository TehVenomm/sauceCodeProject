package com.google.android.gms.common.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.annotation.KeepForSdk;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;

@KeepForSdk
@Class(creator = "ClientIdentityCreator")
@Reserved({1000})
public class ClientIdentity extends AbstractSafeParcelable {
    @KeepForSdk
    public static final Creator<ClientIdentity> CREATOR = new zab();
    @Nullable
    @Field(defaultValueUnchecked = "null", mo13990id = 2)
    private final String packageName;
    @Field(defaultValueUnchecked = "0", mo13990id = 1)
    private final int uid;

    @Constructor
    public ClientIdentity(@Param(mo13993id = 1) int i, @Nullable @Param(mo13993id = 2) String str) {
        this.uid = i;
        this.packageName = str;
    }

    public boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (obj == null || !(obj instanceof ClientIdentity)) {
            return false;
        }
        ClientIdentity clientIdentity = (ClientIdentity) obj;
        return clientIdentity.uid == this.uid && Objects.equal(clientIdentity.packageName, this.packageName);
    }

    public int hashCode() {
        return this.uid;
    }

    public String toString() {
        int i = this.uid;
        String str = this.packageName;
        return new StringBuilder(String.valueOf(str).length() + 12).append(i).append(":").append(str).toString();
    }

    public void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 1, this.uid);
        SafeParcelWriter.writeString(parcel, 2, this.packageName, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
