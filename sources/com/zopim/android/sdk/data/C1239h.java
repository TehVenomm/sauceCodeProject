package com.zopim.android.sdk.data;

import com.zopim.android.sdk.api.Logger;

/* renamed from: com.zopim.android.sdk.data.h */
enum C1239h {
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
    private static final String f909j = null;

    /* renamed from: k */
    private final String f911k;

    static {
        f909j = C1239h.class.getSimpleName();
    }

    private C1239h(String str) {
        this.f911k = str;
    }

    /* renamed from: a */
    static C1239h m711a(String str) {
        C1239h[] values;
        for (C1239h hVar : values()) {
            if (hVar.f911k.contentEquals(str)) {
                return hVar;
            }
        }
        Logger.m575i(f909j, "Unknown protocol path, will return " + UNKNOWN + ": " + str);
        return UNKNOWN;
    }
}
