package com.google.android.gms.common.api.internal;

import android.app.Application;
import android.app.PendingIntent;
import android.content.Context;
import android.os.Handler;
import android.os.Handler.Callback;
import android.os.HandlerThread;
import android.os.Looper;
import android.os.Message;
import android.support.annotation.NonNull;
import android.support.annotation.WorkerThread;
import android.support.v4.util.ArraySet;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.android.gms.common.api.Api.ApiOptions;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApi;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.internal.zzcpm;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.TaskCompletionSource;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.atomic.AtomicInteger;

public final class zzbp implements Callback {
    private static final Object zzaqm = new Object();
    public static final Status zzfne = new Status(4, "Sign-out occurred while this API call was in progress.");
    private static final Status zzfnf = new Status(4, "The user must be signed in to make this API call.");
    private static zzbp zzfnh;
    private final Context mContext;
    private final Handler mHandler;
    private final GoogleApiAvailability zzfhf;
    private final Map<zzh<?>, zzbr<?>> zzfke = new ConcurrentHashMap(5, 0.75f, 1);
    private long zzfmd = 120000;
    private long zzfme = 5000;
    private long zzfng = 10000;
    private int zzfni = -1;
    private final AtomicInteger zzfnj = new AtomicInteger(1);
    private final AtomicInteger zzfnk = new AtomicInteger(0);
    private zzak zzfnl = null;
    private final Set<zzh<?>> zzfnm = new ArraySet();
    private final Set<zzh<?>> zzfnn = new ArraySet();

    private zzbp(Context context, Looper looper, GoogleApiAvailability googleApiAvailability) {
        this.mContext = context;
        this.mHandler = new Handler(looper, this);
        this.zzfhf = googleApiAvailability;
        this.mHandler.sendMessage(this.mHandler.obtainMessage(6));
    }

    public static zzbp zzahn() {
        zzbp zzbp;
        synchronized (zzaqm) {
            com.google.android.gms.common.internal.zzbp.zzb(zzfnh, (Object) "Must guarantee manager is non-null before using getInstance");
            zzbp = zzfnh;
        }
        return zzbp;
    }

    public static void zzaho() {
        synchronized (zzaqm) {
            if (zzfnh != null) {
                zzbp zzbp = zzfnh;
                zzbp.zzfnk.incrementAndGet();
                zzbp.mHandler.sendMessageAtFrontOfQueue(zzbp.mHandler.obtainMessage(10));
            }
        }
    }

    @WorkerThread
    private final void zzahq() {
        for (zzh remove : this.zzfnn) {
            ((zzbr) this.zzfke.remove(remove)).signOut();
        }
        this.zzfnn.clear();
    }

    @WorkerThread
    private final void zzc(GoogleApi<?> googleApi) {
        zzh zzafj = googleApi.zzafj();
        zzbr zzbr = (zzbr) this.zzfke.get(zzafj);
        if (zzbr == null) {
            zzbr = new zzbr(this, googleApi);
            this.zzfke.put(zzafj, zzbr);
        }
        if (zzbr.zzaaa()) {
            this.zzfnn.add(zzafj);
        }
        zzbr.connect();
    }

    public static zzbp zzcb(Context context) {
        zzbp zzbp;
        synchronized (zzaqm) {
            if (zzfnh == null) {
                HandlerThread handlerThread = new HandlerThread("GoogleApiHandler", 9);
                handlerThread.start();
                zzfnh = new zzbp(context.getApplicationContext(), handlerThread.getLooper(), GoogleApiAvailability.getInstance());
            }
            zzbp = zzfnh;
        }
        return zzbp;
    }

