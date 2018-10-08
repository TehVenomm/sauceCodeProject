package com.google.android.gms.common.internal;

import android.os.Bundle;
import android.os.Handler;
import android.os.Handler.Callback;
import android.os.Looper;
import android.os.Message;
import android.util.Log;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import java.util.ArrayList;
import java.util.concurrent.atomic.AtomicInteger;

public final class zzad implements Callback {
    private final Handler mHandler;
    private final Object mLock = new Object();
    private final zzae zzfug;
    private final ArrayList<ConnectionCallbacks> zzfuh = new ArrayList();
    private ArrayList<ConnectionCallbacks> zzfui = new ArrayList();
    private final ArrayList<OnConnectionFailedListener> zzfuj = new ArrayList();
    private volatile boolean zzfuk = false;
    private final AtomicInteger zzful = new AtomicInteger(0);
    private boolean zzfum = false;

    public zzad(Looper looper, zzae zzae) {
        this.zzfug = zzae;
        this.mHandler = new Handler(looper, this);
    }

    public final boolean handleMessage(Message message) {
        if (message.what == 1) {
            ConnectionCallbacks connectionCallbacks = (ConnectionCallbacks) message.obj;
            synchronized (this.mLock) {
                if (this.zzfuk && this.zzfug.isConnected() && this.zzfuh.contains(connectionCallbacks)) {
                    connectionCallbacks.onConnected(this.zzfug.zzaeg());
                }
            }
            return true;
        }
        Log.wtf("GmsClientEvents", "Don't know how to handle message: " + message.what, new Exception());
        return false;
    }

    public final boolean isConnectionCallbacksRegistered(ConnectionCallbacks connectionCallbacks) {
        boolean contains;
        zzbp.zzu(connectionCallbacks);
        synchronized (this.mLock) {
            contains = this.zzfuh.contains(connectionCallbacks);
        }
        return contains;
    }

    public final boolean isConnectionFailedListenerRegistered(OnConnectionFailedListener onConnectionFailedListener) {
        boolean contains;
        zzbp.zzu(onConnectionFailedListener);
        synchronized (this.mLock) {
            contains = this.zzfuj.contains(onConnectionFailedListener);
        }
        return contains;
    }

    public final void registerConnectionCallbacks(ConnectionCallbacks connectionCallbacks) {
        zzbp.zzu(connectionCallbacks);
        synchronized (this.mLock) {
            if (this.zzfuh.contains(connectionCallbacks)) {
                String valueOf = String.valueOf(connectionCallbacks);
                Log.w("GmsClientEvents", new StringBuilder(String.valueOf(valueOf).length() + 62).append("registerConnectionCallbacks(): listener ").append(valueOf).append(" is already registered").toString());
            } else {
                this.zzfuh.add(connectionCallbacks);
            }
        }
        if (this.zzfug.isConnected()) {
            this.mHandler.sendMessage(this.mHandler.obtainMessage(1, connectionCallbacks));
        }
    }

    public final void registerConnectionFailedListener(OnConnectionFailedListener onConnectionFailedListener) {
        zzbp.zzu(onConnectionFailedListener);
        synchronized (this.mLock) {
            if (this.zzfuj.contains(onConnectionFailedListener)) {
                String valueOf = String.valueOf(onConnectionFailedListener);
                Log.w("GmsClientEvents", new StringBuilder(String.valueOf(valueOf).length() + 67).append("registerConnectionFailedListener(): listener ").append(valueOf).append(" is already registered").toString());
            } else {
                this.zzfuj.add(onConnectionFailedListener);
            }
        }
    }

    public final void unregisterConnectionCallbacks(ConnectionCallbacks connectionCallbacks) {
        zzbp.zzu(connectionCallbacks);
        synchronized (this.mLock) {
            if (!this.zzfuh.remove(connectionCallbacks)) {
                String valueOf = String.valueOf(connectionCallbacks);
                Log.w("GmsClientEvents", new StringBuilder(String.valueOf(valueOf).length() + 52).append("unregisterConnectionCallbacks(): listener ").append(valueOf).append(" not found").toString());
            } else if (this.zzfum) {
                this.zzfui.add(connectionCallbacks);
            }
        }
    }

    public final void unregisterConnectionFailedListener(OnConnectionFailedListener onConnectionFailedListener) {
        zzbp.zzu(onConnectionFailedListener);
        synchronized (this.mLock) {
            if (!this.zzfuj.remove(onConnectionFailedListener)) {
                String valueOf = String.valueOf(onConnectionFailedListener);
                Log.w("GmsClientEvents", new StringBuilder(String.valueOf(valueOf).length() + 57).append("unregisterConnectionFailedListener(): listener ").append(valueOf).append(" not found").toString());
            }
        }
    }

    public final void zzake() {
        this.zzfuk = false;
        this.zzful.incrementAndGet();
    }

    public final void zzakf() {
        this.zzfuk = true;
    }

