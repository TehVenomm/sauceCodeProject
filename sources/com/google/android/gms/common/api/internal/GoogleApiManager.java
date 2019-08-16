package com.google.android.gms.common.api.internal;

import android.app.Application;
import android.app.PendingIntent;
import android.content.Context;
import android.os.Bundle;
import android.os.DeadObjectException;
import android.os.Handler;
import android.os.Handler.Callback;
import android.os.HandlerThread;
import android.os.Looper;
import android.os.Message;
import android.os.RemoteException;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import android.support.p000v4.util.ArrayMap;
import android.support.p000v4.util.ArraySet;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.Feature;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.android.gms.common.annotation.KeepForSdk;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.AnyClient;
import com.google.android.gms.common.api.Api.ApiOptions;
import com.google.android.gms.common.api.Api.Client;
import com.google.android.gms.common.api.GoogleApi;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.UnsupportedApiCallException;
import com.google.android.gms.common.api.internal.BaseImplementation.ApiMethodImpl;
import com.google.android.gms.common.api.internal.ListenerHolder.ListenerKey;
import com.google.android.gms.common.internal.BaseGmsClient.ConnectionProgressReportCallbacks;
import com.google.android.gms.common.internal.GoogleApiAvailabilityCache;
import com.google.android.gms.common.internal.IAccountAccessor;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.internal.SimpleClientAdapter;
import com.google.android.gms.common.util.ArrayUtils;
import com.google.android.gms.common.util.PlatformVersion;
import com.google.android.gms.internal.base.zal;
import com.google.android.gms.signin.zad;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.TaskCompletionSource;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Queue;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.atomic.AtomicInteger;
import javax.annotation.concurrent.GuardedBy;

@KeepForSdk
public class GoogleApiManager implements Callback {
    /* access modifiers changed from: private */
    public static final Object lock = new Object();
    public static final Status zahw = new Status(4, "Sign-out occurred while this API call was in progress.");
    /* access modifiers changed from: private */
    public static final Status zahx = new Status(4, "The user must be signed in to make this API call.");
    @GuardedBy("lock")
    private static GoogleApiManager zaib;
    /* access modifiers changed from: private */
    public final Handler handler;
    /* access modifiers changed from: private */
    public long zahy = 5000;
    /* access modifiers changed from: private */
    public long zahz = 120000;
    /* access modifiers changed from: private */
    public long zaia = 10000;
    /* access modifiers changed from: private */
    public final Context zaic;
    /* access modifiers changed from: private */
    public final GoogleApiAvailability zaid;
    /* access modifiers changed from: private */
    public final GoogleApiAvailabilityCache zaie;
    private final AtomicInteger zaif = new AtomicInteger(1);
    private final AtomicInteger zaig = new AtomicInteger(0);
    /* access modifiers changed from: private */
    public final Map<zai<?>, zaa<?>> zaih = new ConcurrentHashMap(5, 0.75f, 1);
    /* access modifiers changed from: private */
    @GuardedBy("lock")
    public zaae zaii = null;
    /* access modifiers changed from: private */
    @GuardedBy("lock")
    public final Set<zai<?>> zaij = new ArraySet();
    private final Set<zai<?>> zaik = new ArraySet();

    public final class zaa<O extends ApiOptions> implements ConnectionCallbacks, OnConnectionFailedListener, zar {
        private final zai<O> zafp;
        private final Queue<zab> zaim = new LinkedList();
        /* access modifiers changed from: private */
        public final Client zain;
        private final AnyClient zaio;
        private final zaab zaip;
        private final Set<zak> zaiq = new HashSet();
        private final Map<ListenerKey<?>, zabw> zair = new HashMap();
        private final int zais;
        private final zace zait;
        private boolean zaiu;
        private final List<zab> zaiv = new ArrayList();
        private ConnectionResult zaiw = null;

