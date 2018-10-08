package im.getsocial.sdk.internal.p072g.p077e;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.BitmapFactory.Options;
import im.getsocial.sdk.Callback;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p089m.EmkjBpiUfq;
import java.io.ByteArrayOutputStream;
import java.io.Closeable;
import java.io.IOException;
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
                Throwable e;
                OutOfMemoryError e2;
                Closeable closeable = null;
                try {
                    HttpURLConnection httpURLConnection = (HttpURLConnection) new URL(str2).openConnection();
                    httpURLConnection.setDoInput(true);
                    httpURLConnection.setUseCaches(true);
                    httpURLConnection.connect();
                    inputStream = httpURLConnection.getInputStream();
                    try {
                        byteArrayOutputStream = new ByteArrayOutputStream();
                    } catch (IOException e3) {
                        e = e3;
                        try {
                            callback2.onFailure(im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(e));
                            EmkjBpiUfq.m2100a(inputStream);
                            EmkjBpiUfq.m2100a(closeable);
                        } catch (Throwable th) {
                            e = th;
                            EmkjBpiUfq.m2100a(inputStream);
                            EmkjBpiUfq.m2100a(closeable);
                            throw e;
                        }
                    } catch (OutOfMemoryError e4) {
                        e2 = e4;
                        callback2.onFailure(new GetSocialException(103, e2.getMessage()));
                        EmkjBpiUfq.m2100a(inputStream);
                        EmkjBpiUfq.m2100a(closeable);
                    } catch (Throwable th2) {
                        e = th2;
                        EmkjBpiUfq.m2100a(inputStream);
                        EmkjBpiUfq.m2100a(closeable);
                        throw e;
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
                    } catch (IOException e5) {
                        e = e5;
                        closeable = byteArrayOutputStream;
                        callback2.onFailure(im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(e));
                        EmkjBpiUfq.m2100a(inputStream);
                        EmkjBpiUfq.m2100a(closeable);
                    } catch (OutOfMemoryError e6) {
                        e2 = e6;
                        closeable = byteArrayOutputStream;
                        callback2.onFailure(new GetSocialException(103, e2.getMessage()));
                        EmkjBpiUfq.m2100a(inputStream);
                        EmkjBpiUfq.m2100a(closeable);
                    } catch (Throwable th3) {
                        e = th3;
                        closeable = byteArrayOutputStream;
                        EmkjBpiUfq.m2100a(inputStream);
                        EmkjBpiUfq.m2100a(closeable);
                        throw e;
                    }
                } catch (IOException e7) {
                    e = e7;
                    inputStream = null;
                    callback2.onFailure(im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(e));
                    EmkjBpiUfq.m2100a(inputStream);
                    EmkjBpiUfq.m2100a(closeable);
                } catch (OutOfMemoryError e8) {
                    e2 = e8;
                    inputStream = null;
                    callback2.onFailure(new GetSocialException(103, e2.getMessage()));
                    EmkjBpiUfq.m2100a(inputStream);
                    EmkjBpiUfq.m2100a(closeable);
                } catch (Throwable th4) {
                    e = th4;
                    inputStream = null;
                    EmkjBpiUfq.m2100a(inputStream);
                    EmkjBpiUfq.m2100a(closeable);
                    throw e;
                }
            }
        });
    }
}
