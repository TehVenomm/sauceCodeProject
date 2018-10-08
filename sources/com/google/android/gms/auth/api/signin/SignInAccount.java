package com.google.android.gms.auth.api.signin;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;

public class SignInAccount extends zza implements ReflectedParcelable {
    public static final Creator<SignInAccount> CREATOR = new zze();
    @Deprecated
    private String zzdnd;
    @Deprecated
    private String zzebx;
    private GoogleSignInAccount zzecq;

    SignInAccount(String str, GoogleSignInAccount googleSignInAccount, String str2) {
        this.zzecq = googleSignInAccount;
        this.zzebx = zzbp.zzh(str, "8.3 and 8.4 SDKs require non-null email");
        this.zzdnd = zzbp.zzh(str2, "8.3 and 8.4 SDKs require non-null userId");
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 4, this.zzebx, false);
        zzd.zza(parcel, 7, this.zzecq, i, false);
        zzd.zza(parcel, 8, this.zzdnd, false);
        zzd.zzai(parcel, zze);
    }

    public final GoogleSignInAccount zzaah() {
        return this.zzecq;
    }
}
