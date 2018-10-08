package com.google.android.gms.common.api.internal;

import android.os.Bundle;
import android.os.DeadObjectException;
import android.os.Looper;
import android.os.Message;
import android.os.RemoteException;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.ApiOptions;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.Api.zze;
import com.google.android.gms.common.api.GoogleApi;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.internal.zzby;
import com.google.android.gms.internal.zzcpm;
import com.google.android.gms.tasks.TaskCompletionSource;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.Map;
import java.util.Queue;
import java.util.Set;

public final class zzbr<O extends ApiOptions> implements ConnectionCallbacks, OnConnectionFailedListener, zzx {
    private final zzh<O> zzfgm;
    private final zze zzfkb;
    private boolean zzfmc;
    private /* synthetic */ zzbp zzfno;
    private final Queue<zza> zzfnp = new LinkedList();
    private final zzb zzfnq;
    private final zzah zzfnr;
    private final Set<zzj> zzfns = new HashSet();
    private final Map<zzcl<?>, zzcs> zzfnt = new HashMap();
    private final int zzfnu;
    private final zzcw zzfnv;
    private ConnectionResult zzfnw = null;

    @WorkerThread
    public zzbr(zzbp zzbp, GoogleApi<O> googleApi) {
        this.zzfno = zzbp;
        this.zzfkb = googleApi.zza(zzbp.mHandler.getLooper(), this);
        if (this.zzfkb instanceof zzby) {
            this.zzfnq = zzby.zzako();
        } else {
            this.zzfnq = this.zzfkb;
        }
        this.zzfgm = googleApi.zzafj();
        this.zzfnr = new zzah();
        this.zzfnu = googleApi.getInstanceId();
        if (this.zzfkb.zzaaa()) {
            this.zzfnv = googleApi.zza(zzbp.mContext, zzbp.mHandler);
        } else {
            this.zzfnv = null;
        }
    }

    @WorkerThread
    private final void zzaht() {
        zzahw();
        zzi(ConnectionResult.zzfez);
        zzahy();
        for (zzcs zzcs : this.zzfnt.values()) {
            try {
                zzcs.zzfhx.zzb(this.zzfnq, new TaskCompletionSource());
            } catch (DeadObjectException e) {
                onConnectionSuspended(1);
                this.zzfkb.disconnect();
            } catch (RemoteException e2) {
            }
        }
        while (this.zzfkb.isConnected() && !this.zzfnp.isEmpty()) {
            zzb((zza) this.zzfnp.remove());
        }
        zzahz();
    }

    @WorkerThread
    private final void zzahu() {
        zzahw();
        this.zzfmc = true;
        this.zzfnr.zzagt();
        this.zzfno.mHandler.sendMessageDelayed(Message.obtain(this.zzfno.mHandler, 9, this.zzfgm), this.zzfno.zzfme);
        this.zzfno.mHandler.sendMessageDelayed(Message.obtain(this.zzfno.mHandler, 11, this.zzfgm), this.zzfno.zzfmd);
        this.zzfno.zzfni = -1;
    }

    @WorkerThread
    private final void zzahy() {
        if (this.zzfmc) {
            this.zzfno.mHandler.removeMessages(11, this.zzfgm);
            this.zzfno.mHandler.removeMessages(9, this.zzfgm);
            this.zzfmc = false;
        }
    }

    private final void zzahz() {
        this.zzfno.mHandler.removeMessages(12, this.zzfgm);
        this.zzfno.mHandler.sendMessageDelayed(this.zzfno.mHandler.obtainMessage(12, this.zzfgm), this.zzfno.zzfng);
    }

    @WorkerThread
    private final void zzb(zza zza) {
        zza.zza(this.zzfnr, zzaaa());
        try {
            zza.zza(this);
        } catch (DeadObjectException e) {
            onConnectionSuspended(1);
            this.zzfkb.disconnect();
        }
    }

    @WorkerThread
    private final void zzi(ConnectionResult connectionResult) {
        for (zzj zza : this.zzfns) {
            zza.zza(this.zzfgm, connectionResult);
        }
        this.zzfns.clear();
    }

