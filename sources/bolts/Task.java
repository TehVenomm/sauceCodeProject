package bolts;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.List;
import java.util.concurrent.Callable;
import java.util.concurrent.CancellationException;
import java.util.concurrent.Executor;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.AtomicInteger;

public class Task<TResult> {
    public static final ExecutorService BACKGROUND_EXECUTOR = BoltsExecutors.background();
    private static final Executor IMMEDIATE_EXECUTOR = BoltsExecutors.immediate();
    private static Task<?> TASK_CANCELLED = new Task(true);
    private static Task<Boolean> TASK_FALSE = new Task(Boolean.valueOf(false));
    private static Task<?> TASK_NULL = new Task(null);
    private static Task<Boolean> TASK_TRUE = new Task(Boolean.valueOf(true));
    public static final Executor UI_THREAD_EXECUTOR = AndroidExecutors.uiThread();
    private static volatile UnobservedExceptionHandler unobservedExceptionHandler;
    private boolean cancelled;
    private boolean complete;
    private List<Continuation<TResult, Void>> continuations = new ArrayList();
    private Exception error;
    private boolean errorHasBeenObserved;
    private final Object lock = new Object();
    private TResult result;
    private UnobservedErrorNotifier unobservedErrorNotifier;

    /* renamed from: bolts.Task$3 */
    class C01733 implements Continuation<TResult, Task<Void>> {
        C01733() {
        }

        public Task<Void> then(Task<TResult> task) throws Exception {
            return task.isCancelled() ? Task.cancelled() : task.isFaulted() ? Task.forError(task.getError()) : Task.forResult(null);
        }
    }

    public class TaskCompletionSource extends TaskCompletionSource<TResult> {
        TaskCompletionSource() {
        }
    }

    public interface UnobservedExceptionHandler {
        void unobservedException(Task<?> task, UnobservedTaskException unobservedTaskException);
    }

    Task() {
    }

    private Task(TResult tResult) {
        trySetResult(tResult);
    }

    private Task(boolean z) {
        if (z) {
            trySetCancelled();
        } else {
            trySetResult(null);
        }
    }

    public static <TResult> Task<TResult> call(Callable<TResult> callable) {
        return call(callable, IMMEDIATE_EXECUTOR, null);
    }

    public static <TResult> Task<TResult> call(Callable<TResult> callable, CancellationToken cancellationToken) {
        return call(callable, IMMEDIATE_EXECUTOR, cancellationToken);
    }

    public static <TResult> Task<TResult> call(Callable<TResult> callable, Executor executor) {
        return call(callable, executor, null);
    }

    public static <TResult> Task<TResult> call(final Callable<TResult> callable, Executor executor, final CancellationToken cancellationToken) {
        final TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        try {
            executor.execute(new Runnable() {
                public void run() {
                    if (cancellationToken == null || !cancellationToken.isCancellationRequested()) {
                        try {
                            taskCompletionSource.setResult(callable.call());
                            return;
                        } catch (CancellationException e) {
                            taskCompletionSource.setCancelled();
                            return;
                        } catch (Exception e2) {
                            taskCompletionSource.setError(e2);
                            return;
                        }
                    }
                    taskCompletionSource.setCancelled();
                }
            });
        } catch (Exception e) {
            taskCompletionSource.setError(new ExecutorException(e));
        }
        return taskCompletionSource.getTask();
    }

    public static <TResult> Task<TResult> callInBackground(Callable<TResult> callable) {
        return call(callable, BACKGROUND_EXECUTOR, null);
    }

    public static <TResult> Task<TResult> callInBackground(Callable<TResult> callable, CancellationToken cancellationToken) {
        return call(callable, BACKGROUND_EXECUTOR, cancellationToken);
    }

    public static <TResult> Task<TResult> cancelled() {
        return TASK_CANCELLED;
    }

