package com.google.firebase.iid;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Looper;
import android.support.v4.util.ArrayMap;
import android.util.Log;
import com.facebook.appevents.AppEventsConstants;
import com.facebook.share.internal.ShareConstants;
import com.google.android.gms.gcm.GoogleCloudMessaging;
import java.io.IOException;
import java.security.KeyPair;
import java.util.Map;
import jp.colopl.gcm.RegistrarHelper;

public final class zzj {
    private static Map<String, zzj> zzhtf = new ArrayMap();
    static String zzhtl;
    private static zzr zzmjg;
    private static zzl zzmjh;
    private Context mContext;
    private KeyPair zzhti;
    private String zzhtj = "";

    private zzj(Context context, String str, Bundle bundle) {
        this.mContext = context.getApplicationContext();
        this.zzhtj = str;
    }

    public static zzj zza(Context context, Bundle bundle) {
        zzj zzj;
        synchronized (zzj.class) {
            String string = bundle == null ? "" : bundle.getString("subtype");
            String str = string == null ? "" : string;
            try {
                Context applicationContext = context.getApplicationContext();
                if (zzmjg == null) {
                    zzmjg = new zzr(applicationContext);
                    zzmjh = new zzl(applicationContext);
                }
                zzhtl = Integer.toString(FirebaseInstanceId.zzei(applicationContext));
                zzj = (zzj) zzhtf.get(str);
                if (zzj == null) {
                    zzj = new zzj(applicationContext, str, bundle);
                    zzhtf.put(str, zzj);
                }
            } catch (Throwable th) {
                Class cls = zzj.class;
            }
        }
        return zzj;
    }

    public static zzr zzbyl() {
        return zzmjg;
    }

    public static zzl zzbym() {
        return zzmjh;
    }

    public final long getCreationTime() {
        return zzmjg.zzpv(this.zzhtj);
    }

    public final String getToken(String str, String str2, Bundle bundle) throws IOException {
        if (Looper.getMainLooper() == Looper.myLooper()) {
            throw new IOException(GoogleCloudMessaging.ERROR_MAIN_THREAD);
        }
        Object obj = 1;
        if (bundle.getString("ttl") != null || "jwt".equals(bundle.getString(ShareConstants.MEDIA_TYPE))) {
            obj = null;
        } else {
            zzs zzo = zzmjg.zzo(this.zzhtj, str, str2);
            if (!(zzo == null || zzo.zzqa(zzhtl))) {
                return zzo.zzkmz;
            }
        }
        String zzb = zzb(str, str2, bundle);
        if (zzb == null || r0 == null) {
            return zzb;
        }
        zzmjg.zza(this.zzhtj, str, str2, zzb, zzhtl);
        return zzb;
    }

    public final void zza(String str, String str2, Bundle bundle) throws IOException {
        if (Looper.getMainLooper() == Looper.myLooper()) {
            throw new IOException(GoogleCloudMessaging.ERROR_MAIN_THREAD);
        }
        zzmjg.zzf(this.zzhtj, str, str2);
        if (bundle == null) {
            bundle = new Bundle();
        }
        bundle.putString("delete", AppEventsConstants.EVENT_PARAM_VALUE_YES);
        zzb(str, str2, bundle);
    }

    final KeyPair zzasp() {
        if (this.zzhti == null) {
            this.zzhti = zzmjg.zzpy(this.zzhtj);
        }
        if (this.zzhti == null) {
            this.zzhti = zzmjg.zzpw(this.zzhtj);
        }
        return this.zzhti;
    }

    public final void zzasq() {
        zzmjg.zzpx(this.zzhtj);
        this.zzhti = null;
    }

    public final String zzb(String str, String str2, Bundle bundle) throws IOException {
        if (str2 != null) {
            bundle.putString("scope", str2);
        }
        bundle.putString("sender", str);
        if (!"".equals(this.zzhtj)) {
            str = this.zzhtj;
        }
        bundle.putString("subtype", str);
        bundle.putString("X-subtype", str);
        Intent zza = zzmjh.zza(bundle, zzasp());
        if (zza == null) {
            throw new IOException(GoogleCloudMessaging.ERROR_SERVICE_NOT_AVAILABLE);
        }
        String stringExtra = zza.getStringExtra(RegistrarHelper.PROPERTY_REG_ID);
        if (stringExtra == null) {
            stringExtra = zza.getStringExtra("unregistered");
        }
        if (stringExtra != null) {
            return stringExtra;
        }
        stringExtra = zza.getStringExtra("error");
        if (stringExtra != null) {
            throw new IOException(stringExtra);
        }
        stringExtra = String.valueOf(zza.getExtras());
        Log.w("InstanceID/Rpc", new StringBuilder(String.valueOf(stringExtra).length() + 29).append("Unexpected response from GCM ").append(stringExtra).toString(), new Throwable());
        throw new IOException(GoogleCloudMessaging.ERROR_SERVICE_NOT_AVAILABLE);
    }
}