    @WorkerThread
    public final void connect() {
        zzbp.zza(this.zzfno.mHandler);
        if (!this.zzfkb.isConnected() && !this.zzfkb.isConnecting()) {
            if (this.zzfkb.zzafe() && this.zzfno.zzfni != 0) {
                this.zzfno.zzfni = this.zzfno.zzfhf.isGooglePlayServicesAvailable(this.zzfno.mContext);
                if (this.zzfno.zzfni != 0) {
                    onConnectionFailed(new ConnectionResult(this.zzfno.zzfni, null));
                    return;
                }
            }
            Object zzbv = new zzbv(this.zzfno, this.zzfkb, this.zzfgm);
            if (this.zzfkb.zzaaa()) {
                this.zzfnv.zza(zzbv);
            }
            this.zzfkb.zza(zzbv);
        }
    }

    public final int getInstanceId() {
        return this.zzfnu;
    }

    final boolean isConnected() {
        return this.zzfkb.isConnected();
    }

    public final void onConnected(@Nullable Bundle bundle) {
        if (Looper.myLooper() == this.zzfno.mHandler.getLooper()) {
            zzaht();
        } else {
            this.zzfno.mHandler.post(new zzbs(this));
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    @android.support.annotation.WorkerThread
    public final void onConnectionFailed(@android.support.annotation.NonNull com.google.android.gms.common.ConnectionResult r6) {
        /*
        r5 = this;
        r0 = r5.zzfno;
        r0 = r0.mHandler;
        com.google.android.gms.common.internal.zzbp.zza(r0);
        r0 = r5.zzfnv;
        if (r0 == 0) goto L_0x0012;
    L_0x000d:
        r0 = r5.zzfnv;
        r0.zzaim();
    L_0x0012:
        r5.zzahw();
        r0 = r5.zzfno;
        r1 = -1;
        r0.zzfni = r1;
        r5.zzi(r6);
        r0 = r6.getErrorCode();
        r1 = 4;
        if (r0 != r1) goto L_0x002d;
    L_0x0025:
        r0 = com.google.android.gms.common.api.internal.zzbp.zzfnf;
        r5.zzu(r0);
    L_0x002c:
        return;
    L_0x002d:
        r0 = r5.zzfnp;
        r0 = r0.isEmpty();
        if (r0 == 0) goto L_0x0038;
    L_0x0035:
        r5.zzfnw = r6;
        goto L_0x002c;
    L_0x0038:
        r1 = com.google.android.gms.common.api.internal.zzbp.zzaqm;
        monitor-enter(r1);
        r0 = r5.zzfno;	 Catch:{ all -> 0x0060 }
        r0 = r0.zzfnl;	 Catch:{ all -> 0x0060 }
        if (r0 == 0) goto L_0x0063;
    L_0x0045:
        r0 = r5.zzfno;	 Catch:{ all -> 0x0060 }
        r0 = r0.zzfnm;	 Catch:{ all -> 0x0060 }
        r2 = r5.zzfgm;	 Catch:{ all -> 0x0060 }
        r0 = r0.contains(r2);	 Catch:{ all -> 0x0060 }
        if (r0 == 0) goto L_0x0063;
    L_0x0053:
        r0 = r5.zzfno;	 Catch:{ all -> 0x0060 }
        r0 = r0.zzfnl;	 Catch:{ all -> 0x0060 }
        r2 = r5.zzfnu;	 Catch:{ all -> 0x0060 }
        r0.zzb(r6, r2);	 Catch:{ all -> 0x0060 }
        monitor-exit(r1);	 Catch:{ all -> 0x0060 }
        goto L_0x002c;
    L_0x0060:
        r0 = move-exception;
        monitor-exit(r1);	 Catch:{ all -> 0x0060 }
        throw r0;
    L_0x0063:
        monitor-exit(r1);	 Catch:{ all -> 0x0060 }
        r0 = r5.zzfno;
        r1 = r5.zzfnu;
        r0 = r0.zzc(r6, r1);
        if (r0 != 0) goto L_0x002c;
    L_0x006e:
        r0 = r6.getErrorCode();
        r1 = 18;
        if (r0 != r1) goto L_0x0079;
    L_0x0076:
        r0 = 1;
        r5.zzfmc = r0;
    L_0x0079:
        r0 = r5.zzfmc;
        if (r0 == 0) goto L_0x009b;
    L_0x007d:
        r0 = r5.zzfno;
        r0 = r0.mHandler;
        r1 = r5.zzfno;
        r1 = r1.mHandler;
        r2 = 9;
        r3 = r5.zzfgm;
        r1 = android.os.Message.obtain(r1, r2, r3);
        r2 = r5.zzfno;
        r2 = r2.zzfme;
        r0.sendMessageDelayed(r1, r2);
        goto L_0x002c;
    L_0x009b:
        r0 = r5.zzfgm;
        r0 = r0.zzafu();
        r1 = new com.google.android.gms.common.api.Status;
        r2 = 17;
        r3 = new java.lang.StringBuilder;
        r4 = java.lang.String.valueOf(r0);
        r4 = r4.length();
        r4 = r4 + 38;
        r3.<init>(r4);
        r4 = "API: ";
        r3 = r3.append(r4);
        r0 = r3.append(r0);
        r3 = " is not available on this device.";
        r0 = r0.append(r3);
        r0 = r0.toString();
        r1.<init>(r2, r0);
        r5.zzu(r1);
        goto L_0x002c;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.common.api.internal.zzbr.onConnectionFailed(com.google.android.gms.common.ConnectionResult):void");
    }

    public final void onConnectionSuspended(int i) {
        if (Looper.myLooper() == this.zzfno.mHandler.getLooper()) {
            zzahu();
        } else {
            this.zzfno.mHandler.post(new zzbt(this));
        }
    }

    @WorkerThread
    public final void resume() {
        zzbp.zza(this.zzfno.mHandler);
        if (this.zzfmc) {
            connect();
        }
    }

    @WorkerThread
    public final void signOut() {
        zzbp.zza(this.zzfno.mHandler);
        zzu(zzbp.zzfne);
        this.zzfnr.zzags();
        for (zzcl zzf : this.zzfnt.keySet()) {
            zza(new zzf(zzf, new TaskCompletionSource()));
        }
        zzi(new ConnectionResult(4));
        this.zzfkb.disconnect();
    }

    public final void zza(ConnectionResult connectionResult, Api<?> api, boolean z) {
        if (Looper.myLooper() == this.zzfno.mHandler.getLooper()) {
            onConnectionFailed(connectionResult);
        } else {
            this.zzfno.mHandler.post(new zzbu(this, connectionResult));
        }
    }

    @WorkerThread
    public final void zza(zza zza) {
        zzbp.zza(this.zzfno.mHandler);
        if (this.zzfkb.isConnected()) {
            zzb(zza);
            zzahz();
            return;
        }
        this.zzfnp.add(zza);
        if (this.zzfnw == null || !this.zzfnw.hasResolution()) {
            connect();
        } else {
            onConnectionFailed(this.zzfnw);
        }
    }

    @WorkerThread
    public final void zza(zzj zzj) {
        zzbp.zza(this.zzfno.mHandler);
        this.zzfns.add(zzj);
    }

    public final boolean zzaaa() {
        return this.zzfkb.zzaaa();
    }

    public final zze zzagm() {
        return this.zzfkb;
    }

    @WorkerThread
    public final void zzahg() {
        zzbp.zza(this.zzfno.mHandler);
        if (this.zzfmc) {
            zzahy();
            zzu(this.zzfno.zzfhf.isGooglePlayServicesAvailable(this.zzfno.mContext) == 18 ? new Status(8, "Connection timed out while waiting for Google Play services update to complete.") : new Status(8, "API failed to connect while resuming due to an unknown error."));
            this.zzfkb.disconnect();
        }
    }

    public final Map<zzcl<?>, zzcs> zzahv() {
        return this.zzfnt;
    }

    @WorkerThread
    public final void zzahw() {
        zzbp.zza(this.zzfno.mHandler);
        this.zzfnw = null;
    }

    @WorkerThread
    public final ConnectionResult zzahx() {
        zzbp.zza(this.zzfno.mHandler);
        return this.zzfnw;
    }

    @WorkerThread
    public final void zzaia() {
        zzbp.zza(this.zzfno.mHandler);
        if (!this.zzfkb.isConnected() || this.zzfnt.size() != 0) {
            return;
        }
        if (this.zzfnr.zzagr()) {
            zzahz();
        } else {
            this.zzfkb.disconnect();
        }
    }

    final zzcpm zzaib() {
        return this.zzfnv == null ? null : this.zzfnv.zzaib();
    }

    @WorkerThread
    public final void zzh(@NonNull ConnectionResult connectionResult) {
        zzbp.zza(this.zzfno.mHandler);
        this.zzfkb.disconnect();
        onConnectionFailed(connectionResult);
    }

    @WorkerThread
    public final void zzu(Status status) {
        zzbp.zza(this.zzfno.mHandler);
        for (zza zzq : this.zzfnp) {
            zzq.zzq(status);
        }
        this.zzfnp.clear();
    }
}
