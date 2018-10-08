package com.zopim.android.sdk.chatlog;

import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;

class ac implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ VisitorMessageHolder f770a;

    ac(VisitorMessageHolder visitorMessageHolder) {
        this.f770a = visitorMessageHolder;
    }

    public void onClick(View view) {
        if (this.f770a.f739h != null) {
            this.f770a.f739h.onClick(this.f770a.getAdapterPosition());
        } else {
            Log.i(VisitorMessageHolder.f731k, "Failed message click listener not configured. Click events are ignored.");
        }
    }
}
