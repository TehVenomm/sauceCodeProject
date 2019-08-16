package com.zopim.android.sdk.chatlog;

import android.text.Editable;
import android.text.TextWatcher;
import android.widget.Button;

/* renamed from: com.zopim.android.sdk.chatlog.ai */
class C1187ai implements TextWatcher {

    /* renamed from: a */
    final /* synthetic */ Button f824a;

    /* renamed from: b */
    final /* synthetic */ ZopimChatLogFragment f825b;

    C1187ai(ZopimChatLogFragment zopimChatLogFragment, Button button) {
        this.f825b = zopimChatLogFragment;
        this.f824a = button;
    }

    public void afterTextChanged(Editable editable) {
        if (editable.length() <= 0 || this.f825b.mNoConnection) {
            this.f824a.setEnabled(false);
        } else {
            this.f824a.setEnabled(true);
        }
    }

    public void beforeTextChanged(CharSequence charSequence, int i, int i2, int i3) {
    }

    public void onTextChanged(CharSequence charSequence, int i, int i2, int i3) {
    }
}
