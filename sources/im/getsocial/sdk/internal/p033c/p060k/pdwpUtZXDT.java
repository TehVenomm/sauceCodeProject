package im.getsocial.sdk.internal.p033c.p060k;

import com.facebook.share.internal.ShareConstants;
import com.google.android.gms.actions.SearchIntents;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import im.getsocial.sdk.activities.ActivitiesQuery;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.ReportingReason;
import im.getsocial.sdk.activities.TagsQuery;
import im.getsocial.sdk.internal.p030e.KSZKMmRWhZ;
import im.getsocial.sdk.internal.p030e.p065a.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.IbawHMWljm;
import im.getsocial.sdk.internal.p033c.QhisXzMgay;
import im.getsocial.sdk.internal.p033c.fOrCGNYyfk;
import im.getsocial.sdk.internal.p033c.p048a.jjbQypPegg;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.internal.p070f.p071a.BpPZzHFMaU;
import im.getsocial.sdk.internal.p070f.p071a.CJZnJxRuoc;
import im.getsocial.sdk.internal.p070f.p071a.CyDeXbQkhA;
import im.getsocial.sdk.internal.p070f.p071a.DvmrLquonW;
import im.getsocial.sdk.internal.p070f.p071a.DvynvDnqtx;
import im.getsocial.sdk.internal.p070f.p071a.EmkjBpiUfq;
import im.getsocial.sdk.internal.p070f.p071a.JQrJMKopAa;
import im.getsocial.sdk.internal.p070f.p071a.JWvbLzaedN;
import im.getsocial.sdk.internal.p070f.p071a.JbBdMtJmlU;
import im.getsocial.sdk.internal.p070f.p071a.KdkQzTlDzz;
import im.getsocial.sdk.internal.p070f.p071a.QCXFOjcJkE;
import im.getsocial.sdk.internal.p070f.p071a.UwIeQkAzJH;
import im.getsocial.sdk.internal.p070f.p071a.VuXsWfriFX;
import im.getsocial.sdk.internal.p070f.p071a.XdbacJlTDQ;
import im.getsocial.sdk.internal.p070f.p071a.YgeTlQwUNa;
import im.getsocial.sdk.internal.p070f.p071a.ZWjsSaCmFq;
import im.getsocial.sdk.internal.p070f.p071a.icjTFWWVFN;
import im.getsocial.sdk.internal.p070f.p071a.iqXBPEYHZB;
import im.getsocial.sdk.internal.p070f.p071a.nGNJgptECj;
import im.getsocial.sdk.internal.p070f.p071a.ofLJAxfaCe;
import im.getsocial.sdk.internal.p070f.p071a.rWfbqYooCV;
import im.getsocial.sdk.internal.p070f.p071a.xAXgtBkRbG;
import im.getsocial.sdk.internal.p070f.p071a.zoToeBNOjF;
import im.getsocial.sdk.invites.LinkParams;
import im.getsocial.sdk.invites.ReferralData;
import im.getsocial.sdk.invites.ReferredUser;
import im.getsocial.sdk.pushnotifications.Notification;
import im.getsocial.sdk.pushnotifications.NotificationsCountQuery;
import im.getsocial.sdk.pushnotifications.NotificationsQuery;
import im.getsocial.sdk.socialgraph.SuggestedFriend;
import im.getsocial.sdk.usermanagement.AuthIdentity;
import im.getsocial.sdk.usermanagement.PrivateUser;
import im.getsocial.sdk.usermanagement.PublicUser;
import im.getsocial.sdk.usermanagement.UserReference;
import im.getsocial.sdk.usermanagement.UsersQuery;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.concurrent.TimeUnit;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: im.getsocial.sdk.internal.c.k.pdwpUtZXDT */
public class pdwpUtZXDT implements jjbQypPegg {
    /* renamed from: a */
    private static final cjrhisSQCL f1464a = upgqDBbsrL.m1274a(pdwpUtZXDT.class);
    /* renamed from: b */
    private final im.getsocial.sdk.internal.p033c.p060k.p063c.jjbQypPegg f1465b;
    /* renamed from: c */
    private final im.getsocial.sdk.internal.p033c.p060k.p064d.jjbQypPegg f1466c;

    /* renamed from: im.getsocial.sdk.internal.c.k.pdwpUtZXDT$upgqDBbsrL */
    private static abstract class upgqDBbsrL<T, R> implements im.getsocial.sdk.internal.p030e.upgqDBbsrL<List<T>, List<R>> {
        private upgqDBbsrL() {
        }

