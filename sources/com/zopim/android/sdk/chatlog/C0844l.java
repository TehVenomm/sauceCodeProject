package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.chatlog.ChatRatingHolder.Listener;
import com.zopim.android.sdk.model.ChatLog.Rating;

/* renamed from: com.zopim.android.sdk.chatlog.l */
class C0844l implements Listener {
    /* renamed from: a */
    final /* synthetic */ C0841i f822a;

    C0844l(C0841i c0841i) {
        this.f822a = c0841i;
    }

    public void onRating(Rating rating) {
        if (this.f822a.f818e != null) {
            this.f822a.f818e.sendChatRating(rating);
        }
    }
}
