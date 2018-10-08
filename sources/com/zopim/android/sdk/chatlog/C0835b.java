package com.zopim.android.sdk.chatlog;

import com.squareup.picasso.Callback.EmptyCallback;

/* renamed from: com.zopim.android.sdk.chatlog.b */
class C0835b extends EmptyCallback {
    /* renamed from: a */
    final /* synthetic */ AgentMessageHolder f802a;

    C0835b(AgentMessageHolder agentMessageHolder) {
        this.f802a = agentMessageHolder;
    }

    public void onError() {
        super.onError();
        this.f802a.f715m.setVisibility(8);
    }

    public void onSuccess() {
        super.onSuccess();
        this.f802a.f715m.setVisibility(8);
    }
}
