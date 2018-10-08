package im.getsocial.sdk.internal.p033c.p054e;

import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import java.util.concurrent.TimeUnit;

/* renamed from: im.getsocial.sdk.internal.c.e.cjrhisSQCL */
public final class cjrhisSQCL implements upgqDBbsrL {
    /* renamed from: a */
    private static final im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL f1268a = upgqDBbsrL.m1274a(cjrhisSQCL.class);
    /* renamed from: b */
    private final upgqDBbsrL f1269b;
    /* renamed from: c */
    private final int f1270c = 3;
    /* renamed from: d */
    private final int f1271d = 1;
    /* renamed from: e */
    private final TimeUnit f1272e;
    /* renamed from: f */
    private int f1273f;

    public cjrhisSQCL(upgqDBbsrL upgqdbbsrl, int i, int i2, TimeUnit timeUnit) {
        this.f1269b = upgqdbbsrl;
        this.f1272e = timeUnit;
        this.f1273f = 0;
    }

    /* renamed from: a */
    public final /* synthetic */ Object mo4344a(Object obj) {
        GetSocialException a = jjbQypPegg.m1222a((Throwable) obj);
        if (this.f1273f >= this.f1270c || a.getErrorCode() != ErrorCode.CONNECTION_TIMEOUT) {
            return (pdwpUtZXDT) this.f1269b.mo4344a(a);
        }
        this.f1273f++;
        f1268a.mo4387a("Chain failed, retry again in " + this.f1271d + " " + this.f1272e.name() + ", attempt #" + this.f1273f + " of " + this.f1270c);
        return pdwpUtZXDT.m1657a((long) this.f1271d, this.f1272e);
    }
}
