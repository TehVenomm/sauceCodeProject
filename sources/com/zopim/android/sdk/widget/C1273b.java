package com.zopim.android.sdk.widget;

import android.support.p000v4.view.ViewCompat;
import com.zopim.android.sdk.api.Logger;
import java.util.Timer;

/* renamed from: com.zopim.android.sdk.widget.b */
class C1273b implements Runnable {

    /* renamed from: a */
    final /* synthetic */ int f974a;

    /* renamed from: b */
    final /* synthetic */ int f975b;

    /* renamed from: c */
    final /* synthetic */ ChatWidgetService f976c;

    C1273b(ChatWidgetService chatWidgetService, int i, int i2) {
        this.f976c = chatWidgetService;
        this.f974a = i;
        this.f975b = i2;
    }

    public void run() {
        if (!ViewCompat.isAttachedToWindow(this.f976c.mWidgetView)) {
            Logger.m577v(ChatWidgetService.LOG_TAG, "Not attached to window. Skip loading widget");
            return;
        }
        switch (C1279h.f984a[this.f976c.mWidgetView.getAnchor().ordinal()]) {
            case 1:
                this.f976c.mRootLayoutParams.x = -this.f976c.mRootLayoutParams.width;
                this.f976c.mRootLayoutParams.y = -this.f976c.mRootLayoutParams.height;
                break;
            case 2:
                this.f976c.mRootLayoutParams.x = this.f974a + this.f976c.mRootLayoutParams.width;
                this.f976c.mRootLayoutParams.y = -this.f976c.mRootLayoutParams.height;
                break;
            case 3:
                this.f976c.mRootLayoutParams.x = -this.f976c.mRootLayoutParams.width;
                this.f976c.mRootLayoutParams.y = this.f975b + this.f976c.mRootLayoutParams.height;
                break;
            case 4:
                this.f976c.mRootLayoutParams.x = this.f974a + this.f976c.mRootLayoutParams.width;
                this.f976c.mRootLayoutParams.y = this.f975b + this.f976c.mRootLayoutParams.height;
                break;
            default:
                this.f976c.mRootLayoutParams.x = -this.f976c.mRootLayoutParams.width;
                this.f976c.mRootLayoutParams.y = (this.f975b - this.f976c.mWidgetView.getHeight()) / 2;
                break;
        }
        this.f976c.mWindowManager.updateViewLayout(this.f976c.mWidgetView, this.f976c.mRootLayoutParams);
        this.f976c.mWidgetView.setVisibility(0);
        this.f976c.mWidgetAnimatorTask = new C1270a(this.f976c, this.f976c.mHorizontalMargin, this.f976c.mVerticalMargin);
        this.f976c.mTimer = new Timer();
        this.f976c.mTimer.schedule(this.f976c.mWidgetAnimatorTask, 0, 30);
    }
}
