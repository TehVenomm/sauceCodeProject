package android.support.p000v4.graphics;

import android.content.Context;
import android.content.res.Resources;
import android.os.Process;
import android.support.annotation.RequiresApi;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import android.util.Log;
import java.io.Closeable;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.nio.ByteBuffer;
import java.nio.MappedByteBuffer;
import java.nio.channels.FileChannel;
import java.nio.channels.FileChannel.MapMode;

@RestrictTo({Scope.LIBRARY_GROUP})
/* renamed from: android.support.v4.graphics.TypefaceCompatUtil */
public class TypefaceCompatUtil {
    private static final String CACHE_FILE_PREFIX = ".font";
    private static final String TAG = "TypefaceCompatUtil";

    private TypefaceCompatUtil() {
    }

    public static void closeQuietly(Closeable closeable) {
        if (closeable != null) {
            try {
                closeable.close();
            } catch (IOException e) {
            }
        }
    }

    @RequiresApi(19)
    public static ByteBuffer copyToDirectBuffer(Context context, Resources resources, int i) {
        ByteBuffer byteBuffer = null;
        File tempFile = getTempFile(context);
        if (tempFile != null) {
            try {
                if (copyToFile(tempFile, resources, i)) {
                    byteBuffer = mmap(tempFile);
                    tempFile.delete();
                }
            } finally {
                tempFile.delete();
            }
        }
        return byteBuffer;
    }

    public static boolean copyToFile(File file, Resources resources, int i) {
        InputStream inputStream = null;
        try {
            inputStream = resources.openRawResource(i);
            return copyToFile(file, inputStream);
        } finally {
            closeQuietly(inputStream);
        }
    }

    public static boolean copyToFile(File file, InputStream inputStream) {
        IOException e;
        Throwable th;
        FileOutputStream fileOutputStream;
        FileOutputStream fileOutputStream2 = null;
        try {
            FileOutputStream fileOutputStream3 = new FileOutputStream(file, false);
            try {
                byte[] bArr = new byte[1024];
                while (true) {
                    int read = inputStream.read(bArr);
                    if (read != -1) {
                        fileOutputStream3.write(bArr, 0, read);
                    } else {
                        closeQuietly(fileOutputStream3);
                        return true;
                    }
                }
            } catch (IOException e2) {
                e = e2;
                fileOutputStream2 = fileOutputStream3;
                try {
                    Log.e(TAG, "Error copying resource contents to temp file: " + e.getMessage());
                    closeQuietly(fileOutputStream2);
                    return false;
                } catch (Throwable th2) {
                    th = th2;
                    fileOutputStream = fileOutputStream2;
                    th = th;
                    closeQuietly(fileOutputStream);
                    throw th;
                }
            } catch (Throwable th3) {
                th = th3;
                fileOutputStream = fileOutputStream3;
                closeQuietly(fileOutputStream);
                throw th;
            }
        } catch (IOException e3) {
            e = e3;
            Log.e(TAG, "Error copying resource contents to temp file: " + e.getMessage());
            closeQuietly(fileOutputStream2);
            return false;
        } catch (Throwable th4) {
            th = th4;
            fileOutputStream = null;
            th = th;
            closeQuietly(fileOutputStream);
            throw th;
        }
    }

