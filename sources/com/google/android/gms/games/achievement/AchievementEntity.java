package com.google.android.gms.games.achievement;

import android.database.CharArrayBuffer;
import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbh;
import com.google.android.gms.common.util.zzg;
import com.google.android.gms.games.Player;
import com.google.android.gms.games.PlayerEntity;
import com.google.android.gms.games.internal.zzc;
import java.util.Arrays;

public final class AchievementEntity extends zzc implements Achievement {
    public static final Creator<AchievementEntity> CREATOR = new zza();
    private final String mName;
    private final int mState;
    private final String zzdmz;
    private final int zzeda;
    private final String zzhdk;
    private final Uri zzhdl;
    private final String zzhdm;
    private final Uri zzhdn;
    private final String zzhdo;
    private final int zzhdp;
    private final String zzhdq;
    private final PlayerEntity zzhdr;
    private final int zzhds;
    private final String zzhdt;
    private final long zzhdu;
    private final long zzhdv;

    public AchievementEntity(Achievement achievement) {
        this.zzhdk = achievement.getAchievementId();
        this.zzeda = achievement.getType();
        this.mName = achievement.getName();
        this.zzdmz = achievement.getDescription();
        this.zzhdl = achievement.getUnlockedImageUri();
        this.zzhdm = achievement.getUnlockedImageUrl();
        this.zzhdn = achievement.getRevealedImageUri();
        this.zzhdo = achievement.getRevealedImageUrl();
        this.zzhdr = (PlayerEntity) achievement.getPlayer().freeze();
        this.mState = achievement.getState();
        this.zzhdu = achievement.getLastUpdatedTimestamp();
        this.zzhdv = achievement.getXpValue();
        if (achievement.getType() == 1) {
            this.zzhdp = achievement.getTotalSteps();
            this.zzhdq = achievement.getFormattedTotalSteps();
            this.zzhds = achievement.getCurrentSteps();
            this.zzhdt = achievement.getFormattedCurrentSteps();
        } else {
            this.zzhdp = 0;
            this.zzhdq = null;
            this.zzhds = 0;
            this.zzhdt = null;
        }
        com.google.android.gms.common.internal.zzc.zzr(this.zzhdk);
        com.google.android.gms.common.internal.zzc.zzr(this.zzdmz);
    }

    AchievementEntity(String str, int i, String str2, String str3, Uri uri, String str4, Uri uri2, String str5, int i2, String str6, PlayerEntity playerEntity, int i3, int i4, String str7, long j, long j2) {
        this.zzhdk = str;
        this.zzeda = i;
        this.mName = str2;
        this.zzdmz = str3;
        this.zzhdl = uri;
        this.zzhdm = str4;
        this.zzhdn = uri2;
        this.zzhdo = str5;
        this.zzhdp = i2;
        this.zzhdq = str6;
        this.zzhdr = playerEntity;
        this.mState = i3;
        this.zzhds = i4;
        this.zzhdt = str7;
        this.zzhdu = j;
        this.zzhdv = j2;
    }

    static String zza(Achievement achievement) {
        zzbh zzg = zzbf.zzt(achievement).zzg("Id", achievement.getAchievementId()).zzg("Type", Integer.valueOf(achievement.getType())).zzg("Name", achievement.getName()).zzg("Description", achievement.getDescription()).zzg("Player", achievement.getPlayer()).zzg("State", Integer.valueOf(achievement.getState()));
        if (achievement.getType() == 1) {
            zzg.zzg("CurrentSteps", Integer.valueOf(achievement.getCurrentSteps()));
            zzg.zzg("TotalSteps", Integer.valueOf(achievement.getTotalSteps()));
        }
        return zzg.toString();
    }

    public final boolean equals(Object obj) {
        if (obj instanceof Achievement) {
            if (this == obj) {
                return true;
            }
            Achievement achievement = (Achievement) obj;
            boolean equal;
            boolean equal2;
            if (getType() == 1) {
                equal = zzbf.equal(Integer.valueOf(achievement.getCurrentSteps()), Integer.valueOf(getCurrentSteps()));
                equal2 = zzbf.equal(Integer.valueOf(achievement.getTotalSteps()), Integer.valueOf(getTotalSteps()));
            } else {
                equal = true;
                equal2 = true;
            }
            if (zzbf.equal(achievement.getAchievementId(), getAchievementId()) && zzbf.equal(achievement.getName(), getName()) && zzbf.equal(Integer.valueOf(achievement.getType()), Integer.valueOf(getType())) && zzbf.equal(achievement.getDescription(), getDescription()) && zzbf.equal(Long.valueOf(achievement.getXpValue()), Long.valueOf(getXpValue())) && zzbf.equal(Integer.valueOf(achievement.getState()), Integer.valueOf(getState())) && zzbf.equal(Long.valueOf(achievement.getLastUpdatedTimestamp()), Long.valueOf(getLastUpdatedTimestamp())) && zzbf.equal(achievement.getPlayer(), getPlayer()) && r1 && r2) {
                return true;
            }
        }
        return false;
    }

