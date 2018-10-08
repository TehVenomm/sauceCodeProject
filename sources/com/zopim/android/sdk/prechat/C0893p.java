package com.zopim.android.sdk.prechat;

import android.widget.Toast;
import com.zopim.android.sdk.C0784R;

/* renamed from: com.zopim.android.sdk.prechat.p */
class C0893p implements Runnable {
    /* renamed from: a */
    final /* synthetic */ C0892o f911a;

    C0893p(C0892o c0892o) {
        this.f911a = c0892o;
    }

    public void run() {
        this.f911a.f910a.mHandler.removeCallbacks(this.f911a.f910a.mShowSendTimeoutDialog);
        this.f911a.f910a.mProgressBar.setVisibility(8);
        Toast.makeText(this.f911a.f910a.getActivity(), C0784R.string.offline_sent_confirmation_message, 1).show();
        this.f911a.f910a.mChat.endChat();
        this.f911a.f910a.close();
        this.f911a.f910a.mChatListener.onChatEnded();
    }
}
