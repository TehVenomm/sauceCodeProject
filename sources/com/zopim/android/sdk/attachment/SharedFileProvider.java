package com.zopim.android.sdk.attachment;

import android.content.Context;
import android.net.Uri;
import android.support.p000v4.content.FileProvider;
import android.util.Log;
import com.zopim.android.sdk.C1122R;
import java.io.File;

public enum SharedFileProvider {
    INSTANCE;
    
    private static final String LOG_TAG = null;

    static {
        LOG_TAG = SharedFileProvider.class.getSimpleName();
    }

    public static Uri getProviderUri(Context context, File file) {
        Uri uri = null;
        if (file == null || context == null) {
            Log.w(LOG_TAG, "Can not provide uri. File or context must not be null");
            return uri;
        }
        String string = context.getResources().getString(C1122R.string.file_provider_authority);
        try {
            return FileProvider.getUriForFile(context, string, file);
        } catch (IllegalArgumentException e) {
            Log.e(LOG_TAG, "The selected file can't be shared: " + file);
            return uri;
        } catch (NullPointerException e2) {
            Log.e(LOG_TAG, "FileProvider failed to retrieve file uri. There might be an issue with provider:authority=" + string, e2);
            return uri;
        }
    }
}
