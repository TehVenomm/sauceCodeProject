package com.google.android.gms.games.multiplayer.turnbased;

import android.database.CharArrayBuffer;
import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.util.zzg;
import com.google.android.gms.games.Game;
import com.google.android.gms.games.GameEntity;
import com.google.android.gms.games.Player;
import com.google.android.gms.games.internal.zzc;
import com.google.android.gms.games.multiplayer.Multiplayer;
import com.google.android.gms.games.multiplayer.Participant;
import com.google.android.gms.games.multiplayer.ParticipantEntity;
import java.util.ArrayList;
import java.util.Arrays;

public final class TurnBasedMatchEntity extends zzc implements TurnBasedMatch {
    public static final Creator<TurnBasedMatchEntity> CREATOR = new zzc();
    private final long mCreationTimestamp;
    private final int mVersion;
    private final String zzdmz;
    private final long zzhdu;
    private final String zzhfi;
    private final GameEntity zzhiw;
    private final ArrayList<ParticipantEntity> zzhmc;
    private final int zzhmd;
    private final Bundle zzhmu;
    private final String zzhmw;
    private final String zzhne;
    private final String zzhnf;
    private final int zzhng;
    private final byte[] zzhnh;
    private final String zzhni;
    private final byte[] zzhnj;
    private final int zzhnk;
    private final int zzhnl;
    private final boolean zzhnm;
    private final String zzhnn;

    TurnBasedMatchEntity(GameEntity gameEntity, String str, String str2, long j, String str3, long j2, String str4, int i, int i2, int i3, byte[] bArr, ArrayList<ParticipantEntity> arrayList, String str5, byte[] bArr2, int i4, Bundle bundle, int i5, boolean z, String str6, String str7) {
        this.zzhiw = gameEntity;
        this.zzhfi = str;
        this.zzhmw = str2;
        this.mCreationTimestamp = j;
        this.zzhne = str3;
        this.zzhdu = j2;
        this.zzhnf = str4;
        this.zzhng = i;
        this.zzhnl = i5;
        this.zzhmd = i2;
        this.mVersion = i3;
        this.zzhnh = bArr;
        this.zzhmc = arrayList;
        this.zzhni = str5;
        this.zzhnj = bArr2;
        this.zzhnk = i4;
        this.zzhmu = bundle;
        this.zzhnm = z;
        this.zzdmz = str6;
        this.zzhnn = str7;
    }

    public TurnBasedMatchEntity(TurnBasedMatch turnBasedMatch) {
        this.zzhiw = new GameEntity(turnBasedMatch.getGame());
        this.zzhfi = turnBasedMatch.getMatchId();
        this.zzhmw = turnBasedMatch.getCreatorId();
        this.mCreationTimestamp = turnBasedMatch.getCreationTimestamp();
        this.zzhne = turnBasedMatch.getLastUpdaterId();
        this.zzhdu = turnBasedMatch.getLastUpdatedTimestamp();
        this.zzhnf = turnBasedMatch.getPendingParticipantId();
        this.zzhng = turnBasedMatch.getStatus();
        this.zzhnl = turnBasedMatch.getTurnStatus();
        this.zzhmd = turnBasedMatch.getVariant();
        this.mVersion = turnBasedMatch.getVersion();
        this.zzhni = turnBasedMatch.getRematchId();
        this.zzhnk = turnBasedMatch.getMatchNumber();
        this.zzhmu = turnBasedMatch.getAutoMatchCriteria();
        this.zzhnm = turnBasedMatch.isLocallyModified();
        this.zzdmz = turnBasedMatch.getDescription();
        this.zzhnn = turnBasedMatch.getDescriptionParticipantId();
        Object data = turnBasedMatch.getData();
        if (data == null) {
            this.zzhnh = null;
        } else {
            this.zzhnh = new byte[data.length];
            System.arraycopy(data, 0, this.zzhnh, 0, data.length);
        }
        data = turnBasedMatch.getPreviousMatchData();
        if (data == null) {
            this.zzhnj = null;
        } else {
            this.zzhnj = new byte[data.length];
            System.arraycopy(data, 0, this.zzhnj, 0, data.length);
        }
        ArrayList participants = turnBasedMatch.getParticipants();
        int size = participants.size();
        this.zzhmc = new ArrayList(size);
        for (int i = 0; i < size; i++) {
            this.zzhmc.add((ParticipantEntity) ((Participant) participants.get(i)).freeze());
        }
    }

