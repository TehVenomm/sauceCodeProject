package p017io.fabric.sdk.android.services.settings;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import org.json.JSONObject;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.Kit;
import p017io.fabric.sdk.android.services.common.CommonUtils;
import p017io.fabric.sdk.android.services.persistence.FileStoreImpl;

/* renamed from: io.fabric.sdk.android.services.settings.DefaultCachedSettingsIo */
class DefaultCachedSettingsIo implements CachedSettingsIo {
    private final Kit kit;

    public DefaultCachedSettingsIo(Kit kit2) {
        this.kit = kit2;
    }

    public JSONObject readCachedSettings() {
        FileInputStream fileInputStream;
        Throwable th;
        FileInputStream fileInputStream2;
        Exception e;
        FileInputStream fileInputStream3;
        JSONObject jSONObject;
        Fabric.getLogger().mo20969d(Fabric.TAG, "Reading cached settings...");
        try {
            File file = new File(new FileStoreImpl(this.kit).getFilesDir(), Settings.SETTINGS_CACHE_FILENAME);
            if (file.exists()) {
                fileInputStream3 = new FileInputStream(file);
                try {
                    jSONObject = new JSONObject(CommonUtils.streamToString(fileInputStream3));
                } catch (Exception e2) {
                    e = e2;
                    fileInputStream2 = fileInputStream3;
                    try {
                        Fabric.getLogger().mo20972e(Fabric.TAG, "Failed to fetch cached settings", e);
                        CommonUtils.closeOrLog(fileInputStream2, "Error while closing settings cache file.");
                        return null;
                    } catch (Throwable th2) {
                        th = th2;
                        fileInputStream = fileInputStream2;
                        th = th;
                        CommonUtils.closeOrLog(fileInputStream, "Error while closing settings cache file.");
                        throw th;
                    }
                } catch (Throwable th3) {
                    th = th3;
                    fileInputStream = fileInputStream3;
                    CommonUtils.closeOrLog(fileInputStream, "Error while closing settings cache file.");
                    throw th;
                }
            } else {
                Fabric.getLogger().mo20969d(Fabric.TAG, "No cached settings found.");
                fileInputStream3 = null;
                jSONObject = null;
            }
            CommonUtils.closeOrLog(fileInputStream3, "Error while closing settings cache file.");
            return jSONObject;
        } catch (Exception e3) {
            e = e3;
            fileInputStream2 = null;
            Fabric.getLogger().mo20972e(Fabric.TAG, "Failed to fetch cached settings", e);
            CommonUtils.closeOrLog(fileInputStream2, "Error while closing settings cache file.");
            return null;
        } catch (Throwable th4) {
            th = th4;
            fileInputStream = null;
            th = th;
            CommonUtils.closeOrLog(fileInputStream, "Error while closing settings cache file.");
            throw th;
        }
    }

    public void writeCachedSettings(long j, JSONObject jSONObject) {
        FileWriter fileWriter;
        Exception e;
        FileWriter fileWriter2 = null;
        Fabric.getLogger().mo20969d(Fabric.TAG, "Writing settings to cache file...");
        if (jSONObject != null) {
            try {
                jSONObject.put(SettingsJsonConstants.EXPIRES_AT_KEY, j);
                fileWriter = new FileWriter(new File(new FileStoreImpl(this.kit).getFilesDir(), Settings.SETTINGS_CACHE_FILENAME));
                try {
                    fileWriter.write(jSONObject.toString());
                    fileWriter.flush();
                    CommonUtils.closeOrLog(fileWriter, "Failed to close settings writer.");
                } catch (Exception e2) {
                    e = e2;
                    fileWriter2 = fileWriter;
                    try {
                        Fabric.getLogger().mo20972e(Fabric.TAG, "Failed to cache settings", e);
                        CommonUtils.closeOrLog(fileWriter2, "Failed to close settings writer.");
                    } catch (Throwable th) {
                        th = th;
                        fileWriter = fileWriter2;
                        CommonUtils.closeOrLog(fileWriter, "Failed to close settings writer.");
                        throw th;
                    }
                } catch (Throwable th2) {
                    th = th2;
                    CommonUtils.closeOrLog(fileWriter, "Failed to close settings writer.");
                    throw th;
                }
            } catch (Exception e3) {
                e = e3;
                Fabric.getLogger().mo20972e(Fabric.TAG, "Failed to cache settings", e);
                CommonUtils.closeOrLog(fileWriter2, "Failed to close settings writer.");
            } catch (Throwable th3) {
                th = th3;
                fileWriter = null;
                CommonUtils.closeOrLog(fileWriter, "Failed to close settings writer.");
                throw th;
            }
        }
    }
}
