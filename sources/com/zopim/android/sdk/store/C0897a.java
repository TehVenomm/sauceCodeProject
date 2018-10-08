package com.zopim.android.sdk.store;

import android.content.Context;
import android.content.SharedPreferences;

/* renamed from: com.zopim.android.sdk.store.a */
abstract class C0897a implements BaseStorage {
    private static final String DEFAULT_PREFS_NAME = "zopim_chat";
    protected boolean mDisabled;
    protected final SharedPreferences mStoragePreferences;

    private C0897a() {
        throw new UnsupportedOperationException("Use of unsupported constructor");
    }

    protected C0897a(Context context) {
        this(context, null);
    }

    protected C0897a(Context context, String str) {
        if (context == null) {
            throw new IllegalArgumentException("Context must not be null");
        }
        Context applicationContext = context.getApplicationContext();
        if (str == null) {
            this.mStoragePreferences = applicationContext.getSharedPreferences(DEFAULT_PREFS_NAME, 0);
        } else {
            this.mStoragePreferences = applicationContext.getSharedPreferences(str, 0);
        }
    }

    public void delete() {
        this.mStoragePreferences.edit().clear().apply();
    }

    public void disable() {
        delete();
        this.mDisabled = true;
    }
}
