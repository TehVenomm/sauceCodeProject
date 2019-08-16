package com.zopim.android.sdk.chatlog;

import android.util.Patterns;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.EditText;
import com.zopim.android.sdk.C1122R;
import com.zopim.android.sdk.model.Profile;

/* renamed from: com.zopim.android.sdk.chatlog.ah */
class C1186ah implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ boolean f820a;

    /* renamed from: b */
    final /* synthetic */ Profile f821b;

    /* renamed from: c */
    final /* synthetic */ EditText f822c;

    /* renamed from: d */
    final /* synthetic */ ZopimChatLogFragment f823d;

    C1186ah(ZopimChatLogFragment zopimChatLogFragment, boolean z, Profile profile, EditText editText) {
        this.f823d = zopimChatLogFragment;
        this.f820a = z;
        this.f821b = profile;
        this.f822c = editText;
    }

    public void onClick(View view) {
        String trim = this.f820a ? this.f821b.getEmail() : this.f822c.getText().toString().trim();
        if (Patterns.EMAIL_ADDRESS.matcher(trim).matches()) {
            this.f823d.mChat.setEmail(trim);
            this.f823d.mChat.emailTranscript(trim);
            this.f823d.mChat.endChat();
            this.f823d.mEmailTranscriptDialog.dismiss();
            this.f823d.close();
            if (this.f823d.mChatListener != null) {
                this.f823d.mChatListener.onChatEnded();
                return;
            }
            return;
        }
        this.f822c.setError(this.f823d.getResources().getText(C1122R.string.email_transcript_email_message));
    }
}
