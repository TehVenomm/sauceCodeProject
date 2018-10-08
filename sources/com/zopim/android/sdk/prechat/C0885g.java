package com.zopim.android.sdk.prechat;

/* renamed from: com.zopim.android.sdk.prechat.g */
class C0885g implements Runnable {
    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f902a;

    C0885g(ZopimChatFragment zopimChatFragment) {
        this.f902a = zopimChatFragment;
    }

    public void run() {
        this.f902a.mProgressBar.setVisibility(8);
        this.f902a.mNoConnectionErrorView.setVisibility(8);
        this.f902a.mNoAgentsView.setVisibility(0);
        this.f902a.mCouldNotConnectErrorView.setVisibility(8);
    }
}
