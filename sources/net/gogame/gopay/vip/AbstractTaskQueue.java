package net.gogame.gopay.vip;

import net.gogame.gopay.vip.TaskQueue.Listener;

public abstract class AbstractTaskQueue<T> implements Runnable, TaskQueue<T> {
    public static final long DEFAULT_DELAY = 60000;

    /* renamed from: a */
    private final Listener<T> f1326a;

    /* renamed from: b */
    private long f1327b = 60000;

    /* renamed from: c */
    private Thread f1328c = null;

    /* access modifiers changed from: protected */
    public abstract T peek();

    /* access modifiers changed from: protected */
    public abstract void remove();

    /* access modifiers changed from: protected */
    public abstract boolean shouldProcess();

    public AbstractTaskQueue(Listener<T> listener) {
        this.f1326a = listener;
    }

    public long getDelay() {
        return this.f1327b;
    }

    public void setDelay(long j) {
        this.f1327b = j;
    }

    public void start() {
        if (this.f1328c == null || !this.f1328c.isAlive()) {
            this.f1328c = new Thread(this);
            this.f1328c.start();
            return;
        }
        throw new IllegalStateException();
    }

    /* JADX WARNING: Code restructure failed: missing block: B:18:0x002d, code lost:
        r0 = move-exception;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:19:0x002e, code lost:
        android.util.Log.e("goPay", "Exception", r0);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:7:0x0013, code lost:
        return;
     */
    /* JADX WARNING: Failed to process nested try/catch */
    /* JADX WARNING: Removed duplicated region for block: B:6:0x0012 A[ExcHandler: InterruptedException (e java.lang.InterruptedException), Splitter:B:0:0x0000] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void run() {
        /*
            r3 = this;
        L_0x0000:
            boolean r0 = r3.shouldProcess()     // Catch:{ InterruptedException -> 0x0012, Exception -> 0x002d }
            if (r0 == 0) goto L_0x000c
            java.lang.Object r0 = r3.peek()     // Catch:{ InterruptedException -> 0x0012, Exception -> 0x002d }
            if (r0 != 0) goto L_0x0014
        L_0x000c:
            long r0 = r3.f1327b     // Catch:{ InterruptedException -> 0x0012, Exception -> 0x002d }
            java.lang.Thread.sleep(r0)     // Catch:{ InterruptedException -> 0x0012, Exception -> 0x002d }
            goto L_0x0000
        L_0x0012:
            r0 = move-exception
            return
        L_0x0014:
            net.gogame.gopay.vip.TaskQueue$Listener<T> r1 = r3.f1326a     // Catch:{ Exception -> 0x0024, InterruptedException -> 0x0012 }
            if (r1 == 0) goto L_0x0036
            net.gogame.gopay.vip.TaskQueue$Listener<T> r1 = r3.f1326a     // Catch:{ Exception -> 0x0024, InterruptedException -> 0x0012 }
            boolean r0 = r1.onTask(r0)     // Catch:{ Exception -> 0x0024, InterruptedException -> 0x0012 }
            if (r0 == 0) goto L_0x000c
            r3.remove()     // Catch:{ Exception -> 0x0024, InterruptedException -> 0x0012 }
            goto L_0x0000
        L_0x0024:
            r0 = move-exception
            java.lang.String r1 = "goPay"
            java.lang.String r2 = "Exception"
            android.util.Log.e(r1, r2, r0)     // Catch:{ InterruptedException -> 0x0012, Exception -> 0x002d }
            goto L_0x0000
        L_0x002d:
            r0 = move-exception
            java.lang.String r1 = "goPay"
            java.lang.String r2 = "Exception"
            android.util.Log.e(r1, r2, r0)
            goto L_0x0000
        L_0x0036:
            r3.remove()     // Catch:{ Exception -> 0x0024, InterruptedException -> 0x0012 }
            goto L_0x0000
        */
        throw new UnsupportedOperationException("Method not decompiled: net.gogame.gopay.vip.AbstractTaskQueue.run():void");
    }
}
