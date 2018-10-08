package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.chatlog.AgentMessageHolder.OptionClickListener;

/* renamed from: com.zopim.android.sdk.chatlog.k */
class C0844k implements OptionClickListener {
    /* renamed from: a */
    final /* synthetic */ C0842i f821a;

    C0844k(C0842i c0842i) {
        this.f821a = c0842i;
    }

    public void onClick(String str) {
        if (this.f821a.f818e != null) {
            this.f821a.f818e.send(str);
        }
    }
}
