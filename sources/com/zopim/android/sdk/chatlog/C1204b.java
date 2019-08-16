package com.zopim.android.sdk.chatlog;

import com.squareup.picasso.Callback.EmptyCallback;

/* renamed from: com.zopim.android.sdk.chatlog.b */
class C1204b extends EmptyCallback {

    /* renamed from: a */
    final /* synthetic */ AgentMessageHolder f846a;

    C1204b(AgentMessageHolder agentMessageHolder) {
        this.f846a = agentMessageHolder;
    }

    public void onError() {
        super.onError();
        this.f846a.f759m.setVisibility(8);
    }

    public void onSuccess() {
        super.onSuccess();
        this.f846a.f759m.setVisibility(8);
    }
}
