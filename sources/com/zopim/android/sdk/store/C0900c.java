package com.zopim.android.sdk.store;

import android.util.Log;
import com.zopim.android.sdk.model.VisitorInfo;
import com.zopim.android.sdk.model.VisitorInfo.Builder;

/* renamed from: com.zopim.android.sdk.store.c */
final class C0900c implements VisitorInfoStorage {
    /* renamed from: a */
    private static final String f916a = C0900c.class.getSimpleName();

    C0900c() {
    }

    public void delete() {
        Log.w(f916a, "Storage is not initialized. Skipping operation.");
    }

    public void disable() {
        Log.w(f916a, "Storage is not initialized. Skipping operation.");
    }

    public VisitorInfo getVisitorInfo() {
        Log.w(f916a, "Storage is not initialized. Skipping operation.");
        return new Builder().build();
    }

    public void setVisitorInfo(VisitorInfo visitorInfo) {
        Log.w(f916a, "Storage is not initialized. Skipping operation.");
    }
}
