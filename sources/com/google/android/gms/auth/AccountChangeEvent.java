package com.google.android.gms.auth;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import java.util.Arrays;

public class AccountChangeEvent extends zza {
    public static final Creator<AccountChangeEvent> CREATOR = new zza();
    private int mVersion;
    private long zzdxf;
    private String zzdxg;
    private int zzdxh;
    private int zzdxi;
    private String zzdxj;

    AccountChangeEvent(int i, long j, String str, int i2, int i3, String str2) {
        this.mVersion = i;
        this.zzdxf = j;
        this.zzdxg = (String) zzbp.zzu(str);
        this.zzdxh = i2;
        this.zzdxi = i3;
        this.zzdxj = str2;
    }

    public AccountChangeEvent(long j, String str, int i, int i2, String str2) {
        this.mVersion = 1;
        this.zzdxf = j;
        this.zzdxg = (String) zzbp.zzu(str);
        this.zzdxh = i;
        this.zzdxi = i2;
        this.zzdxj = str2;
    }

    public boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof AccountChangeEvent)) {
            return false;
        }
        AccountChangeEvent accountChangeEvent = (AccountChangeEvent) obj;
        return this.mVersion == accountChangeEvent.mVersion && this.zzdxf == accountChangeEvent.zzdxf && zzbf.equal(this.zzdxg, accountChangeEvent.zzdxg) && this.zzdxh == accountChangeEvent.zzdxh && this.zzdxi == accountChangeEvent.zzdxi && zzbf.equal(this.zzdxj, accountChangeEvent.zzdxj);
    }

    public String getAccountName() {
        return this.zzdxg;
    }

    public String getChangeData() {
        return this.zzdxj;
    }

    public int getChangeType() {
        return this.zzdxh;
    }

    public int getEventIndex() {
        return this.zzdxi;
    }

    public int hashCode() {
        return Arrays.hashCode(new Object[]{Integer.valueOf(this.mVersion), Long.valueOf(this.zzdxf), this.zzdxg, Integer.valueOf(this.zzdxh), Integer.valueOf(this.zzdxi), this.zzdxj});
    }

    public String toString() {
        String str = "UNKNOWN";
        switch (this.zzdxh) {
            case 1:
                str = "ADDED";
                break;
            case 2:
                str = "REMOVED";
                break;
            case 3:
                str = "RENAMED_FROM";
                break;
            case 4:
                str = "RENAMED_TO";
                break;
        }
        String str2 = this.zzdxg;
        String str3 = this.zzdxj;
        return new StringBuilder(((String.valueOf(str2).length() + 91) + String.valueOf(str).length()) + String.valueOf(str3).length()).append("AccountChangeEvent {accountName = ").append(str2).append(", changeType = ").append(str).append(", changeData = ").append(str3).append(", eventIndex = ").append(this.zzdxi).append("}").toString();
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.mVersion);
        zzd.zza(parcel, 2, this.zzdxf);
        zzd.zza(parcel, 3, this.zzdxg, false);
        zzd.zzc(parcel, 4, this.zzdxh);
        zzd.zzc(parcel, 5, this.zzdxi);
        zzd.zza(parcel, 6, this.zzdxj, false);
        zzd.zzai(parcel, zze);
    }
}
