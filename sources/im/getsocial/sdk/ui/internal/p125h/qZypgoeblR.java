package im.getsocial.sdk.ui.internal.p125h;

import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;

/* renamed from: im.getsocial.sdk.ui.internal.h.qZypgoeblR */
public abstract class qZypgoeblR extends cjrhisSQCL {
    /* renamed from: a */
    private static final ScheduledExecutorService f2729a = Executors.newSingleThreadScheduledExecutor();
    /* renamed from: b */
    private ScheduledFuture<?> f2730b;

    /* renamed from: a */
    public final void mo4710a() {
        if (!m3038b()) {
            synchronized (this) {
                if (this.f2730b != null) {
                    this.f2730b.cancel(true);
                    this.f2730b = null;
                }
                super.mo4710a();
            }
        }
    }

    /* renamed from: a */
    public final void m3040a(long j) {
        synchronized (this) {
            this.f2730b = f2729a.schedule(this, 500, TimeUnit.MILLISECONDS);
        }
    }
}
