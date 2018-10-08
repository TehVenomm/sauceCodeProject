package com.google.android.gms.ads.identifier;

import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager.NameNotFoundException;
import android.support.annotation.Nullable;
import android.util.Log;
import com.facebook.appevents.AppEventsConstants;
import com.google.android.gms.common.GooglePlayServicesNotAvailableException;
import com.google.android.gms.common.GooglePlayServicesRepairableException;
import com.google.android.gms.common.annotation.KeepForSdkWithMembers;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.zze;
import com.google.android.gms.internal.zzfh;
import com.google.android.gms.internal.zzfi;
import java.io.IOException;
import java.lang.ref.WeakReference;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;

@KeepForSdkWithMembers
public class AdvertisingIdClient {
    private final Context mContext;
    @Nullable
    private com.google.android.gms.common.zza zzalw;
    @Nullable
    private zzfh zzalx;
    private boolean zzaly;
    private Object zzalz;
    @Nullable
    private zza zzama;
    private long zzamb;

    public static final class Info {
        private final String zzamh;
        private final boolean zzami;

        public Info(String str, boolean z) {
            this.zzamh = str;
            this.zzami = z;
        }

        public final String getId() {
            return this.zzamh;
        }

        public final boolean isLimitAdTrackingEnabled() {
            return this.zzami;
        }

        public final String toString() {
            String str = this.zzamh;
            return new StringBuilder(String.valueOf(str).length() + 7).append("{").append(str).append("}").append(this.zzami).toString();
        }
    }

    static final class zza extends Thread {
        private WeakReference<AdvertisingIdClient> zzamd;
        private long zzame;
        CountDownLatch zzamf = new CountDownLatch(1);
        boolean zzamg = false;

        public zza(AdvertisingIdClient advertisingIdClient, long j) {
            this.zzamd = new WeakReference(advertisingIdClient);
            this.zzame = j;
            start();
        }

        private final void disconnect() {
            AdvertisingIdClient advertisingIdClient = (AdvertisingIdClient) this.zzamd.get();
            if (advertisingIdClient != null) {
                advertisingIdClient.finish();
                this.zzamg = true;
            }
        }

        public final void run() {
            try {
                if (!this.zzamf.await(this.zzame, TimeUnit.MILLISECONDS)) {
                    disconnect();
                }
            } catch (InterruptedException e) {
                disconnect();
            }
        }
    }

    public AdvertisingIdClient(Context context) {
        this(context, 30000, false);
    }

