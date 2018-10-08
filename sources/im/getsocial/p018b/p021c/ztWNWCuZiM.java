package im.getsocial.p018b.p021c;

import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;

/* renamed from: im.getsocial.b.c.ztWNWCuZiM */
public class ztWNWCuZiM extends RuntimeException {
    /* renamed from: a */
    public final jjbQypPegg f1065a;

    /* renamed from: im.getsocial.b.c.ztWNWCuZiM$jjbQypPegg */
    public enum jjbQypPegg {
        UNKNOWN(0),
        UNKNOWN_METHOD(1),
        INVALID_MESSAGE_TYPE(2),
        WRONG_METHOD_NAME(3),
        BAD_SEQUENCE_ID(4),
        MISSING_RESULT(5),
        INTERNAL_ERROR(6),
        PROTOCOL_ERROR(7),
        INVALID_TRANSFORM(8),
        INVALID_PROTOCOL(9),
        UNSUPPORTED_CLIENT_TYPE(10);
        
        final int value;

        private jjbQypPegg(int i) {
            this.value = i;
        }

        static jjbQypPegg findByValue(int i) {
            for (jjbQypPegg jjbqyppegg : jjbQypPegg.values()) {
                if (jjbqyppegg.value == i) {
                    return jjbqyppegg;
                }
            }
            return UNKNOWN;
        }
    }

    public ztWNWCuZiM(jjbQypPegg jjbqyppegg, String str) {
        super(str);
        this.f1065a = jjbqyppegg;
    }

    /* renamed from: a */
    public static ztWNWCuZiM m859a(zoToeBNOjF zotoebnojf) {
        String str = null;
        jjbQypPegg jjbqyppegg = jjbQypPegg.UNKNOWN;
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        str = zotoebnojf.mo4336m();
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 8) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        jjbqyppegg = jjbQypPegg.findByValue(zotoebnojf.mo4333j());
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return new ztWNWCuZiM(jjbqyppegg, str);
        }
    }
}
