package com.google.android.gms.common.api.internal;

import android.content.Context;
import android.os.Bundle;
import android.os.Looper;
import android.support.annotation.NonNull;
import android.support.v4.app.FragmentActivity;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.zza;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.Api.zzc;
import com.google.android.gms.common.api.Api.zze;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.GoogleApiClient.Builder;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzad;
import com.google.android.gms.common.internal.zzae;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.internal.zzbcd;
import com.google.android.gms.internal.zzcpm;
import com.google.android.gms.internal.zzcpn;
import java.io.FileDescriptor;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.io.Writer;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Queue;
import java.util.Set;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicReference;
import java.util.concurrent.locks.Lock;

public final class zzbd extends GoogleApiClient implements zzce {
    private final Context mContext;
    private final Looper zzakl;
    private final int zzfhd;
    private final GoogleApiAvailability zzfhf;
    private zza<? extends zzcpm, zzcpn> zzfhg;
    private boolean zzfhj;
    private final Lock zzfjy;
    private zzq zzfkd;
    private Map<Api<?>, Boolean> zzfkg;
    final Queue<zzm<?, ?>> zzfkm = new LinkedList();
    private final zzad zzfma;
    private zzcd zzfmb = null;
    private volatile boolean zzfmc;
    private long zzfmd = 120000;
    private long zzfme = 5000;
    private final zzbi zzfmf;
    private zzby zzfmg;
    final Map<zzc<?>, zze> zzfmh;
    Set<Scope> zzfmi = new HashSet();
    private final zzcn zzfmj = new zzcn();
    private final ArrayList<zzw> zzfmk;
    private Integer zzfml = null;
    Set<zzdf> zzfmm = null;
    final zzdi zzfmn;
    private final zzae zzfmo = new zzbe(this);

    public zzbd(Context context, Lock lock, Looper looper, zzq zzq, GoogleApiAvailability googleApiAvailability, zza<? extends zzcpm, zzcpn> zza, Map<Api<?>, Boolean> map, List<ConnectionCallbacks> list, List<OnConnectionFailedListener> list2, Map<zzc<?>, zze> map2, int i, int i2, ArrayList<zzw> arrayList, boolean z) {
        this.mContext = context;
        this.zzfjy = lock;
        this.zzfhj = false;
        this.zzfma = new zzad(looper, this.zzfmo);
        this.zzakl = looper;
        this.zzfmf = new zzbi(this, looper);
        this.zzfhf = googleApiAvailability;
        this.zzfhd = i;
        if (this.zzfhd >= 0) {
            this.zzfml = Integer.valueOf(i2);
        }
        this.zzfkg = map;
        this.zzfmh = map2;
        this.zzfmk = arrayList;
        this.zzfmn = new zzdi(this.zzfmh);
        for (ConnectionCallbacks registerConnectionCallbacks : list) {
            this.zzfma.registerConnectionCallbacks(registerConnectionCallbacks);
        }
        for (OnConnectionFailedListener registerConnectionFailedListener : list2) {
            this.zzfma.registerConnectionFailedListener(registerConnectionFailedListener);
        }
        this.zzfkd = zzq;
        this.zzfhg = zza;
    }

    private final void resume() {
        this.zzfjy.lock();
        try {
            if (this.zzfmc) {
                zzahf();
            }
            this.zzfjy.unlock();
        } catch (Throwable th) {
            this.zzfjy.unlock();
        }
    }

    public static int zza(Iterable<zze> iterable, boolean z) {
        int i = 0;
        int i2 = 0;
        for (zze zze : iterable) {
            if (zze.zzaaa()) {
                i = 1;
            }
            i2 = zze.zzaak() ? 1 : i2;
        }
        return i != 0 ? (i2 == 0 || !z) ? 1 : 2 : 3;
    }

    private final void zza(GoogleApiClient googleApiClient, zzda zzda, boolean z) {
        zzbcd.zzfwb.zzd(googleApiClient).setResultCallback(new zzbh(this, zzda, z, googleApiClient));
    }

    private final void zzahf() {
        this.zzfma.zzakf();
        this.zzfmb.connect();
    }

    private final void zzahg() {
        this.zzfjy.lock();
        try {
            if (zzahh()) {
                zzahf();
            }
            this.zzfjy.unlock();
        } catch (Throwable th) {
            this.zzfjy.unlock();
        }
    }

