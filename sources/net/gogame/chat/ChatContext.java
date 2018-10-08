package net.gogame.chat;

import android.database.DataSetObserver;
import android.net.Uri;
import android.view.View;
import android.view.ViewGroup;
import java.io.File;

public interface ChatContext {

    public enum Rating {
        UNRATED,
        GOOD,
        BAD
    }

    AgentTypingEntry getAgentTypingEntry();

    Object getChatEntry(int i);

    int getChatEntryCount();

    View getView(Object obj, int i, View view, ViewGroup viewGroup);

    boolean isAttachmentSupported();

    void notifyDataSetChanged();

    void registerDataSetObserver(DataSetObserver dataSetObserver);

    void registerImage(String str, Uri uri);

    void send(File file);

    void send(String str);

    void send(Rating rating);

    void start();

    void stop();

    void unregisterDataSetObserver(DataSetObserver dataSetObserver);
}
