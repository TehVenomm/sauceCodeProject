package com.google.android.gms.common;

import android.content.ComponentName;
import android.content.ServiceConnection;
import android.os.IBinder;
import com.google.android.gms.common.internal.zzbp;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;

public final class zza implements ServiceConnection {
    private boolean zzfex = false;
    private final BlockingQueue<IBinder> zzfey = new LinkedBlockingQueue();

    public final void onServiceConnected(ComponentName componentName, IBinder iBinder) {
        this.zzfey.add(iBinder);
    }

    public final void onServiceDisconnected(ComponentName componentName) {
    }

    public final IBinder zza(long j, TimeUnit timeUnit) throws InterruptedException, TimeoutException {
        zzbp.zzgg("BlockingServiceConnection.getServiceWithTimeout() called on main thread");
        if (this.zzfex) {
            throw new IllegalStateException("Cannot call get on this connection more than once");
        }
        this.zzfex = true;
        IBinder iBinder = (IBinder) this.zzfey.poll(10000, timeUnit);
        if (iBinder != null) {
            return iBinder;
        }
        throw new TimeoutException("Timed out waiting for the service connection");
    }

    public final IBinder zzaev() throws InterruptedException {
        zzbp.zzgg("BlockingServiceConnection.getService() called on main thread");
        if (this.zzfex) {
            throw new IllegalStateException("Cannot call get on this connection more than once");
        }
        this.zzfex = true;
        return (IBinder) this.zzfey.take();
    }
}
