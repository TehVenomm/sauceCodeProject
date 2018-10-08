package com.facebook.internal;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Handler;
import android.os.Looper;
import com.facebook.internal.ImageRequest.Callback;
import com.facebook.internal.WorkQueue.WorkItem;
import java.io.Closeable;
import java.util.HashMap;
import java.util.Map;

public class ImageDownloader {
    private static final int CACHE_READ_QUEUE_MAX_CONCURRENT = 2;
    private static final int DOWNLOAD_QUEUE_MAX_CONCURRENT = 8;
    private static WorkQueue cacheReadQueue = new WorkQueue(2);
    private static WorkQueue downloadQueue = new WorkQueue(8);
    private static Handler handler;
    private static final Map<RequestKey, DownloaderContext> pendingRequests = new HashMap();

    private static class CacheReadWorkItem implements Runnable {
        private boolean allowCachedRedirects;
        private Context context;
        private RequestKey key;

        CacheReadWorkItem(Context context, RequestKey requestKey, boolean z) {
            this.context = context;
            this.key = requestKey;
            this.allowCachedRedirects = z;
        }

        public void run() {
            ImageDownloader.readFromCache(this.key, this.context, this.allowCachedRedirects);
        }
    }

    private static class DownloadImageWorkItem implements Runnable {
        private Context context;
        private RequestKey key;

        DownloadImageWorkItem(Context context, RequestKey requestKey) {
            this.context = context;
            this.key = requestKey;
        }

        public void run() {
            ImageDownloader.download(this.key, this.context);
        }
    }

    private static class DownloaderContext {
        boolean isCancelled;
        ImageRequest request;
        WorkItem workItem;

        private DownloaderContext() {
        }
    }

    private static class RequestKey {
        private static final int HASH_MULTIPLIER = 37;
        private static final int HASH_SEED = 29;
        Object tag;
        Uri uri;

        RequestKey(Uri uri, Object obj) {
            this.uri = uri;
            this.tag = obj;
        }

        public boolean equals(Object obj) {
            if (obj == null || !(obj instanceof RequestKey)) {
                return false;
            }
            RequestKey requestKey = (RequestKey) obj;
            return requestKey.uri == this.uri && requestKey.tag == this.tag;
        }

        public int hashCode() {
            return ((this.uri.hashCode() + 1073) * 37) + this.tag.hashCode();
        }
    }

    public static boolean cancelRequest(ImageRequest imageRequest) {
        boolean z;
        RequestKey requestKey = new RequestKey(imageRequest.getImageUri(), imageRequest.getCallerTag());
        synchronized (pendingRequests) {
            DownloaderContext downloaderContext = (DownloaderContext) pendingRequests.get(requestKey);
            if (downloaderContext == null) {
                z = false;
            } else if (downloaderContext.workItem.cancel()) {
                pendingRequests.remove(requestKey);
                z = true;
            } else {
                downloaderContext.isCancelled = true;
                z = true;
            }
        }
        return z;
    }

