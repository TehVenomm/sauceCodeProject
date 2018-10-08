package im.getsocial.p026c.p027a;

import com.google.android.gms.nearby.messages.Strategy;
import io.fabric.sdk.android.services.network.HttpRequest;
import java.net.HttpCookie;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.List;
import javax.annotation.Nonnull;
import jp.colopl.drapro.BuildConfig;

/* renamed from: im.getsocial.c.a.zoToeBNOjF */
public class zoToeBNOjF {
    /* renamed from: a */
    private URL f1093a;
    /* renamed from: b */
    private boolean f1094b;
    /* renamed from: c */
    private qZypgoeblR f1095c;
    /* renamed from: d */
    private int f1096d = 5000;

    /* renamed from: a */
    private void m900a(@Nonnull HttpURLConnection httpURLConnection) {
        httpURLConnection.setInstanceFollowRedirects(Boolean.getBoolean("http.strictPostRedirect"));
        httpURLConnection.setConnectTimeout(this.f1096d);
        httpURLConnection.addRequestProperty("Tus-Resumable", BuildConfig.VERSION_NAME);
    }

    /* renamed from: b */
    private KluUZYuxme m901b(@Nonnull fOrCGNYyfk forcgnyyfk) {
        HttpURLConnection httpURLConnection = (HttpURLConnection) this.f1093a.openConnection();
        httpURLConnection.setRequestMethod(HttpRequest.METHOD_POST);
        m900a(httpURLConnection);
        String c = forcgnyyfk.m889c();
        if (c.length() > 0) {
            httpURLConnection.setRequestProperty("Upload-Metadata", c);
        }
        httpURLConnection.addRequestProperty("Upload-Length", Long.toString(forcgnyyfk.m884a()));
        httpURLConnection.connect();
        int responseCode = httpURLConnection.getResponseCode();
        if (responseCode < 200 || responseCode >= Strategy.TTL_SECONDS_DEFAULT) {
            throw new cjrhisSQCL("unexpected status code (" + responseCode + ") while creating upload", httpURLConnection);
        }
        c = httpURLConnection.getHeaderField("Location");
        if (c == null || c.length() == 0) {
            throw new cjrhisSQCL("missing upload URL in response for creating upload", httpURLConnection);
        }
        URL url = new URL(httpURLConnection.getURL(), c);
        HttpCookie b = zoToeBNOjF.m902b(httpURLConnection);
        if (this.f1094b) {
            this.f1095c.mo4339a(null, url);
            if (b != null) {
                this.f1095c.mo4338a(null, b);
            }
        }
        return new KluUZYuxme(url, forcgnyyfk.m888b(), 0, b);
    }

    /* renamed from: b */
    private static HttpCookie m902b(@Nonnull HttpURLConnection httpURLConnection) {
        List<String> list = (List) httpURLConnection.getHeaderFields().get("Set-Cookie");
        if (list != null) {
            for (String parse : list) {
                HttpCookie httpCookie = (HttpCookie) HttpCookie.parse(parse).get(0);
                if (httpCookie != null && "AWSALB".equalsIgnoreCase(httpCookie.getName())) {
                    return httpCookie;
                }
            }
        }
        return null;
    }

    /* renamed from: a */
    public final KluUZYuxme m903a(@Nonnull fOrCGNYyfk forcgnyyfk) {
        try {
            if (this.f1094b) {
                URL a = this.f1095c.mo4337a(null);
                if (a == null) {
                    throw new jjbQypPegg(null);
                }
                HttpCookie b = this.f1095c.mo4340b(null);
                HttpURLConnection httpURLConnection = (HttpURLConnection) a.openConnection();
                httpURLConnection.setRequestMethod(HttpRequest.METHOD_HEAD);
                m900a(httpURLConnection);
                if (b != null) {
                    httpURLConnection.setRequestProperty("Cookie", b.toString());
                }
                httpURLConnection.connect();
                int responseCode = httpURLConnection.getResponseCode();
                if (responseCode < 200 || responseCode >= Strategy.TTL_SECONDS_DEFAULT) {
                    throw new cjrhisSQCL("unexpected status code (" + responseCode + ") while resuming upload", httpURLConnection);
                }
                HttpCookie b2 = zoToeBNOjF.m902b(httpURLConnection);
                if (this.f1094b) {
                    this.f1095c.mo4338a(null, b2);
                }
                String headerField = httpURLConnection.getHeaderField("Upload-Offset");
                if (headerField == null || headerField.length() == 0) {
                    throw new cjrhisSQCL("missing upload offset in response for resuming upload", httpURLConnection);
                }
                return new KluUZYuxme(a, forcgnyyfk.m888b(), Long.parseLong(headerField), b2);
            }
            throw new XdbacJlTDQ();
        } catch (jjbQypPegg e) {
            return m901b(forcgnyyfk);
        } catch (XdbacJlTDQ e2) {
            return m901b(forcgnyyfk);
        } catch (cjrhisSQCL e3) {
            HttpURLConnection a2 = e3.m881a();
            if (a2 != null && a2.getResponseCode() == 404) {
                return m901b(forcgnyyfk);
            }
            throw e3;
        }
    }

    /* renamed from: a */
    public final void m904a(@Nonnull qZypgoeblR qzypgoeblr) {
        this.f1094b = true;
        this.f1095c = qzypgoeblr;
    }

    /* renamed from: a */
    public final void m905a(String str) {
        if (this.f1094b) {
            this.f1095c.mo4341c(str);
        }
    }

    /* renamed from: a */
    public final void m906a(URL url) {
        this.f1093a = url;
    }

    /* renamed from: b */
    public final HttpCookie m907b(String str) {
        return this.f1094b ? this.f1095c.mo4340b(str) : null;
    }
}
