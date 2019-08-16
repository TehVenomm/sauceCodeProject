package com.google.firebase.iid;

import android.content.BroadcastReceiver.PendingResult;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.IBinder;
import android.support.annotation.GuardedBy;
import android.support.annotation.VisibleForTesting;
import android.util.Log;
import com.google.android.gms.common.util.concurrent.NamedThreadFactory;
import java.util.ArrayDeque;
import java.util.Queue;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledThreadPoolExecutor;

public final class zzi implements ServiceConnection {
    private final Context zzag;
    private final Intent zzah;
    private final ScheduledExecutorService zzai;
    private final Queue<zze> zzaj;
    private zzg zzak;
    @GuardedBy("this")
    private boolean zzal;

    public zzi(Context context, String str) {
        this(context, str, new ScheduledThreadPoolExecutor(0, new NamedThreadFactory("Firebase-FirebaseInstanceIdServiceConnection")));
    }

    @VisibleForTesting
    private zzi(Context context, String str, ScheduledExecutorService scheduledExecutorService) {
        this.zzaj = new ArrayDeque();
        this.zzal = false;
        this.zzag = context.getApplicationContext();
        this.zzah = new Intent(str).setPackage(this.zzag.getPackageName());
        this.zzai = scheduledExecutorService;
    }

