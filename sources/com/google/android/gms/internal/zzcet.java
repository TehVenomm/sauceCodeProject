package com.google.android.gms.internal;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.DeadObjectException;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.os.RemoteException;
import android.support.annotation.MainThread;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.internal.zzf;
import com.google.android.gms.common.internal.zzg;
import com.google.android.gms.common.stats.zza;

public final class zzcet implements ServiceConnection, zzf, zzg {
    final /* synthetic */ zzceg zzivw;
    private volatile boolean zziwd;
    private volatile zzcbn zziwe;

    protected zzcet(zzceg zzceg) {
        this.zzivw = zzceg;
    }

    @MainThread
    public final void onConnected(@Nullable Bundle bundle) {
        zzbp.zzfx("MeasurementServiceConnection.onConnected");
        synchronized (this) {
            try {
                zzcbg zzcbg = (zzcbg) this.zziwe.zzajj();
                this.zziwe = null;
                this.zzivw.zzauj().zzg(new zzcew(this, zzcbg));
            } catch (DeadObjectException e) {
                this.zziwe = null;
                this.zziwd = false;
            } catch (IllegalStateException e2) {
                this.zziwe = null;
                this.zziwd = false;
            }
        }
    }

    @MainThread
    public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        zzbp.zzfx("MeasurementServiceConnection.onConnectionFailed");
        zzcbo zzayv = this.zzivw.zzikb.zzayv();
        if (zzayv != null) {
            zzayv.zzaye().zzj("Service connection failed", connectionResult);
        }
        synchronized (this) {
            this.zziwd = false;
            this.zziwe = null;
        }
        this.zzivw.zzauj().zzg(new zzcey(this));
    }

    @MainThread
    public final void onConnectionSuspended(int i) {
        zzbp.zzfx("MeasurementServiceConnection.onConnectionSuspended");
        this.zzivw.zzauk().zzayh().log("Service connection suspended");
        this.zzivw.zzauj().zzg(new zzcex(this));
    }

    @MainThread
    public final void onServiceConnected(ComponentName componentName, IBinder iBinder) {
        zzbp.zzfx("MeasurementServiceConnection.onServiceConnected");
        synchronized (this) {
            if (iBinder == null) {
                this.zziwd = false;
                this.zzivw.zzauk().zzayc().log("Service connected with null binder");
                return;
            }
            zzcbg zzcbg;
            try {
                String interfaceDescriptor = iBinder.getInterfaceDescriptor();
                if ("com.google.android.gms.measurement.internal.IMeasurementService".equals(interfaceDescriptor)) {
                    if (iBinder == null) {
                        zzcbg = null;
                    } else {
                        IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.measurement.internal.IMeasurementService");
                        zzcbg = queryLocalInterface instanceof zzcbg ? (zzcbg) queryLocalInterface : new zzcbi(iBinder);
                    }
                    try {
                        this.zzivw.zzauk().zzayi().log("Bound to IMeasurementService interface");
                    } catch (RemoteException e) {
                        this.zzivw.zzauk().zzayc().log("Service connect failed to get IMeasurementService");
                        if (zzcbg != null) {
                            this.zziwd = false;
                            try {
                                zza.zzaky();
                                this.zzivw.getContext().unbindService(this.zzivw.zzivp);
                            } catch (IllegalArgumentException e2) {
                            }
                        } else {
                            this.zzivw.zzauj().zzg(new zzceu(this, zzcbg));
                        }
                    }
                    if (zzcbg != null) {
                        this.zziwd = false;
                        zza.zzaky();
                        this.zzivw.getContext().unbindService(this.zzivw.zzivp);
                    } else {
                        this.zzivw.zzauj().zzg(new zzceu(this, zzcbg));
                    }
                }
                this.zzivw.zzauk().zzayc().zzj("Got binder with a wrong descriptor", interfaceDescriptor);
                zzcbg = null;
                if (zzcbg != null) {
                    this.zzivw.zzauj().zzg(new zzceu(this, zzcbg));
                } else {
                    this.zziwd = false;
                    zza.zzaky();
                    this.zzivw.getContext().unbindService(this.zzivw.zzivp);
                }
            } catch (RemoteException e3) {
                zzcbg = null;
                this.zzivw.zzauk().zzayc().log("Service connect failed to get IMeasurementService");
                if (zzcbg != null) {
                    this.zzivw.zzauj().zzg(new zzceu(this, zzcbg));
                } else {
                    this.zziwd = false;
                    zza.zzaky();
                    this.zzivw.getContext().unbindService(this.zzivw.zzivp);
                }
            }
        }
    }

    @MainThread
    public final void onServiceDisconnected(ComponentName componentName) {
        zzbp.zzfx("MeasurementServiceConnection.onServiceDisconnected");
        this.zzivw.zzauk().zzayh().log("Service disconnected");
        this.zzivw.zzauj().zzg(new zzcev(this, componentName));
    }

    @WorkerThread
    public final void zzazr() {
        this.zzivw.zzug();
        Context context = this.zzivw.getContext();
        synchronized (this) {
            if (this.zziwd) {
                this.zzivw.zzauk().zzayi().log("Connection attempt already in progress");
            } else if (this.zziwe != null) {
                this.zzivw.zzauk().zzayi().log("Already awaiting connection attempt");
            } else {
                this.zziwe = new zzcbn(context, Looper.getMainLooper(), this, this);
                this.zzivw.zzauk().zzayi().log("Connecting to remote service");
                this.zziwd = true;
                this.zziwe.zzajf();
            }
        }
    }

    @WorkerThread
    public final void zzk(Intent intent) {
        this.zzivw.zzug();
        Context context = this.zzivw.getContext();
        zza zzaky = zza.zzaky();
        synchronized (this) {
            if (this.zziwd) {
                this.zzivw.zzauk().zzayi().log("Connection attempt already in progress");
                return;
            }
            this.zziwd = true;
            zzaky.zza(context, intent, this.zzivw.zzivp, 129);
        }
    }
}
