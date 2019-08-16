package com.google.android.gms.auth.api.signin.internal;

import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.auth.api.signin.GoogleSignInOptionsExtension;
import com.google.android.gms.common.annotation.KeepForSdk;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.VersionField;

@Class(creator = "GoogleSignInOptionsExtensionCreator")
public class GoogleSignInOptionsExtensionParcelable extends AbstractSafeParcelable {
    public static final Creator<GoogleSignInOptionsExtensionParcelable> CREATOR = new zaa();
    @Field(getter = "getBundle", mo13990id = 3)
    private Bundle mBundle;
    @Field(getter = "getType", mo13990id = 2)
    private int mType;
    @VersionField(mo13996id = 1)
    private final int versionCode;

    @Constructor
    GoogleSignInOptionsExtensionParcelable(@Param(mo13993id = 1) int i, @Param(mo13993id = 2) int i2, @Param(mo13993id = 3) Bundle bundle) {
        this.versionCode = i;
        this.mType = i2;
        this.mBundle = bundle;
    }

    public GoogleSignInOptionsExtensionParcelable(GoogleSignInOptionsExtension googleSignInOptionsExtension) {
        this(1, googleSignInOptionsExtension.getExtensionType(), googleSignInOptionsExtension.toBundle());
    }

    @KeepForSdk
    public int getType() {
        return this.mType;
    }

    public void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 1, this.versionCode);
        SafeParcelWriter.writeInt(parcel, 2, getType());
        SafeParcelWriter.writeBundle(parcel, 3, this.mBundle, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
