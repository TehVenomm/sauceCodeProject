package im.getsocial.sdk.activities.p028a.p035d;

import im.getsocial.sdk.Callback;
import im.getsocial.sdk.internal.p033c.p034l.jjbQypPegg;
import im.getsocial.sdk.usermanagement.PublicUser;
import java.util.List;

/* renamed from: im.getsocial.sdk.activities.a.d.pdwpUtZXDT */
public final class pdwpUtZXDT extends jjbQypPegg {
    /* renamed from: a */
    public final void m994a(String str, int i, int i2, Callback<List<PublicUser>> callback) {
        boolean z = true;
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(i >= 0, "Can not create GetActivityLikersUseCase with offset less then 0");
        if (i2 <= 0) {
            z = false;
        }
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(z, "Can not create GetActivityLikersUseCase with limit equal or less then 0");
        m985a(m988c().mo4426a(str, i, i2), (Callback) callback);
    }
}
