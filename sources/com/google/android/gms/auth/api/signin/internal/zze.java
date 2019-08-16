package com.google.android.gms.auth.api.signin.internal;

import android.content.Context;
import android.support.p000v4.content.AsyncTaskLoader;
import android.util.Log;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.SignInConnectionListener;
import java.util.Iterator;
import java.util.Set;
import java.util.concurrent.Semaphore;
import java.util.concurrent.TimeUnit;

public final class zze extends AsyncTaskLoader<Void> implements SignInConnectionListener {
    private Semaphore zzbg = new Semaphore(0);
    private Set<GoogleApiClient> zzbh;

    public zze(Context context, Set<GoogleApiClient> set) {
        super(context);
        this.zzbh = set;
    }

    /* access modifiers changed from: private */
    /* renamed from: zzf */
    public final Void loadInBackground() {
        int i;
        Iterator it = this.zzbh.iterator();
        int i2 = 0;
        while (true) {
            i = i2;
            if (it.hasNext()) {
                i2 = ((GoogleApiClient) it.next()).maybeSignIn(this) ? i + 1 : i;
            } else {
                try {
                    break;
                } catch (InterruptedException e) {
                    Log.i("GACSignInLoader", "Unexpected InterruptedException", e);
                    Thread.currentThread().interrupt();
                }
            }
        }
        this.zzbg.tryAcquire(i, 5, TimeUnit.SECONDS);
        return null;
    }

    public final void onComplete() {
        this.zzbg.release();
    }

    /* access modifiers changed from: protected */
    public final void onStartLoading() {
        this.zzbg.drainPermits();
        forceLoad();
    }
}