    private final void zzbs(int i) {
        if (this.zzfml == null) {
            this.zzfml = Integer.valueOf(i);
        } else if (this.zzfml.intValue() != i) {
            String zzbt = zzbt(i);
            String zzbt2 = zzbt(this.zzfml.intValue());
            throw new IllegalStateException(new StringBuilder((String.valueOf(zzbt).length() + 51) + String.valueOf(zzbt2).length()).append("Cannot use sign-in mode: ").append(zzbt).append(". Mode was already set to ").append(zzbt2).toString());
        }
        if (this.zzfmb == null) {
            boolean z = false;
            boolean z2 = false;
            for (zze zze : this.zzfmh.values()) {
                if (zze.zzaaa()) {
                    z = true;
                }
                z2 = zze.zzaak() ? true : z2;
            }
            switch (this.zzfml.intValue()) {
                case 1:
                    if (!z) {
                        throw new IllegalStateException("SIGN_IN_MODE_REQUIRED cannot be used on a GoogleApiClient that does not contain any authenticated APIs. Use connect() instead.");
                    } else if (z2) {
                        throw new IllegalStateException("Cannot use SIGN_IN_MODE_REQUIRED with GOOGLE_SIGN_IN_API. Use connect(SIGN_IN_MODE_OPTIONAL) instead.");
                    }
                    break;
                case 2:
                    if (z) {
                        if (this.zzfhj) {
                            this.zzfmb = new zzad(this.mContext, this.zzfjy, this.zzakl, this.zzfhf, this.zzfmh, this.zzfkd, this.zzfkg, this.zzfhg, this.zzfmk, this, true);
                            return;
                        } else {
                            this.zzfmb = zzy.zza(this.mContext, this, this.zzfjy, this.zzakl, this.zzfhf, this.zzfmh, this.zzfkd, this.zzfkg, this.zzfhg, this.zzfmk);
                            return;
                        }
                    }
                    break;
            }
            if (!this.zzfhj || z2) {
                this.zzfmb = new zzbl(this.mContext, this, this.zzfjy, this.zzakl, this.zzfhf, this.zzfmh, this.zzfkd, this.zzfkg, this.zzfhg, this.zzfmk, this);
            } else {
                this.zzfmb = new zzad(this.mContext, this.zzfjy, this.zzakl, this.zzfhf, this.zzfmh, this.zzfkd, this.zzfkg, this.zzfhg, this.zzfmk, this, false);
            }
        }
    }

    private static String zzbt(int i) {
        switch (i) {
            case 1:
                return "SIGN_IN_MODE_REQUIRED";
            case 2:
                return "SIGN_IN_MODE_OPTIONAL";
            case 3:
                return "SIGN_IN_MODE_NONE";
            default:
                return "UNKNOWN";
        }
    }

    public final ConnectionResult blockingConnect() {
        boolean z = true;
        zzbp.zza(Looper.myLooper() != Looper.getMainLooper(), (Object) "blockingConnect must not be called on the UI thread");
        this.zzfjy.lock();
        try {
            if (this.zzfhd >= 0) {
                if (this.zzfml == null) {
                    z = false;
                }
                zzbp.zza(z, (Object) "Sign-in mode should have been set explicitly by auto-manage.");
            } else if (this.zzfml == null) {
                this.zzfml = Integer.valueOf(zza(this.zzfmh.values(), false));
            } else if (this.zzfml.intValue() == 2) {
                throw new IllegalStateException("Cannot call blockingConnect() when sign-in mode is set to SIGN_IN_MODE_OPTIONAL. Call connect(SIGN_IN_MODE_OPTIONAL) instead.");
            }
            zzbs(this.zzfml.intValue());
            this.zzfma.zzakf();
            ConnectionResult blockingConnect = this.zzfmb.blockingConnect();
            return blockingConnect;
        } finally {
            this.zzfjy.unlock();
        }
    }

