package im.getsocial.sdk.internal.p030e.p065a;

import im.getsocial.sdk.internal.e.a.pdwpUtZXDT.AnonymousClass12;
import im.getsocial.sdk.internal.p030e.KSZKMmRWhZ;
import im.getsocial.sdk.internal.p030e.jjbQypPegg;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT.12.C09731;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT.XdbacJlTDQ;
import im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT.zoToeBNOjF;
import im.getsocial.sdk.internal.p030e.upgqDBbsrL;
import im.getsocial.sdk.internal.p030e.ztWNWCuZiM;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.AtomicInteger;

/* renamed from: im.getsocial.sdk.internal.e.a.pdwpUtZXDT */
public final class pdwpUtZXDT<T> {
    /* renamed from: a */
    final KSZKMmRWhZ<ztWNWCuZiM<? super T>> f1558a;
    /* renamed from: b */
    private XdbacJlTDQ f1559b;
    /* renamed from: c */
    private XdbacJlTDQ f1560c;

    /* renamed from: im.getsocial.sdk.internal.e.a.pdwpUtZXDT$1 */
    static final class C09741 extends KSZKMmRWhZ<ztWNWCuZiM<? super T>> {
        C09741() {
        }

        /* renamed from: b */
        public final /* synthetic */ void mo4412b(Object obj) {
            ((ztWNWCuZiM) obj).mo4488a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.e.a.pdwpUtZXDT$4 */
    class C09784 extends KSZKMmRWhZ<T> {
        /* renamed from: b */
        public final void mo4412b(T t) {
        }
    }

    /* renamed from: im.getsocial.sdk.internal.e.a.pdwpUtZXDT$5 */
    static final class C09795 extends KSZKMmRWhZ<ztWNWCuZiM<? super T>> {
        /* renamed from: a */
        final /* synthetic */ Object[] f1529a;

        /* renamed from: b */
        public final /* synthetic */ void mo4412b(Object obj) {
            ztWNWCuZiM ztwnwcuzim = (ztWNWCuZiM) obj;
            for (Object a : this.f1529a) {
                ztwnwcuzim.mo4489a(a);
            }
            ztwnwcuzim.mo4488a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.e.a.pdwpUtZXDT$KSZKMmRWhZ */
    interface KSZKMmRWhZ {
        /* renamed from: d */
        void mo4492d();
    }

    /* renamed from: im.getsocial.sdk.internal.e.a.pdwpUtZXDT$jjbQypPegg */
    private static abstract class jjbQypPegg<T> extends ztWNWCuZiM<T> implements KSZKMmRWhZ {
        /* renamed from: a */
        private final AtomicBoolean f1536a = new AtomicBoolean(false);
        /* renamed from: b */
        private KSZKMmRWhZ f1537b;
        /* renamed from: c */
        private XdbacJlTDQ f1538c;

        /* renamed from: im.getsocial.sdk.internal.e.a.pdwpUtZXDT$jjbQypPegg$3 */
        class C09863 extends ztWNWCuZiM {
            /* renamed from: a */
            final /* synthetic */ jjbQypPegg f1550a;

            C09863(jjbQypPegg jjbqyppegg) {
                this.f1550a = jjbqyppegg;
            }

            /* renamed from: a */
            public final void mo4491a() {
                try {
                    this.f1550a.mo4496c();
                } catch (Throwable th) {
                    th.printStackTrace();
                } finally {
                    this.f1550a.m1615e();
                }
            }
        }

        jjbQypPegg(XdbacJlTDQ xdbacJlTDQ) {
            this.f1538c = xdbacJlTDQ;
        }

        /* renamed from: e */
        private void m1615e() {
            this.f1538c = null;
            KSZKMmRWhZ kSZKMmRWhZ = this.f1537b;
            this.f1537b = null;
            if (kSZKMmRWhZ != null) {
                kSZKMmRWhZ.mo4492d();
            }
            mo4493b();
        }

        /* renamed from: f */
        private boolean m1616f() {
            return this.f1536a.getAndSet(true);
        }

        /* renamed from: a */
        public void mo4488a() {
            if (!m1616f()) {
                this.f1538c.mo4484a(new C09863(this));
            }
        }

        /* renamed from: a */
        protected final void m1618a(ztWNWCuZiM ztwnwcuzim) {
            if (jjbQypPegg.class.isInstance(ztwnwcuzim)) {
                ((jjbQypPegg) ztwnwcuzim).f1537b = this;
            }
        }

        /* renamed from: a */
        public final void mo4489a(final T t) {
            if (!this.f1536a.get()) {
                this.f1538c.mo4484a(new ztWNWCuZiM(this) {
                    /* renamed from: b */
                    final /* synthetic */ jjbQypPegg f1547b;

                    /* renamed from: a */
                    public final void mo4491a() {
                        try {
                            this.f1547b.mo4494b(t);
                        } catch (Throwable th) {
                            th.printStackTrace();
                        }
                    }
                });
            }
        }

        /* renamed from: a */
        public final void mo4490a(final Throwable th) {
            if (!m1616f()) {
                this.f1538c.mo4484a(new ztWNWCuZiM(this) {
                    /* renamed from: b */
                    final /* synthetic */ jjbQypPegg f1549b;

                    /* renamed from: a */
                    public final void mo4491a() {
                        try {
                            this.f1549b.mo4495b(th);
                        } catch (Throwable th) {
                            th.initCause(th);
                            th.printStackTrace();
                        } finally {
                            this.f1549b.m1615e();
                        }
                    }
                });
            }
        }

        /* renamed from: b */
        protected abstract void mo4493b();

        /* renamed from: b */
        protected abstract void mo4494b(T t);

        /* renamed from: b */
        protected abstract void mo4495b(Throwable th);

        /* renamed from: c */
        protected abstract void mo4496c();

        /* renamed from: d */
        public final void mo4492d() {
            if (m1616f()) {
                m1615e();
            }
        }
    }

    /* renamed from: im.getsocial.sdk.internal.e.a.pdwpUtZXDT$XdbacJlTDQ */
    private static class XdbacJlTDQ<T, R> extends jjbQypPegg<T> {
        /* renamed from: a */
        private final upgqDBbsrL<? super T, R> f1539a;
        /* renamed from: b */
        private ztWNWCuZiM<? super R> f1540b;

        public XdbacJlTDQ(XdbacJlTDQ xdbacJlTDQ, ztWNWCuZiM<? super R> ztwnwcuzim, upgqDBbsrL<? super T, R> upgqdbbsrl) {
            super(xdbacJlTDQ);
            this.f1540b = ztwnwcuzim;
            this.f1539a = upgqdbbsrl;
            m1618a((ztWNWCuZiM) ztwnwcuzim);
        }

        /* renamed from: b */
        protected final void mo4493b() {
            this.f1540b = null;
        }

        /* renamed from: b */
        public final void mo4494b(T t) {
            try {
                this.f1540b.mo4489a(this.f1539a.mo4344a(t));
            } catch (Throwable th) {
                mo4490a(th);
            }
        }

        /* renamed from: b */
        public final void mo4495b(Throwable th) {
            this.f1540b.mo4490a(th);
        }

        /* renamed from: c */
        public final void mo4496c() {
            this.f1540b.mo4488a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.e.a.pdwpUtZXDT$cjrhisSQCL */
    private static class cjrhisSQCL<T, R> extends jjbQypPegg<T> {
        /* renamed from: a */
        private final upgqDBbsrL<? super T, ? extends pdwpUtZXDT<? extends R>> f1543a;
        /* renamed from: b */
        private ztWNWCuZiM<? super R> f1544b;
        /* renamed from: c */
        private final AtomicInteger f1545c = new AtomicInteger(1);

        /* renamed from: im.getsocial.sdk.internal.e.a.pdwpUtZXDT$cjrhisSQCL$jjbQypPegg */
        private class jjbQypPegg extends ztWNWCuZiM<R> {
            /* renamed from: a */
            final /* synthetic */ cjrhisSQCL f1541a;
            /* renamed from: b */
            private ztWNWCuZiM<? super R> f1542b;

            public jjbQypPegg(cjrhisSQCL cjrhissqcl, ztWNWCuZiM<? super R> ztwnwcuzim) {
                this.f1541a = cjrhissqcl;
                this.f1542b = ztwnwcuzim;
            }

            /* renamed from: a */
            public final void mo4488a() {
                this.f1541a.mo4488a();
                this.f1542b = null;
            }

            /* renamed from: a */
            public final void mo4489a(R r) {
                this.f1542b.mo4489a((Object) r);
            }

            /* renamed from: a */
            public final void mo4490a(Throwable th) {
                this.f1541a.mo4490a(th);
                this.f1542b = null;
            }
        }

        public cjrhisSQCL(XdbacJlTDQ xdbacJlTDQ, ztWNWCuZiM<? super R> ztwnwcuzim, upgqDBbsrL<? super T, ? extends pdwpUtZXDT<? extends R>> upgqdbbsrl) {
            super(xdbacJlTDQ);
            this.f1544b = ztwnwcuzim;
            this.f1543a = upgqdbbsrl;
            m1618a((ztWNWCuZiM) ztwnwcuzim);
        }

        /* renamed from: a */
        public final void mo4488a() {
            if (this.f1545c.decrementAndGet() == 0) {
                super.mo4488a();
            }
        }

        /* renamed from: b */
        protected final void mo4493b() {
            this.f1544b = null;
        }

        /* renamed from: b */
        public final void mo4494b(T t) {
            this.f1545c.incrementAndGet();
            try {
                ((pdwpUtZXDT) this.f1543a.mo4344a(t)).m1667a(new jjbQypPegg(this, this.f1544b));
            } catch (Throwable th) {
                mo4490a(th);
            }
        }

        /* renamed from: b */
        public final void mo4495b(Throwable th) {
            this.f1544b.mo4490a(th);
        }

        /* renamed from: c */
        protected final void mo4496c() {
            this.f1544b.mo4488a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.e.a.pdwpUtZXDT$pdwpUtZXDT */
    private static class pdwpUtZXDT<T> extends jjbQypPegg<T> {
        /* renamed from: a */
        private ztWNWCuZiM<? super T> f1551a;

        pdwpUtZXDT(XdbacJlTDQ xdbacJlTDQ, ztWNWCuZiM<? super T> ztwnwcuzim) {
            super(xdbacJlTDQ);
            this.f1551a = ztwnwcuzim;
            m1618a((ztWNWCuZiM) ztwnwcuzim);
        }

        /* renamed from: b */
        protected final void mo4493b() {
            this.f1551a = null;
        }

        /* renamed from: b */
        protected final void mo4494b(T t) {
            this.f1551a.mo4489a((Object) t);
        }

        /* renamed from: b */
        protected final void mo4495b(Throwable th) {
            this.f1551a.mo4490a(th);
        }

        /* renamed from: c */
        protected final void mo4496c() {
            this.f1551a.mo4488a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.e.a.pdwpUtZXDT$upgqDBbsrL */
    private static final class upgqDBbsrL<T> implements jjbQypPegg<T> {
        private upgqDBbsrL() {
        }

        /* renamed from: a */
        public final void mo4455a(T t) {
        }
    }

    /* renamed from: im.getsocial.sdk.internal.e.a.pdwpUtZXDT$zoToeBNOjF */
    private static class zoToeBNOjF<T> extends jjbQypPegg<T> {
        /* renamed from: a */
        private final upgqDBbsrL<Throwable, pdwpUtZXDT<? extends T>> f1552a;
        /* renamed from: b */
        private ztWNWCuZiM<? super T> f1553b;

        public zoToeBNOjF(XdbacJlTDQ xdbacJlTDQ, ztWNWCuZiM<? super T> ztwnwcuzim, upgqDBbsrL<Throwable, pdwpUtZXDT<? extends T>> upgqdbbsrl) {
            super(xdbacJlTDQ);
            this.f1553b = ztwnwcuzim;
            this.f1552a = upgqdbbsrl;
            m1618a((ztWNWCuZiM) ztwnwcuzim);
        }

        /* renamed from: b */
        protected final void mo4493b() {
            this.f1553b = null;
        }

        /* renamed from: b */
        public final void mo4494b(T t) {
            this.f1553b.mo4489a((Object) t);
        }

        /* renamed from: b */
        public final void mo4495b(Throwable th) {
            try {
                ((pdwpUtZXDT) this.f1552a.mo4344a(th)).m1667a(this.f1553b);
            } catch (Throwable th2) {
                this.f1553b.mo4490a(th2);
            }
        }

        /* renamed from: c */
        public final void mo4496c() {
            this.f1553b.mo4488a();
        }
    }

    /* renamed from: im.getsocial.sdk.internal.e.a.pdwpUtZXDT$ztWNWCuZiM */
    private static class ztWNWCuZiM<T> extends jjbQypPegg<T> {
        /* renamed from: a */
        private final upgqDBbsrL<Throwable, pdwpUtZXDT<?>> f1554a;
        /* renamed from: b */
        private jjbQypPegg<pdwpUtZXDT<? extends T>> f1555b;
        /* renamed from: c */
        private XdbacJlTDQ f1556c;
        /* renamed from: d */
        private ztWNWCuZiM<? super T> f1557d;

        public ztWNWCuZiM(XdbacJlTDQ xdbacJlTDQ, XdbacJlTDQ xdbacJlTDQ2, ztWNWCuZiM<? super T> ztwnwcuzim, upgqDBbsrL<Throwable, pdwpUtZXDT<?>> upgqdbbsrl, jjbQypPegg<pdwpUtZXDT<? extends T>> jjbqyppegg) {
            super(xdbacJlTDQ);
            this.f1556c = xdbacJlTDQ2;
            this.f1557d = ztwnwcuzim;
            this.f1554a = upgqdbbsrl;
            this.f1555b = jjbqyppegg;
            m1618a((ztWNWCuZiM) ztwnwcuzim);
        }

        /* renamed from: b */
        protected final void mo4493b() {
            this.f1555b = null;
            this.f1557d = null;
            this.f1556c = null;
        }

        /* renamed from: b */
        public final void mo4494b(T t) {
            this.f1557d.mo4489a((Object) t);
        }

        /* renamed from: b */
        public final void mo4495b(Throwable th) {
            try {
                ((pdwpUtZXDT) this.f1554a.mo4344a(th)).m1664a(this.f1556c).m1665a(this.f1555b).m1667a(this.f1557d);
            } catch (Throwable th2) {
                this.f1557d.mo4490a(th2);
            }
        }

        /* renamed from: c */
        public final void mo4496c() {
            this.f1557d.mo4488a();
        }
    }

    private pdwpUtZXDT(KSZKMmRWhZ<ztWNWCuZiM<? super T>> kSZKMmRWhZ, XdbacJlTDQ xdbacJlTDQ, XdbacJlTDQ xdbacJlTDQ2) {
        this.f1558a = kSZKMmRWhZ;
        this.f1559b = xdbacJlTDQ;
        this.f1560c = xdbacJlTDQ2;
    }

    /* renamed from: a */
    public static pdwpUtZXDT<Void> m1656a() {
        return pdwpUtZXDT.m1659a(null);
    }

    /* renamed from: a */
    public static <T> pdwpUtZXDT<T> m1657a(final long j, final TimeUnit timeUnit) {
        return pdwpUtZXDT.m1658a(new KSZKMmRWhZ<ztWNWCuZiM<? super T>>() {
            /* renamed from: b */
            public final /* synthetic */ void mo4412b(Object obj) {
                ztWNWCuZiM ztwnwcuzim = (ztWNWCuZiM) obj;
                try {
                    Thread.sleep(timeUnit.toMillis(j));
                } catch (InterruptedException e) {
                }
                ztwnwcuzim.mo4489a(null);
                ztwnwcuzim.mo4488a();
            }
        });
    }

    /* renamed from: a */
    public static <T> pdwpUtZXDT<T> m1658a(KSZKMmRWhZ<ztWNWCuZiM<? super T>> kSZKMmRWhZ) {
        return new pdwpUtZXDT(kSZKMmRWhZ, zoToeBNOjF.m1675b(), zoToeBNOjF.m1675b());
    }

    /* renamed from: a */
    public static <T> pdwpUtZXDT<T> m1659a(final T t) {
        return pdwpUtZXDT.m1658a(new KSZKMmRWhZ<ztWNWCuZiM<? super T>>() {
            /* renamed from: b */
            public final /* synthetic */ void mo4412b(Object obj) {
                ztWNWCuZiM ztwnwcuzim = (ztWNWCuZiM) obj;
                ztwnwcuzim.mo4489a(t);
                ztwnwcuzim.mo4488a();
            }
        });
    }

    /* renamed from: a */
    public static <T> pdwpUtZXDT<T> m1660a(final Throwable th) {
        return pdwpUtZXDT.m1658a(new KSZKMmRWhZ<ztWNWCuZiM<? super T>>() {
            /* renamed from: b */
            public final /* synthetic */ void mo4412b(Object obj) {
                ((ztWNWCuZiM) obj).mo4490a(th);
            }
        });
    }

    /* renamed from: a */
    static /* synthetic */ ztWNWCuZiM m1661a(pdwpUtZXDT pdwputzxdt, ztWNWCuZiM ztwnwcuzim) {
        return jjbQypPegg.class.isInstance(ztwnwcuzim) ? (jjbQypPegg) ztwnwcuzim : new pdwpUtZXDT(pdwputzxdt.f1559b, ztwnwcuzim);
    }

    /* renamed from: a */
    public final pdwpUtZXDT<T> m1664a(XdbacJlTDQ xdbacJlTDQ) {
        return new pdwpUtZXDT(this.f1558a, xdbacJlTDQ, this.f1560c);
    }

    /* renamed from: a */
    public final <R> pdwpUtZXDT<R> m1665a(final upgqDBbsrL<? super T, ? extends pdwpUtZXDT<? extends R>> upgqdbbsrl) {
        return pdwpUtZXDT.m1658a(new KSZKMmRWhZ<ztWNWCuZiM<? super R>>(this) {
            /* renamed from: b */
            final /* synthetic */ pdwpUtZXDT f1535b;

            /* renamed from: b */
            public final /* synthetic */ void mo4412b(Object obj) {
                this.f1535b.m1667a(new cjrhisSQCL(this.f1535b.f1559b, (ztWNWCuZiM) obj, upgqdbbsrl));
            }
        });
    }

    /* renamed from: a */
    public final void m1666a(final jjbQypPegg<? super T> jjbqyppegg, final jjbQypPegg<? super Throwable> jjbqyppegg2) {
        m1667a(new ztWNWCuZiM<T>(this) {
            /* renamed from: c */
            final /* synthetic */ pdwpUtZXDT f1523c;

            /* renamed from: a */
            public final void mo4488a() {
                this.f1523c.f1559b.mo4483a();
                this.f1523c.f1560c.mo4483a();
                this.f1523c.f1559b = null;
                this.f1523c.f1560c = null;
            }

            /* renamed from: a */
            public final void mo4489a(T t) {
                jjbqyppegg.mo4455a(t);
            }

            /* renamed from: a */
            public final void mo4490a(Throwable th) {
                jjbqyppegg2.mo4455a(th);
                this.f1523c.f1559b.mo4483a();
                this.f1523c.f1560c.mo4483a();
                this.f1523c.f1559b = null;
                this.f1523c.f1560c = null;
            }
        });
    }

    /* renamed from: a */
    public final void m1667a(final ztWNWCuZiM<? super T> ztwnwcuzim) {
        this.f1560c.mo4484a(new ztWNWCuZiM(this) {
            /* renamed from: b */
            final /* synthetic */ pdwpUtZXDT f1528b;

            /* renamed from: a */
            public final void mo4491a() {
                final ztWNWCuZiM a = pdwpUtZXDT.m1661a(this.f1528b, ztwnwcuzim);
                try {
                    this.f1528b.f1558a.mo4412b(a);
                } catch (Throwable th) {
                    th.initCause(th);
                    th.printStackTrace();
                }
            }
        });
    }

    /* renamed from: b */
    public final pdwpUtZXDT<T> m1668b(XdbacJlTDQ xdbacJlTDQ) {
        return new pdwpUtZXDT(this.f1558a, this.f1559b, xdbacJlTDQ);
    }

    /* renamed from: b */
    public final <R> pdwpUtZXDT<R> m1669b(final upgqDBbsrL<? super T, R> upgqdbbsrl) {
        return pdwpUtZXDT.m1658a(new KSZKMmRWhZ<ztWNWCuZiM<? super R>>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT f1515b;

            /* renamed from: b */
            public final /* synthetic */ void mo4412b(Object obj) {
                this.f1515b.m1667a(new XdbacJlTDQ(this.f1515b.f1559b, (im.getsocial.sdk.internal.p030e.p065a.ztWNWCuZiM) obj, upgqdbbsrl));
            }
        });
    }

    /* renamed from: c */
    public final pdwpUtZXDT<T> m1670c(final upgqDBbsrL<Throwable, pdwpUtZXDT<? extends T>> upgqdbbsrl) {
        return pdwpUtZXDT.m1658a(new KSZKMmRWhZ<ztWNWCuZiM<? super T>>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT f1517b;

            /* renamed from: b */
            public final /* synthetic */ void mo4412b(Object obj) {
                this.f1517b.m1667a(new zoToeBNOjF(this.f1517b.f1559b, (im.getsocial.sdk.internal.p030e.p065a.ztWNWCuZiM) obj, upgqdbbsrl));
            }
        });
    }

    /* renamed from: d */
    public final pdwpUtZXDT<T> m1671d(final upgqDBbsrL<Throwable, pdwpUtZXDT<?>> upgqdbbsrl) {
        return pdwpUtZXDT.m1658a(new KSZKMmRWhZ<ztWNWCuZiM<? super T>>(this) {
            /* renamed from: b */
            final /* synthetic */ im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT f1520b;

            /* renamed from: im.getsocial.sdk.internal.e.a.pdwpUtZXDT$12$1 */
            class C09731 extends jjbQypPegg<pdwpUtZXDT<? extends T>> {
                /* renamed from: a */
                final /* synthetic */ AnonymousClass12 f1518a;

                C09731(AnonymousClass12 anonymousClass12) {
                    this.f1518a = anonymousClass12;
                }

                /* renamed from: a */
                public final /* synthetic */ Object mo4487a() {
                    return pdwpUtZXDT.m1658a(this.f1518a.f1520b.f1558a).m1671d(upgqdbbsrl);
                }
            }

            /* renamed from: b */
            public final /* synthetic */ void mo4412b(Object obj) {
                this.f1520b.m1667a(new im.getsocial.sdk.internal.p030e.p065a.pdwpUtZXDT.ztWNWCuZiM(this.f1520b.f1559b, this.f1520b.f1560c, (im.getsocial.sdk.internal.p030e.p065a.ztWNWCuZiM) obj, upgqdbbsrl, new C09731(this)));
            }
        });
    }
}
