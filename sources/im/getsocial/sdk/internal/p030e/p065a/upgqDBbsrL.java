package im.getsocial.sdk.internal.p030e.p065a;

import im.getsocial.sdk.internal.p030e.ztWNWCuZiM;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/* renamed from: im.getsocial.sdk.internal.e.a.upgqDBbsrL */
class upgqDBbsrL extends XdbacJlTDQ {
    /* renamed from: a */
    private final ExecutorService f1563a = Executors.newSingleThreadExecutor();

    upgqDBbsrL() {
    }

    /* renamed from: a */
    public final void mo4483a() {
        this.f1563a.shutdown();
    }

    /* renamed from: a */
    public final void mo4484a(final ztWNWCuZiM ztwnwcuzim) {
        Runnable c09871 = new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ upgqDBbsrL f1562b;

            public void run() {
                ztwnwcuzim.mo4491a();
            }
        };
        if (this.f1563a.isShutdown()) {
            new Thread(c09871).start();
        } else {
            this.f1563a.execute(c09871);
        }
    }
}
