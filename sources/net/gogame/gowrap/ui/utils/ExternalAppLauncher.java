package net.gogame.gowrap.ui.utils;

import android.app.Activity;
import android.content.ActivityNotFoundException;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import android.net.Uri;
import android.os.Build.VERSION;
import net.gogame.gowrap.ui.customtabs.CustomTabsChecker;
import net.gogame.gowrap.ui.customtabs.CustomTabsIntent;
import net.gogame.gowrap.ui.customtabs.CustomTabsIntent.Builder;

public class ExternalAppLauncher {
    private static final String CHROME_PACKAGE_NAME = "com.android.chrome";
    private static final String FACEBOOK_PACKAGE_NAME = "com.facebook.katana";
    private static final boolean HANDLE_FACEBOOK_URLS = true;
    private static final boolean SHARE_BUTTON_ENABLED = false;
    private static final boolean USE_CUSTOM_TABS = true;

    public static boolean openUrlInExternalBrowser(Activity activity, String str) {
        Intent intent;
        Uri parse = Uri.parse(str);
        if (parse.getHost() != null && parse.getHost().endsWith(".facebook.com") && parse.getPath().startsWith("/groups/")) {
            intent = new Intent("android.intent.action.VIEW", parse);
            intent.setPackage(FACEBOOK_PACKAGE_NAME);
            if (doLaunchActivity(activity, intent)) {
                return true;
            }
        }
        if (VERSION.SDK_INT < 18 || !CustomTabsChecker.isChromeCustomTabsSupported(activity)) {
            intent = new Intent("android.intent.action.VIEW", parse);
            intent.setPackage(CHROME_PACKAGE_NAME);
            if (doLaunchActivity(activity, intent)) {
                return true;
            }
            return doLaunchActivity(activity, new Intent("android.intent.action.VIEW", parse));
        }
        CustomTabsIntent build = new Builder().build();
        build.intent.setPackage(CHROME_PACKAGE_NAME);
        build.launchUrl(activity, parse);
        return true;
    }

    private static ApplicationInfo getApplicationInfo(Activity activity, String str) {
        try {
            return activity.getPackageManager().getApplicationInfo(str, 0);
        } catch (NameNotFoundException e) {
            return null;
        }
    }

    private static boolean doLaunchActivity(Activity activity, Intent intent) {
        try {
            activity.startActivity(intent);
            return true;
        } catch (ActivityNotFoundException e) {
            return false;
        }
    }
}
