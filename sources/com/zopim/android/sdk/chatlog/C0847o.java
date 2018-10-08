package com.zopim.android.sdk.chatlog;

import android.view.View;
import android.view.View.OnClickListener;
import com.zopim.android.sdk.model.ChatLog.Rating;

/* renamed from: com.zopim.android.sdk.chatlog.o */
class C0847o implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ ChatRatingHolder f826a;

    C0847o(ChatRatingHolder chatRatingHolder) {
        this.f826a = chatRatingHolder;
    }

    public void onClick(View view) {
        if (this.f826a.f730m.f831a == Rating.GOOD) {
            this.f826a.f723f.clearCheck();
            if (this.f826a.f728k != null) {
                this.f826a.f728k.onRating(Rating.UNRATED);
            }
        } else if (this.f826a.f728k != null) {
            this.f826a.f728k.onRating(Rating.GOOD);
        }
    }
}
