package com.google.android.gms.internal;

import android.content.Context;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.support.annotation.NonNull;
import com.google.android.gms.common.internal.zzd;
import com.google.android.gms.common.internal.zzf;
import com.google.android.gms.common.internal.zzg;

public final class zzcbn extends zzd<zzcbg> {
    public zzcbn(Context context, Looper looper, zzf zzf, zzg zzg) {
        super(context, looper, 93, zzf, zzg, null);
    }

    public final /* synthetic */ IInterface zze(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.measurement.internal.IMeasurementService");
        return queryLocalInterface instanceof zzcbg ? (zzcbg) queryLocalInterface : new zzcbi(iBinder);
    }

    @NonNull
    protected final String zzhc() {
        return "com.google.android.gms.measurement.START";
    }

    @NonNull
    protected final String zzhd() {
        return "com.google.android.gms.measurement.internal.IMeasurementService";
    }
}
