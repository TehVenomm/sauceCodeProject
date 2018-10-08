package com.google.android.gms.common.api.internal;

import android.content.Context;
import android.os.Looper;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.util.ArrayMap;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.zza;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.Api.zzc;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.common.internal.zzs;
import com.google.android.gms.common.zze;
import com.google.android.gms.internal.zzbdk;
import com.google.android.gms.internal.zzcpm;
import com.google.android.gms.internal.zzcpn;
import java.io.FileDescriptor;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Queue;
import java.util.Set;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.locks.Condition;
import java.util.concurrent.locks.Lock;

public final class zzad implements zzcd {
    private final Looper zzakl;
    private final zzbp zzfgp;
    private final Lock zzfjy;
    private final zzq zzfkd;
    private final Map<zzc<?>, zzac<?>> zzfke = new HashMap();
    private final Map<zzc<?>, zzac<?>> zzfkf = new HashMap();
    private final Map<Api<?>, Boolean> zzfkg;
    private final zzbd zzfkh;
    private final zze zzfki;
    private final Condition zzfkj;
    private final boolean zzfkk;
    private final boolean zzfkl;
    private final Queue<zzm<?, ?>> zzfkm = new LinkedList();
    private boolean zzfkn;
    private Map<zzh<?>, ConnectionResult> zzfko;
    private Map<zzh<?>, ConnectionResult> zzfkp;
    private zzag zzfkq;
    private ConnectionResult zzfkr;

    public zzad(Context context, Lock lock, Looper looper, zze zze, Map<zzc<?>, Api.zze> map, zzq zzq, Map<Api<?>, Boolean> map2, zza<? extends zzcpm, zzcpn> zza, ArrayList<zzw> arrayList, zzbd zzbd, boolean z) {
        this.zzfjy = lock;
        this.zzakl = looper;
        this.zzfkj = lock.newCondition();
        this.zzfki = zze;
        this.zzfkh = zzbd;
        this.zzfkg = map2;
        this.zzfkd = zzq;
        this.zzfkk = z;
        Map hashMap = new HashMap();
        for (Api api : map2.keySet()) {
            hashMap.put(api.zzafd(), api);
        }
        Map hashMap2 = new HashMap();
        ArrayList arrayList2 = arrayList;
        int size = arrayList2.size();
        int i = 0;
        while (i < size) {
            Object obj = arrayList2.get(i);
            i++;
            zzw zzw = (zzw) obj;
            hashMap2.put(zzw.zzfda, zzw);
        }
        Object obj2 = null;
        Object obj3 = 1;
        Object obj4 = null;
        for (Entry entry : map.entrySet()) {
            Object obj5;
            Object obj6;
            Object obj7;
            Api api2 = (Api) hashMap.get(entry.getKey());
            Api.zze zze2 = (Api.zze) entry.getValue();
            if (!zze2.zzafe()) {
                obj5 = obj2;
                obj6 = null;
                obj7 = obj4;
            } else if (((Boolean) this.zzfkg.get(api2)).booleanValue()) {
                int i2 = 1;
                obj6 = obj3;
                obj7 = obj4;
            } else {
                obj5 = 1;
                obj6 = obj3;
                obj7 = 1;
            }
            zzac zzac = new zzac(context, api2, looper, zze2, (zzw) hashMap2.get(api2), zzq, zza);
            this.zzfke.put((zzc) entry.getKey(), zzac);
            if (zze2.zzaaa()) {
                this.zzfkf.put((zzc) entry.getKey(), zzac);
            }
            obj2 = obj5;
            obj3 = obj6;
            obj4 = obj7;
        }
        boolean z2 = obj2 != null && obj3 == null && obj4 == null;
        this.zzfkl = z2;
        this.zzfgp = zzbp.zzahn();
    }

    private final boolean zza(zzac<?> zzac, ConnectionResult connectionResult) {
        return !connectionResult.isSuccess() && !connectionResult.hasResolution() && ((Boolean) this.zzfkg.get(zzac.zzafi())).booleanValue() && zzac.zzagm().zzafe() && this.zzfki.isUserResolvableError(connectionResult.getErrorCode());
    }

