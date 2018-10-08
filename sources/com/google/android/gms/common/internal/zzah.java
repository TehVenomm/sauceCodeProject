package com.google.android.gms.common.internal;

import android.content.ComponentName;
import android.content.Context;
import android.content.ServiceConnection;
import android.os.Handler;
import android.os.Handler.Callback;
import android.os.Message;
import android.util.Log;
import com.google.android.gms.common.stats.zza;
import java.util.HashMap;

final class zzah extends zzaf implements Callback {
    private final Context mApplicationContext;
    private final Handler mHandler;
    private final HashMap<zzag, zzai> zzfus = new HashMap();
    private final zza zzfut;
    private final long zzfuu;
    private final long zzfuv;

    zzah(Context context) {
        this.mApplicationContext = context.getApplicationContext();
        this.mHandler = new Handler(context.getMainLooper(), this);
        this.zzfut = zza.zzaky();
        this.zzfuu = 5000;
        this.zzfuv = 300000;
    }

    public final boolean handleMessage(Message message) {
        zzag zzag;
        zzai zzai;
        switch (message.what) {
            case 0:
                synchronized (this.zzfus) {
                    zzag = (zzag) message.obj;
                    zzai = (zzai) this.zzfus.get(zzag);
                    if (zzai != null && zzai.zzaki()) {
                        if (zzai.isBound()) {
                            zzai.zzgc("GmsClientSupervisor");
                        }
                        this.zzfus.remove(zzag);
                    }
                }
                return true;
            case 1:
                synchronized (this.zzfus) {
                    zzag = (zzag) message.obj;
                    zzai = (zzai) this.zzfus.get(zzag);
                    if (zzai != null && zzai.getState() == 3) {
                        String valueOf = String.valueOf(zzag);
                        Log.wtf("GmsClientSupervisor", new StringBuilder(String.valueOf(valueOf).length() + 47).append("Timeout waiting for ServiceConnection callback ").append(valueOf).toString(), new Exception());
                        ComponentName componentName = zzai.getComponentName();
                        if (componentName == null) {
                            componentName = zzag.getComponentName();
                        }
                        zzai.onServiceDisconnected(componentName == null ? new ComponentName(zzag.getPackage(), "unknown") : componentName);
                    }
                }
                return true;
            default:
                return false;
        }
    }

    protected final boolean zza(zzag zzag, ServiceConnection serviceConnection, String str) {
        boolean isBound;
        zzbp.zzb((Object) serviceConnection, (Object) "ServiceConnection must not be null");
        synchronized (this.zzfus) {
            zzai zzai = (zzai) this.zzfus.get(zzag);
            if (zzai != null) {
                this.mHandler.removeMessages(0, zzag);
                if (!zzai.zza(serviceConnection)) {
                    zzai.zza(serviceConnection, str);
                    switch (zzai.getState()) {
                        case 1:
                            serviceConnection.onServiceConnected(zzai.getComponentName(), zzai.getBinder());
                            break;
                        case 2:
                            zzai.zzgb(str);
                            break;
                        default:
                            break;
                    }
                }
                String valueOf = String.valueOf(zzag);
                throw new IllegalStateException(new StringBuilder(String.valueOf(valueOf).length() + 81).append("Trying to bind a GmsServiceConnection that was already connected before.  config=").append(valueOf).toString());
            }
            zzai = new zzai(this, zzag);
            zzai.zza(serviceConnection, str);
            zzai.zzgb(str);
            this.zzfus.put(zzag, zzai);
            isBound = zzai.isBound();
        }
        return isBound;
    }

    protected final void zzb(zzag zzag, ServiceConnection serviceConnection, String str) {
        zzbp.zzb((Object) serviceConnection, (Object) "ServiceConnection must not be null");
        synchronized (this.zzfus) {
            zzai zzai = (zzai) this.zzfus.get(zzag);
            String valueOf;
            if (zzai == null) {
                valueOf = String.valueOf(zzag);
                throw new IllegalStateException(new StringBuilder(String.valueOf(valueOf).length() + 50).append("Nonexistent connection status for service config: ").append(valueOf).toString());
            } else if (zzai.zza(serviceConnection)) {
                zzai.zzb(serviceConnection, str);
                if (zzai.zzaki()) {
                    this.mHandler.sendMessageDelayed(this.mHandler.obtainMessage(0, zzag), this.zzfuu);
                }
            } else {
                valueOf = String.valueOf(zzag);
                throw new IllegalStateException(new StringBuilder(String.valueOf(valueOf).length() + 76).append("Trying to unbind a GmsServiceConnection  that was not bound before.  config=").append(valueOf).toString());
            }
        }
    }
}
