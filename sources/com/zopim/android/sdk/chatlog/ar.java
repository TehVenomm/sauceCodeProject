package com.zopim.android.sdk.chatlog;

import android.view.View;
import android.view.View.OnClickListener;

class ar implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f793a;

    ar(ZopimChatLogFragment zopimChatLogFragment) {
        this.f793a = zopimChatLogFragment;
    }

    public void onClick(View view) {
        String trim = this.f793a.mInputField.getText().toString().trim();
        if (!trim.isEmpty()) {
            this.f793a.mChat.send(trim);
            this.f793a.mInputField.setText("");
        }
    }
}