        /* renamed from: a */
        public final List<R> m1385a(List<T> list) {
            List<R> arrayList = new ArrayList(list.size());
            for (T b : list) {
                arrayList.add(mo4411b(b));
            }
            return arrayList;
        }

        /* renamed from: b */
        protected abstract R mo4411b(T t);
    }

    /* renamed from: im.getsocial.sdk.internal.c.k.pdwpUtZXDT$1 */
    class C09501 implements im.getsocial.sdk.internal.p030e.upgqDBbsrL<xAXgtBkRbG, im.getsocial.sdk.usermanagement.p138a.p139a.upgqDBbsrL> {
        /* renamed from: a */
        final /* synthetic */ pdwpUtZXDT f1372a;

        C09501(pdwpUtZXDT pdwputzxdt) {
            this.f1372a = pdwputzxdt;
        }

        /* renamed from: a */
        public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
            return im.getsocial.sdk.usermanagement.p138a.p142d.jjbQypPegg.m3708a((xAXgtBkRbG) obj);
        }
    }

    /* renamed from: im.getsocial.sdk.internal.c.k.pdwpUtZXDT$XdbacJlTDQ */
    private static final class XdbacJlTDQ implements im.getsocial.sdk.internal.p030e.upgqDBbsrL<Map<String, YgeTlQwUNa>, Map<String, PublicUser>> {
        private XdbacJlTDQ() {
        }

        /* renamed from: a */
        public final /* synthetic */ Object mo4344a(Object obj) {
            Map map = (Map) obj;
            Map hashMap = new HashMap(map.size());
            for (Entry entry : map.entrySet()) {
                hashMap.put(entry.getKey(), im.getsocial.sdk.usermanagement.p138a.p142d.jjbQypPegg.m3706a((YgeTlQwUNa) entry.getValue()));
            }
            return hashMap;
        }
    }

    /* renamed from: im.getsocial.sdk.internal.c.k.pdwpUtZXDT$cjrhisSQCL */
    private static final class cjrhisSQCL implements im.getsocial.sdk.internal.p030e.upgqDBbsrL<nGNJgptECj, PrivateUser> {
        private cjrhisSQCL() {
        }

        /* renamed from: a */
        public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
            return im.getsocial.sdk.usermanagement.p138a.p142d.jjbQypPegg.m3705a((nGNJgptECj) obj);
        }
    }

    /* renamed from: im.getsocial.sdk.internal.c.k.pdwpUtZXDT$jjbQypPegg */
    private static final class jjbQypPegg implements im.getsocial.sdk.internal.p030e.upgqDBbsrL<XdbacJlTDQ, ActivityPost> {
        private jjbQypPegg() {
        }

        /* renamed from: a */
        public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
            return im.getsocial.sdk.activities.p028a.p032c.jjbQypPegg.m976a((XdbacJlTDQ) obj);
        }
    }

    /* renamed from: im.getsocial.sdk.internal.c.k.pdwpUtZXDT$pdwpUtZXDT */
    private static final class pdwpUtZXDT implements im.getsocial.sdk.internal.p030e.upgqDBbsrL<YgeTlQwUNa, PublicUser> {
        private pdwpUtZXDT() {
        }

        /* renamed from: a */
        public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
            return im.getsocial.sdk.usermanagement.p138a.p142d.jjbQypPegg.m3706a((YgeTlQwUNa) obj);
        }
    }

    /* renamed from: im.getsocial.sdk.internal.c.k.pdwpUtZXDT$zoToeBNOjF */
    private static final class zoToeBNOjF implements im.getsocial.sdk.internal.p030e.upgqDBbsrL<YgeTlQwUNa, ReferredUser> {
        private zoToeBNOjF() {
        }

        /* renamed from: a */
        public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
            return im.getsocial.sdk.invites.p092a.p101h.jjbQypPegg.m2359a((YgeTlQwUNa) obj);
        }
    }

    /* renamed from: im.getsocial.sdk.internal.c.k.pdwpUtZXDT$ztWNWCuZiM */
    private static final class ztWNWCuZiM implements im.getsocial.sdk.internal.p030e.upgqDBbsrL<CyDeXbQkhA, UserReference> {
        private ztWNWCuZiM() {
        }

        /* renamed from: a */
        public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
            return im.getsocial.sdk.usermanagement.p138a.p142d.jjbQypPegg.m3707a((CyDeXbQkhA) obj);
        }
    }

    @im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ
    pdwpUtZXDT(im.getsocial.sdk.internal.p033c.p060k.p063c.jjbQypPegg jjbqyppegg, im.getsocial.sdk.internal.p033c.p060k.p064d.jjbQypPegg jjbqyppegg2) {
        this.f1465b = jjbqyppegg;
        this.f1466c = jjbqyppegg2;
    }

    /* renamed from: a */
    private static <T, R> upgqDBbsrL<T, R> m1448a(final im.getsocial.sdk.internal.p030e.upgqDBbsrL<T, R> upgqdbbsrl) {
        return new upgqDBbsrL<T, R>() {
            /* renamed from: b */
            protected final R mo4411b(T t) {
                return upgqdbbsrl.mo4344a(t);
            }
        };
    }

    /* renamed from: a */
    private <T, R> im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<R> m1449a(final im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg<T, R> jjbqyppegg) {
        f1464a.mo4388a("Calling %s with params: %s", jjbqyppegg.m1350d(), jjbqyppegg.m1351e());
        return im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT.m1658a(new KSZKMmRWhZ<ztWNWCuZiM<? super im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL>>(this) {
            /* renamed from: a */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1438a;

            {
                this.f1438a = r1;
            }

            /* renamed from: b */
            public final /* synthetic */ void mo4412b(Object obj) {
                im.getsocial.sdk.internal.p030e.p065a.ztWNWCuZiM ztwnwcuzim = (im.getsocial.sdk.internal.p030e.p065a.ztWNWCuZiM) obj;
                try {
                    Object a = this.f1438a.f1465b.mo4405a();
                    if (a != null) {
                        ztwnwcuzim.mo4489a(a);
                    }
                    ztwnwcuzim.mo4488a();
                } catch (Throwable e) {
                    im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT.f1464a.mo4394c("Failed to open socket, error: %s", e.getMessage());
                    ztwnwcuzim.mo4490a(im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(e));
                }
            }
        }).m1669b(jjbqyppegg.m1348b()).m1669b(new im.getsocial.sdk.internal.p030e.upgqDBbsrL<T, T>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1437b;

            /* renamed from: a */
            public final T mo4344a(T t) {
                im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT.f1464a.mo4388a("Call to %s succeed, response: [%s]", jjbqyppegg.m1350d(), t);
                return t;
            }
        }).m1669b(jjbqyppegg.m1349c()).m1670c(new im.getsocial.sdk.internal.p030e.upgqDBbsrL<Throwable, im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<? extends R>>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1435b;

            /* renamed from: a */
            public final /* synthetic */ Object mo4344a(Object obj) {
                Throwable th = (Throwable) obj;
                if (jjbqyppegg.m1347a(th)) {
                    im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT.f1464a.mo4388a("Call to %s failed, error: %s", jjbqyppegg.m1350d(), th);
                } else {
                    im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT.f1464a.mo4394c("Call to %s failed, error: %s", jjbqyppegg.m1350d(), th);
                }
                return im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT.m1660a(th);
            }
        }).m1671d(new im.getsocial.sdk.internal.p033c.p054e.cjrhisSQCL(new im.getsocial.sdk.internal.p033c.p054e.jjbQypPegg(3), 3, 1, TimeUnit.SECONDS));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<List<ReferredUser>> mo4413a() {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("getReferredUsers").m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<List<YgeTlQwUNa>>(this) {
            /* renamed from: a */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1376a;

            {
                this.f1376a = r1;
            }

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4538d(this.f1376a.m1491f());
            }
        }).m1346a(pdwpUtZXDT.m1448a(new zoToeBNOjF())));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<List<PublicUser>> mo4414a(final int i, final int i2) {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("getFriends").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("limit", Integer.valueOf(i2)).m1334b("offset", Integer.valueOf(i))).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<List<YgeTlQwUNa>>(this) {
            /* renamed from: c */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1359c;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4520a(this.f1359c.m1491f(), Integer.valueOf(i), Integer.valueOf(i2));
            }
        }).m1346a(pdwpUtZXDT.m1448a(new pdwpUtZXDT())));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<List<String>> mo4415a(TagsQuery tagsQuery) {
        final ZWjsSaCmFq a = im.getsocial.sdk.activities.p028a.p032c.jjbQypPegg.m978a(tagsQuery);
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("findTags").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a(SearchIntents.EXTRA_QUERY, a)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<List<String>>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1413b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4519a(this.f1413b.m1491f(), a);
            }
        }));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Void> mo4416a(QhisXzMgay qhisXzMgay) {
        final icjTFWWVFN a = im.getsocial.sdk.usermanagement.p138a.p142d.jjbQypPegg.m3702a(qhisXzMgay);
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("setUserLanguage").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("superProperties", a)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Boolean>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1378b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4511a(this.f1378b.m1491f(), a);
            }
        }).m1342a());
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Void> mo4417a(QhisXzMgay qhisXzMgay, List<im.getsocial.sdk.internal.p036a.p038b.jjbQypPegg> list) {
        final icjTFWWVFN a = im.getsocial.sdk.usermanagement.p138a.p142d.jjbQypPegg.m3702a(qhisXzMgay);
        final List a2 = new upgqDBbsrL<im.getsocial.sdk.internal.p036a.p038b.jjbQypPegg, im.getsocial.sdk.internal.p070f.p071a.ztWNWCuZiM>(this) {
            /* renamed from: a */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1414a;

            {
                this.f1414a = r2;
            }

            /* renamed from: b */
            protected final /* synthetic */ Object mo4411b(Object obj) {
                return im.getsocial.sdk.internal.p036a.p043f.jjbQypPegg.m1042a((im.getsocial.sdk.internal.p036a.p038b.jjbQypPegg) obj);
            }
        }.m1385a((List) list);
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("trackAnalyticsEvents").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("superProperties", a).m1334b("events", a2)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Boolean>(this) {
            /* renamed from: c */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1417c;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4512a(this.f1417c.m1491f(), a, a2);
            }
        }).m1342a());
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<ReferralData> mo4418a(fOrCGNYyfk forcgnyyfk, boolean z, Map<im.getsocial.sdk.invites.p092a.p093a.pdwpUtZXDT, Map<String, String>> map, Map<String, String> map2) {
        final CJZnJxRuoc cJZnJxRuoc = new CJZnJxRuoc();
        cJZnJxRuoc.f1568a = im.getsocial.sdk.invites.p092a.p101h.jjbQypPegg.m2357a(forcgnyyfk);
        cJZnJxRuoc.f1569b = Boolean.valueOf(z);
        cJZnJxRuoc.f1570c = im.getsocial.sdk.invites.p092a.p101h.jjbQypPegg.m2364a((Map) map);
        cJZnJxRuoc.f1571d = map2;
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("processAppOpen").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("appOpenRequest", cJZnJxRuoc)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<JQrJMKopAa>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1375b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4501a(this.f1375b.m1491f(), cJZnJxRuoc);
            }
        }).m1346a(new im.getsocial.sdk.internal.p030e.upgqDBbsrL<JQrJMKopAa, ReferralData>(this) {
            /* renamed from: a */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1373a;

            {
                this.f1373a = r1;
            }

            /* renamed from: a */
            public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
                return im.getsocial.sdk.invites.p092a.p101h.jjbQypPegg.m2358a((JQrJMKopAa) obj);
            }
        }).m1345a(new im.getsocial.sdk.internal.p030e.XdbacJlTDQ<im.getsocial.sdk.internal.p033c.p048a.p049a.jjbQypPegg>() {
            /* renamed from: b */
            protected final /* synthetic */ boolean mo4404b(Object obj) {
                return ((im.getsocial.sdk.internal.p033c.p048a.p049a.jjbQypPegg) obj).m1135a(102);
            }
        }));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Integer> mo4419a(NotificationsCountQuery notificationsCountQuery) {
        final DvmrLquonW a = im.getsocial.sdk.pushnotifications.p067a.p107f.jjbQypPegg.m2449a(notificationsCountQuery);
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("getNotificationsCount").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a(SearchIntents.EXTRA_QUERY, a)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Integer>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1426b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4526b(this.f1426b.m1491f(), a);
            }
        }));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<List<Notification>> mo4420a(NotificationsQuery notificationsQuery) {
        final DvmrLquonW a = im.getsocial.sdk.pushnotifications.p067a.p107f.jjbQypPegg.m2450a(notificationsQuery);
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("getNotificationsList").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a(SearchIntents.EXTRA_QUERY, a)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<List<JWvbLzaedN>>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1424b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4517a(this.f1424b.m1491f(), a);
            }
        }).m1346a(pdwpUtZXDT.m1448a(new im.getsocial.sdk.internal.p030e.upgqDBbsrL<JWvbLzaedN, Notification>(this) {
            /* renamed from: a */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1422a;

            {
                this.f1422a = r1;
            }

            /* renamed from: a */
            public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
                return im.getsocial.sdk.pushnotifications.p067a.p107f.jjbQypPegg.m2452a((JWvbLzaedN) obj);
            }
        })));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<PrivateUser> mo4421a(AuthIdentity authIdentity) {
        final rWfbqYooCV a = im.getsocial.sdk.usermanagement.p138a.p142d.jjbQypPegg.m3704a(authIdentity);
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("addIdentity").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("identity", a)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<nGNJgptECj>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1432b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4507a(this.f1432b.m1491f(), a);
            }
        }).m1346a(new cjrhisSQCL()));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<List<UserReference>> mo4422a(UsersQuery usersQuery) {
        final VuXsWfriFX a = im.getsocial.sdk.usermanagement.p138a.p142d.jjbQypPegg.m3700a(usersQuery);
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("findUsers").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a(SearchIntents.EXTRA_QUERY, a)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<List<CyDeXbQkhA>>(this) {
            /* renamed from: b */
            final /* synthetic */ pdwpUtZXDT f1396b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4518a(this.f1396b.m1491f(), a);
            }
        }).m1346a(pdwpUtZXDT.m1448a(new ztWNWCuZiM())));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<im.getsocial.sdk.usermanagement.p138a.p139a.upgqDBbsrL> mo4423a(im.getsocial.sdk.usermanagement.p138a.p139a.jjbQypPegg jjbqyppegg) {
        final KdkQzTlDzz a = im.getsocial.sdk.usermanagement.p138a.p142d.jjbQypPegg.m3699a(jjbqyppegg);
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("authenticateSdk").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a(ShareConstants.WEB_DIALOG_RESULT_PARAM_REQUEST_ID, a)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<xAXgtBkRbG>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1361b;

            /* renamed from: a */
            protected final /* bridge */ /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4508a(a);
            }
        }).m1346a(new C09501(this)));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<PrivateUser> mo4424a(im.getsocial.sdk.usermanagement.p138a.p139a.pdwpUtZXDT pdwputzxdt) {
        final nGNJgptECj a = im.getsocial.sdk.usermanagement.p138a.p142d.jjbQypPegg.m3703a(pdwputzxdt);
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("updateUser").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("privateUser", a)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<nGNJgptECj>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1409b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4506a(this.f1409b.m1491f(), a);
            }
        }).m1346a(new cjrhisSQCL()));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<PrivateUser> mo4425a(String str) {
        final rWfbqYooCV rwfbqyoocv = new rWfbqYooCV();
        rwfbqyoocv.f1835a = str;
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("removeIdentity").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("identity", rwfbqyoocv)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<nGNJgptECj>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1444b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4525b(this.f1444b.m1491f(), rwfbqyoocv);
            }
        }).m1346a(new cjrhisSQCL()));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<List<PublicUser>> mo4426a(final String str, final int i, final int i2) {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("getActivityLikers").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("activityId", str).m1334b("limit", Integer.valueOf(i2)).m1334b("offset", Integer.valueOf(i))).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<List<ofLJAxfaCe>>(this) {
            /* renamed from: d */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1404d;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4522a(this.f1404d.m1491f(), str, Integer.valueOf(i), Integer.valueOf(i2));
            }
        }).m1346a(pdwpUtZXDT.m1448a(new im.getsocial.sdk.internal.p030e.upgqDBbsrL<ofLJAxfaCe, PublicUser>(this) {
            /* renamed from: a */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1400a;

            {
                this.f1400a = r1;
            }

            /* renamed from: a */
            public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
                return im.getsocial.sdk.activities.p028a.p032c.jjbQypPegg.m977a((ofLJAxfaCe) obj);
            }
        })));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<List<ActivityPost>> mo4427a(final String str, ActivitiesQuery activitiesQuery) {
        final im.getsocial.sdk.internal.p070f.p071a.pdwpUtZXDT a = im.getsocial.sdk.activities.p028a.p032c.jjbQypPegg.m980a(activitiesQuery);
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("getActivities").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("feed", str).m1334b(SearchIntents.EXTRA_QUERY, a)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<List<XdbacJlTDQ>>(this) {
            /* renamed from: c */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1383c;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4521a(this.f1383c.m1491f(), str, a);
            }
        }).m1346a(pdwpUtZXDT.m1448a(new jjbQypPegg())));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Void> mo4428a(final String str, ReportingReason reportingReason) {
        final iqXBPEYHZB a = im.getsocial.sdk.activities.p028a.p032c.jjbQypPegg.m979a(reportingReason);
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("reportActivity").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("activityId", str).m1334b("reportingReason", reportingReason)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Boolean>(this) {
            /* renamed from: c */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1407c;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4514a(this.f1407c.m1491f(), str, a);
            }
        }).m1342a());
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<ActivityPost> mo4429a(final String str, im.getsocial.sdk.activities.p028a.p029a.jjbQypPegg jjbqyppegg) {
        final zoToeBNOjF a = im.getsocial.sdk.activities.p028a.p032c.jjbQypPegg.m981a(jjbqyppegg);
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("postActivity").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("feed", str).m1334b(Param.CONTENT, a)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<XdbacJlTDQ>(this) {
            /* renamed from: c */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1391c;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4503a(this.f1391c.m1491f(), str, a);
            }
        }).m1346a(new jjbQypPegg()));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<im.getsocial.sdk.invites.p092a.p094b.XdbacJlTDQ> mo4430a(String str, LinkParams linkParams) {
        final EmkjBpiUfq emkjBpiUfq = new EmkjBpiUfq();
        emkjBpiUfq.f1582a = str;
        if (linkParams != null) {
            emkjBpiUfq.f1583b = linkParams.getStringValues();
        }
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("createInviteUrl").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a(ShareConstants.WEB_DIALOG_RESULT_PARAM_REQUEST_ID, emkjBpiUfq)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<QCXFOjcJkE>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1371b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4502a(this.f1371b.m1491f(), emkjBpiUfq);
            }
        }).m1346a(new im.getsocial.sdk.internal.p030e.upgqDBbsrL<QCXFOjcJkE, im.getsocial.sdk.invites.p092a.p094b.XdbacJlTDQ>(this) {
            /* renamed from: a */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1369a;

            {
                this.f1369a = r1;
            }

            /* renamed from: a */
            public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
                return im.getsocial.sdk.invites.p092a.p101h.jjbQypPegg.m2360a((QCXFOjcJkE) obj);
            }
        }));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Void> mo4431a(String str, String str2, IbawHMWljm ibawHMWljm, Boolean bool) {
        final UwIeQkAzJH uwIeQkAzJH = new UwIeQkAzJH();
        uwIeQkAzJH.f1640a = str;
        uwIeQkAzJH.f1641b = str2;
        uwIeQkAzJH.f1642c = im.getsocial.sdk.usermanagement.p138a.p142d.jjbQypPegg.m3701a(ibawHMWljm);
        uwIeQkAzJH.f1643d = bool;
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("registerPushTarget").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("target", uwIeQkAzJH)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Boolean>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1419b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4510a(this.f1419b.m1491f(), uwIeQkAzJH);
            }
        }).m1342a());
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Map<String, PublicUser>> mo4432a(final String str, final List<String> list) {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("getPublicUsersByIdentities").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("providerId", str).m1334b("providerUserIds", list)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Map<String, YgeTlQwUNa>>(this) {
            /* renamed from: c */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1451c;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4523a(this.f1451c.m1491f(), str, list);
            }
        }).m1346a(new XdbacJlTDQ()));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<ActivityPost> mo4433a(final String str, final boolean z) {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("likeActivity").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("activityId", str).m1334b("isLiked", Boolean.valueOf(z))).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<XdbacJlTDQ>(this) {
            /* renamed from: c */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1399c;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4504a(this.f1399c.m1491f(), str, Boolean.valueOf(z));
            }
        }).m1346a(new jjbQypPegg()));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Integer> mo4434a(final List<String> list) {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("setFriends").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("userIds", list)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Integer>(this) {
            /* renamed from: b */
            final /* synthetic */ pdwpUtZXDT f1458b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4516a(this.f1458b.m1491f(), list);
            }
        }));
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Void> mo4435a(List<String> list, boolean z) {
        final DvynvDnqtx dvynvDnqtx = new DvynvDnqtx();
        dvynvDnqtx.f1581b = Boolean.valueOf(z);
        dvynvDnqtx.f1580a = list;
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("setNotificationsRead").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("notificationIds", list).m1334b("read", Boolean.valueOf(z))).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Boolean>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1428b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4509a(this.f1428b.m1491f(), dvynvDnqtx);
            }
        }).m1342a());
    }

    /* renamed from: a */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Void> mo4436a(final boolean z) {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("setPushNotificationsEnabled").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("isEnabled", Boolean.valueOf(z))).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Boolean>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1430b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4513a(this.f1430b.m1491f(), Boolean.valueOf(z));
            }
        }).m1342a());
    }

    /* renamed from: b */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Integer> mo4437b() {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("getFriendsCount").m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Integer>(this) {
            /* renamed from: a */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1356a;

            {
                this.f1356a = r1;
            }

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4515a(this.f1356a.m1491f());
            }
        }));
    }

    /* renamed from: b */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<List<SuggestedFriend>> mo4438b(final int i, final int i2) {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("getSuggestedFriends").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("limit", Integer.valueOf(i2)).m1334b("offset", Integer.valueOf(i))).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<List<BpPZzHFMaU>>(this) {
            /* renamed from: c */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1365c;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT.f1464a.mo4388a("Calling %s with params: [offset: %d, limit: %d]", "getSuggestedFriends", Integer.valueOf(i), Integer.valueOf(i2));
                return upgqdbbsrl.mo4530b(this.f1365c.m1491f(), Integer.valueOf(i), Integer.valueOf(i2));
            }
        }).m1346a(new upgqDBbsrL<BpPZzHFMaU, SuggestedFriend>(this) {
            /* renamed from: a */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1362a;

            {
                this.f1362a = r2;
            }

            /* renamed from: b */
            protected final /* synthetic */ Object mo4411b(Object obj) {
                return im.getsocial.sdk.socialgraph.p109a.p111b.jjbQypPegg.m2486a((BpPZzHFMaU) obj);
            }
        }));
    }

    /* renamed from: b */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<PrivateUser> mo4439b(AuthIdentity authIdentity) {
        final rWfbqYooCV a = im.getsocial.sdk.usermanagement.p138a.p142d.jjbQypPegg.m3704a(authIdentity);
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("getPrivateUserByIdentity").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("identity", a)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<nGNJgptECj>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1446b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4533c(this.f1446b.m1491f(), a);
            }
        }).m1346a(new cjrhisSQCL()));
    }

    /* renamed from: b */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<PublicUser> mo4440b(final String str) {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("getPublicUser").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a(AmazonAppstoreBillingService.JSON_KEY_USER_ID, str)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<YgeTlQwUNa>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1448b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4505a(this.f1448b.m1491f(), str);
            }
        }).m1346a(new pdwpUtZXDT()));
    }

    /* renamed from: b */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<List<ActivityPost>> mo4441b(final String str, ActivitiesQuery activitiesQuery) {
        final im.getsocial.sdk.internal.p070f.p071a.pdwpUtZXDT a = im.getsocial.sdk.activities.p028a.p032c.jjbQypPegg.m980a(activitiesQuery);
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("getComments").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("activityId", str).m1334b(SearchIntents.EXTRA_QUERY, a)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<List<XdbacJlTDQ>>(this) {
            /* renamed from: c */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1386c;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4531b(this.f1386c.m1491f(), str, a);
            }
        }).m1346a(pdwpUtZXDT.m1448a(new jjbQypPegg())));
    }

    /* renamed from: b */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<ActivityPost> mo4442b(final String str, im.getsocial.sdk.activities.p028a.p029a.jjbQypPegg jjbqyppegg) {
        final zoToeBNOjF a = im.getsocial.sdk.activities.p028a.p032c.jjbQypPegg.m981a(jjbqyppegg);
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("postComment").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("activityId", str).m1334b(Param.CONTENT, a)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<XdbacJlTDQ>(this) {
            /* renamed from: c */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1394c;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4524b(this.f1394c.m1491f(), str, a);
            }
        }).m1346a(new jjbQypPegg()));
    }

    /* renamed from: b */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Integer> mo4443b(final String str, final List<String> list) {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("addFriendByIdentities").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("providerId", str).m1334b("providerUserIds", list)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Integer>(this) {
            /* renamed from: c */
            final /* synthetic */ pdwpUtZXDT f1441c;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4528b(this.f1441c.m1491f(), str, list);
            }
        }));
    }

    /* renamed from: c */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<List<UserReference>> mo4444c() {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("getMentionFriends").m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<List<CyDeXbQkhA>>(this) {
            /* renamed from: a */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1366a;

            {
                this.f1366a = r1;
            }

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4529b(this.f1366a.m1491f());
            }
        }).m1346a(pdwpUtZXDT.m1448a(new ztWNWCuZiM())));
    }

    /* renamed from: c */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Integer> mo4445c(final String str) {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("addFriend").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a(AmazonAppstoreBillingService.JSON_KEY_USER_ID, str)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Integer>(this) {
            /* renamed from: b */
            final /* synthetic */ pdwpUtZXDT f1421b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4527b(this.f1421b.m1491f(), str);
            }
        }));
    }

    /* renamed from: c */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Integer> mo4446c(final String str, final List<String> list) {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("removeFriendByIdentities").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("providerId", str).m1334b("providerUserIds", list)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Integer>(this) {
            /* renamed from: c */
            final /* synthetic */ pdwpUtZXDT f1456c;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4535c(this.f1456c.m1491f(), str, list);
            }
        }));
    }

    /* renamed from: d */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<im.getsocial.sdk.invites.p092a.p094b.upgqDBbsrL> mo4447d() {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("getInviteProviders").m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<JbBdMtJmlU>(this) {
            /* renamed from: a */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1368a;

            {
                this.f1368a = r1;
            }

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4532c(this.f1368a.m1491f());
            }
        }).m1346a(new im.getsocial.sdk.internal.p030e.upgqDBbsrL<JbBdMtJmlU, im.getsocial.sdk.invites.p092a.p094b.upgqDBbsrL>(this) {
            /* renamed from: a */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1367a;

            {
                this.f1367a = r1;
            }

            /* renamed from: a */
            public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
                return im.getsocial.sdk.invites.p092a.p101h.jjbQypPegg.m2362a((JbBdMtJmlU) obj);
            }
        }));
    }

    /* renamed from: d */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Integer> mo4448d(final String str) {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("removeFriend").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a(AmazonAppstoreBillingService.JSON_KEY_USER_ID, str)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Integer>(this) {
            /* renamed from: b */
            final /* synthetic */ pdwpUtZXDT f1453b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4534c(this.f1453b.m1491f(), str);
            }
        }));
    }

    /* renamed from: d */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Integer> mo4449d(final String str, final List<String> list) {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("setFriendsByIdentity").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("providerId", str).m1334b("providerUserIds", list)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Integer>(this) {
            /* renamed from: c */
            final /* synthetic */ pdwpUtZXDT f1461c;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4537d(this.f1461c.m1491f(), str, list);
            }
        }));
    }

    /* renamed from: e */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Boolean> mo4450e() {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("isPushNotificationsEnabled").m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Boolean>(this) {
            /* renamed from: a */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1433a;

            {
                this.f1433a = r1;
            }

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4539e(this.f1433a.m1491f());
            }
        }));
    }

    /* renamed from: e */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Boolean> mo4451e(final String str) {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("isFriend").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a(AmazonAppstoreBillingService.JSON_KEY_USER_ID, str)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Boolean>(this) {
            /* renamed from: b */
            final /* synthetic */ pdwpUtZXDT f1463b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4536d(this.f1463b.m1491f(), str);
            }
        }));
    }

    /* renamed from: f */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<List<ActivityPost>> mo4452f(final String str) {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("getStickyActivities").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("feed", str)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<List<XdbacJlTDQ>>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1380b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4540e(this.f1380b.m1491f(), str);
            }
        }).m1346a(pdwpUtZXDT.m1448a(new jjbQypPegg())));
    }

    /* renamed from: f */
    protected final String m1491f() {
        im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL b = this.f1466c.m1378b();
        return b == null ? null : b.m1323c();
    }

    /* renamed from: g */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<ActivityPost> mo4453g(final String str) {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("getActivity").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("activityId", str)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<XdbacJlTDQ>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1388b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4541f(this.f1388b.m1491f(), str);
            }
        }).m1346a(new jjbQypPegg()));
    }

    /* renamed from: h */
    public final im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT<Void> mo4454h(final String str) {
        return m1449a(im.getsocial.sdk.internal.p033c.p060k.p061a.jjbQypPegg.m1341a("deleteActivity").m1343a(im.getsocial.sdk.internal.p033c.p060k.p061a.cjrhisSQCL.m1333a("activityId", str)).m1344a(new im.getsocial.sdk.internal.p033c.p060k.p061a.upgqDBbsrL<Boolean>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p033c.p060k.pdwpUtZXDT f1411b;

            /* renamed from: a */
            protected final /* synthetic */ Object mo4410a(im.getsocial.sdk.internal.p070f.p071a.upgqDBbsrL upgqdbbsrl) {
                return upgqdbbsrl.mo4542g(this.f1411b.m1491f(), str);
            }
        }).m1342a());
    }
}
