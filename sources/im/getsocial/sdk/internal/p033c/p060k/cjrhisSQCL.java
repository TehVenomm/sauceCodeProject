package im.getsocial.sdk.internal.p033c.p060k;

import im.getsocial.p018b.p021c.p024c.jjbQypPegg;
import io.fabric.sdk.android.services.network.HttpRequest;
import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;

/* renamed from: im.getsocial.sdk.internal.c.k.cjrhisSQCL */
public class cjrhisSQCL extends jjbQypPegg {
    /* renamed from: a */
    private HttpURLConnection f1343a;
    /* renamed from: b */
    private InputStream f1344b;
    /* renamed from: c */
    private final ByteArrayOutputStream f1345c = new ByteArrayOutputStream();
    /* renamed from: d */
    private final String f1346d;
    /* renamed from: e */
    private final String f1347e;
    /* renamed from: f */
    private final String f1348f;
    /* renamed from: g */
    private final int f1349g;
    /* renamed from: h */
    private final int f1350h;

    /* renamed from: im.getsocial.sdk.internal.c.k.cjrhisSQCL$jjbQypPegg */
    public static class jjbQypPegg {
        /* renamed from: a */
        private final String f1338a;
        /* renamed from: b */
        private int f1339b = 0;
        /* renamed from: c */
        private boolean f1340c;
        /* renamed from: d */
        private int f1341d;
        /* renamed from: e */
        private String f1342e = "GetSocialSDK/HttpTransport";

        public jjbQypPegg(String str) {
            if (str == null || str.length() == 0) {
                throw new NullPointerException("host");
            }
            this.f1338a = str;
            this.f1339b = 0;
        }

        /* renamed from: a */
        public final jjbQypPegg m1364a(int i) {
            this.f1339b = 30000;
            return this;
        }

        /* renamed from: a */
        public final jjbQypPegg m1365a(String str) {
            this.f1342e = str;
            return this;
        }

        /* renamed from: a */
        public final jjbQypPegg m1366a(boolean z) {
            this.f1340c = z;
            return this;
        }

        /* renamed from: a */
        public final cjrhisSQCL m1367a() {
            return new cjrhisSQCL(this);
        }

        /* renamed from: b */
        public final jjbQypPegg m1368b(int i) {
            this.f1341d = 30000;
            return this;
        }
    }

    cjrhisSQCL(jjbQypPegg jjbqyppegg) {
        this.f1346d = jjbqyppegg.f1340c ? "https://" : "http://";
        this.f1348f = jjbqyppegg.f1342e;
        this.f1347e = jjbqyppegg.f1338a;
        this.f1349g = jjbqyppegg.f1339b;
        this.f1350h = jjbqyppegg.f1341d;
    }

    /* renamed from: a */
    public final int mo4406a(byte[] bArr, int i, int i2) {
        return this.f1344b.read(bArr, i, i2);
    }

    /* renamed from: a */
    public final String m1370a() {
        return this.f1347e;
    }

    /* renamed from: b */
    public final void mo4407b() {
        byte[] toByteArray = this.f1345c.toByteArray();
        this.f1345c.reset();
        this.f1343a.getOutputStream().write(toByteArray);
        this.f1344b = this.f1343a.getInputStream();
    }

    /* renamed from: b */
    public final void mo4408b(byte[] bArr, int i, int i2) {
        this.f1345c.write(bArr, 0, i2);
    }

    /* renamed from: c */
    public final void m1373c() {
        this.f1343a = (HttpURLConnection) new URL(this.f1346d + this.f1347e).openConnection();
        this.f1343a.setConnectTimeout(this.f1349g);
        this.f1343a.setReadTimeout(this.f1350h);
        this.f1343a.setRequestMethod(HttpRequest.METHOD_POST);
        this.f1343a.setRequestProperty(HttpRequest.HEADER_CONTENT_TYPE, "application/x-thrift");
        this.f1343a.setRequestProperty("Accept", "application/x-thrift");
        this.f1343a.setRequestProperty("User-Agent", this.f1348f);
        this.f1343a.setDoOutput(true);
        this.f1343a.setUseCaches(false);
    }

    public void close() {
        this.f1343a.disconnect();
    }
}
