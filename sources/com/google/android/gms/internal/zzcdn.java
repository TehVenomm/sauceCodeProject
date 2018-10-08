package com.google.android.gms.internal;

import android.content.Context;
import com.google.android.gms.common.internal.zzbp;

public final class zzcdn {
    final Context mContext;

    public zzcdn(Context context) {
        zzbp.zzu(context);
        Context applicationContext = context.getApplicationContext();
        zzbp.zzu(applicationContext);
        this.mContext = applicationContext;
    }
}