    static int zza(TurnBasedMatch turnBasedMatch) {
        return Arrays.hashCode(new Object[]{turnBasedMatch.getGame(), turnBasedMatch.getMatchId(), turnBasedMatch.getCreatorId(), Long.valueOf(turnBasedMatch.getCreationTimestamp()), turnBasedMatch.getLastUpdaterId(), Long.valueOf(turnBasedMatch.getLastUpdatedTimestamp()), turnBasedMatch.getPendingParticipantId(), Integer.valueOf(turnBasedMatch.getStatus()), Integer.valueOf(turnBasedMatch.getTurnStatus()), turnBasedMatch.getDescription(), Integer.valueOf(turnBasedMatch.getVariant()), Integer.valueOf(turnBasedMatch.getVersion()), turnBasedMatch.getParticipants(), turnBasedMatch.getRematchId(), Integer.valueOf(turnBasedMatch.getMatchNumber()), turnBasedMatch.getAutoMatchCriteria(), Integer.valueOf(turnBasedMatch.getAvailableAutoMatchSlots()), Boolean.valueOf(turnBasedMatch.isLocallyModified())});
    }

    static int zza(TurnBasedMatch turnBasedMatch, String str) {
        ArrayList participants = turnBasedMatch.getParticipants();
        int size = participants.size();
        for (int i = 0; i < size; i++) {
            Participant participant = (Participant) participants.get(i);
            if (participant.getParticipantId().equals(str)) {
                return participant.getStatus();
            }
        }
        String matchId = turnBasedMatch.getMatchId();
        throw new IllegalStateException(new StringBuilder((String.valueOf(str).length() + 29) + String.valueOf(matchId).length()).append("Participant ").append(str).append(" is not in match ").append(matchId).toString());
    }

    static boolean zza(TurnBasedMatch turnBasedMatch, Object obj) {
        if (!(obj instanceof TurnBasedMatch)) {
            return false;
        }
        if (turnBasedMatch == obj) {
            return true;
        }
        TurnBasedMatch turnBasedMatch2 = (TurnBasedMatch) obj;
        return zzbf.equal(turnBasedMatch2.getGame(), turnBasedMatch.getGame()) && zzbf.equal(turnBasedMatch2.getMatchId(), turnBasedMatch.getMatchId()) && zzbf.equal(turnBasedMatch2.getCreatorId(), turnBasedMatch.getCreatorId()) && zzbf.equal(Long.valueOf(turnBasedMatch2.getCreationTimestamp()), Long.valueOf(turnBasedMatch.getCreationTimestamp())) && zzbf.equal(turnBasedMatch2.getLastUpdaterId(), turnBasedMatch.getLastUpdaterId()) && zzbf.equal(Long.valueOf(turnBasedMatch2.getLastUpdatedTimestamp()), Long.valueOf(turnBasedMatch.getLastUpdatedTimestamp())) && zzbf.equal(turnBasedMatch2.getPendingParticipantId(), turnBasedMatch.getPendingParticipantId()) && zzbf.equal(Integer.valueOf(turnBasedMatch2.getStatus()), Integer.valueOf(turnBasedMatch.getStatus())) && zzbf.equal(Integer.valueOf(turnBasedMatch2.getTurnStatus()), Integer.valueOf(turnBasedMatch.getTurnStatus())) && zzbf.equal(turnBasedMatch2.getDescription(), turnBasedMatch.getDescription()) && zzbf.equal(Integer.valueOf(turnBasedMatch2.getVariant()), Integer.valueOf(turnBasedMatch.getVariant())) && zzbf.equal(Integer.valueOf(turnBasedMatch2.getVersion()), Integer.valueOf(turnBasedMatch.getVersion())) && zzbf.equal(turnBasedMatch2.getParticipants(), turnBasedMatch.getParticipants()) && zzbf.equal(turnBasedMatch2.getRematchId(), turnBasedMatch.getRematchId()) && zzbf.equal(Integer.valueOf(turnBasedMatch2.getMatchNumber()), Integer.valueOf(turnBasedMatch.getMatchNumber())) && zzbf.equal(turnBasedMatch2.getAutoMatchCriteria(), turnBasedMatch.getAutoMatchCriteria()) && zzbf.equal(Integer.valueOf(turnBasedMatch2.getAvailableAutoMatchSlots()), Integer.valueOf(turnBasedMatch.getAvailableAutoMatchSlots())) && zzbf.equal(Boolean.valueOf(turnBasedMatch2.isLocallyModified()), Boolean.valueOf(turnBasedMatch.isLocallyModified()));
    }

