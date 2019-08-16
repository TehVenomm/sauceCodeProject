package com.google.android.gms.measurement.internal;

import android.content.Context;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.support.annotation.NonNull;
import com.google.android.gms.common.internal.BaseGmsClient;
import com.google.android.gms.common.internal.BaseGmsClient.BaseConnectionCallbacks;
import com.google.android.gms.common.internal.BaseGmsClient.BaseOnConnectionFailedListener;

public final class zzec extends BaseGmsClient<zzdx> {
    public zzec(Context context, Looper looper, BaseConnectionCallbacks baseConnectionCallbacks, BaseOnConnectionFailedListener baseOnConnectionFailedListener) {
        super(context, looper, 93, baseConnectionCallbacks, baseOnConnectionFailedListener, null);
    }

    public final /* synthetic */ IInterface createServiceInterface(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.measurement.internal.IMeasurementService");
        return queryLocalInterface instanceof zzdx ? (zzdx) queryLocalInterface : new zzdz(iBinder);
    }

    public final int getMinApkVersion() {
        return 12451000;
    }

    /* access modifiers changed from: protected */
    @NonNull
    public final String getServiceDescriptor() {
        return "com.google.android.gms.measurement.internal.IMeasurementService";
    }

    /* access modifiers changed from: protected */
    @NonNull
    public final String getStartServiceAction() {
        return "com.google.android.gms.measurement.START";
    }
}
