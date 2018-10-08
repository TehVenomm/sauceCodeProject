package com.zopim.android.sdk.store;

import android.content.Context;
import android.util.Log;

public final class MachineIdPrefsStorage extends C0897a implements MachineIdStorage {
    private static final String LOG_TAG = MachineIdPrefsStorage.class.getSimpleName();
    private static final String MACHINE_ID_KEY = "stored_machine_id";
    private static final String PREFS_NAME = "machine_id";

    MachineIdPrefsStorage(Context context) {
        super(context, PREFS_NAME);
    }

    public /* bridge */ /* synthetic */ void delete() {
        super.delete();
    }

    public /* bridge */ /* synthetic */ void disable() {
        super.disable();
    }

    public String getMachineId() {
        return this.mDisabled ? null : this.mStoragePreferences.getString(MACHINE_ID_KEY, null);
    }

    public void setMachineId(String str) {
        if (str == null) {
            Log.w(LOG_TAG, "Machine id must not be null. Skipping storing machine id.");
        } else if (this.mDisabled) {
            Log.i(LOG_TAG, "Storage is disabled, will abort storing machine id  ");
        } else {
            this.mStoragePreferences.edit().putString(MACHINE_ID_KEY, str).apply();
        }
    }
}