    private final boolean zzagn() {
        this.zzfjy.lock();
        try {
            if (this.zzfkn && this.zzfkk) {
                for (zzc zzb : this.zzfkf.keySet()) {
                    ConnectionResult zzb2 = zzb(zzb);
                    if (zzb2 != null) {
                        if (!zzb2.isSuccess()) {
                        }
                    }
                    this.zzfjy.unlock();
                    return false;
                }
                this.zzfjy.unlock();
                return true;
            }
            this.zzfjy.unlock();
            return false;
        } catch (Throwable th) {
            this.zzfjy.unlock();
        }
    }

    private final void zzago() {
        if (this.zzfkd == null) {
            this.zzfkh.zzfmi = Collections.emptySet();
            return;
        }
        Set hashSet = new HashSet(this.zzfkd.zzajr());
        Map zzajt = this.zzfkd.zzajt();
        for (Api api : zzajt.keySet()) {
            ConnectionResult connectionResult = getConnectionResult(api);
            if (connectionResult != null && connectionResult.isSuccess()) {
                hashSet.addAll(((zzs) zzajt.get(api)).zzecn);
            }
        }
        this.zzfkh.zzfmi = hashSet;
    }

    private final void zzagp() {
        while (!this.zzfkm.isEmpty()) {
            zze((zzm) this.zzfkm.remove());
        }
        this.zzfkh.zzi(null);
    }

    @Nullable
    private final ConnectionResult zzagq() {
        ConnectionResult connectionResult = null;
        ConnectionResult connectionResult2 = null;
        int i = 0;
        int i2 = 0;
        for (zzac zzac : this.zzfke.values()) {
            Api zzafi = zzac.zzafi();
            ConnectionResult connectionResult3 = (ConnectionResult) this.zzfko.get(zzac.zzafj());
            if (!connectionResult3.isSuccess() && (!((Boolean) this.zzfkg.get(zzafi)).booleanValue() || connectionResult3.hasResolution() || this.zzfki.isUserResolvableError(connectionResult3.getErrorCode()))) {
                int priority;
                if (connectionResult3.getErrorCode() == 4 && this.zzfkk) {
                    priority = zzafi.zzafb().getPriority();
                    if (connectionResult == null || i > priority) {
                        connectionResult = connectionResult3;
                        i = priority;
                    }
                } else {
                    priority = zzafi.zzafb().getPriority();
                    if (connectionResult2 != null && i2 <= priority) {
                        connectionResult3 = connectionResult2;
                        priority = i2;
                    }
                    connectionResult2 = connectionResult3;
                    i2 = priority;
                }
            }
        }
        return (connectionResult2 == null || connectionResult == null || i2 <= i) ? connectionResult2 : connectionResult;
    }

    @Nullable
    private final ConnectionResult zzb(@NonNull zzc<?> zzc) {
        this.zzfjy.lock();
        try {
            zzac zzac = (zzac) this.zzfke.get(zzc);
            if (this.zzfko == null || zzac == null) {
                this.zzfjy.unlock();
                return null;
            }
            ConnectionResult connectionResult = (ConnectionResult) this.zzfko.get(zzac.zzafj());
            return connectionResult;
        } finally {
            this.zzfjy.unlock();
        }
    }

    private final <T extends zzm<? extends Result, ? extends zzb>> boolean zzg(@NonNull T t) {
        zzc zzafd = t.zzafd();
        ConnectionResult zzb = zzb(zzafd);
        if (zzb == null || zzb.getErrorCode() != 4) {
            return false;
        }
        t.zzs(new Status(4, null, this.zzfgp.zza(((zzac) this.zzfke.get(zzafd)).zzafj(), System.identityHashCode(this.zzfkh))));
        return true;
    }

