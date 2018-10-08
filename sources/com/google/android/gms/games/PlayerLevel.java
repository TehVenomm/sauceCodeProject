package com.google.android.gms.games;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.games.internal.zzc;
import java.util.Arrays;

public final class PlayerLevel extends zzc {
    public static final Creator<PlayerLevel> CREATOR = new zzh();
    private final int zzhdb;
    private final long zzhdc;
    private final long zzhdd;

    public PlayerLevel(int i, long j, long j2) {
        boolean z = true;
        zzbp.zza(j >= 0, (Object) "Min XP must be positive!");
        if (j2 <= j) {
            z = false;
        }
        zzbp.zza(z, (Object) "Max XP must be more than min XP!");
        this.zzhdb = i;
        this.zzhdc = j;
        this.zzhdd = j2;
    }

    public final boolean equals(Object obj) {
        if (!(obj instanceof PlayerLevel)) {
            return false;
        }
        if (this == obj) {
            return true;
        }
        PlayerLevel playerLevel = (PlayerLevel) obj;
        return zzbf.equal(Integer.valueOf(playerLevel.getLevelNumber()), Integer.valueOf(getLevelNumber())) && zzbf.equal(Long.valueOf(playerLevel.getMinXp()), Long.valueOf(getMinXp())) && zzbf.equal(Long.valueOf(playerLevel.getMaxXp()), Long.valueOf(getMaxXp()));
    }

    public final int getLevelNumber() {
        return this.zzhdb;
    }

    public final long getMaxXp() {
        return this.zzhdd;
    }

    public final long getMinXp() {
        return this.zzhdc;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{Integer.valueOf(this.zzhdb), Long.valueOf(this.zzhdc), Long.valueOf(this.zzhdd)});
    }

    public final String toString() {
        return zzbf.zzt(this).zzg("LevelNumber", Integer.valueOf(getLevelNumber())).zzg("MinXp", Long.valueOf(getMinXp())).zzg("MaxXp", Long.valueOf(getMaxXp())).toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, getLevelNumber());
        zzd.zza(parcel, 2, getMinXp());
        zzd.zza(parcel, 3, getMaxXp());
        zzd.zzai(parcel, zze);
    }
}
