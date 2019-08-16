package com.crashlytics.android.core;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.OutputStreamWriter;
import java.nio.charset.Charset;
import java.util.Collections;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import org.json.JSONException;
import org.json.JSONObject;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.services.common.CommonUtils;

class MetaDataStore {
    private static final String KEYDATA_SUFFIX = "keys";
    private static final String KEY_USER_EMAIL = "userEmail";
    private static final String KEY_USER_ID = "userId";
    private static final String KEY_USER_NAME = "userName";
    private static final String METADATA_EXT = ".meta";
    private static final String USERDATA_SUFFIX = "user";
    private static final Charset UTF_8 = Charset.forName("UTF-8");
    private final File filesDir;

    public MetaDataStore(File file) {
        this.filesDir = file;
    }

    private static Map<String, String> jsonToKeysData(String str) throws JSONException {
        JSONObject jSONObject = new JSONObject(str);
        HashMap hashMap = new HashMap();
        Iterator keys = jSONObject.keys();
        while (keys.hasNext()) {
            String str2 = (String) keys.next();
            hashMap.put(str2, valueOrNull(jSONObject, str2));
        }
        return hashMap;
    }

    private static UserMetaData jsonToUserData(String str) throws JSONException {
        JSONObject jSONObject = new JSONObject(str);
        return new UserMetaData(valueOrNull(jSONObject, "userId"), valueOrNull(jSONObject, KEY_USER_NAME), valueOrNull(jSONObject, "userEmail"));
    }

    private static String keysDataToJson(Map<String, String> map) throws JSONException {
        return new JSONObject(map).toString();
    }

    private static String userDataToJson(final UserMetaData userMetaData) throws JSONException {
        return new JSONObject() {
            {
                put("userId", userMetaData.f385id);
                put(MetaDataStore.KEY_USER_NAME, userMetaData.name);
                put("userEmail", userMetaData.email);
            }
        }.toString();
    }

    private static String valueOrNull(JSONObject jSONObject, String str) {
        if (!jSONObject.isNull(str)) {
            return jSONObject.optString(str, null);
        }
        return null;
    }

    public File getKeysFileForSession(String str) {
        return new File(this.filesDir, str + KEYDATA_SUFFIX + METADATA_EXT);
    }

    public File getUserDataFileForSession(String str) {
        return new File(this.filesDir, str + USERDATA_SUFFIX + METADATA_EXT);
    }

    public Map<String, String> readKeyData(String str) {
        FileInputStream fileInputStream;
        File keysFileForSession = getKeysFileForSession(str);
        if (!keysFileForSession.exists()) {
            return Collections.emptyMap();
        }
        try {
            fileInputStream = new FileInputStream(keysFileForSession);
            try {
                Map<String, String> jsonToKeysData = jsonToKeysData(CommonUtils.streamToString(fileInputStream));
                CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
                return jsonToKeysData;
            } catch (Exception e) {
                e = e;
                try {
                    Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Error deserializing user metadata.", e);
                    CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
                    return Collections.emptyMap();
                } catch (Throwable th) {
                    th = th;
                    CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
                    throw th;
                }
            } catch (Throwable th2) {
                th = th2;
                CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
                throw th;
            }
        } catch (Exception e2) {
            e = e2;
            fileInputStream = null;
            Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Error deserializing user metadata.", e);
            CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
            return Collections.emptyMap();
        } catch (Throwable th3) {
            th = th3;
            fileInputStream = null;
            CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
            throw th;
        }
    }

    public UserMetaData readUserData(String str) {
        FileInputStream fileInputStream;
        File userDataFileForSession = getUserDataFileForSession(str);
        if (!userDataFileForSession.exists()) {
            return UserMetaData.EMPTY;
        }
        try {
            fileInputStream = new FileInputStream(userDataFileForSession);
            try {
                UserMetaData jsonToUserData = jsonToUserData(CommonUtils.streamToString(fileInputStream));
                CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
                return jsonToUserData;
            } catch (Exception e) {
                e = e;
                try {
                    Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Error deserializing user metadata.", e);
                    CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
                    return UserMetaData.EMPTY;
                } catch (Throwable th) {
                    th = th;
                    CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
                    throw th;
                }
            } catch (Throwable th2) {
                th = th2;
                CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
                throw th;
            }
        } catch (Exception e2) {
            e = e2;
            fileInputStream = null;
            Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Error deserializing user metadata.", e);
            CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
            return UserMetaData.EMPTY;
        } catch (Throwable th3) {
            th = th3;
            fileInputStream = null;
            CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
            throw th;
        }
    }

    public void writeKeyData(String str, Map<String, String> map) {
        BufferedWriter bufferedWriter;
        File keysFileForSession = getKeysFileForSession(str);
        try {
            String keysDataToJson = keysDataToJson(map);
            bufferedWriter = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(keysFileForSession), UTF_8));
            try {
                bufferedWriter.write(keysDataToJson);
                bufferedWriter.flush();
                CommonUtils.closeOrLog(bufferedWriter, "Failed to close key/value metadata file.");
            } catch (Exception e) {
                e = e;
                try {
                    Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Error serializing key/value metadata.", e);
                    CommonUtils.closeOrLog(bufferedWriter, "Failed to close key/value metadata file.");
                } catch (Throwable th) {
                    th = th;
                    CommonUtils.closeOrLog(bufferedWriter, "Failed to close key/value metadata file.");
                    throw th;
                }
            } catch (Throwable th2) {
                th = th2;
                CommonUtils.closeOrLog(bufferedWriter, "Failed to close key/value metadata file.");
                throw th;
            }
        } catch (Exception e2) {
            e = e2;
            bufferedWriter = null;
            Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Error serializing key/value metadata.", e);
            CommonUtils.closeOrLog(bufferedWriter, "Failed to close key/value metadata file.");
        } catch (Throwable th3) {
            th = th3;
            bufferedWriter = null;
            CommonUtils.closeOrLog(bufferedWriter, "Failed to close key/value metadata file.");
            throw th;
        }
    }

    public void writeUserData(String str, UserMetaData userMetaData) {
        BufferedWriter bufferedWriter;
        File userDataFileForSession = getUserDataFileForSession(str);
        try {
            String userDataToJson = userDataToJson(userMetaData);
            bufferedWriter = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(userDataFileForSession), UTF_8));
            try {
                bufferedWriter.write(userDataToJson);
                bufferedWriter.flush();
                CommonUtils.closeOrLog(bufferedWriter, "Failed to close user metadata file.");
            } catch (Exception e) {
                e = e;
                try {
                    Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Error serializing user metadata.", e);
                    CommonUtils.closeOrLog(bufferedWriter, "Failed to close user metadata file.");
                } catch (Throwable th) {
                    th = th;
                    CommonUtils.closeOrLog(bufferedWriter, "Failed to close user metadata file.");
                    throw th;
                }
            } catch (Throwable th2) {
                th = th2;
                CommonUtils.closeOrLog(bufferedWriter, "Failed to close user metadata file.");
                throw th;
            }
        } catch (Exception e2) {
            e = e2;
            bufferedWriter = null;
            Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Error serializing user metadata.", e);
            CommonUtils.closeOrLog(bufferedWriter, "Failed to close user metadata file.");
        } catch (Throwable th3) {
            th = th3;
            bufferedWriter = null;
            CommonUtils.closeOrLog(bufferedWriter, "Failed to close user metadata file.");
            throw th;
        }
    }
}
