package com.zopim.android.sdk.attachment;

import android.content.Context;
import android.database.Cursor;
import android.net.Uri;
import android.support.annotation.NonNull;
import android.webkit.MimeTypeMap;
import java.io.File;
import java.util.Locale;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;

public class UriToFileUtil {
    private static final String DEFAULT_MIMETYPE = "application/octet-stream";

    private UriToFileUtil() {
    }

    public static String getDataColumn(Context context, Uri uri, String str, String[] strArr) {
        Throwable th;
        Cursor cursor = null;
        String str2 = "_data";
        try {
            Cursor query = context.getContentResolver().query(uri, new String[]{"_data"}, str, strArr, null);
            if (query != null) {
                try {
                    if (query.moveToFirst()) {
                        str2 = query.getString(query.getColumnIndexOrThrow("_data"));
                        if (query == null) {
                            return str2;
                        }
                        query.close();
                        return str2;
                    }
                } catch (Throwable th2) {
                    th = th2;
                    cursor = query;
                    if (cursor != null) {
                        cursor.close();
                    }
                    throw th;
                }
            }
            if (query != null) {
                query.close();
            }
            return null;
        } catch (Throwable th3) {
            th = th3;
            if (cursor != null) {
                cursor.close();
            }
            throw th;
        }
    }

    public static String getExtension(String str) {
        if (str == null) {
            return null;
        }
        int lastIndexOf = str.lastIndexOf(AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER);
        if (lastIndexOf < 0) {
            return "";
        }
        try {
            return str.substring(lastIndexOf + 1).toLowerCase(Locale.US);
        } catch (IndexOutOfBoundsException e) {
            return "";
        }
    }

    public static File getFile(Context context, Uri uri) {
        if (uri != null) {
            String path = getPath(context, uri);
            if (path != null && isLocal(path)) {
                return new File(path);
            }
        }
        return null;
    }

