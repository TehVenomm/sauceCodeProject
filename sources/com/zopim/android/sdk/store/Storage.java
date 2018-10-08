package com.zopim.android.sdk.store;

import android.content.Context;
import android.util.Log;

public enum Storage {
    INSTANCE;
    
    private static final String LOG_TAG = null;
    private Context mAppContext;

    static {
        LOG_TAG = Storage.class.getSimpleName();
    }

    public static void init(Context context) {
        if (context == null) {
            Log.e(LOG_TAG, "Can not initialize storage. Context must not be null.");
            return;
        }
        INSTANCE.mAppContext = context.getApplicationContext();
    }

    private boolean isInitialized() {
        return this.mAppContext != null;
    }

    public static MachineIdStorage machineId() {
        if (INSTANCE.isInitialized()) {
            return new MachineIdPrefsStorage(INSTANCE.mAppContext);
        }
        Log.w(LOG_TAG, "Storage must be initialized first. Will return mocked storage implementation.");
        return new C0899b();
    }

    public static VisitorInfoStorage visitorInfo() {
        if (INSTANCE.isInitialized()) {
            return new VisitorInfoPrefsStorage(INSTANCE.mAppContext);
        }
        Log.w(LOG_TAG, "Storage must be initialized first. Will return dummy storage implementation.");
        return new C0900c();
    }

    public void clearAll() {
        machineId().delete();
        visitorInfo().delete();
    }
}
