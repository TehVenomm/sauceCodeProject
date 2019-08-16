package com.zopim.android.sdk.model;

import com.zopim.android.sdk.model.ChatLog.Type;

/* renamed from: com.zopim.android.sdk.model.a */
/* synthetic */ class C1246a {

    /* renamed from: a */
    static final /* synthetic */ int[] f938a = new int[Type.values().length];

    /* renamed from: b */
    static final /* synthetic */ int[] f939b = new int[C1244a.values().length];

    /* renamed from: c */
    static final /* synthetic */ int[] f940c = new int[C1245b.values().length];

    static {
        try {
            f940c[C1245b.CHAT_MSG.ordinal()] = 1;
        } catch (NoSuchFieldError e) {
        }
        try {
            f940c[C1245b.CHAT_EVENT.ordinal()] = 2;
        } catch (NoSuchFieldError e2) {
        }
        try {
            f940c[C1245b.MEMBER_JOIN.ordinal()] = 3;
        } catch (NoSuchFieldError e3) {
        }
        try {
            f940c[C1245b.MEMBER_LEAVE.ordinal()] = 4;
        } catch (NoSuchFieldError e4) {
        }
        try {
            f940c[C1245b.SYSTEM_OFFLINE.ordinal()] = 5;
        } catch (NoSuchFieldError e5) {
        }
        try {
            f940c[C1245b.FILE_UPLOAD.ordinal()] = 6;
        } catch (NoSuchFieldError e6) {
        }
        try {
            f940c[C1245b.CHAT_RATING_REQUEST.ordinal()] = 7;
        } catch (NoSuchFieldError e7) {
        }
        try {
            f940c[C1245b.CHAT_RATING.ordinal()] = 8;
        } catch (NoSuchFieldError e8) {
        }
        try {
            f940c[C1245b.CHAT_COMMENT.ordinal()] = 9;
        } catch (NoSuchFieldError e9) {
        }
        try {
            f939b[C1244a.AGENT_SYSTEM.ordinal()] = 1;
        } catch (NoSuchFieldError e10) {
        }
        try {
            f939b[C1244a.AGENT_TRIGGER.ordinal()] = 2;
        } catch (NoSuchFieldError e11) {
        }
        try {
            f939b[C1244a.AGENT_MSG.ordinal()] = 3;
        } catch (NoSuchFieldError e12) {
        }
        try {
            f939b[C1244a.VISITOR_MSG.ordinal()] = 4;
        } catch (NoSuchFieldError e13) {
        }
        try {
            f938a[Type.CHAT_MSG_VISITOR.ordinal()] = 1;
        } catch (NoSuchFieldError e14) {
        }
        try {
            f938a[Type.CHAT_MSG_AGENT.ordinal()] = 2;
        } catch (NoSuchFieldError e15) {
        }
        try {
            f938a[Type.CHAT_MSG_SYSTEM.ordinal()] = 3;
        } catch (NoSuchFieldError e16) {
        }
        try {
            f938a[Type.CHAT_MSG_TRIGGER.ordinal()] = 4;
        } catch (NoSuchFieldError e17) {
        }
        try {
            f938a[Type.MEMBER_JOIN.ordinal()] = 5;
        } catch (NoSuchFieldError e18) {
        }
        try {
            f938a[Type.MEMBER_LEAVE.ordinal()] = 6;
        } catch (NoSuchFieldError e19) {
        }
        try {
            f938a[Type.SYSTEM_OFFLINE.ordinal()] = 7;
        } catch (NoSuchFieldError e20) {
        }
    }
}
