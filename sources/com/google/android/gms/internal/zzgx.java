package com.google.android.gms.internal;

import android.content.Context;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.os.RemoteException;
import android.util.Log;
import com.google.android.gms.appstate.AppStateBuffer;
import com.google.android.gms.appstate.AppStateManager.StateConflictResult;
import com.google.android.gms.appstate.AppStateManager.StateDeletedResult;
import com.google.android.gms.appstate.AppStateManager.StateListResult;
import com.google.android.gms.appstate.AppStateManager.StateLoadedResult;
import com.google.android.gms.appstate.AppStateManager.StateResult;
import com.google.android.gms.common.Scopes;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.internal.zzi;
import java.util.Set;

public final class zzgx extends zzi<zzgz> {

    private static final class zza extends zzgw {
        private final com.google.android.gms.common.api.zza.zzb<StateDeletedResult> zzKq;

        public zza(com.google.android.gms.common.api.zza.zzb<StateDeletedResult> zzb) {
            this.zzKq = (com.google.android.gms.common.api.zza.zzb) zzv.zzb(zzb, "Result holder must not be null");
        }

        public void zzg(int i, int i2) {
            this.zzKq.zzj(new zzb(new Status(i), i2));
        }
    }

    private static final class zzb implements StateDeletedResult {
        private final Status zzKr;
        private final int zzKs;

        public zzb(Status status, int i) {
            this.zzKr = status;
            this.zzKs = i;
        }

        public int getStateKey() {
            return this.zzKs;
        }

        public Status getStatus() {
            return this.zzKr;
        }
    }

    private static final class zzc extends zzgw {
        private final com.google.android.gms.common.api.zza.zzb<StateListResult> zzKq;

        public zzc(com.google.android.gms.common.api.zza.zzb<StateListResult> zzb) {
            this.zzKq = (com.google.android.gms.common.api.zza.zzb) zzv.zzb(zzb, "Result holder must not be null");
        }

        public void zza(DataHolder dataHolder) {
            this.zzKq.zzj(new zzd(dataHolder));
        }
    }

    private static final class zzd extends com.google.android.gms.common.api.zzc implements StateListResult {
        private final AppStateBuffer zzKt;

        public zzd(DataHolder dataHolder) {
            super(dataHolder);
            this.zzKt = new AppStateBuffer(dataHolder);
        }

        public AppStateBuffer getStateBuffer() {
            return this.zzKt;
        }
    }

    private static final class zze extends zzgw {
        private final com.google.android.gms.common.api.zza.zzb<StateResult> zzKq;

        public zze(com.google.android.gms.common.api.zza.zzb<StateResult> zzb) {
            this.zzKq = (com.google.android.gms.common.api.zza.zzb) zzv.zzb(zzb, "Result holder must not be null");
        }

        public void zza(int i, DataHolder dataHolder) {
            this.zzKq.zzj(new zzf(i, dataHolder));
        }
    }

    private static final class zzf extends com.google.android.gms.common.api.zzc implements StateConflictResult, StateLoadedResult, StateResult {
        private final int zzKs;
        private final AppStateBuffer zzKt;

        public zzf(int i, DataHolder dataHolder) {
            super(dataHolder);
            this.zzKs = i;
            this.zzKt = new AppStateBuffer(dataHolder);
        }

        private boolean zzjP() {
            return this.zzKr.getStatusCode() == 2000;
        }

        public StateConflictResult getConflictResult() {
            return zzjP() ? this : null;
        }

        public StateLoadedResult getLoadedResult() {
            return zzjP() ? null : this;
        }

        public byte[] getLocalData() {
            return this.zzKt.getCount() == 0 ? null : this.zzKt.get(0).getLocalData();
        }

        public String getResolvedVersion() {
            return this.zzKt.getCount() == 0 ? null : this.zzKt.get(0).getConflictVersion();
        }

        public byte[] getServerData() {
            return this.zzKt.getCount() == 0 ? null : this.zzKt.get(0).getConflictData();
        }

        public int getStateKey() {
            return this.zzKs;
        }

        public void release() {
            this.zzKt.release();
        }
    }

    private static final class zzg extends zzgw {
        private final com.google.android.gms.common.api.zza.zzb<Status> zzKq;

        public zzg(com.google.android.gms.common.api.zza.zzb<Status> zzb) {
            this.zzKq = (com.google.android.gms.common.api.zza.zzb) zzv.zzb(zzb, "Holder must not be null");
        }

        public void zzjL() {
            this.zzKq.zzj(new Status(0));
        }
    }

    public zzgx(Context context, Looper looper, zze zze, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
        super(context, looper, 7, connectionCallbacks, onConnectionFailedListener, zze);
    }

    protected /* synthetic */ IInterface zzD(IBinder iBinder) {
        return zzK(iBinder);
    }

    protected zzgz zzK(IBinder iBinder) {
        return com.google.android.gms.internal.zzgz.zza.zzM(iBinder);
    }

    protected Set<Scope> zza(Set<Scope> set) {
        zzv.zza(set.contains(new Scope(Scopes.APP_STATE)), String.format("App State APIs requires %s to function.", new Object[]{Scopes.APP_STATE}));
        return set;
    }

    public void zza(com.google.android.gms.common.api.zza.zzb<StateListResult> zzb) throws RemoteException {
        ((zzgz) zzlX()).zza(new zzc(zzb));
    }

    public void zza(com.google.android.gms.common.api.zza.zzb<StateDeletedResult> zzb, int i) throws RemoteException {
        ((zzgz) zzlX()).zzb(new zza(zzb), i);
    }

    public void zza(com.google.android.gms.common.api.zza.zzb<StateResult> zzb, int i, String str, byte[] bArr) throws RemoteException {
        ((zzgz) zzlX()).zza(new zze(zzb), i, str, bArr);
    }

    public void zza(com.google.android.gms.common.api.zza.zzb<StateResult> zzb, int i, byte[] bArr) throws RemoteException {
        zzgy zzgy;
        if (zzb == null) {
            zzgy = null;
        } else {
            Object zze = new zze(zzb);
        }
        ((zzgz) zzlX()).zza(zzgy, i, bArr);
    }

    public void zzb(com.google.android.gms.common.api.zza.zzb<Status> zzb) throws RemoteException {
        ((zzgz) zzlX()).zzb(new zzg(zzb));
    }

    public void zzb(com.google.android.gms.common.api.zza.zzb<StateResult> zzb, int i) throws RemoteException {
        ((zzgz) zzlX()).zza(new zze(zzb), i);
    }

    protected String zzeq() {
        return "com.google.android.gms.appstate.service.START";
    }

    protected String zzer() {
        return "com.google.android.gms.appstate.internal.IAppStateService";
    }

    public boolean zzjM() {
        return true;
    }

    public int zzjN() {
        try {
            return ((zzgz) zzlX()).zzjN();
        } catch (RemoteException e) {
            Log.w("AppStateClient", "service died");
            return 2;
        }
    }

    public int zzjO() {
        try {
            return ((zzgz) zzlX()).zzjO();
        } catch (RemoteException e) {
            Log.w("AppStateClient", "service died");
            return 2;
        }
    }
}
