package com.google.android.gms.auth;

import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.VersionField;
import java.util.List;

@Class(creator = "TokenDataCreator")
public class TokenData extends AbstractSafeParcelable implements ReflectedParcelable {
    public static final Creator<TokenData> CREATOR = new zzk();
    @Field(getter = "getGrantedScopes", mo13990id = 6)
    private final List<String> zzaa;
    @Field(getter = "getScopeData", mo13990id = 7)
    private final String zzab;
    @VersionField(mo13996id = 1)
    private final int zzv;
    @Field(getter = "getToken", mo13990id = 2)
    private final String zzw;
    @Field(getter = "getExpirationTimeSecs", mo13990id = 3)
    private final Long zzx;
    @Field(getter = "isCached", mo13990id = 4)
    private final boolean zzy;
    @Field(getter = "isSnowballed", mo13990id = 5)
    private final boolean zzz;

    @Constructor
    TokenData(@Param(mo13993id = 1) int i, @Param(mo13993id = 2) String str, @Param(mo13993id = 3) Long l, @Param(mo13993id = 4) boolean z, @Param(mo13993id = 5) boolean z2, @Param(mo13993id = 6) List<String> list, @Param(mo13993id = 7) String str2) {
        this.zzv = i;
        this.zzw = Preconditions.checkNotEmpty(str);
        this.zzx = l;
        this.zzy = z;
        this.zzz = z2;
        this.zzaa = list;
        this.zzab = str2;
    }

    @Nullable
    public static TokenData zza(Bundle bundle, String str) {
        bundle.setClassLoader(TokenData.class.getClassLoader());
        Bundle bundle2 = bundle.getBundle(str);
        if (bundle2 == null) {
            return null;
        }
        bundle2.setClassLoader(TokenData.class.getClassLoader());
        return (TokenData) bundle2.getParcelable("TokenData");
    }

    public boolean equals(Object obj) {
        if (!(obj instanceof TokenData)) {
            return false;
        }
        TokenData tokenData = (TokenData) obj;
        return TextUtils.equals(this.zzw, tokenData.zzw) && Objects.equal(this.zzx, tokenData.zzx) && this.zzy == tokenData.zzy && this.zzz == tokenData.zzz && Objects.equal(this.zzaa, tokenData.zzaa) && Objects.equal(this.zzab, tokenData.zzab);
    }

    public int hashCode() {
        return Objects.hashCode(this.zzw, this.zzx, Boolean.valueOf(this.zzy), Boolean.valueOf(this.zzz), this.zzaa, this.zzab);
    }

    public void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 1, this.zzv);
        SafeParcelWriter.writeString(parcel, 2, this.zzw, false);
        SafeParcelWriter.writeLongObject(parcel, 3, this.zzx, false);
        SafeParcelWriter.writeBoolean(parcel, 4, this.zzy);
        SafeParcelWriter.writeBoolean(parcel, 5, this.zzz);
        SafeParcelWriter.writeStringList(parcel, 6, this.zzaa, false);
        SafeParcelWriter.writeString(parcel, 7, this.zzab, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }

    public final String zzb() {
        return this.zzw;
    }
}
