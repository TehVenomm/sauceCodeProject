package com.zopim.android.sdk.data;

import com.zopim.android.sdk.api.Logger;

/* renamed from: com.zopim.android.sdk.data.h */
enum C0870h {
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
        f865j = C0870h.class.getSimpleName();
    }

    private C0870h(String str) {
        this.f867k = str;
    }

    /* renamed from: a */
    static C0870h m698a(String str) {
        for (C0870h c0870h : C0870h.values()) {
            if (c0870h.f867k.contentEquals(str)) {
                return c0870h;
            }
        }
        Logger.m562i(f865j, "Unknown protocol path, will return " + UNKNOWN + ": " + str);
        return UNKNOWN;
    }
}
