package com.google.android.gms.games.video;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import com.google.android.gms.games.internal.zzd;

@Class(creator = "VideoCapabilitiesCreator")
@Reserved({1000})
public final class VideoCapabilities extends zzd {
    public static final Creator<VideoCapabilities> CREATOR = new zza();
    @Field(getter = "isCameraSupported", mo13990id = 1)
    private final boolean zzsy;
    @Field(getter = "isMicSupported", mo13990id = 2)
    private final boolean zzsz;
    @Field(getter = "isWriteStorageSupported", mo13990id = 3)
    private final boolean zzta;
    @Field(getter = "getSupportedCaptureModes", mo13990id = 4)
    private final boolean[] zztb;
    @Field(getter = "getSupportedQualityLevels", mo13990id = 5)
    private final boolean[] zztc;

    @Constructor
    public VideoCapabilities(@Param(mo13993id = 1) boolean z, @Param(mo13993id = 2) boolean z2, @Param(mo13993id = 3) boolean z3, @Param(mo13993id = 4) boolean[] zArr, @Param(mo13993id = 5) boolean[] zArr2) {
        this.zzsy = z;
        this.zzsz = z2;
        this.zzta = z3;
        this.zztb = zArr;
        this.zztc = zArr2;
    }

    public final boolean equals(Object obj) {
        if (!(obj instanceof VideoCapabilities)) {
            return false;
        }
        if (this == obj) {
            return true;
        }
        VideoCapabilities videoCapabilities = (VideoCapabilities) obj;
        return Objects.equal(videoCapabilities.getSupportedCaptureModes(), getSupportedCaptureModes()) && Objects.equal(videoCapabilities.getSupportedQualityLevels(), getSupportedQualityLevels()) && Objects.equal(Boolean.valueOf(videoCapabilities.isCameraSupported()), Boolean.valueOf(isCameraSupported())) && Objects.equal(Boolean.valueOf(videoCapabilities.isMicSupported()), Boolean.valueOf(isMicSupported())) && Objects.equal(Boolean.valueOf(videoCapabilities.isWriteStorageSupported()), Boolean.valueOf(isWriteStorageSupported()));
    }

    public final boolean[] getSupportedCaptureModes() {
        return this.zztb;
    }

    public final boolean[] getSupportedQualityLevels() {
        return this.zztc;
    }

    public final int hashCode() {
        return Objects.hashCode(getSupportedCaptureModes(), getSupportedQualityLevels(), Boolean.valueOf(isCameraSupported()), Boolean.valueOf(isMicSupported()), Boolean.valueOf(isWriteStorageSupported()));
    }

    public final boolean isCameraSupported() {
        return this.zzsy;
    }

    public final boolean isFullySupported(int i, int i2) {
        return this.zzsy && this.zzsz && this.zzta && supportsCaptureMode(i) && supportsQualityLevel(i2);
    }

    public final boolean isMicSupported() {
        return this.zzsz;
    }

    public final boolean isWriteStorageSupported() {
        return this.zzta;
    }

    public final boolean supportsCaptureMode(int i) {
        Preconditions.checkState(VideoConfiguration.isValidCaptureMode(i, false));
        return this.zztb[i];
    }

    public final boolean supportsQualityLevel(int i) {
        Preconditions.checkState(VideoConfiguration.isValidQualityLevel(i, false));
        return this.zztc[i];
    }

    public final String toString() {
        return Objects.toStringHelper(this).add("SupportedCaptureModes", getSupportedCaptureModes()).add("SupportedQualityLevels", getSupportedQualityLevels()).add("CameraSupported", Boolean.valueOf(isCameraSupported())).add("MicSupported", Boolean.valueOf(isMicSupported())).add("StorageWriteSupported", Boolean.valueOf(isWriteStorageSupported())).toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeBoolean(parcel, 1, isCameraSupported());
        SafeParcelWriter.writeBoolean(parcel, 2, isMicSupported());
        SafeParcelWriter.writeBoolean(parcel, 3, isWriteStorageSupported());
        SafeParcelWriter.writeBooleanArray(parcel, 4, getSupportedCaptureModes(), false);
        SafeParcelWriter.writeBooleanArray(parcel, 5, getSupportedQualityLevels(), false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
