package com.google.android.gms.games;

import android.database.CharArrayBuffer;
import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.DowngradeableSafeParcel;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzc;
import com.google.android.gms.common.util.zzg;
import com.google.android.gms.games.internal.GamesDowngradeableSafeParcel;
import com.google.android.gms.games.internal.player.zzb;
import java.util.Arrays;

public final class PlayerEntity extends GamesDowngradeableSafeParcel implements Player {
    public static final Creator<PlayerEntity> CREATOR = new zza();
    private final String mName;
    private final boolean zzdhy;
    private String zzeby;
    private final String zzehi;
    private String zzezq;
    private final Uri zzhbd;
    private final Uri zzhbe;
    private final String zzhbo;
    private final String zzhbp;
    private final long zzhcn;
    private final int zzhco;
    private final long zzhcp;
    private final zzb zzhcq;
    private final PlayerLevelInfo zzhcr;
    private final boolean zzhcs;
    private final boolean zzhct;
    private final String zzhcu;
    private final Uri zzhcv;
    private final String zzhcw;
    private final Uri zzhcx;
    private final String zzhcy;
    private final int zzhcz;
    private final long zzhda;

    static final class zza extends zzg {
        zza() {
        }

        public final /* synthetic */ Object createFromParcel(Parcel parcel) {
            return zzj(parcel);
        }

        public final PlayerEntity zzj(Parcel parcel) {
            if (GamesDowngradeableSafeParcel.zze(DowngradeableSafeParcel.zzakc()) || DowngradeableSafeParcel.zzga(PlayerEntity.class.getCanonicalName())) {
                return super.zzj(parcel);
            }
            String readString = parcel.readString();
            String readString2 = parcel.readString();
            String readString3 = parcel.readString();
            String readString4 = parcel.readString();
            return new PlayerEntity(readString, readString2, readString3 == null ? null : Uri.parse(readString3), readString4 == null ? null : Uri.parse(readString4), parcel.readLong(), -1, -1, null, null, null, null, null, true, false, parcel.readString(), parcel.readString(), null, null, null, null, -1, -1, false);
        }
    }

    public PlayerEntity(Player player) {
        this(player, true);
    }

    private PlayerEntity(Player player, boolean z) {
        this.zzezq = player.getPlayerId();
        this.zzeby = player.getDisplayName();
        this.zzhbd = player.getIconImageUri();
        this.zzhbo = player.getIconImageUrl();
        this.zzhbe = player.getHiResImageUri();
        this.zzhbp = player.getHiResImageUrl();
        this.zzhcn = player.getRetrievedTimestamp();
        this.zzhco = player.zzapo();
        this.zzhcp = player.getLastPlayedWithTimestamp();
        this.zzehi = player.getTitle();
        this.zzhcs = player.zzapp();
        com.google.android.gms.games.internal.player.zza zzapq = player.zzapq();
        this.zzhcq = zzapq == null ? null : new zzb(zzapq);
        this.zzhcr = player.getLevelInfo();
        this.zzhct = player.zzapn();
        this.zzhcu = player.zzapm();
        this.mName = player.getName();
        this.zzhcv = player.getBannerImageLandscapeUri();
        this.zzhcw = player.getBannerImageLandscapeUrl();
        this.zzhcx = player.getBannerImagePortraitUri();
        this.zzhcy = player.getBannerImagePortraitUrl();
        this.zzhcz = player.zzapr();
        this.zzhda = player.zzaps();
        this.zzdhy = player.isMuted();
        zzc.zzr(this.zzezq);
        zzc.zzr(this.zzeby);
        zzc.zzbg(this.zzhcn > 0);
    }

