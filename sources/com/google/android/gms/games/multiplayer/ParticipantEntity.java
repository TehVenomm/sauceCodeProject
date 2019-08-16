package com.google.android.gms.games.multiplayer;

import android.database.CharArrayBuffer;
import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.NonNull;
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
import com.google.android.gms.common.util.RetainForClient;
import com.google.android.gms.games.Player;
import com.google.android.gms.games.PlayerEntity;
import com.google.android.gms.games.internal.GamesDowngradeableSafeParcel;
import java.util.ArrayList;
import java.util.List;

@RetainForClient
@UsedByReflection("GamesClientImpl.java")
@Class(creator = "ParticipantEntityCreator")
@Reserved({1000})
public final class ParticipantEntity extends GamesDowngradeableSafeParcel implements Participant {
    public static final Creator<ParticipantEntity> CREATOR = new zza();
    @Field(getter = "getStatus", mo13990id = 5)
    private final int status;
    @Nullable
    @Field(getter = "getIconImageUrl", mo13990id = 11)
    private final String zzac;
    @Nullable
    @Field(getter = "getHiResImageUrl", mo13990id = 12)
    private final String zzad;
    @Nullable
    @Field(getter = "getPlayer", mo13990id = 8)
    private final PlayerEntity zzfj;
    @Field(getter = "getDisplayName", mo13990id = 2)
    private final String zzn;
    @Field(getter = "getParticipantId", mo13990id = 1)
    private final String zzpg;
    @Field(getter = "getClientAddress", mo13990id = 6)
    private final String zzph;
    @Field(getter = "isConnectedToRoom", mo13990id = 7)
    private final boolean zzpi;
    @Field(getter = "getCapabilities", mo13990id = 9)
    private final int zzpj;
    @Nullable
    @Field(getter = "getResult", mo13990id = 10)
    private final ParticipantResult zzpk;
    @Nullable
    @Field(getter = "getIconImageUri", mo13990id = 3)
    private final Uri zzr;
    @Nullable
    @Field(getter = "getHiResImageUri", mo13990id = 4)
    private final Uri zzs;

    static final class zza extends zzc {
        zza() {
        }

        public final /* synthetic */ Object createFromParcel(Parcel parcel) {
            return createFromParcel(parcel);
        }

        public final ParticipantEntity zzf(Parcel parcel) {
            boolean z = false;
            if (ParticipantEntity.zzb(ParticipantEntity.getUnparcelClientVersion()) || ParticipantEntity.canUnparcelSafely(ParticipantEntity.class.getCanonicalName())) {
                return super.createFromParcel(parcel);
            }
            String readString = parcel.readString();
            String readString2 = parcel.readString();
            String readString3 = parcel.readString();
            String readString4 = parcel.readString();
            int readInt = parcel.readInt();
            String readString5 = parcel.readString();
            boolean z2 = parcel.readInt() > 0;
            if (parcel.readInt() > 0) {
                z = true;
            }
            return new ParticipantEntity(readString, readString2, readString3 == null ? null : Uri.parse(readString3), readString4 == null ? null : Uri.parse(readString4), readInt, readString5, z2, z ? (PlayerEntity) PlayerEntity.CREATOR.createFromParcel(parcel) : null, 7, null, null, null);
        }
    }

    public ParticipantEntity(Participant participant) {
        Player player = participant.getPlayer();
        this(participant, player == null ? null : new PlayerEntity(player));
    }

    private ParticipantEntity(Participant participant, @Nullable PlayerEntity playerEntity) {
        this.zzpg = participant.getParticipantId();
        this.zzn = participant.getDisplayName();
        this.zzr = participant.getIconImageUri();
        this.zzs = participant.getHiResImageUri();
        this.status = participant.getStatus();
        this.zzph = participant.zzdn();
        this.zzpi = participant.isConnectedToRoom();
        this.zzfj = playerEntity;
        this.zzpj = participant.getCapabilities();
        this.zzpk = participant.getResult();
        this.zzac = participant.getIconImageUrl();
        this.zzad = participant.getHiResImageUrl();
    }

    @Constructor
    ParticipantEntity(@Param(mo13993id = 1) String str, @Param(mo13993id = 2) String str2, @Nullable @Param(mo13993id = 3) Uri uri, @Nullable @Param(mo13993id = 4) Uri uri2, @Param(mo13993id = 5) int i, @Param(mo13993id = 6) String str3, @Param(mo13993id = 7) boolean z, @Nullable @Param(mo13993id = 8) PlayerEntity playerEntity, @Param(mo13993id = 9) int i2, @Nullable @Param(mo13993id = 10) ParticipantResult participantResult, @Nullable @Param(mo13993id = 11) String str4, @Nullable @Param(mo13993id = 12) String str5) {
        this.zzpg = str;
        this.zzn = str2;
        this.zzr = uri;
        this.zzs = uri2;
        this.status = i;
        this.zzph = str3;
        this.zzpi = z;
        this.zzfj = playerEntity;
        this.zzpj = i2;
        this.zzpk = participantResult;
        this.zzac = str4;
        this.zzad = str5;
    }

    static int zza(Participant participant) {
        return Objects.hashCode(participant.getPlayer(), Integer.valueOf(participant.getStatus()), participant.zzdn(), Boolean.valueOf(participant.isConnectedToRoom()), participant.getDisplayName(), participant.getIconImageUri(), participant.getHiResImageUri(), Integer.valueOf(participant.getCapabilities()), participant.getResult(), participant.getParticipantId());
    }

    public static ArrayList<ParticipantEntity> zza(@NonNull List<Participant> list) {
        ArrayList<ParticipantEntity> arrayList = new ArrayList<>(list.size());
        for (Participant participant : list) {
            arrayList.add(participant instanceof ParticipantEntity ? (ParticipantEntity) participant : new ParticipantEntity(participant));
        }
        return arrayList;
    }

