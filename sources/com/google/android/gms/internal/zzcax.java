package com.google.android.gms.internal;

import android.os.Bundle;
import android.text.TextUtils;
import com.google.android.gms.common.internal.zzbp;
import java.util.Iterator;

public final class zzcax {
    final String mAppId;
    final String mName;
    private String mOrigin;
    final long zzfcw;
    final long zzinb;
    final zzcaz zzinc;

    zzcax(zzcco zzcco, String str, String str2, String str3, long j, long j2, Bundle bundle) {
        zzbp.zzgf(str2);
        zzbp.zzgf(str3);
        this.mAppId = str2;
        this.mName = str3;
        if (TextUtils.isEmpty(str)) {
            str = null;
        }
        this.mOrigin = str;
        this.zzfcw = j;
        this.zzinb = j2;
        if (this.zzinb != 0 && this.zzinb > this.zzfcw) {
            zzcco.zzauk().zzaye().zzj("Event created with reverse previous/current timestamps. appId", zzcbo.zzjf(str2));
        }
        this.zzinc = zza(zzcco, bundle);
    }

    private zzcax(zzcco zzcco, String str, String str2, String str3, long j, long j2, zzcaz zzcaz) {
        zzbp.zzgf(str2);
        zzbp.zzgf(str3);
        zzbp.zzu(zzcaz);
        this.mAppId = str2;
        this.mName = str3;
        if (TextUtils.isEmpty(str)) {
            str = null;
        }
        this.mOrigin = str;
        this.zzfcw = j;
        this.zzinb = j2;
        if (this.zzinb != 0 && this.zzinb > this.zzfcw) {
            zzcco.zzauk().zzaye().zzj("Event created with reverse previous/current timestamps. appId", zzcbo.zzjf(str2));
        }
        this.zzinc = zzcaz;
    }

    private static zzcaz zza(zzcco zzcco, Bundle bundle) {
        if (bundle == null || bundle.isEmpty()) {
            return new zzcaz(new Bundle());
        }
        Bundle bundle2 = new Bundle(bundle);
        Iterator it = bundle2.keySet().iterator();
        while (it.hasNext()) {
            String str = (String) it.next();
            if (str == null) {
                zzcco.zzauk().zzayc().log("Param name can't be null");
                it.remove();
            } else {
                Object zzk = zzcco.zzaug().zzk(str, bundle2.get(str));
                if (zzk == null) {
                    zzcco.zzauk().zzaye().zzj("Param value can't be null", zzcco.zzauf().zzjd(str));
                    it.remove();
                } else {
                    zzcco.zzaug().zza(bundle2, str, zzk);
                }
            }
        }
        return new zzcaz(bundle2);
    }

    public final String toString() {
        String str = this.mAppId;
        String str2 = this.mName;
        String valueOf = String.valueOf(this.zzinc);
        return new StringBuilder(((String.valueOf(str).length() + 33) + String.valueOf(str2).length()) + String.valueOf(valueOf).length()).append("Event{appId='").append(str).append("', name='").append(str2).append("', params=").append(valueOf).append("}").toString();
    }

    final zzcax zza(zzcco zzcco, long j) {
        return new zzcax(zzcco, this.mOrigin, this.mAppId, this.mName, this.zzfcw, j, this.zzinc);
    }
}
