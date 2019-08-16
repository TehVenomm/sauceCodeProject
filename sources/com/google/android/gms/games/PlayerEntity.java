package com.google.android.gms.games;

import android.database.CharArrayBuffer;
import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.apps.common.proguard.UsedByReflection;
import com.google.android.gms.common.internal.Asserts;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import com.google.android.gms.common.util.DataUtils;
import com.google.android.gms.common.util.RetainForClient;
import com.google.android.gms.games.internal.GamesDowngradeableSafeParcel;
import com.google.android.gms.games.internal.player.MostRecentGameInfoEntity;

@RetainForClient
@UsedByReflection("GamesClientImpl.java")
@Class(creator = "PlayerEntityCreator")
@Reserved({1000})
public final class PlayerEntity extends GamesDowngradeableSafeParcel implements Player {
    public static final Creator<PlayerEntity> CREATOR = new zza();
    @Field(getter = "getName", mo13990id = 21)
    private final String name;
    @Nullable
    @Field(getter = "getIconImageUrl", mo13990id = 8)
    private final String zzac;
    @Nullable
    @Field(getter = "getHiResImageUrl", mo13990id = 9)
    private final String zzad;
    @Field(getter = "getPlayerId", mo13990id = 1)
    private String zzbz;
    @Field(getter = "getRetrievedTimestamp", mo13990id = 5)
    private final long zzca;
    @Field(getter = "isInCircles", mo13990id = 6)
    private final int zzcb;
    @Field(getter = "getLastPlayedWithTimestamp", mo13990id = 7)
    private final long zzcc;
    @Nullable
    @Field(getter = "getTitle", mo13990id = 14)
    private final String zzcd;
    @Nullable
    @Field(getter = "getMostRecentGameInfo", mo13990id = 15)
    private final MostRecentGameInfoEntity zzce;
    @Nullable
    @Field(getter = "getLevelInfo", mo13990id = 16)
    private final PlayerLevelInfo zzcf;
    @Field(getter = "isProfileVisible", mo13990id = 18)
    private final boolean zzcg;
    @Field(getter = "hasDebugAccess", mo13990id = 19)
    private final boolean zzch;
    @Nullable
    @Field(getter = "getGamerTag", mo13990id = 20)
    private final String zzci;
    @Nullable
    @Field(getter = "getBannerImageLandscapeUri", mo13990id = 22)
    private final Uri zzcj;
    @Nullable
    @Field(getter = "getBannerImageLandscapeUrl", mo13990id = 23)
    private final String zzck;
    @Nullable
    @Field(getter = "getBannerImagePortraitUri", mo13990id = 24)
    private final Uri zzcl;
    @Nullable
    @Field(getter = "getBannerImagePortraitUrl", mo13990id = 25)
    private final String zzcm;
    @Field(getter = "getGamerFriendStatus", mo13990id = 26)
    private final int zzcn;
    @Field(getter = "getGamerFriendUpdateTimestamp", mo13990id = 27)
    private final long zzco;
    @Field(getter = "isMuted", mo13990id = 28)
    private final boolean zzcp;
    @Field(defaultValue = "-1", getter = "getTotalUnlockedAchievement", mo13990id = 29)
    private final long zzcq;
    @Field(getter = "getDisplayName", mo13990id = 2)
    private String zzn;
    @Nullable
    @Field(getter = "getIconImageUri", mo13990id = 3)
    private final Uri zzr;
    @Nullable
    @Field(getter = "getHiResImageUri", mo13990id = 4)
    private final Uri zzs;

    static final class zza extends zzap {
        zza() {
        }

        public final /* synthetic */ Object createFromParcel(Parcel parcel) {
            return createFromParcel(parcel);
        }

        public final PlayerEntity zzc(Parcel parcel) {
            if (PlayerEntity.zzb(PlayerEntity.getUnparcelClientVersion()) || PlayerEntity.canUnparcelSafely(PlayerEntity.class.getCanonicalName())) {
                return super.createFromParcel(parcel);
            }
            String readString = parcel.readString();
            String readString2 = parcel.readString();
            String readString3 = parcel.readString();
            String readString4 = parcel.readString();
            return new PlayerEntity(readString, readString2, readString3 == null ? null : Uri.parse(readString3), readString4 == null ? null : Uri.parse(readString4), parcel.readLong(), -1, -1, null, null, null, null, null, true, false, parcel.readString(), parcel.readString(), null, null, null, null, -1, -1, false, -1);
        }
    }

