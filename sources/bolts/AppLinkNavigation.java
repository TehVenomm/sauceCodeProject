package bolts;

import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.net.Uri;
import android.os.Bundle;
import android.util.SparseArray;
import bolts.AppLink.Target;
import com.facebook.appevents.AppEventsConstants;
import com.facebook.applinks.AppLinkData;
import com.facebook.internal.AnalyticsEvents;
import java.net.URL;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import p017io.fabric.sdk.android.services.settings.SettingsJsonConstants;

public class AppLinkNavigation {
    private static final String KEY_NAME_REFERER_APP_LINK = "referer_app_link";
    private static final String KEY_NAME_REFERER_APP_LINK_APP_NAME = "app_name";
    private static final String KEY_NAME_REFERER_APP_LINK_PACKAGE = "package";
    private static final String KEY_NAME_USER_AGENT = "user_agent";
    private static final String KEY_NAME_VERSION = "version";
    private static final String VERSION = "1.0";
    private static AppLinkResolver defaultResolver;
    private final AppLink appLink;
    private final Bundle appLinkData;
    private final Bundle extras;

    public enum NavigationResult {
        FAILED("failed", false),
        WEB(AnalyticsEvents.PARAMETER_SHARE_DIALOG_SHOW_WEB, true),
        APP(SettingsJsonConstants.APP_KEY, true);
        
        private String code;
        private boolean succeeded;

        private NavigationResult(String str, boolean z) {
            this.code = str;
            this.succeeded = z;
        }

        public String getCode() {
            return this.code;
        }

        public boolean isSucceeded() {
            return this.succeeded;
        }
    }

    public AppLinkNavigation(AppLink appLink2, Bundle bundle, Bundle bundle2) {
        if (appLink2 == null) {
            throw new IllegalArgumentException("appLink must not be null.");
        }
        if (bundle == null) {
            bundle = new Bundle();
        }
        if (bundle2 == null) {
            bundle2 = new Bundle();
        }
        this.appLink = appLink2;
        this.extras = bundle;
        this.appLinkData = bundle2;
    }

    private Bundle buildAppLinkDataForNavigation(Context context) {
        Bundle bundle = new Bundle();
        Bundle bundle2 = new Bundle();
        if (context != null) {
            String packageName = context.getPackageName();
            if (packageName != null) {
                bundle2.putString(KEY_NAME_REFERER_APP_LINK_PACKAGE, packageName);
            }
            ApplicationInfo applicationInfo = context.getApplicationInfo();
            if (applicationInfo != null) {
                String string = context.getString(applicationInfo.labelRes);
                if (string != null) {
                    bundle2.putString("app_name", string);
                }
            }
        }
        bundle.putAll(getAppLinkData());
        bundle.putString("target_url", getAppLink().getSourceUrl().toString());
        bundle.putString("version", "1.0");
        bundle.putString(KEY_NAME_USER_AGENT, "Bolts Android 1.4.0");
        bundle.putBundle(KEY_NAME_REFERER_APP_LINK, bundle2);
        bundle.putBundle(AppLinkData.ARGUMENTS_EXTRAS_KEY, getExtras());
        return bundle;
    }

    public static AppLinkResolver getDefaultResolver() {
        return defaultResolver;
    }

    private JSONObject getJSONForBundle(Bundle bundle) throws JSONException {
        JSONObject jSONObject = new JSONObject();
        for (String str : bundle.keySet()) {
            jSONObject.put(str, getJSONValue(bundle.get(str)));
        }
        return jSONObject;
    }

