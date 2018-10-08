package com.appsflyer.share;

import android.content.Context;
import android.os.AsyncTask;
import com.appsflyer.AFLogger;
import com.appsflyer.AppsFlyerLib;
import com.appsflyer.AppsFlyerProperties;
import com.appsflyer.ServerConfigHandler;
import com.appsflyer.ServerParameters;
import java.lang.ref.WeakReference;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.HashMap;
import java.util.Map;

public class CrossPromotionHelper {

    /* renamed from: com.appsflyer.share.CrossPromotionHelper$b */
    static class C0293b extends AsyncTask<String, Void, Void> {
        /* renamed from: ˊ */
        private boolean f303;
        /* renamed from: ˎ */
        private WeakReference<Context> f304;
        /* renamed from: ॱ */
        private C0294c f305;

        protected final /* synthetic */ Object doInBackground(Object[] objArr) {
            return m355((String[]) objArr);
        }

        C0293b(C0294c c0294c, Context context, boolean z) {
            this.f305 = c0294c;
            this.f304 = new WeakReference(context);
            this.f303 = z;
        }

        /* renamed from: ˊ */
        private Void m355(String... strArr) {
            HttpURLConnection httpURLConnection;
            Throwable th;
            HttpURLConnection httpURLConnection2 = null;
            if (!this.f303) {
                try {
                    String str = strArr[0];
                    HttpURLConnection httpURLConnection3 = (HttpURLConnection) new URL(str).openConnection();
                    try {
                        httpURLConnection3.setConnectTimeout(10000);
                        httpURLConnection3.setInstanceFollowRedirects(false);
                        int responseCode = httpURLConnection3.getResponseCode();
                        if (responseCode == 200) {
                            AFLogger.afInfoLog("Cross promotion impressions success: ".concat(String.valueOf(str)), false);
                        } else if (responseCode == 301 || responseCode == 302) {
                            AFLogger.afInfoLog("Cross promotion redirection success: ".concat(String.valueOf(str)), false);
                            if (!(this.f305 == null || this.f304.get() == null)) {
                                this.f305.m362(httpURLConnection3.getHeaderField("Location"));
                                this.f305.m361((Context) this.f304.get());
                            }
                        } else {
                            AFLogger.afInfoLog(new StringBuilder("call to ").append(str).append(" failed: ").append(responseCode).toString());
                        }
                        if (httpURLConnection3 != null) {
                            httpURLConnection3.disconnect();
                        }
                    } catch (Throwable th2) {
                        httpURLConnection2 = httpURLConnection3;
                        th = th2;
                        if (httpURLConnection2 != null) {
                            httpURLConnection2.disconnect();
                        }
                        throw th;
                    }
                } catch (Throwable th3) {
                    th = th3;
                    if (httpURLConnection2 != null) {
                        httpURLConnection2.disconnect();
                    }
                    throw th;
                }
            }
            return null;
        }
    }

    public static void trackAndOpenStore(Context context, String str, String str2) {
        trackAndOpenStore(context, str, str2, null);
    }

    public static void trackAndOpenStore(Context context, String str, String str2, Map<String, String> map) {
        LinkGenerator ˎ = m356(context, str, str2, map, ServerConfigHandler.getUrl(Constants.BASE_URL_APP_APPSFLYER_COM));
        if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.AF_WAITFOR_CUSTOMERID, false)) {
            AFLogger.afInfoLog("CustomerUserId not set, track And Open Store is disabled", true);
            return;
        }
        Map hashMap = new HashMap();
        if (map != null) {
            hashMap.putAll(map);
        }
        hashMap.put("af_campaign", str2);
        AppsFlyerLib.getInstance().trackEvent(context, "af_cross_promotion", hashMap);
        new C0293b(new C0294c(), context, AppsFlyerLib.getInstance().isTrackingStopped()).execute(new String[]{ˎ.generateLink()});
    }

    public static void trackCrossPromoteImpression(Context context, String str, String str2) {
        if (AppsFlyerProperties.getInstance().getBoolean(AppsFlyerProperties.AF_WAITFOR_CUSTOMERID, false)) {
            AFLogger.afInfoLog("CustomerUserId not set, Promote Impression is disabled", true);
            return;
        }
        LinkGenerator ˎ = m356(context, str, str2, null, ServerConfigHandler.getUrl("https://impression.%s"));
        new C0293b(null, null, AppsFlyerLib.getInstance().isTrackingStopped()).execute(new String[]{ˎ.generateLink()});
    }

    /* renamed from: ˎ */
    private static LinkGenerator m356(Context context, String str, String str2, Map<String, String> map, String str3) {
        LinkGenerator linkGenerator = new LinkGenerator("af_cross_promotion");
        linkGenerator.m359(str3).m360(str).addParameter(Constants.URL_SITE_ID, context.getPackageName());
        if (str2 != null) {
            linkGenerator.setCampaign(str2);
        }
        if (map != null) {
            linkGenerator.addParameters(map);
        }
        String string = AppsFlyerProperties.getInstance().getString(ServerParameters.ADVERTISING_ID_PARAM);
        if (string != null) {
            linkGenerator.addParameter(Constants.URL_ADVERTISING_ID, string);
        }
        return linkGenerator;
    }
}
