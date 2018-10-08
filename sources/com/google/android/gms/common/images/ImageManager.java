package com.google.android.gms.common.images;

import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.ParcelFileDescriptor;
import android.os.ResultReceiver;
import android.os.SystemClock;
import android.support.v4.util.LruCache;
import android.util.Log;
import android.widget.ImageView;
import com.google.android.gms.common.annotation.KeepName;
import com.google.android.gms.internal.zzbcb;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import org.apache.commons.lang3.time.DateUtils;

public final class ImageManager {
    private static final Object zzfqv = new Object();
    private static HashSet<Uri> zzfqw = new HashSet();
    private static ImageManager zzfqx;
    private final Context mContext;
    private final Handler mHandler = new Handler(Looper.getMainLooper());
    private final ExecutorService zzfqy = Executors.newFixedThreadPool(4);
    private final zza zzfqz = null;
    private final zzbcb zzfra = new zzbcb();
    private final Map<zza, ImageReceiver> zzfrb = new HashMap();
    private final Map<Uri, ImageReceiver> zzfrc = new HashMap();
    private final Map<Uri, Long> zzfrd = new HashMap();

    @KeepName
    final class ImageReceiver extends ResultReceiver {
        private final Uri mUri;
        private final ArrayList<zza> zzfre = new ArrayList();
        private /* synthetic */ ImageManager zzfrf;

        ImageReceiver(ImageManager imageManager, Uri uri) {
            this.zzfrf = imageManager;
            super(new Handler(Looper.getMainLooper()));
            this.mUri = uri;
        }

        public final void onReceiveResult(int i, Bundle bundle) {
            this.zzfrf.zzfqy.execute(new zzb(this.zzfrf, this.mUri, (ParcelFileDescriptor) bundle.getParcelable("com.google.android.gms.extra.fileDescriptor")));
        }

        public final void zzaiz() {
            Intent intent = new Intent("com.google.android.gms.common.images.LOAD_IMAGE");
            intent.putExtra("com.google.android.gms.extras.uri", this.mUri);
            intent.putExtra("com.google.android.gms.extras.resultReceiver", this);
            intent.putExtra("com.google.android.gms.extras.priority", 3);
            this.zzfrf.mContext.sendBroadcast(intent);
        }

        public final void zzb(zza zza) {
            com.google.android.gms.common.internal.zzc.zzfx("ImageReceiver.addImageRequest() must be called in the main thread");
            this.zzfre.add(zza);
        }

        public final void zzc(zza zza) {
            com.google.android.gms.common.internal.zzc.zzfx("ImageReceiver.removeImageRequest() must be called in the main thread");
            this.zzfre.remove(zza);
        }
    }

    public interface OnImageLoadedListener {
        void onImageLoaded(Uri uri, Drawable drawable, boolean z);
    }

    static final class zza extends LruCache<zzb, Bitmap> {
        protected final /* synthetic */ void entryRemoved(boolean z, Object obj, Object obj2, Object obj3) {
            super.entryRemoved(z, (zzb) obj, (Bitmap) obj2, (Bitmap) obj3);
        }

        protected final /* synthetic */ int sizeOf(Object obj, Object obj2) {
            Bitmap bitmap = (Bitmap) obj2;
            return bitmap.getHeight() * bitmap.getRowBytes();
        }
    }

    final class zzb implements Runnable {
        private final Uri mUri;
        private /* synthetic */ ImageManager zzfrf;
        private final ParcelFileDescriptor zzfrg;

        public zzb(ImageManager imageManager, Uri uri, ParcelFileDescriptor parcelFileDescriptor) {
            this.zzfrf = imageManager;
            this.mUri = uri;
            this.zzfrg = parcelFileDescriptor;
        }

        public final void run() {
            Bitmap bitmap = null;
            boolean z = false;
            if (Looper.getMainLooper().getThread() == Thread.currentThread()) {
                String valueOf = String.valueOf(Thread.currentThread());
                String valueOf2 = String.valueOf(Looper.getMainLooper().getThread());
                Log.e("Asserts", new StringBuilder((String.valueOf(valueOf).length() + 56) + String.valueOf(valueOf2).length()).append("checkNotMainThread: current thread ").append(valueOf).append(" IS the main thread ").append(valueOf2).append("!").toString());
                throw new IllegalStateException("LoadBitmapFromDiskRunnable can't be executed in the main thread");
            }
            if (this.zzfrg != null) {
                try {
                    bitmap = BitmapFactory.decodeFileDescriptor(this.zzfrg.getFileDescriptor());
                } catch (Throwable e) {
                    String valueOf3 = String.valueOf(this.mUri);
                    Log.e("ImageManager", new StringBuilder(String.valueOf(valueOf3).length() + 34).append("OOM while loading bitmap for uri: ").append(valueOf3).toString(), e);
                    z = true;
                }
                try {
                    this.zzfrg.close();
                } catch (Throwable e2) {
                    Log.e("ImageManager", "closed failed", e2);
                }
            }
            CountDownLatch countDownLatch = new CountDownLatch(1);
            this.zzfrf.mHandler.post(new zzd(this.zzfrf, this.mUri, bitmap, z, countDownLatch));
            try {
                countDownLatch.await();
            } catch (InterruptedException e3) {
                valueOf = String.valueOf(this.mUri);
                Log.w("ImageManager", new StringBuilder(String.valueOf(valueOf).length() + 32).append("Latch interrupted while posting ").append(valueOf).toString());
            }
        }
    }

    final class zzc implements Runnable {
        private /* synthetic */ ImageManager zzfrf;
        private final zza zzfrh;

