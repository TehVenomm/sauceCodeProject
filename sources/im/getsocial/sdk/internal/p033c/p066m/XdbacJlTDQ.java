package im.getsocial.sdk.internal.p033c.p066m;

import im.getsocial.sdk.internal.p033c.QhisXzMgay;
import im.getsocial.sdk.internal.p033c.bpiSwUyLit;
import im.getsocial.sdk.internal.p033c.rFvvVpjzZH;

/* renamed from: im.getsocial.sdk.internal.c.m.XdbacJlTDQ */
public final class XdbacJlTDQ {
    private XdbacJlTDQ() {
    }

    /* renamed from: a */
    public static boolean m1506a(QhisXzMgay qhisXzMgay) {
        return "UNITY".equalsIgnoreCase(qhisXzMgay.mo4467h());
    }

    /* renamed from: a */
    public static boolean m1507a(bpiSwUyLit bpiswuylit) {
        boolean z = !bpiswuylit.mo4361a("first_app_open");
        if (z) {
            bpiswuylit.mo4359a("first_app_open", System.currentTimeMillis());
        }
        return z;
    }

    /* renamed from: a */
    public static boolean m1508a(rFvvVpjzZH rfvvvpjzzh) {
        return rfvvvpjzzh.mo4368a("im.getsocial.sdk.AutoRegisterForPush", true);
    }
}
