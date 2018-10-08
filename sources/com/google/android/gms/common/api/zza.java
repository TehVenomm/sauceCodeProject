package com.google.android.gms.common.api;

import android.support.v4.util.ArrayMap;
import android.text.TextUtils;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api.ApiOptions;
import com.google.android.gms.common.api.internal.zzh;
import com.google.android.gms.common.internal.zzbp;
import java.util.ArrayList;

public final class zza extends Exception {
    private final ArrayMap<zzh<?>, ConnectionResult> zzfgd;

    public zza(ArrayMap<zzh<?>, ConnectionResult> arrayMap) {
        this.zzfgd = arrayMap;
    }

    public final String getMessage() {
        Iterable arrayList = new ArrayList();
        Object obj = 1;
        for (zzh zzh : this.zzfgd.keySet()) {
            ConnectionResult connectionResult = (ConnectionResult) this.zzfgd.get(zzh);
            if (connectionResult.isSuccess()) {
                obj = null;
            }
            String zzafu = zzh.zzafu();
            String valueOf = String.valueOf(connectionResult);
            arrayList.add(new StringBuilder((String.valueOf(zzafu).length() + 2) + String.valueOf(valueOf).length()).append(zzafu).append(": ").append(valueOf).toString());
        }
        StringBuilder stringBuilder = new StringBuilder();
        if (obj != null) {
            stringBuilder.append("None of the queried APIs are available. ");
        } else {
            stringBuilder.append("Some of the queried APIs are unavailable. ");
        }
        stringBuilder.append(TextUtils.join("; ", arrayList));
        return stringBuilder.toString();
    }

    public final ConnectionResult zza(GoogleApi<? extends ApiOptions> googleApi) {
        zzh zzafj = googleApi.zzafj();
        zzbp.zzb(this.zzfgd.get(zzafj) != null, (Object) "The given API was not part of the availability request.");
        return (ConnectionResult) this.zzfgd.get(zzafj);
    }

    public final ArrayMap<zzh<?>, ConnectionResult> zzafg() {
        return this.zzfgd;
    }
}
