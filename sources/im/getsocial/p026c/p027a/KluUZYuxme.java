package im.getsocial.p026c.p027a;

import com.google.android.gms.nearby.messages.Strategy;
import io.fabric.sdk.android.services.network.HttpRequest;
import java.io.OutputStream;
import java.net.HttpCookie;
import java.net.HttpURLConnection;
import java.net.ProtocolException;
import java.net.URL;
import java.net.URLConnection;
import java.util.Locale;
import jp.colopl.drapro.BuildConfig;

/* renamed from: im.getsocial.c.a.KluUZYuxme */
public class KluUZYuxme {
    /* renamed from: a */
    private URL f1071a;
    /* renamed from: b */
    private KSZKMmRWhZ f1072b;
    /* renamed from: c */
    private long f1073c;
    /* renamed from: d */
    private byte[] f1074d;
    /* renamed from: e */
    private int f1075e = 1073741824;
    /* renamed from: f */
    private int f1076f;
    /* renamed from: g */
    private HttpCookie f1077g;
    /* renamed from: h */
    private HttpURLConnection f1078h;
    /* renamed from: i */
    private OutputStream f1079i;

    public KluUZYuxme(URL url, KSZKMmRWhZ kSZKMmRWhZ, long j, HttpCookie httpCookie) {
        this.f1071a = url;
        this.f1072b = kSZKMmRWhZ;
        this.f1073c = j;
        this.f1077g = httpCookie;
        kSZKMmRWhZ.m873a(j);
        this.f1074d = new byte[2097152];
    }

    /* renamed from: a */
    private static long m874a(URLConnection uRLConnection, String str) {
        long j = -1;
        String headerField = uRLConnection.getHeaderField(str);
        if (headerField != null) {
            try {
                j = Long.parseLong(headerField);
            } catch (NumberFormatException e) {
            }
        }
        return j;
    }

    /* renamed from: e */
    private void m875e() {
        if (this.f1079i != null) {
            this.f1079i.close();
        }
        if (this.f1078h != null) {
            int responseCode = this.f1078h.getResponseCode();
            this.f1078h.disconnect();
            if (responseCode < 200 || responseCode >= Strategy.TTL_SECONDS_DEFAULT) {
                throw new cjrhisSQCL("unexpected status code (" + responseCode + ") while uploading chunk", this.f1078h);
            }
            long a = KluUZYuxme.m874a(this.f1078h, "Upload-Offset");
            if (a == -1) {
                throw new cjrhisSQCL("response to PATCH request contains no or invalid Upload-Offset header", this.f1078h);
            } else if (this.f1073c != a) {
                throw new cjrhisSQCL(String.format(Locale.ENGLISH, "response contains different Upload-Offset value (%d) than expected (%d)", new Object[]{Long.valueOf(a), Long.valueOf(this.f1073c)}), this.f1078h);
            } else {
                this.f1078h = null;
            }
        }
    }

    /* renamed from: a */
    public final int m876a() {
        if (this.f1078h == null) {
            this.f1076f = this.f1075e;
            this.f1072b.m872a(this.f1075e);
            this.f1078h = (HttpURLConnection) this.f1071a.openConnection();
            if (this.f1077g != null) {
                this.f1078h.setRequestProperty("Cookie", this.f1077g.toString());
            }
            this.f1078h.setRequestProperty("Upload-Offset", Long.toString(this.f1073c));
            this.f1078h.setRequestProperty(HttpRequest.HEADER_CONTENT_TYPE, "application/offset+octet-stream");
            this.f1078h.setRequestProperty("Expect", "100-continue");
            this.f1078h.addRequestProperty("Tus-Resumable", BuildConfig.VERSION_NAME);
            this.f1078h.setReadTimeout(0);
            try {
                this.f1078h.setRequestMethod("PATCH");
            } catch (ProtocolException e) {
                this.f1078h.setRequestMethod(HttpRequest.METHOD_POST);
                this.f1078h.setRequestProperty("X-HTTP-Method-Override", "PATCH");
            }
            this.f1078h.setDoOutput(true);
            this.f1078h.setChunkedStreamingMode(this.f1074d.length);
            try {
                this.f1079i = this.f1078h.getOutputStream();
            } catch (ProtocolException e2) {
                if (this.f1078h.getResponseCode() != -1) {
                    m880d();
                }
                throw e2;
            }
        }
        int a = this.f1072b.m870a(this.f1074d, Math.min(this.f1074d.length, this.f1076f));
        if (a == -1) {
            return -1;
        }
        this.f1079i.write(this.f1074d, 0, a);
        this.f1079i.flush();
        this.f1073c += (long) a;
        this.f1076f -= a;
        if (this.f1076f > 0) {
            return a;
        }
        m875e();
        return a;
    }

    /* renamed from: a */
    public final void m877a(int i) {
        this.f1074d = new byte[i];
    }

    /* renamed from: b */
    public final long m878b() {
        return this.f1073c;
    }

    /* renamed from: c */
    public final URL m879c() {
        return this.f1071a;
    }

    /* renamed from: d */
    public final void m880d() {
        m875e();
        this.f1072b.m871a();
    }
}
