package com.google.android.gms.games.multiplayer;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.DowngradeableSafeParcel;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.games.Game;
import com.google.android.gms.games.GameEntity;
import com.google.android.gms.games.internal.GamesDowngradeableSafeParcel;
import java.util.ArrayList;
import java.util.Arrays;

public final class InvitationEntity extends GamesDowngradeableSafeParcel implements Invitation {
    public static final Creator<InvitationEntity> CREATOR = new zza();
    private final long mCreationTimestamp;
    private final String zzdww;
    private final GameEntity zzhiw;
    private final int zzhma;
    private final ParticipantEntity zzhmb;
    private final ArrayList<ParticipantEntity> zzhmc;
    private final int zzhmd;
    private final int zzhme;

    static final class zza extends zza {
        zza() {
        }

        public final /* synthetic */ Object createFromParcel(Parcel parcel) {
            return zzk(parcel);
        }

        public final InvitationEntity zzk(Parcel parcel) {
            if (GamesDowngradeableSafeParcel.zze(DowngradeableSafeParcel.zzakc()) || DowngradeableSafeParcel.zzga(InvitationEntity.class.getCanonicalName())) {
                return super.zzk(parcel);
            }
            GameEntity gameEntity = (GameEntity) GameEntity.CREATOR.createFromParcel(parcel);
            String readString = parcel.readString();
            long readLong = parcel.readLong();
            int readInt = parcel.readInt();
            ParticipantEntity participantEntity = (ParticipantEntity) ParticipantEntity.CREATOR.createFromParcel(parcel);
            int readInt2 = parcel.readInt();
            ArrayList arrayList = new ArrayList(readInt2);
            for (int i = 0; i < readInt2; i++) {
                arrayList.add((ParticipantEntity) ParticipantEntity.CREATOR.createFromParcel(parcel));
            }
            return new InvitationEntity(gameEntity, readString, readLong, readInt, participantEntity, arrayList, -1, 0);
        }
    }

    InvitationEntity(GameEntity gameEntity, String str, long j, int i, ParticipantEntity participantEntity, ArrayList<ParticipantEntity> arrayList, int i2, int i3) {
        this.zzhiw = gameEntity;
        this.zzdww = str;
        this.mCreationTimestamp = j;
        this.zzhma = i;
        this.zzhmb = participantEntity;
        this.zzhmc = arrayList;
        this.zzhmd = i2;
        this.zzhme = i3;
    }

    InvitationEntity(Invitation invitation) {
        this.zzhiw = new GameEntity(invitation.getGame());
        this.zzdww = invitation.getInvitationId();
        this.mCreationTimestamp = invitation.getCreationTimestamp();
        this.zzhma = invitation.getInvitationType();
        this.zzhmd = invitation.getVariant();
        this.zzhme = invitation.getAvailableAutoMatchSlots();
        String participantId = invitation.getInviter().getParticipantId();
        Object obj = null;
        ArrayList participants = invitation.getParticipants();
        int size = participants.size();
        this.zzhmc = new ArrayList(size);
        for (int i = 0; i < size; i++) {
            Participant participant = (Participant) participants.get(i);
            if (participant.getParticipantId().equals(participantId)) {
                obj = participant;
            }
            this.zzhmc.add((ParticipantEntity) participant.freeze());
        }
        zzbp.zzb(obj, (Object) "Must have a valid inviter!");
        this.zzhmb = (ParticipantEntity) obj.freeze();
    }

    static int zza(Invitation invitation) {
        return Arrays.hashCode(new Object[]{invitation.getGame(), invitation.getInvitationId(), Long.valueOf(invitation.getCreationTimestamp()), Integer.valueOf(invitation.getInvitationType()), invitation.getInviter(), invitation.getParticipants(), Integer.valueOf(invitation.getVariant()), Integer.valueOf(invitation.getAvailableAutoMatchSlots())});
    }

    static boolean zza(Invitation invitation, Object obj) {
        if (!(obj instanceof Invitation)) {
            return false;
        }
        if (invitation == obj) {
            return true;
        }
        Invitation invitation2 = (Invitation) obj;
        return zzbf.equal(invitation2.getGame(), invitation.getGame()) && zzbf.equal(invitation2.getInvitationId(), invitation.getInvitationId()) && zzbf.equal(Long.valueOf(invitation2.getCreationTimestamp()), Long.valueOf(invitation.getCreationTimestamp())) && zzbf.equal(Integer.valueOf(invitation2.getInvitationType()), Integer.valueOf(invitation.getInvitationType())) && zzbf.equal(invitation2.getInviter(), invitation.getInviter()) && zzbf.equal(invitation2.getParticipants(), invitation.getParticipants()) && zzbf.equal(Integer.valueOf(invitation2.getVariant()), Integer.valueOf(invitation.getVariant())) && zzbf.equal(Integer.valueOf(invitation2.getAvailableAutoMatchSlots()), Integer.valueOf(invitation.getAvailableAutoMatchSlots()));
    }

    static String zzb(Invitation invitation) {
        return zzbf.zzt(invitation).zzg("Game", invitation.getGame()).zzg("InvitationId", invitation.getInvitationId()).zzg("CreationTimestamp", Long.valueOf(invitation.getCreationTimestamp())).zzg("InvitationType", Integer.valueOf(invitation.getInvitationType())).zzg("Inviter", invitation.getInviter()).zzg("Participants", invitation.getParticipants()).zzg("Variant", Integer.valueOf(invitation.getVariant())).zzg("AvailableAutoMatchSlots", Integer.valueOf(invitation.getAvailableAutoMatchSlots())).toString();
    }

    public final boolean equals(Object obj) {
        return zza(this, obj);
    }

    public final Invitation freeze() {
        return this;
    }

    public final int getAvailableAutoMatchSlots() {
        return this.zzhme;
    }

    public final long getCreationTimestamp() {
        return this.mCreationTimestamp;
    }

    public final Game getGame() {
        return this.zzhiw;
    }

    public final String getInvitationId() {
        return this.zzdww;
    }

    public final int getInvitationType() {
        return this.zzhma;
    }

    public final Participant getInviter() {
        return this.zzhmb;
    }

    public final ArrayList<Participant> getParticipants() {
        return new ArrayList(this.zzhmc);
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
        zzd.zza(parcel, 1, getGame(), i, false);
        zzd.zza(parcel, 2, getInvitationId(), false);
        zzd.zza(parcel, 3, getCreationTimestamp());
        zzd.zzc(parcel, 4, getInvitationType());
        zzd.zza(parcel, 5, getInviter(), i, false);
        zzd.zzc(parcel, 6, getParticipants(), false);
        zzd.zzc(parcel, 7, getVariant());
        zzd.zzc(parcel, 8, getAvailableAutoMatchSlots());
        zzd.zzai(parcel, zze);
    }
}
