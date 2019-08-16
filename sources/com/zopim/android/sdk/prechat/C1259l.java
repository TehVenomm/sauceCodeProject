package com.zopim.android.sdk.prechat;

import android.app.AlertDialog.Builder;
import com.zopim.android.sdk.C1122R;

/* renamed from: com.zopim.android.sdk.prechat.l */
class C1259l implements Runnable {

    /* renamed from: a */
    final /* synthetic */ ZopimOfflineFragment f951a;

    C1259l(ZopimOfflineFragment zopimOfflineFragment) {
        this.f951a = zopimOfflineFragment;
    }

    public void run() {
        this.f951a.mProgressBar.setVisibility(8);
        this.f951a.mSendTimeoutDialog = new Builder(this.f951a.getActivity()).setMessage(C1122R.string.offline_message_send_failed).setPositiveButton(C1122R.string.offline_message_retry_button, new C1261n(this)).setNegativeButton(C1122R.string.offline_message_cancel_button, new C1260m(this)).show();
    }
}
