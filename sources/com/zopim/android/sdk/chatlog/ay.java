package com.zopim.android.sdk.chatlog;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.util.Patterns;
import com.zopim.android.sdk.model.Profile;

class ay implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ Profile f800a;
    /* renamed from: b */
    final /* synthetic */ ZopimChatLogFragment f801b;

    ay(ZopimChatLogFragment zopimChatLogFragment, Profile profile) {
        this.f801b = zopimChatLogFragment;
        this.f800a = profile;
    }

    public void onClick(DialogInterface dialogInterface, int i) {
        Object email = this.f800a.getEmail();
        if (Patterns.EMAIL_ADDRESS.matcher(email).matches()) {
            this.f801b.mChat.emailTranscript(email);
            this.f801b.mChat.endChat();
            this.f801b.mEmailTranscriptDialog.dismiss();
            this.f801b.close();
            if (this.f801b.mChatListener != null) {
                this.f801b.mChatListener.onChatEnded();
            }
        }
    }
}
