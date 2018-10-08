package com.google.android.gms.common.api.internal;

import android.app.PendingIntent;
import android.content.Context;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.util.ArrayMap;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.zza;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.Api.zzc;
import com.google.android.gms.common.api.Api.zze;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.internal.zzcpm;
import com.google.android.gms.internal.zzcpn;
import java.io.FileDescriptor;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;
import java.util.WeakHashMap;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.locks.Lock;

final class zzy implements zzcd {
    private final Context mContext;
    private final Looper zzakl;
    private final zzbd zzfjo;
    private final zzbl zzfjp;
    private final zzbl zzfjq;
    private final Map<zzc<?>, zzbl> zzfjr;
    private final Set<zzcv> zzfjs = Collections.newSetFromMap(new WeakHashMap());
    private final zze zzfjt;
    private Bundle zzfju;
    private ConnectionResult zzfjv = null;
    private ConnectionResult zzfjw = null;
    private boolean zzfjx = false;
    private final Lock zzfjy;
    private int zzfjz = 0;

    private zzy(Context context, zzbd zzbd, Lock lock, Looper looper, com.google.android.gms.common.zze zze, Map<zzc<?>, zze> map, Map<zzc<?>, zze> map2, zzq zzq, zza<? extends zzcpm, zzcpn> zza, zze zze2, ArrayList<zzw> arrayList, ArrayList<zzw> arrayList2, Map<Api<?>, Boolean> map3, Map<Api<?>, Boolean> map4) {
        this.mContext = context;
        this.zzfjo = zzbd;
        this.zzfjy = lock;
        this.zzakl = looper;
        this.zzfjt = zze2;
        this.zzfjp = new zzbl(context, this.zzfjo, lock, looper, zze, map2, null, map4, null, arrayList2, new zzaa());
        this.zzfjq = new zzbl(context, this.zzfjo, lock, looper, zze, map, zzq, map3, zza, arrayList, new zzab());
        Map arrayMap = new ArrayMap();
        for (zzc put : map2.keySet()) {
            arrayMap.put(put, this.zzfjp);
        }
        for (zzc put2 : map.keySet()) {
            arrayMap.put(put2, this.zzfjq);
        }
        this.zzfjr = Collections.unmodifiableMap(arrayMap);
    }

    public static zzy zza(Context context, zzbd zzbd, Lock lock, Looper looper, com.google.android.gms.common.zze zze, Map<zzc<?>, zze> map, zzq zzq, Map<Api<?>, Boolean> map2, zza<? extends zzcpm, zzcpn> zza, ArrayList<zzw> arrayList) {
        zze zze2 = null;
        Map arrayMap = new ArrayMap();
        Map arrayMap2 = new ArrayMap();
        for (Entry entry : map.entrySet()) {
            zze zze3 = (zze) entry.getValue();
            if (zze3.zzaak()) {
                zze2 = zze3;
            }
            if (zze3.zzaaa()) {
                arrayMap.put((zzc) entry.getKey(), zze3);
            } else {
                arrayMap2.put((zzc) entry.getKey(), zze3);
            }
        }
        zzbp.zza(!arrayMap.isEmpty(), (Object) "CompositeGoogleApiClient should not be used without any APIs that require sign-in.");
        Map arrayMap3 = new ArrayMap();
        Map arrayMap4 = new ArrayMap();
        for (Api api : map2.keySet()) {
            zzc zzafd = api.zzafd();
            if (arrayMap.containsKey(zzafd)) {
                arrayMap3.put(api, (Boolean) map2.get(api));
            } else if (arrayMap2.containsKey(zzafd)) {
                arrayMap4.put(api, (Boolean) map2.get(api));
            } else {
                throw new IllegalStateException("Each API in the isOptionalMap must have a corresponding client in the clients map.");
            }
        }
        ArrayList arrayList2 = new ArrayList();
        ArrayList arrayList3 = new ArrayList();
        ArrayList arrayList4 = arrayList;
        int size = arrayList4.size();
        int i = 0;
        while (i < size) {
            Object obj = arrayList4.get(i);
            i++;
            zzw zzw = (zzw) obj;
            if (arrayMap3.containsKey(zzw.zzfda)) {
                arrayList2.add(zzw);
            } else if (arrayMap4.containsKey(zzw.zzfda)) {
                arrayList3.add(zzw);
            } else {
                throw new IllegalStateException("Each ClientCallbacks must have a corresponding API in the isOptionalMap");
            }
        }
        return new zzy(context, zzbd, lock, looper, zze, arrayMap, arrayMap2, zzq, zza, zze2, arrayList2, arrayList3, arrayMap3, arrayMap4);
    }

    private final void zza(ConnectionResult connectionResult) {
        switch (this.zzfjz) {
            case 1:
                break;
            case 2:
                this.zzfjo.zzc(connectionResult);
                break;
            default:
                Log.wtf("CompositeGAC", "Attempted to call failure callbacks in CONNECTION_MODE_NONE. Callbacks should be disabled via GmsClientSupervisor", new Exception());
                break;
        }
        zzagj();
        this.zzfjz = 0;
    }

    private final void zzagi() {
        if (zzb(this.zzfjv)) {
            if (zzb(this.zzfjw) || zzagk()) {
                switch (this.zzfjz) {
                    case 1:
                        break;
                    case 2:
                        this.zzfjo.zzi(this.zzfju);
                        break;
                    default:
                        Log.wtf("CompositeGAC", "Attempted to call success callbacks in CONNECTION_MODE_NONE. Callbacks should be disabled via GmsClientSupervisor", new AssertionError());
                        break;
                }
                zzagj();
                this.zzfjz = 0;
            } else if (this.zzfjw == null) {
            } else {
                if (this.zzfjz == 1) {
                    zzagj();
                    return;
                }
                zza(this.zzfjw);
                this.zzfjp.disconnect();
            }
        } else if (this.zzfjv != null && zzb(this.zzfjw)) {
            this.zzfjq.disconnect();
            zza(this.zzfjv);
        } else if (this.zzfjv != null && this.zzfjw != null) {
            ConnectionResult connectionResult = this.zzfjv;
            if (this.zzfjq.zzfmz < this.zzfjp.zzfmz) {
                connectionResult = this.zzfjw;
            }
            zza(connectionResult);
        }
    }

