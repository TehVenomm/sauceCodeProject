package im.getsocial.sdk.ui.internal.p131d;

import im.getsocial.p015a.p016a.pdwpUtZXDT;
import im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme;
import im.getsocial.sdk.ui.internal.p131d.p132a.QWVUXapsSm;
import im.getsocial.sdk.ui.internal.p131d.p132a.fOrCGNYyfk;
import im.getsocial.sdk.ui.internal.p131d.p132a.jMsobIMeui;
import im.getsocial.sdk.ui.internal.p131d.p132a.qZypgoeblR;
import im.getsocial.sdk.ui.internal.p131d.p132a.qdyNCsqjKt;
import im.getsocial.sdk.ui.internal.p131d.p132a.upgqDBbsrL;
import im.getsocial.sdk.ui.internal.p131d.p132a.zoToeBNOjF;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;

/* renamed from: im.getsocial.sdk.ui.internal.d.jjbQypPegg */
final class jjbQypPegg {

    /* renamed from: im.getsocial.sdk.ui.internal.d.jjbQypPegg$1 */
    static final class C11391 implements im.getsocial.sdk.ui.internal.p131d.p133b.jjbQypPegg<fOrCGNYyfk> {
        C11391() {
        }

        /* renamed from: a */
        public final /* synthetic */ Object mo4726a(Object obj) {
            return obj == null ? new fOrCGNYyfk(0) : new fOrCGNYyfk(((Long) obj).intValue());
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.d.jjbQypPegg$2 */
    static final class C11402 implements im.getsocial.sdk.ui.internal.p131d.p133b.jjbQypPegg<im.getsocial.sdk.ui.internal.p131d.p132a.pdwpUtZXDT.jjbQypPegg> {
        C11402() {
        }

        /* renamed from: a */
        public final /* synthetic */ Object mo4726a(Object obj) {
            return "constant-physical-size".equals(obj) ? im.getsocial.sdk.ui.internal.p131d.p132a.pdwpUtZXDT.jjbQypPegg.CONSTANT_PHYSICAL_SIZE : im.getsocial.sdk.ui.internal.p131d.p132a.pdwpUtZXDT.jjbQypPegg.SCALE_WITH_SCREEN_SIZE;
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.d.jjbQypPegg$3 */
    static final class C11413 implements im.getsocial.sdk.ui.internal.p131d.p133b.jjbQypPegg<qZypgoeblR> {
        C11413() {
        }

        /* renamed from: a */
        public final /* synthetic */ Object mo4726a(Object obj) {
            return obj == null ? new qZypgoeblR(0) : new qZypgoeblR(((Long) obj).intValue());
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.d.jjbQypPegg$4 */
    static final class C11424 implements im.getsocial.sdk.ui.internal.p131d.p133b.jjbQypPegg<upgqDBbsrL> {
        C11424() {
        }

        /* renamed from: a */
        public final /* synthetic */ Object mo4726a(Object obj) {
            String[] split = ((String) obj).split(":");
            return new upgqDBbsrL(Double.parseDouble(split[0]) / Double.parseDouble(split[1]));
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.d.jjbQypPegg$5 */
    static final class C11435 implements im.getsocial.sdk.ui.internal.p131d.p133b.jjbQypPegg<zoToeBNOjF> {
        C11435() {
        }

        /* renamed from: a */
        public final /* synthetic */ Object mo4726a(Object obj) {
            return obj == null ? null : new zoToeBNOjF(im.getsocial.sdk.ui.internal.p131d.p134c.jjbQypPegg.m3222a((String) obj));
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.d.jjbQypPegg$6 */
    static final class C11446 implements im.getsocial.sdk.ui.internal.p131d.p133b.jjbQypPegg<im.getsocial.sdk.ui.internal.p131d.p132a.jjbQypPegg> {
        C11446() {
        }

        /* renamed from: a */
        public final /* synthetic */ Object mo4726a(Object obj) {
            String str = (String) obj;
            return "fade".equalsIgnoreCase(str) ? im.getsocial.sdk.ui.internal.p131d.p132a.jjbQypPegg.FADE : "scale".equalsIgnoreCase(str) ? im.getsocial.sdk.ui.internal.p131d.p132a.jjbQypPegg.SCALE : "fade-and-scale".equalsIgnoreCase(str) ? im.getsocial.sdk.ui.internal.p131d.p132a.jjbQypPegg.FADE_AND_SCALE : im.getsocial.sdk.ui.internal.p131d.p132a.jjbQypPegg.NONE;
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.d.jjbQypPegg$7 */
    static final class C11457 implements im.getsocial.sdk.ui.internal.p131d.p133b.jjbQypPegg<jMsobIMeui> {
        C11457() {
        }

        /* renamed from: a */
        public final /* synthetic */ Object mo4726a(Object obj) {
            return new jMsobIMeui((String) obj);
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.d.jjbQypPegg$8 */
    static final class C11468 implements im.getsocial.sdk.ui.internal.p131d.p133b.jjbQypPegg<QWVUXapsSm> {
        C11468() {
        }

        /* renamed from: a */
        public final /* synthetic */ Object mo4726a(Object obj) {
            if (obj == null) {
                return new QWVUXapsSm(new fOrCGNYyfk(0), new fOrCGNYyfk(0), new fOrCGNYyfk(0), new fOrCGNYyfk(0));
            }
            String[] split = ((String) obj).split(" ");
            return new QWVUXapsSm(new fOrCGNYyfk(Integer.parseInt(split[0])), new fOrCGNYyfk(Integer.parseInt(split[1])), new fOrCGNYyfk(Integer.parseInt(split[2])), new fOrCGNYyfk(Integer.parseInt(split[3])));
        }
    }

    /* renamed from: im.getsocial.sdk.ui.internal.d.jjbQypPegg$9 */
    static final class C11479 implements im.getsocial.sdk.ui.internal.p131d.p133b.jjbQypPegg<qdyNCsqjKt> {
        C11479() {
        }

        /* renamed from: a */
        public final /* synthetic */ Object mo4726a(Object obj) {
            Map hashMap = new HashMap();
            for (Entry entry : ((Map) obj).entrySet()) {
                hashMap.put(new jMsobIMeui((String) entry.getKey()), (KluUZYuxme) im.getsocial.sdk.ui.internal.p131d.p133b.upgqDBbsrL.m3218a((pdwpUtZXDT) entry.getValue(), KluUZYuxme.class));
            }
            return new qdyNCsqjKt(hashMap);
        }
    }

    private jjbQypPegg() {
    }

    /* renamed from: a */
    static void m3233a() {
        im.getsocial.sdk.ui.internal.p131d.p133b.upgqDBbsrL.m3220a(fOrCGNYyfk.class, new C11391());
        im.getsocial.sdk.ui.internal.p131d.p133b.upgqDBbsrL.m3220a(im.getsocial.sdk.ui.internal.p131d.p132a.pdwpUtZXDT.jjbQypPegg.class, new C11402());
        im.getsocial.sdk.ui.internal.p131d.p133b.upgqDBbsrL.m3220a(qZypgoeblR.class, new C11413());
        im.getsocial.sdk.ui.internal.p131d.p133b.upgqDBbsrL.m3220a(upgqDBbsrL.class, new C11424());
        im.getsocial.sdk.ui.internal.p131d.p133b.upgqDBbsrL.m3220a(zoToeBNOjF.class, new C11435());
        im.getsocial.sdk.ui.internal.p131d.p133b.upgqDBbsrL.m3220a(im.getsocial.sdk.ui.internal.p131d.p132a.jjbQypPegg.class, new C11446());
        im.getsocial.sdk.ui.internal.p131d.p133b.upgqDBbsrL.m3220a(jMsobIMeui.class, new C11457());
        im.getsocial.sdk.ui.internal.p131d.p133b.upgqDBbsrL.m3220a(QWVUXapsSm.class, new C11468());
        im.getsocial.sdk.ui.internal.p131d.p133b.upgqDBbsrL.m3220a(qdyNCsqjKt.class, new C11479());
    }
}
