package im.getsocial.sdk.internal.p033c;

import android.content.Context;
import android.content.pm.PackageManager.NameNotFoundException;
import com.facebook.appevents.AppEventsConstants;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import java.io.ByteArrayInputStream;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.security.cert.CertificateException;
import java.security.cert.CertificateFactory;
import java.security.cert.X509Certificate;
import java.util.Locale;

/* renamed from: im.getsocial.sdk.internal.c.jjbQypPegg */
public final class jjbQypPegg implements zoToeBNOjF {
    /* renamed from: a */
    private static final cjrhisSQCL f1319a = upgqDBbsrL.m1274a(jjbQypPegg.class);
    /* renamed from: b */
    private static String f1320b = "";
    /* renamed from: c */
    private final Context f1321c;

    @XdbacJlTDQ
    jjbQypPegg(Context context) {
        this.f1321c = context;
    }

    /* renamed from: a */
    private static String m1329a(Context context) {
        if (im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1517c(f1320b)) {
            try {
                f1320b = jjbQypPegg.m1330a(MessageDigest.getInstance("SHA256").digest(((X509Certificate) CertificateFactory.getInstance("X509").generateCertificate(new ByteArrayInputStream(context.getPackageManager().getPackageInfo(context.getPackageName(), 64).signatures[0].toByteArray()))).getEncoded()));
                f1319a.mo4387a("App signature fingerprint: " + f1320b);
            } catch (NameNotFoundException e) {
                f1319a.mo4394c("Failed to get signing certificate SHA256 fingerprint", e);
            } catch (NoSuchAlgorithmException e2) {
                f1319a.mo4394c("Failed to get signing certificate SHA256 fingerprint", e2);
            } catch (CertificateException e3) {
                f1319a.mo4394c("Failed to get signing certificate SHA256 fingerprint", e3);
            }
        }
        return f1320b;
    }

    /* renamed from: a */
    private static String m1330a(byte[] bArr) {
        StringBuilder stringBuilder = new StringBuilder(bArr.length << 1);
        for (int i = 0; i < bArr.length; i++) {
            String toHexString = Integer.toHexString(bArr[i]);
            int length = toHexString.length();
            if (length == 1) {
                toHexString = new StringBuilder(AppEventsConstants.EVENT_PARAM_VALUE_NO).append(toHexString).toString();
            }
            if (length > 2) {
                toHexString = toHexString.substring(length - 2, length);
            }
            stringBuilder.append(toHexString.toUpperCase(Locale.US));
            if (i < bArr.length - 1) {
                stringBuilder.append(':');
            }
        }
        return stringBuilder.toString();
    }

    /* renamed from: a */
    public final String mo4402a() {
        return String.format("Package: %s, Signing-certificate fingerprint: %s", new Object[]{this.f1321c.getPackageName(), jjbQypPegg.m1329a(this.f1321c)});
    }

    /* renamed from: b */
    public final String mo4403b() {
        return jjbQypPegg.m1329a(this.f1321c);
    }
}
