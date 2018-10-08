package net.gogame.gowrap.integrations;

import android.app.Activity;
import android.net.Uri;
import java.util.Map;
import net.gogame.gowrap.VipStatus;

public interface IntegrationSupport {

    public interface IntegrationContext {
        void didCompleteRewardedAd(String str, int i);

        String getAppId();

        Activity getCurrentActivity();

        String getGuid();

        Map<String, String> getUids();

        boolean handleCustomUrl(Uri uri);

        boolean handleCustomUrl(String str);

        boolean isChatBotEnabled();

        boolean isForceEnableChat();

        boolean isVip();

        void onOffersAvailable();

        void onVipStatusUpdated(VipStatus vipStatus);
    }

    String getId();

    void init(Activity activity, Config config, IntegrationContext integrationContext);

    boolean isIntegrated();

    void onActivityCreated(Activity activity);

    void onActivityDestroyed(Activity activity);

    void onActivityPaused(Activity activity);

    void onActivityResumed(Activity activity);

    void onActivityStarted(Activity activity);

    void onActivityStopped(Activity activity);
}
