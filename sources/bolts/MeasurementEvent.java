package bolts;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import com.appsflyer.share.Constants;
import com.facebook.internal.NativeProtocol;
import java.lang.reflect.Method;
import java.util.Collection;
import java.util.Map;
import org.json.JSONArray;
import org.json.JSONObject;

public class MeasurementEvent {
    public static final String APP_LINK_NAVIGATE_IN_EVENT_NAME = "al_nav_in";
    public static final String APP_LINK_NAVIGATE_OUT_EVENT_NAME = "al_nav_out";
    public static final String MEASUREMENT_EVENT_ARGS_KEY = "event_args";
    public static final String MEASUREMENT_EVENT_NAME_KEY = "event_name";
    public static final String MEASUREMENT_EVENT_NOTIFICATION_NAME = "com.parse.bolts.measurement_event";
    private Context appContext;
    private Bundle args;
    private String name;

    private MeasurementEvent(Context context, String str, Bundle bundle) {
        this.appContext = context.getApplicationContext();
        this.name = str;
        this.args = bundle;
    }

    private static Bundle getApplinkLogData(Context context, String str, Bundle bundle, Intent intent) {
        Bundle bundle2 = new Bundle();
        ComponentName resolveActivity = intent.resolveActivity(context.getPackageManager());
        if (resolveActivity != null) {
            bundle2.putString("class", resolveActivity.getShortClassName());
        }
        if (APP_LINK_NAVIGATE_OUT_EVENT_NAME.equals(str)) {
            if (resolveActivity != null) {
                bundle2.putString("package", resolveActivity.getPackageName());
            }
            if (intent.getData() != null) {
                bundle2.putString("outputURL", intent.getData().toString());
            }
            if (intent.getScheme() != null) {
                bundle2.putString("outputURLScheme", intent.getScheme());
            }
        } else if (APP_LINK_NAVIGATE_IN_EVENT_NAME.equals(str)) {
            if (intent.getData() != null) {
                bundle2.putString("inputURL", intent.getData().toString());
            }
            if (intent.getScheme() != null) {
                bundle2.putString("inputURLScheme", intent.getScheme());
            }
        }
        for (String str2 : bundle.keySet()) {
            Object obj = bundle.get(str2);
            if (obj instanceof Bundle) {
                for (String str3 : ((Bundle) obj).keySet()) {
                    String objectToJSONString = objectToJSONString(((Bundle) obj).get(str3));
                    if (str2.equals("referer_app_link")) {
                        if (str3.equalsIgnoreCase("url")) {
                            bundle2.putString("refererURL", objectToJSONString);
                        } else if (str3.equalsIgnoreCase(NativeProtocol.BRIDGE_ARG_APP_NAME_STRING)) {
                            bundle2.putString("refererAppName", objectToJSONString);
                        } else if (str3.equalsIgnoreCase("package")) {
                            bundle2.putString("sourceApplication", objectToJSONString);
                        }
                    }
                    bundle2.putString(str2 + Constants.URL_PATH_DELIMITER + str3, objectToJSONString);
                }
            } else {
                String objectToJSONString2 = objectToJSONString(obj);
                if (str2.equals("target_url")) {
                    Uri parse = Uri.parse(objectToJSONString2);
                    bundle2.putString("targetURL", parse.toString());
                    bundle2.putString("targetURLHost", parse.getHost());
                } else {
                    bundle2.putString(str2, objectToJSONString2);
                }
            }
        }
        return bundle2;
    }

    private static String objectToJSONString(Object obj) {
        if (obj == null) {
            return null;
        }
        if ((obj instanceof JSONArray) || (obj instanceof JSONObject)) {
            return obj.toString();
        }
        try {
            return obj instanceof Collection ? new JSONArray((Collection) obj).toString() : obj instanceof Map ? new JSONObject((Map) obj).toString() : obj.toString();
        } catch (Exception e) {
            return null;
        }
    }

    private void sendBroadcast() {
        if (this.name == null) {
            Log.d(getClass().getName(), "Event name is required");
        }
        try {
            Class cls = Class.forName("android.support.v4.content.LocalBroadcastManager");
            Method method = cls.getMethod("getInstance", new Class[]{Context.class});
            Method method2 = cls.getMethod("sendBroadcast", new Class[]{Intent.class});
            Object invoke = method.invoke(null, new Object[]{this.appContext});
            Intent intent = new Intent(MEASUREMENT_EVENT_NOTIFICATION_NAME);
            intent.putExtra(MEASUREMENT_EVENT_NAME_KEY, this.name);
            intent.putExtra(MEASUREMENT_EVENT_ARGS_KEY, this.args);
            method2.invoke(invoke, new Object[]{intent});
        } catch (Exception e) {
            Log.d(getClass().getName(), "LocalBroadcastManager in android support library is required to raise bolts event.");
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:6:0x0014  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    static void sendBroadcastEvent(android.content.Context r5, java.lang.String r6, android.content.Intent r7, java.util.Map<java.lang.String, java.lang.String> r8) {
        /*
            android.os.Bundle r1 = new android.os.Bundle
            r1.<init>()
            if (r7 == 0) goto L_0x0071
            android.os.Bundle r0 = bolts.AppLinks.getAppLinkData(r7)
            if (r0 == 0) goto L_0x0033
            android.os.Bundle r0 = getApplinkLogData(r5, r6, r0, r7)
            r2 = r0
        L_0x0012:
            if (r8 == 0) goto L_0x0068
            java.util.Set r0 = r8.keySet()
            java.util.Iterator r3 = r0.iterator()
        L_0x001c:
            boolean r0 = r3.hasNext()
            if (r0 == 0) goto L_0x0068
            java.lang.Object r0 = r3.next()
            r1 = r0
            java.lang.String r1 = (java.lang.String) r1
            java.lang.Object r0 = r8.get(r1)
            java.lang.String r0 = (java.lang.String) r0
            r2.putString(r1, r0)
            goto L_0x001c
        L_0x0033:
            android.net.Uri r0 = r7.getData()
            if (r0 == 0) goto L_0x0042
            java.lang.String r2 = "intentData"
            java.lang.String r0 = r0.toString()
            r1.putString(r2, r0)
        L_0x0042:
            android.os.Bundle r2 = r7.getExtras()
            if (r2 == 0) goto L_0x0071
            java.util.Set r0 = r2.keySet()
            java.util.Iterator r3 = r0.iterator()
        L_0x0050:
            boolean r0 = r3.hasNext()
            if (r0 == 0) goto L_0x0071
            java.lang.Object r0 = r3.next()
            java.lang.String r0 = (java.lang.String) r0
            java.lang.Object r4 = r2.get(r0)
            java.lang.String r4 = objectToJSONString(r4)
            r1.putString(r0, r4)
            goto L_0x0050
        L_0x0068:
            bolts.MeasurementEvent r0 = new bolts.MeasurementEvent
            r0.<init>(r5, r6, r2)
            r0.sendBroadcast()
            return
        L_0x0071:
            r2 = r1
            goto L_0x0012
        */
        throw new UnsupportedOperationException("Method not decompiled: bolts.MeasurementEvent.sendBroadcastEvent(android.content.Context, java.lang.String, android.content.Intent, java.util.Map):void");
    }
}
