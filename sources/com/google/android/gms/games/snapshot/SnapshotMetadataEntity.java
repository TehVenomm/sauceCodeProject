package com.google.android.gms.games.snapshot;

import android.database.CharArrayBuffer;
import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.util.zzg;
import com.google.android.gms.games.Game;
import com.google.android.gms.games.GameEntity;
import com.google.android.gms.games.Player;
import com.google.android.gms.games.PlayerEntity;
import com.google.android.gms.games.internal.zzc;
import java.util.Arrays;

public final class SnapshotMetadataEntity extends zzc implements SnapshotMetadata {
    public static final Creator<SnapshotMetadataEntity> CREATOR = new zzf();
    private final String zzdmz;
    private final String zzehi;
    private final String zzhes;
    private final GameEntity zzhiw;
    private final Uri zzhol;
    private final PlayerEntity zzhoo;
    private final String zzhop;
    private final long zzhoq;
    private final long zzhor;
    private final float zzhos;
    private final String zzhot;
    private final boolean zzhou;
    private final long zzhov;
    private final String zzhow;

    SnapshotMetadataEntity(GameEntity gameEntity, PlayerEntity playerEntity, String str, Uri uri, String str2, String str3, String str4, long j, long j2, float f, String str5, boolean z, long j3, String str6) {
        this.zzhiw = gameEntity;
        this.zzhoo = playerEntity;
        this.zzhes = str;
        this.zzhol = uri;
        this.zzhop = str2;
        this.zzhos = f;
        this.zzehi = str3;
        this.zzdmz = str4;
        this.zzhoq = j;
        this.zzhor = j2;
        this.zzhot = str5;
        this.zzhou = z;
        this.zzhov = j3;
        this.zzhow = str6;
    }

    public SnapshotMetadataEntity(SnapshotMetadata snapshotMetadata) {
        this.zzhiw = new GameEntity(snapshotMetadata.getGame());
        this.zzhoo = new PlayerEntity(snapshotMetadata.getOwner());
        this.zzhes = snapshotMetadata.getSnapshotId();
        this.zzhol = snapshotMetadata.getCoverImageUri();
        this.zzhop = snapshotMetadata.getCoverImageUrl();
        this.zzhos = snapshotMetadata.getCoverImageAspectRatio();
        this.zzehi = snapshotMetadata.getTitle();
        this.zzdmz = snapshotMetadata.getDescription();
        this.zzhoq = snapshotMetadata.getLastModifiedTimestamp();
        this.zzhor = snapshotMetadata.getPlayedTime();
        this.zzhot = snapshotMetadata.getUniqueName();
        this.zzhou = snapshotMetadata.hasChangePending();
        this.zzhov = snapshotMetadata.getProgressValue();
        this.zzhow = snapshotMetadata.getDeviceName();
    }

    static int zza(SnapshotMetadata snapshotMetadata) {
        return Arrays.hashCode(new Object[]{snapshotMetadata.getGame(), snapshotMetadata.getOwner(), snapshotMetadata.getSnapshotId(), snapshotMetadata.getCoverImageUri(), Float.valueOf(snapshotMetadata.getCoverImageAspectRatio()), snapshotMetadata.getTitle(), snapshotMetadata.getDescription(), Long.valueOf(snapshotMetadata.getLastModifiedTimestamp()), Long.valueOf(snapshotMetadata.getPlayedTime()), snapshotMetadata.getUniqueName(), Boolean.valueOf(snapshotMetadata.hasChangePending()), Long.valueOf(snapshotMetadata.getProgressValue()), snapshotMetadata.getDeviceName()});
    }

