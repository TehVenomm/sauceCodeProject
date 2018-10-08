package com.zopim.android.sdk.prechat;

import android.widget.Toast;
import com.zopim.android.sdk.C0785R;

/* renamed from: com.zopim.android.sdk.prechat.p */
class C0894p implements Runnable {
    /* renamed from: a */
    final /* synthetic */ C0893o f911a;

    C0894p(C0893o c0893o) {
        this.f911a = c0893o;
    }

    public void run() {
        this.f911a.f910a.mHandler.removeCallbacks(this.f911a.f910a.mShowSendTimeoutDialog);
        this.f911a.f910a.mProgressBar.setVisibility(8);
        Toast.makeText(this.f911a.f910a.getActivity(), C0785R.string.offline_sent_confirmation_message, 1).show();
        this.f911a.f910a.mChat.endChat();
        this.f911a.f910a.close();
        this.f911a.f910a.mChatListener.onChatEnded();
    }
}
