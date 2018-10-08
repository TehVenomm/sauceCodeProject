package com.zopim.android.sdk.model;

import com.zopim.android.sdk.model.ChatLog.C0875a;
import com.zopim.android.sdk.model.ChatLog.C0876b;
import com.zopim.android.sdk.model.ChatLog.Type;

/* renamed from: com.zopim.android.sdk.model.a */
/* synthetic */ class C0877a {
    /* renamed from: a */
    static final /* synthetic */ int[] f894a = new int[Type.values().length];
    /* renamed from: b */
    static final /* synthetic */ int[] f895b = new int[C0875a.values().length];
    /* renamed from: c */
    static final /* synthetic */ int[] f896c = new int[C0876b.values().length];

    static {
        try {
            f896c[C0876b.CHAT_MSG.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f896c[C0876b.CHAT_EVENT.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f896c[C0876b.MEMBER_JOIN.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
        try {
            f896c[C0876b.MEMBER_LEAVE.ordinal()] = 4;
        } catch (NoSuchFieldError e4) {
        }
        try {
            f896c[C0876b.SYSTEM_OFFLINE.ordinal()] = 5;
        } catch (NoSuchFieldError e5) {
        }
        try {
            f896c[C0876b.FILE_UPLOAD.ordinal()] = 6;
        } catch (NoSuchFieldError e6) {
        }
        try {
            f896c[C0876b.CHAT_RATING_REQUEST.ordinal()] = 7;
        } catch (NoSuchFieldError e7) {
        }
        try {
            f896c[C0876b.CHAT_RATING.ordinal()] = 8;
        } catch (NoSuchFieldError e8) {
        }
        try {
            f896c[C0876b.CHAT_COMMENT.ordinal()] = 9;
        } catch (NoSuchFieldError e9) {
        }
        try {
            f895b[C0875a.AGENT_SYSTEM.ordinal()] = 1;
        } catch (NoSuchFieldError e10) {
        }
        try {
            f895b[C0875a.AGENT_TRIGGER.ordinal()] = 2;
        } catch (NoSuchFieldError e11) {
        }
        try {
            f895b[C0875a.AGENT_MSG.ordinal()] = 3;
        } catch (NoSuchFieldError e12) {
        }
        try {
            f895b[C0875a.VISITOR_MSG.ordinal()] = 4;
        } catch (NoSuchFieldError e13) {
        }
        try {
            f894a[Type.CHAT_MSG_VISITOR.ordinal()] = 1;
        } catch (NoSuchFieldError e14) {
        }
        try {
            f894a[Type.CHAT_MSG_AGENT.ordinal()] = 2;
        } catch (NoSuchFieldError e15) {
        }
        try {
            f894a[Type.CHAT_MSG_SYSTEM.ordinal()] = 3;
        } catch (NoSuchFieldError e16) {
        }
        try {
            f894a[Type.CHAT_MSG_TRIGGER.ordinal()] = 4;
        } catch (NoSuchFieldError e17) {
        }
        try {
            f894a[Type.MEMBER_JOIN.ordinal()] = 5;
        } catch (NoSuchFieldError e18) {
        }
        try {
            f894a[Type.MEMBER_LEAVE.ordinal()] = 6;
        } catch (NoSuchFieldError e19) {
        }
        try {
            f894a[Type.SYSTEM_OFFLINE.ordinal()] = 7;
        } catch (NoSuchFieldError e20) {
        }
    }
}
