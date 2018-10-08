package com.google.android.gms.common.api;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;

public final class Scope extends zza implements ReflectedParcelable {
    public static final Creator<Scope> CREATOR = new zzg();
    private int zzdxt;
    private final String zzfho;

    Scope(int i, String str) {
        zzbp.zzh(str, "scopeUri must not be null or empty");
        this.zzdxt = i;
        this.zzfho = str;
    }

    public Scope(String str) {
        this(1, str);
    }

    public final boolean equals(Object obj) {
        return this == obj ? true : !(obj instanceof Scope) ? false : this.zzfho.equals(((Scope) obj).zzfho);
    }

    public final int hashCode() {
        return this.zzfho.hashCode();
    }

    public final String toString() {
        return this.zzfho;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, this.zzfho, false);
        zzd.zzai(parcel, zze);
    }

    public final String zzafs() {
        return this.zzfho;
    }
}