    @NonNull
    public static String getMimeType(File file) {
        String extension = getExtension(file.getName());
        if (extension.length() <= 0) {
            return DEFAULT_MIMETYPE;
        }
        extension = MimeTypeMap.getSingleton().getMimeTypeFromExtension(extension);
        return extension != null ? extension : DEFAULT_MIMETYPE;
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    @android.annotation.SuppressLint({"NewApi"})
    public static java.lang.String getPath(android.content.Context r7, android.net.Uri r8) {
        /*
        r4 = 1;
        r1 = 0;
        r2 = 0;
        r0 = android.os.Build.VERSION.SDK_INT;
        r3 = 19;
        if (r0 < r3) goto L_0x004a;
    L_0x0009:
        r0 = r4;
    L_0x000a:
        if (r0 == 0) goto L_0x00d1;
    L_0x000c:
        r0 = android.provider.DocumentsContract.isDocumentUri(r7, r8);
        if (r0 == 0) goto L_0x00d1;
    L_0x0012:
        r0 = isExternalStorageDocument(r8);
        if (r0 == 0) goto L_0x004c;
    L_0x0018:
        r0 = android.provider.DocumentsContract.getDocumentId(r8);
        r3 = ":";
        r0 = r0.split(r3);
        r2 = r0[r2];
        r3 = "primary";
        r2 = r3.equalsIgnoreCase(r2);
        if (r2 == 0) goto L_0x0049;
    L_0x002c:
        r1 = new java.lang.StringBuilder;
        r1.<init>();
        r2 = android.os.Environment.getExternalStorageDirectory();
        r1 = r1.append(r2);
        r2 = "/";
        r1 = r1.append(r2);
        r0 = r0[r4];
        r0 = r1.append(r0);
        r1 = r0.toString();
    L_0x0049:
        return r1;
    L_0x004a:
        r0 = r2;
        goto L_0x000a;
    L_0x004c:
        r0 = isDownloadsDocument(r8);
        if (r0 == 0) goto L_0x006d;
    L_0x0052:
        r0 = android.provider.DocumentsContract.getDocumentId(r8);
        r2 = "content://downloads/public_downloads";
        r2 = android.net.Uri.parse(r2);
        r0 = java.lang.Long.valueOf(r0);
        r4 = r0.longValue();
        r0 = android.content.ContentUris.withAppendedId(r2, r4);
        r1 = getDataColumn(r7, r0, r1, r1);
        goto L_0x0049;
    L_0x006d:
        r0 = isMediaDocument(r8);
        if (r0 == 0) goto L_0x0049;
    L_0x0073:
        r0 = android.provider.DocumentsContract.getDocumentId(r8);
        r3 = ":";
        r5 = r0.split(r3);
        r0 = r5[r2];
        if (r0 != 0) goto L_0x0089;
    L_0x0081:
        r0 = r5[r2];
        r0 = r0.isEmpty();
        if (r0 != 0) goto L_0x00a7;
    L_0x0089:
        r0 = r5[r2];
    L_0x008b:
        r3 = -1;
        r6 = r0.hashCode();
        switch(r6) {
            case 93166550: goto L_0x00be;
            case 100313435: goto L_0x00aa;
            case 112202875: goto L_0x00b4;
            default: goto L_0x0093;
        };
    L_0x0093:
        r0 = r3;
    L_0x0094:
        switch(r0) {
            case 0: goto L_0x00c8;
            case 1: goto L_0x00cb;
            case 2: goto L_0x00ce;
            default: goto L_0x0097;
        };
    L_0x0097:
        r0 = r1;
    L_0x0098:
        r1 = "_id=?";
        r1 = new java.lang.String[r4];
        r3 = r5[r4];
        r1[r2] = r3;
        r2 = "_id=?";
        r1 = getDataColumn(r7, r0, r2, r1);
        goto L_0x0049;
    L_0x00a7:
        r0 = "";
        goto L_0x008b;
    L_0x00aa:
        r6 = "image";
        r0 = r0.equals(r6);
        if (r0 == 0) goto L_0x0093;
    L_0x00b2:
        r0 = r2;
        goto L_0x0094;
    L_0x00b4:
        r6 = "video";
        r0 = r0.equals(r6);
        if (r0 == 0) goto L_0x0093;
    L_0x00bc:
        r0 = r4;
        goto L_0x0094;
    L_0x00be:
        r6 = "audio";
        r0 = r0.equals(r6);
        if (r0 == 0) goto L_0x0093;
    L_0x00c6:
        r0 = 2;
        goto L_0x0094;
    L_0x00c8:
        r0 = android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI;
        goto L_0x0098;
    L_0x00cb:
        r0 = android.provider.MediaStore.Video.Media.EXTERNAL_CONTENT_URI;
        goto L_0x0098;
    L_0x00ce:
        r0 = android.provider.MediaStore.Audio.Media.EXTERNAL_CONTENT_URI;
        goto L_0x0098;
    L_0x00d1:
        r0 = "content";
        r2 = r8.getScheme();
        r0 = r0.equalsIgnoreCase(r2);
        if (r0 == 0) goto L_0x00ef;
    L_0x00dd:
        r0 = isGooglePhotosUri(r8);
        if (r0 == 0) goto L_0x00e9;
    L_0x00e3:
        r1 = r8.getLastPathSegment();
        goto L_0x0049;
    L_0x00e9:
        r1 = getDataColumn(r7, r8, r1, r1);
        goto L_0x0049;
    L_0x00ef:
        r0 = "file";
        r2 = r8.getScheme();
        r0 = r0.equalsIgnoreCase(r2);
        if (r0 == 0) goto L_0x0049;
    L_0x00fb:
        r1 = r8.getPath();
        goto L_0x0049;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.zopim.android.sdk.attachment.UriToFileUtil.getPath(android.content.Context, android.net.Uri):java.lang.String");
    }

    public static boolean isDownloadsDocument(Uri uri) {
        return "com.android.providers.downloads.documents".equals(uri.getAuthority());
    }

    public static boolean isExternalStorageDocument(Uri uri) {
        return "com.android.externalstorage.documents".equals(uri.getAuthority());
    }

    public static boolean isGooglePhotosUri(Uri uri) {
        return "com.google.android.apps.photos.content".equals(uri.getAuthority());
    }

    public static boolean isLocal(String str) {
        return (str == null || str.startsWith("http://") || str.startsWith("https://")) ? false : true;
    }

    public static boolean isMediaDocument(Uri uri) {
        return "com.android.providers.media.documents".equals(uri.getAuthority());
    }

    public static boolean isMediaUri(Uri uri) {
        return "media".equalsIgnoreCase(uri.getAuthority());
    }
}
