package com.zopim.android.sdk.chatlog;

import android.view.View;
import android.view.View.OnClickListener;
import com.zopim.android.sdk.attachment.ui.AttachmentSourceSelectorDialog;

class as implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f794a;

    as(ZopimChatLogFragment zopimChatLogFragment) {
        this.f794a = zopimChatLogFragment;
    }

    public void onClick(View view) {
        AttachmentSourceSelectorDialog.showDialog(this.f794a.getChildFragmentManager());
    }
}