        @WorkerThread
        public zaa(GoogleApi<O> googleApi) {
            this.zain = googleApi.zaa(GoogleApiManager.this.handler.getLooper(), this);
            if (this.zain instanceof SimpleClientAdapter) {
                this.zaio = ((SimpleClientAdapter) this.zain).getClient();
            } else {
                this.zaio = this.zain;
            }
            this.zafp = googleApi.zak();
            this.zaip = new zaab();
            this.zais = googleApi.getInstanceId();
            if (this.zain.requiresSignIn()) {
                this.zait = googleApi.zaa(GoogleApiManager.this.zaic, GoogleApiManager.this.handler);
            } else {
                this.zait = null;
            }
        }

        @Nullable
        @WorkerThread
        private final Feature zaa(@Nullable Feature[] featureArr) {
            if (featureArr == null || featureArr.length == 0) {
                return null;
            }
            Feature[] availableFeatures = this.zain.getAvailableFeatures();
            if (availableFeatures == null) {
                availableFeatures = new Feature[0];
            }
            ArrayMap arrayMap = new ArrayMap(availableFeatures.length);
            for (Feature feature : availableFeatures) {
                arrayMap.put(feature.getName(), Long.valueOf(feature.getVersion()));
            }
            for (Feature feature2 : featureArr) {
                if (!arrayMap.containsKey(feature2.getName()) || ((Long) arrayMap.get(feature2.getName())).longValue() < feature2.getVersion()) {
                    return feature2;
                }
            }
            return null;
        }

        /* access modifiers changed from: private */
        @WorkerThread
        public final void zaa(zab zab) {
            if (!this.zaiv.contains(zab) || this.zaiu) {
                return;
            }
            if (!this.zain.isConnected()) {
                connect();
            } else {
                zabi();
            }
        }

        /* access modifiers changed from: private */
        @WorkerThread
        public final void zab(zab zab) {
            if (this.zaiv.remove(zab)) {
                GoogleApiManager.this.handler.removeMessages(15, zab);
                GoogleApiManager.this.handler.removeMessages(16, zab);
                Feature zad = zab.zajb;
                ArrayList arrayList = new ArrayList(this.zaim.size());
                for (zab zab2 : this.zaim) {
                    if (zab2 instanceof zac) {
                        Feature[] zab3 = ((zac) zab2).zab(this);
                        if (zab3 != null && ArrayUtils.contains((T[]) zab3, zad)) {
                            arrayList.add(zab2);
                        }
                    }
                }
                ArrayList arrayList2 = arrayList;
                int size = arrayList2.size();
                int i = 0;
                while (i < size) {
                    Object obj = arrayList2.get(i);
                    i++;
                    zab zab4 = (zab) obj;
                    this.zaim.remove(zab4);
                    zab4.zaa((RuntimeException) new UnsupportedApiCallException(zad));
                }
            }
        }

        @WorkerThread
        private final boolean zab(zab zab) {
            if (!(zab instanceof zac)) {
                zac(zab);
                return true;
            }
            zac zac = (zac) zab;
            Feature zaa = zaa(zac.zab(this));
            if (zaa == null) {
                zac(zab);
                return true;
            }
            if (zac.zac(this)) {
                zab zab2 = new zab(this.zafp, zaa, null);
                int indexOf = this.zaiv.indexOf(zab2);
                if (indexOf >= 0) {
                    zab zab3 = (zab) this.zaiv.get(indexOf);
                    GoogleApiManager.this.handler.removeMessages(15, zab3);
                    GoogleApiManager.this.handler.sendMessageDelayed(Message.obtain(GoogleApiManager.this.handler, 15, zab3), GoogleApiManager.this.zahy);
                } else {
                    this.zaiv.add(zab2);
                    GoogleApiManager.this.handler.sendMessageDelayed(Message.obtain(GoogleApiManager.this.handler, 15, zab2), GoogleApiManager.this.zahy);
                    GoogleApiManager.this.handler.sendMessageDelayed(Message.obtain(GoogleApiManager.this.handler, 16, zab2), GoogleApiManager.this.zahz);
                    ConnectionResult connectionResult = new ConnectionResult(2, null);
                    if (!zah(connectionResult)) {
                        GoogleApiManager.this.zac(connectionResult, this.zais);
                    }
                }
            } else {
                zac.zaa((RuntimeException) new UnsupportedApiCallException(zaa));
            }
            return false;
        }

