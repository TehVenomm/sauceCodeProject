package net.gogame.chat;

import android.content.Context;
import android.database.Cursor;
import android.net.Uri;
import com.google.firebase.analytics.FirebaseAnalytics.Param;

public final class ContentUtils {
    private ContentUtils() {
    }

    public static String getPathFromURI(Context context, Uri uri) {
        String str = null;
        Cursor query = context.getContentResolver().query(uri, new String[]{"_data"}, null, null, null);
        if (query != null) {
            if (query.moveToFirst()) {
                str = query.getString(query.getColumnIndexOrThrow("_data"));
            }
            query.close();
        }
        return str;
    }

    public static String getFilename(Context context, Uri uri) {
        Throwable th;
        Cursor cursor = null;
        String scheme = uri.getScheme();
        if (scheme.equals("file")) {
            return uri.getLastPathSegment();
        }
        if (scheme.equals(Param.CONTENT)) {
            try {
                Cursor query = context.getContentResolver().query(uri, new String[]{"_display_name"}, null, null, null);
                if (query != null) {
                    try {
                        if (query.getCount() != 0) {
                            query.moveToFirst();
                            scheme = query.getString(query.getColumnIndexOrThrow("_display_name"));
                            if (scheme != null) {
                                if (query == null) {
                                    return scheme;
                                }
                                query.close();
                                return scheme;
                            }
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
            } catch (Throwable th3) {
                th = th3;
                if (cursor != null) {
                    cursor.close();
                }
                throw th;
            }
        }
        return null;
    }
}
