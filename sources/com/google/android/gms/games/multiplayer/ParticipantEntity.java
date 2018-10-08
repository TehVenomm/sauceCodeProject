package com.google.android.gms.games.multiplayer;

import android.database.CharArrayBuffer;
import android.net.Uri;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.DowngradeableSafeParcel;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.util.zzg;
import com.google.android.gms.games.Player;
import com.google.android.gms.games.PlayerEntity;
import com.google.android.gms.games.internal.GamesDowngradeableSafeParcel;
import java.util.Arrays;

public final class ParticipantEntity extends GamesDowngradeableSafeParcel implements Participant {
    public static final Creator<ParticipantEntity> CREATOR = new zza();
    private final int zzbyx;
    private final String zzeby;
    private final int zzeit;
    private final Uri zzhbd;
    private final Uri zzhbe;
    private final String zzhbo;
    private final String zzhbp;
    private final PlayerEntity zzhdr;
    private final String zzhfq;
    private final String zzhmg;
    private final boolean zzhmh;
    private final ParticipantResult zzhmi;

    static final class zza extends zzc {
        zza() {
        }

        public final /* synthetic */ Object createFromParcel(Parcel parcel) {
            return zzl(parcel);
        }

        public final ParticipantEntity zzl(Parcel parcel) {
            Object obj = null;
            if (GamesDowngradeableSafeParcel.zze(DowngradeableSafeParcel.zzakc()) || DowngradeableSafeParcel.zzga(ParticipantEntity.class.getCanonicalName())) {
                return super.zzl(parcel);
            }
            String readString = parcel.readString();
            String readString2 = parcel.readString();
            String readString3 = parcel.readString();
            Uri parse = readString3 == null ? null : Uri.parse(readString3);
            String readString4 = parcel.readString();
            Uri parse2 = readString4 == null ? null : Uri.parse(readString4);
            int readInt = parcel.readInt();
            String readString5 = parcel.readString();
            boolean z = parcel.readInt() > 0;
            if (parcel.readInt() > 0) {
                obj = 1;
            }
            return new ParticipantEntity(readString, readString2, parse, parse2, readInt, readString5, z, obj != null ? (PlayerEntity) PlayerEntity.CREATOR.createFromParcel(parcel) : null, 7, null, null, null);
        }
    }

    public ParticipantEntity(Participant participant) {
        this.zzhfq = participant.getParticipantId();
        this.zzeby = participant.getDisplayName();
        this.zzhbd = participant.getIconImageUri();
        this.zzhbe = participant.getHiResImageUri();
        this.zzbyx = participant.getStatus();
        this.zzhmg = participant.zzaru();
        this.zzhmh = participant.isConnectedToRoom();
        Player player = participant.getPlayer();
        this.zzhdr = player == null ? null : new PlayerEntity(player);
        this.zzeit = participant.getCapabilities();
        this.zzhmi = participant.getResult();
        this.zzhbo = participant.getIconImageUrl();
        this.zzhbp = participant.getHiResImageUrl();
    }

    ParticipantEntity(String str, String str2, Uri uri, Uri uri2, int i, String str3, boolean z, PlayerEntity playerEntity, int i2, ParticipantResult participantResult, String str4, String str5) {
        this.zzhfq = str;
        this.zzeby = str2;
        this.zzhbd = uri;
        this.zzhbe = uri2;
        this.zzbyx = i;
        this.zzhmg = str3;
        this.zzhmh = z;
        this.zzhdr = playerEntity;
        this.zzeit = i2;
        this.zzhmi = participantResult;
        this.zzhbo = str4;
        this.zzhbp = str5;
    }

    static int zza(Participant participant) {
        return Arrays.hashCode(new Object[]{participant.getPlayer(), Integer.valueOf(participant.getStatus()), participant.zzaru(), Boolean.valueOf(participant.isConnectedToRoom()), participant.getDisplayName(), participant.getIconImageUri(), participant.getHiResImageUri(), Integer.valueOf(participant.getCapabilities()), participant.getResult(), participant.getParticipantId()});
    }

