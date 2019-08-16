package com.squareup.picasso;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.BitmapFactory.Options;
import android.net.NetworkInfo;
import com.squareup.picasso.Downloader.ResponseException;
import com.squareup.picasso.Picasso.LoadedFrom;
import com.squareup.picasso.Picasso.Priority;
import com.squareup.picasso.RequestHandler.Result;
import java.io.IOException;
import java.io.InputStream;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.Future;
import java.util.concurrent.atomic.AtomicInteger;

class BitmapHunter implements Runnable {
    private static final Object DECODE_LOCK = new Object();
    private static final RequestHandler ERRORING_HANDLER = new RequestHandler() {
        public boolean canHandleRequest(Request request) {
            return true;
        }

        public Result load(Request request, int i) throws IOException {
            throw new IllegalStateException("Unrecognized type of request: " + request);
        }
    };
    private static final ThreadLocal<StringBuilder> NAME_BUILDER = new ThreadLocal<StringBuilder>() {
        /* access modifiers changed from: protected */
        public StringBuilder initialValue() {
            return new StringBuilder("Picasso-");
        }
    };
    private static final AtomicInteger SEQUENCE_GENERATOR = new AtomicInteger();
    Action action;
    List<Action> actions;
    final Cache cache;
    final Request data;
    final Dispatcher dispatcher;
    Exception exception;
    int exifRotation;
    Future<?> future;
    final String key;
    LoadedFrom loadedFrom;
    final int memoryPolicy;
    int networkPolicy;
    final Picasso picasso;
    Priority priority;
    final RequestHandler requestHandler;
    Bitmap result;
    int retryCount;
    final int sequence = SEQUENCE_GENERATOR.incrementAndGet();
    final Stats stats;

    BitmapHunter(Picasso picasso2, Dispatcher dispatcher2, Cache cache2, Stats stats2, Action action2, RequestHandler requestHandler2) {
        this.picasso = picasso2;
        this.dispatcher = dispatcher2;
        this.cache = cache2;
        this.stats = stats2;
        this.action = action2;
        this.key = action2.getKey();
        this.data = action2.getRequest();
        this.priority = action2.getPriority();
        this.memoryPolicy = action2.getMemoryPolicy();
        this.networkPolicy = action2.getNetworkPolicy();
        this.requestHandler = requestHandler2;
        this.retryCount = requestHandler2.getRetryCount();
    }

    static Bitmap decodeStream(InputStream inputStream, Request request) throws IOException {
        MarkableInputStream markableInputStream = new MarkableInputStream(inputStream);
        long savePosition = markableInputStream.savePosition(65536);
        Options createBitmapOptions = RequestHandler.createBitmapOptions(request);
        boolean requiresInSampleSize = RequestHandler.requiresInSampleSize(createBitmapOptions);
        boolean isWebPFile = Utils.isWebPFile(markableInputStream);
        markableInputStream.reset(savePosition);
        if (isWebPFile) {
            byte[] byteArray = Utils.toByteArray(markableInputStream);
            if (requiresInSampleSize) {
                BitmapFactory.decodeByteArray(byteArray, 0, byteArray.length, createBitmapOptions);
                RequestHandler.calculateInSampleSize(request.targetWidth, request.targetHeight, createBitmapOptions, request);
            }
            return BitmapFactory.decodeByteArray(byteArray, 0, byteArray.length, createBitmapOptions);
        }
        if (requiresInSampleSize) {
            BitmapFactory.decodeStream(markableInputStream, null, createBitmapOptions);
            RequestHandler.calculateInSampleSize(request.targetWidth, request.targetHeight, createBitmapOptions, request);
            markableInputStream.reset(savePosition);
        }
        Bitmap decodeStream = BitmapFactory.decodeStream(markableInputStream, null, createBitmapOptions);
        if (decodeStream != null) {
            return decodeStream;
        }
        throw new IOException("Failed to decode stream.");
    }

