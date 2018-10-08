package com.google.android.gms.tasks;

import android.support.annotation.NonNull;
import com.google.android.gms.common.internal.zzbp;
import java.util.Arrays;
import java.util.Collection;
import java.util.concurrent.Callable;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Executor;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;

public final class Tasks {

    interface zzb extends OnFailureListener, OnSuccessListener<Object> {
    }

    static final class zza implements zzb {
        private final CountDownLatch zzaop;

        private zza() {
            this.zzaop = new CountDownLatch(1);
        }

        public final void await() throws InterruptedException {
            this.zzaop.await();
        }

        public final boolean await(long j, TimeUnit timeUnit) throws InterruptedException {
            return this.zzaop.await(j, timeUnit);
        }

        public final void onFailure(@NonNull Exception exception) {
            this.zzaop.countDown();
        }

        public final void onSuccess(Object obj) {
            this.zzaop.countDown();
        }
    }

    static final class zzc implements zzb {
        private final Object mLock = new Object();
        private final zzn<Void> zzkfw;
        private Exception zzkgb;
        private final int zzkgd;
        private int zzkge;
        private int zzkgf;

        public zzc(int i, zzn<Void> zzn) {
            this.zzkgd = i;
            this.zzkfw = zzn;
        }

        private final void zzbif() {
            if (this.zzkge + this.zzkgf != this.zzkgd) {
                return;
            }
            if (this.zzkgb == null) {
                this.zzkfw.setResult(null);
                return;
            }
            zzn zzn = this.zzkfw;
            int i = this.zzkgf;
            zzn.setException(new ExecutionException(i + " out of " + this.zzkgd + " underlying tasks failed", this.zzkgb));
        }

        public final void onFailure(@NonNull Exception exception) {
            synchronized (this.mLock) {
                this.zzkgf++;
                this.zzkgb = exception;
                zzbif();
            }
        }

        public final void onSuccess(Object obj) {
            synchronized (this.mLock) {
                this.zzkge++;
                zzbif();
            }
        }
    }

    private Tasks() {
    }

    public static <TResult> TResult await(@NonNull Task<TResult> task) throws ExecutionException, InterruptedException {
        zzbp.zzgg("Must not be called on the main application thread");
        zzbp.zzb((Object) task, (Object) "Task must not be null");
        if (task.isComplete()) {
            return zzb(task);
        }
        Object zza = new zza();
        zza(task, zza);
        zza.await();
        return zzb(task);
    }

    public static <TResult> TResult await(@NonNull Task<TResult> task, long j, @NonNull TimeUnit timeUnit) throws ExecutionException, InterruptedException, TimeoutException {
        zzbp.zzgg("Must not be called on the main application thread");
        zzbp.zzb((Object) task, (Object) "Task must not be null");
        zzbp.zzb((Object) timeUnit, (Object) "TimeUnit must not be null");
        if (task.isComplete()) {
            return zzb(task);
        }
        Object zza = new zza();
        zza(task, zza);
        if (zza.await(j, timeUnit)) {
            return zzb(task);
        }
        throw new TimeoutException("Timed out waiting for Task");
    }

    public static <TResult> Task<TResult> call(@NonNull Callable<TResult> callable) {
        return call(TaskExecutors.MAIN_THREAD, callable);
    }

    public static <TResult> Task<TResult> call(@NonNull Executor executor, @NonNull Callable<TResult> callable) {
        zzbp.zzb((Object) executor, (Object) "Executor must not be null");
        zzbp.zzb((Object) callable, (Object) "Callback must not be null");
        Task zzn = new zzn();
        executor.execute(new zzo(zzn, callable));
        return zzn;
    }

    public static <TResult> Task<TResult> forException(@NonNull Exception exception) {
        Task zzn = new zzn();
        zzn.setException(exception);
        return zzn;
    }

    public static <TResult> Task<TResult> forResult(TResult tResult) {
        Task zzn = new zzn();
        zzn.setResult(tResult);
        return zzn;
    }

    public static Task<Void> whenAll(Collection<? extends Task<?>> collection) {
        if (collection.isEmpty()) {
            return forResult(null);
        }
        for (Task task : collection) {
            if (task == null) {
                throw new NullPointerException("null tasks are not accepted");
            }
        }
        Task zzn = new zzn();
        zzb zzc = new zzc(collection.size(), zzn);
        for (Task task2 : collection) {
            zza(task2, zzc);
        }
        return zzn;
    }

    public static Task<Void> whenAll(Task<?>... taskArr) {
        return taskArr.length == 0 ? forResult(null) : whenAll(Arrays.asList(taskArr));
    }

    private static void zza(Task<?> task, zzb zzb) {
        task.addOnSuccessListener(TaskExecutors.zzkfx, (OnSuccessListener) zzb);
        task.addOnFailureListener(TaskExecutors.zzkfx, (OnFailureListener) zzb);
    }

    private static <TResult> TResult zzb(Task<TResult> task) throws ExecutionException {
        if (task.isSuccessful()) {
            return task.getResult();
        }
        throw new ExecutionException(task.getException());
    }
}
