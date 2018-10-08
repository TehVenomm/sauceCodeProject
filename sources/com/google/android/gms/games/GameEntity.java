package com.google.android.gms.games;

import android.database.CharArrayBuffer;
import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.DowngradeableSafeParcel;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.util.zzg;
import com.google.android.gms.games.internal.GamesDowngradeableSafeParcel;
import java.util.Arrays;

public final class GameEntity extends GamesDowngradeableSafeParcel implements Game {
    public static final Creator<GameEntity> CREATOR = new zza();
    private final boolean zzckg;
    private final String zzdmz;
    private final String zzeby;
    private final String zzehw;
    private final String zzhba;
    private final String zzhbb;
    private final String zzhbc;
    private final Uri zzhbd;
    private final Uri zzhbe;
    private final Uri zzhbf;
    private final boolean zzhbg;
    private final boolean zzhbh;
    private final String zzhbi;
    private final int zzhbj;
    private final int zzhbk;
    private final int zzhbl;
    private final boolean zzhbm;
    private final boolean zzhbn;
    private final String zzhbo;
    private final String zzhbp;
    private final String zzhbq;
    private final boolean zzhbr;
    private final boolean zzhbs;
    private final String zzhbt;
    private final boolean zzhbu;

    static final class zza extends zza {
        zza() {
        }

        public final /* synthetic */ Object createFromParcel(Parcel parcel) {
            return zzi(parcel);
        }

        public final GameEntity zzi(Parcel parcel) {
            if (GamesDowngradeableSafeParcel.zze(DowngradeableSafeParcel.zzakc()) || DowngradeableSafeParcel.zzga(GameEntity.class.getCanonicalName())) {
                return super.zzi(parcel);
            }
            String readString = parcel.readString();
            String readString2 = parcel.readString();
            String readString3 = parcel.readString();
            String readString4 = parcel.readString();
            String readString5 = parcel.readString();
            String readString6 = parcel.readString();
            String readString7 = parcel.readString();
            Uri parse = readString7 == null ? null : Uri.parse(readString7);
            readString7 = parcel.readString();
            Uri parse2 = readString7 == null ? null : Uri.parse(readString7);
            readString7 = parcel.readString();
            return new GameEntity(readString, readString2, readString3, readString4, readString5, readString6, parse, parse2, readString7 == null ? null : Uri.parse(readString7), parcel.readInt() > 0, parcel.readInt() > 0, parcel.readString(), parcel.readInt(), parcel.readInt(), parcel.readInt(), false, false, null, null, null, false, false, false, null, false);
        }
    }

    public GameEntity(Game game) {
        this.zzehw = game.getApplicationId();
        this.zzhba = game.getPrimaryCategory();
        this.zzhbb = game.getSecondaryCategory();
        this.zzdmz = game.getDescription();
        this.zzhbc = game.getDeveloperName();
        this.zzeby = game.getDisplayName();
        this.zzhbd = game.getIconImageUri();
        this.zzhbo = game.getIconImageUrl();
        this.zzhbe = game.getHiResImageUri();
        this.zzhbp = game.getHiResImageUrl();
        this.zzhbf = game.getFeaturedImageUri();
        this.zzhbq = game.getFeaturedImageUrl();
        this.zzhbg = game.zzapg();
        this.zzhbh = game.zzapi();
        this.zzhbi = game.zzapj();
        this.zzhbj = 1;
        this.zzhbk = game.getAchievementTotalCount();
        this.zzhbl = game.getLeaderboardCount();
        this.zzhbm = game.isRealTimeMultiplayerEnabled();
        this.zzhbn = game.isTurnBasedMultiplayerEnabled();
        this.zzckg = game.isMuted();
        this.zzhbr = game.zzaph();
        this.zzhbs = game.areSnapshotsEnabled();
        this.zzhbt = game.getThemeColor();
        this.zzhbu = game.hasGamepadSupport();
    }

    GameEntity(String str, String str2, String str3, String str4, String str5, String str6, Uri uri, Uri uri2, Uri uri3, boolean z, boolean z2, String str7, int i, int i2, int i3, boolean z3, boolean z4, String str8, String str9, String str10, boolean z5, boolean z6, boolean z7, String str11, boolean z8) {
        this.zzehw = str;
        this.zzeby = str2;
        this.zzhba = str3;
        this.zzhbb = str4;
        this.zzdmz = str5;
        this.zzhbc = str6;
        this.zzhbd = uri;
        this.zzhbo = str8;
        this.zzhbe = uri2;
        this.zzhbp = str9;
        this.zzhbf = uri3;
        this.zzhbq = str10;
        this.zzhbg = z;
        this.zzhbh = z2;
        this.zzhbi = str7;
        this.zzhbj = i;
        this.zzhbk = i2;
        this.zzhbl = i3;
        this.zzhbm = z3;
        this.zzhbn = z4;
        this.zzckg = z5;
        this.zzhbr = z6;
        this.zzhbs = z7;
        this.zzhbt = str11;
        this.zzhbu = z8;
    }