    public void run() {
        try {
            updateThreadName(this.data);
            if (this.picasso.loggingEnabled) {
                Utils.log("Hunter", "executing", Utils.getLogIdsForHunter(this));
            }
            this.result = hunt();
            if (this.result == null) {
                this.dispatcher.dispatchFailed(this);
            } else {
                this.dispatcher.dispatchComplete(this);
            }
        } catch (ResponseException e) {
            if (!e.localCacheOnly || e.responseCode != 504) {
                this.exception = e;
            }
            this.dispatcher.dispatchFailed(this);
        } catch (ContentLengthException e2) {
            this.exception = e2;
            this.dispatcher.dispatchRetry(this);
        } catch (IOException e3) {
            this.exception = e3;
            this.dispatcher.dispatchRetry(this);
        } catch (OutOfMemoryError e4) {
            StringWriter stringWriter = new StringWriter();
            this.stats.createSnapshot().dump(new PrintWriter(stringWriter));
            this.exception = new RuntimeException(stringWriter.toString(), e4);
            this.dispatcher.dispatchFailed(this);
        } catch (Exception e5) {
            this.exception = e5;
            this.dispatcher.dispatchFailed(this);
        } finally {
            Thread.currentThread().setName("Picasso-Idle");
        }
    }

    /* access modifiers changed from: 0000 */
    public Bitmap hunt() throws IOException {
        Bitmap bitmap = null;
        if (MemoryPolicy.shouldReadFromMemoryCache(this.memoryPolicy)) {
            bitmap = this.cache.get(this.key);
            if (bitmap != null) {
                this.stats.dispatchCacheHit();
                this.loadedFrom = LoadedFrom.MEMORY;
                if (this.picasso.loggingEnabled) {
                    Utils.log("Hunter", "decoded", this.data.logId(), "from cache");
                }
                return bitmap;
            }
        }
        this.data.networkPolicy = this.retryCount == 0 ? NetworkPolicy.OFFLINE.index : this.networkPolicy;
        Result load = this.requestHandler.load(this.data, this.networkPolicy);
        if (load != null) {
            this.loadedFrom = load.getLoadedFrom();
            this.exifRotation = load.getExifOrientation();
            bitmap = load.getBitmap();
            if (bitmap == null) {
                InputStream stream = load.getStream();
                try {
                    bitmap = decodeStream(stream, this.data);
                } finally {
                    Utils.closeQuietly(stream);
                }
            }
        }
        if (bitmap != null) {
            if (this.picasso.loggingEnabled) {
                Utils.log("Hunter", "decoded", this.data.logId());
            }
            this.stats.dispatchBitmapDecoded(bitmap);
            if (this.data.needsTransformation() || this.exifRotation != 0) {
                synchronized (DECODE_LOCK) {
                    if (this.data.needsMatrixTransform() || this.exifRotation != 0) {
                        bitmap = transformResult(this.data, bitmap, this.exifRotation);
                        if (this.picasso.loggingEnabled) {
                            Utils.log("Hunter", "transformed", this.data.logId());
                        }
                    }
                    if (this.data.hasCustomTransformations()) {
                        bitmap = applyCustomTransformations(this.data.transformations, bitmap);
                        if (this.picasso.loggingEnabled) {
                            Utils.log("Hunter", "transformed", this.data.logId(), "from custom transformations");
                        }
                    }
                }
                if (bitmap != null) {
                    this.stats.dispatchBitmapTransformed(bitmap);
                }
            }
        }
        return bitmap;
    }

    /* access modifiers changed from: 0000 */
    public void attach(Action action2) {
        boolean z = this.picasso.loggingEnabled;
        Request request = action2.request;
        if (this.action == null) {
            this.action = action2;
            if (!z) {
                return;
            }
            if (this.actions == null || this.actions.isEmpty()) {
                Utils.log("Hunter", "joined", request.logId(), "to empty hunter");
            } else {
                Utils.log("Hunter", "joined", request.logId(), Utils.getLogIdsForHunter(this, "to "));
            }
        } else {
            if (this.actions == null) {
                this.actions = new ArrayList(3);
            }
            this.actions.add(action2);
            if (z) {
                Utils.log("Hunter", "joined", request.logId(), Utils.getLogIdsForHunter(this, "to "));
            }
            Priority priority2 = action2.getPriority();
            if (priority2.ordinal() > this.priority.ordinal()) {
                this.priority = priority2;
            }
        }
    }

