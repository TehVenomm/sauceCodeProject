package com.google.android.gms.drive;

import android.content.IntentSender;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.query.Filter;
import com.google.android.gms.drive.query.internal.FilterHolder;
import com.google.android.gms.drive.query.internal.zzk;
import com.google.android.gms.internal.zzbiw;
import com.google.android.gms.internal.zzblb;
import com.google.android.gms.internal.zzbmt;

public class OpenFileActivityBuilder {
    public static final String EXTRA_RESPONSE_DRIVE_ID = "response_drive_id";
    private String zzehi;
    private String[] zzgee;
    private Filter zzgef;
    private DriveId zzgeg;

    public IntentSender build(GoogleApiClient googleApiClient) {
        zzbp.zza(googleApiClient.isConnected(), (Object) "Client must be connected");
        if (this.zzgee == null) {
            this.zzgee = new String[0];
        }
        if (this.zzgee.length <= 0 || this.zzgef == null) {
            try {
                return ((zzblb) ((zzbiw) googleApiClient.zza(Drive.zzdwq)).zzajj()).zza(new zzbmt(this.zzehi, this.zzgee, this.zzgeg, this.zzgef == null ? null : new FilterHolder(this.zzgef)));
            } catch (Throwable e) {
                throw new RuntimeException("Unable to connect Drive Play Service", e);
            }
        }
        throw new IllegalStateException("Cannot use a selection filter and set mimetypes simultaneously");
    }

    public OpenFileActivityBuilder setActivityStartFolder(DriveId driveId) {
        this.zzgeg = (DriveId) zzbp.zzu(driveId);
        return this;
    }

    public OpenFileActivityBuilder setActivityTitle(String str) {
        this.zzehi = (String) zzbp.zzu(str);
        return this;
    }

    public OpenFileActivityBuilder setMimeType(String[] strArr) {
        zzbp.zzb(strArr != null, (Object) "mimeTypes may not be null");
        this.zzgee = strArr;
        return this;
    }

    public OpenFileActivityBuilder setSelectionFilter(Filter filter) {
        boolean z = true;
        zzbp.zzb(filter != null, (Object) "filter may not be null");
        if (zzk.zza(filter)) {
            z = false;
        }
        zzbp.zzb(z, (Object) "FullTextSearchFilter cannot be used as a selection filter");
        this.zzgef = filter;
        return this;
    }
}
