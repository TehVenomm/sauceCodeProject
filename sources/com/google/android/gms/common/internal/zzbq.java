package com.google.android.gms.common.internal;

import android.accounts.Account;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzbq extends zza {
    public static final Creator<zzbq> CREATOR = new zzbr();
    private final Account zzdva;
    private int zzdxt;
    private final int zzfvr;
    private final GoogleSignInAccount zzfvs;

    zzbq(int i, Account account, int i2, GoogleSignInAccount googleSignInAccount) {
        this.zzdxt = i;
        this.zzdva = account;
        this.zzfvr = i2;
        this.zzfvs = googleSignInAccount;
    }

    public zzbq(Account account, int i, GoogleSignInAccount googleSignInAccount) {
        this(2, account, i, googleSignInAccount);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, this.zzdva, i, false);
        zzd.zzc(parcel, 3, this.zzfvr);
        zzd.zza(parcel, 4, this.zzfvs, i, false);
        zzd.zzai(parcel, zze);
    }
}
