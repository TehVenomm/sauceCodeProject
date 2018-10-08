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
    private String f306;
    /* renamed from: ʼ */
    private String f307;
    /* renamed from: ʽ */
    private String f308;
    /* renamed from: ˊ */
    private String f309;
    /* renamed from: ˋ */
    private String f310;
    /* renamed from: ˋॱ */
    private Map<String, String> f311 = new HashMap();
    /* renamed from: ˎ */
    private String f312;
    /* renamed from: ˏ */
    private String f313;
    /* renamed from: ͺ */
    private Map<String, String> f314 = new HashMap();
    /* renamed from: ॱ */
    private String f315;
    /* renamed from: ॱˊ */
    private String f316;
    /* renamed from: ॱॱ */
    private String f317;
    /* renamed from: ᐝ */
    private String f318;

    public LinkGenerator(String str) {
        this.f309 = str;
    }

    /* renamed from: ˎ */
    final LinkGenerator m360(String str) {
        this.f308 = str;
        return this;
    }

    public LinkGenerator setDeeplinkPath(String str) {
        this.f306 = str;
        return this;
    }

    public LinkGenerator setBaseDeeplink(String str) {
        this.f316 = str;
        return this;
    }

    /* renamed from: ˊ */
    final LinkGenerator m359(String str) {
        this.f307 = str;
        return this;
    }

    public LinkGenerator setChannel(String str) {
        this.f310 = str;
        return this;
    }

    public String getChannel() {
        return this.f310;
    }

    public LinkGenerator setReferrerCustomerId(String str) {
        this.f313 = str;
        return this;
    }

    public String getMediaSource() {
        return this.f309;
    }

    public Map<String, String> getParameters() {
        return this.f311;
    }

    public LinkGenerator setCampaign(String str) {
        this.f312 = str;
        return this;
    }

    public String getCampaign() {
        return this.f312;
    }

    public LinkGenerator addParameter(String str, String str2) {
        this.f311.put(str, str2);
        return this;
    }

    public LinkGenerator addParameters(Map<String, String> map) {
        if (map != null) {
            this.f311.putAll(map);
        }
        return this;
    }

    public LinkGenerator setReferrerUID(String str) {
        this.f315 = str;
        return this;
    }

    public LinkGenerator setReferrerName(String str) {
        this.f317 = str;
        return this;
    }

    public LinkGenerator setReferrerImageURL(String str) {
        this.f318 = str;
        return this;
    }

    public LinkGenerator setBaseURL(String str, String str2, String str3) {
        if (str == null || str.length() <= 0) {
            this.f307 = String.format(Constants.AF_BASE_URL_FORMAT, new Object[]{ServerConfigHandler.getUrl(Constants.APPSFLYER_DEFAULT_APP_DOMAIN), str3});
        } else {
            if (str2 == null || str2.length() < 5) {
                str2 = Constants.ONELINK_DEFAULT_DOMAIN;
            }
            this.f307 = String.format(Constants.AF_BASE_URL_FORMAT, new Object[]{str2, str});
        }
        return this;
    }

    /* renamed from: ˊ */
    private StringBuilder m357() {
        StringBuilder stringBuilder = new StringBuilder();
        if (this.f307 == null || !this.f307.startsWith("http")) {
            stringBuilder.append(ServerConfigHandler.getUrl(Constants.BASE_URL_APP_APPSFLYER_COM));
        } else {
            stringBuilder.append(this.f307);
        }
        if (this.f308 != null) {
            stringBuilder.append('/').append(this.f308);
        }
        this.f314.put(Constants.URL_MEDIA_SOURCE, this.f309);
        stringBuilder.append('?').append("pid=").append(m358(this.f309, "media source"));
        if (this.f315 != null) {
            this.f314.put(Constants.URL_REFERRER_UID, this.f315);
            stringBuilder.append('&').append("af_referrer_uid=").append(m358(this.f315, "referrerUID"));
        }
        if (this.f310 != null) {
            this.f314.put("af_channel", this.f310);
            stringBuilder.append('&').append("af_channel=").append(m358(this.f310, AppsFlyerProperties.CHANNEL));
        }
        if (this.f313 != null) {
            this.f314.put(Constants.URL_REFERRER_CUSTOMER_ID, this.f313);
            stringBuilder.append('&').append("af_referrer_customer_id=").append(m358(this.f313, "referrerCustomerId"));
        }
        if (this.f312 != null) {
            this.f314.put(Constants.URL_CAMPAIGN, this.f312);
            stringBuilder.append('&').append("c=").append(m358(this.f312, Param.CAMPAIGN));
        }
        if (this.f317 != null) {
            this.f314.put(Constants.URL_REFERRER_NAME, this.f317);
            stringBuilder.append('&').append("af_referrer_name=").append(m358(this.f317, "referrerName"));
        }
        if (this.f318 != null) {
            this.f314.put(Constants.URL_REFERRER_IMAGE_URL, this.f318);
            stringBuilder.append('&').append("af_referrer_image_url=").append(m358(this.f318, "referrerImageURL"));
        }
        if (this.f316 != null) {
            StringBuilder append = new StringBuilder().append(this.f316);
            append.append(this.f316.endsWith(Constants.URL_PATH_DELIMITER) ? "" : Constants.URL_PATH_DELIMITER);
            if (this.f306 != null) {
                append.append(this.f306);
            }
            this.f314.put(Constants.URL_BASE_DEEPLINK, append.toString());
            stringBuilder.append('&').append("af_dp=").append(m358(this.f316, "baseDeeplink"));
            if (this.f306 != null) {
                stringBuilder.append(this.f316.endsWith(Constants.URL_PATH_DELIMITER) ? "" : "%2F").append(m358(this.f306, "deeplinkPath"));
            }
        }
        for (String str : this.f311.keySet()) {
            if (!stringBuilder.toString().contains(new StringBuilder().append(str).append("=").append(m358((String) this.f311.get(str), str)).toString())) {
                stringBuilder.append('&').append(str).append('=').append(m358((String) this.f311.get(str), str));
            }
        }
        return stringBuilder;
    }

    /* renamed from: ˋ */
    private static String m358(String str, String str2) {
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
        return m357().toString();
    }

    public void generateLink(Context context, ResponseListener responseListener) {
        String string = AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.ONELINK_ID);
        if (!this.f311.isEmpty()) {
            for (Entry entry : this.f311.entrySet()) {
                this.f314.put(entry.getKey(), entry.getValue());
            }
        }
        m357();
        ShareInviteHelper.generateUserInviteLink(context, string, this.f314, responseListener);
    }
}
