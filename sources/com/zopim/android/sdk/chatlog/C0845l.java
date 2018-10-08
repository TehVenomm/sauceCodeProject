package com.zopim.android.sdk.chatlog;

import com.zopim.android.sdk.chatlog.ChatRatingHolder.Listener;
import com.zopim.android.sdk.model.ChatLog.Rating;

/* renamed from: com.zopim.android.sdk.chatlog.l */
class C0845l implements Listener {
    /* renamed from: a */
    final /* synthetic */ C0842i f822a;

    C0845l(C0842i c0842i) {
        this.f822a = c0842i;
    }

    public void onRating(Rating rating) {
        if (this.f822a.f818e != null) {
            this.f822a.f818e.sendChatRating(rating);
        }
    }
}
