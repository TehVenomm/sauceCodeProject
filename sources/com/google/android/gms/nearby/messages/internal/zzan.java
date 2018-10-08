package com.google.android.gms.nearby.messages.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.internal.zzclt;
import com.google.android.gms.internal.zzclw;
import com.google.android.gms.nearby.messages.Message;
import com.google.android.gms.nearby.messages.PublishOptions;

final class zzan extends zzav {
    private /* synthetic */ Message zzjgf;
    private /* synthetic */ zzcj zzjgg;
    private /* synthetic */ PublishOptions zzjgh;

    zzan(zzak zzak, GoogleApiClient googleApiClient, Message message, zzcj zzcj, PublishOptions publishOptions) {
        this.zzjgf = message;
        this.zzjgg = zzcj;
        this.zzjgh = publishOptions;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zzah zzah = (zzah) zzb;
        zzcj zzbbb = zzbbb();
        zzaf zza = zzaf.zza(this.zzjgf);
        zzcj zzcj = this.zzjgg;
        ((zzs) zzah.zzajj()).zza(new zzax(zza, this.zzjgh.getStrategy(), new zzclt(zzbbb), zzcj == null ? null : new zzclw(zzcj)));
    }
}