    public PlayerEntity(Player player) {
        this(player, true);
    }

    private PlayerEntity(Player player, boolean z) {
        this.zzbz = player.getPlayerId();
        this.zzn = player.getDisplayName();
        this.zzr = player.getIconImageUri();
        this.zzac = player.getIconImageUrl();
        this.zzs = player.getHiResImageUri();
        this.zzad = player.getHiResImageUrl();
        this.zzca = player.getRetrievedTimestamp();
        this.zzcb = player.zzj();
        this.zzcc = player.getLastPlayedWithTimestamp();
        this.zzcd = player.getTitle();
        this.zzcg = player.zzk();
        com.google.android.gms.games.internal.player.zza zzl = player.zzl();
        this.zzce = zzl == null ? null : new MostRecentGameInfoEntity(zzl);
        this.zzcf = player.getLevelInfo();
        this.zzch = player.zzi();
        this.zzci = player.zzh();
        this.name = player.getName();
        this.zzcj = player.getBannerImageLandscapeUri();
        this.zzck = player.getBannerImageLandscapeUrl();
        this.zzcl = player.getBannerImagePortraitUri();
        this.zzcm = player.getBannerImagePortraitUrl();
        this.zzcn = player.zzm();
        this.zzco = player.zzn();
        this.zzcp = player.isMuted();
        this.zzcq = player.zzo();
        Asserts.checkNotNull(this.zzbz);
        Asserts.checkNotNull(this.zzn);
        Asserts.checkState(this.zzca > 0);
    }

    @Constructor
    PlayerEntity(@Param(mo13993id = 1) String str, @Param(mo13993id = 2) String str2, @Nullable @Param(mo13993id = 3) Uri uri, @Nullable @Param(mo13993id = 4) Uri uri2, @Param(mo13993id = 5) long j, @Param(mo13993id = 6) int i, @Param(mo13993id = 7) long j2, @Nullable @Param(mo13993id = 8) String str3, @Nullable @Param(mo13993id = 9) String str4, @Nullable @Param(mo13993id = 14) String str5, @Nullable @Param(mo13993id = 15) MostRecentGameInfoEntity mostRecentGameInfoEntity, @Nullable @Param(mo13993id = 16) PlayerLevelInfo playerLevelInfo, @Param(mo13993id = 18) boolean z, @Param(mo13993id = 19) boolean z2, @Nullable @Param(mo13993id = 20) String str6, @Param(mo13993id = 21) String str7, @Nullable @Param(mo13993id = 22) Uri uri3, @Nullable @Param(mo13993id = 23) String str8, @Nullable @Param(mo13993id = 24) Uri uri4, @Nullable @Param(mo13993id = 25) String str9, @Param(mo13993id = 26) int i2, @Param(mo13993id = 27) long j3, @Param(mo13993id = 28) boolean z3, @Param(mo13993id = 29) long j4) {
        this.zzbz = str;
        this.zzn = str2;
        this.zzr = uri;
        this.zzac = str3;
        this.zzs = uri2;
        this.zzad = str4;
        this.zzca = j;
        this.zzcb = i;
        this.zzcc = j2;
        this.zzcd = str5;
        this.zzcg = z;
        this.zzce = mostRecentGameInfoEntity;
        this.zzcf = playerLevelInfo;
        this.zzch = z2;
        this.zzci = str6;
        this.name = str7;
        this.zzcj = uri3;
        this.zzck = str8;
        this.zzcl = uri4;
        this.zzcm = str9;
        this.zzcn = i2;
        this.zzco = j3;
        this.zzcp = z3;
        this.zzcq = j4;
    }

    static int zza(Player player) {
        return Objects.hashCode(player.getPlayerId(), player.getDisplayName(), Boolean.valueOf(player.zzi()), player.getIconImageUri(), player.getHiResImageUri(), Long.valueOf(player.getRetrievedTimestamp()), player.getTitle(), player.getLevelInfo(), player.zzh(), player.getName(), player.getBannerImageLandscapeUri(), player.getBannerImagePortraitUri(), Integer.valueOf(player.zzm()), Long.valueOf(player.zzn()), Boolean.valueOf(player.isMuted()), Long.valueOf(player.zzo()));
    }

