package com.google.android.gms.games.internal;

import android.annotation.TargetApi;
import android.app.Activity;
import android.content.Context;
import android.os.IBinder;
import android.view.Display;
import android.view.View;
import android.view.View.OnAttachStateChangeListener;
import android.view.ViewTreeObserver;
import android.view.ViewTreeObserver.OnGlobalLayoutListener;
import com.google.android.gms.common.util.zzp;
import java.lang.ref.WeakReference;

@TargetApi(12)
final class zzq extends zzn implements OnAttachStateChangeListener, OnGlobalLayoutListener {
    private boolean zzhed = false;
    private WeakReference<View> zzhgx;

    protected zzq(GamesClientImpl gamesClientImpl, int i) {
        super(gamesClientImpl, i);
    }

    @TargetApi(17)
    private final void zzu(View view) {
        int i = -1;
        if (zzp.zzalf()) {
            Display display = view.getDisplay();
            if (display != null) {
                i = display.getDisplayId();
            }
        }
        IBinder windowToken = view.getWindowToken();
        int[] iArr = new int[2];
        view.getLocationInWindow(iArr);
        int width = view.getWidth();
        int height = view.getHeight();
        this.zzhgu.zzhgw = i;
        this.zzhgu.zzhgv = windowToken;
        this.zzhgu.left = iArr[0];
        this.zzhgu.top = iArr[1];
        this.zzhgu.right = iArr[0] + width;
        this.zzhgu.bottom = iArr[1] + height;
        if (this.zzhed) {
            zzaqy();
            this.zzhed = false;
        }
    }

    public final void onGlobalLayout() {
        if (this.zzhgx != null) {
            View view = (View) this.zzhgx.get();
            if (view != null) {
                zzu(view);
            }
        }
    }

    public final void onViewAttachedToWindow(View view) {
        zzu(view);
    }

    public final void onViewDetachedFromWindow(View view) {
        this.zzhgt.zzaqs();
        view.removeOnAttachStateChangeListener(this);
    }

    public final void zzaqy() {
        if (this.zzhgu.zzhgv != null) {
            super.zzaqy();
        } else {
            this.zzhed = this.zzhgx != null;
        }
    }

    protected final void zzde(int i) {
        this.zzhgu = new zzp(i, null);
    }

    @TargetApi(16)
    public final void zzt(View view) {
        View view2;
        Context context;
        this.zzhgt.zzaqs();
        if (this.zzhgx != null) {
            view2 = (View) this.zzhgx.get();
            context = this.zzhgt.getContext();
            if (view2 == null && (context instanceof Activity)) {
                view2 = ((Activity) context).getWindow().getDecorView();
            }
            if (view2 != null) {
                view2.removeOnAttachStateChangeListener(this);
                ViewTreeObserver viewTreeObserver = view2.getViewTreeObserver();
                if (zzp.zzale()) {
                    viewTreeObserver.removeOnGlobalLayoutListener(this);
                } else {
                    viewTreeObserver.removeGlobalOnLayoutListener(this);
                }
            }
        }
        this.zzhgx = null;
        context = this.zzhgt.getContext();
        if (view == null && (context instanceof Activity)) {
            view2 = ((Activity) context).findViewById(16908290);
            if (view2 == null) {
                view2 = ((Activity) context).getWindow().getDecorView();
            }
            zze.zzy("PopupManager", "You have not specified a View to use as content view for popups. Falling back to the Activity content view. Note that this may not work as expected in multi-screen environments");
            view = view2;
        }
        if (view != null) {
            zzu(view);
            this.zzhgx = new WeakReference(view);
            view.addOnAttachStateChangeListener(this);
            view.getViewTreeObserver().addOnGlobalLayoutListener(this);
            return;
        }
        zze.zzz("PopupManager", "No content view usable to display popups. Popups will not be displayed in response to this client's calls. Use setViewForPopups() to set your content view.");
    }
}