    static String zzb(TurnBasedMatch turnBasedMatch) {
        return zzbf.zzt(turnBasedMatch).zzg("Game", turnBasedMatch.getGame()).zzg("MatchId", turnBasedMatch.getMatchId()).zzg("CreatorId", turnBasedMatch.getCreatorId()).zzg("CreationTimestamp", Long.valueOf(turnBasedMatch.getCreationTimestamp())).zzg("LastUpdaterId", turnBasedMatch.getLastUpdaterId()).zzg("LastUpdatedTimestamp", Long.valueOf(turnBasedMatch.getLastUpdatedTimestamp())).zzg("PendingParticipantId", turnBasedMatch.getPendingParticipantId()).zzg("MatchStatus", Integer.valueOf(turnBasedMatch.getStatus())).zzg("TurnStatus", Integer.valueOf(turnBasedMatch.getTurnStatus())).zzg("Description", turnBasedMatch.getDescription()).zzg("Variant", Integer.valueOf(turnBasedMatch.getVariant())).zzg("Data", turnBasedMatch.getData()).zzg("Version", Integer.valueOf(turnBasedMatch.getVersion())).zzg("Participants", turnBasedMatch.getParticipants()).zzg("RematchId", turnBasedMatch.getRematchId()).zzg("PreviousData", turnBasedMatch.getPreviousMatchData()).zzg("MatchNumber", Integer.valueOf(turnBasedMatch.getMatchNumber())).zzg("AutoMatchCriteria", turnBasedMatch.getAutoMatchCriteria()).zzg("AvailableAutoMatchSlots", Integer.valueOf(turnBasedMatch.getAvailableAutoMatchSlots())).zzg("LocallyModified", Boolean.valueOf(turnBasedMatch.isLocallyModified())).zzg("DescriptionParticipantId", turnBasedMatch.getDescriptionParticipantId()).toString();
    }

    static String zzb(TurnBasedMatch turnBasedMatch, String str) {
        ArrayList participants = turnBasedMatch.getParticipants();
        int size = participants.size();
        for (int i = 0; i < size; i++) {
            Participant participant = (Participant) participants.get(i);
            Player player = participant.getPlayer();
            if (player != null && player.getPlayerId().equals(str)) {
                return participant.getParticipantId();
            }
        }
        return null;
    }

    static Participant zzc(TurnBasedMatch turnBasedMatch, String str) {
        ArrayList participants = turnBasedMatch.getParticipants();
        int size = participants.size();
        for (int i = 0; i < size; i++) {
            Participant participant = (Participant) participants.get(i);
            if (participant.getParticipantId().equals(str)) {
                return participant;
            }
        }
        String matchId = turnBasedMatch.getMatchId();
        throw new IllegalStateException(new StringBuilder((String.valueOf(str).length() + 29) + String.valueOf(matchId).length()).append("Participant ").append(str).append(" is not in match ").append(matchId).toString());
    }

    static ArrayList<String> zzc(TurnBasedMatch turnBasedMatch) {
        ArrayList participants = turnBasedMatch.getParticipants();
        int size = participants.size();
        ArrayList<String> arrayList = new ArrayList(size);
        for (int i = 0; i < size; i++) {
            arrayList.add(((Participant) participants.get(i)).getParticipantId());
        }
        return arrayList;
    }

