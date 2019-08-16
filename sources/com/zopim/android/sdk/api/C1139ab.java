package com.zopim.android.sdk.api;

import com.zopim.android.sdk.model.VisitorInfo;
import com.zopim.android.sdk.prechat.PreChatForm;
import com.zopim.android.sdk.prechat.PreChatForm.Builder;

/* renamed from: com.zopim.android.sdk.api.ab */
class C1139ab implements ChatConfig {

    /* renamed from: a */
    final /* synthetic */ ZopimChat f669a;

    C1139ab(ZopimChat zopimChat) {
        this.f669a = zopimChat;
    }

    public String getDepartment() {
        return this.f669a.mSessionConfig.department;
    }

    public PreChatForm getPreChatForm() {
        PreChatForm preChatForm = this.f669a.mSessionConfig.preChatForm;
        return preChatForm == null ? new Builder().build() : preChatForm;
    }

    public String[] getTags() {
        return this.f669a.mSessionConfig.tags;
    }

    public VisitorInfo getVisitorInfo() {
        VisitorInfo visitorInfo = this.f669a.mSessionConfig.visitorInfo;
        return visitorInfo == null ? new VisitorInfo.Builder().build() : visitorInfo;
    }
}
