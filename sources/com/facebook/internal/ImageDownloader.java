package com.facebook.internal;

import android.content.Context;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Handler;
import android.os.Looper;
import com.facebook.internal.ImageRequest.Callback;
import com.facebook.internal.WorkQueue.WorkItem;
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

        CacheReadWorkItem(Context context2, RequestKey requestKey, boolean z) {
            this.context = context2;
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

        DownloadImageWorkItem(Context context2, RequestKey requestKey) {
            this.context = context2;
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

        RequestKey(Uri uri2, Object obj) {
            this.uri = uri2;
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

    /* access modifiers changed from: private */
    /* JADX WARNING: Code restructure failed: missing block: B:30:0x0080, code lost:
        com.facebook.internal.Utility.closeQuietly(r1);
        com.facebook.internal.Utility.disconnectQuietly(r0);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:44:0x00b5, code lost:
        r1 = move-exception;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:45:0x00b6, code lost:
        r3 = r1;
        r4 = r0;
        r5 = null;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:48:0x00bf, code lost:
        r1 = move-exception;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:49:0x00c0, code lost:
        r3 = r1;
        r6 = r0;
        r7 = null;
     */
    /* JADX WARNING: Failed to process nested try/catch */
    /* JADX WARNING: Removed duplicated region for block: B:19:0x004c  */
    /* JADX WARNING: Removed duplicated region for block: B:44:0x00b5 A[ExcHandler: all (r1v4 'th' java.lang.Throwable A[CUSTOM_DECLARE]), Splitter:B:4:0x0015] */
    /* JADX WARNING: Removed duplicated region for block: B:55:? A[RETURN, SYNTHETIC] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static void download(com.facebook.internal.ImageDownloader.RequestKey r10, android.content.Context r11) {
        /*
            r4 = 1
            r5 = 0
            r2 = 0
            java.net.URL r0 = new java.net.URL     // Catch:{ IOException -> 0x00ba, all -> 0x00b0 }
            android.net.Uri r1 = r10.uri     // Catch:{ IOException -> 0x00ba, all -> 0x00b0 }
            java.lang.String r1 = r1.toString()     // Catch:{ IOException -> 0x00ba, all -> 0x00b0 }
            r0.<init>(r1)     // Catch:{ IOException -> 0x00ba, all -> 0x00b0 }
            java.net.URLConnection r0 = r0.openConnection()     // Catch:{ IOException -> 0x00ba, all -> 0x00b0 }
            java.net.HttpURLConnection r0 = (java.net.HttpURLConnection) r0     // Catch:{ IOException -> 0x00ba, all -> 0x00b0 }
            r1 = 0
            r0.setInstanceFollowRedirects(r1)     // Catch:{ IOException -> 0x00bf, all -> 0x00b5 }
            int r1 = r0.getResponseCode()     // Catch:{ IOException -> 0x00bf, all -> 0x00b5 }
            switch(r1) {
                case 200: goto L_0x0087;
                case 301: goto L_0x0050;
                case 302: goto L_0x0050;
                default: goto L_0x001f;
            }     // Catch:{ IOException -> 0x00bf, all -> 0x00b5 }
        L_0x001f:
            java.io.InputStream r1 = r0.getErrorStream()     // Catch:{ IOException -> 0x00bf, all -> 0x00b5 }
            java.lang.StringBuilder r6 = new java.lang.StringBuilder     // Catch:{ IOException -> 0x0040, all -> 0x00a5 }
            r6.<init>()     // Catch:{ IOException -> 0x0040, all -> 0x00a5 }
            if (r1 == 0) goto L_0x009f
            java.io.InputStreamReader r3 = new java.io.InputStreamReader     // Catch:{ IOException -> 0x0040, all -> 0x00a5 }
            r3.<init>(r1)     // Catch:{ IOException -> 0x0040, all -> 0x00a5 }
            r7 = 128(0x80, float:1.794E-43)
            char[] r7 = new char[r7]     // Catch:{ IOException -> 0x0040, all -> 0x00a5 }
        L_0x0033:
            r8 = 0
            int r9 = r7.length     // Catch:{ IOException -> 0x0040, all -> 0x00a5 }
            int r8 = r3.read(r7, r8, r9)     // Catch:{ IOException -> 0x0040, all -> 0x00a5 }
            if (r8 <= 0) goto L_0x0091
            r9 = 0
            r6.append(r7, r9, r8)     // Catch:{ IOException -> 0x0040, all -> 0x00a5 }
            goto L_0x0033
        L_0x0040:
            r3 = move-exception
            r6 = r0
            r7 = r1
        L_0x0043:
            com.facebook.internal.Utility.closeQuietly(r7)
            com.facebook.internal.Utility.disconnectQuietly(r6)
            r6 = r2
        L_0x004a:
            if (r4 == 0) goto L_0x004f
            issueResponse(r10, r3, r6, r5)
        L_0x004f:
            return
        L_0x0050:
            java.lang.String r1 = "location"
            java.lang.String r1 = r0.getHeaderField(r1)     // Catch:{ IOException -> 0x00c4, all -> 0x00b5 }
            boolean r3 = com.facebook.internal.Utility.isNullOrEmpty(r1)     // Catch:{ IOException -> 0x00c4, all -> 0x00b5 }
            if (r3 != 0) goto L_0x00cb
            android.net.Uri r1 = android.net.Uri.parse(r1)     // Catch:{ IOException -> 0x00c4, all -> 0x00b5 }
            android.net.Uri r3 = r10.uri     // Catch:{ IOException -> 0x00c4, all -> 0x00b5 }
            com.facebook.internal.UrlRedirectCache.cacheUriRedirect(r3, r1)     // Catch:{ IOException -> 0x00c4, all -> 0x00b5 }
            com.facebook.internal.ImageDownloader$DownloaderContext r3 = removePendingRequest(r10)     // Catch:{ IOException -> 0x00c4, all -> 0x00b5 }
            if (r3 == 0) goto L_0x00cb
            boolean r4 = r3.isCancelled     // Catch:{ IOException -> 0x00c4, all -> 0x00b5 }
            if (r4 != 0) goto L_0x00cb
            com.facebook.internal.ImageRequest r3 = r3.request     // Catch:{ IOException -> 0x00c4, all -> 0x00b5 }
            com.facebook.internal.ImageDownloader$RequestKey r4 = new com.facebook.internal.ImageDownloader$RequestKey     // Catch:{ IOException -> 0x00c4, all -> 0x00b5 }
            java.lang.Object r6 = r10.tag     // Catch:{ IOException -> 0x00c4, all -> 0x00b5 }
            r4.<init>(r1, r6)     // Catch:{ IOException -> 0x00c4, all -> 0x00b5 }
            r1 = 0
            enqueueCacheRead(r3, r4, r1)     // Catch:{ IOException -> 0x00c4, all -> 0x00b5 }
            r1 = r2
            r3 = r2
            r4 = r5
            r6 = r2
        L_0x0080:
            com.facebook.internal.Utility.closeQuietly(r1)
            com.facebook.internal.Utility.disconnectQuietly(r0)
            goto L_0x004a
        L_0x0087:
            java.io.InputStream r1 = com.facebook.internal.ImageResponseCache.interceptAndCacheImageStream(r11, r0)     // Catch:{ IOException -> 0x00bf, all -> 0x00b5 }
            android.graphics.Bitmap r6 = android.graphics.BitmapFactory.decodeStream(r1)     // Catch:{ IOException -> 0x0040, all -> 0x00a5 }
            r3 = r2
            goto L_0x0080
        L_0x0091:
            com.facebook.internal.Utility.closeQuietly(r3)     // Catch:{ IOException -> 0x0040, all -> 0x00a5 }
        L_0x0094:
            com.facebook.FacebookException r3 = new com.facebook.FacebookException     // Catch:{ IOException -> 0x0040, all -> 0x00a5 }
            java.lang.String r6 = r6.toString()     // Catch:{ IOException -> 0x0040, all -> 0x00a5 }
            r3.<init>(r6)     // Catch:{ IOException -> 0x0040, all -> 0x00a5 }
            r6 = r2
            goto L_0x0080
        L_0x009f:
            java.lang.String r3 = "Unexpected error while downloading an image."
            r6.append(r3)     // Catch:{ IOException -> 0x0040, all -> 0x00a5 }
            goto L_0x0094
        L_0x00a5:
            r2 = move-exception
            r3 = r2
            r4 = r0
            r5 = r1
        L_0x00a9:
            com.facebook.internal.Utility.closeQuietly(r5)
            com.facebook.internal.Utility.disconnectQuietly(r4)
            throw r3
        L_0x00b0:
            r0 = move-exception
            r3 = r0
            r4 = r2
            r5 = r2
            goto L_0x00a9
        L_0x00b5:
            r1 = move-exception
            r3 = r1
            r4 = r0
            r5 = r2
            goto L_0x00a9
        L_0x00ba:
            r0 = move-exception
            r3 = r0
            r6 = r2
            r7 = r2
            goto L_0x0043
        L_0x00bf:
            r1 = move-exception
            r3 = r1
            r6 = r0
            r7 = r2
            goto L_0x0043
        L_0x00c4:
            r1 = move-exception
            r3 = r1
            r6 = r0
            r7 = r2
            r4 = r5
            goto L_0x0043
        L_0x00cb:
            r1 = r2
            r3 = r2
            r4 = r5
            r6 = r2
            goto L_0x0080
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
        Handler handler2;
        synchronized (ImageDownloader.class) {
            try {
                if (handler == null) {
                    handler = new Handler(Looper.getMainLooper());
                }
                handler2 = handler;
            } finally {
                Class<ImageDownloader> cls = ImageDownloader.class;
            }
        }
        return handler2;
    }

    private static void issueResponse(RequestKey requestKey, Exception exc, Bitmap bitmap, boolean z) {
        DownloaderContext removePendingRequest = removePendingRequest(requestKey);
        if (removePendingRequest != null && !removePendingRequest.isCancelled) {
            final ImageRequest imageRequest = removePendingRequest.request;
            final Callback callback = imageRequest.getCallback();
            if (callback != null) {
                final Exception exc2 = exc;
                final boolean z2 = z;
                final Bitmap bitmap2 = bitmap;
                getHandler().post(new Runnable() {
                    public void run() {
                        callback.onCompleted(new ImageResponse(imageRequest, exc2, z2, bitmap2));
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

    /* access modifiers changed from: private */
    /* JADX WARNING: Removed duplicated region for block: B:10:0x001d  */
    /* JADX WARNING: Removed duplicated region for block: B:11:0x0028  */
    /* JADX WARNING: Removed duplicated region for block: B:8:0x0015  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static void readFromCache(com.facebook.internal.ImageDownloader.RequestKey r4, android.content.Context r5, boolean r6) {
        /*
            r1 = 0
            r2 = 0
            if (r6 == 0) goto L_0x0038
            android.net.Uri r0 = r4.uri
            android.net.Uri r0 = com.facebook.internal.UrlRedirectCache.getRedirectedUri(r0)
            if (r0 == 0) goto L_0x0038
            java.io.InputStream r0 = com.facebook.internal.ImageResponseCache.getCachedImageStream(r0, r5)
            if (r0 == 0) goto L_0x0013
            r2 = 1
        L_0x0013:
            if (r2 != 0) goto L_0x001b
            android.net.Uri r0 = r4.uri
            java.io.InputStream r0 = com.facebook.internal.ImageResponseCache.getCachedImageStream(r0, r5)
        L_0x001b:
            if (r0 == 0) goto L_0x0028
            android.graphics.Bitmap r3 = android.graphics.BitmapFactory.decodeStream(r0)
            com.facebook.internal.Utility.closeQuietly(r0)
            issueResponse(r4, r1, r3, r2)
        L_0x0027:
            return
        L_0x0028:
            com.facebook.internal.ImageDownloader$DownloaderContext r0 = removePendingRequest(r4)
            if (r0 == 0) goto L_0x0027
            boolean r1 = r0.isCancelled
            if (r1 != 0) goto L_0x0027
            com.facebook.internal.ImageRequest r0 = r0.request
            enqueueDownload(r0, r4)
            goto L_0x0027
        L_0x0038:
            r0 = r1
            goto L_0x0013
        */
        throw new UnsupportedOperationException("Method not decompiled: com.facebook.internal.ImageDownloader.readFromCache(com.facebook.internal.ImageDownloader$RequestKey, android.content.Context, boolean):void");
    }

    private static DownloaderContext removePendingRequest(RequestKey requestKey) {
        DownloaderContext downloaderContext;
        synchronized (pendingRequests) {
            downloaderContext = (DownloaderContext) pendingRequests.remove(requestKey);
        }
        return downloaderContext;
    }
}
