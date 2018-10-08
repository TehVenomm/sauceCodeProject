package com.zopim.android.sdk.chatlog;

import android.text.Editable;
import android.text.TextWatcher;

class ag implements TextWatcher {
    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f775a;

    ag(ZopimChatLogFragment zopimChatLogFragment) {
        this.f775a = zopimChatLogFragment;
    }

    public void afterTextChanged(Editable editable) {
        if (!this.f775a.canChat() || editable.length() <= 0) {
            this.f775a.mSendButton.setEnabled(false);
            this.f775a.mSendButton.setVisibility(8);
            this.f775a.mAttachButton.setVisibility(0);
        } else {
            this.f775a.mAttachButton.setVisibility(8);
            this.f775a.mSendButton.setVisibility(0);
            this.f775a.mSendButton.setEnabled(true);
        }
        this.f775a.mChat.resetTimeout();
    }

    public void beforeTextChanged(CharSequence charSequence, int i, int i2, int i3) {
    }

    public void onTextChanged(CharSequence charSequence, int i, int i2, int i3) {
    }
}
