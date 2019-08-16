package com.facebook.appevents;

import android.content.SharedPreferences.Editor;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.support.annotation.Nullable;
import android.util.Log;
import com.facebook.FacebookSdk;
import com.facebook.appevents.internal.AppEventUtility;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.concurrent.locks.ReentrantReadWriteLock;
import org.json.JSONException;
import org.json.JSONObject;
import p017io.fabric.sdk.android.services.common.CommonUtils;

public class UserDataStore {
    public static final String CITY = "ct";
    public static final String COUNTRY = "country";
    public static final String DATE_OF_BIRTH = "db";
    public static final String EMAIL = "em";
    public static final String FIRST_NAME = "fn";
    public static final String GENDER = "ge";
    public static final String LAST_NAME = "ln";
    public static final String PHONE = "ph";
    public static final String STATE = "st";
    private static final String TAG = UserDataStore.class.getSimpleName();
    private static final String USER_DATA_KEY = "com.facebook.appevents.UserDataStore.userData";
    public static final String ZIP = "zp";
    /* access modifiers changed from: private */
    public static String hashedUserData;
    private static volatile boolean initialized = false;
    /* access modifiers changed from: private */
    public static ReentrantReadWriteLock lock = new ReentrantReadWriteLock();

    private static String encryptData(String str) {
        String str2 = null;
        if (str == null || str.isEmpty()) {
            return str2;
        }
        try {
            MessageDigest instance = MessageDigest.getInstance(CommonUtils.SHA256_INSTANCE);
            instance.update(str.getBytes());
            return AppEventUtility.bytesToHex(instance.digest());
        } catch (NoSuchAlgorithmException e) {
            return str2;
        }
    }

    public static String getHashedUserData() {
        if (!initialized) {
            Log.w(TAG, "initStore should have been called before calling setUserID");
            initAndWait();
        }
        lock.readLock().lock();
        try {
            return hashedUserData;
        } finally {
            lock.readLock().unlock();
        }
    }

    /* access modifiers changed from: private */
    public static String hashUserData(Bundle bundle) {
        if (bundle == null) {
            return null;
        }
        JSONObject jSONObject = new JSONObject();
        for (String str : bundle.keySet()) {
            try {
                String obj = bundle.get(str).toString();
                if (maybeSHA256Hashed(obj)) {
                    jSONObject.put(str, obj.toLowerCase());
                } else {
                    String encryptData = encryptData(normalizeData(str, bundle.get(str).toString()));
                    if (encryptData != null) {
                        jSONObject.put(str, encryptData);
                    }
                }
            } catch (JSONException e) {
            }
        }
        return jSONObject.toString();
    }

    /* access modifiers changed from: private */
    public static void initAndWait() {
        if (!initialized) {
            lock.writeLock().lock();
            try {
                if (!initialized) {
                    hashedUserData = PreferenceManager.getDefaultSharedPreferences(FacebookSdk.getApplicationContext()).getString(USER_DATA_KEY, null);
                    initialized = true;
                    lock.writeLock().unlock();
                }
            } finally {
                lock.writeLock().unlock();
            }
        }
    }

    public static void initStore() {
        if (!initialized) {
            AppEventsLogger.getAnalyticsExecutor().execute(new Runnable() {
                public void run() {
                    UserDataStore.initAndWait();
                }
            });
        }
    }

    private static boolean maybeSHA256Hashed(String str) {
        return str.matches("[A-Fa-f0-9]{64}");
    }

    private static String normalizeData(String str, String str2) {
        String str3 = "";
        char c = 65535;
        switch (str.hashCode()) {
            case 3185:
                if (str.equals(CITY)) {
                    c = 3;
                    break;
                }
                break;
            case 3240:
                if (str.equals(EMAIL)) {
                    c = 0;
                    break;
                }
                break;
            case 3272:
                if (str.equals(FIRST_NAME)) {
                    c = 1;
                    break;
                }
                break;
            case 3294:
                if (str.equals(GENDER)) {
                    c = 7;
                    break;
                }
                break;
            case 3458:
                if (str.equals(LAST_NAME)) {
                    c = 2;
                    break;
                }
                break;
            case 3576:
                if (str.equals(PHONE)) {
                    c = 6;
                    break;
                }
                break;
            case 3681:
                if (str.equals(STATE)) {
                    c = 4;
                    break;
                }
                break;
            case 957831062:
                if (str.equals(COUNTRY)) {
                    c = 5;
                    break;
                }
                break;
        }
        switch (c) {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                return str2.trim().toLowerCase();
            case 6:
                return str2.trim().replaceAll("[^0-9]", "");
            case 7:
                String lowerCase = str2.trim().toLowerCase();
                return lowerCase.length() > 0 ? lowerCase.substring(0, 1) : "";
            default:
                return str3;
        }
    }

    public static void setUserDataAndHash(final Bundle bundle) {
        if (!initialized) {
            Log.w(TAG, "initStore should have been called before calling setUserData");
            initAndWait();
        }
        AppEventsLogger.getAnalyticsExecutor().execute(new Runnable() {
            public void run() {
                UserDataStore.lock.writeLock().lock();
                try {
                    UserDataStore.hashedUserData = UserDataStore.hashUserData(bundle);
                    Editor edit = PreferenceManager.getDefaultSharedPreferences(FacebookSdk.getApplicationContext()).edit();
                    edit.putString(UserDataStore.USER_DATA_KEY, UserDataStore.hashedUserData);
                    edit.apply();
                } finally {
                    UserDataStore.lock.writeLock().unlock();
                }
            }
        });
    }

    public static void setUserDataAndHash(@Nullable String str, @Nullable String str2, @Nullable String str3, @Nullable String str4, @Nullable String str5, @Nullable String str6, @Nullable String str7, @Nullable String str8, @Nullable String str9, @Nullable String str10) {
        Bundle bundle = new Bundle();
        if (str != null) {
            bundle.putString(EMAIL, str);
        }
        if (str2 != null) {
            bundle.putString(FIRST_NAME, str2);
        }
        if (str3 != null) {
            bundle.putString(LAST_NAME, str3);
        }
        if (str4 != null) {
            bundle.putString(PHONE, str4);
        }
        if (str5 != null) {
            bundle.putString(DATE_OF_BIRTH, str5);
        }
        if (str6 != null) {
            bundle.putString(GENDER, str6);
        }
        if (str7 != null) {
            bundle.putString(CITY, str7);
        }
        if (str8 != null) {
            bundle.putString(STATE, str8);
        }
        if (str9 != null) {
            bundle.putString(ZIP, str9);
        }
        if (str10 != null) {
            bundle.putString(COUNTRY, str10);
        }
        setUserDataAndHash(bundle);
    }
}
