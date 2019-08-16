package com.google.android.gms.games.achievement;

import android.database.CharArrayBuffer;
import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.apps.common.proguard.UsedByReflection;
import com.google.android.gms.common.internal.Asserts;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.Objects.ToStringHelper;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import com.google.android.gms.common.util.DataUtils;
import com.google.android.gms.games.Player;
import com.google.android.gms.games.PlayerEntity;
import com.google.android.gms.games.internal.zzd;

@UsedByReflection("GamesClientImpl.java")
@Class(creator = "AchievementEntityCreator")
@Reserved({1000})
public final class AchievementEntity extends zzd implements Achievement {
    public static final Creator<AchievementEntity> CREATOR = new zza();
    @Field(getter = "getDescription", mo13990id = 4)
    private final String description;
    @Field(getter = "getName", mo13990id = 3)
    private final String name;
    @Field(getter = "getState", mo13990id = 12)
    private final int state;
    @Field(getter = "getType", mo13990id = 2)
    private final int type;
    @Field(getter = "getAchievementId", mo13990id = 1)
    private final String zzfc;
    @Field(getter = "getUnlockedImageUri", mo13990id = 5)
    private final Uri zzfd;
    @Field(getter = "getUnlockedImageUrl", mo13990id = 6)
    private final String zzfe;
    @Field(getter = "getRevealedImageUri", mo13990id = 7)
    private final Uri zzff;
    @Field(getter = "getRevealedImageUrl", mo13990id = 8)
    private final String zzfg;
    @Field(getter = "getTotalStepsRaw", mo13990id = 9)
    private final int zzfh;
    @Field(getter = "getFormattedTotalStepsRaw", mo13990id = 10)
    private final String zzfi;
    @Nullable
    @Field(getter = "getPlayerInternal", mo13990id = 11)
    private final PlayerEntity zzfj;
    @Field(getter = "getCurrentStepsRaw", mo13990id = 13)
    private final int zzfk;
    @Field(getter = "getFormattedCurrentStepsRaw", mo13990id = 14)
    private final String zzfl;
    @Field(getter = "getLastUpdatedTimestamp", mo13990id = 15)
    private final long zzfm;
    @Field(getter = "getXpValue", mo13990id = 16)
    private final long zzfn;
    @Field(defaultValue = "-1.0f", getter = "getRarityPercent", mo13990id = 17)
    private final float zzfo;
    @Field(getter = "getApplicationId", mo13990id = 18)
    private final String zzm;

    public AchievementEntity(Achievement achievement) {
        this.zzfc = achievement.getAchievementId();
        this.type = achievement.getType();
        this.name = achievement.getName();
        this.description = achievement.getDescription();
        this.zzfd = achievement.getUnlockedImageUri();
        this.zzfe = achievement.getUnlockedImageUrl();
        this.zzff = achievement.getRevealedImageUri();
        this.zzfg = achievement.getRevealedImageUrl();
        if (achievement.zzw() != null) {
            this.zzfj = (PlayerEntity) achievement.zzw().freeze();
        } else {
            this.zzfj = null;
        }
        this.state = achievement.getState();
        this.zzfm = achievement.getLastUpdatedTimestamp();
        this.zzfn = achievement.getXpValue();
        this.zzfo = achievement.zzx();
        this.zzm = achievement.getApplicationId();
        if (achievement.getType() == 1) {
            this.zzfh = achievement.getTotalSteps();
            this.zzfi = achievement.getFormattedTotalSteps();
            this.zzfk = achievement.getCurrentSteps();
            this.zzfl = achievement.getFormattedCurrentSteps();
        } else {
            this.zzfh = 0;
            this.zzfi = null;
            this.zzfk = 0;
            this.zzfl = null;
        }
        Asserts.checkNotNull(this.zzfc);
        Asserts.checkNotNull(this.description);
    }

    @Constructor
    AchievementEntity(@Param(mo13993id = 1) String str, @Param(mo13993id = 2) int i, @Param(mo13993id = 3) String str2, @Param(mo13993id = 4) String str3, @Param(mo13993id = 5) Uri uri, @Param(mo13993id = 6) String str4, @Param(mo13993id = 7) Uri uri2, @Param(mo13993id = 8) String str5, @Param(mo13993id = 9) int i2, @Param(mo13993id = 10) String str6, @Nullable @Param(mo13993id = 11) PlayerEntity playerEntity, @Param(mo13993id = 12) int i3, @Param(mo13993id = 13) int i4, @Param(mo13993id = 14) String str7, @Param(mo13993id = 15) long j, @Param(mo13993id = 16) long j2, @Param(mo13993id = 17) float f, @Param(mo13993id = 18) String str8) {
        this.zzfc = str;
        this.type = i;
        this.name = str2;
        this.description = str3;
        this.zzfd = uri;
        this.zzfe = str4;
        this.zzff = uri2;
        this.zzfg = str5;
        this.zzfh = i2;
        this.zzfi = str6;
        this.zzfj = playerEntity;
        this.state = i3;
        this.zzfk = i4;
        this.zzfl = str7;
        this.zzfm = j;
        this.zzfn = j2;
        this.zzfo = f;
        this.zzm = str8;
    }

    static String zza(Achievement achievement) {
        ToStringHelper add = Objects.toStringHelper(achievement).add("Id", achievement.getAchievementId()).add("Game Id", achievement.getApplicationId()).add("Type", Integer.valueOf(achievement.getType())).add("Name", achievement.getName()).add("Description", achievement.getDescription()).add("Player", achievement.zzw()).add("State", Integer.valueOf(achievement.getState())).add("Rarity Percent", Float.valueOf(achievement.zzx()));
        if (achievement.getType() == 1) {
            add.add("CurrentSteps", Integer.valueOf(achievement.getCurrentSteps()));
            add.add("TotalSteps", Integer.valueOf(achievement.getTotalSteps()));
        }
        return add.toString();
    }

