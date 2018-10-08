package com.zopim.android.sdk.chatlog;

import android.view.View;
import android.view.View.OnClickListener;
import com.zopim.android.sdk.model.ChatLog.Rating;

/* renamed from: com.zopim.android.sdk.chatlog.p */
class C0848p implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ ChatRatingHolder f827a;

    C0848p(ChatRatingHolder chatRatingHolder) {
        this.f827a = chatRatingHolder;
    }

    public void onClick(View view) {
        if (this.f827a.f730m.f831a == Rating.BAD) {
            this.f827a.f723f.clearCheck();
            if (this.f827a.f728k != null) {
                this.f827a.f728k.onRating(Rating.UNRATED);
            }
        } else if (this.f827a.f728k != null) {
            this.f827a.f728k.onRating(Rating.BAD);
        }
    }
}
