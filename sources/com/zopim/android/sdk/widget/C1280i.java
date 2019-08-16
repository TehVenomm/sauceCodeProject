package com.zopim.android.sdk.widget;

import android.support.p000v4.view.ViewCompat;

/* renamed from: com.zopim.android.sdk.widget.i */
class C1280i implements Runnable {

    /* renamed from: a */
    final /* synthetic */ C1270a f985a;

    C1280i(C1270a aVar) {
        this.f985a = aVar;
    }

    public void run() {
        if (ViewCompat.isAttachedToWindow(this.f985a.f965e.mWidgetView)) {
            this.f985a.f965e.mRootLayoutParams.x = (((this.f985a.f965e.mRootLayoutParams.x - this.f985a.f963c) * 2) / 3) + this.f985a.f963c;
            this.f985a.f965e.mRootLayoutParams.y = (((this.f985a.f965e.mRootLayoutParams.y - this.f985a.f964d) * 2) / 3) + this.f985a.f964d;
            this.f985a.f965e.mWindowManager.updateViewLayout(this.f985a.f965e.mWidgetView, this.f985a.f965e.mRootLayoutParams);
            if (Math.abs(this.f985a.f965e.mRootLayoutParams.x - this.f985a.f963c) < 2 && Math.abs(this.f985a.f965e.mRootLayoutParams.y - this.f985a.f964d) < 2) {
                this.f985a.cancel();
                this.f985a.f965e.mTimer.cancel();
            }
        }
    }
}