    public final boolean equals(Object obj) {
        if (obj instanceof Achievement) {
            if (this == obj) {
                return true;
            }
            Achievement achievement = (Achievement) obj;
            if (achievement.getType() == getType() && ((getType() != 1 || (achievement.getCurrentSteps() == getCurrentSteps() && achievement.getTotalSteps() == getTotalSteps())) && achievement.getXpValue() == getXpValue() && achievement.getState() == getState() && achievement.getLastUpdatedTimestamp() == getLastUpdatedTimestamp() && Objects.equal(achievement.getAchievementId(), getAchievementId()) && Objects.equal(achievement.getApplicationId(), getApplicationId()) && Objects.equal(achievement.getName(), getName()) && Objects.equal(achievement.getDescription(), getDescription()) && Objects.equal(achievement.zzw(), zzw()) && achievement.zzx() == zzx())) {
                return true;
            }
        }
        return false;
    }

    public final Achievement freeze() {
        return this;
    }

    public final String getAchievementId() {
        return this.zzfc;
    }

    public final String getApplicationId() {
        return this.zzm;
    }

    public final int getCurrentSteps() {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        Asserts.checkState(z);
        return this.zzfk;
    }

    public final String getDescription() {
        return this.description;
    }

    public final void getDescription(CharArrayBuffer charArrayBuffer) {
        DataUtils.copyStringToBuffer(this.description, charArrayBuffer);
    }

    public final String getFormattedCurrentSteps() {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        Asserts.checkState(z);
        return this.zzfl;
    }

    public final void getFormattedCurrentSteps(CharArrayBuffer charArrayBuffer) {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        Asserts.checkState(z);
        DataUtils.copyStringToBuffer(this.zzfl, charArrayBuffer);
    }

    public final String getFormattedTotalSteps() {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        Asserts.checkState(z);
        return this.zzfi;
    }

    public final void getFormattedTotalSteps(CharArrayBuffer charArrayBuffer) {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        Asserts.checkState(z);
        DataUtils.copyStringToBuffer(this.zzfi, charArrayBuffer);
    }

    public final long getLastUpdatedTimestamp() {
        return this.zzfm;
    }

    public final String getName() {
        return this.name;
    }

    public final void getName(CharArrayBuffer charArrayBuffer) {
        DataUtils.copyStringToBuffer(this.name, charArrayBuffer);
    }

    public final Player getPlayer() {
        return (Player) Preconditions.checkNotNull(this.zzfj);
    }

    public final Uri getRevealedImageUri() {
        return this.zzff;
    }

    public final String getRevealedImageUrl() {
        return this.zzfg;
    }

    public final int getState() {
        return this.state;
    }

    public final int getTotalSteps() {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        Asserts.checkState(z);
        return this.zzfh;
    }

    public final int getType() {
        return this.type;
    }

    public final Uri getUnlockedImageUri() {
        return this.zzfd;
    }

    public final String getUnlockedImageUrl() {
        return this.zzfe;
    }

    public final long getXpValue() {
        return this.zzfn;
    }

    public final int hashCode() {
        int i;
        int i2;
        if (getType() == 1) {
            i2 = getCurrentSteps();
            i = getTotalSteps();
        } else {
            i = 0;
            i2 = 0;
        }
        return Objects.hashCode(getAchievementId(), getApplicationId(), getName(), Integer.valueOf(getType()), getDescription(), Long.valueOf(getXpValue()), Integer.valueOf(getState()), Long.valueOf(getLastUpdatedTimestamp()), zzw(), Integer.valueOf(i2), Integer.valueOf(i));
    }

    public final boolean isDataValid() {
        return true;
    }

    public final String toString() {
        return zza(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeString(parcel, 1, getAchievementId(), false);
        SafeParcelWriter.writeInt(parcel, 2, getType());
        SafeParcelWriter.writeString(parcel, 3, getName(), false);
        SafeParcelWriter.writeString(parcel, 4, getDescription(), false);
        SafeParcelWriter.writeParcelable(parcel, 5, getUnlockedImageUri(), i, false);
        SafeParcelWriter.writeString(parcel, 6, getUnlockedImageUrl(), false);
        SafeParcelWriter.writeParcelable(parcel, 7, getRevealedImageUri(), i, false);
        SafeParcelWriter.writeString(parcel, 8, getRevealedImageUrl(), false);
        SafeParcelWriter.writeInt(parcel, 9, this.zzfh);
        SafeParcelWriter.writeString(parcel, 10, this.zzfi, false);
        SafeParcelWriter.writeParcelable(parcel, 11, this.zzfj, i, false);
        SafeParcelWriter.writeInt(parcel, 12, getState());
        SafeParcelWriter.writeInt(parcel, 13, this.zzfk);
        SafeParcelWriter.writeString(parcel, 14, this.zzfl, false);
        SafeParcelWriter.writeLong(parcel, 15, getLastUpdatedTimestamp());
        SafeParcelWriter.writeLong(parcel, 16, getXpValue());
        SafeParcelWriter.writeFloat(parcel, 17, this.zzfo);
        SafeParcelWriter.writeString(parcel, 18, this.zzm, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }

    @Nullable
    public final Player zzw() {
        return this.zzfj;
    }

    public final float zzx() {
        return this.zzfo;
    }
}
