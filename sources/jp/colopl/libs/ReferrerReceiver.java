package p018jp.colopl.libs;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.text.TextUtils;
import android.util.Log;
import com.google.android.apps.analytics.AnalyticsReceiver;
import java.net.URLDecoder;
import java.util.TreeMap;
import jp.appAdForce.android.InstallReceiver;
import p017io.fabric.sdk.android.services.events.EventsFilesManager;
import p018jp.colopl.drapro.AnalyticsHelper;
import p018jp.colopl.drapro.ColoplApplication;
import p018jp.colopl.util.Util;

/* renamed from: jp.colopl.libs.ReferrerReceiver */
public class ReferrerReceiver extends BroadcastReceiver {
    private static final String TAG = "ReferrerReceiver";

    private String filterReferrer(String str, String str2) {
        String[] split = str.split("&");
        if (split == null) {
            return null;
        }
        StringBuilder sb = new StringBuilder();
        TreeMap treeMap = new TreeMap();
        for (String split2 : split) {
            String[] split3 = split2.split("=", 2);
            if (split3 != null && split3.length == 2) {
                String str3 = split3[0];
                if (str3.startsWith(str2)) {
                    str3 = str3.substring(str2.length());
                }
                treeMap.put(str3, split3[1]);
            }
        }
        for (String str4 : treeMap.keySet()) {
            if (sb.length() != 0) {
                sb.append("&");
            }
            sb.append(str4).append("=").append((String) treeMap.get(str4));
        }
        return sb.toString();
    }

    public void onReceive(Context context, Intent intent) {
        String str;
        new InstallReceiver().onReceive(context, intent);
        new AnalyticsReceiver().onReceive(context, intent);
        String stringExtra = intent.getStringExtra("referrer");
        if (!TextUtils.isEmpty(stringExtra)) {
            if (stringExtra.startsWith("LINE_")) {
                Util.dLog(TAG, String.format("LINE referrer: %s", new Object[]{stringExtra}));
                int lastIndexOf = stringExtra.lastIndexOf(EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR);
                if (lastIndexOf != -1) {
                    try {
                        str = stringExtra.substring(lastIndexOf + 1);
                    } catch (IndexOutOfBoundsException e) {
                        return;
                    }
                } else {
                    return;
                }
            } else {
                str = stringExtra;
            }
            String decode = URLDecoder.decode(str);
            if (!TextUtils.isEmpty(decode)) {
                String filterReferrer = filterReferrer(decode, "clp_");
                if (!TextUtils.isEmpty(filterReferrer)) {
                    ((ColoplApplication) context.getApplicationContext()).getConfig().setReferrerAtInstalled(filterReferrer);
                    AnalyticsHelper.trackPageView("/referrer/" + filterReferrer);
                    Util.dLog(TAG, String.format("referrer: %s", new Object[]{str}));
                    Util.dLog(TAG, String.format("decodedReferrer: %s", new Object[]{decode}));
                    Util.dLog(TAG, String.format("filteredReferrer: %s", new Object[]{filterReferrer}));
                    Log.i(TAG, String.format("Installed referrer: %s", new Object[]{filterReferrer}));
                }
            }
        }
    }
}
