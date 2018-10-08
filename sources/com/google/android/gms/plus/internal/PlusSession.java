package com.google.android.gms.plus.internal;

import android.os.Bundle;
import android.os.Parcel;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable;
import com.google.android.gms.common.internal.zzu;
import java.util.Arrays;

public class PlusSession implements SafeParcelable {
    public static final zzh CREATOR = new zzh();
    private final String zzKw;
    private final String[] zzazg;
    private final String[] zzazh;
    private final String[] zzazi;
    private final String zzazj;
    private final String zzazk;
    private final String zzazl;
    private final String zzazm;
    private final PlusCommonExtras zzazn;
    private final int zzzH;

    PlusSession(int i, String str, String[] strArr, String[] strArr2, String[] strArr3, String str2, String str3, String str4, String str5, PlusCommonExtras plusCommonExtras) {
        this.zzzH = i;
        this.zzKw = str;
        this.zzazg = strArr;
        this.zzazh = strArr2;
        this.zzazi = strArr3;
        this.zzazj = str2;
        this.zzazk = str3;
        this.zzazl = str4;
        this.zzazm = str5;
        this.zzazn = plusCommonExtras;
    }

    public PlusSession(String str, String[] strArr, String[] strArr2, String[] strArr3, String str2, String str3, String str4, PlusCommonExtras plusCommonExtras) {
        this.zzzH = 1;
        this.zzKw = str;
        this.zzazg = strArr;
        this.zzazh = strArr2;
        this.zzazi = strArr3;
        this.zzazj = str2;
        this.zzazk = str3;
        this.zzazl = str4;
        this.zzazm = null;
        this.zzazn = plusCommonExtras;
    }

    public int describeContents() {
        return 0;
    }

    public boolean equals(Object obj) {
        if (!(obj instanceof PlusSession)) {
            return false;
        }
        PlusSession plusSession = (PlusSession) obj;
        return this.zzzH == plusSession.zzzH && zzu.equal(this.zzKw, plusSession.zzKw) && Arrays.equals(this.zzazg, plusSession.zzazg) && Arrays.equals(this.zzazh, plusSession.zzazh) && Arrays.equals(this.zzazi, plusSession.zzazi) && zzu.equal(this.zzazj, plusSession.zzazj) && zzu.equal(this.zzazk, plusSession.zzazk) && zzu.equal(this.zzazl, plusSession.zzazl) && zzu.equal(this.zzazm, plusSession.zzazm) && zzu.equal(this.zzazn, plusSession.zzazn);
    }

    public String getAccountName() {
        return this.zzKw;
    }

    public int getVersionCode() {
        return this.zzzH;
    }

    public int hashCode() {
        return zzu.hashCode(new Object[]{Integer.valueOf(this.zzzH), this.zzKw, this.zzazg, this.zzazh, this.zzazi, this.zzazj, this.zzazk, this.zzazl, this.zzazm, this.zzazn});
    }

    public String toString() {
        return zzu.zzq(this).zzg("versionCode", Integer.valueOf(this.zzzH)).zzg("accountName", this.zzKw).zzg("requestedScopes", this.zzazg).zzg("visibleActivities", this.zzazh).zzg("requiredFeatures", this.zzazi).zzg("packageNameForAuth", this.zzazj).zzg("callingPackageName", this.zzazk).zzg("applicationName", this.zzazl).zzg("extra", this.zzazn.toString()).toString();
    }

    public void writeToParcel(Parcel parcel, int i) {
        zzh.zza(this, parcel, i);
    }

    public String[] zzvC() {
        return this.zzazg;
    }

    public String[] zzvD() {
        return this.zzazh;
    }

    public String[] zzvE() {
        return this.zzazi;
    }

    public String zzvF() {
        return this.zzazj;
    }

    public String zzvG() {
        return this.zzazk;
    }

    public String zzvH() {
        return this.zzazl;
    }

    public String zzvI() {
        return this.zzazm;
    }

    public PlusCommonExtras zzvJ() {
        return this.zzazn;
    }

    public Bundle zzvK() {
        Bundle bundle = new Bundle();
        bundle.setClassLoader(PlusCommonExtras.class.getClassLoader());
        this.zzazn.zzt(bundle);
        return bundle;
    }
}
