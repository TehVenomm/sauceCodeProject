package com.zopim.android.sdk.api;

import android.util.Log;
import com.zopim.android.sdk.prechat.PreChatForm;
import java.io.Serializable;

/* renamed from: com.zopim.android.sdk.api.i */
abstract class C0795i<T extends C0795i> implements Serializable {
    private static final String LOG_TAG = C0795i.class.getSimpleName();
    private static final long serialVersionUID = 6741887007926757052L;
    String department;
    PreChatForm preChatForm;
    String referrer;
    String[] tags;
    String title;

    C0795i() {
    }

    public T department(String str) {
        if (str == null || str.isEmpty()) {
            Log.w(LOG_TAG, "Minimum department validation failed. Can not be null or empty string");
        } else {
            this.department = str;
        }
        return this;
    }

    public T preChatForm(PreChatForm preChatForm) {
        if (preChatForm == null) {
            Log.w(LOG_TAG, "PreChatForm must not be null");
        } else {
            this.preChatForm = preChatForm;
        }
        return this;
    }

    public T tags(String... strArr) {
        if (strArr == null) {
            Log.w(LOG_TAG, "Tags must not be null or empty string");
        } else {
            this.tags = strArr;
        }
        return this;
    }

    public T visitorPathOne(String str) {
        if (str == null || str.isEmpty()) {
            Log.w(LOG_TAG, "Visitor path must not be null or empty string");
        } else {
            this.title = str;
        }
        return this;
    }

    public T visitorPathTwo(String str) {
        if (str == null || str.isEmpty()) {
            Log.w(LOG_TAG, "Visitor path must not be null or empty string");
        } else {
            this.referrer = str;
        }
        return this;
    }
}
