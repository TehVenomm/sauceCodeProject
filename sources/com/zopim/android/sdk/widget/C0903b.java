package com.zopim.android.sdk.widget;

import android.support.v4.view.ViewCompat;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.widget.ChatWidgetService.C0900a;
import java.util.Timer;

/* renamed from: com.zopim.android.sdk.widget.b */
class C0903b implements Runnable {
    /* renamed from: a */
    final /* synthetic */ int f930a;
    /* renamed from: b */
    final /* synthetic */ int f931b;
    /* renamed from: c */
    final /* synthetic */ ChatWidgetService f932c;

    C0903b(ChatWidgetService chatWidgetService, int i, int i2) {
        this.f932c = chatWidgetService;
        this.f930a = i;
        this.f931b = i2;
    }

    public void run() {
        if (ViewCompat.isAttachedToWindow(this.f932c.mWidgetView)) {
            switch (C0909h.f940a[this.f932c.mWidgetView.getAnchor().ordinal()]) {
                case 1:
                    this.f932c.mRootLayoutParams.x = -this.f932c.mRootLayoutParams.width;
                    this.f932c.mRootLayoutParams.y = -this.f932c.mRootLayoutParams.height;
                    break;
                case 2:
                    this.f932c.mRootLayoutParams.x = this.f930a + this.f932c.mRootLayoutParams.width;
                    this.f932c.mRootLayoutParams.y = -this.f932c.mRootLayoutParams.height;
                    break;
                case 3:
                    this.f932c.mRootLayoutParams.x = -this.f932c.mRootLayoutParams.width;
                    this.f932c.mRootLayoutParams.y = this.f931b + this.f932c.mRootLayoutParams.height;
                    break;
                case 4:
                    this.f932c.mRootLayoutParams.x = this.f930a + this.f932c.mRootLayoutParams.width;
                    this.f932c.mRootLayoutParams.y = this.f931b + this.f932c.mRootLayoutParams.height;
                    break;
                default:
                    this.f932c.mRootLayoutParams.x = -this.f932c.mRootLayoutParams.width;
                    this.f932c.mRootLayoutParams.y = (this.f931b - this.f932c.mWidgetView.getHeight()) / 2;
                    break;
            }
            this.f932c.mWindowManager.updateViewLayout(this.f932c.mWidgetView, this.f932c.mRootLayoutParams);
            this.f932c.mWidgetView.setVisibility(0);
            this.f932c.mWidgetAnimatorTask = new C0900a(this.f932c, this.f932c.mHorizontalMargin, this.f932c.mVerticalMargin);
            this.f932c.mTimer = new Timer();
            this.f932c.mTimer.schedule(this.f932c.mWidgetAnimatorTask, 0, 30);
            return;
        }
        Logger.m564v(ChatWidgetService.LOG_TAG, "Not attached to window. Skip loading widget");
    }
}
