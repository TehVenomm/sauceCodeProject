package com.zopim.android.sdk.store;

import android.util.Log;

/* renamed from: com.zopim.android.sdk.store.b */
final class C0899b implements MachineIdStorage {
    /* renamed from: a */
    private static final String f915a = C0899b.class.getSimpleName();

    C0899b() {
    }

    public void delete() {
        Log.w(f915a, "Storage is not initialized. Skipping operation.");
    }

    public void disable() {
        Log.w(f915a, "Storage is not initialized. Skipping operation.");
    }

    public String getMachineId() {
        Log.w(f915a, "Storage is not initialized. Skipping operation. Will return empty string");
        return "";
    }

    public void setMachineId(String str) {
        Log.w(f915a, "Storage is not initialized. Skipping operation.");
    }
}
