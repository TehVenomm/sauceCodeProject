package com.facebook.internal;

import android.content.Context;
import android.net.Uri;
import com.facebook.LoggingBehavior;
import com.facebook.internal.FileLruCache.Limits;
import java.io.BufferedInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;

class ImageResponseCache {
    static final String TAG = ImageResponseCache.class.getSimpleName();
    private static FileLruCache imageCache;

    private static class BufferedHttpInputStream extends BufferedInputStream {
        HttpURLConnection connection;

        BufferedHttpInputStream(InputStream inputStream, HttpURLConnection httpURLConnection) {
            super(inputStream, 8192);
            this.connection = httpURLConnection;
        }

        public void close() throws IOException {
            super.close();
            Utility.disconnectQuietly(this.connection);
        }
    }

    ImageResponseCache() {
    }

    static void clearCache(Context context) {
        try {
            getCache(context).clearCache();
        } catch (IOException e) {
            Logger.log(LoggingBehavior.CACHE, 5, TAG, "clearCache failed " + e.getMessage());
        }
    }

    static FileLruCache getCache(Context context) throws IOException {
        FileLruCache fileLruCache;
        synchronized (ImageResponseCache.class) {
            try {
                if (imageCache == null) {
                    imageCache = new FileLruCache(TAG, new Limits());
                }
                fileLruCache = imageCache;
            } finally {
                Class<ImageResponseCache> cls = ImageResponseCache.class;
            }
        }
        return fileLruCache;
    }

    static InputStream getCachedImageStream(Uri uri, Context context) {
        InputStream inputStream = null;
        if (uri == null || !isCDNURL(uri)) {
            return inputStream;
        }
        try {
            return getCache(context).get(uri.toString());
        } catch (IOException e) {
            Logger.log(LoggingBehavior.CACHE, 5, TAG, e.toString());
            return inputStream;
        }
    }

    static InputStream interceptAndCacheImageStream(Context context, HttpURLConnection httpURLConnection) throws IOException {
        if (httpURLConnection.getResponseCode() != 200) {
            return null;
        }
        Uri parse = Uri.parse(httpURLConnection.getURL().toString());
        InputStream inputStream = httpURLConnection.getInputStream();
        try {
            return isCDNURL(parse) ? getCache(context).interceptAndPut(parse.toString(), new BufferedHttpInputStream(inputStream, httpURLConnection)) : inputStream;
        } catch (IOException e) {
            return inputStream;
        }
    }

    private static boolean isCDNURL(Uri uri) {
        if (uri != null) {
            String host = uri.getHost();
            if (host.endsWith("fbcdn.net")) {
                return true;
            }
            if (host.startsWith("fbcdn") && host.endsWith("akamaihd.net")) {
                return true;
            }
        }
        return false;
    }
}
