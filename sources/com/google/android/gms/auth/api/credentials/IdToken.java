package com.google.android.gms.auth.api.credentials;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.NonNull;
import android.text.TextUtils;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;

public final class IdToken extends zza implements ReflectedParcelable {
    public static final Creator<IdToken> CREATOR = new zzh();
    @NonNull
    private final String zzdzs;
    @NonNull
    private final String zzeax;

    public IdToken(@NonNull String str, @NonNull String str2) {
        boolean z = true;
        zzbp.zzb(!TextUtils.isEmpty(str), (Object) "account type string cannot be null or empty");
        if (TextUtils.isEmpty(str2)) {
            z = false;
        }
        zzbp.zzb(z, (Object) "id token string cannot be null or empty");
        this.zzdzs = str;
        this.zzeax = str2;
    }

    @NonNull
    public final String getAccountType() {
        return this.zzdzs;
    }

    @NonNull
    public final String getIdToken() {
        return this.zzeax;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getAccountType(), false);
        zzd.zza(parcel, 2, getIdToken(), false);
        zzd.zzai(parcel, zze);
    }
}