    PlayerEntity(String str, String str2, Uri uri, Uri uri2, long j, int i, long j2, String str3, String str4, String str5, zzb zzb, PlayerLevelInfo playerLevelInfo, boolean z, boolean z2, String str6, String str7, Uri uri3, String str8, Uri uri4, String str9, int i2, long j3, boolean z3) {
        this.zzezq = str;
        this.zzeby = str2;
        this.zzhbd = uri;
        this.zzhbo = str3;
        this.zzhbe = uri2;
        this.zzhbp = str4;
        this.zzhcn = j;
        this.zzhco = i;
        this.zzhcp = j2;
        this.zzehi = str5;
        this.zzhcs = z;
        this.zzhcq = zzb;
        this.zzhcr = playerLevelInfo;
        this.zzhct = z2;
        this.zzhcu = str6;
        this.mName = str7;
        this.zzhcv = uri3;
        this.zzhcw = str8;
        this.zzhcx = uri4;
        this.zzhcy = str9;
        this.zzhcz = i2;
        this.zzhda = j3;
        this.zzdhy = z3;
    }

    static int zza(Player player) {
        return Arrays.hashCode(new Object[]{player.getPlayerId(), player.getDisplayName(), Boolean.valueOf(player.zzapn()), player.getIconImageUri(), player.getHiResImageUri(), Long.valueOf(player.getRetrievedTimestamp()), player.getTitle(), player.getLevelInfo(), player.zzapm(), player.getName(), player.getBannerImageLandscapeUri(), player.getBannerImagePortraitUri(), Integer.valueOf(player.zzapr()), Long.valueOf(player.zzaps()), Boolean.valueOf(player.isMuted())});
    }

    static boolean zza(Player player, Object obj) {
        if (!(obj instanceof Player)) {
            return false;
        }
        if (player == obj) {
            return true;
        }
        Player player2 = (Player) obj;
        return zzbf.equal(player2.getPlayerId(), player.getPlayerId()) && zzbf.equal(player2.getDisplayName(), player.getDisplayName()) && zzbf.equal(Boolean.valueOf(player2.zzapn()), Boolean.valueOf(player.zzapn())) && zzbf.equal(player2.getIconImageUri(), player.getIconImageUri()) && zzbf.equal(player2.getHiResImageUri(), player.getHiResImageUri()) && zzbf.equal(Long.valueOf(player2.getRetrievedTimestamp()), Long.valueOf(player.getRetrievedTimestamp())) && zzbf.equal(player2.getTitle(), player.getTitle()) && zzbf.equal(player2.getLevelInfo(), player.getLevelInfo()) && zzbf.equal(player2.zzapm(), player.zzapm()) && zzbf.equal(player2.getName(), player.getName()) && zzbf.equal(player2.getBannerImageLandscapeUri(), player.getBannerImageLandscapeUri()) && zzbf.equal(player2.getBannerImagePortraitUri(), player.getBannerImagePortraitUri()) && zzbf.equal(Integer.valueOf(player2.zzapr()), Integer.valueOf(player.zzapr())) && zzbf.equal(Long.valueOf(player2.zzaps()), Long.valueOf(player.zzaps())) && zzbf.equal(Boolean.valueOf(player2.isMuted()), Boolean.valueOf(player.isMuted()));
    }

    static String zzb(Player player) {
        return zzbf.zzt(player).zzg("PlayerId", player.getPlayerId()).zzg("DisplayName", player.getDisplayName()).zzg("HasDebugAccess", Boolean.valueOf(player.zzapn())).zzg("IconImageUri", player.getIconImageUri()).zzg("IconImageUrl", player.getIconImageUrl()).zzg("HiResImageUri", player.getHiResImageUri()).zzg("HiResImageUrl", player.getHiResImageUrl()).zzg("RetrievedTimestamp", Long.valueOf(player.getRetrievedTimestamp())).zzg("Title", player.getTitle()).zzg("LevelInfo", player.getLevelInfo()).zzg("GamerTag", player.zzapm()).zzg("Name", player.getName()).zzg("BannerImageLandscapeUri", player.getBannerImageLandscapeUri()).zzg("BannerImageLandscapeUrl", player.getBannerImageLandscapeUrl()).zzg("BannerImagePortraitUri", player.getBannerImagePortraitUri()).zzg("BannerImagePortraitUrl", player.getBannerImagePortraitUrl()).zzg("GamerFriendStatus", Integer.valueOf(player.zzapr())).zzg("GamerFriendUpdateTimestamp", Long.valueOf(player.zzaps())).zzg("IsMuted", Boolean.valueOf(player.isMuted())).toString();
    }

