package com.google.android.gms.common.internal;

import android.accounts.Account;
import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.zzc;
import com.google.android.gms.common.zze;

public final class zzy extends zza {
    public static final Creator<zzy> CREATOR = new zzz();
    private int version;
    private int zzftw;
    private int zzftx;
    String zzfty;
    IBinder zzftz;
    Scope[] zzfua;
    Bundle zzfub;
    Account zzfuc;
    zzc[] zzfud;

    public zzy(int i) {
        this.version = 3;
        this.zzftx = zze.GOOGLE_PLAY_SERVICES_VERSION_CODE;
        this.zzftw = i;
    }

    zzy(int i, int i2, int i3, String str, IBinder iBinder, Scope[] scopeArr, Bundle bundle, Account account, zzc[] zzcArr) {
        Account account2 = null;
        this.version = i;
        this.zzftw = i2;
        this.zzftx = i3;
        if ("com.google.android.gms".equals(str)) {
            this.zzfty = "com.google.android.gms";
        } else {
            this.zzfty = str;
        }
        if (i < 2) {
            if (iBinder != null) {
                zzam zzao;
                if (iBinder != null) {
                    IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.common.internal.IAccountAccessor");
                    zzao = queryLocalInterface instanceof zzam ? (zzam) queryLocalInterface : new zzao(iBinder);
                }
                account2 = zza.zza(zzao);
            }
            this.zzfuc = account2;
        } else {
            this.zzftz = iBinder;
            this.zzfuc = account;
        }
        this.zzfua = scopeArr;
        this.zzfub = bundle;
        this.zzfud = zzcArr;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.version);
        zzd.zzc(parcel, 2, this.zzftw);
        zzd.zzc(parcel, 3, this.zzftx);
        zzd.zza(parcel, 4, this.zzfty, false);
        zzd.zza(parcel, 5, this.zzftz, false);
        zzd.zza(parcel, 6, this.zzfua, i, false);
        zzd.zza(parcel, 7, this.zzfub, false);
        zzd.zza(parcel, 8, this.zzfuc, i, false);
        zzd.zza(parcel, 10, this.zzfud, i, false);
        zzd.zzai(parcel, zze);
    }
}