    static boolean zza(Player player, Object obj) {
        if (!(obj instanceof Player)) {
            return false;
        }
        if (player == obj) {
            return true;
        }
        Player player2 = (Player) obj;
        return Objects.equal(player2.getPlayerId(), player.getPlayerId()) && Objects.equal(player2.getDisplayName(), player.getDisplayName()) && Objects.equal(Boolean.valueOf(player2.zzi()), Boolean.valueOf(player.zzi())) && Objects.equal(player2.getIconImageUri(), player.getIconImageUri()) && Objects.equal(player2.getHiResImageUri(), player.getHiResImageUri()) && Objects.equal(Long.valueOf(player2.getRetrievedTimestamp()), Long.valueOf(player.getRetrievedTimestamp())) && Objects.equal(player2.getTitle(), player.getTitle()) && Objects.equal(player2.getLevelInfo(), player.getLevelInfo()) && Objects.equal(player2.zzh(), player.zzh()) && Objects.equal(player2.getName(), player.getName()) && Objects.equal(player2.getBannerImageLandscapeUri(), player.getBannerImageLandscapeUri()) && Objects.equal(player2.getBannerImagePortraitUri(), player.getBannerImagePortraitUri()) && Objects.equal(Integer.valueOf(player2.zzm()), Integer.valueOf(player.zzm())) && Objects.equal(Long.valueOf(player2.zzn()), Long.valueOf(player.zzn())) && Objects.equal(Boolean.valueOf(player2.isMuted()), Boolean.valueOf(player.isMuted())) && Objects.equal(Long.valueOf(player2.zzo()), Long.valueOf(player.zzo()));
    }

    static String zzb(Player player) {
        return Objects.toStringHelper(player).add("PlayerId", player.getPlayerId()).add("DisplayName", player.getDisplayName()).add("HasDebugAccess", Boolean.valueOf(player.zzi())).add("IconImageUri", player.getIconImageUri()).add("IconImageUrl", player.getIconImageUrl()).add("HiResImageUri", player.getHiResImageUri()).add("HiResImageUrl", player.getHiResImageUrl()).add("RetrievedTimestamp", Long.valueOf(player.getRetrievedTimestamp())).add("Title", player.getTitle()).add("LevelInfo", player.getLevelInfo()).add("GamerTag", player.zzh()).add("Name", player.getName()).add("BannerImageLandscapeUri", player.getBannerImageLandscapeUri()).add("BannerImageLandscapeUrl", player.getBannerImageLandscapeUrl()).add("BannerImagePortraitUri", player.getBannerImagePortraitUri()).add("BannerImagePortraitUrl", player.getBannerImagePortraitUrl()).add("GamerFriendStatus", Integer.valueOf(player.zzm())).add("GamerFriendUpdateTimestamp", Long.valueOf(player.zzn())).add("IsMuted", Boolean.valueOf(player.isMuted())).add("totalUnlockedAchievement", Long.valueOf(player.zzo())).toString();
    }

    public final boolean equals(Object obj) {
        return zza(this, obj);
    }

    public final Player freeze() {
        return this;
    }

    @Nullable
    public final Uri getBannerImageLandscapeUri() {
        return this.zzcj;
    }

    @Nullable
    public final String getBannerImageLandscapeUrl() {
        return this.zzck;
    }

    @Nullable
    public final Uri getBannerImagePortraitUri() {
        return this.zzcl;
    }

    @Nullable
    public final String getBannerImagePortraitUrl() {
        return this.zzcm;
    }

    public final String getDisplayName() {
        return this.zzn;
    }

    public final void getDisplayName(CharArrayBuffer charArrayBuffer) {
        DataUtils.copyStringToBuffer(this.zzn, charArrayBuffer);
    }

    @Nullable
    public final Uri getHiResImageUri() {
        return this.zzs;
    }

    @Nullable
    public final String getHiResImageUrl() {
        return this.zzad;
    }

    @Nullable
    public final Uri getIconImageUri() {
        return this.zzr;
    }

