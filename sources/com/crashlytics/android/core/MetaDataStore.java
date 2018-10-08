package com.crashlytics.android.core;

import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.services.common.CommonUtils;
import java.io.BufferedWriter;
import java.io.Closeable;
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

    private File getKeysFileForSession(String str) {
        return new File(this.filesDir, str + KEYDATA_SUFFIX + METADATA_EXT);
    }

    private File getUserDataFileForSession(String str) {
        return new File(this.filesDir, str + "user" + METADATA_EXT);
    }

    private static Map<String, String> jsonToKeysData(String str) throws JSONException {
        JSONObject jSONObject = new JSONObject(str);
        Map<String, String> hashMap = new HashMap();
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
        }.toString();
    }

    private static String valueOrNull(JSONObject jSONObject, String str) {
        return !jSONObject.isNull(str) ? jSONObject.optString(str, null) : null;
    }

    public Map<String, String> readKeyData(String str) {
        Closeable fileInputStream;
        Throwable e;
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
            } catch (Exception e2) {
                e = e2;
                try {
                    Fabric.getLogger().mo4756e("Fabric", "Error deserializing user metadata.", e);
                    CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
                    return Collections.emptyMap();
                } catch (Throwable th) {
                    e = th;
                    CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
                    throw e;
                }
            } catch (Throwable th2) {
                e = th2;
                CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
                throw e;
            }
        } catch (Exception e3) {
            e = e3;
            fileInputStream = null;
            Fabric.getLogger().mo4756e("Fabric", "Error deserializing user metadata.", e);
            CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
            return Collections.emptyMap();
        } catch (Throwable th3) {
            e = th3;
            fileInputStream = null;
            CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
            throw e;
        }
    }

    public UserMetaData readUserData(String str) {
        Closeable fileInputStream;
        Throwable e;
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
            } catch (Exception e2) {
                e = e2;
                try {
                    Fabric.getLogger().mo4756e("Fabric", "Error deserializing user metadata.", e);
                    CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
                    return UserMetaData.EMPTY;
                } catch (Throwable th) {
                    e = th;
                    CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
                    throw e;
                }
            } catch (Throwable th2) {
                e = th2;
                CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
                throw e;
            }
        } catch (Exception e3) {
            e = e3;
            fileInputStream = null;
            Fabric.getLogger().mo4756e("Fabric", "Error deserializing user metadata.", e);
            CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
            return UserMetaData.EMPTY;
        } catch (Throwable th3) {
            e = th3;
            fileInputStream = null;
            CommonUtils.closeOrLog(fileInputStream, "Failed to close user metadata file.");
            throw e;
        }
    }

    public void writeKeyData(String str, Map<String, String> map) {
        Closeable bufferedWriter;
        Throwable e;
        File keysFileForSession = getKeysFileForSession(str);
        try {
            String keysDataToJson = keysDataToJson(map);
            bufferedWriter = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(keysFileForSession), UTF_8));
            try {
                bufferedWriter.write(keysDataToJson);
                bufferedWriter.flush();
                CommonUtils.closeOrLog(bufferedWriter, "Failed to close key/value metadata file.");
            } catch (Exception e2) {
                e = e2;
                try {
                    Fabric.getLogger().mo4756e("Fabric", "Error serializing key/value metadata.", e);
                    CommonUtils.closeOrLog(bufferedWriter, "Failed to close key/value metadata file.");
                } catch (Throwable th) {
                    e = th;
                    CommonUtils.closeOrLog(bufferedWriter, "Failed to close key/value metadata file.");
                    throw e;
                }
            } catch (Throwable th2) {
                e = th2;
                CommonUtils.closeOrLog(bufferedWriter, "Failed to close key/value metadata file.");
                throw e;
            }
        } catch (Exception e3) {
            e = e3;
            bufferedWriter = null;
            Fabric.getLogger().mo4756e("Fabric", "Error serializing key/value metadata.", e);
            CommonUtils.closeOrLog(bufferedWriter, "Failed to close key/value metadata file.");
        } catch (Throwable th3) {
            e = th3;
            bufferedWriter = null;
            CommonUtils.closeOrLog(bufferedWriter, "Failed to close key/value metadata file.");
            throw e;
        }
    }

    public void writeUserData(String str, UserMetaData userMetaData) {
        Throwable e;
        File userDataFileForSession = getUserDataFileForSession(str);
        Closeable bufferedWriter;
        try {
            String userDataToJson = userDataToJson(userMetaData);
            bufferedWriter = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(userDataFileForSession), UTF_8));
            try {
                bufferedWriter.write(userDataToJson);
                bufferedWriter.flush();
                CommonUtils.closeOrLog(bufferedWriter, "Failed to close user metadata file.");
            } catch (Exception e2) {
                e = e2;
                try {
                    Fabric.getLogger().mo4756e("Fabric", "Error serializing user metadata.", e);
                    CommonUtils.closeOrLog(bufferedWriter, "Failed to close user metadata file.");
                } catch (Throwable th) {
                    e = th;
                    CommonUtils.closeOrLog(bufferedWriter, "Failed to close user metadata file.");
                    throw e;
                }
            } catch (Throwable th2) {
                e = th2;
                CommonUtils.closeOrLog(bufferedWriter, "Failed to close user metadata file.");
                throw e;
            }
        } catch (Exception e3) {
            e = e3;
            bufferedWriter = null;
            Fabric.getLogger().mo4756e("Fabric", "Error serializing user metadata.", e);
            CommonUtils.closeOrLog(bufferedWriter, "Failed to close user metadata file.");
        } catch (Throwable th3) {
            e = th3;
            bufferedWriter = null;
            CommonUtils.closeOrLog(bufferedWriter, "Failed to close user metadata file.");
            throw e;
        }
    }
}
