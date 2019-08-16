package com.google.android.gms.common.api.internal;

import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GoogleApiAvailabilityLight;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.AbstractClientBuilder;
import com.google.android.gms.common.api.Api.AnyClient;
import com.google.android.gms.common.api.Api.AnyClientKey;
import com.google.android.gms.common.api.Api.Client;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.api.internal.BaseImplementation.ApiMethodImpl;
import com.google.android.gms.common.internal.ClientSettings;
import com.google.android.gms.common.internal.ClientSettings.OptionalApiSettings;
import com.google.android.gms.common.internal.IAccountAccessor;
import com.google.android.gms.common.internal.ResolveAccountResponse;
import com.google.android.gms.signin.SignInOptions;
import com.google.android.gms.signin.internal.zaj;
import com.google.android.gms.signin.zad;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.Future;
import java.util.concurrent.locks.Lock;
import javax.annotation.concurrent.GuardedBy;

public final class zaak implements zabd {
    /* access modifiers changed from: private */
    public final Context mContext;
    private final AbstractClientBuilder<? extends zad, SignInOptions> zacd;
    /* access modifiers changed from: private */
    public final Lock zaen;
    private final ClientSettings zaes;
    private final Map<Api<?>, Boolean> zaev;
    /* access modifiers changed from: private */
    public final GoogleApiAvailabilityLight zaex;
    private ConnectionResult zafg;
    /* access modifiers changed from: private */
    public final zabe zafs;
    private int zafv;
    private int zafw = 0;
    private int zafx;
    private final Bundle zafy = new Bundle();
    private final Set<AnyClientKey> zafz = new HashSet();
    /* access modifiers changed from: private */
    public zad zaga;
    private boolean zagb;
    /* access modifiers changed from: private */
    public boolean zagc;
    private boolean zagd;
    /* access modifiers changed from: private */
    public IAccountAccessor zage;
    private boolean zagf;
    private boolean zagg;
    private ArrayList<Future<?>> zagh = new ArrayList<>();

    public zaak(zabe zabe, ClientSettings clientSettings, Map<Api<?>, Boolean> map, GoogleApiAvailabilityLight googleApiAvailabilityLight, AbstractClientBuilder<? extends zad, SignInOptions> abstractClientBuilder, Lock lock, Context context) {
        this.zafs = zabe;
        this.zaes = clientSettings;
        this.zaev = map;
        this.zaex = googleApiAvailabilityLight;
        this.zacd = abstractClientBuilder;
        this.zaen = lock;
        this.mContext = context;
    }

    /* access modifiers changed from: private */
    @GuardedBy("mLock")
    public final void zaa(zaj zaj) {
        if (zac(0)) {
            ConnectionResult connectionResult = zaj.getConnectionResult();
            if (connectionResult.isSuccess()) {
                ResolveAccountResponse zacw = zaj.zacw();
                ConnectionResult connectionResult2 = zacw.getConnectionResult();
                if (!connectionResult2.isSuccess()) {
                    String valueOf = String.valueOf(connectionResult2);
                    Log.wtf("GoogleApiClientConnecting", new StringBuilder(String.valueOf(valueOf).length() + 48).append("Sign-in succeeded with resolve account failure: ").append(valueOf).toString(), new Exception());
                    zae(connectionResult2);
                    return;
                }
                this.zagd = true;
                this.zage = zacw.getAccountAccessor();
                this.zagf = zacw.getSaveDefaultAccount();
                this.zagg = zacw.isFromCrossClientAuth();
                zaap();
            } else if (zad(connectionResult)) {
                zaar();
                zaap();
            } else {
                zae(connectionResult);
            }
        }
    }

    /* access modifiers changed from: private */
    @GuardedBy("mLock")
    public final boolean zaao() {
        this.zafx--;
        if (this.zafx > 0) {
            return false;
        }
        if (this.zafx < 0) {
            Log.w("GoogleApiClientConnecting", this.zafs.zaed.zaay());
            Log.wtf("GoogleApiClientConnecting", "GoogleApiClient received too many callbacks for the given step. Clients may be in an unexpected state; GoogleApiClient will now disconnect.", new Exception());
            zae(new ConnectionResult(8, null));
            return false;
        } else if (this.zafg == null) {
            return true;
        } else {
            this.zafs.zahr = this.zafv;
            zae(this.zafg);
            return false;
        }
    }

