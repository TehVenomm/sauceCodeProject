package im.getsocial.sdk.internal.p078h.p081c;

import im.getsocial.sdk.CompletionCallback;
import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p033c.KSZKMmRWhZ;
import im.getsocial.sdk.internal.p033c.QhisXzMgay;
import im.getsocial.sdk.internal.p033c.SKUqohGtGQ;
import im.getsocial.sdk.internal.p033c.bpiSwUyLit;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.jMsobIMeui;
import im.getsocial.sdk.internal.p033c.p041b.pdwpUtZXDT;
import im.getsocial.sdk.internal.p033c.p041b.ruWsnwUPKh;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p060k.p064d.jjbQypPegg;
import im.getsocial.sdk.internal.p033c.rFvvVpjzZH;
import im.getsocial.sdk.internal.p033c.zoToeBNOjF;
import im.getsocial.sdk.internal.p078h.p081c.upgqDBbsrL.C10129;
import im.getsocial.sdk.invites.ReferralData;
import im.getsocial.sdk.usermanagement.PrivateUser;
import im.getsocial.sdk.usermanagement.p138a.p143e.HptYHntaqF;
import java.util.EnumSet;
import java.util.concurrent.TimeUnit;

/* renamed from: im.getsocial.sdk.internal.h.c.upgqDBbsrL */
public final class upgqDBbsrL implements im.getsocial.sdk.internal.p033c.p034l.upgqDBbsrL {
    /* renamed from: o */
    private static final cjrhisSQCL f1960o = im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL.m1274a(upgqDBbsrL.class);
    @XdbacJlTDQ
    /* renamed from: a */
    pdwpUtZXDT f1961a;
    @XdbacJlTDQ
    /* renamed from: b */
    QhisXzMgay f1962b;
    @XdbacJlTDQ
    /* renamed from: c */
    rFvvVpjzZH f1963c;
    @XdbacJlTDQ
    /* renamed from: d */
    im.getsocial.sdk.internal.p030e.p065a.XdbacJlTDQ f1964d;
    @XdbacJlTDQ
    /* renamed from: e */
    jjbQypPegg f1965e;
    @XdbacJlTDQ
    /* renamed from: f */
    im.getsocial.sdk.internal.p033c.p059j.jjbQypPegg f1966f;
    @XdbacJlTDQ
    /* renamed from: g */
    im.getsocial.sdk.internal.p082i.p084b.jjbQypPegg f1967g;
    @XdbacJlTDQ
    /* renamed from: h */
    im.getsocial.sdk.invites.p092a.p099f.jjbQypPegg f1968h;
    @XdbacJlTDQ
    /* renamed from: i */
    im.getsocial.sdk.internal.p036a.p037a.jjbQypPegg f1969i;
    @XdbacJlTDQ
    /* renamed from: j */
    bpiSwUyLit f1970j;
    @XdbacJlTDQ
    /* renamed from: k */
    zoToeBNOjF f1971k;
    @XdbacJlTDQ
    /* renamed from: l */
    SKUqohGtGQ f1972l;
    @XdbacJlTDQ
    /* renamed from: m */
    KSZKMmRWhZ f1973m;
    @XdbacJlTDQ
    /* renamed from: n */
    im.getsocial.sdk.internal.p033c.p066m.pdwpUtZXDT f1974n;

    /* renamed from: im.getsocial.sdk.internal.h.c.upgqDBbsrL$6 */
    class C10096 extends im.getsocial.sdk.internal.p030e.KSZKMmRWhZ<String> {
        /* renamed from: a */
        final /* synthetic */ upgqDBbsrL f1954a;

        C10096(upgqDBbsrL upgqdbbsrl) {
            this.f1954a = upgqdbbsrl;
        }

        /* renamed from: b */
        public final /* synthetic */ void mo4412b(Object obj) {
            this.f1954a.f1967g.m1991a((String) obj);
        }
    }

    /* renamed from: im.getsocial.sdk.internal.h.c.upgqDBbsrL$9 */
    class C10129 implements Runnable {
        /* renamed from: a */
        final /* synthetic */ upgqDBbsrL f1959a;

        C10129(upgqDBbsrL upgqdbbsrl) {
            this.f1959a = upgqdbbsrl;
        }

