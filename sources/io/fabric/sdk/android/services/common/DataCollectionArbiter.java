package p017io.fabric.sdk.android.services.common;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import p017io.fabric.sdk.android.Fabric;

/* renamed from: io.fabric.sdk.android.services.common.DataCollectionArbiter */
public class DataCollectionArbiter {
    private static final String FIREBASE_CRASHLYTICS_COLLECTION_ENABLED = "firebase_crashlytics_collection_enabled";
    private static final String FIREBASE_CRASHLYTICS_PREFS = "com.google.firebase.crashlytics.prefs";
    private static DataCollectionArbiter instance;
    private static Object instanceLock = new Object();
    private volatile boolean crashlyticsDataCollectionEnabled;
    private volatile boolean crashlyticsDataCollectionExplicitlySet;
    private final FirebaseApp firebaseApp;
    private boolean isUnity = false;
    private final SharedPreferences sharedPreferences;

    private DataCollectionArbiter(Context context) {
        boolean z;
        boolean z2;
        boolean z3 = true;
        if (context == null) {
            throw new RuntimeException("null context");
        }
        this.sharedPreferences = context.getSharedPreferences(FIREBASE_CRASHLYTICS_PREFS, 0);
        this.firebaseApp = FirebaseAppImpl.getInstance(context);
        if (this.sharedPreferences.contains(FIREBASE_CRASHLYTICS_COLLECTION_ENABLED)) {
            z = this.sharedPreferences.getBoolean(FIREBASE_CRASHLYTICS_COLLECTION_ENABLED, true);
            z2 = true;
        } else {
            try {
                PackageManager packageManager = context.getPackageManager();
                if (packageManager != null) {
                    ApplicationInfo applicationInfo = packageManager.getApplicationInfo(context.getPackageName(), 128);
                    if (!(applicationInfo == null || applicationInfo.metaData == null || !applicationInfo.metaData.containsKey(FIREBASE_CRASHLYTICS_COLLECTION_ENABLED))) {
                        z = applicationInfo.metaData.getBoolean(FIREBASE_CRASHLYTICS_COLLECTION_ENABLED);
                        z2 = true;
                    }
                }
                z = true;
                z2 = false;
            } catch (NameNotFoundException e) {
                Fabric.getLogger().mo20970d(Fabric.TAG, "Unable to get PackageManager. Falling through", e);
                z = true;
                z2 = false;
            }
        }
        this.crashlyticsDataCollectionEnabled = z;
        this.crashlyticsDataCollectionExplicitlySet = z2;
        if (CommonUtils.resolveUnityEditorVersion(context) == null) {
            z3 = false;
        }
        this.isUnity = z3;
    }

    public static DataCollectionArbiter getInstance(Context context) {
        DataCollectionArbiter dataCollectionArbiter;
        synchronized (instanceLock) {
            if (instance == null) {
                instance = new DataCollectionArbiter(context);
            }
            dataCollectionArbiter = instance;
        }
        return dataCollectionArbiter;
    }

    public static void resetForTesting(Context context) {
        synchronized (instanceLock) {
            instance = new DataCollectionArbiter(context);
        }
    }

    public boolean isDataCollectionEnabled() {
        if (this.isUnity && this.crashlyticsDataCollectionExplicitlySet) {
            return this.crashlyticsDataCollectionEnabled;
        }
        if (this.firebaseApp != null) {
            return this.firebaseApp.isDataCollectionDefaultEnabled();
        }
        return true;
    }

    @SuppressLint({"CommitPrefEdits", "ApplySharedPref"})
    public void setCrashlyticsDataCollectionEnabled(boolean z) {
        this.crashlyticsDataCollectionEnabled = z;
        this.crashlyticsDataCollectionExplicitlySet = true;
        this.sharedPreferences.edit().putBoolean(FIREBASE_CRASHLYTICS_COLLECTION_ENABLED, z).commit();
    }

    public boolean shouldAutoInitialize() {
        return this.crashlyticsDataCollectionEnabled;
    }
}
