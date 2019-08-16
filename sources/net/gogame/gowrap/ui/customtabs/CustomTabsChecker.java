package net.gogame.gowrap.p019ui.customtabs;

import android.content.Context;
import android.content.Intent;
import java.util.List;

/* renamed from: net.gogame.gowrap.ui.customtabs.CustomTabsChecker */
public class CustomTabsChecker {
    private static final String CHROME_PACKAGE = "com.android.chrome";
    private static final String SERVICE_ACTION = "android.support.customtabs.action.CustomTabsService";

    public static boolean isChromeCustomTabsSupported(Context context) {
        Intent intent = new Intent("android.support.customtabs.action.CustomTabsService");
        intent.setPackage(CHROME_PACKAGE);
        List queryIntentServices = context.getPackageManager().queryIntentServices(intent, 0);
        if (queryIntentServices == null || queryIntentServices.isEmpty()) {
            return false;
        }
        return true;
    }
}
