package com.google.android.gms.security;

import android.content.Context;
import android.os.AsyncTask;
import com.google.android.gms.common.GooglePlayServicesNotAvailableException;
import com.google.android.gms.common.GooglePlayServicesRepairableException;
import com.google.android.gms.common.zze;
import com.google.android.gms.security.ProviderInstaller.ProviderInstallListener;

final class zza extends AsyncTask<Void, Void, Integer> {
    private /* synthetic */ Context zzaok;
    private /* synthetic */ ProviderInstallListener zzjna;

    zza(Context context, ProviderInstallListener providerInstallListener) {
        this.zzaok = context;
        this.zzjna = providerInstallListener;
    }

    private final Integer zzb(Void... voidArr) {
        try {
            ProviderInstaller.installIfNeeded(this.zzaok);
            return Integer.valueOf(0);
        } catch (GooglePlayServicesRepairableException e) {
            return Integer.valueOf(e.getConnectionStatusCode());
        } catch (GooglePlayServicesNotAvailableException e2) {
            return Integer.valueOf(e2.errorCode);
        }
    }

    protected final /* synthetic */ Object doInBackground(Object[] objArr) {
        return zzb((Void[]) objArr);
    }

    protected final /* synthetic */ void onPostExecute(Object obj) {
        Integer num = (Integer) obj;
        if (num.intValue() == 0) {
            this.zzjna.onProviderInstalled();
            return;
        }
        ProviderInstaller.zzjmy;
        this.zzjna.onProviderInstallFailed(num.intValue(), zze.zza(this.zzaok, num.intValue(), "pi"));
    }
}
