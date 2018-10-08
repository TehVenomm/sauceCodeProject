package com.zopim.android.sdk.widget;

import android.annotation.TargetApi;
import android.os.Build.VERSION;
import com.zopim.android.sdk.model.Agent;
import java.util.Map;

/* renamed from: com.zopim.android.sdk.widget.d */
class C0905d implements Runnable {
    /* renamed from: a */
    final /* synthetic */ Map f934a;
    /* renamed from: b */
    final /* synthetic */ C0904c f935b;

    C0905d(C0904c c0904c, Map map) {
        this.f935b = c0904c;
        this.f934a = map;
    }

    @TargetApi(11)
    public void run() {
        int i = 0;
        for (Agent agent : this.f934a.values()) {
            if (agent.isTyping() != null) {
                int i2 = (i != 0 || agent.isTyping().booleanValue()) ? 1 : 0;
                i = i2;
            }
        }
        if (i != 0) {
            this.f935b.f933a.mTypingIndicatorView.start();
            this.f935b.f933a.mUnreadNotificationView.setVisibility(4);
            this.f935b.f933a.mTypingIndicatorView.setVisibility(0);
            return;
        }
        long j = 0;
        if (VERSION.SDK_INT >= 11) {
            j = this.f935b.f933a.mCrossfadeAnimator.getDuration();
        }
        this.f935b.f933a.mAnimationHandler.postDelayed(new C0906e(this), j * 2);
    }
}
