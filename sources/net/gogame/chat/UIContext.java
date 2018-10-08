package net.gogame.chat;

import android.content.BroadcastReceiver;
import android.content.IntentFilter;

public interface UIContext {
    void registerReceiver(BroadcastReceiver broadcastReceiver, IntentFilter intentFilter);

    void showImage(String str);

    void unregisterReceiver(BroadcastReceiver broadcastReceiver);
}
