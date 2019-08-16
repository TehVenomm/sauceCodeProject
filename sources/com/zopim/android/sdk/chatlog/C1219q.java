package com.zopim.android.sdk.chatlog;

import android.content.Intent;
import android.view.View;
import android.view.View.OnClickListener;

/* renamed from: com.zopim.android.sdk.chatlog.q */
class C1219q implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ ChatRatingHolder f872a;

    C1219q(ChatRatingHolder chatRatingHolder) {
        this.f872a = chatRatingHolder;
    }

    public void onClick(View view) {
        this.f872a.itemView.getContext().startActivity(new Intent(this.f872a.itemView.getContext(), ZopimCommentActivity.class));
    }
}
