package im.getsocial.sdk.internal.p086j.p087a;

import im.getsocial.sdk.Callback;
import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p030e.KSZKMmRWhZ;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT;
import im.getsocial.sdk.internal.p033c.iFpupLCESp;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.internal.p086j.p088b.cjrhisSQCL;
import io.fabric.sdk.android.services.common.CommonUtils;
import java.math.BigInteger;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.j.a.jjbQypPegg */
public class jjbQypPegg extends im.getsocial.sdk.internal.p030e.jjbQypPegg<pdwpUtZXDT<cjrhisSQCL>> {
    /* renamed from: e */
    private static Map<String, cjrhisSQCL> f1985e;
    /* renamed from: f */
    private static final im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL f1986f = upgqDBbsrL.m1274a(jjbQypPegg.class);
    @XdbacJlTDQ
    /* renamed from: a */
    iFpupLCESp f1987a;
    @XdbacJlTDQ
    /* renamed from: b */
    im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL f1988b;
    /* renamed from: c */
    private final im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg f1989c;
    /* renamed from: d */
    private final im.getsocial.sdk.internal.p086j.p088b.pdwpUtZXDT f1990d;

    public jjbQypPegg(im.getsocial.sdk.internal.p086j.p088b.jjbQypPegg jjbqyppegg, im.getsocial.sdk.internal.p086j.p088b.pdwpUtZXDT pdwputzxdt) {
        ztWNWCuZiM.m1221a((Object) this);
        this.f1989c = jjbqyppegg;
        this.f1990d = pdwputzxdt;
    }

    /* renamed from: a */
    static /* synthetic */ void m1999a(jjbQypPegg jjbqyppegg, String str, cjrhisSQCL cjrhissqcl) {
        if (str != null) {
            jjbQypPegg.m2001b().put(str, cjrhissqcl);
        }
    }

    /* renamed from: b */
    private static Map<String, cjrhisSQCL> m2001b() {
        if (f1985e == null) {
            f1985e = new HashMap();
        }
        return f1985e;
    }

    /* renamed from: c */
    private String m2002c() {
        try {
            MessageDigest instance = MessageDigest.getInstance(CommonUtils.MD5_INSTANCE);
            instance.update(this.f1989c.m2009a());
            return new BigInteger(1, instance.digest()).toString(16);
        } catch (NoSuchAlgorithmException e) {
            f1986f.mo4387a("Could not calculate hash for media content, error: " + e.getMessage());
            return null;
        }
    }

    /* renamed from: a */
    public final /* synthetic */ Object mo4487a() {
        if (this.f1989c == null) {
            return pdwpUtZXDT.m1659a(null);
        }
        long b = this.f1988b.m1326f().m1107b();
        long length = (long) this.f1989c.m2009a().length;
        if (length > b) {
            return pdwpUtZXDT.m1660a(new GetSocialException(ErrorCode.MEDIAUPLOAD_FILE_SIZE_OVER_LIMIT, "The file you want to upload is too large [" + length + "], limit is [" + b + "]"));
        }
        final String c = m2002c();
        return (c == null || !jjbQypPegg.m2001b().containsKey(c)) ? pdwpUtZXDT.m1658a(new KSZKMmRWhZ<im.getsocial.sdk.internal.p030e.p065a.ztWNWCuZiM<? super cjrhisSQCL>>(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f1984b;

            /* renamed from: b */
            public final /* synthetic */ void mo4412b(Object obj) {
                final im.getsocial.sdk.internal.p030e.p065a.ztWNWCuZiM ztwnwcuzim = (im.getsocial.sdk.internal.p030e.p065a.ztWNWCuZiM) obj;
                this.f1984b.f1987a.mo4400a(this.f1984b.f1989c, this.f1984b.f1990d, new Callback<cjrhisSQCL>(this) {
                    /* renamed from: b */
                    final /* synthetic */ C10141 f1982b;

                    public void onFailure(GetSocialException getSocialException) {
                        ztwnwcuzim.mo4490a((Throwable) getSocialException);
                    }

                    public /* synthetic */ void onSuccess(Object obj) {
                        obj = (cjrhisSQCL) obj;
                        jjbQypPegg.m1999a(this.f1982b.f1984b, c, obj);
                        ztwnwcuzim.mo4489a(obj);
                    }
                });
            }
        }) : pdwpUtZXDT.m1659a((cjrhisSQCL) jjbQypPegg.m2001b().get(c));
    }
}