    @Nullable
    public final String getIconImageUrl() {
        return this.zzac;
    }

    public final long getLastPlayedWithTimestamp() {
        return this.zzcc;
    }

    @Nullable
    public final PlayerLevelInfo getLevelInfo() {
        return this.zzcf;
    }

    public final String getName() {
        return this.name;
    }

    public final String getPlayerId() {
        return this.zzbz;
    }

    public final long getRetrievedTimestamp() {
        return this.zzca;
    }

    @Nullable
    public final String getTitle() {
        return this.zzcd;
    }

    public final void getTitle(CharArrayBuffer charArrayBuffer) {
        DataUtils.copyStringToBuffer(this.zzcd, charArrayBuffer);
    }

    public final boolean hasHiResImage() {
        return getHiResImageUri() != null;
    }

    public final boolean hasIconImage() {
        return getIconImageUri() != null;
    }

    public final int hashCode() {
        return zza((Player) this);
    }

    public final boolean isDataValid() {
        return true;
    }

    public final boolean isMuted() {
        return this.zzcp;
    }

    public final String toString() {
        return zzb(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        String str = null;
        if (!shouldDowngrade()) {
            int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
            SafeParcelWriter.writeString(parcel, 1, getPlayerId(), false);
            SafeParcelWriter.writeString(parcel, 2, getDisplayName(), false);
            SafeParcelWriter.writeParcelable(parcel, 3, getIconImageUri(), i, false);
            SafeParcelWriter.writeParcelable(parcel, 4, getHiResImageUri(), i, false);
            SafeParcelWriter.writeLong(parcel, 5, getRetrievedTimestamp());
            SafeParcelWriter.writeInt(parcel, 6, this.zzcb);
            SafeParcelWriter.writeLong(parcel, 7, getLastPlayedWithTimestamp());
            SafeParcelWriter.writeString(parcel, 8, getIconImageUrl(), false);
            SafeParcelWriter.writeString(parcel, 9, getHiResImageUrl(), false);
            SafeParcelWriter.writeString(parcel, 14, getTitle(), false);
            SafeParcelWriter.writeParcelable(parcel, 15, this.zzce, i, false);
            SafeParcelWriter.writeParcelable(parcel, 16, getLevelInfo(), i, false);
            SafeParcelWriter.writeBoolean(parcel, 18, this.zzcg);
            SafeParcelWriter.writeBoolean(parcel, 19, this.zzch);
            SafeParcelWriter.writeString(parcel, 20, this.zzci, false);
            SafeParcelWriter.writeString(parcel, 21, this.name, false);
            SafeParcelWriter.writeParcelable(parcel, 22, getBannerImageLandscapeUri(), i, false);
            SafeParcelWriter.writeString(parcel, 23, getBannerImageLandscapeUrl(), false);
            SafeParcelWriter.writeParcelable(parcel, 24, getBannerImagePortraitUri(), i, false);
            SafeParcelWriter.writeString(parcel, 25, getBannerImagePortraitUrl(), false);
            SafeParcelWriter.writeInt(parcel, 26, this.zzcn);
            SafeParcelWriter.writeLong(parcel, 27, this.zzco);
            SafeParcelWriter.writeBoolean(parcel, 28, this.zzcp);
            SafeParcelWriter.writeLong(parcel, 29, this.zzcq);
            SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
            return;
        }
        parcel.writeString(this.zzbz);
        parcel.writeString(this.zzn);
        parcel.writeString(this.zzr == null ? null : this.zzr.toString());
        if (this.zzs != null) {
            str = this.zzs.toString();
        }
        parcel.writeString(str);
        parcel.writeLong(this.zzca);
    }

    @Nullable
    public final String zzh() {
        return this.zzci;
    }

    public final boolean zzi() {
        return this.zzch;
    }

    public final int zzj() {
        return this.zzcb;
    }

    public final boolean zzk() {
        return this.zzcg;
    }

    @Nullable
    public final com.google.android.gms.games.internal.player.zza zzl() {
        return this.zzce;
    }

    public final int zzm() {
        return this.zzcn;
    }

    public final long zzn() {
        return this.zzco;
    }

    public final long zzo() {
        return this.zzcq;
    }
}