    public final boolean canRematch() {
        return this.zzhng == 2 && this.zzhni == null;
    }

    public final boolean equals(Object obj) {
        return zza((TurnBasedMatch) this, obj);
    }

    public final TurnBasedMatch freeze() {
        return this;
    }

    public final Bundle getAutoMatchCriteria() {
        return this.zzhmu;
    }

    public final int getAvailableAutoMatchSlots() {
        return this.zzhmu == null ? 0 : this.zzhmu.getInt(Multiplayer.EXTRA_MAX_AUTOMATCH_PLAYERS);
    }

    public final long getCreationTimestamp() {
        return this.mCreationTimestamp;
    }

    public final String getCreatorId() {
        return this.zzhmw;
    }

    public final byte[] getData() {
        return this.zzhnh;
    }

    public final String getDescription() {
        return this.zzdmz;
    }

    public final void getDescription(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.zzdmz, charArrayBuffer);
    }

    public final Participant getDescriptionParticipant() {
        String descriptionParticipantId = getDescriptionParticipantId();
        return descriptionParticipantId == null ? null : getParticipant(descriptionParticipantId);
    }

    public final String getDescriptionParticipantId() {
        return this.zzhnn;
    }

    public final Game getGame() {
        return this.zzhiw;
    }

    public final long getLastUpdatedTimestamp() {
        return this.zzhdu;
    }

    public final String getLastUpdaterId() {
        return this.zzhne;
    }

    public final String getMatchId() {
        return this.zzhfi;
    }

    public final int getMatchNumber() {
        return this.zzhnk;
    }

    public final Participant getParticipant(String str) {
        return zzc(this, str);
    }

    public final String getParticipantId(String str) {
        return zzb(this, str);
    }

    public final ArrayList<String> getParticipantIds() {
        return zzc(this);
    }

    public final int getParticipantStatus(String str) {
        return zza((TurnBasedMatch) this, str);
    }

    public final ArrayList<Participant> getParticipants() {
        return new ArrayList(this.zzhmc);
    }

    public final String getPendingParticipantId() {
        return this.zzhnf;
    }

    public final byte[] getPreviousMatchData() {
        return this.zzhnj;
    }

    public final String getRematchId() {
        return this.zzhni;
    }

    public final int getStatus() {
        return this.zzhng;
    }

    public final int getTurnStatus() {
        return this.zzhnl;
    }

    public final int getVariant() {
        return this.zzhmd;
    }

    public final int getVersion() {
        return this.mVersion;
    }

    public final int hashCode() {
        return zza(this);
    }

    public final boolean isDataValid() {
        return true;
    }

    public final boolean isLocallyModified() {
        return this.zzhnm;
    }

    public final String toString() {
        return zzb(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getGame(), i, false);
        zzd.zza(parcel, 2, getMatchId(), false);
        zzd.zza(parcel, 3, getCreatorId(), false);
        zzd.zza(parcel, 4, getCreationTimestamp());
        zzd.zza(parcel, 5, getLastUpdaterId(), false);
        zzd.zza(parcel, 6, getLastUpdatedTimestamp());
        zzd.zza(parcel, 7, getPendingParticipantId(), false);
        zzd.zzc(parcel, 8, getStatus());
        zzd.zzc(parcel, 10, getVariant());
        zzd.zzc(parcel, 11, getVersion());
        zzd.zza(parcel, 12, getData(), false);
        zzd.zzc(parcel, 13, getParticipants(), false);
        zzd.zza(parcel, 14, getRematchId(), false);
        zzd.zza(parcel, 15, getPreviousMatchData(), false);
        zzd.zzc(parcel, 16, getMatchNumber());
        zzd.zza(parcel, 17, getAutoMatchCriteria(), false);
        zzd.zzc(parcel, 18, getTurnStatus());
        zzd.zza(parcel, 19, isLocallyModified());
        zzd.zza(parcel, 20, getDescription(), false);
        zzd.zza(parcel, 21, getDescriptionParticipantId(), false);
        zzd.zzai(parcel, zze);
    }
}