    public static File getTempFile(Context context) {
        String str = CACHE_FILE_PREFIX + Process.myPid() + "-" + Process.myTid() + "-";
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 >= 100) {
                return null;
            }
            File file = new File(context.getCacheDir(), str + i2);
            try {
                if (file.createNewFile()) {
                    return file;
                }
                i = i2 + 1;
            } catch (IOException e) {
            }
        }
    }

    /* JADX WARNING: Code restructure failed: missing block: B:16:0x0031, code lost:
        r1 = th;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:17:0x0032, code lost:
        r2 = r0;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:26:0x003e, code lost:
        r0 = move-exception;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:27:0x003f, code lost:
        r1 = r0;
        r2 = null;
     */
    /* JADX WARNING: Failed to process nested try/catch */
    /* JADX WARNING: Removed duplicated region for block: B:26:0x003e A[ExcHandler: all (r0v3 'th' java.lang.Throwable A[CUSTOM_DECLARE]), Splitter:B:3:0x000b] */
    @android.support.annotation.RequiresApi(19)
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static java.nio.ByteBuffer mmap(android.content.Context r9, android.os.CancellationSignal r10, android.net.Uri r11) {
        /*
            r6 = 0
            android.content.ContentResolver r0 = r9.getContentResolver()
            java.lang.String r1 = "r"
            android.os.ParcelFileDescriptor r7 = r0.openFileDescriptor(r11, r1, r10)     // Catch:{ IOException -> 0x003b }
            java.io.FileInputStream r8 = new java.io.FileInputStream     // Catch:{ Throwable -> 0x002f, all -> 0x003e }
            java.io.FileDescriptor r0 = r7.getFileDescriptor()     // Catch:{ Throwable -> 0x002f, all -> 0x003e }
            r8.<init>(r0)     // Catch:{ Throwable -> 0x002f, all -> 0x003e }
            java.nio.channels.FileChannel r0 = r8.getChannel()     // Catch:{ Throwable -> 0x0042, all -> 0x0060 }
            long r4 = r0.size()     // Catch:{ Throwable -> 0x0042, all -> 0x0060 }
            java.nio.channels.FileChannel$MapMode r1 = java.nio.channels.FileChannel.MapMode.READ_ONLY     // Catch:{ Throwable -> 0x0042, all -> 0x0060 }
            r2 = 0
            java.nio.MappedByteBuffer r0 = r0.map(r1, r2, r4)     // Catch:{ Throwable -> 0x0042, all -> 0x0060 }
            if (r8 == 0) goto L_0x0029
            r8.close()     // Catch:{ Throwable -> 0x002f, all -> 0x003e }
        L_0x0029:
            if (r7 == 0) goto L_0x002e
            r7.close()     // Catch:{ IOException -> 0x003b }
        L_0x002e:
            return r0
        L_0x002f:
            r0 = move-exception
            throw r0     // Catch:{ all -> 0x0031 }
        L_0x0031:
            r1 = move-exception
            r2 = r0
        L_0x0033:
            if (r7 == 0) goto L_0x003a
            if (r2 == 0) goto L_0x005c
            r7.close()     // Catch:{ Throwable -> 0x0057 }
        L_0x003a:
            throw r1     // Catch:{ IOException -> 0x003b }
        L_0x003b:
            r0 = move-exception
            r0 = r6
            goto L_0x002e
        L_0x003e:
            r0 = move-exception
            r1 = r0
            r2 = r6
            goto L_0x0033
        L_0x0042:
            r0 = move-exception
            throw r0     // Catch:{ all -> 0x0044 }
        L_0x0044:
            r1 = move-exception
            r2 = r0
        L_0x0046:
            if (r8 == 0) goto L_0x004d
            if (r2 == 0) goto L_0x0053
            r8.close()     // Catch:{ Throwable -> 0x004e, all -> 0x003e }
        L_0x004d:
            throw r1     // Catch:{ Throwable -> 0x002f, all -> 0x003e }
        L_0x004e:
            r0 = move-exception
            r2.addSuppressed(r0)     // Catch:{ Throwable -> 0x002f, all -> 0x003e }
            goto L_0x004d
        L_0x0053:
            r8.close()     // Catch:{ Throwable -> 0x002f, all -> 0x003e }
            goto L_0x004d
        L_0x0057:
            r0 = move-exception
            r2.addSuppressed(r0)     // Catch:{ IOException -> 0x003b }
            goto L_0x003a
        L_0x005c:
            r7.close()     // Catch:{ IOException -> 0x003b }
            goto L_0x003a
        L_0x0060:
            r0 = move-exception
            r1 = r0
            r2 = r6
            goto L_0x0046
        */
        throw new UnsupportedOperationException("Method not decompiled: android.support.p000v4.graphics.TypefaceCompatUtil.mmap(android.content.Context, android.os.CancellationSignal, android.net.Uri):java.nio.ByteBuffer");
    }

    @RequiresApi(19)
    private static ByteBuffer mmap(File file) {
        Throwable th;
        Throwable th2;
        try {
            FileInputStream fileInputStream = new FileInputStream(file);
            try {
                FileChannel channel = fileInputStream.getChannel();
                MappedByteBuffer map = channel.map(MapMode.READ_ONLY, 0, channel.size());
                if (fileInputStream == null) {
                    return map;
                }
                fileInputStream.close();
                return map;
            } catch (Throwable th3) {
                th = th3;
                th2 = r0;
            }
            if (fileInputStream != null) {
                if (th2 != null) {
                    try {
                        fileInputStream.close();
                    } catch (Throwable th4) {
                        th2.addSuppressed(th4);
                    }
                } else {
                    fileInputStream.close();
                }
            }
            throw th;
            throw th;
        } catch (IOException e) {
            return null;
        }
    }
}
