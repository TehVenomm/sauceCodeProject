package com.zopim.android.sdk.chatlog;

import android.content.Intent;
import android.view.View;
import android.view.View.OnClickListener;

/* renamed from: com.zopim.android.sdk.chatlog.r */
class C0850r implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ ChatRatingHolder f829a;

    C0850r(ChatRatingHolder chatRatingHolder) {
        this.f829a = chatRatingHolder;
    }

    public void onClick(View view) {
        Intent intent = new Intent(this.f829a.itemView.getContext(), ZopimCommentActivity.class);
        intent.putExtra(ZopimCommentActivity.EXTRA_COMMENT, this.f829a.f729l.getText().toString());
        this.f829a.itemView.getContext().startActivity(intent);
    }
}
