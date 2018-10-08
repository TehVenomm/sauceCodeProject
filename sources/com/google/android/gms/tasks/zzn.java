package com.google.android.gms.tasks;

import android.app.Activity;
import android.support.annotation.MainThread;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.google.android.gms.common.api.internal.LifecycleCallback;
import com.google.android.gms.common.api.internal.zzcg;
import com.google.android.gms.common.internal.zzbp;
import java.lang.ref.WeakReference;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.Executor;

final class zzn<TResult> extends Task<TResult> {
    private final Object mLock = new Object();
    private final zzl<TResult> zzkfy = new zzl();
    private boolean zzkfz;
    private TResult zzkga;
    private Exception zzkgb;

    static class zza extends LifecycleCallback {
        private final List<WeakReference<zzk<?>>> mListeners = new ArrayList();

        private zza(zzcg zzcg) {
            super(zzcg);
            this.zzfoi.zza("TaskOnStopCallback", (LifecycleCallback) this);
        }

        public static zza zzr(Activity activity) {
            zzcg zzn = LifecycleCallback.zzn(activity);
            zza zza = (zza) zzn.zza("TaskOnStopCallback", zza.class);
            return zza == null ? new zza(zzn) : zza;
        }

        @MainThread
        public final void onStop() {
            synchronized (this.mListeners) {
                for (WeakReference weakReference : this.mListeners) {
                    zzk zzk = (zzk) weakReference.get();
                    if (zzk != null) {
                        zzk.cancel();
                    }
                }
                this.mListeners.clear();
            }
        }

        public final <T> void zzb(zzk<T> zzk) {
            synchronized (this.mListeners) {
                this.mListeners.add(new WeakReference(zzk));
            }
        }
    }

    zzn() {
    }

    private final void zzbic() {
        zzbp.zza(this.zzkfz, (Object) "Task is not yet complete");
    }

    private final void zzbid() {
        zzbp.zza(!this.zzkfz, (Object) "Task is already complete");
    }

    private final void zzbie() {
        synchronized (this.mLock) {
            if (this.zzkfz) {
                this.zzkfy.zza((Task) this);
                return;
            }
        }
    }

    @NonNull
    public final Task<TResult> addOnCompleteListener(@NonNull Activity activity, @NonNull OnCompleteListener<TResult> onCompleteListener) {
        zzk zze = new zze(TaskExecutors.MAIN_THREAD, onCompleteListener);
        this.zzkfy.zza(zze);
        zza.zzr(activity).zzb(zze);
        zzbie();
        return this;
    }

    @NonNull
    public final Task<TResult> addOnCompleteListener(@NonNull OnCompleteListener<TResult> onCompleteListener) {
        return addOnCompleteListener(TaskExecutors.MAIN_THREAD, (OnCompleteListener) onCompleteListener);
    }

    @NonNull
    public final Task<TResult> addOnCompleteListener(@NonNull Executor executor, @NonNull OnCompleteListener<TResult> onCompleteListener) {
        this.zzkfy.zza(new zze(executor, onCompleteListener));
        zzbie();
        return this;
    }

    @NonNull
    public final Task<TResult> addOnFailureListener(@NonNull Activity activity, @NonNull OnFailureListener onFailureListener) {
        zzk zzg = new zzg(TaskExecutors.MAIN_THREAD, onFailureListener);
        this.zzkfy.zza(zzg);
        zza.zzr(activity).zzb(zzg);
        zzbie();
        return this;
    }

    @NonNull
    public final Task<TResult> addOnFailureListener(@NonNull OnFailureListener onFailureListener) {
        return addOnFailureListener(TaskExecutors.MAIN_THREAD, onFailureListener);
    }

    @NonNull
    public final Task<TResult> addOnFailureListener(@NonNull Executor executor, @NonNull OnFailureListener onFailureListener) {
        this.zzkfy.zza(new zzg(executor, onFailureListener));
        zzbie();
        return this;
    }

    @NonNull
    public final Task<TResult> addOnSuccessListener(@NonNull Activity activity, @NonNull OnSuccessListener<? super TResult> onSuccessListener) {
        zzk zzi = new zzi(TaskExecutors.MAIN_THREAD, onSuccessListener);
        this.zzkfy.zza(zzi);
        zza.zzr(activity).zzb(zzi);
        zzbie();
        return this;
    }

