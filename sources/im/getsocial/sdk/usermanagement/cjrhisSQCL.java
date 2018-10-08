package im.getsocial.sdk.usermanagement;

import im.getsocial.sdk.internal.p033c.p066m.upgqDBbsrL;
import java.util.Map;

public class cjrhisSQCL extends pdwpUtZXDT {
    /* renamed from: a */
    private final PrivateUser f3328a;

    public cjrhisSQCL(PrivateUser privateUser) {
        super(privateUser);
        this.f3328a = privateUser;
    }

    /* renamed from: a */
    public static void m3731a(PrivateUser privateUser, Map<String, String> map) {
        privateUser.f3252a = upgqDBbsrL.m1519a((Map) map);
    }

    /* renamed from: a */
    public final String m3732a(String str) {
        return (String) this.f3328a.f3252a.get(str);
    }
}
