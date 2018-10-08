package com.google.android.gms.plus;

import android.content.Context;
import android.os.Looper;
import com.google.android.gms.common.Scopes;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.ApiOptions.Optional;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.Api.zzc;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.internal.zzif;
import com.google.android.gms.internal.zzll;
import com.google.android.gms.internal.zzlm;
import com.google.android.gms.internal.zzln;
import com.google.android.gms.internal.zzlo;
import com.google.android.gms.internal.zzlp;
import com.google.android.gms.plus.internal.PlusCommonExtras;
import com.google.android.gms.plus.internal.PlusSession;
import com.google.android.gms.plus.internal.zze;
import java.util.HashSet;
import java.util.Set;

public final class Plus {
    public static final Api<PlusOptions> API = new Api("Plus.API", zzKi, zzKh, new Scope[0]);
    public static final Account AccountApi = new zzll();
    public static final Moments MomentsApi = new zzlo();
    public static final People PeopleApi = new zzlp();
    public static final Scope SCOPE_PLUS_LOGIN = new Scope(Scopes.PLUS_LOGIN);
    public static final Scope SCOPE_PLUS_PROFILE = new Scope(Scopes.PLUS_ME);
    public static final zzc<zze> zzKh = new zzc();
    static final zzb<zze, PlusOptions> zzKi = new C06361();
    public static final zzb zzayL = new zzln();
    public static final zza zzayM = new zzlm();

    public static abstract class zza<R extends Result> extends com.google.android.gms.common.api.zza.zza<R, zze> {
        public zza(GoogleApiClient googleApiClient) {
            super(Plus.zzKh, googleApiClient);
        }
    }

    /* renamed from: com.google.android.gms.plus.Plus$1 */
    static final class C06361 implements zzb<zze, PlusOptions> {
        C06361() {
        }

        public int getPriority() {
            return 2;
        }

        public zze zza(Context context, Looper looper, zze zze, PlusOptions plusOptions, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
            if (plusOptions == null) {
                plusOptions = new PlusOptions();
            }
            return new zze(context, looper, zze, new PlusSession(zze.zzlD(), zzif.zzc(zze.zzlH()), (String[]) plusOptions.zzayO.toArray(new String[0]), new String[0], context.getPackageName(), context.getPackageName(), null, new PlusCommonExtras()), connectionCallbacks, onConnectionFailedListener);
        }
    }

    public static final class PlusOptions implements Optional {
        final String zzayN;
        final Set<String> zzayO;

        public static final class Builder {
            String zzayN;
            final Set<String> zzayO = new HashSet();

            public Builder addActivityTypes(String... strArr) {
                zzv.zzb(strArr, "activityTypes may not be null.");
                for (Object add : strArr) {
                    this.zzayO.add(add);
                }
                return this;
            }

            public PlusOptions build() {
                return new PlusOptions();
            }

            public Builder setServerClientId(String str) {
                this.zzayN = str;
                return this;
            }
        }

        private PlusOptions() {
            this.zzayN = null;
            this.zzayO = new HashSet();
        }

        private PlusOptions(Builder builder) {
            this.zzayN = builder.zzayN;
            this.zzayO = builder.zzayO;
        }

        public static Builder builder() {
            return new Builder();
        }
    }

    private Plus() {
    }

    public static zze zzf(GoogleApiClient googleApiClient, boolean z) {
        zzv.zzb(googleApiClient != null, "GoogleApiClient parameter is required.");
        zzv.zza(googleApiClient.isConnected(), "GoogleApiClient must be connected.");
        zzv.zza(googleApiClient.zza(API), "GoogleApiClient is not configured to use the Plus.API Api. Pass this into GoogleApiClient.Builder#addApi() to use this feature.");
        boolean hasConnectedApi = googleApiClient.hasConnectedApi(API);
        if (!z || hasConnectedApi) {
            return hasConnectedApi ? (zze) googleApiClient.zza(zzKh) : null;
        } else {
            throw new IllegalStateException("GoogleApiClient has an optional Plus.API and is not connected to Plus. Use GoogleApiClient.hasConnectedApi(Plus.API) to guard this call.");
        }
    }
}