    /* access modifiers changed from: 0000 */
    public void detach(Action action2) {
        boolean z = false;
        if (this.action == action2) {
            this.action = null;
            z = true;
        } else if (this.actions != null) {
            z = this.actions.remove(action2);
        }
        if (z && action2.getPriority() == this.priority) {
            this.priority = computeNewPriority();
        }
        if (this.picasso.loggingEnabled) {
            Utils.log("Hunter", "removed", action2.request.logId(), Utils.getLogIdsForHunter(this, "from "));
        }
    }

    private Priority computeNewPriority() {
        Priority priority2;
        boolean z = true;
        int i = 0;
        Priority priority3 = Priority.LOW;
        boolean z2 = this.actions != null && !this.actions.isEmpty();
        if (this.action == null && !z2) {
            z = false;
        }
        if (!z) {
            return priority3;
        }
        if (this.action != null) {
            priority2 = this.action.getPriority();
        } else {
            priority2 = priority3;
        }
        if (!z2) {
            return priority2;
        }
        int size = this.actions.size();
        while (i < size) {
            Priority priority4 = ((Action) this.actions.get(i)).getPriority();
            if (priority4.ordinal() <= priority2.ordinal()) {
                priority4 = priority2;
            }
            i++;
            priority2 = priority4;
        }
        return priority2;
    }

    /* access modifiers changed from: 0000 */
    public boolean cancel() {
        if (this.action != null) {
            return false;
        }
        if ((this.actions == null || this.actions.isEmpty()) && this.future != null && this.future.cancel(false)) {
            return true;
        }
        return false;
    }

    /* access modifiers changed from: 0000 */
    public boolean isCancelled() {
        return this.future != null && this.future.isCancelled();
    }

    /* access modifiers changed from: 0000 */
    public boolean shouldRetry(boolean z, NetworkInfo networkInfo) {
        if (!(this.retryCount > 0)) {
            return false;
        }
        this.retryCount--;
        return this.requestHandler.shouldRetry(z, networkInfo);
    }

    /* access modifiers changed from: 0000 */
    public boolean supportsReplay() {
        return this.requestHandler.supportsReplay();
    }

    /* access modifiers changed from: 0000 */
    public Bitmap getResult() {
        return this.result;
    }

    /* access modifiers changed from: 0000 */
    public String getKey() {
        return this.key;
    }

    /* access modifiers changed from: 0000 */
    public int getMemoryPolicy() {
        return this.memoryPolicy;
    }

    /* access modifiers changed from: 0000 */
    public Request getData() {
        return this.data;
    }

    /* access modifiers changed from: 0000 */
    public Action getAction() {
        return this.action;
    }

    /* access modifiers changed from: 0000 */
    public Picasso getPicasso() {
        return this.picasso;
    }

    /* access modifiers changed from: 0000 */
    public List<Action> getActions() {
        return this.actions;
    }

    /* access modifiers changed from: 0000 */
    public Exception getException() {
        return this.exception;
    }

    /* access modifiers changed from: 0000 */
    public LoadedFrom getLoadedFrom() {
        return this.loadedFrom;
    }

    /* access modifiers changed from: 0000 */
    public Priority getPriority() {
        return this.priority;
    }

    static void updateThreadName(Request request) {
        String name = request.getName();
        StringBuilder sb = (StringBuilder) NAME_BUILDER.get();
        sb.ensureCapacity("Picasso-".length() + name.length());
        sb.replace("Picasso-".length(), sb.length(), name);
        Thread.currentThread().setName(sb.toString());
    }

