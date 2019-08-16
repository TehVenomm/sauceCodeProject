package com.google.firebase.iid;

import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Looper;
import android.os.Message;
import android.os.Messenger;
import android.os.Parcelable;
import android.support.p000v4.util.SimpleArrayMap;
import android.util.Log;
import com.google.android.gms.tasks.TaskCompletionSource;
import com.google.android.gms.tasks.Tasks;
import com.google.firebase.iid.zzm.zza;
import java.io.IOException;
import java.util.concurrent.ExecutionException;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import javax.annotation.concurrent.GuardedBy;
import p017io.fabric.sdk.android.services.settings.SettingsJsonConstants;
import p018jp.colopl.gcm.RegistrarHelper;

final class zzau {
    private static int zzck = 0;
    private static PendingIntent zzcx;
    private final Context zzag;
    private final zzan zzav;
    @GuardedBy("responseCallbacks")
    private final SimpleArrayMap<String, TaskCompletionSource<Bundle>> zzcy = new SimpleArrayMap<>();
    private Messenger zzcz;
    private Messenger zzda;
    private zzm zzdb;

    public zzau(Context context, zzan zzan) {
        this.zzag = context;
        this.zzav = zzan;
        this.zzcz = new Messenger(new zzat(this, Looper.getMainLooper()));
    }

    private final void zza(String str, Bundle bundle) {
        synchronized (this.zzcy) {
            TaskCompletionSource taskCompletionSource = (TaskCompletionSource) this.zzcy.remove(str);
            if (taskCompletionSource == null) {
                String valueOf = String.valueOf(str);
                Log.w("FirebaseInstanceId", valueOf.length() != 0 ? "Missing callback for ".concat(valueOf) : new String("Missing callback for "));
                return;
            }
            taskCompletionSource.setResult(bundle);
        }
    }

    private static String zzah() {
        String num;
        synchronized (zzau.class) {
            try {
                int i = zzck;
                zzck = i + 1;
                num = Integer.toString(i);
            } finally {
                Class<zzau> cls = zzau.class;
            }
        }
        return num;
    }

    private static void zzb(Context context, Intent intent) {
        synchronized (zzau.class) {
            try {
                if (zzcx == null) {
                    Intent intent2 = new Intent();
                    intent2.setPackage("com.google.example.invalidpackage");
                    zzcx = PendingIntent.getBroadcast(context, 0, intent2, 0);
                }
                intent.putExtra(SettingsJsonConstants.APP_KEY, zzcx);
            } finally {
                Class<zzau> cls = zzau.class;
            }
        }
    }

