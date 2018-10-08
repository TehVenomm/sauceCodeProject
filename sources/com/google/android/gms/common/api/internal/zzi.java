package com.google.android.gms.common.api.internal;

import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.util.Log;
import android.util.SparseArray;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.internal.zzbp;
import java.io.FileDescriptor;
import java.io.PrintWriter;

public class zzi extends zzo {
    private final SparseArray<zza> zzfie = new SparseArray();

    final class zza implements OnConnectionFailedListener {
        public final int zzfif;
        public final GoogleApiClient zzfig;
        public final OnConnectionFailedListener zzfih;
        private /* synthetic */ zzi zzfii;

        public zza(zzi zzi, int i, GoogleApiClient googleApiClient, OnConnectionFailedListener onConnectionFailedListener) {
            this.zzfii = zzi;
            this.zzfif = i;
            this.zzfig = googleApiClient;
            this.zzfih = onConnectionFailedListener;
            googleApiClient.registerConnectionFailedListener(this);
        }

        public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
            String valueOf = String.valueOf(connectionResult);
            Log.d("AutoManageHelper", new StringBuilder(String.valueOf(valueOf).length() + 27).append("beginFailureResolution for ").append(valueOf).toString());
            this.zzfii.zzb(connectionResult, this.zzfif);
        }
    }

    private zzi(zzcg zzcg) {
        super(zzcg);
        this.zzfoi.zza("AutoManageHelper", (LifecycleCallback) this);
    }

    public static zzi zza(zzcf zzcf) {
        zzcg zzb = LifecycleCallback.zzb(zzcf);
        zzi zzi = (zzi) zzb.zza("AutoManageHelper", zzi.class);
        return zzi != null ? zzi : new zzi(zzb);
    }

    @Nullable
    private final zza zzbp(int i) {
        return this.zzfie.size() <= i ? null : (zza) this.zzfie.get(this.zzfie.keyAt(i));
    }

    public final void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
        for (int i = 0; i < this.zzfie.size(); i++) {
            zza zzbp = zzbp(i);
            if (zzbp != null) {
                printWriter.append(str).append("GoogleApiClient #").print(zzbp.zzfif);
                printWriter.println(":");
                zzbp.zzfig.dump(String.valueOf(str).concat("  "), fileDescriptor, printWriter, strArr);
            }
        }
    }

    public final void onStart() {
        super.onStart();
        boolean z = this.mStarted;
        String valueOf = String.valueOf(this.zzfie);
        Log.d("AutoManageHelper", new StringBuilder(String.valueOf(valueOf).length() + 14).append("onStart ").append(z).append(" ").append(valueOf).toString());
        if (this.zzfiq.get() == null) {
            for (int i = 0; i < this.zzfie.size(); i++) {
                zza zzbp = zzbp(i);
                if (zzbp != null) {
                    zzbp.zzfig.connect();
                }
            }
        }
    }

    public final void onStop() {
        super.onStop();
        for (int i = 0; i < this.zzfie.size(); i++) {
            zza zzbp = zzbp(i);
            if (zzbp != null) {
                zzbp.zzfig.disconnect();
            }
        }
    }

    public final void zza(int i, GoogleApiClient googleApiClient, OnConnectionFailedListener onConnectionFailedListener) {
        zzbp.zzb((Object) googleApiClient, (Object) "GoogleApiClient instance cannot be null");
        zzbp.zza(this.zzfie.indexOfKey(i) < 0, "Already managing a GoogleApiClient with id " + i);
        zzp zzp = (zzp) this.zzfiq.get();
        boolean z = this.mStarted;
        String valueOf = String.valueOf(zzp);
        Log.d("AutoManageHelper", new StringBuilder(String.valueOf(valueOf).length() + 49).append("starting AutoManage for client ").append(i).append(" ").append(z).append(" ").append(valueOf).toString());
        this.zzfie.put(i, new zza(this, i, googleApiClient, onConnectionFailedListener));
        if (this.mStarted && zzp == null) {
            String valueOf2 = String.valueOf(googleApiClient);
            Log.d("AutoManageHelper", new StringBuilder(String.valueOf(valueOf2).length() + 11).append("connecting ").append(valueOf2).toString());
            googleApiClient.connect();
        }
    }

    protected final void zza(ConnectionResult connectionResult, int i) {
        Log.w("AutoManageHelper", "Unresolved error while connecting client. Stopping auto-manage.");
        if (i < 0) {
            Log.wtf("AutoManageHelper", "AutoManageLifecycleHelper received onErrorResolutionFailed callback but no failing client ID is set", new Exception());
            return;
        }
        zza zza = (zza) this.zzfie.get(i);
        if (zza != null) {
            zzbo(i);
            OnConnectionFailedListener onConnectionFailedListener = zza.zzfih;
            if (onConnectionFailedListener != null) {
                onConnectionFailedListener.onConnectionFailed(connectionResult);
            }
        }
    }

    protected final void zzafv() {
        for (int i = 0; i < this.zzfie.size(); i++) {
            zza zzbp = zzbp(i);
            if (zzbp != null) {
                zzbp.zzfig.connect();
            }
        }
    }

    public final void zzbo(int i) {
        zza zza = (zza) this.zzfie.get(i);
        this.zzfie.remove(i);
        if (zza != null) {
            zza.zzfig.unregisterConnectionFailedListener(zza);
            zza.zzfig.disconnect();
        }
    }
}
