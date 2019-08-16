package p017io.fabric.sdk.android.services.common;

import android.content.Context;
import android.text.TextUtils;
import p017io.fabric.sdk.android.Fabric;

/* renamed from: io.fabric.sdk.android.services.common.FirebaseInfo */
public class FirebaseInfo {
    static final String AUTO_INITIALIZE = "io.fabric.auto_initialize";
    static final String FIREBASE_FEATURE_SWITCH = "com.crashlytics.useFirebaseAppId";
    static final String GOOGLE_APP_ID = "google_app_id";

    /* access modifiers changed from: 0000 */
    public String createApiKeyFromFirebaseAppId(String str) {
        return CommonUtils.sha256(str).substring(0, 40);
    }

    /* access modifiers changed from: 0000 */
    public String getApiKeyFromFirebaseAppId(Context context) {
        int resourcesIdentifier = CommonUtils.getResourcesIdentifier(context, GOOGLE_APP_ID, "string");
        if (resourcesIdentifier == 0) {
            return null;
        }
        Fabric.getLogger().mo20969d(Fabric.TAG, "Generating Crashlytics ApiKey from google_app_id in Strings");
        return createApiKeyFromFirebaseAppId(context.getResources().getString(resourcesIdentifier));
    }

    /* access modifiers changed from: 0000 */
    public boolean hasApiKey(Context context) {
        return !TextUtils.isEmpty(new ApiKey().getApiKeyFromManifest(context)) || !TextUtils.isEmpty(new ApiKey().getApiKeyFromStrings(context));
    }

    /* access modifiers changed from: 0000 */
    public boolean hasGoogleAppId(Context context) {
        int resourcesIdentifier = CommonUtils.getResourcesIdentifier(context, GOOGLE_APP_ID, "string");
        return resourcesIdentifier != 0 && !TextUtils.isEmpty(context.getResources().getString(resourcesIdentifier));
    }

    public boolean isAutoInitializeFlagEnabled(Context context) {
        int resourcesIdentifier = CommonUtils.getResourcesIdentifier(context, AUTO_INITIALIZE, "bool");
        if (resourcesIdentifier == 0) {
            return false;
        }
        boolean z = context.getResources().getBoolean(resourcesIdentifier);
        if (!z) {
            return z;
        }
        Fabric.getLogger().mo20969d(Fabric.TAG, "Found Fabric auto-initialization flag for joint Firebase/Fabric customers");
        return z;
    }

    public boolean isFirebaseCrashlyticsEnabled(Context context) {
        if (CommonUtils.getBooleanResourceValue(context, FIREBASE_FEATURE_SWITCH, false)) {
            return true;
        }
        return hasGoogleAppId(context) && !hasApiKey(context);
    }
}
