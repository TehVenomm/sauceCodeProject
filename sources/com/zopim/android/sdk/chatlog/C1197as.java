package com.zopim.android.sdk.chatlog;

import android.view.View;
import android.view.View.OnClickListener;
import com.zopim.android.sdk.attachment.p016ui.AttachmentSourceSelectorDialog;

/* renamed from: com.zopim.android.sdk.chatlog.as */
class C1197as implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f838a;

    C1197as(ZopimChatLogFragment zopimChatLogFragment) {
        this.f838a = zopimChatLogFragment;
    }

    public void onClick(View view) {
        AttachmentSourceSelectorDialog.showDialog(this.f838a.getChildFragmentManager());
    }
}
