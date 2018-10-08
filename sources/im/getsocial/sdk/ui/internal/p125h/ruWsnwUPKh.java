package im.getsocial.sdk.ui.internal.p125h;

import android.graphics.Rect;
import android.view.TouchDelegate;
import android.view.View;
import im.getsocial.sdk.ui.internal.views.AssetButton;

/* renamed from: im.getsocial.sdk.ui.internal.h.ruWsnwUPKh */
public final class ruWsnwUPKh {
    private ruWsnwUPKh() {
    }

    /* renamed from: a */
    public static void m3364a(final AssetButton assetButton) {
        final View view = (View) assetButton.getParent();
        view.post(new Runnable() {
            public final void run() {
                Rect rect = new Rect();
                assetButton.getHitRect(rect);
                int width = rect.width();
                rect.left -= width;
                rect.right = width + rect.right;
                view.setTouchDelegate(new TouchDelegate(rect, assetButton));
            }
        });
    }
}
