package com.zopim.android.sdk.attachment;

import android.content.Context;
import android.util.Log;
import com.zopim.android.sdk.api.Logger;
import java.io.File;

public enum SdkCache {
    INSTANCE;
    
    private static final String CACHE_DIR = "zopim_sdk_file_cache";
    private static final String LOG_TAG = null;

    static {
        LOG_TAG = SdkCache.class.getSimpleName();
    }

    public void deleteCache(Context context) {
        Logger.m564v(LOG_TAG, "Clearing cached files");
        if (context == null) {
            Log.w(LOG_TAG, "Context must not be null. File cache will not be deleted.");
        }
        File[] listFiles = getSdkCacheDir(context).listFiles();
        if (listFiles != null) {
            for (File delete : listFiles) {
                delete.delete();
            }
        }
    }

    public File getSdkCacheDir(Context context) {
        File cacheDir = context.getCacheDir();
        File file = new File(cacheDir.getPath() + File.separator + CACHE_DIR);
        return !(file.exists() ? true : file.mkdir()) ? cacheDir : file;
    }
}
