package com.google.android.gms.common.api;

import android.accounts.Account;
import android.content.Context;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.FragmentActivity;
import android.support.v4.util.ArrayMap;
import android.view.View;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.android.gms.common.api.Api.ApiOptions;
import com.google.android.gms.common.api.Api.ApiOptions.HasOptions;
import com.google.android.gms.common.api.Api.ApiOptions.NotRequiredOptions;
import com.google.android.gms.common.api.Api.zza;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.Api.zzc;
import com.google.android.gms.common.api.Api.zzd;
import com.google.android.gms.common.api.Api.zze;
import com.google.android.gms.common.api.internal.zzbd;
import com.google.android.gms.common.api.internal.zzcf;
import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.common.api.internal.zzcv;
import com.google.android.gms.common.api.internal.zzdf;
import com.google.android.gms.common.api.internal.zzi;
import com.google.android.gms.common.api.internal.zzm;
import com.google.android.gms.common.api.internal.zzw;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.common.internal.zzs;
import com.google.android.gms.internal.zzcpj;
import com.google.android.gms.internal.zzcpm;
import com.google.android.gms.internal.zzcpn;
import java.io.FileDescriptor;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import java.util.WeakHashMap;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.locks.ReentrantLock;

public abstract class GoogleApiClient {
    public static final int SIGN_IN_MODE_OPTIONAL = 2;
    public static final int SIGN_IN_MODE_REQUIRED = 1;
    private static final Set<GoogleApiClient> zzfgu = Collections.newSetFromMap(new WeakHashMap());

    public static final class Builder {
        private final Context mContext;
        private Looper zzakl;
        private Account zzdva;
        private String zzdxd;
        private final Set<Scope> zzfgv;
        private final Set<Scope> zzfgw;
        private int zzfgx;
        private View zzfgy;
        private String zzfgz;
        private final Map<Api<?>, zzs> zzfha;
        private final Map<Api<?>, ApiOptions> zzfhb;
        private zzcf zzfhc;
        private int zzfhd;
        private OnConnectionFailedListener zzfhe;
        private GoogleApiAvailability zzfhf;
        private zza<? extends zzcpm, zzcpn> zzfhg;
        private final ArrayList<ConnectionCallbacks> zzfhh;
        private final ArrayList<OnConnectionFailedListener> zzfhi;
        private boolean zzfhj;

        public Builder(@NonNull Context context) {
            this.zzfgv = new HashSet();
            this.zzfgw = new HashSet();
            this.zzfha = new ArrayMap();
            this.zzfhb = new ArrayMap();
            this.zzfhd = -1;
            this.zzfhf = GoogleApiAvailability.getInstance();
            this.zzfhg = zzcpj.zzdwr;
            this.zzfhh = new ArrayList();
            this.zzfhi = new ArrayList();
            this.zzfhj = false;
            this.mContext = context;
            this.zzakl = context.getMainLooper();
            this.zzdxd = context.getPackageName();
            this.zzfgz = context.getClass().getName();
        }

        public Builder(@NonNull Context context, @NonNull ConnectionCallbacks connectionCallbacks, @NonNull OnConnectionFailedListener onConnectionFailedListener) {
            this(context);
            zzbp.zzb((Object) connectionCallbacks, (Object) "Must provide a connected listener");
            this.zzfhh.add(connectionCallbacks);
            zzbp.zzb((Object) onConnectionFailedListener, (Object) "Must provide a connection failed listener");
            this.zzfhi.add(onConnectionFailedListener);
        }

        private final <O extends ApiOptions> void zza(Api<O> api, O o, Scope... scopeArr) {
            Set hashSet = new HashSet(api.zzafb().zzn(o));
            for (Object add : scopeArr) {
                hashSet.add(add);
            }
            this.zzfha.put(api, new zzs(hashSet));
        }

        public final Builder addApi(@NonNull Api<? extends NotRequiredOptions> api) {
            zzbp.zzb((Object) api, (Object) "Api must not be null");
            this.zzfhb.put(api, null);
            Collection zzn = api.zzafb().zzn(null);
            this.zzfgw.addAll(zzn);
            this.zzfgv.addAll(zzn);
            return this;
        }

        public final <O extends HasOptions> Builder addApi(@NonNull Api<O> api, @NonNull O o) {
            zzbp.zzb((Object) api, (Object) "Api must not be null");
            zzbp.zzb((Object) o, (Object) "Null options are not permitted for this Api");
            this.zzfhb.put(api, o);
            Collection zzn = api.zzafb().zzn(o);
            this.zzfgw.addAll(zzn);
            this.zzfgv.addAll(zzn);
            return this;
        }

