package net.gogame.gowrap.p019ui.utils;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.BitmapFactory.Options;
import android.graphics.drawable.BitmapDrawable;
import android.util.DisplayMetrics;
import com.github.droidfu.support.DisplaySupport;
import java.io.Closeable;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import net.gogame.gowrap.p021io.utils.IOUtils;

/* renamed from: net.gogame.gowrap.ui.utils.ImageUtils */
public final class ImageUtils {

    /* renamed from: net.gogame.gowrap.ui.utils.ImageUtils$DrawableResourceSource */
    public static class DrawableResourceSource implements Source {
        private final Context context;
        private final int resourceId;

        public DrawableResourceSource(Context context2, int i) {
            this.context = context2;
            this.resourceId = i;
        }

        public InputStream getInputStream() throws IOException {
            return this.context.getResources().openRawResource(this.resourceId);
        }

        public void close() throws IOException {
        }
    }

    /* renamed from: net.gogame.gowrap.ui.utils.ImageUtils$FileSource */
    public static class FileSource implements Source {
        private final File file;

        public FileSource(File file2) {
            this.file = file2;
        }

        public InputStream getInputStream() throws IOException {
            return new FileInputStream(this.file);
        }

        public void close() throws IOException {
        }
    }

    /* renamed from: net.gogame.gowrap.ui.utils.ImageUtils$Source */
    public interface Source extends Closeable {
        InputStream getInputStream() throws IOException;
    }

    private ImageUtils() {
    }

    /* JADX INFO: finally extract failed */
    public static Bitmap getSampledBitmap(Context context, Source source, Integer num, Integer num2) throws IOException {
        Bitmap bitmap = null;
        if (num == null || num2 == null) {
            InputStream inputStream = source.getInputStream();
            if (inputStream == null) {
                IOUtils.closeQuietly(inputStream);
                source.close();
            } else {
                try {
                    bitmap = BitmapFactory.decodeStream(inputStream);
                } finally {
                    IOUtils.closeQuietly(inputStream);
                    source.close();
                }
            }
        } else {
            Options options = new Options();
            options.inJustDecodeBounds = true;
            DisplayMetrics displayMetrics = context.getApplicationContext().getResources().getDisplayMetrics();
            options.inScreenDensity = displayMetrics.densityDpi;
            options.inTargetDensity = displayMetrics.densityDpi;
            options.inDensity = DisplaySupport.SCREEN_DENSITY_MEDIUM;
            InputStream inputStream2 = source.getInputStream();
            if (inputStream2 == null) {
                IOUtils.closeQuietly(inputStream2);
                source.close();
            } else {
                try {
                    BitmapFactory.decodeStream(inputStream2, null, options);
                    IOUtils.closeQuietly(inputStream2);
                    source.close();
                    calculateInSampleSize(options, num.intValue(), num2.intValue());
                    options.inJustDecodeBounds = false;
                    InputStream inputStream3 = source.getInputStream();
                    try {
                        bitmap = BitmapFactory.decodeStream(inputStream3, null, options);
                    } finally {
                        IOUtils.closeQuietly(inputStream3);
                        source.close();
                    }
                } catch (Throwable th) {
                    IOUtils.closeQuietly(inputStream2);
                    source.close();
                    throw th;
                }
            }
        }
        return bitmap;
    }

    public static BitmapDrawable getSampledBitmapDrawable(Context context, Source source, Integer num, Integer num2) throws IOException {
        Bitmap sampledBitmap = getSampledBitmap(context, source, num, num2);
        if (sampledBitmap == null) {
            return null;
        }
        sampledBitmap.setDensity(DisplaySupport.SCREEN_DENSITY_LOW);
        return new BitmapDrawable(sampledBitmap);
    }

    private static int calculateInSampleSize(Options options, int i, int i2) {
        int i3 = options.outHeight;
        int i4 = options.outWidth;
        int i5 = 1;
        if (i3 > i2 || i4 > i) {
            int i6 = i3 / 2;
            int i7 = i4 / 2;
            while (i6 / i5 >= i2 && i7 / i5 >= i) {
                i5 *= 2;
            }
        }
        return i5;
    }
}
