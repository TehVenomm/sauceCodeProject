package com.zopim.android.sdk.prechat;

/* renamed from: com.zopim.android.sdk.prechat.h */
class C1255h implements Runnable {

    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f947a;

    C1255h(ZopimChatFragment zopimChatFragment) {
        this.f947a = zopimChatFragment;
    }

    public void run() {
        this.f947a.mProgressBar.setVisibility(8);
        this.f947a.mNoConnectionErrorView.setVisibility(8);
        this.f947a.mNoAgentsView.setVisibility(8);
        this.f947a.mCouldNotConnectErrorView.setVisibility(0);
    }
}
