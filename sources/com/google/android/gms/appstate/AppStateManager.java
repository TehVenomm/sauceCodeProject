package com.google.android.gms.appstate;

import android.content.Context;
import android.os.Looper;
import android.os.RemoteException;
import com.google.android.gms.common.Scopes;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.ApiOptions.NoOptions;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Releasable;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.internal.zzgx;
import com.google.android.gms.nearby.messages.Strategy;

@Deprecated
public final class AppStateManager {
    public static final Api<NoOptions> API = new Api("AppStateManager.API", zzKi, zzKh, new Scope[]{SCOPE_APP_STATE});
    public static final Scope SCOPE_APP_STATE = new Scope(Scopes.APP_STATE);
    static final com.google.android.gms.common.api.Api.zzc<zzgx> zzKh = new com.google.android.gms.common.api.Api.zzc();
    private static final com.google.android.gms.common.api.Api.zzb<zzgx, NoOptions> zzKi = new C06041();

    /* renamed from: com.google.android.gms.appstate.AppStateManager$1 */
    static final class C06041 implements com.google.android.gms.common.api.Api.zzb<zzgx, NoOptions> {
        C06041() {
        }

        public int getPriority() {
            return Strategy.TTL_SECONDS_INFINITE;
        }

        public /* synthetic */ com.google.android.gms.common.api.Api.zza zza(Context context, Looper looper, zze zze, Object obj, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
            return zzb(context, looper, zze, (NoOptions) obj, connectionCallbacks, onConnectionFailedListener);
        }

        public zzgx zzb(Context context, Looper looper, zze zze, NoOptions noOptions, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
            return new zzgx(context, looper, zze, connectionCallbacks, onConnectionFailedListener);
        }
    }

    public interface StateResult extends Releasable, Result {
        StateConflictResult getConflictResult();

        StateLoadedResult getLoadedResult();
    }

    public static abstract class zza<R extends Result> extends com.google.android.gms.common.api.zza.zza<R, zzgx> {
        public zza(GoogleApiClient googleApiClient) {
            super(AppStateManager.zzKh, googleApiClient);
        }
    }

    private static abstract class zze extends zza<StateResult> {
        public zze(GoogleApiClient googleApiClient) {
            super(googleApiClient);
        }

        public /* synthetic */ Result createFailedResult(Status status) {
            return zzg(status);
        }

        public StateResult zzg(Status status) {
            return AppStateManager.zzc(status);
        }
    }

    public interface StateDeletedResult extends Result {
        int getStateKey();
    }

    private static abstract class zzb extends zza<StateDeletedResult> {
        zzb(GoogleApiClient googleApiClient) {
            super(googleApiClient);
        }
    }

    private static abstract class zzc extends zza<StateListResult> {
        public zzc(GoogleApiClient googleApiClient) {
            super(googleApiClient);
        }

        public /* synthetic */ Result createFailedResult(Status status) {
            return zzf(status);
        }

        public StateListResult zzf(final Status status) {
            return new StateListResult(this) {
                final /* synthetic */ zzc zzKp;

                public AppStateBuffer getStateBuffer() {
                    return new AppStateBuffer(null);
                }

                public Status getStatus() {
                    return status;
                }
            };
        }
    }

    private static abstract class zzd extends zza<Status> {
        public zzd(GoogleApiClient googleApiClient) {
            super(googleApiClient);
        }

        public /* synthetic */ Result createFailedResult(Status status) {
            return zzb(status);
        }

        public Status zzb(Status status) {
            return status;
        }
    }

    public interface StateConflictResult extends Releasable, Result {
        byte[] getLocalData();

        String getResolvedVersion();

        byte[] getServerData();

        int getStateKey();
    }

    public interface StateListResult extends Result {
        AppStateBuffer getStateBuffer();
    }

    public interface StateLoadedResult extends Releasable, Result {
        byte[] getLocalData();

        int getStateKey();
    }

    private AppStateManager() {
    }

    public static PendingResult<StateDeletedResult> delete(GoogleApiClient googleApiClient, final int i) {
        return googleApiClient.zzb(new zzb(googleApiClient) {
            public /* synthetic */ Result createFailedResult(Status status) {
                return zze(status);
            }

            protected void zza(zzgx zzgx) throws RemoteException {
                zzgx.zza(this, i);
            }

            public StateDeletedResult zze(final Status status) {
                return new StateDeletedResult(this) {
                    final /* synthetic */ C06095 zzKm;

                    public int getStateKey() {
                        return i;
                    }

                    public Status getStatus() {
                        return status;
                    }
                };
            }
        });
    }

    public static int getMaxNumKeys(GoogleApiClient googleApiClient) {
        return zza(googleApiClient).zzjO();
    }

    public static int getMaxStateSize(GoogleApiClient googleApiClient) {
        return zza(googleApiClient).zzjN();
    }

    public static PendingResult<StateListResult> list(GoogleApiClient googleApiClient) {
        return googleApiClient.zza(new zzc(googleApiClient) {
            protected void zza(zzgx zzgx) throws RemoteException {
                zzgx.zza((com.google.android.gms.common.api.zza.zzb) this);
            }
        });
    }

    public static PendingResult<StateResult> load(GoogleApiClient googleApiClient, final int i) {
        return googleApiClient.zza(new zze(googleApiClient) {
            protected void zza(zzgx zzgx) throws RemoteException {
                zzgx.zzb(this, i);
            }
        });
    }

    public static PendingResult<StateResult> resolve(GoogleApiClient googleApiClient, final int i, final String str, final byte[] bArr) {
        return googleApiClient.zzb(new zze(googleApiClient) {
            protected void zza(zzgx zzgx) throws RemoteException {
                zzgx.zza(this, i, str, bArr);
            }
        });
    }

    public static PendingResult<Status> signOut(GoogleApiClient googleApiClient) {
        return googleApiClient.zzb(new zzd(googleApiClient) {
            protected void zza(zzgx zzgx) throws RemoteException {
                zzgx.zzb(this);
            }
        });
    }

    public static void update(GoogleApiClient googleApiClient, final int i, final byte[] bArr) {
        googleApiClient.zzb(new zze(googleApiClient) {
            protected void zza(zzgx zzgx) throws RemoteException {
                zzgx.zza(null, i, bArr);
            }
        });
    }

    public static PendingResult<StateResult> updateImmediate(GoogleApiClient googleApiClient, final int i, final byte[] bArr) {
        return googleApiClient.zzb(new zze(googleApiClient) {
            protected void zza(zzgx zzgx) throws RemoteException {
                zzgx.zza(this, i, bArr);
            }
        });
    }

    public static zzgx zza(GoogleApiClient googleApiClient) {
        zzv.zzb(googleApiClient != null, "GoogleApiClient parameter is required.");
        zzv.zza(googleApiClient.isConnected(), "GoogleApiClient must be connected.");
        zzv.zza(googleApiClient.zza(API), "GoogleApiClient is not configured to use the AppState API. Pass AppStateManager.API into GoogleApiClient.Builder#addApi() to use this feature.");
        return (zzgx) googleApiClient.zza(zzKh);
    }

    private static StateResult zzc(final Status status) {
        return new StateResult() {
            public StateConflictResult getConflictResult() {
                return null;
            }

            public StateLoadedResult getLoadedResult() {
                return null;
            }

            public Status getStatus() {
                return status;
            }

            public void release() {
            }
        };
    }
}