    static int zza(Game game) {
        return Arrays.hashCode(new Object[]{game.getApplicationId(), game.getDisplayName(), game.getPrimaryCategory(), game.getSecondaryCategory(), game.getDescription(), game.getDeveloperName(), game.getIconImageUri(), game.getHiResImageUri(), game.getFeaturedImageUri(), Boolean.valueOf(game.zzapg()), Boolean.valueOf(game.zzapi()), game.zzapj(), Integer.valueOf(game.getAchievementTotalCount()), Integer.valueOf(game.getLeaderboardCount()), Boolean.valueOf(game.isRealTimeMultiplayerEnabled()), Boolean.valueOf(game.isTurnBasedMultiplayerEnabled()), Boolean.valueOf(game.isMuted()), Boolean.valueOf(game.zzaph()), Boolean.valueOf(game.areSnapshotsEnabled()), game.getThemeColor(), Boolean.valueOf(game.hasGamepadSupport())});
    }

    static boolean zza(Game game, Object obj) {
        if (!(obj instanceof Game)) {
            return false;
        }
        if (game == obj) {
            return true;
        }
        Game game2 = (Game) obj;
        if (!zzbf.equal(game2.getApplicationId(), game.getApplicationId()) || !zzbf.equal(game2.getDisplayName(), game.getDisplayName()) || !zzbf.equal(game2.getPrimaryCategory(), game.getPrimaryCategory()) || !zzbf.equal(game2.getSecondaryCategory(), game.getSecondaryCategory()) || !zzbf.equal(game2.getDescription(), game.getDescription()) || !zzbf.equal(game2.getDeveloperName(), game.getDeveloperName()) || !zzbf.equal(game2.getIconImageUri(), game.getIconImageUri()) || !zzbf.equal(game2.getHiResImageUri(), game.getHiResImageUri()) || !zzbf.equal(game2.getFeaturedImageUri(), game.getFeaturedImageUri()) || !zzbf.equal(Boolean.valueOf(game2.zzapg()), Boolean.valueOf(game.zzapg())) || !zzbf.equal(Boolean.valueOf(game2.zzapi()), Boolean.valueOf(game.zzapi())) || !zzbf.equal(game2.zzapj(), game.zzapj()) || !zzbf.equal(Integer.valueOf(game2.getAchievementTotalCount()), Integer.valueOf(game.getAchievementTotalCount())) || !zzbf.equal(Integer.valueOf(game2.getLeaderboardCount()), Integer.valueOf(game.getLeaderboardCount())) || !zzbf.equal(Boolean.valueOf(game2.isRealTimeMultiplayerEnabled()), Boolean.valueOf(game.isRealTimeMultiplayerEnabled()))) {
            return false;
        }
        boolean isTurnBasedMultiplayerEnabled = game2.isTurnBasedMultiplayerEnabled();
        boolean z = game.isTurnBasedMultiplayerEnabled() && zzbf.equal(Boolean.valueOf(game2.isMuted()), Boolean.valueOf(game.isMuted())) && zzbf.equal(Boolean.valueOf(game2.zzaph()), Boolean.valueOf(game.zzaph()));
        return zzbf.equal(Boolean.valueOf(isTurnBasedMultiplayerEnabled), Boolean.valueOf(z)) && zzbf.equal(Boolean.valueOf(game2.areSnapshotsEnabled()), Boolean.valueOf(game.areSnapshotsEnabled())) && zzbf.equal(game2.getThemeColor(), game.getThemeColor()) && zzbf.equal(Boolean.valueOf(game2.hasGamepadSupport()), Boolean.valueOf(game.hasGamepadSupport()));
    }

    static String zzb(Game game) {
        return zzbf.zzt(game).zzg("ApplicationId", game.getApplicationId()).zzg("DisplayName", game.getDisplayName()).zzg("PrimaryCategory", game.getPrimaryCategory()).zzg("SecondaryCategory", game.getSecondaryCategory()).zzg("Description", game.getDescription()).zzg("DeveloperName", game.getDeveloperName()).zzg("IconImageUri", game.getIconImageUri()).zzg("IconImageUrl", game.getIconImageUrl()).zzg("HiResImageUri", game.getHiResImageUri()).zzg("HiResImageUrl", game.getHiResImageUrl()).zzg("FeaturedImageUri", game.getFeaturedImageUri()).zzg("FeaturedImageUrl", game.getFeaturedImageUrl()).zzg("PlayEnabledGame", Boolean.valueOf(game.zzapg())).zzg("InstanceInstalled", Boolean.valueOf(game.zzapi())).zzg("InstancePackageName", game.zzapj()).zzg("AchievementTotalCount", Integer.valueOf(game.getAchievementTotalCount())).zzg("LeaderboardCount", Integer.valueOf(game.getLeaderboardCount())).zzg("RealTimeMultiplayerEnabled", Boolean.valueOf(game.isRealTimeMultiplayerEnabled())).zzg("TurnBasedMultiplayerEnabled", Boolean.valueOf(game.isTurnBasedMultiplayerEnabled())).zzg("AreSnapshotsEnabled", Boolean.valueOf(game.areSnapshotsEnabled())).zzg("ThemeColor", game.getThemeColor()).zzg("HasGamepadSupport", Boolean.valueOf(game.hasGamepadSupport())).toString();
    }