    /* access modifiers changed from: private */
    @GuardedBy("mLock")
    public final void zaap() {
        if (this.zafx == 0) {
            if (!this.zagc || this.zagd) {
                ArrayList arrayList = new ArrayList();
                this.zafw = 1;
                this.zafx = this.zafs.zagy.size();
                for (AnyClientKey anyClientKey : this.zafs.zagy.keySet()) {
                    if (!this.zafs.zaho.containsKey(anyClientKey)) {
                        arrayList.add((Client) this.zafs.zagy.get(anyClientKey));
                    } else if (zaao()) {
                        zaaq();
                    }
                }
                if (!arrayList.isEmpty()) {
                    this.zagh.add(zabh.zabb().submit(new zaaq(this, arrayList)));
                }
            }
        }
    }

    @GuardedBy("mLock")
    private final void zaaq() {
        this.zafs.zaba();
        zabh.zabb().execute(new zaal(this));
        if (this.zaga != null) {
            if (this.zagf) {
                this.zaga.zaa(this.zage, this.zagg);
            }
            zab(false);
        }
        for (AnyClientKey anyClientKey : this.zafs.zaho.keySet()) {
            ((Client) this.zafs.zagy.get(anyClientKey)).disconnect();
        }
        this.zafs.zahs.zab(this.zafy.isEmpty() ? null : this.zafy);
    }

    /* access modifiers changed from: private */
    @GuardedBy("mLock")
    public final void zaar() {
        this.zagc = false;
        this.zafs.zaed.zagz = Collections.emptySet();
        for (AnyClientKey anyClientKey : this.zafz) {
            if (!this.zafs.zaho.containsKey(anyClientKey)) {
                this.zafs.zaho.put(anyClientKey, new ConnectionResult(17, null));
            }
        }
    }

    private final void zaas() {
        ArrayList arrayList = this.zagh;
        int size = arrayList.size();
        int i = 0;
        while (i < size) {
            Object obj = arrayList.get(i);
            i++;
            ((Future) obj).cancel(true);
        }
        this.zagh.clear();
    }

    /* access modifiers changed from: private */
    public final Set<Scope> zaat() {
        if (this.zaes == null) {
            return Collections.emptySet();
        }
        HashSet hashSet = new HashSet(this.zaes.getRequiredScopes());
        Map optionalApiSettings = this.zaes.getOptionalApiSettings();
        for (Api api : optionalApiSettings.keySet()) {
            if (!this.zafs.zaho.containsKey(api.getClientKey())) {
                hashSet.addAll(((OptionalApiSettings) optionalApiSettings.get(api)).mScopes);
            }
        }
        return hashSet;
    }

