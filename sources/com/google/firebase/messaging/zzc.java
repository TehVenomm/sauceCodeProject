package com.google.firebase.messaging;

import android.app.ActivityManager;
import android.app.ActivityManager.RunningAppProcessInfo;
import android.app.KeyguardManager;
import android.app.NotificationManager;
import android.content.Context;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.os.Process;
import android.os.SystemClock;
import android.support.p000v4.app.NotificationCompat.BigPictureStyle;
import android.support.p000v4.app.NotificationCompat.Builder;
import android.util.Log;
import com.facebook.appevents.AppEventsConstants;
import com.google.android.gms.common.util.PlatformVersion;
import com.google.android.gms.tasks.Tasks;
import java.util.Iterator;
import java.util.List;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Executor;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;
import p018jp.colopl.drapro.LocalNotificationAlarmReceiver;

final class zzc {
    private final Context zzag;
    private final Bundle zzcm;
    private final Executor zzdy;
    private final zzb zzdz;

    public zzc(Context context, Bundle bundle, Executor executor) {
        this.zzdy = executor;
        this.zzag = context;
        this.zzcm = bundle;
        this.zzdz = new zzb(context, context.getPackageName());
    }

    /* access modifiers changed from: 0000 */
    public final boolean zzas() {
        boolean z;
        if (AppEventsConstants.EVENT_PARAM_VALUE_YES.equals(zzb.zza(this.zzcm, "gcm.n.noui"))) {
            return true;
        }
        if (!((KeyguardManager) this.zzag.getSystemService("keyguard")).inKeyguardRestrictedInputMode()) {
            if (!PlatformVersion.isAtLeastLollipop()) {
                SystemClock.sleep(10);
            }
            int myPid = Process.myPid();
            List runningAppProcesses = ((ActivityManager) this.zzag.getSystemService(LocalNotificationAlarmReceiver.EXTRA_ACTIVITY)).getRunningAppProcesses();
            if (runningAppProcesses != null) {
                Iterator it = runningAppProcesses.iterator();
                while (true) {
                    if (!it.hasNext()) {
                        break;
                    }
                    RunningAppProcessInfo runningAppProcessInfo = (RunningAppProcessInfo) it.next();
                    if (runningAppProcessInfo.pid == myPid) {
                        z = runningAppProcessInfo.importance == 100;
                    }
                }
            }
        }
        z = false;
        if (z) {
            return false;
        }
        zzd zzo = zzd.zzo(zzb.zza(this.zzcm, "gcm.n.image"));
        if (zzo != null) {
            zzo.zza(this.zzdy);
        }
        zza zzf = this.zzdz.zzf(this.zzcm);
        Builder builder = zzf.zzds;
        if (zzo != null) {
            try {
                Bitmap bitmap = (Bitmap) Tasks.await(zzo.getTask(), 5, TimeUnit.SECONDS);
                builder.setLargeIcon(bitmap);
                builder.setStyle(new BigPictureStyle().bigPicture(bitmap).bigLargeIcon(null));
            } catch (ExecutionException e) {
            } catch (InterruptedException e2) {
                Log.w("FirebaseMessaging", "Interrupted while downloading image, showing notification without it");
                zzo.close();
                Thread.currentThread().interrupt();
            } catch (TimeoutException e3) {
                Log.w("FirebaseMessaging", "Failed to download image in time, showing notification without it");
                zzo.close();
            }
        }
        if (Log.isLoggable("FirebaseMessaging", 3)) {
            Log.d("FirebaseMessaging", "Showing notification");
        }
        ((NotificationManager) this.zzag.getSystemService("notification")).notify(zzf.tag, 0, zzf.zzds.build());
        return true;
    }
}
