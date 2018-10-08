package com.zopim.android.sdk.chatlog;

import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Toast;
import com.zopim.android.sdk.C0785R;

/* renamed from: com.zopim.android.sdk.chatlog.d */
class C0837d implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ AgentMessageHolder f804a;

    C0837d(AgentMessageHolder agentMessageHolder) {
        this.f804a = agentMessageHolder;
    }

    public void onClick(View view) {
        try {
            this.f804a.itemView.getContext().startActivity(this.f804a.f717o);
        } catch (Throwable e) {
            Log.i(AgentMessageHolder.f703c, "Can't open attachment. No application can handle this uri. " + this.f804a.f717o.getData(), e);
            Toast.makeText(this.f804a.itemView.getContext(), C0785R.string.attachment_open_error_message, 0).show();
        }
    }
}