        public void run() {
            StringBuilder append = new StringBuilder("GETSOCIAL: App signature do not match information provided on the Dashboard.\nTo fix the issue, go to https://dashboard.getsocial.im/#/app-settings and add the following info in the App Stores Configuration section:\n").append(this.f1959a.f1971k.mo4402a());
            if (this.f1959a.f1974n == im.getsocial.sdk.internal.p033c.p066m.pdwpUtZXDT.ANDROID) {
                append.append("\n!!!IMPORTANT: Do not forget to add your release certificate signing configuration as well!");
            }
            String stringBuilder = append.toString();
            upgqDBbsrL.f1960o.mo4396d(stringBuilder);
            throw new RuntimeException(stringBuilder);
        }
    }

    public upgqDBbsrL() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    /* renamed from: a */
    public final void m1986a(final String str, final CompletionCallback completionCallback) {
        f1960o.mo4387a("InitSdkUseCase.execute called");
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) completionCallback), "Can not execute InitSdkUseCase with null callback");
        synchronized (this.f1966f) {
            switch (this.f1966f.m1307a(jMsobIMeui.SESSION)) {
                case INITIALIZED:
                    completionCallback.onSuccess();
                    return;
                case INITIALIZING:
                    completionCallback.onFailure(new im.getsocial.sdk.internal.p033c.p051c.upgqDBbsrL("GetSocial initialization has already started."));
                    return;
                default:
                    this.f1966f.m1312b(jMsobIMeui.SESSION);
                    this.f1966f.m1312b(jMsobIMeui.USER);
                    final ruWsnwUPKh ruwsnwupkh = new ruWsnwUPKh(EnumSet.of(jMsobIMeui.SESSION, jMsobIMeui.USER));
                    this.f1965e.m1377a(new jjbQypPegg.jjbQypPegg(this) {
                        /* renamed from: b */
                        final /* synthetic */ upgqDBbsrL f1945b;

                        /* renamed from: a */
                        public final im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL mo4409a() {
                            return (im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL) ruwsnwupkh.m1218a(im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL.class);
                        }
                    });
                    boolean u = this.f1962b.mo4480u();
                    boolean a = im.getsocial.sdk.internal.p033c.p066m.XdbacJlTDQ.m1507a(this.f1970j);
                    im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT.m1659a((Object) str).m1669b(new im.getsocial.sdk.internal.p030e.jjbQypPegg<String>(this) {
                        /* renamed from: b */
                        final /* synthetic */ upgqDBbsrL f1958b;

                        /* renamed from: a */
                        public final /* bridge */ /* synthetic */ Object mo4487a() {
                            return str == null ? this.f1958b.f1963c.mo4367a("im.getsocial.sdk.AppId") : str;
                        }
                    }).m1669b(im.getsocial.sdk.internal.p078h.p080b.jjbQypPegg.m1958a()).m1669b(new im.getsocial.sdk.internal.p030e.KSZKMmRWhZ<im.getsocial.sdk.internal.p033c.XdbacJlTDQ>(this) {
                        /* renamed from: b */
                        final /* synthetic */ upgqDBbsrL f1956b;

                        /* renamed from: b */
                        public final /* synthetic */ void mo4412b(Object obj) {
                            ((im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL) ruwsnwupkh.m1218a(im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL.class)).m1319a((im.getsocial.sdk.internal.p033c.XdbacJlTDQ) obj);
                        }
                    }).m1669b(new im.getsocial.sdk.internal.p082i.p083a.jjbQypPegg()).m1669b(new im.getsocial.sdk.internal.p082i.p083a.upgqDBbsrL()).m1669b(new C10096(this)).m1669b(new im.getsocial.sdk.internal.p030e.jjbQypPegg<im.getsocial.sdk.internal.p033c.XdbacJlTDQ>(this) {
                        /* renamed from: b */
                        final /* synthetic */ upgqDBbsrL f1953b;

                        /* renamed from: a */
                        public final /* synthetic */ Object mo4487a() {
                            return ((im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL) ruwsnwupkh.m1218a(im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL.class)).m1322b();
                        }
                    }).m1669b(new im.getsocial.sdk.usermanagement.p138a.p140b.pdwpUtZXDT()).m1665a(new im.getsocial.sdk.usermanagement.p138a.p140b.upgqDBbsrL()).m1669b(new im.getsocial.sdk.usermanagement.p138a.p140b.zoToeBNOjF()).m1669b(new im.getsocial.sdk.internal.p030e.KSZKMmRWhZ<im.getsocial.sdk.usermanagement.p138a.p139a.upgqDBbsrL>(this) {
                        /* renamed from: b */
                        final /* synthetic */ upgqDBbsrL f1951b;

                        /* renamed from: b */
                        public final /* synthetic */ void mo4412b(Object obj) {
                            im.getsocial.sdk.usermanagement.p138a.p139a.upgqDBbsrL upgqdbbsrl = (im.getsocial.sdk.usermanagement.p138a.p139a.upgqDBbsrL) obj;
                            ((im.getsocial.sdk.usermanagement.p138a.p141c.jjbQypPegg) ruwsnwupkh.m1218a(im.getsocial.sdk.usermanagement.p138a.p141c.jjbQypPegg.class)).m3697a(upgqdbbsrl.m3677b());
                            ((im.getsocial.sdk.internal.p036a.p042e.jjbQypPegg) ruwsnwupkh.m1218a(im.getsocial.sdk.internal.p036a.p042e.jjbQypPegg.class)).m1040a(this.f1951b.f1962b.mo4474o());
                            ((im.getsocial.sdk.pushnotifications.p067a.p105d.jjbQypPegg) ruwsnwupkh.m1218a(im.getsocial.sdk.pushnotifications.p067a.p105d.jjbQypPegg.class)).m2431a(upgqdbbsrl.m3679d());
                            ((im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL) ruwsnwupkh.m1218a(im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL.class)).m1320a(upgqdbbsrl.m3680e());
                        }
                    }).m1669b(new im.getsocial.sdk.internal.p030e.jjbQypPegg<im.getsocial.sdk.internal.p030e.zoToeBNOjF<im.getsocial.sdk.internal.p033c.XdbacJlTDQ, PrivateUser>>(this) {
                        /* renamed from: b */
                        final /* synthetic */ upgqDBbsrL f1949b;

                        /* renamed from: a */
                        public final /* synthetic */ Object mo4487a() {
                            return im.getsocial.sdk.internal.p030e.zoToeBNOjF.m1677b(((im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL) ruwsnwupkh.m1218a(im.getsocial.sdk.internal.p033c.p059j.upgqDBbsrL.class)).m1322b(), ((im.getsocial.sdk.usermanagement.p138a.p141c.jjbQypPegg) ruwsnwupkh.m1218a(im.getsocial.sdk.usermanagement.p138a.p141c.jjbQypPegg.class)).m3698b());
                        }
                    }).m1669b(new im.getsocial.sdk.usermanagement.p138a.p140b.ztWNWCuZiM()).m1665a(new im.getsocial.sdk.invites.p092a.p096d.cjrhisSQCL()).m1669b(new im.getsocial.sdk.internal.p030e.KSZKMmRWhZ<im.getsocial.sdk.invites.p092a.p094b.upgqDBbsrL>(this) {
                        /* renamed from: b */
                        final /* synthetic */ upgqDBbsrL f1947b;

                        /* renamed from: b */
                        public final /* synthetic */ void mo4412b(Object obj) {
                            ((im.getsocial.sdk.invites.p092a.p099f.upgqDBbsrL) ruwsnwupkh.m1218a(im.getsocial.sdk.invites.p092a.p099f.upgqDBbsrL.class)).m2353a((im.getsocial.sdk.invites.p092a.p094b.upgqDBbsrL) obj);
                        }
                    }).m1665a(new im.getsocial.sdk.invites.p092a.p096d.upgqDBbsrL(a, u)).m1665a(new im.getsocial.sdk.invites.p092a.p096d.jjbQypPegg(a, u)).m1665a(new im.getsocial.sdk.invites.p092a.p096d.ztWNWCuZiM(null, a, u)).m1669b(new im.getsocial.sdk.internal.p030e.KSZKMmRWhZ<ReferralData>(this) {
                        /* renamed from: a */
                        final /* synthetic */ im.getsocial.sdk.internal.p078h.p081c.upgqDBbsrL f1943a;

                        {
                            this.f1943a = r1;
                        }

                        /* renamed from: b */
                        public final /* synthetic */ void mo4412b(Object obj) {
                            this.f1943a.f1968h.m2349a((ReferralData) obj);
                        }
                    }).m1669b(new im.getsocial.sdk.internal.p030e.ztWNWCuZiM(this) {
                        /* renamed from: b */
                        final /* synthetic */ im.getsocial.sdk.internal.p078h.p081c.upgqDBbsrL f1942b;

                        /* renamed from: a */
                        public final void mo4491a() {
                            this.f1942b.f1961a.m1207a(ruwsnwupkh);
                            this.f1942b.f1966f.m1314c(jMsobIMeui.SESSION);
                            this.f1942b.f1966f.m1314c(jMsobIMeui.USER);
                            this.f1942b.f1965e.m1376a();
                        }
                    }).m1669b(new im.getsocial.sdk.internal.p030e.ztWNWCuZiM(this) {
                        /* renamed from: a */
                        final /* synthetic */ im.getsocial.sdk.internal.p078h.p081c.upgqDBbsrL f1940a;

                        {
                            this.f1940a = r1;
                        }

                        /* renamed from: a */
                        public final void mo4491a() {
                            new HptYHntaqF().m3710a();
                        }
                    }).m1669b(new im.getsocial.sdk.internal.p030e.ztWNWCuZiM(this) {
                        /* renamed from: a */
                        final /* synthetic */ im.getsocial.sdk.internal.p078h.p081c.upgqDBbsrL f1939a;

                        {
                            this.f1939a = r1;
                        }

                        /* renamed from: a */
                        public final void mo4491a() {
                            this.f1939a.f1969i.mo4345a();
                        }
                    }).m1669b(new im.getsocial.sdk.internal.p030e.jjbQypPegg<Boolean>(this) {
                        /* renamed from: a */
                        final /* synthetic */ im.getsocial.sdk.internal.p078h.p081c.upgqDBbsrL f1938a;

                        {
                            this.f1938a = r1;
                        }

                        /* renamed from: a */
                        public final /* synthetic */ Object mo4487a() {
                            return Boolean.valueOf(im.getsocial.sdk.internal.p033c.p066m.XdbacJlTDQ.m1508a(this.f1938a.f1963c));
                        }
                    }).m1669b(new im.getsocial.sdk.pushnotifications.p067a.p104c.jjbQypPegg()).m1671d(new im.getsocial.sdk.internal.p033c.p054e.cjrhisSQCL(new im.getsocial.sdk.internal.p033c.p054e.upgqDBbsrL(this) {
                        /* renamed from: a */
                        final /* synthetic */ im.getsocial.sdk.internal.p078h.p081c.upgqDBbsrL f1937a;

                        {
                            this.f1937a = r1;
                        }

                        /* renamed from: a */
                        public final /* bridge */ /* synthetic */ Object mo4344a(Object obj) {
                            return im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT.m1660a((Throwable) obj);
                        }
                    }, 3, 1, TimeUnit.SECONDS)).m1664a(this.f1964d).m1668b(im.getsocial.sdk.internal.p030e.p065a.zoToeBNOjF.m1674a()).m1666a(new im.getsocial.sdk.internal.p030e.p065a.jjbQypPegg<Void>(this) {
                        /* renamed from: b */
                        final /* synthetic */ im.getsocial.sdk.internal.p078h.p081c.upgqDBbsrL f1934b;

                        /* renamed from: a */
                        public final /* synthetic */ void mo4455a(Object obj) {
                            im.getsocial.sdk.internal.p078h.p081c.upgqDBbsrL.f1960o.mo4387a("InitSdkUseCase callback success");
                            completionCallback.onSuccess();
                        }
                    }, new im.getsocial.sdk.internal.p030e.p065a.jjbQypPegg<Throwable>(this) {
                        /* renamed from: b */
                        final /* synthetic */ im.getsocial.sdk.internal.p078h.p081c.upgqDBbsrL f1936b;

                        /* renamed from: a */
                        public final /* synthetic */ void mo4455a(Object obj) {
                            Throwable th = (Throwable) obj;
                            im.getsocial.sdk.internal.p078h.p081c.upgqDBbsrL.f1960o.mo4387a("InitSdkUseCase callback failure");
                            im.getsocial.sdk.internal.p078h.p081c.upgqDBbsrL.f1960o.mo4389a(th);
                            this.f1936b.f1966f.m1315d(jMsobIMeui.SESSION);
                            this.f1936b.f1966f.m1315d(jMsobIMeui.USER);
                            this.f1936b.f1965e.m1376a();
                            GetSocialException a = im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(th);
                            if (a.getErrorCode() == ErrorCode.APP_SIGNATURE_MISMATCH && this.f1936b.f1973m.mo4459a()) {
                                this.f1936b.f1972l.mo4358a(new C10129(this.f1936b));
                            }
                            completionCallback.onFailure(a);
                        }
                    });
                    return;
            }
        }
    }
}
