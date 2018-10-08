package im.getsocial.sdk.usermanagement.p138a.p142d;

import im.getsocial.sdk.internal.p033c.IbawHMWljm;
import im.getsocial.sdk.internal.p033c.QCXFOjcJkE;
import im.getsocial.sdk.internal.p033c.QhisXzMgay;
import im.getsocial.sdk.internal.p033c.ztWNWCuZiM;
import im.getsocial.sdk.internal.p070f.p071a.CyDeXbQkhA;
import im.getsocial.sdk.internal.p070f.p071a.FvojpKUsoc;
import im.getsocial.sdk.internal.p070f.p071a.KdkQzTlDzz;
import im.getsocial.sdk.internal.p070f.p071a.KluUZYuxme;
import im.getsocial.sdk.internal.p070f.p071a.VuXsWfriFX;
import im.getsocial.sdk.internal.p070f.p071a.YgeTlQwUNa;
import im.getsocial.sdk.internal.p070f.p071a.fOrCGNYyfk;
import im.getsocial.sdk.internal.p070f.p071a.iFpupLCESp;
import im.getsocial.sdk.internal.p070f.p071a.icjTFWWVFN;
import im.getsocial.sdk.internal.p070f.p071a.nGNJgptECj;
import im.getsocial.sdk.internal.p070f.p071a.rWfbqYooCV;
import im.getsocial.sdk.internal.p070f.p071a.xAXgtBkRbG;
import im.getsocial.sdk.internal.p070f.p071a.zITzQAtzdj;
import im.getsocial.sdk.internal.p089m.qdyNCsqjKt;
import im.getsocial.sdk.usermanagement.AuthIdentity;
import im.getsocial.sdk.usermanagement.PrivateUser;
import im.getsocial.sdk.usermanagement.PrivateUser.Builder;
import im.getsocial.sdk.usermanagement.PublicUser;
import im.getsocial.sdk.usermanagement.UserReference;
import im.getsocial.sdk.usermanagement.UsersQuery;
import im.getsocial.sdk.usermanagement.cjrhisSQCL;
import im.getsocial.sdk.usermanagement.p138a.p139a.pdwpUtZXDT;
import im.getsocial.sdk.usermanagement.upgqDBbsrL;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/* renamed from: im.getsocial.sdk.usermanagement.a.d.jjbQypPegg */
public final class jjbQypPegg {
    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static KdkQzTlDzz m3699a(im.getsocial.sdk.usermanagement.p138a.p139a.jjbQypPegg jjbqyppegg) {
        KdkQzTlDzz kdkQzTlDzz = new KdkQzTlDzz();
        kdkQzTlDzz.f1621b = jjbqyppegg.m3648b();
        kdkQzTlDzz.f1620a = jjbqyppegg.m3645a();
        kdkQzTlDzz.f1622c = jjbqyppegg.m3650c();
        kdkQzTlDzz.f1623d = jjbQypPegg.m3702a(jjbqyppegg.m3652d());
        kdkQzTlDzz.f1624e = jjbqyppegg.m3654e();
        return kdkQzTlDzz;
    }

    /* renamed from: a */
    public static VuXsWfriFX m3700a(UsersQuery usersQuery) {
        VuXsWfriFX vuXsWfriFX = new VuXsWfriFX();
        vuXsWfriFX.f1645b = usersQuery.getQuery();
        vuXsWfriFX.f1644a = Integer.valueOf(usersQuery.getLimit());
        return vuXsWfriFX;
    }

    /* renamed from: a */
    public static iFpupLCESp m3701a(IbawHMWljm ibawHMWljm) {
        switch (ibawHMWljm) {
            case ANDROID:
                return iFpupLCESp.ANDROID;
            case IOS:
                return iFpupLCESp.IOS;
            default:
                return null;
        }
    }

