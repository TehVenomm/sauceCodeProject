package com.zopim.android.sdk.api;

import com.zopim.android.sdk.model.VisitorInfo;
import com.zopim.android.sdk.prechat.PreChatForm;
import com.zopim.android.sdk.prechat.PreChatForm.Builder;

/* renamed from: com.zopim.android.sdk.api.w */
class C1163w implements ChatConfig {

    /* renamed from: a */
    final /* synthetic */ C1162v f716a;

    C1163w(C1162v vVar) {
        this.f716a = vVar;
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
