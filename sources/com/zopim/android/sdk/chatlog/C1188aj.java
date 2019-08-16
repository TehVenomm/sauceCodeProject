package com.zopim.android.sdk.chatlog;

import android.app.AlertDialog.Builder;
import com.zopim.android.sdk.C1122R;

/* renamed from: com.zopim.android.sdk.chatlog.aj */
class C1188aj implements Runnable {

    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f826a;

    C1188aj(ZopimChatLogFragment zopimChatLogFragment) {
        this.f826a = zopimChatLogFragment;
    }

    public void run() {
        this.f826a.mReconnectTimeoutDialog = new Builder(this.f826a.getActivity()).setTitle(C1122R.string.reconnect_timeout_title).setMessage(C1122R.string.reconnect_timeout_message).setPositiveButton(C1122R.string.reconnect_timeout_confirm_button, new C1190al(this)).setNegativeButton(C1122R.string.reconnect_timeout_cancel_button, new C1189ak(this)).show();
    }
}