        /* access modifiers changed from: private */
        @WorkerThread
        public final void zabg() {
            zabl();
            zai(ConnectionResult.RESULT_SUCCESS);
            zabn();
            Iterator it = this.zair.values().iterator();
            while (it.hasNext()) {
                zabw zabw = (zabw) it.next();
                if (zaa(zabw.zajw.getRequiredFeatures()) != null) {
                    it.remove();
                } else {
                    try {
                        zabw.zajw.registerListener(this.zaio, new TaskCompletionSource());
                    } catch (DeadObjectException e) {
                        onConnectionSuspended(1);
                        this.zain.disconnect();
                    } catch (RemoteException e2) {
                        it.remove();
                    }
                }
            }
            zabi();
            zabo();
        }

        /* access modifiers changed from: private */
        @WorkerThread
        public final void zabh() {
            zabl();
            this.zaiu = true;
            this.zaip.zaai();
            GoogleApiManager.this.handler.sendMessageDelayed(Message.obtain(GoogleApiManager.this.handler, 9, this.zafp), GoogleApiManager.this.zahy);
            GoogleApiManager.this.handler.sendMessageDelayed(Message.obtain(GoogleApiManager.this.handler, 11, this.zafp), GoogleApiManager.this.zahz);
            GoogleApiManager.this.zaie.flush();
        }

        @WorkerThread
        private final void zabi() {
            ArrayList arrayList = new ArrayList(this.zaim);
            int size = arrayList.size();
            int i = 0;
            while (i < size) {
                Object obj = arrayList.get(i);
                i++;
                zab zab = (zab) obj;
                if (!this.zain.isConnected()) {
                    return;
                }
                if (zab(zab)) {
                    this.zaim.remove(zab);
                }
            }
        }

        @WorkerThread
        private final void zabn() {
            if (this.zaiu) {
                GoogleApiManager.this.handler.removeMessages(11, this.zafp);
                GoogleApiManager.this.handler.removeMessages(9, this.zafp);
                this.zaiu = false;
            }
        }

        private final void zabo() {
            GoogleApiManager.this.handler.removeMessages(12, this.zafp);
            GoogleApiManager.this.handler.sendMessageDelayed(GoogleApiManager.this.handler.obtainMessage(12, this.zafp), GoogleApiManager.this.zaia);
        }

        @WorkerThread
        private final void zac(zab zab) {
            zab.zaa(this.zaip, requiresSignIn());
            try {
                zab.zaa(this);
            } catch (DeadObjectException e) {
                onConnectionSuspended(1);
                this.zain.disconnect();
            }
        }

        /* access modifiers changed from: private */
        @WorkerThread
        public final boolean zac(boolean z) {
            Preconditions.checkHandlerThread(GoogleApiManager.this.handler);
            if (!this.zain.isConnected() || this.zair.size() != 0) {
                return false;
            }
            if (!this.zaip.zaag()) {
                this.zain.disconnect();
                return true;
            } else if (!z) {
                return false;
            } else {
                zabo();
                return false;
            }
        }

        @WorkerThread
        private final boolean zah(@NonNull ConnectionResult connectionResult) {
            boolean z;
            synchronized (GoogleApiManager.lock) {
                if (GoogleApiManager.this.zaii == null || !GoogleApiManager.this.zaij.contains(this.zafp)) {
                    z = false;
                } else {
                    GoogleApiManager.this.zaii.zab(connectionResult, this.zais);
                    z = true;
                }
            }
            return z;
        }

        @WorkerThread
        private final void zai(ConnectionResult connectionResult) {
            for (zak zak : this.zaiq) {
                String str = null;
                if (Objects.equal(connectionResult, ConnectionResult.RESULT_SUCCESS)) {
                    str = this.zain.getEndpointPackageName();
                }
                zak.zaa(this.zafp, connectionResult, str);
            }
            this.zaiq.clear();
        }

