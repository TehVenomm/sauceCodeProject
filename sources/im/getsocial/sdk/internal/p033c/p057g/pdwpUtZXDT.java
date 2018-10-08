package im.getsocial.sdk.internal.p033c.p057g;

import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL.jjbQypPegg;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.io.Writer;

/* renamed from: im.getsocial.sdk.internal.c.g.pdwpUtZXDT */
public final class pdwpUtZXDT {
    /* renamed from: a */
    private static final jjbQypPegg f1285a;
    /* renamed from: b */
    private static jjbQypPegg f1286b;
    /* renamed from: c */
    private static zoToeBNOjF f1287c = new jjbQypPegg();

    static {
        jjbQypPegg jjbqyppegg = jjbQypPegg.WARN;
        f1285a = jjbqyppegg;
        f1286b = jjbqyppegg;
    }

    private pdwpUtZXDT() {
    }

    /* renamed from: a */
    static jjbQypPegg m1269a() {
        return f1286b;
    }

    /* renamed from: a */
    static String m1270a(Throwable th) {
        Writer stringWriter = new StringWriter();
        PrintWriter printWriter = new PrintWriter(stringWriter);
        th.printStackTrace(printWriter);
        printWriter.flush();
        return stringWriter.toString();
    }

    /* renamed from: a */
    public static void m1271a(jjbQypPegg jjbqyppegg) {
        f1286b = jjbqyppegg;
    }

    /* renamed from: a */
    public static void m1272a(zoToeBNOjF zotoebnojf) {
        f1287c = zotoebnojf;
    }

    /* renamed from: b */
    public static zoToeBNOjF m1273b() {
        return f1287c;
    }
}
