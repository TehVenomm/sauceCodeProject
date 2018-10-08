package com.google.android.gms.internal;

import android.content.IntentSender;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.Drive;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.MetadataChangeSet;

public final class zzbhr {
    private String zzehi;
    private DriveId zzgeg;
    private MetadataChangeSet zzggd;
    private Integer zzgge;
    private final int zzggf = 0;

    public zzbhr(int i) {
    }

    public final IntentSender build(GoogleApiClient googleApiClient) {
        int i = 0;
        zzbp.zzb(this.zzggd, (Object) "Must provide initial metadata to CreateFileActivityBuilder.");
        zzbp.zza(googleApiClient.isConnected(), (Object) "Client must be connected");
        zzbiw zzbiw = (zzbiw) googleApiClient.zza(Drive.zzdwq);
        this.zzggd.zzana().setContext(zzbiw.getContext());
        if (this.zzgge != null) {
            i = this.zzgge.intValue();
        }
        try {
            return ((zzblb) zzbiw.zzajj()).zza(new zzbhs(this.zzggd.zzana(), i, this.zzehi, this.zzgeg, Integer.valueOf(0)));
        } catch (Throwable e) {
            throw new RuntimeException("Unable to connect Drive Play Service", e);
        }
    }

    public final void zza(DriveId driveId) {
        this.zzgeg = (DriveId) zzbp.zzu(driveId);
    }

    public final void zza(MetadataChangeSet metadataChangeSet) {
        this.zzggd = (MetadataChangeSet) zzbp.zzu(metadataChangeSet);
    }

    public final void zzcp(int i) {
        this.zzgge = Integer.valueOf(i);
    }

    public final void zzgq(String str) {
        this.zzehi = (String) zzbp.zzu(str);
    }
}
