package net.gogame.gowrap.integrations;

import android.app.Activity;
import android.util.Log;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.integrations.IntegrationSupport.IntegrationContext;

public abstract class AbstractIntegrationSupport implements IntegrationSupport {
    public static final String DEFAULT_BANNER_ZONE_ID = "defaultBanner";
    public static final String DEFAULT_EVENT_NAME_DELIMITER = ".";
    public static final String DEFAULT_INTERSTITIAL_ZONE_ID = "defaultInterstitial";
    public static final String DEFAULT_PURCHASE_CATEGORY = "purchase";
    public static final String DEFAULT_REWARDED_ZONE_ID = "defaultRewarded";
    public static final String DEFAULT_REWARD_ID = "DEFAULT";
    public static final int DEFAULT_REWARD_QUANTITY = -1;

    /* renamed from: id */
    private final String f1181id;
    private boolean initialized = false;

    /* access modifiers changed from: protected */
    public abstract void doInit(Activity activity, Config config, IntegrationContext integrationContext);

    public AbstractIntegrationSupport(String str) {
        this.f1181id = str;
    }

    public void init(Activity activity, Config config, IntegrationContext integrationContext) {
        if (!this.initialized) {
            try {
                doInit(activity, config, integrationContext);
            } catch (Exception e) {
                Log.e(Constants.TAG, "Error initializing " + this.f1181id, e);
            } finally {
                this.initialized = true;
            }
        }
    }

    public String getId() {
        return this.f1181id;
    }

    public void onActivityCreated(Activity activity) {
    }

    public void onActivityStarted(Activity activity) {
    }

    public void onActivityResumed(Activity activity) {
    }

    public void onActivityPaused(Activity activity) {
    }

    public void onActivityStopped(Activity activity) {
    }

    public void onActivityDestroyed(Activity activity) {
    }
}
