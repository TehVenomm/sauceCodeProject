package com.zopim.android.sdk.store;

import android.util.Log;

/* renamed from: com.zopim.android.sdk.store.b */
final class C1268b implements MachineIdStorage {

    /* renamed from: a */
    private static final String f959a = C1268b.class.getSimpleName();

    C1268b() {
    }

    public void delete() {
        Log.w(f959a, "Storage is not initialized. Skipping operation.");
    }

    public void disable() {
        Log.w(f959a, "Storage is not initialized. Skipping operation.");
    }

    public String getMachineId() {
        Log.w(f959a, "Storage is not initialized. Skipping operation. Will return empty string");
        return "";
    }

    public void setMachineId(String str) {
        Log.w(f959a, "Storage is not initialized. Skipping operation.");
    }
}
