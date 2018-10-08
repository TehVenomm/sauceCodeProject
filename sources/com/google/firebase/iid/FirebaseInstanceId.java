package com.google.firebase.iid;

import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.Bundle;
import android.support.annotation.Keep;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import android.support.v4.util.ArrayMap;
import android.util.Base64;
import android.util.Log;
import com.google.firebase.FirebaseApp;
import java.io.IOException;
import java.security.KeyPair;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Map;

public class FirebaseInstanceId {
    private static Map<String, FirebaseInstanceId> zzhtf = new ArrayMap();
    private static zzk zzmiw;
    private final FirebaseApp zzmix;
    private final zzj zzmiy;
    private final String zzmiz;

    private FirebaseInstanceId(FirebaseApp firebaseApp, zzj zzj) {
        String str = null;
        this.zzmix = firebaseApp;
        this.zzmiy = zzj;
        String gcmSenderId = this.zzmix.getOptions().getGcmSenderId();
        if (gcmSenderId == null) {
            gcmSenderId = this.zzmix.getOptions().getApplicationId();
            if (gcmSenderId.startsWith("1:")) {
                String[] split = gcmSenderId.split(":");
                if (split.length >= 2) {
                    gcmSenderId = split[1];
                    if (gcmSenderId.isEmpty()) {
                    }
                }
                this.zzmiz = str;
                if (this.zzmiz != null) {
                    throw new IllegalStateException("IID failing to initialize, FirebaseApp is missing project ID");
                }
                FirebaseInstanceIdService.zza(this.zzmix.getApplicationContext(), this);
                return;
            }
        }
        str = gcmSenderId;
        this.zzmiz = str;
        if (this.zzmiz != null) {
            FirebaseInstanceIdService.zza(this.zzmix.getApplicationContext(), this);
            return;
        }
        throw new IllegalStateException("IID failing to initialize, FirebaseApp is missing project ID");
    }

    public static FirebaseInstanceId getInstance() {
        return getInstance(FirebaseApp.getInstance());
    }

    @Keep
    public static FirebaseInstanceId getInstance(@NonNull FirebaseApp firebaseApp) {
        FirebaseInstanceId firebaseInstanceId;
        synchronized (FirebaseInstanceId.class) {
            try {
                firebaseInstanceId = (FirebaseInstanceId) zzhtf.get(firebaseApp.getOptions().getApplicationId());
                if (firebaseInstanceId == null) {
                    zzj zza = zzj.zza(firebaseApp.getApplicationContext(), null);
                    if (zzmiw == null) {
                        zzmiw = new zzk(zzj.zzbyl());
                    }
                    firebaseInstanceId = new FirebaseInstanceId(firebaseApp, zza);
                    zzhtf.put(firebaseApp.getOptions().getApplicationId(), firebaseInstanceId);
                }
            } catch (Throwable th) {
                Class cls = FirebaseInstanceId.class;
            }
        }
        return firebaseInstanceId;
    }

    static String zza(KeyPair keyPair) {
        try {
            byte[] digest = MessageDigest.getInstance("SHA1").digest(keyPair.getPublic().getEncoded());
            digest[0] = (byte) ((byte) ((digest[0] & 15) + 112));
            return Base64.encodeToString(digest, 0, 8, 11);
        } catch (NoSuchAlgorithmException e) {
            Log.w("FirebaseInstanceId", "Unexpected error, device missing required alghorithms");
            return null;
        }
    }

    static void zza(Context context, zzr zzr) {
        zzr.zzasu();
        Intent intent = new Intent();
        intent.putExtra("CMD", "RST");
        zzq.zzbyp().zze(context, intent);
    }

    private final void zzab(Bundle bundle) {
        bundle.putString("gmp_app_id", this.zzmix.getOptions().getApplicationId());
    }

