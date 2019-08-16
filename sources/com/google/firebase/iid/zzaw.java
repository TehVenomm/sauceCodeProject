package com.google.firebase.iid;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ResolveInfo;
import android.content.pm.ServiceInfo;
import android.support.annotation.Nullable;
import android.support.annotation.VisibleForTesting;
import android.support.p000v4.content.WakefulBroadcastReceiver;
import android.util.Log;
import com.appsflyer.share.Constants;
import java.util.ArrayDeque;
import java.util.Queue;
import javax.annotation.concurrent.GuardedBy;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;

public final class zzaw {
    private static zzaw zzdf;
    @Nullable
    @GuardedBy("this")
    private String zzdg = null;
    private Boolean zzdh = null;
    private Boolean zzdi = null;
    @VisibleForTesting
    private final Queue<Intent> zzdj = new ArrayDeque();

    private zzaw() {
    }

    public static zzaw zzak() {
        zzaw zzaw;
        synchronized (zzaw.class) {
            try {
                if (zzdf == null) {
                    zzdf = new zzaw();
                }
                zzaw = zzdf;
            } finally {
                Class<zzaw> cls = zzaw.class;
            }
        }
        return zzaw;
    }

    private final int zzd(Context context, Intent intent) {
        ComponentName startService;
        String zze = zze(context, intent);
        if (zze != null) {
            if (Log.isLoggable("FirebaseInstanceId", 3)) {
                String valueOf = String.valueOf(zze);
                Log.d("FirebaseInstanceId", valueOf.length() != 0 ? "Restricting intent to a specific service: ".concat(valueOf) : new String("Restricting intent to a specific service: "));
            }
            intent.setClassName(context.getPackageName(), zze);
        }
        try {
            if (zzd(context)) {
                startService = WakefulBroadcastReceiver.startWakefulService(context, intent);
            } else {
                startService = context.startService(intent);
                Log.d("FirebaseInstanceId", "Missing wake lock permission, service start may be delayed");
            }
            if (startService != null) {
                return -1;
            }
            Log.e("FirebaseInstanceId", "Error while delivering the message: ServiceIntent not found.");
            return 404;
        } catch (SecurityException e) {
            Log.e("FirebaseInstanceId", "Error while delivering the message to the serviceIntent", e);
            return 401;
        } catch (IllegalStateException e2) {
            String valueOf2 = String.valueOf(e2);
            Log.e("FirebaseInstanceId", new StringBuilder(String.valueOf(valueOf2).length() + 45).append("Failed to start service while in background: ").append(valueOf2).toString());
            return 402;
        }
    }

    @Nullable
    private final String zze(Context context, Intent intent) {
        String str = null;
        synchronized (this) {
            if (this.zzdg != null) {
                str = this.zzdg;
            } else {
                ResolveInfo resolveService = context.getPackageManager().resolveService(intent, 0);
                if (resolveService == null || resolveService.serviceInfo == null) {
                    Log.e("FirebaseInstanceId", "Failed to resolve target intent service, skipping classname enforcement");
                } else {
                    ServiceInfo serviceInfo = resolveService.serviceInfo;
                    if (!context.getPackageName().equals(serviceInfo.packageName) || serviceInfo.name == null) {
                        String str2 = serviceInfo.packageName;
                        String str3 = serviceInfo.name;
                        Log.e("FirebaseInstanceId", new StringBuilder(String.valueOf(str2).length() + 94 + String.valueOf(str3).length()).append("Error resolving target intent service, skipping classname enforcement. Resolved service was: ").append(str2).append(Constants.URL_PATH_DELIMITER).append(str3).toString());
                    } else {
                        if (serviceInfo.name.startsWith(AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER)) {
                            String valueOf = String.valueOf(context.getPackageName());
                            String valueOf2 = String.valueOf(serviceInfo.name);
                            this.zzdg = valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf);
                        } else {
                            this.zzdg = serviceInfo.name;
                        }
                        str = this.zzdg;
                    }
                }
            }
        }
        return str;
    }

    public final Intent zzal() {
        return (Intent) this.zzdj.poll();
    }

    public final int zzc(Context context, Intent intent) {
        if (Log.isLoggable("FirebaseInstanceId", 3)) {
            Log.d("FirebaseInstanceId", "Starting service");
        }
        this.zzdj.offer(intent);
        Intent intent2 = new Intent("com.google.firebase.MESSAGING_EVENT");
        intent2.setPackage(context.getPackageName());
        return zzd(context, intent2);
    }

    /* access modifiers changed from: 0000 */
    public final boolean zzd(Context context) {
        if (this.zzdh == null) {
            this.zzdh = Boolean.valueOf(context.checkCallingOrSelfPermission("android.permission.WAKE_LOCK") == 0);
        }
        if (!this.zzdh.booleanValue() && Log.isLoggable("FirebaseInstanceId", 3)) {
            Log.d("FirebaseInstanceId", "Missing Permission: android.permission.WAKE_LOCK this should normally be included by the manifest merger, but may needed to be manually added to your manifest");
        }
        return this.zzdh.booleanValue();
    }

    /* access modifiers changed from: 0000 */
    public final boolean zze(Context context) {
        if (this.zzdi == null) {
            this.zzdi = Boolean.valueOf(context.checkCallingOrSelfPermission("android.permission.ACCESS_NETWORK_STATE") == 0);
        }
        if (!this.zzdh.booleanValue() && Log.isLoggable("FirebaseInstanceId", 3)) {
            Log.d("FirebaseInstanceId", "Missing Permission: android.permission.ACCESS_NETWORK_STATE this should normally be included by the manifest merger, but may needed to be manually added to your manifest");
        }
        return this.zzdi.booleanValue();
    }
}
