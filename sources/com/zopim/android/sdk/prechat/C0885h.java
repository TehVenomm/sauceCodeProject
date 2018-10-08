package com.zopim.android.sdk.prechat;

/* renamed from: com.zopim.android.sdk.prechat.h */
class C0885h implements Runnable {
    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f903a;

    C0885h(ZopimChatFragment zopimChatFragment) {
        this.f903a = zopimChatFragment;
    }

    public void run() {
        this.f903a.mProgressBar.setVisibility(8);
        this.f903a.mNoConnectionErrorView.setVisibility(8);
        this.f903a.mNoAgentsView.setVisibility(8);
        this.f903a.mCouldNotConnectErrorView.setVisibility(0);
    }
}
