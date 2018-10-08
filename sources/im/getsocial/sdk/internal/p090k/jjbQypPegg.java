package im.getsocial.sdk.internal.p090k;

import java.util.LinkedList;
import java.util.List;

/* renamed from: im.getsocial.sdk.internal.k.jjbQypPegg */
public final class jjbQypPegg {
    /* renamed from: a */
    private final Class<?> f2204a;

    private jjbQypPegg(String str) {
        this.f2204a = Class.forName(str);
    }

    /* renamed from: a */
    public static jjbQypPegg m2088a(String str) {
        return new jjbQypPegg(str);
    }

    /* renamed from: a */
    public final upgqDBbsrL m2089a(String str, cjrhisSQCL... cjrhissqclArr) {
        List linkedList = new LinkedList();
        List linkedList2 = new LinkedList();
        for (cjrhisSQCL cjrhissqcl : cjrhissqclArr) {
            linkedList.add(cjrhissqcl.f2203b);
            linkedList2.add(cjrhissqcl.f2202a);
        }
        return new upgqDBbsrL(this.f2204a.getMethod(str, (Class[]) linkedList.toArray(new Class[linkedList.size()])).invoke(null, linkedList2.toArray(new Object[linkedList2.size()])));
    }
}