    public final ConnectionResult blockingConnect(long j, @NonNull TimeUnit timeUnit) {
        boolean z = false;
        if (Looper.myLooper() != Looper.getMainLooper()) {
            z = true;
        }
        zzbp.zza(z, (Object) "blockingConnect must not be called on the UI thread");
        zzbp.zzb((Object) timeUnit, (Object) "TimeUnit must not be null");
        this.zzfjy.lock();
        try {
            if (this.zzfml == null) {
                this.zzfml = Integer.valueOf(zza(this.zzfmh.values(), false));
            } else if (this.zzfml.intValue() == 2) {
                throw new IllegalStateException("Cannot call blockingConnect() when sign-in mode is set to SIGN_IN_MODE_OPTIONAL. Call connect(SIGN_IN_MODE_OPTIONAL) instead.");
            }
            zzbs(this.zzfml.intValue());
            this.zzfma.zzakf();
            ConnectionResult blockingConnect = this.zzfmb.blockingConnect(j, timeUnit);
            return blockingConnect;
        } finally {
            this.zzfjy.unlock();
        }
    }

    public final PendingResult<Status> clearDefaultAccountAndReconnect() {
        zzbp.zza(isConnected(), (Object) "GoogleApiClient is not connected yet.");
        zzbp.zza(this.zzfml.intValue() != 2, (Object) "Cannot use clearDefaultAccountAndReconnect with GOOGLE_SIGN_IN_API");
        PendingResult zzda = new zzda((GoogleApiClient) this);
        if (this.zzfmh.containsKey(zzbcd.zzdwq)) {
            zza(this, zzda, false);
        } else {
            AtomicReference atomicReference = new AtomicReference();
            GoogleApiClient build = new Builder(this.mContext).addApi(zzbcd.API).addConnectionCallbacks(new zzbf(this, atomicReference, zzda)).addOnConnectionFailedListener(new zzbg(this, zzda)).setHandler(this.zzfmf).build();
            atomicReference.set(build);
            build.connect();
        }
        return zzda;
    }

    public final void connect() {
        boolean z = false;
        this.zzfjy.lock();
        try {
            if (this.zzfhd >= 0) {
                if (this.zzfml != null) {
                    z = true;
                }
                zzbp.zza(z, (Object) "Sign-in mode should have been set explicitly by auto-manage.");
            } else if (this.zzfml == null) {
                this.zzfml = Integer.valueOf(zza(this.zzfmh.values(), false));
            } else if (this.zzfml.intValue() == 2) {
                throw new IllegalStateException("Cannot call connect() when SignInMode is set to SIGN_IN_MODE_OPTIONAL. Call connect(SIGN_IN_MODE_OPTIONAL) instead.");
            }
            connect(this.zzfml.intValue());
        } finally {
            this.zzfjy.unlock();
        }
    }

    public final void connect(int i) {
        boolean z = true;
        this.zzfjy.lock();
        if (!(i == 3 || i == 1 || i == 2)) {
            z = false;
        }
        try {
            zzbp.zzb(z, "Illegal sign-in mode: " + i);
            zzbs(i);
            zzahf();
        } finally {
            this.zzfjy.unlock();
        }
    }

    public final void disconnect() {
        this.zzfjy.lock();
        try {
            this.zzfmn.release();
            if (this.zzfmb != null) {
                this.zzfmb.disconnect();
            }
            this.zzfmj.release();
            for (zzm zzm : this.zzfkm) {
                zzm.zza(null);
                zzm.cancel();
            }
            this.zzfkm.clear();
            if (this.zzfmb != null) {
                zzahh();
                this.zzfma.zzake();
                this.zzfjy.unlock();
            }
        } finally {
            this.zzfjy.unlock();
        }
    }

