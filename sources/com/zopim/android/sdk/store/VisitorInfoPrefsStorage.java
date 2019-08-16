package com.zopim.android.sdk.store;

import android.content.Context;
import android.content.SharedPreferences.Editor;
import android.util.Log;
import com.zopim.android.sdk.model.VisitorInfo;
import com.zopim.android.sdk.model.VisitorInfo.Builder;

public final class VisitorInfoPrefsStorage extends C1267a implements VisitorInfoStorage {
    private static final String EMAIL_KEY = "email_key";
    private static final String LOG_TAG = VisitorInfoPrefsStorage.class.getSimpleName();
    private static final String NAME_KEY = "name_key";
    private static final String PHONE_NUMBER_KEY = "phone_number_key";
    private static final String PREFS_NAME = "visitor_info";

    VisitorInfoPrefsStorage(Context context) {
        super(context, PREFS_NAME);
    }

    public /* bridge */ /* synthetic */ void delete() {
        super.delete();
    }

    public /* bridge */ /* synthetic */ void disable() {
        super.disable();
    }

    public VisitorInfo getVisitorInfo() {
        if (this.mDisabled) {
            return null;
        }
        String string = this.mStoragePreferences.getString(EMAIL_KEY, null);
        String string2 = this.mStoragePreferences.getString(NAME_KEY, null);
        String string3 = this.mStoragePreferences.getString(PHONE_NUMBER_KEY, null);
        if (string == null && string2 == null && string3 == null) {
            return null;
        }
        return new Builder().email(string).name(string2).phoneNumber(string3).build();
    }

    public void setVisitorInfo(VisitorInfo visitorInfo) {
        if (visitorInfo == null) {
            Log.w(LOG_TAG, "Visitor info must not be null. Skipping storing visitor info.");
        } else if (this.mDisabled) {
            Log.i(LOG_TAG, "Storage is disabled, will abort storing visitor info");
        } else {
            Editor edit = this.mStoragePreferences.edit();
            String email = visitorInfo.getEmail();
            String name = visitorInfo.getName();
            String phoneNumber = visitorInfo.getPhoneNumber();
            if (email != null && !email.isEmpty()) {
                edit.putString(EMAIL_KEY, email);
            }
            if (name != null && !name.isEmpty()) {
                edit.putString(NAME_KEY, name);
            }
            if (phoneNumber != null && !phoneNumber.isEmpty()) {
                edit.putString(PHONE_NUMBER_KEY, phoneNumber);
            }
            edit.apply();
        }
    }
}