        public final <O extends HasOptions> Builder addApiIfAvailable(@NonNull Api<O> api, @NonNull O o, Scope... scopeArr) {
            zzbp.zzb((Object) api, (Object) "Api must not be null");
            zzbp.zzb((Object) o, (Object) "Null options are not permitted for this Api");
            this.zzfhb.put(api, o);
            zza(api, o, scopeArr);
            return this;
        }

        public final Builder addApiIfAvailable(@NonNull Api<? extends NotRequiredOptions> api, Scope... scopeArr) {
            zzbp.zzb((Object) api, (Object) "Api must not be null");
            this.zzfhb.put(api, null);
            zza(api, null, scopeArr);
            return this;
        }

        public final Builder addConnectionCallbacks(@NonNull ConnectionCallbacks connectionCallbacks) {
            zzbp.zzb((Object) connectionCallbacks, (Object) "Listener must not be null");
            this.zzfhh.add(connectionCallbacks);
            return this;
        }

        public final Builder addOnConnectionFailedListener(@NonNull OnConnectionFailedListener onConnectionFailedListener) {
            zzbp.zzb((Object) onConnectionFailedListener, (Object) "Listener must not be null");
            this.zzfhi.add(onConnectionFailedListener);
            return this;
        }

        public final Builder addScope(@NonNull Scope scope) {
            zzbp.zzb((Object) scope, (Object) "Scope must not be null");
            this.zzfgv.add(scope);
            return this;
        }

        public final GoogleApiClient build() {
            zzbp.zzb(!this.zzfhb.isEmpty(), (Object) "must call addApi() to add at least one API");
            zzq zzafq = zzafq();
            Map zzajt = zzafq.zzajt();
            Map arrayMap = new ArrayMap();
            Map arrayMap2 = new ArrayMap();
            ArrayList arrayList = new ArrayList();
            Api api = null;
            Object obj = null;
            for (Api api2 : this.zzfhb.keySet()) {
                Api api3;
                Object obj2 = this.zzfhb.get(api2);
                boolean z = zzajt.get(api2) != null;
                arrayMap.put(api2, Boolean.valueOf(z));
                ConnectionCallbacks zzw = new zzw(api2, z);
                arrayList.add(zzw);
                zzd zzafc = api2.zzafc();
                zze zza = zzafc.zza(this.mContext, this.zzakl, zzafq, obj2, zzw, zzw);
                arrayMap2.put(api2.zzafd(), zza);
                Object obj3 = zzafc.getPriority() == 1 ? obj2 != null ? 1 : null : obj;
                if (!zza.zzaak()) {
                    api3 = api;
                } else if (api != null) {
                    String name = api2.getName();
                    String name2 = api.getName();
                    throw new IllegalStateException(new StringBuilder((String.valueOf(name).length() + 21) + String.valueOf(name2).length()).append(name).append(" cannot be used with ").append(name2).toString());
                } else {
                    api3 = api2;
                }
                api = api3;
                obj = obj3;
            }
            if (api != null) {
                if (obj != null) {
                    name = api.getName();
                    throw new IllegalStateException(new StringBuilder(String.valueOf(name).length() + 82).append("With using ").append(name).append(", GamesOptions can only be specified within GoogleSignInOptions.Builder").toString());
                }
                zzbp.zza(this.zzdva == null, "Must not set an account in GoogleApiClient.Builder when using %s. Set account in GoogleSignInOptions.Builder instead", api.getName());
                zzbp.zza(this.zzfgv.equals(this.zzfgw), "Must not set scopes in GoogleApiClient.Builder when using %s. Set account in GoogleSignInOptions.Builder instead.", api.getName());
            }
            GoogleApiClient zzbd = new zzbd(this.mContext, new ReentrantLock(), this.zzakl, zzafq, this.zzfhf, this.zzfhg, arrayMap, this.zzfhh, this.zzfhi, arrayMap2, this.zzfhd, zzbd.zza(arrayMap2.values(), true), arrayList, false);
            synchronized (GoogleApiClient.zzfgu) {
                GoogleApiClient.zzfgu.add(zzbd);
            }
            if (this.zzfhd >= 0) {
                zzi.zza(this.zzfhc).zza(this.zzfhd, zzbd, this.zzfhe);
            }
            return zzbd;
        }

        public final Builder enableAutoManage(@NonNull FragmentActivity fragmentActivity, int i, @Nullable OnConnectionFailedListener onConnectionFailedListener) {
            zzcf zzcf = new zzcf(fragmentActivity);
            zzbp.zzb(i >= 0, (Object) "clientId must be non-negative");
            this.zzfhd = i;
            this.zzfhe = onConnectionFailedListener;
            this.zzfhc = zzcf;
            return this;
        }

        public final Builder enableAutoManage(@NonNull FragmentActivity fragmentActivity, @Nullable OnConnectionFailedListener onConnectionFailedListener) {
            return enableAutoManage(fragmentActivity, 0, onConnectionFailedListener);
        }

