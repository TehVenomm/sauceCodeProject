package im.getsocial.p018b.p021c.p025d;

import im.getsocial.p018b.p021c.p022a.cjrhisSQCL;
import im.getsocial.p018b.p021c.p022a.pdwpUtZXDT;
import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p022a.ztWNWCuZiM;
import java.net.ProtocolException;

/* renamed from: im.getsocial.b.c.d.jjbQypPegg */
public final class jjbQypPegg {
    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static void m858a(zoToeBNOjF zotoebnojf, byte b) {
        int i = 0;
        switch (b) {
            case (byte) 2:
                zotoebnojf.mo4330g();
                return;
            case (byte) 3:
                zotoebnojf.mo4331h();
                return;
            case (byte) 4:
                zotoebnojf.mo4335l();
                return;
            case (byte) 6:
                zotoebnojf.mo4332i();
                return;
            case (byte) 8:
                zotoebnojf.mo4333j();
                return;
            case (byte) 10:
                zotoebnojf.mo4334k();
                return;
            case (byte) 11:
                zotoebnojf.mo4336m();
                return;
            case (byte) 12:
                break;
            case (byte) 13:
                pdwpUtZXDT d = zotoebnojf.mo4327d();
                while (i < d.f1053c) {
                    jjbQypPegg.m858a(zotoebnojf, d.f1051a);
                    jjbQypPegg.m858a(zotoebnojf, d.f1052b);
                    i++;
                }
                return;
            case (byte) 14:
                ztWNWCuZiM f = zotoebnojf.mo4329f();
                while (i < f.f1058b) {
                    jjbQypPegg.m858a(zotoebnojf, f.f1057a);
                    i++;
                }
                return;
            case (byte) 15:
                cjrhisSQCL e = zotoebnojf.mo4328e();
                while (i < e.f1045b) {
                    jjbQypPegg.m858a(zotoebnojf, e.f1044a);
                    i++;
                }
                return;
            case (byte) 16:
                zotoebnojf.mo4333j();
                return;
            default:
                throw new ProtocolException("Unrecognized TType value: " + b);
        }
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                jjbQypPegg.m858a(zotoebnojf, c.f1055b);
            } else {
                return;
            }
        }
    }
}
