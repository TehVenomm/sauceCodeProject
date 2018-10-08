package com.google.android.gms.nearby.messages.internal;

import android.annotation.TargetApi;
import android.app.Activity;
import android.app.Application;
import android.app.Service;
import android.content.Context;
import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.os.RemoteException;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.util.Log;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.common.internal.zzaa;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.internal.zzclq;
import com.google.android.gms.internal.zzclt;
import com.google.android.gms.internal.zzcma;
import com.google.android.gms.internal.zzcmc;
import com.google.android.gms.nearby.messages.MessageListener;
import com.google.android.gms.nearby.messages.MessagesOptions;
import com.google.android.gms.nearby.messages.StatusCallback;
import com.google.android.gms.nearby.messages.SubscribeCallback;
import com.google.android.gms.nearby.messages.SubscribeOptions;

final class zzah extends zzaa<zzs> {
    private final int zzjed;
    private final zzcmc zzjfz = new zzcmc();
    private final ClientAppContext zzjga;

    @TargetApi(14)
    zzah(Context context, Looper looper, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener, zzq zzq, MessagesOptions messagesOptions) {
        super(context, looper, 62, zzq, connectionCallbacks, onConnectionFailedListener);
        String zzaju = zzq.zzaju();
        int i = context instanceof Activity ? 1 : context instanceof Application ? 2 : context instanceof Service ? 3 : 0;
        if (messagesOptions != null) {
            this.zzjga = new ClientAppContext(zzaju, null, false, null, i);
            this.zzjed = messagesOptions.zzjeb;
        } else {
            this.zzjga = new ClientAppContext(zzaju, null, false, null, i);
            this.zzjed = -1;
        }
        if (i == 1) {
            Activity activity = (Activity) context;
            Log.v("NearbyMessagesClient", String.format("Registering ClientLifecycleSafetyNet's ActivityLifecycleCallbacks for %s", new Object[]{activity.getPackageName()}));
            activity.getApplication().registerActivityLifecycleCallbacks(new zzaj(activity, this));
        }
    }

    public final void disconnect() {
        try {
            zzdy(2);
        } catch (RemoteException e) {
            Log.v("NearbyMessagesClient", String.format("Failed to emit CLIENT_DISCONNECTED from override of GmsClient#disconnect(): %s", new Object[]{e}));
        }
        this.zzjfz.clear();
        super.disconnect();
    }

    @Nullable
    final zzcj<MessageListener> zza(GoogleApiClient googleApiClient, @Nullable MessageListener messageListener) {
        return this.zzjfz.zzb(googleApiClient, messageListener).zzbbb();
    }

    @Nullable
    final zzcj<StatusCallback> zza(GoogleApiClient googleApiClient, @Nullable StatusCallback statusCallback) {
        return this.zzjfz.zzb(googleApiClient, statusCallback).zzbbb();
    }

    @Nullable
    final zzcj<MessageListener> zza(@Nullable MessageListener messageListener) {
        zzclq zzae = this.zzjfz.zzae(messageListener);
        return zzae == null ? null : zzae.zzbbb();
    }

    @Nullable
    final zzcj<StatusCallback> zza(@Nullable StatusCallback statusCallback) {
        zzclq zzae = this.zzjfz.zzae(statusCallback);
        return zzae == null ? null : zzae.zzbbb();
    }

    final void zza(zzcj<zzn<Status>> zzcj, @Nullable zzcj<MessageListener> zzcj2) throws RemoteException {
        if (zzcj2 != null) {
            ((zzs) zzajj()).zza(new zzbe(this.zzjfz.zzh(zzcj2), new zzclt(zzcj), null));
            this.zzjfz.zzi(zzcj2);
        }
    }

    final void zza(zzcj<zzn<Status>> zzcj, zzcj<MessageListener> zzcj2, @Nullable zzcj<SubscribeCallback> zzcj3, SubscribeOptions subscribeOptions, @Nullable byte[] bArr) throws RemoteException {
        ((zzs) zzajj()).zza(new SubscribeRequest(this.zzjfz.zzh(zzcj2), subscribeOptions.getStrategy(), new zzclt(zzcj), subscribeOptions.getFilter(), null, null, zzcj3 == null ? null : new zzcma(zzcj3), subscribeOptions.zzjev));
    }

    final void zza(zzcj<zzn<Status>> zzcj, zzaf zzaf) throws RemoteException {
        ((zzs) zzajj()).zza(new zzbc(zzaf, new zzclt(zzcj), this.zzjga));
    }

    final void zzb(zzcj<zzn<Status>> zzcj, zzcj<StatusCallback> zzcj2) throws RemoteException {
        zzaz zzaz = new zzaz(new zzclt(zzcj), this.zzjfz.zzh(zzcj2));
        zzaz.zzjgp = true;
        ((zzs) zzajj()).zza(zzaz);
    }

    final void zzc(zzcj<zzn<Status>> zzcj, @Nullable zzcj<StatusCallback> zzcj2) throws RemoteException {
        if (zzcj2 != null) {
            zzaz zzaz = new zzaz(new zzclt(zzcj), this.zzjfz.zzh(zzcj2));
            zzaz.zzjgp = false;
            ((zzs) zzajj()).zza(zzaz);
            this.zzjfz.zzi(zzcj2);
        }
    }

    final void zzdy(int i) throws RemoteException {
        String str;
        switch (i) {
            case 1:
                str = "ACTIVITY_STOPPED";
                break;
            case 2:
                str = "CLIENT_DISCONNECTED";
                break;
            default:
                Log.w("NearbyMessagesClient", String.format("Received unknown/unforeseen client lifecycle event %d, can't do anything with it.", new Object[]{Integer.valueOf(i)}));
                return;
        }
        if (isConnected()) {
            zzj zzj = new zzj(i);
            Log.d("NearbyMessagesClient", String.format("Emitting client lifecycle event %s", new Object[]{str}));
            ((zzs) zzajj()).zza(zzj);
            return;
        }
        Log.d("NearbyMessagesClient", String.format("Failed to emit client lifecycle event %s due to GmsClient being disconnected", new Object[]{str}));
    }

    protected final /* synthetic */ IInterface zze(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.messages.internal.INearbyMessagesService");
        return queryLocalInterface instanceof zzs ? (zzs) queryLocalInterface : new zzt(iBinder);
    }

    @NonNull
    protected final String zzhc() {
        return "com.google.android.gms.nearby.messages.service.NearbyMessagesService.START";
    }

    @NonNull
    protected final String zzhd() {
        return "com.google.android.gms.nearby.messages.internal.INearbyMessagesService";
    }

    @NonNull
    protected final Bundle zzzs() {
        Bundle zzzs = super.zzzs();
        zzzs.putInt("NearbyPermissions", this.zzjed);
        zzzs.putParcelable("ClientAppContext", this.zzjga);
        return zzzs;
    }
}
