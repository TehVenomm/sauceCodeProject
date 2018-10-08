package io.fabric.sdk.android.services.settings;

import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.Kit;
import io.fabric.sdk.android.services.common.CommonUtils;
import io.fabric.sdk.android.services.persistence.FileStoreImpl;
import java.io.Closeable;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import org.json.JSONObject;

class DefaultCachedSettingsIo implements CachedSettingsIo {
    private final Kit kit;

    public DefaultCachedSettingsIo(Kit kit) {
        this.kit = kit;
    }

    public JSONObject readCachedSettings() {
        Closeable fileInputStream;
        Throwable th;
        Closeable closeable;
        Throwable th2;
        Throwable th3;
        JSONObject jSONObject = null;
        Fabric.getLogger().mo4753d("Fabric", "Reading cached settings...");
        try {
            File file = new File(new FileStoreImpl(this.kit).getFilesDir(), Settings.SETTINGS_CACHE_FILENAME);
            if (file.exists()) {
                fileInputStream = new FileInputStream(file);
                try {
                    jSONObject = new JSONObject(CommonUtils.streamToString(fileInputStream));
                } catch (Throwable e) {
                    th = e;
                    closeable = fileInputStream;
                    th2 = th;
                    try {
                        Fabric.getLogger().mo4756e("Fabric", "Failed to fetch cached settings", th2);
                        CommonUtils.closeOrLog(closeable, "Error while closing settings cache file.");
                        return jSONObject;
                    } catch (Throwable th4) {
                        th3 = th4;
                        fileInputStream = closeable;
                        CommonUtils.closeOrLog(fileInputStream, "Error while closing settings cache file.");
                        throw th3;
                    }
                } catch (Throwable th5) {
                    th3 = th5;
                    CommonUtils.closeOrLog(fileInputStream, "Error while closing settings cache file.");
                    throw th3;
                }
            }
            Fabric.getLogger().mo4753d("Fabric", "No cached settings found.");
            fileInputStream = null;
            CommonUtils.closeOrLog(fileInputStream, "Error while closing settings cache file.");
        } catch (Exception e2) {
            th2 = e2;
            closeable = null;
            Fabric.getLogger().mo4756e("Fabric", "Failed to fetch cached settings", th2);
            CommonUtils.closeOrLog(closeable, "Error while closing settings cache file.");
            return jSONObject;
        } catch (Throwable th22) {
            th = th22;
            fileInputStream = null;
            th3 = th;
            CommonUtils.closeOrLog(fileInputStream, "Error while closing settings cache file.");
            throw th3;
        }
        return jSONObject;
    }

    public void writeCachedSettings(long j, JSONObject jSONObject) {
        Throwable th;
        Throwable e;
        Throwable th2;
        Closeable closeable = null;
        Fabric.getLogger().mo4753d("Fabric", "Writing settings to cache file...");
        if (jSONObject != null) {
            Closeable fileWriter;
            try {
                jSONObject.put(SettingsJsonConstants.EXPIRES_AT_KEY, j);
                fileWriter = new FileWriter(new File(new FileStoreImpl(this.kit).getFilesDir(), Settings.SETTINGS_CACHE_FILENAME));
                try {
                    fileWriter.write(jSONObject.toString());
                    fileWriter.flush();
                    CommonUtils.closeOrLog(fileWriter, "Failed to close settings writer.");
                } catch (Throwable e2) {
                    th = e2;
                    closeable = fileWriter;
                    th2 = th;
                    try {
                        Fabric.getLogger().mo4756e("Fabric", "Failed to cache settings", th2);
                        CommonUtils.closeOrLog(closeable, "Failed to close settings writer.");
                    } catch (Throwable th22) {
                        th = th22;
                        fileWriter = closeable;
                        e2 = th;
                        CommonUtils.closeOrLog(fileWriter, "Failed to close settings writer.");
                        throw e2;
                    }
                } catch (Throwable th3) {
                    e2 = th3;
                    CommonUtils.closeOrLog(fileWriter, "Failed to close settings writer.");
                    throw e2;
                }
            } catch (Exception e3) {
                th22 = e3;
                Fabric.getLogger().mo4756e("Fabric", "Failed to cache settings", th22);
                CommonUtils.closeOrLog(closeable, "Failed to close settings writer.");
            } catch (Throwable th222) {
                th = th222;
                fileWriter = null;
                e2 = th;
                CommonUtils.closeOrLog(fileWriter, "Failed to close settings writer.");
                throw e2;
            }
        }
    }
}
