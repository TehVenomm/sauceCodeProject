package com.zopim.android.sdk.chatlog;

import android.app.AlertDialog.Builder;
import com.zopim.android.sdk.C0785R;

class aj implements Runnable {
    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f782a;

    aj(ZopimChatLogFragment zopimChatLogFragment) {
        this.f782a = zopimChatLogFragment;
    }

    public void run() {
        this.f782a.mReconnectTimeoutDialog = new Builder(this.f782a.getActivity()).setTitle(C0785R.string.reconnect_timeout_title).setMessage(C0785R.string.reconnect_timeout_message).setPositiveButton(C0785R.string.reconnect_timeout_confirm_button, new al(this)).setNegativeButton(C0785R.string.reconnect_timeout_cancel_button, new ak(this)).show();
    }
}
