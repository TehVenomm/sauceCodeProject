package com.google.android.gms.drive;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.text.TextUtils;
import com.facebook.share.internal.ShareConstants;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zze;
import java.util.Set;
import java.util.regex.Pattern;

public class DriveSpace extends zza implements ReflectedParcelable {
    public static final Creator<DriveSpace> CREATOR = new zzl();
    public static final DriveSpace zzgdn = new DriveSpace("DRIVE");
    public static final DriveSpace zzgdo = new DriveSpace("APP_DATA_FOLDER");
    public static final DriveSpace zzgdp = new DriveSpace(ShareConstants.PHOTOS);
    private static Set<DriveSpace> zzgdq = zze.zza(zzgdn, zzgdo, zzgdp);
    private static String zzgdr = TextUtils.join(",", zzgdq.toArray());
    private static final Pattern zzgds = Pattern.compile("[A-Z0-9_]*");
    private final String mName;

    DriveSpace(String str) {
        this.mName = (String) zzbp.zzu(str);
    }

    public boolean equals(Object obj) {
        return (obj == null || obj.getClass() != DriveSpace.class) ? false : this.mName.equals(((DriveSpace) obj).mName);
    }

    public int hashCode() {
        return 1247068382 ^ this.mName.hashCode();
    }

    public String toString() {
        return this.mName;
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.mName, false);
        zzd.zzai(parcel, zze);
    }
}
