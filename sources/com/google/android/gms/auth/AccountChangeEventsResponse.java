package com.google.android.gms.auth;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import java.util.List;

public class AccountChangeEventsResponse extends zza {
    public static final Creator<AccountChangeEventsResponse> CREATOR = new zzc();
    private int mVersion;
    private List<AccountChangeEvent> zzaom;

    AccountChangeEventsResponse(int i, List<AccountChangeEvent> list) {
        this.mVersion = i;
        this.zzaom = (List) zzbp.zzu(list);
    }

    public AccountChangeEventsResponse(List<AccountChangeEvent> list) {
        this.mVersion = 1;
        this.zzaom = (List) zzbp.zzu(list);
    }

    public List<AccountChangeEvent> getEvents() {
        return this.zzaom;
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.mVersion);
        zzd.zzc(parcel, 2, this.zzaom, false);
        zzd.zzai(parcel, zze);
    }
}