    /* access modifiers changed from: private */
    public final void zzb(Message message) {
        if (message == null || !(message.obj instanceof Intent)) {
            Log.w("FirebaseInstanceId", "Dropping invalid message");
            return;
        }
        Intent intent = (Intent) message.obj;
        intent.setExtrasClassLoader(new zza());
        if (intent.hasExtra("google.messenger")) {
            Parcelable parcelableExtra = intent.getParcelableExtra("google.messenger");
            if (parcelableExtra instanceof zzm) {
                this.zzdb = (zzm) parcelableExtra;
            }
            if (parcelableExtra instanceof Messenger) {
                this.zzda = (Messenger) parcelableExtra;
            }
        }
        Intent intent2 = (Intent) message.obj;
        String action = intent2.getAction();
        if ("com.google.android.c2dm.intent.REGISTRATION".equals(action)) {
            String stringExtra = intent2.getStringExtra(RegistrarHelper.PROPERTY_REG_ID);
            if (stringExtra == null) {
                stringExtra = intent2.getStringExtra("unregistered");
            }
            if (stringExtra == null) {
                String stringExtra2 = intent2.getStringExtra("error");
                if (stringExtra2 == null) {
                    String valueOf = String.valueOf(intent2.getExtras());
                    Log.w("FirebaseInstanceId", new StringBuilder(String.valueOf(valueOf).length() + 49).append("Unexpected response, no error or registration id ").append(valueOf).toString());
                    return;
                }
                if (Log.isLoggable("FirebaseInstanceId", 3)) {
                    String valueOf2 = String.valueOf(stringExtra2);
                    Log.d("FirebaseInstanceId", valueOf2.length() != 0 ? "Received InstanceID error ".concat(valueOf2) : new String("Received InstanceID error "));
                }
                if (stringExtra2.startsWith("|")) {
                    String[] split = stringExtra2.split("\\|");
                    if (split.length <= 2 || !"ID".equals(split[1])) {
                        String valueOf3 = String.valueOf(stringExtra2);
                        Log.w("FirebaseInstanceId", valueOf3.length() != 0 ? "Unexpected structured response ".concat(valueOf3) : new String("Unexpected structured response "));
                        return;
                    }
                    String str = split[2];
                    String str2 = split[3];
                    if (str2.startsWith(":")) {
                        str2 = str2.substring(1);
                    }
                    zza(str, intent2.putExtra("error", str2).getExtras());
                    return;
                }
                synchronized (this.zzcy) {
                    for (int i = 0; i < this.zzcy.size(); i++) {
                        zza((String) this.zzcy.keyAt(i), intent2.getExtras());
                    }
                }
                return;
            }
            Matcher matcher = Pattern.compile("\\|ID\\|([^|]+)\\|:?+(.*)").matcher(stringExtra);
            if (matcher.matches()) {
                String group = matcher.group(1);
                String group2 = matcher.group(2);
                Bundle extras = intent2.getExtras();
                extras.putString(RegistrarHelper.PROPERTY_REG_ID, group2);
                zza(group, extras);
            } else if (Log.isLoggable("FirebaseInstanceId", 3)) {
                String valueOf4 = String.valueOf(stringExtra);
                Log.d("FirebaseInstanceId", valueOf4.length() != 0 ? "Unexpected response string: ".concat(valueOf4) : new String("Unexpected response string: "));
            }
        } else if (Log.isLoggable("FirebaseInstanceId", 3)) {
            String valueOf5 = String.valueOf(action);
            Log.d("FirebaseInstanceId", valueOf5.length() != 0 ? "Unexpected response action: ".concat(valueOf5) : new String("Unexpected response action: "));
        }
    }

    private final Bundle zzd(Bundle bundle) throws IOException {
        Bundle zze = zze(bundle);
        if (zze == null || !zze.containsKey("google.messenger")) {
            return zze;
        }
        Bundle zze2 = zze(bundle);
        if (zze2 == null || !zze2.containsKey("google.messenger")) {
            return zze2;
        }
        return null;
    }

