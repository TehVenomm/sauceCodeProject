package im.getsocial.sdk.internal.unity;

import android.graphics.Bitmap;
import android.graphics.Bitmap.CompressFormat;
import android.util.Base64;
import java.io.ByteArrayOutputStream;
import java.io.OutputStream;

public final class BitmapFactory {
    private BitmapFactory() {
    }

    public static Bitmap decodeBase64(String str) {
        if (str == null) {
            return null;
        }
        byte[] decode = Base64.decode(str, 0);
        return android.graphics.BitmapFactory.decodeByteArray(decode, 0, decode.length);
    }

    public static String encodeBase64(Bitmap bitmap) {
        if (bitmap == null) {
            return null;
        }
        OutputStream byteArrayOutputStream = new ByteArrayOutputStream();
        bitmap.compress(CompressFormat.PNG, 100, byteArrayOutputStream);
        return Base64.encodeToString(byteArrayOutputStream.toByteArray(), 0);
    }
}
