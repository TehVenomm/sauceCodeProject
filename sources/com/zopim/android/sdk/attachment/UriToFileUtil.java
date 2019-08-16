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
        Cursor cursor = null;
        String str2 = "_data";
        try {
            Cursor query = context.getContentResolver().query(uri, new String[]{"_data"}, str, strArr, null);
            if (query != null) {
                try {
                    if (query.moveToFirst()) {
                        String string = query.getString(query.getColumnIndexOrThrow("_data"));
                        if (query == null) {
                            return string;
                        }
                        query.close();
                        return string;
                    }
                } catch (Throwable th) {
                    th = th;
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
        } catch (Throwable th2) {
            th = th2;
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
        String mimeTypeFromExtension = MimeTypeMap.getSingleton().getMimeTypeFromExtension(extension);
        return mimeTypeFromExtension != null ? mimeTypeFromExtension : DEFAULT_MIMETYPE;
    }

    @android.annotation.SuppressLint({"NewApi"})
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static java.lang.String getPath(android.content.Context r7, android.net.Uri r8) {
        /*
            r4 = 1
            r1 = 0
            r2 = 0
            int r0 = android.os.Build.VERSION.SDK_INT
            r3 = 19
            if (r0 < r3) goto L_0x004a
            r0 = r4
        L_0x000a:
            if (r0 == 0) goto L_0x00d1
            boolean r0 = android.provider.DocumentsContract.isDocumentUri(r7, r8)
            if (r0 == 0) goto L_0x00d1
            boolean r0 = isExternalStorageDocument(r8)
            if (r0 == 0) goto L_0x004c
            java.lang.String r0 = android.provider.DocumentsContract.getDocumentId(r8)
            java.lang.String r3 = ":"
            java.lang.String[] r0 = r0.split(r3)
            r2 = r0[r2]
            java.lang.String r3 = "primary"
            boolean r2 = r3.equalsIgnoreCase(r2)
            if (r2 == 0) goto L_0x0049
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.io.File r2 = android.os.Environment.getExternalStorageDirectory()
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.String r2 = "/"
            java.lang.StringBuilder r1 = r1.append(r2)
            r0 = r0[r4]
            java.lang.StringBuilder r0 = r1.append(r0)
            java.lang.String r1 = r0.toString()
        L_0x0049:
            return r1
        L_0x004a:
            r0 = r2
            goto L_0x000a
        L_0x004c:
            boolean r0 = isDownloadsDocument(r8)
            if (r0 == 0) goto L_0x006d
            java.lang.String r0 = android.provider.DocumentsContract.getDocumentId(r8)
            java.lang.String r2 = "content://downloads/public_downloads"
            android.net.Uri r2 = android.net.Uri.parse(r2)
            java.lang.Long r0 = java.lang.Long.valueOf(r0)
            long r4 = r0.longValue()
            android.net.Uri r0 = android.content.ContentUris.withAppendedId(r2, r4)
            java.lang.String r1 = getDataColumn(r7, r0, r1, r1)
            goto L_0x0049
        L_0x006d:
            boolean r0 = isMediaDocument(r8)
            if (r0 == 0) goto L_0x0049
            java.lang.String r0 = android.provider.DocumentsContract.getDocumentId(r8)
            java.lang.String r3 = ":"
            java.lang.String[] r5 = r0.split(r3)
            r0 = r5[r2]
            if (r0 != 0) goto L_0x0089
            r0 = r5[r2]
            boolean r0 = r0.isEmpty()
            if (r0 != 0) goto L_0x00a7
        L_0x0089:
            r0 = r5[r2]
        L_0x008b:
            r3 = -1
            int r6 = r0.hashCode()
            switch(r6) {
                case 93166550: goto L_0x00be;
                case 100313435: goto L_0x00aa;
                case 112202875: goto L_0x00b4;
                default: goto L_0x0093;
            }
        L_0x0093:
            r0 = r3
        L_0x0094:
            switch(r0) {
                case 0: goto L_0x00c8;
                case 1: goto L_0x00cb;
                case 2: goto L_0x00ce;
                default: goto L_0x0097;
            }
        L_0x0097:
            r0 = r1
        L_0x0098:
            java.lang.String r1 = "_id=?"
            java.lang.String[] r1 = new java.lang.String[r4]
            r3 = r5[r4]
            r1[r2] = r3
            java.lang.String r2 = "_id=?"
            java.lang.String r1 = getDataColumn(r7, r0, r2, r1)
            goto L_0x0049
        L_0x00a7:
            java.lang.String r0 = ""
            goto L_0x008b
        L_0x00aa:
            java.lang.String r6 = "image"
            boolean r0 = r0.equals(r6)
            if (r0 == 0) goto L_0x0093
            r0 = r2
            goto L_0x0094
        L_0x00b4:
            java.lang.String r6 = "video"
            boolean r0 = r0.equals(r6)
            if (r0 == 0) goto L_0x0093
            r0 = r4
            goto L_0x0094
        L_0x00be:
            java.lang.String r6 = "audio"
            boolean r0 = r0.equals(r6)
            if (r0 == 0) goto L_0x0093
            r0 = 2
            goto L_0x0094
        L_0x00c8:
            android.net.Uri r0 = android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI
            goto L_0x0098
        L_0x00cb:
            android.net.Uri r0 = android.provider.MediaStore.Video.Media.EXTERNAL_CONTENT_URI
            goto L_0x0098
        L_0x00ce:
            android.net.Uri r0 = android.provider.MediaStore.Audio.Media.EXTERNAL_CONTENT_URI
            goto L_0x0098
        L_0x00d1:
            java.lang.String r0 = "content"
            java.lang.String r2 = r8.getScheme()
            boolean r0 = r0.equalsIgnoreCase(r2)
            if (r0 == 0) goto L_0x00ef
            boolean r0 = isGooglePhotosUri(r8)
            if (r0 == 0) goto L_0x00e9
            java.lang.String r1 = r8.getLastPathSegment()
            goto L_0x0049
        L_0x00e9:
            java.lang.String r1 = getDataColumn(r7, r8, r1, r1)
            goto L_0x0049
        L_0x00ef:
            java.lang.String r0 = "file"
            java.lang.String r2 = r8.getScheme()
            boolean r0 = r0.equalsIgnoreCase(r2)
            if (r0 == 0) goto L_0x0049
            java.lang.String r1 = r8.getPath()
            goto L_0x0049
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
        return str != null && !str.startsWith("http://") && !str.startsWith("https://");
    }

    public static boolean isMediaDocument(Uri uri) {
        return "com.android.providers.media.documents".equals(uri.getAuthority());
    }

    public static boolean isMediaUri(Uri uri) {
        return "media".equalsIgnoreCase(uri.getAuthority());
    }
}
