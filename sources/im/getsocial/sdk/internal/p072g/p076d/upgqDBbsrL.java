package im.getsocial.sdk.internal.p072g.p076d;

import android.os.Handler;
import android.os.Looper;

/* renamed from: im.getsocial.sdk.internal.g.d.upgqDBbsrL */
public class upgqDBbsrL implements jjbQypPegg {
    /* renamed from: a */
    private static Handler m1901a() {
        return new Handler(Looper.getMainLooper());
    }

    /* renamed from: a */
    public final void mo4547a(Runnable runnable) {
        upgqDBbsrL.m1901a().post(runnable);
    }

    /* renamed from: a */
    public final void mo4548a(Runnable runnable, int i) {
        upgqDBbsrL.m1901a().postDelayed(runnable, 300);
    }
}
