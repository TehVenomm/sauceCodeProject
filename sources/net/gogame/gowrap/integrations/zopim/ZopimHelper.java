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
import java.util.List;
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
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < str.length(); i++) {
            Object obj;
            char charAt = str.charAt(i);
            if ((charAt < '0' || charAt > '9') && ((charAt < 'A' || charAt > 'Z') && !((charAt >= 'a' && charAt <= 'z') || charAt == '-' || charAt == '_'))) {
                obj = null;
            } else {
                obj = 1;
            }
            if (obj != null) {
                stringBuilder.append(charAt);
            } else {
                stringBuilder.append('_');
            }
        }
        return stringBuilder.toString();
    }

    private static String makeTag(String str, String str2) {
        return encodeTagComponent(str) + "-" + encodeTagComponent(str2);
    }

    private static String getAppLabel(Context context) {
        CharSequence applicationLabel;
        PackageManager packageManager = context.getPackageManager();
        ApplicationInfo applicationInfo = null;
        try {
            applicationInfo = packageManager.getApplicationInfo(context.getApplicationInfo().packageName, 0);
        } catch (NameNotFoundException e) {
        }
        if (applicationInfo != null) {
            applicationLabel = packageManager.getApplicationLabel(applicationInfo);
        } else {
            applicationLabel = context.getApplicationContext().getPackageName();
        }
        return (String) applicationLabel;
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
        List arrayList = new ArrayList();
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
