package com.zopim.android.sdk.api;

import android.content.ComponentName;
import android.content.ServiceConnection;
import android.os.IBinder;
import android.util.Log;
import com.zopim.android.sdk.api.ChatService.LocalBinder;
import com.zopim.android.sdk.api.ZopimChat.ChatServiceBinder;

class ac implements ServiceConnection {
    /* renamed from: a */
    final /* synthetic */ ChatServiceBinder f626a;

    ac(ChatServiceBinder chatServiceBinder) {
        this.f626a = chatServiceBinder;
    }

    public void onServiceConnected(ComponentName componentName, IBinder iBinder) {
        ZopimChat.singleton.mChatService = ((LocalBinder) iBinder).getService();
        this.f626a.mBound = true;
        Log.d(ChatServiceBinder.LOG_TAG, "Connected to chat service");
        ZopimChat.singleton.resendUnsentMessages();
        ZopimChat.singleton.resendUnsentFiles();
    }

    public void onServiceDisconnected(ComponentName componentName) {
        this.f626a.mBound = false;
        ZopimChat.singleton.mChatService = null;
        Log.d(ChatServiceBinder.LOG_TAG, "Disconnected from chat service");
    }
}
