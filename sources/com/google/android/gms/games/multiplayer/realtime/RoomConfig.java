package com.google.android.gms.games.multiplayer.realtime;

import android.os.Bundle;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.games.multiplayer.Multiplayer;
import java.util.ArrayList;
import java.util.Arrays;

public abstract class RoomConfig {

    public static final class Builder {
        int zzhmd;
        final RoomUpdateListener zzhmp;
        RoomStatusUpdateListener zzhmq;
        RealTimeMessageReceivedListener zzhmr;
        String zzhms;
        ArrayList<String> zzhmt;
        Bundle zzhmu;

        private Builder(RoomUpdateListener roomUpdateListener) {
            this.zzhms = null;
            this.zzhmd = -1;
            this.zzhmt = new ArrayList();
            this.zzhmp = (RoomUpdateListener) zzbp.zzb((Object) roomUpdateListener, (Object) "Must provide a RoomUpdateListener");
        }

        public final Builder addPlayersToInvite(ArrayList<String> arrayList) {
            zzbp.zzu(arrayList);
            this.zzhmt.addAll(arrayList);
            return this;
        }

        public final Builder addPlayersToInvite(String... strArr) {
            zzbp.zzu(strArr);
            this.zzhmt.addAll(Arrays.asList(strArr));
            return this;
        }

        public final RoomConfig build() {
            return new zzd(this);
        }

        public final Builder setAutoMatchCriteria(Bundle bundle) {
            this.zzhmu = bundle;
            return this;
        }

        public final Builder setInvitationIdToAccept(String str) {
            zzbp.zzu(str);
            this.zzhms = str;
            return this;
        }

        public final Builder setMessageReceivedListener(RealTimeMessageReceivedListener realTimeMessageReceivedListener) {
            this.zzhmr = realTimeMessageReceivedListener;
            return this;
        }

        public final Builder setRoomStatusUpdateListener(RoomStatusUpdateListener roomStatusUpdateListener) {
            this.zzhmq = roomStatusUpdateListener;
            return this;
        }

        public final Builder setVariant(int i) {
            boolean z = i == -1 || i > 0;
            zzbp.zzb(z, (Object) "Variant must be a positive integer or Room.ROOM_VARIANT_ANY");
            this.zzhmd = i;
            return this;
        }
    }

    protected RoomConfig() {
    }

    public static Builder builder(RoomUpdateListener roomUpdateListener) {
        return new Builder(roomUpdateListener);
    }

    public static Bundle createAutoMatchCriteria(int i, int i2, long j) {
        Bundle bundle = new Bundle();
        bundle.putInt(Multiplayer.EXTRA_MIN_AUTOMATCH_PLAYERS, i);
        bundle.putInt(Multiplayer.EXTRA_MAX_AUTOMATCH_PLAYERS, i2);
        bundle.putLong(Multiplayer.EXTRA_EXCLUSIVE_BIT_MASK, j);
        return bundle;
    }

    public abstract Bundle getAutoMatchCriteria();

    public abstract String getInvitationId();

    public abstract String[] getInvitedPlayerIds();

    public abstract RealTimeMessageReceivedListener getMessageReceivedListener();

    public abstract RoomStatusUpdateListener getRoomStatusUpdateListener();

    public abstract RoomUpdateListener getRoomUpdateListener();

    public abstract int getVariant();
}
