package com.zopim.android.sdk.prechat;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: com.zopim.android.sdk.prechat.n */
class C0891n implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ C0889l f909a;

    C0891n(C0889l c0889l) {
        this.f909a = c0889l;
    }

    public void onClick(DialogInterface dialogInterface, int i) {
        this.f909a.f907a.sendOfflineMessage();
    }
}
