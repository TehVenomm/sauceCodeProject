package com.zopim.android.sdk.widget;

import android.annotation.TargetApi;
import android.os.Build.VERSION;
import com.zopim.android.sdk.model.Agent;
import java.util.Map;

/* renamed from: com.zopim.android.sdk.widget.d */
class C1275d implements Runnable {

    /* renamed from: a */
    final /* synthetic */ Map f978a;

    /* renamed from: b */
    final /* synthetic */ C1274c f979b;

    C1275d(C1274c cVar, Map map) {
        this.f979b = cVar;
        this.f978a = map;
    }

    @TargetApi(11)
    public void run() {
        boolean z = false;
        for (Agent agent : this.f978a.values()) {
            if (agent.isTyping() != null) {
                z = z || agent.isTyping().booleanValue();
            }
        }
        if (z) {
            this.f979b.f977a.mTypingIndicatorView.start();
            this.f979b.f977a.mUnreadNotificationView.setVisibility(4);
            this.f979b.f977a.mTypingIndicatorView.setVisibility(0);
            return;
        }
        long j = 0;
        if (VERSION.SDK_INT >= 11) {
            j = this.f979b.f977a.mCrossfadeAnimator.getDuration();
        }
        this.f979b.f977a.mAnimationHandler.postDelayed(new C1276e(this), j * 2);
    }
}
