package com.google.android.gms.measurement.internal;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.DeadObjectException;
import android.os.Looper;
import android.support.annotation.MainThread;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.internal.BaseGmsClient.BaseConnectionCallbacks;
import com.google.android.gms.common.internal.BaseGmsClient.BaseOnConnectionFailedListener;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.stats.ConnectionTracker;
import com.google.android.gms.common.util.VisibleForTesting;

@VisibleForTesting
public final class zzin implements ServiceConnection, BaseConnectionCallbacks, BaseOnConnectionFailedListener {
    final /* synthetic */ zzhv zzrd;
    /* access modifiers changed from: private */
    public volatile boolean zzrt;
    private volatile zzec zzru;

    protected zzin(zzhv zzhv) {
        this.zzrd = zzhv;
    }

    @MainThread
    public final void onConnected(@Nullable Bundle bundle) {
        Preconditions.checkMainThread("MeasurementServiceConnection.onConnected");
        synchronized (this) {
            try {
                this.zzrd.zzaa().zza((Runnable) new zzio(this, (zzdx) this.zzru.getService()));
            } catch (DeadObjectException | IllegalStateException e) {
                this.zzru = null;
                this.zzrt = false;
            }
        }
    }

    @MainThread
    public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        Preconditions.checkMainThread("MeasurementServiceConnection.onConnectionFailed");
        zzef zzhs = this.zzrd.zzj.zzhs();
        if (zzhs != null) {
            zzhs.zzgn().zza("Service connection failed", connectionResult);
        }
        synchronized (this) {
            this.zzrt = false;
            this.zzru = null;
        }
        this.zzrd.zzaa().zza((Runnable) new zziq(this));
    }

    @MainThread
    public final void onConnectionSuspended(int i) {
        Preconditions.checkMainThread("MeasurementServiceConnection.onConnectionSuspended");
        this.zzrd.zzab().zzgr().zzao("Service connection suspended");
        this.zzrd.zzaa().zza((Runnable) new zzir(this));
    }

    /* JADX WARNING: Removed duplicated region for block: B:15:0x003d  */
    /* JADX WARNING: Removed duplicated region for block: B:39:0x008e A[SYNTHETIC, Splitter:B:39:0x008e] */
    @android.support.annotation.MainThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void onServiceConnected(android.content.ComponentName r5, android.os.IBinder r6) {
        /*
            r4 = this;
            r1 = 0
            java.lang.String r0 = "MeasurementServiceConnection.onServiceConnected"
            com.google.android.gms.common.internal.Preconditions.checkMainThread(r0)
            monitor-enter(r4)
            if (r6 != 0) goto L_0x001d
            r0 = 0
            r4.zzrt = r0     // Catch:{ all -> 0x0055 }
            com.google.android.gms.measurement.internal.zzhv r0 = r4.zzrd     // Catch:{ all -> 0x0055 }
            com.google.android.gms.measurement.internal.zzef r0 = r0.zzab()     // Catch:{ all -> 0x0055 }
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgk()     // Catch:{ all -> 0x0055 }
            java.lang.String r1 = "Service connected with null binder"
            r0.zzao(r1)     // Catch:{ all -> 0x0055 }
            monitor-exit(r4)     // Catch:{ all -> 0x0055 }
        L_0x001c:
            return
        L_0x001d:
            java.lang.String r0 = r6.getInterfaceDescriptor()     // Catch:{ RemoteException -> 0x006b }
            java.lang.String r2 = "com.google.android.gms.measurement.internal.IMeasurementService"
            boolean r2 = r2.equals(r0)     // Catch:{ RemoteException -> 0x006b }
            if (r2 == 0) goto L_0x007d
            if (r6 != 0) goto L_0x0058
            r0 = r1
        L_0x002c:
            com.google.android.gms.measurement.internal.zzhv r1 = r4.zzrd     // Catch:{ RemoteException -> 0x009d }
            com.google.android.gms.measurement.internal.zzef r1 = r1.zzab()     // Catch:{ RemoteException -> 0x009d }
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgs()     // Catch:{ RemoteException -> 0x009d }
            java.lang.String r2 = "Bound to IMeasurementService interface"
            r1.zzao(r2)     // Catch:{ RemoteException -> 0x009d }
        L_0x003b:
            if (r0 != 0) goto L_0x008e
            r0 = 0
            r4.zzrt = r0     // Catch:{ all -> 0x0055 }
            com.google.android.gms.common.stats.ConnectionTracker r0 = com.google.android.gms.common.stats.ConnectionTracker.getInstance()     // Catch:{ IllegalArgumentException -> 0x009f }
            com.google.android.gms.measurement.internal.zzhv r1 = r4.zzrd     // Catch:{ IllegalArgumentException -> 0x009f }
            android.content.Context r1 = r1.getContext()     // Catch:{ IllegalArgumentException -> 0x009f }
            com.google.android.gms.measurement.internal.zzhv r2 = r4.zzrd     // Catch:{ IllegalArgumentException -> 0x009f }
            com.google.android.gms.measurement.internal.zzin r2 = r2.zzre     // Catch:{ IllegalArgumentException -> 0x009f }
            r0.unbindService(r1, r2)     // Catch:{ IllegalArgumentException -> 0x009f }
        L_0x0053:
            monitor-exit(r4)     // Catch:{ all -> 0x0055 }
            goto L_0x001c
        L_0x0055:
            r0 = move-exception
            monitor-exit(r4)     // Catch:{ all -> 0x0055 }
            throw r0
        L_0x0058:
            java.lang.String r0 = "com.google.android.gms.measurement.internal.IMeasurementService"
            android.os.IInterface r0 = r6.queryLocalInterface(r0)     // Catch:{ RemoteException -> 0x006b }
            boolean r2 = r0 instanceof com.google.android.gms.measurement.internal.zzdx     // Catch:{ RemoteException -> 0x006b }
            if (r2 == 0) goto L_0x0065
            com.google.android.gms.measurement.internal.zzdx r0 = (com.google.android.gms.measurement.internal.zzdx) r0     // Catch:{ RemoteException -> 0x006b }
            goto L_0x002c
        L_0x0065:
            com.google.android.gms.measurement.internal.zzdz r0 = new com.google.android.gms.measurement.internal.zzdz     // Catch:{ RemoteException -> 0x006b }
            r0.<init>(r6)     // Catch:{ RemoteException -> 0x006b }
            goto L_0x002c
        L_0x006b:
            r0 = move-exception
            r0 = r1
        L_0x006d:
            com.google.android.gms.measurement.internal.zzhv r1 = r4.zzrd     // Catch:{ all -> 0x0055 }
            com.google.android.gms.measurement.internal.zzef r1 = r1.zzab()     // Catch:{ all -> 0x0055 }
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgk()     // Catch:{ all -> 0x0055 }
            java.lang.String r2 = "Service connect failed to get IMeasurementService"
            r1.zzao(r2)     // Catch:{ all -> 0x0055 }
            goto L_0x003b
        L_0x007d:
            com.google.android.gms.measurement.internal.zzhv r2 = r4.zzrd     // Catch:{ RemoteException -> 0x006b }
            com.google.android.gms.measurement.internal.zzef r2 = r2.zzab()     // Catch:{ RemoteException -> 0x006b }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ RemoteException -> 0x006b }
            java.lang.String r3 = "Got binder with a wrong descriptor"
            r2.zza(r3, r0)     // Catch:{ RemoteException -> 0x006b }
            r0 = r1
            goto L_0x003b
        L_0x008e:
            com.google.android.gms.measurement.internal.zzhv r1 = r4.zzrd     // Catch:{ all -> 0x0055 }
            com.google.android.gms.measurement.internal.zzfc r1 = r1.zzaa()     // Catch:{ all -> 0x0055 }
            com.google.android.gms.measurement.internal.zzim r2 = new com.google.android.gms.measurement.internal.zzim     // Catch:{ all -> 0x0055 }
            r2.<init>(r4, r0)     // Catch:{ all -> 0x0055 }
            r1.zza(r2)     // Catch:{ all -> 0x0055 }
            goto L_0x0053
        L_0x009d:
            r1 = move-exception
            goto L_0x006d
        L_0x009f:
            r0 = move-exception
            goto L_0x0053
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzin.onServiceConnected(android.content.ComponentName, android.os.IBinder):void");
    }

    @MainThread
    public final void onServiceDisconnected(ComponentName componentName) {
        Preconditions.checkMainThread("MeasurementServiceConnection.onServiceDisconnected");
        this.zzrd.zzab().zzgr().zzao("Service disconnected");
        this.zzrd.zzaa().zza((Runnable) new zzip(this, componentName));
    }

    @WorkerThread
    public final void zzb(Intent intent) {
        this.zzrd.zzo();
        Context context = this.zzrd.getContext();
        ConnectionTracker instance = ConnectionTracker.getInstance();
        synchronized (this) {
            if (this.zzrt) {
                this.zzrd.zzab().zzgs().zzao("Connection attempt already in progress");
                return;
            }
            this.zzrd.zzab().zzgs().zzao("Using local app measurement service");
            this.zzrt = true;
            instance.bindService(context, intent, this.zzrd.zzre, 129);
        }
    }

    @WorkerThread
    public final void zziw() {
        if (this.zzru != null && (this.zzru.isConnected() || this.zzru.isConnecting())) {
            this.zzru.disconnect();
        }
        this.zzru = null;
    }

    @WorkerThread
    public final void zzix() {
        this.zzrd.zzo();
        Context context = this.zzrd.getContext();
        synchronized (this) {
            if (this.zzrt) {
                this.zzrd.zzab().zzgs().zzao("Connection attempt already in progress");
            } else if (this.zzru == null || (!this.zzru.isConnecting() && !this.zzru.isConnected())) {
                this.zzru = new zzec(context, Looper.getMainLooper(), this, this);
                this.zzrd.zzab().zzgs().zzao("Connecting to remote service");
                this.zzrt = true;
                this.zzru.checkAvailabilityAndConnect();
            } else {
                this.zzrd.zzab().zzgs().zzao("Already awaiting connection attempt");
            }
        }
    }
}