    public final void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
        printWriter.append(str).append("mContext=").println(this.mContext);
        printWriter.append(str).append("mResuming=").print(this.zzfmc);
        printWriter.append(" mWorkQueue.size()=").print(this.zzfkm.size());
        printWriter.append(" mUnconsumedApiCalls.size()=").println(this.zzfmn.zzfpm.size());
        if (this.zzfmb != null) {
            this.zzfmb.dump(str, fileDescriptor, printWriter, strArr);
        }
    }

    @NonNull
    public final ConnectionResult getConnectionResult(@NonNull Api<?> api) {
        this.zzfjy.lock();
        try {
            if (!isConnected() && !this.zzfmc) {
                throw new IllegalStateException("Cannot invoke getConnectionResult unless GoogleApiClient is connected");
            } else if (this.zzfmh.containsKey(api.zzafd())) {
                ConnectionResult connectionResult = this.zzfmb.getConnectionResult(api);
                if (connectionResult != null) {
                    this.zzfjy.unlock();
                } else if (this.zzfmc) {
                    connectionResult = ConnectionResult.zzfez;
                } else {
                    Log.w("GoogleApiClientImpl", zzahj());
                    Log.wtf("GoogleApiClientImpl", String.valueOf(api.getName()).concat(" requested in getConnectionResult is not connected but is not present in the failed  connections map"), new Exception());
                    connectionResult = new ConnectionResult(8, null);
                    this.zzfjy.unlock();
                }
                return connectionResult;
            } else {
                throw new IllegalArgumentException(String.valueOf(api.getName()).concat(" was never registered with GoogleApiClient"));
            }
        } finally {
            this.zzfjy.unlock();
        }
    }

    public final Context getContext() {
        return this.mContext;
    }

    public final Looper getLooper() {
        return this.zzakl;
    }

    public final boolean hasConnectedApi(@NonNull Api<?> api) {
        if (!isConnected()) {
            return false;
        }
        zze zze = (zze) this.zzfmh.get(api.zzafd());
        return zze != null && zze.isConnected();
    }

    public final boolean isConnected() {
        return this.zzfmb != null && this.zzfmb.isConnected();
    }

    public final boolean isConnecting() {
        return this.zzfmb != null && this.zzfmb.isConnecting();
    }

    public final boolean isConnectionCallbacksRegistered(@NonNull ConnectionCallbacks connectionCallbacks) {
        return this.zzfma.isConnectionCallbacksRegistered(connectionCallbacks);
    }

    public final boolean isConnectionFailedListenerRegistered(@NonNull OnConnectionFailedListener onConnectionFailedListener) {
        return this.zzfma.isConnectionFailedListenerRegistered(onConnectionFailedListener);
    }

    public final void reconnect() {
        disconnect();
        connect();
    }

    public final void registerConnectionCallbacks(@NonNull ConnectionCallbacks connectionCallbacks) {
        this.zzfma.registerConnectionCallbacks(connectionCallbacks);
    }

    public final void registerConnectionFailedListener(@NonNull OnConnectionFailedListener onConnectionFailedListener) {
        this.zzfma.registerConnectionFailedListener(onConnectionFailedListener);
    }

    public final void stopAutoManage(@NonNull FragmentActivity fragmentActivity) {
        zzcf zzcf = new zzcf(fragmentActivity);
        if (this.zzfhd >= 0) {
            zzi.zza(zzcf).zzbo(this.zzfhd);
            return;
        }
        throw new IllegalStateException("Called stopAutoManage but automatic lifecycle management is not enabled.");
    }

    public final void unregisterConnectionCallbacks(@NonNull ConnectionCallbacks connectionCallbacks) {
        this.zzfma.unregisterConnectionCallbacks(connectionCallbacks);
    }

    public final void unregisterConnectionFailedListener(@NonNull OnConnectionFailedListener onConnectionFailedListener) {
        this.zzfma.unregisterConnectionFailedListener(onConnectionFailedListener);
    }

    @NonNull
    public final <C extends zze> C zza(@NonNull zzc<C> zzc) {
        Object obj = (zze) this.zzfmh.get(zzc);
        zzbp.zzb(obj, (Object) "Appropriate Api was not requested.");
        return obj;
    }

    public final void zza(zzdf zzdf) {
        this.zzfjy.lock();
        try {
            if (this.zzfmm == null) {
                this.zzfmm = new HashSet();
            }
            this.zzfmm.add(zzdf);
        } finally {
            this.zzfjy.unlock();
        }
    }

    public final boolean zza(@NonNull Api<?> api) {
        return this.zzfmh.containsKey(api.zzafd());
    }

    public final boolean zza(zzcv zzcv) {
        return this.zzfmb != null && this.zzfmb.zza(zzcv);
    }

    public final void zzafo() {
        if (this.zzfmb != null) {
            this.zzfmb.zzafo();
        }
    }

    final boolean zzahh() {
        if (!this.zzfmc) {
            return false;
        }
        this.zzfmc = false;
        this.zzfmf.removeMessages(2);
        this.zzfmf.removeMessages(1);
        if (this.zzfmg == null) {
            return true;
        }
        this.zzfmg.unregister();
        this.zzfmg = null;
        return true;
    }

    final boolean zzahi() {
        boolean z = false;
        this.zzfjy.lock();
        try {
            if (this.zzfmm != null) {
                if (!this.zzfmm.isEmpty()) {
                    z = true;
                }
                this.zzfjy.unlock();
            }
            return z;
        } finally {
            this.zzfjy.unlock();
        }
    }

    final String zzahj() {
        Writer stringWriter = new StringWriter();
        dump("", null, new PrintWriter(stringWriter), null);
        return stringWriter.toString();
    }

    public final void zzb(zzdf zzdf) {
        this.zzfjy.lock();
        try {
            if (this.zzfmm == null) {
                Log.wtf("GoogleApiClientImpl", "Attempted to remove pending transform when no transforms are registered.", new Exception());
            } else if (!this.zzfmm.remove(zzdf)) {
                Log.wtf("GoogleApiClientImpl", "Failed to remove pending transform - this may lead to memory leaks!", new Exception());
            } else if (!zzahi()) {
                this.zzfmb.zzagh();
            }
            this.zzfjy.unlock();
        } catch (Throwable th) {
            this.zzfjy.unlock();
        }
    }

    public final void zzc(ConnectionResult connectionResult) {
        if (!com.google.android.gms.common.zze.zze(this.mContext, connectionResult.getErrorCode())) {
            zzahh();
        }
        if (!this.zzfmc) {
            this.zzfma.zzk(connectionResult);
            this.zzfma.zzake();
        }
    }

    public final <A extends zzb, R extends Result, T extends zzm<R, A>> T zzd(@NonNull T t) {
        zzbp.zzb(t.zzafd() != null, (Object) "This task can not be enqueued (it's probably a Batch or malformed)");
        boolean containsKey = this.zzfmh.containsKey(t.zzafd());
        String name = t.zzafi() != null ? t.zzafi().getName() : "the API";
        zzbp.zzb(containsKey, new StringBuilder(String.valueOf(name).length() + 65).append("GoogleApiClient is not configured to use ").append(name).append(" required for this call.").toString());
        this.zzfjy.lock();
        try {
            if (this.zzfmb == null) {
                this.zzfkm.add(t);
            } else {
                t = this.zzfmb.zzd(t);
                this.zzfjy.unlock();
            }
            return t;
        } finally {
            this.zzfjy.unlock();
        }
    }

    public final <A extends zzb, T extends zzm<? extends Result, A>> T zze(@NonNull T t) {
        zzbp.zzb(t.zzafd() != null, (Object) "This task can not be executed (it's probably a Batch or malformed)");
        boolean containsKey = this.zzfmh.containsKey(t.zzafd());
        String name = t.zzafi() != null ? t.zzafi().getName() : "the API";
        zzbp.zzb(containsKey, new StringBuilder(String.valueOf(name).length() + 65).append("GoogleApiClient is not configured to use ").append(name).append(" required for this call.").toString());
        this.zzfjy.lock();
        try {
            if (this.zzfmb == null) {
                throw new IllegalStateException("GoogleApiClient is not connected yet.");
            }
            if (this.zzfmc) {
                this.zzfkm.add(t);
                while (!this.zzfkm.isEmpty()) {
                    zzm zzm = (zzm) this.zzfkm.remove();
                    this.zzfmn.zzb(zzm);
                    zzm.zzs(Status.zzfhr);
                }
            } else {
                t = this.zzfmb.zze(t);
                this.zzfjy.unlock();
            }
            return t;
        } finally {
            this.zzfjy.unlock();
        }
    }

    public final void zzf(int i, boolean z) {
        if (!(i != 1 || z || this.zzfmc)) {
            this.zzfmc = true;
            if (this.zzfmg == null) {
                this.zzfmg = GoogleApiAvailability.zza(this.mContext.getApplicationContext(), new zzbj(this));
            }
            this.zzfmf.sendMessageDelayed(this.zzfmf.obtainMessage(1), this.zzfmd);
            this.zzfmf.sendMessageDelayed(this.zzfmf.obtainMessage(2), this.zzfme);
        }
        this.zzfmn.zzaiq();
        this.zzfma.zzcd(i);
        this.zzfma.zzake();
        if (i == 2) {
            zzahf();
        }
    }

    public final void zzi(Bundle bundle) {
        while (!this.zzfkm.isEmpty()) {
            zze((zzm) this.zzfkm.remove());
        }
        this.zzfma.zzj(bundle);
    }

    public final <L> zzcj<L> zzp(@NonNull L l) {
        this.zzfjy.lock();
        try {
            zzcj<L> zza = this.zzfmj.zza(l, this.zzakl, "NO_TYPE");
            return zza;
        } finally {
            this.zzfjy.unlock();
        }
    }
}