        public final Builder setAccountName(String str) {
            this.zzdva = str == null ? null : new Account(str, "com.google");
            return this;
        }

        public final Builder setGravityForPopups(int i) {
            this.zzfgx = i;
            return this;
        }

        public final Builder setHandler(@NonNull Handler handler) {
            zzbp.zzb((Object) handler, (Object) "Handler must not be null");
            this.zzakl = handler.getLooper();
            return this;
        }

        public final Builder setViewForPopups(@NonNull View view) {
            zzbp.zzb((Object) view, (Object) "View must not be null");
            this.zzfgy = view;
            return this;
        }

        public final Builder useDefaultAccount() {
            return setAccountName("<<default account>>");
        }

        public final zzq zzafq() {
            zzcpn zzcpn = zzcpn.zzjnd;
            if (this.zzfhb.containsKey(zzcpj.API)) {
                zzcpn = (zzcpn) this.zzfhb.get(zzcpj.API);
            }
            return new zzq(this.zzdva, this.zzfgv, this.zzfha, this.zzfgx, this.zzfgy, this.zzdxd, this.zzfgz, zzcpn);
        }
    }

    public interface ConnectionCallbacks {
        public static final int CAUSE_NETWORK_LOST = 2;
        public static final int CAUSE_SERVICE_DISCONNECTED = 1;

        void onConnected(@Nullable Bundle bundle);

        void onConnectionSuspended(int i);
    }

    public interface OnConnectionFailedListener {
        void onConnectionFailed(@NonNull ConnectionResult connectionResult);
    }

    public static void dumpAll(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
        synchronized (zzfgu) {
            String concat = String.valueOf(str).concat("  ");
            int i = 0;
            for (GoogleApiClient googleApiClient : zzfgu) {
                printWriter.append(str).append("GoogleApiClient#").println(i);
                googleApiClient.dump(concat, fileDescriptor, printWriter, strArr);
                i++;
            }
        }
    }

    public static Set<GoogleApiClient> zzafn() {
        Set<GoogleApiClient> set;
        synchronized (zzfgu) {
            set = zzfgu;
        }
        return set;
    }

    public abstract ConnectionResult blockingConnect();

    public abstract ConnectionResult blockingConnect(long j, @NonNull TimeUnit timeUnit);

    public abstract PendingResult<Status> clearDefaultAccountAndReconnect();

    public abstract void connect();

    public void connect(int i) {
        throw new UnsupportedOperationException();
    }

    public abstract void disconnect();

    public abstract void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr);

    @NonNull
    public abstract ConnectionResult getConnectionResult(@NonNull Api<?> api);

    public Context getContext() {
        throw new UnsupportedOperationException();
    }

    public Looper getLooper() {
        throw new UnsupportedOperationException();
    }

    public abstract boolean hasConnectedApi(@NonNull Api<?> api);

    public abstract boolean isConnected();

    public abstract boolean isConnecting();

    public abstract boolean isConnectionCallbacksRegistered(@NonNull ConnectionCallbacks connectionCallbacks);

    public abstract boolean isConnectionFailedListenerRegistered(@NonNull OnConnectionFailedListener onConnectionFailedListener);

    public abstract void reconnect();

    public abstract void registerConnectionCallbacks(@NonNull ConnectionCallbacks connectionCallbacks);

    public abstract void registerConnectionFailedListener(@NonNull OnConnectionFailedListener onConnectionFailedListener);

    public abstract void stopAutoManage(@NonNull FragmentActivity fragmentActivity);

    public abstract void unregisterConnectionCallbacks(@NonNull ConnectionCallbacks connectionCallbacks);

    public abstract void unregisterConnectionFailedListener(@NonNull OnConnectionFailedListener onConnectionFailedListener);

    @NonNull
    public <C extends zze> C zza(@NonNull zzc<C> zzc) {
        throw new UnsupportedOperationException();
    }

    public void zza(zzdf zzdf) {
        throw new UnsupportedOperationException();
    }

    public boolean zza(@NonNull Api<?> api) {
        throw new UnsupportedOperationException();
    }

    public boolean zza(zzcv zzcv) {
        throw new UnsupportedOperationException();
    }

    public void zzafo() {
        throw new UnsupportedOperationException();
    }

    public void zzb(zzdf zzdf) {
        throw new UnsupportedOperationException();
    }

    public <A extends zzb, R extends Result, T extends zzm<R, A>> T zzd(@NonNull T t) {
        throw new UnsupportedOperationException();
    }

    public <A extends zzb, T extends zzm<? extends Result, A>> T zze(@NonNull T t) {
        throw new UnsupportedOperationException();
    }

    public <L> zzcj<L> zzp(@NonNull L l) {
        throw new UnsupportedOperationException();
    }
}
