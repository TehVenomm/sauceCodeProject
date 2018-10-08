package com.google.android.gms.common.internal;

import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.RemoteException;
import android.support.annotation.BinderThread;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;

public final class zzn extends zze {
    private /* synthetic */ zzd zzftf;
    private IBinder zzftj;

    @BinderThread
    public zzn(zzd zzd, int i, IBinder iBinder, Bundle bundle) {
        this.zzftf = zzd;
        super(zzd, i, bundle);
        this.zzftj = iBinder;
    }

    protected final boolean zzajn() {
        try {
            String interfaceDescriptor = this.zzftj.getInterfaceDescriptor();
            if (this.zzftf.zzhd().equals(interfaceDescriptor)) {
                IInterface zze = this.zzftf.zze(this.zzftj);
                if (zze == null) {
                    return false;
                }
                if (!this.zzftf.zza(2, 4, zze) && !this.zzftf.zza(3, 4, zze)) {
                    return false;
                }
                this.zzftf.zzfta = null;
                Bundle zzaeg = this.zzftf.zzaeg();
                if (this.zzftf.zzfsw != null) {
                    this.zzftf.zzfsw.onConnected(zzaeg);
                }
                return true;
            }
            String zzhd = this.zzftf.zzhd();
            Log.e("GmsClient", new StringBuilder((String.valueOf(zzhd).length() + 34) + String.valueOf(interfaceDescriptor).length()).append("service descriptor mismatch: ").append(zzhd).append(" vs. ").append(interfaceDescriptor).toString());
            return false;
        } catch (RemoteException e) {
            Log.w("GmsClient", "service probably died");
            return false;
        }
    }

    protected final void zzj(ConnectionResult connectionResult) {
        if (this.zzftf.zzfsx != null) {
            this.zzftf.zzfsx.onConnectionFailed(connectionResult);
        }
        this.zzftf.onConnectionFailed(connectionResult);
    }
}
