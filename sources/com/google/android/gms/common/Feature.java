package com.google.android.gms.common;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.facebook.internal.ServerProtocol;
import com.google.android.gms.common.annotation.KeepForSdk;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;

@KeepForSdk
@Class(creator = "FeatureCreator")
public class Feature extends AbstractSafeParcelable {
    public static final Creator<Feature> CREATOR = new zzb();
    @Field(getter = "getName", mo13990id = 1)
    private final String name;
    @Field(getter = "getOldVersion", mo13990id = 2)
    @Deprecated
    private final int zzk;
    @Field(defaultValue = "-1", getter = "getVersion", mo13990id = 3)
    private final long zzl;

    @Constructor
    public Feature(@Param(mo13993id = 1) String str, @Param(mo13993id = 2) int i, @Param(mo13993id = 3) long j) {
        this.name = str;
        this.zzk = i;
        this.zzl = j;
    }

    @KeepForSdk
    public Feature(String str, long j) {
        this.name = str;
        this.zzl = j;
        this.zzk = -1;
    }

    public boolean equals(@Nullable Object obj) {
        if (!(obj instanceof Feature)) {
            return false;
        }
        Feature feature = (Feature) obj;
        return ((getName() != null && getName().equals(feature.getName())) || (getName() == null && feature.getName() == null)) && getVersion() == feature.getVersion();
    }

    @KeepForSdk
    public String getName() {
        return this.name;
    }

    @KeepForSdk
    public long getVersion() {
        return this.zzl == -1 ? (long) this.zzk : this.zzl;
    }

    public int hashCode() {
        return Objects.hashCode(getName(), Long.valueOf(getVersion()));
    }

    public String toString() {
        return Objects.toStringHelper(this).add("name", getName()).add(ServerProtocol.FALLBACK_DIALOG_PARAM_VERSION, Long.valueOf(getVersion())).toString();
    }

    public void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeString(parcel, 1, getName(), false);
        SafeParcelWriter.writeInt(parcel, 2, this.zzk);
        SafeParcelWriter.writeLong(parcel, 3, getVersion());
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
