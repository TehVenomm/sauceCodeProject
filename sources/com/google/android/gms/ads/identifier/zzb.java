package com.google.android.gms.ads.identifier;

import android.content.Context;
import android.content.SharedPreferences;
import com.google.android.gms.ads.identifier.AdvertisingIdClient.Info;
import com.google.android.gms.common.zzo;

public class zzb {
    private static zzb zzamj;
    private final Context zzaie;

    private zzb(Context context) {
        this.zzaie = context;
    }

    private final void zza(Info info, boolean z) {
        if (Math.random() <= ((double) new zzd(this.zzaie).getFloat("gads:ad_id_use_shared_preference:ping_ratio", 0.0f))) {
            new Thread(new zzc(info, z)).start();
        }
    }

    public static zzb zze(Context context) {
        zzb zzb;
        synchronized (zzb.class) {
            try {
                if (zzamj == null) {
                    zzamj = new zzb(context);
                }
                zzb = zzamj;
            } catch (Throwable th) {
                Class cls = zzb.class;
            }
        }
        return zzb;
    }

    public final Info getInfo() {
        Info info = null;
        Context remoteContext = zzo.getRemoteContext(this.zzaie);
        if (remoteContext == null) {
            zza(null, false);
        } else {
            SharedPreferences sharedPreferences = remoteContext.getSharedPreferences("adid_settings", 0);
            if (sharedPreferences == null) {
                zza(null, false);
            } else {
                if (sharedPreferences.contains("adid_key") && sharedPreferences.contains("enable_limit_ad_tracking")) {
                    info = new Info(sharedPreferences.getString("adid_key", ""), sharedPreferences.getBoolean("enable_limit_ad_tracking", false));
                }
                zza(info, true);
            }
        }
        return info;
    }
}
