package com.zopim.android.sdk.chatlog;

import android.util.Patterns;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.EditText;
import com.zopim.android.sdk.C0784R;
import com.zopim.android.sdk.model.Profile;

class ah implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ boolean f776a;
    /* renamed from: b */
    final /* synthetic */ Profile f777b;
    /* renamed from: c */
    final /* synthetic */ EditText f778c;
    /* renamed from: d */
    final /* synthetic */ ZopimChatLogFragment f779d;

    ah(ZopimChatLogFragment zopimChatLogFragment, boolean z, Profile profile, EditText editText) {
        this.f779d = zopimChatLogFragment;
        this.f776a = z;
        this.f777b = profile;
        this.f778c = editText;
    }

    public void onClick(View view) {
        String email = this.f776a ? this.f777b.getEmail() : this.f778c.getText().toString().trim();
        if (Patterns.EMAIL_ADDRESS.matcher(email).matches()) {
            this.f779d.mChat.setEmail(email);
            this.f779d.mChat.emailTranscript(email);
            this.f779d.mChat.endChat();
            this.f779d.mEmailTranscriptDialog.dismiss();
            this.f779d.close();
            if (this.f779d.mChatListener != null) {
                this.f779d.mChatListener.onChatEnded();
                return;
            }
            return;
        }
        this.f778c.setError(this.f779d.getResources().getText(C0784R.string.email_transcript_email_message));
    }
}
