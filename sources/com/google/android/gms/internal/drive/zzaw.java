package com.google.android.gms.internal.drive;

import android.content.Context;
import android.content.Intent;
import android.content.pm.ResolveInfo;
import android.content.pm.ServiceInfo;
import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.os.Process;
import android.os.RemoteException;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.ClientSettings;
import com.google.android.gms.common.internal.GmsClient;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.internal.ServiceSpecificExtraArgs.CastExtraArgs;
import com.google.android.gms.common.util.UidVerifier;
import com.google.android.gms.common.util.VisibleForTesting;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.events.ChangeListener;
import com.google.android.gms.drive.events.DriveEventService;
import com.google.android.gms.drive.events.zzd;
import com.google.android.gms.drive.events.zzj;
import com.google.android.gms.drive.events.zzl;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import javax.annotation.concurrent.GuardedBy;

public final class zzaw extends GmsClient<zzeo> {
    private final String zzdz;
    protected final boolean zzea;
    private volatile DriveId zzeb;
    private volatile DriveId zzec;
    private volatile boolean zzed = false;
    @GuardedBy("changeEventCallbackMap")
    @VisibleForTesting
    private final Map<DriveId, Map<ChangeListener, zzee>> zzee = new HashMap();
    @GuardedBy("changesAvailableEventCallbackMap")
    @VisibleForTesting
    private final Map<zzd, zzee> zzef = new HashMap();
    @GuardedBy("uploadProgressEventCallbackMap")
    @VisibleForTesting
    private final Map<DriveId, Map<zzl, zzee>> zzeg = new HashMap();
    @GuardedBy("pinnedDownloadProgressEventCallbackMap")
    @VisibleForTesting
    private final Map<DriveId, Map<zzl, zzee>> zzeh = new HashMap();
    private final Bundle zzx;

    public zzaw(Context context, Looper looper, ClientSettings clientSettings, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener, Bundle bundle) {
        super(context, looper, 11, clientSettings, connectionCallbacks, onConnectionFailedListener);
        this.zzdz = clientSettings.getRealClientPackageName();
        this.zzx = bundle;
        Intent intent = new Intent(DriveEventService.ACTION_HANDLE_EVENT);
        intent.setPackage(context.getPackageName());
        List queryIntentServices = context.getPackageManager().queryIntentServices(intent, 0);
        switch (queryIntentServices.size()) {
            case 0:
                this.zzea = false;
                return;
            case 1:
                ServiceInfo serviceInfo = ((ResolveInfo) queryIntentServices.get(0)).serviceInfo;
                if (!serviceInfo.exported) {
                    String str = serviceInfo.name;
                    throw new IllegalStateException(new StringBuilder(String.valueOf(str).length() + 60).append("Drive event service ").append(str).append(" must be exported in AndroidManifest.xml").toString());
                } else {
                    this.zzea = true;
                    return;
                }
            default:
                String action = intent.getAction();
                throw new IllegalStateException(new StringBuilder(String.valueOf(action).length() + 72).append("AndroidManifest.xml can only define one service that handles the ").append(action).append(" action").toString());
        }
    }

    /* access modifiers changed from: protected */
    public final /* synthetic */ IInterface createServiceInterface(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.drive.internal.IDriveService");
        return queryLocalInterface instanceof zzeo ? (zzeo) queryLocalInterface : new zzep(iBinder);
    }

    public final void disconnect() {
        if (isConnected()) {
            try {
                ((zzeo) getService()).zza(new zzad());
            } catch (RemoteException e) {
            }
        }
        super.disconnect();
        synchronized (this.zzee) {
            this.zzee.clear();
        }
        synchronized (this.zzef) {
            this.zzef.clear();
        }
        synchronized (this.zzeg) {
            this.zzeg.clear();
        }
        synchronized (this.zzeh) {
            this.zzeh.clear();
        }
    }

