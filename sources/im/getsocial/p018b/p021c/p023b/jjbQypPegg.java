package im.getsocial.p018b.p021c.p023b;

import im.getsocial.p018b.p021c.p022a.XdbacJlTDQ;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.ztWNWCuZiM;
import java.io.Closeable;
import java.io.IOException;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.AtomicInteger;

/* renamed from: im.getsocial.b.c.b.jjbQypPegg */
public class jjbQypPegg implements Closeable {
    /* renamed from: a */
    final AtomicBoolean f1060a = new AtomicBoolean(true);
    /* renamed from: b */
    private final AtomicInteger f1061b = new AtomicInteger(0);
    /* renamed from: c */
    private final zoToeBNOjF f1062c;

    /* renamed from: im.getsocial.b.c.b.jjbQypPegg$jjbQypPegg */
    static class jjbQypPegg extends Exception {
        /* renamed from: a */
        final ztWNWCuZiM f1059a;

        jjbQypPegg(ztWNWCuZiM ztwnwcuzim) {
            this.f1059a = ztwnwcuzim;
        }
    }

    protected jjbQypPegg(zoToeBNOjF zotoebnojf) {
        this.f1062c = zotoebnojf;
    }

    /* renamed from: a */
    protected final <T> T m852a(upgqDBbsrL<T> upgqdbbsrl) {
        if (this.f1060a.get()) {
            try {
                Object obj = upgqdbbsrl.f1064b == (byte) 4 ? 1 : null;
                int incrementAndGet = this.f1061b.incrementAndGet();
                this.f1062c.mo4323a(upgqdbbsrl.f1063a, upgqdbbsrl.f1064b, incrementAndGet);
                upgqdbbsrl.mo4500a(this.f1062c);
                this.f1062c.m827n();
                if (obj != null) {
                    return null;
                }
                XdbacJlTDQ b = this.f1062c.mo4325b();
                if (b.f1043c != incrementAndGet) {
                    throw new ztWNWCuZiM(im.getsocial.p018b.p021c.ztWNWCuZiM.jjbQypPegg.BAD_SEQUENCE_ID, "Unrecognized sequence ID");
                } else if (b.f1042b == (byte) 3) {
                    throw new jjbQypPegg(ztWNWCuZiM.m859a(this.f1062c));
                } else if (b.f1042b != (byte) 2) {
                    throw new ztWNWCuZiM(im.getsocial.p018b.p021c.ztWNWCuZiM.jjbQypPegg.INVALID_MESSAGE_TYPE, "Invalid message type: " + b.f1042b);
                } else if (b.f1043c != this.f1061b.get()) {
                    throw new ztWNWCuZiM(im.getsocial.p018b.p021c.ztWNWCuZiM.jjbQypPegg.BAD_SEQUENCE_ID, "Out-of-order response");
                } else if (b.f1041a.equals(upgqdbbsrl.f1063a)) {
                    return upgqdbbsrl.mo4499a(this.f1062c, b);
                } else {
                    throw new ztWNWCuZiM(im.getsocial.p018b.p021c.ztWNWCuZiM.jjbQypPegg.WRONG_METHOD_NAME, "Unexpected method name in reply; expected " + upgqdbbsrl.f1063a + " but received " + b.f1041a);
                }
            } catch (jjbQypPegg e) {
                throw e.f1059a;
            }
        }
        throw new IllegalStateException("Cannot write to a closed service client");
    }

    public void close() {
        if (this.f1060a.compareAndSet(true, false)) {
            try {
                this.f1062c.close();
            } catch (IOException e) {
            }
        }
    }
}
