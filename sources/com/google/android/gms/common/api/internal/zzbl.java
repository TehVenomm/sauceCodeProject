package com.google.android.gms.common.api.internal;

import android.content.Context;
import android.os.Bundle;
import android.os.Looper;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.zza;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.Api.zzc;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.common.zze;
import com.google.android.gms.internal.zzcpm;
import com.google.android.gms.internal.zzcpn;
import java.io.FileDescriptor;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.locks.Condition;
import java.util.concurrent.locks.Lock;

public final class zzbl implements zzcd, zzx {
    private final Context mContext;
    private zza<? extends zzcpm, zzcpn> zzfhg;
    final zzbd zzfjo;
    private final Lock zzfjy;
    private zzq zzfkd;
    private Map<Api<?>, Boolean> zzfkg;
    private final zze zzfki;
    final Map<zzc<?>, Api.zze> zzfmh;
    private final Condition zzfmu;
    private final zzbn zzfmv;
    final Map<zzc<?>, ConnectionResult> zzfmw = new HashMap();
    private volatile zzbk zzfmx;
    private ConnectionResult zzfmy = null;
    int zzfmz;
    final zzce zzfna;

    public zzbl(Context context, zzbd zzbd, Lock lock, Looper looper, zze zze, Map<zzc<?>, Api.zze> map, zzq zzq, Map<Api<?>, Boolean> map2, zza<? extends zzcpm, zzcpn> zza, ArrayList<zzw> arrayList, zzce zzce) {
        this.mContext = context;
        this.zzfjy = lock;
        this.zzfki = zze;
        this.zzfmh = map;
        this.zzfkd = zzq;
        this.zzfkg = map2;
        this.zzfhg = zza;
        this.zzfjo = zzbd;
        this.zzfna = zzce;
        ArrayList arrayList2 = arrayList;
        int size = arrayList2.size();
        int i = 0;
        while (i < size) {
            Object obj = arrayList2.get(i);
            i++;
            ((zzw) obj).zza(this);
        }
        this.zzfmv = new zzbn(this, looper);
        this.zzfmu = lock.newCondition();
        this.zzfmx = new zzbc(this);
    }

    public final ConnectionResult blockingConnect() {
        connect();
        while (isConnecting()) {
            try {
                this.zzfmu.await();
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                return new ConnectionResult(15, null);
            }
        }
        return isConnected() ? ConnectionResult.zzfez : this.zzfmy != null ? this.zzfmy : new ConnectionResult(13, null);
    }

    public final ConnectionResult blockingConnect(long j, TimeUnit timeUnit) {
        connect();
        long toNanos = timeUnit.toNanos(j);
        while (isConnecting()) {
            if (toNanos <= 0) {
                try {
                    disconnect();
                    return new ConnectionResult(14, null);
                } catch (InterruptedException e) {
                    Thread.currentThread().interrupt();
                    return new ConnectionResult(15, null);
                }
            }
            toNanos = this.zzfmu.awaitNanos(toNanos);
        }
        return isConnected() ? ConnectionResult.zzfez : this.zzfmy != null ? this.zzfmy : new ConnectionResult(13, null);
    }

    public final void connect() {
        this.zzfmx.connect();
    }

    public final void disconnect() {
        if (this.zzfmx.disconnect()) {
            this.zzfmw.clear();
        }
    }

    public final void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
        String concat = String.valueOf(str).concat("  ");
        printWriter.append(str).append("mState=").println(this.zzfmx);
        for (Api api : this.zzfkg.keySet()) {
            printWriter.append(str).append(api.getName()).println(":");
            ((Api.zze) this.zzfmh.get(api.zzafd())).dump(concat, fileDescriptor, printWriter, strArr);
        }
    }

    @Nullable
    public final ConnectionResult getConnectionResult(@NonNull Api<?> api) {
        zzc zzafd = api.zzafd();
        if (this.zzfmh.containsKey(zzafd)) {
            if (((Api.zze) this.zzfmh.get(zzafd)).isConnected()) {
                return ConnectionResult.zzfez;
            }
            if (this.zzfmw.containsKey(zzafd)) {
                return (ConnectionResult) this.zzfmw.get(zzafd);
            }
        }
        return null;
    }

    public final boolean isConnected() {
        return this.zzfmx instanceof zzao;
    }

    public final boolean isConnecting() {
        return this.zzfmx instanceof zzar;
    }

    public final void onConnected(@Nullable Bundle bundle) {
        this.zzfjy.lock();
        try {
            this.zzfmx.onConnected(bundle);
        } finally {
            this.zzfjy.unlock();
        }
    }

    public final void onConnectionSuspended(int i) {
        this.zzfjy.lock();
        try {
            this.zzfmx.onConnectionSuspended(i);
        } finally {
            this.zzfjy.unlock();
        }
    }

    public final void zza(@NonNull ConnectionResult connectionResult, @NonNull Api<?> api, boolean z) {
        this.zzfjy.lock();
        try {
            this.zzfmx.zza(connectionResult, api, z);
        } finally {
            this.zzfjy.unlock();
        }
    }

    final void zza(zzbm zzbm) {
        this.zzfmv.sendMessage(this.zzfmv.obtainMessage(1, zzbm));
    }

    final void zza(RuntimeException runtimeException) {
        this.zzfmv.sendMessage(this.zzfmv.obtainMessage(2, runtimeException));
    }

    public final boolean zza(zzcv zzcv) {
        return false;
    }

    public final void zzafo() {
    }

    public final void zzagh() {
        if (isConnected()) {
            ((zzao) this.zzfmx).zzagx();
        }
    }

    final void zzahk() {
        this.zzfjy.lock();
        try {
            this.zzfmx = new zzar(this, this.zzfkd, this.zzfkg, this.zzfki, this.zzfhg, this.zzfjy, this.mContext);
            this.zzfmx.begin();
            this.zzfmu.signalAll();
        } finally {
            this.zzfjy.unlock();
        }
    }

    final void zzahl() {
        this.zzfjy.lock();
        try {
            this.zzfjo.zzahh();
            this.zzfmx = new zzao(this);
            this.zzfmx.begin();
            this.zzfmu.signalAll();
        } finally {
            this.zzfjy.unlock();
        }
    }

    public final <A extends zzb, R extends Result, T extends zzm<R, A>> T zzd(@NonNull T t) {
        t.zzagf();
        return this.zzfmx.zzd(t);
    }

    public final <A extends zzb, T extends zzm<? extends Result, A>> T zze(@NonNull T t) {
        t.zzagf();
        return this.zzfmx.zze(t);
    }

    final void zzg(ConnectionResult connectionResult) {
        this.zzfjy.lock();
        try {
            this.zzfmy = connectionResult;
            this.zzfmx = new zzbc(this);
            this.zzfmx.begin();
            this.zzfmu.signalAll();
        } finally {
            this.zzfjy.unlock();
        }
    }
}