    public static void clearCache(Context context) {
        ImageResponseCache.clearCache(context);
        UrlRedirectCache.clearCache();
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static void download(com.facebook.internal.ImageDownloader.RequestKey r11, android.content.Context r12) {
        /*
        r4 = 1;
        r5 = 0;
        r2 = 0;
        r0 = new java.net.URL;	 Catch:{ IOException -> 0x00c4, all -> 0x00bb }
        r1 = r11.uri;	 Catch:{ IOException -> 0x00c4, all -> 0x00bb }
        r1 = r1.toString();	 Catch:{ IOException -> 0x00c4, all -> 0x00bb }
        r0.<init>(r1);	 Catch:{ IOException -> 0x00c4, all -> 0x00bb }
        r0 = r0.openConnection();	 Catch:{ IOException -> 0x00c4, all -> 0x00bb }
        r0 = (java.net.HttpURLConnection) r0;	 Catch:{ IOException -> 0x00c4, all -> 0x00bb }
        r1 = 0;
        r0.setInstanceFollowRedirects(r1);	 Catch:{ IOException -> 0x00c9, all -> 0x00be }
        r1 = r0.getResponseCode();	 Catch:{ IOException -> 0x00c9, all -> 0x00be }
        switch(r1) {
            case 200: goto L_0x0089;
            case 301: goto L_0x0053;
            case 302: goto L_0x0053;
            default: goto L_0x001f;
        };	 Catch:{ IOException -> 0x00c9, all -> 0x00be }
    L_0x001f:
        r1 = r0.getErrorStream();	 Catch:{ IOException -> 0x00c9, all -> 0x00be }
        r6 = new java.lang.StringBuilder;	 Catch:{ IOException -> 0x0040, all -> 0x00b0 }
        r6.<init>();	 Catch:{ IOException -> 0x0040, all -> 0x00b0 }
        if (r1 == 0) goto L_0x00a6;
    L_0x002a:
        r3 = new java.io.InputStreamReader;	 Catch:{ IOException -> 0x0040, all -> 0x00b0 }
        r3.<init>(r1);	 Catch:{ IOException -> 0x0040, all -> 0x00b0 }
        r7 = 128; // 0x80 float:1.794E-43 double:6.32E-322;
        r7 = new char[r7];	 Catch:{ IOException -> 0x0040, all -> 0x00b0 }
    L_0x0033:
        r8 = 0;
        r9 = r7.length;	 Catch:{ IOException -> 0x0040, all -> 0x00b0 }
        r8 = r3.read(r7, r8, r9);	 Catch:{ IOException -> 0x0040, all -> 0x00b0 }
        if (r8 <= 0) goto L_0x0096;
    L_0x003b:
        r9 = 0;
        r6.append(r7, r9, r8);	 Catch:{ IOException -> 0x0040, all -> 0x00b0 }
        goto L_0x0033;
    L_0x0040:
        r3 = move-exception;
        r10 = r3;
        r3 = r1;
        r1 = r0;
        r0 = r10;
    L_0x0045:
        com.facebook.internal.Utility.closeQuietly(r3);
        com.facebook.internal.Utility.disconnectQuietly(r1);
        r3 = r4;
        r4 = r0;
    L_0x004d:
        if (r3 == 0) goto L_0x0052;
    L_0x004f:
        issueResponse(r11, r4, r2, r5);
    L_0x0052:
        return;
    L_0x0053:
        r1 = "location";
        r1 = r0.getHeaderField(r1);	 Catch:{ IOException -> 0x00d0, all -> 0x00be }
        r3 = com.facebook.internal.Utility.isNullOrEmpty(r1);	 Catch:{ IOException -> 0x00d0, all -> 0x00be }
        if (r3 != 0) goto L_0x00d8;
    L_0x005f:
        r1 = android.net.Uri.parse(r1);	 Catch:{ IOException -> 0x00d0, all -> 0x00be }
        r3 = r11.uri;	 Catch:{ IOException -> 0x00d0, all -> 0x00be }
        com.facebook.internal.UrlRedirectCache.cacheUriRedirect(r3, r1);	 Catch:{ IOException -> 0x00d0, all -> 0x00be }
        r3 = removePendingRequest(r11);	 Catch:{ IOException -> 0x00d0, all -> 0x00be }
        if (r3 == 0) goto L_0x00d8;
    L_0x006e:
        r4 = r3.isCancelled;	 Catch:{ IOException -> 0x00d0, all -> 0x00be }
        if (r4 != 0) goto L_0x00d8;
    L_0x0072:
        r3 = r3.request;	 Catch:{ IOException -> 0x00d0, all -> 0x00be }
        r4 = new com.facebook.internal.ImageDownloader$RequestKey;	 Catch:{ IOException -> 0x00d0, all -> 0x00be }
        r6 = r11.tag;	 Catch:{ IOException -> 0x00d0, all -> 0x00be }
        r4.<init>(r1, r6);	 Catch:{ IOException -> 0x00d0, all -> 0x00be }
        r1 = 0;
        enqueueCacheRead(r3, r4, r1);	 Catch:{ IOException -> 0x00d0, all -> 0x00be }
        r1 = r2;
        r3 = r5;
        r4 = r2;
    L_0x0082:
        com.facebook.internal.Utility.closeQuietly(r1);
        com.facebook.internal.Utility.disconnectQuietly(r0);
        goto L_0x004d;
    L_0x0089:
        r1 = com.facebook.internal.ImageResponseCache.interceptAndCacheImageStream(r12, r0);	 Catch:{ IOException -> 0x00c9, all -> 0x00be }
        r3 = android.graphics.BitmapFactory.decodeStream(r1);	 Catch:{ IOException -> 0x0040, all -> 0x00b0 }
        r10 = r3;
        r3 = r4;
        r4 = r2;
        r2 = r10;
        goto L_0x0082;
    L_0x0096:
        com.facebook.internal.Utility.closeQuietly(r3);	 Catch:{ IOException -> 0x0040, all -> 0x00b0 }
    L_0x0099:
        r3 = new com.facebook.FacebookException;	 Catch:{ IOException -> 0x0040, all -> 0x00b0 }
        r6 = r6.toString();	 Catch:{ IOException -> 0x0040, all -> 0x00b0 }
        r3.<init>(r6);	 Catch:{ IOException -> 0x0040, all -> 0x00b0 }
        r10 = r4;
        r4 = r3;
        r3 = r10;
        goto L_0x0082;
    L_0x00a6:
        r3 = com.facebook.C0365R.string.com_facebook_image_download_unknown_error;	 Catch:{ IOException -> 0x0040, all -> 0x00b0 }
        r3 = r12.getString(r3);	 Catch:{ IOException -> 0x0040, all -> 0x00b0 }
        r6.append(r3);	 Catch:{ IOException -> 0x0040, all -> 0x00b0 }
        goto L_0x0099;
    L_0x00b0:
        r2 = move-exception;
        r10 = r2;
        r2 = r0;
        r0 = r10;
    L_0x00b4:
        com.facebook.internal.Utility.closeQuietly(r1);
        com.facebook.internal.Utility.disconnectQuietly(r2);
        throw r0;
    L_0x00bb:
        r0 = move-exception;
        r1 = r2;
        goto L_0x00b4;
    L_0x00be:
        r1 = move-exception;
        r10 = r1;
        r1 = r2;
        r2 = r0;
        r0 = r10;
        goto L_0x00b4;
    L_0x00c4:
        r0 = move-exception;
        r1 = r2;
        r3 = r2;
        goto L_0x0045;
    L_0x00c9:
        r1 = move-exception;
        r3 = r2;
        r10 = r0;
        r0 = r1;
        r1 = r10;
        goto L_0x0045;
    L_0x00d0:
        r1 = move-exception;
        r3 = r2;
        r4 = r5;
        r10 = r1;
        r1 = r0;
        r0 = r10;
        goto L_0x0045;
    L_0x00d8:
        r1 = r2;
        r3 = r5;
        r4 = r2;
        goto L_0x0082;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.facebook.internal.ImageDownloader.download(com.facebook.internal.ImageDownloader$RequestKey, android.content.Context):void");
    }

    public static void downloadAsync(ImageRequest imageRequest) {
        if (imageRequest != null) {
            RequestKey requestKey = new RequestKey(imageRequest.getImageUri(), imageRequest.getCallerTag());
            synchronized (pendingRequests) {
                DownloaderContext downloaderContext = (DownloaderContext) pendingRequests.get(requestKey);
                if (downloaderContext != null) {
                    downloaderContext.request = imageRequest;
                    downloaderContext.isCancelled = false;
                    downloaderContext.workItem.moveToFront();
                } else {
                    enqueueCacheRead(imageRequest, requestKey, imageRequest.isCachedRedirectAllowed());
                }
            }
        }
    }

    private static void enqueueCacheRead(ImageRequest imageRequest, RequestKey requestKey, boolean z) {
        enqueueRequest(imageRequest, requestKey, cacheReadQueue, new CacheReadWorkItem(imageRequest.getContext(), requestKey, z));
    }

    private static void enqueueDownload(ImageRequest imageRequest, RequestKey requestKey) {
        enqueueRequest(imageRequest, requestKey, downloadQueue, new DownloadImageWorkItem(imageRequest.getContext(), requestKey));
    }

    private static void enqueueRequest(ImageRequest imageRequest, RequestKey requestKey, WorkQueue workQueue, Runnable runnable) {
        synchronized (pendingRequests) {
            DownloaderContext downloaderContext = new DownloaderContext();
            downloaderContext.request = imageRequest;
            pendingRequests.put(requestKey, downloaderContext);
            downloaderContext.workItem = workQueue.addActiveWorkItem(runnable);
        }
    }

    private static Handler getHandler() {
        synchronized (ImageDownloader.class) {
            Class mainLooper;
            try {
                if (handler == null) {
                    mainLooper = Looper.getMainLooper();
                    handler = new Handler(mainLooper);
                }
                Handler handler = handler;
                return handler;
            } finally {
                mainLooper = ImageDownloader.class;
            }
        }
    }

    private static void issueResponse(RequestKey requestKey, Exception exception, Bitmap bitmap, boolean z) {
        DownloaderContext removePendingRequest = removePendingRequest(requestKey);
        if (removePendingRequest != null && !removePendingRequest.isCancelled) {
            final ImageRequest imageRequest = removePendingRequest.request;
            final Callback callback = imageRequest.getCallback();
            if (callback != null) {
                final Exception exception2 = exception;
                final boolean z2 = z;
                final Bitmap bitmap2 = bitmap;
                getHandler().post(new Runnable() {
                    public void run() {
                        callback.onCompleted(new ImageResponse(imageRequest, exception2, z2, bitmap2));
                    }
                });
            }
        }
    }

    public static void prioritizeRequest(ImageRequest imageRequest) {
        RequestKey requestKey = new RequestKey(imageRequest.getImageUri(), imageRequest.getCallerTag());
        synchronized (pendingRequests) {
            DownloaderContext downloaderContext = (DownloaderContext) pendingRequests.get(requestKey);
            if (downloaderContext != null) {
                downloaderContext.workItem.moveToFront();
            }
        }
    }

    private static void readFromCache(RequestKey requestKey, Context context, boolean z) {
        Closeable cachedImageStream;
        DownloaderContext removePendingRequest;
        boolean z2 = false;
        if (z) {
            Uri redirectedUri = UrlRedirectCache.getRedirectedUri(requestKey.uri);
            if (redirectedUri != null) {
                cachedImageStream = ImageResponseCache.getCachedImageStream(redirectedUri, context);
                if (cachedImageStream != null) {
                    z2 = true;
                }
                if (!z2) {
                    cachedImageStream = ImageResponseCache.getCachedImageStream(requestKey.uri, context);
                }
                if (cachedImageStream == null) {
                    Bitmap decodeStream = BitmapFactory.decodeStream(cachedImageStream);
                    Utility.closeQuietly(cachedImageStream);
                    issueResponse(requestKey, null, decodeStream, z2);
                }
                removePendingRequest = removePendingRequest(requestKey);
                if (removePendingRequest != null && !removePendingRequest.isCancelled) {
                    enqueueDownload(removePendingRequest.request, requestKey);
                    return;
                }
                return;
            }
        }
        cachedImageStream = null;
        if (z2) {
            cachedImageStream = ImageResponseCache.getCachedImageStream(requestKey.uri, context);
        }
        if (cachedImageStream == null) {
            removePendingRequest = removePendingRequest(requestKey);
            if (removePendingRequest != null) {
                return;
            }
            return;
        }
        Bitmap decodeStream2 = BitmapFactory.decodeStream(cachedImageStream);
        Utility.closeQuietly(cachedImageStream);
        issueResponse(requestKey, null, decodeStream2, z2);
    }

    private static DownloaderContext removePendingRequest(RequestKey requestKey) {
        DownloaderContext downloaderContext;
        synchronized (pendingRequests) {
            downloaderContext = (DownloaderContext) pendingRequests.remove(requestKey);
        }
        return downloaderContext;
    }
}