    @WorkerThread
    public final boolean handleMessage(Message message) {
        zzbr zzbr;
        switch (message.what) {
            case 1:
                this.zzfng = ((Boolean) message.obj).booleanValue() ? 10000 : 300000;
                this.mHandler.removeMessages(12);
                for (zzh obtainMessage : this.zzfke.keySet()) {
                    this.mHandler.sendMessageDelayed(this.mHandler.obtainMessage(12, obtainMessage), this.zzfng);
                }
                break;
            case 2:
                zzj zzj = (zzj) message.obj;
                for (zzh zzh : zzj.zzafw()) {
                    zzbr zzbr2 = (zzbr) this.zzfke.get(zzh);
                    if (zzbr2 == null) {
                        zzj.zza(zzh, new ConnectionResult(13));
                        break;
                    } else if (zzbr2.isConnected()) {
                        zzj.zza(zzh, ConnectionResult.zzfez);
                    } else if (zzbr2.zzahx() != null) {
                        zzj.zza(zzh, zzbr2.zzahx());
                    } else {
                        zzbr2.zza(zzj);
                    }
                }
                break;
            case 3:
                for (zzbr zzbr3 : this.zzfke.values()) {
                    zzbr3.zzahw();
                    zzbr3.connect();
                }
                break;
            case 4:
            case 8:
            case 13:
                zzcq zzcq = (zzcq) message.obj;
                zzbr = (zzbr) this.zzfke.get(zzcq.zzfov.zzafj());
                if (zzbr == null) {
                    zzc(zzcq.zzfov);
                    zzbr = (zzbr) this.zzfke.get(zzcq.zzfov.zzafj());
                }
                if (zzbr.zzaaa() && this.zzfnk.get() != zzcq.zzfou) {
                    zzcq.zzfot.zzq(zzfne);
                    zzbr.signOut();
                    break;
                }
                zzbr.zza(zzcq.zzfot);
                break;
                break;
            case 5:
                String errorString;
                String errorMessage;
                int i = message.arg1;
                ConnectionResult connectionResult = (ConnectionResult) message.obj;
                for (zzbr zzbr4 : this.zzfke.values()) {
                    if (zzbr4.getInstanceId() == i) {
                        if (zzbr4 != null) {
                            Log.wtf("GoogleApiManager", "Could not find API instance " + i + " while trying to fail enqueued calls.", new Exception());
                            break;
                        }
                        errorString = this.zzfhf.getErrorString(connectionResult.getErrorCode());
                        errorMessage = connectionResult.getErrorMessage();
                        zzbr4.zzu(new Status(17, new StringBuilder((String.valueOf(errorString).length() + 69) + String.valueOf(errorMessage).length()).append("Error resolution was canceled by the user, original error message: ").append(errorString).append(": ").append(errorMessage).toString()));
                        break;
                    }
                }
                zzbr4 = null;
                if (zzbr4 != null) {
                    Log.wtf("GoogleApiManager", "Could not find API instance " + i + " while trying to fail enqueued calls.", new Exception());
                } else {
                    errorString = this.zzfhf.getErrorString(connectionResult.getErrorCode());
                    errorMessage = connectionResult.getErrorMessage();
                    zzbr4.zzu(new Status(17, new StringBuilder((String.valueOf(errorString).length() + 69) + String.valueOf(errorMessage).length()).append("Error resolution was canceled by the user, original error message: ").append(errorString).append(": ").append(errorMessage).toString()));
                }
            case 6:
                if (this.mContext.getApplicationContext() instanceof Application) {
                    zzk.zza((Application) this.mContext.getApplicationContext());
                    zzk.zzafy().zza(new zzbq(this));
                    if (!zzk.zzafy().zzbd(true)) {
                        this.zzfng = 300000;
                        break;
                    }
                }
                break;
            case 7:
                zzc((GoogleApi) message.obj);
                break;
            case 9:
                if (this.zzfke.containsKey(message.obj)) {
                    ((zzbr) this.zzfke.get(message.obj)).resume();
                    break;
                }
                break;
            case 10:
                zzahq();
                break;
            case 11:
                if (this.zzfke.containsKey(message.obj)) {
                    ((zzbr) this.zzfke.get(message.obj)).zzahg();
                    break;
                }
                break;
            case 12:
                if (this.zzfke.containsKey(message.obj)) {
                    ((zzbr) this.zzfke.get(message.obj)).zzaia();
                    break;
                }
                break;
            default:
                Log.w("GoogleApiManager", "Unknown message id: " + message.what);
                return false;
        }
        return true;
    }

