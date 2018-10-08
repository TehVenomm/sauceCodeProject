package com.google.android.gms.games.video;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;

public final class VideoConfiguration extends zza {
    public static final int CAPTURE_MODE_FILE = 0;
    public static final int CAPTURE_MODE_STREAM = 1;
    public static final int CAPTURE_MODE_UNKNOWN = -1;
    public static final Creator<VideoConfiguration> CREATOR = new zzb();
    public static final int NUM_CAPTURE_MODE = 2;
    public static final int NUM_QUALITY_LEVEL = 4;
    public static final int QUALITY_LEVEL_FULLHD = 3;
    public static final int QUALITY_LEVEL_HD = 1;
    public static final int QUALITY_LEVEL_SD = 0;
    public static final int QUALITY_LEVEL_UNKNOWN = -1;
    public static final int QUALITY_LEVEL_XHD = 2;
    private final int zzhpk;
    private final int zzhpr;
    private final String zzhps;
    private final String zzhpt;
    private final String zzhpu;
    private final String zzhpv;
    private final boolean zzhpw;

    public static final class Builder {
        private int zzhpk;
        private int zzhpr;
        private String zzhps = null;
        private String zzhpt = null;
        private String zzhpu = null;
        private String zzhpv = null;
        private boolean zzhpw = true;

        public Builder(int i, int i2) {
            this.zzhpr = i;
            this.zzhpk = i2;
        }

        public final VideoConfiguration build() {
            return new VideoConfiguration(this.zzhpr, this.zzhpk, null, null, null, null, this.zzhpw);
        }

        public final Builder setCaptureMode(int i) {
            this.zzhpk = i;
            return this;
        }

        public final Builder setQualityLevel(int i) {
            this.zzhpr = i;
            return this;
        }
    }

    public VideoConfiguration(int i, int i2, String str, String str2, String str3, String str4, boolean z) {
        boolean z2 = true;
        zzbp.zzbh(isValidQualityLevel(i, false));
        zzbp.zzbh(isValidCaptureMode(i2, false));
        this.zzhpr = i;
        this.zzhpk = i2;
        this.zzhpw = z;
        if (i2 == 1) {
            this.zzhpt = str2;
            this.zzhps = str;
            this.zzhpu = str3;
            this.zzhpv = str4;
            return;
        }
        zzbp.zzb(str2 == null, (Object) "Stream key should be null when not streaming");
        zzbp.zzb(str == null, (Object) "Stream url should be null when not streaming");
        zzbp.zzb(str3 == null, (Object) "Stream title should be null when not streaming");
        if (str4 != null) {
            z2 = false;
        }
        zzbp.zzb(z2, (Object) "Stream description should be null when not streaming");
        this.zzhpt = null;
        this.zzhps = null;
        this.zzhpu = null;
        this.zzhpv = null;
    }

    public static boolean isValidCaptureMode(int i, boolean z) {
        switch (i) {
            case -1:
                return z;
            case 0:
            case 1:
                return true;
            default:
                return false;
        }
    }

    public static boolean isValidQualityLevel(int i, boolean z) {
        switch (i) {
            case -1:
                return z;
            case 0:
            case 1:
            case 2:
            case 3:
                return true;
            default:
                return false;
        }
    }

    public final int getCaptureMode() {
        return this.zzhpk;
    }

    public final int getQualityLevel() {
        return this.zzhpr;
    }

    public final String getStreamUrl() {
        return this.zzhps;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, getQualityLevel());
        zzd.zzc(parcel, 2, getCaptureMode());
        zzd.zza(parcel, 3, getStreamUrl(), false);
        zzd.zza(parcel, 4, this.zzhpt, false);
        zzd.zza(parcel, 5, this.zzhpu, false);
        zzd.zza(parcel, 6, this.zzhpv, false);
        zzd.zza(parcel, 7, this.zzhpw);
        zzd.zzai(parcel, zze);
    }
}
