package com.zopim.android.sdk.chatlog;

import android.content.ActivityNotFoundException;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Toast;
import com.zopim.android.sdk.C1122R;

/* renamed from: com.zopim.android.sdk.chatlog.d */
class C1206d implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ AgentMessageHolder f848a;

    C1206d(AgentMessageHolder agentMessageHolder) {
        this.f848a = agentMessageHolder;
    }

    public void onClick(View view) {
        try {
            this.f848a.itemView.getContext().startActivity(this.f848a.f761o);
        } catch (ActivityNotFoundException e) {
            Log.i(AgentMessageHolder.f747c, "Can't open attachment. No application can handle this uri. " + this.f848a.f761o.getData(), e);
            Toast.makeText(this.f848a.itemView.getContext(), C1122R.string.attachment_open_error_message, 0).show();
        }
    }
}
