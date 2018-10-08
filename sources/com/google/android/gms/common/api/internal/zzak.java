package com.google.android.gms.common.api.internal;

import android.app.Activity;
import android.support.v4.util.ArraySet;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.internal.zzbp;

public class zzak extends zzo {
    private zzbp zzfgp;
    private final ArraySet<zzh<?>> zzfky = new ArraySet();

    private zzak(zzcg zzcg) {
        super(zzcg);
        this.zzfoi.zza("ConnectionlessLifecycleHelper", (LifecycleCallback) this);
    }

    public static void zza(Activity activity, zzbp zzbp, zzh<?> zzh) {
        LifecycleCallback.zzn(activity);
        zzcg zzn = LifecycleCallback.zzn(activity);
        zzak zzak = (zzak) zzn.zza("ConnectionlessLifecycleHelper", zzak.class);
        if (zzak == null) {
            zzak = new zzak(zzn);
        }
        zzak.zzfgp = zzbp;
        zzbp.zzb((Object) zzh, (Object) "ApiKey cannot be null");
        zzak.zzfky.add(zzh);
        zzbp.zza(zzak);
    }

    private final void zzagv() {
        if (!this.zzfky.isEmpty()) {
            this.zzfgp.zza(this);
        }
    }

    public final void onResume() {
        super.onResume();
        zzagv();
    }

    public final void onStart() {
        super.onStart();
        zzagv();
    }

    public final void onStop() {
        super.onStop();
        this.zzfgp.zzb(this);
    }

    protected final void zza(ConnectionResult connectionResult, int i) {
        this.zzfgp.zza(connectionResult, i);
    }

    protected final void zzafv() {
        this.zzfgp.zzafv();
    }

    final ArraySet<zzh<?>> zzagu() {
        return this.zzfky;
    }
}
