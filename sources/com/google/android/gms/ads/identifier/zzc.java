package com.google.android.gms.ads.identifier;

import com.facebook.appevents.AppEventsConstants;
import com.google.android.gms.ads.identifier.AdvertisingIdClient.Info;
import java.util.HashMap;
import java.util.Map;

final /* synthetic */ class zzc implements Runnable {
    private final Info zzamk;
    private final boolean zzaml;

    zzc(Info info, boolean z) {
        this.zzamk = info;
        this.zzaml = z;
    }

    public final void run() {
        Info info = this.zzamk;
        boolean z = this.zzaml;
        Map hashMap = new HashMap();
        hashMap.put("ad_id_size", Integer.toString(info == null ? -1 : info.getId().length()));
        hashMap.put("hasGmscore", z ? AppEventsConstants.EVENT_PARAM_VALUE_YES : AppEventsConstants.EVENT_PARAM_VALUE_NO);
        hashMap.put("tag", "AdvertisingIdLightClient");
        new zze().zzb(hashMap);
    }
}