    /* renamed from: a */
    public static icjTFWWVFN m3702a(QhisXzMgay qhisXzMgay) {
        icjTFWWVFN icjtfwwvfn = new icjTFWWVFN();
        icjtfwwvfn.f1789b = qhisXzMgay.mo4461b();
        icjtfwwvfn.f1792e = qhisXzMgay.mo4462c();
        icjtfwwvfn.f1790c = qhisXzMgay.mo4464e();
        icjtfwwvfn.f1791d = qhisXzMgay.mo4465f();
        icjtfwwvfn.f1793f = FvojpKUsoc.valueOf(qhisXzMgay.mo4467h());
        icjtfwwvfn.f1794g = qhisXzMgay.mo4468i();
        icjtfwwvfn.f1796i = qhisXzMgay.mo4469j();
        icjtfwwvfn.f1795h = qhisXzMgay.mo4466g();
        icjtfwwvfn.f1797j = qhisXzMgay.mo4470k();
        icjtfwwvfn.f1788a = qhisXzMgay.mo4473n();
        icjtfwwvfn.f1798k = qhisXzMgay.mo4471l();
        icjtfwwvfn.f1799l = jjbQypPegg.m3701a(qhisXzMgay.mo4460a());
        icjtfwwvfn.f1800m = qhisXzMgay.mo4463d();
        icjtfwwvfn.f1801n = qhisXzMgay.mo4476q();
        icjtfwwvfn.f1802o = qhisXzMgay.mo4475p();
        icjtfwwvfn.f1803p = qhisXzMgay.mo4477r();
        icjtfwwvfn.f1804q = qhisXzMgay.mo4478s();
        icjtfwwvfn.f1805r = qhisXzMgay.mo4479t();
        icjtfwwvfn.f1806s = null;
        icjtfwwvfn.f1807t = qhisXzMgay.mo4481v().m1536a();
        icjtfwwvfn.f1808u = qhisXzMgay.mo4481v().m1537b();
        icjtfwwvfn.f1809v = Boolean.valueOf(qhisXzMgay.mo4482w());
        return icjtfwwvfn;
    }

    /* renamed from: a */
    public static nGNJgptECj m3703a(pdwpUtZXDT pdwputzxdt) {
        nGNJgptECj ngnjgptecj = new nGNJgptECj();
        ngnjgptecj.f1812c = pdwputzxdt.m3658a();
        ngnjgptecj.f1813d = pdwputzxdt.m3661b();
        ngnjgptecj.f1814e = pdwputzxdt.m3665d();
        ngnjgptecj.f1815f = pdwputzxdt.m3666e();
        ngnjgptecj.f1816g = pdwputzxdt.m3667f();
        ngnjgptecj.f1818i = pdwputzxdt.m3668g();
        return ngnjgptecj;
    }

    /* renamed from: a */
    public static rWfbqYooCV m3704a(AuthIdentity authIdentity) {
        upgqDBbsrL upgqdbbsrl = new upgqDBbsrL(authIdentity);
        rWfbqYooCV rwfbqyoocv = new rWfbqYooCV();
        rwfbqyoocv.f1837c = upgqdbbsrl.m3736c();
        rwfbqyoocv.f1835a = upgqdbbsrl.m3734a();
        rwfbqyoocv.f1836b = upgqdbbsrl.m3735b();
        return rwfbqyoocv;
    }

    /* renamed from: a */
    public static PrivateUser m3705a(nGNJgptECj ngnjgptecj) {
        Builder builder = new Builder(ngnjgptecj.f1810a);
        builder.setPassword(ngnjgptecj.f1811b);
        builder.setDisplayName(ngnjgptecj.f1812c);
        builder.setAvatarUrl(ngnjgptecj.f1813d);
        builder.setIdentities(jjbQypPegg.m3709a(ngnjgptecj.f1817h));
        builder.setPublicProperties(ngnjgptecj.f1814e);
        builder.setPrivateProperties(ngnjgptecj.f1815f);
        PublicUser build = builder.build();
        cjrhisSQCL.m3731a(build, ngnjgptecj.f1818i);
        im.getsocial.sdk.usermanagement.pdwpUtZXDT.m3728a(build, ngnjgptecj.f1816g);
        return build;
    }

