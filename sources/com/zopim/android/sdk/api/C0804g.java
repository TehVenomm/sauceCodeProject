package com.zopim.android.sdk.api;

import com.zopim.android.sdk.model.VisitorInfo;
import com.zopim.android.sdk.prechat.PreChatForm;
import com.zopim.android.sdk.prechat.PreChatForm.Builder;

/* renamed from: com.zopim.android.sdk.api.g */
class C0804g implements ChatConfig {
    /* renamed from: a */
    final /* synthetic */ ChatService f635a;

    C0804g(ChatService chatService) {
        this.f635a = chatService;
    }

    public String getDepartment() {
        return this.f635a.mDepartment;
    }

    public PreChatForm getPreChatForm() {
        return this.f635a.mPreChatForm == null ? new Builder().build() : this.f635a.mPreChatForm;
    }

    public String[] getTags() {
        return this.f635a.mTags;
    }

    public VisitorInfo getVisitorInfo() {
        return new VisitorInfo.Builder().name(this.f635a.mVisitorName).email(this.f635a.mVisitorEmail).phoneNumber(this.f635a.mVisitorPhoneNumber).build();
    }
}
