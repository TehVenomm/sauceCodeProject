package im.getsocial.p018b.p020b;

import java.security.MessageDigest;
import javax.crypto.Mac;

/* renamed from: im.getsocial.b.b.KluUZYuxme */
public final class KluUZYuxme extends HptYHntaqF {
    /* renamed from: a */
    private final MessageDigest f981a;
    /* renamed from: b */
    private final Mac f982b;

    /* renamed from: a */
    public final long mo4293a(cjrhisSQCL cjrhissqcl, long j) {
        long a = super.mo4293a(cjrhissqcl, j);
        if (a != -1) {
            long j2 = cjrhissqcl.f998b - a;
            long j3 = cjrhissqcl.f998b;
            QCXFOjcJkE qCXFOjcJkE = cjrhissqcl.f997a;
            while (j3 > j2) {
                qCXFOjcJkE = qCXFOjcJkE.f989g;
                j3 -= (long) (qCXFOjcJkE.f985c - qCXFOjcJkE.f984b);
            }
            while (j3 < cjrhissqcl.f998b) {
                int i = (int) ((j2 + ((long) qCXFOjcJkE.f984b)) - j3);
                if (this.f981a != null) {
                    this.f981a.update(qCXFOjcJkE.f983a, i, qCXFOjcJkE.f985c - i);
                } else {
                    this.f982b.update(qCXFOjcJkE.f983a, i, qCXFOjcJkE.f985c - i);
                }
                j2 = ((long) (qCXFOjcJkE.f985c - qCXFOjcJkE.f984b)) + j3;
                qCXFOjcJkE = qCXFOjcJkE.f988f;
                j3 = j2;
            }
        }
        return a;
    }
}
