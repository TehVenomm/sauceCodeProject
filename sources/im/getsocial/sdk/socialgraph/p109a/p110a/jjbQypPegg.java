package im.getsocial.sdk.socialgraph.p109a.p110a;

import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p030e.upgqDBbsrL;
import im.getsocial.sdk.internal.p030e.zoToeBNOjF;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import java.util.List;

/* renamed from: im.getsocial.sdk.socialgraph.a.a.jjbQypPegg */
public class jjbQypPegg implements upgqDBbsrL<zoToeBNOjF<String, List<String>>, pdwpUtZXDT<Integer>> {
    @XdbacJlTDQ
    /* renamed from: a */
    im.getsocial.sdk.internal.p033c.p048a.jjbQypPegg f2522a;

    public jjbQypPegg() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    public final /* synthetic */ Object mo4344a(Object obj) {
        zoToeBNOjF zotoebnojf = (zoToeBNOjF) obj;
        String str = (String) zotoebnojf.mo4497a();
        Object obj2 = (List) zotoebnojf.mo4498b();
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1516b(str), "Provider id can not be null");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a(obj2), "User ids list can not be null");
        return obj2.isEmpty() ? pdwpUtZXDT.m1659a(Integer.valueOf(0)) : this.f2522a.mo4449d(str, obj2);
    }
}
