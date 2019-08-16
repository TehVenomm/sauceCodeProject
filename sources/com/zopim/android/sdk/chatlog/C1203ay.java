package com.zopim.android.sdk.chatlog;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.util.Patterns;
import com.zopim.android.sdk.model.Profile;

/* renamed from: com.zopim.android.sdk.chatlog.ay */
class C1203ay implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ Profile f844a;

    /* renamed from: b */
    final /* synthetic */ ZopimChatLogFragment f845b;

    C1203ay(ZopimChatLogFragment zopimChatLogFragment, Profile profile) {
        this.f845b = zopimChatLogFragment;
        this.f844a = profile;
    }

    public void onClick(DialogInterface dialogInterface, int i) {
        String email = this.f844a.getEmail();
        if (Patterns.EMAIL_ADDRESS.matcher(email).matches()) {
            this.f845b.mChat.emailTranscript(email);
            this.f845b.mChat.endChat();
            this.f845b.mEmailTranscriptDialog.dismiss();
            this.f845b.close();
            if (this.f845b.mChatListener != null) {
                this.f845b.mChatListener.onChatEnded();
            }
        }
    }
}
