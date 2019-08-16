package com.google.firebase.iid;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.pm.ResolveInfo;
import android.os.Build.VERSION;
import android.os.Looper;
import android.support.annotation.Keep;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import android.util.Log;
import com.google.android.gms.common.util.VisibleForTesting;
import com.google.android.gms.common.util.concurrent.NamedThreadFactory;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.Tasks;
import com.google.firebase.DataCollectionDefaultChange;
import com.google.firebase.FirebaseApp;
import com.google.firebase.events.EventHandler;
import com.google.firebase.events.Subscriber;
import com.google.firebase.platforminfo.UserAgentPublisher;
import java.io.IOException;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Executor;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledThreadPoolExecutor;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;
import javax.annotation.concurrent.GuardedBy;

public class FirebaseInstanceId {
    private static final long zzaq = TimeUnit.HOURS.toSeconds(8);
    private static zzav zzar;
    @GuardedBy("FirebaseInstanceId.class")
    @VisibleForTesting
    private static ScheduledExecutorService zzas;
    private final Executor zzat;
    /* access modifiers changed from: private */
    public final FirebaseApp zzau;
    private final zzan zzav;
    private MessagingChannel zzaw;
    private final zzaq zzax;
    private final zzaz zzay;
    @GuardedBy("this")
    private boolean zzaz;
    private final zza zzba;

    private final class zza {
        private final boolean zzbg = zzu();
        private final Subscriber zzbh;
        @Nullable
        @GuardedBy("this")
        private EventHandler<DataCollectionDefaultChange> zzbi;
        @Nullable
        @GuardedBy("this")
        private Boolean zzbj = zzt();

        zza(Subscriber subscriber) {
            this.zzbh = subscriber;
            if (this.zzbj == null && this.zzbg) {
                this.zzbi = new zzq(this);
                subscriber.subscribe(DataCollectionDefaultChange.class, this.zzbi);
            }
        }

        @Nullable
        private final Boolean zzt() {
            Context applicationContext = FirebaseInstanceId.this.zzau.getApplicationContext();
            SharedPreferences sharedPreferences = applicationContext.getSharedPreferences("com.google.firebase.messaging", 0);
            if (sharedPreferences.contains("auto_init")) {
                return Boolean.valueOf(sharedPreferences.getBoolean("auto_init", false));
            }
            try {
                PackageManager packageManager = applicationContext.getPackageManager();
                if (packageManager != null) {
                    ApplicationInfo applicationInfo = packageManager.getApplicationInfo(applicationContext.getPackageName(), 128);
                    if (!(applicationInfo == null || applicationInfo.metaData == null || !applicationInfo.metaData.containsKey("firebase_messaging_auto_init_enabled"))) {
                        return Boolean.valueOf(applicationInfo.metaData.getBoolean("firebase_messaging_auto_init_enabled"));
                    }
                }
            } catch (NameNotFoundException e) {
            }
            return null;
        }

        private final boolean zzu() {
            try {
                Class.forName("com.google.firebase.messaging.FirebaseMessaging");
                return true;
            } catch (ClassNotFoundException e) {
                Context applicationContext = FirebaseInstanceId.this.zzau.getApplicationContext();
                Intent intent = new Intent("com.google.firebase.MESSAGING_EVENT");
                intent.setPackage(applicationContext.getPackageName());
                ResolveInfo resolveService = applicationContext.getPackageManager().resolveService(intent, 0);
                return (resolveService == null || resolveService.serviceInfo == null) ? false : true;
            }
        }

        /* access modifiers changed from: 0000 */
        public final boolean isEnabled() {
            boolean z;
            synchronized (this) {
                z = this.zzbj != null ? this.zzbj.booleanValue() : this.zzbg && FirebaseInstanceId.this.zzau.isDataCollectionDefaultEnabled();
            }
            return z;
        }

        /* access modifiers changed from: 0000 */
        public final void setEnabled(boolean z) {
            synchronized (this) {
                if (this.zzbi != null) {
                    this.zzbh.unsubscribe(DataCollectionDefaultChange.class, this.zzbi);
                    this.zzbi = null;
                }
                Editor edit = FirebaseInstanceId.this.zzau.getApplicationContext().getSharedPreferences("com.google.firebase.messaging", 0).edit();
                edit.putBoolean("auto_init", z);
                edit.apply();
                if (z) {
                    FirebaseInstanceId.this.zzh();
                }
                this.zzbj = Boolean.valueOf(z);
            }
        }
    }

    FirebaseInstanceId(FirebaseApp firebaseApp, Subscriber subscriber, UserAgentPublisher userAgentPublisher) {
        this(firebaseApp, new zzan(firebaseApp.getApplicationContext()), zzh.zze(), zzh.zze(), subscriber, userAgentPublisher);
    }

