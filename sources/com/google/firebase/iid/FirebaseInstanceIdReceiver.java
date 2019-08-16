package com.google.firebase.iid;

import android.annotation.SuppressLint;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.os.Build.VERSION;
import android.os.Parcelable;
import android.support.p000v4.content.WakefulBroadcastReceiver;
import android.util.Base64;
import android.util.Log;
import com.google.android.gms.common.internal.ShowFirstParty;
import com.google.android.gms.common.util.PlatformVersion;
import com.google.android.gms.drive.DriveFile;
import javax.annotation.concurrent.GuardedBy;

public final class FirebaseInstanceIdReceiver extends WakefulBroadcastReceiver {
    @GuardedBy("FirebaseInstanceIdReceiver.class")
    private static zzi zzbl;

    @ShowFirstParty
    @SuppressLint({"InlinedApi"})
    public static int zza(BroadcastReceiver broadcastReceiver, Context context, Intent intent) {
        boolean z = false;
        boolean z2 = PlatformVersion.isAtLeastO() && context.getApplicationInfo().targetSdkVersion >= 26;
        if ((intent.getFlags() & DriveFile.MODE_READ_ONLY) != 0) {
            z = true;
        }
        if (z2 && !z) {
            return zzb(broadcastReceiver, context, intent);
        }
        int zzc = zzaw.zzak().zzc(context, intent);
        if (!PlatformVersion.isAtLeastO() || zzc != 402) {
            return zzc;
        }
        zzb(broadcastReceiver, context, intent);
        return 403;
    }

    private static zzi zza(Context context, String str) {
        zzi zzi;
        synchronized (FirebaseInstanceIdReceiver.class) {
            try {
                if (zzbl == null) {
                    zzbl = new zzi(context, str);
                }
                zzi = zzbl;
            } finally {
                Class<FirebaseInstanceIdReceiver> cls = FirebaseInstanceIdReceiver.class;
            }
        }
        return zzi;
    }

    private final void zza(Context context, Intent intent) {
        int zza;
        intent.setComponent(null);
        intent.setPackage(context.getPackageName());
        if (VERSION.SDK_INT <= 18) {
            intent.removeCategory(context.getPackageName());
        }
        if ("google.com/iid".equals(intent.getStringExtra("from"))) {
            String stringExtra = intent.getStringExtra("CMD");
            if (stringExtra != null) {
                if (Log.isLoggable("FirebaseInstanceId", 3)) {
                    String valueOf = String.valueOf(intent.getExtras());
                    Log.d("FirebaseInstanceId", new StringBuilder(String.valueOf(stringExtra).length() + 21 + String.valueOf(valueOf).length()).append("Received command: ").append(stringExtra).append(" - ").append(valueOf).toString());
                }
                if ("RST".equals(stringExtra) || "RST_FULL".equals(stringExtra)) {
                    FirebaseInstanceId.getInstance().zzn();
                } else if ("SYNC".equals(stringExtra)) {
                    FirebaseInstanceId.getInstance().zzp();
                }
            }
            zza = -1;
        } else {
            String stringExtra2 = intent.getStringExtra("gcm.rawData64");
            if (stringExtra2 != null) {
                intent.putExtra("rawData", Base64.decode(stringExtra2, 0));
                intent.removeExtra("gcm.rawData64");
            }
            zza = zza(this, context, intent);
        }
        if (isOrderedBroadcast()) {
            setResultCode(zza);
        }
    }

    private static int zzb(BroadcastReceiver broadcastReceiver, Context context, Intent intent) {
        if (Log.isLoggable("FirebaseInstanceId", 3)) {
            Log.d("FirebaseInstanceId", "Binding to service");
        }
        if (broadcastReceiver.isOrderedBroadcast()) {
            broadcastReceiver.setResultCode(-1);
        }
        zza(context, "com.google.firebase.MESSAGING_EVENT").zza(intent, broadcastReceiver.goAsync());
        return -1;
    }

    public final void onReceive(Context context, Intent intent) {
        if (intent != null) {
            Parcelable parcelableExtra = intent.getParcelableExtra("wrapped_intent");
            Intent intent2 = parcelableExtra instanceof Intent ? (Intent) parcelableExtra : null;
            if (intent2 != null) {
                zza(context, intent2);
            } else {
                zza(context, intent);
            }
        }
    }
}
