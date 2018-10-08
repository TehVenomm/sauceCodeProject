package com.google.android.gms.games.multiplayer;

import android.os.Parcel;
import com.facebook.share.internal.ShareConstants;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.data.zzc;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.games.Game;
import com.google.android.gms.games.GameRef;
import java.util.ArrayList;

public final class zzb extends zzc implements Invitation {
    private final Game zzhkx;
    private final ArrayList<Participant> zzhmc;
    private final ParticipantRef zzhmf;

    zzb(DataHolder dataHolder, int i, int i2) {
        super(dataHolder, i);
        this.zzhkx = new GameRef(dataHolder, i);
        this.zzhmc = new ArrayList(i2);
        String string = getString("external_inviter_id");
        Object obj = null;
        for (int i3 = 0; i3 < i2; i3++) {
            ParticipantRef participantRef = new ParticipantRef(this.zzfkz, this.zzfqb + i3);
            if (participantRef.getParticipantId().equals(string)) {
                obj = participantRef;
            }
            this.zzhmc.add(participantRef);
        }
        this.zzhmf = (ParticipantRef) zzbp.zzb(obj, (Object) "Must have a valid inviter!");
    }

    public final int describeContents() {
        return 0;
    }

    public final boolean equals(Object obj) {
        return InvitationEntity.zza(this, obj);
    }

    public final /* synthetic */ Object freeze() {
        return new InvitationEntity(this);
    }

    public final int getAvailableAutoMatchSlots() {
        return !getBoolean("has_automatch_criteria") ? 0 : getInteger("automatch_max_players");
    }

    public final long getCreationTimestamp() {
        return Math.max(getLong("creation_timestamp"), getLong("last_modified_timestamp"));
    }

    public final Game getGame() {
        return this.zzhkx;
    }

    public final String getInvitationId() {
        return getString("external_invitation_id");
    }

    public final int getInvitationType() {
        return getInteger(ShareConstants.MEDIA_TYPE);
    }

    public final Participant getInviter() {
        return this.zzhmf;
    }

    public final ArrayList<Participant> getParticipants() {
        return this.zzhmc;
    }

    public final int getVariant() {
        return getInteger("variant");
    }

    public final int hashCode() {
        return InvitationEntity.zza(this);
    }

    public final String toString() {
        return InvitationEntity.zzb(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        ((InvitationEntity) ((Invitation) freeze())).writeToParcel(parcel, i);
    }
}
