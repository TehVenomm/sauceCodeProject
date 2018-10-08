package com.zopim.android.sdk.data;

import java.util.ArrayDeque;
import java.util.Queue;
import java.util.concurrent.Executor;

/* renamed from: com.zopim.android.sdk.data.k */
class C0872k implements Executor {
    /* renamed from: a */
    final Queue<Runnable> f870a = new ArrayDeque();
    /* renamed from: b */
    final Executor f871b;
    /* renamed from: c */
    Runnable f872c;

    C0872k(Executor executor) {
        this.f871b = executor;
    }

    /* renamed from: a */
    protected synchronized void m704a() {
        Runnable runnable = (Runnable) this.f870a.poll();
        this.f872c = runnable;
        if (runnable != null) {
            this.f871b.execute(this.f872c);
        }
    }

    public synchronized void execute(Runnable runnable) {
        this.f870a.add(new C0873l(this, runnable));
        if (this.f872c == null) {
            m704a();
        }
    }
}
