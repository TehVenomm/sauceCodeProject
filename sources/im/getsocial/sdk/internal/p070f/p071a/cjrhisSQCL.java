package im.getsocial.sdk.internal.p070f.p071a;

import com.facebook.share.internal.ShareConstants;
import com.google.android.gms.actions.SearchIntents;
import im.getsocial.p018b.p021c.p022a.XdbacJlTDQ;
import im.getsocial.p018b.p021c.p022a.pdwpUtZXDT;
import im.getsocial.p018b.p021c.p022a.zoToeBNOjF;
import im.getsocial.p018b.p021c.p023b.jjbQypPegg;
import im.getsocial.p018b.p021c.p023b.upgqDBbsrL;
import im.getsocial.p018b.p021c.ztWNWCuZiM;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL */
public class cjrhisSQCL extends jjbQypPegg implements upgqDBbsrL {

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$CJZnJxRuoc */
    private static final class CJZnJxRuoc extends upgqDBbsrL<Integer> {
        /* renamed from: c */
        private final String f1673c;
        /* renamed from: d */
        private final String f1674d;
        /* renamed from: e */
        private final List<String> f1675e;

        CJZnJxRuoc(String str, String str2, List<String> list) {
            super("setFriendsByIdentity", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1673c = str;
            if (str2 == null) {
                throw new NullPointerException("provider");
            }
            this.f1674d = str2;
            if (list == null) {
                throw new NullPointerException("providerIds");
            }
            this.f1675e = list;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 8) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = Integer.valueOf(zotoebnojf.mo4333j());
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1673c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1674d);
            zotoebnojf.mo4320a(3, (byte) 15);
            zotoebnojf.mo4318a((byte) 11, this.f1675e.size());
            for (String a : this.f1675e) {
                zotoebnojf.mo4322a(a);
            }
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$DvmrLquonW */
    private static final class DvmrLquonW extends upgqDBbsrL<Integer> {
        /* renamed from: c */
        private final String f1676c;
        /* renamed from: d */
        private final String f1677d;

