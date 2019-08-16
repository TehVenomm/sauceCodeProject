package com.zopim.android.sdk.widget;

/* renamed from: com.zopim.android.sdk.widget.e */
class C1276e implements Runnable {

    /* renamed from: a */
    final /* synthetic */ C1275d f980a;

    C1276e(C1275d dVar) {
        this.f980a = dVar;
    }

    public void run() {
        this.f980a.f979b.f977a.mTypingIndicatorView.stop();
        if (this.f980a.f979b.f977a.mUnreadCount > 0) {
            this.f980a.f979b.f977a.mUnreadNotificationView.setVisibility(0);
            this.f980a.f979b.f977a.mTypingIndicatorView.setVisibility(4);
        }
    }
}
