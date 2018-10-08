package com.google.firebase.iid;

import android.content.Context;
import android.content.Intent;
import android.os.Parcelable;
import android.support.v4.content.WakefulBroadcastReceiver;
import android.util.Log;
import com.google.android.gms.common.util.zzp;

public final class FirebaseInstanceIdInternalReceiver extends WakefulBroadcastReceiver {
    private static boolean zzhqp = false;
    private static zzh zzmja;
    private static zzh zzmjb;

    static zzh zzah(Context context, String str) {
        zzh zzh;
        synchronized (FirebaseInstanceIdInternalReceiver.class) {
            try {
                if ("com.google.firebase.MESSAGING_EVENT".equals(str)) {
                    if (zzmjb == null) {
                        zzmjb = new zzh(context, str);
                    }
                    zzh = zzmjb;
                } else {
                    if (zzmja == null) {
                        zzmja = new zzh(context, str);
                    }
                    zzh = zzmja;
                }
            } catch (Throwable th) {
                Class cls = FirebaseInstanceIdInternalReceiver.class;
            }
        }
        return zzh;
    }

    static boolean zzek(Context context) {
        return zzp.isAtLeastO() && context.getApplicationInfo().targetSdkVersion > 25;
    }

    public final void onReceive(Context context, Intent intent) {
        if (intent != null) {
            Parcelable parcelableExtra = intent.getParcelableExtra("wrapped_intent");
            if (parcelableExtra instanceof Intent) {
                Intent intent2 = (Intent) parcelableExtra;
                if (zzek(context)) {
                    zzah(context, intent.getAction()).zza(intent2, goAsync());
                    return;
                } else {
                    zzq.zzbyp().zza(context, intent.getAction(), intent2);
                    return;
                }
            }
            Log.e("FirebaseInstanceId", "Missing or invalid wrapped intent");
        }
    }
}
