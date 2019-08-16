package bolts;

import java.io.Closeable;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;
import java.util.concurrent.CancellationException;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;

public class CancellationTokenSource implements Closeable {
    private boolean cancellationRequested;
    private boolean closed;
    private final ScheduledExecutorService executor = BoltsExecutors.scheduled();
    /* access modifiers changed from: private */
    public final Object lock = new Object();
    private final List<CancellationTokenRegistration> registrations = new ArrayList();
    /* access modifiers changed from: private */
    public ScheduledFuture<?> scheduledCancellation;

    /* JADX WARNING: Code restructure failed: missing block: B:24:?, code lost:
        return;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private void cancelAfter(long r6, java.util.concurrent.TimeUnit r8) {
        /*
            r5 = this;
            r2 = -1
            int r0 = (r6 > r2 ? 1 : (r6 == r2 ? 0 : -1))
            if (r0 >= 0) goto L_0x000e
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.String r1 = "Delay must be >= -1"
            r0.<init>(r1)
            throw r0
        L_0x000e:
            r0 = 0
            int r0 = (r6 > r0 ? 1 : (r6 == r0 ? 0 : -1))
            if (r0 != 0) goto L_0x0018
            r5.cancel()
        L_0x0017:
            return
        L_0x0018:
            java.lang.Object r1 = r5.lock
            monitor-enter(r1)
            boolean r0 = r5.cancellationRequested     // Catch:{ all -> 0x0021 }
            if (r0 == 0) goto L_0x0024
            monitor-exit(r1)     // Catch:{ all -> 0x0021 }
            goto L_0x0017
        L_0x0021:
            r0 = move-exception
            monitor-exit(r1)     // Catch:{ all -> 0x0021 }
            throw r0
        L_0x0024:
            r5.cancelScheduledCancellation()     // Catch:{ all -> 0x0021 }
            int r0 = (r6 > r2 ? 1 : (r6 == r2 ? 0 : -1))
            if (r0 == 0) goto L_0x0038
            java.util.concurrent.ScheduledExecutorService r0 = r5.executor     // Catch:{ all -> 0x0021 }
            bolts.CancellationTokenSource$1 r2 = new bolts.CancellationTokenSource$1     // Catch:{ all -> 0x0021 }
            r2.<init>()     // Catch:{ all -> 0x0021 }
            java.util.concurrent.ScheduledFuture r0 = r0.schedule(r2, r6, r8)     // Catch:{ all -> 0x0021 }
            r5.scheduledCancellation = r0     // Catch:{ all -> 0x0021 }
        L_0x0038:
            monitor-exit(r1)     // Catch:{ all -> 0x0021 }
            goto L_0x0017
        */
        throw new UnsupportedOperationException("Method not decompiled: bolts.CancellationTokenSource.cancelAfter(long, java.util.concurrent.TimeUnit):void");
    }

    private void cancelScheduledCancellation() {
        if (this.scheduledCancellation != null) {
            this.scheduledCancellation.cancel(true);
            this.scheduledCancellation = null;
        }
    }

    private void notifyListeners(List<CancellationTokenRegistration> list) {
        for (CancellationTokenRegistration runAction : list) {
            runAction.runAction();
        }
    }

    private void throwIfClosed() {
        if (this.closed) {
            throw new IllegalStateException("Object already closed");
        }
    }

    public void cancel() {
        synchronized (this.lock) {
            throwIfClosed();
            if (!this.cancellationRequested) {
                cancelScheduledCancellation();
                this.cancellationRequested = true;
                ArrayList arrayList = new ArrayList(this.registrations);
                notifyListeners(arrayList);
            }
        }
    }

    public void cancelAfter(long j) {
        cancelAfter(j, TimeUnit.MILLISECONDS);
    }

    public void close() {
        synchronized (this.lock) {
            if (!this.closed) {
                cancelScheduledCancellation();
                for (CancellationTokenRegistration close : this.registrations) {
                    close.close();
                }
                this.registrations.clear();
                this.closed = true;
            }
        }
    }

    public CancellationToken getToken() {
        CancellationToken cancellationToken;
        synchronized (this.lock) {
            throwIfClosed();
            cancellationToken = new CancellationToken(this);
        }
        return cancellationToken;
    }

    public boolean isCancellationRequested() {
        boolean z;
        synchronized (this.lock) {
            throwIfClosed();
            z = this.cancellationRequested;
        }
        return z;
    }

    /* access modifiers changed from: 0000 */
    public CancellationTokenRegistration register(Runnable runnable) {
        CancellationTokenRegistration cancellationTokenRegistration;
        synchronized (this.lock) {
            throwIfClosed();
            cancellationTokenRegistration = new CancellationTokenRegistration(this, runnable);
            if (this.cancellationRequested) {
                cancellationTokenRegistration.runAction();
            } else {
                this.registrations.add(cancellationTokenRegistration);
            }
        }
        return cancellationTokenRegistration;
    }

    /* access modifiers changed from: 0000 */
    public void throwIfCancellationRequested() throws CancellationException {
        synchronized (this.lock) {
            throwIfClosed();
            if (this.cancellationRequested) {
                throw new CancellationException();
            }
        }
    }

    public String toString() {
        return String.format(Locale.US, "%s@%s[cancellationRequested=%s]", new Object[]{getClass().getName(), Integer.toHexString(hashCode()), Boolean.toString(isCancellationRequested())});
    }

    /* access modifiers changed from: 0000 */
    public void unregister(CancellationTokenRegistration cancellationTokenRegistration) {
        synchronized (this.lock) {
            throwIfClosed();
            this.registrations.remove(cancellationTokenRegistration);
        }
    }
}
