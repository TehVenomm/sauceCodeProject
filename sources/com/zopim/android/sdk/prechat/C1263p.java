package com.zopim.android.sdk.prechat;

import android.widget.Toast;
import com.zopim.android.sdk.C1122R;

/* renamed from: com.zopim.android.sdk.prechat.p */
class C1263p implements Runnable {

    /* renamed from: a */
    final /* synthetic */ C1262o f955a;

    C1263p(C1262o oVar) {
        this.f955a = oVar;
    }

    public void run() {
        this.f955a.f954a.mHandler.removeCallbacks(this.f955a.f954a.mShowSendTimeoutDialog);
        this.f955a.f954a.mProgressBar.setVisibility(8);
        Toast.makeText(this.f955a.f954a.getActivity(), C1122R.string.offline_sent_confirmation_message, 1).show();
        this.f955a.f954a.mChat.endChat();
        this.f955a.f954a.close();
        this.f955a.f954a.mChatListener.onChatEnded();
    }
}
