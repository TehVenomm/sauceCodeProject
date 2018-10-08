package im.getsocial.sdk.internal.p089m;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Bitmap.CompressFormat;
import android.net.Uri;
import android.os.AsyncTask;
import com.appsflyer.share.Constants;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import java.io.ByteArrayOutputStream;
import java.io.Closeable;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;

/* renamed from: im.getsocial.sdk.internal.m.EmkjBpiUfq */
public class EmkjBpiUfq {
    /* renamed from: a */
    private static final cjrhisSQCL f2208a = upgqDBbsrL.m1274a(EmkjBpiUfq.class);

    /* renamed from: im.getsocial.sdk.internal.m.EmkjBpiUfq$cjrhisSQCL */
    static class cjrhisSQCL extends AsyncTask<Object, Integer, String> {
        /* renamed from: a */
        private final upgqDBbsrL f2206a;

        cjrhisSQCL(upgqDBbsrL upgqdbbsrl) {
            this.f2206a = upgqdbbsrl;
        }

        protected /* synthetic */ Object doInBackground(Object[] objArr) {
            String str = (String) objArr[1];
            File file = new File(((Context) objArr[0]).getCacheDir(), "af-video-" + str.substring(str.lastIndexOf(Constants.URL_PATH_DELIMITER) + 1) + ".mp4");
            return (file.exists() || EmkjBpiUfq.m2106b(file, str)) ? "file://" + file.getAbsolutePath() : null;
        }

        protected /* synthetic */ void onPostExecute(Object obj) {
            this.f2206a.mo4569a((String) obj);
        }
    }

    /* renamed from: im.getsocial.sdk.internal.m.EmkjBpiUfq$jjbQypPegg */
    static class jjbQypPegg extends AsyncTask<Object, Integer, String> {
        /* renamed from: a */
        private final upgqDBbsrL f2207a;

        jjbQypPegg(upgqDBbsrL upgqdbbsrl) {
            this.f2207a = upgqdbbsrl;
        }

        protected /* synthetic */ Object doInBackground(Object[] objArr) {
            Context context = (Context) objArr[0];
            if (KSZKMmRWhZ.m2108a(context) == null) {
                return null;
            }
            String str;
            String str2;
            String str3 = (String) objArr[1];
            if (str3.endsWith("gif")) {
                str = "getsocial-smartinvite-tempgif.gif";
                str2 = "smart-invite-gif.gif";
            } else if (!str3.endsWith("mp4")) {
                return null;
            } else {
                str = "getsocial-smartinvite-tempvideo.mp4";
                str2 = "smart-invite-video.mp4";
            }
            File file = new File(context.getCacheDir(), str);
            if (file.exists() && !file.delete()) {
                EmkjBpiUfq.f2208a.mo4387a("Failed to delete file " + file.getPath());
                return null;
            } else if (!EmkjBpiUfq.m2106b(file, str3)) {
                return null;
            } else {
                return String.format("content://%s/%s", new Object[]{r5, str2});
            }
        }

        protected /* synthetic */ void onPostExecute(Object obj) {
            this.f2207a.mo4569a((String) obj);
        }
    }

    /* renamed from: im.getsocial.sdk.internal.m.EmkjBpiUfq$upgqDBbsrL */
    public interface upgqDBbsrL {
        /* renamed from: a */
        void mo4569a(String str);
    }

    private EmkjBpiUfq() {
    }

    /* renamed from: a */
    public static String m2097a(Context context, Bitmap bitmap) {
        bitmap.compress(CompressFormat.JPEG, 100, new ByteArrayOutputStream());
        if (!EmkjBpiUfq.m2105b(context, bitmap) || KSZKMmRWhZ.m2108a(context) == null) {
            return null;
        }
        return String.format("content://%s/%s", new Object[]{KSZKMmRWhZ.m2108a(context), "smart-invite.jpg"});
    }

    /* renamed from: a */
    public static String m2098a(String str) {
        if (str != null) {
            try {
                return URLEncoder.encode(str, "UTF-8").replace("-", "%2D");
            } catch (Throwable e) {
                f2208a.mo4395c(e);
            }
        }
        return "";
    }

    /* renamed from: a */
    public static void m2099a(Context context, String str, upgqDBbsrL upgqdbbsrl) {
        try {
            new jjbQypPegg(upgqdbbsrl).execute(new Object[]{context, str});
        } catch (Exception e) {
            f2208a.mo4393c("Could not download contents of url, returning null. error: " + e.getMessage());
        }
    }

    /* renamed from: a */
    public static void m2100a(Closeable closeable) {
        if (closeable != null) {
            try {
                closeable.close();
            } catch (IOException e) {
                f2208a.mo4387a("Failed to close the stream: " + e.getMessage());
            }
        }
    }

    /* renamed from: a */
    public static byte[] m2102a(Bitmap bitmap) {
        if (bitmap == null) {
            return null;
        }
        OutputStream byteArrayOutputStream = new ByteArrayOutputStream();
        bitmap.compress(CompressFormat.PNG, 100, byteArrayOutputStream);
        return byteArrayOutputStream.toByteArray();
    }

    /* renamed from: b */
    public static Uri m2103b(String str) {
        return str == null ? null : Uri.parse(str);
    }

