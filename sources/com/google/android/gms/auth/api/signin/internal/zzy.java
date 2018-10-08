package com.google.android.gms.auth.api.signin.internal;

import android.content.Context;
import android.content.SharedPreferences;
import android.text.TextUtils;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.common.internal.zzbp;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;
import org.json.JSONException;

public final class zzy {
    private static final Lock zzedm = new ReentrantLock();
    private static zzy zzedn;
    private final Lock zzedo = new ReentrantLock();
    private final SharedPreferences zzedp;

    private zzy(Context context) {
        this.zzedp = context.getSharedPreferences("com.google.android.gms.signin", 0);
    }

    public static zzy zzbm(Context context) {
        zzbp.zzu(context);
        zzedm.lock();
        try {
            if (zzedn == null) {
                zzedn = new zzy(context.getApplicationContext());
            }
            zzy zzy = zzedn;
            return zzy;
        } finally {
            zzedm.unlock();
        }
    }

    private final GoogleSignInAccount zzeq(String str) {
        GoogleSignInAccount googleSignInAccount = null;
        if (!TextUtils.isEmpty(str)) {
            String zzes = zzes(zzs("googleSignInAccount", str));
            if (zzes != null) {
                try {
                    googleSignInAccount = GoogleSignInAccount.zzen(zzes);
                } catch (JSONException e) {
                }
            }
        }
        return googleSignInAccount;
    }

    private final GoogleSignInOptions zzer(String str) {
        GoogleSignInOptions googleSignInOptions = null;
        if (!TextUtils.isEmpty(str)) {
            String zzes = zzes(zzs("googleSignInOptions", str));
            if (zzes != null) {
                try {
                    googleSignInOptions = GoogleSignInOptions.zzeo(zzes);
                } catch (JSONException e) {
                }
            }
        }
        return googleSignInOptions;
    }

    private final String zzes(String str) {
        this.zzedo.lock();
        try {
            String string = this.zzedp.getString(str, null);
            return string;
        } finally {
            this.zzedo.unlock();
        }
    }

    private final void zzet(String str) {
        this.zzedo.lock();
        try {
            this.zzedp.edit().remove(str).apply();
        } finally {
            this.zzedo.unlock();
        }
    }

    private final void zzr(String str, String str2) {
        this.zzedo.lock();
        try {
            this.zzedp.edit().putString(str, str2).apply();
        } finally {
            this.zzedo.unlock();
        }
    }

    private static String zzs(String str, String str2) {
        return new StringBuilder((String.valueOf(str).length() + String.valueOf(":").length()) + String.valueOf(str2).length()).append(str).append(":").append(str2).toString();
    }

    public final void zza(GoogleSignInAccount googleSignInAccount, GoogleSignInOptions googleSignInOptions) {
        zzbp.zzu(googleSignInAccount);
        zzbp.zzu(googleSignInOptions);
        zzr("defaultGoogleSignInAccount", googleSignInAccount.zzaac());
        zzbp.zzu(googleSignInAccount);
        zzbp.zzu(googleSignInOptions);
        String zzaac = googleSignInAccount.zzaac();
        zzr(zzs("googleSignInAccount", zzaac), googleSignInAccount.zzaad());
        zzr(zzs("googleSignInOptions", zzaac), googleSignInOptions.zzaag());
    }

    public final GoogleSignInAccount zzaar() {
        return zzeq(zzes("defaultGoogleSignInAccount"));
    }

    public final GoogleSignInOptions zzaas() {
        return zzer(zzes("defaultGoogleSignInAccount"));
    }

    public final void zzaat() {
        String zzes = zzes("defaultGoogleSignInAccount");
        zzet("defaultGoogleSignInAccount");
        if (!TextUtils.isEmpty(zzes)) {
            zzet(zzs("googleSignInAccount", zzes));
            zzet(zzs("googleSignInOptions", zzes));
        }
    }
}
