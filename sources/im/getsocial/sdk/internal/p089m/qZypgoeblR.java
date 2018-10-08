package im.getsocial.sdk.internal.p089m;

import android.os.Handler;
import android.os.Looper;
import im.getsocial.sdk.internal.p030e.p065a.XdbacJlTDQ;
import im.getsocial.sdk.internal.p030e.ztWNWCuZiM;

/* renamed from: im.getsocial.sdk.internal.m.qZypgoeblR */
class qZypgoeblR extends XdbacJlTDQ {
    /* renamed from: a */
    private final Handler f2223a;

    private qZypgoeblR(Handler handler) {
        this.f2223a = handler;
    }

    qZypgoeblR(Looper looper) {
        this(new Handler(looper));
    }

    /* renamed from: a */
    public final void mo4483a() {
    }

    /* renamed from: a */
    public final void mo4484a(final ztWNWCuZiM ztwnwcuzim) {
        this.f2223a.post(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ qZypgoeblR f2222b;

            public void run() {
                ztwnwcuzim.mo4491a();
            }
        });
    }
}