    public final void zzcd(int i) {
        int i2 = 0;
        zzbp.zza(Looper.myLooper() == this.mHandler.getLooper(), (Object) "onUnintentionalDisconnection must only be called on the Handler thread");
        this.mHandler.removeMessages(1);
        synchronized (this.mLock) {
            this.zzfum = true;
            ArrayList arrayList = new ArrayList(this.zzfuh);
            int i3 = this.zzful.get();
            arrayList = arrayList;
            int size = arrayList.size();
            while (i2 < size) {
                Object obj = arrayList.get(i2);
                i2++;
                ConnectionCallbacks connectionCallbacks = (ConnectionCallbacks) obj;
                if (this.zzfuk && this.zzful.get() == i3) {
                    if (this.zzfuh.contains(connectionCallbacks)) {
                        connectionCallbacks.onConnectionSuspended(i);
                    }
                }
            }
            this.zzfui.clear();
            this.zzfum = false;
        }
    }

    public final void zzj(Bundle bundle) {
        boolean z = true;
        int i = 0;
        zzbp.zza(Looper.myLooper() == this.mHandler.getLooper(), (Object) "onConnectionSuccess must only be called on the Handler thread");
        synchronized (this.mLock) {
            zzbp.zzbg(!this.zzfum);
            this.mHandler.removeMessages(1);
            this.zzfum = true;
            if (this.zzfui.size() != 0) {
                z = false;
            }
            zzbp.zzbg(z);
            ArrayList arrayList = new ArrayList(this.zzfuh);
            int i2 = this.zzful.get();
            arrayList = arrayList;
            int size = arrayList.size();
            while (i < size) {
                Object obj = arrayList.get(i);
                i++;
                ConnectionCallbacks connectionCallbacks = (ConnectionCallbacks) obj;
                if (this.zzfuk && this.zzfug.isConnected() && this.zzful.get() == i2) {
                    if (!this.zzfui.contains(connectionCallbacks)) {
                        connectionCallbacks.onConnected(bundle);
                    }
                }
            }
            this.zzfui.clear();
            this.zzfum = false;
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void zzk(com.google.android.gms.common.ConnectionResult r8) {
        /*
        r7 = this;
        r1 = 1;
        r2 = 0;
        r0 = android.os.Looper.myLooper();
        r3 = r7.mHandler;
        r3 = r3.getLooper();
        if (r0 != r3) goto L_0x0047;
    L_0x000e:
        r0 = r1;
    L_0x000f:
        r3 = "onConnectionFailure must only be called on the Handler thread";
        com.google.android.gms.common.internal.zzbp.zza(r0, r3);
        r0 = r7.mHandler;
        r0.removeMessages(r1);
        r3 = r7.mLock;
        monitor-enter(r3);
        r0 = new java.util.ArrayList;	 Catch:{ all -> 0x0055 }
        r1 = r7.zzfuj;	 Catch:{ all -> 0x0055 }
        r0.<init>(r1);	 Catch:{ all -> 0x0055 }
        r1 = r7.zzful;	 Catch:{ all -> 0x0055 }
        r4 = r1.get();	 Catch:{ all -> 0x0055 }
        r0 = (java.util.ArrayList) r0;	 Catch:{ all -> 0x0055 }
        r5 = r0.size();	 Catch:{ all -> 0x0055 }
    L_0x002f:
        if (r2 >= r5) goto L_0x0058;
    L_0x0031:
        r1 = r0.get(r2);	 Catch:{ all -> 0x0055 }
        r2 = r2 + 1;
        r1 = (com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener) r1;	 Catch:{ all -> 0x0055 }
        r6 = r7.zzfuk;	 Catch:{ all -> 0x0055 }
        if (r6 == 0) goto L_0x0045;
    L_0x003d:
        r6 = r7.zzful;	 Catch:{ all -> 0x0055 }
        r6 = r6.get();	 Catch:{ all -> 0x0055 }
        if (r6 == r4) goto L_0x0049;
    L_0x0045:
        monitor-exit(r3);	 Catch:{ all -> 0x0055 }
    L_0x0046:
        return;
    L_0x0047:
        r0 = r2;
        goto L_0x000f;
    L_0x0049:
        r6 = r7.zzfuj;	 Catch:{ all -> 0x0055 }
        r6 = r6.contains(r1);	 Catch:{ all -> 0x0055 }
        if (r6 == 0) goto L_0x002f;
    L_0x0051:
        r1.onConnectionFailed(r8);	 Catch:{ all -> 0x0055 }
        goto L_0x002f;
    L_0x0055:
        r0 = move-exception;
        monitor-exit(r3);	 Catch:{ all -> 0x0055 }
        throw r0;
    L_0x0058:
        monitor-exit(r3);	 Catch:{ all -> 0x0055 }
        goto L_0x0046;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.common.internal.zzad.zzk(com.google.android.gms.common.ConnectionResult):void");
    }
}
