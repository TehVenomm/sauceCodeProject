package com.google.android.gms.common.api.internal;

import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.zza;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.Api.zzc;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.internal.zzam;
import com.google.android.gms.common.internal.zzbs;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.common.internal.zzs;
import com.google.android.gms.common.zze;
import com.google.android.gms.internal.zzcpm;
import com.google.android.gms.internal.zzcpn;
import com.google.android.gms.internal.zzcpz;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.Future;
import java.util.concurrent.locks.Lock;

public final class zzar implements zzbk {
    private final Context mContext;
    private final zza<? extends zzcpm, zzcpn> zzfhg;
    private final Lock zzfjy;
    private final zzq zzfkd;
    private final Map<Api<?>, Boolean> zzfkg;
    private final zze zzfki;
    private ConnectionResult zzfkr;
    private final zzbl zzflb;
    private int zzfle;
    private int zzflf = 0;
    private int zzflg;
    private final Bundle zzflh = new Bundle();
    private final Set<zzc> zzfli = new HashSet();
    private zzcpm zzflj;
    private boolean zzflk;
    private boolean zzfll;
    private boolean zzflm;
    private zzam zzfln;
    private boolean zzflo;
    private boolean zzflp;
    private ArrayList<Future<?>> zzflq = new ArrayList();

    public zzar(zzbl zzbl, zzq zzq, Map<Api<?>, Boolean> map, zze zze, zza<? extends zzcpm, zzcpn> zza, Lock lock, Context context) {
        this.zzflb = zzbl;
        this.zzfkd = zzq;
        this.zzfkg = map;
        this.zzfki = zze;
        this.zzfhg = zza;
        this.zzfjy = lock;
        this.mContext = context;
    }

    private final void zza(zzcpz zzcpz) {
        if (zzbq(0)) {
            ConnectionResult zzagc = zzcpz.zzagc();
            if (zzagc.isSuccess()) {
                zzbs zzbca = zzcpz.zzbca();
                ConnectionResult zzagc2 = zzbca.zzagc();
                if (zzagc2.isSuccess()) {
                    this.zzflm = true;
                    this.zzfln = zzbca.zzakl();
                    this.zzflo = zzbca.zzakm();
                    this.zzflp = zzbca.zzakn();
                    zzaha();
                    return;
                }
                String valueOf = String.valueOf(zzagc2);
                Log.wtf("GoogleApiClientConnecting", new StringBuilder(String.valueOf(valueOf).length() + 48).append("Sign-in succeeded with resolve account failure: ").append(valueOf).toString(), new Exception());
                zze(zzagc2);
            } else if (zzd(zzagc)) {
                zzahc();
                zzaha();
            } else {
                zze(zzagc);
            }
        }
    }

    private final boolean zzagz() {
        this.zzflg--;
        if (this.zzflg > 0) {
            return false;
        }
        if (this.zzflg < 0) {
            Log.w("GoogleApiClientConnecting", this.zzflb.zzfjo.zzahj());
            Log.wtf("GoogleApiClientConnecting", "GoogleApiClient received too many callbacks for the given step. Clients may be in an unexpected state; GoogleApiClient will now disconnect.", new Exception());
            zze(new ConnectionResult(8, null));
            return false;
        } else if (this.zzfkr == null) {
            return true;
        } else {
            this.zzflb.zzfmz = this.zzfle;
            zze(this.zzfkr);
            return false;
        }
    }

    private final void zzaha() {
        if (this.zzflg == 0) {
            if (!this.zzfll || this.zzflm) {
                ArrayList arrayList = new ArrayList();
                this.zzflf = 1;
                this.zzflg = this.zzflb.zzfmh.size();
                for (zzc zzc : this.zzflb.zzfmh.keySet()) {
                    if (!this.zzflb.zzfmw.containsKey(zzc)) {
                        arrayList.add((Api.zze) this.zzflb.zzfmh.get(zzc));
                    } else if (zzagz()) {
                        zzahb();
                    }
                }
                if (!arrayList.isEmpty()) {
                    this.zzflq.add(zzbo.zzahm().submit(new zzax(this, arrayList)));
                }
            }
        }
    }

