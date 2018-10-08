package com.google.android.gms.games.internal.player;

import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.games.internal.zzc;
import java.util.Arrays;

public final class StockProfileImageEntity extends zzc implements StockProfileImage {
    public static final Creator<StockProfileImageEntity> CREATOR = new zzf();
    private final String zzhkq;
    private final Uri zzhkr;

    public StockProfileImageEntity(String str, Uri uri) {
        this.zzhkq = str;
        this.zzhkr = uri;
    }

    public final boolean equals(Object obj) {
        if (!(obj instanceof StockProfileImage)) {
            return false;
        }
        if (obj == this) {
            return true;
        }
        StockProfileImage stockProfileImage = (StockProfileImage) obj;
        return zzbf.equal(this.zzhkq, stockProfileImage.getImageUrl()) && zzbf.equal(this.zzhkr, stockProfileImage.zzaro());
    }

    public final /* bridge */ /* synthetic */ Object freeze() {
        if (this != null) {
            return this;
        }
        throw null;
    }

    public final String getImageUrl() {
        return this.zzhkq;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzhkq, this.zzhkr});
    }

    public final boolean isDataValid() {
        return true;
    }

    public final String toString() {
        return zzbf.zzt(this).zzg("ImageId", this.zzhkq).zzg("ImageUri", this.zzhkr).toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getImageUrl(), false);
        zzd.zza(parcel, 2, this.zzhkr, i, false);
        zzd.zzai(parcel, zze);
    }

    public final Uri zzaro() {
        return this.zzhkr;
    }
}
