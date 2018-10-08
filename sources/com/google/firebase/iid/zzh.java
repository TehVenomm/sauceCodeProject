package com.google.firebase.iid;

import android.content.BroadcastReceiver.PendingResult;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.IBinder;
import android.support.annotation.VisibleForTesting;
import android.util.Log;
import com.google.android.gms.common.stats.zza;
import java.util.LinkedList;
import java.util.Queue;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledThreadPoolExecutor;

public final class zzh implements ServiceConnection {
    private final Context zzaie;
    private final Intent zzmiq;
    private final ScheduledExecutorService zzmir;
    private final Queue<zzd> zzmis;
    private zzf zzmit;
    private boolean zzmiu;

    public zzh(Context context, String str) {
        this(context, str, new ScheduledThreadPoolExecutor(0));
    }

    @VisibleForTesting
    private zzh(Context context, String str, ScheduledExecutorService scheduledExecutorService) {
        this.zzmis = new LinkedList();
        this.zzmiu = false;
        this.zzaie = context.getApplicationContext();
        this.zzmiq = new Intent(str).setPackage(this.zzaie.getPackageName());
        this.zzmir = scheduledExecutorService;
    }

    private final void zzbyg() {
        synchronized (this) {
            if (Log.isLoggable("EnhancedIntentService", 3)) {
                Log.d("EnhancedIntentService", "flush queue called");
            }
            while (!this.zzmis.isEmpty()) {
                if (Log.isLoggable("EnhancedIntentService", 3)) {
                    Log.d("EnhancedIntentService", "found intent to be delivered");
                }
                if (this.zzmit == null || !this.zzmit.isBinderAlive()) {
                    if (Log.isLoggable("EnhancedIntentService", 3)) {
                        Log.d("EnhancedIntentService", "binder is dead. start connection? " + (!this.zzmiu));
                    }
                    if (!this.zzmiu) {
                        this.zzmiu = true;
                        try {
                            if (!zza.zzaky().zza(this.zzaie, this.zzmiq, this, 65)) {
                                Log.e("EnhancedIntentService", "binding to the service failed");
                                while (!this.zzmis.isEmpty()) {
                                    ((zzd) this.zzmis.poll()).finish();
                                }
                            }
                        } catch (Throwable e) {
                            Log.e("EnhancedIntentService", "Exception while binding the service", e);
                        }
                    }
                } else {
                    if (Log.isLoggable("EnhancedIntentService", 3)) {
                        Log.d("EnhancedIntentService", "binder is alive, sending the intent.");
                    }
                    this.zzmit.zza((zzd) this.zzmis.poll());
                }
            }
        }
    }

    public final void onServiceConnected(ComponentName componentName, IBinder iBinder) {
        synchronized (this) {
            this.zzmiu = false;
            this.zzmit = (zzf) iBinder;
            if (Log.isLoggable("EnhancedIntentService", 3)) {
                String valueOf = String.valueOf(componentName);
                Log.d("EnhancedIntentService", new StringBuilder(String.valueOf(valueOf).length() + 20).append("onServiceConnected: ").append(valueOf).toString());
            }
            zzbyg();
        }
    }

    public final void onServiceDisconnected(ComponentName componentName) {
        if (Log.isLoggable("EnhancedIntentService", 3)) {
            String valueOf = String.valueOf(componentName);
            Log.d("EnhancedIntentService", new StringBuilder(String.valueOf(valueOf).length() + 23).append("onServiceDisconnected: ").append(valueOf).toString());
        }
        zzbyg();
    }

    public final void zza(Intent intent, PendingResult pendingResult) {
        synchronized (this) {
            if (Log.isLoggable("EnhancedIntentService", 3)) {
                Log.d("EnhancedIntentService", "new intent queued in the bind-strategy delivery");
            }
            this.zzmis.add(new zzd(intent, pendingResult, this.zzmir));
            zzbyg();
        }
    }
}
