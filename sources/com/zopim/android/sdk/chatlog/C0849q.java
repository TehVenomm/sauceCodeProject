package com.zopim.android.sdk.chatlog;

import android.content.Intent;
import android.view.View;
import android.view.View.OnClickListener;

/* renamed from: com.zopim.android.sdk.chatlog.q */
class C0849q implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ ChatRatingHolder f828a;

    C0849q(ChatRatingHolder chatRatingHolder) {
        this.f828a = chatRatingHolder;
    }

    public void onClick(View view) {
        this.f828a.itemView.getContext().startActivity(new Intent(this.f828a.itemView.getContext(), ZopimCommentActivity.class));
    }
}
