package im.getsocial.sdk.ui.internal.p125h;

import android.graphics.Bitmap;
import android.graphics.Bitmap.Config;
import android.graphics.BitmapFactory;
import android.graphics.BitmapFactory.Options;
import android.graphics.drawable.ColorDrawable;
import android.graphics.drawable.Drawable;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg;
import java.io.BufferedInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;

/* renamed from: im.getsocial.sdk.ui.internal.h.upgqDBbsrL */
public final class upgqDBbsrL {
    /* renamed from: a */
    public static final Bitmap f3000a;
    /* renamed from: b */
    public static final Drawable f3001b = new ColorDrawable(0);
    /* renamed from: c */
    private static final cjrhisSQCL f3002c = im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL.m1274a(upgqDBbsrL.class);

    static {
        Bitmap createBitmap = Bitmap.createBitmap(1, 1, Config.ARGB_8888);
        createBitmap.setPixel(0, 0, 0);
        f3000a = createBitmap;
    }

    private upgqDBbsrL() {
    }

    /* renamed from: a */
    public static Bitmap m3365a(InputStream inputStream) {
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) inputStream), "Can not get bitmap for null input stream");
        InputStream bufferedInputStream = new BufferedInputStream(inputStream);
        try {
            Bitmap decodeStream = BitmapFactory.decodeStream(bufferedInputStream, null, new Options());
            try {
                bufferedInputStream.close();
            } catch (IOException e) {
                f3002c.mo4396d("Failed to close input stream, error: " + e.getMessage());
            }
            return decodeStream;
        } catch (Throwable e2) {
            FileNotFoundException fileNotFoundException = new FileNotFoundException("Failed to load bitmap, error: " + e2.getMessage());
            fileNotFoundException.initCause(e2);
            throw fileNotFoundException;
        } catch (Throwable th) {
            try {
                bufferedInputStream.close();
            } catch (IOException e3) {
                f3002c.mo4396d("Failed to close input stream, error: " + e3.getMessage());
            }
        }
    }
}
