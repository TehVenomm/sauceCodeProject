package im.getsocial.sdk.socialgraph.p109a.p110a;

import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p048a.jjbQypPegg;
import java.util.List;

/* renamed from: im.getsocial.sdk.socialgraph.a.a.upgqDBbsrL */
public class upgqDBbsrL implements im.getsocial.sdk.internal.p030e.upgqDBbsrL<List<String>, pdwpUtZXDT<Integer>> {
    @XdbacJlTDQ
    /* renamed from: a */
    jjbQypPegg f2523a;

    public upgqDBbsrL() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    public final /* synthetic */ Object mo4344a(Object obj) {
        List list = (List) obj;
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) list), "Can not call SetFriendsFunc with null userId list");
        return list.isEmpty() ? pdwpUtZXDT.m1659a(Integer.valueOf(0)) : this.f2523a.mo4434a(list);
    }
}