        @WorkerThread
        public final void connect() {
            Preconditions.checkHandlerThread(GoogleApiManager.this.handler);
            if (!this.zain.isConnected() && !this.zain.isConnecting()) {
                int clientAvailability = GoogleApiManager.this.zaie.getClientAvailability(GoogleApiManager.this.zaic, this.zain);
                if (clientAvailability != 0) {
                    onConnectionFailed(new ConnectionResult(clientAvailability, null));
                    return;
                }
                zac zac = new zac(this.zain, this.zafp);
                if (this.zain.requiresSignIn()) {
                    this.zait.zaa((zach) zac);
                }
                this.zain.connect(zac);
            }
        }

        public final int getInstanceId() {
            return this.zais;
        }

        /* access modifiers changed from: 0000 */
        public final boolean isConnected() {
            return this.zain.isConnected();
        }

        public final void onConnected(@Nullable Bundle bundle) {
            if (Looper.myLooper() == GoogleApiManager.this.handler.getLooper()) {
                zabg();
            } else {
                GoogleApiManager.this.handler.post(new zabj(this));
            }
        }

        @WorkerThread
        public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
            Preconditions.checkHandlerThread(GoogleApiManager.this.handler);
            if (this.zait != null) {
                this.zait.zabs();
            }
            zabl();
            GoogleApiManager.this.zaie.flush();
            zai(connectionResult);
            if (connectionResult.getErrorCode() == 4) {
                zac(GoogleApiManager.zahx);
            } else if (this.zaim.isEmpty()) {
                this.zaiw = connectionResult;
            } else if (!zah(connectionResult) && !GoogleApiManager.this.zac(connectionResult, this.zais)) {
                if (connectionResult.getErrorCode() == 18) {
                    this.zaiu = true;
                }
                if (this.zaiu) {
                    GoogleApiManager.this.handler.sendMessageDelayed(Message.obtain(GoogleApiManager.this.handler, 9, this.zafp), GoogleApiManager.this.zahy);
                    return;
                }
                String zan = this.zafp.zan();
                zac(new Status(17, new StringBuilder(String.valueOf(zan).length() + 38).append("API: ").append(zan).append(" is not available on this device.").toString()));
            }
        }

        public final void onConnectionSuspended(int i) {
            if (Looper.myLooper() == GoogleApiManager.this.handler.getLooper()) {
                zabh();
            } else {
                GoogleApiManager.this.handler.post(new zabk(this));
            }
        }

        public final boolean requiresSignIn() {
            return this.zain.requiresSignIn();
        }

        @WorkerThread
        public final void resume() {
            Preconditions.checkHandlerThread(GoogleApiManager.this.handler);
            if (this.zaiu) {
                connect();
            }
        }

        public final void zaa(ConnectionResult connectionResult, Api<?> api, boolean z) {
            if (Looper.myLooper() == GoogleApiManager.this.handler.getLooper()) {
                onConnectionFailed(connectionResult);
            } else {
                GoogleApiManager.this.handler.post(new zabl(this, connectionResult));
            }
        }

        @WorkerThread
        public final void zaa(zab zab) {
            Preconditions.checkHandlerThread(GoogleApiManager.this.handler);
            if (!this.zain.isConnected()) {
                this.zaim.add(zab);
                if (this.zaiw == null || !this.zaiw.hasResolution()) {
                    connect();
                } else {
                    onConnectionFailed(this.zaiw);
                }
            } else if (zab(zab)) {
                zabo();
            } else {
                this.zaim.add(zab);
            }
        }

        @WorkerThread
        public final void zaa(zak zak) {
            Preconditions.checkHandlerThread(GoogleApiManager.this.handler);
            this.zaiq.add(zak);
        }

        public final Client zaab() {
            return this.zain;
        }

