package com.zopim.android.sdk.data.observers;

import android.util.Log;
import com.zopim.android.sdk.data.LivechatChatLogPath;
import com.zopim.android.sdk.model.ChatLog;
import java.util.LinkedHashMap;
import java.util.Observable;
import java.util.Observer;

public abstract class ChatLogObserver implements Observer {
    private static final String LOG_TAG = ChatLogObserver.class.getSimpleName();

    public abstract void update(LinkedHashMap<String, ChatLog> linkedHashMap);

    public final void update(Observable observable, Object obj) {
        if (!(observable instanceof LivechatChatLogPath)) {
            Log.i(LOG_TAG, "Unexpected broadcast observable " + observable + " Observable should be of type " + LivechatChatLogPath.class);
        } else if (obj instanceof LinkedHashMap) {
            update((LinkedHashMap) obj);
        } else {
            Log.i(LOG_TAG, "Unexpected broadcast object " + obj + " Broadcast object should be of type " + LinkedHashMap.class);
        }
    }
}
