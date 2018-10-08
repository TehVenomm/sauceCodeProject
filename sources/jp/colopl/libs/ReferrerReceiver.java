package jp.colopl.libs;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.text.TextUtils;
import android.util.Log;
import com.google.android.apps.analytics.AnalyticsReceiver;
import io.fabric.sdk.android.services.events.EventsFilesManager;
import java.net.URLDecoder;
import java.util.TreeMap;
import jp.appAdForce.android.InstallReceiver;
import jp.colopl.drapro.AnalyticsHelper;
import jp.colopl.drapro.ColoplApplication;
import jp.colopl.util.Util;

public class ReferrerReceiver extends BroadcastReceiver {
    private static final String TAG = "ReferrerReceiver";

    private String filterReferrer(String str, String str2) {
        String[] split = str.split("&");
        if (split == null) {
            return null;
        }
        StringBuilder stringBuilder = new StringBuilder();
        TreeMap treeMap = new TreeMap();
        for (String split2 : split) {
            String[] split3 = split2.split("=", 2);
            if (split3 != null && split3.length == 2) {
                Object obj = split3[0];
                if (obj.startsWith(str2)) {
                    obj = obj.substring(str2.length());
                }
                treeMap.put(obj, split3[1]);
            }
        }
        for (String split22 : treeMap.keySet()) {
            if (stringBuilder.length() != 0) {
                stringBuilder.append("&");
            }
            stringBuilder.append(split22).append("=").append((String) treeMap.get(split22));
        }
        return stringBuilder.toString();
    }

    public void onReceive(Context context, Intent intent) {
        new InstallReceiver().onReceive(context, intent);
        new AnalyticsReceiver().onReceive(context, intent);
        String stringExtra = intent.getStringExtra("referrer");
        if (!TextUtils.isEmpty(stringExtra)) {
            String substring;
            if (stringExtra.startsWith("LINE_")) {
                Util.dLog(TAG, String.format("LINE referrer: %s", new Object[]{stringExtra}));
                int lastIndexOf = stringExtra.lastIndexOf(EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR);
                if (lastIndexOf != -1) {
                    try {
                        substring = stringExtra.substring(lastIndexOf + 1);
                    } catch (IndexOutOfBoundsException e) {
                        return;
                    }
                }
                return;
            }
            substring = stringExtra;
            Object decode = URLDecoder.decode(substring);
            if (!TextUtils.isEmpty(decode)) {
                String filterReferrer = filterReferrer(decode, "clp_");
                if (!TextUtils.isEmpty(filterReferrer)) {
                    ((ColoplApplication) context.getApplicationContext()).getConfig().setReferrerAtInstalled(filterReferrer);
                    AnalyticsHelper.trackPageView("/referrer/" + filterReferrer);
                    Util.dLog(TAG, String.format("referrer: %s", new Object[]{substring}));
                    Util.dLog(TAG, String.format("decodedReferrer: %s", new Object[]{decode}));
                    Util.dLog(TAG, String.format("filteredReferrer: %s", new Object[]{filterReferrer}));
                    Log.i(TAG, String.format("Installed referrer: %s", new Object[]{filterReferrer}));
                }
            }
        }
    }
}
