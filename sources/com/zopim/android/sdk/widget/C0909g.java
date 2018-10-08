package com.zopim.android.sdk.widget;

/* renamed from: com.zopim.android.sdk.widget.g */
class C0909g implements Runnable {
    /* renamed from: a */
    final /* synthetic */ int f938a;
    /* renamed from: b */
    final /* synthetic */ C0908f f939b;

    C0909g(C0908f c0908f, int i) {
        this.f939b = c0908f;
        this.f938a = i;
    }

    public void run() {
        this.f939b.f937a.mUnreadNotificationView.setText(String.valueOf(this.f938a));
        this.f939b.f937a.showUnreadNotification();
    }
}
