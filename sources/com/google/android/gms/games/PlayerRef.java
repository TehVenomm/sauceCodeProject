package com.google.android.gms.games;

import android.database.CharArrayBuffer;
import android.net.Uri;
import android.os.Parcel;
import com.google.android.gms.common.data.DataBufferRef;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.games.internal.player.zza;
import com.google.android.gms.games.internal.player.zzc;
import com.google.android.gms.games.internal.player.zzd;

public final class PlayerRef extends DataBufferRef implements Player {
    private final PlayerLevelInfo zzcf;
    private final zzd zzcy;
    private final zzc zzcz;

    public PlayerRef(DataHolder dataHolder, int i) {
        this(dataHolder, i, null);
    }

    public PlayerRef(DataHolder dataHolder, int i, String str) {
        super(dataHolder, i);
        this.zzcy = new zzd(str);
        this.zzcz = new zzc(dataHolder, i, this.zzcy);
        if (!hasNull(this.zzcy.zzml) && getLong(this.zzcy.zzml) != -1) {
            int integer = getInteger(this.zzcy.zzmm);
            int integer2 = getInteger(this.zzcy.zzmp);
            PlayerLevel playerLevel = new PlayerLevel(integer, getLong(this.zzcy.zzmn), getLong(this.zzcy.zzmo));
            this.zzcf = new PlayerLevelInfo(getLong(this.zzcy.zzml), getLong(this.zzcy.zzmr), playerLevel, integer != integer2 ? new PlayerLevel(integer2, getLong(this.zzcy.zzmo), getLong(this.zzcy.zzmq)) : playerLevel);
            return;
        }
        this.zzcf = null;
    }

    public final int describeContents() {
        return 0;
    }

    public final boolean equals(Object obj) {
        return PlayerEntity.zza(this, obj);
    }

    public final /* synthetic */ Object freeze() {
        return new PlayerEntity(this);
    }

    public final Uri getBannerImageLandscapeUri() {
        return parseUri(this.zzcy.zznb);
    }

    public final String getBannerImageLandscapeUrl() {
        return getString(this.zzcy.zznc);
    }

    public final Uri getBannerImagePortraitUri() {
        return parseUri(this.zzcy.zznd);
    }

    public final String getBannerImagePortraitUrl() {
        return getString(this.zzcy.zzne);
    }

    public final String getDisplayName() {
        return getString(this.zzcy.zzmd);
    }

    public final void getDisplayName(CharArrayBuffer charArrayBuffer) {
        copyToBuffer(this.zzcy.zzmd, charArrayBuffer);
    }

    public final Uri getHiResImageUri() {
        return parseUri(this.zzcy.zzmg);
    }

    public final String getHiResImageUrl() {
        return getString(this.zzcy.zzmh);
    }

    public final Uri getIconImageUri() {
        return parseUri(this.zzcy.zzme);
    }

    public final String getIconImageUrl() {
        return getString(this.zzcy.zzmf);
    }

    public final long getLastPlayedWithTimestamp() {
        if (!hasColumn(this.zzcy.zzmk) || hasNull(this.zzcy.zzmk)) {
            return -1;
        }
        return getLong(this.zzcy.zzmk);
    }

    public final PlayerLevelInfo getLevelInfo() {
        return this.zzcf;
    }

    public final String getName() {
        return getString(this.zzcy.name);
    }

    public final String getPlayerId() {
        return getString(this.zzcy.zzmc);
    }

    public final long getRetrievedTimestamp() {
        return getLong(this.zzcy.zzmi);
    }

    public final String getTitle() {
        return getString(this.zzcy.zzcd);
    }

    public final void getTitle(CharArrayBuffer charArrayBuffer) {
        copyToBuffer(this.zzcy.zzcd, charArrayBuffer);
    }

    public final boolean hasHiResImage() {
        return getHiResImageUri() != null;
    }

    public final boolean hasIconImage() {
        return getIconImageUri() != null;
    }

    public final int hashCode() {
        return PlayerEntity.zza((Player) this);
    }

    public final boolean isMuted() {
        return getBoolean(this.zzcy.zznh);
    }

    public final String toString() {
        return PlayerEntity.zzb(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        ((PlayerEntity) ((Player) freeze())).writeToParcel(parcel, i);
    }

    public final String zzh() {
        return getString(this.zzcy.zzci);
    }

    public final boolean zzi() {
        return getBoolean(this.zzcy.zzna);
    }

    public final int zzj() {
        return getInteger(this.zzcy.zzmj);
    }

    public final boolean zzk() {
        return getBoolean(this.zzcy.zzmt);
    }

    public final zza zzl() {
        if (hasNull(this.zzcy.zzmu)) {
            return null;
        }
        return this.zzcz;
    }

    public final int zzm() {
        return getInteger(this.zzcy.zznf);
    }

    public final long zzn() {
        return getLong(this.zzcy.zzng);
    }

    public final long zzo() {
        return getLong(this.zzcy.zzni);
    }
}
