package im.getsocial.p018b.p020b;

import java.security.MessageDigest;
import javax.crypto.Mac;

/* renamed from: im.getsocial.b.b.fOrCGNYyfk */
public final class fOrCGNYyfk extends KSZKMmRWhZ {
    /* renamed from: a */
    private final MessageDigest f999a;
    /* renamed from: b */
    private final Mac f1000b;

    public final void a_(cjrhisSQCL cjrhissqcl, long j) {
        long j2 = 0;
        rWfbqYooCV.m783a(cjrhissqcl.f998b, 0, j);
        QCXFOjcJkE qCXFOjcJkE = cjrhissqcl.f997a;
        while (j2 < j) {
            int min = (int) Math.min(j - j2, (long) (qCXFOjcJkE.f985c - qCXFOjcJkE.f984b));
            if (this.f999a != null) {
                this.f999a.update(qCXFOjcJkE.f983a, qCXFOjcJkE.f984b, min);
            } else {
                this.f1000b.update(qCXFOjcJkE.f983a, qCXFOjcJkE.f984b, min);
            }
            j2 += (long) min;
            qCXFOjcJkE = qCXFOjcJkE.f988f;
        }
        super.a_(cjrhissqcl, j);
    }
}
