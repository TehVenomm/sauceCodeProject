package io.fabric.sdk.android.services.concurrency.internal;

import java.util.concurrent.CancellationException;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Future;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;
import java.util.concurrent.locks.AbstractQueuedSynchronizer;

public abstract class AbstractFuture<V> implements Future<V> {
    private final Sync<V> sync = new Sync();

    static final class Sync<V> extends AbstractQueuedSynchronizer {
        static final int CANCELLED = 4;
        static final int COMPLETED = 2;
        static final int COMPLETING = 1;
        static final int INTERRUPTED = 8;
        static final int RUNNING = 0;
        private static final long serialVersionUID = 0;
        private Throwable exception;
        private V value;

        Sync() {
        }

        private boolean complete(V v, Throwable th, int i) {
            boolean compareAndSetState = compareAndSetState(0, 1);
            if (compareAndSetState) {
                this.value = v;
                if ((i & 12) != 0) {
                    th = new CancellationException("Future.cancel() was called.");
                }
                this.exception = th;
                releaseShared(i);
            } else if (getState() == 1) {
                acquireShared(-1);
            }
            return compareAndSetState;
        }

        private V getValue() throws CancellationException, ExecutionException {
            int state = getState();
            switch (state) {
                case 2:
                    if (this.exception == null) {
                        return this.value;
                    }
                    throw new ExecutionException(this.exception);
                case 4:
                case 8:
                    throw AbstractFuture.cancellationExceptionWithCause("Task was cancelled.", this.exception);
                default:
                    throw new IllegalStateException("Error, synchronizer in invalid state: " + state);
            }
        }

        boolean cancel(boolean z) {
            return complete(null, null, z ? 8 : 4);
        }

        V get() throws CancellationException, ExecutionException, InterruptedException {
            acquireSharedInterruptibly(-1);
            return getValue();
        }

        V get(long j) throws TimeoutException, CancellationException, ExecutionException, InterruptedException {
            if (tryAcquireSharedNanos(-1, j)) {
                return getValue();
            }
            throw new TimeoutException("Timeout waiting for task.");
        }

        boolean isCancelled() {
            return (getState() & 12) != 0;
        }

        boolean isDone() {
            return (getState() & 14) != 0;
        }

        boolean set(V v) {
            return complete(v, null, 2);
        }

        boolean setException(Throwable th) {
            return complete(null, th, 2);
        }

        protected int tryAcquireShared(int i) {
            return isDone() ? 1 : -1;
        }

        protected boolean tryReleaseShared(int i) {
            setState(i);
            return true;
        }

        boolean wasInterrupted() {
            return getState() == 8;
        }
    }

    protected AbstractFuture() {
    }

    static final CancellationException cancellationExceptionWithCause(String str, Throwable th) {
        CancellationException cancellationException = new CancellationException(str);
        cancellationException.initCause(th);
        return cancellationException;
    }

    public boolean cancel(boolean z) {
        if (!this.sync.cancel(z)) {
            return false;
        }
        if (z) {
            interruptTask();
        }
        return true;
    }

    public V get() throws InterruptedException, ExecutionException {
        return this.sync.get();
    }

    public V get(long j, TimeUnit timeUnit) throws InterruptedException, TimeoutException, ExecutionException {
        return this.sync.get(timeUnit.toNanos(j));
    }

    protected void interruptTask() {
    }

    public boolean isCancelled() {
        return this.sync.isCancelled();
    }

    public boolean isDone() {
        return this.sync.isDone();
    }

    protected boolean set(V v) {
        return this.sync.set(v);
    }

    protected boolean setException(Throwable th) {
        if (th != null) {
            return this.sync.setException(th);
        }
        throw new NullPointerException();
    }

    protected final boolean wasInterrupted() {
        return this.sync.wasInterrupted();
    }
}
