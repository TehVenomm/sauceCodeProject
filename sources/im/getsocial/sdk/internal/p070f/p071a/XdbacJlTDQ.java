package im.getsocial.sdk.internal.p070f.p071a;

import im.getsocial.p018b.p021c.p022a.cjrhisSQCL;
import im.getsocial.p018b.p021c.p022a.upgqDBbsrL;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p025d.jjbQypPegg;
import java.util.ArrayList;
import java.util.List;

/* renamed from: im.getsocial.sdk.internal.f.a.XdbacJlTDQ */
public final class XdbacJlTDQ {
    /* renamed from: a */
    public String f1646a;
    /* renamed from: b */
    public bpiSwUyLit f1647b;
    /* renamed from: c */
    public ofLJAxfaCe f1648c;
    /* renamed from: d */
    public Integer f1649d;
    /* renamed from: e */
    public Integer f1650e;
    /* renamed from: f */
    public Integer f1651f;
    /* renamed from: g */
    public Integer f1652g;
    /* renamed from: h */
    public Integer f1653h;
    /* renamed from: i */
    public Boolean f1654i;
    /* renamed from: j */
    public String f1655j;
    /* renamed from: k */
    public List<SKUqohGtGQ> f1656k;

    /* renamed from: a */
    public static XdbacJlTDQ m1701a(zoToeBNOjF zotoebnojf) {
        XdbacJlTDQ xdbacJlTDQ = new XdbacJlTDQ();
        while (true) {
            upgqDBbsrL c = zotoebnojf.mo4326c();
            if (c.f1055b != (byte) 0) {
                switch (c.f1056c) {
                    case (short) 1:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        xdbacJlTDQ.f1646a = zotoebnojf.mo4336m();
                        break;
                    case (short) 2:
                        if (c.f1055b != (byte) 12) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        xdbacJlTDQ.f1647b = bpiSwUyLit.m1704a(zotoebnojf);
                        break;
                    case (short) 3:
                        if (c.f1055b != (byte) 12) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        xdbacJlTDQ.f1648c = ofLJAxfaCe.m1879a(zotoebnojf);
                        break;
                    case (short) 4:
                        if (c.f1055b != (byte) 8) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        xdbacJlTDQ.f1649d = Integer.valueOf(zotoebnojf.mo4333j());
                        break;
                    case (short) 5:
                        if (c.f1055b != (byte) 8) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        xdbacJlTDQ.f1650e = Integer.valueOf(zotoebnojf.mo4333j());
                        break;
                    case (short) 6:
                        if (c.f1055b != (byte) 8) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        xdbacJlTDQ.f1651f = Integer.valueOf(zotoebnojf.mo4333j());
                        break;
                    case (short) 7:
                        if (c.f1055b != (byte) 8) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        xdbacJlTDQ.f1652g = Integer.valueOf(zotoebnojf.mo4333j());
                        break;
                    case (short) 8:
                        if (c.f1055b != (byte) 8) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        xdbacJlTDQ.f1653h = Integer.valueOf(zotoebnojf.mo4333j());
                        break;
                    case (short) 9:
                        if (c.f1055b != (byte) 2) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        xdbacJlTDQ.f1654i = Boolean.valueOf(zotoebnojf.mo4330g());
                        break;
                    case (short) 10:
                        if (c.f1055b != (byte) 11) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        xdbacJlTDQ.f1655j = zotoebnojf.mo4336m();
                        break;
                    case (short) 11:
                        if (c.f1055b != (byte) 15) {
                            jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                        }
                        cjrhisSQCL e = zotoebnojf.mo4328e();
                        List arrayList = new ArrayList(e.f1045b);
                        for (int i = 0; i < e.f1045b; i++) {
                            arrayList.add(SKUqohGtGQ.m1698a(zotoebnojf));
                        }
                        xdbacJlTDQ.f1656k = arrayList;
                        break;
                    default:
                        jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                        break;
                }
            }
            return xdbacJlTDQ;
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!(obj instanceof XdbacJlTDQ)) {
            return false;
        }
        XdbacJlTDQ xdbacJlTDQ = (XdbacJlTDQ) obj;
        if ((this.f1646a == xdbacJlTDQ.f1646a || (this.f1646a != null && this.f1646a.equals(xdbacJlTDQ.f1646a))) && ((this.f1647b == xdbacJlTDQ.f1647b || (this.f1647b != null && this.f1647b.equals(xdbacJlTDQ.f1647b))) && ((this.f1648c == xdbacJlTDQ.f1648c || (this.f1648c != null && this.f1648c.equals(xdbacJlTDQ.f1648c))) && ((this.f1649d == xdbacJlTDQ.f1649d || (this.f1649d != null && this.f1649d.equals(xdbacJlTDQ.f1649d))) && ((this.f1650e == xdbacJlTDQ.f1650e || (this.f1650e != null && this.f1650e.equals(xdbacJlTDQ.f1650e))) && ((this.f1651f == xdbacJlTDQ.f1651f || (this.f1651f != null && this.f1651f.equals(xdbacJlTDQ.f1651f))) && ((this.f1652g == xdbacJlTDQ.f1652g || (this.f1652g != null && this.f1652g.equals(xdbacJlTDQ.f1652g))) && ((this.f1653h == xdbacJlTDQ.f1653h || (this.f1653h != null && this.f1653h.equals(xdbacJlTDQ.f1653h))) && ((this.f1654i == xdbacJlTDQ.f1654i || (this.f1654i != null && this.f1654i.equals(xdbacJlTDQ.f1654i))) && (this.f1655j == xdbacJlTDQ.f1655j || (this.f1655j != null && this.f1655j.equals(xdbacJlTDQ.f1655j)))))))))))) {
            if (this.f1656k == xdbacJlTDQ.f1656k) {
                return true;
            }
            if (this.f1656k != null && this.f1656k.equals(xdbacJlTDQ.f1656k)) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int i = 0;
        int hashCode = this.f1646a == null ? 0 : this.f1646a.hashCode();
        int hashCode2 = this.f1647b == null ? 0 : this.f1647b.hashCode();
        int hashCode3 = this.f1648c == null ? 0 : this.f1648c.hashCode();
        int hashCode4 = this.f1649d == null ? 0 : this.f1649d.hashCode();
        int hashCode5 = this.f1650e == null ? 0 : this.f1650e.hashCode();
        int hashCode6 = this.f1651f == null ? 0 : this.f1651f.hashCode();
        int hashCode7 = this.f1652g == null ? 0 : this.f1652g.hashCode();
        int hashCode8 = this.f1653h == null ? 0 : this.f1653h.hashCode();
        int hashCode9 = this.f1654i == null ? 0 : this.f1654i.hashCode();
        int hashCode10 = this.f1655j == null ? 0 : this.f1655j.hashCode();
        if (this.f1656k != null) {
            i = this.f1656k.hashCode();
        }
        return (((((((((((((((((((((hashCode ^ 16777619) * -2128831035) ^ hashCode2) * -2128831035) ^ hashCode3) * -2128831035) ^ hashCode4) * -2128831035) ^ hashCode5) * -2128831035) ^ hashCode6) * -2128831035) ^ hashCode7) * -2128831035) ^ hashCode8) * -2128831035) ^ hashCode9) * -2128831035) ^ hashCode10) * -2128831035) ^ i) * -2128831035;
    }

    public final String toString() {
        return "THActivityPost{id=" + this.f1646a + ", content=" + this.f1647b + ", author=" + this.f1648c + ", createdAt=" + this.f1649d + ", stickyStart=" + this.f1650e + ", stickyEnd=" + this.f1651f + ", commentsCount=" + this.f1652g + ", likesCount=" + this.f1653h + ", likedByMe=" + this.f1654i + ", feedId=" + this.f1655j + ", mentions=" + this.f1656k + "}";
    }
}
