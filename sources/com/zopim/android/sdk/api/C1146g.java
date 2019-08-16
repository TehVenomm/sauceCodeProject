package com.zopim.android.sdk.api;

import com.zopim.android.sdk.model.VisitorInfo;
import com.zopim.android.sdk.prechat.PreChatForm;
import com.zopim.android.sdk.prechat.PreChatForm.Builder;

/* renamed from: com.zopim.android.sdk.api.g */
class C1146g implements ChatConfig {

    /* renamed from: a */
    final /* synthetic */ ChatService f678a;

    C1146g(ChatService chatService) {
        this.f678a = chatService;
    }

    public String getDepartment() {
        return this.f678a.mDepartment;
    }

    public PreChatForm getPreChatForm() {
        return this.f678a.mPreChatForm == null ? new Builder().build() : this.f678a.mPreChatForm;
    }

    public String[] getTags() {
        return this.f678a.mTags;
    }

    public VisitorInfo getVisitorInfo() {
        return new VisitorInfo.Builder().name(this.f678a.mVisitorName).email(this.f678a.mVisitorEmail).phoneNumber(this.f678a.mVisitorPhoneNumber).build();
    }
}