    /* JADX WARNING: Code restructure failed: missing block: B:26:0x005f, code lost:
        if (android.util.Log.isLoggable("EnhancedIntentService", 3) == false) goto L_0x0080;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:28:0x0063, code lost:
        if (r4.zzal != false) goto L_0x0099;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:29:0x0065, code lost:
        r0 = true;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:30:0x0066, code lost:
        android.util.Log.d("EnhancedIntentService", "binder is dead. start connection? " + r0);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:32:0x0082, code lost:
        if (r4.zzal != false) goto L_0x0097;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:33:0x0084, code lost:
        r4.zzal = true;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:36:0x0095, code lost:
        if (com.google.android.gms.common.stats.ConnectionTracker.getInstance().bindService(r4.zzag, r4.zzah, r4, 65) == false) goto L_0x009b;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:39:0x0099, code lost:
        r0 = false;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:42:?, code lost:
        android.util.Log.e("EnhancedIntentService", "binding to the service failed");
     */
    /* JADX WARNING: Code restructure failed: missing block: B:46:0x00a9, code lost:
        r0 = move-exception;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:47:0x00aa, code lost:
        android.util.Log.e("EnhancedIntentService", "Exception while binding the service", r0);
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final void zzf() {
        /*
            r4 = this;
            r1 = 1
            r2 = 0
            monitor-enter(r4)
            java.lang.String r0 = "EnhancedIntentService"
            r3 = 3
            boolean r0 = android.util.Log.isLoggable(r0, r3)     // Catch:{ all -> 0x0055 }
            if (r0 == 0) goto L_0x0013
            java.lang.String r0 = "EnhancedIntentService"
            java.lang.String r3 = "flush queue called"
            android.util.Log.d(r0, r3)     // Catch:{ all -> 0x0055 }
        L_0x0013:
            java.util.Queue<com.google.firebase.iid.zze> r0 = r4.zzaj     // Catch:{ all -> 0x0055 }
            boolean r0 = r0.isEmpty()     // Catch:{ all -> 0x0055 }
            if (r0 != 0) goto L_0x0097
            java.lang.String r0 = "EnhancedIntentService"
            r3 = 3
            boolean r0 = android.util.Log.isLoggable(r0, r3)     // Catch:{ all -> 0x0055 }
            if (r0 == 0) goto L_0x002b
            java.lang.String r0 = "EnhancedIntentService"
            java.lang.String r3 = "found intent to be delivered"
            android.util.Log.d(r0, r3)     // Catch:{ all -> 0x0055 }
        L_0x002b:
            com.google.firebase.iid.zzg r0 = r4.zzak     // Catch:{ all -> 0x0055 }
            if (r0 == 0) goto L_0x0058
            com.google.firebase.iid.zzg r0 = r4.zzak     // Catch:{ all -> 0x0055 }
            boolean r0 = r0.isBinderAlive()     // Catch:{ all -> 0x0055 }
            if (r0 == 0) goto L_0x0058
            java.lang.String r0 = "EnhancedIntentService"
            r3 = 3
            boolean r0 = android.util.Log.isLoggable(r0, r3)     // Catch:{ all -> 0x0055 }
            if (r0 == 0) goto L_0x0047
            java.lang.String r0 = "EnhancedIntentService"
            java.lang.String r3 = "binder is alive, sending the intent."
            android.util.Log.d(r0, r3)     // Catch:{ all -> 0x0055 }
        L_0x0047:
            java.util.Queue<com.google.firebase.iid.zze> r0 = r4.zzaj     // Catch:{ all -> 0x0055 }
            java.lang.Object r0 = r0.poll()     // Catch:{ all -> 0x0055 }
            com.google.firebase.iid.zze r0 = (com.google.firebase.iid.zze) r0     // Catch:{ all -> 0x0055 }
            com.google.firebase.iid.zzg r3 = r4.zzak     // Catch:{ all -> 0x0055 }
            r3.zza(r0)     // Catch:{ all -> 0x0055 }
            goto L_0x0013
        L_0x0055:
            r0 = move-exception
            monitor-exit(r4)
            throw r0
        L_0x0058:
            java.lang.String r0 = "EnhancedIntentService"
            r3 = 3
            boolean r0 = android.util.Log.isLoggable(r0, r3)     // Catch:{ all -> 0x0055 }
            if (r0 == 0) goto L_0x0080
            boolean r0 = r4.zzal     // Catch:{ all -> 0x0055 }
            if (r0 != 0) goto L_0x0099
            r0 = r1
        L_0x0066:
            java.lang.StringBuilder r1 = new java.lang.StringBuilder     // Catch:{ all -> 0x0055 }
            r2 = 39
            r1.<init>(r2)     // Catch:{ all -> 0x0055 }
            java.lang.String r2 = "EnhancedIntentService"
            java.lang.String r3 = "binder is dead. start connection? "
            java.lang.StringBuilder r1 = r1.append(r3)     // Catch:{ all -> 0x0055 }
            java.lang.StringBuilder r0 = r1.append(r0)     // Catch:{ all -> 0x0055 }
            java.lang.String r0 = r0.toString()     // Catch:{ all -> 0x0055 }
            android.util.Log.d(r2, r0)     // Catch:{ all -> 0x0055 }
        L_0x0080:
            boolean r0 = r4.zzal     // Catch:{ all -> 0x0055 }
            if (r0 != 0) goto L_0x0097
            r0 = 1
            r4.zzal = r0     // Catch:{ all -> 0x0055 }
            com.google.android.gms.common.stats.ConnectionTracker r0 = com.google.android.gms.common.stats.ConnectionTracker.getInstance()     // Catch:{ SecurityException -> 0x00a9 }
            android.content.Context r1 = r4.zzag     // Catch:{ SecurityException -> 0x00a9 }
            android.content.Intent r2 = r4.zzah     // Catch:{ SecurityException -> 0x00a9 }
            r3 = 65
            boolean r0 = r0.bindService(r1, r2, r4, r3)     // Catch:{ SecurityException -> 0x00a9 }
            if (r0 == 0) goto L_0x009b
        L_0x0097:
            monitor-exit(r4)
            return
        L_0x0099:
            r0 = r2
            goto L_0x0066
        L_0x009b:
            java.lang.String r0 = "EnhancedIntentService"
            java.lang.String r1 = "binding to the service failed"
            android.util.Log.e(r0, r1)     // Catch:{ SecurityException -> 0x00a9 }
        L_0x00a2:
            r0 = 0
            r4.zzal = r0     // Catch:{ all -> 0x0055 }
            r4.zzg()     // Catch:{ all -> 0x0055 }
            goto L_0x0097
        L_0x00a9:
            r0 = move-exception
            java.lang.String r1 = "EnhancedIntentService"
            java.lang.String r2 = "Exception while binding the service"
            android.util.Log.e(r1, r2, r0)     // Catch:{ all -> 0x0055 }
            goto L_0x00a2
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.firebase.iid.zzi.zzf():void");
    }

    @GuardedBy("this")
    private final void zzg() {
        while (!this.zzaj.isEmpty()) {
            ((zze) this.zzaj.poll()).finish();
        }
    }

    public final void onServiceConnected(ComponentName componentName, IBinder iBinder) {
        synchronized (this) {
            if (Log.isLoggable("EnhancedIntentService", 3)) {
                String valueOf = String.valueOf(componentName);
                Log.d("EnhancedIntentService", new StringBuilder(String.valueOf(valueOf).length() + 20).append("onServiceConnected: ").append(valueOf).toString());
            }
            this.zzal = false;
            if (!(iBinder instanceof zzg)) {
                String valueOf2 = String.valueOf(iBinder);
                Log.e("EnhancedIntentService", new StringBuilder(String.valueOf(valueOf2).length() + 28).append("Invalid service connection: ").append(valueOf2).toString());
                zzg();
            } else {
                this.zzak = (zzg) iBinder;
                zzf();
            }
        }
    }

    public final void onServiceDisconnected(ComponentName componentName) {
        if (Log.isLoggable("EnhancedIntentService", 3)) {
            String valueOf = String.valueOf(componentName);
            Log.d("EnhancedIntentService", new StringBuilder(String.valueOf(valueOf).length() + 23).append("onServiceDisconnected: ").append(valueOf).toString());
        }
        zzf();
    }

    public final void zza(Intent intent, PendingResult pendingResult) {
        synchronized (this) {
            if (Log.isLoggable("EnhancedIntentService", 3)) {
                Log.d("EnhancedIntentService", "new intent queued in the bind-strategy delivery");
            }
            this.zzaj.add(new zze(intent, pendingResult, this.zzai));
            zzf();
        }
    }
}
