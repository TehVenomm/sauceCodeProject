package android.support.p000v4.graphics;

import android.os.ParcelFileDescriptor;
import android.support.annotation.RequiresApi;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import android.system.ErrnoException;
import android.system.Os;
import android.system.OsConstants;
import java.io.File;

@RequiresApi(21)
@RestrictTo({Scope.LIBRARY_GROUP})
/* renamed from: android.support.v4.graphics.TypefaceCompatApi21Impl */
class TypefaceCompatApi21Impl extends TypefaceCompatBaseImpl {
    private static final String TAG = "TypefaceCompatApi21Impl";

    TypefaceCompatApi21Impl() {
    }

    private File getFile(ParcelFileDescriptor parcelFileDescriptor) {
        try {
            String readlink = Os.readlink("/proc/self/fd/" + parcelFileDescriptor.getFd());
            if (OsConstants.S_ISREG(Os.stat(readlink).st_mode)) {
                return new File(readlink);
            }
            return null;
        } catch (ErrnoException e) {
            return null;
        }
    }

    /* JADX WARNING: Code restructure failed: missing block: B:26:0x0043, code lost:
        if (r2 != null) goto L_0x0045;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:28:?, code lost:
        r4.close();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:32:0x004b, code lost:
        r1 = move-exception;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:33:0x004c, code lost:
        r2 = null;
        r3 = r1;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:44:0x005a, code lost:
        r3 = move-exception;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:45:0x005b, code lost:
        r2.addSuppressed(r3);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:51:0x006e, code lost:
        r1 = move-exception;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:52:0x006f, code lost:
        r2.addSuppressed(r1);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:53:0x0073, code lost:
        r4.close();
     */
    /* JADX WARNING: Failed to process nested try/catch */
    /* JADX WARNING: Removed duplicated region for block: B:26:0x0043  */
    /* JADX WARNING: Removed duplicated region for block: B:32:0x004b A[ExcHandler: all (r1v4 'th' java.lang.Throwable A[CUSTOM_DECLARE]), Splitter:B:5:0x0018] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public android.graphics.Typeface createFromFontInfo(android.content.Context r6, android.os.CancellationSignal r7, @android.support.annotation.NonNull android.support.p000v4.provider.FontsContractCompat.FontInfo[] r8, int r9) {
        /*
            r5 = this;
            r0 = 0
            int r1 = r8.length
            r2 = 1
            if (r1 >= r2) goto L_0x0006
        L_0x0005:
            return r0
        L_0x0006:
            android.support.v4.provider.FontsContractCompat$FontInfo r1 = r5.findBestInfo(r8, r9)
            android.content.ContentResolver r2 = r6.getContentResolver()
            android.net.Uri r1 = r1.getUri()     // Catch:{ IOException -> 0x0049 }
            java.lang.String r3 = "r"
            android.os.ParcelFileDescriptor r4 = r2.openFileDescriptor(r1, r3, r7)     // Catch:{ IOException -> 0x0049 }
            java.io.File r1 = r5.getFile(r4)     // Catch:{ Throwable -> 0x003d, all -> 0x004b }
            if (r1 == 0) goto L_0x0024
            boolean r2 = r1.canRead()     // Catch:{ Throwable -> 0x003d, all -> 0x004b }
            if (r2 != 0) goto L_0x0063
        L_0x0024:
            java.io.FileInputStream r3 = new java.io.FileInputStream     // Catch:{ Throwable -> 0x003d, all -> 0x004b }
            java.io.FileDescriptor r1 = r4.getFileDescriptor()     // Catch:{ Throwable -> 0x003d, all -> 0x004b }
            r3.<init>(r1)     // Catch:{ Throwable -> 0x003d, all -> 0x004b }
            android.graphics.Typeface r1 = super.createFromInputStream(r6, r3)     // Catch:{ Throwable -> 0x004f, all -> 0x0077 }
            if (r3 == 0) goto L_0x0036
            r3.close()     // Catch:{ Throwable -> 0x003d, all -> 0x004b }
        L_0x0036:
            if (r4 == 0) goto L_0x007a
            r4.close()     // Catch:{ IOException -> 0x0049 }
            r0 = r1
            goto L_0x0005
        L_0x003d:
            r1 = move-exception
            throw r1     // Catch:{ all -> 0x003f }
        L_0x003f:
            r3 = move-exception
            r2 = r1
        L_0x0041:
            if (r4 == 0) goto L_0x0048
            if (r2 == 0) goto L_0x0073
            r4.close()     // Catch:{ Throwable -> 0x006e }
        L_0x0048:
            throw r3     // Catch:{ IOException -> 0x0049 }
        L_0x0049:
            r1 = move-exception
            goto L_0x0005
        L_0x004b:
            r1 = move-exception
            r2 = r0
            r3 = r1
            goto L_0x0041
        L_0x004f:
            r2 = move-exception
            throw r2     // Catch:{ all -> 0x0051 }
        L_0x0051:
            r1 = move-exception
        L_0x0052:
            if (r3 == 0) goto L_0x0059
            if (r2 == 0) goto L_0x005f
            r3.close()     // Catch:{ Throwable -> 0x005a, all -> 0x004b }
        L_0x0059:
            throw r1     // Catch:{ Throwable -> 0x003d, all -> 0x004b }
        L_0x005a:
            r3 = move-exception
            r2.addSuppressed(r3)     // Catch:{ Throwable -> 0x003d, all -> 0x004b }
            goto L_0x0059
        L_0x005f:
            r3.close()     // Catch:{ Throwable -> 0x003d, all -> 0x004b }
            goto L_0x0059
        L_0x0063:
            android.graphics.Typeface r1 = android.graphics.Typeface.createFromFile(r1)     // Catch:{ Throwable -> 0x003d, all -> 0x004b }
            if (r4 == 0) goto L_0x007a
            r4.close()     // Catch:{ IOException -> 0x0049 }
            r0 = r1
            goto L_0x0005
        L_0x006e:
            r1 = move-exception
            r2.addSuppressed(r1)     // Catch:{ IOException -> 0x0049 }
            goto L_0x0048
        L_0x0073:
            r4.close()     // Catch:{ IOException -> 0x0049 }
            goto L_0x0048
        L_0x0077:
            r1 = move-exception
            r2 = r0
            goto L_0x0052
        L_0x007a:
            r0 = r1
            goto L_0x0005
        */
        throw new UnsupportedOperationException("Method not decompiled: android.support.p000v4.graphics.TypefaceCompatApi21Impl.createFromFontInfo(android.content.Context, android.os.CancellationSignal, android.support.v4.provider.FontsContractCompat$FontInfo[], int):android.graphics.Typeface");
    }
}
