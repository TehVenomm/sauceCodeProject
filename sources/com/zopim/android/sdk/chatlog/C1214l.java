package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.chatlog.ChatRatingHolder.Listener;
import com.zopim.android.sdk.model.ChatLog.Rating;

/* renamed from: com.zopim.android.sdk.chatlog.l */
class C1214l implements Listener {

    /* renamed from: a */
    final /* synthetic */ C1211i f866a;

    C1214l(C1211i iVar) {
        this.f866a = iVar;
    }

    public void onRating(Rating rating) {
        if (this.f866a.f862e != null) {
            this.f866a.f862e.sendChatRating(rating);
        }
    }
}