    public final boolean equals(Object obj) {
        return zza(this, obj);
    }

    public final Player freeze() {
        return this;
    }

    public final Uri getBannerImageLandscapeUri() {
        return this.zzhcv;
    }

    public final String getBannerImageLandscapeUrl() {
        return this.zzhcw;
    }

    public final Uri getBannerImagePortraitUri() {
        return this.zzhcx;
    }

    public final String getBannerImagePortraitUrl() {
        return this.zzhcy;
    }

    public final String getDisplayName() {
        return this.zzeby;
    }

    public final void getDisplayName(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.zzeby, charArrayBuffer);
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

    public final long getLastPlayedWithTimestamp() {
        return this.zzhcp;
    }

    public final PlayerLevelInfo getLevelInfo() {
        return this.zzhcr;
    }

    public final String getName() {
        return this.mName;
    }

    public final String getPlayerId() {
        return this.zzezq;
    }

    public final long getRetrievedTimestamp() {
        return this.zzhcn;
    }

    public final String getTitle() {
        return this.zzehi;
    }

    public final void getTitle(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.zzehi, charArrayBuffer);
    }

    public final boolean hasHiResImage() {
        return getHiResImageUri() != null;
    }

    public final boolean hasIconImage() {
        return getIconImageUri() != null;
    }

    public final int hashCode() {
        return zza(this);
    }

    public final boolean isDataValid() {
        return true;
    }

    public final boolean isMuted() {
        return this.zzdhy;
    }

    public final String toString() {
        return zzb(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getPlayerId(), false);
        zzd.zza(parcel, 2, getDisplayName(), false);
        zzd.zza(parcel, 3, getIconImageUri(), i, false);
        zzd.zza(parcel, 4, getHiResImageUri(), i, false);
        zzd.zza(parcel, 5, getRetrievedTimestamp());
        zzd.zzc(parcel, 6, this.zzhco);
        zzd.zza(parcel, 7, getLastPlayedWithTimestamp());
        zzd.zza(parcel, 8, getIconImageUrl(), false);
        zzd.zza(parcel, 9, getHiResImageUrl(), false);
        zzd.zza(parcel, 14, getTitle(), false);
        zzd.zza(parcel, 15, this.zzhcq, i, false);
        zzd.zza(parcel, 16, getLevelInfo(), i, false);
        zzd.zza(parcel, 18, this.zzhcs);
        zzd.zza(parcel, 19, this.zzhct);
        zzd.zza(parcel, 20, this.zzhcu, false);
        zzd.zza(parcel, 21, this.mName, false);
        zzd.zza(parcel, 22, getBannerImageLandscapeUri(), i, false);
        zzd.zza(parcel, 23, getBannerImageLandscapeUrl(), false);
        zzd.zza(parcel, 24, getBannerImagePortraitUri(), i, false);
        zzd.zza(parcel, 25, getBannerImagePortraitUrl(), false);
        zzd.zzc(parcel, 26, this.zzhcz);
        zzd.zza(parcel, 27, this.zzhda);
        zzd.zza(parcel, 28, this.zzdhy);
        zzd.zzai(parcel, zze);
    }

    public final String zzapm() {
        return this.zzhcu;
    }

    public final boolean zzapn() {
        return this.zzhct;
    }

    public final int zzapo() {
        return this.zzhco;
    }

    public final boolean zzapp() {
        return this.zzhcs;
    }

    public final com.google.android.gms.games.internal.player.zza zzapq() {
        return this.zzhcq;
    }

    public final int zzapr() {
        return this.zzhcz;
    }

    public final long zzaps() {
        return this.zzhda;
    }
}
