package com.zopim.android.sdk.prechat;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: com.zopim.android.sdk.prechat.n */
class C0892n implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ C0890l f909a;

    C0892n(C0890l c0890l) {
        this.f909a = c0890l;
    }

    public void onClick(DialogInterface dialogInterface, int i) {
        this.f909a.f907a.sendOfflineMessage();
    }
}
