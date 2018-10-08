package com.zopim.android.sdk.api;

import com.zopim.android.sdk.model.VisitorInfo;
import com.zopim.android.sdk.prechat.PreChatForm;

public interface ChatConfig {
    String getDepartment();

    PreChatForm getPreChatForm();

    String[] getTags();

    VisitorInfo getVisitorInfo();
}
