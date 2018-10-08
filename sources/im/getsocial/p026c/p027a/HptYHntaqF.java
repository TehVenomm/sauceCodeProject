package im.getsocial.p026c.p027a;

import java.net.HttpCookie;
import java.net.URL;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.c.a.HptYHntaqF */
public class HptYHntaqF implements qZypgoeblR {
    /* renamed from: a */
    private Map<String, URL> f1066a = new HashMap();
    /* renamed from: b */
    private Map<String, HttpCookie> f1067b = new HashMap();

    /* renamed from: a */
    public final URL mo4337a(String str) {
        return (URL) this.f1066a.get(str);
    }

    /* renamed from: a */
    public final void mo4338a(String str, HttpCookie httpCookie) {
        this.f1067b.put(str, httpCookie);
    }

    /* renamed from: a */
    public final void mo4339a(String str, URL url) {
        this.f1066a.put(str, url);
    }

    /* renamed from: b */
    public final HttpCookie mo4340b(String str) {
        return (HttpCookie) this.f1067b.get(str);
    }

    /* renamed from: c */
    public final void mo4341c(String str) {
        this.f1067b.remove(str);
    }
}
