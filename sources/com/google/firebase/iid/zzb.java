package com.google.firebase.iid;

import android.app.Service;
import android.content.Intent;
import android.os.Binder;
import android.os.IBinder;
import android.support.annotation.VisibleForTesting;
import android.support.v4.content.WakefulBroadcastReceiver;
import android.util.Log;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public abstract class zzb extends Service {
    private final Object mLock = new Object();
    @VisibleForTesting
    final ExecutorService zzirr = Executors.newSingleThreadExecutor();
    private Binder zzmie;
    private int zzmif;
    private int zzmig = 0;

    private final void zzm(Intent intent) {
        if (intent != null) {
            WakefulBroadcastReceiver.completeWakefulIntent(intent);
        }
        synchronized (this.mLock) {
            this.zzmig--;
            if (this.zzmig == 0) {
                stopSelfResult(this.zzmif);
            }
        }
    }

    public abstract void handleIntent(Intent intent);

    public final IBinder onBind(Intent intent) {
        IBinder iBinder;
        synchronized (this) {
            if (Log.isLoggable("EnhancedIntentService", 3)) {
                Log.d("EnhancedIntentService", "Service received bind request");
            }
            if (this.zzmie == null) {
                this.zzmie = new zzf(this);
            }
            iBinder = this.zzmie;
        }
        return iBinder;
    }

    public final int onStartCommand(Intent intent, int i, int i2) {
        synchronized (this.mLock) {
            this.zzmif = i2;
            this.zzmig++;
        }
        Intent zzn = zzn(intent);
        if (zzn == null) {
            zzm(intent);
            return 2;
        } else if (zzo(zzn)) {
            zzm(intent);
            return 2;
        } else {
            this.zzirr.execute(new zzc(this, zzn, intent));
            return 3;
        }
    }

    protected Intent zzn(Intent intent) {
        return intent;
    }

    public boolean zzo(Intent intent) {
        return false;
    }
}
