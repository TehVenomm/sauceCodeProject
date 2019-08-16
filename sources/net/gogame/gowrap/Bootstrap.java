package net.gogame.gowrap;

import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import net.gogame.gowrap.integrations.Config;
import net.gogame.gowrap.integrations.appsflyer.AppsFlyerSupport;
import net.gogame.gowrap.integrations.core.CoreSupport;
import net.gogame.gowrap.integrations.firebase.FirebaseSupport;
import net.gogame.gowrap.integrations.gopay.GoPaySupport;
import net.gogame.gowrap.integrations.zopim.CustomZopimSupport;
import net.gogame.gowrap.p019ui.ActivityHelper;
import net.gogame.gowrap.p019ui.dpro.p020ui.MainActivity;

public class Bootstrap {
    private static boolean initialized = false;

    public static void init(Activity activity) {
        init(activity, false);
    }

    public static void init(Activity activity, boolean z) {
        if (!initialized) {
            Log.v(Constants.TAG, "Initializing...");
            try {
                GoWrapImpl.INSTANCE.setMainActivity(MainActivity.class);
                Config config = new Config();
                config.putString("appId", "DragonProject");
                config.putString(CoreSupport.CONFIG_VARIANT_ID, "google");
                config.putBoolean(CoreSupport.CONFIG_DISABLE_FAB, true);
                config.putBoolean(CoreSupport.CONFIG_FORCE_ENABLE_CHAT, false);
                GoWrapImpl.INSTANCE.register(CoreSupport.INSTANCE, activity, config);
            } catch (Exception e) {
                Log.e(Constants.TAG, "Exception", e);
            } catch (Throwable th) {
                if (z) {
                    try {
                        ActivityHelper.INSTANCE.onActivityCreated(activity, new Bundle());
                        ActivityHelper.INSTANCE.onActivityStarted(activity);
                        ActivityHelper.INSTANCE.onActivityResumed(activity);
                    } catch (Exception e2) {
                        Log.e(Constants.TAG, "Exception", e2);
                    }
                }
                initialized = true;
                throw th;
            }
            try {
                Config config2 = new Config();
                config2.putString(CustomZopimSupport.CONFIG_ACCOUNT_KEY, "3NVMS9MbGUuR3KVE1V3mBiaPkWsaJgyr");
                GoWrapImpl.INSTANCE.register(new CustomZopimSupport(), activity, config2);
            } catch (Exception e3) {
                Log.e(Constants.TAG, "Exception", e3);
            }
            try {
                Config config3 = new Config();
                config3.putString(AppsFlyerSupport.CONFIG_DEV_KEY, "BdP724hHJFraJaxKXvNex7");
                GoWrapImpl.INSTANCE.register(new AppsFlyerSupport(), activity, config3);
            } catch (Exception e4) {
                Log.e(Constants.TAG, "Exception", e4);
            }
            try {
                Class.forName("com.google.firebase.analytics.FirebaseAnalytics");
                GoWrapImpl.INSTANCE.register(new FirebaseSupport(), activity, new Config());
            } catch (ClassNotFoundException e5) {
                try {
                    System.err.println("Firebase SDK not found.");
                } catch (Exception e6) {
                    Log.e(Constants.TAG, "Exception", e6);
                }
            }
            try {
                Config config4 = new Config();
                config4.putString("appId", "424419412411496365097632");
                config4.putString(GoPaySupport.CONFIG_SECRET, "bvhp6z9myz91cs83ejhnrwnqmqfvjy9p");
                config4.putBoolean(GoPaySupport.CONFIG_GAME_MANAGED_VIP_STATUS, false);
                GoWrapImpl.INSTANCE.register(new GoPaySupport(), activity, config4);
            } catch (Exception e7) {
                Log.e(Constants.TAG, "Exception", e7);
            }
            Log.v(Constants.TAG, "Initialized");
            if (z) {
                try {
                    ActivityHelper.INSTANCE.onActivityCreated(activity, new Bundle());
                    ActivityHelper.INSTANCE.onActivityStarted(activity);
                    ActivityHelper.INSTANCE.onActivityResumed(activity);
                } catch (Exception e8) {
                    Log.e(Constants.TAG, "Exception", e8);
                }
            }
            initialized = true;
            return;
        }
        Log.v(Constants.TAG, "Already initialized");
    }

    public static void unityInit() {
        try {
            init((Activity) Class.forName("com.unity3d.player.UnityPlayer").getField("currentActivity").get(null), true);
        } catch (Exception e) {
        }
    }
}
