package com.zopim.android.sdk.chatlog;

import android.view.View;
import android.view.View.OnClickListener;
import com.zopim.android.sdk.model.ChatLog.Rating;

/* renamed from: com.zopim.android.sdk.chatlog.p */
class C1218p implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ ChatRatingHolder f871a;

    C1218p(ChatRatingHolder chatRatingHolder) {
        this.f871a = chatRatingHolder;
    }

    public void onClick(View view) {
        if (this.f871a.f774m.f875a == Rating.BAD) {
            this.f871a.f767f.clearCheck();
            if (this.f871a.f772k != null) {
                this.f871a.f772k.onRating(Rating.UNRATED);
            }
        } else if (this.f871a.f772k != null) {
            this.f871a.f772k.onRating(Rating.BAD);
        }
    }
}
