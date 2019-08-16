package com.zopim.android.sdk.prechat;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;

/* renamed from: com.zopim.android.sdk.prechat.n */
class C1261n implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ C1259l f953a;

    C1261n(C1259l lVar) {
        this.f953a = lVar;
    }

    public void onClick(DialogInterface dialogInterface, int i) {
        this.f953a.f951a.sendOfflineMessage();
    }
}