    private static <TContinuationResult, TResult> void completeAfterTask(final TaskCompletionSource<TContinuationResult> taskCompletionSource, final Continuation<TResult, Task<TContinuationResult>> continuation, final Task<TResult> task, Executor executor, final CancellationToken cancellationToken) {
        try {
            executor.execute(new Runnable() {

                /* renamed from: bolts.Task$15$1 */
                class C01701 implements Continuation<TContinuationResult, Void> {
                    C01701() {
                    }

                    public Void then(Task<TContinuationResult> task) {
                        if (cancellationToken != null && cancellationToken.isCancellationRequested()) {
                            taskCompletionSource.setCancelled();
                        } else if (task.isCancelled()) {
                            taskCompletionSource.setCancelled();
                        } else if (task.isFaulted()) {
                            taskCompletionSource.setError(task.getError());
                        } else {
                            taskCompletionSource.setResult(task.getResult());
                        }
                        return null;
                    }
                }

                public void run() {
                    if (cancellationToken == null || !cancellationToken.isCancellationRequested()) {
                        try {
                            Task task = (Task) continuation.then(task);
                            if (task == null) {
                                taskCompletionSource.setResult(null);
                                return;
                            } else {
                                task.continueWith(new C01701());
                                return;
                            }
                        } catch (CancellationException e) {
                            taskCompletionSource.setCancelled();
                            return;
                        } catch (Exception e2) {
                            taskCompletionSource.setError(e2);
                            return;
                        }
                    }
                    taskCompletionSource.setCancelled();
                }
            });
        } catch (Exception e) {
            taskCompletionSource.setError(new ExecutorException(e));
        }
    }

    private static <TContinuationResult, TResult> void completeImmediately(final TaskCompletionSource<TContinuationResult> taskCompletionSource, final Continuation<TResult, TContinuationResult> continuation, final Task<TResult> task, Executor executor, final CancellationToken cancellationToken) {
        try {
            executor.execute(new Runnable() {
                public void run() {
                    if (cancellationToken == null || !cancellationToken.isCancellationRequested()) {
                        try {
                            taskCompletionSource.setResult(continuation.then(task));
                            return;
                        } catch (CancellationException e) {
                            taskCompletionSource.setCancelled();
                            return;
                        } catch (Exception e2) {
                            taskCompletionSource.setError(e2);
                            return;
                        }
                    }
                    taskCompletionSource.setCancelled();
                }
            });
        } catch (Exception e) {
            taskCompletionSource.setError(new ExecutorException(e));
        }
    }

    public static <TResult> TaskCompletionSource create() {
        Task task = new Task();
        task.getClass();
        return new TaskCompletionSource();
    }

    public static Task<Void> delay(long j) {
        return delay(j, BoltsExecutors.scheduled(), null);
    }

    public static Task<Void> delay(long j, CancellationToken cancellationToken) {
        return delay(j, BoltsExecutors.scheduled(), cancellationToken);
    }

    static Task<Void> delay(long j, ScheduledExecutorService scheduledExecutorService, CancellationToken cancellationToken) {
        if (cancellationToken != null && cancellationToken.isCancellationRequested()) {
            return cancelled();
        }
        if (j <= 0) {
            return forResult(null);
        }
        final TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        final ScheduledFuture schedule = scheduledExecutorService.schedule(new Runnable() {
            public void run() {
                taskCompletionSource.trySetResult(null);
            }
        }, j, TimeUnit.MILLISECONDS);
        if (cancellationToken != null) {
            cancellationToken.register(new Runnable() {
                public void run() {
                    schedule.cancel(true);
                    taskCompletionSource.trySetCancelled();
                }
            });
        }
        return taskCompletionSource.getTask();
    }

    public static <TResult> Task<TResult> forError(Exception exception) {
        TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        taskCompletionSource.setError(exception);
        return taskCompletionSource.getTask();
    }

