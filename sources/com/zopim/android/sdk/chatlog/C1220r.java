package com.zopim.android.sdk.chatlog;

import android.content.Intent;
import android.view.View;
import android.view.View.OnClickListener;

/* renamed from: com.zopim.android.sdk.chatlog.r */
class C1220r implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ ChatRatingHolder f873a;

    C1220r(ChatRatingHolder chatRatingHolder) {
        this.f873a = chatRatingHolder;
    }

    public void onClick(View view) {
        Intent intent = new Intent(this.f873a.itemView.getContext(), ZopimCommentActivity.class);
        intent.putExtra(ZopimCommentActivity.EXTRA_COMMENT, this.f873a.f773l.getText().toString());
        this.f873a.itemView.getContext().startActivity(intent);
    }
}
