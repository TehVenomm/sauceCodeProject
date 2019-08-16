package com.zopim.android.sdk.chatlog;

import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;

/* renamed from: com.zopim.android.sdk.chatlog.ac */
class C1181ac implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ VisitorMessageHolder f814a;

    C1181ac(VisitorMessageHolder visitorMessageHolder) {
        this.f814a = visitorMessageHolder;
    }

    public void onClick(View view) {
        if (this.f814a.f783h != null) {
            this.f814a.f783h.onClick(this.f814a.getAdapterPosition());
        } else {
            Log.i(VisitorMessageHolder.f775k, "Failed message click listener not configured. Click events are ignored.");
        }
    }
}
