package com.zopim.android.sdk.data;

import com.zopim.android.sdk.api.Logger;

/* renamed from: com.zopim.android.sdk.data.h */
enum C0869h {
    LIVECHAT_CHANNEL_LOG("livechat.channel.log"),
    LIVECHAT_PROFILE("livechat.profile"),
    LIVECHAT_AGENTS("livechat.agents"),
    LIVECHAT_UI("livechat.ui"),
    LIVECHAT_DEPARTMENTS("livechat.departments"),
    LIVECHAT_ACCOUNT("livechat.account"),
    LIVECHAT_SETTINGS_FORMS("livechat.settings.forms"),
    CONNECTION("connection"),
    UNKNOWN("unknown");
    
    /* renamed from: j */
    private static final String f865j = null;
    /* renamed from: k */
    private final String f867k;

    static {
        f865j = C0869h.class.getSimpleName();
    }

    private C0869h(String str) {
        this.f867k = str;
    }

    /* renamed from: a */
    static C0869h m698a(String str) {
        for (C0869h c0869h : C0869h.values()) {
            if (c0869h.f867k.contentEquals(str)) {
                return c0869h;
            }
        }
        Logger.m562i(f865j, "Unknown protocol path, will return " + UNKNOWN + ": " + str);
        return UNKNOWN;
    }
}
