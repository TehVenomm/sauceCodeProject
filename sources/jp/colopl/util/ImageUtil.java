package jp.colopl.util;

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
import android.provider.MediaStore.Audio;
import android.provider.MediaStore.Images.Media;
import android.text.format.DateFormat;
import com.appsflyer.share.Constants;
import com.google.android.gms.nearby.messages.Message;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.lang.reflect.Method;

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

    public static Uri addImageAsApplication(ContentResolver contentResolver, String str, long j, String str2, String str3, Bitmap bitmap, byte[] bArr) {
        Throwable th;
        String str4 = str2 + Constants.URL_PATH_DELIMITER + str3;
        OutputStream fileOutputStream;
        try {
            File file = new File(str2);
            if (!file.exists()) {
                file.mkdirs();
            }
            File file2 = new File(str2, str3);
            if (file2.createNewFile()) {
                fileOutputStream = new FileOutputStream(file2);
                if (bitmap != null) {
                    try {
                        bitmap.compress(CompressFormat.JPEG, 90, fileOutputStream);
                    } catch (FileNotFoundException e) {
                        if (fileOutputStream != null) {
                            return null;
                        }
                        try {
                            fileOutputStream.close();
                            return null;
                        } catch (Throwable th2) {
                            return null;
                        }
                    } catch (IOException e2) {
                        if (fileOutputStream != null) {
                            return null;
                        }
                        try {
                            fileOutputStream.close();
                            return null;
                        } catch (Throwable th3) {
                            return null;
                        }
                    } catch (Throwable th4) {
                        th = th4;
                        if (fileOutputStream != null) {
                            try {
                                fileOutputStream.close();
                            } catch (Throwable th5) {
                            }
                        }
                        throw th;
                    }
                }
                fileOutputStream.write(bArr);
            } else {
                fileOutputStream = null;
            }
            if (fileOutputStream != null) {
                try {
                    fileOutputStream.close();
                } catch (Throwable th6) {
                }
            }
            ContentValues contentValues = new ContentValues(7);
            contentValues.put("title", str);
            contentValues.put("_display_name", str3);
            contentValues.put("datetaken", Long.valueOf(j));
            contentValues.put("mime_type", "image/jpeg");
            contentValues.put("_data", str4);
            return contentResolver.insert(Media.EXTERNAL_CONTENT_URI, contentValues);
        } catch (FileNotFoundException e3) {
            fileOutputStream = null;
            if (fileOutputStream != null) {
                return null;
            }
            fileOutputStream.close();
            return null;
        } catch (IOException e4) {
            fileOutputStream = null;
            if (fileOutputStream != null) {
                return null;
            }
            fileOutputStream.close();
            return null;
        } catch (Throwable th7) {
            th = th7;
            fileOutputStream = null;
            if (fileOutputStream != null) {
                fileOutputStream.close();
            }
            throw th;
        }
    }

    public static File convertImageUriToFile(Context context, Uri uri) {
        Throwable th;
        Cursor cursor = null;
        try {
            Cursor query = context.getContentResolver().query(uri, new String[]{"_data", "_id", "orientation"}, null, null, null);
            try {
                int columnIndexOrThrow = query.getColumnIndexOrThrow("_data");
                int columnIndexOrThrow2 = query.getColumnIndexOrThrow("orientation");
                if (query.moveToFirst()) {
                    query.getString(columnIndexOrThrow2);
                    File file = new File(query.getString(columnIndexOrThrow));
                    if (query == null) {
                        return file;
                    }
                    query.close();
                    return file;
                }
                if (query != null) {
                    query.close();
                }
                return null;
            } catch (Throwable th2) {
                th = th2;
                cursor = query;
                if (cursor != null) {
                    cursor.close();
                }
                throw th;
            }
        } catch (Throwable th3) {
            th = th3;
            if (cursor != null) {
                cursor.close();
            }
            throw th;
        }
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
        if (isSdCardWriteable()) {
            File file2 = new File(file, str);
            if (file2.exists()) {
                try {
                    bitmap = BitmapFactory.decodeStream(new FileInputStream(file2));
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        }
        return bitmap;
    }

    public static Bitmap getBitmapFromInternalStorage(Context context, String str) {
        Bitmap bitmap = null;
        try {
            FileInputStream openFileInput = context.openFileInput(str);
            Object obj = new byte[Message.MAX_CONTENT_SIZE_BYTES];
            int read = openFileInput.read(obj);
            openFileInput.close();
            Object obj2 = new byte[read];
            System.arraycopy(obj, 0, obj2, 0, read);
            bitmap = BitmapFactory.decodeByteArray(obj2, 0, obj2.length);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return bitmap;
    }

    public static byte[] getByteFromBitmap(Bitmap bitmap) {
        OutputStream byteArrayOutputStream = new ByteArrayOutputStream();
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
            Integer num = new Integer(((Integer) cls.getDeclaredField("ORIENTATION_UNDEFINED").get(Integer.TYPE)).intValue());
            int intValue4 = ((Integer) method.invoke(newInstance, new Object[]{str2, num})).intValue();
            return intValue4 == intValue ? 90 : intValue4 == intValue2 ? 180 : intValue4 == intValue3 ? 270 : 0;
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
        int i2;
        if (width > height) {
            i2 = (int) ((((float) i) * ((float) height)) / ((float) width));
        } else {
            i2 = i;
            i = (int) ((((float) i) * ((float) width)) / ((float) height));
        }
        float f = ((float) i) / ((float) width);
        float f2 = ((float) i2) / ((float) height);
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
            decodeByteArray.compress(CompressFormat.JPEG, 90, context.getContentResolver().openOutputStream(context.getContentResolver().insert(Audio.Media.EXTERNAL_CONTENT_URI, contentValues)));
        } catch (Exception e) {
            throw e;
        }
    }

    public static void saveFileToExternalContent(Context context, String str, String str2) {
        Bitmap decodeFile = BitmapFactory.decodeFile(str);
        ContentValues contentValues = new ContentValues();
        contentValues.put("_display_name", str2);
        contentValues.put("mime_type", "image/jpeg");
        Media.insertImage(context.getContentResolver(), decodeFile, str2, null);
    }

    public static Boolean saveImageToExternelStoragy(Bitmap bitmap, File file, String str) {
        File file2 = new File(file, str);
        if (!file.exists()) {
            file.mkdirs();
        }
        try {
            OutputStream fileOutputStream = new FileOutputStream(file2.getPath());
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
