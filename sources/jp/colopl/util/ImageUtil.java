package p018jp.colopl.util;

import android.content.ContentResolver;
import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.Bitmap.CompressFormat;
import android.graphics.BitmapFactory;
import android.graphics.Matrix;
import android.net.Uri;
import android.os.Environment;
import android.provider.MediaStore.Audio.Media;
import android.provider.MediaStore.Images;
import android.text.format.DateFormat;
import com.google.android.gms.nearby.messages.Message;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.lang.reflect.Method;

/* renamed from: jp.colopl.util.ImageUtil */
public class ImageUtil {
    public static final int CROP_MAX_SIZE = 320;
    public static final String DATE_PATTERN = "yyyy-MM-dd_kk.mm.ss";
    public static final int OUTPUT_QUALITY = 90;
    public static final int SCALE_DOWN_SIZE = 640;
    private static final String TEMPORARY_IMAGE_FILENAME = "image.jpeg";
    public static final int THUMBNAIL_MAX_SIZE = 480;

    public static Uri addImageAsApplication(ContentResolver contentResolver, Bitmap bitmap, String str) {
        long currentTimeMillis = System.currentTimeMillis();
        String str2 = createName(currentTimeMillis) + ".jpeg";
        return addImageAsApplication(contentResolver, str2, currentTimeMillis, str, str2, bitmap, null);
    }

