package com.google.firebase.iid;

import android.text.TextUtils;
import android.util.Log;
import com.google.android.gms.measurement.AppMeasurement.Param;
import java.util.concurrent.TimeUnit;
import org.json.JSONException;
import org.json.JSONObject;

final class zzs {
    private static final long zzmjs = TimeUnit.DAYS.toMillis(7);
    private long timestamp;
    private String zzhtl;
    final String zzkmz;

    private zzs(String str, String str2, long j) {
        this.zzkmz = str;
        this.zzhtl = str2;
        this.timestamp = j;
    }

    static String zzc(String str, String str2, long j) {
        try {
            JSONObject jSONObject = new JSONObject();
            jSONObject.put("token", str);
            jSONObject.put("appVersion", str2);
            jSONObject.put(Param.TIMESTAMP, j);
            return jSONObject.toString();
        } catch (JSONException e) {
            String valueOf = String.valueOf(e);
            Log.w("InstanceID/Store", new StringBuilder(String.valueOf(valueOf).length() + 24).append("Failed to encode token: ").append(valueOf).toString());
            return null;
        }
    }

    static zzs zzpz(String str) {
        if (TextUtils.isEmpty(str)) {
            return null;
        }
        if (!str.startsWith("{")) {
            return new zzs(str, null, 0);
        }
        try {
            JSONObject jSONObject = new JSONObject(str);
            return new zzs(jSONObject.getString("token"), jSONObject.getString("appVersion"), jSONObject.getLong(Param.TIMESTAMP));
        } catch (JSONException e) {
            String valueOf = String.valueOf(e);
            Log.w("InstanceID/Store", new StringBuilder(String.valueOf(valueOf).length() + 23).append("Failed to parse token: ").append(valueOf).toString());
            return null;
        }
    }

    final boolean zzqa(String str) {
        return System.currentTimeMillis() > this.timestamp + zzmjs || !str.equals(this.zzhtl);
    }
}
