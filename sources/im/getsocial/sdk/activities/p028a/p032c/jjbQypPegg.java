package im.getsocial.sdk.activities.p028a.p032c;

import im.getsocial.sdk.activities.ActivitiesQuery;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.ActivityPost.Builder;
import im.getsocial.sdk.activities.Mention;
import im.getsocial.sdk.activities.PostAuthor;
import im.getsocial.sdk.activities.ReportingReason;
import im.getsocial.sdk.activities.TagsQuery;
import im.getsocial.sdk.internal.p033c.p066m.ztWNWCuZiM;
import im.getsocial.sdk.internal.p070f.p071a.SKUqohGtGQ;
import im.getsocial.sdk.internal.p070f.p071a.XdbacJlTDQ;
import im.getsocial.sdk.internal.p070f.p071a.ZWjsSaCmFq;
import im.getsocial.sdk.internal.p070f.p071a.iqXBPEYHZB;
import im.getsocial.sdk.internal.p070f.p071a.ofLJAxfaCe;
import im.getsocial.sdk.internal.p070f.p071a.zoToeBNOjF;
import im.getsocial.sdk.internal.p089m.qdyNCsqjKt;
import im.getsocial.sdk.usermanagement.PublicUser;
import im.getsocial.sdk.usermanagement.pdwpUtZXDT;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/* renamed from: im.getsocial.sdk.activities.a.c.jjbQypPegg */
public final class jjbQypPegg {
    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static ActivityPost m976a(XdbacJlTDQ xdbacJlTDQ) {
        String str;
        Builder feedId;
        List<SKUqohGtGQ> list;
        List emptyList;
        List arrayList;
        String str2 = null;
        if (xdbacJlTDQ.f1647b.f1671c != null) {
            str2 = xdbacJlTDQ.f1647b.f1671c.f1834b;
            str = xdbacJlTDQ.f1647b.f1671c.f1833a;
        } else {
            str = null;
        }
        Builder content = ActivityPost.builder().id(xdbacJlTDQ.f1646a).commentsCount(qdyNCsqjKt.m2121a(xdbacJlTDQ.f1652g)).likesCount(qdyNCsqjKt.m2121a(xdbacJlTDQ.f1653h)).likedByMe(qdyNCsqjKt.m2122a(xdbacJlTDQ.f1654i)).createdAt(qdyNCsqjKt.m2123b(xdbacJlTDQ.f1649d)).stickyStart(qdyNCsqjKt.m2123b(xdbacJlTDQ.f1650e)).stickyEnd(qdyNCsqjKt.m2123b(xdbacJlTDQ.f1651f)).author(jjbQypPegg.m977a(xdbacJlTDQ.f1648c)).content(xdbacJlTDQ.f1647b.f1669a, xdbacJlTDQ.f1647b.f1670b, xdbacJlTDQ.f1647b.f1672d, str2, str);
        String str3 = xdbacJlTDQ.f1655j;
        if (!ztWNWCuZiM.m1521a(str3)) {
            if (ActivitiesQuery.GLOBAL_FEED.equals(str3)) {
                str3 = ActivitiesQuery.GLOBAL_FEED;
            } else if (str3.startsWith("s-")) {
                str3 = str3.substring(2);
            }
            feedId = content.feedId(str3);
            list = xdbacJlTDQ.f1656k;
            if (list != null) {
                emptyList = Collections.emptyList();
            } else {
                arrayList = new ArrayList(list.size());
                for (SKUqohGtGQ sKUqohGtGQ : list) {
                    arrayList.add(Mention.builder().withUserId(sKUqohGtGQ.f1638c).withType(sKUqohGtGQ.f1639d).withStartIndex(sKUqohGtGQ.f1636a.intValue()).withEndIndex(sKUqohGtGQ.f1637b.intValue()).build());
                }
                emptyList = arrayList;
            }
            return feedId.mentions(emptyList).build();
        }
        str3 = "";
        feedId = content.feedId(str3);
        list = xdbacJlTDQ.f1656k;
        if (list != null) {
            arrayList = new ArrayList(list.size());
            for (SKUqohGtGQ sKUqohGtGQ2 : list) {
                arrayList.add(Mention.builder().withUserId(sKUqohGtGQ2.f1638c).withType(sKUqohGtGQ2.f1639d).withStartIndex(sKUqohGtGQ2.f1636a.intValue()).withEndIndex(sKUqohGtGQ2.f1637b.intValue()).build());
            }
            emptyList = arrayList;
        } else {
            emptyList = Collections.emptyList();
        }
        return feedId.mentions(emptyList).build();
    }

