package p017io.fabric.sdk.android.services.common;

import android.content.Context;
import android.os.Bundle;
import android.text.TextUtils;
import p017io.fabric.sdk.android.Fabric;

/* renamed from: io.fabric.sdk.android.services.common.ApiKey */
public class ApiKey {
    static final String CRASHLYTICS_API_KEY = "com.crashlytics.ApiKey";
    static final String FABRIC_API_KEY = "io.fabric.ApiKey";
    static final String STRING_TWITTER_CONSUMER_SECRET = "@string/twitter_consumer_secret";

    @Deprecated
    public static String getApiKey(Context context) {
        Fabric.getLogger().mo20982w(Fabric.TAG, "getApiKey(context) is deprecated, please upgrade kit(s) to the latest version.");
        return new ApiKey().getValue(context);
    }

    @Deprecated
    public static String getApiKey(Context context, boolean z) {
        Fabric.getLogger().mo20982w(Fabric.TAG, "getApiKey(context, debug) is deprecated, please upgrade kit(s) to the latest version.");
        return new ApiKey().getValue(context);
    }

    /* access modifiers changed from: protected */
    public String buildApiKeyInstructions() {
        return "Fabric could not be initialized, API key missing from AndroidManifest.xml. Add the following tag to your Application element \n\t<meta-data android:name=\"io.fabric.ApiKey\" android:value=\"YOUR_API_KEY\"/>";
    }

    /* access modifiers changed from: protected */
    public String getApiKeyFromFirebaseAppId(Context context) {
        return new FirebaseInfo().getApiKeyFromFirebaseAppId(context);
    }

    /* access modifiers changed from: protected */
    public String getApiKeyFromManifest(Context context) {
        String str = null;
        try {
            Bundle bundle = context.getPackageManager().getApplicationInfo(context.getPackageName(), 128).metaData;
            if (bundle == null) {
                return null;
            }
            String string = bundle.getString(FABRIC_API_KEY);
            try {
                if (STRING_TWITTER_CONSUMER_SECRET.equals(string)) {
                    Fabric.getLogger().mo20969d(Fabric.TAG, "Ignoring bad default value for Fabric ApiKey set by FirebaseUI-Auth");
                } else {
                    str = string;
                }
                if (str != null) {
                    return str;
                }
                Fabric.getLogger().mo20969d(Fabric.TAG, "Falling back to Crashlytics key lookup from Manifest");
                return bundle.getString(CRASHLYTICS_API_KEY);
            } catch (Exception e) {
                e = e;
                str = string;
                Fabric.getLogger().mo20969d(Fabric.TAG, "Caught non-fatal exception while retrieving apiKey: " + e);
                return str;
            }
        } catch (Exception e2) {
            e = e2;
        }
    }

    /* access modifiers changed from: protected */
    public String getApiKeyFromStrings(Context context) {
        int resourcesIdentifier = CommonUtils.getResourcesIdentifier(context, FABRIC_API_KEY, "string");
        if (resourcesIdentifier == 0) {
            Fabric.getLogger().mo20969d(Fabric.TAG, "Falling back to Crashlytics key lookup from Strings");
            resourcesIdentifier = CommonUtils.getResourcesIdentifier(context, CRASHLYTICS_API_KEY, "string");
        }
        if (resourcesIdentifier != 0) {
            return context.getResources().getString(resourcesIdentifier);
        }
        return null;
    }

    public String getValue(Context context) {
        String apiKeyFromManifest = getApiKeyFromManifest(context);
        if (TextUtils.isEmpty(apiKeyFromManifest)) {
            apiKeyFromManifest = getApiKeyFromStrings(context);
        }
        if (TextUtils.isEmpty(apiKeyFromManifest)) {
            apiKeyFromManifest = getApiKeyFromFirebaseAppId(context);
        }
        if (TextUtils.isEmpty(apiKeyFromManifest)) {
            logErrorOrThrowException(context);
        }
        return apiKeyFromManifest;
    }

    /* access modifiers changed from: protected */
    public void logErrorOrThrowException(Context context) {
        if (Fabric.isDebuggable() || CommonUtils.isAppDebuggable(context)) {
            throw new IllegalArgumentException(buildApiKeyInstructions());
        }
        Fabric.getLogger().mo20971e(Fabric.TAG, buildApiKeyInstructions());
    }
}
