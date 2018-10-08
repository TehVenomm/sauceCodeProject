package com.zopim.android.sdk.prechat;

/* renamed from: com.zopim.android.sdk.prechat.f */
class C0883f implements Runnable {
    /* renamed from: a */
    final /* synthetic */ ZopimChatFragment f901a;

    C0883f(ZopimChatFragment zopimChatFragment) {
        this.f901a = zopimChatFragment;
    }

    public void run() {
        this.f901a.mProgressBar.setVisibility(8);
        this.f901a.mNoConnectionErrorView.setVisibility(0);
        this.f901a.mNoAgentsView.setVisibility(8);
        this.f901a.mCouldNotConnectErrorView.setVisibility(8);
    }
}
