package com.zopim.android.sdk.chatlog;

import android.text.Editable;
import android.text.TextWatcher;

/* renamed from: com.zopim.android.sdk.chatlog.ag */
class C1185ag implements TextWatcher {

    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f819a;

    C1185ag(ZopimChatLogFragment zopimChatLogFragment) {
        this.f819a = zopimChatLogFragment;
    }

    public void afterTextChanged(Editable editable) {
        if (!this.f819a.canChat() || editable.length() <= 0) {
            this.f819a.mSendButton.setEnabled(false);
            this.f819a.mSendButton.setVisibility(8);
            this.f819a.mAttachButton.setVisibility(0);
        } else {
            this.f819a.mAttachButton.setVisibility(8);
            this.f819a.mSendButton.setVisibility(0);
            this.f819a.mSendButton.setEnabled(true);
        }
        this.f819a.mChat.resetTimeout();
    }

    public void beforeTextChanged(CharSequence charSequence, int i, int i2, int i3) {
    }

    public void onTextChanged(CharSequence charSequence, int i, int i2, int i3) {
    }
}
