package com.google.firebase.iid;

import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.pm.ResolveInfo;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.os.Looper;
import android.os.Message;
import android.os.Messenger;
import android.os.Parcelable;
import android.os.Process;
import android.os.RemoteException;
import android.os.SystemClock;
import android.support.v4.util.SimpleArrayMap;
import android.text.TextUtils;
import android.util.Log;
import com.appsflyer.AppsFlyerProperties;
import com.google.android.gms.common.util.zzp;
import com.google.android.gms.gcm.GoogleCloudMessaging;
import com.google.android.gms.iid.MessengerCompat;
import io.fabric.sdk.android.services.settings.SettingsJsonConstants;
import java.io.IOException;
import java.security.KeyPair;
import java.security.PrivateKey;
import java.security.Signature;
import java.security.interfaces.RSAPrivateKey;
import java.util.Random;
import jp.colopl.gcm.RegistrarHelper;
import org.apache.commons.lang3.StringUtils;

public final class zzl {
    private static PendingIntent zzhre;
    private static String zzhto = null;
    private static boolean zzhtp = false;
    private static int zzhtq = 0;
    private static int zzhtr = 0;
    private static int zzhts = 0;
    private static BroadcastReceiver zzhtt = null;
    private Context zzaie;
    private Messenger zzhri;
    private Messenger zzhtv;
    private MessengerCompat zzhtw;
    private long zzhtx;
    private long zzhty;
    private int zzhtz;
    private int zzhua;
    private long zzhub;
    private final SimpleArrayMap<String, zzp> zzmjj = new SimpleArrayMap();

    public zzl(Context context) {
        this.zzaie = context;
    }

    private static String zza(KeyPair keyPair, String... strArr) {
        String str = null;
        try {
            byte[] bytes = TextUtils.join(StringUtils.LF, strArr).getBytes("UTF-8");
            try {
                PrivateKey privateKey = keyPair.getPrivate();
                Signature instance = Signature.getInstance(privateKey instanceof RSAPrivateKey ? "SHA256withRSA" : "SHA256withECDSA");
                instance.initSign(privateKey);
                instance.update(bytes);
                str = FirebaseInstanceId.zzm(instance.sign());
            } catch (Throwable e) {
                Log.e("InstanceID/Rpc", "Unable to sign registration request", e);
            }
        } catch (Throwable e2) {
            Log.e("InstanceID/Rpc", "Unable to encode string", e2);
        }
        return str;
    }

    private static boolean zza(PackageManager packageManager) {
        for (ResolveInfo resolveInfo : packageManager.queryIntentServices(new Intent("com.google.android.c2dm.intent.REGISTER"), 0)) {
            if (zza(packageManager, resolveInfo.serviceInfo.packageName, "com.google.android.c2dm.intent.REGISTER")) {
                zzhtp = false;
                return true;
            }
        }
        return false;
    }

    private static boolean zza(PackageManager packageManager, String str, String str2) {
        if (packageManager.checkPermission("com.google.android.c2dm.permission.SEND", str) == 0) {
            return zzb(packageManager, str);
        }
        Log.w("InstanceID/Rpc", new StringBuilder((String.valueOf(str).length() + 56) + String.valueOf(str2).length()).append("Possible malicious package ").append(str).append(" declares ").append(str2).append(" without permission").toString());
        return false;
    }

    private final void zzass() {
        if (this.zzhri == null) {
            zzdg(this.zzaie);
            this.zzhri = new Messenger(new zzm(this, Looper.getMainLooper()));
        }
    }

    public static String zzast() {
        synchronized (zzl.class) {
            int i;
            try {
                int i2 = zzhts;
                i = i2 + 1;
                zzhts = i;
                String num = Integer.toString(i2);
                return num;
            } finally {
                i = zzl.class;
            }
        }
    }