    /* renamed from: b */
    public static void m2104b(Context context, String str, upgqDBbsrL upgqdbbsrl) {
        try {
            new cjrhisSQCL(upgqdbbsrl).execute(new Object[]{context, str});
        } catch (Exception e) {
            f2208a.mo4393c("Could not download contents of url, returning null. error: " + e.getMessage());
        }
    }

    /* renamed from: b */
    private static boolean m2105b(Context context, Bitmap bitmap) {
        Closeable fileOutputStream;
        IOException e;
        Throwable th;
        File file = new File(context.getCacheDir(), "getsocial-smartinvite-tempimage.jpg");
        if (!file.exists() || file.delete()) {
            try {
                if (file.createNewFile()) {
                    fileOutputStream = new FileOutputStream(file);
                    try {
                        bitmap.compress(CompressFormat.JPEG, 100, fileOutputStream);
                        EmkjBpiUfq.m2100a(fileOutputStream);
                        return true;
                    } catch (IOException e2) {
                        e = e2;
                        try {
                            f2208a.mo4393c("Could not save image to the cache directory, returning null. error: " + e.getMessage());
                            EmkjBpiUfq.m2100a(fileOutputStream);
                            return false;
                        } catch (Throwable th2) {
                            th = th2;
                            EmkjBpiUfq.m2100a(fileOutputStream);
                            throw th;
                        }
                    } catch (Throwable th3) {
                        th = th3;
                        EmkjBpiUfq.m2100a(fileOutputStream);
                        throw th;
                    }
                }
                f2208a.mo4387a("Couldn't create the new file " + file.getPath());
                EmkjBpiUfq.m2100a(null);
                return false;
            } catch (IOException e3) {
                e = e3;
                fileOutputStream = null;
                f2208a.mo4393c("Could not save image to the cache directory, returning null. error: " + e.getMessage());
                EmkjBpiUfq.m2100a(fileOutputStream);
                return false;
            } catch (Throwable th4) {
                th = th4;
                fileOutputStream = null;
                EmkjBpiUfq.m2100a(fileOutputStream);
                throw th;
            }
        }
        f2208a.mo4387a("Couldn't delete the old file " + file.getPath());
        return false;
    }

    /* renamed from: b */
    private static boolean m2106b(File file, String str) {
        IOException e;
        Throwable th;
        Closeable closeable = null;
        Closeable inputStream;
        try {
            HttpURLConnection httpURLConnection = (HttpURLConnection) new URL(str).openConnection();
            httpURLConnection.setDoInput(true);
            httpURLConnection.setUseCaches(true);
            httpURLConnection.connect();
            inputStream = httpURLConnection.getInputStream();
            try {
                if (file.createNewFile()) {
                    Closeable fileOutputStream = new FileOutputStream(file);
                    try {
                        byte[] bArr = new byte[256];
                        while (true) {
                            int read = inputStream.read(bArr);
                            if (-1 != read) {
                                fileOutputStream.write(bArr, 0, read);
                            } else {
                                fileOutputStream.close();
                                EmkjBpiUfq.m2100a(fileOutputStream);
                                EmkjBpiUfq.m2100a(inputStream);
                                return true;
                            }
                        }
                    } catch (IOException e2) {
                        e = e2;
                        closeable = fileOutputStream;
                        try {
                            f2208a.mo4393c("Could not save url content to the cache directory, returning null. error: " + e.getMessage());
                            EmkjBpiUfq.m2100a(closeable);
                            EmkjBpiUfq.m2100a(inputStream);
                            return false;
                        } catch (Throwable th2) {
                            th = th2;
                            EmkjBpiUfq.m2100a(closeable);
                            EmkjBpiUfq.m2100a(inputStream);
                            throw th;
                        }
                    } catch (Throwable th3) {
                        th = th3;
                        closeable = fileOutputStream;
                        EmkjBpiUfq.m2100a(closeable);
                        EmkjBpiUfq.m2100a(inputStream);
                        throw th;
                    }
                }
                f2208a.mo4387a("Couldn't create new file " + file.getPath());
                EmkjBpiUfq.m2100a(null);
                EmkjBpiUfq.m2100a(inputStream);
                return false;
            } catch (IOException e3) {
                e = e3;
                f2208a.mo4393c("Could not save url content to the cache directory, returning null. error: " + e.getMessage());
                EmkjBpiUfq.m2100a(closeable);
                EmkjBpiUfq.m2100a(inputStream);
                return false;
            } catch (Throwable th4) {
                th = th4;
                EmkjBpiUfq.m2100a(closeable);
                EmkjBpiUfq.m2100a(inputStream);
                throw th;
            }
        } catch (IOException e4) {
            e = e4;
            inputStream = null;
            f2208a.mo4393c("Could not save url content to the cache directory, returning null. error: " + e.getMessage());
            EmkjBpiUfq.m2100a(closeable);
            EmkjBpiUfq.m2100a(inputStream);
            return false;
        } catch (Throwable th5) {
            th = th5;
            inputStream = null;
            EmkjBpiUfq.m2100a(closeable);
            EmkjBpiUfq.m2100a(inputStream);
            throw th;
        }
    }
}
