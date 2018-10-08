package im.getsocial.sdk.socialgraph.p109a.p112c;

import im.getsocial.sdk.CompletionCallback;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p030e.upgqDBbsrL;
import im.getsocial.sdk.internal.p030e.zoToeBNOjF;
import im.getsocial.sdk.internal.p033c.p034l.jjbQypPegg;
import im.getsocial.sdk.socialgraph.p109a.p110a.XdbacJlTDQ;
import im.getsocial.sdk.usermanagement.p138a.p140b.KSZKMmRWhZ;
import java.util.Collections;
import java.util.List;

/* renamed from: im.getsocial.sdk.socialgraph.a.c.qZypgoeblR */
public class qZypgoeblR extends jjbQypPegg {
    /* renamed from: a */
    public final void m2498a(final String str, final List<String> list, CompletionCallback completionCallback) {
        m986a(pdwpUtZXDT.m1659a((Object) list).m1669b(new XdbacJlTDQ()).m1669b(new upgqDBbsrL<Boolean, zoToeBNOjF<String, List<String>>>(this) {
            /* renamed from: c */
            final /* synthetic */ qZypgoeblR f2533c;

            /* renamed from: a */
            public final /* synthetic */ Object mo4344a(Object obj) {
                return ((Boolean) obj).booleanValue() ? zoToeBNOjF.m1676a(str, list) : zoToeBNOjF.m1676a(str, Collections.emptyList());
            }
        }).m1665a(new im.getsocial.sdk.socialgraph.p109a.p110a.jjbQypPegg()).m1669b(new upgqDBbsrL<Integer, List<String>>(this) {
            /* renamed from: b */
            final /* synthetic */ qZypgoeblR f2530b;

            /* renamed from: a */
            public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
                return list;
            }
        }).m1669b(new im.getsocial.sdk.socialgraph.p109a.p110a.pdwpUtZXDT()).m1669b(new KSZKMmRWhZ()), completionCallback);
    }
}
