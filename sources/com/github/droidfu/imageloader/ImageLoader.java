package com.github.droidfu.imageloader;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.os.Message;
import android.os.SystemClock;
import android.util.Log;
import android.widget.ImageView;
import com.github.droidfu.cachefu.ImageCache;
import java.io.BufferedInputStream;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.concurrent.Executors;
import java.util.concurrent.ThreadPoolExecutor;

public class ImageLoader implements Runnable {
    public static final String BITMAP_EXTRA = "droidfu:extra_bitmap";
    private static final int DEFAULT_NUM_RETRIES = 3;
    private static final int DEFAULT_POOL_SIZE = 3;
    private static final int DEFAULT_RETRY_HANDLER_SLEEP_TIME = 1000;
    private static final int DEFAULT_TTL_MINUTES = 1440;
    public static final int HANDLER_MESSAGE_ID = 0;
    public static final String IMAGE_URL_EXTRA = "droidfu:extra_image_url";
    private static final String LOG_TAG = "Droid-Fu/ImageLoader";
    private static ThreadPoolExecutor executor;
    private static ImageCache imageCache;
    private static int numRetries = 3;
    private ImageLoaderHandler handler;
    private String imageUrl;

    private ImageLoader(String str, ImageLoaderHandler imageLoaderHandler) {
        this.imageUrl = str;
        this.handler = imageLoaderHandler;
    }

    public static void clearCache() {
        imageCache.clear();
    }

    public static ImageCache getImageCache() {
        return imageCache;
    }

    public static void initialize(Context context) {
        synchronized (ImageLoader.class) {
            try {
                if (executor == null) {
                    executor = (ThreadPoolExecutor) Executors.newFixedThreadPool(3);
                }
                if (imageCache == null) {
                    imageCache = new ImageCache(25, 1440, 3);
                    imageCache.enableDiskCache(context, 1);
                }
            } finally {
                Class<ImageLoader> cls = ImageLoader.class;
            }
        }
    }

    public static void setMaxDownloadAttempts(int i) {
        numRetries = i;
    }

    public static void setThreadPoolSize(int i) {
        executor.setMaximumPoolSize(i);
    }

    public static void start(String str, ImageView imageView) {
        start(str, imageView, new ImageLoaderHandler(imageView, str), null, null);
    }

    public static void start(String str, ImageView imageView, Drawable drawable, Drawable drawable2) {
        start(str, imageView, new ImageLoaderHandler(imageView, str, drawable2), drawable, drawable2);
    }

    private static void start(String str, ImageView imageView, ImageLoaderHandler imageLoaderHandler, Drawable drawable, Drawable drawable2) {
        if (imageView != null) {
            if (str == null) {
                imageView.setTag(null);
                imageView.setImageDrawable(drawable);
                return;
            } else if (!str.equals((String) imageView.getTag())) {
                imageView.setImageDrawable(drawable);
                imageView.setTag(str);
            } else {
                return;
            }
        }
        if (imageCache.containsKeyInMemory(str)) {
            imageLoaderHandler.handleImageLoaded(imageCache.getBitmap(str), null);
        } else {
            executor.execute(new ImageLoader(str, imageLoaderHandler));
        }
    }

    public static void start(String str, ImageLoaderHandler imageLoaderHandler) {
        start(str, imageLoaderHandler.getImageView(), imageLoaderHandler, null, null);
    }

    public static void start(String str, ImageLoaderHandler imageLoaderHandler, Drawable drawable, Drawable drawable2) {
        start(str, imageLoaderHandler.getImageView(), imageLoaderHandler, drawable, drawable2);
    }

    /* access modifiers changed from: protected */
    public Bitmap downloadImage() {
        int i = 1;
        while (true) {
            if (i > numRetries) {
                break;
            }
            try {
                byte[] retrieveImageData = retrieveImageData();
                if (retrieveImageData != null) {
                    imageCache.put(this.imageUrl, retrieveImageData);
                    return BitmapFactory.decodeByteArray(retrieveImageData, 0, retrieveImageData.length);
                }
            } catch (Throwable th) {
                Log.w(LOG_TAG, "download for " + this.imageUrl + " failed (attempt " + i + ")");
                th.printStackTrace();
                SystemClock.sleep(1000);
                i++;
            }
        }
        return null;
    }

    public void notifyImageLoaded(String str, Bitmap bitmap) {
        Message message = new Message();
        message.what = 0;
        Bundle bundle = new Bundle();
        bundle.putString(IMAGE_URL_EXTRA, str);
        bundle.putParcelable(BITMAP_EXTRA, bitmap);
        message.setData(bundle);
        this.handler.sendMessage(message);
    }

    /* access modifiers changed from: protected */
    public byte[] retrieveImageData() throws IOException {
        int i = 0;
        HttpURLConnection httpURLConnection = (HttpURLConnection) new URL(this.imageUrl).openConnection();
        int contentLength = httpURLConnection.getContentLength();
        if (contentLength < 0) {
            return null;
        }
        byte[] bArr = new byte[contentLength];
        Log.d(LOG_TAG, "fetching image " + this.imageUrl + " (" + contentLength + ")");
        BufferedInputStream bufferedInputStream = new BufferedInputStream(httpURLConnection.getInputStream());
        int i2 = 0;
        while (true) {
            int i3 = i;
            if (i2 == -1 || i3 >= contentLength) {
                bufferedInputStream.close();
                httpURLConnection.disconnect();
            } else {
                i2 = bufferedInputStream.read(bArr, i3, contentLength - i3);
                i = i3 + i2;
            }
        }
        bufferedInputStream.close();
        httpURLConnection.disconnect();
        return bArr;
    }

    public void run() {
        Bitmap bitmap = imageCache.getBitmap(this.imageUrl);
        if (bitmap == null) {
            bitmap = downloadImage();
        }
        notifyImageLoaded(this.imageUrl, bitmap);
    }
}
