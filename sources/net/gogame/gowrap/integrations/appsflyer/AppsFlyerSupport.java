package net.gogame.gowrap.integrations.appsflyer;

import android.app.Activity;
import android.content.Context;
import com.appsflyer.AFInAppEventParameterName;
import com.appsflyer.AFInAppEventType;
import com.appsflyer.AppsFlyerLib;
import java.util.HashMap;
import java.util.Map;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import net.gogame.gowrap.integrations.CanGetUid;
import net.gogame.gowrap.integrations.CanSetGuid;
import net.gogame.gowrap.integrations.CanTrackEvent;
import net.gogame.gowrap.integrations.CanTrackPurchaseDetails;
import net.gogame.gowrap.integrations.Config;
import net.gogame.gowrap.integrations.IntegrationSupport.IntegrationContext;
import net.gogame.gowrap.integrations.PurchaseDetails;
import net.gogame.gowrap.support.AbstractPushTokenAsyncTask;
import net.gogame.gowrap.support.ClassUtils;
import net.gogame.gowrap.support.StringUtils;

public class AppsFlyerSupport extends AbstractIntegrationSupport implements CanGetUid, CanSetGuid, CanTrackPurchaseDetails, CanTrackEvent {
    public static final String CONFIG_DEV_KEY = "devKey";
    public static final String CONFIG_EVENT_NAME_DELIMITER = "eventNameDelimiter";
    public static final String CONFIG_SENDER_ID = "senderId";
    private String eventNameDelimiter = AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER;
    private IntegrationContext integrationContext;

    private static class PushTokenAsyncTask extends AbstractPushTokenAsyncTask {
        private PushTokenAsyncTask() {
        }

        /* access modifiers changed from: protected */
        public void onPushTokenReceived(Context context, String str) throws Exception {
            AppsFlyerLib.getInstance().updateServerUninstallToken(context, str);
        }
    }

    public AppsFlyerSupport() {
        super("appsflyer");
    }

    public boolean isIntegrated() {
        return ClassUtils.hasClass("com.appsflyer.AppsFlyerLib");
    }

    /* access modifiers changed from: protected */
    public void doInit(Activity activity, Config config, IntegrationContext integrationContext2) {
        this.integrationContext = integrationContext2;
        String string = config.getString(CONFIG_DEV_KEY);
        String trimToNull = StringUtils.trimToNull(config.getString(CONFIG_SENDER_ID));
        this.eventNameDelimiter = config.getString("eventNameDelimiter", AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
        AppsFlyerLib.getInstance().setCollectIMEI(false);
        if (trimToNull != null) {
            AppsFlyerLib.getInstance().enableUninstallTracking(trimToNull);
        }
        AppsFlyerLib.getInstance().startTracking(activity.getApplication(), string);
        trackEvent("DEFAULT", "APP_LAUNCH");
        new PushTokenAsyncTask().execute(new Context[]{activity});
    }

    public String getUid() {
        if (!isIntegrated()) {
            return null;
        }
        return AppsFlyerLib.getInstance().getAppsFlyerUID(this.integrationContext.getCurrentActivity());
    }

    public void setGuid(String str) {
        if (isIntegrated()) {
            AppsFlyerLib.getInstance().setCustomerUserId(str);
            if (str != null) {
                trackEvent("DEFAULT", "LOGIN");
            }
        }
    }

    public void trackPurchase(PurchaseDetails purchaseDetails) {
        if (isIntegrated()) {
            HashMap hashMap = new HashMap();
            if (purchaseDetails.getProductId() != null) {
                hashMap.put(AFInAppEventParameterName.CONTENT_ID, purchaseDetails.getProductId());
            }
            if (purchaseDetails.getCurrencyCode() != null) {
                hashMap.put(AFInAppEventParameterName.CURRENCY, purchaseDetails.getCurrencyCode());
            }
            if (purchaseDetails.getOrderId() != null) {
                hashMap.put(AFInAppEventType.ORDER_ID, purchaseDetails.getOrderId());
            }
            if (purchaseDetails.getPrice() != null) {
                hashMap.put(AFInAppEventParameterName.REVENUE, Float.valueOf(purchaseDetails.getPrice().floatValue()));
            }
            if (purchaseDetails.getComment() != null) {
                hashMap.put("gowrap_comment", purchaseDetails.getComment());
                hashMap.put(AFInAppEventParameterName.CONTENT_TYPE, purchaseDetails.getComment());
            }
            AppsFlyerLib.getInstance().trackEvent(this.integrationContext.getCurrentActivity(), AFInAppEventType.PURCHASE, hashMap);
        }
    }

    private String toEventName(String str, String str2) {
        if (str == null) {
            return str2;
        }
        return String.format("%s%s%s", new Object[]{str, this.eventNameDelimiter, str2});
    }

    public void trackEvent(String str, String str2) {
        if (isIntegrated()) {
            AppsFlyerLib.getInstance().trackEvent(this.integrationContext.getCurrentActivity(), toEventName(str, str2), new HashMap());
        }
    }

    public void trackEvent(String str, String str2, long j) {
        if (isIntegrated()) {
            HashMap hashMap = new HashMap();
            hashMap.put(AFInAppEventParameterName.PARAM_1, Long.valueOf(j));
            AppsFlyerLib.getInstance().trackEvent(this.integrationContext.getCurrentActivity(), toEventName(str, str2), hashMap);
        }
    }

    public void trackEvent(String str, String str2, Map<String, Object> map) {
        if (isIntegrated()) {
            AppsFlyerLib.getInstance().trackEvent(this.integrationContext.getCurrentActivity(), toEventName(str, str2), map);
        }
    }
}
