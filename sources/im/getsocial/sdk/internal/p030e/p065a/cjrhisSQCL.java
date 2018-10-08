package im.getsocial.sdk.internal.p030e.p065a;

import java.io.PrintStream;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;
import org.apache.commons.lang3.StringUtils;

/* renamed from: im.getsocial.sdk.internal.e.a.cjrhisSQCL */
public final class cjrhisSQCL extends RuntimeException {
    /* renamed from: a */
    private final List<Throwable> f1511a;
    /* renamed from: b */
    private Throwable f1512b;
    /* renamed from: c */
    private final String f1513c;

    /* renamed from: im.getsocial.sdk.internal.e.a.cjrhisSQCL$upgqDBbsrL */
    private static abstract class upgqDBbsrL {
        private upgqDBbsrL() {
        }

        /* renamed from: a */
        abstract Object mo4485a();

        /* renamed from: a */
        abstract void mo4486a(Object obj);
    }

    /* renamed from: im.getsocial.sdk.internal.e.a.cjrhisSQCL$cjrhisSQCL */
    private static class cjrhisSQCL extends upgqDBbsrL {
        /* renamed from: a */
        private final PrintStream f1508a;

        cjrhisSQCL(PrintStream printStream) {
            super();
            this.f1508a = printStream;
        }

        /* renamed from: a */
        final Object mo4485a() {
            return this.f1508a;
        }

        /* renamed from: a */
        final void mo4486a(Object obj) {
            this.f1508a.println(obj);
        }
    }

    /* renamed from: im.getsocial.sdk.internal.e.a.cjrhisSQCL$jjbQypPegg */
    static final class jjbQypPegg extends RuntimeException {
        /* renamed from: a */
        static String f1509a = "Chain of Causes for CompositeException In Order Received =>";

        jjbQypPegg() {
        }

        public final String getMessage() {
            return f1509a;
        }
    }

    /* renamed from: im.getsocial.sdk.internal.e.a.cjrhisSQCL$pdwpUtZXDT */
    private static class pdwpUtZXDT extends upgqDBbsrL {
        /* renamed from: a */
        private final PrintWriter f1510a;

        pdwpUtZXDT(PrintWriter printWriter) {
            super();
            this.f1510a = printWriter;
        }

        /* renamed from: a */
        final Object mo4485a() {
            return this.f1510a;
        }

        /* renamed from: a */
        final void mo4486a(Object obj) {
            this.f1510a.println(obj);
        }
    }

    /* renamed from: a */
    private static List<Throwable> m1587a(Throwable th) {
        List arrayList = new ArrayList();
        Throwable cause = th.getCause();
        if (cause != null) {
            while (true) {
                arrayList.add(cause);
                if (cause.getCause() == null) {
                    break;
                }
                cause = cause.getCause();
            }
        }
        return arrayList;
    }

    /* renamed from: a */
    private void m1588a(upgqDBbsrL upgqdbbsrl) {
        int i;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(this).append(StringUtils.LF);
        for (Object append : getStackTrace()) {
            stringBuilder.append("\tat ").append(append).append(StringUtils.LF);
        }
        for (i = 0; i < this.f1511a.size(); i++) {
            Throwable th = (Throwable) this.f1511a.get(i);
            stringBuilder.append("  ComposedException ").append(i).append(" :\n");
            m1589a(stringBuilder, th, "\t");
        }
        synchronized (upgqdbbsrl.mo4485a()) {
            upgqdbbsrl.mo4486a(stringBuilder.toString());
        }
    }

    /* renamed from: a */
    private void m1589a(StringBuilder stringBuilder, Throwable th, String str) {
        while (true) {
            stringBuilder.append(str).append(th).append(StringUtils.LF);
            for (Object append : th.getStackTrace()) {
                stringBuilder.append("\t\tat ").append(append).append(StringUtils.LF);
            }
            if (th.getCause() != null) {
                stringBuilder.append("\tCaused by: ");
                th = th.getCause();
                str = "";
            } else {
                return;
            }
        }
    }

    public final Throwable getCause() {
        if (this.f1512b == null) {
            Throwable jjbqyppegg = new jjbQypPegg();
            Set hashSet = new HashSet();
            Throwable th = jjbqyppegg;
            for (Throwable th2 : this.f1511a) {
                if (!hashSet.contains(th2)) {
                    hashSet.add(th2);
                    Throwable th3 = th2;
                    for (Throwable th22 : cjrhisSQCL.m1587a(th22)) {
                        if (hashSet.contains(th22)) {
                            th3 = new RuntimeException("Duplicate found in causal chain so cropping to prevent loop ...");
                        } else {
                            hashSet.add(th22);
                        }
                    }
                    try {
                        th.initCause(th3);
                        th = th.getCause();
                    } catch (Throwable th4) {
                        th = th3;
                    }
                }
            }
            this.f1512b = jjbqyppegg;
        }
        return this.f1512b;
    }

    public final String getMessage() {
        return this.f1513c;
    }

    public final void printStackTrace() {
        printStackTrace(System.err);
    }

    public final void printStackTrace(PrintStream printStream) {
        m1588a(new cjrhisSQCL(printStream));
    }

    public final void printStackTrace(PrintWriter printWriter) {
        m1588a(new pdwpUtZXDT(printWriter));
    }
}