    private Object getJSONValue(Object obj) throws JSONException {
        int i = 0;
        if (obj instanceof Bundle) {
            return getJSONForBundle((Bundle) obj);
        }
        if (obj instanceof CharSequence) {
            return obj.toString();
        }
        if (obj instanceof List) {
            JSONArray jSONArray = new JSONArray();
            for (Object jSONValue : (List) obj) {
                jSONArray.put(getJSONValue(jSONValue));
            }
            return jSONArray;
        } else if (obj instanceof SparseArray) {
            JSONArray jSONArray2 = new JSONArray();
            SparseArray sparseArray = (SparseArray) obj;
            while (i < sparseArray.size()) {
                jSONArray2.put(sparseArray.keyAt(i), getJSONValue(sparseArray.valueAt(i)));
                i++;
            }
            return jSONArray2;
        } else if (obj instanceof Character) {
            return obj.toString();
        } else {
            if (obj instanceof Boolean) {
                return obj;
            }
            if (obj instanceof Number) {
                return ((obj instanceof Double) || (obj instanceof Float)) ? Double.valueOf(((Number) obj).doubleValue()) : Long.valueOf(((Number) obj).longValue());
            }
            if (obj instanceof boolean[]) {
                JSONArray jSONArray3 = new JSONArray();
                boolean[] zArr = (boolean[]) obj;
                int length = zArr.length;
                while (i < length) {
                    jSONArray3.put(getJSONValue(Boolean.valueOf(zArr[i])));
                    i++;
                }
                return jSONArray3;
            } else if (obj instanceof char[]) {
                JSONArray jSONArray4 = new JSONArray();
                char[] cArr = (char[]) obj;
                int length2 = cArr.length;
                while (i < length2) {
                    jSONArray4.put(getJSONValue(Character.valueOf(cArr[i])));
                    i++;
                }
                return jSONArray4;
            } else if (obj instanceof CharSequence[]) {
                JSONArray jSONArray5 = new JSONArray();
                CharSequence[] charSequenceArr = (CharSequence[]) obj;
                int length3 = charSequenceArr.length;
                while (i < length3) {
                    jSONArray5.put(getJSONValue(charSequenceArr[i]));
                    i++;
                }
                return jSONArray5;
            } else if (obj instanceof double[]) {
                JSONArray jSONArray6 = new JSONArray();
                double[] dArr = (double[]) obj;
                int length4 = dArr.length;
                while (i < length4) {
                    jSONArray6.put(getJSONValue(Double.valueOf(dArr[i])));
                    i++;
                }
                return jSONArray6;
            } else if (obj instanceof float[]) {
                JSONArray jSONArray7 = new JSONArray();
                float[] fArr = (float[]) obj;
                int length5 = fArr.length;
                while (i < length5) {
                    jSONArray7.put(getJSONValue(Float.valueOf(fArr[i])));
                    i++;
                }
                return jSONArray7;
            } else if (obj instanceof int[]) {
                JSONArray jSONArray8 = new JSONArray();
                int[] iArr = (int[]) obj;
                int length6 = iArr.length;
                while (i < length6) {
                    jSONArray8.put(getJSONValue(Integer.valueOf(iArr[i])));
                    i++;
                }
                return jSONArray8;
            } else if (obj instanceof long[]) {
                JSONArray jSONArray9 = new JSONArray();
                long[] jArr = (long[]) obj;
                int length7 = jArr.length;
                while (i < length7) {
                    jSONArray9.put(getJSONValue(Long.valueOf(jArr[i])));
                    i++;
                }
                return jSONArray9;
            } else if (obj instanceof short[]) {
                JSONArray jSONArray10 = new JSONArray();
                short[] sArr = (short[]) obj;
                int length8 = sArr.length;
                while (i < length8) {
                    jSONArray10.put(getJSONValue(Short.valueOf(sArr[i])));
                    i++;
                }
                return jSONArray10;
            } else if (!(obj instanceof String[])) {
                return null;
            } else {
                JSONArray jSONArray11 = new JSONArray();
                String[] strArr = (String[]) obj;
                int length9 = strArr.length;
                while (i < length9) {
                    jSONArray11.put(getJSONValue(strArr[i]));
                    i++;
                }
                return jSONArray11;
            }
        }
    }

    private static AppLinkResolver getResolver(Context context) {
        return getDefaultResolver() != null ? getDefaultResolver() : new WebViewAppLinkResolver(context);
    }

    public static NavigationResult navigate(Context context, AppLink appLink2) {
        return new AppLinkNavigation(appLink2, null, null).navigate(context);
    }

    public static Task<NavigationResult> navigateInBackground(Context context, Uri uri) {
        return navigateInBackground(context, uri, getResolver(context));
    }

