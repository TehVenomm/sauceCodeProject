package com.google.android.gms.auth.api.signin.internal;

import android.content.Context;
import android.support.annotation.NonNull;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.common.util.VisibleForTesting;

public final class zzp {
    private static zzp zzbn = null;
    @VisibleForTesting
    private Storage zzbo;
    @VisibleForTesting
    private GoogleSignInAccount zzbp = this.zzbo.getSavedDefaultGoogleSignInAccount();
    @VisibleForTesting
    private GoogleSignInOptions zzbq = this.zzbo.getSavedDefaultGoogleSignInOptions();

    private zzp(Context context) {
        this.zzbo = Storage.getInstance(context);
    }

    public static zzp zzd(@NonNull Context context) {
        zzp zze;
        synchronized (zzp.class) {
            try {
                zze = zze(context.getApplicationContext());
            } finally {
                Class<zzp> cls = zzp.class;
            }
        }
        return zze;
    }

    private static zzp zze(Context context) {
        zzp zzp;
        synchronized (zzp.class) {
            try {
                if (zzbn == null) {
                    zzbn = new zzp(context);
                }
                zzp = zzbn;
            } finally {
                Class<zzp> cls = zzp.class;
            }
        }
        return zzp;
    }

    public final void clear() {
        synchronized (this) {
            this.zzbo.clear();
            this.zzbp = null;
            this.zzbq = null;
        }
    }

    public final void zzc(GoogleSignInOptions googleSignInOptions, GoogleSignInAccount googleSignInAccount) {
        synchronized (this) {
            this.zzbo.saveDefaultGoogleSignInAccount(googleSignInAccount, googleSignInOptions);
            this.zzbp = googleSignInAccount;
            this.zzbq = googleSignInOptions;
        }
    }

    public final GoogleSignInAccount zzh() {
        GoogleSignInAccount googleSignInAccount;
        synchronized (this) {
            googleSignInAccount = this.zzbp;
        }
        return googleSignInAccount;
    }

    public final GoogleSignInOptions zzi() {
        GoogleSignInOptions googleSignInOptions;
        synchronized (this) {
            googleSignInOptions = this.zzbq;
        }
        return googleSignInOptions;
    }
}
