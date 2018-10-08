package com.google.android.gms.games.internal.player;

import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.games.internal.zzc;
import java.util.Arrays;

public final class zzb extends zzc implements zza {
    public static final Creator<zzb> CREATOR = new zzc();
    private final String zzhjd;
    private final String zzhje;
    private final long zzhjf;
    private final Uri zzhjg;
    private final Uri zzhjh;
    private final Uri zzhji;

    public zzb(zza zza) {
        this.zzhjd = zza.zzari();
        this.zzhje = zza.zzarj();
        this.zzhjf = zza.zzark();
        this.zzhjg = zza.zzarl();
        this.zzhjh = zza.zzarm();
        this.zzhji = zza.zzarn();
    }

    zzb(String str, String str2, long j, Uri uri, Uri uri2, Uri uri3) {
        this.zzhjd = str;
        this.zzhje = str2;
        this.zzhjf = j;
        this.zzhjg = uri;
        this.zzhjh = uri2;
        this.zzhji = uri3;
    }

    static int zza(zza zza) {
        return Arrays.hashCode(new Object[]{zza.zzari(), zza.zzarj(), Long.valueOf(zza.zzark()), zza.zzarl(), zza.zzarm(), zza.zzarn()});
    }

    static boolean zza(zza zza, Object obj) {
        if (!(obj instanceof zza)) {
            return false;
        }
        if (zza == obj) {
            return true;
        }
        zza zza2 = (zza) obj;
        return zzbf.equal(zza2.zzari(), zza.zzari()) && zzbf.equal(zza2.zzarj(), zza.zzarj()) && zzbf.equal(Long.valueOf(zza2.zzark()), Long.valueOf(zza.zzark())) && zzbf.equal(zza2.zzarl(), zza.zzarl()) && zzbf.equal(zza2.zzarm(), zza.zzarm()) && zzbf.equal(zza2.zzarn(), zza.zzarn());
    }

    static String zzb(zza zza) {
        return zzbf.zzt(zza).zzg("GameId", zza.zzari()).zzg("GameName", zza.zzarj()).zzg("ActivityTimestampMillis", Long.valueOf(zza.zzark())).zzg("GameIconUri", zza.zzarl()).zzg("GameHiResUri", zza.zzarm()).zzg("GameFeaturedUri", zza.zzarn()).toString();
    }

    public final boolean equals(Object obj) {
        return zza(this, obj);
    }

    public final /* bridge */ /* synthetic */ Object freeze() {
        if (this != null) {
            return this;
        }
        throw null;
    }

    public final int hashCode() {
        return zza(this);
    }

    public final boolean isDataValid() {
        return true;
    }

    public final String toString() {
        return zzb(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzhjd, false);
        zzd.zza(parcel, 2, this.zzhje, false);
        zzd.zza(parcel, 3, this.zzhjf);
        zzd.zza(parcel, 4, this.zzhjg, i, false);
        zzd.zza(parcel, 5, this.zzhjh, i, false);
        zzd.zza(parcel, 6, this.zzhji, i, false);
        zzd.zzai(parcel, zze);
    }

    public final String zzari() {
        return this.zzhjd;
    }

    public final String zzarj() {
        return this.zzhje;
    }

    public final long zzark() {
        return this.zzhjf;
    }

    public final Uri zzarl() {
        return this.zzhjg;
    }

    public final Uri zzarm() {
        return this.zzhjh;
    }

    public final Uri zzarn() {
        return this.zzhji;
    }
}
