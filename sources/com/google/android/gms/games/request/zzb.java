package com.google.android.gms.games.request;

import android.os.Parcel;
import com.facebook.share.internal.ShareConstants;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.data.zzc;
import com.google.android.gms.games.Game;
import com.google.android.gms.games.GameRef;
import com.google.android.gms.games.Player;
import com.google.android.gms.games.PlayerRef;
import java.util.ArrayList;
import java.util.List;

@Deprecated
public final class zzb extends zzc implements GameRequest {
    private final int zzhky;

    public zzb(DataHolder dataHolder, int i, int i2) {
        super(dataHolder, i);
        this.zzhky = i2;
    }

    public final int describeContents() {
        return 0;
    }

    public final boolean equals(Object obj) {
        return GameRequestEntity.zza(this, obj);
    }

    public final /* synthetic */ Object freeze() {
        return new GameRequestEntity(this);
    }

    public final long getCreationTimestamp() {
        return getLong("creation_timestamp");
    }

    public final byte[] getData() {
        return getByteArray(ShareConstants.WEB_DIALOG_PARAM_DATA);
    }

    public final long getExpirationTimestamp() {
        return getLong("expiration_timestamp");
    }

    public final Game getGame() {
        return new GameRef(this.zzfkz, this.zzfqb);
    }

    public final int getRecipientStatus(String str) {
        for (int i = this.zzfqb; i < this.zzfqb + this.zzhky; i++) {
            int zzbw = this.zzfkz.zzbw(i);
            if (this.zzfkz.zzd("recipient_external_player_id", i, zzbw).equals(str)) {
                return this.zzfkz.zzc("recipient_status", i, zzbw);
            }
        }
        return -1;
    }

    public final List<Player> getRecipients() {
        List arrayList = new ArrayList(this.zzhky);
        for (int i = 0; i < this.zzhky; i++) {
            arrayList.add(new PlayerRef(this.zzfkz, this.zzfqb + i, "recipient_"));
        }
        return arrayList;
    }

    public final String getRequestId() {
        return getString("external_request_id");
    }

    public final Player getSender() {
        return new PlayerRef(this.zzfkz, this.zzfqb, "sender_");
    }

    public final int getStatus() {
        return getInteger("status");
    }

    public final int getType() {
        return getInteger(ShareConstants.MEDIA_TYPE);
    }

    public final int hashCode() {
        return GameRequestEntity.zza(this);
    }

    public final boolean isConsumed(String str) {
        return getRecipientStatus(str) == 1;
    }

    public final String toString() {
        return GameRequestEntity.zzc(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        ((GameRequestEntity) ((GameRequest) freeze())).writeToParcel(parcel, i);
    }
}