    public final boolean areSnapshotsEnabled() {
        return this.zzhbs;
    }

    public final boolean equals(Object obj) {
        return zza(this, obj);
    }

    public final Game freeze() {
        return this;
    }

    public final int getAchievementTotalCount() {
        return this.zzhbk;
    }

    public final String getApplicationId() {
        return this.zzehw;
    }

    public final String getDescription() {
        return this.zzdmz;
    }

    public final void getDescription(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.zzdmz, charArrayBuffer);
    }

    public final String getDeveloperName() {
        return this.zzhbc;
    }

    public final void getDeveloperName(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.zzhbc, charArrayBuffer);
    }

    public final String getDisplayName() {
        return this.zzeby;
    }

    public final void getDisplayName(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.zzeby, charArrayBuffer);
    }

    public final Uri getFeaturedImageUri() {
        return this.zzhbf;
    }

    public final String getFeaturedImageUrl() {
        return this.zzhbq;
    }

    public final Uri getHiResImageUri() {
        return this.zzhbe;
    }

    public final String getHiResImageUrl() {
        return this.zzhbp;
    }

    public final Uri getIconImageUri() {
        return this.zzhbd;
    }

    public final String getIconImageUrl() {
        return this.zzhbo;
    }

    public final int getLeaderboardCount() {
        return this.zzhbl;
    }

    public final String getPrimaryCategory() {
        return this.zzhba;
    }

    public final String getSecondaryCategory() {
        return this.zzhbb;
    }

    public final String getThemeColor() {
        return this.zzhbt;
    }

    public final boolean hasGamepadSupport() {
        return this.zzhbu;
    }

    public final int hashCode() {
        return zza(this);
    }

    public final boolean isDataValid() {
        return true;
    }

    public final boolean isMuted() {
        return this.zzckg;
    }

    public final boolean isRealTimeMultiplayerEnabled() {
        return this.zzhbm;
    }

    public final boolean isTurnBasedMultiplayerEnabled() {
        return this.zzhbn;
    }

    public final String toString() {
        return zzb(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getApplicationId(), false);
        zzd.zza(parcel, 2, getDisplayName(), false);
        zzd.zza(parcel, 3, getPrimaryCategory(), false);
        zzd.zza(parcel, 4, getSecondaryCategory(), false);
        zzd.zza(parcel, 5, getDescription(), false);
        zzd.zza(parcel, 6, getDeveloperName(), false);
        zzd.zza(parcel, 7, getIconImageUri(), i, false);
        zzd.zza(parcel, 8, getHiResImageUri(), i, false);
        zzd.zza(parcel, 9, getFeaturedImageUri(), i, false);
        zzd.zza(parcel, 10, this.zzhbg);
        zzd.zza(parcel, 11, this.zzhbh);
        zzd.zza(parcel, 12, this.zzhbi, false);
        zzd.zzc(parcel, 13, this.zzhbj);
        zzd.zzc(parcel, 14, getAchievementTotalCount());
        zzd.zzc(parcel, 15, getLeaderboardCount());
        zzd.zza(parcel, 16, isRealTimeMultiplayerEnabled());
        zzd.zza(parcel, 17, isTurnBasedMultiplayerEnabled());
        zzd.zza(parcel, 18, getIconImageUrl(), false);
        zzd.zza(parcel, 19, getHiResImageUrl(), false);
        zzd.zza(parcel, 20, getFeaturedImageUrl(), false);
        zzd.zza(parcel, 21, this.zzckg);
        zzd.zza(parcel, 22, this.zzhbr);
        zzd.zza(parcel, 23, areSnapshotsEnabled());
        zzd.zza(parcel, 24, getThemeColor(), false);
        zzd.zza(parcel, 25, hasGamepadSupport());
        zzd.zzai(parcel, zze);
    }

    public final boolean zzapg() {
        return this.zzhbg;
    }

    public final boolean zzaph() {
        return this.zzhbr;
    }

    public final boolean zzapi() {
        return this.zzhbh;
    }

    public final String zzapj() {
        return this.zzhbi;
    }
}
