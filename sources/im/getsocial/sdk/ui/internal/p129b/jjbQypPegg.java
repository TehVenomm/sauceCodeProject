package im.getsocial.sdk.ui.internal.p129b;

import android.app.Activity;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.ui.UiAction;
import im.getsocial.sdk.ui.UiAction.Pending;
import im.getsocial.sdk.ui.UiActionListener;
import im.getsocial.sdk.ui.internal.cjrhisSQCL;

/* renamed from: im.getsocial.sdk.ui.internal.b.jjbQypPegg */
public final class jjbQypPegg implements UiActionListener {
    @XdbacJlTDQ
    /* renamed from: a */
    cjrhisSQCL f2777a;
    /* renamed from: b */
    private final Activity f2778b = this.f2777a.m3099a();
    /* renamed from: c */
    private final UiActionListener f2779c;

    public jjbQypPegg(UiActionListener uiActionListener) {
        ztWNWCuZiM.m1221a((Object) this);
        this.f2779c = uiActionListener;
    }

    public final void onUiAction(UiAction uiAction, final Pending pending) {
        this.f2779c.onUiAction(uiAction, new Pending(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f2776b;

            /* renamed from: im.getsocial.sdk.ui.internal.b.jjbQypPegg$1$1 */
            class C11331 implements Runnable {
                /* renamed from: a */
                final /* synthetic */ C11341 f2774a;

                C11331(C11341 c11341) {
                    this.f2774a = c11341;
                }

                public void run() {
                    pending.proceed();
                }
            }

            public void proceed() {
                this.f2776b.f2778b.runOnUiThread(new C11331(this));
            }
        });
    }
}
