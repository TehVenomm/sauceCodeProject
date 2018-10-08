package net.gogame.gowrap.wrapper;

import android.app.Activity;
import android.os.Handler;
import android.util.Log;
import android.view.View;
import android.view.ViewGroup;
import android.widget.PopupWindow;
import net.gogame.gowrap.Constants;

public class OverlayHelper {
    public static final int DEFAULT_MAX_RETRIES = 4;
    public static final long DEFAULT_RETRY_MS = 500;
    private final Activity activity;
    private final Handler handler;
    private final int maxRetries;
    private PopupWindow popupWindow;
    private int retries;
    private final long retryMs;
    private final View view;

    public OverlayHelper(Activity activity, View view) {
        this(activity, view, 500, 4);
    }

    public OverlayHelper(Activity activity, View view, long j, int i) {
        this.handler = new Handler();
        this.popupWindow = null;
        this.retries = 0;
        this.activity = activity;
        this.view = view;
        this.retryMs = j;
        this.maxRetries = i;
    }

    public void show(final int i, final int i2, final int i3) {
        if (this.popupWindow == null) {
            View findViewById = this.activity.getWindow().getDecorView().findViewById(16908290);
            View leafView = getLeafView(findViewById);
            if (leafView == null) {
                this.retries++;
                if (this.retries <= this.maxRetries) {
                    this.handler.postDelayed(new Runnable() {
                        public void run() {
                            OverlayHelper.this.show(i, i2, i3);
                        }
                    }, this.retryMs);
                    return;
                }
                leafView = findViewById;
            }
            this.retries = 0;
            final ViewGroup viewGroup = (ViewGroup) leafView.getParent();
            this.popupWindow = new PopupWindow(this.view, -2, -2, false);
            this.popupWindow.setClippingEnabled(false);
            final int i4 = i;
            final int i5 = i2;
            final int i6 = i3;
            findViewById.post(new Runnable() {
                public void run() {
                    if (OverlayHelper.this.popupWindow != null) {
                        try {
                            OverlayHelper.this.popupWindow.showAtLocation(viewGroup, i4, i5, i6);
                        } catch (Throwable e) {
                            Log.e(Constants.TAG, "Exception", e);
                        }
                    }
                }
            });
        }
    }

    public void hide() {
        if (this.popupWindow != null) {
            this.popupWindow.dismiss();
            this.popupWindow = null;
        }
    }

    private View getLeafView(View view) {
        if (!(view instanceof ViewGroup)) {
            return view;
        }
        ViewGroup viewGroup = (ViewGroup) view;
        for (int i = 0; i < viewGroup.getChildCount(); i++) {
            View leafView = getLeafView(viewGroup.getChildAt(i));
            if (leafView != null) {
                return leafView;
            }
        }
        return null;
    }
}
