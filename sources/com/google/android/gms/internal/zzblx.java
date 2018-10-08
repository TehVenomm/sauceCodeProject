package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.drive.events.ChangeEvent;
import com.google.android.gms.drive.events.CompletionEvent;
import com.google.android.gms.drive.events.zzl;
import com.google.android.gms.drive.events.zzn;
import com.google.android.gms.drive.events.zzr;

public final class zzblx implements Creator<zzblw> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        ChangeEvent changeEvent = null;
        int zzd = zzb.zzd(parcel);
        int i = 0;
        CompletionEvent completionEvent = null;
        zzl zzl = null;
        com.google.android.gms.drive.events.zzb zzb = null;
        zzr zzr = null;
        zzn zzn = null;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 2:
                    i = zzb.zzg(parcel, readInt);
                    break;
                case 3:
                    changeEvent = (ChangeEvent) zzb.zza(parcel, readInt, ChangeEvent.CREATOR);
                    break;
                case 5:
                    completionEvent = (CompletionEvent) zzb.zza(parcel, readInt, CompletionEvent.CREATOR);
                    break;
                case 6:
                    zzl = (zzl) zzb.zza(parcel, readInt, zzl.CREATOR);
                    break;
                case 7:
                    zzb = (com.google.android.gms.drive.events.zzb) zzb.zza(parcel, readInt, com.google.android.gms.drive.events.zzb.CREATOR);
                    break;
                case 9:
                    zzr = (zzr) zzb.zza(parcel, readInt, zzr.CREATOR);
                    break;
                case 10:
                    zzn = (zzn) zzb.zza(parcel, readInt, zzn.CREATOR);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new zzblw(i, changeEvent, completionEvent, zzl, zzb, zzr, zzn);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzblw[i];
    }
}
