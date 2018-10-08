package com.google.android.gms.common.api.internal;

import android.app.Activity;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzb;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.TaskCompletionSource;
import java.util.concurrent.CancellationException;

public class zzco extends zzo {
    private TaskCompletionSource<Void> zzdzd = new TaskCompletionSource();

    private zzco(zzcg zzcg) {
        super(zzcg);
        this.zzfoi.zza("GmsAvailabilityHelper", (LifecycleCallback) this);
    }

    public static zzco zzp(Activity activity) {
        zzcg zzn = LifecycleCallback.zzn(activity);
        zzco zzco = (zzco) zzn.zza("GmsAvailabilityHelper", zzco.class);
        if (zzco == null) {
            return new zzco(zzn);
        }
        if (!zzco.zzdzd.getTask().isComplete()) {
            return zzco;
        }
        zzco.zzdzd = new TaskCompletionSource();
        return zzco;
    }

    public final Task<Void> getTask() {
        return this.zzdzd.getTask();
    }

    public final void onDestroy() {
        super.onDestroy();
        this.zzdzd.trySetException(new CancellationException("Host activity was destroyed before Google Play services could be made available."));
    }

    protected final void zza(ConnectionResult connectionResult, int i) {
        this.zzdzd.setException(zzb.zzx(new Status(connectionResult.getErrorCode(), connectionResult.getErrorMessage(), connectionResult.getResolution())));
    }

    protected final void zzafv() {
        int isGooglePlayServicesAvailable = this.zzfhf.isGooglePlayServicesAvailable(this.zzfoi.zzaij());
        if (isGooglePlayServicesAvailable == 0) {
            this.zzdzd.setResult(null);
        } else if (!this.zzdzd.getTask().isComplete()) {
            zzb(new ConnectionResult(isGooglePlayServicesAvailable, null), 0);
        }
    }
}
