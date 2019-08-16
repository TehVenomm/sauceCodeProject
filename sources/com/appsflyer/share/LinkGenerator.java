package com.appsflyer.share;

import android.content.Context;
import com.appsflyer.AFLogger;
import com.appsflyer.AppsFlyerProperties;
import com.appsflyer.CreateOneLinkHttpTask.ResponseListener;
import com.appsflyer.ServerConfigHandler;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import java.io.UnsupportedEncodingException;
import java.net.URLEncoder;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;

public class LinkGenerator {

    /* renamed from: ʻ */
    private String f327;

    /* renamed from: ʼ */
    private String f328;

    /* renamed from: ʽ */
    private String f329;

    /* renamed from: ˊ */
    private String f330;

    /* renamed from: ˋ */
    private String f331;

    /* renamed from: ˋॱ */
    private Map<String, String> f332 = new HashMap();

    /* renamed from: ˎ */
    private String f333;

    /* renamed from: ˏ */
    private String f334;

    /* renamed from: ͺ */
    private Map<String, String> f335 = new HashMap();

    /* renamed from: ॱ */
    private String f336;

    /* renamed from: ॱˊ */
    private String f337;

    /* renamed from: ॱॱ */
    private String f338;

    /* renamed from: ᐝ */
    private String f339;

    public LinkGenerator(String str) {
        this.f330 = str;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˎ */
    public final LinkGenerator mo6630(String str) {
        this.f329 = str;
        return this;
    }

    public LinkGenerator setDeeplinkPath(String str) {
        this.f327 = str;
        return this;
    }

    public LinkGenerator setBaseDeeplink(String str) {
        this.f337 = str;
        return this;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˊ */
    public final LinkGenerator mo6629(String str) {
        this.f328 = str;
        return this;
    }

    public LinkGenerator setChannel(String str) {
        this.f331 = str;
        return this;
    }

    public String getChannel() {
        return this.f331;
    }

    public LinkGenerator setReferrerCustomerId(String str) {
        this.f334 = str;
        return this;
    }

    public String getMediaSource() {
        return this.f330;
    }

    public Map<String, String> getParameters() {
        return this.f332;
    }

    public LinkGenerator setCampaign(String str) {
        this.f333 = str;
        return this;
    }

    public String getCampaign() {
        return this.f333;
    }

    public LinkGenerator addParameter(String str, String str2) {
        this.f332.put(str, str2);
        return this;
    }

    public LinkGenerator addParameters(Map<String, String> map) {
        if (map != null) {
            this.f332.putAll(map);
        }
        return this;
    }

    public LinkGenerator setReferrerUID(String str) {
        this.f336 = str;
        return this;
    }

    public LinkGenerator setReferrerName(String str) {
        this.f338 = str;
        return this;
    }

    public LinkGenerator setReferrerImageURL(String str) {
        this.f339 = str;
        return this;
    }

    public LinkGenerator setBaseURL(String str, String str2, String str3) {
        if (str == null || str.length() <= 0) {
            this.f328 = String.format(Constants.AF_BASE_URL_FORMAT, new Object[]{ServerConfigHandler.getUrl(Constants.APPSFLYER_DEFAULT_APP_DOMAIN), str3});
        } else {
            if (str2 == null || str2.length() < 5) {
                str2 = Constants.ONELINK_DEFAULT_DOMAIN;
            }
            this.f328 = String.format(Constants.AF_BASE_URL_FORMAT, new Object[]{str2, str});
        }
        return this;
    }

    /* renamed from: ˊ */
    private StringBuilder m352() {
        StringBuilder sb = new StringBuilder();
        if (this.f328 == null || !this.f328.startsWith("http")) {
            sb.append(ServerConfigHandler.getUrl(Constants.BASE_URL_APP_APPSFLYER_COM));
        } else {
            sb.append(this.f328);
        }
        if (this.f329 != null) {
            sb.append('/').append(this.f329);
        }
        this.f335.put(Constants.URL_MEDIA_SOURCE, this.f330);
        sb.append('?').append("pid=").append(m353(this.f330, "media source"));
        if (this.f336 != null) {
            this.f335.put(Constants.URL_REFERRER_UID, this.f336);
            sb.append('&').append("af_referrer_uid=").append(m353(this.f336, "referrerUID"));
        }
        if (this.f331 != null) {
            this.f335.put("af_channel", this.f331);
            sb.append('&').append("af_channel=").append(m353(this.f331, AppsFlyerProperties.CHANNEL));
        }
        if (this.f334 != null) {
            this.f335.put(Constants.URL_REFERRER_CUSTOMER_ID, this.f334);
            sb.append('&').append("af_referrer_customer_id=").append(m353(this.f334, "referrerCustomerId"));
        }
        if (this.f333 != null) {
            this.f335.put(Constants.URL_CAMPAIGN, this.f333);
            sb.append('&').append("c=").append(m353(this.f333, Param.CAMPAIGN));
        }
        if (this.f338 != null) {
            this.f335.put(Constants.URL_REFERRER_NAME, this.f338);
            sb.append('&').append("af_referrer_name=").append(m353(this.f338, "referrerName"));
        }
        if (this.f339 != null) {
            this.f335.put(Constants.URL_REFERRER_IMAGE_URL, this.f339);
            sb.append('&').append("af_referrer_image_url=").append(m353(this.f339, "referrerImageURL"));
        }
        if (this.f337 != null) {
            StringBuilder append = new StringBuilder().append(this.f337);
            append.append(this.f337.endsWith(Constants.URL_PATH_DELIMITER) ? "" : Constants.URL_PATH_DELIMITER);
            if (this.f327 != null) {
                append.append(this.f327);
            }
            this.f335.put(Constants.URL_BASE_DEEPLINK, append.toString());
            sb.append('&').append("af_dp=").append(m353(this.f337, "baseDeeplink"));
            if (this.f327 != null) {
                sb.append(this.f337.endsWith(Constants.URL_PATH_DELIMITER) ? "" : "%2F").append(m353(this.f327, "deeplinkPath"));
            }
        }
        for (String str : this.f332.keySet()) {
            if (!sb.toString().contains(new StringBuilder().append(str).append("=").append(m353((String) this.f332.get(str), str)).toString())) {
                sb.append('&').append(str).append('=').append(m353((String) this.f332.get(str), str));
            }
        }
        return sb;
    }

    /* renamed from: ˋ */
    private static String m353(String str, String str2) {
        try {
            return URLEncoder.encode(str, "utf8");
        } catch (UnsupportedEncodingException e) {
            AFLogger.afInfoLog(new StringBuilder("Illegal ").append(str2).append(": ").append(str).toString());
            return "";
        } catch (Throwable th) {
            return "";
        }
    }

    public String generateLink() {
        return m352().toString();
    }

    public void generateLink(Context context, ResponseListener responseListener) {
        String string = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.ONELINK_ID);
        if (!this.f332.isEmpty()) {
            for (Entry entry : this.f332.entrySet()) {
                this.f335.put(entry.getKey(), entry.getValue());
            }
        }
        m352();
        ShareInviteHelper.generateUserInviteLink(context, string, this.f335, responseListener);
    }
}
