package com.google.android.gms.common.api.internal;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Keep;
import android.support.annotation.MainThread;
import java.io.FileDescriptor;
import java.io.PrintWriter;

public class LifecycleCallback {
    protected final zzcg zzfoi;

    protected LifecycleCallback(zzcg zzcg) {
        this.zzfoi = zzcg;
    }

    @Keep
    private static zzcg getChimeraLifecycleFragmentImpl(zzcf zzcf) {
        throw new IllegalStateException("Method not available in SDK.");
    }

    protected static zzcg zzb(zzcf zzcf) {
        if (zzcf.zzaif()) {
            return zzdb.zza(zzcf.zzaii());
        }
        if (zzcf.zzaig()) {
            return zzch.zzo(zzcf.zzaih());
        }
        throw new IllegalArgumentException("Can't get fragment for unexpected activity.");
    }

    public static zzcg zzn(Activity activity) {
        return zzb(new zzcf(activity));
    }

    @MainThread
    public void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
    }

    public final Activity getActivity() {
        return this.zzfoi.zzaij();
    }

    @MainThread
    public void onActivityResult(int i, int i2, Intent intent) {
    }

    @MainThread
    public void onCreate(Bundle bundle) {
    }

    @MainThread
    public void onDestroy() {
    }

    @MainThread
    public void onResume() {
    }

    @MainThread
    public void onSaveInstanceState(Bundle bundle) {
    }

    @MainThread
    public void onStart() {
    }

    @MainThread
    public void onStop() {
    }
}
