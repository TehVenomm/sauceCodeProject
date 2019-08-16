package net.gogame.chat;

import android.content.Context;
import android.database.DataSetObserver;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import com.zopim.android.sdk.C1122R;
import p017io.fabric.sdk.android.services.common.AbstractSpiCall;

public class ChatAdapter extends BaseAdapter {
    private final ChatContext chatContext;
    private final Context context;
    private final DataSetObserver dataSetObserver = new DataSetObserver() {
        public void onChanged() {
            ChatAdapter.this.notifyDataSetChanged();
        }
    };
    private final UIContext uiContext;
    private final ChatAdapterViewFactory viewFactory;

    public ChatAdapter(Context context2, UIContext uIContext, ChatAdapterViewFactory chatAdapterViewFactory, ChatContext chatContext2) {
        this.context = context2;
        this.uiContext = uIContext;
        this.viewFactory = chatAdapterViewFactory;
        this.chatContext = chatContext2;
    }

    public ChatContext getChatContext() {
        return this.chatContext;
    }

    public void start() {
        this.chatContext.registerDataSetObserver(this.dataSetObserver);
    }

    public void stop() {
        this.chatContext.unregisterDataSetObserver(this.dataSetObserver);
    }

    public int getCount() {
        return this.chatContext.getChatEntryCount() + 1;
    }

    public Object getItem(int i) {
        if (i < 0) {
            return null;
        }
        if (i < this.chatContext.getChatEntryCount()) {
            return this.chatContext.getChatEntry(i);
        }
        if (i == this.chatContext.getChatEntryCount()) {
            return this.chatContext.getAgentTypingEntry();
        }
        return null;
    }

    public long getItemId(int i) {
        return (long) (i + AbstractSpiCall.DEFAULT_TIMEOUT);
    }

    public View getView(int i, View view, ViewGroup viewGroup) {
        Object item = getItem(i);
        if (!(item instanceof AgentTypingEntry)) {
            View view2 = this.chatContext.getView(item, i, view, viewGroup);
            if (view2 == null) {
                return this.viewFactory.getEmptyView(view2, viewGroup);
            }
            return view2;
        } else if (((AgentTypingEntry) item).isTyping()) {
            return this.viewFactory.getNotificationView(view, viewGroup, this.context.getResources().getString(C1122R.string.net_gogame_chat_agent_typing_message));
        } else {
            return this.viewFactory.getNotificationView(view, viewGroup, null, true);
        }
    }
}
