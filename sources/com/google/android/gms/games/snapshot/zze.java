package com.google.android.gms.games.snapshot;

import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.data.BitmapTeleporter;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.games.internal.zzc;

public final class zze extends zzc implements SnapshotMetadataChange {
    public static final Creator<zze> CREATOR = new zzd();
    private final String zzdmz;
    private final Long zzhoj;
    private final Uri zzhol;
    private final Long zzhom;
    private BitmapTeleporter zzhon;

    zze() {
        this(null, null, null, null, null);
    }

    zze(String str, Long l, BitmapTeleporter bitmapTeleporter, Uri uri, Long l2) {
        boolean z = true;
        boolean z2 = false;
        this.zzdmz = str;
        this.zzhom = l;
        this.zzhon = bitmapTeleporter;
        this.zzhol = uri;
        this.zzhoj = l2;
        if (this.zzhon != null) {
            if (this.zzhol == null) {
                z2 = true;
            }
            zzbp.zza(z2, (Object) "Cannot set both a URI and an image");
        } else if (this.zzhol != null) {
            if (this.zzhon != null) {
                z = false;
            }
            zzbp.zza(z, (Object) "Cannot set both a URI and an image");
        }
    }

    public final Bitmap getCoverImage() {
        return this.zzhon == null ? null : this.zzhon.zzais();
    }

    public final String getDescription() {
        return this.zzdmz;
    }

    public final Long getPlayedTimeMillis() {
        return this.zzhom;
    }

    public final Long getProgressValue() {
        return this.zzhoj;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getDescription(), false);
        zzd.zza(parcel, 2, getPlayedTimeMillis(), false);
        zzd.zza(parcel, 4, this.zzhol, i, false);
        zzd.zza(parcel, 5, this.zzhon, i, false);
        zzd.zza(parcel, 6, getProgressValue(), false);
        zzd.zzai(parcel, zze);
    }

    public final BitmapTeleporter zzary() {
        return this.zzhon;
    }
}