    /* renamed from: a */
    public static PostAuthor m977a(ofLJAxfaCe ofljaxface) {
        Map a = im.getsocial.sdk.usermanagement.p138a.p142d.jjbQypPegg.m3709a(ofljaxface.f1824d);
        HashMap hashMap = new HashMap();
        boolean a2 = qdyNCsqjKt.m2122a(ofljaxface.f1825e);
        boolean a3 = qdyNCsqjKt.m2122a(ofljaxface.f1826f);
        PublicUser build = new PostAuthor.Builder(ofljaxface.f1821a).setAvatarUrl(ofljaxface.f1823c).setDisplayName(ofljaxface.f1822b).setIdentities(a).setVerified(a2).setPublicProperties(hashMap).build();
        pdwpUtZXDT.m3729a(build, a3);
        return build;
    }

    /* renamed from: a */
    public static ZWjsSaCmFq m978a(TagsQuery tagsQuery) {
        ZWjsSaCmFq zWjsSaCmFq = new ZWjsSaCmFq();
        zWjsSaCmFq.f1666a = Integer.valueOf(tagsQuery.getLimit());
        zWjsSaCmFq.f1667b = tagsQuery.getQuery();
        zWjsSaCmFq.f1668c = tagsQuery.getFeedName();
        return zWjsSaCmFq;
    }

    /* renamed from: a */
    public static iqXBPEYHZB m979a(ReportingReason reportingReason) {
        switch (reportingReason) {
            case SPAM:
                return iqXBPEYHZB.SPAM;
            case INAPPROPRIATE_CONTENT:
                return iqXBPEYHZB.INAPPROPRIATE_CONTENT;
            default:
                return iqXBPEYHZB.SPAM;
        }
    }

    /* renamed from: a */
    public static im.getsocial.sdk.internal.p070f.p071a.pdwpUtZXDT m980a(ActivitiesQuery activitiesQuery) {
        im.getsocial.sdk.activities.jjbQypPegg a = im.getsocial.sdk.activities.jjbQypPegg.m998a(activitiesQuery);
        im.getsocial.sdk.internal.p070f.p071a.pdwpUtZXDT pdwputzxdt = new im.getsocial.sdk.internal.p070f.p071a.pdwpUtZXDT();
        pdwputzxdt.f1829c = a.m1004f();
        pdwputzxdt.f1828b = a.m1003e();
        pdwputzxdt.f1827a = Integer.valueOf(a.m1002d());
        pdwputzxdt.f1830d = a.m1005g();
        pdwputzxdt.f1831e = Boolean.valueOf(a.m1006h());
        pdwputzxdt.f1832f = a.m1007i();
        return pdwputzxdt;
    }

    /* renamed from: a */
    public static zoToeBNOjF m981a(im.getsocial.sdk.activities.p028a.p029a.jjbQypPegg jjbqyppegg) {
        zoToeBNOjF zotoebnojf = new zoToeBNOjF();
        zotoebnojf.f1867a = jjbqyppegg.m961d();
        zotoebnojf.f1871e = jjbqyppegg.m966g();
        zotoebnojf.f1868b = jjbqyppegg.m963e();
        zotoebnojf.f1872f = jjbqyppegg.m965f();
        zotoebnojf.f1869c = jjbqyppegg.m968i();
        zotoebnojf.f1870d = jjbqyppegg.m969j();
        return zotoebnojf;
    }
}
