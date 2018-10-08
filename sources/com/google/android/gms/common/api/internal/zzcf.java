package com.google.android.gms.common.api.internal;

import android.app.Activity;
import android.support.v4.app.FragmentActivity;
import com.google.android.gms.common.internal.zzbp;

public final class zzcf {
    private final Object zzfoh;

    public zzcf(Activity activity) {
        zzbp.zzb((Object) activity, (Object) "Activity must not be null");
        this.zzfoh = activity;
    }

    public final boolean zzaif() {
        return this.zzfoh instanceof FragmentActivity;
    }

    public final boolean zzaig() {
        return this.zzfoh instanceof Activity;
    }

    public final Activity zzaih() {
        return (Activity) this.zzfoh;
    }

    public final FragmentActivity zzaii() {
        return (FragmentActivity) this.zzfoh;
    }
}
