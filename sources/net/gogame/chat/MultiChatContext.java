package net.gogame.chat;

import android.database.DataSetObserver;
import android.view.View;
import android.view.ViewGroup;
import java.io.File;
import java.util.ArrayList;
import java.util.List;
import net.gogame.chat.ChatContext.Rating;

public class MultiChatContext extends AbstractChatContext {
    private final AgentTypingEntry agentTypingEntry;
    private final List<ChatContext> chatContexts;
    private ChatContext currentChatContext;
    private final DataSetObserver dataSetObserver;
    private final Listener listener;

    /* renamed from: net.gogame.chat.MultiChatContext$1 */
    class C10111 extends DataSetObserver {
        C10111() {
        }

        public void onChanged() {
            super.onChanged();
            MultiChatContext.this.notifyDataSetChanged();
        }
    }

    public interface Listener {
        void onChatContextAdded(ChatContext chatContext);
    }

    public MultiChatContext() {
        this.chatContexts = new ArrayList();
        this.currentChatContext = null;
        this.agentTypingEntry = new AgentTypingEntry(false);
        this.dataSetObserver = new C10111();
        this.listener = null;
    }

    public MultiChatContext(Listener listener) {
        this.chatContexts = new ArrayList();
        this.currentChatContext = null;
        this.agentTypingEntry = new AgentTypingEntry(false);
        this.dataSetObserver = new C10111();
        this.listener = listener;
    }

    public ChatContext getCurrentChatContext() {
        return this.currentChatContext;
    }

    public void start() {
        for (ChatContext start : this.chatContexts) {
            start.start();
        }
    }

    public void stop() {
        for (ChatContext stop : this.chatContexts) {
            stop.stop();
        }
    }

    public void addChatContext(ChatContext chatContext) {
        chatContext.registerDataSetObserver(this.dataSetObserver);
        this.chatContexts.add(chatContext);
        this.currentChatContext = chatContext;
        if (this.listener != null) {
            this.listener.onChatContextAdded(chatContext);
        }
    }

    public boolean isAttachmentSupported() {
        if (this.currentChatContext != null) {
            return this.currentChatContext.isAttachmentSupported();
        }
        return false;
    }

    public int getChatEntryCount() {
        int i = 0;
        for (ChatContext chatEntryCount : this.chatContexts) {
            i = chatEntryCount.getChatEntryCount() + i;
        }
        return i;
    }

    public Object getChatEntry(int i) {
        int i2 = 0;
        for (ChatContext chatContext : this.chatContexts) {
            int i3 = i - i2;
            if (i3 < chatContext.getChatEntryCount()) {
                return chatContext.getChatEntry(i3);
            }
            i2 = chatContext.getChatEntryCount() + i2;
        }
        return null;
    }

    public AgentTypingEntry getAgentTypingEntry() {
        if (this.currentChatContext != null) {
            return this.currentChatContext.getAgentTypingEntry();
        }
        return this.agentTypingEntry;
    }

    public void send(String str) {
        this.currentChatContext.send(str);
    }

    public void send(File file) {
        if (this.currentChatContext != null) {
            this.currentChatContext.send(file);
        }
    }

    public void send(Rating rating) {
        if (this.currentChatContext != null) {
            this.currentChatContext.send(rating);
        }
    }

    public View getView(Object obj, int i, View view, ViewGroup viewGroup) {
        int i2 = 0;
        for (ChatContext chatContext : this.chatContexts) {
            int i3 = i - i2;
            if (i3 < chatContext.getChatEntryCount()) {
                return chatContext.getView(obj, i3, view, viewGroup);
            }
            i2 = chatContext.getChatEntryCount() + i2;
        }
        return null;
    }
}
