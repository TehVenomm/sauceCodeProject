package im.getsocial.sdk.ui.internal.p125h;

import java.util.concurrent.atomic.AtomicBoolean;

/* renamed from: im.getsocial.sdk.ui.internal.h.cjrhisSQCL */
public abstract class cjrhisSQCL implements Runnable {
    /* renamed from: a */
    private final AtomicBoolean f2728a = new AtomicBoolean(false);

    /* renamed from: a */
    public void mo4710a() {
        this.f2728a.set(true);
    }

    /* renamed from: b */
    public final boolean m3038b() {
        return this.f2728a.get();
    }
}