    final PendingIntent zza(zzh<?> zzh, int i) {
        zzbr zzbr = (zzbr) this.zzfke.get(zzh);
        if (zzbr == null) {
            return null;
        }
        zzcpm zzaib = zzbr.zzaib();
        return zzaib == null ? null : PendingIntent.getActivity(this.mContext, i, zzaib.zzaal(), 134217728);
    }

    public final <O extends ApiOptions> Task<Void> zza(@NonNull GoogleApi<O> googleApi, @NonNull zzcl<?> zzcl) {
        TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        this.mHandler.sendMessage(this.mHandler.obtainMessage(13, new zzcq(new zzf(zzcl, taskCompletionSource), this.zzfnk.get(), googleApi)));
        return taskCompletionSource.getTask();
    }

    public final <O extends ApiOptions> Task<Void> zza(@NonNull GoogleApi<O> googleApi, @NonNull zzcr<zzb, ?> zzcr, @NonNull zzdm<zzb, ?> zzdm) {
        TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        this.mHandler.sendMessage(this.mHandler.obtainMessage(8, new zzcq(new zzd(new zzcs(zzcr, zzdm), taskCompletionSource), this.zzfnk.get(), googleApi)));
        return taskCompletionSource.getTask();
    }

    public final Task<Void> zza(Iterable<? extends GoogleApi<?>> iterable) {
        zzj zzj = new zzj(iterable);
        for (GoogleApi zzafj : iterable) {
            zzbr zzbr = (zzbr) this.zzfke.get(zzafj.zzafj());
            if (zzbr != null) {
                if (!zzbr.isConnected()) {
                }
            }
            this.mHandler.sendMessage(this.mHandler.obtainMessage(2, zzj));
            return zzj.getTask();
        }
        zzj.zzafx();
        return zzj.getTask();
    }

    public final void zza(ConnectionResult connectionResult, int i) {
        if (!zzc(connectionResult, i)) {
            this.mHandler.sendMessage(this.mHandler.obtainMessage(5, i, 0, connectionResult));
        }
    }

    public final <O extends ApiOptions, TResult> void zza(GoogleApi<O> googleApi, int i, zzdd<zzb, TResult> zzdd, TaskCompletionSource<TResult> taskCompletionSource, zzcz zzcz) {
        this.mHandler.sendMessage(this.mHandler.obtainMessage(4, new zzcq(new zze(i, zzdd, taskCompletionSource, zzcz), this.zzfnk.get(), googleApi)));
    }

    public final <O extends ApiOptions> void zza(GoogleApi<O> googleApi, int i, zzm<? extends Result, zzb> zzm) {
        this.mHandler.sendMessage(this.mHandler.obtainMessage(4, new zzcq(new zzc(i, zzm), this.zzfnk.get(), googleApi)));
    }

    public final void zza(@NonNull zzak zzak) {
        synchronized (zzaqm) {
            if (this.zzfnl != zzak) {
                this.zzfnl = zzak;
                this.zzfnm.clear();
                this.zzfnm.addAll(zzak.zzagu());
            }
        }
    }

    final void zzafo() {
        this.zzfnk.incrementAndGet();
        this.mHandler.sendMessage(this.mHandler.obtainMessage(10));
    }

    public final void zzafv() {
        this.mHandler.sendMessage(this.mHandler.obtainMessage(3));
    }

    public final int zzahp() {
        return this.zzfnj.getAndIncrement();
    }

    public final void zzb(GoogleApi<?> googleApi) {
        this.mHandler.sendMessage(this.mHandler.obtainMessage(7, googleApi));
    }

    final void zzb(@NonNull zzak zzak) {
        synchronized (zzaqm) {
            if (this.zzfnl == zzak) {
                this.zzfnl = null;
                this.zzfnm.clear();
            }
        }
    }

    final boolean zzc(ConnectionResult connectionResult, int i) {
        return this.zzfhf.zza(this.mContext, connectionResult, i);
    }
}