    public AdvertisingIdClient(Context context, long j, boolean z) {
        this.zzalz = new Object();
        zzbp.zzu(context);
        if (z) {
            Context applicationContext = context.getApplicationContext();
            if (applicationContext != null) {
                context = applicationContext;
            }
            this.mContext = context;
        } else {
            this.mContext = context;
        }
        this.zzaly = false;
        this.zzamb = j;
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static com.google.android.gms.ads.identifier.AdvertisingIdClient.Info getAdvertisingIdInfo(android.content.Context r6) throws java.io.IOException, java.lang.IllegalStateException, com.google.android.gms.common.GooglePlayServicesNotAvailableException, com.google.android.gms.common.GooglePlayServicesRepairableException {
        /*
        r4 = 0;
        r0 = new com.google.android.gms.ads.identifier.zzd;
        r0.<init>(r6);
        r1 = "gads:ad_id_app_context:enabled";
        r1 = r0.getBoolean(r1, r4);
        r2 = "gads:ad_id_app_context:ping_ratio";
        r3 = 0;
        r2 = r0.getFloat(r2, r3);
        r3 = "gads:ad_id_use_shared_preference:enabled";
        r0 = r0.getBoolean(r3, r4);
        if (r0 == 0) goto L_0x0026;
    L_0x001b:
        r0 = com.google.android.gms.ads.identifier.zzb.zze(r6);
        r0 = r0.getInfo();
        if (r0 == 0) goto L_0x0026;
    L_0x0025:
        return r0;
    L_0x0026:
        r3 = new com.google.android.gms.ads.identifier.AdvertisingIdClient;
        r4 = -1;
        r3.<init>(r6, r4, r1);
        r0 = 0;
        r3.start(r0);	 Catch:{ Throwable -> 0x003d }
        r0 = r3.getInfo();	 Catch:{ Throwable -> 0x003d }
        r4 = 0;
        r3.zza(r0, r1, r2, r4);	 Catch:{ Throwable -> 0x003d }
        r3.finish();
        goto L_0x0025;
    L_0x003d:
        r0 = move-exception;
        r4 = 0;
        r3.zza(r4, r1, r2, r0);	 Catch:{ all -> 0x0043 }
        throw r0;	 Catch:{ all -> 0x0043 }
    L_0x0043:
        r0 = move-exception;
        r3.finish();
        throw r0;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.ads.identifier.AdvertisingIdClient.getAdvertisingIdInfo(android.content.Context):com.google.android.gms.ads.identifier.AdvertisingIdClient$Info");
    }

    public static void setShouldSkipGmsCoreVersionCheck(boolean z) {
    }

    private final void start(boolean z) throws IOException, IllegalStateException, GooglePlayServicesNotAvailableException, GooglePlayServicesRepairableException {
        zzbp.zzgg("Calling this from your main thread can lead to deadlock");
        synchronized (this) {
            if (this.zzaly) {
                finish();
            }
            this.zzalw = zzd(this.mContext);
            this.zzalx = zza(this.mContext, this.zzalw);
            this.zzaly = true;
            if (z) {
                zzbi();
            }
        }
    }

    private static zzfh zza(Context context, com.google.android.gms.common.zza zza) throws IOException {
        try {
            return zzfi.zzd(zza.zza(10000, TimeUnit.MILLISECONDS));
        } catch (InterruptedException e) {
            throw new IOException("Interrupted exception");
        } catch (Throwable th) {
            IOException iOException = new IOException(th);
        }
    }

    private final boolean zza(Info info, boolean z, float f, Throwable th) {
        if (Math.random() > ((double) f)) {
            return false;
        }
        Map hashMap = new HashMap();
        hashMap.put("app_context", z ? AppEventsConstants.EVENT_PARAM_VALUE_YES : AppEventsConstants.EVENT_PARAM_VALUE_NO);
        if (info != null) {
            hashMap.put("limit_ad_tracking", info.isLimitAdTrackingEnabled() ? AppEventsConstants.EVENT_PARAM_VALUE_YES : AppEventsConstants.EVENT_PARAM_VALUE_NO);
        }
        if (!(info == null || info.getId() == null)) {
            hashMap.put("ad_id_size", Integer.toString(info.getId().length()));
        }
        if (th != null) {
            hashMap.put("error", th.getClass().getName());
        }
        new zza(this, hashMap).start();
        return true;
    }

    private final void zzbi() {
        synchronized (this.zzalz) {
            if (this.zzama != null) {
                this.zzama.zzamf.countDown();
                try {
                    this.zzama.join();
                } catch (InterruptedException e) {
                }
            }
            if (this.zzamb > 0) {
                this.zzama = new zza(this, this.zzamb);
            }
        }
    }

    private static com.google.android.gms.common.zza zzd(Context context) throws IOException, GooglePlayServicesNotAvailableException, GooglePlayServicesRepairableException {
        try {
            context.getPackageManager().getPackageInfo("com.android.vending", 0);
            switch (zze.zzaew().isGooglePlayServicesAvailable(context)) {
                case 0:
                case 2:
                    Object zza = new com.google.android.gms.common.zza();
                    Intent intent = new Intent(AdvertisingInfoServiceStrategy.GOOGLE_PLAY_SERVICES_INTENT);
                    intent.setPackage("com.google.android.gms");
                    try {
                        if (com.google.android.gms.common.stats.zza.zzaky().zza(context, intent, zza, 1)) {
                            return zza;
                        }
                        throw new IOException("Connection failure");
                    } catch (Throwable th) {
                        IOException iOException = new IOException(th);
                    }
                default:
                    throw new IOException("Google Play services not available");
            }
        } catch (NameNotFoundException e) {
            throw new GooglePlayServicesNotAvailableException(9);
        }
    }

    protected void finalize() throws Throwable {
        finish();
        super.finalize();
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void finish() {
        /*
        r3 = this;
        r0 = "Calling this from your main thread can lead to deadlock";
        com.google.android.gms.common.internal.zzbp.zzgg(r0);
        monitor-enter(r3);
        r0 = r3.mContext;	 Catch:{ all -> 0x0029 }
        if (r0 == 0) goto L_0x000e;
    L_0x000a:
        r0 = r3.zzalw;	 Catch:{ all -> 0x0029 }
        if (r0 != 0) goto L_0x0010;
    L_0x000e:
        monitor-exit(r3);	 Catch:{ all -> 0x0029 }
    L_0x000f:
        return;
    L_0x0010:
        r0 = r3.zzaly;	 Catch:{ Throwable -> 0x002c }
        if (r0 == 0) goto L_0x001e;
    L_0x0014:
        com.google.android.gms.common.stats.zza.zzaky();	 Catch:{ Throwable -> 0x002c }
        r0 = r3.mContext;	 Catch:{ Throwable -> 0x002c }
        r1 = r3.zzalw;	 Catch:{ Throwable -> 0x002c }
        r0.unbindService(r1);	 Catch:{ Throwable -> 0x002c }
    L_0x001e:
        r0 = 0;
        r3.zzaly = r0;	 Catch:{ all -> 0x0029 }
        r0 = 0;
        r3.zzalx = r0;	 Catch:{ all -> 0x0029 }
        r0 = 0;
        r3.zzalw = r0;	 Catch:{ all -> 0x0029 }
        monitor-exit(r3);	 Catch:{ all -> 0x0029 }
        goto L_0x000f;
    L_0x0029:
        r0 = move-exception;
        monitor-exit(r3);	 Catch:{ all -> 0x0029 }
        throw r0;
    L_0x002c:
        r0 = move-exception;
        r1 = "AdvertisingIdClient";
        r2 = "AdvertisingIdClient unbindService failed.";
        android.util.Log.i(r1, r2, r0);	 Catch:{ all -> 0x0029 }
        goto L_0x001e;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.ads.identifier.AdvertisingIdClient.finish():void");
    }

    public Info getInfo() throws IOException {
        Info info;
        zzbp.zzgg("Calling this from your main thread can lead to deadlock");
        synchronized (this) {
            if (!this.zzaly) {
                synchronized (this.zzalz) {
                    if (this.zzama == null || !this.zzama.zzamg) {
                        throw new IOException("AdvertisingIdClient is not connected.");
                    }
                }
                try {
                    start(false);
                    if (!this.zzaly) {
                        throw new IOException("AdvertisingIdClient cannot reconnect.");
                    }
                } catch (Throwable e) {
                    Log.i("AdvertisingIdClient", "GMS remote exception ", e);
                    throw new IOException("Remote exception");
                } catch (Throwable e2) {
                    throw new IOException("AdvertisingIdClient cannot reconnect.", e2);
                }
            }
            zzbp.zzu(this.zzalw);
            zzbp.zzu(this.zzalx);
            info = new Info(this.zzalx.getId(), this.zzalx.zzb(true));
        }
        zzbi();
        return info;
    }

    public void start() throws IOException, IllegalStateException, GooglePlayServicesNotAvailableException, GooglePlayServicesRepairableException {
        start(true);
    }
}
