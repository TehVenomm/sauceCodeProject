package com.google.android.gms.games.multiplayer.realtime;

import android.database.CharArrayBuffer;
import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.DowngradeableSafeParcel;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.util.zzg;
import com.google.android.gms.games.Player;
import com.google.android.gms.games.internal.GamesDowngradeableSafeParcel;
import com.google.android.gms.games.multiplayer.Participant;
import com.google.android.gms.games.multiplayer.ParticipantEntity;
import java.util.ArrayList;
import java.util.Arrays;

public final class RoomEntity extends GamesDowngradeableSafeParcel implements Room {
    public static final Creator<RoomEntity> CREATOR = new zza();
    private final long mCreationTimestamp;
    private final String zzdmz;
    private final String zzhew;
    private final ArrayList<ParticipantEntity> zzhmc;
    private final int zzhmd;
    private final Bundle zzhmu;
    private final String zzhmw;
    private final int zzhmx;
    private final int zzhmy;

    static final class zza extends zze {
        zza() {
        }

        public final /* synthetic */ Object createFromParcel(Parcel parcel) {
            return zzm(parcel);
        }

        public final RoomEntity zzm(Parcel parcel) {
            if (GamesDowngradeableSafeParcel.zze(DowngradeableSafeParcel.zzakc()) || DowngradeableSafeParcel.zzga(RoomEntity.class.getCanonicalName())) {
                return super.zzm(parcel);
            }
            String readString = parcel.readString();
            String readString2 = parcel.readString();
            long readLong = parcel.readLong();
            int readInt = parcel.readInt();
            String readString3 = parcel.readString();
            int readInt2 = parcel.readInt();
            Bundle readBundle = parcel.readBundle();
            int readInt3 = parcel.readInt();
            ArrayList arrayList = new ArrayList(readInt3);
            for (int i = 0; i < readInt3; i++) {
                arrayList.add((ParticipantEntity) ParticipantEntity.CREATOR.createFromParcel(parcel));
            }
            return new RoomEntity(readString, readString2, readLong, readInt, readString3, readInt2, readBundle, arrayList, -1);
        }
    }

    public RoomEntity(Room room) {
        this.zzhew = room.getRoomId();
        this.zzhmw = room.getCreatorId();
        this.mCreationTimestamp = room.getCreationTimestamp();
        this.zzhmx = room.getStatus();
        this.zzdmz = room.getDescription();
        this.zzhmd = room.getVariant();
        this.zzhmu = room.getAutoMatchCriteria();
        ArrayList participants = room.getParticipants();
        int size = participants.size();
        this.zzhmc = new ArrayList(size);
        for (int i = 0; i < size; i++) {
            this.zzhmc.add((ParticipantEntity) ((Participant) participants.get(i)).freeze());
        }
        this.zzhmy = room.getAutoMatchWaitEstimateSeconds();
    }

    RoomEntity(String str, String str2, long j, int i, String str3, int i2, Bundle bundle, ArrayList<ParticipantEntity> arrayList, int i3) {
        this.zzhew = str;
        this.zzhmw = str2;
        this.mCreationTimestamp = j;
        this.zzhmx = i;
        this.zzdmz = str3;
        this.zzhmd = i2;
        this.zzhmu = bundle;
        this.zzhmc = arrayList;
        this.zzhmy = i3;
    }

    static int zza(Room room) {
        return Arrays.hashCode(new Object[]{room.getRoomId(), room.getCreatorId(), Long.valueOf(room.getCreationTimestamp()), Integer.valueOf(room.getStatus()), room.getDescription(), Integer.valueOf(room.getVariant()), room.getAutoMatchCriteria(), room.getParticipants(), Integer.valueOf(room.getAutoMatchWaitEstimateSeconds())});
    }

    static int zza(Room room, String str) {
        ArrayList participants = room.getParticipants();
        int size = participants.size();
        for (int i = 0; i < size; i++) {
            Participant participant = (Participant) participants.get(i);
            if (participant.getParticipantId().equals(str)) {
                return participant.getStatus();
            }
        }
        String roomId = room.getRoomId();
        throw new IllegalStateException(new StringBuilder((String.valueOf(str).length() + 28) + String.valueOf(roomId).length()).append("Participant ").append(str).append(" is not in room ").append(roomId).toString());
    }

