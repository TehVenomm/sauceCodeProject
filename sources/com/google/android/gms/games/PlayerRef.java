package com.google.android.gms.games;

import android.database.CharArrayBuffer;
import android.net.Uri;
import android.os.Parcel;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.data.zzc;
import com.google.android.gms.games.internal.player.zza;
import com.google.android.gms.games.internal.player.zzd;
import com.google.android.gms.games.internal.player.zze;

public final class PlayerRef extends zzc implements Player {
    private final PlayerLevelInfo zzhcr;
    private final zze zzhdi;
    private final zzd zzhdj;

    public PlayerRef(DataHolder dataHolder, int i) {
        this(dataHolder, i, null);
    }

    public PlayerRef(DataHolder dataHolder, int i, String str) {
        super(dataHolder, i);
        this.zzhdi = new zze(str);
        this.zzhdj = new zzd(dataHolder, i, this.zzhdi);
        Object obj = (zzfv(this.zzhdi.zzhjs) || getLong(this.zzhdi.zzhjs) == -1) ? null : 1;
        if (obj != null) {
            int integer = getInteger(this.zzhdi.zzhjt);
            int integer2 = getInteger(this.zzhdi.zzhjw);
            PlayerLevel playerLevel = new PlayerLevel(integer, getLong(this.zzhdi.zzhju), getLong(this.zzhdi.zzhjv));
            this.zzhcr = new PlayerLevelInfo(getLong(this.zzhdi.zzhjs), getLong(this.zzhdi.zzhjy), playerLevel, integer != integer2 ? new PlayerLevel(integer2, getLong(this.zzhdi.zzhjv), getLong(this.zzhdi.zzhjx)) : playerLevel);
            return;
        }
        this.zzhcr = null;
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
        return zzfu(this.zzhdi.zzhkj);
    }

    public final String getBannerImageLandscapeUrl() {
        return getString(this.zzhdi.zzhkk);
    }

    public final Uri getBannerImagePortraitUri() {
        return zzfu(this.zzhdi.zzhkl);
    }

    public final String getBannerImagePortraitUrl() {
        return getString(this.zzhdi.zzhkm);
    }

    public final String getDisplayName() {
        return getString(this.zzhdi.zzhjk);
    }

    public final void getDisplayName(CharArrayBuffer charArrayBuffer) {
        zza(this.zzhdi.zzhjk, charArrayBuffer);
    }

    public final Uri getHiResImageUri() {
        return zzfu(this.zzhdi.zzhjn);
    }

    public final String getHiResImageUrl() {
        return getString(this.zzhdi.zzhjo);
    }

    public final Uri getIconImageUri() {
        return zzfu(this.zzhdi.zzhjl);
    }

    public final String getIconImageUrl() {
        return getString(this.zzhdi.zzhjm);
    }

    public final long getLastPlayedWithTimestamp() {
        return (!zzft(this.zzhdi.zzhjr) || zzfv(this.zzhdi.zzhjr)) ? -1 : getLong(this.zzhdi.zzhjr);
    }

    public final PlayerLevelInfo getLevelInfo() {
        return this.zzhcr;
    }

    public final String getName() {
        return getString(this.zzhdi.name);
    }

    public final String getPlayerId() {
        return getString(this.zzhdi.zzhjj);
    }

    public final long getRetrievedTimestamp() {
        return getLong(this.zzhdi.zzhjp);
    }

    public final String getTitle() {
        return getString(this.zzhdi.title);
    }

    public final void getTitle(CharArrayBuffer charArrayBuffer) {
        zza(this.zzhdi.title, charArrayBuffer);
    }

    public final boolean hasHiResImage() {
        return getHiResImageUri() != null;
    }

    public final boolean hasIconImage() {
        return getIconImageUri() != null;
    }

    public final int hashCode() {
        return PlayerEntity.zza(this);
    }

    public final boolean isMuted() {
        return getBoolean(this.zzhdi.zzhkp);
    }

    public final String toString() {
        return PlayerEntity.zzb(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        ((PlayerEntity) ((Player) freeze())).writeToParcel(parcel, i);
    }

    public final String zzapm() {
        return getString(this.zzhdi.zzhki);
    }

    public final boolean zzapn() {
        return getBoolean(this.zzhdi.zzhkh);
    }

    public final int zzapo() {
        return getInteger(this.zzhdi.zzhjq);
    }

    public final boolean zzapp() {
        return getBoolean(this.zzhdi.zzhka);
    }

    public final zza zzapq() {
        return zzfv(this.zzhdi.zzhkb) ? null : this.zzhdj;
    }

    public final int zzapr() {
        return getInteger(this.zzhdi.zzhkn);
    }

    public final long zzaps() {
        return getLong(this.zzhdi.zzhko);
    }
}