    public final ConnectionResult blockingConnect() {
        connect();
        while (isConnecting()) {
            try {
                this.zzfkj.await();
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
                return new ConnectionResult(15, null);
            }
        }
        return isConnected() ? ConnectionResult.zzfez : this.zzfkr != null ? this.zzfkr : new ConnectionResult(13, null);
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
            toNanos = this.zzfkj.awaitNanos(toNanos);
        }
        return isConnected() ? ConnectionResult.zzfez : this.zzfkr != null ? this.zzfkr : new ConnectionResult(13, null);
    }

    public final void connect() {
        this.zzfjy.lock();
        try {
            if (!this.zzfkn) {
                this.zzfkn = true;
                this.zzfko = null;
                this.zzfkp = null;
                this.zzfkq = null;
                this.zzfkr = null;
                this.zzfgp.zzafv();
                this.zzfgp.zza(this.zzfke.values()).addOnCompleteListener(new zzbdk(this.zzakl), new zzaf());
                this.zzfjy.unlock();
            }
        } finally {
            this.zzfjy.unlock();
        }
    }

    public final void disconnect() {
        this.zzfjy.lock();
        try {
            this.zzfkn = false;
            this.zzfko = null;
            this.zzfkp = null;
            if (this.zzfkq != null) {
                this.zzfkq.cancel();
                this.zzfkq = null;
            }
            this.zzfkr = null;
            while (!this.zzfkm.isEmpty()) {
                zzm zzm = (zzm) this.zzfkm.remove();
                zzm.zza(null);
                zzm.cancel();
            }
            this.zzfkj.signalAll();
        } finally {
            this.zzfjy.unlock();
        }
    }

    public final void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
    }

    @Nullable
    public final ConnectionResult getConnectionResult(@NonNull Api<?> api) {
        return zzb(api.zzafd());
    }

    public final boolean isConnected() {
        this.zzfjy.lock();
        try {
            boolean z = this.zzfko != null && this.zzfkr == null;
            this.zzfjy.unlock();
            return z;
        } catch (Throwable th) {
            this.zzfjy.unlock();
        }
    }

    public final boolean isConnecting() {
        this.zzfjy.lock();
        try {
            boolean z = this.zzfko == null && this.zzfkn;
            this.zzfjy.unlock();
            return z;
        } catch (Throwable th) {
            this.zzfjy.unlock();
        }
    }

    public final boolean zza(zzcv zzcv) {
        this.zzfjy.lock();
        try {
            if (!this.zzfkn || zzagn()) {
                this.zzfjy.unlock();
                return false;
            }
            this.zzfgp.zzafv();
            this.zzfkq = new zzag(this, zzcv);
            this.zzfgp.zza(this.zzfkf.values()).addOnCompleteListener(new zzbdk(this.zzakl), this.zzfkq);
            return true;
        } finally {
            this.zzfjy.unlock();
        }
    }

    public final void zzafo() {
        this.zzfjy.lock();
        try {
            this.zzfgp.zzafo();
            if (this.zzfkq != null) {
                this.zzfkq.cancel();
                this.zzfkq = null;
            }
            if (this.zzfkp == null) {
                this.zzfkp = new ArrayMap(this.zzfkf.size());
            }
            ConnectionResult connectionResult = new ConnectionResult(4);
            for (zzac zzafj : this.zzfkf.values()) {
                this.zzfkp.put(zzafj.zzafj(), connectionResult);
            }
            if (this.zzfko != null) {
                this.zzfko.putAll(this.zzfkp);
            }
            this.zzfjy.unlock();
        } catch (Throwable th) {
            this.zzfjy.unlock();
        }
    }

    public final void zzagh() {
    }

    public final <A extends zzb, R extends Result, T extends zzm<R, A>> T zzd(@NonNull T t) {
        if (this.zzfkk && zzg((zzm) t)) {
            return t;
        }
        if (isConnected()) {
            this.zzfkh.zzfmn.zzb(t);
            return ((zzac) this.zzfke.get(t.zzafd())).zza((zzm) t);
        }
        this.zzfkm.add(t);
        return t;
    }

    public final <A extends zzb, T extends zzm<? extends Result, A>> T zze(@NonNull T t) {
        zzc zzafd = t.zzafd();
        if (this.zzfkk && zzg((zzm) t)) {
            return t;
        }
        this.zzfkh.zzfmn.zzb(t);
        return ((zzac) this.zzfke.get(zzafd)).zzb((zzm) t);
    }
}
