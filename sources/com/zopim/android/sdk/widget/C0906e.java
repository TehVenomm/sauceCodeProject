package com.zopim.android.sdk.widget;

/* renamed from: com.zopim.android.sdk.widget.e */
class C0906e implements Runnable {
    /* renamed from: a */
    final /* synthetic */ C0905d f936a;

    C0906e(C0905d c0905d) {
        this.f936a = c0905d;
    }

    public void run() {
        this.f936a.f935b.f933a.mTypingIndicatorView.stop();
        if (this.f936a.f935b.f933a.mUnreadCount > 0) {
            this.f936a.f935b.f933a.mUnreadNotificationView.setVisibility(0);
            this.f936a.f935b.f933a.mTypingIndicatorView.setVisibility(4);
        }
    }
}