    public static <TResult> Task<TResult> forResult(TResult tResult) {
        if (tResult == null) {
            return TASK_NULL;
        }
        if (tResult instanceof Boolean) {
            return ((Boolean) tResult).booleanValue() ? TASK_TRUE : TASK_FALSE;
        } else {
            TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
            taskCompletionSource.setResult(tResult);
            return taskCompletionSource.getTask();
        }
    }

    public static UnobservedExceptionHandler getUnobservedExceptionHandler() {
        return unobservedExceptionHandler;
    }

    private void runContinuations() {
        synchronized (this.lock) {
            for (Continuation then : this.continuations) {
                try {
                    then.then(this);
                } catch (RuntimeException e) {
                    throw e;
                } catch (Throwable e2) {
                    throw new RuntimeException(e2);
                }
            }
            this.continuations = null;
        }
    }

    public static void setUnobservedExceptionHandler(UnobservedExceptionHandler unobservedExceptionHandler) {
        unobservedExceptionHandler = unobservedExceptionHandler;
    }

    public static Task<Void> whenAll(Collection<? extends Task<?>> collection) {
        if (collection.size() == 0) {
            return forResult(null);
        }
        final TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        final ArrayList arrayList = new ArrayList();
        final Object obj = new Object();
        final AtomicInteger atomicInteger = new AtomicInteger(collection.size());
        final AtomicBoolean atomicBoolean = new AtomicBoolean(false);
        for (Task continueWith : collection) {
            continueWith.continueWith(new Continuation<Object, Void>() {
                public Void then(Task<Object> task) {
                    if (task.isFaulted()) {
                        synchronized (obj) {
                            arrayList.add(task.getError());
                        }
                    }
                    if (task.isCancelled()) {
                        atomicBoolean.set(true);
                    }
                    if (atomicInteger.decrementAndGet() == 0) {
                        if (arrayList.size() != 0) {
                            if (arrayList.size() == 1) {
                                taskCompletionSource.setError((Exception) arrayList.get(0));
                            } else {
                                taskCompletionSource.setError(new AggregateException(String.format("There were %d exceptions.", new Object[]{Integer.valueOf(arrayList.size())}), arrayList));
                            }
                        } else if (atomicBoolean.get()) {
                            taskCompletionSource.setCancelled();
                        } else {
                            taskCompletionSource.setResult(null);
                        }
                    }
                    return null;
                }
            });
        }
        return taskCompletionSource.getTask();
    }

    public static <TResult> Task<List<TResult>> whenAllResult(final Collection<? extends Task<TResult>> collection) {
        return whenAll(collection).onSuccess(new Continuation<Void, List<TResult>>() {
            public List<TResult> then(Task<Void> task) throws Exception {
                if (collection.size() == 0) {
                    return Collections.emptyList();
                }
                List<TResult> arrayList = new ArrayList();
                for (Task result : collection) {
                    arrayList.add(result.getResult());
                }
                return arrayList;
            }
        });
    }

    public static Task<Task<?>> whenAny(Collection<? extends Task<?>> collection) {
        if (collection.size() == 0) {
            return forResult(null);
        }
        final TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        final AtomicBoolean atomicBoolean = new AtomicBoolean(false);
        for (Task continueWith : collection) {
            continueWith.continueWith(new Continuation<Object, Void>() {
                public Void then(Task<Object> task) {
                    if (atomicBoolean.compareAndSet(false, true)) {
                        taskCompletionSource.setResult(task);
                    } else {
                        task.getError();
                    }
                    return null;
                }
            });
        }
        return taskCompletionSource.getTask();
    }

    public static <TResult> Task<Task<TResult>> whenAnyResult(Collection<? extends Task<TResult>> collection) {
        if (collection.size() == 0) {
            return forResult(null);
        }
        final TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        final AtomicBoolean atomicBoolean = new AtomicBoolean(false);
        for (Task continueWith : collection) {
            continueWith.continueWith(new Continuation<TResult, Void>() {
                public Void then(Task<TResult> task) {
                    if (atomicBoolean.compareAndSet(false, true)) {
                        taskCompletionSource.setResult(task);
                    } else {
                        task.getError();
                    }
                    return null;
                }
            });
        }
        return taskCompletionSource.getTask();
    }

