package com.zopim.android.sdk.chatlog;

import android.util.Log;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.attachment.ImagePicker.Callback;
import java.io.File;
import java.util.List;

/* renamed from: com.zopim.android.sdk.chatlog.at */
class C1198at implements Callback {

    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f839a;

    C1198at(ZopimChatLogFragment zopimChatLogFragment) {
        this.f839a = zopimChatLogFragment;
    }

    public void onSuccess(List<File> list) {
        if (list == null) {
            Log.i(ZopimChatLogFragment.LOG_TAG, "No files selected for upload.");
            return;
        }
        Logger.m575i(ZopimChatLogFragment.LOG_TAG, "Sending " + list.size());
        for (File send : list) {
            this.f839a.mChat.send(send);
        }
    }
}
