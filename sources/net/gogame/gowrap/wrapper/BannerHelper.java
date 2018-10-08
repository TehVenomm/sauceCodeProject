package net.gogame.gowrap.wrapper;

import android.app.Activity;
import android.os.Handler;
import android.util.Log;
import android.view.View;
import android.view.ViewGroup;
import android.widget.PopupWindow;
import net.gogame.gowrap.Constants;

public class BannerHelper {
    private final Activity activity;
    private final int gravity;
    private ViewGroup parentLayout;
    private PopupWindow popupWindow;
    private final View view;

    /* renamed from: net.gogame.gowrap.wrapper.BannerHelper$1 */
    class C12481 implements Runnable {
        C12481() {
        }

        public void run() {
            try {
                BannerHelper.this.popupWindow = new PopupWindow(BannerHelper.this.view, -2, -2, false);
                BannerHelper.this.popupWindow.setClippingEnabled(false);
                BannerHelper.this.popupWindow.showAtLocation(BannerHelper.this.parentLayout, BannerHelper.this.gravity, 0, 0);
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
    }

    public BannerHelper(Activity activity, int i, View view) {
        this.activity = activity;
        this.gravity = i;
        this.view = view;
    }

    public boolean show() {
        if (this.parentLayout != null && this.popupWindow != null) {
            return true;
        }
        if (this.parentLayout == null) {
            View rootView = OverlayUIHelper.getRootView(this.activity);
            if (rootView == null) {
                return false;
            }
            if (!(rootView instanceof ViewGroup)) {
                return false;
            }
            this.parentLayout = (ViewGroup) rootView;
        }
        new Handler(this.activity.getMainLooper()).post(new C12481());
        return true;
    }

    public void hide() {
        if (this.view instanceof ViewGroup) {
            ((ViewGroup) this.view).removeAllViews();
        }
        if (this.popupWindow != null) {
            this.popupWindow.dismiss();
            this.popupWindow = null;
            this.parentLayout = null;
        }
    }
}