    /* access modifiers changed from: private */
    /* JADX WARNING: Code restructure failed: missing block: B:18:0x003f, code lost:
        r0 = false;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:5:0x0013, code lost:
        if (r2 != false) goto L_0x0015;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:9:0x001b, code lost:
        if (r3 >= r5.zafv) goto L_0x003f;
     */
    @javax.annotation.concurrent.GuardedBy("mLock")
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void zab(com.google.android.gms.common.ConnectionResult r6, com.google.android.gms.common.api.Api<?> r7, boolean r8) {
        /*
            r5 = this;
            r1 = 0
            r0 = 1
            com.google.android.gms.common.api.Api$BaseClientBuilder r2 = r7.zah()
            int r3 = r2.getPriority()
            if (r8 == 0) goto L_0x0015
            boolean r2 = r6.hasResolution()
            if (r2 == 0) goto L_0x002f
            r2 = r0
        L_0x0013:
            if (r2 == 0) goto L_0x003f
        L_0x0015:
            com.google.android.gms.common.ConnectionResult r2 = r5.zafg
            if (r2 == 0) goto L_0x001d
            int r2 = r5.zafv
            if (r3 >= r2) goto L_0x003f
        L_0x001d:
            if (r0 == 0) goto L_0x0023
            r5.zafg = r6
            r5.zafv = r3
        L_0x0023:
            com.google.android.gms.common.api.internal.zabe r0 = r5.zafs
            java.util.Map<com.google.android.gms.common.api.Api$AnyClientKey<?>, com.google.android.gms.common.ConnectionResult> r0 = r0.zaho
            com.google.android.gms.common.api.Api$AnyClientKey r1 = r7.getClientKey()
            r0.put(r1, r6)
            return
        L_0x002f:
            com.google.android.gms.common.GoogleApiAvailabilityLight r2 = r5.zaex
            int r4 = r6.getErrorCode()
            android.content.Intent r2 = r2.getErrorResolutionIntent(r4)
            if (r2 == 0) goto L_0x003d
            r2 = r0
            goto L_0x0013
        L_0x003d:
            r2 = r1
            goto L_0x0013
        L_0x003f:
            r0 = r1
            goto L_0x001d
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.common.api.internal.zaak.zab(com.google.android.gms.common.ConnectionResult, com.google.android.gms.common.api.Api, boolean):void");
    }

    private final void zab(boolean z) {
        if (this.zaga != null) {
            if (this.zaga.isConnected() && z) {
                this.zaga.zacv();
            }
            this.zaga.disconnect();
            this.zage = null;
        }
    }

    /* access modifiers changed from: private */
    @GuardedBy("mLock")
    public final boolean zac(int i) {
        if (this.zafw == i) {
            return true;
        }
        Log.w("GoogleApiClientConnecting", this.zafs.zaed.zaay());
        String valueOf = String.valueOf(this);
        Log.w("GoogleApiClientConnecting", new StringBuilder(String.valueOf(valueOf).length() + 23).append("Unexpected callback in ").append(valueOf).toString());
        Log.w("GoogleApiClientConnecting", "mRemainingConnections=" + this.zafx);
        String zad = zad(this.zafw);
        String zad2 = zad(i);
        Log.wtf("GoogleApiClientConnecting", new StringBuilder(String.valueOf(zad).length() + 70 + String.valueOf(zad2).length()).append("GoogleApiClient connecting is in step ").append(zad).append(" but received callback for step ").append(zad2).toString(), new Exception());
        zae(new ConnectionResult(8, null));
        return false;
    }

    private static String zad(int i) {
        switch (i) {
            case 0:
                return "STEP_SERVICE_BINDINGS_AND_SIGN_IN";
            case 1:
                return "STEP_GETTING_REMOTE_SERVICE";
            default:
                return "UNKNOWN";
        }
    }

    /* access modifiers changed from: private */
    @GuardedBy("mLock")
    public final boolean zad(ConnectionResult connectionResult) {
        return this.zagb && !connectionResult.hasResolution();
    }

    /* access modifiers changed from: private */
    @GuardedBy("mLock")
    public final void zae(ConnectionResult connectionResult) {
        zaas();
        zab(!connectionResult.hasResolution());
        this.zafs.zaf(connectionResult);
        this.zafs.zahs.zac(connectionResult);
    }

    public final void begin() {
        this.zafs.zaho.clear();
        this.zagc = false;
        this.zafg = null;
        this.zafw = 0;
        this.zagb = true;
        this.zagd = false;
        this.zagf = false;
        HashMap hashMap = new HashMap();
        boolean z = false;
        for (Api api : this.zaev.keySet()) {
            Client client = (Client) this.zafs.zagy.get(api.getClientKey());
            boolean z2 = api.zah().getPriority() == 1;
            boolean booleanValue = ((Boolean) this.zaev.get(api)).booleanValue();
            if (client.requiresSignIn()) {
                this.zagc = true;
                if (booleanValue) {
                    this.zafz.add(api.getClientKey());
                } else {
                    this.zagb = false;
                }
            }
            hashMap.put(client, new zaam(this, api, booleanValue));
            z = z2 | z;
        }
        if (z) {
            this.zagc = false;
        }
        if (this.zagc) {
            this.zaes.setClientSessionId(Integer.valueOf(System.identityHashCode(this.zafs.zaed)));
            zaat zaat = new zaat(this, null);
            this.zaga = (zad) this.zacd.buildClient(this.mContext, this.zafs.zaed.getLooper(), this.zaes, this.zaes.getSignInOptions(), zaat, zaat);
        }
        this.zafx = this.zafs.zagy.size();
        this.zagh.add(zabh.zabb().submit(new zaan(this, hashMap)));
    }

    public final void connect() {
    }

    public final boolean disconnect() {
        zaas();
        zab(true);
        this.zafs.zaf(null);
        return true;
    }

    public final <A extends AnyClient, R extends Result, T extends ApiMethodImpl<R, A>> T enqueue(T t) {
        this.zafs.zaed.zafb.add(t);
        return t;
    }

    public final <A extends AnyClient, T extends ApiMethodImpl<? extends Result, A>> T execute(T t) {
        throw new IllegalStateException("GoogleApiClient is not connected yet.");
    }

    @GuardedBy("mLock")
    public final void onConnected(Bundle bundle) {
        if (zac(1)) {
            if (bundle != null) {
                this.zafy.putAll(bundle);
            }
            if (zaao()) {
                zaaq();
            }
        }
    }

    @GuardedBy("mLock")
    public final void onConnectionSuspended(int i) {
        zae(new ConnectionResult(8, null));
    }

    @GuardedBy("mLock")
    public final void zaa(ConnectionResult connectionResult, Api<?> api, boolean z) {
        if (zac(1)) {
            zab(connectionResult, api, z);
            if (zaao()) {
                zaaq();
            }
        }
    }
}
