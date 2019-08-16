package com.zopim.android.sdk.data;

import java.util.ArrayDeque;
import java.util.Queue;
import java.util.concurrent.Executor;

/* renamed from: com.zopim.android.sdk.data.k */
class C1242k implements Executor {

    /* renamed from: a */
    final Queue<Runnable> f914a = new ArrayDeque();

    /* renamed from: b */
    final Executor f915b;

    /* renamed from: c */
    Runnable f916c;

    C1242k(Executor executor) {
        this.f915b = executor;
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public synchronized void mo20821a() {
        Runnable runnable = (Runnable) this.f914a.poll();
        this.f916c = runnable;
        if (runnable != null) {
            this.f915b.execute(this.f916c);
        }
    }

    public synchronized void execute(Runnable runnable) {
        this.f914a.add(new C1243l(this, runnable));
        if (this.f916c == null) {
            mo20821a();
        }
    }
}