    static boolean zza(Room room, Object obj) {
        if (!(obj instanceof Room)) {
            return false;
        }
        if (room == obj) {
            return true;
        }
        Room room2 = (Room) obj;
        return zzbf.equal(room2.getRoomId(), room.getRoomId()) && zzbf.equal(room2.getCreatorId(), room.getCreatorId()) && zzbf.equal(Long.valueOf(room2.getCreationTimestamp()), Long.valueOf(room.getCreationTimestamp())) && zzbf.equal(Integer.valueOf(room2.getStatus()), Integer.valueOf(room.getStatus())) && zzbf.equal(room2.getDescription(), room.getDescription()) && zzbf.equal(Integer.valueOf(room2.getVariant()), Integer.valueOf(room.getVariant())) && zzbf.equal(room2.getAutoMatchCriteria(), room.getAutoMatchCriteria()) && zzbf.equal(room2.getParticipants(), room.getParticipants()) && zzbf.equal(Integer.valueOf(room2.getAutoMatchWaitEstimateSeconds()), Integer.valueOf(room.getAutoMatchWaitEstimateSeconds()));
    }

    static String zzb(Room room) {
        return zzbf.zzt(room).zzg("RoomId", room.getRoomId()).zzg("CreatorId", room.getCreatorId()).zzg("CreationTimestamp", Long.valueOf(room.getCreationTimestamp())).zzg("RoomStatus", Integer.valueOf(room.getStatus())).zzg("Description", room.getDescription()).zzg("Variant", Integer.valueOf(room.getVariant())).zzg("AutoMatchCriteria", room.getAutoMatchCriteria()).zzg("Participants", room.getParticipants()).zzg("AutoMatchWaitEstimateSeconds", Integer.valueOf(room.getAutoMatchWaitEstimateSeconds())).toString();
    }

    static String zzb(Room room, String str) {
        ArrayList participants = room.getParticipants();
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

    static Participant zzc(Room room, String str) {
        ArrayList participants = room.getParticipants();
        int size = participants.size();
        for (int i = 0; i < size; i++) {
            Participant participant = (Participant) participants.get(i);
            if (participant.getParticipantId().equals(str)) {
                return participant;
            }
        }
        String roomId = room.getRoomId();
        throw new IllegalStateException(new StringBuilder((String.valueOf(str).length() + 29) + String.valueOf(roomId).length()).append("Participant ").append(str).append(" is not in match ").append(roomId).toString());
    }

    static ArrayList<String> zzc(Room room) {
        ArrayList participants = room.getParticipants();
        int size = participants.size();
        ArrayList<String> arrayList = new ArrayList(size);
        for (int i = 0; i < size; i++) {
            arrayList.add(((Participant) participants.get(i)).getParticipantId());
        }
        return arrayList;
    }

    public final boolean equals(Object obj) {
        return zza((Room) this, obj);
    }

    public final Room freeze() {
        return this;
    }

    public final Bundle getAutoMatchCriteria() {
        return this.zzhmu;
    }

    public final int getAutoMatchWaitEstimateSeconds() {
        return this.zzhmy;
    }

    public final long getCreationTimestamp() {
        return this.mCreationTimestamp;
    }

    public final String getCreatorId() {
        return this.zzhmw;
    }

    public final String getDescription() {
        return this.zzdmz;
    }

    public final void getDescription(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.zzdmz, charArrayBuffer);
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
        return zza((Room) this, str);
    }

    public final ArrayList<Participant> getParticipants() {
        return new ArrayList(this.zzhmc);
    }

    public final String getRoomId() {
        return this.zzhew;
    }

    public final int getStatus() {
        return this.zzhmx;
    }

    public final int getVariant() {
        return this.zzhmd;
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
        zzd.zza(parcel, 1, getRoomId(), false);
        zzd.zza(parcel, 2, getCreatorId(), false);
        zzd.zza(parcel, 3, getCreationTimestamp());
        zzd.zzc(parcel, 4, getStatus());
        zzd.zza(parcel, 5, getDescription(), false);
        zzd.zzc(parcel, 6, getVariant());
        zzd.zza(parcel, 7, getAutoMatchCriteria(), false);
        zzd.zzc(parcel, 8, getParticipants(), false);
        zzd.zzc(parcel, 9, getAutoMatchWaitEstimateSeconds());
        zzd.zzai(parcel, zze);
    }
}
