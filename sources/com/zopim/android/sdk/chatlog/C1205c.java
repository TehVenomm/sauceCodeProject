package com.zopim.android.sdk.chatlog;

import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.TextView;
import com.zopim.android.sdk.C1122R;
import com.zopim.android.sdk.api.Logger;

/* renamed from: com.zopim.android.sdk.chatlog.c */
class C1205c implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ AgentMessageHolder f847a;

    C1205c(AgentMessageHolder agentMessageHolder) {
        this.f847a = agentMessageHolder;
    }

    public void onClick(View view) {
        if (this.f847a.f760n == null) {
            Log.i(AgentMessageHolder.f747c, "Agent item click listener not configured. Click events are ignored.");
        }
        Logger.m577v(AgentMessageHolder.f747c, "Clicked option item");
        view.setClickable(false);
        TextView textView = (TextView) view;
        textView.setBackgroundResource(C1122R.C1124drawable.bg_chat_bubble_visitor);
        textView.setTextAppearance(this.f847a.itemView.getContext(), C1122R.style.chat_bubble_visitor);
        this.f847a.f760n.onClick(textView.getText().toString());
    }
}