    static int zzap(Context context, String str) {
        int i = 0;
        try {
            return context.getPackageManager().getPackageInfo(str, 0).versionCode;
        } catch (NameNotFoundException e) {
            String valueOf = String.valueOf(e);
            Log.w("FirebaseInstanceId", new StringBuilder(String.valueOf(valueOf).length() + 23).append("Failed to find package ").append(valueOf).toString());
            return i;
        }
    }

    static zzk zzbyk() {
        return zzmiw;
    }

    static String zzde(Context context) {
        try {
            return context.getPackageManager().getPackageInfo(context.getPackageName(), 0).versionName;
        } catch (NameNotFoundException e) {
            String valueOf = String.valueOf(e);
            Log.w("FirebaseInstanceId", new StringBuilder(String.valueOf(valueOf).length() + 38).append("Never happens: can't find own package ").append(valueOf).toString());
            return null;
        }
    }

    static int zzei(Context context) {
        return zzap(context, context.getPackageName());
    }

    static void zzej(Context context) {
        Intent intent = new Intent();
        intent.putExtra("CMD", "SYNC");
        zzq.zzbyp().zze(context, intent);
    }

    static String zzm(byte[] bArr) {
        return Base64.encodeToString(bArr, 11);
    }

    public void deleteInstanceId() throws IOException {
        this.zzmiy.zza("*", "*", null);
        this.zzmiy.zzasq();
    }

    @WorkerThread
    public void deleteToken(String str, String str2) throws IOException {
        Bundle bundle = new Bundle();
        zzab(bundle);
        this.zzmiy.zza(str, str2, bundle);
    }

    public long getCreationTime() {
        return this.zzmiy.getCreationTime();
    }

    public String getId() {
        return zza(this.zzmiy.zzasp());
    }

    @Nullable
    public String getToken() {
        zzs zzbyi = zzbyi();
        if (zzbyi == null || zzbyi.zzqa(zzj.zzhtl)) {
            FirebaseInstanceIdService.zzel(this.zzmix.getApplicationContext());
        }
        return zzbyi != null ? zzbyi.zzkmz : null;
    }

    @WorkerThread
    public String getToken(String str, String str2) throws IOException {
        Bundle bundle = new Bundle();
        zzab(bundle);
        return this.zzmiy.getToken(str, str2, bundle);
    }

    @Nullable
    final zzs zzbyi() {
        return zzj.zzbyl().zzo("", this.zzmiz, "*");
    }

    final String zzbyj() throws IOException {
        return getToken(this.zzmiz, "*");
    }

    public final void zzpq(String str) {
        zzmiw.zzpq(str);
        FirebaseInstanceIdService.zzel(this.zzmix.getApplicationContext());
    }

    final void zzpr(String str) throws IOException {
        zzs zzbyi = zzbyi();
        if (zzbyi == null || zzbyi.zzqa(zzj.zzhtl)) {
            throw new IOException("token not available");
        }
        Bundle bundle = new Bundle();
        String valueOf = String.valueOf("/topics/");
        String valueOf2 = String.valueOf(str);
        bundle.putString("gcm.topic", valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf));
        String str2 = zzbyi.zzkmz;
        valueOf = String.valueOf("/topics/");
        valueOf2 = String.valueOf(str);
        valueOf2 = valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf);
        zzab(bundle);
        this.zzmiy.zzb(str2, valueOf2, bundle);
    }

    final void zzps(String str) throws IOException {
        zzs zzbyi = zzbyi();
        if (zzbyi == null || zzbyi.zzqa(zzj.zzhtl)) {
            throw new IOException("token not available");
        }
        Bundle bundle = new Bundle();
        String valueOf = String.valueOf("/topics/");
        String valueOf2 = String.valueOf(str);
        bundle.putString("gcm.topic", valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf));
        zzj zzj = this.zzmiy;
        String str2 = zzbyi.zzkmz;
        String valueOf3 = String.valueOf("/topics/");
        valueOf2 = String.valueOf(str);
        zzj.zza(str2, valueOf2.length() != 0 ? valueOf3.concat(valueOf2) : new String(valueOf3), bundle);
    }
}
