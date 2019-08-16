package net.gogame.gowrap.integrations.core;

import android.app.Activity;
import android.util.Log;
import net.gogame.gowrap.GoWrapImpl;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import net.gogame.gowrap.integrations.Config;
import net.gogame.gowrap.integrations.IntegrationSupport.IntegrationContext;
import net.gogame.gowrap.integrations.zendesk.ZendeskSupport;
import net.gogame.gowrap.p019ui.ActivityHelper;
import net.gogame.gowrap.p019ui.fab.FabManager;

public class CoreSupport extends AbstractIntegrationSupport {
    public static final String CONFIG_APP_ID = "appId";
    public static final String CONFIG_CUSTOM_MAIN_ACTIVITY = "customMainActivity";
    public static final String CONFIG_DISABLE_FAB = "disableFab";
    public static final String CONFIG_FORCE_ENABLE_CHAT = "forceEnableChat";
    public static final String CONFIG_VARIANT_ID = "variantId";
    public static final CoreSupport INSTANCE = new CoreSupport();
    private String appId;
    private String customMainActivity;
    private boolean disableFab = false;
    private boolean forceEnableChat = false;
    private String variantId;

    private CoreSupport() {
        super("core");
    }

    public String getAppId() {
        return this.appId;
    }

    public String getVariantId() {
        return this.variantId;
    }

    public boolean isForceEnableChat() {
        return this.forceEnableChat;
    }

    public boolean isIntegrated() {
        return true;
    }

    /* access modifiers changed from: protected */
    public void doInit(Activity activity, Config config, IntegrationContext integrationContext) {
        this.appId = config.getString("appId");
        this.variantId = config.getString(CONFIG_VARIANT_ID);
        this.disableFab = config.getBoolean(CONFIG_DISABLE_FAB, false);
        this.forceEnableChat = config.getBoolean(CONFIG_FORCE_ENABLE_CHAT, false);
        this.customMainActivity = config.getString(CONFIG_CUSTOM_MAIN_ACTIVITY);
        if (this.customMainActivity != null) {
            try {
                GoWrapImpl.INSTANCE.setMainActivity(Class.forName(this.customMainActivity));
            } catch (ClassNotFoundException e) {
                Log.w("Exception", e);
            }
        }
        activity.getApplication().registerActivityLifecycleCallbacks(ActivityHelper.INSTANCE);
        Wrapper.INSTANCE.setup(activity);
        ZendeskSupport.INSTANCE.init(activity, new Config(), integrationContext);
        if (shouldDisplayFab(activity)) {
            FabManager.onCreate(activity);
        }
    }

    private boolean shouldDisplayFab(Activity activity) {
        return !this.disableFab && !activity.getClass().getName().startsWith("net.gogame.gowrap.") && !activity.getClass().getName().startsWith("net.gogame.gopay.") && !activity.getClass().getName().startsWith("net.gogame.zopim.") && !activity.getClass().getName().startsWith("jp.noahapps.sdk.");
    }

    public void onActivityCreated(Activity activity) {
        super.onActivityCreated(activity);
        if (shouldDisplayFab(activity)) {
            FabManager.onCreate(activity);
        }
    }

    public void onActivityResumed(Activity activity) {
        super.onActivityResumed(activity);
        if (shouldDisplayFab(activity)) {
            FabManager.onResume(activity);
        }
    }

    public void onActivityPaused(Activity activity) {
        super.onActivityPaused(activity);
        if (shouldDisplayFab(activity)) {
            FabManager.onPause(activity);
        }
    }

    public void onActivityDestroyed(Activity activity) {
        super.onActivityDestroyed(activity);
        if (shouldDisplayFab(activity)) {
            FabManager.onDestroy(activity);
        }
    }
}
