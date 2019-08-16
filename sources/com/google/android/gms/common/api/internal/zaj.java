package com.google.android.gms.common.api.internal;

import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.util.Log;
import android.util.SparseArray;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.internal.Preconditions;
import java.io.FileDescriptor;
import java.io.PrintWriter;

public class zaj extends zal {
    private final SparseArray<zaa> zacv = new SparseArray<>();

    private final class zaa implements OnConnectionFailedListener {
        public final int zacw;
        public final GoogleApiClient zacx;
        public final OnConnectionFailedListener zacy;

        public zaa(int i, GoogleApiClient googleApiClient, OnConnectionFailedListener onConnectionFailedListener) {
            this.zacw = i;
            this.zacx = googleApiClient;
            this.zacy = onConnectionFailedListener;
            googleApiClient.registerConnectionFailedListener(this);
        }

        public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
            String valueOf = String.valueOf(connectionResult);
            Log.d("AutoManageHelper", new StringBuilder(String.valueOf(valueOf).length() + 27).append("beginFailureResolution for ").append(valueOf).toString());
            zaj.this.zab(connectionResult, this.zacw);
        }
    }

    private zaj(LifecycleFragment lifecycleFragment) {
        super(lifecycleFragment);
        this.mLifecycleFragment.addCallback("AutoManageHelper", this);
    }

    public static zaj zaa(LifecycleActivity lifecycleActivity) {
        LifecycleFragment fragment = getFragment(lifecycleActivity);
        zaj zaj = (zaj) fragment.getCallbackOrNull("AutoManageHelper", zaj.class);
        return zaj != null ? zaj : new zaj(fragment);
    }

    @Nullable
    private final zaa zab(int i) {
        if (this.zacv.size() <= i) {
            return null;
        }
        return (zaa) this.zacv.get(this.zacv.keyAt(i));
    }

    public void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
        for (int i = 0; i < this.zacv.size(); i++) {
            zaa zab = zab(i);
            if (zab != null) {
                printWriter.append(str).append("GoogleApiClient #").print(zab.zacw);
                printWriter.println(":");
                zab.zacx.dump(String.valueOf(str).concat("  "), fileDescriptor, printWriter, strArr);
            }
        }
    }

    public void onStart() {
        super.onStart();
        boolean z = this.mStarted;
        String valueOf = String.valueOf(this.zacv);
        Log.d("AutoManageHelper", new StringBuilder(String.valueOf(valueOf).length() + 14).append("onStart ").append(z).append(" ").append(valueOf).toString());
        if (this.zade.get() == null) {
            for (int i = 0; i < this.zacv.size(); i++) {
                zaa zab = zab(i);
                if (zab != null) {
                    zab.zacx.connect();
                }
            }
        }
    }

    public void onStop() {
        super.onStop();
        for (int i = 0; i < this.zacv.size(); i++) {
            zaa zab = zab(i);
            if (zab != null) {
                zab.zacx.disconnect();
            }
        }
    }

    public final void zaa(int i) {
        zaa zaa2 = (zaa) this.zacv.get(i);
        this.zacv.remove(i);
        if (zaa2 != null) {
            zaa2.zacx.unregisterConnectionFailedListener(zaa2);
            zaa2.zacx.disconnect();
        }
    }

    public final void zaa(int i, GoogleApiClient googleApiClient, OnConnectionFailedListener onConnectionFailedListener) {
        Preconditions.checkNotNull(googleApiClient, "GoogleApiClient instance cannot be null");
        Preconditions.checkState(this.zacv.indexOfKey(i) < 0, "Already managing a GoogleApiClient with id " + i);
        zam zam = (zam) this.zade.get();
        boolean z = this.mStarted;
        String valueOf = String.valueOf(zam);
        Log.d("AutoManageHelper", new StringBuilder(String.valueOf(valueOf).length() + 49).append("starting AutoManage for client ").append(i).append(" ").append(z).append(" ").append(valueOf).toString());
        this.zacv.put(i, new zaa(i, googleApiClient, onConnectionFailedListener));
        if (this.mStarted && zam == null) {
            String valueOf2 = String.valueOf(googleApiClient);
            Log.d("AutoManageHelper", new StringBuilder(String.valueOf(valueOf2).length() + 11).append("connecting ").append(valueOf2).toString());
            googleApiClient.connect();
        }
    }

    /* access modifiers changed from: protected */
    public final void zaa(ConnectionResult connectionResult, int i) {
        Log.w("AutoManageHelper", "Unresolved error while connecting client. Stopping auto-manage.");
        if (i < 0) {
            Log.wtf("AutoManageHelper", "AutoManageLifecycleHelper received onErrorResolutionFailed callback but no failing client ID is set", new Exception());
            return;
        }
        zaa zaa2 = (zaa) this.zacv.get(i);
        if (zaa2 != null) {
            zaa(i);
            OnConnectionFailedListener onConnectionFailedListener = zaa2.zacy;
            if (onConnectionFailedListener != null) {
                onConnectionFailedListener.onConnectionFailed(connectionResult);
            }
        }
    }

    /* access modifiers changed from: protected */
    public final void zao() {
        for (int i = 0; i < this.zacv.size(); i++) {
            zaa zab = zab(i);
            if (zab != null) {
                zab.zacx.connect();
            }
        }
    }
}
