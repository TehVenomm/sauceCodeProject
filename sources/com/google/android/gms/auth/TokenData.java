package com.google.android.gms.auth;

import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import java.util.Arrays;
import java.util.List;

public class TokenData extends zza implements ReflectedParcelable {
    public static final Creator<TokenData> CREATOR = new zzj();
    private int zzdxt;
    private final String zzdxu;
    private final Long zzdxv;
    private final boolean zzdxw;
    private final boolean zzdxx;
    private final List<String> zzdxy;

    TokenData(int i, String str, Long l, boolean z, boolean z2, List<String> list) {
        this.zzdxt = i;
        this.zzdxu = zzbp.zzgf(str);
        this.zzdxv = l;
        this.zzdxw = z;
        this.zzdxx = z2;
        this.zzdxy = list;
    }

    @Nullable
    public static TokenData zzd(Bundle bundle, String str) {
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
        return TextUtils.equals(this.zzdxu, tokenData.zzdxu) && zzbf.equal(this.zzdxv, tokenData.zzdxv) && this.zzdxw == tokenData.zzdxw && this.zzdxx == tokenData.zzdxx && zzbf.equal(this.zzdxy, tokenData.zzdxy);
    }

    public final String getToken() {
        return this.zzdxu;
    }

    public int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzdxu, this.zzdxv, Boolean.valueOf(this.zzdxw), Boolean.valueOf(this.zzdxx), this.zzdxy});
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, this.zzdxu, false);
        zzd.zza(parcel, 3, this.zzdxv, false);
        zzd.zza(parcel, 4, this.zzdxw);
        zzd.zza(parcel, 5, this.zzdxx);
        zzd.zzb(parcel, 6, this.zzdxy, false);
        zzd.zzai(parcel, zze);
    }
}
