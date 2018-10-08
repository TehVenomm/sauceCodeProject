package jp.colopl.util;

import android.content.Context;
import android.graphics.Bitmap;
import android.net.Uri;
import android.provider.MediaStore.Images.Media;
import java.io.FileNotFoundException;
import java.io.IOException;

public class ContentResolverUtil {
    private static final String TAG = "ContentResolverUtil";

    public static Bitmap getBitmapFromMedia(Context context, Uri uri) {
        Bitmap bitmap = null;
        try {
            bitmap = Media.getBitmap(context.getContentResolver(), uri);
        } catch (FileNotFoundException e) {
            Util.dLog(TAG, e.toString());
        } catch (IOException e2) {
            Util.dLog(TAG, e2.toString());
        }
        return bitmap;
    }
}