    private final void zzahb() {
        this.zzflb.zzahl();
        zzbo.zzahm().execute(new zzas(this));
        if (this.zzflj != null) {
            if (this.zzflo) {
                this.zzflj.zza(this.zzfln, this.zzflp);
            }
            zzbf(false);
        }
        for (zzc zzc : this.zzflb.zzfmw.keySet()) {
            ((Api.zze) this.zzflb.zzfmh.get(zzc)).disconnect();
        }
        this.zzflb.zzfna.zzi(this.zzflh.isEmpty() ? null : this.zzflh);
    }

    private final void zzahc() {
        this.zzfll = false;
        this.zzflb.zzfjo.zzfmi = Collections.emptySet();
        for (zzc zzc : this.zzfli) {
            if (!this.zzflb.zzfmw.containsKey(zzc)) {
                this.zzflb.zzfmw.put(zzc, new ConnectionResult(17, null));
            }
        }
    }

    private final void zzahd() {
        ArrayList arrayList = this.zzflq;
        int size = arrayList.size();
        int i = 0;
        while (i < size) {
            Object obj = arrayList.get(i);
            i++;
            ((Future) obj).cancel(true);
        }
        this.zzflq.clear();
    }

    private final Set<Scope> zzahe() {
        if (this.zzfkd == null) {
            return Collections.emptySet();
        }
        Set<Scope> hashSet = new HashSet(this.zzfkd.zzajr());
        Map zzajt = this.zzfkd.zzajt();
        for (Api api : zzajt.keySet()) {
            if (!this.zzflb.zzfmw.containsKey(api.zzafd())) {
                hashSet.addAll(((zzs) zzajt.get(api)).zzecn);
            }
        }
        return hashSet;
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final void zzb(com.google.android.gms.common.ConnectionResult r6, com.google.android.gms.common.api.Api<?> r7, boolean r8) {
        /*
        r5 = this;
        r1 = 0;
        r0 = 1;
        r2 = r7.zzafb();
        r3 = r2.getPriority();
        if (r8 == 0) goto L_0x0015;
    L_0x000c:
        r2 = r6.hasResolution();
        if (r2 == 0) goto L_0x002f;
    L_0x0012:
        r2 = r0;
    L_0x0013:
        if (r2 == 0) goto L_0x003f;
    L_0x0015:
        r2 = r5.zzfkr;
        if (r2 == 0) goto L_0x001d;
    L_0x0019:
        r2 = r5.zzfle;
        if (r3 >= r2) goto L_0x003f;
    L_0x001d:
        if (r0 == 0) goto L_0x0023;
    L_0x001f:
        r5.zzfkr = r6;
        r5.zzfle = r3;
    L_0x0023:
        r0 = r5.zzflb;
        r0 = r0.zzfmw;
        r1 = r7.zzafd();
        r0.put(r1, r6);
        return;
    L_0x002f:
        r2 = r5.zzfki;
        r4 = r6.getErrorCode();
        r2 = r2.zzbn(r4);
        if (r2 == 0) goto L_0x003d;
    L_0x003b:
        r2 = r0;
        goto L_0x0013;
    L_0x003d:
        r2 = r1;
        goto L_0x0013;
    L_0x003f:
        r0 = r1;
        goto L_0x001d;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.common.api.internal.zzar.zzb(com.google.android.gms.common.ConnectionResult, com.google.android.gms.common.api.Api, boolean):void");
    }

    private final void zzbf(boolean z) {
        if (this.zzflj != null) {
            if (this.zzflj.isConnected() && z) {
                this.zzflj.zzbbt();
            }
            this.zzflj.disconnect();
            this.zzfln = null;
        }
    }

    private final boolean zzbq(int i) {
        if (this.zzflf == i) {
            return true;
        }
        Log.w("GoogleApiClientConnecting", this.zzflb.zzfjo.zzahj());
        String valueOf = String.valueOf(this);
        Log.w("GoogleApiClientConnecting", new StringBuilder(String.valueOf(valueOf).length() + 23).append("Unexpected callback in ").append(valueOf).toString());
        Log.w("GoogleApiClientConnecting", "mRemainingConnections=" + this.zzflg);
        valueOf = zzbr(this.zzflf);
        String zzbr = zzbr(i);
        Log.wtf("GoogleApiClientConnecting", new StringBuilder((String.valueOf(valueOf).length() + 70) + String.valueOf(zzbr).length()).append("GoogleApiClient connecting is in step ").append(valueOf).append(" but received callback for step ").append(zzbr).toString(), new Exception());
        zze(new ConnectionResult(8, null));
        return false;
    }

    private static String zzbr(int i) {
        switch (i) {
            case 0:
                return "STEP_SERVICE_BINDINGS_AND_SIGN_IN";
            case 1:
                return "STEP_GETTING_REMOTE_SERVICE";
            default:
                return "UNKNOWN";
        }
    }

    private final boolean zzd(ConnectionResult connectionResult) {
        return this.zzflk && !connectionResult.hasResolution();
    }

    private final void zze(ConnectionResult connectionResult) {
        zzahd();
        zzbf(!connectionResult.hasResolution());
        this.zzflb.zzg(connectionResult);
        this.zzflb.zzfna.zzc(connectionResult);
    }

    public final void begin() {
        this.zzflb.zzfmw.clear();
        this.zzfll = false;
        this.zzfkr = null;
        this.zzflf = 0;
        this.zzflk = true;
        this.zzflm = false;
        this.zzflo = false;
        Map hashMap = new HashMap();
        int i = 0;
        for (Api api : this.zzfkg.keySet()) {
            Api.zze zze = (Api.zze) this.zzflb.zzfmh.get(api.zzafd());
            int i2 = api.zzafb().getPriority() == 1 ? 1 : 0;
            boolean booleanValue = ((Boolean) this.zzfkg.get(api)).booleanValue();
            if (zze.zzaaa()) {
                this.zzfll = true;
                if (booleanValue) {
                    this.zzfli.add(api.zzafd());
                } else {
                    this.zzflk = false;
                }
            }
            hashMap.put(zze, new zzat(this, api, booleanValue));
            i = i2 | i;
        }
        if (i != 0) {
            this.zzfll = false;
        }
        if (this.zzfll) {
            this.zzfkd.zzc(Integer.valueOf(System.identityHashCode(this.zzflb.zzfjo)));
            ConnectionCallbacks zzba = new zzba();
            this.zzflj = (zzcpm) this.zzfhg.zza(this.mContext, this.zzflb.zzfjo.getLooper(), this.zzfkd, this.zzfkd.zzajx(), zzba, zzba);
        }
        this.zzflg = this.zzflb.zzfmh.size();
        this.zzflq.add(zzbo.zzahm().submit(new zzau(this, hashMap)));
    }

    public final void connect() {
    }

    public final boolean disconnect() {
        zzahd();
        zzbf(true);
        this.zzflb.zzg(null);
        return true;
    }

    public final void onConnected(Bundle bundle) {
        if (zzbq(1)) {
            if (bundle != null) {
                this.zzflh.putAll(bundle);
            }
            if (zzagz()) {
                zzahb();
            }
        }
    }

    public final void onConnectionSuspended(int i) {
        zze(new ConnectionResult(8, null));
    }

    public final void zza(ConnectionResult connectionResult, Api<?> api, boolean z) {
        if (zzbq(1)) {
            zzb(connectionResult, api, z);
            if (zzagz()) {
                zzahb();
            }
        }
    }

    public final <A extends zzb, R extends Result, T extends zzm<R, A>> T zzd(T t) {
        this.zzflb.zzfjo.zzfkm.add(t);
        return t;
    }

    public final <A extends zzb, T extends zzm<? extends Result, A>> T zze(T t) {
        throw new IllegalStateException("GoogleApiClient is not connected yet.");
    }
}
