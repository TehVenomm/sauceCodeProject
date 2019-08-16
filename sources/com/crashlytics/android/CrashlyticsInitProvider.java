package com.crashlytics.android;

import android.content.ContentProvider;
import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.net.Uri;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.services.common.FirebaseInfo;

public class CrashlyticsInitProvider extends ContentProvider {
    private static final String TAG = "CrashlyticsInitProvider";

    interface EnabledCheckStrategy {
        boolean isCrashlyticsEnabled(Context context);
    }

    public int delete(Uri uri, String str, String[] strArr) {
        return 0;
    }

    public String getType(Uri uri) {
        return null;
    }

    public Uri insert(Uri uri, ContentValues contentValues) {
        return null;
    }

    public boolean onCreate() {
        Context context = getContext();
        if (shouldInitializeFabric(context, new FirebaseInfo(), new ManifestEnabledCheckStrategy())) {
            try {
                Fabric.with(context, new Crashlytics());
                Fabric.getLogger().mo20974i(TAG, "CrashlyticsInitProvider initialization successful");
            } catch (IllegalStateException e) {
                Fabric.getLogger().mo20974i(TAG, "CrashlyticsInitProvider initialization unsuccessful");
                return false;
            }
        } else {
            Fabric.getLogger().mo20974i(TAG, "CrashlyticsInitProvider skipping initialization");
        }
        return true;
    }

    public Cursor query(Uri uri, String[] strArr, String str, String[] strArr2, String str2) {
        return null;
    }

    /* access modifiers changed from: 0000 */
    public boolean shouldInitializeFabric(Context context, FirebaseInfo firebaseInfo, EnabledCheckStrategy enabledCheckStrategy) {
        return firebaseInfo.isFirebaseCrashlyticsEnabled(context) ? enabledCheckStrategy.isCrashlyticsEnabled(context) : firebaseInfo.isAutoInitializeFlagEnabled(context);
    }

    public int update(Uri uri, ContentValues contentValues, String str, String[] strArr) {
        return 0;
    }
}
