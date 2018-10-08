package im.getsocial.sdk.invites.p092a.p101h;

import im.getsocial.sdk.internal.p033c.fOrCGNYyfk;
import im.getsocial.sdk.internal.p033c.p066m.ztWNWCuZiM;
import im.getsocial.sdk.internal.p070f.p071a.HptYHntaqF;
import im.getsocial.sdk.internal.p070f.p071a.IbawHMWljm;
import im.getsocial.sdk.internal.p070f.p071a.JQrJMKopAa;
import im.getsocial.sdk.internal.p070f.p071a.JbBdMtJmlU;
import im.getsocial.sdk.internal.p070f.p071a.KCGqEGAizh;
import im.getsocial.sdk.internal.p070f.p071a.QCXFOjcJkE;
import im.getsocial.sdk.internal.p070f.p071a.QWVUXapsSm;
import im.getsocial.sdk.internal.p070f.p071a.YgeTlQwUNa;
import im.getsocial.sdk.internal.p070f.p071a.qZypgoeblR;
import im.getsocial.sdk.internal.p070f.p071a.ruWsnwUPKh;
import im.getsocial.sdk.internal.p070f.p071a.wWemqSpYTx;
import im.getsocial.sdk.invites.LinkParams;
import im.getsocial.sdk.invites.LocalizableText;
import im.getsocial.sdk.invites.ReferralData;
import im.getsocial.sdk.invites.ReferredUser;
import im.getsocial.sdk.invites.ReferredUser.Builder;
import im.getsocial.sdk.invites.p092a.p094b.XdbacJlTDQ;
import im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT;
import im.getsocial.sdk.invites.p092a.p094b.upgqDBbsrL;
import im.getsocial.sdk.invites.p092a.p094b.zoToeBNOjF;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;

/* renamed from: im.getsocial.sdk.invites.a.h.jjbQypPegg */
public final class jjbQypPegg {
    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static IbawHMWljm m2357a(fOrCGNYyfk forcgnyyfk) {
        IbawHMWljm ibawHMWljm = new IbawHMWljm();
        ibawHMWljm.f1587d = forcgnyyfk.mo4372d();
        ibawHMWljm.f1588e = forcgnyyfk.mo4373e();
        ibawHMWljm.f1585b = String.valueOf(forcgnyyfk.mo4369a());
        ibawHMWljm.f1584a = String.valueOf(forcgnyyfk.mo4370b());
        ibawHMWljm.f1586c = String.valueOf(forcgnyyfk.mo4371c());
        ibawHMWljm.f1590g = forcgnyyfk.mo4375g();
        ibawHMWljm.f1589f = forcgnyyfk.mo4374f();
        return ibawHMWljm;
    }

    /* renamed from: a */
    public static ReferralData m2358a(JQrJMKopAa jQrJMKopAa) {
        if (jQrJMKopAa == null || jQrJMKopAa.f1595e == null || !jQrJMKopAa.f1595e.containsKey("$token")) {
            return null;
        }
        LinkParams linkParams = new LinkParams();
        linkParams.putAll(jQrJMKopAa.f1595e);
        LinkParams linkParams2 = new LinkParams();
        if (jQrJMKopAa.f1598h != null) {
            linkParams2.putAll(jQrJMKopAa.f1598h);
        }
        return im.getsocial.sdk.invites.jjbQypPegg.m2407a(String.valueOf(jQrJMKopAa.f1595e.get("$token")), String.valueOf(jQrJMKopAa.f1595e.get("$referrer_user_guid")), String.valueOf(jQrJMKopAa.f1595e.get("$channel")), Boolean.valueOf((String) jQrJMKopAa.f1595e.get("$first_match")).booleanValue(), Boolean.valueOf((String) jQrJMKopAa.f1595e.get("$guaranteed_match")).booleanValue(), Boolean.valueOf((String) jQrJMKopAa.f1595e.get("$reinstall")).booleanValue(), Boolean.valueOf((String) jQrJMKopAa.f1595e.get("$first_match_link")).booleanValue(), linkParams, linkParams2);
    }

    /* renamed from: a */
    public static ReferredUser m2359a(YgeTlQwUNa ygeTlQwUNa) {
        return new Builder(ygeTlQwUNa.f1657a).setAvatarUrl(ygeTlQwUNa.f1659c).setDisplayName(ygeTlQwUNa.f1658b).setIdentities(im.getsocial.sdk.usermanagement.p138a.p142d.jjbQypPegg.m3709a(ygeTlQwUNa.f1660d)).setPublicProperties(ygeTlQwUNa.f1661e).setInstallationDate(Long.parseLong(ygeTlQwUNa.f1663g)).setInstallationChannel(ygeTlQwUNa.f1664h).build();
    }

    /* renamed from: a */
    public static XdbacJlTDQ m2360a(QCXFOjcJkE qCXFOjcJkE) {
        return new XdbacJlTDQ(qCXFOjcJkE.f1629a, qCXFOjcJkE.f1630b);
    }

