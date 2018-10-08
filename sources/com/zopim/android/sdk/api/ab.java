package com.zopim.android.sdk.api;

import com.zopim.android.sdk.model.VisitorInfo;
import com.zopim.android.sdk.prechat.PreChatForm;
import com.zopim.android.sdk.prechat.PreChatForm.Builder;

class ab implements ChatConfig {
    /* renamed from: a */
    final /* synthetic */ ZopimChat f625a;

    ab(ZopimChat zopimChat) {
        this.f625a = zopimChat;
    }

    public String getDepartment() {
        return this.f625a.mSessionConfig.department;
    }

    public PreChatForm getPreChatForm() {
        PreChatForm preChatForm = this.f625a.mSessionConfig.preChatForm;
        return preChatForm == null ? new Builder().build() : preChatForm;
    }

    public String[] getTags() {
        return this.f625a.mSessionConfig.tags;
    }

    public VisitorInfo getVisitorInfo() {
        VisitorInfo visitorInfo = this.f625a.mSessionConfig.visitorInfo;
        return visitorInfo == null ? new VisitorInfo.Builder().build() : visitorInfo;
    }
}
