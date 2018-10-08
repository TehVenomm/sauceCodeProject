package com.zopim.android.sdk.chatlog;

import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.TextView;
import com.zopim.android.sdk.C0784R;
import com.zopim.android.sdk.api.Logger;

/* renamed from: com.zopim.android.sdk.chatlog.c */
class C0835c implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ AgentMessageHolder f803a;

    C0835c(AgentMessageHolder agentMessageHolder) {
        this.f803a = agentMessageHolder;
    }

    public void onClick(View view) {
        if (this.f803a.f716n == null) {
            Log.i(AgentMessageHolder.f703c, "Agent item click listener not configured. Click events are ignored.");
        }
        Logger.m564v(AgentMessageHolder.f703c, "Clicked option item");
        view.setClickable(false);
        TextView textView = (TextView) view;
        textView.setBackgroundResource(C0784R.drawable.bg_chat_bubble_visitor);
        textView.setTextAppearance(this.f803a.itemView.getContext(), C0784R.style.chat_bubble_visitor);
        this.f803a.f716n.onClick(textView.getText().toString());
    }
}
