package com.google.android.gms.auth;

import android.accounts.Account;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.text.TextUtils;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public class AccountChangeEventsRequest extends zza {
    public static final Creator<AccountChangeEventsRequest> CREATOR = new zzb();
    private int mVersion;
    private Account zzdva;
    @Deprecated
    private String zzdxg;
    private int zzdxi;

    public AccountChangeEventsRequest() {
        this.mVersion = 1;
    }

    AccountChangeEventsRequest(int i, int i2, String str, Account account) {
        this.mVersion = i;
        this.zzdxi = i2;
        this.zzdxg = str;
        if (account != null || TextUtils.isEmpty(str)) {
            this.zzdva = account;
        } else {
            this.zzdva = new Account(str, "com.google");
        }
    }

    public Account getAccount() {
        return this.zzdva;
    }

    @Deprecated
    public String getAccountName() {
        return this.zzdxg;
    }

    public int getEventIndex() {
        return this.zzdxi;
    }

    public AccountChangeEventsRequest setAccount(Account account) {
        this.zzdva = account;
        return this;
    }

    @Deprecated
    public AccountChangeEventsRequest setAccountName(String str) {
        this.zzdxg = str;
        return this;
    }

    public AccountChangeEventsRequest setEventIndex(int i) {
        this.zzdxi = i;
        return this;
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.mVersion);
        zzd.zzc(parcel, 2, this.zzdxi);
        zzd.zza(parcel, 3, this.zzdxg, false);
        zzd.zza(parcel, 4, this.zzdva, i, false);
        zzd.zzai(parcel, zze);
    }
}
