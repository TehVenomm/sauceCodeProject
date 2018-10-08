package com.google.android.gms.games.multiplayer;

import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.data.zzg;

public final class InvitationBuffer extends zzg<Invitation> {
    public InvitationBuffer(DataHolder dataHolder) {
        super(dataHolder);
    }

    protected final String zzaiw() {
        return "external_invitation_id";
    }

    protected final /* synthetic */ Object zzk(int i, int i2) {
        return new zzb(this.zzfkz, i, i2);
    }
}
