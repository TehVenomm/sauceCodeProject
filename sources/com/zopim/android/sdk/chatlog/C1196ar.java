package com.zopim.android.sdk.chatlog;

import android.view.View;
import android.view.View.OnClickListener;

/* renamed from: com.zopim.android.sdk.chatlog.ar */
class C1196ar implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f837a;

    C1196ar(ZopimChatLogFragment zopimChatLogFragment) {
        this.f837a = zopimChatLogFragment;
    }

    public void onClick(View view) {
        String trim = this.f837a.mInputField.getText().toString().trim();
        if (!trim.isEmpty()) {
            this.f837a.mChat.send(trim);
            this.f837a.mInputField.setText("");
        }
    }
}
