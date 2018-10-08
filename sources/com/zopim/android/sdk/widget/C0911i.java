package com.zopim.android.sdk.widget;

import android.support.v4.view.ViewCompat;
import com.zopim.android.sdk.widget.ChatWidgetService.C0901a;

/* renamed from: com.zopim.android.sdk.widget.i */
class C0911i implements Runnable {
    /* renamed from: a */
    final /* synthetic */ C0901a f941a;

    C0911i(C0901a c0901a) {
        this.f941a = c0901a;
    }

    public void run() {
        if (ViewCompat.isAttachedToWindow(this.f941a.f921e.mWidgetView)) {
            this.f941a.f921e.mRootLayoutParams.x = (((this.f941a.f921e.mRootLayoutParams.x - this.f941a.f919c) * 2) / 3) + this.f941a.f919c;
            this.f941a.f921e.mRootLayoutParams.y = (((this.f941a.f921e.mRootLayoutParams.y - this.f941a.f920d) * 2) / 3) + this.f941a.f920d;
            this.f941a.f921e.mWindowManager.updateViewLayout(this.f941a.f921e.mWidgetView, this.f941a.f921e.mRootLayoutParams);
            if (Math.abs(this.f941a.f921e.mRootLayoutParams.x - this.f941a.f919c) < 2 && Math.abs(this.f941a.f921e.mRootLayoutParams.y - this.f941a.f920d) < 2) {
                this.f941a.cancel();
                this.f941a.f921e.mTimer.cancel();
            }
        }
    }
}