    /* renamed from: a */
    public static PublicUser m3706a(YgeTlQwUNa ygeTlQwUNa) {
        PublicUser build = new PublicUser.Builder(ygeTlQwUNa.f1657a).setAvatarUrl(ygeTlQwUNa.f1659c).setDisplayName(ygeTlQwUNa.f1658b).setIdentities(jjbQypPegg.m3709a(ygeTlQwUNa.f1660d)).setPublicProperties(ygeTlQwUNa.f1661e).build();
        im.getsocial.sdk.usermanagement.pdwpUtZXDT.m3728a(build, ygeTlQwUNa.f1662f);
        return build;
    }

    /* renamed from: a */
    public static UserReference m3707a(CyDeXbQkhA cyDeXbQkhA) {
        return new UserReference.Builder(cyDeXbQkhA.f1572a).setAvatarUrl(cyDeXbQkhA.f1574c).setDisplayName(cyDeXbQkhA.f1573b).build();
    }

    /* renamed from: a */
    public static im.getsocial.sdk.usermanagement.p138a.p139a.upgqDBbsrL m3708a(xAXgtBkRbG xaxgtbkrbg) {
        im.getsocial.sdk.usermanagement.p138a.p139a.upgqDBbsrL upgqdbbsrl = new im.getsocial.sdk.usermanagement.p138a.p139a.upgqDBbsrL();
        upgqdbbsrl.m3675a(xaxgtbkrbg.f1854a);
        upgqdbbsrl.m3670a(qdyNCsqjKt.m2123b(xaxgtbkrbg.f1856c));
        upgqdbbsrl.m3674a(jjbQypPegg.m3705a(xaxgtbkrbg.f1855b));
        fOrCGNYyfk forcgnyyfk = xaxgtbkrbg.f1857d;
        upgqdbbsrl.m3673a(new im.getsocial.sdk.pushnotifications.p067a.p103b.cjrhisSQCL(forcgnyyfk == null ? false : qdyNCsqjKt.m2122a(forcgnyyfk.f1778l), forcgnyyfk == null ? "" : forcgnyyfk.f1776j));
        upgqdbbsrl.m3676a(xaxgtbkrbg.f1858e == null ? false : xaxgtbkrbg.f1858e.booleanValue());
        KluUZYuxme kluUZYuxme = xaxgtbkrbg.f1859f;
        upgqdbbsrl.m3672a(new ztWNWCuZiM(kluUZYuxme.f1626a, kluUZYuxme.f1627b, kluUZYuxme.f1628c));
        QCXFOjcJkE qCXFOjcJkE = new QCXFOjcJkE(xaxgtbkrbg.f1860g, xaxgtbkrbg.f1862i);
        zITzQAtzdj zitzqatzdj = xaxgtbkrbg.f1861h;
        if (zitzqatzdj != null) {
            qCXFOjcJkE.m1106a("WIFI", zitzqatzdj.f1863a);
            qCXFOjcJkE.m1106a("LTE", zitzqatzdj.f1864b);
            qCXFOjcJkE.m1106a("3G", zitzqatzdj.f1865c);
            qCXFOjcJkE.m1106a("OTHER", zitzqatzdj.f1866d);
        }
        upgqdbbsrl.m3671a(qCXFOjcJkE);
        return upgqdbbsrl;
    }

    /* renamed from: a */
    public static Map<String, String> m3709a(List<rWfbqYooCV> list) {
        Map<String, String> hashMap = new HashMap();
        if (list != null) {
            for (rWfbqYooCV rwfbqyoocv : list) {
                hashMap.put(rwfbqyoocv.f1835a, rwfbqyoocv.f1836b);
            }
        }
        return hashMap;
    }
}