    private final Intent zzb(Bundle bundle, KeyPair keyPair) throws IOException {
        String zzast = zzast();
        zzo zzo = new zzo();
        synchronized (this.zzmjj) {
            this.zzmjj.put(zzast, zzo);
        }
        long elapsedRealtime = SystemClock.elapsedRealtime();
        if (this.zzhub == 0 || elapsedRealtime > this.zzhub) {
            zzass();
            if (zzhto == null) {
                throw new IOException("MISSING_INSTANCEID_SERVICE");
            }
            this.zzhtx = SystemClock.elapsedRealtime();
            Intent intent = new Intent(zzhtp ? "com.google.iid.TOKEN_REQUEST" : "com.google.android.c2dm.intent.REGISTER");
            intent.setPackage(zzhto);
            bundle.putString("gmsv", Integer.toString(FirebaseInstanceId.zzap(this.zzaie, zzdg(this.zzaie))));
            bundle.putString("osv", Integer.toString(VERSION.SDK_INT));
            bundle.putString("app_ver", Integer.toString(FirebaseInstanceId.zzei(this.zzaie)));
            bundle.putString("app_ver_name", FirebaseInstanceId.zzde(this.zzaie));
            bundle.putString("cliv", "fiid-11200000");
            bundle.putString(AppsFlyerProperties.APP_ID, FirebaseInstanceId.zza(keyPair));
            bundle.putString("pub2", FirebaseInstanceId.zzm(keyPair.getPublic().getEncoded()));
            bundle.putString("sig", zza(keyPair, this.zzaie.getPackageName(), r0));
            intent.putExtras(bundle);
            zzd(this.zzaie, intent);
            this.zzhtx = SystemClock.elapsedRealtime();
            intent.putExtra("kid", new StringBuilder(String.valueOf(zzast).length() + 5).append("|ID|").append(zzast).append("|").toString());
            intent.putExtra("X-kid", new StringBuilder(String.valueOf(zzast).length() + 5).append("|ID|").append(zzast).append("|").toString());
            boolean equals = "com.google.android.gsf".equals(zzhto);
            if (Log.isLoggable("InstanceID/Rpc", 3)) {
                String valueOf = String.valueOf(intent.getExtras());
                Log.d("InstanceID/Rpc", new StringBuilder(String.valueOf(valueOf).length() + 8).append("Sending ").append(valueOf).toString());
            }
            if (equals) {
                synchronized (this) {
                    if (zzhtt == null) {
                        zzhtt = new zzn(this);
                        if (Log.isLoggable("InstanceID/Rpc", 3)) {
                            Log.d("InstanceID/Rpc", "Registered GSF callback receiver");
                        }
                        IntentFilter intentFilter = new IntentFilter("com.google.android.c2dm.intent.REGISTRATION");
                        intentFilter.addCategory(this.zzaie.getPackageName());
                        this.zzaie.registerReceiver(zzhtt, intentFilter, "com.google.android.c2dm.permission.SEND", null);
                    }
                }
                this.zzaie.startService(intent);
            } else {
                intent.putExtra("google.messenger", this.zzhri);
                if (!(this.zzhtv == null && this.zzhtw == null)) {
                    Message obtain = Message.obtain();
                    obtain.obj = intent;
                    try {
                        if (this.zzhtv != null) {
                            this.zzhtv.send(obtain);
                        } else {
                            this.zzhtw.send(obtain);
                        }
                    } catch (RemoteException e) {
                        if (Log.isLoggable("InstanceID/Rpc", 3)) {
                            Log.d("InstanceID/Rpc", "Messenger failed, fallback to startService");
                        }
                    }
                }
                if (zzhtp) {
                    this.zzaie.sendBroadcast(intent);
                } else {
                    this.zzaie.startService(intent);
                }
            }
            try {
                Intent zzbyo = zzo.zzbyo();
                synchronized (this.zzmjj) {
                    this.zzmjj.remove(zzast);
                }
                return zzbyo;
            } catch (Throwable th) {
                synchronized (this.zzmjj) {
                    this.zzmjj.remove(zzast);
                }
            }
        } else {
            Log.w("InstanceID/Rpc", "Backoff mode, next request attempt: " + (this.zzhub - elapsedRealtime) + " interval: " + this.zzhua);
            throw new IOException("RETRY_LATER");
        }
    }

