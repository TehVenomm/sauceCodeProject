package com.google.android.gms.games.snapshot;

import android.database.CharArrayBuffer;
import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.apps.common.proguard.UsedByReflection;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import com.google.android.gms.common.util.DataUtils;
import com.google.android.gms.games.Game;
import com.google.android.gms.games.GameEntity;
import com.google.android.gms.games.Player;
import com.google.android.gms.games.PlayerEntity;
import com.google.android.gms.games.internal.zzd;

@UsedByReflection("GamesClientImpl.java")
@Class(creator = "SnapshotMetadataEntityCreator")
@Reserved({1000})
public final class SnapshotMetadataEntity extends zzd implements SnapshotMetadata {
    public static final Creator<SnapshotMetadataEntity> CREATOR = new zzd();
    @Field(getter = "getDescription", mo13990id = 8)
    private final String description;
    @Nullable
    @Field(getter = "getDeviceName", mo13990id = 15)
    private final String deviceName;
    @Field(getter = "getTitle", mo13990id = 7)
    private final String zzcd;
    @Field(getter = "getSnapshotId", mo13990id = 3)
    private final String zzhs;
    @Field(getter = "getGame", mo13990id = 1)
    private final GameEntity zzlp;
    @Nullable
    @Field(getter = "getCoverImageUri", mo13990id = 5)
    private final Uri zzrw;
    @Field(getter = "getOwner", mo13990id = 2)
    private final PlayerEntity zzrz;
    @Nullable
    @Field(getter = "getCoverImageUrl", mo13990id = 6)
    private final String zzsa;
    @Field(getter = "getLastModifiedTimestamp", mo13990id = 9)
    private final long zzsb;
    @Field(getter = "getPlayedTime", mo13990id = 10)
    private final long zzsc;
    @Field(getter = "getCoverImageAspectRatio", mo13990id = 11)
    private final float zzsd;
    @Field(getter = "getUniqueName", mo13990id = 12)
    private final String zzse;
    @Field(getter = "hasChangePending", mo13990id = 13)
    private final boolean zzsf;
    @Field(getter = "getProgressValue", mo13990id = 14)
    private final long zzsg;

    @Constructor
    SnapshotMetadataEntity(@Param(mo13993id = 1) GameEntity gameEntity, @Param(mo13993id = 2) PlayerEntity playerEntity, @Param(mo13993id = 3) String str, @Nullable @Param(mo13993id = 5) Uri uri, @Nullable @Param(mo13993id = 6) String str2, @Param(mo13993id = 7) String str3, @Param(mo13993id = 8) String str4, @Param(mo13993id = 9) long j, @Param(mo13993id = 10) long j2, @Param(mo13993id = 11) float f, @Param(mo13993id = 12) String str5, @Param(mo13993id = 13) boolean z, @Param(mo13993id = 14) long j3, @Nullable @Param(mo13993id = 15) String str6) {
        this.zzlp = gameEntity;
        this.zzrz = playerEntity;
        this.zzhs = str;
        this.zzrw = uri;
        this.zzsa = str2;
        this.zzsd = f;
        this.zzcd = str3;
        this.description = str4;
        this.zzsb = j;
        this.zzsc = j2;
        this.zzse = str5;
        this.zzsf = z;
        this.zzsg = j3;
        this.deviceName = str6;
    }

    public SnapshotMetadataEntity(SnapshotMetadata snapshotMetadata) {
        this(snapshotMetadata, new PlayerEntity(snapshotMetadata.getOwner()));
    }

    private SnapshotMetadataEntity(SnapshotMetadata snapshotMetadata, PlayerEntity playerEntity) {
        this.zzlp = new GameEntity(snapshotMetadata.getGame());
        this.zzrz = playerEntity;
        this.zzhs = snapshotMetadata.getSnapshotId();
        this.zzrw = snapshotMetadata.getCoverImageUri();
        this.zzsa = snapshotMetadata.getCoverImageUrl();
        this.zzsd = snapshotMetadata.getCoverImageAspectRatio();
        this.zzcd = snapshotMetadata.getTitle();
        this.description = snapshotMetadata.getDescription();
        this.zzsb = snapshotMetadata.getLastModifiedTimestamp();
        this.zzsc = snapshotMetadata.getPlayedTime();
        this.zzse = snapshotMetadata.getUniqueName();
        this.zzsf = snapshotMetadata.hasChangePending();
        this.zzsg = snapshotMetadata.getProgressValue();
        this.deviceName = snapshotMetadata.getDeviceName();
    }

    static int zza(SnapshotMetadata snapshotMetadata) {
        return Objects.hashCode(snapshotMetadata.getGame(), snapshotMetadata.getOwner(), snapshotMetadata.getSnapshotId(), snapshotMetadata.getCoverImageUri(), Float.valueOf(snapshotMetadata.getCoverImageAspectRatio()), snapshotMetadata.getTitle(), snapshotMetadata.getDescription(), Long.valueOf(snapshotMetadata.getLastModifiedTimestamp()), Long.valueOf(snapshotMetadata.getPlayedTime()), snapshotMetadata.getUniqueName(), Boolean.valueOf(snapshotMetadata.hasChangePending()), Long.valueOf(snapshotMetadata.getProgressValue()), snapshotMetadata.getDeviceName());
    }

