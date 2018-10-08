package im.getsocial.sdk.usermanagement.p138a.p143e;

import im.getsocial.sdk.Callback;
import im.getsocial.sdk.internal.p033c.p034l.jjbQypPegg;
import im.getsocial.sdk.usermanagement.UserReference;
import im.getsocial.sdk.usermanagement.UsersQuery;
import java.util.List;

/* renamed from: im.getsocial.sdk.usermanagement.a.e.pdwpUtZXDT */
public final class pdwpUtZXDT extends jjbQypPegg {
    /* renamed from: a */
    public final void m3719a(UsersQuery usersQuery, Callback<List<UserReference>> callback) {
        boolean z = true;
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(usersQuery.getLimit() > 0, "Limit can not be less than or equal 0");
        if (usersQuery.getLimit() > 20) {
            z = false;
        }
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(z, "Limit can not be greater that 20");
        m985a(m988c().mo4422a(usersQuery), (Callback) callback);
    }
}