    /* access modifiers changed from: protected */
    public final Bundle getGetServiceRequestExtraArgs() {
        String packageName = getContext().getPackageName();
        Preconditions.checkNotNull(packageName);
        Preconditions.checkState(!getClientSettings().getAllRequestedScopes().isEmpty());
        Bundle bundle = new Bundle();
        if (!packageName.equals(this.zzdz)) {
            bundle.putString("proxy_package_name", this.zzdz);
        }
        bundle.putAll(this.zzx);
        return bundle;
    }

    public final int getMinApkVersion() {
        return 12451000;
    }

    /* access modifiers changed from: protected */
    public final String getServiceDescriptor() {
        return "com.google.android.gms.drive.internal.IDriveService";
    }

    /* access modifiers changed from: protected */
    public final String getStartServiceAction() {
        return "com.google.android.gms.drive.ApiService.START";
    }

    /* access modifiers changed from: protected */
    public final void onPostInitHandler(int i, IBinder iBinder, Bundle bundle, int i2) {
        if (bundle != null) {
            bundle.setClassLoader(getClass().getClassLoader());
            this.zzeb = (DriveId) bundle.getParcelable("com.google.android.gms.drive.root_id");
            this.zzec = (DriveId) bundle.getParcelable("com.google.android.gms.drive.appdata_id");
            this.zzed = true;
        }
        super.onPostInitHandler(i, iBinder, bundle, i2);
    }

    public final boolean requiresAccount() {
        return true;
    }

    public final boolean requiresSignIn() {
        return !getContext().getPackageName().equals(this.zzdz) || !UidVerifier.isGooglePlayServicesUid(getContext(), Process.myUid());
    }

    /* access modifiers changed from: 0000 */
    public final PendingResult<Status> zza(GoogleApiClient googleApiClient, DriveId driveId, ChangeListener changeListener) {
        Map map;
        PendingResult<Status> zzat;
        Preconditions.checkArgument(zzj.zza(1, driveId));
        Preconditions.checkNotNull(changeListener, CastExtraArgs.LISTENER);
        Preconditions.checkState(isConnected(), "Client must be connected");
        synchronized (this.zzee) {
            Map map2 = (Map) this.zzee.get(driveId);
            if (map2 == null) {
                HashMap hashMap = new HashMap();
                this.zzee.put(driveId, hashMap);
                map = hashMap;
            } else {
                map = map2;
            }
            zzee zzee2 = (zzee) map.get(changeListener);
            if (zzee2 == null) {
                zzee2 = new zzee(getLooper(), getContext(), 1, changeListener);
                map.put(changeListener, zzee2);
            } else if (zzee2.zzg(1)) {
                zzat = new zzat<>(googleApiClient, Status.RESULT_SUCCESS);
            }
            zzee2.zzf(1);
            zzat = googleApiClient.execute(new zzax(this, googleApiClient, new zzj(1, driveId), zzee2));
        }
        return zzat;
    }

    public final DriveId zzad() {
        return this.zzeb;
    }

    public final DriveId zzae() {
        return this.zzec;
    }

    public final boolean zzaf() {
        return this.zzed;
    }

    public final boolean zzag() {
        return this.zzea;
    }

    /* access modifiers changed from: 0000 */
    public final PendingResult<Status> zzb(GoogleApiClient googleApiClient, DriveId driveId, ChangeListener changeListener) {
        PendingResult<Status> execute;
        Preconditions.checkArgument(zzj.zza(1, driveId));
        Preconditions.checkState(isConnected(), "Client must be connected");
        Preconditions.checkNotNull(changeListener, CastExtraArgs.LISTENER);
        synchronized (this.zzee) {
            Map map = (Map) this.zzee.get(driveId);
            if (map == null) {
                execute = new zzat<>(googleApiClient, Status.RESULT_SUCCESS);
            } else {
                zzee zzee2 = (zzee) map.remove(changeListener);
                if (zzee2 == null) {
                    execute = new zzat<>(googleApiClient, Status.RESULT_SUCCESS);
                } else {
                    if (map.isEmpty()) {
                        this.zzee.remove(driveId);
                    }
                    execute = googleApiClient.execute(new zzay(this, googleApiClient, new zzgm(driveId, 1), zzee2));
                }
            }
        }
        return execute;
    }
}
