package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.model.ChatLog.Error;
import com.zopim.android.sdk.model.ChatLog.Type;

/* synthetic */ class aq {
    /* renamed from: a */
    static final /* synthetic */ int[] f791a = new int[Error.values().length];
    /* renamed from: b */
    static final /* synthetic */ int[] f792b = new int[Type.values().length];

    static {
        try {
            f792b[Type.CHAT_MSG_VISITOR.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f792b[Type.CHAT_MSG_AGENT.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f792b[Type.CHAT_MSG_SYSTEM.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
        try {
            f792b[Type.CHAT_MSG_TRIGGER.ordinal()] = 4;
        } catch (NoSuchFieldError e4) {
        }
        try {
            f792b[Type.MEMBER_JOIN.ordinal()] = 5;
        } catch (NoSuchFieldError e5) {
        }
        try {
            f792b[Type.MEMBER_LEAVE.ordinal()] = 6;
        } catch (NoSuchFieldError e6) {
        }
        try {
            f792b[Type.ATTACHMENT_UPLOAD.ordinal()] = 7;
        } catch (NoSuchFieldError e7) {
        }
        try {
            f792b[Type.CHAT_RATING.ordinal()] = 8;
        } catch (NoSuchFieldError e8) {
        }
        try {
            f791a[Error.UNKNOWN.ordinal()] = 1;
        } catch (NoSuchFieldError e9) {
        }
        try {
            f791a[Error.UPLOAD_SIZE_ERROR.ordinal()] = 2;
        } catch (NoSuchFieldError e10) {
        }
        try {
            f791a[Error.UPLOAD_FILE_EXTENSION_ERROR.ordinal()] = 3;
        } catch (NoSuchFieldError e11) {
        }
    }
}
