package com.google.android.gms.games.video;

import android.os.Bundle;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;

public final class CaptureState {
    private final boolean zzark;
    private final boolean zzeqa;
    private final boolean zzhpj;
    private final int zzhpk;
    private final int zzhpl;

    private CaptureState(boolean z, int i, int i2, boolean z2, boolean z3) {
        zzbp.zzbh(VideoConfiguration.isValidCaptureMode(i, true));
        zzbp.zzbh(VideoConfiguration.isValidQualityLevel(i2, true));
        this.zzhpj = z;
        this.zzhpk = i;
        this.zzhpl = i2;
        this.zzeqa = z2;
        this.zzark = z3;
    }

    public static CaptureState zzo(Bundle bundle) {
        return (bundle == null || bundle.get("IsCapturing") == null) ? null : new CaptureState(bundle.getBoolean("IsCapturing", false), bundle.getInt("CaptureMode", -1), bundle.getInt("CaptureQuality", -1), bundle.getBoolean("IsOverlayVisible", false), bundle.getBoolean("IsPaused", false));
    }

    public final int getCaptureMode() {
        return this.zzhpk;
    }

    public final int getCaptureQuality() {
        return this.zzhpl;
    }

    public final boolean isCapturing() {
        return this.zzhpj;
    }

    public final boolean isOverlayVisible() {
        return this.zzeqa;
    }

    public final boolean isPaused() {
        return this.zzark;
    }

    public final String toString() {
        return zzbf.zzt(this).zzg("IsCapturing", Boolean.valueOf(this.zzhpj)).zzg("CaptureMode", Integer.valueOf(this.zzhpk)).zzg("CaptureQuality", Integer.valueOf(this.zzhpl)).zzg("IsOverlayVisible", Boolean.valueOf(this.zzeqa)).zzg("IsPaused", Boolean.valueOf(this.zzark)).toString();
    }
}