        @WorkerThread
        public final void zaav() {
            Preconditions.checkHandlerThread(GoogleApiManager.this.handler);
            if (this.zaiu) {
                zabn();
                zac(GoogleApiManager.this.zaid.isGooglePlayServicesAvailable(GoogleApiManager.this.zaic) == 18 ? new Status(8, "Connection timed out while waiting for Google Play services update to complete.") : new Status(8, "API failed to connect while resuming due to an unknown error."));
                this.zain.disconnect();
            }
        }

        @WorkerThread
        public final void zabj() {
            Preconditions.checkHandlerThread(GoogleApiManager.this.handler);
            zac(GoogleApiManager.zahw);
            this.zaip.zaah();
            for (ListenerKey zah : (ListenerKey[]) this.zair.keySet().toArray(new ListenerKey[this.zair.size()])) {
                zaa((zab) new zah(zah, new TaskCompletionSource()));
            }
            zai(new ConnectionResult(4));
            if (this.zain.isConnected()) {
                this.zain.onUserSignOut(new zabm(this));
            }
        }

        public final Map<ListenerKey<?>, zabw> zabk() {
            return this.zair;
        }

        @WorkerThread
        public final void zabl() {
            Preconditions.checkHandlerThread(GoogleApiManager.this.handler);
            this.zaiw = null;
        }

        @WorkerThread
        public final ConnectionResult zabm() {
            Preconditions.checkHandlerThread(GoogleApiManager.this.handler);
            return this.zaiw;
        }

        @WorkerThread
        public final boolean zabp() {
            return zac(true);
        }

        /* access modifiers changed from: 0000 */
        public final zad zabq() {
            if (this.zait == null) {
                return null;
            }
            return this.zait.zabq();
        }

        @WorkerThread
        public final void zac(Status status) {
            Preconditions.checkHandlerThread(GoogleApiManager.this.handler);
            for (zab zaa : this.zaim) {
                zaa.zaa(status);
            }
            this.zaim.clear();
        }