    static BitmapHunter forRequest(Picasso picasso2, Dispatcher dispatcher2, Cache cache2, Stats stats2, Action action2) {
        Request request = action2.getRequest();
        List requestHandlers = picasso2.getRequestHandlers();
        int size = requestHandlers.size();
        for (int i = 0; i < size; i++) {
            RequestHandler requestHandler2 = (RequestHandler) requestHandlers.get(i);
            if (requestHandler2.canHandleRequest(request)) {
                return new BitmapHunter(picasso2, dispatcher2, cache2, stats2, action2, requestHandler2);
            }
        }
        return new BitmapHunter(picasso2, dispatcher2, cache2, stats2, action2, ERRORING_HANDLER);
    }

    static Bitmap applyCustomTransformations(List<Transformation> list, Bitmap bitmap) {
        int size = list.size();
        int i = 0;
        Bitmap bitmap2 = bitmap;
        while (i < size) {
            final Transformation transformation = (Transformation) list.get(i);
            try {
                Bitmap transform = transformation.transform(bitmap2);
                if (transform == null) {
                    final StringBuilder append = new StringBuilder().append("Transformation ").append(transformation.key()).append(" returned null after ").append(i).append(" previous transformation(s).\n\nTransformation list:\n");
                    for (Transformation key2 : list) {
                        append.append(key2.key()).append(10);
                    }
                    Picasso.HANDLER.post(new Runnable() {
                        public void run() {
                            throw new NullPointerException(append.toString());
                        }
                    });
                    return null;
                } else if (transform == bitmap2 && bitmap2.isRecycled()) {
                    Picasso.HANDLER.post(new Runnable() {
                        public void run() {
                            throw new IllegalStateException("Transformation " + transformation.key() + " returned input Bitmap but recycled it.");
                        }
                    });
                    return null;
                } else if (transform == bitmap2 || bitmap2.isRecycled()) {
                    i++;
                    bitmap2 = transform;
                } else {
                    Picasso.HANDLER.post(new Runnable() {
                        public void run() {
                            throw new IllegalStateException("Transformation " + transformation.key() + " mutated input Bitmap but failed to recycle the original.");
                        }
                    });
                    return null;
                }
            } catch (RuntimeException e) {
                Picasso.HANDLER.post(new Runnable() {
                    public void run() {
                        throw new RuntimeException("Transformation " + transformation.key() + " crashed with exception.", e);
                    }
                });
                return null;
            }
        }
        return bitmap2;
    }

