package p018jp.colopl.util;

import android.content.Context;
import android.graphics.Bitmap;
import android.net.Uri;
import android.provider.MediaStore.Images.Media;
import java.io.FileNotFoundException;
import java.io.IOException;

/* renamed from: jp.colopl.util.ContentResolverUtil */
public class ContentResolverUtil {
    private static final String TAG = "ContentResolverUtil";

    public static Bitmap getBitmapFromMedia(Context context, Uri uri) {
        boolean z = false;
        try {
            return Media.getBitmap(context.getContentResolver(), uri);
        } catch (FileNotFoundException e) {
            Util.dLog(TAG, e.toString());
            return z;
        } catch (IOException e2) {
            Util.dLog(TAG, e2.toString());
            return z;
        }
    }
}