    public static Task<NavigationResult> navigateInBackground(final Context context, Uri uri, AppLinkResolver appLinkResolver) {
        return appLinkResolver.getAppLinkFromUrlInBackground(uri).onSuccess((Continuation<TResult, TContinuationResult>) new Continuation<AppLink, NavigationResult>() {
            public NavigationResult then(Task<AppLink> task) throws Exception {
                return AppLinkNavigation.navigate(context, (AppLink) task.getResult());
            }
        }, Task.UI_THREAD_EXECUTOR);
    }

    public static Task<NavigationResult> navigateInBackground(Context context, String str) {
        return navigateInBackground(context, str, getResolver(context));
    }

    public static Task<NavigationResult> navigateInBackground(Context context, String str, AppLinkResolver appLinkResolver) {
        return navigateInBackground(context, Uri.parse(str), appLinkResolver);
    }

    public static Task<NavigationResult> navigateInBackground(Context context, URL url) {
        return navigateInBackground(context, url, getResolver(context));
    }

    public static Task<NavigationResult> navigateInBackground(Context context, URL url, AppLinkResolver appLinkResolver) {
        return navigateInBackground(context, Uri.parse(url.toString()), appLinkResolver);
    }

    private void sendAppLinkNavigateEventBroadcast(Context context, Intent intent, NavigationResult navigationResult, JSONException jSONException) {
        HashMap hashMap = new HashMap();
        if (jSONException != null) {
            hashMap.put("error", jSONException.getLocalizedMessage());
        }
        hashMap.put("success", navigationResult.isSucceeded() ? AppEventsConstants.EVENT_PARAM_VALUE_YES : AppEventsConstants.EVENT_PARAM_VALUE_NO);
        hashMap.put("type", navigationResult.getCode());
        MeasurementEvent.sendBroadcastEvent(context, MeasurementEvent.APP_LINK_NAVIGATE_OUT_EVENT_NAME, intent, hashMap);
    }

    public static void setDefaultResolver(AppLinkResolver appLinkResolver) {
        defaultResolver = appLinkResolver;
    }

    public AppLink getAppLink() {
        return this.appLink;
    }

    public Bundle getAppLinkData() {
        return this.appLinkData;
    }

    public Bundle getExtras() {
        return this.extras;
    }

    public NavigationResult navigate(Context context) {
        Intent intent;
        Intent intent2;
        PackageManager packageManager = context.getPackageManager();
        Bundle buildAppLinkDataForNavigation = buildAppLinkDataForNavigation(context);
        Iterator it = getAppLink().getTargets().iterator();
        while (true) {
            if (!it.hasNext()) {
                intent = null;
                break;
            }
            Target target = (Target) it.next();
            Intent intent3 = new Intent("android.intent.action.VIEW");
            if (target.getUrl() != null) {
                intent3.setData(target.getUrl());
            } else {
                intent3.setData(this.appLink.getSourceUrl());
            }
            intent3.setPackage(target.getPackageName());
            if (target.getClassName() != null) {
                intent3.setClassName(target.getPackageName(), target.getClassName());
            }
            intent3.putExtra("al_applink_data", buildAppLinkDataForNavigation);
            if (packageManager.resolveActivity(intent3, 65536) != null) {
                intent = intent3;
                break;
            }
        }
        NavigationResult navigationResult = NavigationResult.FAILED;
        if (intent != null) {
            navigationResult = NavigationResult.APP;
            intent2 = intent;
        } else {
            Uri webUrl = getAppLink().getWebUrl();
            if (webUrl != null) {
                try {
                    intent2 = new Intent("android.intent.action.VIEW", webUrl.buildUpon().appendQueryParameter("al_applink_data", getJSONForBundle(buildAppLinkDataForNavigation).toString()).build());
                    navigationResult = NavigationResult.WEB;
                } catch (JSONException e) {
                    sendAppLinkNavigateEventBroadcast(context, intent, NavigationResult.FAILED, e);
                    throw new RuntimeException(e);
                }
            } else {
                intent2 = null;
            }
        }
        sendAppLinkNavigateEventBroadcast(context, intent2, navigationResult, null);
        if (intent2 != null) {
            context.startActivity(intent2);
        }
        return navigationResult;
    }
}