    /* JADX WARNING: Removed duplicated region for block: B:17:0x005b  */
    /* JADX WARNING: Removed duplicated region for block: B:20:0x0067  */
    /* JADX WARNING: Removed duplicated region for block: B:47:? A[RETURN, SYNTHETIC] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    static android.graphics.Bitmap transformResult(com.squareup.picasso.Request r13, android.graphics.Bitmap r14, int r15) {
        /*
            int r7 = r14.getWidth()
            int r6 = r14.getHeight()
            boolean r10 = r13.onlyScaleDown
            r9 = 0
            r8 = 0
            android.graphics.Matrix r5 = new android.graphics.Matrix
            r5.<init>()
            boolean r0 = r13.needsMatrixTransform()
            if (r0 == 0) goto L_0x00bd
            int r11 = r13.targetWidth
            int r12 = r13.targetHeight
            float r0 = r13.rotationDegrees
            r1 = 0
            int r1 = (r0 > r1 ? 1 : (r0 == r1 ? 0 : -1))
            if (r1 == 0) goto L_0x002d
            boolean r1 = r13.hasRotationPivot
            if (r1 == 0) goto L_0x006c
            float r1 = r13.rotationPivotX
            float r2 = r13.rotationPivotY
            r5.setRotate(r0, r1, r2)
        L_0x002d:
            boolean r0 = r13.centerCrop
            if (r0 == 0) goto L_0x0083
            float r0 = (float) r11
            float r1 = (float) r7
            float r1 = r0 / r1
            float r0 = (float) r12
            float r2 = (float) r6
            float r0 = r0 / r2
            int r2 = (r1 > r0 ? 1 : (r1 == r0 ? 0 : -1))
            if (r2 <= 0) goto L_0x0070
            float r2 = (float) r6
            float r0 = r0 / r1
            float r0 = r0 * r2
            double r2 = (double) r0
            double r2 = java.lang.Math.ceil(r2)
            int r2 = (int) r2
            int r0 = r6 - r2
            int r8 = r0 / 2
            float r0 = (float) r12
            float r3 = (float) r2
            float r0 = r0 / r3
            r4 = r2
            r3 = r7
        L_0x004e:
            boolean r2 = shouldResize(r10, r7, r6, r11, r12)
            if (r2 == 0) goto L_0x0057
            r5.preScale(r1, r0)
        L_0x0057:
            r2 = r8
            r1 = r9
        L_0x0059:
            if (r15 == 0) goto L_0x005f
            float r0 = (float) r15
            r5.preRotate(r0)
        L_0x005f:
            r6 = 1
            r0 = r14
            android.graphics.Bitmap r0 = android.graphics.Bitmap.createBitmap(r0, r1, r2, r3, r4, r5, r6)
            if (r0 == r14) goto L_0x006b
            r14.recycle()
            r14 = r0
        L_0x006b:
            return r14
        L_0x006c:
            r5.setRotate(r0)
            goto L_0x002d
        L_0x0070:
            float r2 = (float) r7
            float r1 = r1 / r0
            float r1 = r1 * r2
            double r2 = (double) r1
            double r2 = java.lang.Math.ceil(r2)
            int r2 = (int) r2
            int r1 = r7 - r2
            int r9 = r1 / 2
            float r1 = (float) r11
            float r3 = (float) r2
            float r1 = r1 / r3
            r4 = r6
            r3 = r2
            goto L_0x004e
        L_0x0083:
            boolean r0 = r13.centerInside
            if (r0 == 0) goto L_0x00a1
            float r0 = (float) r11
            float r1 = (float) r7
            float r0 = r0 / r1
            float r1 = (float) r12
            float r2 = (float) r6
            float r1 = r1 / r2
            int r2 = (r0 > r1 ? 1 : (r0 == r1 ? 0 : -1))
            if (r2 >= 0) goto L_0x009f
        L_0x0091:
            boolean r1 = shouldResize(r10, r7, r6, r11, r12)
            if (r1 == 0) goto L_0x009a
            r5.preScale(r0, r0)
        L_0x009a:
            r4 = r6
            r3 = r7
            r2 = r8
            r1 = r9
            goto L_0x0059
        L_0x009f:
            r0 = r1
            goto L_0x0091
        L_0x00a1:
            if (r11 != 0) goto L_0x00a5
            if (r12 == 0) goto L_0x00bd
        L_0x00a5:
            if (r11 != r7) goto L_0x00a9
            if (r12 == r6) goto L_0x00bd
        L_0x00a9:
            if (r11 == 0) goto L_0x00c2
            float r0 = (float) r11
            float r1 = (float) r7
            float r0 = r0 / r1
            r1 = r0
        L_0x00af:
            if (r12 == 0) goto L_0x00c7
            float r0 = (float) r12
            float r2 = (float) r6
            float r0 = r0 / r2
        L_0x00b4:
            boolean r2 = shouldResize(r10, r7, r6, r11, r12)
            if (r2 == 0) goto L_0x00bd
            r5.preScale(r1, r0)
        L_0x00bd:
            r4 = r6
            r3 = r7
            r2 = r8
            r1 = r9
            goto L_0x0059
        L_0x00c2:
            float r0 = (float) r12
            float r1 = (float) r6
            float r0 = r0 / r1
            r1 = r0
            goto L_0x00af
        L_0x00c7:
            float r0 = (float) r11
            float r2 = (float) r7
            float r0 = r0 / r2
            goto L_0x00b4
        */
        throw new UnsupportedOperationException("Method not decompiled: com.squareup.picasso.BitmapHunter.transformResult(com.squareup.picasso.Request, android.graphics.Bitmap, int):android.graphics.Bitmap");
    }

    private static boolean shouldResize(boolean z, int i, int i2, int i3, int i4) {
        return !z || i > i3 || i2 > i4;
    }
}
