package com.google.android.gms.plus.internal;

import android.os.Bundle;
import android.os.Parcel;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable;
import com.google.android.gms.common.internal.safeparcel.zzc;
import com.google.android.gms.common.internal.zzu;

public class PlusCommonExtras implements SafeParcelable {
    public static final zzf CREATOR = new zzf();
    public static String TAG = "PlusCommonExtras";
    private String zzazd;
    private String zzaze;
    private final int zzzH;

    public PlusCommonExtras() {
        this.zzzH = 1;
        this.zzazd = "";
        this.zzaze = "";
    }

    PlusCommonExtras(int i, String str, String str2) {
        this.zzzH = i;
        this.zzazd = str;
        this.zzaze = str2;
    }

    public int describeContents() {
        return 0;
    }

    public boolean equals(Object obj) {
        if (!(obj instanceof PlusCommonExtras)) {
            return false;
        }
        PlusCommonExtras plusCommonExtras = (PlusCommonExtras) obj;
        return this.zzzH == plusCommonExtras.zzzH && zzu.equal(this.zzazd, plusCommonExtras.zzazd) && zzu.equal(this.zzaze, plusCommonExtras.zzaze);
    }

    public int getVersionCode() {
        return this.zzzH;
    }

    public int hashCode() {
        return zzu.hashCode(new Object[]{Integer.valueOf(this.zzzH), this.zzazd, this.zzaze});
    }

    public String toString() {
        return zzu.zzq(this).zzg("versionCode", Integer.valueOf(this.zzzH)).zzg("Gpsrc", this.zzazd).zzg("ClientCallingPackage", this.zzaze).toString();
    }

    public void writeToParcel(Parcel parcel, int i) {
        zzf.zza(this, parcel, i);
    }

    public void zzt(Bundle bundle) {
        bundle.putByteArray("android.gms.plus.internal.PlusCommonExtras.extraPlusCommon", zzc.zza(this));
    }

    public String zzvA() {
        return this.zzazd;
    }

    public String zzvB() {
        return this.zzaze;
    }
}
