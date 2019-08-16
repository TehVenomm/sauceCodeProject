package com.zopim.android.sdk.prechat;

/* renamed from: com.zopim.android.sdk.prechat.f */
class C1253f implements Runnable {

    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f945a;

    C1253f(ZopimChatFragment zopimChatFragment) {
        this.f945a = zopimChatFragment;
    }

    public void run() {
        this.f945a.mProgressBar.setVisibility(8);
        this.f945a.mNoConnectionErrorView.setVisibility(0);
        this.f945a.mNoAgentsView.setVisibility(8);
        this.f945a.mCouldNotConnectErrorView.setVisibility(8);
    }
}
