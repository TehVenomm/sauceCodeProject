package com.google.android.gms.internal;

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
import com.google.android.gms.common.internal.zzaa;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.common.util.zzv;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.events.ChangeListener;
import com.google.android.gms.drive.events.DriveEventService;
import com.google.android.gms.drive.events.zzd;
import com.google.android.gms.drive.events.zzj;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public final class zzbiw extends zzaa<zzblb> {
    private final String zzdxd;
    private final Bundle zzggt;
    private final boolean zzggu;
    private volatile DriveId zzggv;
    private volatile DriveId zzggw;
    private volatile boolean zzggx = false;
    private ConnectionCallbacks zzggy;
    private Map<DriveId, Map<ChangeListener, zzbkr>> zzggz = new HashMap();
    private Map<zzd, zzbkr> zzgha = new HashMap();
    private Map<DriveId, Map<Object, zzbkr>> zzghb = new HashMap();
    private Map<DriveId, Map<Object, zzbkr>> zzghc = new HashMap();

    public zzbiw(Context context, Looper looper, zzq zzq, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener, Bundle bundle) {
        super(context, looper, 11, zzq, connectionCallbacks, onConnectionFailedListener);
        this.zzdxd = zzq.zzaju();
        this.zzggy = connectionCallbacks;
        this.zzggt = bundle;
        Intent intent = new Intent(DriveEventService.ACTION_HANDLE_EVENT);
        intent.setPackage(context.getPackageName());
        List queryIntentServices = context.getPackageManager().queryIntentServices(intent, 0);
        String str;
        switch (queryIntentServices.size()) {
            case 0:
                this.zzggu = false;
                return;
            case 1:
                ServiceInfo serviceInfo = ((ResolveInfo) queryIntentServices.get(0)).serviceInfo;
                if (serviceInfo.exported) {
                    this.zzggu = true;
                    return;
                } else {
                    str = serviceInfo.name;
                    throw new IllegalStateException(new StringBuilder(String.valueOf(str).length() + 60).append("Drive event service ").append(str).append(" must be exported in AndroidManifest.xml").toString());
                }
            default:
                str = intent.getAction();
                throw new IllegalStateException(new StringBuilder(String.valueOf(str).length() + 72).append("AndroidManifest.xml can only define one service that handles the ").append(str).append(" action").toString());
        }
    }

    public final void disconnect() {
        if (isConnected()) {
            try {
                ((zzblb) zzajj()).zza(new zzbib());
            } catch (RemoteException e) {
            }
        }
        super.disconnect();
        synchronized (this.zzggz) {
            this.zzggz.clear();
        }
        synchronized (this.zzgha) {
            this.zzgha.clear();
        }
        synchronized (this.zzghb) {
            this.zzghb.clear();
        }
        synchronized (this.zzghc) {
            this.zzghc.clear();
        }
    }

    final PendingResult<Status> zza(GoogleApiClient googleApiClient, DriveId driveId) {
        zzbhg zzbhg = new zzbhg(1, driveId);
        zzbp.zzbh(zzj.zza(zzbhg.zzfxs, zzbhg.zzgcx));
        zzbp.zza(isConnected(), (Object) "Client must be connected");
        if (this.zzggu) {
            return googleApiClient.zze(new zzbiz(this, googleApiClient, zzbhg));
        }
        throw new IllegalStateException("Application must define an exported DriveEventService subclass in AndroidManifest.xml to add event subscriptions");
    }

    final PendingResult<Status> zza(GoogleApiClient googleApiClient, DriveId driveId, ChangeListener changeListener) {
        PendingResult<Status> zzbit;
        zzbp.zzbh(zzj.zza(1, driveId));
        zzbp.zzb((Object) changeListener, (Object) "listener");
        zzbp.zza(isConnected(), (Object) "Client must be connected");
        synchronized (this.zzggz) {
            Map map;
            Map map2 = (Map) this.zzggz.get(driveId);
            if (map2 == null) {
                HashMap hashMap = new HashMap();
                this.zzggz.put(driveId, hashMap);
                map = hashMap;
            } else {
                map = map2;
            }
            zzbkr zzbkr = (zzbkr) map.get(changeListener);
            if (zzbkr == null) {
                zzbkr = new zzbkr(getLooper(), getContext(), 1, changeListener);
                map.put(changeListener, zzbkr);
            } else if (zzbkr.zzcr(1)) {
                zzbit = new zzbit(googleApiClient, Status.zzfhp);
            }
            zzbkr.zzcq(1);
            zzbit = googleApiClient.zze(new zzbix(this, googleApiClient, new zzbhg(1, driveId), zzbkr));
        }
        return zzbit;
    }

    protected final void zza(int i, IBinder iBinder, Bundle bundle, int i2) {
        if (bundle != null) {
            bundle.setClassLoader(getClass().getClassLoader());
            this.zzggv = (DriveId) bundle.getParcelable("com.google.android.gms.drive.root_id");
            this.zzggw = (DriveId) bundle.getParcelable("com.google.android.gms.drive.appdata_id");
            this.zzggx = true;
        }
        super.zza(i, iBinder, bundle, i2);
    }

    public final boolean zzaaa() {
        return (getContext().getPackageName().equals(this.zzdxd) && zzv.zzf(getContext(), Process.myUid())) ? false : true;
    }

    public final boolean zzajk() {
        return true;
    }

    public final DriveId zzanj() {
        return this.zzggv;
    }

    public final DriveId zzank() {
        return this.zzggw;
    }

    public final boolean zzanl() {
        return this.zzggx;
    }

    public final boolean zzanm() {
        return this.zzggu;
    }

    final PendingResult<Status> zzb(GoogleApiClient googleApiClient, DriveId driveId, ChangeListener changeListener) {
        PendingResult<Status> zzbit;
        zzbp.zzbh(zzj.zza(1, driveId));
        zzbp.zza(isConnected(), (Object) "Client must be connected");
        zzbp.zzb((Object) changeListener, (Object) "listener");
        synchronized (this.zzggz) {
            Map map = (Map) this.zzggz.get(driveId);
            if (map == null) {
                zzbit = new zzbit(googleApiClient, Status.zzfhp);
            } else {
                zzbkr zzbkr = (zzbkr) map.remove(changeListener);
                if (zzbkr == null) {
                    zzbit = new zzbit(googleApiClient, Status.zzfhp);
                } else {
                    if (map.isEmpty()) {
                        this.zzggz.remove(driveId);
                    }
                    zzbit = googleApiClient.zze(new zzbiy(this, googleApiClient, new zzbmz(driveId, 1), zzbkr));
                }
            }
        }
        return zzbit;
    }

    protected final /* synthetic */ IInterface zze(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.drive.internal.IDriveService");
        return queryLocalInterface instanceof zzblb ? (zzblb) queryLocalInterface : new zzblc(iBinder);
    }

    protected final String zzhc() {
        return "com.google.android.gms.drive.ApiService.START";
    }

    protected final String zzhd() {
        return "com.google.android.gms.drive.internal.IDriveService";
    }

    protected final Bundle zzzs() {
        String packageName = getContext().getPackageName();
        zzbp.zzu(packageName);
        zzbp.zzbg(!zzakd().zzajs().isEmpty());
        Bundle bundle = new Bundle();
        if (!packageName.equals(this.zzdxd)) {
            bundle.putString("proxy_package_name", this.zzdxd);
        }
        bundle.putAll(this.zzggt);
        return bundle;
    }
}
