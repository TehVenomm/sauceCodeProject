package com.google.firebase.iid;

import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.util.Base64;
import android.util.Log;
import com.google.android.gms.common.util.PlatformVersion;
import com.google.firebase.FirebaseApp;
import java.security.KeyPair;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.List;
import javax.annotation.concurrent.GuardedBy;

public final class zzan {
    private final Context zzag;
    @GuardedBy("this")
    private String zzcn;
    @GuardedBy("this")
    private String zzco;
    @GuardedBy("this")
    private int zzcp;
    @GuardedBy("this")
    private int zzcq = 0;

    public zzan(Context context) {
        this.zzag = context;
    }

    public static String zza(FirebaseApp firebaseApp) {
        String gcmSenderId = firebaseApp.getOptions().getGcmSenderId();
        if (gcmSenderId != null) {
            return gcmSenderId;
        }
        String applicationId = firebaseApp.getOptions().getApplicationId();
        if (!applicationId.startsWith("1:")) {
            return applicationId;
        }
        String[] split = applicationId.split(":");
        if (split.length < 2) {
            return null;
        }
        String str = split[1];
        if (str.isEmpty()) {
            return null;
        }
        return str;
    }

    public static String zza(KeyPair keyPair) {
        try {
            byte[] digest = MessageDigest.getInstance("SHA1").digest(keyPair.getPublic().getEncoded());
            digest[0] = (byte) ((byte) ((digest[0] & 15) + 112));
            return Base64.encodeToString(digest, 0, 8, 11);
        } catch (NoSuchAlgorithmException e) {
            Log.w("FirebaseInstanceId", "Unexpected error, device missing required algorithms");
            return null;
        }
    }

    private final void zzag() {
        synchronized (this) {
            PackageInfo zze = zze(this.zzag.getPackageName());
            if (zze != null) {
                this.zzcn = Integer.toString(zze.versionCode);
                this.zzco = zze.versionName;
            }
        }
    }

    private final PackageInfo zze(String str) {
        try {
            return this.zzag.getPackageManager().getPackageInfo(str, 0);
        } catch (NameNotFoundException e) {
            String valueOf = String.valueOf(e);
            Log.w("FirebaseInstanceId", new StringBuilder(String.valueOf(valueOf).length() + 23).append("Failed to find package ").append(valueOf).toString());
            return null;
        }
    }

    public final int zzac() {
        int i = 0;
        synchronized (this) {
            if (this.zzcq != 0) {
                i = this.zzcq;
            } else {
                PackageManager packageManager = this.zzag.getPackageManager();
                if (packageManager.checkPermission("com.google.android.c2dm.permission.SEND", "com.google.android.gms") == -1) {
                    Log.e("FirebaseInstanceId", "Google Play services missing or without correct permission.");
                } else {
                    if (!PlatformVersion.isAtLeastO()) {
                        Intent intent = new Intent("com.google.android.c2dm.intent.REGISTER");
                        intent.setPackage("com.google.android.gms");
                        List queryIntentServices = packageManager.queryIntentServices(intent, 0);
                        if (queryIntentServices != null && queryIntentServices.size() > 0) {
                            this.zzcq = 1;
                            i = this.zzcq;
                        }
                    }
                    Intent intent2 = new Intent("com.google.iid.TOKEN_REQUEST");
                    intent2.setPackage("com.google.android.gms");
                    List queryBroadcastReceivers = packageManager.queryBroadcastReceivers(intent2, 0);
                    if (queryBroadcastReceivers == null || queryBroadcastReceivers.size() <= 0) {
                        Log.w("FirebaseInstanceId", "Failed to resolve IID implementation package, falling back");
                        if (PlatformVersion.isAtLeastO()) {
                            this.zzcq = 2;
                        } else {
                            this.zzcq = 1;
                        }
                        i = this.zzcq;
                    } else {
                        this.zzcq = 2;
                        i = this.zzcq;
                    }
                }
            }
        }
        return i;
    }

    public final String zzad() {
        String str;
        synchronized (this) {
            if (this.zzcn == null) {
                zzag();
            }
            str = this.zzcn;
        }
        return str;
    }

    public final String zzae() {
        String str;
        synchronized (this) {
            if (this.zzco == null) {
                zzag();
            }
            str = this.zzco;
        }
        return str;
    }

    public final int zzaf() {
        int i;
        synchronized (this) {
            if (this.zzcp == 0) {
                PackageInfo zze = zze("com.google.android.gms");
                if (zze != null) {
                    this.zzcp = zze.versionCode;
                }
            }
            i = this.zzcp;
        }
        return i;
    }
}
