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

public class AppsFlyerSupport extends AbstractIntegrationSupport implements CanGetUid, CanSetGuid, CanTrackPurchaseDetails, CanTrackEvent {
    public static final String CONFIG_DEV_KEY = "devKey";
    public static final String CONFIG_EVENT_NAME_DELIMITER = "eventNameDelimiter";
    public static final AppsFlyerSupport INSTANCE = new AppsFlyerSupport();
    private String eventNameDelimiter = AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER;
    private IntegrationContext integrationContext;

    private static class PushTokenAsyncTask extends AbstractPushTokenAsyncTask {
        private PushTokenAsyncTask() {
        }

        protected void onPushTokenReceived(Context context, String str) throws Exception {
            AppsFlyerLib.getInstance().updateServerUninstallToken(context, str);
        }
    }

    private AppsFlyerSupport() {
        super("appsflyer");
    }

    public boolean isIntegrated() {
        return ClassUtils.hasClass("com.appsflyer.AppsFlyerLib");
    }

    protected void doInit(Activity activity, Config config, IntegrationContext integrationContext) {
        this.integrationContext = integrationContext;
        String string = config.getString(CONFIG_DEV_KEY);
        this.eventNameDelimiter = config.getString("eventNameDelimiter", AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
        AppsFlyerLib.getInstance().setCollectIMEI(false);
        AppsFlyerLib.getInstance().startTracking(activity.getApplication(), string);
        trackEvent(AbstractIntegrationSupport.DEFAULT_REWARD_ID, "APP_LAUNCH");
        new PushTokenAsyncTask().execute(new Context[]{activity});
    }

    public String getUid() {
        if (isIntegrated()) {
            return AppsFlyerLib.getInstance().getAppsFlyerUID(this.integrationContext.getCurrentActivity());
        }
        return null;
    }

    public void setGuid(String str) {
        if (isIntegrated()) {
            AppsFlyerLib.getInstance().setCustomerUserId(str);
            if (str != null) {
                trackEvent(AbstractIntegrationSupport.DEFAULT_REWARD_ID, "LOGIN");
            }
        }
    }

    public void trackPurchase(PurchaseDetails purchaseDetails) {
        if (isIntegrated()) {
            Map hashMap = new HashMap();
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
            Map hashMap = new HashMap();
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