        @WorkerThread
        public final void zag(@NonNull ConnectionResult connectionResult) {
            Preconditions.checkHandlerThread(GoogleApiManager.this.handler);
            this.zain.disconnect();
            onConnectionFailed(connectionResult);
        }
    }

    private static final class zab {
        /* access modifiers changed from: private */
        public final zai<?> zaja;
        /* access modifiers changed from: private */
        public final Feature zajb;

        private zab(zai<?> zai, Feature feature) {
            this.zaja = zai;
            this.zajb = feature;
        }

        /* synthetic */ zab(zai zai, Feature feature, zabi zabi) {
            this(zai, feature);
        }

        public final boolean equals(Object obj) {
            if (obj == null || !(obj instanceof zab)) {
                return false;
            }
            zab zab = (zab) obj;
            return Objects.equal(this.zaja, zab.zaja) && Objects.equal(this.zajb, zab.zajb);
        }

        public final int hashCode() {
            return Objects.hashCode(this.zaja, this.zajb);
        }

        public final String toString() {
            return Objects.toStringHelper(this).add("key", this.zaja).add("feature", this.zajb).toString();
        }
    }

    private final class zac implements zach, ConnectionProgressReportCallbacks {
        /* access modifiers changed from: private */
        public final zai<?> zafp;
        /* access modifiers changed from: private */
        public final Client zain;
        private IAccountAccessor zajc = null;
        private Set<Scope> zajd = null;
        /* access modifiers changed from: private */
        public boolean zaje = false;

        public zac(Client client, zai<?> zai) {
            this.zain = client;
            this.zafp = zai;
        }

        /* access modifiers changed from: private */
        @WorkerThread
        public final void zabr() {
            if (this.zaje && this.zajc != null) {
                this.zain.getRemoteService(this.zajc, this.zajd);
            }
        }

        public final void onReportServiceBinding(@NonNull ConnectionResult connectionResult) {
            GoogleApiManager.this.handler.post(new zabo(this, connectionResult));
        }

        @WorkerThread
        public final void zaa(IAccountAccessor iAccountAccessor, Set<Scope> set) {
            if (iAccountAccessor == null || set == null) {
                Log.wtf("GoogleApiManager", "Received null response from onSignInSuccess", new Exception());
                zag(new ConnectionResult(4));
                return;
            }
            this.zajc = iAccountAccessor;
            this.zajd = set;
            zabr();
        }

        @WorkerThread
        public final void zag(ConnectionResult connectionResult) {
            ((zaa) GoogleApiManager.this.zaih.get(this.zafp)).zag(connectionResult);
        }
    }

    @KeepForSdk
    private GoogleApiManager(Context context, Looper looper, GoogleApiAvailability googleApiAvailability) {
        this.zaic = context;
        this.handler = new zal(looper, this);
        this.zaid = googleApiAvailability;
        this.zaie = new GoogleApiAvailabilityCache(googleApiAvailability);
        this.handler.sendMessage(this.handler.obtainMessage(6));
    }

    @KeepForSdk
    public static void reportSignOut() {
        synchronized (lock) {
            if (zaib != null) {
                GoogleApiManager googleApiManager = zaib;
                googleApiManager.zaig.incrementAndGet();
                googleApiManager.handler.sendMessageAtFrontOfQueue(googleApiManager.handler.obtainMessage(10));
            }
        }
    }

    public static GoogleApiManager zab(Context context) {
        GoogleApiManager googleApiManager;
        synchronized (lock) {
            if (zaib == null) {
                HandlerThread handlerThread = new HandlerThread("GoogleApiHandler", 9);
                handlerThread.start();
                zaib = new GoogleApiManager(context.getApplicationContext(), handlerThread.getLooper(), GoogleApiAvailability.getInstance());
            }
            googleApiManager = zaib;
        }
        return googleApiManager;
    }

    @WorkerThread
    private final void zab(GoogleApi<?> googleApi) {
        zai zak = googleApi.zak();
        zaa zaa2 = (zaa) this.zaih.get(zak);
        if (zaa2 == null) {
            zaa2 = new zaa(googleApi);
            this.zaih.put(zak, zaa2);
        }
        if (zaa2.requiresSignIn()) {
            this.zaik.add(zak);
        }
        zaa2.connect();
    }

    public static GoogleApiManager zabc() {
        GoogleApiManager googleApiManager;
        synchronized (lock) {
            Preconditions.checkNotNull(zaib, "Must guarantee manager is non-null before using getInstance");
            googleApiManager = zaib;
        }
        return googleApiManager;
    }

    @WorkerThread
    public boolean handleMessage(Message message) {
        zaa zaa2;
        switch (message.what) {
            case 1:
                this.zaia = ((Boolean) message.obj).booleanValue() ? 10000 : 300000;
                this.handler.removeMessages(12);
                for (zai obtainMessage : this.zaih.keySet()) {
                    this.handler.sendMessageDelayed(this.handler.obtainMessage(12, obtainMessage), this.zaia);
                }
                break;
            case 2:
                zak zak = (zak) message.obj;
                Iterator it = zak.zap().iterator();
                while (true) {
                    if (!it.hasNext()) {
                        break;
                    } else {
                        zai zai = (zai) it.next();
                        zaa zaa3 = (zaa) this.zaih.get(zai);
                        if (zaa3 == null) {
                            zak.zaa(zai, new ConnectionResult(13), null);
                            break;
                        } else if (zaa3.isConnected()) {
                            zak.zaa(zai, ConnectionResult.RESULT_SUCCESS, zaa3.zaab().getEndpointPackageName());
                        } else if (zaa3.zabm() != null) {
                            zak.zaa(zai, zaa3.zabm(), null);
                        } else {
                            zaa3.zaa(zak);
                            zaa3.connect();
                        }
                    }
                }
            case 3:
                for (zaa zaa4 : this.zaih.values()) {
                    zaa4.zabl();
                    zaa4.connect();
                }
                break;
            case 4:
            case 8:
            case 13:
                zabv zabv = (zabv) message.obj;
                zaa zaa5 = (zaa) this.zaih.get(zabv.zajs.zak());
                if (zaa5 == null) {
                    zab(zabv.zajs);
                    zaa5 = (zaa) this.zaih.get(zabv.zajs.zak());
                }
                if (zaa5.requiresSignIn() && this.zaig.get() != zabv.zajr) {
                    zabv.zajq.zaa(zahw);
                    zaa5.zabj();
                    break;
                } else {
                    zaa5.zaa(zabv.zajq);
                    break;
                }
                break;
            case 5:
                int i = message.arg1;
                ConnectionResult connectionResult = (ConnectionResult) message.obj;
                Iterator it2 = this.zaih.values().iterator();
                while (true) {
                    if (it2.hasNext()) {
                        zaa2 = (zaa) it2.next();
                        if (zaa2.getInstanceId() == i) {
                        }
                    } else {
                        zaa2 = null;
                    }
                }
                if (zaa2 == null) {
                    Log.wtf("GoogleApiManager", "Could not find API instance " + i + " while trying to fail enqueued calls.", new Exception());
                    break;
                } else {
                    String errorString = this.zaid.getErrorString(connectionResult.getErrorCode());
                    String errorMessage = connectionResult.getErrorMessage();
                    zaa2.zac(new Status(17, new StringBuilder(String.valueOf(errorString).length() + 69 + String.valueOf(errorMessage).length()).append("Error resolution was canceled by the user, original error message: ").append(errorString).append(": ").append(errorMessage).toString()));
                    break;
                }
            case 6:
                if (PlatformVersion.isAtLeastIceCreamSandwich() && (this.zaic.getApplicationContext() instanceof Application)) {
                    BackgroundDetector.initialize((Application) this.zaic.getApplicationContext());
                    BackgroundDetector.getInstance().addListener(new zabi(this));
                    if (!BackgroundDetector.getInstance().readCurrentStateIfPossible(true)) {
                        this.zaia = 300000;
                        break;
                    }
                }
                break;
            case 7:
                zab((GoogleApi) message.obj);
                break;
            case 9:
                if (this.zaih.containsKey(message.obj)) {
                    ((zaa) this.zaih.get(message.obj)).resume();
                    break;
                }
                break;
            case 10:
                for (zai remove : this.zaik) {
                    ((zaa) this.zaih.remove(remove)).zabj();
                }
                this.zaik.clear();
                break;
            case 11:
                if (this.zaih.containsKey(message.obj)) {
                    ((zaa) this.zaih.get(message.obj)).zaav();
                    break;
                }
                break;
            case 12:
                if (this.zaih.containsKey(message.obj)) {
                    ((zaa) this.zaih.get(message.obj)).zabp();
                    break;
                }
                break;
            case 14:
                zaaf zaaf = (zaaf) message.obj;
                zai zak2 = zaaf.zak();
                if (this.zaih.containsKey(zak2)) {
                    zaaf.zaal().setResult(Boolean.valueOf(((zaa) this.zaih.get(zak2)).zac(false)));
                    break;
                } else {
                    zaaf.zaal().setResult(Boolean.valueOf(false));
                    break;
                }
            case 15:
                zab zab2 = (zab) message.obj;
                if (this.zaih.containsKey(zab2.zaja)) {
                    ((zaa) this.zaih.get(zab2.zaja)).zaa(zab2);
                    break;
                }
                break;
            case 16:
                zab zab3 = (zab) message.obj;
                if (this.zaih.containsKey(zab3.zaja)) {
                    ((zaa) this.zaih.get(zab3.zaja)).zab(zab3);
                    break;
                }
                break;
            default:
                Log.w("GoogleApiManager", "Unknown message id: " + message.what);
                return false;
        }
        return true;
    }

    /* access modifiers changed from: 0000 */
    public final void maybeSignOut() {
        this.zaig.incrementAndGet();
        this.handler.sendMessage(this.handler.obtainMessage(10));
    }

    /* access modifiers changed from: 0000 */
    public final PendingIntent zaa(zai<?> zai, int i) {
        zaa zaa2 = (zaa) this.zaih.get(zai);
        if (zaa2 == null) {
            return null;
        }
        zad zabq = zaa2.zabq();
        if (zabq == null) {
            return null;
        }
        return PendingIntent.getActivity(this.zaic, i, zabq.getSignInIntent(), 134217728);
    }

    public final <O extends ApiOptions> Task<Boolean> zaa(@NonNull GoogleApi<O> googleApi, @NonNull ListenerKey<?> listenerKey) {
        TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        this.handler.sendMessage(this.handler.obtainMessage(13, new zabv(new zah(listenerKey, taskCompletionSource), this.zaig.get(), googleApi)));
        return taskCompletionSource.getTask();
    }

    public final <O extends ApiOptions> Task<Void> zaa(@NonNull GoogleApi<O> googleApi, @NonNull RegisterListenerMethod<AnyClient, ?> registerListenerMethod, @NonNull UnregisterListenerMethod<AnyClient, ?> unregisterListenerMethod) {
        TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        this.handler.sendMessage(this.handler.obtainMessage(8, new zabv(new zaf(new zabw(registerListenerMethod, unregisterListenerMethod), taskCompletionSource), this.zaig.get(), googleApi)));
        return taskCompletionSource.getTask();
    }

    public final Task<Map<zai<?>, String>> zaa(Iterable<? extends GoogleApi<?>> iterable) {
        zak zak = new zak(iterable);
        this.handler.sendMessage(this.handler.obtainMessage(2, zak));
        return zak.getTask();
    }

    public final void zaa(ConnectionResult connectionResult, int i) {
        if (!zac(connectionResult, i)) {
            this.handler.sendMessage(this.handler.obtainMessage(5, i, 0, connectionResult));
        }
    }

    public final void zaa(GoogleApi<?> googleApi) {
        this.handler.sendMessage(this.handler.obtainMessage(7, googleApi));
    }

    public final <O extends ApiOptions> void zaa(GoogleApi<O> googleApi, int i, ApiMethodImpl<? extends Result, AnyClient> apiMethodImpl) {
        this.handler.sendMessage(this.handler.obtainMessage(4, new zabv(new zae(i, apiMethodImpl), this.zaig.get(), googleApi)));
    }

    public final <O extends ApiOptions, ResultT> void zaa(GoogleApi<O> googleApi, int i, TaskApiCall<AnyClient, ResultT> taskApiCall, TaskCompletionSource<ResultT> taskCompletionSource, StatusExceptionMapper statusExceptionMapper) {
        this.handler.sendMessage(this.handler.obtainMessage(4, new zabv(new zag(i, taskApiCall, taskCompletionSource, statusExceptionMapper), this.zaig.get(), googleApi)));
    }

    public final void zaa(@NonNull zaae zaae) {
        synchronized (lock) {
            if (this.zaii != zaae) {
                this.zaii = zaae;
                this.zaij.clear();
            }
            this.zaij.addAll(zaae.zaaj());
        }
    }

    /* access modifiers changed from: 0000 */
    public final void zab(@NonNull zaae zaae) {
        synchronized (lock) {
            if (this.zaii == zaae) {
                this.zaii = null;
                this.zaij.clear();
            }
        }
    }

    public final int zabd() {
        return this.zaif.getAndIncrement();
    }

    public final Task<Boolean> zac(GoogleApi<?> googleApi) {
        zaaf zaaf = new zaaf(googleApi.zak());
        this.handler.sendMessage(this.handler.obtainMessage(14, zaaf));
        return zaaf.zaal().getTask();
    }

    /* access modifiers changed from: 0000 */
    public final boolean zac(ConnectionResult connectionResult, int i) {
        return this.zaid.zaa(this.zaic, connectionResult, i);
    }

    public final void zao() {
        this.handler.sendMessage(this.handler.obtainMessage(3));
    }
}