    static boolean zza(SnapshotMetadata snapshotMetadata, Object obj) {
        if (!(obj instanceof SnapshotMetadata)) {
            return false;
        }
        if (snapshotMetadata == obj) {
            return true;
        }
        SnapshotMetadata snapshotMetadata2 = (SnapshotMetadata) obj;
        return zzbf.equal(snapshotMetadata2.getGame(), snapshotMetadata.getGame()) && zzbf.equal(snapshotMetadata2.getOwner(), snapshotMetadata.getOwner()) && zzbf.equal(snapshotMetadata2.getSnapshotId(), snapshotMetadata.getSnapshotId()) && zzbf.equal(snapshotMetadata2.getCoverImageUri(), snapshotMetadata.getCoverImageUri()) && zzbf.equal(Float.valueOf(snapshotMetadata2.getCoverImageAspectRatio()), Float.valueOf(snapshotMetadata.getCoverImageAspectRatio())) && zzbf.equal(snapshotMetadata2.getTitle(), snapshotMetadata.getTitle()) && zzbf.equal(snapshotMetadata2.getDescription(), snapshotMetadata.getDescription()) && zzbf.equal(Long.valueOf(snapshotMetadata2.getLastModifiedTimestamp()), Long.valueOf(snapshotMetadata.getLastModifiedTimestamp())) && zzbf.equal(Long.valueOf(snapshotMetadata2.getPlayedTime()), Long.valueOf(snapshotMetadata.getPlayedTime())) && zzbf.equal(snapshotMetadata2.getUniqueName(), snapshotMetadata.getUniqueName()) && zzbf.equal(Boolean.valueOf(snapshotMetadata2.hasChangePending()), Boolean.valueOf(snapshotMetadata.hasChangePending())) && zzbf.equal(Long.valueOf(snapshotMetadata2.getProgressValue()), Long.valueOf(snapshotMetadata.getProgressValue())) && zzbf.equal(snapshotMetadata2.getDeviceName(), snapshotMetadata.getDeviceName());
    }

    static String zzb(SnapshotMetadata snapshotMetadata) {
        return zzbf.zzt(snapshotMetadata).zzg("Game", snapshotMetadata.getGame()).zzg("Owner", snapshotMetadata.getOwner()).zzg("SnapshotId", snapshotMetadata.getSnapshotId()).zzg("CoverImageUri", snapshotMetadata.getCoverImageUri()).zzg("CoverImageUrl", snapshotMetadata.getCoverImageUrl()).zzg("CoverImageAspectRatio", Float.valueOf(snapshotMetadata.getCoverImageAspectRatio())).zzg("Description", snapshotMetadata.getDescription()).zzg("LastModifiedTimestamp", Long.valueOf(snapshotMetadata.getLastModifiedTimestamp())).zzg("PlayedTime", Long.valueOf(snapshotMetadata.getPlayedTime())).zzg("UniqueName", snapshotMetadata.getUniqueName()).zzg("ChangePending", Boolean.valueOf(snapshotMetadata.hasChangePending())).zzg("ProgressValue", Long.valueOf(snapshotMetadata.getProgressValue())).zzg("DeviceName", snapshotMetadata.getDeviceName()).toString();
    }

    public final boolean equals(Object obj) {
        return zza(this, obj);
    }

    public final SnapshotMetadata freeze() {
        return this;
    }

    public final float getCoverImageAspectRatio() {
        return this.zzhos;
    }

    public final Uri getCoverImageUri() {
        return this.zzhol;
    }

    public final String getCoverImageUrl() {
        return this.zzhop;
    }

    public final String getDescription() {
        return this.zzdmz;
    }

    public final void getDescription(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.zzdmz, charArrayBuffer);
    }

    public final String getDeviceName() {
        return this.zzhow;
    }

    public final Game getGame() {
        return this.zzhiw;
    }

    public final long getLastModifiedTimestamp() {
        return this.zzhoq;
    }

    public final Player getOwner() {
        return this.zzhoo;
    }

    public final long getPlayedTime() {
        return this.zzhor;
    }

    public final long getProgressValue() {
        return this.zzhov;
    }

    public final String getSnapshotId() {
        return this.zzhes;
    }

    public final String getTitle() {
        return this.zzehi;
    }

    public final String getUniqueName() {
        return this.zzhot;
    }

    public final boolean hasChangePending() {
        return this.zzhou;
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
        zzd.zza(parcel, 1, getGame(), i, false);
        zzd.zza(parcel, 2, getOwner(), i, false);
        zzd.zza(parcel, 3, getSnapshotId(), false);
        zzd.zza(parcel, 5, getCoverImageUri(), i, false);
        zzd.zza(parcel, 6, getCoverImageUrl(), false);
        zzd.zza(parcel, 7, this.zzehi, false);
        zzd.zza(parcel, 8, getDescription(), false);
        zzd.zza(parcel, 9, getLastModifiedTimestamp());
        zzd.zza(parcel, 10, getPlayedTime());
        zzd.zza(parcel, 11, getCoverImageAspectRatio());
        zzd.zza(parcel, 12, getUniqueName(), false);
        zzd.zza(parcel, 13, hasChangePending());
        zzd.zza(parcel, 14, getProgressValue());
        zzd.zza(parcel, 15, getDeviceName(), false);
        zzd.zzai(parcel, zze);
    }
}
