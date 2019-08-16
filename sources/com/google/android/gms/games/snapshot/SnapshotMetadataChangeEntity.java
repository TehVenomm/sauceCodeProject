package com.google.android.gms.games.snapshot;

import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.apps.common.proguard.UsedByReflection;
import com.google.android.gms.common.data.BitmapTeleporter;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import com.google.android.gms.games.internal.zzd;

@UsedByReflection("GamesClientImpl.java")
@Class(creator = "SnapshotMetadataChangeCreator")
@Reserved({1000})
public final class SnapshotMetadataChangeEntity extends zzd implements SnapshotMetadataChange {
    public static final Creator<SnapshotMetadataChangeEntity> CREATOR = new zzc();
    @Field(getter = "getDescription", mo13990id = 1)
    private final String description;
    @Field(getter = "getProgressValue", mo13990id = 6)
    private final Long zzru;
    @Field(getter = "getCoverImageUri", mo13990id = 4)
    private final Uri zzrw;
    @Field(getter = "getPlayedTimeMillis", mo13990id = 2)
    private final Long zzrx;
    @Field(getter = "getCoverImageTeleporter", mo13990id = 5)
    private BitmapTeleporter zzry;

    SnapshotMetadataChangeEntity() {
        this(null, null, null, null, null);
    }

    @Constructor
    SnapshotMetadataChangeEntity(@Param(mo13993id = 1) String str, @Param(mo13993id = 2) Long l, @Param(mo13993id = 5) BitmapTeleporter bitmapTeleporter, @Param(mo13993id = 4) Uri uri, @Param(mo13993id = 6) Long l2) {
        boolean z = true;
        boolean z2 = false;
        this.description = str;
        this.zzrx = l;
        this.zzry = bitmapTeleporter;
        this.zzrw = uri;
        this.zzru = l2;
        if (this.zzry != null) {
            if (this.zzrw == null) {
                z2 = true;
            }
            Preconditions.checkState(z2, "Cannot set both a URI and an image");
        } else if (this.zzrw != null) {
            if (this.zzry != null) {
                z = false;
            }
            Preconditions.checkState(z, "Cannot set both a URI and an image");
        }
    }

    public final Bitmap getCoverImage() {
        if (this.zzry == null) {
            return null;
        }
        return this.zzry.get();
    }

    public final String getDescription() {
        return this.description;
    }

    public final Long getPlayedTimeMillis() {
        return this.zzrx;
    }

    public final Long getProgressValue() {
        return this.zzru;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeString(parcel, 1, getDescription(), false);
        SafeParcelWriter.writeLongObject(parcel, 2, getPlayedTimeMillis(), false);
        SafeParcelWriter.writeParcelable(parcel, 4, this.zzrw, i, false);
        SafeParcelWriter.writeParcelable(parcel, 5, this.zzry, i, false);
        SafeParcelWriter.writeLongObject(parcel, 6, getProgressValue(), false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }

    public final BitmapTeleporter zzdt() {
        return this.zzry;
    }
}