    /* renamed from: a */
    private static pdwpUtZXDT m2361a(QWVUXapsSm qWVUXapsSm) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) qWVUXapsSm), "Can not convert null THInviteContent.");
        im.getsocial.sdk.invites.p092a.p094b.pdwpUtZXDT.jjbQypPegg a = pdwpUtZXDT.m2276a();
        if (!(qWVUXapsSm.f1633c == null || jjbQypPegg.m2366c(qWVUXapsSm.f1633c))) {
            a.m2270b(new LocalizableText(new HashMap(qWVUXapsSm.f1633c)));
        }
        if (!(qWVUXapsSm.f1632b == null || jjbQypPegg.m2366c(qWVUXapsSm.f1632b))) {
            a.m2266a(new LocalizableText(new HashMap(qWVUXapsSm.f1632b)));
        }
        if (qWVUXapsSm.f1631a != null && qWVUXapsSm.f1631a.trim().length() > 0) {
            a.m2273c(qWVUXapsSm.f1631a);
        }
        if (qWVUXapsSm.f1634d != null && qWVUXapsSm.f1634d.trim().length() > 0) {
            a.m2275e(qWVUXapsSm.f1634d);
        }
        if (qWVUXapsSm.f1635e != null && qWVUXapsSm.f1635e.trim().length() > 0) {
            a.m2274d(qWVUXapsSm.f1635e);
        }
        return a.m2269a();
    }

    /* renamed from: a */
    public static upgqDBbsrL m2362a(JbBdMtJmlU jbBdMtJmlU) {
        pdwpUtZXDT a = jjbQypPegg.m2361a(jbBdMtJmlU.f1608a);
        Object<wWemqSpYTx> obj = jbBdMtJmlU.f1609b;
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) obj), "Unexpected response from the API. Providers list is empty");
        List arrayList = new ArrayList(obj.size());
        for (wWemqSpYTx wwemqspytx : obj) {
            arrayList.add(im.getsocial.sdk.invites.jjbQypPegg.m2406a(wwemqspytx.f1842b, new LocalizableText(wwemqspytx.f1841a), wwemqspytx.f1845e, wwemqspytx.f1846f == null ? false : wwemqspytx.f1846f.booleanValue(), wwemqspytx.f1844d == null ? 0 : wwemqspytx.f1844d.intValue(), jjbQypPegg.m2363a(wwemqspytx.f1850j), jjbQypPegg.m2361a(wwemqspytx.f1851k)));
        }
        return new upgqDBbsrL(a, arrayList);
    }

    /* renamed from: a */
    private static zoToeBNOjF m2363a(KCGqEGAizh kCGqEGAizh) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) kCGqEGAizh), "Can not convert null THInviteProperties.");
        String str = kCGqEGAizh.f1610a;
        List arrayList = new ArrayList();
        if (kCGqEGAizh.f1611b != null) {
            for (ruWsnwUPKh ruwsnwupkh : kCGqEGAizh.f1611b) {
                Object obj;
                switch (ruwsnwupkh) {
                    case ExtraSubject:
                        obj = im.getsocial.sdk.invites.p092a.p094b.jjbQypPegg.EXTRA_SUBJECT;
                        break;
                    case ExtraText:
                        obj = im.getsocial.sdk.invites.p092a.p094b.jjbQypPegg.EXTRA_TEXT;
                        break;
                    case ExtraStream:
                        obj = im.getsocial.sdk.invites.p092a.p094b.jjbQypPegg.EXTRA_STREAM;
                        break;
                    case ExtraGif:
                        obj = im.getsocial.sdk.invites.p092a.p094b.jjbQypPegg.EXTRA_GIF;
                        break;
                    case ExtraVideo:
                        obj = im.getsocial.sdk.invites.p092a.p094b.jjbQypPegg.EXTRA_VIDEO;
                        break;
                    default:
                        throw new IllegalArgumentException();
                }
                arrayList.add(obj);
            }
        }
        return new zoToeBNOjF(str, arrayList, kCGqEGAizh.f1612c, kCGqEGAizh.f1613d, kCGqEGAizh.f1614e, kCGqEGAizh.f1615f, kCGqEGAizh.f1617h);
    }

    /* renamed from: a */
    public static Map<qZypgoeblR, Map<HptYHntaqF, String>> m2364a(Map<im.getsocial.sdk.invites.p092a.p093a.pdwpUtZXDT, Map<String, String>> map) {
        Map<qZypgoeblR, Map<HptYHntaqF, String>> hashMap = new HashMap();
        for (Entry entry : map.entrySet()) {
            Object obj;
            switch ((im.getsocial.sdk.invites.p092a.p093a.pdwpUtZXDT) entry.getKey()) {
                case GOOGLE_PLAY:
                    obj = qZypgoeblR.Google;
                    break;
                case FACEBOOK:
                    obj = qZypgoeblR.Facebook;
                    break;
                case DEEP_LINK:
                    obj = qZypgoeblR.DeepLink;
                    break;
                default:
                    throw new IllegalArgumentException();
            }
            hashMap.put(obj, jjbQypPegg.m2365b((Map) entry.getValue()));
        }
        return hashMap;
    }

    /* renamed from: b */
    private static Map<HptYHntaqF, String> m2365b(Map<String, String> map) {
        Map<HptYHntaqF, String> hashMap = new HashMap();
        for (Entry entry : map.entrySet()) {
            hashMap.put(HptYHntaqF.valueOf((String) entry.getKey()), entry.getValue());
        }
        return hashMap;
    }

    /* renamed from: c */
    private static boolean m2366c(Map<String, String> map) {
        for (String a : map.values()) {
            if (!ztWNWCuZiM.m1521a(a)) {
                return false;
            }
        }
        return true;
    }
}
