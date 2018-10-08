package com.google.android.gms.games.multiplayer.realtime;

import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.data.zzg;

public final class zzb extends zzg<Room> {
    public zzb(DataHolder dataHolder) {
        super(dataHolder);
    }

    protected final String zzaiw() {
        return "external_match_id";
    }

    protected final /* synthetic */ Object zzk(int i, int i2) {
        return new zzf(this.zzfkz, i, i2);
    }
}
