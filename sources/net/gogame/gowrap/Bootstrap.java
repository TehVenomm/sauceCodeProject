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
import net.gogame.gowrap.ui.ActivityHelper;
import net.gogame.gowrap.ui.dpro.ui.MainActivity;

public class Bootstrap {
    private static boolean initialized = false;

    public static void init(Activity activity) {
        init(activity, false);
    }

    public static void init(Activity activity, boolean z) {
        if (initialized) {
            Log.v(Constants.TAG, "Already initialized");
            return;
        }
        Log.v(Constants.TAG, "Initializing...");
        try {
            GoWrapImpl.INSTANCE.setMainActivity(MainActivity.class);
            Config config = new Config();
            config.putString("appId", "DragonProject");
            config.putString(CoreSupport.CONFIG_VARIANT_ID, "google");
            config.putBoolean(CoreSupport.CONFIG_DISABLE_FAB, true);
            config.putBoolean(CoreSupport.CONFIG_FORCE_ENABLE_CHAT, false);
            GoWrapImpl.INSTANCE.register(CoreSupport.INSTANCE, activity, config);
        } catch (Throwable e) {
            Log.e(Constants.TAG, "Exception", e);
        } catch (Throwable th) {
            if (z) {
                try {
                    ActivityHelper.INSTANCE.onActivityCreated(activity, new Bundle());
                    ActivityHelper.INSTANCE.onActivityStarted(activity);
                    ActivityHelper.INSTANCE.onActivityResumed(activity);
                } catch (Throwable e2) {
                    Log.e(Constants.TAG, "Exception", e2);
                }
            }
            initialized = true;
        }
        try {
            config = new Config();
            config.putString(CustomZopimSupport.CONFIG_ACCOUNT_KEY, "3NVMS9MbGUuR3KVE1V3mBiaPkWsaJgyr");
            GoWrapImpl.INSTANCE.register(CustomZopimSupport.INSTANCE, activity, config);
        } catch (Throwable e3) {
            Log.e(Constants.TAG, "Exception", e3);
        }
        try {
            config = new Config();
            config.putString(AppsFlyerSupport.CONFIG_DEV_KEY, "BdP724hHJFraJaxKXvNex7");
            GoWrapImpl.INSTANCE.register(AppsFlyerSupport.INSTANCE, activity, config);
        } catch (Throwable e32) {
            Log.e(Constants.TAG, "Exception", e32);
        }
        try {
            Class.forName("com.google.firebase.analytics.FirebaseAnalytics");
            GoWrapImpl.INSTANCE.register(FirebaseSupport.INSTANCE, activity, new Config());
        } catch (ClassNotFoundException e4) {
            try {
                System.err.println("Firebase SDK not found.");
            } catch (Throwable e322) {
                Log.e(Constants.TAG, "Exception", e322);
            }
        }
        try {
            config = new Config();
            config.putString("appId", "424419412411496365097632");
            config.putString(GoPaySupport.CONFIG_SECRET, "bvhp6z9myz91cs83ejhnrwnqmqfvjy9p");
            config.putBoolean(GoPaySupport.CONFIG_GAME_MANAGED_VIP_STATUS, false);
            GoWrapImpl.INSTANCE.register(GoPaySupport.INSTANCE, activity, config);
        } catch (Throwable e3222) {
            Log.e(Constants.TAG, "Exception", e3222);
        }
        Log.v(Constants.TAG, "Initialized");
        if (z) {
            try {
                ActivityHelper.INSTANCE.onActivityCreated(activity, new Bundle());
                ActivityHelper.INSTANCE.onActivityStarted(activity);
                ActivityHelper.INSTANCE.onActivityResumed(activity);
            } catch (Throwable e32222) {
                Log.e(Constants.TAG, "Exception", e32222);
            }
        }
        initialized = true;
    }

    public static void unityInit() {
        try {
            init((Activity) Class.forName("com.unity3d.player.UnityPlayer").getField("currentActivity").get(null), true);
        } catch (Exception e) {
        }
    }
}
