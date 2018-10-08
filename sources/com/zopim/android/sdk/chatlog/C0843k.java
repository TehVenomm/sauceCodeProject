package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.chatlog.AgentMessageHolder.OptionClickListener;

/* renamed from: com.zopim.android.sdk.chatlog.k */
class C0843k implements OptionClickListener {
    /* renamed from: a */
    final /* synthetic */ C0841i f821a;

    C0843k(C0841i c0841i) {
        this.f821a = c0841i;
    }

    public void onClick(String str) {
        if (this.f821a.f818e != null) {
            this.f821a.f818e.send(str);
        }
    }
}