    public <TOut> Task<TOut> cast() {
        return this;
    }

    public Task<Void> continueWhile(Callable<Boolean> callable, Continuation<Void, Task<Void>> continuation) {
        return continueWhile(callable, continuation, IMMEDIATE_EXECUTOR, null);
    }

    public Task<Void> continueWhile(Callable<Boolean> callable, Continuation<Void, Task<Void>> continuation, CancellationToken cancellationToken) {
        return continueWhile(callable, continuation, IMMEDIATE_EXECUTOR, cancellationToken);
    }

    public Task<Void> continueWhile(Callable<Boolean> callable, Continuation<Void, Task<Void>> continuation, Executor executor) {
        return continueWhile(callable, continuation, executor, null);
    }

    public Task<Void> continueWhile(Callable<Boolean> callable, Continuation<Void, Task<Void>> continuation, Executor executor, CancellationToken cancellationToken) {
        final Capture capture = new Capture();
        final CancellationToken cancellationToken2 = cancellationToken;
        final Callable<Boolean> callable2 = callable;
        final Continuation<Void, Task<Void>> continuation2 = continuation;
        final Executor executor2 = executor;
        capture.set(new Continuation<Void, Task<Void>>() {
            public Task<Void> then(Task<Void> task) throws Exception {
                return (cancellationToken2 == null || !cancellationToken2.isCancellationRequested()) ? ((Boolean) callable2.call()).booleanValue() ? Task.forResult(null).onSuccessTask(continuation2, executor2).onSuccessTask((Continuation) capture.get(), executor2) : Task.forResult(null) : Task.cancelled();
            }
        });
        return makeVoid().continueWithTask((Continuation) capture.get(), executor);
    }

    public <TContinuationResult> Task<TContinuationResult> continueWith(Continuation<TResult, TContinuationResult> continuation) {
        return continueWith(continuation, IMMEDIATE_EXECUTOR, null);
    }

    public <TContinuationResult> Task<TContinuationResult> continueWith(Continuation<TResult, TContinuationResult> continuation, CancellationToken cancellationToken) {
        return continueWith(continuation, IMMEDIATE_EXECUTOR, cancellationToken);
    }

    public <TContinuationResult> Task<TContinuationResult> continueWith(Continuation<TResult, TContinuationResult> continuation, Executor executor) {
        return continueWith(continuation, executor, null);
    }

    public <TContinuationResult> Task<TContinuationResult> continueWith(Continuation<TResult, TContinuationResult> continuation, Executor executor, CancellationToken cancellationToken) {
        final TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        synchronized (this.lock) {
            boolean isCompleted = isCompleted();
            if (!isCompleted) {
                final Continuation<TResult, TContinuationResult> continuation2 = continuation;
                final Executor executor2 = executor;
                final CancellationToken cancellationToken2 = cancellationToken;
                this.continuations.add(new Continuation<TResult, Void>() {
                    public Void then(Task<TResult> task) {
                        Task.completeImmediately(taskCompletionSource, continuation2, task, executor2, cancellationToken2);
                        return null;
                    }
                });
            }
        }
        if (isCompleted) {
            completeImmediately(taskCompletionSource, continuation, this, executor, cancellationToken);
        }
        return taskCompletionSource.getTask();
    }

    public <TContinuationResult> Task<TContinuationResult> continueWithTask(Continuation<TResult, Task<TContinuationResult>> continuation) {
        return continueWithTask(continuation, IMMEDIATE_EXECUTOR, null);
    }

    public <TContinuationResult> Task<TContinuationResult> continueWithTask(Continuation<TResult, Task<TContinuationResult>> continuation, CancellationToken cancellationToken) {
        return continueWithTask(continuation, IMMEDIATE_EXECUTOR, cancellationToken);
    }

