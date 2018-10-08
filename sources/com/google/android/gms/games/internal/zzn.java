package com.google.android.gms.games.internal;

import android.os.Binder;
import android.view.View;

public class zzn {
    protected GamesClientImpl zzhgt;
    protected zzp zzhgu;

    private zzn(GamesClientImpl gamesClientImpl, int i) {
        this.zzhgt = gamesClientImpl;
        zzde(i);
    }

    public void zzaqy() {
        this.zzhgt.zza(this.zzhgu.zzhgv, this.zzhgu.zzaqz());
    }

    protected void zzde(int i) {
        this.zzhgu = new zzp(i, new Binder());
    }

    public void zzt(View view) {
    }
}
