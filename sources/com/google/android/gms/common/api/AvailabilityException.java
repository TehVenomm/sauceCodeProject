package com.google.android.gms.common.api;

import android.support.p000v4.util.ArrayMap;
import android.text.TextUtils;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api.ApiOptions;
import com.google.android.gms.common.api.internal.zai;
import com.google.android.gms.common.internal.Preconditions;
import java.util.ArrayList;

public class AvailabilityException extends Exception {
    private final ArrayMap<zai<?>, ConnectionResult> zaay;

    public AvailabilityException(ArrayMap<zai<?>, ConnectionResult> arrayMap) {
        this.zaay = arrayMap;
    }

    public ConnectionResult getConnectionResult(GoogleApi<? extends ApiOptions> googleApi) {
        zai zak = googleApi.zak();
        Preconditions.checkArgument(this.zaay.get(zak) != null, "The given API was not part of the availability request.");
        return (ConnectionResult) this.zaay.get(zak);
    }

    public String getMessage() {
        ArrayList arrayList = new ArrayList();
        boolean z = true;
        for (zai zai : this.zaay.keySet()) {
            ConnectionResult connectionResult = (ConnectionResult) this.zaay.get(zai);
            if (connectionResult.isSuccess()) {
                z = false;
            }
            String zan = zai.zan();
            String valueOf = String.valueOf(connectionResult);
            arrayList.add(new StringBuilder(String.valueOf(zan).length() + 2 + String.valueOf(valueOf).length()).append(zan).append(": ").append(valueOf).toString());
        }
        StringBuilder sb = new StringBuilder();
        if (z) {
            sb.append("None of the queried APIs are available. ");
        } else {
            sb.append("Some of the queried APIs are unavailable. ");
        }
        sb.append(TextUtils.join("; ", arrayList));
        return sb.toString();
    }

    public final ArrayMap<zai<?>, ConnectionResult> zaj() {
        return this.zaay;
    }
}
