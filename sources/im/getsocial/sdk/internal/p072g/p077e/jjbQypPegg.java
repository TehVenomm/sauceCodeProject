package im.getsocial.sdk.internal.p072g.p077e;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.BitmapFactory.Options;
import im.getsocial.sdk.Callback;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p089m.EmkjBpiUfq;
import java.io.ByteArrayOutputStream;
import java.io.Closeable;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.concurrent.Executor;
import java.util.concurrent.Executors;

/* renamed from: im.getsocial.sdk.internal.g.e.jjbQypPegg */
public class jjbQypPegg implements upgqDBbsrL {
    /* renamed from: a */
    private static final Executor f1888a = Executors.newFixedThreadPool(20);

    /* renamed from: a */
    static /* synthetic */ int m1905a(jjbQypPegg jjbqyppegg, Options options, int i, int i2) {
        int i3 = 1;
        int i4 = options.outWidth;
        int i5 = options.outHeight;
        if (!((i5 <= i2 && i4 <= i) || i == 0 || i2 == 0)) {
            i4 /= 2;
            i5 /= 2;
            while (i5 / i3 >= i2 && i4 / i3 >= i) {
                i3 <<= 1;
            }
        }
        return i3;
    }

    /* renamed from: a */
    static /* synthetic */ void m1906a(InputStream inputStream, OutputStream outputStream) {
        byte[] bArr = new byte[256];
        while (true) {
            int read = inputStream.read(bArr);
            if (-1 != read) {
                outputStream.write(bArr, 0, read);
            } else {
                return;
            }
        }
    }

    /* renamed from: a */
    public final void mo4549a(String str, int i, int i2, Callback<Bitmap> callback) {
        final String str2 = str;
        final int i3 = i;
        final int i4 = i2;
        final Callback<Bitmap> callback2 = callback;
        f1888a.execute(new Runnable(this) {
            /* renamed from: e */
            final /* synthetic */ jjbQypPegg f1887e;

            public void run() {
                Closeable inputStream;
                Closeable byteArrayOutputStream;
                Throwable th;
                Closeable closeable;
                Throwable th2;
                OutOfMemoryError e;
                Closeable closeable2 = null;
                try {
                    HttpURLConnection httpURLConnection = (HttpURLConnection) new URL(str2).openConnection();
                    httpURLConnection.setDoInput(true);
                    httpURLConnection.setUseCaches(true);
                    httpURLConnection.connect();
                    inputStream = httpURLConnection.getInputStream();
                    try {
                        byteArrayOutputStream = new ByteArrayOutputStream();
                    } catch (Throwable e2) {
                        th = e2;
                        closeable = null;
                        th2 = th;
                        try {
                            callback2.onFailure(im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(th2));
                            EmkjBpiUfq.m2100a(inputStream);
                            EmkjBpiUfq.m2100a(closeable);
                        } catch (Throwable th3) {
                            th2 = th3;
                            EmkjBpiUfq.m2100a(inputStream);
                            EmkjBpiUfq.m2100a(closeable);
                            throw th2;
                        }
                    } catch (OutOfMemoryError e3) {
                        e = e3;
                        try {
                            callback2.onFailure(new GetSocialException(103, e.getMessage()));
                            EmkjBpiUfq.m2100a(inputStream);
                            EmkjBpiUfq.m2100a(closeable2);
                        } catch (Throwable e22) {
                            th = e22;
                            closeable = closeable2;
                            th2 = th;
                            EmkjBpiUfq.m2100a(inputStream);
                            EmkjBpiUfq.m2100a(closeable);
                            throw th2;
                        }
                    } catch (Throwable e222) {
                        th2 = e222;
                        closeable = null;
                        EmkjBpiUfq.m2100a(inputStream);
                        EmkjBpiUfq.m2100a(closeable);
                        throw th2;
                    }
                    try {
                        jjbQypPegg.m1906a(inputStream, byteArrayOutputStream);
                        byte[] toByteArray = byteArrayOutputStream.toByteArray();
                        Options options = new Options();
                        options.inJustDecodeBounds = true;
                        options.inPreferredConfig = null;
                        BitmapFactory.decodeByteArray(toByteArray, 0, toByteArray.length, options);
                        options.inSampleSize = jjbQypPegg.m1905a(this.f1887e, options, i3, i4);
                        options.inJustDecodeBounds = false;
                        Bitmap decodeByteArray = BitmapFactory.decodeByteArray(toByteArray, 0, toByteArray.length, options);
                        if (decodeByteArray == null) {
                            callback2.onFailure(im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(new Exception("Failed to create image")));
                        } else {
                            callback2.onSuccess(decodeByteArray);
                        }
                        EmkjBpiUfq.m2100a(inputStream);
                        EmkjBpiUfq.m2100a(byteArrayOutputStream);
                    } catch (Throwable e2222) {
                        th2 = e2222;
                        closeable = byteArrayOutputStream;
                        callback2.onFailure(im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(th2));
                        EmkjBpiUfq.m2100a(inputStream);
                        EmkjBpiUfq.m2100a(closeable);
                    } catch (OutOfMemoryError e4) {
                        e = e4;
                        closeable2 = byteArrayOutputStream;
                        callback2.onFailure(new GetSocialException(103, e.getMessage()));
                        EmkjBpiUfq.m2100a(inputStream);
                        EmkjBpiUfq.m2100a(closeable2);
                    } catch (Throwable th4) {
                        th2 = th4;
                        closeable = byteArrayOutputStream;
                        EmkjBpiUfq.m2100a(inputStream);
                        EmkjBpiUfq.m2100a(closeable);
                        throw th2;
                    }
                } catch (Throwable e22222) {
                    inputStream = null;
                    th = e22222;
                    closeable = null;
                    th2 = th;
                    callback2.onFailure(im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(th2));
                    EmkjBpiUfq.m2100a(inputStream);
                    EmkjBpiUfq.m2100a(closeable);
                } catch (OutOfMemoryError e5) {
                    e = e5;
                    inputStream = null;
                    callback2.onFailure(new GetSocialException(103, e.getMessage()));
                    EmkjBpiUfq.m2100a(inputStream);
                    EmkjBpiUfq.m2100a(closeable2);
                } catch (Throwable e222222) {
                    inputStream = null;
                    th = e222222;
                    closeable = null;
                    th2 = th;
                    EmkjBpiUfq.m2100a(inputStream);
                    EmkjBpiUfq.m2100a(closeable);
                    throw th2;
                }
            }
        });
    }
}