    public final Achievement freeze() {
        return this;
    }

    public final String getAchievementId() {
        return this.zzhdk;
    }

    public final int getCurrentSteps() {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        com.google.android.gms.common.internal.zzc.zzbg(z);
        return this.zzhds;
    }

    public final String getDescription() {
        return this.zzdmz;
    }

    public final void getDescription(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.zzdmz, charArrayBuffer);
    }

    public final String getFormattedCurrentSteps() {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        com.google.android.gms.common.internal.zzc.zzbg(z);
        return this.zzhdt;
    }

    public final void getFormattedCurrentSteps(CharArrayBuffer charArrayBuffer) {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        com.google.android.gms.common.internal.zzc.zzbg(z);
        zzg.zzb(this.zzhdt, charArrayBuffer);
    }

    public final String getFormattedTotalSteps() {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        com.google.android.gms.common.internal.zzc.zzbg(z);
        return this.zzhdq;
    }

    public final void getFormattedTotalSteps(CharArrayBuffer charArrayBuffer) {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        com.google.android.gms.common.internal.zzc.zzbg(z);
        zzg.zzb(this.zzhdq, charArrayBuffer);
    }

    public final long getLastUpdatedTimestamp() {
        return this.zzhdu;
    }

    public final String getName() {
        return this.mName;
    }

    public final void getName(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.mName, charArrayBuffer);
    }

    public final Player getPlayer() {
        return this.zzhdr;
    }

    public final Uri getRevealedImageUri() {
        return this.zzhdn;
    }

    public final String getRevealedImageUrl() {
        return this.zzhdo;
    }

    public final int getState() {
        return this.mState;
    }

    public final int getTotalSteps() {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        com.google.android.gms.common.internal.zzc.zzbg(z);
        return this.zzhdp;
    }

    public final int getType() {
        return this.zzeda;
    }

    public final Uri getUnlockedImageUri() {
        return this.zzhdl;
    }

    public final String getUnlockedImageUrl() {
        return this.zzhdm;
    }

    public final long getXpValue() {
        return this.zzhdv;
    }

    public final int hashCode() {
        int currentSteps;
        int totalSteps;
        if (getType() == 1) {
            currentSteps = getCurrentSteps();
            totalSteps = getTotalSteps();
        } else {
            totalSteps = 0;
            currentSteps = 0;
        }
        return Arrays.hashCode(new Object[]{getAchievementId(), getName(), Integer.valueOf(getType()), getDescription(), Long.valueOf(getXpValue()), Integer.valueOf(getState()), Long.valueOf(getLastUpdatedTimestamp()), getPlayer(), Integer.valueOf(currentSteps), Integer.valueOf(totalSteps)});
    }

    public final boolean isDataValid() {
        return true;
    }

    public final String toString() {
        return zza(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getAchievementId(), false);
        zzd.zzc(parcel, 2, getType());
        zzd.zza(parcel, 3, getName(), false);
        zzd.zza(parcel, 4, getDescription(), false);
        zzd.zza(parcel, 5, getUnlockedImageUri(), i, false);
        zzd.zza(parcel, 6, getUnlockedImageUrl(), false);
        zzd.zza(parcel, 7, getRevealedImageUri(), i, false);
        zzd.zza(parcel, 8, getRevealedImageUrl(), false);
        zzd.zzc(parcel, 9, this.zzhdp);
        zzd.zza(parcel, 10, this.zzhdq, false);
        zzd.zza(parcel, 11, getPlayer(), i, false);
        zzd.zzc(parcel, 12, getState());
        zzd.zzc(parcel, 13, this.zzhds);
        zzd.zza(parcel, 14, this.zzhdt, false);
        zzd.zza(parcel, 15, getLastUpdatedTimestamp());
        zzd.zza(parcel, 16, getXpValue());
        zzd.zzai(parcel, zze);
    }
}