    @NonNull
    public final Task<TResult> addOnSuccessListener(@NonNull OnSuccessListener<? super TResult> onSuccessListener) {
        return addOnSuccessListener(TaskExecutors.MAIN_THREAD, (OnSuccessListener) onSuccessListener);
    }

    @NonNull
    public final Task<TResult> addOnSuccessListener(@NonNull Executor executor, @NonNull OnSuccessListener<? super TResult> onSuccessListener) {
        this.zzkfy.zza(new zzi(executor, onSuccessListener));
        zzbie();
        return this;
    }

    @NonNull
    public final <TContinuationResult> Task<TContinuationResult> continueWith(@NonNull Continuation<TResult, TContinuationResult> continuation) {
        return continueWith(TaskExecutors.MAIN_THREAD, continuation);
    }

    @NonNull
    public final <TContinuationResult> Task<TContinuationResult> continueWith(@NonNull Executor executor, @NonNull Continuation<TResult, TContinuationResult> continuation) {
        Task zzn = new zzn();
        this.zzkfy.zza(new zza(executor, continuation, zzn));
        zzbie();
        return zzn;
    }

    @NonNull
    public final <TContinuationResult> Task<TContinuationResult> continueWithTask(@NonNull Continuation<TResult, Task<TContinuationResult>> continuation) {
        return continueWithTask(TaskExecutors.MAIN_THREAD, continuation);
    }

    @NonNull
    public final <TContinuationResult> Task<TContinuationResult> continueWithTask(@NonNull Executor executor, @NonNull Continuation<TResult, Task<TContinuationResult>> continuation) {
        Task zzn = new zzn();
        this.zzkfy.zza(new zzc(executor, continuation, zzn));
        zzbie();
        return zzn;
    }

    @Nullable
    public final Exception getException() {
        Exception exception;
        synchronized (this.mLock) {
            exception = this.zzkgb;
        }
        return exception;
    }

    public final TResult getResult() {
        TResult tResult;
        synchronized (this.mLock) {
            zzbic();
            if (this.zzkgb != null) {
                throw new RuntimeExecutionException(this.zzkgb);
            }
            tResult = this.zzkga;
        }
        return tResult;
    }

    public final <X extends Throwable> TResult getResult(@NonNull Class<X> cls) throws Throwable {
        TResult tResult;
        synchronized (this.mLock) {
            zzbic();
            if (cls.isInstance(this.zzkgb)) {
                throw ((Throwable) cls.cast(this.zzkgb));
            } else if (this.zzkgb != null) {
                throw new RuntimeExecutionException(this.zzkgb);
            } else {
                tResult = this.zzkga;
            }
        }
        return tResult;
    }

    public final boolean isComplete() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzkfz;
        }
        return z;
    }

    public final boolean isSuccessful() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzkfz && this.zzkgb == null;
        }
        return z;
    }

    public final void setException(@NonNull Exception exception) {
        zzbp.zzb((Object) exception, (Object) "Exception must not be null");
        synchronized (this.mLock) {
            zzbid();
            this.zzkfz = true;
            this.zzkgb = exception;
        }
        this.zzkfy.zza((Task) this);
    }

    public final void setResult(TResult tResult) {
        synchronized (this.mLock) {
            zzbid();
            this.zzkfz = true;
            this.zzkga = tResult;
        }
        this.zzkfy.zza((Task) this);
    }

    public final boolean trySetException(@NonNull Exception exception) {
        boolean z = true;
        zzbp.zzb((Object) exception, (Object) "Exception must not be null");
        synchronized (this.mLock) {
            if (this.zzkfz) {
                z = false;
            } else {
                this.zzkfz = true;
                this.zzkgb = exception;
                this.zzkfy.zza((Task) this);
            }
        }
        return z;
    }

    public final boolean trySetResult(TResult tResult) {
        boolean z = true;
        synchronized (this.mLock) {
            if (this.zzkfz) {
                z = false;
            } else {
                this.zzkfz = true;
                this.zzkga = tResult;
                this.zzkfy.zza((Task) this);
            }
        }
        return z;
    }
}
