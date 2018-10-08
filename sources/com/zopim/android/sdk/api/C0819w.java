package com.zopim.android.sdk.api;

import com.zopim.android.sdk.model.VisitorInfo;
import com.zopim.android.sdk.prechat.PreChatForm;
import com.zopim.android.sdk.prechat.PreChatForm.Builder;

/* renamed from: com.zopim.android.sdk.api.w */
class C0819w implements ChatConfig {
    /* renamed from: a */
    final /* synthetic */ C0818v f672a;

    C0819w(C0818v c0818v) {
        this.f672a = c0818v;
    }

    public String getDepartment() {
        return "";
    }

    public PreChatForm getPreChatForm() {
        return new Builder().build();
    }

    public String[] getTags() {
        return new String[0];
    }

    public VisitorInfo getVisitorInfo() {
        return new VisitorInfo.Builder().build();
    }
}
