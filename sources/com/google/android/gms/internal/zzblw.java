package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.events.ChangeEvent;
import com.google.android.gms.drive.events.CompletionEvent;
import com.google.android.gms.drive.events.DriveEvent;
import com.google.android.gms.drive.events.zzb;
import com.google.android.gms.drive.events.zzl;
import com.google.android.gms.drive.events.zzn;
import com.google.android.gms.drive.events.zzr;

public final class zzblw extends zza {
    public static final Creator<zzblw> CREATOR = new zzblx();
    private int zzfxs;
    private ChangeEvent zzgje;
    private CompletionEvent zzgjf;
    private zzl zzgjg;
    private zzb zzgjh;
    private zzr zzgji;
    private zzn zzgjj;

    zzblw(int i, ChangeEvent changeEvent, CompletionEvent completionEvent, zzl zzl, zzb zzb, zzr zzr, zzn zzn) {
        this.zzfxs = i;
        this.zzgje = changeEvent;
        this.zzgjf = completionEvent;
        this.zzgjg = zzl;
        this.zzgjh = zzb;
        this.zzgji = zzr;
        this.zzgjj = zzn;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 2, this.zzfxs);
        zzd.zza(parcel, 3, this.zzgje, i, false);
        zzd.zza(parcel, 5, this.zzgjf, i, false);
        zzd.zza(parcel, 6, this.zzgjg, i, false);
        zzd.zza(parcel, 7, this.zzgjh, i, false);
        zzd.zza(parcel, 9, this.zzgji, i, false);
        zzd.zza(parcel, 10, this.zzgjj, i, false);
        zzd.zzai(parcel, zze);
    }

    public final DriveEvent zzann() {
        switch (this.zzfxs) {
            case 1:
                return this.zzgje;
            case 2:
                return this.zzgjf;
            case 3:
                return this.zzgjg;
            case 4:
                return this.zzgjh;
            case 7:
                return this.zzgji;
            case 8:
                return this.zzgjj;
            default:
                throw new IllegalStateException("Unexpected event type " + this.zzfxs);
        }
    }
}
