package com.google.android.gms.drive;

import android.content.Context;
import android.os.Bundle;
import android.os.Looper;
import com.google.android.gms.common.Scopes;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.ApiOptions;
import com.google.android.gms.common.api.Api.ApiOptions.NoOptions;
import com.google.android.gms.common.api.Api.ApiOptions.Optional;
import com.google.android.gms.common.api.Api.zze;
import com.google.android.gms.common.api.Api.zzf;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.internal.zzbid;
import com.google.android.gms.internal.zzbiw;
import com.google.android.gms.internal.zzbjl;
import com.google.android.gms.internal.zzbjw;
import com.google.android.gms.internal.zzbko;

public final class Drive {
    public static final Api<NoOptions> API = new Api("Drive.API", new zze(), zzdwq);
    public static final DriveApi DriveApi = new zzbid();
    public static final DrivePreferencesApi DrivePreferencesApi = new zzbjw();
    public static final Scope SCOPE_APPFOLDER = new Scope(Scopes.DRIVE_APPFOLDER);
    public static final Scope SCOPE_FILE = new Scope(Scopes.DRIVE_FILE);
    public static final zzf<zzbiw> zzdwq = new zzf();
    private static Scope zzgdc = new Scope("https://www.googleapis.com/auth/drive");
    private static Scope zzgdd = new Scope("https://www.googleapis.com/auth/drive.apps");
    private static Api<zzb> zzgde = new Api("Drive.INTERNAL_API", new zzf(), zzdwq);
    private static zzi zzgdf = new zzbjl();
    private static zzk zzgdg = new zzbko();

    public static abstract class zza<O extends ApiOptions> extends com.google.android.gms.common.api.Api.zza<zzbiw, O> {
        protected abstract Bundle zza(O o);

        public final /* synthetic */ zze zza(Context context, Looper looper, zzq zzq, Object obj, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
            return new zzbiw(context, looper, zzq, connectionCallbacks, onConnectionFailedListener, zza((ApiOptions) obj));
        }
    }

    public static final class zzb implements Optional {
    }

    private Drive() {
    }
}