    public <TContinuationResult> Task<TContinuationResult> continueWithTask(Continuation<TResult, Task<TContinuationResult>> continuation, Executor executor) {
        return continueWithTask(continuation, executor, null);
    }

    public <TContinuationResult> Task<TContinuationResult> continueWithTask(Continuation<TResult, Task<TContinuationResult>> continuation, Executor executor, CancellationToken cancellationToken) {
        final TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        synchronized (this.lock) {
            boolean isCompleted = isCompleted();
            if (!isCompleted) {
                final Continuation<TResult, Task<TContinuationResult>> continuation2 = continuation;
                final Executor executor2 = executor;
                final CancellationToken cancellationToken2 = cancellationToken;
                this.continuations.add(new Continuation<TResult, Void>() {
                    public Void then(Task<TResult> task) {
                        Task.completeAfterTask(taskCompletionSource, continuation2, task, executor2, cancellationToken2);
                        return null;
                    }
                });
            }
        }
        if (isCompleted) {
            completeAfterTask(taskCompletionSource, continuation, this, executor, cancellationToken);
        }
        return taskCompletionSource.getTask();
    }

    public Exception getError() {
        Exception exception;
        synchronized (this.lock) {
            if (this.error != null) {
                this.errorHasBeenObserved = true;
                if (this.unobservedErrorNotifier != null) {
                    this.unobservedErrorNotifier.setObserved();
                    this.unobservedErrorNotifier = null;
                }
            }
            exception = this.error;
        }
        return exception;
    }

    public TResult getResult() {
        TResult tResult;
        synchronized (this.lock) {
            tResult = this.result;
        }
        return tResult;
    }

    public boolean isCancelled() {
        boolean z;
        synchronized (this.lock) {
            z = this.cancelled;
        }
        return z;
    }

    public boolean isCompleted() {
        boolean z;
        synchronized (this.lock) {
            z = this.complete;
        }
        return z;
    }

    public boolean isFaulted() {
        boolean z;
        synchronized (this.lock) {
            z = getError() != null;
        }
        return z;
    }

    public Task<Void> makeVoid() {
        return continueWithTask(new C01733());
    }

    public <TContinuationResult> Task<TContinuationResult> onSuccess(Continuation<TResult, TContinuationResult> continuation) {
        return onSuccess(continuation, IMMEDIATE_EXECUTOR, null);
    }

    public <TContinuationResult> Task<TContinuationResult> onSuccess(Continuation<TResult, TContinuationResult> continuation, CancellationToken cancellationToken) {
        return onSuccess(continuation, IMMEDIATE_EXECUTOR, cancellationToken);
    }

    public <TContinuationResult> Task<TContinuationResult> onSuccess(Continuation<TResult, TContinuationResult> continuation, Executor executor) {
        return onSuccess(continuation, executor, null);
    }

    public <TContinuationResult> Task<TContinuationResult> onSuccess(final Continuation<TResult, TContinuationResult> continuation, Executor executor, final CancellationToken cancellationToken) {
        return continueWithTask(new Continuation<TResult, Task<TContinuationResult>>() {
            public Task<TContinuationResult> then(Task<TResult> task) {
                return (cancellationToken == null || !cancellationToken.isCancellationRequested()) ? task.isFaulted() ? Task.forError(task.getError()) : task.isCancelled() ? Task.cancelled() : task.continueWith(continuation) : Task.cancelled();
            }
        }, executor);
    }

    public <TContinuationResult> Task<TContinuationResult> onSuccessTask(Continuation<TResult, Task<TContinuationResult>> continuation) {
        return onSuccessTask((Continuation) continuation, IMMEDIATE_EXECUTOR);
    }

    public <TContinuationResult> Task<TContinuationResult> onSuccessTask(Continuation<TResult, Task<TContinuationResult>> continuation, CancellationToken cancellationToken) {
        return onSuccessTask(continuation, IMMEDIATE_EXECUTOR, cancellationToken);
    }

