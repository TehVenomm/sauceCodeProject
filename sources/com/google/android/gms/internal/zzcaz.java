package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import java.util.Iterator;

public final class zzcaz extends zza implements Iterable<String> {
    public static final Creator<zzcaz> CREATOR = new zzcbb();
    private final Bundle zzing;

    zzcaz(Bundle bundle) {
        this.zzing = bundle;
    }

    final Object get(String str) {
        return this.zzing.get(str);
    }

    public final Iterator<String> iterator() {
        return new zzcba(this);
    }

    public final int size() {
        return this.zzing.size();
    }

    public final String toString() {
        return this.zzing.toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, zzaxy(), false);
        zzd.zzai(parcel, zze);
    }

    public final Bundle zzaxy() {
        return new Bundle(this.zzing);
    }
}
