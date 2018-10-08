package com.google.android.gms.drive.query.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import java.util.Locale;

public final class zzf extends zza {
    public static final Creator<zzf> CREATOR = new zzg();
    private String zzgke;
    private boolean zzgnj;

    public zzf(String str, boolean z) {
        this.zzgke = str;
        this.zzgnj = z;
    }

    public final String toString() {
        Locale locale = Locale.US;
        String str = this.zzgke;
        String str2 = this.zzgnj ? "ASC" : "DESC";
        return String.format(locale, "FieldWithSortOrder[%s %s]", new Object[]{str, str2});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzgke, false);
        zzd.zza(parcel, 2, this.zzgnj);
        zzd.zzai(parcel, zze);
    }
}
