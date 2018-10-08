package im.getsocial.p026c.p027a;

import io.fabric.sdk.android.services.settings.SettingsJsonConstants;
import java.io.IOException;
import java.net.HttpURLConnection;

/* renamed from: im.getsocial.c.a.cjrhisSQCL */
public class cjrhisSQCL extends Exception {
    /* renamed from: a */
    private HttpURLConnection f1080a;

    public cjrhisSQCL(String str, HttpURLConnection httpURLConnection) {
        super(str);
        this.f1080a = httpURLConnection;
    }

    /* renamed from: a */
    public final HttpURLConnection m881a() {
        return this.f1080a;
    }

    /* renamed from: b */
    public final boolean m882b() {
        if (this.f1080a == null) {
            return false;
        }
        try {
            int responseCode = this.f1080a.getResponseCode();
            return (responseCode >= 500 && responseCode < SettingsJsonConstants.ANALYTICS_FLUSH_INTERVAL_SECS_DEFAULT) || responseCode == 423;
        } catch (IOException e) {
            return false;
        }
    }
}
