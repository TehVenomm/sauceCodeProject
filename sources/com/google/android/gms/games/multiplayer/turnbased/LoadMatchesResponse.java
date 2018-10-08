package com.google.android.gms.games.multiplayer.turnbased;

import android.os.Bundle;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.games.internal.zze;
import com.google.android.gms.games.multiplayer.InvitationBuffer;

public final class LoadMatchesResponse {
    private final InvitationBuffer zzhmz;
    private final TurnBasedMatchBuffer zzhna;
    private final TurnBasedMatchBuffer zzhnb;
    private final TurnBasedMatchBuffer zzhnc;

    public LoadMatchesResponse(Bundle bundle) {
        DataHolder zzc = zzc(bundle, 0);
        if (zzc != null) {
            this.zzhmz = new InvitationBuffer(zzc);
        } else {
            this.zzhmz = null;
        }
        zzc = zzc(bundle, 1);
        if (zzc != null) {
            this.zzhna = new TurnBasedMatchBuffer(zzc);
        } else {
            this.zzhna = null;
        }
        zzc = zzc(bundle, 2);
        if (zzc != null) {
            this.zzhnb = new TurnBasedMatchBuffer(zzc);
        } else {
            this.zzhnb = null;
        }
        zzc = zzc(bundle, 3);
        if (zzc != null) {
            this.zzhnc = new TurnBasedMatchBuffer(zzc);
        } else {
            this.zzhnc = null;
        }
    }

    private static DataHolder zzc(Bundle bundle, int i) {
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
                zze.zzz("MatchTurnStatus", "Unknown match turn status: " + i);
                str = "TURN_STATUS_UNKNOWN";
                break;
        }
        return !bundle.containsKey(str) ? null : (DataHolder) bundle.getParcelable(str);
    }

    @Deprecated
    public final void close() {
        release();
    }

    public final TurnBasedMatchBuffer getCompletedMatches() {
        return this.zzhnc;
    }

    public final InvitationBuffer getInvitations() {
        return this.zzhmz;
    }

    public final TurnBasedMatchBuffer getMyTurnMatches() {
        return this.zzhna;
    }

    public final TurnBasedMatchBuffer getTheirTurnMatches() {
        return this.zzhnb;
    }

    public final boolean hasData() {
        return (this.zzhmz == null || this.zzhmz.getCount() <= 0) ? (this.zzhna == null || this.zzhna.getCount() <= 0) ? (this.zzhnb == null || this.zzhnb.getCount() <= 0) ? this.zzhnc != null && this.zzhnc.getCount() > 0 : true : true : true;
    }

    public final void release() {
        if (this.zzhmz != null) {
            this.zzhmz.release();
        }
        if (this.zzhna != null) {
            this.zzhna.release();
        }
        if (this.zzhnb != null) {
            this.zzhnb.release();
        }
        if (this.zzhnc != null) {
            this.zzhnc.release();
        }
    }
}
