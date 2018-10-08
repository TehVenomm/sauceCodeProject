package com.google.android.gms.games.request;

import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.data.zzg;

@Deprecated
public final class GameRequestBuffer extends zzg<GameRequest> {
    public GameRequestBuffer(DataHolder dataHolder) {
        super(dataHolder);
    }

    protected final String zzaiw() {
        return "external_request_id";
    }

    protected final /* synthetic */ Object zzk(int i, int i2) {
        return new zzb(this.zzfkz, i, i2);
    }
}
