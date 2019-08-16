package com.google.firebase.iid;

import android.os.Bundle;
import android.util.Log;
import com.google.android.gms.tasks.TaskCompletionSource;

abstract class zzaj<T> {
    final int what;
    final int zzck;
    final TaskCompletionSource<T> zzcl = new TaskCompletionSource<>();
    final Bundle zzcm;

    zzaj(int i, int i2, Bundle bundle) {
        this.zzck = i;
        this.what = i2;
        this.zzcm = bundle;
    }

    /* access modifiers changed from: 0000 */
    public final void finish(T t) {
        if (Log.isLoggable("MessengerIpcClient", 3)) {
            String valueOf = String.valueOf(this);
            String valueOf2 = String.valueOf(t);
            Log.d("MessengerIpcClient", new StringBuilder(String.valueOf(valueOf).length() + 16 + String.valueOf(valueOf2).length()).append("Finishing ").append(valueOf).append(" with ").append(valueOf2).toString());
        }
        this.zzcl.setResult(t);
    }

    public String toString() {
        int i = this.what;
        int i2 = this.zzck;
        return "Request { what=" + i + " id=" + i2 + " oneWay=" + zzab() + "}";
    }

    /* access modifiers changed from: 0000 */
    public final void zza(zzam zzam) {
        if (Log.isLoggable("MessengerIpcClient", 3)) {
            String valueOf = String.valueOf(this);
            String valueOf2 = String.valueOf(zzam);
            Log.d("MessengerIpcClient", new StringBuilder(String.valueOf(valueOf).length() + 14 + String.valueOf(valueOf2).length()).append("Failing ").append(valueOf).append(" with ").append(valueOf2).toString());
        }
        this.zzcl.setException(zzam);
    }

    /* access modifiers changed from: 0000 */
    public abstract boolean zzab();

    /* access modifiers changed from: 0000 */
    public abstract void zzb(Bundle bundle);
}