    /* JADX WARNING: Removed duplicated region for block: B:32:0x00cf A[SYNTHETIC] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final android.os.Bundle zze(android.os.Bundle r10) throws java.io.IOException {
        /*
            r9 = this;
            r8 = 3
            r7 = 2
            java.lang.String r1 = zzah()
            com.google.android.gms.tasks.TaskCompletionSource r0 = new com.google.android.gms.tasks.TaskCompletionSource
            r0.<init>()
            android.support.v4.util.SimpleArrayMap<java.lang.String, com.google.android.gms.tasks.TaskCompletionSource<android.os.Bundle>> r2 = r9.zzcy
            monitor-enter(r2)
            android.support.v4.util.SimpleArrayMap<java.lang.String, com.google.android.gms.tasks.TaskCompletionSource<android.os.Bundle>> r3 = r9.zzcy     // Catch:{ all -> 0x0024 }
            r3.put(r1, r0)     // Catch:{ all -> 0x0024 }
            monitor-exit(r2)     // Catch:{ all -> 0x0024 }
            com.google.firebase.iid.zzan r2 = r9.zzav
            int r2 = r2.zzac()
            if (r2 != 0) goto L_0x0027
            java.io.IOException r0 = new java.io.IOException
            java.lang.String r1 = "MISSING_INSTANCEID_SERVICE"
            r0.<init>(r1)
            throw r0
        L_0x0024:
            r0 = move-exception
            monitor-exit(r2)     // Catch:{ all -> 0x0024 }
            throw r0
        L_0x0027:
            android.content.Intent r2 = new android.content.Intent
            r2.<init>()
            java.lang.String r3 = "com.google.android.gms"
            r2.setPackage(r3)
            com.google.firebase.iid.zzan r3 = r9.zzav
            int r3 = r3.zzac()
            if (r3 != r7) goto L_0x00d6
            java.lang.String r3 = "com.google.iid.TOKEN_REQUEST"
            r2.setAction(r3)
        L_0x003e:
            r2.putExtras(r10)
            android.content.Context r3 = r9.zzag
            zzb(r3, r2)
            java.lang.String r3 = "kid"
            java.lang.StringBuilder r4 = new java.lang.StringBuilder
            java.lang.String r5 = java.lang.String.valueOf(r1)
            int r5 = r5.length()
            int r5 = r5 + 5
            r4.<init>(r5)
            java.lang.String r5 = "|ID|"
            java.lang.StringBuilder r4 = r4.append(r5)
            java.lang.StringBuilder r4 = r4.append(r1)
            java.lang.String r5 = "|"
            java.lang.StringBuilder r4 = r4.append(r5)
            java.lang.String r4 = r4.toString()
            r2.putExtra(r3, r4)
            java.lang.String r3 = "FirebaseInstanceId"
            boolean r3 = android.util.Log.isLoggable(r3, r8)
            if (r3 == 0) goto L_0x00a0
            android.os.Bundle r3 = r2.getExtras()
            java.lang.String r3 = java.lang.String.valueOf(r3)
            java.lang.String r4 = "FirebaseInstanceId"
            java.lang.StringBuilder r5 = new java.lang.StringBuilder
            java.lang.String r6 = java.lang.String.valueOf(r3)
            int r6 = r6.length()
            int r6 = r6 + 8
            r5.<init>(r6)
            java.lang.String r6 = "Sending "
            java.lang.StringBuilder r5 = r5.append(r6)
            java.lang.StringBuilder r3 = r5.append(r3)
            java.lang.String r3 = r3.toString()
            android.util.Log.d(r4, r3)
        L_0x00a0:
            java.lang.String r3 = "google.messenger"
            android.os.Messenger r4 = r9.zzcz
            r2.putExtra(r3, r4)
            android.os.Messenger r3 = r9.zzda
            if (r3 != 0) goto L_0x00af
            com.google.firebase.iid.zzm r3 = r9.zzdb
            if (r3 == 0) goto L_0x00f3
        L_0x00af:
            android.os.Message r3 = android.os.Message.obtain()
            r3.obj = r2
            android.os.Messenger r4 = r9.zzda     // Catch:{ RemoteException -> 0x00e3 }
            if (r4 == 0) goto L_0x00dd
            android.os.Messenger r4 = r9.zzda     // Catch:{ RemoteException -> 0x00e3 }
            r4.send(r3)     // Catch:{ RemoteException -> 0x00e3 }
        L_0x00be:
            com.google.android.gms.tasks.Task r0 = r0.getTask()     // Catch:{ InterruptedException -> 0x012f, TimeoutException -> 0x010a, ExecutionException -> 0x0125 }
            r2 = 30000(0x7530, double:1.4822E-319)
            java.util.concurrent.TimeUnit r4 = java.util.concurrent.TimeUnit.MILLISECONDS     // Catch:{ InterruptedException -> 0x012f, TimeoutException -> 0x010a, ExecutionException -> 0x0125 }
            java.lang.Object r0 = com.google.android.gms.tasks.Tasks.await(r0, r2, r4)     // Catch:{ InterruptedException -> 0x012f, TimeoutException -> 0x010a, ExecutionException -> 0x0125 }
            android.os.Bundle r0 = (android.os.Bundle) r0     // Catch:{ InterruptedException -> 0x012f, TimeoutException -> 0x010a, ExecutionException -> 0x0125 }
            android.support.v4.util.SimpleArrayMap<java.lang.String, com.google.android.gms.tasks.TaskCompletionSource<android.os.Bundle>> r2 = r9.zzcy
            monitor-enter(r2)
            android.support.v4.util.SimpleArrayMap<java.lang.String, com.google.android.gms.tasks.TaskCompletionSource<android.os.Bundle>> r3 = r9.zzcy     // Catch:{ all -> 0x0107 }
            r3.remove(r1)     // Catch:{ all -> 0x0107 }
            monitor-exit(r2)     // Catch:{ all -> 0x0107 }
            return r0
        L_0x00d6:
            java.lang.String r3 = "com.google.android.c2dm.intent.REGISTER"
            r2.setAction(r3)
            goto L_0x003e
        L_0x00dd:
            com.google.firebase.iid.zzm r4 = r9.zzdb     // Catch:{ RemoteException -> 0x00e3 }
            r4.send(r3)     // Catch:{ RemoteException -> 0x00e3 }
            goto L_0x00be
        L_0x00e3:
            r3 = move-exception
            java.lang.String r3 = "FirebaseInstanceId"
            boolean r3 = android.util.Log.isLoggable(r3, r8)
            if (r3 == 0) goto L_0x00f3
            java.lang.String r3 = "FirebaseInstanceId"
            java.lang.String r4 = "Messenger failed, fallback to startService"
            android.util.Log.d(r3, r4)
        L_0x00f3:
            com.google.firebase.iid.zzan r3 = r9.zzav
            int r3 = r3.zzac()
            if (r3 != r7) goto L_0x0101
            android.content.Context r3 = r9.zzag
            r3.sendBroadcast(r2)
            goto L_0x00be
        L_0x0101:
            android.content.Context r3 = r9.zzag
            r3.startService(r2)
            goto L_0x00be
        L_0x0107:
            r0 = move-exception
            monitor-exit(r2)     // Catch:{ all -> 0x0107 }
            throw r0
        L_0x010a:
            r0 = move-exception
        L_0x010b:
            java.lang.String r0 = "FirebaseInstanceId"
            java.lang.String r2 = "No response"
            android.util.Log.w(r0, r2)     // Catch:{ all -> 0x011a }
            java.io.IOException r0 = new java.io.IOException     // Catch:{ all -> 0x011a }
            java.lang.String r2 = "TIMEOUT"
            r0.<init>(r2)     // Catch:{ all -> 0x011a }
            throw r0     // Catch:{ all -> 0x011a }
        L_0x011a:
            r0 = move-exception
            android.support.v4.util.SimpleArrayMap<java.lang.String, com.google.android.gms.tasks.TaskCompletionSource<android.os.Bundle>> r2 = r9.zzcy
            monitor-enter(r2)
            android.support.v4.util.SimpleArrayMap<java.lang.String, com.google.android.gms.tasks.TaskCompletionSource<android.os.Bundle>> r3 = r9.zzcy     // Catch:{ all -> 0x012c }
            r3.remove(r1)     // Catch:{ all -> 0x012c }
            monitor-exit(r2)     // Catch:{ all -> 0x012c }
            throw r0
        L_0x0125:
            r0 = move-exception
            java.io.IOException r2 = new java.io.IOException     // Catch:{ all -> 0x011a }
            r2.<init>(r0)     // Catch:{ all -> 0x011a }
            throw r2     // Catch:{ all -> 0x011a }
        L_0x012c:
            r0 = move-exception
            monitor-exit(r2)     // Catch:{ all -> 0x012c }
            throw r0
        L_0x012f:
            r0 = move-exception
            goto L_0x010b
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.firebase.iid.zzau.zze(android.os.Bundle):android.os.Bundle");
    }

    /* access modifiers changed from: 0000 */
    public final Bundle zzc(Bundle bundle) throws IOException {
        if (this.zzav.zzaf() < 12000000) {
            return zzd(bundle);
        }
        try {
            return (Bundle) Tasks.await(zzac.zzc(this.zzag).zzb(1, bundle));
        } catch (InterruptedException | ExecutionException e) {
            if (Log.isLoggable("FirebaseInstanceId", 3)) {
                String valueOf = String.valueOf(e);
                Log.d("FirebaseInstanceId", new StringBuilder(String.valueOf(valueOf).length() + 22).append("Error making request: ").append(valueOf).toString());
            }
            if (!(e.getCause() instanceof zzam) || ((zzam) e.getCause()).getErrorCode() != 4) {
                return null;
            }
            return zzd(bundle);
        }
    }
}
