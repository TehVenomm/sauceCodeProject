package com.google.android.gms.nearby.messages.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import android.util.Log;
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
import java.util.Locale;

@Class(creator = "ClientAppContextCreator")
public final class ClientAppContext extends AbstractSafeParcelable implements ReflectedParcelable {
    public static final Creator<ClientAppContext> CREATOR = new zzd();
    @VersionField(mo13996id = 1)
    private final int versionCode;
    @Field(mo13990id = 4)
    private final boolean zzfg;
    @Field(mo13990id = 6)
    private final String zzfi;
    @Field(mo13990id = 2)
    private final String zzfj;
    @Nullable
    @Field(mo13990id = 3)
    private final String zzhe;
    @Field(mo13990id = 5)
    @Deprecated
    public final int zzhf;

    @Constructor
    ClientAppContext(@Param(mo13993id = 1) int i, @Param(mo13993id = 2) String str, @Nullable @Param(mo13993id = 3) String str2, @Param(mo13993id = 4) boolean z, @Param(mo13993id = 5) int i2, @Nullable @Param(mo13993id = 6) String str3) {
        this.versionCode = i;
        this.zzfj = (String) Preconditions.checkNotNull(str);
        if (str2 != null && !str2.isEmpty() && !str2.startsWith("0p:")) {
            Log.w("NearbyMessages", String.format(Locale.US, "ClientAppContext: 0P identifier(%s) without 0P prefix(%s)", new Object[]{str2, "0p:"}));
            String valueOf = String.valueOf("0p:");
            String valueOf2 = String.valueOf(str2);
            str2 = valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf);
        }
        this.zzhe = str2;
        this.zzfg = z;
        this.zzhf = i2;
        this.zzfi = str3;
    }

    public ClientAppContext(String str, @Nullable String str2, boolean z, @Nullable String str3, int i) {
        this(1, str, str2, z, i, str3);
    }

    @Nullable
    static final ClientAppContext zza(@Nullable ClientAppContext clientAppContext, @Nullable String str, @Nullable String str2, boolean z) {
        if (clientAppContext != null) {
            return clientAppContext;
        }
        if (str == null && str2 == null) {
            return null;
        }
        return new ClientAppContext(str, str2, z, null, 0);
    }

    private static boolean zzc(String str, String str2) {
        return TextUtils.isEmpty(str) ? TextUtils.isEmpty(str2) : str.equals(str2);
    }

    public final boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof ClientAppContext)) {
                return false;
            }
            ClientAppContext clientAppContext = (ClientAppContext) obj;
            if (!zzc(this.zzfj, clientAppContext.zzfj) || !zzc(this.zzhe, clientAppContext.zzhe) || this.zzfg != clientAppContext.zzfg || !zzc(this.zzfi, clientAppContext.zzfi) || this.zzhf != clientAppContext.zzhf) {
                return false;
            }
        }
        return true;
    }

    public final int hashCode() {
        return Objects.hashCode(this.zzfj, this.zzhe, Boolean.valueOf(this.zzfg), this.zzfi, Integer.valueOf(this.zzhf));
    }

    public final String toString() {
        return String.format(Locale.US, "{realClientPackageName: %s, zeroPartyIdentifier: %s, useRealClientApiKey: %b, apiKey: %s, callingContext: %d}", new Object[]{this.zzfj, this.zzhe, Boolean.valueOf(this.zzfg), this.zzfi, Integer.valueOf(this.zzhf)});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 1, this.versionCode);
        SafeParcelWriter.writeString(parcel, 2, this.zzfj, false);
        SafeParcelWriter.writeString(parcel, 3, this.zzhe, false);
        SafeParcelWriter.writeBoolean(parcel, 4, this.zzfg);
        SafeParcelWriter.writeInt(parcel, 5, this.zzhf);
        SafeParcelWriter.writeString(parcel, 6, this.zzfi, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