    static boolean zza(SnapshotMetadata snapshotMetadata, Object obj) {
        if (!(obj instanceof SnapshotMetadata)) {
            return false;
        }
        if (snapshotMetadata == obj) {
            return true;
        }
        SnapshotMetadata snapshotMetadata2 = (SnapshotMetadata) obj;
        return Objects.equal(snapshotMetadata2.getGame(), snapshotMetadata.getGame()) && Objects.equal(snapshotMetadata2.getOwner(), snapshotMetadata.getOwner()) && Objects.equal(snapshotMetadata2.getSnapshotId(), snapshotMetadata.getSnapshotId()) && Objects.equal(snapshotMetadata2.getCoverImageUri(), snapshotMetadata.getCoverImageUri()) && Objects.equal(Float.valueOf(snapshotMetadata2.getCoverImageAspectRatio()), Float.valueOf(snapshotMetadata.getCoverImageAspectRatio())) && Objects.equal(snapshotMetadata2.getTitle(), snapshotMetadata.getTitle()) && Objects.equal(snapshotMetadata2.getDescription(), snapshotMetadata.getDescription()) && Objects.equal(Long.valueOf(snapshotMetadata2.getLastModifiedTimestamp()), Long.valueOf(snapshotMetadata.getLastModifiedTimestamp())) && Objects.equal(Long.valueOf(snapshotMetadata2.getPlayedTime()), Long.valueOf(snapshotMetadata.getPlayedTime())) && Objects.equal(snapshotMetadata2.getUniqueName(), snapshotMetadata.getUniqueName()) && Objects.equal(Boolean.valueOf(snapshotMetadata2.hasChangePending()), Boolean.valueOf(snapshotMetadata.hasChangePending())) && Objects.equal(Long.valueOf(snapshotMetadata2.getProgressValue()), Long.valueOf(snapshotMetadata.getProgressValue())) && Objects.equal(snapshotMetadata2.getDeviceName(), snapshotMetadata.getDeviceName());
    }

    static String zzb(SnapshotMetadata snapshotMetadata) {
        return Objects.toStringHelper(snapshotMetadata).add("Game", snapshotMetadata.getGame()).add("Owner", snapshotMetadata.getOwner()).add("SnapshotId", snapshotMetadata.getSnapshotId()).add("CoverImageUri", snapshotMetadata.getCoverImageUri()).add("CoverImageUrl", snapshotMetadata.getCoverImageUrl()).add("CoverImageAspectRatio", Float.valueOf(snapshotMetadata.getCoverImageAspectRatio())).add("Description", snapshotMetadata.getDescription()).add("LastModifiedTimestamp", Long.valueOf(snapshotMetadata.getLastModifiedTimestamp())).add("PlayedTime", Long.valueOf(snapshotMetadata.getPlayedTime())).add("UniqueName", snapshotMetadata.getUniqueName()).add("ChangePending", Boolean.valueOf(snapshotMetadata.hasChangePending())).add("ProgressValue", Long.valueOf(snapshotMetadata.getProgressValue())).add("DeviceName", snapshotMetadata.getDeviceName()).toString();
    }

    public final boolean equals(Object obj) {
        return zza(this, obj);
    }

    public final SnapshotMetadata freeze() {
        return this;
    }

    public final float getCoverImageAspectRatio() {
        return this.zzsd;
    }

    @Nullable
    public final Uri getCoverImageUri() {
        return this.zzrw;
    }

    @Nullable
    public final String getCoverImageUrl() {
        return this.zzsa;
    }

    public final String getDescription() {
        return this.description;
    }

    public final void getDescription(CharArrayBuffer charArrayBuffer) {
        DataUtils.copyStringToBuffer(this.description, charArrayBuffer);
    }

    public final String getDeviceName() {
        return this.deviceName;
    }

    public final Game getGame() {
        return this.zzlp;
    }

    public final long getLastModifiedTimestamp() {
        return this.zzsb;
    }

    public final Player getOwner() {
        return this.zzrz;
    }

    public final long getPlayedTime() {
        return this.zzsc;
    }

    public final long getProgressValue() {
        return this.zzsg;
    }

    public final String getSnapshotId() {
        return this.zzhs;
    }

    public final String getTitle() {
        return this.zzcd;
    }

    public final String getUniqueName() {
        return this.zzse;
    }

    public final boolean hasChangePending() {
        return this.zzsf;
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
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeParcelable(parcel, 1, getGame(), i, false);
        SafeParcelWriter.writeParcelable(parcel, 2, getOwner(), i, false);
        SafeParcelWriter.writeString(parcel, 3, getSnapshotId(), false);
        SafeParcelWriter.writeParcelable(parcel, 5, getCoverImageUri(), i, false);
        SafeParcelWriter.writeString(parcel, 6, getCoverImageUrl(), false);
        SafeParcelWriter.writeString(parcel, 7, this.zzcd, false);
        SafeParcelWriter.writeString(parcel, 8, getDescription(), false);
        SafeParcelWriter.writeLong(parcel, 9, getLastModifiedTimestamp());
        SafeParcelWriter.writeLong(parcel, 10, getPlayedTime());
        SafeParcelWriter.writeFloat(parcel, 11, getCoverImageAspectRatio());
        SafeParcelWriter.writeString(parcel, 12, getUniqueName(), false);
        SafeParcelWriter.writeBoolean(parcel, 13, hasChangePending());
        SafeParcelWriter.writeLong(parcel, 14, getProgressValue());
        SafeParcelWriter.writeString(parcel, 15, getDeviceName(), false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
