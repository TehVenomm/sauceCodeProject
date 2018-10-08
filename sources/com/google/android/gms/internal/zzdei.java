package com.google.android.gms.internal;

import android.database.ContentObserver;
import android.os.Handler;

final class zzdei extends ContentObserver {
    zzdei(Handler handler) {
        super(null);
    }

    public final void onChange(boolean z) {
        zzdeh.zzkvr.set(true);
    }
}
