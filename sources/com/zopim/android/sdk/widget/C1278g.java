package com.zopim.android.sdk.widget;

/* renamed from: com.zopim.android.sdk.widget.g */
class C1278g implements Runnable {

    /* renamed from: a */
    final /* synthetic */ int f982a;

    /* renamed from: b */
    final /* synthetic */ C1277f f983b;

    C1278g(C1277f fVar, int i) {
        this.f983b = fVar;
        this.f982a = i;
    }

    public void run() {
        this.f983b.f981a.mUnreadNotificationView.setText(String.valueOf(this.f982a));
        this.f983b.f981a.showUnreadNotification();
    }
}
