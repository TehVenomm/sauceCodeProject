package im.getsocial.sdk.ui.internal.p131d;

import im.getsocial.p015a.p016a.p017a.cjrhisSQCL;
import im.getsocial.p015a.p016a.pdwpUtZXDT;
import im.getsocial.sdk.ui.internal.p131d.p132a.sqEuGXwfLT;
import im.getsocial.sdk.ui.internal.p131d.p133b.upgqDBbsrL;
import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.nio.charset.Charset;

/* renamed from: im.getsocial.sdk.ui.internal.d.XdbacJlTDQ */
public final class XdbacJlTDQ {
    /* renamed from: a */
    private static XdbacJlTDQ f2785a;
    /* renamed from: b */
    private final cjrhisSQCL f2786b = new cjrhisSQCL();

    private XdbacJlTDQ() {
        jjbQypPegg.m3233a();
    }

    /* renamed from: a */
    public static XdbacJlTDQ m3101a() {
        synchronized (XdbacJlTDQ.class) {
            try {
                if (f2785a == null) {
                    f2785a = new XdbacJlTDQ();
                }
            } catch (Throwable th) {
                while (true) {
                    Class cls = XdbacJlTDQ.class;
                }
            }
        }
        return f2785a;
    }

    /* renamed from: a */
    public static sqEuGXwfLT m3102a(pdwpUtZXDT pdwputzxdt) {
        return (sqEuGXwfLT) upgqDBbsrL.m3218a(pdwputzxdt, sqEuGXwfLT.class);
    }

    /* renamed from: a */
    public final sqEuGXwfLT m3103a(InputStream inputStream) {
        try {
            return XdbacJlTDQ.m3102a((pdwpUtZXDT) this.f2786b.m720a(new BufferedReader(new InputStreamReader(inputStream, Charset.forName("UTF-8"))), null));
        } catch (Throwable e) {
            throw new RuntimeException("Ui Configuration could not be parsed. Please check that it is well formed.", e);
        }
    }
}
