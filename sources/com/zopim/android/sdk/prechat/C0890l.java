package com.zopim.android.sdk.prechat;

import android.app.AlertDialog.Builder;
import com.zopim.android.sdk.C0785R;

/* renamed from: com.zopim.android.sdk.prechat.l */
class C0890l implements Runnable {
    /* renamed from: a */
    final /* synthetic */ ZopimOfflineFragment f907a;

    C0890l(ZopimOfflineFragment zopimOfflineFragment) {
        this.f907a = zopimOfflineFragment;
    }

    public void run() {
        this.f907a.mProgressBar.setVisibility(8);
        this.f907a.mSendTimeoutDialog = new Builder(this.f907a.getActivity()).setMessage(C0785R.string.offline_message_send_failed).setPositiveButton(C0785R.string.offline_message_retry_button, new C0892n(this)).setNegativeButton(C0785R.string.offline_message_cancel_button, new C0891m(this)).show();
    }
}
