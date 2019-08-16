package com.zopim.android.sdk.chatlog;

import android.view.View;
import android.view.View.OnClickListener;
import com.zopim.android.sdk.model.ChatLog.Rating;

/* renamed from: com.zopim.android.sdk.chatlog.o */
class C1217o implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ ChatRatingHolder f870a;

    C1217o(ChatRatingHolder chatRatingHolder) {
        this.f870a = chatRatingHolder;
    }

    public void onClick(View view) {
        if (this.f870a.f774m.f875a == Rating.GOOD) {
            this.f870a.f767f.clearCheck();
            if (this.f870a.f772k != null) {
                this.f870a.f772k.onRating(Rating.UNRATED);
            }
        } else if (this.f870a.f772k != null) {
            this.f870a.f772k.onRating(Rating.GOOD);
        }
    }
}
