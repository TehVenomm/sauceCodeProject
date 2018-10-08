package io.fabric.sdk.android;

import android.os.SystemClock;
import android.text.TextUtils;
import io.fabric.sdk.android.services.common.CommonUtils;
import java.io.Closeable;
import java.io.IOException;
import java.util.Enumeration;
import java.util.HashMap;
import java.util.Map;
import java.util.Properties;
import java.util.concurrent.Callable;
import java.util.zip.ZipEntry;
import java.util.zip.ZipFile;

class FabricKitsFinder implements Callable<Map<String, KitInfo>> {
    private static final String FABRIC_BUILD_TYPE_KEY = "fabric-build-type";
    static final String FABRIC_DIR = "fabric/";
    private static final String FABRIC_IDENTIFIER_KEY = "fabric-identifier";
    private static final String FABRIC_VERSION_KEY = "fabric-version";
    final String apkFileName;

    FabricKitsFinder(String str) {
        this.apkFileName = str;
    }

    private KitInfo loadKitInfo(ZipEntry zipEntry, ZipFile zipFile) {
        Throwable e;
        Closeable inputStream;
        try {
            inputStream = zipFile.getInputStream(zipEntry);
            try {
                Properties properties = new Properties();
                properties.load(inputStream);
                Object property = properties.getProperty(FABRIC_IDENTIFIER_KEY);
                Object property2 = properties.getProperty(FABRIC_VERSION_KEY);
                String property3 = properties.getProperty(FABRIC_BUILD_TYPE_KEY);
                if (TextUtils.isEmpty(property) || TextUtils.isEmpty(property2)) {
                    throw new IllegalStateException("Invalid format of fabric file," + zipEntry.getName());
                }
                KitInfo kitInfo = new KitInfo(property, property2, property3);
                CommonUtils.closeQuietly(inputStream);
                return kitInfo;
            } catch (IOException e2) {
                e = e2;
                try {
                    Fabric.getLogger().mo4756e("Fabric", "Error when parsing fabric properties " + zipEntry.getName(), e);
                    CommonUtils.closeQuietly(inputStream);
                    return null;
                } catch (Throwable th) {
                    e = th;
                    CommonUtils.closeQuietly(inputStream);
                    throw e;
                }
            }
        } catch (IOException e3) {
            e = e3;
            inputStream = null;
            Fabric.getLogger().mo4756e("Fabric", "Error when parsing fabric properties " + zipEntry.getName(), e);
            CommonUtils.closeQuietly(inputStream);
            return null;
        } catch (Throwable th2) {
            e = th2;
            inputStream = null;
            CommonUtils.closeQuietly(inputStream);
            throw e;
        }
    }

    public Map<String, KitInfo> call() throws Exception {
        Map<String, KitInfo> hashMap = new HashMap();
        long elapsedRealtime = SystemClock.elapsedRealtime();
        ZipFile loadApkFile = loadApkFile();
        Enumeration entries = loadApkFile.entries();
        int i = 0;
        while (entries.hasMoreElements()) {
            int i2 = i + 1;
            ZipEntry zipEntry = (ZipEntry) entries.nextElement();
            if (zipEntry.getName().startsWith(FABRIC_DIR)) {
                KitInfo loadKitInfo = loadKitInfo(zipEntry, loadApkFile);
                if (loadKitInfo != null) {
                    hashMap.put(loadKitInfo.getIdentifier(), loadKitInfo);
                    Fabric.getLogger().mo4764v("Fabric", String.format("Found kit:[%s] version:[%s]", new Object[]{loadKitInfo.getIdentifier(), loadKitInfo.getVersion()}));
                    i = i2;
                }
            }
            i = i2;
        }
        if (loadApkFile != null) {
            try {
                loadApkFile.close();
            } catch (IOException e) {
            }
        }
        Fabric.getLogger().mo4764v("Fabric", "finish scanning in " + (SystemClock.elapsedRealtime() - elapsedRealtime) + " reading:" + i);
        return hashMap;
    }

    protected ZipFile loadApkFile() throws IOException {
        return new ZipFile(this.apkFileName);
    }
}
