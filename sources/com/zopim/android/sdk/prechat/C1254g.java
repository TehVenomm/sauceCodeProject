package com.zopim.android.sdk.prechat;

/* renamed from: com.zopim.android.sdk.prechat.g */
class C1254g implements Runnable {

    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f946a;

    C1254g(ZopimChatFragment zopimChatFragment) {
        this.f946a = zopimChatFragment;
    }

    public void run() {
        this.f946a.mProgressBar.setVisibility(8);
        this.f946a.mNoConnectionErrorView.setVisibility(8);
        this.f946a.mNoAgentsView.setVisibility(0);
        this.f946a.mCouldNotConnectErrorView.setVisibility(8);
    }
}
