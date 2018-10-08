package com.zopim.android.sdk.prechat;

import android.app.AlertDialog.Builder;
import com.zopim.android.sdk.C0784R;

/* renamed from: com.zopim.android.sdk.prechat.l */
class C0889l implements Runnable {
    /* renamed from: a */
    final /* synthetic */ ZopimOfflineFragment f907a;

    C0889l(ZopimOfflineFragment zopimOfflineFragment) {
        this.f907a = zopimOfflineFragment;
    }

    public void run() {
        this.f907a.mProgressBar.setVisibility(8);
        this.f907a.mSendTimeoutDialog = new Builder(this.f907a.getActivity()).setMessage(C0784R.string.offline_message_send_failed).setPositiveButton(C0784R.string.offline_message_retry_button, new C0891n(this)).setNegativeButton(C0784R.string.offline_message_cancel_button, new C0890m(this)).show();
    }
}