        DvmrLquonW(String str, String str2) {
            super("removeFriend", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1676c = str;
            if (str2 == null) {
                throw new NullPointerException(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
            }
            this.f1677d = str2;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 8) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = Integer.valueOf(zotoebnojf.mo4333j());
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1676c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1677d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$DvynvDnqtx */
    private static final class DvynvDnqtx extends upgqDBbsrL<Integer> {
        /* renamed from: c */
        private final String f1678c;
        /* renamed from: d */
        private final String f1679d;
        /* renamed from: e */
        private final List<String> f1680e;

        DvynvDnqtx(String str, String str2, List<String> list) {
            super("removeFriendsByIdentity", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1678c = str;
            if (str2 == null) {
                throw new NullPointerException("provider");
            }
            this.f1679d = str2;
            if (list == null) {
                throw new NullPointerException("providerIds");
            }
            this.f1680e = list;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 8) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = Integer.valueOf(zotoebnojf.mo4333j());
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1678c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1679d);
            zotoebnojf.mo4320a(3, (byte) 15);
            zotoebnojf.mo4318a((byte) 11, this.f1680e.size());
            for (String a : this.f1680e) {
                zotoebnojf.mo4322a(a);
            }
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$EmkjBpiUfq */
    private static final class EmkjBpiUfq extends upgqDBbsrL<List<CyDeXbQkhA>> {
        /* renamed from: c */
        private final String f1681c;

        EmkjBpiUfq(String str) {
            super("getMentionFriends", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1681c = str;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            Object obj = null;
            KkSvQPDhNi kkSvQPDhNi = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 15) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            im.getsocial.p018b.p021c.p022a.cjrhisSQCL e = zotoebnojf.mo4328e();
                            obj = new ArrayList(e.f1045b);
                            for (int i = 0; i < e.f1045b; i++) {
                                obj.add(CyDeXbQkhA.m1684a(zotoebnojf));
                            }
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1681c);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$HptYHntaqF */
    private static final class HptYHntaqF extends upgqDBbsrL<List<CyDeXbQkhA>> {
        /* renamed from: c */
        private final String f1682c;
        /* renamed from: d */
        private final VuXsWfriFX f1683d;

        HptYHntaqF(String str, VuXsWfriFX vuXsWfriFX) {
            super("findUsers", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1682c = str;
            if (vuXsWfriFX == null) {
                throw new NullPointerException(SearchIntents.EXTRA_QUERY);
            }
            this.f1683d = vuXsWfriFX;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            Object obj = null;
            KkSvQPDhNi kkSvQPDhNi = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 15) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            im.getsocial.p018b.p021c.p022a.cjrhisSQCL e = zotoebnojf.mo4328e();
                            obj = new ArrayList(e.f1045b);
                            for (int i = 0; i < e.f1045b; i++) {
                                obj.add(CyDeXbQkhA.m1684a(zotoebnojf));
                            }
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1682c);
            zotoebnojf.mo4320a(2, (byte) 12);
            VuXsWfriFX.m1700a(zotoebnojf, this.f1683d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$IbawHMWljm */
    private static final class IbawHMWljm extends upgqDBbsrL<List<YgeTlQwUNa>> {
        /* renamed from: c */
        private final String f1684c;

        IbawHMWljm(String str) {
            super("getReferredUsers", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1684c = str;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            Object obj = null;
            KkSvQPDhNi kkSvQPDhNi = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 15) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            im.getsocial.p018b.p021c.p022a.cjrhisSQCL e = zotoebnojf.mo4328e();
                            obj = new ArrayList(e.f1045b);
                            for (int i = 0; i < e.f1045b; i++) {
                                obj.add(YgeTlQwUNa.m1702a(zotoebnojf));
                            }
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1684c);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$JWvbLzaedN */
    private static final class JWvbLzaedN extends upgqDBbsrL<Boolean> {
        /* renamed from: c */
        private final String f1685c;
        /* renamed from: d */
        private final UwIeQkAzJH f1686d;

        JWvbLzaedN(String str, UwIeQkAzJH uwIeQkAzJH) {
            super("registerPushTarget", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1685c = str;
            if (uwIeQkAzJH == null) {
                throw new NullPointerException("pushTargetData");
            }
            this.f1686d = uwIeQkAzJH;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 2) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = Boolean.valueOf(zotoebnojf.mo4330g());
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1685c);
            zotoebnojf.mo4320a(2, (byte) 12);
            UwIeQkAzJH.m1699a(zotoebnojf, this.f1686d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$JbBdMtJmlU */
    private static final class JbBdMtJmlU extends upgqDBbsrL<XdbacJlTDQ> {
        /* renamed from: c */
        private final String f1687c;
        /* renamed from: d */
        private final String f1688d;
        /* renamed from: e */
        private final Boolean f1689e;

        JbBdMtJmlU(String str, String str2, Boolean bool) {
            super("likeActivity", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1687c = str;
            if (str2 == null) {
                throw new NullPointerException("activityId");
            }
            this.f1688d = str2;
            if (bool == null) {
                throw new NullPointerException("isLiked");
            }
            this.f1689e = bool;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = XdbacJlTDQ.m1701a(zotoebnojf);
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1687c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1688d);
            zotoebnojf.mo4320a(3, (byte) 2);
            zotoebnojf.mo4324a(this.f1689e.booleanValue());
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$KCGqEGAizh */
    private static final class KCGqEGAizh extends upgqDBbsrL<Boolean> {
        /* renamed from: c */
        private final String f1690c;
        /* renamed from: d */
        private final String f1691d;

        KCGqEGAizh(String str, String str2) {
            super("isFriend", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1690c = str;
            if (str2 == null) {
                throw new NullPointerException(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
            }
            this.f1691d = str2;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 2) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = Boolean.valueOf(zotoebnojf.mo4330g());
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1690c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1691d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$KSZKMmRWhZ */
    private static final class KSZKMmRWhZ extends upgqDBbsrL<List<String>> {
        /* renamed from: c */
        private final String f1692c;
        /* renamed from: d */
        private final ZWjsSaCmFq f1693d;

        KSZKMmRWhZ(String str, ZWjsSaCmFq zWjsSaCmFq) {
            super("findTags", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1692c = str;
            if (zWjsSaCmFq == null) {
                throw new NullPointerException(SearchIntents.EXTRA_QUERY);
            }
            this.f1693d = zWjsSaCmFq;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            Object obj = null;
            KkSvQPDhNi kkSvQPDhNi = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 15) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            im.getsocial.p018b.p021c.p022a.cjrhisSQCL e = zotoebnojf.mo4328e();
                            obj = new ArrayList(e.f1045b);
                            for (int i = 0; i < e.f1045b; i++) {
                                obj.add(zotoebnojf.mo4336m());
                            }
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1692c);
            zotoebnojf.mo4320a(2, (byte) 12);
            ZWjsSaCmFq.m1703a(zotoebnojf, this.f1693d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$KdkQzTlDzz */
    private static final class KdkQzTlDzz extends upgqDBbsrL<nGNJgptECj> {
        /* renamed from: c */
        private final String f1694c;
        /* renamed from: d */
        private final nGNJgptECj f1695d;

        KdkQzTlDzz(String str, nGNJgptECj ngnjgptecj) {
            super("updateUser", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1694c = str;
            if (ngnjgptecj == null) {
                throw new NullPointerException(ShareConstants.WEB_DIALOG_RESULT_PARAM_REQUEST_ID);
            }
            this.f1695d = ngnjgptecj;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = nGNJgptECj.m1877a(zotoebnojf);
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1694c);
            zotoebnojf.mo4320a(2, (byte) 12);
            nGNJgptECj.m1878a(zotoebnojf, this.f1695d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$KkSvQPDhNi */
    private static final class KkSvQPDhNi extends upgqDBbsrL<Map<String, YgeTlQwUNa>> {
        /* renamed from: c */
        private final String f1696c;
        /* renamed from: d */
        private final String f1697d;
        /* renamed from: e */
        private final List<String> f1698e;

        KkSvQPDhNi(String str, String str2, List<String> list) {
            super("getPublicUsersByIdentity", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1696c = str;
            if (str2 == null) {
                throw new NullPointerException("provider");
            }
            this.f1697d = str2;
            if (list == null) {
                throw new NullPointerException("providerIds");
            }
            this.f1698e = list;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            Object obj = null;
            KkSvQPDhNi kkSvQPDhNi = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 13) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            pdwpUtZXDT d = zotoebnojf.mo4327d();
                            obj = new HashMap(d.f1053c);
                            for (int i = 0; i < d.f1053c; i++) {
                                obj.put(zotoebnojf.mo4336m(), YgeTlQwUNa.m1702a(zotoebnojf));
                            }
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1696c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1697d);
            zotoebnojf.mo4320a(3, (byte) 15);
            zotoebnojf.mo4318a((byte) 11, this.f1698e.size());
            for (String a : this.f1698e) {
                zotoebnojf.mo4322a(a);
            }
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$KluUZYuxme */
    private static final class KluUZYuxme extends upgqDBbsrL<XdbacJlTDQ> {
        /* renamed from: c */
        private final String f1699c;
        /* renamed from: d */
        private final String f1700d;

        KluUZYuxme(String str, String str2) {
            super("getActivity", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1699c = str;
            if (str2 == null) {
                throw new NullPointerException("activityId");
            }
            this.f1700d = str2;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = XdbacJlTDQ.m1701a(zotoebnojf);
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1699c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1700d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$QCXFOjcJkE */
    private static final class QCXFOjcJkE extends upgqDBbsrL<Integer> {
        /* renamed from: c */
        private final String f1701c;
        /* renamed from: d */
        private final DvmrLquonW f1702d;

        QCXFOjcJkE(String str, DvmrLquonW dvmrLquonW) {
            super("getNotificationsCount", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1701c = str;
            if (dvmrLquonW == null) {
                throw new NullPointerException(SearchIntents.EXTRA_QUERY);
            }
            this.f1702d = dvmrLquonW;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 8) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = Integer.valueOf(zotoebnojf.mo4333j());
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1701c);
            zotoebnojf.mo4320a(2, (byte) 12);
            DvmrLquonW.m1685a(zotoebnojf, this.f1702d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$QWVUXapsSm */
    private static final class QWVUXapsSm extends upgqDBbsrL<List<BpPZzHFMaU>> {
        /* renamed from: c */
        private final String f1703c;
        /* renamed from: d */
        private final Integer f1704d;
        /* renamed from: e */
        private final Integer f1705e;

        QWVUXapsSm(String str, Integer num, Integer num2) {
            super("getSuggestedFriends", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1703c = str;
            if (num == null) {
                throw new NullPointerException("offset");
            }
            this.f1704d = num;
            if (num2 == null) {
                throw new NullPointerException("limit");
            }
            this.f1705e = num2;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            Object obj = null;
            KkSvQPDhNi kkSvQPDhNi = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 15) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            im.getsocial.p018b.p021c.p022a.cjrhisSQCL e = zotoebnojf.mo4328e();
                            obj = new ArrayList(e.f1045b);
                            for (int i = 0; i < e.f1045b; i++) {
                                obj.add(BpPZzHFMaU.m1682a(zotoebnojf));
                            }
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1703c);
            zotoebnojf.mo4320a(2, (byte) 8);
            zotoebnojf.mo4319a(this.f1704d.intValue());
            zotoebnojf.mo4320a(3, (byte) 8);
            zotoebnojf.mo4319a(this.f1705e.intValue());
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$QhisXzMgay */
    private static final class QhisXzMgay extends upgqDBbsrL<XdbacJlTDQ> {
        /* renamed from: c */
        private final String f1706c;
        /* renamed from: d */
        private final String f1707d;
        /* renamed from: e */
        private final zoToeBNOjF f1708e;

        QhisXzMgay(String str, String str2, zoToeBNOjF zotoebnojf) {
            super("postActivity", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1706c = str;
            if (str2 == null) {
                throw new NullPointerException("feed");
            }
            this.f1707d = str2;
            if (zotoebnojf == null) {
                throw new NullPointerException("activityPostContent");
            }
            this.f1708e = zotoebnojf;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = XdbacJlTDQ.m1701a(zotoebnojf);
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1706c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1707d);
            zotoebnojf.mo4320a(3, (byte) 12);
            zoToeBNOjF.m1888a(zotoebnojf, this.f1708e);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$SKUqohGtGQ */
    private static final class SKUqohGtGQ extends upgqDBbsrL<JQrJMKopAa> {
        /* renamed from: c */
        private final String f1709c;
        /* renamed from: d */
        private final CJZnJxRuoc f1710d;

        SKUqohGtGQ(String str, CJZnJxRuoc cJZnJxRuoc) {
            super("processAppOpen", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1709c = str;
            if (cJZnJxRuoc == null) {
                throw new NullPointerException(ShareConstants.WEB_DIALOG_RESULT_PARAM_REQUEST_ID);
            }
            this.f1710d = cJZnJxRuoc;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = JQrJMKopAa.m1689a(zotoebnojf);
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1709c);
            zotoebnojf.mo4320a(2, (byte) 12);
            CJZnJxRuoc.m1683a(zotoebnojf, this.f1710d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$UwIeQkAzJH */
    private static final class UwIeQkAzJH extends upgqDBbsrL<Boolean> {
        /* renamed from: c */
        private final String f1711c;
        /* renamed from: d */
        private final DvynvDnqtx f1712d;

        UwIeQkAzJH(String str, DvynvDnqtx dvynvDnqtx) {
            super("setNotificationsStatus", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1711c = str;
            if (dvynvDnqtx == null) {
                throw new NullPointerException(SearchIntents.EXTRA_QUERY);
            }
            this.f1712d = dvynvDnqtx;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 2) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = Boolean.valueOf(zotoebnojf.mo4330g());
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1711c);
            zotoebnojf.mo4320a(2, (byte) 12);
            DvynvDnqtx.m1686a(zotoebnojf, this.f1712d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$XdbacJlTDQ */
    private static final class XdbacJlTDQ extends upgqDBbsrL<QCXFOjcJkE> {
        /* renamed from: c */
        private final String f1713c;
        /* renamed from: d */
        private final EmkjBpiUfq f1714d;

        XdbacJlTDQ(String str, EmkjBpiUfq emkjBpiUfq) {
            super("createInviteUrl", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1713c = str;
            if (emkjBpiUfq == null) {
                throw new NullPointerException(ShareConstants.WEB_DIALOG_RESULT_PARAM_REQUEST_ID);
            }
            this.f1714d = emkjBpiUfq;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, im.getsocial.p018b.p021c.p022a.XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = QCXFOjcJkE.m1696a(zotoebnojf);
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1713c);
            zotoebnojf.mo4320a(2, (byte) 12);
            EmkjBpiUfq.m1687a(zotoebnojf, this.f1714d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$YgeTlQwUNa */
    private static final class YgeTlQwUNa extends upgqDBbsrL<Integer> {
        /* renamed from: c */
        private final String f1715c;
        /* renamed from: d */
        private final List<String> f1716d;

        YgeTlQwUNa(String str, List<String> list) {
            super("setFriends", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1715c = str;
            if (list == null) {
                throw new NullPointerException("userIds");
            }
            this.f1716d = list;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 8) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = Integer.valueOf(zotoebnojf.mo4333j());
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1715c);
            zotoebnojf.mo4320a(2, (byte) 15);
            zotoebnojf.mo4318a((byte) 11, this.f1716d.size());
            for (String a : this.f1716d) {
                zotoebnojf.mo4322a(a);
            }
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$bpiSwUyLit */
    private static final class bpiSwUyLit extends upgqDBbsrL<JbBdMtJmlU> {
        /* renamed from: c */
        private final String f1717c;

        bpiSwUyLit(String str) {
            super("getInviteProviders", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1717c = str;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = JbBdMtJmlU.m1691a(zotoebnojf);
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1717c);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$cjrhisSQCL */
    private static final class cjrhisSQCL extends upgqDBbsrL<nGNJgptECj> {
        /* renamed from: c */
        private final String f1718c;
        /* renamed from: d */
        private final rWfbqYooCV f1719d;

        cjrhisSQCL(String str, rWfbqYooCV rwfbqyoocv) {
            super("addIdentity", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1718c = str;
            if (rwfbqyoocv == null) {
                throw new NullPointerException("identity");
            }
            this.f1719d = rwfbqyoocv;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = nGNJgptECj.m1877a(zotoebnojf);
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1718c);
            zotoebnojf.mo4320a(2, (byte) 12);
            rWfbqYooCV.m1883a(zotoebnojf, this.f1719d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$fOrCGNYyfk */
    private static final class fOrCGNYyfk extends upgqDBbsrL<List<ofLJAxfaCe>> {
        /* renamed from: c */
        private final String f1720c;
        /* renamed from: d */
        private final String f1721d;
        /* renamed from: e */
        private final Integer f1722e;
        /* renamed from: f */
        private final Integer f1723f;

        fOrCGNYyfk(String str, String str2, Integer num, Integer num2) {
            super("getActivityLikers", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1720c = str;
            if (str2 == null) {
                throw new NullPointerException("activityId");
            }
            this.f1721d = str2;
            if (num == null) {
                throw new NullPointerException("offset");
            }
            this.f1722e = num;
            if (num2 == null) {
                throw new NullPointerException("limit");
            }
            this.f1723f = num2;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            Object obj = null;
            KkSvQPDhNi kkSvQPDhNi = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 15) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            im.getsocial.p018b.p021c.p022a.cjrhisSQCL e = zotoebnojf.mo4328e();
                            obj = new ArrayList(e.f1045b);
                            for (int i = 0; i < e.f1045b; i++) {
                                obj.add(ofLJAxfaCe.m1879a(zotoebnojf));
                            }
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1720c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1721d);
            zotoebnojf.mo4320a(3, (byte) 8);
            zotoebnojf.mo4319a(this.f1722e.intValue());
            zotoebnojf.mo4320a(4, (byte) 8);
            zotoebnojf.mo4319a(this.f1723f.intValue());
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$iFpupLCESp */
    private static final class iFpupLCESp extends upgqDBbsrL<List<JWvbLzaedN>> {
        /* renamed from: c */
        private final String f1724c;
        /* renamed from: d */
        private final DvmrLquonW f1725d;

        iFpupLCESp(String str, DvmrLquonW dvmrLquonW) {
            super("getNotificationsList", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1724c = str;
            if (dvmrLquonW == null) {
                throw new NullPointerException(SearchIntents.EXTRA_QUERY);
            }
            this.f1725d = dvmrLquonW;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            Object obj = null;
            KkSvQPDhNi kkSvQPDhNi = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 15) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            im.getsocial.p018b.p021c.p022a.cjrhisSQCL e = zotoebnojf.mo4328e();
                            obj = new ArrayList(e.f1045b);
                            for (int i = 0; i < e.f1045b; i++) {
                                obj.add(JWvbLzaedN.m1690a(zotoebnojf));
                            }
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1724c);
            zotoebnojf.mo4320a(2, (byte) 12);
            DvmrLquonW.m1685a(zotoebnojf, this.f1725d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$iqXBPEYHZB */
    private static final class iqXBPEYHZB extends upgqDBbsrL<Boolean> {
        /* renamed from: c */
        private final String f1726c;
        /* renamed from: d */
        private final icjTFWWVFN f1727d;

        iqXBPEYHZB(String str, icjTFWWVFN icjtfwwvfn) {
            super("updateSession", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1726c = str;
            if (icjtfwwvfn == null) {
                throw new NullPointerException("superProperties");
            }
            this.f1727d = icjtfwwvfn;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 2) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = Boolean.valueOf(zotoebnojf.mo4330g());
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1726c);
            zotoebnojf.mo4320a(2, (byte) 12);
            icjTFWWVFN.m1876a(zotoebnojf, this.f1727d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$jMsobIMeui */
    private static final class jMsobIMeui extends upgqDBbsrL<Integer> {
        /* renamed from: c */
        private final String f1728c;

        jMsobIMeui(String str) {
            super("getFriendsCount", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1728c = str;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 8) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = Integer.valueOf(zotoebnojf.mo4333j());
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1728c);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$jjbQypPegg */
    private static final class jjbQypPegg extends upgqDBbsrL<Integer> {
        /* renamed from: c */
        private final String f1729c;
        /* renamed from: d */
        private final String f1730d;

        jjbQypPegg(String str, String str2) {
            super("addFriend", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1729c = str;
            if (str2 == null) {
                throw new NullPointerException(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
            }
            this.f1730d = str2;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 8) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = Integer.valueOf(zotoebnojf.mo4333j());
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(im.getsocial.p018b.p021c.ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1729c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1730d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$nGNJgptECj */
    private static final class nGNJgptECj extends upgqDBbsrL<Boolean> {
        /* renamed from: c */
        private final String f1731c;
        /* renamed from: d */
        private final String f1732d;
        /* renamed from: e */
        private final iqXBPEYHZB f1733e;

        nGNJgptECj(String str, String str2, iqXBPEYHZB iqxbpeyhzb) {
            super("reportActivity", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1731c = str;
            if (str2 == null) {
                throw new NullPointerException("activityId");
            }
            this.f1732d = str2;
            if (iqxbpeyhzb == null) {
                throw new NullPointerException("reportingReason");
            }
            this.f1733e = iqxbpeyhzb;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 2) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = Boolean.valueOf(zotoebnojf.mo4330g());
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1731c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1732d);
            zotoebnojf.mo4320a(3, (byte) 8);
            zotoebnojf.mo4319a(this.f1733e.value);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$ofLJAxfaCe */
    private static final class ofLJAxfaCe extends upgqDBbsrL<nGNJgptECj> {
        /* renamed from: c */
        private final String f1734c;
        /* renamed from: d */
        private final rWfbqYooCV f1735d;

        ofLJAxfaCe(String str, rWfbqYooCV rwfbqyoocv) {
            super("removeIdentity", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1734c = str;
            if (rwfbqyoocv == null) {
                throw new NullPointerException("identity");
            }
            this.f1735d = rwfbqyoocv;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = nGNJgptECj.m1877a(zotoebnojf);
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1734c);
            zotoebnojf.mo4320a(2, (byte) 12);
            rWfbqYooCV.m1883a(zotoebnojf, this.f1735d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$pdwpUtZXDT */
    private static final class pdwpUtZXDT extends upgqDBbsrL<xAXgtBkRbG> {
        /* renamed from: c */
        private final KdkQzTlDzz f1736c;

        pdwpUtZXDT(KdkQzTlDzz kdkQzTlDzz) {
            super("authenticateSdk", (byte) 1);
            if (kdkQzTlDzz == null) {
                throw new NullPointerException(ShareConstants.WEB_DIALOG_RESULT_PARAM_REQUEST_ID);
            }
            this.f1736c = kdkQzTlDzz;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = xAXgtBkRbG.m1886a(zotoebnojf);
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 12);
            KdkQzTlDzz.m1693a(zotoebnojf, this.f1736c);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$qZypgoeblR */
    private static final class qZypgoeblR extends upgqDBbsrL<List<XdbacJlTDQ>> {
        /* renamed from: c */
        private final String f1737c;
        /* renamed from: d */
        private final String f1738d;
        /* renamed from: e */
        private final pdwpUtZXDT f1739e;

        qZypgoeblR(String str, String str2, pdwpUtZXDT pdwputzxdt) {
            super("getActivities", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1737c = str;
            if (str2 == null) {
                throw new NullPointerException("feed");
            }
            this.f1738d = str2;
            if (pdwputzxdt == null) {
                throw new NullPointerException(SearchIntents.EXTRA_QUERY);
            }
            this.f1739e = pdwputzxdt;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            Object obj = null;
            KkSvQPDhNi kkSvQPDhNi = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 15) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            im.getsocial.p018b.p021c.p022a.cjrhisSQCL e = zotoebnojf.mo4328e();
                            obj = new ArrayList(e.f1045b);
                            for (int i = 0; i < e.f1045b; i++) {
                                obj.add(XdbacJlTDQ.m1701a(zotoebnojf));
                            }
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1737c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1738d);
            zotoebnojf.mo4320a(3, (byte) 12);
            pdwpUtZXDT.m1880a(zotoebnojf, this.f1739e);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$qdyNCsqjKt */
    private static final class qdyNCsqjKt extends upgqDBbsrL<List<YgeTlQwUNa>> {
        /* renamed from: c */
        private final String f1740c;
        /* renamed from: d */
        private final Integer f1741d;
        /* renamed from: e */
        private final Integer f1742e;

        qdyNCsqjKt(String str, Integer num, Integer num2) {
            super("getFriends", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1740c = str;
            if (num == null) {
                throw new NullPointerException("offset");
            }
            this.f1741d = num;
            if (num2 == null) {
                throw new NullPointerException("limit");
            }
            this.f1742e = num2;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            Object obj = null;
            KkSvQPDhNi kkSvQPDhNi = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 15) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            im.getsocial.p018b.p021c.p022a.cjrhisSQCL e = zotoebnojf.mo4328e();
                            obj = new ArrayList(e.f1045b);
                            for (int i = 0; i < e.f1045b; i++) {
                                obj.add(YgeTlQwUNa.m1702a(zotoebnojf));
                            }
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1740c);
            zotoebnojf.mo4320a(2, (byte) 8);
            zotoebnojf.mo4319a(this.f1741d.intValue());
            zotoebnojf.mo4320a(3, (byte) 8);
            zotoebnojf.mo4319a(this.f1742e.intValue());
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$rFvvVpjzZH */
    private static final class rFvvVpjzZH extends upgqDBbsrL<YgeTlQwUNa> {
        /* renamed from: c */
        private final String f1743c;
        /* renamed from: d */
        private final String f1744d;

        rFvvVpjzZH(String str, String str2) {
            super("getPublicUser", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1743c = str;
            if (str2 == null) {
                throw new NullPointerException("id");
            }
            this.f1744d = str2;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = YgeTlQwUNa.m1702a(zotoebnojf);
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1743c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1744d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$rWfbqYooCV */
    private static final class rWfbqYooCV extends upgqDBbsrL<List<XdbacJlTDQ>> {
        /* renamed from: c */
        private final String f1745c;
        /* renamed from: d */
        private final String f1746d;

        rWfbqYooCV(String str, String str2) {
            super("getStickyActivities", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1745c = str;
            if (str2 == null) {
                throw new NullPointerException("feed");
            }
            this.f1746d = str2;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            Object obj = null;
            KkSvQPDhNi kkSvQPDhNi = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 15) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            im.getsocial.p018b.p021c.p022a.cjrhisSQCL e = zotoebnojf.mo4328e();
                            obj = new ArrayList(e.f1045b);
                            for (int i = 0; i < e.f1045b; i++) {
                                obj.add(XdbacJlTDQ.m1701a(zotoebnojf));
                            }
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1745c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1746d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$ruWsnwUPKh */
    private static final class ruWsnwUPKh extends upgqDBbsrL<List<XdbacJlTDQ>> {
        /* renamed from: c */
        private final String f1747c;
        /* renamed from: d */
        private final String f1748d;
        /* renamed from: e */
        private final pdwpUtZXDT f1749e;

        ruWsnwUPKh(String str, String str2, pdwpUtZXDT pdwputzxdt) {
            super("getComments", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1747c = str;
            if (str2 == null) {
                throw new NullPointerException("activityId");
            }
            this.f1748d = str2;
            if (pdwputzxdt == null) {
                throw new NullPointerException(SearchIntents.EXTRA_QUERY);
            }
            this.f1749e = pdwputzxdt;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            Object obj = null;
            KkSvQPDhNi kkSvQPDhNi = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 15) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            im.getsocial.p018b.p021c.p022a.cjrhisSQCL e = zotoebnojf.mo4328e();
                            obj = new ArrayList(e.f1045b);
                            for (int i = 0; i < e.f1045b; i++) {
                                obj.add(XdbacJlTDQ.m1701a(zotoebnojf));
                            }
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1747c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1748d);
            zotoebnojf.mo4320a(3, (byte) 12);
            pdwpUtZXDT.m1880a(zotoebnojf, this.f1749e);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$sdizKTglGl */
    private static final class sdizKTglGl extends upgqDBbsrL<Boolean> {
        /* renamed from: c */
        private final String f1750c;
        /* renamed from: d */
        private final Boolean f1751d;

        sdizKTglGl(String str, Boolean bool) {
            super("setPushNotificationsEnabled", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1750c = str;
            if (bool == null) {
                throw new NullPointerException("enabled");
            }
            this.f1751d = bool;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 2) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = Boolean.valueOf(zotoebnojf.mo4330g());
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1750c);
            zotoebnojf.mo4320a(2, (byte) 2);
            zotoebnojf.mo4324a(this.f1751d.booleanValue());
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$sqEuGXwfLT */
    private static final class sqEuGXwfLT extends upgqDBbsrL<nGNJgptECj> {
        /* renamed from: c */
        private final String f1752c;
        /* renamed from: d */
        private final rWfbqYooCV f1753d;

        sqEuGXwfLT(String str, rWfbqYooCV rwfbqyoocv) {
            super("getPrivateUserByIdentity", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1752c = str;
            if (rwfbqyoocv == null) {
                throw new NullPointerException("identity");
            }
            this.f1753d = rwfbqyoocv;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = nGNJgptECj.m1877a(zotoebnojf);
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1752c);
            zotoebnojf.mo4320a(2, (byte) 12);
            rWfbqYooCV.m1883a(zotoebnojf, this.f1753d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$upgqDBbsrL */
    private static final class upgqDBbsrL extends im.getsocial.p018b.p021c.p023b.upgqDBbsrL<Integer> {
        /* renamed from: c */
        private final String f1754c;
        /* renamed from: d */
        private final String f1755d;
        /* renamed from: e */
        private final List<String> f1756e;

        upgqDBbsrL(String str, String str2, List<String> list) {
            super("addFriendsByIdentity", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1754c = str;
            if (str2 == null) {
                throw new NullPointerException("provider");
            }
            this.f1755d = str2;
            if (list == null) {
                throw new NullPointerException("providerIds");
            }
            this.f1756e = list;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 8) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = Integer.valueOf(zotoebnojf.mo4333j());
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1754c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1755d);
            zotoebnojf.mo4320a(3, (byte) 15);
            zotoebnojf.mo4318a((byte) 11, this.f1756e.size());
            for (String a : this.f1756e) {
                zotoebnojf.mo4322a(a);
            }
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$vkXhnjhKGp */
    private static final class vkXhnjhKGp extends upgqDBbsrL<XdbacJlTDQ> {
        /* renamed from: c */
        private final String f1757c;
        /* renamed from: d */
        private final String f1758d;
        /* renamed from: e */
        private final zoToeBNOjF f1759e;

        vkXhnjhKGp(String str, String str2, zoToeBNOjF zotoebnojf) {
            super("postComment", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1757c = str;
            if (str2 == null) {
                throw new NullPointerException("activityId");
            }
            this.f1758d = str2;
            if (zotoebnojf == null) {
                throw new NullPointerException("activityPostContent");
            }
            this.f1759e = zotoebnojf;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = XdbacJlTDQ.m1701a(zotoebnojf);
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1757c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1758d);
            zotoebnojf.mo4320a(3, (byte) 12);
            zoToeBNOjF.m1888a(zotoebnojf, this.f1759e);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$wWemqSpYTx */
    private static final class wWemqSpYTx extends upgqDBbsrL<Boolean> {
        /* renamed from: c */
        private final String f1760c;

        wWemqSpYTx(String str) {
            super("isPushNotificationsEnabled", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1760c = str;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 2) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = Boolean.valueOf(zotoebnojf.mo4330g());
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1760c);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$xlPHPMtUBa */
    private static final class xlPHPMtUBa extends upgqDBbsrL<Boolean> {
        /* renamed from: c */
        private final String f1761c;
        /* renamed from: d */
        private final icjTFWWVFN f1762d;
        /* renamed from: e */
        private final List<ztWNWCuZiM> f1763e;

        xlPHPMtUBa(String str, icjTFWWVFN icjtfwwvfn, List<ztWNWCuZiM> list) {
            super("trackAnalyticsEvents", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1761c = str;
            if (icjtfwwvfn == null) {
                throw new NullPointerException("commonProperties");
            }
            this.f1762d = icjtfwwvfn;
            if (list == null) {
                throw new NullPointerException("events");
            }
            this.f1763e = list;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 2) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = Boolean.valueOf(zotoebnojf.mo4330g());
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1761c);
            zotoebnojf.mo4320a(2, (byte) 12);
            icjTFWWVFN.m1876a(zotoebnojf, this.f1762d);
            zotoebnojf.mo4320a(3, (byte) 15);
            zotoebnojf.mo4318a((byte) 12, this.f1763e.size());
            for (ztWNWCuZiM a : this.f1763e) {
                ztWNWCuZiM.m1889a(zotoebnojf, a);
            }
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$zoToeBNOjF */
    private static final class zoToeBNOjF extends upgqDBbsrL<Boolean> {
        /* renamed from: c */
        private final String f1764c;
        /* renamed from: d */
        private final String f1765d;

        zoToeBNOjF(String str, String str2) {
            super("deleteActivity", (byte) 1);
            if (str == null) {
                throw new NullPointerException("sessionId");
            }
            this.f1764c = str;
            if (str2 == null) {
                throw new NullPointerException("activityId");
            }
            this.f1765d = str2;
        }

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(im.getsocial.p018b.p021c.p022a.zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            KkSvQPDhNi kkSvQPDhNi = null;
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 2) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = Boolean.valueOf(zotoebnojf.mo4330g());
                            break;
                        case (short) 1:
                            if (c.f1055b != (byte) 12) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            kkSvQPDhNi = KkSvQPDhNi.m1694a(zotoebnojf);
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    if (kkSvQPDhNi != null) {
                        throw kkSvQPDhNi;
                    }
                    throw new ztWNWCuZiM(ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(im.getsocial.p018b.p021c.p022a.zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1764c);
            zotoebnojf.mo4320a(2, (byte) 11);
            zotoebnojf.mo4322a(this.f1765d);
            zotoebnojf.mo4316a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.f.a.cjrhisSQCL$ztWNWCuZiM */
    private static final class ztWNWCuZiM extends upgqDBbsrL<String> {
        /* renamed from: c */
        private final String f1766c;

        /* renamed from: a */
        protected final /* synthetic */ Object mo4499a(zoToeBNOjF zotoebnojf, XdbacJlTDQ xdbacJlTDQ) {
            Object obj = null;
            while (true) {
                im.getsocial.p018b.p021c.p022a.upgqDBbsrL c = zotoebnojf.mo4326c();
                if (c.f1055b != (byte) 0) {
                    switch (c.f1056c) {
                        case (short) 0:
                            if (c.f1055b != (byte) 11) {
                                im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                                break;
                            }
                            obj = zotoebnojf.mo4336m();
                            break;
                        default:
                            im.getsocial.p018b.p021c.p025d.jjbQypPegg.m858a(zotoebnojf, c.f1055b);
                            break;
                    }
                } else if (obj != null) {
                    return obj;
                } else {
                    throw new im.getsocial.p018b.p021c.ztWNWCuZiM(im.getsocial.p018b.p021c.ztWNWCuZiM.jjbQypPegg.MISSING_RESULT, "Missing result");
                }
            }
        }

        /* renamed from: a */
        protected final void mo4500a(zoToeBNOjF zotoebnojf) {
            zotoebnojf.mo4320a(1, (byte) 11);
            zotoebnojf.mo4322a(this.f1766c);
            zotoebnojf.mo4316a();
        }
    }

    public cjrhisSQCL(zoToeBNOjF zotoebnojf) {
        super(zotoebnojf);
    }

    /* renamed from: a */
    public final JQrJMKopAa mo4501a(String str, CJZnJxRuoc cJZnJxRuoc) {
        IOException e;
        try {
            return (JQrJMKopAa) m852a(new SKUqohGtGQ(str, cJZnJxRuoc));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final QCXFOjcJkE mo4502a(String str, EmkjBpiUfq emkjBpiUfq) {
        IOException e;
        try {
            return (QCXFOjcJkE) m852a(new XdbacJlTDQ(str, emkjBpiUfq));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final XdbacJlTDQ mo4503a(String str, String str2, zoToeBNOjF zotoebnojf) {
        IOException e;
        try {
            return (XdbacJlTDQ) m852a(new QhisXzMgay(str, str2, zotoebnojf));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final XdbacJlTDQ mo4504a(String str, String str2, Boolean bool) {
        IOException e;
        try {
            return (XdbacJlTDQ) m852a(new JbBdMtJmlU(str, str2, bool));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final YgeTlQwUNa mo4505a(String str, String str2) {
        IOException e;
        try {
            return (YgeTlQwUNa) m852a(new rFvvVpjzZH(str, str2));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final nGNJgptECj mo4506a(String str, nGNJgptECj ngnjgptecj) {
        IOException e;
        try {
            return (nGNJgptECj) m852a(new KdkQzTlDzz(str, ngnjgptecj));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final nGNJgptECj mo4507a(String str, rWfbqYooCV rwfbqyoocv) {
        IOException e;
        try {
            return (nGNJgptECj) m852a(new cjrhisSQCL(str, rwfbqyoocv));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final xAXgtBkRbG mo4508a(KdkQzTlDzz kdkQzTlDzz) {
        IOException e;
        try {
            return (xAXgtBkRbG) m852a(new pdwpUtZXDT(kdkQzTlDzz));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final Boolean mo4509a(String str, DvynvDnqtx dvynvDnqtx) {
        IOException e;
        try {
            return (Boolean) m852a(new UwIeQkAzJH(str, dvynvDnqtx));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final Boolean mo4510a(String str, UwIeQkAzJH uwIeQkAzJH) {
        IOException e;
        try {
            return (Boolean) m852a(new JWvbLzaedN(str, uwIeQkAzJH));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final Boolean mo4511a(String str, icjTFWWVFN icjtfwwvfn) {
        IOException e;
        try {
            return (Boolean) m852a(new iqXBPEYHZB(str, icjtfwwvfn));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final Boolean mo4512a(String str, icjTFWWVFN icjtfwwvfn, List<ztWNWCuZiM> list) {
        IOException e;
        try {
            return (Boolean) m852a(new xlPHPMtUBa(str, icjtfwwvfn, list));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final Boolean mo4513a(String str, Boolean bool) {
        IOException e;
        try {
            return (Boolean) m852a(new sdizKTglGl(str, bool));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final Boolean mo4514a(String str, String str2, iqXBPEYHZB iqxbpeyhzb) {
        IOException e;
        try {
            return (Boolean) m852a(new nGNJgptECj(str, str2, iqxbpeyhzb));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final Integer mo4515a(String str) {
        IOException e;
        try {
            return (Integer) m852a(new jMsobIMeui(str));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final Integer mo4516a(String str, List<String> list) {
        IOException e;
        try {
            return (Integer) m852a(new YgeTlQwUNa(str, list));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final List<JWvbLzaedN> mo4517a(String str, DvmrLquonW dvmrLquonW) {
        IOException e;
        try {
            return (List) m852a(new iFpupLCESp(str, dvmrLquonW));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final List<CyDeXbQkhA> mo4518a(String str, VuXsWfriFX vuXsWfriFX) {
        IOException e;
        try {
            return (List) m852a(new HptYHntaqF(str, vuXsWfriFX));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final List<String> mo4519a(String str, ZWjsSaCmFq zWjsSaCmFq) {
        IOException e;
        try {
            return (List) m852a(new KSZKMmRWhZ(str, zWjsSaCmFq));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final List<YgeTlQwUNa> mo4520a(String str, Integer num, Integer num2) {
        IOException e;
        try {
            return (List) m852a(new qdyNCsqjKt(str, num, num2));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final List<XdbacJlTDQ> mo4521a(String str, String str2, pdwpUtZXDT pdwputzxdt) {
        IOException e;
        try {
            return (List) m852a(new qZypgoeblR(str, str2, pdwputzxdt));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final List<ofLJAxfaCe> mo4522a(String str, String str2, Integer num, Integer num2) {
        IOException e;
        try {
            return (List) m852a(new fOrCGNYyfk(str, str2, num, num2));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: a */
    public final Map<String, YgeTlQwUNa> mo4523a(String str, String str2, List<String> list) {
        IOException e;
        try {
            return (Map) m852a(new KkSvQPDhNi(str, str2, list));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: b */
    public final XdbacJlTDQ mo4524b(String str, String str2, zoToeBNOjF zotoebnojf) {
        IOException e;
        try {
            return (XdbacJlTDQ) m852a(new vkXhnjhKGp(str, str2, zotoebnojf));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: b */
    public final nGNJgptECj mo4525b(String str, rWfbqYooCV rwfbqyoocv) {
        IOException e;
        try {
            return (nGNJgptECj) m852a(new ofLJAxfaCe(str, rwfbqyoocv));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: b */
    public final Integer mo4526b(String str, DvmrLquonW dvmrLquonW) {
        IOException e;
        try {
            return (Integer) m852a(new QCXFOjcJkE(str, dvmrLquonW));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: b */
    public final Integer mo4527b(String str, String str2) {
        IOException e;
        try {
            return (Integer) m852a(new jjbQypPegg(str, str2));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: b */
    public final Integer mo4528b(String str, String str2, List<String> list) {
        IOException e;
        try {
            return (Integer) m852a(new upgqDBbsrL(str, str2, list));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: b */
    public final List<CyDeXbQkhA> mo4529b(String str) {
        IOException e;
        try {
            return (List) m852a(new EmkjBpiUfq(str));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: b */
    public final List<BpPZzHFMaU> mo4530b(String str, Integer num, Integer num2) {
        IOException e;
        try {
            return (List) m852a(new QWVUXapsSm(str, num, num2));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: b */
    public final List<XdbacJlTDQ> mo4531b(String str, String str2, pdwpUtZXDT pdwputzxdt) {
        IOException e;
        try {
            return (List) m852a(new ruWsnwUPKh(str, str2, pdwputzxdt));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: c */
    public final JbBdMtJmlU mo4532c(String str) {
        IOException e;
        try {
            return (JbBdMtJmlU) m852a(new bpiSwUyLit(str));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: c */
    public final nGNJgptECj mo4533c(String str, rWfbqYooCV rwfbqyoocv) {
        IOException e;
        try {
            return (nGNJgptECj) m852a(new sqEuGXwfLT(str, rwfbqyoocv));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: c */
    public final Integer mo4534c(String str, String str2) {
        IOException e;
        try {
            return (Integer) m852a(new DvmrLquonW(str, str2));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: c */
    public final Integer mo4535c(String str, String str2, List<String> list) {
        IOException e;
        try {
            return (Integer) m852a(new DvynvDnqtx(str, str2, list));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: d */
    public final Boolean mo4536d(String str, String str2) {
        IOException e;
        try {
            return (Boolean) m852a(new KCGqEGAizh(str, str2));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: d */
    public final Integer mo4537d(String str, String str2, List<String> list) {
        IOException e;
        try {
            return (Integer) m852a(new CJZnJxRuoc(str, str2, list));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: d */
    public final List<YgeTlQwUNa> mo4538d(String str) {
        IOException e;
        try {
            return (List) m852a(new IbawHMWljm(str));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: e */
    public final Boolean mo4539e(String str) {
        IOException e;
        try {
            return (Boolean) m852a(new wWemqSpYTx(str));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: e */
    public final List<XdbacJlTDQ> mo4540e(String str, String str2) {
        IOException e;
        try {
            return (List) m852a(new rWfbqYooCV(str, str2));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: f */
    public final XdbacJlTDQ mo4541f(String str, String str2) {
        IOException e;
        try {
            return (XdbacJlTDQ) m852a(new KluUZYuxme(str, str2));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }

    /* renamed from: g */
    public final Boolean mo4542g(String str, String str2) {
        IOException e;
        try {
            return (Boolean) m852a(new zoToeBNOjF(str, str2));
        } catch (KkSvQPDhNi e2) {
            e = e2;
            throw e;
        } catch (IOException e3) {
            e = e3;
            throw e;
        } catch (RuntimeException e4) {
            e = e4;
            throw e;
        } catch (Throwable e5) {
            throw new RuntimeException("Unexpected exception", e5);
        }
    }
}
