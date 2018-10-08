package im.getsocial.p026c.p027a;

import com.github.droidfu.support.DisplaySupport;
import java.io.InputStream;
import java.util.Map;
import java.util.Map.Entry;

/* renamed from: im.getsocial.c.a.fOrCGNYyfk */
public class fOrCGNYyfk {
    /* renamed from: a */
    private long f1081a;
    /* renamed from: b */
    private InputStream f1082b;
    /* renamed from: c */
    private KSZKMmRWhZ f1083c;
    /* renamed from: d */
    private Map<String, String> f1084d;

    /* renamed from: a */
    private static String m883a(byte[] bArr) {
        StringBuilder stringBuilder = new StringBuilder((bArr.length << 2) / 3);
        for (int i = 0; i < bArr.length; i += 3) {
            stringBuilder.append("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt((bArr[i] & 252) >> 2));
            int i2 = (bArr[i] & 3) << 4;
            if (i + 1 < bArr.length) {
                stringBuilder.append("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt(i2 | ((bArr[i + 1] & DisplaySupport.SCREEN_DENSITY_HIGH) >> 4)));
                i2 = (bArr[i + 1] & 15) << 2;
                if (i + 2 < bArr.length) {
                    stringBuilder.append("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt(i2 | ((bArr[i + 2] & 192) >> 6)));
                    stringBuilder.append("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt(bArr[i + 2] & 63));
                } else {
                    stringBuilder.append("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt(i2));
                    stringBuilder.append('=');
                }
            } else {
                stringBuilder.append("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".charAt(i2));
                stringBuilder.append("==");
            }
        }
        return stringBuilder.toString();
    }

    /* renamed from: a */
    public final long m884a() {
        return this.f1081a;
    }

    /* renamed from: a */
    public final void m885a(long j) {
        this.f1081a = j;
    }

    /* renamed from: a */
    public final void m886a(InputStream inputStream) {
        this.f1082b = inputStream;
        this.f1083c = new KSZKMmRWhZ(inputStream);
    }

    /* renamed from: a */
    public final void m887a(Map<String, String> map) {
        this.f1084d = map;
    }

    /* renamed from: b */
    public final KSZKMmRWhZ m888b() {
        return this.f1083c;
    }

    /* renamed from: c */
    public final String m889c() {
        if (this.f1084d == null || this.f1084d.size() == 0) {
            return "";
        }
        String str = "";
        Object obj = 1;
        for (Entry entry : this.f1084d.entrySet()) {
            str = (obj == null ? str + "," : str) + ((String) entry.getKey()) + " " + fOrCGNYyfk.m883a(((String) entry.getValue()).getBytes());
            obj = null;
        }
        return str;
    }
}
