package com.zopim.android.sdk.chatlog;

import android.text.Editable;
import android.text.TextWatcher;
import android.widget.Button;

class ai implements TextWatcher {
    /* renamed from: a */
    final /* synthetic */ Button f780a;
    /* renamed from: b */
    final /* synthetic */ ZopimChatLogFragment f781b;

    ai(ZopimChatLogFragment zopimChatLogFragment, Button button) {
        this.f781b = zopimChatLogFragment;
        this.f780a = button;
    }

    public void afterTextChanged(Editable editable) {
        if (editable.length() <= 0 || this.f781b.mNoConnection) {
            this.f780a.setEnabled(false);
        } else {
            this.f780a.setEnabled(true);
        }
    }

    public void beforeTextChanged(CharSequence charSequence, int i, int i2, int i3) {
    }

    public void onTextChanged(CharSequence charSequence, int i, int i2, int i3) {
    }
}
