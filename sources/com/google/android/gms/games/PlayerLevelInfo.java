package com.google.android.gms.games;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.games.internal.zzc;
import java.util.Arrays;

public final class PlayerLevelInfo extends zzc {
    public static final Creator<PlayerLevelInfo> CREATOR = new zzi();
    private final long zzhde;
    private final long zzhdf;
    private final PlayerLevel zzhdg;
    private final PlayerLevel zzhdh;

    public PlayerLevelInfo(long j, long j2, PlayerLevel playerLevel, PlayerLevel playerLevel2) {
        zzbp.zzbg(j != -1);
        zzbp.zzu(playerLevel);
        zzbp.zzu(playerLevel2);
        this.zzhde = j;
        this.zzhdf = j2;
        this.zzhdg = playerLevel;
        this.zzhdh = playerLevel2;
    }

    public final boolean equals(Object obj) {
        if (!(obj instanceof PlayerLevelInfo)) {
            return false;
        }
        if (obj == this) {
            return true;
        }
        PlayerLevelInfo playerLevelInfo = (PlayerLevelInfo) obj;
        return zzbf.equal(Long.valueOf(this.zzhde), Long.valueOf(playerLevelInfo.zzhde)) && zzbf.equal(Long.valueOf(this.zzhdf), Long.valueOf(playerLevelInfo.zzhdf)) && zzbf.equal(this.zzhdg, playerLevelInfo.zzhdg) && zzbf.equal(this.zzhdh, playerLevelInfo.zzhdh);
    }

    public final PlayerLevel getCurrentLevel() {
        return this.zzhdg;
    }

    public final long getCurrentXpTotal() {
        return this.zzhde;
    }

    public final long getLastLevelUpTimestamp() {
        return this.zzhdf;
    }

    public final PlayerLevel getNextLevel() {
        return this.zzhdh;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{Long.valueOf(this.zzhde), Long.valueOf(this.zzhdf), this.zzhdg, this.zzhdh});
    }

    public final boolean isMaxLevel() {
        return this.zzhdg.equals(this.zzhdh);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getCurrentXpTotal());
        zzd.zza(parcel, 2, getLastLevelUpTimestamp());
        zzd.zza(parcel, 3, getCurrentLevel(), i, false);
        zzd.zza(parcel, 4, getNextLevel(), i, false);
        zzd.zzai(parcel, zze);
    }
}
