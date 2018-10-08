package com.google.android.gms.auth.api.accounttransfer;

import android.app.Activity;
import android.app.PendingIntent;
import android.content.Context;
import android.os.RemoteException;
import android.support.annotation.NonNull;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.zzf;
import com.google.android.gms.common.api.GoogleApi;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzdd;
import com.google.android.gms.common.api.internal.zzg;
import com.google.android.gms.common.api.zze;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.internal.zzarn;
import com.google.android.gms.internal.zzarp;
import com.google.android.gms.internal.zzarq;
import com.google.android.gms.internal.zzart;
import com.google.android.gms.internal.zzaru;
import com.google.android.gms.internal.zzarw;
import com.google.android.gms.internal.zzary;
import com.google.android.gms.internal.zzasa;
import com.google.android.gms.internal.zzasc;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.TaskCompletionSource;

public class AccountTransferClient extends GoogleApi<zzo> {
    private static final zzf<zzarp> zzdyq = new zzf();
    private static final com.google.android.gms.common.api.Api.zza<zzarp, zzo> zzdyr = new zzc();
    private static final Api<zzo> zzdys = new Api("AccountTransfer.ACCOUNT_TRANSFER_API", zzdyr, zzdyq);

    static class zza<T> extends zzarn {
        private zzb<T> zzdzc;

        public zza(zzb<T> zzb) {
            this.zzdzc = zzb;
        }

        public final void onFailure(Status status) {
            this.zzdzc.zzd(status);
        }
    }

    static abstract class zzb<T> extends zzdd<zzarp, T> {
        private TaskCompletionSource<T> zzdzd;

        private zzb() {
        }

        protected final void setResult(T t) {
            this.zzdzd.setResult(t);
        }

        protected final /* synthetic */ void zza(com.google.android.gms.common.api.Api.zzb zzb, TaskCompletionSource taskCompletionSource) throws RemoteException {
            zzarp zzarp = (zzarp) zzb;
            this.zzdzd = taskCompletionSource;
            zza((zzaru) zzarp.zzajj());
        }

        protected abstract void zza(zzaru zzaru) throws RemoteException;

        protected final void zzd(Status status) {
            AccountTransferClient.zza(this.zzdzd, status);
        }
    }

    static abstract class zzc extends zzb<Void> {
        zzart zzdze;

        private zzc() {
            super();
            this.zzdze = new zzk(this);
        }
    }

    AccountTransferClient(@NonNull Activity activity) {
        super(activity, zzdys, null, new zze().zza(new zzg()).zzafm());
    }

    AccountTransferClient(@NonNull Context context) {
        super(context, zzdys, null, new zze().zza(new zzg()).zzafm());
    }

    private static void zza(TaskCompletionSource taskCompletionSource, Status status) {
        String valueOf = String.valueOf(status.zzaft());
        taskCompletionSource.setException(new zzl(valueOf.length() != 0 ? "Exception with Status code=".concat(valueOf) : new String("Exception with Status code=")));
    }

    public Task<DeviceMetaData> getDeviceMetaData(String str) {
        zzbp.zzu(str);
        return zza(new zzg(this, new zzarq(str)));
    }

    public Task<Void> notifyCompletion(String str, int i) {
        zzbp.zzu(str);
        return zzb(new zzj(this, new zzarw(str, i)));
    }

    public Task<byte[]> retrieveData(String str) {
        zzbp.zzu(str);
        return zza(new zze(this, new zzary(str)));
    }

    public Task<Void> sendData(String str, byte[] bArr) {
        zzbp.zzu(str);
        zzbp.zzu(bArr);
        return zzb(new zzd(this, new zzasa(str, bArr)));
    }

    public Task<Void> showUserChallenge(String str, PendingIntent pendingIntent) {
        zzbp.zzu(str);
        zzbp.zzu(pendingIntent);
        return zzb(new zzi(this, new zzasc(str, pendingIntent)));
    }
}
