package com.zopim.android.sdk.store;

import android.util.Log;
import com.zopim.android.sdk.model.VisitorInfo;
import com.zopim.android.sdk.model.VisitorInfo.Builder;

/* renamed from: com.zopim.android.sdk.store.c */
final class C1269c implements VisitorInfoStorage {

    /* renamed from: a */
    private static final String f960a = C1269c.class.getSimpleName();

    C1269c() {
    }

    public void delete() {
        Log.w(f960a, "Storage is not initialized. Skipping operation.");
    }

    public void disable() {
        Log.w(f960a, "Storage is not initialized. Skipping operation.");
    }

    public VisitorInfo getVisitorInfo() {
        Log.w(f960a, "Storage is not initialized. Skipping operation.");
        return new Builder().build();
    }

    public void setVisitorInfo(VisitorInfo visitorInfo) {
        Log.w(f960a, "Storage is not initialized. Skipping operation.");
    }
}