    static boolean zza(Participant participant, Object obj) {
        if (!(obj instanceof Participant)) {
            return false;
        }
        if (participant == obj) {
            return true;
        }
        Participant participant2 = (Participant) obj;
        return zzbf.equal(participant2.getPlayer(), participant.getPlayer()) && zzbf.equal(Integer.valueOf(participant2.getStatus()), Integer.valueOf(participant.getStatus())) && zzbf.equal(participant2.zzaru(), participant.zzaru()) && zzbf.equal(Boolean.valueOf(participant2.isConnectedToRoom()), Boolean.valueOf(participant.isConnectedToRoom())) && zzbf.equal(participant2.getDisplayName(), participant.getDisplayName()) && zzbf.equal(participant2.getIconImageUri(), participant.getIconImageUri()) && zzbf.equal(participant2.getHiResImageUri(), participant.getHiResImageUri()) && zzbf.equal(Integer.valueOf(participant2.getCapabilities()), Integer.valueOf(participant.getCapabilities())) && zzbf.equal(participant2.getResult(), participant.getResult()) && zzbf.equal(participant2.getParticipantId(), participant.getParticipantId());
    }

    static String zzb(Participant participant) {
        return zzbf.zzt(participant).zzg("ParticipantId", participant.getParticipantId()).zzg("Player", participant.getPlayer()).zzg("Status", Integer.valueOf(participant.getStatus())).zzg("ClientAddress", participant.zzaru()).zzg("ConnectedToRoom", Boolean.valueOf(participant.isConnectedToRoom())).zzg("DisplayName", participant.getDisplayName()).zzg("IconImage", participant.getIconImageUri()).zzg("IconImageUrl", participant.getIconImageUrl()).zzg("HiResImage", participant.getHiResImageUri()).zzg("HiResImageUrl", participant.getHiResImageUrl()).zzg("Capabilities", Integer.valueOf(participant.getCapabilities())).zzg("Result", participant.getResult()).toString();
    }

    public final boolean equals(Object obj) {
        return zza(this, obj);
    }

    public final Participant freeze() {
        return this;
    }

    public final int getCapabilities() {
        return this.zzeit;
    }

    public final String getDisplayName() {
        return this.zzhdr == null ? this.zzeby : this.zzhdr.getDisplayName();
    }

    public final void getDisplayName(CharArrayBuffer charArrayBuffer) {
        if (this.zzhdr == null) {
            zzg.zzb(this.zzeby, charArrayBuffer);
        } else {
            this.zzhdr.getDisplayName(charArrayBuffer);
        }
    }

    public final Uri getHiResImageUri() {
        return this.zzhdr == null ? this.zzhbe : this.zzhdr.getHiResImageUri();
    }

    public final String getHiResImageUrl() {
        return this.zzhdr == null ? this.zzhbp : this.zzhdr.getHiResImageUrl();
    }

    public final Uri getIconImageUri() {
        return this.zzhdr == null ? this.zzhbd : this.zzhdr.getIconImageUri();
    }

    public final String getIconImageUrl() {
        return this.zzhdr == null ? this.zzhbo : this.zzhdr.getIconImageUrl();
    }

    public final String getParticipantId() {
        return this.zzhfq;
    }

    public final Player getPlayer() {
        return this.zzhdr;
    }

    public final ParticipantResult getResult() {
        return this.zzhmi;
    }

    public final int getStatus() {
        return this.zzbyx;
    }

    public final int hashCode() {
        return zza(this);
    }

    public final boolean isConnectedToRoom() {
        return this.zzhmh;
    }

    public final boolean isDataValid() {
        return true;
    }

    public final String toString() {
        return zzb(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getParticipantId(), false);
        zzd.zza(parcel, 2, getDisplayName(), false);
        zzd.zza(parcel, 3, getIconImageUri(), i, false);
        zzd.zza(parcel, 4, getHiResImageUri(), i, false);
        zzd.zzc(parcel, 5, getStatus());
        zzd.zza(parcel, 6, this.zzhmg, false);
        zzd.zza(parcel, 7, isConnectedToRoom());
        zzd.zza(parcel, 8, getPlayer(), i, false);
        zzd.zzc(parcel, 9, this.zzeit);
        zzd.zza(parcel, 10, getResult(), i, false);
        zzd.zza(parcel, 11, getIconImageUrl(), false);
        zzd.zza(parcel, 12, getHiResImageUrl(), false);
        zzd.zzai(parcel, zze);
    }

    public final String zzaru() {
        return this.zzhmg;
    }
}
