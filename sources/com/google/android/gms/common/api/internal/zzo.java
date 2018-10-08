package com.google.android.gms.common.api.internal;

import android.app.PendingIntent;
import android.content.DialogInterface;
import android.content.DialogInterface.OnCancelListener;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.Nullable;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GoogleApiAvailability;
import java.util.concurrent.atomic.AtomicReference;

public abstract class zzo extends LifecycleCallback implements OnCancelListener {
    protected volatile boolean mStarted;
    protected final GoogleApiAvailability zzfhf;
    protected final AtomicReference<zzp> zzfiq;
    private final Handler zzfir;

    protected zzo(zzcg zzcg) {
        this(zzcg, GoogleApiAvailability.getInstance());
    }

    private zzo(zzcg zzcg, GoogleApiAvailability googleApiAvailability) {
        super(zzcg);
        this.zzfiq = new AtomicReference(null);
        this.zzfir = new Handler(Looper.getMainLooper());
        this.zzfhf = googleApiAvailability;
    }

    private static int zza(@Nullable zzp zzp) {
        return zzp == null ? -1 : zzp.zzagb();
    }

    public final void onActivityResult(int i, int i2, Intent intent) {
        int i3 = 13;
        Object obj = null;
        zzp zzp = (zzp) this.zzfiq.get();
        switch (i) {
            case 1:
                if (i2 != -1) {
                    if (i2 == 0) {
                        if (intent != null) {
                            i3 = intent.getIntExtra("<<ResolutionFailureErrorDetail>>", 13);
                        }
                        zzp zzp2 = new zzp(new ConnectionResult(i3, null), zza(zzp));
                        this.zzfiq.set(zzp2);
                        zzp = zzp2;
                        break;
                    }
                }
                int i4 = 1;
                break;
                break;
            case 2:
                int isGooglePlayServicesAvailable = this.zzfhf.isGooglePlayServicesAvailable(getActivity());
                Object obj2 = isGooglePlayServicesAvailable == 0 ? 1 : null;
                if (zzp == null) {
                    return;
                }
                if (zzp.zzagc().getErrorCode() != 18 || isGooglePlayServicesAvailable != 18) {
                    obj = obj2;
                    break;
                }
                return;
        }
        if (obj != null) {
            zzaga();
        } else if (zzp != null) {
            zza(zzp.zzagc(), zzp.zzagb());
        }
    }

    public void onCancel(DialogInterface dialogInterface) {
        zza(new ConnectionResult(13, null), zza((zzp) this.zzfiq.get()));
        zzaga();
    }

    public final void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        if (bundle != null) {
            this.zzfiq.set(bundle.getBoolean("resolving_error", false) ? new zzp(new ConnectionResult(bundle.getInt("failed_status"), (PendingIntent) bundle.getParcelable("failed_resolution")), bundle.getInt("failed_client_id", -1)) : null);
        }
    }

    public final void onSaveInstanceState(Bundle bundle) {
        super.onSaveInstanceState(bundle);
        zzp zzp = (zzp) this.zzfiq.get();
        if (zzp != null) {
            bundle.putBoolean("resolving_error", true);
            bundle.putInt("failed_client_id", zzp.zzagb());
            bundle.putInt("failed_status", zzp.zzagc().getErrorCode());
            bundle.putParcelable("failed_resolution", zzp.zzagc().getResolution());
        }
    }

    public void onStart() {
        super.onStart();
        this.mStarted = true;
    }

    public void onStop() {
        super.onStop();
        this.mStarted = false;
    }

    protected abstract void zza(ConnectionResult connectionResult, int i);

    protected abstract void zzafv();

    protected final void zzaga() {
        this.zzfiq.set(null);
        zzafv();
    }

    public final void zzb(ConnectionResult connectionResult, int i) {
        zzp zzp = new zzp(connectionResult, i);
        if (this.zzfiq.compareAndSet(null, zzp)) {
            this.zzfir.post(new zzq(this, zzp));
        }
    }
}
