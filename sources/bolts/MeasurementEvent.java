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
            String objectToJSONString;
            if (obj instanceof Bundle) {
                for (String objectToJSONString2 : ((Bundle) obj).keySet()) {
                    String objectToJSONString3 = objectToJSONString(((Bundle) obj).get(objectToJSONString2));
                    if (str2.equals("referer_app_link")) {
                        if (objectToJSONString2.equalsIgnoreCase("url")) {
                            bundle2.putString("refererURL", objectToJSONString3);
                        } else if (objectToJSONString2.equalsIgnoreCase(NativeProtocol.BRIDGE_ARG_APP_NAME_STRING)) {
                            bundle2.putString("refererAppName", objectToJSONString3);
                        } else if (objectToJSONString2.equalsIgnoreCase("package")) {
                            bundle2.putString("sourceApplication", objectToJSONString3);
                        }
                    }
                    bundle2.putString(str2 + Constants.URL_PATH_DELIMITER + objectToJSONString2, objectToJSONString3);
                }
            } else {
                objectToJSONString2 = objectToJSONString(obj);
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

    static void sendBroadcastEvent(Context context, String str, Intent intent, Map<String, String> map) {
        Bundle applinkLogData;
        Bundle bundle = new Bundle();
        if (intent != null) {
            Bundle appLinkData = AppLinks.getAppLinkData(intent);
            if (appLinkData != null) {
                applinkLogData = getApplinkLogData(context, str, appLinkData, intent);
                if (map != null) {
                    for (String str2 : map.keySet()) {
                        applinkLogData.putString(str2, (String) map.get(str2));
                    }
                }
                new MeasurementEvent(context, str, applinkLogData).sendBroadcast();
            }
            Uri data = intent.getData();
            if (data != null) {
                bundle.putString("intentData", data.toString());
            }
            applinkLogData = intent.getExtras();
            if (applinkLogData != null) {
                for (String str3 : applinkLogData.keySet()) {
                    bundle.putString(str3, objectToJSONString(applinkLogData.get(str3)));
                }
            }
        }
        applinkLogData = bundle;
        if (map != null) {
            for (String str22 : map.keySet()) {
                applinkLogData.putString(str22, (String) map.get(str22));
            }
        }
        new MeasurementEvent(context, str, applinkLogData).sendBroadcast();
    }
}
