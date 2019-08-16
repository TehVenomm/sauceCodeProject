package p017io.fabric.sdk.android.services.settings;

import android.content.Context;
import android.graphics.BitmapFactory;
import android.graphics.BitmapFactory.Options;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.services.common.CommonUtils;

/* renamed from: io.fabric.sdk.android.services.settings.IconRequest */
public class IconRequest {
    public final String hash;
    public final int height;
    public final int iconResourceId;
    public final int width;

    public IconRequest(String str, int i, int i2, int i3) {
        this.hash = str;
        this.iconResourceId = i;
        this.width = i2;
        this.height = i3;
    }

    public static IconRequest build(Context context, String str) {
        if (str == null) {
            return null;
        }
        try {
            int appIconResourceId = CommonUtils.getAppIconResourceId(context);
            Fabric.getLogger().mo20969d(Fabric.TAG, "App icon resource ID is " + appIconResourceId);
            Options options = new Options();
            options.inJustDecodeBounds = true;
            BitmapFactory.decodeResource(context.getResources(), appIconResourceId, options);
            return new IconRequest(str, appIconResourceId, options.outWidth, options.outHeight);
        } catch (Exception e) {
            Fabric.getLogger().mo20972e(Fabric.TAG, "Failed to load icon", e);
            return null;
        }
    }
}