        public zzc(ImageManager imageManager, zza zza) {
            this.zzfrf = imageManager;
            this.zzfrh = zza;
        }

        public final void run() {
            com.google.android.gms.common.internal.zzc.zzfx("LoadImageRunnable must be executed on the main thread");
            ImageReceiver imageReceiver = (ImageReceiver) this.zzfrf.zzfrb.get(this.zzfrh);
            if (imageReceiver != null) {
                this.zzfrf.zzfrb.remove(this.zzfrh);
                imageReceiver.zzc(this.zzfrh);
            }
            zzb zzb = this.zzfrh.zzfrj;
            if (zzb.uri == null) {
                this.zzfrh.zza(this.zzfrf.mContext, this.zzfrf.zzfra, true);
                return;
            }
            Bitmap zza = this.zzfrf.zza(zzb);
            if (zza != null) {
                this.zzfrh.zza(this.zzfrf.mContext, zza, true);
                return;
            }
            Long l = (Long) this.zzfrf.zzfrd.get(zzb.uri);
            if (l != null) {
                if (SystemClock.elapsedRealtime() - l.longValue() < DateUtils.MILLIS_PER_HOUR) {
                    this.zzfrh.zza(this.zzfrf.mContext, this.zzfrf.zzfra, true);
                    return;
                }
                this.zzfrf.zzfrd.remove(zzb.uri);
            }
            this.zzfrh.zza(this.zzfrf.mContext, this.zzfrf.zzfra);
            imageReceiver = (ImageReceiver) this.zzfrf.zzfrc.get(zzb.uri);
            if (imageReceiver == null) {
                imageReceiver = new ImageReceiver(this.zzfrf, zzb.uri);
                this.zzfrf.zzfrc.put(zzb.uri, imageReceiver);
            }
            imageReceiver.zzb(this.zzfrh);
            if (!(this.zzfrh instanceof zzd)) {
                this.zzfrf.zzfrb.put(this.zzfrh, imageReceiver);
            }
            synchronized (ImageManager.zzfqv) {
                if (!ImageManager.zzfqw.contains(zzb.uri)) {
                    ImageManager.zzfqw.add(zzb.uri);
                    imageReceiver.zzaiz();
                }
            }
        }
    }

    final class zzd implements Runnable {
        private final Bitmap mBitmap;
        private final Uri mUri;
        private final CountDownLatch zzaop;
        private /* synthetic */ ImageManager zzfrf;
        private boolean zzfri;

        public zzd(ImageManager imageManager, Uri uri, Bitmap bitmap, boolean z, CountDownLatch countDownLatch) {
            this.zzfrf = imageManager;
            this.mUri = uri;
            this.mBitmap = bitmap;
            this.zzfri = z;
            this.zzaop = countDownLatch;
        }

        public final void run() {
            com.google.android.gms.common.internal.zzc.zzfx("OnBitmapLoadedRunnable must be executed in the main thread");
            boolean z = this.mBitmap != null;
            if (this.zzfrf.zzfqz != null) {
                if (this.zzfri) {
                    this.zzfrf.zzfqz.evictAll();
                    System.gc();
                    this.zzfri = false;
                    this.zzfrf.mHandler.post(this);
                    return;
                } else if (z) {
                    this.zzfrf.zzfqz.put(new zzb(this.mUri), this.mBitmap);
                }
            }
            ImageReceiver imageReceiver = (ImageReceiver) this.zzfrf.zzfrc.remove(this.mUri);
            if (imageReceiver != null) {
                ArrayList zza = imageReceiver.zzfre;
                int size = zza.size();
                for (int i = 0; i < size; i++) {
                    zza zza2 = (zza) zza.get(i);
                    if (z) {
                        zza2.zza(this.zzfrf.mContext, this.mBitmap, false);
                    } else {
                        this.zzfrf.zzfrd.put(this.mUri, Long.valueOf(SystemClock.elapsedRealtime()));
                        zza2.zza(this.zzfrf.mContext, this.zzfrf.zzfra, false);
                    }
                    if (!(zza2 instanceof zzd)) {
                        this.zzfrf.zzfrb.remove(zza2);
                    }
                }
            }
            this.zzaop.countDown();
            synchronized (ImageManager.zzfqv) {
                ImageManager.zzfqw.remove(this.mUri);
            }
        }
    }

    private ImageManager(Context context, boolean z) {
        this.mContext = context.getApplicationContext();
    }

    public static ImageManager create(Context context) {
        if (zzfqx == null) {
            zzfqx = new ImageManager(context, false);
        }
        return zzfqx;
    }

    private final Bitmap zza(zzb zzb) {
        return this.zzfqz == null ? null : (Bitmap) this.zzfqz.get(zzb);
    }

    private final void zza(zza zza) {
        com.google.android.gms.common.internal.zzc.zzfx("ImageManager.loadImage() must be called in the main thread");
        new zzc(this, zza).run();
    }

    public final void loadImage(ImageView imageView, int i) {
        zza(new zzc(imageView, i));
    }

    public final void loadImage(ImageView imageView, Uri uri) {
        zza(new zzc(imageView, uri));
    }

    public final void loadImage(ImageView imageView, Uri uri, int i) {
        zza zzc = new zzc(imageView, uri);
        zzc.zzfrl = i;
        zza(zzc);
    }

    public final void loadImage(OnImageLoadedListener onImageLoadedListener, Uri uri) {
        zza(new zzd(onImageLoadedListener, uri));
    }

    public final void loadImage(OnImageLoadedListener onImageLoadedListener, Uri uri, int i) {
        zza zzd = new zzd(onImageLoadedListener, uri);
        zzd.zzfrl = i;
        zza(zzd);
    }
}
