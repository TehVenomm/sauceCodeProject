package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.chatlog.AgentMessageHolder.OptionClickListener;

/* renamed from: com.zopim.android.sdk.chatlog.k */
class C1213k implements OptionClickListener {

    /* renamed from: a */
    final /* synthetic */ C1211i f865a;

    C1213k(C1211i iVar) {
        this.f865a = iVar;
    }

    public void onClick(String str) {
        if (this.f865a.f862e != null) {
            this.f865a.f862e.send(str);
        }
    }
}
