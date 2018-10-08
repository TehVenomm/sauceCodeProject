package com.google.android.gms.games.multiplayer.realtime;

import android.os.Bundle;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.games.multiplayer.realtime.RoomConfig.Builder;

public final class zzd extends RoomConfig {
    private final String zzdww;
    private final int zzhmd;
    private final RoomUpdateListener zzhmp;
    private final RoomStatusUpdateListener zzhmq;
    private final RealTimeMessageReceivedListener zzhmr;
    private final Bundle zzhmu;
    private final String[] zzhmv;

    zzd(Builder builder) {
        this.zzhmp = builder.zzhmp;
        this.zzhmq = builder.zzhmq;
        this.zzhmr = builder.zzhmr;
        this.zzdww = builder.zzhms;
        this.zzhmd = builder.zzhmd;
        this.zzhmu = builder.zzhmu;
        this.zzhmv = (String[]) builder.zzhmt.toArray(new String[builder.zzhmt.size()]);
        zzbp.zzb(this.zzhmr, (Object) "Must specify a message listener");
    }

    public final Bundle getAutoMatchCriteria() {
        return this.zzhmu;
    }

    public final String getInvitationId() {
        return this.zzdww;
    }

    public final String[] getInvitedPlayerIds() {
        return this.zzhmv;
    }

    public final RealTimeMessageReceivedListener getMessageReceivedListener() {
        return this.zzhmr;
    }

    public final RoomStatusUpdateListener getRoomStatusUpdateListener() {
        return this.zzhmq;
    }

    public final RoomUpdateListener getRoomUpdateListener() {
        return this.zzhmp;
    }

    public final int getVariant() {
        return this.zzhmd;
    }
}