    static boolean zza(Participant participant, Object obj) {
        if (!(obj instanceof Participant)) {
            return false;
        }
        if (participant == obj) {
            return true;
        }
        Participant participant2 = (Participant) obj;
        return Objects.equal(participant2.getPlayer(), participant.getPlayer()) && Objects.equal(Integer.valueOf(participant2.getStatus()), Integer.valueOf(participant.getStatus())) && Objects.equal(participant2.zzdn(), participant.zzdn()) && Objects.equal(Boolean.valueOf(participant2.isConnectedToRoom()), Boolean.valueOf(participant.isConnectedToRoom())) && Objects.equal(participant2.getDisplayName(), participant.getDisplayName()) && Objects.equal(participant2.getIconImageUri(), participant.getIconImageUri()) && Objects.equal(participant2.getHiResImageUri(), participant.getHiResImageUri()) && Objects.equal(Integer.valueOf(participant2.getCapabilities()), Integer.valueOf(participant.getCapabilities())) && Objects.equal(participant2.getResult(), participant.getResult()) && Objects.equal(participant2.getParticipantId(), participant.getParticipantId());
    }

    static String zzb(Participant participant) {
        return Objects.toStringHelper(participant).add("ParticipantId", participant.getParticipantId()).add("Player", participant.getPlayer()).add("Status", Integer.valueOf(participant.getStatus())).add("ClientAddress", participant.zzdn()).add("ConnectedToRoom", Boolean.valueOf(participant.isConnectedToRoom())).add("DisplayName", participant.getDisplayName()).add("IconImage", participant.getIconImageUri()).add("IconImageUrl", participant.getIconImageUrl()).add("HiResImage", participant.getHiResImageUri()).add("HiResImageUrl", participant.getHiResImageUrl()).add("Capabilities", Integer.valueOf(participant.getCapabilities())).add("Result", participant.getResult()).toString();
    }

    public final boolean equals(Object obj) {
        return zza(this, obj);
    }

    public final Participant freeze() {
        return this;
    }

    public final int getCapabilities() {
        return this.zzpj;
    }

    public final String getDisplayName() {
        return this.zzfj == null ? this.zzn : this.zzfj.getDisplayName();
    }

    public final void getDisplayName(CharArrayBuffer charArrayBuffer) {
        if (this.zzfj != null) {
            this.zzfj.getDisplayName(charArrayBuffer);
        } else if (this.zzn == null) {
            charArrayBuffer.sizeCopied = 0;
        } else {
            DataUtils.copyStringToBuffer(this.zzn, charArrayBuffer);
        }
    }

    @Nullable
    public final Uri getHiResImageUri() {
        return this.zzfj == null ? this.zzs : this.zzfj.getHiResImageUri();
    }

    public final String getHiResImageUrl() {
        return this.zzfj == null ? this.zzad : this.zzfj.getHiResImageUrl();
    }

    @Nullable
    public final Uri getIconImageUri() {
        return this.zzfj == null ? this.zzr : this.zzfj.getIconImageUri();
    }

    @Nullable
    public final String getIconImageUrl() {
        return this.zzfj == null ? this.zzac : this.zzfj.getIconImageUrl();
    }

    public final String getParticipantId() {
        return this.zzpg;
    }

    public final Player getPlayer() {
        return this.zzfj;
    }

    public final ParticipantResult getResult() {
        return this.zzpk;
    }

    public final int getStatus() {
        return this.status;
    }

    public final int hashCode() {
        return zza((Participant) this);
    }

    public final boolean isConnectedToRoom() {
        return this.zzpi;
    }

    public final boolean isDataValid() {
        return true;
    }

    public final void setShouldDowngrade(boolean z) {
        super.setShouldDowngrade(z);
        if (this.zzfj != null) {
            this.zzfj.setShouldDowngrade(z);
        }
    }

    public final String toString() {
        return zzb(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        String str = null;
        if (!shouldDowngrade()) {
            int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
            SafeParcelWriter.writeString(parcel, 1, getParticipantId(), false);
            SafeParcelWriter.writeString(parcel, 2, getDisplayName(), false);
            SafeParcelWriter.writeParcelable(parcel, 3, getIconImageUri(), i, false);
            SafeParcelWriter.writeParcelable(parcel, 4, getHiResImageUri(), i, false);
            SafeParcelWriter.writeInt(parcel, 5, getStatus());
            SafeParcelWriter.writeString(parcel, 6, this.zzph, false);
            SafeParcelWriter.writeBoolean(parcel, 7, isConnectedToRoom());
            SafeParcelWriter.writeParcelable(parcel, 8, getPlayer(), i, false);
            SafeParcelWriter.writeInt(parcel, 9, this.zzpj);
            SafeParcelWriter.writeParcelable(parcel, 10, getResult(), i, false);
            SafeParcelWriter.writeString(parcel, 11, getIconImageUrl(), false);
            SafeParcelWriter.writeString(parcel, 12, getHiResImageUrl(), false);
            SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
            return;
        }
        parcel.writeString(this.zzpg);
        parcel.writeString(this.zzn);
        parcel.writeString(this.zzr == null ? null : this.zzr.toString());
        if (this.zzs != null) {
            str = this.zzs.toString();
        }
        parcel.writeString(str);
        parcel.writeInt(this.status);
        parcel.writeString(this.zzph);
        parcel.writeInt(this.zzpi ? 1 : 0);
        if (this.zzfj == null) {
            parcel.writeInt(0);
            return;
        }
        parcel.writeInt(1);
        this.zzfj.writeToParcel(parcel, i);
    }

    public final String zzdn() {
        return this.zzph;
    }
}
