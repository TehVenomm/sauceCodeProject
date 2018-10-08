package com.google.android.gms.games.stats;

import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.games.internal.zzc;
import java.util.Arrays;

public final class zza extends zzc implements PlayerStats {
    public static final Creator<zza> CREATOR = new zzb();
    private final float zzhoy;
    private final float zzhoz;
    private final int zzhpa;
    private final int zzhpb;
    private final int zzhpc;
    private final float zzhpd;
    private final float zzhpe;
    private final Bundle zzhpf;
    private final float zzhpg;
    private final float zzhph;
    private final float zzhpi;

    zza(float f, float f2, int i, int i2, int i3, float f3, float f4, Bundle bundle, float f5, float f6, float f7) {
        this.zzhoy = f;
        this.zzhoz = f2;
        this.zzhpa = i;
        this.zzhpb = i2;
        this.zzhpc = i3;
        this.zzhpd = f3;
        this.zzhpe = f4;
        this.zzhpf = bundle;
        this.zzhpg = f5;
        this.zzhph = f6;
        this.zzhpi = f7;
    }

    public zza(PlayerStats playerStats) {
        this.zzhoy = playerStats.getAverageSessionLength();
        this.zzhoz = playerStats.getChurnProbability();
        this.zzhpa = playerStats.getDaysSinceLastPlayed();
        this.zzhpb = playerStats.getNumberOfPurchases();
        this.zzhpc = playerStats.getNumberOfSessions();
        this.zzhpd = playerStats.getSessionPercentile();
        this.zzhpe = playerStats.getSpendPercentile();
        this.zzhpg = playerStats.getSpendProbability();
        this.zzhph = playerStats.getHighSpenderProbability();
        this.zzhpi = playerStats.getTotalSpendNext28Days();
        this.zzhpf = playerStats.zzarz();
    }

    static int zza(PlayerStats playerStats) {
        return Arrays.hashCode(new Object[]{Float.valueOf(playerStats.getAverageSessionLength()), Float.valueOf(playerStats.getChurnProbability()), Integer.valueOf(playerStats.getDaysSinceLastPlayed()), Integer.valueOf(playerStats.getNumberOfPurchases()), Integer.valueOf(playerStats.getNumberOfSessions()), Float.valueOf(playerStats.getSessionPercentile()), Float.valueOf(playerStats.getSpendPercentile()), Float.valueOf(playerStats.getSpendProbability()), Float.valueOf(playerStats.getHighSpenderProbability()), Float.valueOf(playerStats.getTotalSpendNext28Days())});
    }

    static boolean zza(PlayerStats playerStats, Object obj) {
        if (!(obj instanceof PlayerStats)) {
            return false;
        }
        if (playerStats == obj) {
            return true;
        }
        PlayerStats playerStats2 = (PlayerStats) obj;
        return zzbf.equal(Float.valueOf(playerStats2.getAverageSessionLength()), Float.valueOf(playerStats.getAverageSessionLength())) && zzbf.equal(Float.valueOf(playerStats2.getChurnProbability()), Float.valueOf(playerStats.getChurnProbability())) && zzbf.equal(Integer.valueOf(playerStats2.getDaysSinceLastPlayed()), Integer.valueOf(playerStats.getDaysSinceLastPlayed())) && zzbf.equal(Integer.valueOf(playerStats2.getNumberOfPurchases()), Integer.valueOf(playerStats.getNumberOfPurchases())) && zzbf.equal(Integer.valueOf(playerStats2.getNumberOfSessions()), Integer.valueOf(playerStats.getNumberOfSessions())) && zzbf.equal(Float.valueOf(playerStats2.getSessionPercentile()), Float.valueOf(playerStats.getSessionPercentile())) && zzbf.equal(Float.valueOf(playerStats2.getSpendPercentile()), Float.valueOf(playerStats.getSpendPercentile())) && zzbf.equal(Float.valueOf(playerStats2.getSpendProbability()), Float.valueOf(playerStats.getSpendProbability())) && zzbf.equal(Float.valueOf(playerStats2.getHighSpenderProbability()), Float.valueOf(playerStats.getHighSpenderProbability())) && zzbf.equal(Float.valueOf(playerStats2.getTotalSpendNext28Days()), Float.valueOf(playerStats.getTotalSpendNext28Days()));
    }

    static String zzb(PlayerStats playerStats) {
        return zzbf.zzt(playerStats).zzg("AverageSessionLength", Float.valueOf(playerStats.getAverageSessionLength())).zzg("ChurnProbability", Float.valueOf(playerStats.getChurnProbability())).zzg("DaysSinceLastPlayed", Integer.valueOf(playerStats.getDaysSinceLastPlayed())).zzg("NumberOfPurchases", Integer.valueOf(playerStats.getNumberOfPurchases())).zzg("NumberOfSessions", Integer.valueOf(playerStats.getNumberOfSessions())).zzg("SessionPercentile", Float.valueOf(playerStats.getSessionPercentile())).zzg("SpendPercentile", Float.valueOf(playerStats.getSpendPercentile())).zzg("SpendProbability", Float.valueOf(playerStats.getSpendProbability())).zzg("HighSpenderProbability", Float.valueOf(playerStats.getHighSpenderProbability())).zzg("TotalSpendNext28Days", Float.valueOf(playerStats.getTotalSpendNext28Days())).toString();
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

    public final float getAverageSessionLength() {
        return this.zzhoy;
    }

    public final float getChurnProbability() {
        return this.zzhoz;
    }

    public final int getDaysSinceLastPlayed() {
        return this.zzhpa;
    }

    public final float getHighSpenderProbability() {
        return this.zzhph;
    }

    public final int getNumberOfPurchases() {
        return this.zzhpb;
    }

    public final int getNumberOfSessions() {
        return this.zzhpc;
    }

    public final float getSessionPercentile() {
        return this.zzhpd;
    }

    public final float getSpendPercentile() {
        return this.zzhpe;
    }

    public final float getSpendProbability() {
        return this.zzhpg;
    }

    public final float getTotalSpendNext28Days() {
        return this.zzhpi;
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
        zzd.zza(parcel, 1, getAverageSessionLength());
        zzd.zza(parcel, 2, getChurnProbability());
        zzd.zzc(parcel, 3, getDaysSinceLastPlayed());
        zzd.zzc(parcel, 4, getNumberOfPurchases());
        zzd.zzc(parcel, 5, getNumberOfSessions());
        zzd.zza(parcel, 6, getSessionPercentile());
        zzd.zza(parcel, 7, getSpendPercentile());
        zzd.zza(parcel, 8, this.zzhpf, false);
        zzd.zza(parcel, 9, getSpendProbability());
        zzd.zza(parcel, 10, getHighSpenderProbability());
        zzd.zza(parcel, 11, getTotalSpendNext28Days());
        zzd.zzai(parcel, zze);
    }

    public final Bundle zzarz() {
        return this.zzhpf;
    }
}
