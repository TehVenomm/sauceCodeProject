package com.google.android.gms.drive;

import android.os.Parcel;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.zzbp;

public abstract class zzv extends zza {
    private volatile transient boolean zzger = false;

    public void writeToParcel(Parcel parcel, int i) {
        zzbp.zzbg(!this.zzger);
        this.zzger = true;
        zzaj(parcel, i);
    }

    protected abstract void zzaj(Parcel parcel, int i);
}
