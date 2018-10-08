package com.google.android.gms.games.video;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.games.internal.zzc;
import java.util.Arrays;

public final class VideoCapabilities extends zzc {
    public static final Creator<VideoCapabilities> CREATOR = new zza();
    private final boolean zzhpm;
    private final boolean zzhpn;
    private final boolean zzhpo;
    private final boolean[] zzhpp;
    private final boolean[] zzhpq;

    public VideoCapabilities(boolean z, boolean z2, boolean z3, boolean[] zArr, boolean[] zArr2) {
        this.zzhpm = z;
        this.zzhpn = z2;
        this.zzhpo = z3;
        this.zzhpp = zArr;
        this.zzhpq = zArr2;
    }

    public final boolean equals(Object obj) {
        if (!(obj instanceof VideoCapabilities)) {
            return false;
        }
        if (this == obj) {
            return true;
        }
        VideoCapabilities videoCapabilities = (VideoCapabilities) obj;
        return zzbf.equal(videoCapabilities.getSupportedCaptureModes(), getSupportedCaptureModes()) && zzbf.equal(videoCapabilities.getSupportedQualityLevels(), getSupportedQualityLevels()) && zzbf.equal(Boolean.valueOf(videoCapabilities.isCameraSupported()), Boolean.valueOf(isCameraSupported())) && zzbf.equal(Boolean.valueOf(videoCapabilities.isMicSupported()), Boolean.valueOf(isMicSupported())) && zzbf.equal(Boolean.valueOf(videoCapabilities.isWriteStorageSupported()), Boolean.valueOf(isWriteStorageSupported()));
    }

    public final boolean[] getSupportedCaptureModes() {
        return this.zzhpp;
    }

    public final boolean[] getSupportedQualityLevels() {
        return this.zzhpq;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{getSupportedCaptureModes(), getSupportedQualityLevels(), Boolean.valueOf(isCameraSupported()), Boolean.valueOf(isMicSupported()), Boolean.valueOf(isWriteStorageSupported())});
    }

    public final boolean isCameraSupported() {
        return this.zzhpm;
    }

    public final boolean isFullySupported(int i, int i2) {
        return this.zzhpm && this.zzhpn && this.zzhpo && supportsCaptureMode(i) && supportsQualityLevel(i2);
    }

    public final boolean isMicSupported() {
        return this.zzhpn;
    }

    public final boolean isWriteStorageSupported() {
        return this.zzhpo;
    }

    public final boolean supportsCaptureMode(int i) {
        zzbp.zzbg(VideoConfiguration.isValidCaptureMode(i, false));
        return this.zzhpp[i];
    }

    public final boolean supportsQualityLevel(int i) {
        zzbp.zzbg(VideoConfiguration.isValidQualityLevel(i, false));
        return this.zzhpq[i];
    }

    public final String toString() {
        return zzbf.zzt(this).zzg("SupportedCaptureModes", getSupportedCaptureModes()).zzg("SupportedQualityLevels", getSupportedQualityLevels()).zzg("CameraSupported", Boolean.valueOf(isCameraSupported())).zzg("MicSupported", Boolean.valueOf(isMicSupported())).zzg("StorageWriteSupported", Boolean.valueOf(isWriteStorageSupported())).toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, isCameraSupported());
        zzd.zza(parcel, 2, isMicSupported());
        zzd.zza(parcel, 3, isWriteStorageSupported());
        zzd.zza(parcel, 4, getSupportedCaptureModes(), false);
        zzd.zza(parcel, 5, getSupportedQualityLevels(), false);
        zzd.zzai(parcel, zze);
    }
}
