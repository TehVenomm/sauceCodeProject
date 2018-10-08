package com.zopim.android.sdk.attachment;

import android.content.Context;
import android.net.Uri;
import android.support.v4.content.FileProvider;
import android.util.Log;
import com.zopim.android.sdk.C0784R;
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
        } else {
            String string = context.getResources().getString(C0784R.string.file_provider_authority);
            try {
                uri = FileProvider.getUriForFile(context, string, file);
            } catch (IllegalArgumentException e) {
                Log.e(LOG_TAG, "The selected file can't be shared: " + file);
            } catch (Throwable e2) {
                Log.e(LOG_TAG, "FileProvider failed to retrieve file uri. There might be an issue with provider:authority=" + string, e2);
            }
        }
        return uri;
    }
}