    private final void zzb(String str, Intent intent) {
        synchronized (this.zzmjj) {
            zzp zzp = (zzp) this.zzmjj.remove(str);
            if (zzp == null) {
                String valueOf = String.valueOf(str);
                Log.w("InstanceID/Rpc", valueOf.length() != 0 ? "Missing callback for ".concat(valueOf) : new String("Missing callback for "));
                return;
            }
            zzp.zzq(intent);
        }
    }

    private static boolean zzb(PackageManager packageManager, String str) {
        try {
            ApplicationInfo applicationInfo = packageManager.getApplicationInfo(str, 0);
            zzhto = applicationInfo.packageName;
            zzhtr = applicationInfo.uid;
            return true;
        } catch (NameNotFoundException e) {
            return false;
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final void zzbk(java.lang.String r4, java.lang.String r5) {
        /*
        r3 = this;
        r2 = r3.zzmjj;
        monitor-enter(r2);
        if (r4 != 0) goto L_0x0025;
    L_0x0005:
        r0 = 0;
        r1 = r0;
    L_0x0007:
        r0 = r3.zzmjj;	 Catch:{ all -> 0x0046 }
        r0 = r0.size();	 Catch:{ all -> 0x0046 }
        if (r1 >= r0) goto L_0x001e;
    L_0x000f:
        r0 = r3.zzmjj;	 Catch:{ all -> 0x0046 }
        r0 = r0.valueAt(r1);	 Catch:{ all -> 0x0046 }
        r0 = (com.google.firebase.iid.zzp) r0;	 Catch:{ all -> 0x0046 }
        r0.onError(r5);	 Catch:{ all -> 0x0046 }
        r0 = r1 + 1;
        r1 = r0;
        goto L_0x0007;
    L_0x001e:
        r0 = r3.zzmjj;	 Catch:{ all -> 0x0046 }
        r0.clear();	 Catch:{ all -> 0x0046 }
    L_0x0023:
        monitor-exit(r2);	 Catch:{ all -> 0x0046 }
    L_0x0024:
        return;
    L_0x0025:
        r0 = r3.zzmjj;	 Catch:{ all -> 0x0046 }
        r0 = r0.remove(r4);	 Catch:{ all -> 0x0046 }
        r0 = (com.google.firebase.iid.zzp) r0;	 Catch:{ all -> 0x0046 }
        if (r0 != 0) goto L_0x0051;
    L_0x002f:
        r0 = java.lang.String.valueOf(r4);	 Catch:{ all -> 0x0046 }
        r1 = r0.length();	 Catch:{ all -> 0x0046 }
        if (r1 == 0) goto L_0x0049;
    L_0x0039:
        r1 = "Missing callback for ";
        r0 = r1.concat(r0);	 Catch:{ all -> 0x0046 }
    L_0x003f:
        r1 = "InstanceID/Rpc";
        android.util.Log.w(r1, r0);	 Catch:{ all -> 0x0046 }
        monitor-exit(r2);	 Catch:{ all -> 0x0046 }
        goto L_0x0024;
    L_0x0046:
        r0 = move-exception;
        monitor-exit(r2);	 Catch:{ all -> 0x0046 }
        throw r0;
    L_0x0049:
        r0 = new java.lang.String;	 Catch:{ all -> 0x0046 }
        r1 = "Missing callback for ";
        r0.<init>(r1);	 Catch:{ all -> 0x0046 }
        goto L_0x003f;
    L_0x0051:
        r0.onError(r5);	 Catch:{ all -> 0x0046 }
        goto L_0x0023;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.firebase.iid.zzl.zzbk(java.lang.String, java.lang.String):void");
    }

    public static void zzd(Context context, Intent intent) {
        synchronized (zzl.class) {
            try {
                if (zzhre == null) {
                    Intent intent2 = new Intent();
                    intent2.setPackage("com.google.example.invalidpackage");
                    zzhre = PendingIntent.getBroadcast(context, 0, intent2, 0);
                }
                Object obj = SettingsJsonConstants.APP_KEY;
                intent.putExtra(obj, zzhre);
            } finally {
                Class cls = zzl.class;
            }
        }
    }

    public static String zzdg(Context context) {
        if (zzhto != null) {
            return zzhto;
        }
        boolean z;
        zzhtq = Process.myUid();
        PackageManager packageManager = context.getPackageManager();
        for (ResolveInfo resolveInfo : packageManager.queryBroadcastReceivers(new Intent("com.google.iid.TOKEN_REQUEST"), 0)) {
            if (zza(packageManager, resolveInfo.activityInfo.packageName, "com.google.iid.TOKEN_REQUEST")) {
                zzhtp = true;
                z = true;
                break;
            }
        }
        z = false;
        if (z) {
            return zzhto;
        }
        if (!zzp.isAtLeastO() && zza(packageManager)) {
            return zzhto;
        }
        Log.w("InstanceID/Rpc", "Failed to resolve IID implementation package, falling back");
        if (zzb(packageManager, "com.google.android.gms")) {
            zzhtp = zzp.isAtLeastO();
            return zzhto;
        } else if (zzp.zzalj() || !zzb(packageManager, "com.google.android.gsf")) {
            Log.w("InstanceID/Rpc", "Google Play services is missing, unable to get tokens");
            return null;
        } else {
            zzhtp = false;
            return zzhto;
        }
    }

    final Intent zza(Bundle bundle, KeyPair keyPair) throws IOException {
        Intent zzb = zzb(bundle, keyPair);
        if (zzb == null || !zzb.hasExtra("google.messenger")) {
            return zzb;
        }
        zzb = zzb(bundle, keyPair);
        return (zzb == null || !zzb.hasExtra("google.messenger")) ? zzb : null;
    }

    final void zzc(Message message) {
        if (message != null) {
            if (message.obj instanceof Intent) {
                Intent intent = (Intent) message.obj;
                intent.setExtrasClassLoader(MessengerCompat.class.getClassLoader());
                if (intent.hasExtra("google.messenger")) {
                    Parcelable parcelableExtra = intent.getParcelableExtra("google.messenger");
                    if (parcelableExtra instanceof MessengerCompat) {
                        this.zzhtw = (MessengerCompat) parcelableExtra;
                    }
                    if (parcelableExtra instanceof Messenger) {
                        this.zzhtv = (Messenger) parcelableExtra;
                    }
                }
                zzi((Intent) message.obj);
                return;
            }
            Log.w("InstanceID/Rpc", "Dropping invalid message");
        }
    }

    final void zzi(Intent intent) {
        String str = null;
        if (intent == null) {
            if (Log.isLoggable("InstanceID/Rpc", 3)) {
                Log.d("InstanceID/Rpc", "Unexpected response: null");
            }
        } else if ("com.google.android.c2dm.intent.REGISTRATION".equals(intent.getAction())) {
            r0 = intent.getStringExtra(RegistrarHelper.PROPERTY_REG_ID);
            if (r0 == null) {
                r0 = intent.getStringExtra("unregistered");
            }
            String stringExtra;
            String[] split;
            if (r0 == null) {
                stringExtra = intent.getStringExtra("error");
                if (stringExtra == null) {
                    r0 = String.valueOf(intent.getExtras());
                    Log.w("InstanceID/Rpc", new StringBuilder(String.valueOf(r0).length() + 49).append("Unexpected response, no error or registration id ").append(r0).toString());
                    return;
                }
                if (Log.isLoggable("InstanceID/Rpc", 3)) {
                    r0 = String.valueOf(stringExtra);
                    Log.d("InstanceID/Rpc", r0.length() != 0 ? "Received InstanceID error ".concat(r0) : new String("Received InstanceID error "));
                }
                if (stringExtra.startsWith("|")) {
                    split = stringExtra.split("\\|");
                    if (!"ID".equals(split[1])) {
                        r0 = String.valueOf(stringExtra);
                        Log.w("InstanceID/Rpc", r0.length() != 0 ? "Unexpected structured response ".concat(r0) : new String("Unexpected structured response "));
                    }
                    if (split.length > 2) {
                        r0 = split[2];
                        str = split[3];
                        if (str.startsWith(":")) {
                            str = str.substring(1);
                        }
                    } else {
                        str = "UNKNOWN";
                        r0 = null;
                    }
                    intent.putExtra("error", str);
                } else {
                    r0 = null;
                    str = stringExtra;
                }
                zzbk(r0, str);
                long longExtra = intent.getLongExtra("Retry-After", 0);
                if (longExtra > 0) {
                    this.zzhty = SystemClock.elapsedRealtime();
                    this.zzhua = ((int) longExtra) * 1000;
                    this.zzhub = SystemClock.elapsedRealtime() + ((long) this.zzhua);
                    Log.w("InstanceID/Rpc", "Explicit request from server to backoff: " + this.zzhua);
                    return;
                } else if ((GoogleCloudMessaging.ERROR_SERVICE_NOT_AVAILABLE.equals(str) || "AUTHENTICATION_FAILED".equals(str)) && "com.google.android.gsf".equals(zzhto)) {
                    this.zzhtz++;
                    if (this.zzhtz >= 3) {
                        if (this.zzhtz == 3) {
                            this.zzhua = new Random().nextInt(1000) + 1000;
                        }
                        this.zzhua <<= 1;
                        this.zzhub = SystemClock.elapsedRealtime() + ((long) this.zzhua);
                        Log.w("InstanceID/Rpc", new StringBuilder(String.valueOf(str).length() + 31).append("Backoff due to ").append(str).append(" for ").append(this.zzhua).toString());
                        return;
                    }
                    return;
                } else {
                    return;
                }
            }
            this.zzhtx = SystemClock.elapsedRealtime();
            this.zzhub = 0;
            this.zzhtz = 0;
            this.zzhua = 0;
            if (r0.startsWith("|")) {
                split = r0.split("\\|");
                if (!"ID".equals(split[1])) {
                    r0 = String.valueOf(r0);
                    Log.w("InstanceID/Rpc", r0.length() != 0 ? "Unexpected structured response ".concat(r0) : new String("Unexpected structured response "));
                }
                stringExtra = split[2];
                if (split.length > 4) {
                    if ("SYNC".equals(split[3])) {
                        FirebaseInstanceId.zzej(this.zzaie);
                    } else if ("RST".equals(split[3])) {
                        Context context = this.zzaie;
                        zzj.zza(this.zzaie, null);
                        FirebaseInstanceId.zza(context, zzj.zzbyl());
                        intent.removeExtra(RegistrarHelper.PROPERTY_REG_ID);
                        zzb(stringExtra, intent);
                        return;
                    }
                }
                r0 = split[split.length - 1];
                if (r0.startsWith(":")) {
                    r0 = r0.substring(1);
                }
                intent.putExtra(RegistrarHelper.PROPERTY_REG_ID, r0);
                str = stringExtra;
            }
            if (str != null) {
                zzb(str, intent);
            } else if (Log.isLoggable("InstanceID/Rpc", 3)) {
                Log.d("InstanceID/Rpc", "Ignoring response without a request ID");
            }
        } else if (Log.isLoggable("InstanceID/Rpc", 3)) {
            r0 = String.valueOf(intent.getAction());
            Log.d("InstanceID/Rpc", r0.length() != 0 ? "Unexpected response ".concat(r0) : new String("Unexpected response "));
        }
    }
}
