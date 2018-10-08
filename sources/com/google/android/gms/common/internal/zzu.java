package com.google.android.gms.common.internal;

import android.app.Activity;
import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.content.Intent;
import android.support.annotation.NonNull;
import android.support.v4.app.Fragment;
import android.util.Log;
import com.google.android.gms.common.api.internal.zzcg;

public abstract class zzu implements OnClickListener {
    public static zzu zza(Activity activity, Intent intent, int i) {
        return new zzv(intent, activity, i);
    }

    public static zzu zza(@NonNull Fragment fragment, Intent intent, int i) {
        return new zzw(intent, fragment, i);
    }

    public static zzu zza(@NonNull zzcg zzcg, Intent intent, int i) {
        return new zzx(intent, zzcg, 2);
    }

    public void onClick(DialogInterface dialogInterface, int i) {
        try {
            zzaka();
        } catch (Throwable e) {
            Log.e("DialogRedirect", "Failed to start resolution intent", e);
        } finally {
            dialogInterface.dismiss();
        }
    }

    protected abstract void zzaka();
}
