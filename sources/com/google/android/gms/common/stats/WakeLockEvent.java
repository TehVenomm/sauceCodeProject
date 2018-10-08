package com.google.android.gms.common.stats;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.text.TextUtils;
import com.google.android.gms.common.internal.safeparcel.zzd;
import java.util.List;

public final class WakeLockEvent extends StatsEvent {
    public static final Creator<WakeLockEvent> CREATOR = new zzd();
    private final long mTimeout;
    private int zzdxt;
    private final long zzfxr;
    private int zzfxs;
    private final String zzfxt;
    private final String zzfxu;
    private final String zzfxv;
    private final int zzfxw;
    private final List<String> zzfxx;
    private final String zzfxy;
    private final long zzfxz;
    private int zzfya;
    private final String zzfyb;
    private final float zzfyc;
    private long zzfyd;

    WakeLockEvent(int i, long j, int i2, String str, int i3, List<String> list, String str2, long j2, int i4, String str3, String str4, float f, long j3, String str5) {
        this.zzdxt = i;
        this.zzfxr = j;
        this.zzfxs = i2;
        this.zzfxt = str;
        this.zzfxu = str3;
        this.zzfxv = str5;
        this.zzfxw = i3;
        this.zzfyd = -1;
        this.zzfxx = list;
        this.zzfxy = str2;
        this.zzfxz = j2;
        this.zzfya = i4;
        this.zzfyb = str4;
        this.zzfyc = f;
        this.mTimeout = j3;
    }

    public WakeLockEvent(long j, int i, String str, int i2, List<String> list, String str2, long j2, int i3, String str3, String str4, float f, long j3, String str5) {
        this(2, j, i, str, i2, list, str2, j2, i3, str3, str4, f, j3, str5);
    }

    public final int getEventType() {
        return this.zzfxs;
    }

    public final long getTimeMillis() {
        return this.zzfxr;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, getTimeMillis());
        zzd.zza(parcel, 4, this.zzfxt, false);
        zzd.zzc(parcel, 5, this.zzfxw);
        zzd.zzb(parcel, 6, this.zzfxx, false);
        zzd.zza(parcel, 8, this.zzfxz);
        zzd.zza(parcel, 10, this.zzfxu, false);
        zzd.zzc(parcel, 11, getEventType());
        zzd.zza(parcel, 12, this.zzfxy, false);
        zzd.zza(parcel, 13, this.zzfyb, false);
        zzd.zzc(parcel, 14, this.zzfya);
        zzd.zza(parcel, 15, this.zzfyc);
        zzd.zza(parcel, 16, this.mTimeout);
        zzd.zza(parcel, 17, this.zzfxv, false);
        zzd.zzai(parcel, zze);
    }

    public final long zzakz() {
        return this.zzfyd;
    }

    public final String zzala() {
        String str = this.zzfxt;
        int i = this.zzfxw;
        String join = this.zzfxx == null ? "" : TextUtils.join(",", this.zzfxx);
        int i2 = this.zzfya;
        String str2 = this.zzfxu == null ? "" : this.zzfxu;
        String str3 = this.zzfyb == null ? "" : this.zzfyb;
        float f = this.zzfyc;
        String str4 = this.zzfxv == null ? "" : this.zzfxv;
        return new StringBuilder(((((((((((((String.valueOf("\t").length() + 37) + String.valueOf(str).length()) + String.valueOf("\t").length()) + String.valueOf("\t").length()) + String.valueOf(join).length()) + String.valueOf("\t").length()) + String.valueOf("\t").length()) + String.valueOf(str2).length()) + String.valueOf("\t").length()) + String.valueOf(str3).length()) + String.valueOf("\t").length()) + String.valueOf("\t").length()) + String.valueOf(str4).length()).append("\t").append(str).append("\t").append(i).append("\t").append(join).append("\t").append(i2).append("\t").append(str2).append("\t").append(str3).append("\t").append(f).append("\t").append(str4).toString();
    }
}