    private FirebaseInstanceId(FirebaseApp firebaseApp, zzan zzan, Executor executor, Executor executor2, Subscriber subscriber, UserAgentPublisher userAgentPublisher) {
        this.zzaz = false;
        if (zzan.zza(firebaseApp) == null) {
            throw new IllegalStateException("FirebaseInstanceId failed to initialize, FirebaseApp is missing project ID");
        }
        synchronized (FirebaseInstanceId.class) {
            try {
                if (zzar == null) {
                    zzar = new zzav(firebaseApp.getApplicationContext());
                }
            } finally {
                while (true) {
                    Class<FirebaseInstanceId> cls = FirebaseInstanceId.class;
                }
            }
        }
        this.zzau = firebaseApp;
        this.zzav = zzan;
        if (this.zzaw == null) {
            MessagingChannel messagingChannel = (MessagingChannel) firebaseApp.get(MessagingChannel.class);
            if (messagingChannel == null || !messagingChannel.isAvailable()) {
                this.zzaw = new zzs(firebaseApp, zzan, executor, userAgentPublisher);
            } else {
                this.zzaw = messagingChannel;
            }
        }
        this.zzaw = this.zzaw;
        this.zzat = executor2;
        this.zzay = new zzaz(zzar);
        this.zzba = new zza(subscriber);
        this.zzax = new zzaq(executor);
        if (this.zzba.isEnabled()) {
            zzh();
        }
    }

    public static FirebaseInstanceId getInstance() {
        return getInstance(FirebaseApp.getInstance());
    }

    @Keep
    public static FirebaseInstanceId getInstance(@NonNull FirebaseApp firebaseApp) {
        return (FirebaseInstanceId) firebaseApp.get(FirebaseInstanceId.class);
    }

    private final void startSync() {
        synchronized (this) {
            if (!this.zzaz) {
                zza(0);
            }
        }
    }

    private final Task<InstanceIdResult> zza(String str, String str2) {
        return Tasks.forResult(null).continueWithTask(this.zzat, new zzo(this, str, zzd(str2)));
    }

    private final <T> T zza(Task<T> task) throws IOException {
        try {
            return Tasks.await(task, 30000, TimeUnit.MILLISECONDS);
        } catch (ExecutionException e) {
            ExecutionException executionException = e;
            Throwable cause = executionException.getCause();
            if (cause instanceof IOException) {
                if ("INSTANCE_ID_RESET".equals(cause.getMessage())) {
                    zzn();
                }
                throw ((IOException) cause);
            } else if (cause instanceof RuntimeException) {
                throw ((RuntimeException) cause);
            } else {
                throw new IOException(executionException);
            }
        } catch (InterruptedException | TimeoutException e2) {
            throw new IOException("SERVICE_NOT_AVAILABLE");
        }
    }

    static void zza(Runnable runnable, long j) {
        synchronized (FirebaseInstanceId.class) {
            try {
                if (zzas == null) {
                    zzas = new ScheduledThreadPoolExecutor(1, new NamedThreadFactory("FirebaseInstanceId"));
                }
                zzas.schedule(runnable, j, TimeUnit.SECONDS);
            } finally {
                Class<FirebaseInstanceId> cls = FirebaseInstanceId.class;
            }
        }
    }

    @Nullable
    @VisibleForTesting
    private static zzay zzb(String str, String str2) {
        return zzar.zzb("", str, str2);
    }

    private static String zzd(String str) {
        return (str.isEmpty() || str.equalsIgnoreCase("fcm") || str.equalsIgnoreCase("gcm")) ? "*" : str;
    }

    /* access modifiers changed from: private */
    public final void zzh() {
        zzay zzk = zzk();
        if (zzr() || zza(zzk) || this.zzay.zzao()) {
            startSync();
        }
    }

    private static String zzj() {
        return zzan.zza(zzar.zzg("").getKeyPair());
    }

    static boolean zzm() {
        return Log.isLoggable("FirebaseInstanceId", 3) || (VERSION.SDK_INT == 23 && Log.isLoggable("FirebaseInstanceId", 3));
    }

    @WorkerThread
    public void deleteInstanceId() throws IOException {
        if (Looper.getMainLooper() == Looper.myLooper()) {
            throw new IOException("MAIN_THREAD");
        }
        zza(this.zzaw.deleteInstanceId(zzj()));
        zzn();
    }

    @WorkerThread
    public void deleteToken(String str, String str2) throws IOException {
        if (Looper.getMainLooper() == Looper.myLooper()) {
            throw new IOException("MAIN_THREAD");
        }
        String zzd = zzd(str2);
        zza(this.zzaw.deleteToken(zzj(), zzay.zzb(zzb(str, zzd)), str, zzd));
        zzar.zzc("", str, zzd);
    }

