package com.google.android.gms.games.multiplayer.turnbased;

import android.os.Bundle;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.games.internal.zzbd;
import com.google.android.gms.games.multiplayer.InvitationBuffer;

public final class LoadMatchesResponse {
    private final InvitationBuffer zzqh;
    private final TurnBasedMatchBuffer zzqi;
    private final TurnBasedMatchBuffer zzqj;
    private final TurnBasedMatchBuffer zzqk;

    public LoadMatchesResponse(Bundle bundle) {
        DataHolder zza = zza(bundle, 0);
        if (zza != null) {
            this.zzqh = new InvitationBuffer(zza);
        } else {
            this.zzqh = null;
        }
        DataHolder zza2 = zza(bundle, 1);
        if (zza2 != null) {
            this.zzqi = new TurnBasedMatchBuffer(zza2);
        } else {
            this.zzqi = null;
        }
        DataHolder zza3 = zza(bundle, 2);
        if (zza3 != null) {
            this.zzqj = new TurnBasedMatchBuffer(zza3);
        } else {
            this.zzqj = null;
        }
        DataHolder zza4 = zza(bundle, 3);
        if (zza4 != null) {
            this.zzqk = new TurnBasedMatchBuffer(zza4);
        } else {
            this.zzqk = null;
        }
    }

    private static DataHolder zza(Bundle bundle, int i) {
        String str;
        switch (i) {
            case 0:
                str = "TURN_STATUS_INVITED";
                break;
            case 1:
                str = "TURN_STATUS_MY_TURN";
                break;
            case 2:
                str = "TURN_STATUS_THEIR_TURN";
                break;
            case 3:
                str = "TURN_STATUS_COMPLETE";
                break;
            default:
                zzbd.m430e("MatchTurnStatus", "Unknown match turn status: " + i);
                str = "TURN_STATUS_UNKNOWN";
                break;
        }
        if (!bundle.containsKey(str)) {
            return null;
        }
        return (DataHolder) bundle.getParcelable(str);
    }

    @Deprecated
    public final void close() {
        release();
    }

    public final TurnBasedMatchBuffer getCompletedMatches() {
        return this.zzqk;
    }

    public final InvitationBuffer getInvitations() {
        return this.zzqh;
    }

    public final TurnBasedMatchBuffer getMyTurnMatches() {
        return this.zzqi;
    }

    public final TurnBasedMatchBuffer getTheirTurnMatches() {
        return this.zzqj;
    }

    public final boolean hasData() {
        if (this.zzqh != null && this.zzqh.getCount() > 0) {
            return true;
        }
        if (this.zzqi != null && this.zzqi.getCount() > 0) {
            return true;
        }
        if (this.zzqj == null || this.zzqj.getCount() <= 0) {
            return this.zzqk != null && this.zzqk.getCount() > 0;
        }
        return true;
    }

    public final void release() {
        if (this.zzqh != null) {
            this.zzqh.release();
        }
        if (this.zzqi != null) {
            this.zzqi.release();
        }
        if (this.zzqj != null) {
            this.zzqj.release();
        }
        if (this.zzqk != null) {
            this.zzqk.release();
        }
    }
}
