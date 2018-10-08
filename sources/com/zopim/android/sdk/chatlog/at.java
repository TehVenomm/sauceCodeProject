package com.zopim.android.sdk.chatlog;

import android.util.Log;
import com.zopim.android.sdk.api.Logger;
import com.zopim.android.sdk.attachment.ImagePicker.Callback;
import java.io.File;
import java.util.List;

class at implements Callback {
    /* renamed from: a */
    final /* synthetic */ ZopimChatLogFragment f795a;

    at(ZopimChatLogFragment zopimChatLogFragment) {
        this.f795a = zopimChatLogFragment;
    }

    public void onSuccess(List<File> list) {
        if (list == null) {
            Log.i(ZopimChatLogFragment.LOG_TAG, "No files selected for upload.");
            return;
        }
        Logger.m562i(ZopimChatLogFragment.LOG_TAG, "Sending " + list.size());
        for (File send : list) {
            this.f795a.mChat.send(send);
        }
    }
}
