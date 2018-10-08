package com.google.android.gms.drive.events;

import android.app.Service;
import android.content.Intent;
import android.os.Binder;
import android.os.Handler;
import android.os.IBinder;
import android.os.Message;
import android.os.RemoteException;
import com.google.android.gms.common.util.zzv;
import com.google.android.gms.internal.zzbjv;
import com.google.android.gms.internal.zzblg;
import com.google.android.gms.internal.zzblw;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;

public class DriveEventService extends Service implements ChangeListener, CompletionListener, zzd, zzi {
    public static final String ACTION_HANDLE_EVENT = "com.google.android.gms.drive.events.HANDLE_EVENT";
    private final String mName;
    private CountDownLatch zzgfg;
    zza zzgfh;
    boolean zzgfi;
    private int zzgfj;

    final class zza extends Handler {
        private /* synthetic */ DriveEventService zzgfl;

        zza(DriveEventService driveEventService) {
            this.zzgfl = driveEventService;
        }

        private final Message zzane() {
            return obtainMessage(2);
        }

        private final Message zzb(zzblw zzblw) {
            return obtainMessage(1, zzblw);
        }

        public final void handleMessage(Message message) {
            zzbjv.zzx("DriveEventService", "handleMessage message type:" + message.what);
            switch (message.what) {
                case 1:
                    this.zzgfl.zza((zzblw) message.obj);
                    return;
                case 2:
                    getLooper().quit();
                    return;
                default:
                    zzbjv.zzy("DriveEventService", "Unexpected message type:" + message.what);
                    return;
            }
        }
    }

    final class zzb extends zzblg {
        private /* synthetic */ DriveEventService zzgfl;

        zzb(DriveEventService driveEventService) {
            this.zzgfl = driveEventService;
        }

        public final void zzc(zzblw zzblw) throws RemoteException {
            synchronized (this.zzgfl) {
                String valueOf = String.valueOf(zzblw);
                zzbjv.zzx("DriveEventService", new StringBuilder(String.valueOf(valueOf).length() + 9).append("onEvent: ").append(valueOf).toString());
                this.zzgfl.zzand();
                if (this.zzgfl.zzgfh != null) {
                    this.zzgfl.zzgfh.sendMessage(this.zzgfl.zzgfh.zzb(zzblw));
                } else {
                    zzbjv.zzz("DriveEventService", "Receiving event before initialize is completed.");
                }
            }
        }
    }

    protected DriveEventService() {
        this("DriveEventService");
    }

    protected DriveEventService(String str) {
        this.zzgfi = false;
        this.zzgfj = -1;
        this.mName = str;
    }

    private final void zza(zzblw zzblw) {
        String str;
        String valueOf;
        DriveEvent zzann = zzblw.zzann();
        String valueOf2 = String.valueOf(zzann);
        zzbjv.zzx("DriveEventService", new StringBuilder(String.valueOf(valueOf2).length() + 20).append("handleEventMessage: ").append(valueOf2).toString());
        try {
            switch (zzann.getType()) {
                case 1:
                    onChange((ChangeEvent) zzann);
                    return;
                case 2:
                    onCompletion((CompletionEvent) zzann);
                    return;
                case 4:
                    zza((zzb) zzann);
                    return;
                case 7:
                    zzr zzr = (zzr) zzann;
                    str = this.mName;
                    valueOf2 = String.valueOf(zzr);
                    zzbjv.zzy(str, new StringBuilder(String.valueOf(valueOf2).length() + 32).append("Unhandled transfer state event: ").append(valueOf2).toString());
                    return;
                default:
                    valueOf2 = this.mName;
                    str = String.valueOf(zzann);
                    zzbjv.zzy(valueOf2, new StringBuilder(String.valueOf(str).length() + 17).append("Unhandled event: ").append(str).toString());
                    return;
            }
        } catch (Throwable e) {
            str = this.mName;
            valueOf = String.valueOf(zzann);
            zzbjv.zza(str, e, new StringBuilder(String.valueOf(valueOf).length() + 22).append("Error handling event: ").append(valueOf).toString());
        }
        str = this.mName;
        valueOf = String.valueOf(zzann);
        zzbjv.zza(str, e, new StringBuilder(String.valueOf(valueOf).length() + 22).append("Error handling event: ").append(valueOf).toString());
    }

    private final void zzand() throws SecurityException {
        int callingUid = getCallingUid();
        if (callingUid != this.zzgfj) {
            if (zzv.zzf(this, callingUid)) {
                this.zzgfj = callingUid;
                return;
            }
            throw new SecurityException("Caller is not GooglePlayServices");
        }
    }

    protected int getCallingUid() {
        return Binder.getCallingUid();
    }

    public final IBinder onBind(Intent intent) {
        IBinder asBinder;
        synchronized (this) {
            if (ACTION_HANDLE_EVENT.equals(intent.getAction())) {
                if (this.zzgfh == null && !this.zzgfi) {
                    this.zzgfi = true;
                    CountDownLatch countDownLatch = new CountDownLatch(1);
                    this.zzgfg = new CountDownLatch(1);
                    new zzh(this, countDownLatch).start();
                    try {
                        if (!countDownLatch.await(5000, TimeUnit.MILLISECONDS)) {
                            zzbjv.zzz("DriveEventService", "Failed to synchronously initialize event handler.");
                        }
                    } catch (Throwable e) {
                        throw new RuntimeException("Unable to start event handler", e);
                    }
                }
                asBinder = new zzb(this).asBinder();
            } else {
                asBinder = null;
            }
        }
        return asBinder;
    }

    public void onChange(ChangeEvent changeEvent) {
        String str = this.mName;
        String valueOf = String.valueOf(changeEvent);
        zzbjv.zzy(str, new StringBuilder(String.valueOf(valueOf).length() + 24).append("Unhandled change event: ").append(valueOf).toString());
    }

    public void onCompletion(CompletionEvent completionEvent) {
        String str = this.mName;
        String valueOf = String.valueOf(completionEvent);
        zzbjv.zzy(str, new StringBuilder(String.valueOf(valueOf).length() + 28).append("Unhandled completion event: ").append(valueOf).toString());
    }

    public void onDestroy() {
        synchronized (this) {
            zzbjv.zzx("DriveEventService", "onDestroy");
            if (this.zzgfh != null) {
                this.zzgfh.sendMessage(this.zzgfh.zzane());
                this.zzgfh = null;
                try {
                    if (!this.zzgfg.await(5000, TimeUnit.MILLISECONDS)) {
                        zzbjv.zzy("DriveEventService", "Failed to synchronously quit event handler. Will quit itself");
                    }
                } catch (InterruptedException e) {
                }
                this.zzgfg = null;
            }
            super.onDestroy();
        }
    }

    public boolean onUnbind(Intent intent) {
        return true;
    }

    public final void zza(zzb zzb) {
        String str = this.mName;
        String valueOf = String.valueOf(zzb);
        zzbjv.zzy(str, new StringBuilder(String.valueOf(valueOf).length() + 35).append("Unhandled changes available event: ").append(valueOf).toString());
    }
}