    /* JADX WARNING: Removed duplicated region for block: B:27:0x0084 A[SYNTHETIC, Splitter:B:27:0x0084] */
    /* JADX WARNING: Removed duplicated region for block: B:33:0x008f A[SYNTHETIC, Splitter:B:33:0x008f] */
    /* JADX WARNING: Removed duplicated region for block: B:44:? A[RETURN, SYNTHETIC] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static android.net.Uri addImageAsApplication(android.content.ContentResolver r6, java.lang.String r7, long r8, java.lang.String r10, java.lang.String r11, android.graphics.Bitmap r12, byte[] r13) {
        /*
            r0 = 0
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.lang.StringBuilder r1 = r1.append(r10)
            java.lang.String r2 = "/"
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.StringBuilder r1 = r1.append(r11)
            java.lang.String r2 = r1.toString()
            java.io.File r1 = new java.io.File     // Catch:{ FileNotFoundException -> 0x007d, IOException -> 0x0080, all -> 0x008a }
            r1.<init>(r10)     // Catch:{ FileNotFoundException -> 0x007d, IOException -> 0x0080, all -> 0x008a }
            boolean r3 = r1.exists()     // Catch:{ FileNotFoundException -> 0x007d, IOException -> 0x0080, all -> 0x008a }
            if (r3 != 0) goto L_0x0026
            r1.mkdirs()     // Catch:{ FileNotFoundException -> 0x007d, IOException -> 0x0080, all -> 0x008a }
        L_0x0026:
            java.io.File r3 = new java.io.File     // Catch:{ FileNotFoundException -> 0x007d, IOException -> 0x0080, all -> 0x008a }
            r3.<init>(r10, r11)     // Catch:{ FileNotFoundException -> 0x007d, IOException -> 0x0080, all -> 0x008a }
            boolean r1 = r3.createNewFile()     // Catch:{ FileNotFoundException -> 0x007d, IOException -> 0x0080, all -> 0x008a }
            if (r1 == 0) goto L_0x009d
            java.io.FileOutputStream r1 = new java.io.FileOutputStream     // Catch:{ FileNotFoundException -> 0x007d, IOException -> 0x0080, all -> 0x008a }
            r1.<init>(r3)     // Catch:{ FileNotFoundException -> 0x007d, IOException -> 0x0080, all -> 0x008a }
            if (r12 == 0) goto L_0x0070
            android.graphics.Bitmap$CompressFormat r3 = android.graphics.Bitmap.CompressFormat.JPEG     // Catch:{ FileNotFoundException -> 0x0074, IOException -> 0x009b, all -> 0x0093 }
            r4 = 90
            r12.compress(r3, r4, r1)     // Catch:{ FileNotFoundException -> 0x0074, IOException -> 0x009b, all -> 0x0093 }
        L_0x003f:
            if (r1 == 0) goto L_0x0044
            r1.close()     // Catch:{ Throwable -> 0x0097 }
        L_0x0044:
            android.content.ContentValues r0 = new android.content.ContentValues
            r1 = 7
            r0.<init>(r1)
            java.lang.String r1 = "title"
            r0.put(r1, r7)
            java.lang.String r1 = "_display_name"
            r0.put(r1, r11)
            java.lang.String r1 = "datetaken"
            java.lang.Long r3 = java.lang.Long.valueOf(r8)
            r0.put(r1, r3)
            java.lang.String r1 = "mime_type"
            java.lang.String r3 = "image/jpeg"
            r0.put(r1, r3)
            java.lang.String r1 = "_data"
            r0.put(r1, r2)
            android.net.Uri r1 = android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI
            android.net.Uri r0 = r6.insert(r1, r0)
        L_0x006f:
            return r0
        L_0x0070:
            r1.write(r13)     // Catch:{ FileNotFoundException -> 0x0074, IOException -> 0x009b, all -> 0x0093 }
            goto L_0x003f
        L_0x0074:
            r2 = move-exception
        L_0x0075:
            if (r1 == 0) goto L_0x006f
            r1.close()     // Catch:{ Throwable -> 0x007b }
            goto L_0x006f
        L_0x007b:
            r1 = move-exception
            goto L_0x006f
        L_0x007d:
            r1 = move-exception
            r1 = r0
            goto L_0x0075
        L_0x0080:
            r1 = move-exception
            r1 = r0
        L_0x0082:
            if (r1 == 0) goto L_0x006f
            r1.close()     // Catch:{ Throwable -> 0x0088 }
            goto L_0x006f
        L_0x0088:
            r1 = move-exception
            goto L_0x006f
        L_0x008a:
            r1 = move-exception
            r2 = r0
            r3 = r1
        L_0x008d:
            if (r2 == 0) goto L_0x0092
            r2.close()     // Catch:{ Throwable -> 0x0099 }
        L_0x0092:
            throw r3
        L_0x0093:
            r0 = move-exception
            r2 = r1
            r3 = r0
            goto L_0x008d
        L_0x0097:
            r0 = move-exception
            goto L_0x0044
        L_0x0099:
            r0 = move-exception
            goto L_0x0092
        L_0x009b:
            r2 = move-exception
            goto L_0x0082
        L_0x009d:
            r1 = r0
            goto L_0x003f
        */
        throw new UnsupportedOperationException("Method not decompiled: p018jp.colopl.util.ImageUtil.addImageAsApplication(android.content.ContentResolver, java.lang.String, long, java.lang.String, java.lang.String, android.graphics.Bitmap, byte[]):android.net.Uri");
    }

    /* JADX WARNING: Removed duplicated region for block: B:15:0x004d  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static java.io.File convertImageUriToFile(android.content.Context r7, android.net.Uri r8) {
        /*
            r6 = 0
            android.content.ContentResolver r0 = r7.getContentResolver()     // Catch:{ all -> 0x004a }
            r1 = 3
            java.lang.String[] r2 = new java.lang.String[r1]     // Catch:{ all -> 0x004a }
            r1 = 0
            java.lang.String r3 = "_data"
            r2[r1] = r3     // Catch:{ all -> 0x004a }
            r1 = 1
            java.lang.String r3 = "_id"
            r2[r1] = r3     // Catch:{ all -> 0x004a }
            r1 = 2
            java.lang.String r3 = "orientation"
            r2[r1] = r3     // Catch:{ all -> 0x004a }
            r3 = 0
            r4 = 0
            r5 = 0
            r1 = r8
            android.database.Cursor r1 = r0.query(r1, r2, r3, r4, r5)     // Catch:{ all -> 0x004a }
            java.lang.String r0 = "_data"
            int r2 = r1.getColumnIndexOrThrow(r0)     // Catch:{ all -> 0x0051 }
            java.lang.String r0 = "orientation"
            int r0 = r1.getColumnIndexOrThrow(r0)     // Catch:{ all -> 0x0051 }
            boolean r3 = r1.moveToFirst()     // Catch:{ all -> 0x0051 }
            if (r3 == 0) goto L_0x0043
            r1.getString(r0)     // Catch:{ all -> 0x0051 }
            java.io.File r0 = new java.io.File     // Catch:{ all -> 0x0051 }
            java.lang.String r2 = r1.getString(r2)     // Catch:{ all -> 0x0051 }
            r0.<init>(r2)     // Catch:{ all -> 0x0051 }
            if (r1 == 0) goto L_0x0042
            r1.close()
        L_0x0042:
            return r0
        L_0x0043:
            if (r1 == 0) goto L_0x0048
            r1.close()
        L_0x0048:
            r0 = r6
            goto L_0x0042
        L_0x004a:
            r0 = move-exception
        L_0x004b:
            if (r6 == 0) goto L_0x0050
            r6.close()
        L_0x0050:
            throw r0
        L_0x0051:
            r0 = move-exception
            r6 = r1
            goto L_0x004b
        */
        throw new UnsupportedOperationException("Method not decompiled: p018jp.colopl.util.ImageUtil.convertImageUriToFile(android.content.Context, android.net.Uri):java.io.File");
    }

    public static String convertToFilePath(Context context, Uri uri) {
        Cursor query = context.getContentResolver().query(uri, new String[]{"_data"}, null, null, null);
        if (query == null) {
            return null;
        }
        query.moveToFirst();
        return query.getString(0);
    }

    public static String createName(long j) {
        return DateFormat.format(DATE_PATTERN, j).toString();
    }

    public static String createNameFromDate() {
        return createName(System.currentTimeMillis());
    }

    public static Bitmap getBitmapFromExternalStorage(File file, String str) {
        Bitmap bitmap = null;
        if (!isSdCardWriteable()) {
            return bitmap;
        }
        File file2 = new File(file, str);
        if (!file2.exists()) {
            return bitmap;
        }
        try {
            return BitmapFactory.decodeStream(new FileInputStream(file2));
        } catch (Exception e) {
            e.printStackTrace();
            return bitmap;
        }
    }

    public static Bitmap getBitmapFromInternalStorage(Context context, String str) {
        boolean z = false;
        try {
            FileInputStream openFileInput = context.openFileInput(str);
            byte[] bArr = new byte[Message.MAX_CONTENT_SIZE_BYTES];
            int read = openFileInput.read(bArr);
            openFileInput.close();
            byte[] bArr2 = new byte[read];
            System.arraycopy(bArr, 0, bArr2, 0, read);
            return BitmapFactory.decodeByteArray(bArr2, 0, bArr2.length);
        } catch (Exception e) {
            e.printStackTrace();
            return z;
        }
    }

    public static byte[] getByteFromBitmap(Bitmap bitmap) {
        ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
        bitmap.compress(CompressFormat.PNG, 100, byteArrayOutputStream);
        return byteArrayOutputStream.toByteArray();
    }

    public static int getExifRotationDegrees(String str) {
        try {
            Class cls = Class.forName("android.media.ExifInterface");
            Object newInstance = cls.getConstructor(new Class[]{String.class}).newInstance(new Object[]{str});
            Method method = cls.getMethod("getAttributeInt", new Class[]{String.class, Integer.TYPE});
            String str2 = (String) cls.getDeclaredField("TAG_ORIENTATION").get(String.class);
            int intValue = ((Integer) cls.getDeclaredField("ORIENTATION_ROTATE_90").get(Integer.TYPE)).intValue();
            int intValue2 = ((Integer) cls.getDeclaredField("ORIENTATION_ROTATE_180").get(Integer.TYPE)).intValue();
            int intValue3 = ((Integer) cls.getDeclaredField("ORIENTATION_ROTATE_270").get(Integer.TYPE)).intValue();
            int intValue4 = ((Integer) method.invoke(newInstance, new Object[]{str2, new Integer(((Integer) cls.getDeclaredField("ORIENTATION_UNDEFINED").get(Integer.TYPE)).intValue())})).intValue();
            if (intValue4 == intValue) {
                return 90;
            }
            if (intValue4 == intValue2) {
                return 180;
            }
            return intValue4 == intValue3 ? 270 : 0;
        } catch (Exception e) {
            return 0;
        }
    }

    public static File getTempFile(Context context) {
        File file = new File(Environment.getExternalStorageDirectory(), context.getPackageName());
        if (!file.exists()) {
            file.mkdir();
        }
        return new File(file, TEMPORARY_IMAGE_FILENAME);
    }

    public static Uri getTempFileUri(Context context) {
        return Uri.fromFile(getTempFile(context));
    }

    public static boolean isSdCardReadable() {
        return Environment.getExternalStorageState().equals("mounted_ro");
    }

    public static boolean isSdCardWriteable() {
        return Environment.getExternalStorageState().equals("mounted");
    }

    public static Bitmap resizeBitmapByEdge(Bitmap bitmap, int i) {
        int i2;
        int i3;
        int width = bitmap.getWidth();
        int height = bitmap.getHeight();
        if (width == 0 || height == 0) {
            return bitmap;
        }
        if (width <= i && height <= i) {
            return bitmap;
        }
        if (i > bitmap.getWidth() || i > bitmap.getHeight()) {
            return null;
        }
        if (width > height) {
            i3 = (int) ((((float) i) * ((float) height)) / ((float) width));
            i2 = i;
        } else {
            i2 = (int) ((((float) i) * ((float) width)) / ((float) height));
            i3 = i;
        }
        float f = ((float) i2) / ((float) width);
        float f2 = ((float) i3) / ((float) height);
        Matrix matrix = new Matrix();
        matrix.postScale(f, f2);
        return Bitmap.createBitmap(bitmap, 0, 0, width, height, matrix, false);
    }

    public static void saveDataToExternalContent(Context context, byte[] bArr, String str) throws Exception {
        Bitmap decodeByteArray = BitmapFactory.decodeByteArray(bArr, 0, bArr.length);
        ContentValues contentValues = new ContentValues();
        contentValues.put("_display_name", str);
        contentValues.put("mime_type", "image/jpeg");
        try {
            decodeByteArray.compress(CompressFormat.JPEG, 90, context.getContentResolver().openOutputStream(context.getContentResolver().insert(Media.EXTERNAL_CONTENT_URI, contentValues)));
        } catch (Exception e) {
            throw e;
        }
    }

    public static void saveFileToExternalContent(Context context, String str, String str2) {
        Bitmap decodeFile = BitmapFactory.decodeFile(str);
        ContentValues contentValues = new ContentValues();
        contentValues.put("_display_name", str2);
        contentValues.put("mime_type", "image/jpeg");
        Images.Media.insertImage(context.getContentResolver(), decodeFile, str2, null);
    }

    public static Boolean saveImageToExternelStoragy(Bitmap bitmap, File file, String str) {
        File file2 = new File(file, str);
        if (!file.exists()) {
            file.mkdirs();
        }
        try {
            FileOutputStream fileOutputStream = new FileOutputStream(file2.getPath());
            bitmap.compress(CompressFormat.PNG, 100, fileOutputStream);
            fileOutputStream.flush();
            fileOutputStream.close();
            return Boolean.valueOf(true);
        } catch (Exception e) {
            e.printStackTrace();
            return Boolean.valueOf(false);
        }
    }

    public static Boolean saveImageToInternalStoragy(Context context, Bitmap bitmap, String str) {
        try {
            FileOutputStream openFileOutput = context.openFileOutput(str, 0);
            openFileOutput.write(getByteFromBitmap(bitmap));
            openFileOutput.flush();
            openFileOutput.close();
            return Boolean.valueOf(true);
        } catch (Exception e) {
            e.printStackTrace();
            return Boolean.valueOf(false);
        }
    }
}
