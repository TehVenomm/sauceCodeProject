package im.getsocial.sdk.internal.p090k;

import java.util.LinkedList;
import java.util.List;

/* renamed from: im.getsocial.sdk.internal.k.upgqDBbsrL */
public final class upgqDBbsrL {
    /* renamed from: a */
    private final Object f2205a;

    upgqDBbsrL(Object obj) {
        this.f2205a = obj;
    }

    /* renamed from: a */
    public final upgqDBbsrL m2090a(String str, cjrhisSQCL... cjrhissqclArr) {
        if (this.f2205a == null) {
            throw new NullPointerException();
        }
        List linkedList = new LinkedList();
        List linkedList2 = new LinkedList();
        for (cjrhisSQCL cjrhissqcl : cjrhissqclArr) {
            linkedList.add(cjrhissqcl.f2203b);
            linkedList2.add(cjrhissqcl.f2202a);
        }
        return new upgqDBbsrL(this.f2205a.getClass().getMethod(str, (Class[]) linkedList.toArray(new Class[linkedList.size()])).invoke(this.f2205a, linkedList2.toArray(new Object[linkedList2.size()])));
    }

    /* renamed from: a */
    public final <T> T m2091a() {
        return this.f2205a;
    }
}
