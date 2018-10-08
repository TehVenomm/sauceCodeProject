package im.getsocial.sdk.internal.p036a.p043f;

import im.getsocial.sdk.internal.p070f.p071a.ztWNWCuZiM;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;

/* renamed from: im.getsocial.sdk.internal.a.f.jjbQypPegg */
public final class jjbQypPegg {
    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static ztWNWCuZiM m1042a(im.getsocial.sdk.internal.p036a.p038b.jjbQypPegg jjbqyppegg) {
        ztWNWCuZiM ztwnwcuzim = new ztWNWCuZiM();
        Map b = jjbqyppegg.m1018b();
        Map hashMap = new HashMap();
        for (Entry entry : b.entrySet()) {
            if (entry.getValue() != null) {
                hashMap.put(entry.getKey(), entry.getValue());
            }
        }
        ztwnwcuzim.f1873a = hashMap;
        ztwnwcuzim.f1874b = Long.valueOf(jjbqyppegg.m1019c());
        ztwnwcuzim.f1875c = jjbqyppegg.m1017a();
        ztwnwcuzim.f1876d = jjbqyppegg.m1020d();
        ztwnwcuzim.f1877e = Long.valueOf(jjbqyppegg.m1021e());
        return ztwnwcuzim;
    }
}
