package im.getsocial.sdk.socialgraph.p109a.p112c;

import im.getsocial.sdk.CompletionCallback;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p030e.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.p034l.jjbQypPegg;
import im.getsocial.sdk.socialgraph.p109a.p110a.XdbacJlTDQ;
import im.getsocial.sdk.usermanagement.p138a.p140b.KSZKMmRWhZ;
import java.util.ArrayList;
import java.util.List;

/* renamed from: im.getsocial.sdk.socialgraph.a.c.fOrCGNYyfk */
public class fOrCGNYyfk extends jjbQypPegg {
    /* renamed from: a */
    public final void m2493a(final List<String> list, CompletionCallback completionCallback) {
        m986a(pdwpUtZXDT.m1659a((Object) list).m1669b(new XdbacJlTDQ()).m1669b(new upgqDBbsrL<Boolean, List<String>>(this) {
            /* renamed from: b */
            final /* synthetic */ fOrCGNYyfk f2527b;

            /* renamed from: a */
            public final /* synthetic */ Object mo4344a(Object obj) {
                return ((Boolean) obj).booleanValue() ? list : new ArrayList();
            }
        }).m1665a(new im.getsocial.sdk.socialgraph.p109a.p110a.upgqDBbsrL()).m1669b(new upgqDBbsrL<Integer, List<String>>(this) {
            /* renamed from: b */
            final /* synthetic */ fOrCGNYyfk f2525b;

            /* renamed from: a */
            public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
                return list;
            }
        }).m1669b(new im.getsocial.sdk.socialgraph.p109a.p110a.pdwpUtZXDT()).m1669b(new KSZKMmRWhZ()), completionCallback);
    }
}