    public <TContinuationResult> Task<TContinuationResult> onSuccessTask(Continuation<TResult, Task<TContinuationResult>> continuation, Executor executor) {
        return onSuccessTask(continuation, executor, null);
    }

    public <TContinuationResult> Task<TContinuationResult> onSuccessTask(final Continuation<TResult, Task<TContinuationResult>> continuation, Executor executor, final CancellationToken cancellationToken) {
        return continueWithTask(new Continuation<TResult, Task<TContinuationResult>>() {
            public Task<TContinuationResult> then(Task<TResult> task) {
                return (cancellationToken == null || !cancellationToken.isCancellationRequested()) ? task.isFaulted() ? Task.forError(task.getError()) : task.isCancelled() ? Task.cancelled() : task.continueWithTask(continuation) : Task.cancelled();
            }
        }, executor);
    }

    boolean trySetCancelled() {
        boolean z = true;
        synchronized (this.lock) {
            if (this.complete) {
                z = false;
            } else {
                this.complete = true;
                this.cancelled = true;
                this.lock.notifyAll();
                runContinuations();
            }
        }
        return z;
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    boolean trySetError(java.lang.Exception r5) {
        /*
        r4 = this;
        r1 = 1;
        r0 = 0;
        r2 = r4.lock;
        monitor-enter(r2);
        r3 = r4.complete;	 Catch:{ all -> 0x002f }
        if (r3 == 0) goto L_0x000b;
    L_0x0009:
        monitor-exit(r2);	 Catch:{ all -> 0x002f }
    L_0x000a:
        return r0;
    L_0x000b:
        r0 = 1;
        r4.complete = r0;	 Catch:{ all -> 0x002f }
        r4.error = r5;	 Catch:{ all -> 0x002f }
        r0 = 0;
        r4.errorHasBeenObserved = r0;	 Catch:{ all -> 0x002f }
        r0 = r4.lock;	 Catch:{ all -> 0x002f }
        r0.notifyAll();	 Catch:{ all -> 0x002f }
        r4.runContinuations();	 Catch:{ all -> 0x002f }
        r0 = r4.errorHasBeenObserved;	 Catch:{ all -> 0x002f }
        if (r0 != 0) goto L_0x002c;
    L_0x001f:
        r0 = getUnobservedExceptionHandler();	 Catch:{ all -> 0x002f }
        if (r0 == 0) goto L_0x002c;
    L_0x0025:
        r0 = new bolts.UnobservedErrorNotifier;	 Catch:{ all -> 0x002f }
        r0.<init>(r4);	 Catch:{ all -> 0x002f }
        r4.unobservedErrorNotifier = r0;	 Catch:{ all -> 0x002f }
    L_0x002c:
        monitor-exit(r2);	 Catch:{ all -> 0x002f }
        r0 = r1;
        goto L_0x000a;
    L_0x002f:
        r0 = move-exception;
        monitor-exit(r2);	 Catch:{ all -> 0x002f }
        throw r0;
        */
        throw new UnsupportedOperationException("Method not decompiled: bolts.Task.trySetError(java.lang.Exception):boolean");
    }

    boolean trySetResult(TResult tResult) {
        boolean z = true;
        synchronized (this.lock) {
            if (this.complete) {
                z = false;
            } else {
                this.complete = true;
                this.result = tResult;
                this.lock.notifyAll();
                runContinuations();
            }
        }
        return z;
    }

    public void waitForCompletion() throws InterruptedException {
        synchronized (this.lock) {
            if (!isCompleted()) {
                this.lock.wait();
            }
        }
    }

    public boolean waitForCompletion(long j, TimeUnit timeUnit) throws InterruptedException {
        boolean isCompleted;
        synchronized (this.lock) {
            if (!isCompleted()) {
                this.lock.wait(timeUnit.toMillis(j));
            }
            isCompleted = isCompleted();
        }
        return isCompleted;
    }
}
