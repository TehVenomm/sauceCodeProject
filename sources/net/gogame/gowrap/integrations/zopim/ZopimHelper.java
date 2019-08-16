package net.gogame.gowrap.integrations.zopim;

import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import com.zopim.android.sdk.api.ZopimChat;
import com.zopim.android.sdk.api.ZopimChat.DefaultConfig;
import com.zopim.android.sdk.api.ZopimChat.SessionConfig;
import java.util.ArrayList;
import net.gogame.gowrap.support.ClassUtils;

public final class ZopimHelper {
    private ZopimHelper() {
    }

    public static boolean isIntegrated() {
        return ClassUtils.hasClass("com.zopim.android.sdk.api.ZopimChat");
    }

    public static void initChat(Context context, String str) {
        String packageName = context.getApplicationContext().getPackageName();
        ((DefaultConfig) ((DefaultConfig) ZopimChat.init(str).visitorPathOne("http://www.gogame.net/support/android/" + packageName)).visitorPathTwo(getAppLabel(context))).build();
    }

    private static String encodeTagComponent(String str) {
        boolean z;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < str.length(); i++) {
            char charAt = str.charAt(i);
            if ((charAt < '0' || charAt > '9') && ((charAt < 'A' || charAt > 'Z') && !((charAt >= 'a' && charAt <= 'z') || charAt == '-' || charAt == '_'))) {
                z = false;
            } else {
                z = true;
            }
            if (z) {
                sb.append(charAt);
            } else {
                sb.append('_');
            }
        }
        return sb.toString();
    }

    private static String makeTag(String str, String str2) {
        return encodeTagComponent(str) + "-" + encodeTagComponent(str2);
    }

    private static String getAppLabel(Context context) {
        Object packageName;
        PackageManager packageManager = context.getPackageManager();
        ApplicationInfo applicationInfo = null;
        try {
            applicationInfo = packageManager.getApplicationInfo(context.getApplicationInfo().packageName, 0);
        } catch (NameNotFoundException e) {
        }
        if (applicationInfo != null) {
            packageName = packageManager.getApplicationLabel(applicationInfo);
        } else {
            packageName = context.getApplicationContext().getPackageName();
        }
        return (String) packageName;
    }

    private static String getAppVersion(Context context) {
        PackageInfo packageInfo;
        try {
            packageInfo = context.getPackageManager().getPackageInfo(context.getPackageName(), 0);
        } catch (NameNotFoundException e) {
            packageInfo = null;
        }
        if (packageInfo != null) {
            return packageInfo.versionName;
        }
        return null;
    }

    public static SessionConfig getSessionConfig(Context context, String str) {
        Context applicationContext = context.getApplicationContext();
        String packageName = applicationContext.getPackageName();
        String appLabel = getAppLabel(applicationContext);
        String appVersion = getAppVersion(applicationContext);
        ArrayList arrayList = new ArrayList();
        if (packageName != null) {
            arrayList.add(makeTag("packageName", packageName));
        }
        if (appLabel != null) {
            arrayList.add(makeTag("appName", appLabel));
        }
        if (appVersion != null) {
            arrayList.add(makeTag("appVersion", appVersion));
        }
        if (str != null) {
            arrayList.add(makeTag("guid", str));
        }
        return (SessionConfig) new SessionConfig().tags((String[]) arrayList.toArray(new String[arrayList.size()]));
    }
}
