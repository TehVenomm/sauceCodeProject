package com.google.firebase.iid;

import android.content.ComponentName;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.IBinder;
import android.os.Looper;
import android.os.Message;
import android.os.Messenger;
import android.os.RemoteException;
import android.util.Log;
import android.util.SparseArray;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.stats.ConnectionTracker;
import com.google.android.gms.internal.firebase_messaging.zze;
import java.util.ArrayDeque;
import java.util.Queue;
import java.util.concurrent.TimeUnit;
import javax.annotation.concurrent.GuardedBy;

final class zzae implements ServiceConnection {
    @GuardedBy("this")
    int state;
    final Messenger zzcd;
    zzah zzce;
    @GuardedBy("this")
    final Queue<zzaj<?>> zzcf;
    @GuardedBy("this")
    final SparseArray<zzaj<?>> zzcg;
    final /* synthetic */ zzac zzch;

    private zzae(zzac zzac) {
        this.zzch = zzac;
        this.state = 0;
        this.zzcd = new Messenger(new zze(Looper.getMainLooper(), new zzad(this)));
        this.zzcf = new ArrayDeque();
        this.zzcg = new SparseArray<>();
    }

    private final void zzy() {
        this.zzch.zzbz.execute(new zzaf(this));
    }

    public final void onServiceConnected(ComponentName componentName, IBinder iBinder) {
        synchronized (this) {
            if (Log.isLoggable("MessengerIpcClient", 2)) {
                Log.v("MessengerIpcClient", "Service connected");
            }
            if (iBinder == null) {
                zza(0, "Null service connection");
            } else {
                try {
                    this.zzce = new zzah(iBinder);
                    this.state = 2;
                    zzy();
                } catch (RemoteException e) {
                    zza(0, e.getMessage());
                }
            }
        }
        return;
    }

    public final void onServiceDisconnected(ComponentName componentName) {
        synchronized (this) {
            if (Log.isLoggable("MessengerIpcClient", 2)) {
                Log.v("MessengerIpcClient", "Service disconnected");
            }
            zza(2, "Service disconnected");
        }
    }

    /* access modifiers changed from: 0000 */
    public final void zza(int i) {
        synchronized (this) {
            zzaj zzaj = (zzaj) this.zzcg.get(i);
            if (zzaj != null) {
                Log.w("MessengerIpcClient", "Timing out request: " + i);
                this.zzcg.remove(i);
                zzaj.zza(new zzam(3, "Timed out waiting for response"));
                zzz();
            }
        }
    }

    /* access modifiers changed from: 0000 */
    public final void zza(int i, String str) {
        synchronized (this) {
            if (Log.isLoggable("MessengerIpcClient", 3)) {
                String valueOf = String.valueOf(str);
                Log.d("MessengerIpcClient", valueOf.length() != 0 ? "Disconnected: ".concat(valueOf) : new String("Disconnected: "));
            }
            switch (this.state) {
                case 0:
                    throw new IllegalStateException();
                case 1:
                case 2:
                    if (Log.isLoggable("MessengerIpcClient", 2)) {
                        Log.v("MessengerIpcClient", "Unbinding service");
                    }
                    this.state = 4;
                    ConnectionTracker.getInstance().unbindService(this.zzch.zzag, this);
                    zzam zzam = new zzam(i, str);
                    for (zzaj zza : this.zzcf) {
                        zza.zza(zzam);
                    }
                    this.zzcf.clear();
                    for (int i2 = 0; i2 < this.zzcg.size(); i2++) {
                        ((zzaj) this.zzcg.valueAt(i2)).zza(zzam);
                    }
                    this.zzcg.clear();
                    break;
                case 3:
                    this.state = 4;
                    break;
                case 4:
                    break;
                default:
                    throw new IllegalStateException("Unknown state: " + this.state);
            }
        }
    }

    /* access modifiers changed from: 0000 */
    public final boolean zza(Message message) {
        int i = message.arg1;
        if (Log.isLoggable("MessengerIpcClient", 3)) {
            Log.d("MessengerIpcClient", "Received response to request: " + i);
        }
        synchronized (this) {
            zzaj zzaj = (zzaj) this.zzcg.get(i);
            if (zzaj == null) {
                Log.w("MessengerIpcClient", "Received response for unknown request: " + i);
            } else {
                this.zzcg.remove(i);
                zzz();
                Bundle data = message.getData();
                if (data.getBoolean("unsupported", false)) {
                    zzaj.zza(new zzam(4, "Not supported by GmsCore"));
                } else {
                    zzaj.zzb(data);
                }
            }
        }
        return true;
    }

    /* access modifiers changed from: 0000 */
    public final void zzaa() {
        synchronized (this) {
            if (this.state == 1) {
                zza(1, "Timed out while binding");
            }
        }
    }

    /* access modifiers changed from: 0000 */
    public final boolean zzb(zzaj zzaj) {
        boolean z = false;
        boolean z2 = true;
        synchronized (this) {
            switch (this.state) {
                case 0:
                    this.zzcf.add(zzaj);
                    if (this.state == 0) {
                        z = true;
                    }
                    Preconditions.checkState(z);
                    if (Log.isLoggable("MessengerIpcClient", 2)) {
                        Log.v("MessengerIpcClient", "Starting bind to GmsCore");
                    }
                    this.state = 1;
                    Intent intent = new Intent("com.google.android.c2dm.intent.REGISTER");
                    intent.setPackage("com.google.android.gms");
                    if (ConnectionTracker.getInstance().bindService(this.zzch.zzag, intent, this, 1)) {
                        this.zzch.zzbz.schedule(new zzag(this), 30, TimeUnit.SECONDS);
                        break;
                    } else {
                        zza(0, "Unable to bind to service");
                        break;
                    }
                case 1:
                    this.zzcf.add(zzaj);
                    break;
                case 2:
                    this.zzcf.add(zzaj);
                    zzy();
                    break;
                case 3:
                case 4:
                    z2 = false;
                    break;
                default:
                    throw new IllegalStateException("Unknown state: " + this.state);
            }
        }
        return z2;
    }

    /* access modifiers changed from: 0000 */
    public final void zzz() {
        synchronized (this) {
            if (this.state == 2 && this.zzcf.isEmpty() && this.zzcg.size() == 0) {
                if (Log.isLoggable("MessengerIpcClient", 2)) {
                    Log.v("MessengerIpcClient", "Finished handling requests, unbinding");
                }
                this.state = 3;
                ConnectionTracker.getInstance().unbindService(this.zzch.zzag, this);
            }
        }
    }
}