    private final void zzagj() {
        for (zzcv zzaaj : this.zzfjs) {
            zzaaj.zzaaj();
        }
        this.zzfjs.clear();
    }

    private final boolean zzagk() {
        return this.zzfjw != null && this.zzfjw.getErrorCode() == 4;
    }

    @Nullable
    private final PendingIntent zzagl() {
        return this.zzfjt == null ? null : PendingIntent.getActivity(this.mContext, System.identityHashCode(this.zzfjo), this.zzfjt.zzaal(), 134217728);
    }

    private static boolean zzb(ConnectionResult connectionResult) {
        return connectionResult != null && connectionResult.isSuccess();
    }

    private final void zze(int i, boolean z) {
        this.zzfjo.zzf(i, z);
        this.zzfjw = null;
        this.zzfjv = null;
    }

    private final boolean zzf(zzm<? extends Result, ? extends zzb> zzm) {
        zzc zzafd = zzm.zzafd();
        zzbp.zzb(this.zzfjr.containsKey(zzafd), (Object) "GoogleApiClient is not configured to use the API required for this call.");
        return ((zzbl) this.zzfjr.get(zzafd)).equals(this.zzfjq);
    }

    private final void zzh(Bundle bundle) {
        if (this.zzfju == null) {
            this.zzfju = bundle;
        } else if (bundle != null) {
            this.zzfju.putAll(bundle);
        }
    }

    public final ConnectionResult blockingConnect() {
        throw new UnsupportedOperationException();
    }

    public final ConnectionResult blockingConnect(long j, @NonNull TimeUnit timeUnit) {
        throw new UnsupportedOperationException();
    }

    public final void connect() {
        this.zzfjz = 2;
        this.zzfjx = false;
        this.zzfjw = null;
        this.zzfjv = null;
        this.zzfjp.connect();
        this.zzfjq.connect();
    }

    public final void disconnect() {
        this.zzfjw = null;
        this.zzfjv = null;
        this.zzfjz = 0;
        this.zzfjp.disconnect();
        this.zzfjq.disconnect();
        zzagj();
    }

    public final void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
        printWriter.append(str).append("authClient").println(":");
        this.zzfjq.dump(String.valueOf(str).concat("  "), fileDescriptor, printWriter, strArr);
        printWriter.append(str).append("anonClient").println(":");
        this.zzfjp.dump(String.valueOf(str).concat("  "), fileDescriptor, printWriter, strArr);
    }

    @Nullable
    public final ConnectionResult getConnectionResult(@NonNull Api<?> api) {
        return ((zzbl) this.zzfjr.get(api.zzafd())).equals(this.zzfjq) ? zzagk() ? new ConnectionResult(4, zzagl()) : this.zzfjq.getConnectionResult(api) : this.zzfjp.getConnectionResult(api);
    }

    public final boolean isConnected() {
        boolean z = true;
        this.zzfjy.lock();
        try {
            if (!(this.zzfjp.isConnected() && (this.zzfjq.isConnected() || zzagk() || this.zzfjz == 1))) {
                z = false;
            }
            this.zzfjy.unlock();
            return z;
        } catch (Throwable th) {
            this.zzfjy.unlock();
        }
    }

    public final boolean isConnecting() {
        this.zzfjy.lock();
        try {
            boolean z = this.zzfjz == 2;
            this.zzfjy.unlock();
            return z;
        } catch (Throwable th) {
            this.zzfjy.unlock();
        }
    }

    public final boolean zza(zzcv zzcv) {
        this.zzfjy.lock();
        try {
            if ((isConnecting() || isConnected()) && !this.zzfjq.isConnected()) {
                this.zzfjs.add(zzcv);
                if (this.zzfjz == 0) {
                    this.zzfjz = 1;
                }
                this.zzfjw = null;
                this.zzfjq.connect();
                return true;
            }
            this.zzfjy.unlock();
            return false;
        } finally {
            this.zzfjy.unlock();
        }
    }

    public final void zzafo() {
        this.zzfjy.lock();
        try {
            boolean isConnecting = isConnecting();
            this.zzfjq.disconnect();
            this.zzfjw = new ConnectionResult(4);
            if (isConnecting) {
                new Handler(this.zzakl).post(new zzz(this));
            } else {
                zzagj();
            }
            this.zzfjy.unlock();
        } catch (Throwable th) {
            this.zzfjy.unlock();
        }
    }

    public final void zzagh() {
        this.zzfjp.zzagh();
        this.zzfjq.zzagh();
    }

    public final <A extends zzb, R extends Result, T extends zzm<R, A>> T zzd(@NonNull T t) {
        if (!zzf((zzm) t)) {
            return this.zzfjp.zzd(t);
        }
        if (!zzagk()) {
            return this.zzfjq.zzd(t);
        }
        t.zzs(new Status(4, null, zzagl()));
        return t;
    }

    public final <A extends zzb, T extends zzm<? extends Result, A>> T zze(@NonNull T t) {
        if (!zzf((zzm) t)) {
            return this.zzfjp.zze(t);
        }
        if (!zzagk()) {
            return this.zzfjq.zze(t);
        }
        t.zzs(new Status(4, null, zzagl()));
        return t;
    }
}