    public long getCreationTime() {
        return zzar.zzg("").getCreationTime();
    }

    @WorkerThread
    public String getId() {
        zzh();
        return zzj();
    }

    @NonNull
    public Task<InstanceIdResult> getInstanceId() {
        return zza(zzan.zza(this.zzau), "*");
    }

    @Nullable
    @Deprecated
    public String getToken() {
        zzay zzk = zzk();
        if (this.zzaw.needsRefresh() || zza(zzk)) {
            startSync();
        }
        return zzay.zzb(zzk);
    }

    @WorkerThread
    public String getToken(String str, String str2) throws IOException {
        if (Looper.getMainLooper() != Looper.myLooper()) {
            return ((InstanceIdResult) zza(zza(str, str2))).getToken();
        }
        throw new IOException("MAIN_THREAD");
    }

    public final Task<Void> zza(String str) {
        Task<Void> zza2;
        synchronized (this) {
            zza2 = this.zzay.zza(str);
            startSync();
        }
        return zza2;
    }

    /* access modifiers changed from: 0000 */
    public final /* synthetic */ Task zza(String str, String str2, Task task) throws Exception {
        String zzj = zzj();
        zzay zzb = zzb(str, str2);
        if (!this.zzaw.needsRefresh() && !zza(zzb)) {
            return Tasks.forResult(new zzx(zzj, zzb.zzbv));
        }
        return this.zzax.zza(str, str2, new zzn(this, zzj, zzay.zzb(zzb), str, str2));
    }

    /* access modifiers changed from: 0000 */
    public final /* synthetic */ Task zza(String str, String str2, String str3, String str4) {
        return this.zzaw.getToken(str, str2, str3, str4).onSuccessTask(this.zzat, new zzp(this, str3, str4, str));
    }

    /* access modifiers changed from: 0000 */
    public final void zza(long j) {
        synchronized (this) {
            zza((Runnable) new zzax(this, this.zzav, this.zzay, Math.min(Math.max(30, j << 1), zzaq)), j);
            this.zzaz = true;
        }
    }

    /* access modifiers changed from: 0000 */
    public final void zza(boolean z) {
        synchronized (this) {
            this.zzaz = z;
        }
    }

    /* access modifiers changed from: 0000 */
    public final boolean zza(@Nullable zzay zzay2) {
        return zzay2 == null || zzay2.zzj(this.zzav.zzad());
    }

    /* access modifiers changed from: 0000 */
    public final /* synthetic */ Task zzb(String str, String str2, String str3, String str4) throws Exception {
        zzar.zza("", str, str2, str4, this.zzav.zzad());
        return Tasks.forResult(new zzx(str3, str4));
    }

    /* access modifiers changed from: 0000 */
    public final void zzb(String str) throws IOException {
        zzay zzk = zzk();
        if (zza(zzk)) {
            throw new IOException("token not available");
        }
        zza(this.zzaw.subscribeToTopic(zzj(), zzk.zzbv, str));
    }

    @VisibleForTesting
    public final void zzb(boolean z) {
        this.zzba.setEnabled(z);
    }

    /* access modifiers changed from: 0000 */
    public final void zzc(String str) throws IOException {
        zzay zzk = zzk();
        if (zza(zzk)) {
            throw new IOException("token not available");
        }
        zza(this.zzaw.unsubscribeFromTopic(zzj(), zzk.zzbv, str));
    }

    /* access modifiers changed from: 0000 */
    public final FirebaseApp zzi() {
        return this.zzau;
    }

    /* access modifiers changed from: 0000 */
    @Nullable
    public final zzay zzk() {
        return zzb(zzan.zza(this.zzau), "*");
    }

    /* access modifiers changed from: 0000 */
    public final String zzl() throws IOException {
        return getToken(zzan.zza(this.zzau), "*");
    }

    /* access modifiers changed from: 0000 */
    public final void zzn() {
        synchronized (this) {
            zzar.zzaj();
            if (this.zzba.isEnabled()) {
                startSync();
            }
        }
    }

    /* access modifiers changed from: 0000 */
    public final boolean zzo() {
        return this.zzaw.isAvailable();
    }

    /* access modifiers changed from: 0000 */
    public final void zzp() {
        zzar.zzh("");
        startSync();
    }

    @VisibleForTesting
    public final boolean zzq() {
        return this.zzba.isEnabled();
    }

    /* access modifiers changed from: 0000 */
    public final boolean zzr() {
        return this.zzaw.needsRefresh();
    }
}
